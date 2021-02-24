using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class NameWithSchemaForm : FormEx
    {
        public NameWithSchemaForm()
        {
            InitializeComponent();
        }

        private void NameWithSchemaForm_Shown(object sender, EventArgs e)
        {
            tbxName.Focus();
        }

        public static NameWithSchema Run(IEnumerable<string> schemata, bool enabledSchema, NameWithSchema defvalue)
        {
            NameWithSchemaForm win = new NameWithSchemaForm();
            if (schemata != null)
            {
                foreach (var sch in schemata)
                {
                    win.cbxSchema.Items.Add(sch);
                }
            }
            if (defvalue != null)
            {
                if (defvalue.Schema != null) win.cbxSchema.Text = defvalue.Schema;
                if (defvalue.Name != null) win.tbxName.Text = defvalue.Name;
            }
            win.cbxSchema.Enabled = enabledSchema;
            if (win.ShowDialogEx() == DialogResult.OK)
            {
                return new NameWithSchema(win.cbxSchema.Text == "" ? null : win.cbxSchema.Text, win.tbxName.Text);
            }
            return null;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
