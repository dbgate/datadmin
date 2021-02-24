using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DatAdmin;

namespace Plugin.datasyn
{
    public partial class DataSynDefItemFrame : UserControl
    {
        DataSynDefItem m_item;
        int m_loading;
        public DataSynForm MainForm;
        Dictionary<DataSynDefItem, CacheItem> m_itemCache = new Dictionary<DataSynDefItem, CacheItem>();
        ImageCache m_imgCache;
        NameWithSchema m_sourceHint, m_targetHint;
        SynItem m_synItem;
        Func<SynTableData, SynTableData> m_loadGrid;
        GridTable[] m_grids;
        IInvoker m_invoker;

        public DataSynDefItemFrame()
        {
            InitializeComponent();
            m_imgCache = new ImageCache(imageList1, Color.White);
            UpdateSourceVisibility();
            tabSourceAndTarget.ImageIndex = m_imgCache.GetImageIndex(CoreIcons.table);
            UpdateTabIcons();
            m_loadGrid = DoLoadGrid;
            m_invoker = new ControlInvoker(this);
            var svals = Enum.GetValues(typeof(SynTableData));
            m_grids = new GridTable[svals.Length];
        }

        public event EventHandler ChangedItem;
        public event EventHandler<CreateDataSynItemEventArgs> CreatedItem;
        public event EventHandler RemovedItem;

        public DataSynDefItem Item
        {
            get
            {
                return m_item;
            }
        }

        public void SetItem(DataSynDefItem item, SynItem synitem)
        {
            m_item = item;
            m_sourceHint = null;
            m_targetHint = null;
            if (m_item != null && !m_itemCache.ContainsKey(m_item))
            {
                m_itemCache[m_item] = new CacheItem();
            }
            m_synItem = synitem;
            RefreshData();
        }

        public void RefreshData()
        {
            try
            {
                m_loading++;
                cbxSourceTable.Items.Clear();
                //cbxTargetTable.Items.Clear();
                cbxSourceView.Items.Clear();
                codeEditorQuery.SetCodeText("", false);
                tbxTargetTable.Text = m_targetHint != null ? m_targetHint.ToString() : "";
                if (m_item == null && m_sourceHint != null)
                {
                    cbxSourceTable.Items.Add(m_sourceHint);
                    cbxSourceTable.SelectedIndex = 0;
                }
                //tbxSourceQuery.Text = "";
                //cbxSourceType.SelectedIndex = -1;
                //tableLayoutPanel1.Visible = m_item != null;
                rbtNone.Checked = true;
                tbxTargetWhere.Enabled = m_item != null;
                UpdateTabIcons();
                if (MainForm == null || MainForm.SourceModel == null || MainForm.TargetModel == null || m_item == null) return;
                foreach (var tbl in MainForm.SourceModel.Tables.SortedByKey<ITableStructure, NameWithSchema>(t => t.FullName))
                {
                    cbxSourceTable.Items.Add(tbl.FullName);
                }
                foreach (var v in MainForm.SourceModel.SpecByType("view").SortedByKey<ISpecificObjectStructure, NameWithSchema>(s => s.ObjectName))
                {
                    cbxSourceView.Items.Add(v.ObjectName);
                }
                //foreach (var tbl in MainForm.TargetModel.Tables.SortedByKey<ITableStructure, NameWithSchema>(t => t.FullName))
                //{
                //    cbxTargetTable.Items.Add(tbl.FullName);
                //}
                var src0 = m_item.Source as DataSynTableSource;
                if (src0 != null)
                {
                    rbtTable.Checked = true;
                    //cbxSourceType.SelectedIndex = 0;
                    cbxSourceTable.SelectedIndex = cbxSourceTable.Items.IndexOf(src0.Name);
                }
                var src1 = m_item.Source as DataSynViewSource;
                if (src1 != null)
                {
                    rbtView.Checked = true;
                    //cbxSourceType.SelectedIndex = 1;
                    cbxSourceView.SelectedIndex = cbxSourceView.Items.IndexOf(src1.Name);
                }
                var src2 = m_item.Source as DataSynQuerySource;
                if (src2 != null)
                {
                    //cbxSourceType.SelectedIndex = 2;
                    //tbxSourceQuery.Text = src2.Query;
                    rbtQuery.Checked = true;
                    codeEditorQuery.SetCodeText(src2.Query, false);
                }
                if (m_item.Target != null)
                {
                    tbxTargetTable.Text = m_item.Target.Table.ToString();
                    //cbxTargetTable.SelectedIndex = cbxTargetTable.Items.IndexOf(m_item.Target.Table);
                }
                switch (m_item.ColMode)
                {
                    case DataSynDefItem.ColumnMode.All:
                        rbtAllColumns.Checked = true;
                        //tbcColumns.SelectedIndex = 0;
                        break;
                    case DataSynDefItem.ColumnMode.Selected:
                        rbtCompareOnlySelected.Checked = true;
                        //tbcColumns.SelectedIndex = 1;
                        break;
                    case DataSynDefItem.ColumnMode.AllExceptSelected:
                        rbtAllExceptSelected.Checked = true;
                        //tbcColumns.SelectedIndex = 2;
                        break;
                    case DataSynDefItem.ColumnMode.CustomMapping:
                        rbtCustomMapping.Checked = true;
                        //tbcColumns.SelectedIndex = 3;
                        break;
                }
                rbtDefineOwnKey.Checked = m_item.KeyColsOverride != null;
                rbtUsePrimaryKey.Checked = m_item.KeyColsOverride == null;

                switch (m_item.CompareColMode)
                {
                    case DataSynDefItem.ColumnMode.All:
                        rbtCompareAllColumns.Checked = true;
                        break;
                    case DataSynDefItem.ColumnMode.Selected:
                        rbtCompareOnlySelected.Checked = true;
                        break;
                    case DataSynDefItem.ColumnMode.AllExceptSelected:
                        rbtCompareAllExceptSelected.Checked = true;
                        break;
                }
            }
            finally
            {
                m_loading--;
            }
            RefreshModelsIfNeeded(true);
            LoadColumnsPage();
            LoadKeyInfo();
            LoadCompareInfo();
            tbxSourceTableWhere.Text = m_item.Source != null && m_item.Source.SqlCondition != null ? m_item.Source.SqlCondition : "";
            tbxTargetWhere.Text = m_item.Source != null && m_item.Target.SqlCondition != null ? m_item.Target.SqlCondition : "";
        }

        private void LoadCompareInfo()
        {
            lbxCompareColumns.Items.Clear();
            if (CurCache.TargetModel == null) return;
            foreach (var col in CurCache.TargetModel.Columns) lbxCompareColumns.Items.Add(col.ColumnName);
            switch (m_item.CompareColMode)
            {
                case DataSynDefItem.ColumnMode.AllExceptSelected:
                    LoadCheckCols(lbxCompareColumns, m_item.CompareNoColsOverride);
                    break;
                case DataSynDefItem.ColumnMode.Selected:
                    LoadCheckCols(lbxCompareColumns, m_item.CompareColsOverride);
                    break;
            }
        }

        private void LoadKeyInfo()
        {
            lbxKeyColumns.Items.Clear();
            if (!AvailableModels) return;
            foreach (var col in CurCache.TargetModel.Columns) lbxKeyColumns.Items.Add(col.ColumnName);
            lbxKeyColumns.Enabled = m_item.KeyColsOverride != null;
            if (m_item.KeyColsOverride != null)
            {
                rbtDefineOwnKey.Checked = true;
                LoadCheckCols(lbxKeyColumns, m_item.KeyColsOverride);
            }
            else
            {
                rbtUsePrimaryKey.Checked = true;
                var pk = CurCache.TargetModel.FindConstraint<IPrimaryKey>();
                if (pk != null)
                {
                    try
                    {
                        m_loading++;
                        LoadCheckCols(lbxKeyColumns, from c in pk.Columns select c.ColumnName);
                    }
                    finally
                    {
                        m_loading--;
                    }
                }
            }

        }

        private void UpdateSourceVisibility()
        {
            cbxSourceTable.Enabled = tbxSourceTableWhere.Enabled = rbtTable.Checked;
            cbxSourceView.Enabled = tbxSourceViewWhere.Enabled = rbtView.Checked;
            codeEditorQuery.Enabled = rbtQuery.Checked;
        }

        private void cbxSourceType_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateSourceVisibility();
            SaveSource(true);
        }

        private ITableStructure InvokeGetModel(Func<IDatabaseSource, ITableStructure> getModel, IDatabaseSource conn)
        {
            return conn.Connection.InvokeR(() => getModel(conn));
        }

        private void SaveSource(bool catchErrors)
        {
            if (m_loading > 0) return;
            m_item.Source = null;
            if (rbtTable.Checked)
            {
                if (cbxSourceTable.SelectedIndex < 0) return;
                m_item.Source = new DataSynTableSource { Name = (NameWithSchema)cbxSourceTable.SelectedItem };
            }
            if (rbtView.Checked)
            {
                if (cbxSourceView.SelectedIndex < 0) return;
                m_item.Source = new DataSynViewSource { Name = (NameWithSchema)cbxSourceView.SelectedItem };
            }
            if (rbtQuery.Checked)
            {
                m_item.Source = new DataSynQuerySource { Query = codeEditorQuery.CodeText };
            }
            CurCache.ChangeSource();
            RefreshModelsIfNeeded(catchErrors);
            if (ChangedItem != null) ChangedItem(this, EventArgs.Empty);
        }

        private void RefreshModelsIfNeeded(bool catchErrors)
        {
            try
            {
                if (CurCache.SourceChanged && m_item.Source != null && m_item.Source.StaticValid())
                {
                    CurCache.SourceModel = InvokeGetModel(m_item.Source.GetModel, MainForm.SourceDb);
                    CurCache.SourceChanged = false;
                }
                if (CurCache.TargetChanged && m_item.Target != null)
                {
                    CurCache.TargetModel = InvokeGetModel(m_item.Target.GetModel, MainForm.TargetDb);
                    CurCache.TargetChanged = false;
                }
            }
            catch (Exception err)
            {
                if (!catchErrors) throw;
                Logging.Warning(Errors.ExtractImportantException(err).Message);
            }
            UpdateTabIcons();
        }

        private void UpdateTabIcons()
        {
            if (AvailableModels)
            {
                tabColumns.ImageIndex = m_imgCache.GetImageIndex(CoreIcons.column);
                tabKey.ImageIndex = m_imgCache.GetImageIndex(CoreIcons.primary_key);
            }
            else
            {
                tabColumns.ImageIndex = m_imgCache.GetImageIndex(CoreIcons.cancel);
                tabKey.ImageIndex = m_imgCache.GetImageIndex(CoreIcons.cancel);
            }
            if (AvailableCompareInfo)
            {
                tabOnlyInSource.ImageIndex = m_imgCache.GetImageIndex(CoreIcons.source);
                tabOnlyInTarget.ImageIndex = m_imgCache.GetImageIndex(CoreIcons.target);
                tabEqual.ImageIndex = m_imgCache.GetImageIndex(CoreIcons.equals);
                tabModified.ImageIndex = m_imgCache.GetImageIndex(CoreIcons.pen);
            }
            else
            {
                tabOnlyInSource.ImageIndex = m_imgCache.GetImageIndex(CoreIcons.cancel);
                tabOnlyInTarget.ImageIndex = m_imgCache.GetImageIndex(CoreIcons.cancel);
                tabEqual.ImageIndex = m_imgCache.GetImageIndex(CoreIcons.cancel);
                tabModified.ImageIndex = m_imgCache.GetImageIndex(CoreIcons.cancel);
            }
            tabControl1_SelectedIndexChanged(this, EventArgs.Empty);
        }

        private bool AvailableModels
        {
            get
            {
                return CurCache != null && CurCache.SourceModel != null && CurCache.TargetModel != null;
            }
        }

        private bool AvailableCompareInfo
        {
            get
            {
                return m_synItem != null;
            }
        }

        //private void SaveTarget(bool catchErrors)
        //{
        //    if (m_loading > 0) return;
        //    m_item.Target = new DataSynTarget { Table = (NameWithSchema)cbxTargetTable.SelectedItem };
        //    CurCache.ChangeTarget();
        //    RefreshModelsIfNeeded(catchErrors);
        //    if (ChangedItem != null) ChangedItem(this, EventArgs.Empty);
        //}

        private void cbxSourceTable_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_loading > 0) return;
            SaveSource(false);
        }

        private void LoadCheckCols(CheckedListBox chb, IEnumerable<string> list)
        {
            chb.Items.Clear();
            if (CurCache == null) throw new InternalError("DAE-00373 temporary error");
            if (CurCache.TargetModel == null) throw new InternalError("DAE-00374 temporary error");
            foreach (var col in CurCache.TargetModel.Columns) chb.Items.Add(col.ColumnName);
            foreach (var selcol in list)
            {
                var idx = chb.Items.IndexOf(selcol);
                if (idx >= 0) chb.SetItemChecked(idx, true);
            }
        }

        private void LoadColumnsPage()
        {
            lbxAllColumns.Visible = chbSelectedColumns.Visible = chbNotSelectedColumns.Visible = dataGridView1.Visible = false;
            if (!AvailableModels) return;
            if (rbtAllColumns.Checked)
            {
                lbxAllColumns.Items.Clear();
                foreach (var col in CurCache.TargetModel.Columns) lbxAllColumns.Items.Add(col.ColumnName);
                lbxAllColumns.Visible = true;
                lbxAllColumns.Dock = DockStyle.Fill;
            }
            if (rbtOnlySelected.Checked)
            {
                LoadCheckCols(chbSelectedColumns, m_item.SelectedColumns);
                chbSelectedColumns.Visible = true;
                chbSelectedColumns.Dock = DockStyle.Fill;
            }
            if (rbtAllExceptSelected.Checked)
            {
                LoadCheckCols(chbNotSelectedColumns, m_item.SelectedNoColumns);
                chbNotSelectedColumns.Visible = true;
                chbNotSelectedColumns.Dock = DockStyle.Fill;
            }
            if (rbtCustomMapping.Checked)
            {
                LoadCustomGrid();
                dataGridView1.Visible = true;
                dataGridView1.Dock = DockStyle.Fill;
            }
        }

        private void LoadCustomGrid()
        {
            dataGridView1.Rows.Clear();
            var col0 = dataGridView1.Columns[0] as DataGridViewComboBoxColumn;
            col0.Items.Clear();
            foreach (var srccol in CurCache.SourceModel.Columns) col0.Items.Add(srccol.ColumnName);
            foreach (var dstcol in CurCache.TargetModel.Columns)
            {
                string srccol = m_item.SelectedMapping.Get(dstcol.ColumnName, null);
                string defval = m_item.DefaultValues.Get(dstcol.ColumnName, "");
                dataGridView1.Rows.Add(srccol ?? "", dstcol.ColumnName, srccol != null, defval);
            }
        }

        private void tbcColumns_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_loading > 0) return;
            if (rbtAllColumns.Checked) m_item.ColMode = DataSynDefItem.ColumnMode.All;
            if (rbtCompareOnlySelected.Checked) m_item.ColMode = DataSynDefItem.ColumnMode.Selected;
            if (rbtAllExceptSelected.Checked) m_item.ColMode = DataSynDefItem.ColumnMode.AllExceptSelected;
            if (rbtCustomMapping.Checked) m_item.ColMode = DataSynDefItem.ColumnMode.CustomMapping;
            LoadColumnsPage();
        }

        private void chbSelectedColumns_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (m_loading > 0) return;
            MainWindow.Instance.RunInMainWindow(() =>
                m_item.SelectedColumns = new List<string>(from string c in chbSelectedColumns.CheckedItems select c)
            );
        }

        private void chbNotSelectedColumns_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (m_loading > 0) return;
            MainWindow.Instance.RunInMainWindow(() =>
                m_item.SelectedNoColumns = new List<string>(from string c in chbNotSelectedColumns.CheckedItems select c)
            );
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (m_loading > 0) return;
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            SaveColGridRow(e.RowIndex);
        }

        private void SaveColGridRow(int rowindex)
        {
            var row = dataGridView1.Rows[rowindex];
            string colname = row.Cells[1].Value.ToString();
            bool use = !(bool)row.Cells[2].Value;
            if (use) m_item.SelectedMapping[colname] = row.Cells[0].Value.ToString();
            else if (m_item.SelectedMapping.ContainsKey(colname)) m_item.SelectedMapping.Remove(colname);
            if (!row.Cells[3].Value.SafeToString().IsEmpty()) m_item.DefaultValues[colname] = row.Cells[3].Value.SafeToString();
            else if (m_item.DefaultValues.ContainsKey(colname)) m_item.DefaultValues.Remove(colname);
        }

        //private void cbxTargetTable_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (m_loading > 0) return;
        //    SaveTarget(false);
        //}

        private void rbtKey_CheckedChanged(object sender, EventArgs e)
        {
            if (m_loading > 0) return;
            lbxKeyColumns.Enabled = rbtDefineOwnKey.Checked;
            if (rbtDefineOwnKey.Checked)
            {
                m_item.KeyColsOverride = new List<string>(from string c in lbxKeyColumns.CheckedItems select c);
            }
            else
            {
                m_item.KeyColsOverride = null;
            }
        }

        private void lbxKeyColumns_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (m_loading > 0) return;
            MainWindow.Instance.RunInMainWindow(() =>
                m_item.KeyColsOverride = new List<string>(from string c in lbxKeyColumns.CheckedItems select c)
            );
        }

        private void cbxSourceView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_loading > 0) return;
            SaveSource(false);
        }

        CacheItem CurCache
        {
            get
            {
                if (m_item == null) return null;
                return m_itemCache[m_item];
            }
        }

        class CacheItem
        {
            internal ITableStructure SourceModel;
            internal bool SourceChanged = true;
            internal ITableStructure TargetModel;
            internal bool TargetChanged = true;

            internal void ChangeTarget()
            {
                TargetChanged = true;
                TargetModel = null;
            }
            internal void ChangeSource()
            {
                SourceChanged = true;
                SourceModel = null;
            }
        }

        private void SaveCompareInfo()
        {
            lbxCompareColumns.Enabled = rbtCompareOnlySelected.Checked || rbtCompareAllExceptSelected.Checked;
            if (rbtCompareAllColumns.Checked) m_item.CompareColMode = DataSynDefItem.ColumnMode.All;
            if (rbtCompareOnlySelected.Checked)
            {
                m_item.CompareColMode = DataSynDefItem.ColumnMode.Selected;
                m_item.CompareColsOverride = new List<string>(from string c in lbxCompareColumns.CheckedItems select c);
            }
            if (rbtCompareAllExceptSelected.Checked)
            {
                m_item.CompareColMode = DataSynDefItem.ColumnMode.AllExceptSelected;
                m_item.CompareNoColsOverride = new List<string>(from string c in lbxCompareColumns.CheckedItems select c);
            }
        }

        private void rbtCompareAllColumns_CheckedChanged(object sender, EventArgs e)
        {
            if (m_loading > 0) return;
            SaveCompareInfo();
        }

        private void lbxCompareColumns_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (m_loading > 0) return;
            MainWindow.Instance.RunInMainWindow(SaveCompareInfo);
        }

        private void tbxSourceWhere_TextChanged(object sender, EventArgs e)
        {
            m_item.Source.SqlCondition = tbxSourceTableWhere.Text;
        }

        private void tbxTargetWhere_TextChanged(object sender, EventArgs e)
        {
            m_item.Target.SqlCondition = tbxTargetWhere.Text;
        }

        private void rbtNone_CheckedChanged(object sender, EventArgs e)
        {
            if (m_loading == 0)
            {
                if (rbtNone.Checked && m_item != null)
                {
                    if (StdDialog.YesNoDialog("s_really_remove_datasyn_item"))
                    {
                        if (RemovedItem != null) RemovedItem(this, EventArgs.Empty);
                    }
                }
                if (!rbtNone.Checked && m_item == null)
                {
                    if (CreatedItem != null)
                    {
                        var args = new CreateDataSynItemEventArgs();
                        if (rbtTable.Checked) args.Source = new DataSynTableSource();
                        if (rbtView.Checked) args.Source = new DataSynViewSource();
                        if (rbtQuery.Checked) args.Source = new DataSynQuerySource();
                        CreatedItem(this, args);
                    }
                }
            }
            UpdateSourceVisibility();
        }

        SynTableData DoLoadGrid(SynTableData data)
        {
            try
            {
                var newdata = m_synItem.GetGridData(MainForm.SourceDb, MainForm.TargetDb, data, false);
                m_grids[(int)data] = newdata;
            }
            catch (Exception err)
            {
                Errors.Report(err);
            }
            return data;
        }

        private void LoadedGrid(IAsyncResult async)
        {
            var data = (SynTableData)m_loadGrid.EndInvoke(async);
            TestTabs((pg, dt, tbl) =>
            {
                if (data == dt)
                {
                    tbl.TabularData = m_grids[(int)dt];
                    pg.Controls.ShowProgress(false);
                }
            });
        }

        private void TestGridTab_LoadGrid(TabPage page, SynTableData data, TableDataFrame grid)
        {
            if (tabControl1.SelectedTab == page)
            {
                if (AvailableCompareInfo)
                {
                    var table = m_synItem.GetGridData(null, null, data, true);
                    if (table != null)
                    {
                        grid.TabularData = table;
                    }
                    else
                    {
                        page.Controls.ShowProgress(true);
                        m_loadGrid.BeginInvoke(data, m_invoker.CreateInvokeCallback(LoadedGrid), null);
                    }
                }
                else
                {
                    tabControl1.SelectedTab = tabSourceAndTarget;
                }
            }
        }

        private void TestTabs(Action<TabPage, SynTableData, TableDataFrame> proc)
        {
            proc(tabOnlyInSource, SynTableData.OnlyInSource, tblOnlyInSource);
            proc(tabOnlyInTarget, SynTableData.OnlyInTarget, tblOnlyInTarget);
            proc(tabModified, SynTableData.Modified, tblModified);
            proc(tabEqual, SynTableData.Equal, tblEqual);
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!AvailableModels)
            {
                if (tabControl1.SelectedTab == tabKey || tabControl1.SelectedTab == tabColumns)
                {
                    tabControl1.SelectedTab = tabSourceAndTarget;
                }
            }

            TestTabs(TestGridTab_LoadGrid);
        }

        private void codeEditorQuery_Leave(object sender, EventArgs e)
        {
            try
            {
                SaveSource(false);
            }
            catch (Exception err)
            {
                CurCache.TargetChanged = false;
                CurCache.SourceModel = null;
                StdDialog.ShowError(Errors.ExtractImportantException(err).Message);
            }
            if (CurCache.SourceModel != null)
            {
                var cols = new HashSetEx<string>();
                foreach (var col in CurCache.SourceModel.Columns)
                {
                    if (cols.Contains(col.ColumnName))
                    {
                        CurCache.TargetChanged = false;
                        CurCache.SourceModel = null;
                        StdDialog.ShowError(Texts.Get("s_datasynerr_duplicate$column", "column", col.ColumnName));
                        break;
                    }
                    cols.Add(col.ColumnName);
                }
            }
        }

        internal void SetTableNameHint(NameWithSchema sourceHint, NameWithSchema targetHint)
        {
            m_sourceHint = sourceHint;
            m_targetHint = targetHint;
            RefreshData();
        }
    }

    public class CreateDataSynItemEventArgs : EventArgs
    {
        public DataSynDataSource Source;
    }
}
