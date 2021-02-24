using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;

namespace Plugin.Csv
{
    [Feature(Name = _Name, Title = "CSV import & export")]
    public class CsvFeature : FeatureBase
    {
        public const string _Name = "csv";
        public const string Test = _Name;
    }
}
