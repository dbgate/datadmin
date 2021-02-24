using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;
using System.Drawing;

namespace DatAdmin
{
    public class Framework
    {
        public static Framework Instance = new Framework();
        public static IFrameworkMainWindow MainWindow;

        static string m_appDataOverride;
        static HashSetEx<string> m_usedTempNames = new HashSetEx<string>();
        static int m_lastTempFile;

        public static string ProgramNeutralName = "MyApp";
        public static string ProgramCodeName = "myapp";

        public static readonly string ExecuteID = Guid.NewGuid().ToString();

        public static bool IsGUI;
        public static bool IsCommandLine;
        public static bool IsDesigning
        {
            get { return !IsGUI && !IsCommandLine; }
        }

        public static bool IsMono { get { return Type.GetType("Mono.Runtime") != null; } }
        public static string ExeSuffix { get { return IsMono ? "" : ".exe"; } }

        public static string AppDataOverride
        {
            get { return m_appDataOverride; }
            set
            {
                m_appDataOverride = value;
                Instance.CreateWantedDirs();
            }
        }

        public static void ClearTemp()
        {
            foreach (string fn in Directory.GetFiles(TempDirectory))
            {
                try { File.Delete(fn); }
                catch { m_usedTempNames.Add(Path.GetFileName(fn).ToLower()); }
            }
        }

        public static string GetTempFile(string ext)
        {
            if (ext == null) ext = "";
            if (ext.Length > 1 && !ext.StartsWith(".")) ext = "." + ext;
            while (m_usedTempNames.Contains("tmpfile" + m_lastTempFile.ToString() + ext))
            {
                m_lastTempFile++;
            }
            string name = "tmpfile" + m_lastTempFile.ToString() + ext;
            m_usedTempNames.Add(name);
            return Path.Combine(TempDirectory, name);
        }

        public static void FinalizeApp()
        {
            ClearTemp();
        }

        public virtual void CreateWantedDirs()
        {
        }

        public static string ProgramDirectory
        {
            get { return AppDomain.CurrentDomain.BaseDirectory; }
        }
        public static string AppDataDirectory
        {
            get
            {
                if (AppDataOverride != null) return AppDataOverride;
                return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), VersionInfo.ProgramFolderName);
            }
        }
        public static string TempDirectory
        {
            get { return Path.Combine(AppDataDirectory, "temp"); }
        }
        public static string AutoInstallDirectory
        {
            get { return Path.Combine(AppDataDirectory, "auto-install"); }
        }
        public static string PluginsDirectoryOverride;
        public static string PluginsDirectory
        {
            get { return PluginsDirectoryOverride ?? ProgramDirectory; }
        }
        public static string LicensesDirectory
        {
            get { return Path.Combine(AppDataDirectory, "licenses"); }
        }
        public static string ConfigDirectory
        {
            get { return Path.Combine(AppDataDirectory, "cfg"); }
        }
        public static string AddonsDirectory
        {
            get { return Path.Combine(ConfigDirectory, "addons"); }
        }
        public static string LibDirectory
        {
            get { return Path.Combine(ProgramDirectory, "lib"); }
        }
        public static string AddonLibsDirectory
        {
            get { return Path.Combine(LibDirectory, "addons"); }
        }
        public static string LogsDirectory
        {
            get { return Path.Combine(AppDataDirectory, "logs"); }
        }
        public static string InstallInfoFile
        {
            get { return Path.Combine(ConfigDirectory, "instinfo.xml"); }
        }

        // XML Tool hooks
        public virtual bool ValueToString(Type type, object val, out string res)
        {
            res = "";
            return false;
        }
        public virtual bool ValueFromString(Type type, string sval, out object res)
        {
            res = null;
            return false;
        }

        public virtual AddonType FindAddonType(Type type)
        {
            return null;
        }
        public virtual bool AllowSendErrorReports()
        {
            return false;
        }

        public virtual void ShowOptions(string cfgpath)
        {
        }

        public virtual void AddFixedLicenses()
        {
        }

        public virtual CheckNewVersions GetCheckNewVersion()
        {
            return CheckNewVersions.DontCheck;
        }

        public static void WaitForMainWindow()
        {
            while (MainWindow == null) Thread.Sleep(100);
        }

        public virtual string GetPublicKey()
        {
            return null;
        }

        public virtual Bitmap ImageFromName(string name, Bitmap defimage)
        {
            return defimage;
        }

        //public virtual void InitializeInstallInfo(InstallationInfo inst)
        //{
        //}

        public virtual bool AllowSendUsageStats()
        {
            return false;
        }
    }
}
