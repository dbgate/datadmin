using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    public static class TimeExtension
    {
        public static string NiceFormat(this TimeSpan time)
        {
            string res = time.ToString("c");
            // leave only one decimal digit
            int pos = res.IndexOf('.');
            if (pos > 0) return res.Substring(0, pos + 2);
            return res;
        }
    }
}
