using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.ComponentModel;
using System.Drawing.Design;

namespace Plugin.mssql
{
    [SettingsPage(Name = "mssql_client", Title = "MS SQL/s_client_settings", Targets = SettingsTargets.Global)]
    public class MsSqlSettings : SettingsPageBase
    {
        bool m_useNativeDependencies = true;

        [Category("MS SQL")]
        [DatAdmin.DisplayName("s_use_native_dependencies")]
        [SettingsKey("mssql.use_native_dependencies")]
        [TypeConverter(typeof(YesNoTypeConverter))]
        public bool UseNativeDependencies
        {
            get { return m_useNativeDependencies; }
            set { m_useNativeDependencies = value; }
        }
    }
}
