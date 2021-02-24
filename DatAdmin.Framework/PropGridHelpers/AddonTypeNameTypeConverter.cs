using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace DatAdmin
{
    public class AddonTypeNameTypeConverter : TypeConverter
    {
        //public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        //{
        //    if (sourceType == typeof(string))
        //        return true;
        //    return base.CanConvertFrom(context, sourceType);
        //}

        //public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        //{
        //    if (destinationType == typeof(string))
        //        return true;
        //    return base.CanConvertTo(context, destinationType);
        //}

        //public override object ConvertFrom(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value)
        //{
        //    if (value.GetType() == typeof(string))
        //    {
        //        return value;
        //        //return AddonRegister.Instance.FindAddonType(value.ToString());
        //    }
        //    return base.ConvertFrom(context, culture, value);
        //}

        //public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        //{
        //    if (destinationType == typeof(string))
        //    {
        //        return value;
        //        //return ((AddonType)value).Name;
        //    }
        //    return base.ConvertTo(context, culture, value, destinationType);
        //}

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override System.ComponentModel.TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            List<string> addons = new List<string>();
            foreach(var a in AddonRegister.Instance.GetAddonTypes()) addons.Add(a.Name);
            addons.Sort();
            return new System.ComponentModel.TypeConverter.StandardValuesCollection(addons);
        }
    }
}
