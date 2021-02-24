using System;
using System.Collections.Generic;
using System.Text;

namespace Plugin.effiproz
{
    public static class EfzTool
    {
        public static string GetConnectionString(string filename, string user, string pwd)
        {
            const string EXT = ".properties";
            if (filename.ToLower().EndsWith(EXT)) filename = filename.Substring(0, filename.Length - EXT.Length);
            return String.Format("Connection Type=File;Initial Catalog={0};User={1};Password={2}", filename, user, pwd);
        }
    }
}
