using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace DatAdmin
{
    public enum CacheMode
    {
        [Description("s_use_persistent")]
        UsePersistent,
        [Description("s_use_memory")]
        UseMemory,
        [Description("s_dont_use")]
        DontUse,
    }

    [SettingsPage(Name = "cache", Title = "s_cache", Targets = SettingsTargets.Global)]
    public class CacheSettings : SettingsPageBase
    {
        [DisplayName("s_clear_now")]
        [Category("s_special")]
        [TypeConverter(typeof(YesNoTypeConverter))]
        [SettingsKey("gui.cache.clearnow")]
        public bool ClearNow
        {
            get { return false; }
            set { if (value) UICache.Clear(); }
        }

        private CacheMode m_cacheMode = CacheMode.UsePersistent;
        [DisplayName("s_cache_mode")]
        [SettingsKey("gui.cache.mode")]
        public CacheMode CacheMode
        {
            get { return m_cacheMode; }
            set { m_cacheMode = value; }
        }
        private int m_deleteAfterDays = 30;

        [DisplayName("s_delete_after_days")]
        [SettingsKey("gui.cache.delete_after_days")]
        public int DeleteAfterDays
        {
            get { return m_deleteAfterDays; }
            set { m_deleteAfterDays = value; }
        }

        [DisplayName("s_cache_size")]
        public int CacheSize
        {
            get { return UICache.Size; }
        }
    }

    public static class SettingsPageCollection_Cache
    {
        public static CacheSettings Cache(this SettingsPageCollection col)
        {
            return (CacheSettings)col.PageByName("cache");
        }
    }
}
