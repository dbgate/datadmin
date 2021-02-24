using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class EmailFromFrame : UserControl
    {
        public EmailFromFrame()
        {
            InitializeComponent();

            Reload();
        }

        private void Reload()
        {
            if (GlobalSettings.Pages == null) return;
            var cfg = GlobalSettings.Pages.Email();
            if (cfg == null) return;
            tbxFrom.Text = cfg.FromAddr;
            if (!cfg.FromName.IsEmpty()) tbxFrom.Text += String.Format(" <{0}>", cfg.FromName);
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            OptionsForm.Run("s_email");
            Reload();
        }
    }
}
