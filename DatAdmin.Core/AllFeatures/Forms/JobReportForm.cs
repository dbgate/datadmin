using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class JobReportForm : FormEx
    {
        public JobReportForm()
        {
            InitializeComponent();
        }

        public static bool Run(string file, JobCommand cmd)
        {
            var win = new JobReportForm();
            win.jobReportFrame1.LoadCommand(file, cmd);
            if (win.ShowDialogEx() == DialogResult.OK)
            {
                win.jobReportFrame1.Save();
                return true;
            }
            return false;
        }
    }
}
