using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.IO;
using System.Xml.Serialization;
using System.Xml;

namespace DatAdmin
{
    public enum SettingsTargets
    {
        Global = 1,
        Dialect = 2,
        Connection = 4,
        Database = 8,
        All = Global | Dialect | Connection | Database
    }

    public class SettingsPageStruct
    {
        public SettingsPageAttribute Attribute;
        public SettingsPageBase SettingsPage;
    }

    internal class SettingsKeyStruct
    {
        public object SettingsPage;
        public PropertyInfo Property;
    }

    public class SettingsPageCollection
    {
        Dictionary<string, SettingsPageStruct> m_settings = new Dictionary<string, SettingsPageStruct>();
        Dictionary<string, SettingsKeyStruct> m_keys = new Dictionary<string, SettingsKeyStruct>();
        SettingsPageCollection m_basePages;

        static Dictionary<string, SettingsPageCollection> m_settingsByFile = new Dictionary<string, SettingsPageCollection>();

        string m_filename;
        static SettingsPageCollection m_root;

        List<SettingsPageCollection> m_children = new List<SettingsPageCollection>();

        public static SettingsPageCollection Root
        {
            get
            {
                if (m_root == null) m_root = new SettingsPageCollection();
                return m_root;
            }
        }

        public string Filename
        {
            get { return m_filename; }
        }

        bool m_loaded = false;

        internal void Unload()
        {
            m_loaded = false;
            foreach (var col in m_children) col.Unload();
        }

        public void SetBasePages(SettingsPageCollection basePages)
        {
            if (basePages == this) throw new InternalError("DAE-00075 GlobalSettings.SetBasePages: basePages == this");
            Unload();
            m_basePages = basePages;
        }

        public void WantLoaded()
        {
            if (!m_loaded)
            {
                FillFromParent();
                if (File.Exists(Filename))
                {
                    XmlDocument doc = new XmlDocument();
                    doc.Load(Filename);
                    LoadSettings(doc.DocumentElement);
                }
                m_loaded = true;
            }
        }

        private void FillFromParent()
        {
            if (m_basePages == null) return;
            m_basePages.WantLoaded();
            foreach (var page in m_settings.Values)
            {
                if (!m_basePages.m_settings.ContainsKey(page.Attribute.Name)) continue;
                var pageBase = m_basePages.m_settings[page.Attribute.Name];
                SettingsTool.CopySettingsPage(pageBase.SettingsPage, page.SettingsPage);
            }
        }

        public void DirectModify(Dictionary<string, string> dct)
        {
            BeginEdit();
            EndEdit(dct);
            Unload();
        }

        public void BeginEdit()
        {
            Unload();
            WantLoaded();
        }

        private void EndEdit(Dictionary<string, string> dct)
        {
            XmlDocument doc = XmlTool.CreateDocument("Settings");
            SaveSettings(doc.DocumentElement, m_basePages);
            if (dct != null)
            {
                // modify settings before save
                foreach (var tuple in dct)
                {
                    string name = tuple.Key;
                    XmlElement node = doc.DocumentElement.SelectSingleNode("Param[@name='" + name + "']") as XmlElement;
                    if (node != null)
                    {
                        node.SetAttribute("value", tuple.Value);
                    }
                    else
                    {
                        var p = doc.DocumentElement.AddChild("Param");
                        p.SetAttribute("name", name);
                        p.SetAttribute("value", tuple.Value);
                    }
                }
            }
            string dir = Path.GetDirectoryName(Filename);
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            doc.Save(Filename);
            Unload();
        }

        public void EndEdit()
        {
            EndEdit(null);
        }

        // constructor for root
        private SettingsPageCollection()
            : this(null, null)
        {
            SetDefaults();
        }

        public static SettingsPageCollection Get(SettingsPageCollection basePages, string filename)
        {
            lock (m_settingsByFile)
            {
                if (m_settingsByFile.ContainsKey(filename)) return m_settingsByFile[filename];
                m_settingsByFile[filename] = new SettingsPageCollection(basePages, filename);
                return m_settingsByFile[filename];
            }
        }

        public static SettingsPageCollection Get(string filename)
        {
            lock (m_settingsByFile)
            {
                if (m_settingsByFile.ContainsKey(filename)) return m_settingsByFile[filename];
                m_settingsByFile[filename] = new SettingsPageCollection(filename);
                return m_settingsByFile[filename];
            }
        }

        private SettingsPageCollection(string filename) : this(Root, filename) { }

        private SettingsPageCollection(SettingsPageCollection basePages, string filename)
        {
            m_basePages = basePages;
            if (m_basePages != null) m_basePages.m_children.Add(this);
            m_filename = filename;
            m_loaded = false;

            foreach (var item in SettingsPageAddonType.Instance.StaticSpace.GetAllAddons())
            {
                m_settings[item.Name] = new SettingsPageStruct
                {
                    Attribute = (SettingsPageAttribute)item.Attrib,
                    SettingsPage = (SettingsPageBase)item.CreateInstance()
                };
                m_settings[item.Name].SettingsPage.Pages = this;
            }
            foreach (var page in m_settings.Values)
            {
                foreach (PropertyInfo prop in page.SettingsPage.GetType().GetProperties())
                {
                    foreach (SettingsKeyAttribute attr in prop.GetCustomAttributes(typeof(SettingsKeyAttribute), true))
                    {
                        m_keys[attr.KeyName] = new SettingsKeyStruct { Property = prop, SettingsPage = page.SettingsPage };
                    }
                }
            }
        }

        private void LoadSettings(XmlElement xml)
        {
            foreach (string key in m_keys.Keys)
            {
                XmlElement px = xml.SelectSingleNode("Param[@name='" + key + "']") as XmlElement;
                if (px == null) continue;
                var p = m_keys[key];
                try
                {
                    p.Property.CallSet(p.SettingsPage, XmlTool.PropertyFromString(p.Property, px.GetAttribute("value")));
                }
                catch (Exception err)
                {
                    Logging.Warning("Error loading settings property {0} (value={1}): {2}", p.Property, px.GetAttribute("value"), err.Message);
                }
            }
        }

        private void SaveSettings(XmlElement xml, SettingsPageCollection diffBase)
        {
            foreach (var page in m_settings.Values)
            {
                var pageBase = diffBase.m_settings[page.Attribute.Name];
                foreach (PropertyInfo prop in pageBase.SettingsPage.GetType().GetProperties())
                {
                    foreach (SettingsKeyAttribute attr in prop.GetCustomAttributes(typeof(SettingsKeyAttribute), true))
                    {
                        object myvalue = prop.CallGet(page.SettingsPage), baseValue = prop.CallGet(pageBase.SettingsPage);
                        if (myvalue != null && !myvalue.Equals(baseValue))
                        {
                            XmlElement parx = xml.AddChild("Param");
                            parx.SetAttribute("name", attr.KeyName);
                            parx.SetAttribute("value", XmlTool.PropertyToString(prop, myvalue));
                        }
                    }
                }
            }
        }

        public IEnumerable<SettingsPageStruct> SettingsPages
        {
            get
            {
                WantLoaded();
                return m_settings.Values;
            }
        }

        public SettingsPageBase PageByName(string name)
        {
            WantLoaded();
            return m_settings[name].SettingsPage;
        }

        private void SetDefaults()
        {
            foreach (var pg in m_settings.Values)
            {
                pg.SettingsPage.SetDefaults();
            }
        }
    }

    public static class SettingsTool
    {
        public static void CopySettingsPage(SettingsPageBase fromPage, SettingsPageBase toPage)
        {
            foreach (PropertyInfo prop in fromPage.GetType().GetProperties())
            {
                foreach (SettingsKeyAttribute attr in prop.GetCustomAttributes(typeof(SettingsKeyAttribute), true))
                {
                    prop.CallSet(toPage, prop.CallGet(fromPage));
                }
            }
        }
        public static T CreateCopy<T>(this T page) where T : SettingsPageBase, new()
        {
            T res = new T();
            if (page != null) CopySettingsPage(page, res);
            return res;
        }
        public static bool SettingsEqual(SettingsPageBase a, SettingsPageBase b)
        {
            if (a.GetType() != b.GetType()) return false;
            foreach (PropertyInfo prop in a.GetType().GetProperties())
            {
                foreach (SettingsKeyAttribute attr in prop.GetCustomAttributes(typeof(SettingsKeyAttribute), true))
                {
                    object vala = prop.CallGet(a), valb = prop.CallGet(b);
                    if (!Object.Equals(vala, valb)) return false;
                }
            }
            return true;
        }
        public static void SaveSettingsPage(SettingsPageBase page, SettingsPageBase pageBase, XmlElement xml)
        {
            foreach (PropertyInfo prop in page.GetType().GetProperties())
            {
                foreach (SettingsKeyAttribute attr in prop.GetCustomAttributes(typeof(SettingsKeyAttribute), true))
                {
                    object myvalue = prop.CallGet(page), baseValue = null;
                    if (pageBase != null) baseValue = prop.CallGet(pageBase);
                    if (myvalue != null && (baseValue == null || !myvalue.Equals(baseValue)))
                    {
                        XmlElement parx = xml.AddChild("Param");
                        parx.SetAttribute("name", attr.KeyName);
                        parx.SetAttribute("value", XmlTool.ValueToString(prop.PropertyType, myvalue));
                    }
                }
            }
        }
        public static void LoadSettingsPage(SettingsPageBase page, XmlElement xml)
        {
            foreach (PropertyInfo prop in page.GetType().GetProperties())
            {
                foreach (SettingsKeyAttribute attr in prop.GetCustomAttributes(typeof(SettingsKeyAttribute), true))
                {
                    XmlElement node = xml.SelectSingleNode(String.Format("Param[@name=\"{0}\"]", attr.KeyName)) as XmlElement;
                    if (node != null)
                    {
                        object value = XmlTool.ValueFromString(prop.PropertyType, node.GetAttribute("value"));
                        prop.CallSet(page, value);
                    }
                }
            }
        }
    }
}
