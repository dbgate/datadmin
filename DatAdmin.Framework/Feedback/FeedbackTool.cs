using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Drawing;

namespace DatAdmin
{
    public static class FeedbackTool
    {
        public static string OSVersion()
        {
            return String.Format("{0}; {1} bit", System.Environment.OSVersion.VersionString, IntPtr.Size * 8);
        }

        public static void FillStdParams(Dictionary<string, string> pars, bool addUsage)
        {
            pars["VERSION"] = VersionInfo.VERSION;
            pars["START"] = UsageStats.ProgramStartedAt.ToString("s");
            pars["EDITION"] = LicenseTool.EditionText();
            pars["REGEMAIL"] = LicenseTool.RegEmails();
            pars["REGNAME"] = LicenseTool.RegisteredToUser();
            pars["INSTMODE"] = InstallationInfo.Instance.InstallMode.ToString();
            pars["OSVERSION"] = OSVersion();
            pars["LANGUAGE"] = Texts.Language;
            pars["INSTID"] = InstallationInfo.Instance.InstallID;
            pars["EXEID"] = Framework.ExecuteID;
            pars["ALLOWSTATS"] = Framework.Instance.AllowSendUsageStats() ? "1" : "0";
            if (VersionInfo.IsRelease) pars["VERTYPE"] = "release";
            if (VersionInfo.IsBeta) pars["VERTYPE"] = "beta";
            pars["BRAND"] = VersionInfo.Brand;
            if (addUsage)
            {
                pars["LICENSES"] = LicenseTool.GetFeedbackLicenseInfo();
                pars["USAGE"] = UsageStats.GetAndClear();
            }
        }
    }
}
