using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DatAdmin;
using System.Xml;

namespace Plugin.querytool
{
    public partial class QueryHistoryDocker : DockerBase
    {
        QueryDbConnection m_conn;
        Dictionary<string, ISqlDialect> m_dialects = new Dictionary<string, ISqlDialect>();
        bool m_visible = true;

        public QueryHistoryDocker(IDockerFactory factory)
            : base(factory)
        {
            InitializeComponent();
            m_conn = new QueryDbConnection();
            foreach (var holder in DialectAddonType.Instance.CommonSpace.GetAllAddons())
            {
                m_dialects[holder.Name] = (ISqlDialect)holder.InstanceModel;
            }
            ReloadData();
            HQuery.QueryExecute += HQuery_QueryExecute;
            Disposed += new EventHandler(QueryHistoryDocker_Disposed);
        }

        void QueryHistoryDocker_Disposed(object sender, EventArgs e)
        {
            HQuery.QueryExecute -= HQuery_QueryExecute;
        }

        void HQuery_QueryExecute(QueryExecuteParams obj)
        {
            if (m_visible)
            {
                MainWindow.Instance.RunInMainWindow(ReloadData);
            }
        }

        public override void DockerVisibleChanged(bool visible)
        {
            base.DockerVisibleChanged(visible);
            m_visible = visible;
            if (visible) ReloadData();
        }

        private void ReloadData()
        {
            string sql = "select ID, QueryText_ID, ExecutedAt as \"{s_executed_at}\", Dialect as \"{s_dialect}\", DbServer as \"{s_server}\", DbName as \"{s_database}\", FileName as \"{s_filename}\", DurationInMilisecs as \"{s_duration} (ms)\" from QueryExecute order by ID desc";
            var tbl = m_conn.LoadQuery(sql);
            dataGridView1.DataSource = tbl;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[2].Width = 150;
            Translating.TranslateColumns(dataGridView1.Columns);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ReloadData();
        }

        private void dataGridViewEx1_SelectionChanged(object sender, EventArgs e)
        {
            MainWindow.Instance.RunInMainWindow(LoadCurrentCellData);
        }

        private void LoadCurrentCellData()
        {
            if (dataGridView1.CurrentCell != null)
            {
                string sid = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString();
                string codeid = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[1].Value.ToString();
                string sdialect = m_conn.ExecuteScalar("select Dialect from QueryExecute WHERE ID=@p1", sid).ToString();
                ISqlDialect dialect = m_dialects.Get(sdialect);
                codeEditor1.Dialect = dialect;
                string sql = m_conn.ExecuteScalar("select QueryText from QueryText WHERE ID=@p1", codeid).ToString();
                codeEditor1.Text = sql ?? "";
            }
        }

        private void btnClearHistory_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(Texts.Get("s_really_clear_whole_query_history"), VersionInfo.ProgramTitle, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                m_conn.ExecuteNonQuery("delete from QueryExecute");
                m_conn.ExecuteNonQuery("delete from QueryText");
                ReloadData();
            }
        }

        private void btnOpenInEditor_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCell == null) return;
            MainWindow.Instance.RunInMainWindow(OpenCurrentQuery);
        }

        private void OpenCurrentQuery()
        {
            string sid = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString();
            string codeid = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[1].Value.ToString();
            string sql = m_conn.ExecuteScalar("select QueryText from QueryText WHERE ID=@p1", codeid).ToString();
            string ctx = m_conn.ExecuteScalar("select QueryContext from QueryExecute WHERE ID=@p1", sid).ToString();
            var doc = new XmlDocument();
            doc.LoadXml(ctx);
            var pars = new OpenQueryParameters();
            pars.SavedContext = doc;
            pars.SqlText = sql;
            MainWindow.Instance.OpenContent(new QueryFrame(null, pars));
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            OptionsForm.Run("s_query_history");
        }
    }

    [DockerFactory(Title = "Query history", Name = "query_history", RequiredFeature = QueryHistoryFeature.Test)]
    public class QueryHistoryDockerFactory : DockerFactoryBase
    {
        public override IDocker CreateDocker()
        {
            return new QueryHistoryDocker(this);
        }

        public override string MenuTitle
        {
            get { return "s_query_history"; }
        }

        public override Bitmap Icon
        {
            get { return CoreIcons.history; }
        }

        public override DockerState InitialState
        {
            get { return DockerState.Document; }
        }

        public override Keys Shortcut
        {
            get { return Keys.Control | Keys.Alt | Keys.H; }
        }
    }
}
