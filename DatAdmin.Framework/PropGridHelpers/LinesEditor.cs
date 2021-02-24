using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Design;
using System.ComponentModel;

namespace DatAdmin
{
    public class LinesEditor : UITypeEditor
    {
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            if (context != null && context.Instance != null)
            {
                if (!context.PropertyDescriptor.IsReadOnly)
                {
                    return UITypeEditorEditStyle.Modal;
                }
            }
            return UITypeEditorEditStyle.None;
        }

        [RefreshProperties(RefreshProperties.All)]
        public override object EditValue(ITypeDescriptorContext context, System.IServiceProvider provider, object value)
        {
            if (context == null || provider == null || context.Instance == null)
            {
                return base.EditValue(provider, value);
            }

            string label = "s_type_items_each_item_per_line";
            foreach (Attribute attrib in context.PropertyDescriptor.Attributes)
            {
                if (attrib is EditorDisplayLabelAttribute) label = ((EditorDisplayLabelAttribute)attrib).Label;
            }
            List<string> res = LinesDialog.Run(VersionInfo.ProgramTitle, label, (IEnumerable<string>)value, true);
            return res ?? value;
        }
    }

    public class EditorDisplayLabelAttribute : Attribute
    {
        public string Label;
        public EditorDisplayLabelAttribute(string label)
        {
            this.Label = label;
        }
    }

    public class LinesTypeConverter : TypeConverter
    {
        protected virtual string ProcessItem(string value) { return value; }
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
                List<string> res = new List<string>();
                foreach (string x in value.ToString().Split(','))
                {
                    res.Add(ProcessItem(x));
                }
                return res;
            }
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, System.Globalization.CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                return String.Join(",", ((List<string>)value).ToArray());
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }
    }

    public class TrimLinesTypeConverter : LinesTypeConverter
    {
        protected override string ProcessItem(string value)
        {
            if (value != null) return value.Trim();
            return value;
        }
    }
}
