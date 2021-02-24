using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    [Feature(Name = _Name, Title = "Professional Tools")]
    public class ProfessionalFeature : FeatureBase
    {
        public const string _Name = "pro";
    }

    [Feature(Name = _Name, Title = "Query Designer")]
    public class QueryDesignerFeature : FeatureBase
    {
        public const string _Name = "querydesigner";
        public const string Test = _Name + "|" + ProfessionalFeature._Name;
    }

    [Feature(Name = _Name, Title = "Jobs - saving, executing")]
    public class JobsFeature : FeatureBase
    {
        public const string _Name = "jobs";
        public const string Test = _Name + "|" + ProfessionalFeature._Name;
    }

    [Feature(Name = _Name, Title = "On server files")]
    public class DxDriverFeature : FeatureBase
    {
        public const string _Name = "dxdriver";
        public const string Test = _Name + "|" + ProfessionalFeature._Name;
    }

    [Feature(Name = _Name, Title = "Extended file places")]
    public class ExtendedFilePlacesFeature : FeatureBase
    {
        public const string _Name = "extendedfileplaces";
        public const string Test = _Name + "|" + ProfessionalFeature._Name;
    }

    [Feature(Name = _Name, Title = "Generic Tunnel")]
    public class GenericTunnelFeature : FeatureBase
    {
        public const string _Name = "generictunnel";
        public const string Test = _Name;
    }

    [Feature(Name = _Name, Title = "Database Backup")]
    public class DatabaseBackupFeature : FeatureBase
    {
        public const string _Name = "databasebackup";
        public const string Test = _Name;
    }

    [Feature(Name = _Name, Title = "Table perspectives - advanced features")]
    public class AdvancedPerspectivesFeature : FeatureBase
    {
        public const string _Name = "advperspectives";
        public const string Test = _Name + "|" + ProfessionalFeature._Name;
        public static bool Allowed { get { return LicenseTool.FeatureAllowed(AdvancedPerspectivesFeature.Test); } }
    }

    [Feature(Name = _Name, Title = "Custom dashboards")]
    public class CustomDashboardsFeature : FeatureBase
    {
        public const string _Name = "customdashboards";
        public const string Test = _Name + "|" + ProfessionalFeature._Name;
        public static bool Allowed { get { return LicenseTool.FeatureAllowed(CustomDashboardsFeature.Test); } }
    }

    [Feature(Name = _Name, Title = "Master/detail views")]
    public class MasterDetailViewsFeature : FeatureBase
    {
        public const string _Name = "masterdetail";
        public const string Test = _Name + "|" + ProfessionalFeature._Name;
        public static bool Allowed { get { return LicenseTool.FeatureAllowed(MasterDetailViewsFeature.Test); } }
    }

    [Feature(Name = _Name, Title = "Code completion")]
    public class CodeCompletionFeature : FeatureBase
    {
        public const string _Name = "codecompletion";
        public const string Test = _Name + "|" + ProfessionalFeature._Name;
        public static bool Allowed { get { return LicenseTool.FeatureAllowed(CodeCompletionFeature.Test); } }
    }

    [Feature(Name = _Name, Title = "All installed features")]
    public class AllFeature : FeatureBase
    {
        public const string _Name = "all";
    }
}
