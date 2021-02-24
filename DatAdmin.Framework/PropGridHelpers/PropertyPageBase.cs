using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Reflection;

namespace DatAdmin
{
    public class ModifiedPropertyDescriptor : PropertyDescriptor
    {
        PropertyDescriptor m_original;
        string m_displayName;
        public bool? ReadonlyOverride = null;

        public ModifiedPropertyDescriptor(PropertyDescriptor original, string displayName)
            : base(original)
        {
            m_original = original;
            m_displayName = displayName;
        }

        public override bool CanResetValue(object component)
        {
            return m_original.CanResetValue(component);
        }

        public override Type ComponentType
        {
            get { return m_original.ComponentType; }
        }

        public override object GetValue(object component)
        {
            return m_original.GetValue(component);
        }

        public override bool IsReadOnly
        {
            get { return ReadonlyOverride ?? m_original.IsReadOnly; }
        }

        public override Type PropertyType
        {
            get { return m_original.PropertyType; }
        }

        public override void ResetValue(object component)
        {
            m_original.ResetValue(component);
        }

        public override void SetValue(object component, object value)
        {
            try
            {
                m_original.SetValue(component, value);
            }
            catch (Exception err)
            {
                Errors.Report(err);
            }
        }

        public override bool ShouldSerializeValue(object component)
        {
            return m_original.ShouldSerializeValue(component);
        }

        public override string Category
        {
            get { return Texts.Get(m_original.Category); }
        }

        public override bool IsBrowsable
        {
            get
            {
                //if (m_displayName == null) return false;
                return m_original.IsBrowsable;
            }
        }

        public override string Description
        {
            get
            {
                return Texts.Get(base.Description);
            }
        }

        public override string DisplayName
        {
            get
            {
                if (m_displayName != null) return Texts.Get(m_displayName);
                return m_original.DisplayName;
            }
        }

        public override object GetEditor(Type editorBaseType)
        {
            return m_original.GetEditor(editorBaseType);
        }

        public override AttributeCollection Attributes
        {
            get { return m_original.Attributes; }
        }
        public override TypeConverter Converter
        {
            get { return m_original.Converter; }
        }
    }

    public class ReferencedPropertyDescriptor : PropertyDescriptor
    {
        PropertyDescriptor m_original;
        string m_displayName;
        object m_component;
        public bool? ReadonlyOverride = null;

        public ReferencedPropertyDescriptor(PropertyDescriptor original, string displayName, object component)
            : base(original)
        {
            m_original = original;
            m_displayName = displayName;
            m_component = component;
        }

        public override bool CanResetValue(object component)
        {
            return m_original.CanResetValue(m_component);
        }

        public override Type ComponentType
        {
            get { return m_original.ComponentType; }
        }

        public override object GetValue(object component)
        {
            return m_original.GetValue(m_component);
        }

        public override bool IsReadOnly
        {
            get { return ReadonlyOverride ?? m_original.IsReadOnly; }
        }

        public override Type PropertyType
        {
            get { return m_original.PropertyType; }
        }

        public override void ResetValue(object component)
        {
            m_original.ResetValue(component);
        }

        public override void SetValue(object component, object value)
        {
            m_original.SetValue(m_component, value);
        }

        public override bool ShouldSerializeValue(object component)
        {
            return m_original.ShouldSerializeValue(m_component);
        }

        public override string Category
        {
            get { return Texts.Get(m_original.Category); }
        }

        public override bool IsBrowsable
        {
            get
            {
                //if (m_displayName == null) return false;
                return m_original.IsBrowsable;
            }
        }

        public override string Description
        {
            get
            {
                return Texts.Get(base.Description);
            }
        }

        public override string DisplayName
        {
            get
            {
                if (m_displayName != null) return Texts.Get(m_displayName);
                return m_original.DisplayName;
            }
        }

        public override object GetEditor(Type editorBaseType)
        {
            return m_original.GetEditor(editorBaseType);
        }

        public override AttributeCollection Attributes
        {
            get { return m_original.Attributes; }
        }
        public override TypeConverter Converter
        {
            get { return m_original.Converter; }
        }
    }

    public class PropertyPageBase : ICustomTypeDescriptor
    {
        #region "Implements ICustomTypeDescriptor"

        public System.ComponentModel.AttributeCollection GetAttributes()
        {
            return TypeDescriptor.GetAttributes(this, true);
        }

        public string GetClassName()
        {
            return TypeDescriptor.GetClassName(this, true);
        }

        public string GetComponentName()
        {
            return TypeDescriptor.GetComponentName(this, true);
        }

        public System.ComponentModel.TypeConverter GetConverter()
        {
            return TypeDescriptor.GetConverter(this, true);
        }

        public System.ComponentModel.EventDescriptor GetDefaultEvent()
        {
            return TypeDescriptor.GetDefaultEvent(this, true);
        }

        public System.ComponentModel.PropertyDescriptor GetDefaultProperty()
        {
            return TypeDescriptor.GetDefaultProperty(this, true);
        }

        public object GetEditor(System.Type editorBaseType)
        {
            return TypeDescriptor.GetEditor(this, editorBaseType, true);
        }

        public System.ComponentModel.EventDescriptorCollection GetEvents()
        {
            return TypeDescriptor.GetEvents(this, true);
        }

        public System.ComponentModel.EventDescriptorCollection GetEvents(System.Attribute[] attributes)
        {
            return TypeDescriptor.GetEvents(this, attributes, true);
        }

        public System.ComponentModel.PropertyDescriptorCollection GetProperties()
        {
            return TypeDescriptor.GetProperties(this, true);
        }

        public System.ComponentModel.PropertyDescriptorCollection GetProperties(System.Attribute[] attributes)
        {
            PropertyDescriptorCollection src = TypeDescriptor.GetProperties(this, attributes, true);

            PropertyDescriptorCollection res = new PropertyDescriptorCollection(null);

            foreach (PropertyDescriptor desc in src)
            {
                string displayName = GetDisplayName(desc);
                if (displayName != null)
                {
                    res.Add(new ModifiedPropertyDescriptor(desc, displayName));
                }
                else
                {
                    res.Add(desc);
                }
            }
            return res;
        }

        public object GetPropertyOwner(System.ComponentModel.PropertyDescriptor pd)
        {
            return this;
        }

        #endregion

        public static string GetDisplayName(PropertyDescriptor desc)
        {
            string name = desc.Name;
            PropertyInfo info = desc.ComponentType.GetProperty(name);
            string displayName = null;
            foreach (DisplayNameAttribute attr in info.GetCustomAttributes(typeof(DisplayNameAttribute), true))
            {
                displayName = attr.DisplayName;
            }
            return displayName;
        }
    }
}
