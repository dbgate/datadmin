using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;

namespace Plugin.dbmodel
{
    [Feature(Name = _Name, Title = "Database structure synchronization")]
    public class DbStructSynchronizationFeature : FeatureBase
    {
        public const string _Name = "dbsyn";
        public const string Test = _Name + "|" + ProfessionalFeature._Name;
    }

    [Feature(Name = _Name, Title = "Dependency browser")]
    public class DependencyBrowserFeature : FeatureBase
    {
        public const string _Name = "depbrowse";
        public const string Test = _Name + "|" + ProfessionalFeature._Name;
    }

    [Feature(Name = _Name, Title = "Database models")]
    public class DatabaseModelsFeature : FeatureBase
    {
        public const string _Name = "databasemodels";
        public const string Test = _Name + "|" + ProfessionalFeature._Name;
        public static bool Allowed { get { return LicenseTool.FeatureAllowed(DatabaseModelsFeature.Test); } }
    }
}
