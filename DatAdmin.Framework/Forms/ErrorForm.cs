using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Xml;

namespace DatAdmin
{
    public partial class ErrorForm : FrameworkFormEx
    {
        List<LogMessageRecord> m_log;
        Exception m_error;
        bool m_loaded;

        public ErrorForm(Exception err, List<LogMessageRecord> log)
        {
            InitializeComponent();
            m_error = err;
            m_log = log;
            labMessage.Text = err.Message;
            labText.Text = err.ToString();
            cbxSendError.Checked = Framework.Instance.AllowSendErrorReports();
            btnSupportRequest.Visible = HUsage.DefinedOpenErrorSupport;

            chbShowDetails.Checked = false;
            SetDetailVisible(true);
            m_loaded = true;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SetDetailVisible(bool oldvalue)
        {
            if (oldvalue != chbShowDetails.Checked)
            {
                labText.Visible = chbShowDetails.Checked;
                if (labText.Visible)
                {
                    Height += labText.Height + 10;
                }
                else
                {
                    Height -= labText.Height + 10;
                }
            }
        }

        public static bool Run(Exception err, List<LogMessageRecord> log)
        {
            ErrorForm win = new ErrorForm(err, log);
            return win.ShowDialogEx() == DialogResult.OK;
        }

        private void ErrorForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (cbxSendError.Checked)
            {
                ErrorSendThread.SendError(m_error, m_log, Framework.MainWindow.TakeScreenshot());
            }
        }

        private void chbShowDetails_CheckedChanged(object sender, EventArgs e)
        {
            if (!m_loaded) return;
            SetDetailVisible(labText.Visible);
    }

        private void btnSupportRequest_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            HUsage.CallOpenErrorSupport(m_error);
        }
}
}
