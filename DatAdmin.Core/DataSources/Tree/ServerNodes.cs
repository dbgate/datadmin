using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    //[NodeFilter("s_server")]
    public class Server_SourceConnectionTreeNode : ConnectionTreeNode
    {
        IServerSource m_conn;
        ServerAppObject m_infoappobj;
        
        public Server_SourceConnectionTreeNode(IServerSource conn, ITreeNode parent, IFileHandler fhandler, IStoredConnection stored)
            : base(parent, fhandler, stored, false)
        {
            m_conn = conn.ChangeConnection(ConnPack);
            SetConnection(m_conn.Connection);
            var appobj = new ServerAppObject();
            appobj.FillFromUsage(conn);
            SetAppObject(appobj);

            m_infoappobj = new ServerAppObject();
            m_infoappobj.FillFromUsage(m_conn);
            m_infoappobj.DisableAutoConnect = true;
        }

        public override AppObject GetFirstValidAppObject()
        {
            if (!m_conn.IsFullAvailable()) return m_infoappobj;
            return m_primaryAppobj;
        }

        protected override bool AppObjectAvailable(AppObject appobj)
        {
            if (appobj is ServerFieldsAppObject)
            {
                return m_conn.IsFullAvailable();
            }
            return base.AppObjectAvailable(appobj);
        }
        public override System.Drawing.Bitmap Image
        {
            get
            {
                if (m_conn.Connection.IsOpened || AutoConnect) return CoreIcons.dbserver;
                else return CoreIcons.dbserver_disconnected;
            }
        }
        protected override void OnDisconnect()
        {
        }
        protected override void OnConnect()
        {
            RealNode.ExpandNode();
            bool expand;
            try { expand = m_conn.Connection.FindSettings(null).Tree().AutoExpandDatabases; }
            catch { expand = false; }
            if (expand) MainWindow.Instance.RunInMainWindow(RealNode.ChildByName("databases").ExpandNode);
        }
        public override ITreeNode[] GetChildren()
        {
            if (m_conn.Connection.IsOpened || AutoConnect)
            {
                return new ITreeNode[] { new DatabasesTreeNode(m_conn, this) };
            }
            else
            {
                return new ITreeNode[] { };
            }
        }
        public override string TypeTitle
        {
            get { return "s_server"; }
        }
        //[PopupMenu("s_query", ImageName = CoreIcons.sqlName, Weight = MenuWeights.QUERY)]
        //public void RunQuery()
        //{
        //    MainWindow.Instance.OpenContent(new QueryFrame(m_conn.CloneConnection(), null));
        //}
        //[PopupMenuEnabled("s_query")]
        //public bool RunQueryEnabled()
        //{
        //    return m_conn.Connection.SystemConnection != null;
        //}
        //public override List<IObjectView> GetObjectViews()
        //{
        //    var res = base.GetObjectViews();
        //    if (m_conn.IsFullAvailable()) res.Add(new DatabasesObjectsView());
        //    return res;
        //}
        public override bool DoubleClick()
        {
            if (base.DoubleClick()) return true;
            return OpenTheBestDashboard();
        }
    }
}
