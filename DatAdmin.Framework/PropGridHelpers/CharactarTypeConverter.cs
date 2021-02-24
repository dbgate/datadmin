using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;

namespace DatAdmin
{
    public class CharacterTypeConverter : TypeConverter
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
                string val = (string)value;
                if (val.ToUpper() == "(TAB)")
                    return '\t';
                if (val.ToUpper() == "(SPACE)")
                    return ' ';
                //if (val.ToUpper() == "(CR)")
                //    return '\n';
                //if (val.ToUpper() == "(LF)")
                //    return '\r';
                if (val.Length > 0) return val[0];
                return '?';
            }
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                char ch = (char)value;
                if (ch == ' ') return "(SPACE)";
                if (ch == '\t') return "(TAB)";
                //if (ch == '\r') return "(CR)";
                //if (ch == '\n') return "(LF)";
                return new String(ch, 1);
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override System.ComponentModel.TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            char[] chs = new char[] { ' ', '\t' };
            System.ComponentModel.TypeConverter.StandardValuesCollection svc = new System.ComponentModel.TypeConverter.StandardValuesCollection(chs);
            return svc;
        }
    }
}
