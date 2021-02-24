using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;

namespace DatAdmin
{
    public interface IDialectDetector
    {
        ISqlDialect DetectDialect(DbConnection conn);
        ISqlDialect DetectDialect(string displayName);
        ISqlDialect ApproximateDialect { get; }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class DialectAutoDetectorAttribute : RegisterAttribute
    {
    }

    [AddonType]
    public class DialectAutoDetectorAddonType : AddonType
    {
        public override Type InterfaceType
        {
            get { return typeof(IDialectDetector); }
        }

        public override string Name
        {
            get { return "dialectdetector"; }
        }

        public override Type RegisterAttributeType
        {
            get { return typeof(DialectAutoDetectorAttribute); }
        }

        public static readonly DialectAutoDetectorAddonType Instance = new DialectAutoDetectorAddonType();
    }

}
