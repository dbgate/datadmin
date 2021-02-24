using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class RunOrSaveJobForm : FormEx
    {
        JobCommand m_command;
        Func<JobCommand, bool> m_checkConfig;

        public RunOrSaveJobForm(JobCommand command, Func<JobCommand, bool> checkConfig, string label)
        {
            InitializeComponent();
            m_command = command;
            label1.Text = Texts.Get(label);
            m_checkConfig = checkConfig;
            propertyFrame1.SelectedObject = m_command;
        }

        public static void Run(JobCommand command, Func<JobCommand, bool> checkConfig, string label)
        {
            var win = new RunOrSaveJobForm(command, checkConfig, label);
            win.ShowDialogEx();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnRunNow_Click(object sender, EventArgs e)
        {
            if (m_checkConfig != null && !m_checkConfig(m_command)) return;
            Job job = Job.FromCommand(m_command, new JobProperties());
            job.StartProcess();
            Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (m_checkConfig != null && !m_checkConfig(m_command)) return;
            Job.AskAndExportToFile(() => Job.FromCommand(m_command, new JobProperties()));
        }
    }
}
