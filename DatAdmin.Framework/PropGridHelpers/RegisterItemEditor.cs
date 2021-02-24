using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Reflection;

namespace DatAdmin
{
    [AttributeUsage(AttributeTargets.Property)]
    public class RegisterItemTypeAttribute : Attribute
    {
        public Type AddonTypeClass;
        public RegisterItemTypeAttribute(Type type)
        {
            AddonTypeClass = type;
        }
    }

    public class RegisterItemTypeConverter : TypeConverter
    {
        private AddonType GetAddonType(ITypeDescriptorContext context)
        {
            foreach (Attribute attr in context.PropertyDescriptor.Attributes)
            {
                var a = attr as RegisterItemTypeAttribute;
                if (a != null)
                {
                    return (AddonType)a.AddonTypeClass.GetStaticPropertyOrField("Instance");
                }
            }
            return null;
        }

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
                foreach (var item in GetAddonType(context).CommonSpace.GetAllAddons())
                {
                    if (Texts.Get(item.Title) == value.SafeToString()) return item;
                }
            }
            return null;
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                if (value == null) return null;
                return Texts.Get(((AddonHolder)value).Title);
            }
            return "";
        }

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override System.ComponentModel.TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            System.ComponentModel.TypeConverter.StandardValuesCollection svc = new System.ComponentModel.TypeConverter.StandardValuesCollection( 
                new List<AddonHolder>((GetAddonType(context).CommonSpace.GetFilteredAddons(RegisterItemUsage.DirectUse))));
            return svc;
        }
    }
}
