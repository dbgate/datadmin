using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Data.Common;

namespace DatAdmin
{
    public abstract class PhysicalConnectionBase : IPhysicalConnection
    {
        Thread m_thread;
        WaitQueue<QElement> m_queue = null;
        bool m_breakFlag = false;
        //bool m_hasOwner = false;
        object m_owner;
        int m_statProcessedOkQElements;
        int m_statProcessedFailQElements;
        //IDictionary<string, object> m_cachedData = new Dictionary<string, object>();
        List<OnCancelAction> m_onCancelActions = new List<OnCancelAction>();
        bool m_breakStarted;
        
        class OnCancelAction : IDisposable
        {
            internal Action Cancel;
            PhysicalConnectionBase m_conn;
            internal OnCancelAction(Action cancel, PhysicalConnectionBase conn)
            {
                Cancel = cancel;
                m_conn = conn;
            }

            #region IDisposable Members

            public void Dispose()
            {
                lock (m_conn.m_onCancelActions) m_conn.m_onCancelActions.Remove(this);
            }

            #endregion
        }

        protected class QElement : IStandaloneAsyncResult, ICancelable
        {
            IInvokableAction m_action;
            AsyncCallback m_callback;
            ManualResetEvent m_event = new ManualResetEvent(false);
            Exception m_error;
            object m_result;
            bool m_finished = false;
            PhysicalConnectionBase m_conn;
            bool m_canCancel = false;
            bool m_canceled = false;
            Thread m_thread;
            Thread m_cancelThread;
            object m_cancelLock = new object();

            internal QElement(PhysicalConnectionBase conn, IInvokableAction action, AsyncCallback callback, Thread thread)
            {
                m_action = action;
                m_callback = callback;
                m_conn = conn;
                m_thread = thread;
            }

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

            internal void Run()
            {
                try
                {
                    try
                    {
                        lock (m_cancelLock)
                        {
                            m_canCancel = true;
                        }
                        m_result = m_action.RunProc();
                    }
                    finally
                    {
                        lock (m_cancelLock)
                        {
                            m_canCancel = false;
                        }
                    }
                }
                catch (ThreadAbortException e)
                {
                    m_error = e;
                    if (m_canceled)
                    {
                        // only this QElement is canceled, abort flag should be cleared
                        Thread.ResetAbort();
                    }
                    if (ThreadRegister.IsThreadDefinitivelyAborted(m_thread)) m_thread.Abort();
                }
                catch (Exception e)
                {
                    m_error = e;
                    try { m_conn.RepairConnection(); }
                    catch { }
                }
                finally
                {
                    m_finished = true;
                    m_event.Set();
                }
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

            private void DoCancel()
            {
                // at first run all oncancel actions
                List<OnCancelAction> oncancs = new List<OnCancelAction>();
                lock (m_conn.m_onCancelActions) oncancs.AddRange(m_conn.m_onCancelActions);
                foreach (var a in oncancs)
                {
                    try { a.Cancel(); }
                    catch (Exception e) { Logging.Warning("Error canceling:" + e.ToString()); }
                }

                // explicit action cancel - not used
                if (m_action.CanCancel) m_action.Cancel();

                // abort thread - only if it is running action currently, so that
                // surrounding try-catch ThreadAbortException can handle it
                lock (m_cancelLock)
                {
                    if (m_canCancel)
                    {
                        m_canceled = true;
                        m_thread.Abort();
                    }
                }
            }

            #region ICancelable Members

            public void Cancel()
            {
                if (m_cancelThread != null) return;
                m_cancelThread = new Thread(DoCancel);
                m_cancelThread.Start();
            }

            public bool CanCancel
            {
                get { return true; }
            }

            #endregion
        }

        protected PhysicalConnectionBase(IPhysicalConnectionFactory factory)
        {
            Cache = new ConnectionCache();
            PhysicalFactory = factory;
            ConnectionRegister.AddConnection(this);
        }

        protected virtual void DoCreateConnection() { }

        private void Run()
        {
            try
            {
                ThreadRegister.RegisterThread(Thread.CurrentThread);
                DoCreateConnection();
                if (AfterCreateConnection != null) AfterCreateConnection(this);
                while (!m_breakFlag)
                {
                    QElement obj = m_queue.Get();
                    try
                    {
                        obj.Run();
                        m_statProcessedOkQElements++;
                    }
                    catch (Exception e)
                    {
                        Logging.Error("Error processing queue element:" + e.ToString());
                        m_statProcessedFailQElements++;
                    }
                }
            }
            finally
            {
                ThreadRegister.UnregisterThread(Thread.CurrentThread);
            }
        }

        protected virtual void DoOpen() { }
        protected virtual void DoClose() { }

        protected void DoCallOpen()
        {
            if (BeforeOpen != null) BeforeOpen(this);
            DoOpen();
            if (AfterOpen != null) AfterOpen(this);
            ConnectionRegister.ChangedConnection(this);
        }

        protected void DoCallClose(bool setBreakFlag)
        {
            if (setBreakFlag) m_breakStarted = true;
            if (BeforeClose != null) BeforeClose(this);
            DoClose();
            if (AfterClose != null) AfterClose(this);
            if (setBreakFlag) m_breakFlag = true;
            ConnectionRegister.ChangedConnection(this);
        }

        #region IPhysicalConnection Members

        public virtual IStoredConnection StoredConnection { get { return null; } }
        public IPhysicalConnectionFactory PhysicalFactory { get; private set; }

        public bool WasUsed { get; private set; }

        public void DispatchInfo(InfoMessageEventArgs e)
        {
            if (InfoMessage != null) InfoMessage(this, e);
        }

        public IAsyncResult BeginOpen(AsyncCallback callback)
        {
            if (m_thread != null)
            {
                return new ValueAsyncResult(null, new ConnectionException("Opening allready opened connection "));
            }

            m_breakFlag = false;
            WasUsed = true;
            m_thread = new Thread(Run);
            if (ThreadName != null) m_thread.Name = ThreadName;
            m_queue = new WaitQueue<QElement>();
            m_thread.Start();
            return this.BeginInvoke((Action)DoCallOpen, callback);
        }

        public virtual string ThreadName { get { return null; } }

        public void EndOpen(IAsyncResult async)
        {
            EndInvoke(async);
        }

        public IAsyncResult BeginClose(AsyncCallback callback)
        {
            if (m_thread == null || !m_thread.IsAlive)
            {
                return new ValueAsyncResult(null, null);
            }
            KillObject kill = new KillObject(callback, this);
            this.BeginInvoke((Action<bool>)DoCallClose, kill.CloseFinished, true);
            kill.Start();
            return kill;
        }

        public class KillObject : IStandaloneAsyncResult
        {
            ManualResetEvent m_event = new ManualResetEvent(false);
            PhysicalConnectionBase m_conn;
            bool m_finished = false;

            AsyncCallback m_callback;

            internal KillObject(AsyncCallback callback, PhysicalConnectionBase conn)
            {
                m_callback = callback;
                m_conn = conn;
            }

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

            // called from connection thread, if close QElement was succesfully finished
            internal void CloseFinished(IAsyncResult async)
            {
                CallClosed();
            }

            internal void Start()
            {
                Thread thr = new Thread(RunKillThread);
                thr.IsBackground = true;
                thr.Start();
            }

            private void RunKillThread()
            {
                try
                {
                    Thread thr = m_conn.m_thread;
                    Thread.Sleep(2500);
                    try
                    {
                        if (thr != null && thr.IsAlive)
                        {
                            ThreadRegister.MarkThreadDefinitivelyAborted(thr);
                            thr.Abort();
                        }
                    }
                    catch
                    {
                        // neresime zamykani, tak to tady asi muze spadnout, ale nic se nedeje...
                    }
                    CallClosed();
                }
                catch (Exception e)
                {
                    Logging.Error("Error in RunKillThread:" + e.ToString());
                }
            }

            private void CallClosed()
            {
                m_finished = true;
                try
                {
                    m_event.Set();
                }
                catch { } // can be closed or null
                AsyncCallback callback;
                lock (this)
                {
                    callback = m_callback;
                    m_callback = null;
                }
                if (callback != null) callback(this);
            }

            internal void EndInvoke()
            {
                if (m_event != null)
                {
                    m_event.WaitOne();
                    m_event.Close();
                    m_event = null;
                }
                else
                {
                    Logging.Warning("KillObject.EndInvoke  called twice");
                }
            }

            #region IStandaloneAsyncResult Members

            object IStandaloneAsyncResult.EndInvoke()
            {
                m_conn.EndClose(this);
                return null;
            }

            #endregion
        }

        public void EndClose(IAsyncResult async)
        {
            if (m_thread != null)
            {
                m_thread.Join(100);
                if (m_thread.IsAlive)
                {
                    ThreadRegister.MarkThreadDefinitivelyAborted(m_thread);
                    m_thread.Abort();
                }
            }
            m_thread = null;
            m_breakStarted = false;
            m_queue = null;
            if (async is KillObject)
            {
                ((KillObject)async).EndInvoke();
            }
            else
            {
                EndInvoke(async);
            }
        }

        public object Owner
        {
            get
            {
                return m_owner;
            }
            set
            {
                if (m_owner != null) throw new InternalError("DAE-00003 Owner of connection allready set");
                if (value == null) throw new InternalError("DAE-00004 Owner cannot be null");
                m_owner = value;
            }
        }

        public event InfoMessageDelegate InfoMessage;

        public event PhysicalConnectionDelegate BeforeOpen;

        public event PhysicalConnectionDelegate AfterOpen;

        public event PhysicalConnectionDelegate BeforeClose;

        public event PhysicalConnectionDelegate AfterClose;

        public event PhysicalConnectionDelegate AfterCreateConnection;

        public virtual DbConnection SystemConnection
        {
            get { return null; }
        }

        public virtual DbProviderFactory DbFactory
        {
            get { return null; }
        }

        public virtual ISqlDialect Dialect
        {
            get { return null; }
        }

        public virtual bool IsOpened
        {
            get { return m_thread != null && !m_breakStarted; }
        }

        public virtual IPhysicalConnection Clone()
        {
            return PhysicalFactory.CreateConnection();
        }

        public ConnectionCache Cache { get; set; }
        //public IDictionary<string, object> CachedData
        //{
        //    get { return m_cachedData; }
        //}

        public virtual bool IsBroken()
        {
            return false;
        }
        public virtual bool IsBroken(DbTransaction trans)
        {
            return false;
        }

        public virtual void Reconnect() { }

        public IProviderHooks ProviderHooks { get; set; }

        #endregion

        #region IInvoker Members

        public bool IsInInvokerThread
        {
            get { return Thread.CurrentThread == m_thread; }
        }

        public IAsyncResult BeginInvoke(PriorityLevel priority, bool behaveAsStack, IInvokableAction action, AsyncCallback callback)
        {
            if (m_queue == null) throw new InternalError("DAE-00005 Connection queue is NULL, probably connection is not open");
            if (IsInInvokerThread)
            {
                try
                {
                    object res = action.RunProc();
                    return new ValueAsyncResult(res, null);
                }
                catch (Exception err)
                {
                    return new ValueAsyncResult(null, err);
                }
                //throw new Exception("Invoke called from the same thread");
            }
            QElement elem = new QElement(this, action, callback, m_thread);
            m_queue.Put(priority, behaveAsStack, elem);
            return elem;
        }

        public object EndInvoke(IAsyncResult async)
        {
            return ((IStandaloneAsyncResult)async).EndInvoke();
        }

        public IDisposable AddOnCancel(Action cancelAction)
        {
            lock (m_onCancelActions)
            {
                OnCancelAction res = new OnCancelAction(cancelAction, this);
                m_onCancelActions.Add(res);
                return res;
            }
        }

        #endregion

        internal virtual void RepairConnection() { }

        #region IPhysicalConnection Members

        public virtual void CloseResources()
        {
        }


        #endregion

        #region IDisposable Members

        public virtual void Dispose()
        {
            CloseResources();
            ConnectionRegister.RemoveConnection(this);
        }

        #endregion

        public virtual PhysicalConnectionExtendedState GetExtendedState()
        {
            return new PhysicalConnectionExtendedState
            {
                ProcessedFailQElements = m_statProcessedFailQElements,
                ProcessedOkQElements = m_statProcessedOkQElements,
                QueueLength = m_queue != null ? m_queue.Length : 0,
            };
        }
    }
}
