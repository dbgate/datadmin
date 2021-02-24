using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace DatAdmin
{
    public partial class LoadJobCommandForm : FormEx
    {
        Func<JobCommand, bool> m_filter;
        JobCommandWithConnection m_result;

        public LoadJobCommandForm(Func<JobCommand, bool> filter)
        {
            InitializeComponent();

            m_filter = filter;

            foreach (string file in Directory.GetFiles(Core.JobsDirectory, "*.djb", SearchOption.AllDirectories))
            {
                var job = Job.LoadFromFile(file);
                foreach (var cmd in job.GetAllCommands())
                {
                    if (m_filter != null && !m_filter(cmd)) continue;
                    var item = listView1.Items.Add(IOTool.RelativePathTo(Core.JobsDirectory, file));
                    item.SubItems.Add(cmd.ToString());
                    item.Tag = new JobCommandWithConnection { Command = cmd, Connection = new JobConnection(file) };
                }
            }
        }

        public static JobCommandWithConnection Run(Func<JobCommand, bool> filter)
        {
            var win = new LoadJobCommandForm(filter);
            win.ShowDialogEx();
            return win.m_result;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            m_result = null;
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (listView1.FocusedItem != null)
            {
                m_result = (JobCommandWithConnection)listView1.FocusedItem.Tag;
                Close();
            }
            else
            {
                StdDialog.ShowError("s_please_select_command");
            }
        }
    }

    public class JobCommandWithConnection
    {
        public JobCommand Command;
        public JobConnection Connection;
    }
}
