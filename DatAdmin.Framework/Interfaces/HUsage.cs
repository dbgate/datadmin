using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    public static class HUsage
    {
        public static event Action ClosingApp;
        public static event Action<Exception> OpenErrorSupport;

        public static void CallClosingApp()
        {
            if (ClosingApp != null) ClosingApp();
        }

        public static void CallOpenErrorSupport(Exception err)
        {
            if (OpenErrorSupport != null) OpenErrorSupport(err);
        }

        public static bool DefinedOpenErrorSupport
        {
            get { return OpenErrorSupport != null; }
        }
    }
}
