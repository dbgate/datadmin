using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    public static class InvokerExtension
    {
        private class SimpleAction : IInvokableAction
        {
            Delegate m_proc;
            object[] m_args;

            internal SimpleAction(Delegate proc, object[] args)
            {
                m_proc = proc;
                m_args = args;
            }

            public object RunProc() { return m_proc.DynamicInvoke(m_args); }
            public bool CanCancel { get { return false; } }
            public void Cancel() { }
        }

        public static IAsyncResult BeginInvoke(this IInvoker conn, Delegate proc, AsyncCallback callback, params object[] args)
        {
            return conn.BeginInvoke(PriorityLevel.Normal, false, new SimpleAction(proc, args), callback);
        }

        public static IAsyncResult BeginInvoke(this IInvoker conn, PriorityLevel priority, bool behaveAsStack, Delegate proc, AsyncCallback callback, params object[] args)
        {
            return conn.BeginInvoke(priority, behaveAsStack, new SimpleAction(proc, args), callback);
        }

        public static void Invoke(this IInvoker conn, Action func)
        {
            if (conn.IsInInvokerThread) { func(); return; }
            IAsyncResult res = conn.BeginInvoke(func, null, null);
            Async.WaitFor(res);
            conn.EndInvoke(res);
        }
        public static void Invoke1<T1>(this IInvoker conn, Action<T1> func, T1 p1)
        {
            if (conn.IsInInvokerThread) { func(p1); return; }
            IAsyncResult res = conn.BeginInvoke((Action)delegate() { func(p1); }, null, null);
            Async.WaitFor(res);
            conn.EndInvoke(res);
        }
        public static void Invoke2<T1, T2>(this IInvoker conn, Action<T1, T2> func, T1 p1, T2 p2)
        {
            if (conn.IsInInvokerThread) { func(p1, p2); return; }
            IAsyncResult res = conn.BeginInvoke((Action)delegate() { func(p1, p2); }, null, null);
            Async.WaitFor(res);
            conn.EndInvoke(res);
        }
        public static void Invoke3<T1, T2, T3>(this IInvoker conn, Action<T1, T2, T3> func, T1 p1, T2 p2, T3 p3)
        {
            if (conn.IsInInvokerThread) { func(p1, p2, p3); return; }
            IAsyncResult res = conn.BeginInvoke((Action)delegate() { func(p1, p2, p3); }, null, null);
            Async.WaitFor(res);
            conn.EndInvoke(res);
        }

        public static void Invoke4<T1, T2, T3, T4>(this IInvoker conn, Action<T1, T2, T3, T4> func, T1 p1, T2 p2, T3 p3, T4 p4)
        {
            if (conn.IsInInvokerThread) { func(p1, p2, p3, p4); return; }
            IAsyncResult res = conn.BeginInvoke((Action)delegate() { func(p1, p2, p3, p4); }, null, null);
            Async.WaitFor(res);
            conn.EndInvoke(res);
        }

        public static R InvokeR<R>(this IInvoker conn, Func<R> func)
        {
            if (conn.IsInInvokerThread) return func();
            IAsyncResult res = conn.BeginInvoke((Func<R>)delegate() { return func(); }, null, null);
            Async.WaitFor(res);
            return (R)conn.EndInvoke(res);
        }

        public static R InvokeR1<R, T1>(this IInvoker conn, Func<T1, R> func, T1 p1)
        {
            if (conn.IsInInvokerThread) return func(p1);
            IAsyncResult res = conn.BeginInvoke((Func<R>)delegate() { return func(p1); }, null, null);
            Async.WaitFor(res);
            return (R)conn.EndInvoke(res);
        }
        public static R InvokeR2<R, T1, T2>(this IInvoker conn, Func<T1, T2, R> func, T1 p1, T2 p2)
        {
            if (conn.IsInInvokerThread) return func(p1, p2);
            IAsyncResult res = conn.BeginInvoke((Func<R>)delegate() { return func(p1, p2); }, null, null);
            Async.WaitFor(res);
            return (R)conn.EndInvoke(res);
        }
        public static R InvokeR3<R, T1, T2, T3>(this IInvoker conn, Func<T1, T2, T3, R> func, T1 p1, T2 p2, T3 p3)
        {
            if (conn.IsInInvokerThread) return func(p1, p2, p3);
            IAsyncResult res = conn.BeginInvoke((Func<R>)delegate() { return func(p1, p2, p3); }, null, null);
            Async.WaitFor(res);
            return (R)conn.EndInvoke(res);
        }

        public static R InvokeR4<R, T1, T2, T3, T4>(this IInvoker conn, Func<T1, T2, T3, T4, R> func, T1 p1, T2 p2, T3 p3, T4 p4)
        {
            if (conn.IsInInvokerThread) return func(p1, p2, p3, p4);
            IAsyncResult res = conn.BeginInvoke((Func<R>)delegate() { return func(p1, p2, p3, p4); }, null, null);
            Async.WaitFor(res);
            return (R)conn.EndInvoke(res);
        }
    }
}
