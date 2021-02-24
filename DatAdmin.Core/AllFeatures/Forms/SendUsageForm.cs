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
    public partial class SendUsageForm : FormEx
    {
        public SendUsageForm()
        {
            InitializeComponent();
            cbxAllowUpload.Checked = GlobalSettings.Pages.General().AllowUploadUsageStats;
            cbxDontAskNext.Checked = !GlobalSettings.Pages.General().AskWhenUploadUsageStats;
        }

        private void btnViewUsageFolder_Click(object sender, EventArgs e)
        {
            //System.Diagnostics.Process.Start(Core.UsageDirectory);
        }

        public static void UploadIfNeeded(Action closesplash)
        {
            //if (Directory.GetFiles(Core.UsageDirectory).Length >= UsageStats.MinimalUploadCount)
            //{
            //    if (GlobalSettings.Pages.General().AskWhenUploadUsageStats)
            //    {
            //        closesplash();
            //        var win = new SendUsageForm();
            //        win.ShowDialogEx();
            //    }
            //    else
            //    {
            //        SendUsageStatsThread.SendUsage();
            //    }
            //}
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SendUsageForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            GlobalSettings.Pages.BeginEdit();
            GlobalSettings.Pages.General().AskWhenUploadUsageStats = !cbxDontAskNext.Checked;
            GlobalSettings.Pages.General().AllowUploadUsageStats = cbxAllowUpload.Checked;
            GlobalSettings.Pages.EndEdit();
            SendUsageStatsThread.SendUsage();
            if (GlobalSettings.Pages.General().AllowUploadUsageStats) StdDialog.ShowInfo("s_thank_you_for_uploading_stats");
        }
    }
}
