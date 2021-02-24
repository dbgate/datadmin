using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.Reflection;
using System.Threading;

namespace DatAdmin
{
    public class DaciProgram
    {
        public static void PublicMain(string[] args)
        {
            Main(args);
        }

        static void Main(string[] args)
        {
            Core.IsCommandLine = true;
            Core.Instance.CreateWantedDirs();
            Core.ConfigureLogging();

            FileTextProvider.LoadStdTexts();
            LicenseTool.ReloadLicenses();

            PluginTools.AddMasterAssembly(Assembly.GetAssembly(typeof(DaciProgram)));
            PluginTools.LoadPlugins();
            GlobalSettings.Initialize();
            InternetSettings.Initialize();
            HSettings.CallLoaded();

            //Async.MainThread = Thread.CurrentThread;

            try
            {
                if (args.Length == 0)
                {
                    CmdLine.PrintHelp();
                    Environment.Exit(0);
                }
                Logging.Info("Starting DACI - DatAdmin CommandLine interface");
                ICommandLineCommandInstance cmd = CmdLine.LoadCommand(args);
                cmd.RunCommand();
            }
            catch (ExpectedError e)
            {
                Logging.Error(e.Message);
            }
            catch (Exception e)
            {
                Logging.Error("Fatal error:" + e.ToString());
            }
            Logging.QuitAndWait();
            Core.FinalizeApp();
            Environment.Exit(0);
        }
    }
}
