using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;

namespace Plugin.querytool
{
    [Feature(Name = _Name, Title = "Query history")]
    public class QueryHistoryFeature : FeatureBase
    {
        public const string _Name = "queryhistory";
        public const string Test = _Name + "|" + ProfessionalFeature._Name;
    }
}
