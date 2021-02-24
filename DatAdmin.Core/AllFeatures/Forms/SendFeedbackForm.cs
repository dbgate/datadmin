using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class SendFeedbackForm : FormEx
    {
        bool m_sent = false;

        public SendFeedbackForm()
        {
            InitializeComponent();
        }

        public static void Run()
        {
            var win = new SendFeedbackForm();
            win.tbxEmail.Text = LicenseTool.RegEmail1() ?? "";
            win.ShowDialogEx();
        }

        private void SendFeedbackForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!m_sent && (!String.IsNullOrEmpty(tbxSubject.Text) || !String.IsNullOrEmpty(tbxBody.Text)))
            {
                if (MessageBox.Show(Texts.Get("s_feedback_not_sent_close_q"), VersionInfo.ProgramTitle, MessageBoxButtons.YesNo) != DialogResult.Yes)
                {
                    e.Cancel = true;
                }
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (tbxSubject.Text.Trim() == "")
            {
                MessageBox.Show(Texts.Get("s_please_fill_subject"));
                tbxSubject.Focus();
                return;
            }
            if (tbxBody.Text.Trim() == "")
            {
                MessageBox.Show(Texts.Get("s_please_fill_body"));
                tbxBody.Focus();
                return;
            }
            if (SendFeedback.Send(tbxSubject.Text, tbxBody.Text, chbSendMeAnswer.Checked, tbxEmail.Text))
            {
                MessageBox.Show(Texts.Get("s_feedback_sent_thanks"));
                m_sent = true;
                Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void chbSendMeAnswer_CheckedChanged(object sender, EventArgs e)
        {
            tbxEmail.Enabled = chbSendMeAnswer.Checked;
        }
    }
}
