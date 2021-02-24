using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class NameTemplateCompactFrame : UserControl
    {
        public NameTemplateCompactFrame()
        {
            InitializeComponent();
        }

        public string NameTemplate
        {
            get { return tbxName.Text; }
            set { tbxName.Text = value; }
        }

        public event EventHandler NameTemplateChanged;

        private void tbxName_TextChanged(object sender, EventArgs e)
        {
            if (NameTemplateChanged != null) NameTemplateChanged(this, e);
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            string val = NameTemplateEditorForm.Run(VersionInfo.ProgramTitle, "s_type_text", NameTemplate);
            if (val != null)
            {
                NameTemplate = val;
            }
        }
    }
}
