using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class SaveDataForm : FormEx
    {
        bool m_isFinished;
        string m_settingsKey;

        public SaveDataForm(SaveDataProgress progress, string setingsKey)
        {
            InitializeComponent();
            saveDataFrame1.Progress = progress;
            m_settingsKey = setingsKey;
            chbDontShowAgain.Visible = pictureBox1.Visible = m_settingsKey != null;
        }

        private void UpdateEnabling()
        {
            btnOk.Enabled = m_isFinished;
            btnOnBackground.Enabled = !m_isFinished;
            if (btnOk.Enabled) btnOk.Focus();
        }

        public void SaveFinished(bool waitForUser)
        {
            m_isFinished = true;
            UpdateEnabling();
            if (!waitForUser) Close();
        }

        public void ShowResultDialog()
        {
            m_isFinished = true;
            UpdateEnabling();
            this.ShowDialogEx();
        }

        public bool IsFinished()
        {
            return m_isFinished;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOnBackground_Click(object sender, EventArgs e)
        {
            Close();
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
    }
}
