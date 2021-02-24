using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;

namespace DatAdmin
{
    public class DynamicAttribute
    {
        string m_name = "";
        string m_title = "";
        string m_description = "";
        string m_category = "s_misc";

        TypeCode m_type = TypeCode.String;

        public string Name
        {
            get { return m_name; }
            set { m_name = value; }
        }

        public string Title
        {
            get { return m_title; }
            set { m_title = value; }
        }

        public TypeCode Type
        {
            get { return m_type; }
            set { m_type = value; }
        }

        public string Description
        {
            get { return m_description; }
            set { m_description = value; }
        }

        public string Category
        {
            get { return m_category; }
            set { m_category = value; }
        }

        public System.Type GetSystemType()
        {
            return System.Type.GetType("System." + m_type.ToString());
        }
    }

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public class DynamicClass 
    {
        List<DynamicAttribute> m_attributes = new List<DynamicAttribute>();

        [Editor(typeof(CollectionEditor), typeof(UITypeEditor))]
        public List<DynamicAttribute> Attributes
        {
            get { return m_attributes; }
            set { m_attributes = value; }
        }

        public DynamicInstance CreateInstance()
        {
            return new DynamicInstance(this);
        }

        public bool Empty { get { return m_attributes.Count == 0; } }

        public DynamicInstance QueryInstance()
        {
            DynamicInstance res = new DynamicInstance(this);
            if (!Empty)
            {
                if (!EditPropertiesForm.Run(res, true))
                {
                    return null;
                }
            }
            return res;
        }
    }

    public class DynamicInstancePropertyDescriptor : PropertyDescriptor
    {
        DynamicAttribute m_attrib;

        public DynamicInstancePropertyDescriptor(DynamicAttribute attrib)
            : base(attrib.Name, new Attribute[] { })
        {
            m_attrib = attrib;
        }

        public override bool CanResetValue(object component)
        {
            return false;
        }

        public override Type ComponentType
        {
            get { return m_attrib.GetSystemType(); }
        }

        public override object GetValue(object component)
        {
            return ((DynamicInstance)component).Data[m_attrib.Name];
        }

        public override bool IsReadOnly
        {
            get { return false; }
        }

        public override Type PropertyType
        {
            get { return m_attrib.GetSystemType(); }
        }

        public override void ResetValue(object component)
        {
        }

        public override void SetValue(object component, object value)
        {
            ((DynamicInstance)component).Data[m_attrib.Name] = value;
        }

        public override bool ShouldSerializeValue(object component)
        {
            return true;
        }

        public override bool IsBrowsable
        {
            get
            {
                return true;
            }
        }

        public override string Description
        {
            get
            {
                return Texts.Get(m_attrib.Description);
            }
        }

        public override string Category
        {
            get
            {
                return Texts.Get(m_attrib.Category);
            }
        }

        public override string DisplayName
        {
            get
            {
                if (m_attrib.Title != "") return Texts.Get(m_attrib.Title);
                return m_attrib.Name;
            }
        }

        public override string Name
        {
            get
            {
                return m_attrib.Name;
            }
        }
    }

    public class DynamicInstance : ICustomTypeDescriptor
    {
        Dictionary<string, object> m_data = new Dictionary<string, object>();
        DynamicClass m_class;

        public DynamicClass Class
        {
            get { return m_class; }
            set { m_class = value; }
        }

        public Dictionary<string, object> Data
        {
            get { return m_data; }
            set { m_data = value; }
        }

        public DynamicInstance(DynamicClass cls)
        {
            m_class = cls;
            foreach (DynamicAttribute attrib in cls.Attributes)
            {
                m_data[attrib.Name] = Array.CreateInstance(attrib.GetSystemType(), 1).GetValue(0);
            }
        }

        #region "Implements ICustomTypeDescriptor"

        public AttributeCollection GetAttributes()
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
            PropertyDescriptorCollection res = new PropertyDescriptorCollection(null);

            foreach (DynamicAttribute attr in m_class.Attributes)
            {
                res.Add(new DynamicInstancePropertyDescriptor(attr));
            }
            return res;
        }

        public System.ComponentModel.PropertyDescriptorCollection GetProperties(System.Attribute[] attributes)
        {
            return GetProperties();
        }

        public object GetPropertyOwner(System.ComponentModel.PropertyDescriptor pd)
        {
            return this;
        }

        #endregion
    }
}


//[AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
//public class DisplayNameAttribute : Attribute
//{
//    public string DisplayName;
//    public DisplayNameAttribute(string displayName)
//    {
//        DisplayName = displayName;
//    }
//}

//public class ModifiedPropertyDescriptor : PropertyDescriptor
//{
//    PropertyDescriptor m_original;
//    string m_displayName;

//    public ModifiedPropertyDescriptor(PropertyDescriptor original, string displayName)
//        : base(original)
//    {
//        m_original = original;
//        m_displayName = displayName;
//    }

//    public override bool CanResetValue(object component)
//    {
//        return m_original.CanResetValue(component);
//    }

//    public override Type ComponentType
//    {
//        get { return m_original.ComponentType; }
//    }

//    public override object GetValue(object component)
//    {
//        return m_original.GetValue(component);
//    }

//    public override bool IsReadOnly
//    {
//        get { return m_original.IsReadOnly; }
//    }

//    public override Type PropertyType
//    {
//        get { return m_original.PropertyType; }
//    }

//    public override void ResetValue(object component)
//    {
//        m_original.ResetValue(component);
//    }

//    public override void SetValue(object component, object value)
//    {
//        m_original.SetValue(component, value);
//    }

//    public override bool ShouldSerializeValue(object component)
//    {
//        return m_original.ShouldSerializeValue(component);
//    }

//    /*
//    public override string Category
//    {
//        get { return m_original.Category; }
//    }
//    */

//    public override bool IsBrowsable
//    {
//        get
//        {
//            //if (m_displayName == null) return false;
//            return m_original.IsBrowsable;
//        }
//    }

//    public override string Description
//    {
//        get
//        {
//            return Texts.Get(base.Description);
//        }
//    }

//    public override string DisplayName
//    {
//        get
//        {
//            if (m_displayName != null) return Texts.Get(m_displayName);
//            return m_original.DisplayName;
//        }
//    }
//}

//public class PropertyPageBase : ICustomTypeDescriptor
//{
//    #region "Implements ICustomTypeDescriptor"

//    public System.ComponentModel.AttributeCollection GetAttributes()
//    {
//        return TypeDescriptor.GetAttributes(this, true);
//    }

//    public string GetClassName()
//    {
//        return TypeDescriptor.GetClassName(this, true);
//    }

//    public string GetComponentName()
//    {
//        return TypeDescriptor.GetComponentName(this, true);
//    }

//    public System.ComponentModel.TypeConverter GetConverter()
//    {
//        return TypeDescriptor.GetConverter(this, true);
//    }

//    public System.ComponentModel.EventDescriptor GetDefaultEvent()
//    {
//        return TypeDescriptor.GetDefaultEvent(this, true);
//    }

//    public System.ComponentModel.PropertyDescriptor GetDefaultProperty()
//    {
//        return TypeDescriptor.GetDefaultProperty(this, true);
//    }

//    public object GetEditor(System.Type editorBaseType)
//    {
//        return TypeDescriptor.GetEditor(this, editorBaseType, true);
//    }

//    public System.ComponentModel.EventDescriptorCollection GetEvents()
//    {
//        return TypeDescriptor.GetEvents(this, true);
//    }

//    public System.ComponentModel.EventDescriptorCollection GetEvents(System.Attribute[] attributes)
//    {
//        return TypeDescriptor.GetEvents(this, attributes, true);
//    }

//    public System.ComponentModel.PropertyDescriptorCollection GetProperties()
//    {
//        return TypeDescriptor.GetProperties(this, true);
//    }

//    public System.ComponentModel.PropertyDescriptorCollection GetProperties(System.Attribute[] attributes)
//    {
//        PropertyDescriptorCollection src = TypeDescriptor.GetProperties(this, attributes, true);

//        PropertyDescriptorCollection res = new PropertyDescriptorCollection(null);

//        foreach (PropertyDescriptor desc in src)
//        {
//            string name = desc.Name;
//            PropertyInfo info = this.GetType().GetProperty(name);
//            string displayName = null;
//            foreach (DisplayNameAttribute attr in info.GetCustomAttributes(typeof(DisplayNameAttribute), true))
//            {
//                displayName = attr.DisplayName;
//            }
//            if (displayName != null)
//            {
//                res.Add(new ModifiedPropertyDescriptor(desc, displayName));
//            }
//        }
//        return res;
//    }

//    public object GetPropertyOwner(System.ComponentModel.PropertyDescriptor pd)
//    {
//        return this;
//    }

//    #endregion

//}
