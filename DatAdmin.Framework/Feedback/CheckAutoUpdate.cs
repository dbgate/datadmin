using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Xml;
using System.Windows.Forms;
using System.Threading;
using System.Globalization;
using System.ComponentModel;

namespace DatAdmin
{
    public enum CheckNewVersions
    {
        [Description("s_dont_check")]
        DontCheck,
        [Description("s_check_only")]
        CheckOnly,
        [Description("s_check_and_download")]
        CheckAndDownload
    }

    public class CheckAutoUpdate
    {
        //public static int? UpdateID;
        //public static bool SendErrorLogs = true;
        //public static int ScreenshotWidth = 0;
        //public static bool SendErrorScreenshots { get { return ScreenshotWidth > 0; } }

        static string IfBetaSuffix { get { if (VersionInfo.IsBeta) return "-beta"; return ""; } }
        public static string VersionFile { get { return Path.Combine(Framework.AutoInstallDirectory, Framework.ProgramCodeName + IfBetaSuffix + "-version.txt"); } }
        public static string InstallExeFile { get { return Path.Combine(Framework.AutoInstallDirectory, Framework.ProgramCodeName + IfBetaSuffix + "-install.exe"); } }
        public static string InstallTimestampFile { get { return Path.Combine(Framework.AutoInstallDirectory, Framework.ProgramCodeName + IfBetaSuffix + "-date.txt"); } }

        public static int CompareVersions(string v1, string v2)
        {
            List<int> l1 = new List<int>(), l2 = new List<int>();
            foreach (string s in v1.Split('.')) l1.Add(Int32.Parse(s));
            foreach (string s in v2.Split('.')) l2.Add(Int32.Parse(s));
            for (int i = 0; i < Math.Min(l1.Count, l2.Count); i++)
            {
                if (l1[i] < l2[i]) return -1;
                if (l1[i] > l2[i]) return 1;
            }
            return l1.Count - l2.Count;
        }

        private static bool m_checked = false;
        private static void DoRunCheckThreadCore()
        {
            var desc = ApiDescriptor.GetInstance();
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(desc.GetUpdate);

            req.ContentType = "application/x-www-form-urlencoded; charset=utf-8";
            req.Method = "POST";
            Dictionary<string, string> pars = new Dictionary<string, string>();
            FeedbackTool.FillStdParams(pars, true);
            if (m_checked) pars["REPEATED"] = "1";
            string pars_enc = StringTool.UrlEncode(pars, Encoding.UTF8);
            byte[] data = Encoding.UTF8.GetBytes(pars_enc);
            req.ContentLength = data.Length;
            req.Timeout = 5000;
            using (Stream requestStream = req.GetRequestStream())
            {
                requestStream.Write(data, 0, data.Length);
            }

            using (WebResponse resp = req.GetResponse())
            {
                XmlDocument doc = new XmlDocument();
                using (Stream fr = resp.GetResponseStream())
                {
                    doc.Load(fr);
                }
                string version = doc.SelectSingleNode("/update/version").InnerText;
                string url = doc.SelectSingleNode("/update/url").InnerText;
                //try { UpdateID = Int32.Parse(doc.SelectSingleNode("/update/updateid").InnerText); }
                //catch { }
                DateTime updatedat;
                try { updatedat = DateTime.ParseExact(doc.SelectSingleNode("/update/updatedat").InnerText, "s", CultureInfo.InvariantCulture); }
                catch { updatedat = DateTime.UtcNow; }
                //try { SendErrorLogs = doc.SelectSingleNode("/update/errorlogs").InnerText == "1"; }
                //catch { SendErrorLogs = true; }
                //try { ScreenshotWidth = Int32.Parse(doc.SelectSingleNode("/update/screenshots").InnerText); }
                //catch { ScreenshotWidth = 0; }

                if (VersionInfo.AllowAutoUpdate && CompareVersions(version, VersionInfo.VERSION) > 0)
                {
                    if (Framework.Instance.GetCheckNewVersion() == CheckNewVersions.CheckOnly)
                    {
                        //MainWindow.Instance.RunInMainWindow(delegate() { StdDialog.ShowInfo(Texts.Get("s_new_version_available$version", "version", version)); });
                        Framework.MainWindow.SendVersionInfo(Texts.Get("s_new_version_available$version", "version", version), null);
                    }
                    if (Framework.Instance.GetCheckNewVersion() == CheckNewVersions.CheckAndDownload)
                    {
                        if (File.Exists(VersionFile) && File.Exists(InstallExeFile))
                        {
                            string downloadedVersion = IOTool.LoadTextFile(VersionFile).Trim();
                            if (CompareVersions(version, downloadedVersion) > 0)
                            {
                                File.Delete(VersionFile);
                                File.Delete(InstallExeFile);
                            }
                            else
                            {
                                Logging.Debug("Version allready downloaded");
                                return;
                            }
                        }

                        Framework.MainWindow.SendVersionInfo(Texts.Get("s_downloading_version$version", "version", version), null);
                        WebRequest req2 = WebRequest.Create(url);
                        using (WebResponse resp2 = req2.GetResponse())
                        {
                            using (Stream fr = resp2.GetResponseStream())
                            {
                                using (FileStream fw = new FileStream(InstallExeFile, FileMode.Create))
                                {
                                    IOTool.CopyStream(fr, fw);
                                }
                            }
                        }
                        using (StreamWriter fw = new StreamWriter(VersionFile)) fw.Write(version);
                        using (StreamWriter fw = new StreamWriter(InstallTimestampFile)) fw.Write(updatedat.ToString("s"));

                        Framework.MainWindow.SendVersionInfo(Texts.Get("s_new_version_downloaded$version", "version", version), version);
                    }
                }
                else
                {
                    if (VersionInfo.AllowAutoUpdate)
                    {
                        Framework.MainWindow.SendVersionInfo(Texts.Get("s_you_have_current_version"), null);
                    }
                }
            }
        }

        private static void DoRunCheckThread()
        {
            try
            {
                DoRunCheckThreadCore();
            }
            catch (Exception e)
            {
                Logging.Error("Failed check new version:" + e.ToString());
                Framework.WaitForMainWindow();
                Framework.MainWindow.SendVersionInfo(Texts.Get("s_version_check_failed"), null);
            }
        }

        private static void RunCheckThread()
        {
            Framework.WaitForMainWindow();
            for (; ; )
            {
                if (Framework.Instance.GetCheckNewVersion() != CheckNewVersions.DontCheck)
                {
                    DoRunCheckThread();
                    m_checked = true;
                }
                Thread.Sleep(TimeSpan.FromMinutes(5));
            }
        }

        public static bool Run(Action closesplash)
        {
            // debug version, don't check auto-update
            if (!VersionInfo.IsRelease && !VersionInfo.IsBeta) return false;

            if (Framework.Instance.GetCheckNewVersion() == CheckNewVersions.CheckAndDownload)
            {
                if (File.Exists(VersionFile) && File.Exists(InstallExeFile))
                {
                    string newver = IOTool.LoadTextFile(VersionFile).Trim();
                    DateTime newverdt;
                    try { newverdt = DateTime.ParseExact(IOTool.LoadTextFile(InstallTimestampFile).Trim(), "s", CultureInfo.InvariantCulture); }
                    catch { newverdt = new DateTime(1900, 1, 1); }
                    if (CompareVersions(newver, VersionInfo.VERSION) > 0)
                    {
                        closesplash();
                        var lst = new List<string>();
                        foreach (var lic in LicenseTool.ValidLicenses)
                        {
                            if (lic.UpdatesTo < newverdt)
                            {
                                lst.Add(lic.LongText);
                            }
                        }
                        string msg = Texts.Get("s_install_version$version", "version", newver);
                        if (lst.Count > 0) msg += "\n" + Texts.Get("s_license_update_expire_warning") + "\n" + lst.CreateDelimitedText("\n");
                        if (MessageBox.Show(msg, VersionInfo.ProgramTitle, MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            System.Diagnostics.Process.Start(InstallExeFile);
                            return true;
                        }
                    }
                }
            }
            Thread t = new Thread(RunCheckThread);
            t.IsBackground = true;
            t.Start();
            return false;
        }
    }
}
