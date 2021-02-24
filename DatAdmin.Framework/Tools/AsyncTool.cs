using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace DatAdmin
{
    public static class AsyncTool
    {
        public static Thread MainThread;
        //public static IWaitDialog WaitDialog;
        public static bool IsMainThread
        {
            get
            {
                return Thread.CurrentThread == MainThread;
            }
        }
    }
}
