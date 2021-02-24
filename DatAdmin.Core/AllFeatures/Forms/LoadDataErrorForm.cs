using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class LoadDataErrorForm : FormEx
    {
        LoadDataErrorFormResult m_result = LoadDataErrorFormResult.Cancel;
        bool m_loaded = false;
        Exception m_error;

        public LoadDataErrorForm(Exception error)
        {
            InitializeComponent();
            m_error = error;
            tbxDetails.Text = Errors.ExtractImportantException(m_error).ToString();
            chbShowDetails.Checked = false;
            SetDetailVisible(true);
            m_loaded = true;
        }

        public static LoadDataErrorFormResult Run(Exception err)
        {
            var win = new LoadDataErrorForm(err);
            win.ShowDialogEx();
            return win.m_result;
        }

        private void SetDetailVisible(bool oldvalue)
        {
            if (oldvalue != chbShowDetails.Checked)
            {
                tbxDetails.Visible = chbShowDetails.Checked;
                if (tbxDetails.Visible)
                {
                    Height += tbxDetails.Height + 10;
                }
                else
                {
                    Height -= tbxDetails.Height + 10;
                }
            }
        }

        private void chbShowDetails_CheckedChanged(object sender, EventArgs e)
        {
            if (!m_loaded) return;
            SetDetailVisible(tbxDetails.Visible);
        }

        private void btnTryAgain_Click(object sender, EventArgs e)
        {
            m_result = LoadDataErrorFormResult.LoadAgain;
            Close();
        }

        private void btnClearSettings_Click(object sender, EventArgs e)
        {
            m_result = LoadDataErrorFormResult.ClearSettings;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            m_result = LoadDataErrorFormResult.Cancel;
            Close();
        }

        private void LoadDataErrorForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (cbxSendError.Checked)
            {
                ErrorSendThread.SendError(m_error, Logging.GetFeedbackLastLogEntries(), MainWindow.Instance.TakeScreenshot());
            }
        }
    }

    public enum LoadDataErrorFormResult { LoadAgain, ClearSettings, Cancel }
}
