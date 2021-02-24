using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace DatAdmin
{
    public static class ThreadPool
    {
        public static object EmptyFunc() { return null; }

        class ActionWrapper : IAsyncResult, IStandaloneAsyncResult
        {
            public Func<object> RunFunc;
            public AsyncCallback Callback;
            public Exception Error;
            public object Result;

            #region IAsyncResult Members

            public object AsyncState
            {
                get { return null; }
            }

            public WaitHandle AsyncWaitHandle
            {
                get { return null; }
            }

            public bool CompletedSynchronously
            {
                get { return false; }
            }

            public bool IsCompleted { get; private set; }

            #endregion

            #region IStandaloneAsyncResult Members

            public object EndInvoke()
            {
                if (Error != null) throw Error;
                return Result;
            }

            #endregion

            public void DoRun(object state)
            {
                try
                {
                    Result = RunFunc();
                }
                catch (Exception err)
                {
                    Error = err;
                }
                IsCompleted = true;
                if (Callback != null)
                {
                    try
                    {
                        Callback(this);
                    }
                    catch (Exception err)
                    {
                        Logging.Warning("Error when running threadpool callback:" + err.Message);
                    }
                }
            }
        }

        public static IAsyncResult BeginInvoke(Func<object> proc, AsyncCallback callback)
        {
            ActionWrapper wrap = new ActionWrapper
            {
                RunFunc = proc,
                Callback = callback,
            };
            System.Threading.ThreadPool.QueueUserWorkItem(wrap.DoRun);
            return wrap;
        }
    }
}
