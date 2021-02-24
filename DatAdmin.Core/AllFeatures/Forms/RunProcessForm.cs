using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class RunProcessForm : FormEx
    {
        Process m_process;
        bool m_isErrorSent;
        public RunProcessForm(Process process)
        {
            InitializeComponent();
            m_process = process;
            messageLogFrame1.Source = m_process.LogSource;
            cbxSendError.Checked = GlobalSettings.Pages.General().AllowSendErrorReports;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ReloadState();
        }

        private void ReloadState()
        {
            labProcessName.Text = m_process.Title;
            labDuration.Text = m_process.Duration.NiceFormat();
            labCurWork.Text = Texts.Get(m_process.CurWork);
            labJob.Text = m_process.Job.SavedName ?? "";
            labState.Text = m_process.StateTitle;
            btnStopProcess.Enabled = m_process.CanCancel;
            bool finished = false;
            switch (m_process.State)
            {
                case ProcessState.Canceled:
                case ProcessState.Error_Finished:
                    cbxSendError.Visible = true;
                    pictureBox1.Image = CoreIcons.error;
                    finished = true;
                    break;
                case ProcessState.Finished:
                case ProcessState.Warning_Finished:
                    cbxSendError.Visible = false;
                    pictureBox1.Image = CoreIcons.ok;
                    finished = true;
                    break;
                case ProcessState.Starting:
                case ProcessState.Running:
                    cbxSendError.Visible = false;
                    if (pictureBox1.Image != CoreIcons.busy_loop)
                    {
                        pictureBox1.Image = CoreIcons.busy_loop;
                    }
                    break;
            }
            if (finished)
            {
                btnClose.Text = Texts.Get("s_close");
                if (m_process.CloseOnFinish) Close();
            }
        }

        private void btclose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btcancel_Click(object sender, EventArgs e)
        {
            m_process.CancelWithQuery();
        }

        public static void Run(Process process)
        {
            RunProcessForm win = new RunProcessForm(process);
            win.ShowDialogEx();
        }

        private void RunProcessForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!m_process.State.IsEndState())
            {
                MainWindow.Instance.ShowDocker(new ProcesslistDockerFactory());
            }
            if (cbxSendError.Visible && cbxSendError.Checked && !m_isErrorSent)
            {
                ProcessFailedError ex = m_process.GetFailError();
                var logs = new List<LogMessageRecord>();
                logs.AddRange(m_process.LogSource.GetMessages());
                logs.Add(new LogMessageRecord { Level = LogLevel.Info, Message = "===============LOW LEVEL LOG============" });
                logs.AddRange(Logging.GetFeedbackLastLogEntries());
                ErrorSendThread.SendError(ex, logs, MainWindow.Instance.TakeScreenshot());
                ErrorSendThread.SendError(m_process.GetFailReasonError(), null, null);
                m_isErrorSent = true;
            }
        }
    }
}