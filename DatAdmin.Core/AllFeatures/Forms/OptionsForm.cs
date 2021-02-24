using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class OptionsForm : FormEx
    {
        public OptionsForm()
        {
            InitializeComponent();
            settingsPageGeneral.Target = SettingsTargets.Global;
            settingsPageDialect.Target = SettingsTargets.Dialect;
            settingsPageGeneral.Pages = GlobalSettings.Pages;
            if (DialectAddonType.Instance != null)
            {
                foreach (AddonHolder item in DialectAddonType.Instance.StaticSpace.GetAllAddons())
                {
                    cbxDialect.Items.Add(item);
                }
            }
            if (cbxDialect.Items.Count > 0) cbxDialect.SelectedIndex = 0;
        }

        public static void Run(string cfgpath)
        {
            OptionsForm win = new OptionsForm();
            if (cfgpath != null) win.settingsPageGeneral.SelectSettingsPage(cfgpath);
            win.ShowDialogEx();
        }

        public static void Run()
        {
            Run(null);
        }

        private void OptionsForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            settingsPageGeneral.Pages = null;
            settingsPageDialect.Pages = null;
            GlobalSettings.DispatchChanged();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        void ChangedCurrentPage()
        {
            settingsPageGeneral.Pages = null;
            settingsPageDialect.Pages = null;
            switch (tabControl1.SelectedIndex)
            {
                case 0:
                    settingsPageGeneral.Pages = GlobalSettings.Pages;
                    break;
                case 1:
                    AddonHolder ditem = (AddonHolder)cbxDialect.SelectedItem;
                    settingsPageDialect.Pages = GlobalSettings.DialectSettings[ditem.Name];
                    break;
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangedCurrentPage();
        }

        private void cbxDialect_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangedCurrentPage();
        }
    }
}
