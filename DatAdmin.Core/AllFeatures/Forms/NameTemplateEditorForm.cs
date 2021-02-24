using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class NameTemplateEditorForm : FormEx
    {
        public NameTemplateEditorForm()
        {
            InitializeComponent();
        }

        public static string Run(string caption, string label, string text)
        {
            var win = new NameTemplateEditorForm();
            win.nameTemplateFrame1.NameTemplate = text;
            win.label1.Text = Texts.Get(label);
            win.Text = Texts.Get(caption);
            if (win.ShowDialogEx() == DialogResult.OK)
            {
                return win.nameTemplateFrame1.NameTemplate;
            }
            return null;
        }
    }
}
