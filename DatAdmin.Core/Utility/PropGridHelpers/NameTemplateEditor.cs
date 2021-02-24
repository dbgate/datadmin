using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Design;
using System.ComponentModel;

namespace DatAdmin
{
    public class NameTemplateEditor : UITypeEditor
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

            string label = "s_type_text";
            foreach (Attribute attrib in context.PropertyDescriptor.Attributes)
            {
                if (attrib is EditorDisplayLabelAttribute) label = ((EditorDisplayLabelAttribute)attrib).Label;
            }
            string res = NameTemplateEditorForm.Run(VersionInfo.ProgramTitle, label, value.SafeToString());
            return res ?? value;
        }
    }
}
