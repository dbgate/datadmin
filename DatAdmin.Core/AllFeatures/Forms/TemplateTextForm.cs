using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Design;

namespace DatAdmin
{
    public partial class TemplateTextForm : FormEx
    {
        public TemplateTextForm()
        {
            InitializeComponent();
        }

        public static string Run(string text)
        {
            TemplateTextForm win = new TemplateTextForm();
            win.textBox1.Text = text;
            if (win.ShowDialogEx() == DialogResult.OK) return win.textBox1.Text;
            return text;
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            textBox1.SelectedText = "$[NL]";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.SelectedText = "$[VALUE]";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.SelectedText = "$[NAME]";
        }
    }

    public class TemplateTextEditor : UITypeEditor
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
            return TemplateTextForm.Run((string)value);
        }
    }
}

