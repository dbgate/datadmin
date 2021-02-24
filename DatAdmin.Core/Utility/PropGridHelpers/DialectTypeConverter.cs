using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace DatAdmin
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ExpandDialectVersionsAttribute : Attribute
    {
    }

    public class DialectTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
                return true;
            return base.CanConvertFrom(context, sourceType);
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string))
                return true;
            return base.CanConvertTo(context, destinationType);
        }

        public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        {
            if (value.GetType() == typeof(string))
            {
                var res = ParseDialectName(value.ToString());
                if (res != null) return res;
            }
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string) && value != null)
            {
                return ((ISqlDialect)value).DisplayName;
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override System.ComponentModel.TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            bool expandVersions = false;
            foreach (var attr in context.PropertyDescriptor.Attributes)
            {
                if (attr is ExpandDialectVersionsAttribute) expandVersions = true;
            }
            List<ISqlDialect> dials = DialectAddonType.GetAllDialects(expandVersions);
            System.ComponentModel.TypeConverter.StandardValuesCollection svc = new System.ComponentModel.TypeConverter.StandardValuesCollection(dials);
            return svc;
        }

        public static ISqlDialect ParseDialectName(string name)
        {
            foreach (var hld in DialectAutoDetectorAddonType.Instance.CommonSpace.GetAllAddons())
            {
                var det = hld.InstanceModel as IDialectDetector;
                var res = det.DetectDialect(name);
                if (res != null) return res;
            }
            return null;
        }
    }
}
