using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using Microsoft.Win32;

namespace DatAdmin
{
    public static class IntegratedServiceInstaller
    {
        public static void Install(String ServiceName, String DisplayName, String Description, String exeName,
                System.ServiceProcess.ServiceAccount Account,
                System.ServiceProcess.ServiceStartMode StartMode)
        {
            //http://www.theblacksparrow.com/
            System.ServiceProcess.ServiceProcessInstaller ProcessInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            ProcessInstaller.Account = Account;

            System.ServiceProcess.ServiceInstaller SINST = new System.ServiceProcess.ServiceInstaller();

            System.Configuration.Install.InstallContext Context = new System.Configuration.Install.InstallContext();
            System.IO.FileInfo fi = new System.IO.FileInfo(exeName);

            String path = String.Format("/assemblypath={0}", fi.FullName);
            String[] cmdline = { path };
            Context = new System.Configuration.Install.InstallContext("", cmdline);

            SINST.Context = Context;
            SINST.DisplayName = DisplayName;
            SINST.Description = Description;
            SINST.ServiceName = ServiceName;
            SINST.StartType = StartMode;
            SINST.Parent = ProcessInstaller;

            // http://bytes.com/forum/thread527221.html
            //SINST.ServicesDependedOn = new String[] { "Spooler", "Netlogon", "Netman" };
            SINST.ServicesDependedOn = new String[] { };

            System.Collections.Specialized.ListDictionary state = new System.Collections.Specialized.ListDictionary();
            SINST.Install(state);

            // http://www.dotnet247.com/247reference/msgs/43/219565.aspx
            using (RegistryKey oKey = Registry.LocalMachine.OpenSubKey(String.Format(@"SYSTEM\CurrentControlSet\Services\{0}", ServiceName), true))
            {
                try
                {
                    Object sValue = oKey.GetValue("ImagePath");
                    oKey.SetValue("ImagePath", sValue);
                }
                catch (Exception Ex)
                {
                    System.Windows.Forms.MessageBox.Show(Ex.Message);
                }
            }
        }

        public static void Uninstall(String ServiceName)
        {
            //http://www.theblacksparrow.com/
            System.ServiceProcess.ServiceInstaller SINST = new System.ServiceProcess.ServiceInstaller();

            System.Configuration.Install.InstallContext Context = new System.Configuration.Install.InstallContext("c:\\install.log", null);
            SINST.Context = Context;
            SINST.ServiceName = ServiceName;
            SINST.Uninstall(null);
        }
    }
}
