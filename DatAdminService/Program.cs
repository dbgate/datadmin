using System;
using System.Collections.Generic;
using System.ServiceProcess;
using System.Text;
using System.Reflection;

namespace DatAdmin
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            SystemLogTool.Info("Initializing DatAdmin Service process...");

            try
            {
                var si = new DatAdminServiceInfo();
                si.Load();
                si.Apply();

                Core.IsCommandLine = true;
                Core.Instance.CreateWantedDirs();
                Core.ConfigureLogging();

                FileTextProvider.LoadStdTexts();
                LicenseTool.ReloadLicenses();

                PluginTools.AddMasterAssembly(Assembly.GetAssembly(typeof(Program)));
                PluginTools.LoadPlugins();
                GlobalSettings.Initialize();
                InternetSettings.Initialize();

                FileTextProvider.LoadStdTexts();
                LicenseTool.ReloadLicenses();
                HSettings.CallLoaded();

                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[] 
			{ 
				new DatAdminService() 
			};
                ServiceBase.Run(ServicesToRun);
            }
            catch (Exception err)
            {
                SystemLogTool.Error("Failed to initialize DatAdmin Service process\n" + err.ToString());
            }
        }
    }
}
