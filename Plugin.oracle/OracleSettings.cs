using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.ComponentModel;
using System.Drawing.Design;
using System.IO;
using System.Runtime.InteropServices;

namespace Plugin.oracle
{
    [SettingsPage(Name = "oracle_client", Title = "Oracle/s_client_settings", Targets = SettingsTargets.Global)]
    public class OracleSettings : SettingsPageBase
    {
        public static bool? OciLibraryTestResult = null;

        [DllImport("kernel32")]
        public extern static IntPtr LoadLibrary(string libraryName);

        [DllImport("kernel32")]
        public extern static bool FreeLibrary(IntPtr handle);


        string m_oraclePath;

        [Category("Oracle")]
        [DatAdmin.DisplayName("s_client_path")]
        [SettingsKey("oracle.client_path")]
        [Editor(typeof(FolderNameEditor), typeof(UITypeEditor))]
        public string OraclePath
        {
            get { return m_oraclePath; }
            set { m_oraclePath = value; }
        }

        public OracleSettings()
        {
            if (Core.IsMono) m_oraclePath = "/usr/bin";
        }

        public static OracleSettings Page { get { return (OracleSettings)GlobalSettings.Pages.PageByName("oracle_client"); } }

        private bool OciLibraryAvailable()
        {
            if (OciLibraryTestResult == null)
            {
                var hnd = LoadLibrary("oci.dll");
                if (hnd != null && (int)hnd != 0)
                {
                    FreeLibrary(hnd);
                    OciLibraryTestResult = true;
                }
                else
                {
                    OciLibraryTestResult = false;
                }
            }
            return OciLibraryTestResult.Value;
        }

        public void CheckConfigured()
        {
            if (OciLibraryAvailable())
            {
                // OK, oci.dll is in system path
                return;
            }
            if (OraclePath.IsEmpty())
            {
                throw new BadSettingsError("DAE-00343 " + Texts.Get("s_oracle_path_not_configured"), "Oracle/s_client_settings");
            }
            if (!File.Exists(Path.Combine(OraclePath, "oci.dll")))
            {
                throw new BadSettingsError("DAE-00344 " + Texts.Get("s_oracle_client_oci_dll_not_found"), "Oracle/s_client_settings");
            }
        }
    }

    [PluginHandler]
    public class OracleSettingsPluginHandler : PluginHandlerBase
    {
        public override void OnLoadedAllPlugins()
        {
            base.OnLoadedAllPlugins();
            HSystem.GetSystemPathEx += HSystem_GetSystemPathEx;
            HSystem.SystemPathChanged += HSystem_SystemPathChanged;
            HSettings.ReloadSettings += HSettings_ReloadSettings;
            HSettings.Loaded += HSettings_Loaded;
        }

        void HSystem_SystemPathChanged()
        {
            OracleSettings.OciLibraryTestResult = null;
        }

        void HSettings_Loaded()
        {
            HSystem.RebuildSystemPath();
        }

        void HSettings_ReloadSettings()
        {
            HSystem.RebuildSystemPath();
        }

        void HSystem_GetSystemPathEx(List<string> obj)
        {
            if (!String.IsNullOrEmpty(OracleSettings.Page.OraclePath)) obj.Add(OracleSettings.Page.OraclePath);
        }
    }
}
