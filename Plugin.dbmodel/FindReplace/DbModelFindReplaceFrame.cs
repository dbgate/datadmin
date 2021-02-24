using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DatAdmin;

namespace Plugin.dbmodel
{
    public partial class DbModelFindReplaceFrame : ContentFrame
    {
        IDatabaseSource m_conn;
        IDatabaseStructure m_db;
        IDatabaseStructure m_origDb;

        public DbModelFindReplaceFrame(IDatabaseSource conn)
        {
            InitializeComponent();
            m_conn = conn;
            Async.SafeOpen(m_conn.Connection);
            LoadStructure();
        }

        public override void OnClose()
        {
            base.OnClose();
            Async.SafeClose(m_conn.Connection);
        }

        private void LoadStructure()
        {
            objectGridView1.DataSource = null;
            if (m_conn.Connection != null)
            {
                m_conn.Connection.BeginInvoke((Action)DoLoadStructure, Async.CreateInvokeCallback(m_invoker, LoadedStructure));
                btnSave.Enabled = btnRefresh.Enabled = btnSearch.Enabled = false;
                IsLoadingIcon = true;
                UpdateState();
            }
            else
            {
                DoLoadStructure();
                LoadedStructure(null);
            }
        }

        private void DoLoadStructure()
        {
            m_db = new DatabaseStructure(m_conn.LoadDatabaseStructure(DatabaseStructureMembers.FullStructure, null));
            m_origDb = new DatabaseStructure(m_db);
        }

        private void LoadedStructure(IAsyncResult async)
        {
            try
            {
                if (async != null) m_conn.Connection.EndInvoke(async);
                btnRefresh.Enabled = btnSearch.Enabled = true;
                btnSave.Enabled = SupportsSave;
                ShowCurrentData();
            }
            catch (Exception err)
            {
                Errors.Report(err);
            }
            finally
            {
                IsLoadingIcon = false;
            }
            UpdateState();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            ShowCurrentData();
        }

        private void ShowCurrentData()
        {
            var objs = m_db.GetAllObjects();
            var objs2 = new List<IAbstractObjectStructure>();
            foreach (var obj in objs)
            {
                bool add = false;
                if (obj.ToString().ToLower().Contains(tbxSearch.Text.ToLower()))
                {
                    add = true;
                }
                if (obj is ISpecificObjectStructure && ((ISpecificObjectStructure)obj).CreateSql != null && ((ISpecificObjectStructure)obj).CreateSql.ToLower().Contains(tbxSearch.Text.ToLower()))
                {
                    add = true;
                }
                if (add) objs2.Add(obj);
            }
            objectGridView1.DataSource = objs2;
            objectGridView1.ReadOnly = m_conn.DatabaseCaps.ReadOnly;
            UpdateState();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }

        public override Bitmap Image
        {
            get { return CoreIcons.find; }
        }

        public override string PageTitle
        {
            get { return m_conn.ToString(); }
        }

        public override string PageTypeTitle
        {
            get { return "s_find_and_replace"; }
        }

        private void tbxSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnSearch_Click(sender, e);
            }
        }

        public override bool SupportsSave
        {
            get
            {
                return !m_conn.DatabaseCaps.ReadOnly;
            }
        }

        public override bool Save()
        {
            var dialect = m_conn.Dialect ?? GenericDialect.Instance;
            var plan = new AlterPlan();
            var opts = new DbDiffOptions();
            var log = new CachingLogger(LogLevel.Info);
            opts.AlterLogger = log;
            DbDiffTool.AlterDatabase(plan, new DbObjectPairing(m_origDb, m_db), opts);
            string alterSql = dialect.GenerateScript(dmp => plan.CreateRunner().Run(dmp, opts));
            if (!SqlConfirmForm.Run(alterSql, dialect, log)) return false;
            m_conn.AlterDatabase(plan, opts);
            objectGridView1.Modified = false;
            UpdateState();
            LoadStructure();
            return true;
        }

        public override bool AllowClose()
        {
            if (objectGridView1.Modified)
            {
                DialogResult dr = MessageBox.Show(Texts.Get("s_database_modified_save_q"), VersionInfo.ProgramTitle, MessageBoxButtons.YesNoCancel);
                if (dr == DialogResult.Yes)
                {
                    Save();
                    return !objectGridView1.Modified;
                }
                if (dr == DialogResult.No) return true;
                return false;
            }
            return true;
        }

        [PopupMenu("s_set_value/s_ask_value")]
        public void SetAskedValue()
        {
            string def = "";
            if (objectGridView1.CurrentCell != null) def = (objectGridView1.CurrentCell.Value ?? "").ToString();
            string value = InputBox.Run(Texts.Get("s_type_cell_value"), def);
            if (value != null) objectGridView1.FillCellSelection(value);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (!AllowClose()) return;
            LoadStructure();
        }

        private void objectGridView1_Click(object sender, EventArgs e)
        {
            UpdateState();
        }

        private void UpdateState()
        {
            labModified.Text = objectGridView1.Modified ? Texts.Get("s_modified") : "";
            if (IsLoadingIcon) labRowCount.Text = Texts.Get("s_loading");
            else labRowCount.Text = Texts.Get("s_loaded$objects", "objects", objectGridView1.DataSource.Count);
        }

        private MenuObject GetMenuObject()
        {
            object obj = objectGridView1.SelectedObject;
            var tbl = obj as ITableStructure;
            if (tbl != null) return new TableMenu { Conn = m_conn, Table = tbl };
            var spec = obj as ISpecificObjectStructure;
            if (spec != null) return new SpecObjectMenu { Conn = m_conn, SpecObject = spec };
            return null;
        }

        private void FillPopupMenu()
        {
            mnuGrid.Items.Clear();
            MenuBuilder mb = new MenuBuilder();
            mb.AddObject(this);
            var menu = GetMenuObject();
            if (menu != null) mb.AddObject(menu);
            mb.GetMenuItems(mnuGrid.Items);
        }

        private void objectGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var hinfo = objectGridView1.HitTest(e.X, e.Y);
                if (hinfo.Type == DataGridViewHitTestType.Cell)
                {
                    var cell = objectGridView1.Rows[hinfo.RowIndex].Cells[hinfo.ColumnIndex];
                    if (!cell.Selected)
                    {
                        objectGridView1.CurrentCell = cell;
                    }
                }
                FillPopupMenu();
                mnuGrid.Show(objectGridView1, new Point(e.X, e.Y));
            }
        }

        public abstract class MenuObject
        {
            public abstract void DoubleClick();
        }

        public class TableMenu : MenuObject
        {
            public IDatabaseSource Conn;
            public ITableStructure Table;

            [PopupMenu("s_design", ImageName = CoreIcons.designName)]
            public void Design()
            {
                MainWindow.Instance.OpenContent(new TableEditFrame(Conn.CloneSource(), Table, new AlterTableEditorPars()));
            }

            public override void DoubleClick() { Design(); }
        }

        public class SpecObjectMenu : MenuObject
        {
            public IDatabaseSource Conn;
            public ISpecificObjectStructure SpecObject;

            [PopupMenu("s_design", ImageName = CoreIcons.designName)]
            public void Design()
            {
                MainWindow.Instance.OpenContent(new SpecificObjectFrame(Conn, SpecObject, new ObjectEditorPars()));
            }

            public override void DoubleClick() { Design(); }
        }

        private void objectGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var menu = GetMenuObject();
            if (menu != null) menu.DoubleClick();
        }
    }
}
