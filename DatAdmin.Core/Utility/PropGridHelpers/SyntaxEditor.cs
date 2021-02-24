using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Design;
using System.ComponentModel;

namespace DatAdmin
{
    public class SyntaxEditor : UITypeEditor
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
            CodeLanguage lang = CodeLanguage.None;
            foreach (Attribute attrib in context.PropertyDescriptor.Attributes)
            {
                if (attrib is SyntaxEditorLanguageAttribute) lang = ((SyntaxEditorLanguageAttribute)attrib).Language;
            }
            string res = SyntaxEditorForm.Run(VersionInfo.ProgramTitle, label, value.SafeToString(), lang);
            return res ?? value;
        }
    }

    public class SyntaxEditorLanguageAttribute : Attribute
    {
        public CodeLanguage Language;
        public SyntaxEditorLanguageAttribute(CodeLanguage lang)
        {
            this.Language = lang;
        }
    }

}
