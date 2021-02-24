using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin.selftest
{
    public static class TestLog
    {
        private static void Put(string msg)
        {
            Console.Out.WriteLine(msg);
        }

        public static void Info(string format, params string[] args)
        {
            string msg;
            if (args.Length > 0) msg = String.Format(format, args);
            else msg = format;
            Put(msg);
        }

        public static void Error(string errmsg, Exception e)
        {
            string msg = errmsg + "; " + e.Message;
            Put(msg);
        }
    }
}
