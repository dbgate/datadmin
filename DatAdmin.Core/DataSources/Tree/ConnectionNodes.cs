using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    //[NodeFilter("s_connection")]
    public abstract class ConnectionTreeNode : VirtualFileTreeNodeBase
    {
        IPhysicalConnection m_conn;
        bool m_connecting = false;
        protected IStoredConnection m_stored;
        protected bool m_autoConnect;

        public ConnectionTreeNode(ITreeNode parent, IFileHandler fhandler, IStoredConnection stored, bool autoConnect)
            : base(parent, fhandler)
        {
            m_stored = stored;
            m_autoConnect = autoConnect;
            //Properties["connected"] = "";

            //m_conn.DispatchGetConnectionProperties(Properties);
        }
        protected void SetConnection(IPhysicalConnection conn)
        {
            m_conn = conn;
            //m_conn.Owner = this;
        }
        public override string Title
        {
            get
            {
                string suffix = "";
                if (m_connecting) suffix = String.Format("({0}...)", Texts.Get("s_connecting"));
                return base.Title + suffix;
            }
        }

        public override void CloseAllResources()
        {
            Async.SafeClose(m_conn);
        }

        [PopupMenuVisible("s_connect")]
        [PopupMenuVisible("s_disconnect")]
        public bool VisibleConnect()
        {
            if (AutoConnect) return false;
            return true;
        }

        [PopupMenuEnabled("s_connect")]
        public bool EnabledConnect()
        {
            if (m_connecting) return false;
            if (m_conn.IsOpened) return false;
            return !AutoConnect;
        }

        [PopupMenu("s_connect", ImageName = CoreIcons.connectName, Weight = MenuWeights.CONNECT)]
        public void Connect()
        {
            if (!EnabledConnect()) return;
            m_connecting = true;
            CallRefresh();

            ProcessRegister.AddBackgroundTask("s_connect");
            m_conn.BeginOpen(Async.CreateInvokeCallback(RealNode.Invoker, (AsyncCallback)Connected));
        }

        public override bool AllowExpand()
        {
            if (AutoConnect)
            {
                Async.SafeOpen(m_conn);
                return true;
            }
            else
            {
                return base.AllowExpand();
            }
        }

        private void Connected(IAsyncResult async)
        {
            try
            {
                m_connecting = false;
                m_conn.EndOpen(async);
                UsageStats.Usage("dbconnect",
                    "dialect", m_conn.Dialect != null ? m_conn.Dialect.DialectName : "",
                    "dbversion", m_conn.SystemConnection != null ? m_conn.SystemConnection.ServerVersion : "",
                    "type", m_conn.SystemConnection != null ? m_conn.SystemConnection.GetType().FullName : m_conn.GetType().FullName);
                //Properties["connected"] = "1";
                DispatchChangedProperties();
                CallRefresh();
                OnConnect();
            }
            catch (Exception e)
            {
                Errors.Report(e);
                m_conn.BeginClose(Async.CreateInvokeCallback(RealNode.Invoker, (AsyncCallback)Disconnected));
                CallRefresh();
            }
            ProcessRegister.RemoveBackgroundTask("s_connect");
        }

        [PopupMenuEnabled("s_disconnect")]
        public bool EnabledDisconnect()
        {
            return !AutoConnect && (m_connecting || m_conn.IsOpened);
        }

        public override void RemovedConnection(string connkey)
        {
            if (connkey != m_conn.GetConnKey()) return;
            Disconnected(null);
            //IAsyncResult res = m_conn.BeginClose(Async.CreateInvokeCallback(RealNode.Invoker, (AsyncCallback)Disconnected));
            //ProcessRegister.AddBackgroundTask("s_disconnect");
            //if (res.CompletedSynchronously) Disconnected(res);
        }

        [PopupMenu("s_disconnect", Weight = MenuWeights.DISCONNECT)]
        public void Disconnect()
        {
            if (!EnabledDisconnect()) return;
            if (!HConnection.CallRemoveByKey(m_conn.GetConnKey())) return;
            //m_conn.Close().OnFinish(delegate()
            //{
            //    OnDisconnect();
            //    CallRefresh();
            //}, RealNode.Invoker);
        }
        private void Disconnected(IAsyncResult async)
        {
            try
            {
                if (async != null) Async.WaitFor(async);
                //Properties["connected"] = "";
                DispatchChangedProperties();
                RealNode.CollapseNode();
                if (async != null) m_conn.EndClose(async);
            }
            catch (Exception e)
            {
                Errors.Report(e);
            }
            m_connecting = false;
            OnDisconnect();
            CallRefresh();
            //ProcessRegister.RemoveBackgroundTask("s_disconnect");
        }

        public IStoredConnection StoredConnection { get { return m_stored; } }

        [PopupMenu("s_edit", ImageName = CoreIcons.designName, Weight = MenuWeights.EDIT)]
        public void EditConnection()
        {
            if (m_stored != null)
            {
                var stored2 = m_stored.CloneUsingXml();
                if (stored2.Edit())
                {
                    stored2.Save();
                    RealNode.KillNode();
                    TreeNodeExtension.CompleteRefresh(Parent);
                    //m_conn = m_stored.Connect(m_filepath);
                    //m_conn.HasOwner = true;
                }
            }
        }

        [PopupMenuEnabled("s_edit")]
        public bool CanEditConnection() { return m_stored != null && !m_conn.IsOpened; }

        [PopupMenuVisible("s_edit")]
        public bool VisibleEditConnection() { return m_stored != null; }

        public override bool AutoConnect
        {
            get { return m_autoConnect || (RealNode != null && RealNode.TreeBehaviour.ForceAutoConnect); }
        }

        public override bool DoubleClick()
        {
            if (!AutoConnect && EnabledConnect())
            {
                Connect();
                return true;
            }
            return false;
        }

        //private void DoConnect()
        //{
        //    try
        //    {
        //        m_conn.Open();
        //        OnConnect();
        //    }
        //    finally
        //    {
        //        m_connecting = false;
        //    }
        //}
        //private void DoDisconnect()
        //{
        //    m_conn.Close();
        //    OnDisconnect();
        //}

        protected virtual void OnConnect() { }
        protected virtual void OnDisconnect() { }

        public override IPhysicalConnection GetConnection()
        {
            return m_conn;
        }

        public static ITreeNode FromStoredConnection(ITreeNode parent, IStoredConnection stored, IFileHandler fhandler)
        {
            IPhysicalConnection phys = stored.CreatePhysicalConnection();

            if (stored.DatabaseMode == ConnectionDatabaseMode.All)
            {
                IServerSource conn = new GenericServerSource(phys);
                return new Server_SourceConnectionTreeNode(conn, parent, fhandler, stored);
            }
            else
            {
                string exdbname = stored.DatabaseMode == ConnectionDatabaseMode.Explicit ? stored.ExplicitDatabaseName : null;
                IDatabaseSource conn = new GenericDatabaseSource(null, phys, exdbname);
                return new Database_SourceConnectionTreeNode(conn, parent, fhandler, stored, false);
            }
        }

        public override List<IWidget> GetWidgets()
        {
            var res = base.GetWidgets();
            if (ConnectionInfoWidget.IsSupported) res.Add(new ConnectionInfoWidget());
            return res;
        }

        public override bool ContainsDatabaseNode()
        {
            return true;
        }

        public override void NotifyDeepRefresh()
        {
            base.NotifyDeepRefresh();
            var cache = this.FindDatabaseCache();
            if (cache != null) cache.Clear();
        }
    }

    public abstract class ConnectionUsageTreeNodeBase : TreeNodeBase
    {
        protected IConnectionUsage m_usageConn;

        public ConnectionUsageTreeNodeBase(IConnectionUsage conn, ITreeNode parent, string name)
            : base(parent, name)
        {
            m_usageConn = conn;
            //Initialize();
        }

        public ConnectionUsageTreeNodeBase(IConnectionUsage conn)
            : base("data")
        {
            m_usageConn = conn;
            //Initialize();
        }

        //private void Initialize()
        //{
        //    //if (m_usageConn != null && m_usageConn.Connection != null) m_usageConn.Connection.DispatchGetConnectionProperties(Properties);
        //    //Properties["connected"] = "1";
        //}

        public override IPhysicalConnection GetConnection()
        {
            return m_usageConn.Connection;
        }

        protected ISqlDialect Dialect
        {
            get
            {
                if (m_usageConn.Connection != null) return m_usageConn.Connection.Dialect;
                return null;
            }
        }

        protected void InvokeScript(Action<ISqlDumper> script)
        {
            GetConnection().InvokeScript(script, this.GetDatabaseName());
        }

        public override void NotifyDeepRefresh()
        {
            base.NotifyDeepRefresh();
            var cache = this.FindDatabaseCache();
            if (cache != null) cache.Clear();
        }

        //protected bool SupportsScript(DialectScripts scripts)
        //{
        //    return Dialect != null && (Dialect.SupportedScripts & scripts) == scripts;
        //}
        //protected void InvokeScript(string sql)
        //{
        //    InvokerExtension.Invoke(GetConnection(), (Action)delegate()
        //    {
        //        DbConnectionExtension.SafeChangeDatabase(GetConnection().SystemConnection, TreeNodeExtension.GetDatabaseName(this));
        //        DbConnectionExtension.ExecuteNonQueries(GetConnection().SystemConnection, sql, GetConnection().Dialect);
        //    });
        //}
    }

    public abstract class LateLoadChildrenConnectionTreeNodeBase : ConnectionUsageTreeNodeBase
    {
        protected ITreeNode[] m_children = null;
        DatabaseCache m_refreshing;

        public LateLoadChildrenConnectionTreeNodeBase(IConnectionUsage conn, ITreeNode parent, string name)
            : base(conn, parent, name)
        {
        }

        public override ITreeNode[] GetChildren()
        {
            if (m_children == null) return new ITreeNode[] { };
            return m_children;
        }

        public override bool PreparedChildren
        {
            get
            {
                if (m_usageConn.Connection == null)
                {
                    DoGetChildren();
                }
                return m_children != null;
            }
        }
        protected abstract void DoGetChildren();
        public override IAsyncResult BeginLoadChildren(AsyncCallback callback)
        {
            if (AutoConnect && !m_usageConn.Connection.IsOpened)
            {
                Async.SafeOpen(m_usageConn.Connection);
            }
            return m_usageConn.Connection.BeginInvoke((Action)DoGetChildren, callback);
        }
        public override void EndLoadChildren(IAsyncResult async)
        {
            if (m_refreshing != null)
            {
                m_refreshing.EndRefresh();
                m_refreshing = null;
            }
            m_usageConn.Connection.EndInvoke(async);
        }
        public override void DataRefresh()
        {
            m_children = null;
            m_refreshing = this.FindDatabaseCache();
            if (m_refreshing != null)
            {
                m_refreshing.BeginRefresh();
            }
        }

        public override bool AutoConnect
        {
            get
            {
                if (Parent != null) return ((TreeNodeBase)Parent).AutoConnect;
                return base.AutoConnect;
            }
        }
    }
}
