using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class InstallConfigForm : FormEx
    {
        public InstallConfigForm()
        {
            InitializeComponent();
            var inst = InstallationInfo.Instance;
            rbtPersonal.Enabled = LicenseTool.ValidLicenses.Count == 1 && LicenseTool.InvalidLicenses.Count == 0;
            chbProEval.Enabled = !LicenseTool.ContainsProduct("pro", true) && !LicenseTool.ContainsProduct("pro-eval", true);
            chbDataSynEval.Enabled = !LicenseTool.ContainsProduct("datasyn", true) && !LicenseTool.ContainsProduct("datasyn-eval", true);
            chbVersionDbEval.Enabled = !LicenseTool.ContainsProduct("versiondb", true) && !LicenseTool.ContainsProduct("versiondb-eval", true);
            switch (inst.InstallMode)
            {
                case InstallationMode.Personal:
                    rbtPersonal.Checked = true;
                    break;
                case InstallationMode.Professional:
                    rbtProfessional.Checked = true;
                    break;
                case InstallationMode.Unknown:
                    rbtProfessional.Checked = true;
                    if (chbProEval.Enabled && LicenseTool.ValidLicenses.Count == 1) chbProEval.Checked = true;
                    break;
            }
            chbAllowUploadStats.Checked = GlobalSettings.Pages.General().AllowUploadUsageStats;

            rbtPersonal_CheckedChanged(this, EventArgs.Empty);
        }

        public static void ShowIfNeccessary()
        {
            var inst = InstallationInfo.Instance;
            bool show = inst.InstallMode == InstallationMode.Unknown
                || (inst.InstallMode == InstallationMode.Professional && LicenseTool.ValidLicenses.Count == 1)
                || (inst.InstallMode == InstallationMode.Personal && (DateTime.UtcNow - inst.LastShown).TotalDays > 30);
            if (GlobalSettings.Pages.General().AskWhenUploadUsageStats) show = true;
            if (show)
            {
                var win = new InstallConfigForm();
                win.ShowDialogEx();
            }
        }

        private void btnQuit_Click(object sender, EventArgs e)
        {
            Close();
            MainWindow.Instance.CloseMainWindow();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            GlobalSettings.Pages.BeginEdit();
            GlobalSettings.Pages.General().AskWhenUploadUsageStats = false;
            GlobalSettings.Pages.General().AllowUploadUsageStats = chbAllowUploadStats.Checked;
            GlobalSettings.Pages.EndEdit();

            if (rbtPersonal.Checked) InstallationInfo.Instance.InstallMode = InstallationMode.Personal;
            if (rbtProfessional.Checked) InstallationInfo.Instance.InstallMode = InstallationMode.Professional;
            InstallationInfo.Instance.LastShown = DateTime.UtcNow;
            InstallationInfo.Instance.Save();

            if (grpEval.Enabled && (chbDataSynEval.Checked || chbProEval.Checked || chbVersionDbEval.Checked))
            {
                var evdata = GetEvalCodeForm.Run();
                if (evdata == null)
                {
                    DialogResult = DialogResult.None;
                    return;
                }
                int cnt = 0;
                using (var wc = new WaitContext())
                {
                    if (chbDataSynEval.Checked && GetEvalCode.GetLicense(evdata.Name, evdata.Email, "datasyn")) cnt++;
                    if (chbProEval.Checked && GetEvalCode.GetLicense(evdata.Name, evdata.Email, "pro")) cnt++;
                    if (chbVersionDbEval.Checked && GetEvalCode.GetLicense(evdata.Name, evdata.Email, "versiondb")) cnt++;
                }
                if (cnt > 0)
                {
                    LicenseTool.ReloadLicenses();
                    HLicense.CallChangedLicenses();
                    StdDialog.ShowInfo("s_license_succesfuly_installed");
                }
                else
                {
                    StdDialog.ShowError("s_error_when_install_license");
                }
            }
            Close();
        }

        private void lnkPro_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.shareit.com/product.html?productid=300362697&backlink=http%3A%2F%2Fdatadmin.com");
        }

        private void lnkVersionDb_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.shareit.com/product.html?productid=300413171&backlink=http%3A%2F%2Fdatadmin.com");
        }

        private void lnkDataSyn_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.shareit.com/product.html?productid=300413172&backlink=http%3A%2F%2Fdatadmin.com");
        }

        private void rbtPersonal_CheckedChanged(object sender, EventArgs e)
        {
            grpEval.Enabled = rbtProfessional.Checked;
        }

        private void lnkProducts_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://datadmin.com/en/products");
        }

        private void linkFacebook_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(Core.FacebookUrl);
        }

        private void linkTwitter_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start(Core.TwitterUrl);
        }
    }
}
