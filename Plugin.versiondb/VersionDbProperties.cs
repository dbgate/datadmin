using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.ComponentModel;

namespace Plugin.versiondb
{
    public class VersionDbProperties : DbDefProperties
    {
        public VersionDbProperties()
        {
            ForceSingleSchema = true;
        }

        [XmlElem]
        [Category("s_sql_scripts")]
        [DatAdmin.DisplayName("s_get_version_sql")]
        [Description("s_get_sql_version_desc")]
        public string GetVersionSql { get; set; }

        [XmlElem]
        [Category("s_sql_scripts")]
        [DatAdmin.DisplayName("s_set_version_sql")]
        [Description("s_set_sql_version_desc")]
        public string SetVersionSql { get; set; }
    }
}
