using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;

namespace Plugin.diagrams
{
    [Feature(Name = _Name, Title = "Database diagrams")]
    public class DiagramsFeature : FeatureBase
    {
        public const string _Name = "diagrams";
        public const string Test = _Name + "|" + ProfessionalFeature._Name;
        public static bool Allowed { get { return LicenseTool.FeatureAllowed(DiagramsFeature.Test); } }
    }
}
