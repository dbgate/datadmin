using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class SettingsForm : FormEx
    {
        public SettingsForm()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        public static void Run(SettingsPageCollection pages, SettingsTargets target)
        {
            SettingsForm win = new SettingsForm();
            win.settingsPageFrame1.Target = target;
            win.settingsPageFrame1.Pages = pages;
            win.ShowDialogEx();
            // save settings
            win.settingsPageFrame1.Pages = null;
        }
    }
}
