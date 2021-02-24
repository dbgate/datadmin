using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    public static class HSystem
    {
        static string m_originalPath = Environment.GetEnvironmentVariable("PATH");

        public static event Action<List<string>> GetSystemPathEx;
        public static event Action SystemPathChanged;

        public static void CallGetSystemPathEx(List<string> paths)
        {
            if (GetSystemPathEx != null) GetSystemPathEx(paths);
        }

        public static void CallSystemPathChanged()
        {
            if (SystemPathChanged != null) SystemPathChanged();
        }

        public static void RebuildSystemPath()
        {
            var lst = new List<string>();
            CallGetSystemPathEx(lst);
            string newpath = m_originalPath;
            foreach (string pth in lst)
            {
                newpath += ";" + pth;
            }
            Environment.SetEnvironmentVariable("PATH", newpath);
            CallSystemPathChanged();
        }
    }
}
