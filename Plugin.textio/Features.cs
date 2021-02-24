using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;

namespace Plugin.textio
{
    [Feature(Name = _Name, Title = "Multiple file export")]
    public class MultiFileExportFeature : FeatureBase
    {
        public const string _Name = "multifileexport";
        public const string Test = _Name + "|" + ProfessionalFeature._Name;
    }

    [Feature(Name = _Name, Title = "Multiple table export")]
    public class MultiTableExportFeature : FeatureBase
    {
        public const string _Name = "multitableexport";
        public const string Test = _Name + "|" + ProfessionalFeature._Name;
    }

    [Feature(Name = _Name, Title = "Import from text")]
    public class TextImportFeature : FeatureBase
    {
        public const string _Name = "textimport";
        public const string Test = _Name + "|" + ProfessionalFeature._Name;
    }

    [Feature(Name = _Name, Title = "Database documentation export")]
    public class DatabaseDocsWriterFeature : FeatureBase
    {
        public const string _Name = "dbdocswriter";
        public const string Test = _Name;
    }

    [Feature(Name = _Name, Title = "Export to MS Excel")]
    public class ExcelExportFeature : FeatureBase
    {
        public const string _Name = "excelexport";
        public const string Test = _Name;
    }

    [Feature(Name = _Name, Title = "Export table to text formats")]
    public class TableTextExportFeature : FeatureBase
    {
        public const string _Name = "tabletextexport";
        public const string Test = _Name;
    }

    [Feature(Name = _Name, Title = "Export table to BLOB files")]
    public class BlobExportFeature : FeatureBase
    {
        public const string _Name = "blobexport";
        public const string Test = _Name + "|" + ProfessionalFeature._Name;
    }
}
