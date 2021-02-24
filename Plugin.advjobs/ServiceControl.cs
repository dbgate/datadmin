using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using DatAdmin;
using System.ServiceProcess;

namespace Plugin.advjobs
{
    public static class ServiceControl
    {
        public static void Install()
        {
            System.Diagnostics.Process.Start(Path.Combine(Core.ProgramDirectory, "DatAdminServiceControl.exe"), "install");
        }

        public static void Uninstall()
        {
            System.Diagnostics.Process.Start(Path.Combine(Core.ProgramDirectory, "DatAdminServiceControl.exe"), "uninstall");
        }

        private static ServiceController GetController()
        {
            return new ServiceController("DatAdminService");
        }

        public static void Start()
        {
            System.Diagnostics.Process.Start(Path.Combine(Core.ProgramDirectory, "DatAdminServiceControl.exe"), "start");
        }

        public static void Stop()
        {
            System.Diagnostics.Process.Start(Path.Combine(Core.ProgramDirectory, "DatAdminServiceControl.exe"), "stop");
        }

        public static string GetState()
        {
            try
            {
                using (var sc = GetController())
                {
                    switch (sc.Status)
                    {
                        case ServiceControllerStatus.Running:
                            return "s_running";
                        case ServiceControllerStatus.Stopped:
                            return "s_stopped";
                        case ServiceControllerStatus.ContinuePending:
                        case ServiceControllerStatus.PausePending:
                        case ServiceControllerStatus.StartPending:
                        case ServiceControllerStatus.StopPending:
                            return "s_pending";
                        default:
                            return "s_unknown";
                    }
                }
            }
            catch
            {
                return "s_not_installed";
            }
        }

        public static bool IsRunning()
        {
            try
            {
                using (var sc = GetController())
                {
                    return sc.Status == ServiceControllerStatus.Running;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
