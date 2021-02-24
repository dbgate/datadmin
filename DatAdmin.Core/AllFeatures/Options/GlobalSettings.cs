using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.IO;
using System.Xml.Serialization;
using System.Xml;

namespace DatAdmin
{

    public static class GlobalSettings
    {
        public static SettingsPageCollection Pages;
        public static event Action OnChange;
        public static Dictionary<string, SettingsPageCollection> DialectSettings = new Dictionary<string, SettingsPageCollection>();
        public static string SettingsFileName = Path.Combine(Core.SettingsDirectory, "global.xml");

        public static void DispatchChanged()
        {
            if (OnChange != null) OnChange();
        }

        public static void Initialize()
        {
            Pages = SettingsPageCollection.Get(SettingsFileName);
            foreach (var item in DialectAddonType.Instance.StaticSpace.GetAllAddons())
            {
                DialectSettings[item.Name] = SettingsPageCollection.Get(Pages, Path.Combine(Core.SettingsDirectory, "dialect-" + item.Name + ".xml"));
            }
        }
    }

    //public class SettingsPageRegister : RegisterBase
    //{
    //    private SettingsPageRegister()
    //        : base(typeof(SettingsPageAttribute), null)
    //    {
    //    }
    //    public static readonly SettingsPageRegister Instance = new SettingsPageRegister();

    //    //public static string[] GetSettingsNames()
    //    //{
    //    //    List<string> names = new List<string>();
    //    //    names.AddRange(m_settings.Keys);
    //    //    return names.ToArray();
    //    //}

    //    //public static object GetSettings(string name)
    //    //{
    //    //    if (m_settings == null) return null;
    //    //    if (!m_settings.ContainsKey(name)) return null;
    //    //    return m_settings[name].CfgObject;
    //    //}
    //    //public static string GetSettingsFile(string name) { return Path.Combine(Core.SettingsDirectory, m_settings[name].Attribute.Filename); }

    //    //public static void LoadAll()
    //    //{
    //    //    foreach (string name in GetSettingsNames())
    //    //    {
    //    //        Load(name);
    //    //    }
    //    //}

    //    //public static void Load(string name)
    //    //{
    //    //    XmlSerializer ser = new XmlSerializer(m_settings[name].Type);
    //    //    try
    //    //    {
    //    //        using (FileStream fr = new FileStream(GetSettingsFile(name), FileMode.Open, FileAccess.Read, FileShare.Read))
    //    //        {
    //    //            m_settings[name].CfgObject = ser.Deserialize(fr);
    //    //        }
    //    //    }
    //    //    catch (FileNotFoundException)
    //    //    {
    //    //        Logging.Warning("Settings file {0} not found", GetSettingsFile(name));
    //    //        try
    //    //        {
    //    //            ((IGlobalSettings)m_settings[name].CfgObject).SetDefaults();
    //    //        }
    //    //        catch (Exception e)
    //    //        {
    //    //            Errors.Report(e);
    //    //        }
    //    //    }
    //    //    catch (Exception e)
    //    //    {
    //    //        Errors.Report(e);
    //    //    }
    //    //}

    //    //public static void Save(string name)
    //    //{
    //    //    XmlSerializer ser = new XmlSerializer(m_settings[name].Type);
    //    //    using (FileStream fw = new FileStream(GetSettingsFile(name), FileMode.Create))
    //    //    {
    //    //        ser.Serialize(fw, m_settings[name].CfgObject);
    //    //    }
    //    //}

    //    //public static void SaveAll()
    //    //{
    //    //    foreach (string name in GetSettingsNames())
    //    //    {
    //    //        Save(name);
    //    //    }
    //    //}

    //}

}
