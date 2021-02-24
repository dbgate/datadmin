using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Reflection;
using System.Threading;

namespace DatAdmin
{
    public static class Program
    {
        static bool IsMono;

        public static void PublicMain(string[] args)
        {
            Main(args);
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length > 0)
            {
                if (Singleton.SwitchToCurrentInstance(args)) return;
            }

            IsMono = Type.GetType("Mono.Runtime") != null;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

#if (DEBUG)
            RunMain(null, args);
#else
            try
            {
                RunMain(null, args);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());
            }
#endif
            ThreadRegister.QuitAllThreads();
        }

        static bool InitializeApp()
        {
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(Application_ThreadException);

            //SplashForm splash = null;
            if (!IsMono) SplashForm.Start();

            SplashForm.SetProgress("Loading core...", 0);
            //if (splash != null) splash.SetCurWork("Loading core...");
            Core.IsGUI = true;
            Core.Instance.CreateWantedDirs();
            HSplash.AddModuleInfo += SplashForm.AddModuleInfo;
            DeletedFileRegistrer.Initialize();
            Core.ConfigureLogging();
            AsyncTool.MainThread = Thread.CurrentThread;
            //NodeFactory.RegisterRootCreator(RootTreeNode.CreateRoot);
            FileTextProvider.LoadStdTexts();
            LicenseTool.ReloadLicenses();
            UICache.Load();

            PluginTools.AddMasterAssembly(Assembly.GetAssembly(typeof(Program)));
            PluginTools.LoadPlugins(SplashForm.LoadPluginCallback);

            SplashForm.SetProgress(Texts.Get("s_loading_configuration"), 80);
            //AddonLibrary.ReloadLibs();
            GlobalSettings.Initialize();
            InternetSettings.Initialize();
            Texts.Language = GlobalSettings.Pages.General().Language;
            HSettings.CallLoaded();

            if (CheckAutoUpdate.Run(SplashForm.EnsureNoSplash))
            {
                Environment.Exit(0);
                return true;
            }

            Core.RunAutoInstall();

            Core.CopyDefaultData();

            UsageStats.OnStart();
            Favorites.OnStart();
            //SendUsageForm.UploadIfNeeded(SplashForm.EnsureNoSplash);

            SplashForm.SetProgress(Texts.Get("s_creating_main_window"), 100);

            Logging.Info("Starting DatAdmin GUI");
            return false;
        }

        static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            Errors.Report(e.Exception);
        }

        static void FinalizeApp()
        {
            UICache.Save();
            UsageStats.OnFinish();
            Favorites.OnFinish();
            Logging.Info("Ending DatAdmin GUI");
            Logging.QuitLogging();
            Core.FinalizeApp();
            if (Core.ExecuteAfterFinalize != null)
            {
                System.Diagnostics.Process.Start(Core.ExecuteAfterFinalize);
            }
        }

        public static void RunMain(Action autoRun, string[] args)
        {
            if (InitializeApp()) return;
            for (int i = 0; i < args.Length; i++)
            {
                if (args[i] == "--appdata" && i + 1 < args.Length)
                {
                    Core.AppDataOverride = args[i + 1];
                }
                if (args[i] == "--data" && i + 1 < args.Length)
                {
                    Core.DataOverride = args[i + 1];
                }
            }
            MainForm win = new MainForm(autoRun, args);
            Application.Run(win);
            FinalizeApp();
        }
    }
}