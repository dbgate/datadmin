using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;

namespace Plugin.oledb
{
    public static class DsnEnumerator
    {
        /// <summary>
        /// Gets all System data source names for the local machine.
        /// </summary>
        public static List<string> GetSystemDataSourceNames()
        {
            var dsnList = new List<string>();

            // get system dsn's
            Microsoft.Win32.RegistryKey reg = (Microsoft.Win32.Registry.LocalMachine).OpenSubKey("Software");
            if (reg != null)
            {
                reg = reg.OpenSubKey("ODBC");
                if (reg != null)
                {
                    reg = reg.OpenSubKey("ODBC.INI");
                    if (reg != null)
                    {
                        reg = reg.OpenSubKey("ODBC Data Sources");
                        if (reg != null)
                        {
                            // Get all DSN entries defined in DSN_LOC_IN_REGISTRY.
                            foreach (string sName in reg.GetValueNames())
                            {
                                dsnList.Add(sName);
                            }
                        }
                        try
                        {
                            reg.Close();
                        }
                        catch { /* ignore this exception if we couldn't close */ }
                    }
                }
            }

            return dsnList;
        }

        /// <summary>
        /// Gets all User data source names for the local machine.
        /// </summary>
        public static List<string> GetUserDataSourceNames()
        {
            var dsnList = new List<string>();

            // get user dsn's
            Microsoft.Win32.RegistryKey reg = (Microsoft.Win32.Registry.CurrentUser).OpenSubKey("Software");
            if (reg != null)
            {
                reg = reg.OpenSubKey("ODBC");
                if (reg != null)
                {
                    reg = reg.OpenSubKey("ODBC.INI");
                    if (reg != null)
                    {
                        reg = reg.OpenSubKey("ODBC Data Sources");
                        if (reg != null)
                        {
                            // Get all DSN entries defined in DSN_LOC_IN_REGISTRY.
                            foreach (string sName in reg.GetValueNames())
                            {
                                dsnList.Add(sName);
                            }
                        }
                        try
                        {
                            reg.Close();
                        }
                        catch { /* ignore this exception if we couldn't close */ }
                    }
                }
            }

            return dsnList;
        }

        // Returns a list of data source names from the local machine.
        public static List<string> GetAllDataSourceNames()
        {
            // Get the list of user DSN's first.
            var dsnList = GetUserDataSourceNames();

            // Get list of System DSN's and add them to the first list.
            var systemDsnList = GetSystemDataSourceNames();

            var res = new HashSetEx<string>();
            res.AddRange(dsnList);
            res.AddRange(systemDsnList);
            return new List<string>(res);
        }
    }
}
