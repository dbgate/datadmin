using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;

namespace Plugin.mssql
{
    [Feature(Name = _Name, Title = "MS SQL Native Backup")]
    public class MsSqlBackupFeature : FeatureBase
    {
        public const string _Name = "mssql_backup";
        public const string Test = _Name + "|" + ProfessionalFeature._Name;
    }
}
