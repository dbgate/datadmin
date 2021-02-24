using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Xml.Serialization;
using System.Security.Cryptography;
using System.Collections;

namespace DatAdmin
{
    public class UICacheItem
    {
        public string Key;
        public XmlDocument XmlValue;
        public DateTime LastUsedAt;
        public DateTime LastUpdatedAt;
        private object m_value;

        [XmlIgnore]
        public object Value
        {
            get
            {
                if (m_value == null) m_value = XmlTool.DeserializeObject(XmlValue);
                return m_value;
            }
            set
            {
                m_value = value;
                MemoryStream fw = new MemoryStream();
                XmlTool.SerializeObject(fw, m_value);
                MemoryStream fr = new MemoryStream(fw.ToArray());
                XmlValue = new XmlDocument();
                XmlValue.Load(fr);
            }
        }
    }

    public class UICache
    {
        [XmlIgnore]
        public Dictionary<string, UICacheItem> Items = new Dictionary<string, UICacheItem>();

        private List<UICacheItem> ItemsForLoading = new List<UICacheItem>();

        [XmlArray(ElementName = "Items")]
        public List<UICacheItem> ItemsForXml
        {
            get
            {
                if (Items.Count > 0)
                {
                    // for writing
                    List<UICacheItem> res = new List<UICacheItem>();
                    foreach (UICacheItem item in Items.Values)
                    {
                        TimeSpan delta = DateTime.UtcNow - item.LastUsedAt;
                        if (delta.TotalDays < GlobalSettings.Pages.Cache().DeleteAfterDays) res.Add(item);
                    }
                    return res;
                }
                return ItemsForLoading;
            }
            set
            {
                Items.Clear();
                foreach (UICacheItem item in value)
                {
                    Items[item.Key] = item;
                }
            }
        }

        public static UICache Instance = new UICache();

        public static string Filename { get { return Path.Combine(Core.ConfigDirectory, "cache.xml"); } }

        public static void Clear()
        {
            Instance.Items.Clear();
        }

        public static int Size { get { return Instance.Items.Count; } }

        public static void Load()
        {
            try
            {
                Instance = (UICache)XmlTool.DeserializeObject(Filename);
                if (Instance.ItemsForLoading.Count > 0 && Instance.Items.Count == 0)
                {
                    Instance.ItemsForXml = Instance.ItemsForLoading;
                    Instance.ItemsForLoading.Clear();
                }
            }
            catch (Exception)
            {
                Instance = new UICache();
            }
        }

        public static void Save()
        {
            try
            {
                if (GlobalSettings.Pages.Cache().CacheMode == CacheMode.UsePersistent)
                {
                    XmlTool.SerializeObject(Filename, Instance);
                }
            }
            catch (Exception err)
            {
                Logging.Warning("Error saving UI cache:" + err.Message);
            }
        }

        public static string CountKey(IEnumerable key)
        {
            MD5 md5 = MD5.Create();
            StringBuilder sb = new StringBuilder();
            foreach (object o in key)
            {
                sb.Append("#sep#");
                if (o == null) sb.Append("(NULL)");
                else sb.Append(o.ToString());
            }
            byte[] bkey = md5.ComputeHash(Encoding.UTF8.GetBytes(sb.ToString()));
            return Convert.ToBase64String(bkey);
        }

        public static void Put(IEnumerable key, object value)
        {
            if (GlobalSettings.Pages.Cache().CacheMode == CacheMode.DontUse) return;
            UICacheItem item = new UICacheItem();
            item.Key = CountKey(key);
            item.Value = value;
            item.LastUpdatedAt = DateTime.UtcNow;
            item.LastUsedAt = DateTime.UtcNow;
            Instance.Items[item.Key] = item;
        }

        public static T Get<T>(IEnumerable key) where T : class
        {
            if (GlobalSettings.Pages.Cache().CacheMode == CacheMode.DontUse) return null;
            string sk = CountKey(key);
            if (Instance.Items.ContainsKey(sk))
            {
                UICacheItem item = Instance.Items[sk];
                if (item.Value is T)
                {
                    item.LastUsedAt = DateTime.UtcNow;
                    return (T)item.Value;
                }
            }
            return null;
        }
    }
}
