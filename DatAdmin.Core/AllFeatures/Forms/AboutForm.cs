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
    public partial class AboutForm : FormEx
    {
        public AboutForm()
        {
            InitializeComponent();
            ReloadLicenses();

            if (!String.IsNullOrEmpty(LicenseTool.RegisteredToUser()))
            {
                labRegistration.Text = Texts.Get("s_registered_to$user", "user", LicenseTool.RegisteredToUser());
            }
            else
            {
                labRegistration.Text = Texts.Get("s_unregistered");
            }
            labVersion.Text = VersionInfo.VERSION;
            labRevision.Text = VersionInfo.SVN_REVISION;
            labBuildAt.Text = VersionInfo.BuildAt.ToShortDateString();
            labDataDirectory.Text = Core.AppDataDirectory;
            labProgramDirectory.Text = Core.ProgramDirectory;
            labOperatingSystem.Text = System.Environment.OSVersion.VersionString;
            labApplicationMode.Text = String.Format("{0} bit", IntPtr.Size * 8);

            var sb = new StringBuilder();
            sb.AppendLine(String.Format("{0}: Jean Pierre Ravez", Texts.Get("s_french_localization")));
            sb.AppendLine(String.Format("{0}: Stefano Leardini", Texts.Get("s_italian_localization")));
            sb.AppendLine(String.Format("{0}: 王笑宇 48596@qq.com", Texts.Get("s_chinese_localization")));
            sb.AppendLine("");
            sb.AppendLine(String.Format("{0}:", Texts.Get("s_used_libraries")));
            sb.AppendLine("ICSharpCode - #ziplib, text editor");
            sb.AppendLine("Iron Python");
            sb.AppendLine("SQLite database, SQLite.NET");
            sb.AppendLine("Menees Diff.NET");
            sb.AppendLine("MySQL ADO.NET Connector");
            sb.AppendLine("NauckIT PostgreSQL Provider");
            sb.AppendLine("LINQBridge");
            sb.AppendLine("LumenWorks.Framework.IO");
            sb.AppendLine("EffiProz (if you are using this DB for non-commercial use, you need to purchase license)");
            sb.AppendLine("    http://www.effiproz.com/License.aspx");
            sb.AppendLine("Icons: http://dryicons.com");
            tbxCredits.Text = sb.ToString().Replace("\n", "\r\n");

            lsvLicenses_SelectedIndexChanged(this, EventArgs.Empty);

            label1.Text = VersionInfo.ProgramTitle;

            btnInstallLicense.Visible = !VersionInfo.DenyCustomLicenses;
            btnRemoveLicense.Visible = !VersionInfo.DenyCustomLicenses;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://datadmin.com");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            SupportConnector.SupportRequest();
            //System.Diagnostics.Process.Start("mailto:support@datadmin.com");
        }

        private void btnInstallLicense_Click(object sender, EventArgs e)
        {
            if (openFileDialogLicense.ShowDialogEx() == DialogResult.OK)
            {
                var lic = LicenseTool.LoadLicense(openFileDialogLicense.FileName);
                if (lic != null)
                {
                    LicenseTool.InstallLicense(openFileDialogLicense.FileName);
                    StdDialog.ShowInfo("s_license_installed_please_restart");
                    ReloadLicenses();
                }
                else
                {
                    StdDialog.ShowError("s_license_file_is_not_valid");
                }
            }
        }

        private void AddLincese(License lic, int imgindex)
        {
            var item = lsvLicenses.Items.Add(lic.LongText);
            item.ImageIndex = imgindex;
            item.Tag = lic;
            item.SubItems.Add(LicenseTool.FormatLicenceDate(lic.ActiveTo));
            item.SubItems.Add(LicenseTool.FormatLicenceDate(lic.SupportTo));
            item.SubItems.Add(LicenseTool.FormatLicenceDate(lic.UpdatesTo));
            item.SubItems.Add(lic.UserName);
            item.SubItems.Add(lic.UserEmail);
        }

        private void ReloadLicenses()
        {
            lsvLicenses.Items.Clear();
            foreach (var lic in LicenseTool.ValidLicenses) AddLincese(lic, 1);
            foreach (var lic in LicenseTool.InvalidLicenses) AddLincese(lic, 0);

            lsvFeatures.Items.Clear();
            foreach (var holder in FeatureAddonType.Instance.CommonSpace.GetAllAddons())
            {
                var item = lsvFeatures.Items.Add(holder.Title);
                item.ImageIndex = LicenseTool.FeatureAllowed(holder.Name) ? 1 : 0;
                item.SubItems.Add(holder.GetDefiner());
                var lst = new List<string>();
                foreach (var lic in LicenseTool.ValidLicenses)
                {
                    if (lic.FeatureAllowed(holder.Name)) lst.Add(lic.LongText);
                }
                item.SubItems.Add(lst.CreateDelimitedText(","));
            }
        }

        public static void RunLicenses()
        {
            var win = new AboutForm();
            win.tabControl1.SelectedIndex = 1;
            win.ShowDialogEx();
        }

        private void btnRemoveLicense_Click(object sender, EventArgs e)
        {
            if (lsvLicenses.SelectedItems.Count > 0)
            {
                if (MessageBox.Show(Texts.Get("s_really_delete$licenses", "licenses", lsvLicenses.SelectedItems.Count), VersionInfo.ProgramTitle, MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    foreach (ListViewItem item in lsvLicenses.SelectedItems)
                    {
                        var lic = (License)item.Tag;
                        File.Delete(lic.Filename);
                    }
                }
                LicenseTool.ReloadLicenses();
                ReloadLicenses();
            }
        }

        private void lsvLicenses_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool enabled = false;
            foreach (ListViewItem item in lsvLicenses.SelectedItems)
            {
                var lic = (License)item.Tag;
                if (lic.Filename != null && Path.GetExtension(lic.Filename).ToLower() != ".dll") enabled = true;
            }
            btnRemoveLicense.Enabled = enabled;
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
