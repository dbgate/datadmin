using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;

namespace Plugin.datasyn
{
    [Feature(Name = _Name, Title = "Data synchronization")]
    public class DataSynchronizationFeature : FeatureBase
    {
        public const string _Name = "datasyn";
        public const string Test = _Name;
    }
}
