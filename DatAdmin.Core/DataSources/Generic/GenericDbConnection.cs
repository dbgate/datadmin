using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Threading;
using System.Data.Common;
using System.Data;
using System.Xml;

namespace DatAdmin
{
    public sealed class GenericDbConnection : PhysicalConnectionBase
    {
        string m_connectionString;
        DbConnection m_conn = null;
        DbProviderFactory m_factory;
        //string m_title;
        ISqlDialect m_dialect;
        IDialectDetector m_dialectDetector;
        IStoredConnection m_storedConnection;

        public GenericDbConnection(GenericDbConnectionFactory fact)
            : base(fact)
        {
            //m_conn = conn;
            //string connectionString, DbProviderFactory factory, string title, IDialectDetector dialectDetector, IStoredConnection storedConnection
            //m_title = storedConnection.ToString() .GetTitle();
            m_storedConnection = fact.Stored;
            m_factory = m_storedConnection.GetFactory();
            m_connectionString = m_storedConnection.GenerateConnectionString(true);
            m_dialectDetector = m_storedConnection.GetDialectDetector();

            ProviderHooks = m_storedConnection.CreateHooks();
            m_storedConnection.InstallHooks(this);
        }

        public GenericDbConnection(IStoredConnection sconn)
            : this(new GenericDbConnectionFactory { Stored = sconn })
        {
        }

        public void OverrideConnectionString(string value)
        {
            m_connectionString = value;
        }

        public override string ToString()
        {
            string dialstr = "???";
            if (m_dialect != null) dialstr = m_dialect.DialectName;
            else if (m_dialectDetector != null) dialstr = m_dialectDetector.ToString();
            return String.Format("{0}-{1}", StoredConnection.ToString(), dialstr);
        }

        public override string ThreadName
        {
            get { return String.Format("DB:{0},CONN:{1}", m_factory.GetType().Name, m_connectionString); }
        }

        protected override void DoCreateConnection()
        {
            m_conn = m_factory.CreateConnection();
            m_conn.ConnectionString = m_connectionString;
        }

        #region IPhysicalConnection Members

        //public event PhysicalConnectionDelegate BeforeOpen;

        //public event PhysicalConnectionDelegate AfterOpen;

        //public event PhysicalConnectionDelegate BeforeClose;

        //public event PhysicalConnectionDelegate AfterClose;

        //public event PhysicalConnectionDelegate AfterCreateConnection;

        //public event CloneConnectionDelegate OnClone;

        //public IAsyncVoid InvokeVoid(SimpleCallback func)
        //{
        //    AsyncAction async = new AsyncAction(func);
        //    m_queue.Put((SimpleCallback)async.DoRun);
        //    return async.Async;
        //}

        //public IAsyncValue<T> InvokeValue<T>(ReturnValueCallback<T> func)
        //{
        //    AsyncResultAction<T> async = new AsyncResultAction<T>(func);
        //    m_queue.Put((SimpleCallback)async.DoRun);
        //    return async.Async;
        //}

        public override DbConnection SystemConnection
        {
            get { return m_conn; }
        }

        public override IStoredConnection StoredConnection
        {
            get { return m_storedConnection; }
        }

        public override ISqlDialect Dialect
        {
            get
            {
                if (m_dialect != null) return m_dialect;
                if (m_dialectDetector != null && m_dialectDetector.ApproximateDialect != null) return m_dialectDetector.ApproximateDialect;
                return GenericDialect.Instance;
            }
        }

        public override DbProviderFactory DbFactory
        {
            get { return m_factory; }
        }

        public override IPhysicalConnection Clone()
        {
            var res = (GenericDbConnection)base.Clone();
            if (m_dialect != null) res.m_dialectDetector = new FixedDialectDetector(m_dialect);
            return res;
        }

        protected override void DoOpen()
        {
            Logging.Debug("Connecting database type {0}: {1}", m_conn.GetType().FullName, Logging.MangleConnectionString_RemovePassword(m_conn.ConnectionString));
            try
            {
                m_conn.Open();
                Logging.Debug("Connected OK, version={0}", m_conn.ServerVersion);
            }
            catch (Exception e)
            {
                throw new ConnectionFailedError("DAE-00231", e);
            }
            m_dialect = m_dialectDetector.DetectDialect(m_conn);            
            var opt = StoredConnection.ConnectionSettings.PageByName("connection") as ConnectionSettings;
            if (opt != null)
            {
                if (!String.IsNullOrEmpty(opt.OnConnectSql)) m_conn.ExecuteNonQuery(opt.OnConnectSql, Dialect);
            }
        }

        // is called from connection thread
        protected override void DoClose()
        {
            var opt = StoredConnection.ConnectionSettings.PageByName("connection") as ConnectionSettings;
            if (opt != null)
            {
                if (!String.IsNullOrEmpty(opt.OnDisconnectSql)) m_conn.ExecuteNonQuery(opt.OnDisconnectSql, Dialect);
            }

            m_conn.Close();
        }

        //public IAsyncVoid Open()
        //{
        //    if (m_thread != null) throw new ConnectionException("Opening allready opened connection ");
        //    m_thread = new Thread(Run);
        //    m_queue = new WaitQueue<object>();
        //    m_thread.Start();
        //    return InvokeVoid(DoOpen);
        //}

        //public IAsyncVoid Close()
        //{
        //    if (m_thread == null) throw new ConnectionException("Closing closed connection");
        //    return InvokeVoid(DoClose);
        //}

        public override bool IsBroken(DbTransaction trans)
        {
            try
            {
                using (DbCommand cmd = m_conn.CreateCommand())
                {
                    cmd.CommandText = m_dialect.GeneratePing();
                    cmd.Transaction = trans;
                    using (DbDataReader reader = cmd.ExecuteReader(CommandBehavior.SchemaOnly))
                    {
                        return false;
                    }
                }
            }
            catch
            {
                return true;
            }
        }

        public override bool IsBroken()
        {
            return IsBroken(null);
        }

        public override void Reconnect()
        {
            try
            {
                DoCallClose(false);
            }
            catch { }
            try
            {
                DoCallOpen();
            }
            catch { }
        }

        #endregion

        internal override void RepairConnection()
        {
            this.ReconnectIfBroken();
        }
    }
}
