using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;

namespace Plugin.versiondb
{
    [Feature(Name = _Name, Title = "Versioned database")]
    public class VersionedDbFeature : FeatureBase
    {
        public const string _Name = "versiondb";
        public const string Test = _Name;
    }
}
