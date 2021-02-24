using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;

namespace DatAdmin
{
    public abstract class NoThreadPhysicalConnectionBase : IPhysicalConnection
    {
        protected NoThreadPhysicalConnectionBase(IPhysicalConnectionFactory factory)
        {
            PhysicalFactory = factory;
            Cache = new ConnectionCache();
        }

        #region IPhysicalConnection Members

        public IAsyncResult BeginOpen(AsyncCallback callback)
        {
            return ThreadPool.BeginInvoke(ThreadPool.EmptyFunc, callback);
        }

        public void EndOpen(IAsyncResult async)
        {
            ((IStandaloneAsyncResult)async).EndInvoke();
        }

        public IAsyncResult BeginClose(AsyncCallback callback)
        {
            return ThreadPool.BeginInvoke(ThreadPool.EmptyFunc, callback);
        }

        public void EndClose(IAsyncResult async)
        {
            ((IStandaloneAsyncResult)async).EndInvoke();
        }

        public event PhysicalConnectionDelegate BeforeOpen;

        public event PhysicalConnectionDelegate AfterOpen;

        public event PhysicalConnectionDelegate BeforeClose;

        public event PhysicalConnectionDelegate AfterClose;

        public event PhysicalConnectionDelegate AfterCreateConnection;

        public bool IsOpened
        {
            get { return true; }
        }

        public bool WasUsed
        {
            get { return false; }
        }

        public System.Data.Common.DbConnection SystemConnection
        {
            get { return null; }
        }

        public System.Data.Common.DbProviderFactory DbFactory
        {
            get { return null; }
        }

        public IPhysicalConnectionFactory PhysicalFactory { get; private set; }

        public IStoredConnection StoredConnection
        {
            get { return null; }
        }

        public virtual IPhysicalConnection Clone()
        {
            return PhysicalFactory.CreateConnection();
        }

        public object Owner { get; set; }

        public event InfoMessageDelegate InfoMessage;

        public void DispatchInfo(InfoMessageEventArgs e)
        {
            if (InfoMessage != null) InfoMessage(this, e);
        }

        public virtual ISqlDialect Dialect
        {
            get { return GenericDialect.Instance; }
        }

        public ConnectionCache Cache { get; set; }

        public void Reconnect()
        {
        }

        public bool IsBroken()
        {
            return false;
        }

        public bool IsBroken(DbTransaction trans)
        {
            return false;
        }

        public IProviderHooks ProviderHooks
        {
            get { return null; }
        }

        public void CloseResources()
        {
        }

        public PhysicalConnectionExtendedState GetExtendedState()
        {
            return new PhysicalConnectionExtendedState();
        }

        #endregion

        #region IInvoker Members

        public IAsyncResult BeginInvoke(PriorityLevel priority, bool behaveAsStack, IInvokableAction action, AsyncCallback callback)
        {
            return ThreadPool.BeginInvoke(action.RunProc, callback);
        }

        public object EndInvoke(IAsyncResult async)
        {
            return ((IStandaloneAsyncResult)async).EndInvoke();
        }

        public bool IsInInvokerThread
        {
            get { return true; }
        }

        public IDisposable AddOnCancel(Action cancelAction)
        {
            return new NullDisposable();
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
        }

        #endregion
    }

    public class NullDisposable : IDisposable
    {
        #region IDisposable Members

        public void Dispose()
        {
        }

        #endregion
    }
}
