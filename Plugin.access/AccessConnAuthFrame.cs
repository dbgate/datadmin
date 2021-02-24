using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace Plugin.access
{
    public partial class AccessConnAuthFrame : UserControl
    {
        public AccessConnAuthFrame()
        {
            InitializeComponent();
        }

        public bool UseAutentization { get { return cbxUseAutentization.Checked; } set { cbxUseAutentization.Checked = value; } }
        public string Login { get { return tbxLogin.Text; } set { tbxLogin.Text = value; } }
        public string Password { get { return tbxPassword.Text; } set { tbxPassword.Text = value; } }
        public string SystemDatabase { get { return tbxSystemDatabase.Text; } set { tbxSystemDatabase.Text = value; } }

        private void cbxUseAutentization_CheckedChanged(object sender, EventArgs e)
        {
            tbxSystemDatabase.Enabled = tbxLogin.Enabled = tbxPassword.Enabled = cbxUseAutentization.Checked;
        }
    }
}
