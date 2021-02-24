using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;

namespace DatAdmin
{
    public class PhysicalConnectionExtendedState
    {
        public int QueueLength;
        public int ProcessedOkQElements;
        public int ProcessedFailQElements;
    }

    /// <summary>
    /// connection, should autamatically open/close/reopen connection, when needed
    /// </summary>
    public interface IPhysicalConnection : IInvoker, IDisposable
    {
        IAsyncResult BeginOpen(AsyncCallback callback);
        void EndOpen(IAsyncResult async);
        IAsyncResult BeginClose(AsyncCallback callback);
        void EndClose(IAsyncResult async);
        //IAsyncVoid Open();
        //IAsyncVoid Close();
        //ConnectionStatus State { get;}
        //public IInvoker EventsInvoker { get;set;}
        event PhysicalConnectionDelegate BeforeOpen;
        event PhysicalConnectionDelegate AfterOpen;
        event PhysicalConnectionDelegate BeforeClose;
        event PhysicalConnectionDelegate AfterClose;
        event PhysicalConnectionDelegate AfterCreateConnection;
        //IAsyncVoid InvokeVoid(SimpleCallback func);
        //IAsyncValue<T> InvokeValue(ReturnValueCallback<T> func);
        bool IsOpened { get; }
        /// <summary>
        /// if false connection was not used, it is not neccessary to dispose it (it is not real connection yet)
        /// </summary>
        bool WasUsed { get; }
        //IInvoker Invoker { get;}
        //IConnectionHooks Hooks { get;set;}

        DbConnection SystemConnection { get; }
        DbProviderFactory DbFactory { get; }
        /// <summary>
        /// factory which creates this connection
        /// </summary>
        IPhysicalConnectionFactory PhysicalFactory { get; }
        IStoredConnection StoredConnection { get; }

        IPhysicalConnection Clone();
        object Owner { get; set; }
        //bool HasOwner { get; set; }

        event InfoMessageDelegate InfoMessage;

        void DispatchInfo(InfoMessageEventArgs e);
        /// <summary>
        /// Associated dialect
        /// </summary>
        ISqlDialect Dialect { get; }

        //event PhysicalConnectionPropertiesDelegate GetConnectionProperties;
        //void DispatchGetConnectionProperties(IDictionary<string, object> properties);

        ConnectionCache Cache { get; set; }
        //IDictionary<string, object> CachedData { get; }

        /// <summary>
        /// creates connection in initial state, called from connection thread
        /// </summary>
        void Reconnect();
        /// <summary>
        /// tests, whether connection is broken. Used ping (dialect-dependend) command
        /// </summary>
        /// <returns>true if connection is broken</returns>
        bool IsBroken();
        bool IsBroken(DbTransaction trans);


        IProviderHooks ProviderHooks { get; }

        /// <summary>
        /// closes all resources; should be called after BeginClose...EndClose; can be called more-times
        /// </summary>
        void CloseResources();

        PhysicalConnectionExtendedState GetExtendedState();
    }
}
