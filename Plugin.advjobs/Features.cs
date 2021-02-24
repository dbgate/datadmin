using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;

namespace Plugin.advjobs
{
    [Feature(Name = _Name, Title = "Advanced jobs scheduler")]
    public class AdvancedJobsFeature : FeatureBase
    {
        public const string _Name = "advjobs";
        public const string Test = _Name + "|" + ProfessionalFeature._Name;
    }
}
