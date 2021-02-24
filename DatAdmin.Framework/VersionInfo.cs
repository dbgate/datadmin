using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace DatAdmin
{
    public static class VersionInfo
    {
        public static string BUILT_AT = "#BUILT_AT#";
        public static string SVN_REVISION = "#SVN_REVISION#";
        public static string VERSION = "#VERSION#";
        //public static string VERSION = "5.1.1";

        public static bool IsDevVersion { get { return VERSION.StartsWith("#"); } }

        static string m_programTitle = "#PRGTITLE#";
        static string m_programFolderName = "#PRGFOLDER#";
        static string m_pluginFilter = "#PLUGINFILTER#";
        static string m_databaseSamplesFolder = "#DATABASESAMPLESFOLDER#";
        static HashSetEx<string> m_pluginFilterSet = null;

        public static string ProgramTitle
        {
            get
            {
                if (m_programTitle.StartsWith("#")) return Framework.ProgramNeutralName;
                return m_programTitle;
            }
        }
        public static string ProgramFolderName
        {
            get
            {
                if (m_programFolderName.StartsWith("#")) return Framework.ProgramNeutralName;
                return m_programFolderName;
            }
        }

        public static bool AllowLoadPlugin(string name)
        {
            if (m_pluginFilter.StartsWith("#")) return true;
            if (m_pluginFilter.IsEmpty()) return true;
            if (m_pluginFilterSet == null) m_pluginFilterSet = new HashSetEx<string>(m_pluginFilter.ToLower().Split(';'));
            return m_pluginFilterSet.Contains(name.ToLower());
        }

        public static string DatabaseSamplesFolder
        {
            get
            {
                if (String.IsNullOrEmpty(m_databaseSamplesFolder) || m_databaseSamplesFolder.StartsWith("#")) return null;
                return System.IO.Path.Combine(Framework.ProgramDirectory, m_databaseSamplesFolder);
            }
        }

        public static string LicenseInfo = "#LICINFO#";
        public static string Brand = "#BRAND#";
        public static bool HideLicenseInfo = "#HIDELICINFO#" == "1";
        public static bool HidePurchaseLinks = "#HIDEPURCHASELINKS#" == "1";
        public static bool DenyCustomLicenses = "#DENYCUSTOMLICENSES#" == "1";
        public static bool HideGroupsInCreateDialog = "#HIDEGROUPSINCREATEDIALOG#" == "1";
        public static bool AllowAutoUpdate = "#ALLOWAUTOUPDATE#" == "1";

        public static string VersionTypeName
        {
            get
            {
                try
                {
                    var ar = VERSION.Split('.');
                    if (ar.Length == 3 || (ar.Length == 4 && Int32.Parse(ar[3]) < 100))
                    {
                        if (Int32.Parse(ar[1]) % 2 == 0) return "";
                        else return "BETA";
                    }
                    if (ar.Length == 4)
                    {
                        if (Int32.Parse(ar[1]) % 2 == 0) return "GAMMA";
                        else return "ALPHA";
                    }
                }
                catch { }
                return null;
            }
        }

        public static bool IsSnapshot
        {
            get
            {
                var ar = VERSION.Split('.');
                return ar.Length == 4 && Int32.Parse(ar[3]) >= 100;
            }
        }
        public static bool IsRelease
        {
            get { return VersionTypeName == ""; }
        }
        public static bool IsBeta
        {
            get { return VersionTypeName == "BETA"; }
        }
        public static bool IsGamma
        {
            get { return VersionTypeName == "GAMMA"; }
        }
        public static bool IsAlpha
        {
            get { return VersionTypeName == "ALPHA"; }
        }
        public static DateTime BuildAt
        {
            get
            {
                if (BUILT_AT.StartsWith("#")) return DateTime.UtcNow;
                return DateTime.ParseExact(BUILT_AT, "s", CultureInfo.InvariantCulture);
            }
        }
    }
}
