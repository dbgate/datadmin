using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Xml;

namespace DatAdmin
{
    public abstract class AddonBase : PropertyPageBase, IAddonInstance 
    {
        public static IAddonInstance Load(XmlElement xml)
        {
            AddonType adtype = AddonRegister.Instance.FindAddonType(xml.GetAttribute("adtype"));
            return adtype.LoadAddon(xml);
        }

        public static IAddonInstance LoadFromFile(string file)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(file);
            return Load(doc.DocumentElement);
        }

        public virtual void LoadFromXml(XmlElement xml)
        {
            this.LoadPropertiesCore(xml);
        }

        public virtual void SaveToXml(XmlElement xml)
        {
            this.SavePropertiesCore(xml, true);
            xml.SetAttribute("adtype", AddonType.Name);
        }

        public void SaveToFile(string file)
        {
            AddonInstanceExtension.SaveToFile(this, file);
        }

        [Browsable(false)]
        public abstract AddonType AddonType { get; }
    }

    public sealed class NamedAddonInstance
    {
        public IAddonInstance Instance { get; set; }
        [XmlAttrib("name")]
        public string Name { get; set; }

        public NamedAddonInstance(IAddonInstance instance, string name)
        {
            Instance = instance;
            Name = name;
        }

        public NamedAddonInstance(XmlElement xml, AddonType type)
        {
            this.LoadPropertiesCore(xml);
            Instance = type.LoadAddon(xml);
        }

        public void SaveToXml(XmlElement xml)
        {
            this.SavePropertiesCore(xml);
            Instance.SaveToXml(xml);
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class NamedAddonCollection : List<NamedAddonInstance>, IExplicitXmlPersistent
    {
        AddonType m_addonType;

        public NamedAddonCollection(AddonType type)
        {
            m_addonType = type;
        }

        #region IExplicitXmlPersistent Members

        public void SaveToXml(XmlElement xml)
        {
            foreach (var item in this)
            {
                item.SaveToXml(xml.AddChild("Item"));
            }
        }

        public void LoadFromXml(XmlElement xml)
        {
            Clear();
            foreach (XmlElement x in xml.SelectNodes("Item"))
            {
                Add(new NamedAddonInstance(x, AddonType));
            }
        }

        #endregion

        public AddonType AddonType { get { return m_addonType; } }
    }

    public class AddonCollection : List<IAddonInstance>, IExplicitXmlPersistent
    {
        AddonType m_addonType;

        public AddonCollection(AddonType type)
        {
            m_addonType = type;
        }

        #region IExplicitXmlPersistent Members

        public void SaveToXml(XmlElement xml)
        {
            foreach (var item in this)
            {
                item.SaveToXml(xml.AddChild("Item"));
            }
        }

        public void LoadFromXml(XmlElement xml)
        {
            Clear();
            foreach (XmlElement x in xml.SelectNodes("Item"))
            {
                Add(AddonType.LoadAddon(x));
            }
        }

        #endregion

        public AddonType AddonType { get { return m_addonType; } }
    }

    public class TAddonCollection<T> : AddonCollection, IEnumerable<T>
        where T : IAddonInstance
    {
        public TAddonCollection()
            : base(AddonRegister.Instance.FindAddonType(typeof(T)))
        {
        }

        public T this[int index]
        {
            get
            {
                return (T)base[index];
            }
            set
            {
                base[index] = value;
            }
        }

        #region IEnumerable<T> Members

        public new IEnumerator<T> GetEnumerator()
        {
            foreach (var item in (IEnumerable<NamedAddonInstance>)this)
            {
                yield return (T)item.Instance;
            }
        }

        #endregion
    }

    public static class AddonInstanceExtension
    {
        public static void SaveToFile(this IAddonInstance addon, string file)
        {
            XmlDocument doc = XmlTool.CreateDocument("Addon");
            addon.SaveToXml(doc.DocumentElement);
            doc.Save(file);
        }
        public static void LoadFromFile(this IAddonInstance addon, string file)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(file);
            addon.LoadFromXml(doc.DocumentElement);
        }
    }
}
