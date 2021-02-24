using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DatAdmin;

namespace Plugin.mssql
{
    public partial class MsSqlBackupsFrame : WidgetBaseFrame
    {
        DataTable m_table;
        enum Mode { Widget, Content }
        Mode m_mode;

        public MsSqlBackupsFrame()
        {
            InitializeComponent();
            Initialize();
        }

        public MsSqlBackupsFrame(IPhysicalConnection conn)
        {
            InitializeComponent();
            Initialize();
            m_conn = conn;
            m_conn.Owner = this;
            Async.SafeOpen(m_conn);
            m_mode = Mode.Content;
            LoadDatabases();
        }

        public MsSqlBackupsFrame(IWidget widget)
            : base(widget)
        {
            InitializeComponent();
            Initialize();
            m_mode = Mode.Widget;
            panel1.Visible = false;
        }

        protected override void CallLoad(AppObject appobj)
        {
            if (m_mode == Mode.Widget)
            {
                base.CallLoad(appobj);
            }
        }

        protected override bool? ControlVisibility(Control ctrl)
        {
            if (m_mode == Mode.Widget && ctrl == panel1) return false;
            return base.ControlVisibility(ctrl);
        }

        public override void OnClose()
        {
            base.OnClose();
            if (m_conn != null && m_mode == Mode.Content) Async.SafeClose(m_conn);
        }

        private void LoadDatabases()
        {
            var table = m_conn.SystemConnection.LoadTableFromQuery("select distinct database_name from msdb..backupset order by database_name");
            lbxDatabases.Items.Clear();
            foreach (DataRow row in table.Rows)
            {
                lbxDatabases.Items.Add(row[0].ToString());
            }
        }

        private void Initialize()
        {
            Translating.TranslateControl(this);
            dataGridView1.Sort(dataGridView1.Columns[1], ListSortDirection.Descending);
        }

        protected override void DoLoadData()
        {
            if (m_appobj == null)
            {
                m_table = null;
            }
            else
            {
                var db = m_appobj.FindDatabaseConnection(ConnPack);
                DoLoadData(db.DatabaseName);
            }
        }

        private void DoLoadData(string dbname)
        {
            m_table = m_conn.SystemConnection.LoadTableFromQuery("select name, backup_finish_date, user_name, backup_size, backup_set_id from msdb..backupset where database_name='" + dbname + "'");
        }

        protected override void ShowDataInGui()
        {
            dataGridView1.Rows.Clear();
            //if (m_mode == Mode.ObjectView) panel1.Visible = false;
            if (m_table == null) return;
            foreach (DataRow row in m_table.Rows)
            {
                int rowid = dataGridView1.Rows.Add(row[0], row[1], row[2], row[3]);
                dataGridView1.Rows[rowid].Tag = row[4];
            }
            if (dataGridView1.SortedColumn != null)
            {
                var ord = dataGridView1.SortedColumn.HeaderCell.SortGlyphDirection;
                dataGridView1.Sort(dataGridView1.SortedColumn, ord == SortOrder.Ascending ? ListSortDirection.Ascending : ListSortDirection.Descending);
            }
        }

        private string FormatSize(object o)
        {
            long s = Int64.Parse(o.ToString());
            if (s < 1000) return String.Format("{0} B", s);
            if (s < 1000 * 1000) return String.Format("{0:#.##} KB", s / 1000.0);
            if (s < 1000 * 1000 * 1000) return String.Format("{0:#.##} MB", s / 1000.0 / 1000.0);
            return String.Format("{0:#.##} GB", s / 1000.0 / 1000.0 / 1000.0);
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                e.Value = FormatSize(e.Value);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (m_mode == Mode.Content)
            {
                string db = lbxDatabases.SelectedItem as string;
                LoadDatabases();
                if (db != null) lbxDatabases.SelectedIndex = lbxDatabases.Items.IndexOf(db);
            }
            if (m_mode == Mode.Widget)
            {
                CallLoad(m_appobj);
            }
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            DataGridViewRow row = dataGridView1.CurrentRow;
            if (row == null) return;
            if (m_mode == Mode.Content)
            {
                MsSqlRestoreDialog.Run(m_conn.Clone(), Int32.Parse(row.Tag.ToString()));
            }
            if (m_mode == Mode.Widget)
            {
                if (m_appobj == null) return;
                var db = m_appobj.FindDatabaseConnection(ConnPack);
                MsSqlRestoreDialog.Run(db.Connection.Clone(), Int32.Parse(row.Tag.ToString()));
            }
        }

        private void lbxDatabases_SelectedIndexChanged(object sender, EventArgs e)
        {
            string db = lbxDatabases.SelectedItem as string;
            if (db != null)
            {
                DoLoadData(db);
                ShowDataInGui();
            }
        }

        public override string PageTitle
        {
            get
            {
                if (m_mode == Mode.Widget) return base.PageTitle;
                if (m_conn == null) return "MS SQL backups";
                return m_conn.ToString();
            }
        }

        public override Bitmap Image
        {
            get
            {
                return CoreIcons.restore;
            }
        }
    }
}
