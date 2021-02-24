using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.ComponentModel;

namespace Plugin.querytool
{
    [SettingsPage(Name = "queryhistory", Title = "s_query_history", Targets = SettingsTargets.Global, ImageName = CoreIcons.historyName)]
    public class QueryHistorySettings : SettingsPageBase
    {
        bool m_useQueryHistory = true;
        [DatAdmin.DisplayName("s_use_query_history")]
        [SettingsKey("gui.queryhistory.use")]
        [TypeConverter(typeof(YesNoTypeConverter))]
        public bool UseQueryHistory
        {
            get { return m_useQueryHistory; }
            set { m_useQueryHistory = value; }
        }

        int m_deleteAfterDays = 30;
        [DatAdmin.DisplayName("s_delete_after_days")]
        [SettingsKey("gui.queryhistory.max_age")]
        public int DeleteAfterDays
        {
            get { return m_deleteAfterDays; }
            set { m_deleteAfterDays = value; }
        }

        public static QueryHistorySettings Page
        {
            get { return (QueryHistorySettings)GlobalSettings.Pages.PageByName("queryhistory"); }
        }
    }
}
