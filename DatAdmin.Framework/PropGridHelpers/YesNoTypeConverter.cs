using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;

namespace DatAdmin
{
    public class YesNoTypeConverter : TypeConverter
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
                if (((string)value).ToLower() == Texts.Get("s_yes").ToLower())
                    return true;
                if (((string)value).ToLower() == Texts.Get("s_no").ToLower())
                    return false;
                if (((string)value).ToLower() == Texts.Get("s_default").ToLower())
                    return null;
                throw new Exception("DAE-00307 " + Texts.Get("s_values_can_be_yes_or_no"));
            }
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                bool? bval = (bool?)value;
                if (bval == true) return Texts.Get("s_yes");
                if (bval == false) return Texts.Get("s_no");
                return Texts.Get("s_default");
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override System.ComponentModel.TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            if (context.PropertyDescriptor.PropertyType == typeof(bool?))
            {
                bool?[] bools = new bool?[] { true, false, null };
                System.ComponentModel.TypeConverter.StandardValuesCollection svc = new System.ComponentModel.TypeConverter.StandardValuesCollection(bools);
                return svc;
            }
            else
            {
                bool[] bools = new bool[] { true, false };
                System.ComponentModel.TypeConverter.StandardValuesCollection svc = new System.ComponentModel.TypeConverter.StandardValuesCollection(bools);
                return svc;
            }
        }
    }
}
