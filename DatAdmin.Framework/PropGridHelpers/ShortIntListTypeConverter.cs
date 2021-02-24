using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.ComponentModel;

namespace DatAdmin
{
    public class ShortIntListTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string)) return true;
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
                var ptype = context.PropertyDescriptor.PropertyType;
                if (ptype == typeof(List<int>))
                {
                    return new List<int>(from s in value.ToString().Split(',') select Int32.Parse(s.Trim()));
                }
            }
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                var ptype = context.PropertyDescriptor.PropertyType;
                if (ptype == typeof(List<int>))
                {
                    return String.Join(", ", (from i in (List<int>)value select i.ToString()).ToArray());
                }
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
