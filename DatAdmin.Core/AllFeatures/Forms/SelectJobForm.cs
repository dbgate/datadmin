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
    public partial class SelectJobForm : FormEx
    {
        public SelectJobForm()
        {
            InitializeComponent();

            foreach (string file in Directory.GetFiles(Core.JobsDirectory, "*.djb", SearchOption.AllDirectories))
            {
                lbxJobs.Items.Add(IOTool.RelativePathTo(Core.JobsDirectory, file));
            }
        }

        public static string Run()
        {
            var win = new SelectJobForm();
            if (win.ShowDialogEx() == DialogResult.OK)
            {
                return win.lbxJobs.Items[win.lbxJobs.SelectedIndex].ToString();
            }
            return null;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (lbxJobs.SelectedIndex < 0)
            {
                StdDialog.ShowError("s_please_choose_job");
                return;
            }
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
