using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace DatAdmin
{
    public partial class ConnectionsDocker : DockerBase
    {
        bool m_plannedReload;
        public ConnectionsDocker(IDockerFactory factory)
            : base(factory)
        {
            InitializeComponent();

            HConnection.AddConnection += HConnection_AddConnection;
            HConnection.ChangeConnection += HConnection_ChangeConnection;
            HConnection.RemoveConnection += HConnection_RemoveConnection;

            cbxType.Items.Add(Texts.Get("s_all"));
            cbxType.Items.Add(Texts.Get("s_opened"));
            cbxType.SelectedIndex = 0;

            //ReloadConnections();
        }

        public override void OnClose()
        {
            base.OnClose();

            HConnection.AddConnection -= HConnection_AddConnection;
            HConnection.ChangeConnection -= HConnection_ChangeConnection;
            HConnection.RemoveConnection -= HConnection_RemoveConnection;
        }

        void ReloadConnections()
        {
            lsvConnections.BeginUpdate();
            lsvConnections.ListViewItemSorter = null;
            m_plannedReload = false;
            var groups = new List<string>();
            IPhysicalConnection selected = SelectedConnection;
            lsvConnections.Items.Clear();
            ListViewItem selitem = null;
            foreach (var conn in ConnectionRegister.GetConnections())
            {
                if (!conn.WasUsed) continue;
                if (cbxType.SelectedIndex == 1 && !conn.IsOpened) continue;
                if (!groups.Contains(conn.GetConnKey())) groups.Add(conn.GetConnKey());
                var item = lsvConnections.Items.Add(conn.ToString());
                item.SubItems.Add(conn.IsOpened ? "Opened" : "Closed");
                if (conn.Owner != null) item.SubItems.Add(conn.Owner.ToString());
                else item.SubItems.Add("");
                if (conn.StoredConnection != null && conn.StoredConnection.FileName != null) item.SubItems.Add(IOTool.RelativePathTo(Core.DataDirectory, conn.StoredConnection.FileName));
                else item.SubItems.Add("");
                if (conn.Dialect != null) item.SubItems.Add(conn.Dialect.ToString());
                else item.SubItems.Add("");
                item.SubItems.Add((groups.IndexOf(conn.GetConnKey()) + 1).ToString());
                item.Tag = conn;
                if (selected == conn)
                {
                    item.Selected = true;
                    item.Focused = true;
                    selitem = item;
                }
                var state = conn.GetExtendedState();
                item.SubItems.Add(state.QueueLength.ToString());
                item.SubItems.Add(state.ProcessedOkQElements.ToString());
                item.SubItems.Add(state.ProcessedFailQElements.ToString());
            }
            if (selitem != null) lsvConnections.EnsureVisible(selitem.Index);
            lsvConnections.EndUpdate();
        }

        private void PlanReload()
        {
            if (m_plannedReload) return;
            MainWindow.Instance.RunInMainWindow(ReloadConnections);
            m_plannedReload = true;
        }

        void HConnection_RemoveConnection(IPhysicalConnection obj)
        {
            PlanReload();
        }

        void HConnection_ChangeConnection(IPhysicalConnection obj)
        {
            PlanReload();
        }

        void HConnection_AddConnection(IPhysicalConnection obj)
        {
            PlanReload();
        }

        private IPhysicalConnection SelectedConnection
        {
            get
            {
                if (lsvConnections.SelectedItems.Count == 0) return null;
                var conn = lsvConnections.SelectedItems[0].Tag as IPhysicalConnection;
                return conn;
            }
        }

        private void btnCloseConnection_Click(object sender, EventArgs e)
        {
            var conn = SelectedConnection;
            if (conn != null && conn.IsOpened) Async.SafeClose(conn);
        }

        private void lsvConnections_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            lsvConnections.ListViewItemSorter = new ListViewItemComparer(e.Column);
            lsvConnections.Sort();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ReloadConnections();
        }

        private void cbxType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ReloadConnections();
        }
    }

    class ListViewItemComparer : IComparer
    {
        private int col;
        public ListViewItemComparer()
        {
            col = 0;
        }
        public ListViewItemComparer(int column)
        {
            col = column;
        }
        public int Compare(object x, object y)
        {
            int returnVal = -1;
            returnVal = String.Compare(((ListViewItem)x).SubItems[col].Text,
            ((ListViewItem)y).SubItems[col].Text);
            return returnVal;
        }
    }
    [DockerFactory(Title = "Connections window", Name = "connections_window")]
    public class ConnectionsDockerFactory : DockerFactoryBase
    {
        public override IDocker CreateDocker()
        {
            return new ConnectionsDocker(this);
        }

        public override string MenuTitle
        {
            get { return "s_active_connections"; }
        }

        public override Bitmap Icon
        {
            get { return CoreIcons.connect; }
        }

        public override DockerState InitialState
        {
            get { return DockerState.DockBottom; }
        }

        public override Keys Shortcut
        {
            get { return Keys.Control | Keys.Alt | Keys.N; }
        }
    }
}
