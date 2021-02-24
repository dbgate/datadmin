using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Windows.Forms;

namespace DatAdmin
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
    public class DisplayNameAttribute : Attribute
    {
        public string DisplayName;
        public DisplayNameAttribute(string displayName)
        {
            DisplayName = displayName;
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class DefaultPropertyTabAttribute : Attribute
    {
        string m_pageTitle;
        public DefaultPropertyTabAttribute(string pageTitle)
        {
            m_pageTitle = pageTitle;
        }
        public string PageTitle { get { return m_pageTitle; } }
        public int TabWeight;
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class HideDefaultPropertyTabAttribute : Attribute { }

    [AttributeUsage(AttributeTargets.Property)]
    public class TabbedPropertyAttribute : Attribute
    {
        string m_pageTitle;
        public TabbedPropertyAttribute(string pageTitle)
        {
            m_pageTitle = pageTitle;
        }
        public string PageTitle { get { return m_pageTitle; } }
        public int TabWeight;
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class TabbedEditorAttribute : Attribute
    {
        public Type EditorType { get; private set; }
        public int TabWeight;
        public string EnabledFunc;

        public TabbedEditorAttribute(Type editorType)
        {
            EditorType = editorType;
        }
    }

    public interface ITabbedEditor
    {
        void LoadFromObject(object obj, PropertyInfo prop);
        string PageTitle { get; }
    }

    public interface ICustomPropertyPage
    {
        Control CreatePropertyPage();
    }

    public interface IPropertyPageWithSerialization
    {
        string GetSerializationKey();
        string GetSerializationValue();
        void SetSerializedValue(string value);
    }
}
