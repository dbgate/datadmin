using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;

namespace Plugin.dbf
{
    /// <summary>
    /// register into DatAdmin system of features
    /// </summary>
    [Feature(Name = _Name, Title = "DBF import & export")]
    public class DbfFeature : FeatureBase
    {
        public const string _Name = "dbf";
        public const string Test = _Name;

        public override bool AllwaysAllowed
        {
            get { return true; }
        }
    }
}
