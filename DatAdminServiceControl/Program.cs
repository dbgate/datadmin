using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.ServiceProcess;

namespace DatAdmin
{
    class Program
    {
        private static ServiceController GetController()
        {
            return new ServiceController("DatAdminService");
        }

        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.Out.WriteLine("ERROR: Undefined action");
                return;
            }
            string action = args[0];
            switch (action)
            {
                case "install":
                    IntegratedServiceInstaller.Install("DatAdminService", "DatAdmin Service",
                        "Manages DatAdmin scheduled jobs",
                        Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "DatAdminService.exe"),
                        System.ServiceProcess.ServiceAccount.LocalSystem, System.ServiceProcess.ServiceStartMode.Automatic);
                    var si = new DatAdminServiceInfo();
                    si.Fill();
                    si.Save();
                    break;
                case "uninstall":
                    IntegratedServiceInstaller.Uninstall("DatAdminService");
                    break;
                case "start":
                    using (var sc = GetController())
                    {
                        sc.Start();
                    }
                    break;
                case "stop":
                    using (var sc = GetController())
                    {
                        sc.Stop();
                    }
                    break;
                default:
                    Console.Out.WriteLine("ERROR: Undefined action");
                    break;
            }
        }
    }
}
