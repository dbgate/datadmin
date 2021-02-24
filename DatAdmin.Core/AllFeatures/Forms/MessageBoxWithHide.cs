using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class MessageBoxWithHide : FormEx
    {
        string m_settingsKey;

        public MessageBoxWithHide(string message, string settingsKey)
        {
            InitializeComponent();
            m_settingsKey = settingsKey;
            textBox1.Text = Texts.Replace(message);
            chbDontShowAgain.Visible = pictureBox1.Visible = m_settingsKey != null;
            this.Text = VersionInfo.ProgramTitle;
        }

        public static void Run(string message, string settingsKey)
        {
            var win = new MessageBoxWithHide(message, settingsKey);
            win.ShowDialogEx();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            StdDialog.ShowInfo("s_this_options_changes_global_settings");
        }

        private void chbDontShowAgain_CheckedChanged(object sender, EventArgs e)
        {
            var dct = new Dictionary<string, string>();
            dct[m_settingsKey] = chbDontShowAgain.Checked ? "0" : "1";
            GlobalSettings.Pages.DirectModify(dct);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void MessageBoxWithHide_Shown(object sender, EventArgs e)
        {
            textBox1.SelectionLength = 0;
            btnOk.Focus();
        }
    }
}
