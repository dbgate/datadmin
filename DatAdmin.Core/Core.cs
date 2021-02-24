using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Net;
using System.Reflection;
using System.Xml;
using System.Drawing;

namespace DatAdmin
{
    public class Core : Framework
    {
        static string m_dataOverride;

        /// <summary>
        /// file to run after finalize
        /// </summary>
        public static string ExecuteAfterFinalize;

        public override void CreateWantedDirs()
        {
            try { Directory.CreateDirectory(ConfigDirectory); }
            catch (Exception) { }
            try { Directory.CreateDirectory(DataDirectory); }
            catch (Exception) { }
            //try { Directory.CreateDirectory(PluginsDirectory); }
            //catch (Exception) { }
            try { Directory.CreateDirectory(SettingsDirectory); }
            catch (Exception) { }
            try { Directory.CreateDirectory(LogsDirectory); }
            catch (Exception) { }
            //try { Directory.CreateDirectory(UsageDirectory); }
            //catch (Exception) { }
            try { Directory.CreateDirectory(JobsDirectory); }
            catch (Exception) { }
            try { Directory.CreateDirectory(AppsDirectory); }
            catch (Exception) { }
            try { Directory.CreateDirectory(TablePerspectivesDirectory); }
            catch (Exception) { }
            try { Directory.CreateDirectory(DashboardsDirectory); }
            catch (Exception) { }
            try { Directory.CreateDirectory(DashboardLayoutsDirectory); }
            catch (Exception) { }
            try { Directory.CreateDirectory(AddonsDirectory); }
            catch (Exception) { }
            //try { Directory.CreateDirectory(CommandsDirectory); }
            //catch (Exception) { }
            //try { Directory.CreateDirectory(DataStoresDirectory); }
            //catch (Exception) { }
            //try { Directory.CreateDirectory(DiagramStylesDirectory); }
            //catch (Exception) { }
            //try { Directory.CreateDirectory(DialectsDirectory); }
            //catch (Exception) { }
            try { Directory.CreateDirectory(MacrosDirectory); }
            catch (Exception) { }
            //try { Directory.CreateDirectory(ObjectViewsDirectory); }
            //catch (Exception) { }
            //try { Directory.CreateDirectory(NodeGeneratorsDirectory); }
            //catch (Exception) { }
            try { Directory.CreateDirectory(AutoInstallDirectory); }
            catch (Exception) { }
            try { Directory.CreateDirectory(AddonLibsDirectory); }
            catch (Exception) { }
            try { Directory.CreateDirectory(ImagesDirectory); }
            catch (Exception) { }
            try { Directory.CreateDirectory(TempDirectory); }
            catch (Exception) { }
            try { Directory.CreateDirectory(IpcDirectory); }
            catch (Exception) { }
            try { Directory.CreateDirectory(LicensesDirectory); }
            catch (Exception) { }
            try { Directory.CreateDirectory(LayoutsDirectory); }
            catch (Exception) { }
            try { Directory.CreateDirectory(FavoritesDirectory); }
            catch (Exception) { }

            ClearTemp();
        }

        static Core()
        {
            ProgramCodeName = "datadmin";
            ProgramNeutralName = "DatAdmin";

            AddonRegister.CoreAssemblies.Add(Assembly.GetAssembly(typeof(Core)));

            Instance = new Core();
            //Instance.CreateWantedDirs();

            HUsage.OpenErrorSupport += SupportConnector.OpenErrorSupport;
            //SetupFrameworkHooks();
        }

        //private static void SetupFrameworkHooks()
        //{
        //    HFramework.ValueToString = XmlDataTool.CustomValueToStringOverride;
        //    HFramework.ValueFromString = XmlDataTool.CustomValueFromStringOverride;

        //    HFramework.FindAddonType = AddonTool.CustomFindAddonType;
        //    HFramework.AllowSendErrorReports = () => GlobalSettings.Pages.General().AllowSendErrorReports;

        //    Framework.NautralProgramName = "DatAdmin";
        //}

        public override bool ValueFromString(Type type, string sval, out object res)
        {
            return XmlDataTool.CustomValueFromStringOverride(type, sval, out res);
        }
        public override bool ValueToString(Type type, object val, out string res)
        {
            return XmlDataTool.CustomValueToStringOverride(type, val, out res);
        }
        public override AddonType FindAddonType(Type interfaceType)
        {
            if (interfaceType.IsSubclassOf(typeof(AppObject))) return AppObjectAddonType.Instance; // handle subtypes
            return null;
        }
        public override bool AllowSendErrorReports()
        {
            return GlobalSettings.Pages.General().AllowSendErrorReports;
        }
        public override bool AllowSendUsageStats()
        {
            return GlobalSettings.Pages.General().AllowUploadUsageStats;
        }
        public override void ShowOptions(string cfgpath)
        {
            OptionsForm.Run(cfgpath);
        }
        public override void AddFixedLicenses()
        {
            var pdoc = new XmlDocument();
            pdoc.LoadXml(CoreRes.personal);
            LicenseTool.AddLicense(new License(pdoc.DocumentElement, "DatAdmin.Core.dll"));
        }
        public override CheckNewVersions GetCheckNewVersion()
        {
            return GlobalSettings.Pages.General().CheckNewVersion;
        }
        public override string GetPublicKey()
        {
            return CoreRes.publickey;
        }
        public override System.Drawing.Bitmap ImageFromName(string name, Bitmap defimage)
        {
            return ImageTool.ImageFromName(name, defimage);
        }
        //public override void InitializeInstallInfo(InstallationInfo inst)
        //{
        //    if (inst.InstallMode == InstallationMode.Unknown)
        //    {
        //        bool personal = LicenseTool.ValidLicenses.Count == 1 && LicenseTool.InvalidLicenses.Count == 0;
        //        inst.Save();
        //    }
        //}

        public static string DataOverride
        {
            get { return m_dataOverride; }
            set
            {
                m_dataOverride = value;
                Instance.CreateWantedDirs();
            }
        }

        public static string DataDirectory
        {
            get
            {
                if (DataOverride != null) return DataOverride;
                return Path.Combine(AppDataDirectory, "data");
            }
        }
        public static string DataSamplesDirectory
        {
            get { return Path.Combine(DataDirectory, "Samples"); }
        }
        public static string ScriptsDirectory
        {
            get { return Path.Combine(AppDataDirectory, "scripts"); }
        }
        public static string ChartsDirectory
        {
            get { return Path.Combine(AppDataDirectory, "charts"); }
        }
        public static string LayoutsDirectory
        {
            get { return Path.Combine(ConfigDirectory, "layouts"); }
        }
        public static string MacrosDirectory
        {
            get { return Path.Combine(ConfigDirectory, "macros"); }
        }
        public static string JobsDirectory
        {
            get { return Path.Combine(ConfigDirectory, "jobs"); }
        }
        public static string AppsDirectory
        {
            get { return Path.Combine(ConfigDirectory, "apps"); }
        }
        public static string FilesDirectory
        {
            get { return Path.Combine(ProgramDirectory, "files"); }
        }
        public static string ImagesDirectory
        {
            get { return Path.Combine(ProgramDirectory, "images"); }
        }
        public static string ProgramDirectory
        {
            get { return AppDomain.CurrentDomain.BaseDirectory; }
        }
        public static string FullExeName
        {
            get { return Assembly.GetExecutingAssembly().Location; }
        }
        public static string IpcDirectory
        {
            get { return Path.Combine(TempDirectory, "ipc"); }
        }
        public static string LayoutFile
        {
            get { return Path.Combine(ConfigDirectory, "layout.xml"); }
        }
        public static string SettingsDirectory
        {
            get { return Path.Combine(ConfigDirectory, "settings"); }
        }
        public static string FavoritesDirectory
        {
            get { return Path.Combine(ConfigDirectory, "favorites"); }
        }
        public static string LangDirectoryOverride;
        public static string LangDirectory
        {
            get { return LangDirectoryOverride ?? Path.Combine(ProgramDirectory, "lang"); }
        }
        public static string SelfTestDirectory
        {
            get { return Path.Combine(ProgramDirectory, "selftest"); }
        }
        //public static string UsageDirectory
        //{
        //    get { return Path.Combine(AppDataDirectory, "usage"); }
        //}
        public static string TablePerspectivesDirectory
        {
            get { return Path.Combine(ConfigDirectory, "tblpers"); }
        }
        public static string DashboardsDirectory
        {
            get { return Path.Combine(ConfigDirectory, "dashboards"); }
        }
        public static string DashboardLayoutsDirectory
        {
            get { return Path.Combine(ConfigDirectory, "dashlayouts"); }
        }

        public static void RunAutoInstall()
        {
            //foreach (string fn in Directory.GetFiles(AutoInstallDirectory))
            //{
            //    if (fn.ToLower().EndsWith(".adp"))
            //    {
            //        try
            //        {
            //            Logging.Info("Installing file:" + fn);
            //            AddonDbTool.ImportAddons(fn);
            //            File.Delete(fn);
            //        }
            //        catch (Exception e)
            //        {
            //            Errors.Report(e);
            //        }
            //    }
            //}
        }
        //public static void ShowHelp(Control parent, string topic)
        //{
        //    Help.ShowHelp(parent, "datadmin-" + Texts.Language + ".chm", topic + "-" + Texts.Language + ".html");
        //}

        //public static void ShowHelp(Control parent)
        //{
        //    Help.ShowHelp(parent, "datadmin-" + Texts.Language + ".chm");
        //}
		
        public static void CopyDefaultData()
        {
            if (!IsMono) return;
            Logging.Debug("Copying default data");
            try
            {
                IOTool.CopyDirectory("/etc/datadmin/.config", Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
            }
            catch (Exception e)
            {
                Logging.Root.LogError(e);
            }
        }

        public static void ShowHelp(string topic)
        {
            System.Diagnostics.Process.Start("http://help.datadmin.com/topic/" + topic);
        }

        public static void ShowHelp()
        {
            System.Diagnostics.Process.Start("http://help.datadmin.com/");
        }

        public static void ConfigureLogging()
        {
            if (Core.IsGUI)
            {
                Logging.MultiCast.AddLogger(new LogsDirectoryFileLogger("yyyy'-'MM'-'dd'.log'", () => GlobalSettings.Pages != null ? GlobalSettings.Pages.Log().FileLogLevel : LogLevel.Warning));
            }
            else
            {
                Logging.MultiCast.AddLogger(new LogsDirectoryFileLogger("'cli-'yyyy'-'MM'-'dd'-'HH'-'mm'-'ss'.log'", () => GlobalSettings.Pages != null ? GlobalSettings.Pages.Log().CliFileLogLevel : LogLevel.Warning));
                Logging.MultiCast.AddLogger(new ConsoleLogger());
            }
            //Logging.MultiCast.AddLogger(m_feedbackCache);
        }

        public static string FacebookUrl
        {
            get
            {
                return "http://www.facebook.com/pages/DatAdmin/166854033336408";
            }
        }

        public static string TwitterUrl
        {
            get
            {
                return "http://twitter.com/#!/jenasoft";
            }
        }
    }
}
