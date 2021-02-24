using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace DatAdmin
{
    public static class Async
    {
        //public static IAsyncResult BeginInvokeCallback(IInvoker proc_invoker, Delegate proc, IInvoker callback_invoker, AsyncCallback callback, params object[] proc_args)
        //{
        //    List<object> newargs = new List<object>();
        //    proc_invoker.BeginInvoke(
        //    //IAsyncResult res=proc.DynamicInvoke(
        //}

        //public static IAsyncVoid InvokeVoid(SimpleCallback proc)
        //{
        //    AsyncAction res = new AsyncAction(proc);
        //    ThreadPool.Invoke(res.DoRun);
        //    return res.Async;
        //}
        //public static IAsyncValue<T> InvokeValue<T>(ReturnValueCallback<T> proc)
        //{
        //    AsyncResultAction<T> res = new AsyncResultAction<T>(proc);
        //    ThreadPool.Invoke(res.DoRun);
        //    return res.Async;
        //}

        //public static void InvokeVoid(SimpleCallback proc, IInvoker invoker, SimpleCallback onfinish)
        //{
        //    InvokeVoid(proc).OnFinish(onfinish, invoker);
        //}

        public static AsyncCallback CreateGenericInvokeCallback(this IInvoker invoker, Delegate callback, params object[] args)
        {
            return delegate(IAsyncResult result)
            {
                List<object> newargs = new List<object>();
                newargs.Add(result);
                newargs.AddRange(args);
                IAsyncResult rtmp = invoker.BeginInvoke(callback, null, newargs.ToArray());
                invoker.EndInvoke(rtmp);
            };
        }

        public static AsyncCallback CreateInvokeCallback(this IInvoker invoker, AsyncCallback callback)
        {
            return delegate(IAsyncResult result)
            {
                IAsyncResult rtmp = invoker.BeginInvoke(callback, null, result);
                invoker.EndInvoke(rtmp);
            };
        }

        public static void InvokeFromGui(Action proc)
        {
            IAsyncResult res = proc.BeginInvoke(null, null);
            Async.WaitFor(res);
            proc.EndInvoke(res);
        }

        public static void WaitFor(IAsyncResult async)
        {
            if (async.IsCompleted) return;
            if (AsyncTool.IsMainThread)
            {
                WaitObject.ActiveWait(async);
            }
            else
            {
                if (async.AsyncWaitHandle != null)
                {
                    async.AsyncWaitHandle.WaitOne();
                }
                else
                {
                    while (!async.IsCompleted) Thread.Sleep(100);
                }
            }
        }

        public static void SafeOpen(IPhysicalConnection conn)
        {
            if (conn == null) return;
            if (conn.IsOpened) return;
            Open(conn);
        }

        public static void SafeReconnect(IPhysicalConnection conn)
        {
            IAsyncResult res = conn.BeginInvoke((Action)conn.Reconnect, null);
            Async.WaitFor(res);
            conn.EndInvoke(res);
        }

        private static void Open(IPhysicalConnection conn)
        {
            IAsyncResult res = conn.BeginOpen(null);
            Async.WaitFor(res);
            conn.EndOpen(res);
        }

        public static void Close(IPhysicalConnection conn)
        {
            IAsyncResult res = conn.BeginClose(null);
            Async.WaitFor(res);
            conn.EndClose(res);
        }

        public static void SafeClose(IPhysicalConnection conn)
        {
            if (conn == null) return;
            if (conn.IsOpened) Close(conn);
            conn.CloseResources();
        }

        public static IAsyncResult BeginCancelableInvoke(Delegate proc, AsyncCallback callback, object []args)
        {
            CancelableAction res = new CancelableAction(proc, callback, args);
            res.Start();
            return res;
        }
    }

    public class CancelableAction : IStandaloneAsyncResult, ICancelable
    {
        Thread m_thread;
        Exception m_error;
        object m_result;
        object[] m_args;
        Delegate m_proc;
        AsyncCallback m_callback;
        bool m_finished;
        ManualResetEvent m_event = new ManualResetEvent(false);

        internal CancelableAction(Delegate proc, AsyncCallback callback, object[] args)
        {
            m_proc = proc;
            m_args = args;
            m_callback = callback;
        }

        internal void Start()
        {
            m_thread = new Thread(Run);
            m_thread.Start();
        }

        private void Run()
        {
            try
            {
                m_result = m_proc.DynamicInvoke(m_args);
            }
            catch (Exception e)
            {
                m_error = e;
            }
            m_finished = true;
            m_event.Set();
            if (m_callback != null)
            {
                try
                {
                    m_callback(this);
                }
                catch (Exception e)
                {
                    Logging.Warning("Exception when calling async callback: {0}", e.Message);
                }
            }

        }

        #region IStandaloneAsyncResult Members

        public object EndInvoke()
        {
            if (m_event != null)
            {
                m_event.WaitOne();
                m_event.Close();
                m_event = null;
            }
            else
            {
                Logging.Warning("EndInvoke called mutiple times");
            }
            if (m_error != null) throw m_error;
            return m_result;
        }

        #endregion

        #region IAsyncResult Members

        public object AsyncState
        {
            get { return null; }
        }

        public WaitHandle AsyncWaitHandle
        {
            get { return m_event; }
        }

        public bool CompletedSynchronously
        {
            get { return false; }
        }

        public bool IsCompleted
        {
            get { return m_finished; }
        }

        #endregion

        #region ICancelable Members

        public void Cancel()
        {
            m_thread.Abort();
        }

        public bool CanCancel
        {
            get { return true; }
        }

        #endregion
    }

    public class ValueAsyncResult : IStandaloneAsyncResult
    {
        public readonly object Value;
        public readonly Exception Error;

        public ValueAsyncResult(object value, Exception error)
        {
            Value = value;
            Error = error;
        }

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
            get { return true; }
        }

        public bool IsCompleted
        {
            get { return true; }
        }

        #endregion

        public object EndInvoke()
        {
            if (Error != null) throw Error;
            return Value;
        }
    }
}
