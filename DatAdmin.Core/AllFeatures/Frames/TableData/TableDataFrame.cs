using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.Common;
using System.Collections;
using System.Threading;
using System.IO;
using System.Xml;
using System.Linq;

namespace DatAdmin
{
    public partial class TableDataFrame : ContentFrame
    {
        // all fields are defined HERE

        internal BedTable m_table;
        TableRelatedState m_state = new TableRelatedState();
        bool m_ownsConnection;
        public event ReportErrorDelegate ReportError;
        //ITableStructure m_structure;
        //BedTable m_detailTable;
        //int m_trySelectDetailTable = -1;
        int? m_rowCount = null;
        //bool m_preservePaging;
        IAsyncResult m_loadingData;
        // description of load state into status bar
        string m_loadResult;
        bool m_saving;
        TableDataSettings m_defSettings, m_settings;
        DataFormatSettings m_defFmtSettings, m_fmtSettings;
        TableDataMainMenu m_mainMenu;

        //internal int m_statGoToCount;
        //int m_statCommitCount;
        //internal int m_statBackCount;
        //int m_statSetValueCount;
        //int m_statSearchCount;
        //int m_statFilterCount;
        bool m_pagingExplicitlyOn;
        bool m_rowsRequiresPaging;
        bool m_loadedPerspectives;
        bool m_isFilterError;
        bool m_missingReloadPerspectives;
        bool m_forceReadOnly;
        bool m_openedRelated;
        //bool m_disablePerspectiveLayouts = false;

        SaveDataProgress m_saveProgress;
        SaveDataForm m_saveDataForm;
        SaveDataFrame m_saveDataFrame;

        TD_DisplayModel m_dispModel;
        IChartingEngine m_charting;
        //internal ReferencesDockPanelDesign m_loadDockPanelDesign;

        //List<ReferencesTableDataFrame> m_visibleRefs = new List<ReferencesTableDataFrame>();

        public TableDataFrame()
        {
            InitializeComponent();
            m_charting = HChartingEngine.CallCreateChartingEngine();
            btnChart.Visible = m_charting != null;
            Usage["type"] = GetType().FullName;
            if (GlobalSettings.Pages == null) return; // we are in VS
            m_defSettings = GlobalSettings.Pages.TableData();
            m_defFmtSettings = GlobalSettings.Pages.DataFormat();
            if (m_defSettings == null) m_defSettings = new TableDataSettings();
            if (m_defFmtSettings == null) m_defFmtSettings = new DataFormatSettings();
            SetSettings(null, null);
            nbcount.Text = m_settings.DefaultRowLimit.ToString();
            nbstart.Text = "0";
            toolStripPaging.Visible = false;
            toolStripFilter.Visible = false;
            toolStripHistory.Visible = false;
            toolStripPerspective.Visible = false;
            m_mainMenu = new TableDataMainMenu(this);
            UpdateState();
            Disposed += new EventHandler(TableDataFrame_Disposed);
            //HConnection.RemoveByGroup += RemoveConnectionByGroup;
            HQuickExport.ChangedExports += HQuickExport_ChangedExports;
            ReloadQuickExports();
            ReloadPerspectives();
            btnAddPerspective.Visible = AdvancedPerspectivesFeature.Allowed;
            btnDesignPerspective.Visible = AdvancedPerspectivesFeature.Allowed;
            btnManagePerspectives.Visible = AdvancedPerspectivesFeature.Allowed;
            btnSavePerspective.Visible = AdvancedPerspectivesFeature.Allowed;
            SetReferencesEnabled(false);

            foreach (var zoom in dataGridView1.ZoomNames)
            {
                var btn = btnZoom.DropDownItems.Add(zoom);
                btn.Tag = zoom;
                btn.Click += btnChangeZoom_Click;
            }

            OnlineHelpManager.RegisterHelpButton(btnOnlineHelp, "datagrid");
        }

        void btnChangeZoom_Click(object sender, EventArgs e)
        {
            var item = (ToolStripItem)sender;
            dataGridView1.SetZoomName(item.Tag.ToString());
        }

        protected override void OnChangedMasterFrame()
        {
            base.OnChangedMasterFrame();
            btnAddToFavorites.Visible = ParentFrame == null;
        }

        void HQuickExport_ChangedExports()
        {
            ReloadQuickExports();
        }

        [Browsable(false)]
        [DefaultValue(false)]
        public bool ForceReadOnly
        {
            get { return m_forceReadOnly; }
            set
            {
                m_forceReadOnly = value;
                dataGridView1.ReadOnly = IsReadOnly;
            }
        }

        bool m_hideToolbars;
        [Browsable(false)]
        [DefaultValue(false)]
        public bool HideToolbars
        {
            get { return m_hideToolbars; }
            set
            {
                m_hideToolbars = value;
                UpdateState();
            }
        }

        //[Browsable(false)]
        //public bool DisablePerspectiveLayouts
        //{
        //    get { return m_disablePerspectiveLayouts; }
        //    set
        //    {
        //        m_disablePerspectiveLayouts = value;
        //    }
        //}

        private void ReloadQuickExports()
        {
            while (btnQuickExport.DropDownItems.Count > 1) btnQuickExport.DropDownItems.RemoveAt(1);
            QuickExportAddonType.Instance.CommonSpace.ClearCache();
            foreach (var holder in QuickExportAddonType.Instance.CommonSpace.GetFilteredAddons(RegisterItemUsage.DirectUse))
            {
                var item = btnQuickExport.DropDownItems.Add(Texts.Get(holder.Title));
                item.Click += new EventHandler(quickExportItem_Click);
                item.Tag = holder.InstanceModel;
            }
        }

        private ITableStructure GetTableStructure()
        {
            var tbl = CGetStructure(null);
            if (tbl == null && m_table != null) tbl = m_table.Structure;
            return tbl;
        }

        void quickExportItem_Click(object sender, EventArgs e)
        {
            var tbl = GetTableStructure();
            if (tbl == null)
            {
                StdDialog.ShowError("Missing row format");
                return;
            }
            var item = (ToolStripItem)sender;
            var qexp = (IQuickExport)item.Tag;
            var tabdata = qexp.GetDataStore();
            var props = GetDataProperties();
            var transform = new IdentityTransform(tbl);
            var job = BulkCopyJob.Create(TabularData.GetStoreAndClone(props), tabdata, transform, new TableCopyOptions(), null);
            job.StartProcess();
        }

        //public override string UsageEventName
        //{
        //    get { return "table_data"; }
        //}

        void TableDataFrame_Disposed(object sender, EventArgs e)
        {
            //UsageStats.Usage("table_data",
            //    "type", GetType().FullName,
            //    "goto", m_statGoToCount.ToString(),
            //    "commit", m_statCommitCount.ToString(),
            //    "back", m_statBackCount.ToString(),
            //    "setvalue", m_statSetValueCount.ToString(),
            //    "search", m_statSearchCount.ToString(),
            //    "filter", m_statFilterCount.ToString()
            //    );
            //HConnection.RemoveByGroup -= RemoveConnectionByGroup;
            HQuickExport.ChangedExports -= HQuickExport_ChangedExports;
        }

        public TableDataFrame(ITabularDataView tabdata)
            : this()
        {
            TabularData = tabdata;
            UpdateState();
        }

        public event EventHandler ChangedDataState;
        public override string PageTitle
        {
            get
            {
                if (TabularData != null) return String.Format("{0:S}", TabularData);
                return "s_table";
                //return String.Format("{0} {1}", Texts.Get("s_table"), m_tabdata);
            }
        }
        public override string PageToolTip
        {
            get
            {
                if (TabularData != null) return String.Format("{0:M}", TabularData);
                return "s_table";
            }
        }
        public override string PageTypeTitle
        {
            get
            {
                return "s_data";
            }
        }

        private void OpenAndLoadData()
        {
            TabularData.Connection.BeginOpen(Async.CreateInvokeCallback(m_invoker, Opened));
        }

        private void Opened(IAsyncResult async)
        {
            try
            {
                TabularData.Connection.EndOpen(async);
                LoadDataPage();
            }
            catch (Exception e)
            {
                Report(e);
                TabularData = null;
                m_table = null;
            }
        }

        private void Report(Exception e)
        {
            if (ReportError != null) ReportError(this, new ReportErrorEventArgs { Error = e });
            else Errors.Report(e);
        }

        private bool IsReadOnly
        {
            get
            {
                return ForceReadOnly || (TabularData != null ? TabularData.Readonly : true);
            }
        }

        [PopupMenuVisible("s_delete_row")]
        public bool DeleteRowVisible()
        {
            if (dataGridView1.PopupRow < 0) return false;
            if (dataGridView1.ReadOnly) return false;
            return true;
        }

        [PopupMenu("s_delete_row", Shortcut = Keys.Control | Keys.Delete, ImageName = CoreIcons.deleterowName, GroupName = "row")]
        public void DeleteRow()
        {
            if (!DeleteRowVisible()) return;
            dataGridView1.DeleteSelectedRows();
        }

        [PopupMenuVisible("s_duplicate_row")]
        public bool DuplicateRowVisible()
        {
            if (dataGridView1.PopupRow < 0) return false;
            if (dataGridView1.ReadOnly) return false;
            return true;
        }

        [PopupMenu("s_duplicate_row", Shortcut = Keys.Control | Keys.D, ImageName = CoreIcons.duplicateName, GroupName = "row")]
        public void DuplicateRow()
        {
            if (!DuplicateRowVisible()) return;
            dataGridView1.DuplicatePopupRow();
        }

        [PopupMenuVisible("s_insert_row")]
        public bool InsertRowVisible()
        {
            if (IsReadOnly) return false;
            if (dataGridView1.HighlightColumn >= 0) return false;
            return true;
        }

        [PopupMenu("s_insert_row", Shortcut = Keys.Control | Keys.Insert, ImageName = CoreIcons.addrowName, GroupName = "row")]
        public void InsertRow()
        {
            DoAddRows(1);
        }

        private void MakePerspectiveAction(Func<TablePerspective, bool> action)
        {
            if (TabularData == null) return;
            var per = SelectedPerspective;
            if (per == null)
            {
                per = CreateInMemoryPerspective();
            }
            if (per == null)
            {
                return;
            }
            if (!action(per)) return;
            if (TabularData != null) TabularData.NotifyPerspectiveChanged(per);
            per.DockPanelDesign = SaveDockPanel();
            if (per.FileName != null)
            {
                per.SaveToFile();
            }
            ReloadPerspectives(per);
            LoadDataPage();
            toolStripPerspective.Visible = btnPerspective.Checked = true;
        }

        [PopupMenuVisible("s_hide_column")]
        public bool HideColumnVisible()
        {
            return TabularData != null
                && TabularData.TabDataCaps.Perspectives
                && dataGridView1.PopupColumn >= 0
                && !HideToolbars;
        }

        [PopupMenu("s_hide_column", ImageName = CoreIcons.delete_columnName, GroupName = "column")]
        public void HideColumn()
        {
            MakePerspectiveAction(DoHideColumn);
        }

        [PopupMenuVisible("s_add_related_column")]
        public bool AddRelatedColumnVisible()
        {
            return TabularData != null
                && TabularData.TabDataCaps.Perspectives
                && dataGridView1.PopupColumn >= 0
                && !HideToolbars;
        }

        [PopupMenu("s_add_related_column", ImageName = CoreIcons.insert_columnName, GroupName = "column")]
        public void AddRelatedColumn()
        {
            MakePerspectiveAction(DoAddRelatedColumn);
        }

        public bool RealAddRelatedColumnVisible()
        {
            return TabularData != null
                && TabularData.TabDataCaps.Perspectives
                && m_dispModel != null
                && dataGridView1.PopupColumn >= 0
                && m_dispModel[dataGridView1.PopupColumn].Reference != null;
        }

        private bool DoAddRelatedColumn(TablePerspective per)
        {
            var handler = _GetDmlfHandler();
            if (!RealAddRelatedColumnVisible() || handler == null)
            {
                StdDialog.ShowError("s_this_column_has_no_related_table");
                return false;
            }
            handler.BaseStructure = CGetStructure(null);
            handler.Database = TabularData.DatabaseSource;
            int colindex = dataGridView1.PopupColumn;
            var colsrc = per.Select.Columns[colindex].Column;
            if (colsrc == null) return false;
            var tbl = m_dispModel[colindex].Reference.PrimaryKeyTable;
            var ts = TabularData.DatabaseSource.GetTable(tbl).InvokeLoadStructure(TableStructureMembers.ColumnNames | TableStructureMembers.PrimaryKey);
            handler.Tables[tbl] = ts;
            var pk = ts.FindConstraint<IPrimaryKey>();
            if (pk == null) return false;
            if (pk.Columns.Count != 1) return false;
            string[] cols = SelectAddColumnsForm.Run(tbl.ToString(), ts.Columns.GetNames());
            if (cols == null) return false;
            string alias = "NATREF_" + colsrc.GetIdentifier();
            var src = per.Select.From.FindSourceWithAlias(alias);
            if (src == null)
            {
                src = new DmlfSource
                {
                    Alias = alias,
                    TableOrView = tbl,
                };
                var rel = new DmlfRelation
                {
                    JoinType = DmlfJoinType.Left,
                    Reference = src,
                };
                per.Select.From.Relations.Add(rel);
                rel.Conditions.Add(new DmlfEqualCondition
                {
                    LeftExpr = new DmlfColumnRefExpression
                    {
                        Column = new DmlfColumnRef { Source = src, ColumnName = pk.Columns[0].ColumnName }
                    },
                    RightExpr = new DmlfColumnRefExpression { Column = colsrc },
                });
            }
            var colres = new DmlfResultFieldCollection();
            foreach (string col in cols) colres.Add(DmlfResultField.BuildFromColumn(col, src));
            if (colindex == per.Select.Columns.Count - 1) per.Select.Columns.AddRange(colres);
            else per.Select.Columns.InsertRange(colindex + 1, colres);
            per.Select.CompleteUpdatingInfo(handler);
            Usage.AddSub("add_related_column");
            return true;
        }

        private bool DoHideColumn(TablePerspective per)
        {
            var indexes = new List<int>();
            if (dataGridView1.HighlightColumn >= 0)
            {
                indexes.Add(dataGridView1.HighlightColumn);
            }
            else
            {
                foreach (DataGridViewCell cell in dataGridView1.SelectedCells)
                {
                    if (!indexes.Contains(cell.ColumnIndex)) indexes.Add(cell.ColumnIndex);
                }
            }
            indexes.Sort();
            indexes.Reverse();
            foreach (var index in indexes)
            {
                per.Select.Columns.RemoveAt(index);
            }
            Usage.AddSub("hide_column");
            return true;
        }

        private void DoInsertRow(int dindex)
        {
            if (!InsertRowVisible()) return;
            if (m_table != null)
            {
                BedRow row = m_table.NewRow();
                if (dataGridView1.CurrentCell != null)
                {
                    m_table.Rows.Insert((dataGridView1.HighlightRow >= 0 ? dataGridView1.HighlightRow : dataGridView1.CurrentCell.RowIndex) + dindex, row);
                }
                else
                {
                    m_table.Rows.Add(row);
                }
            }
        }

        private void DoAddRows(int count)
        {
            if (!InsertRowVisible()) return;
            if (m_table != null)
            {
                for (int i = 0; i < count; i++)
                {
                    BedRow row = m_table.NewRow();
                    m_table.Rows.Add(row);
                }
                if (dataGridView1.CurrentCell != null)
                {
                    dataGridView1.CurrentCell = dataGridView1.Rows[dataGridView1.Rows.Count - count].Cells[dataGridView1.CurrentCell.ColumnIndex];
                }
            }
        }

        private void FillCellSelection(object value)
        {
            Usage.AddSub("setvalue", value != null ? value.ToString() : "(NULL)");
            //m_statSetValueCount++;
            dataGridView1.FillCellSelection(value);
            //SetCellValue(dataGridView1.CurrentCell, value);
            //if (dataGridView1.CurrentCell == null) return;
            //dataGridView1.CurrentCell.Value = value;
            //DataRow dbrow = ((DataRowView)dataGridView1.CurrentCell.OwningRow.DataBoundItem).Row;
            //dbrow.ItemArray[dataGridView1.CurrentCell.ColumnIndex] = value;
        }

        [PopupMenu("s_set_value", GroupName = "cell")]
        public void _SetValue() { }

        [PopupMenu("s_set_value/NULL", Shortcut = Keys.Control | Keys.D0)]
        public void SetNull()
        {
            FillCellSelection(null);
            //if (dataGridView1.CurrentCell == null) return;
            //dataGridView1.CurrentCell.Value = DBNull.Value;
            //if (dataGridView1.CurrentCell.Value is string) dataGridView1.CurrentCell.Value = null;
            //if (dataGridView1.CurrentCell == null) return;
            //dataGridView1.CurrentCell.Value = DBNull.Value;
        }

        [PopupMenu("s_set_value/FALSE", Shortcut = Keys.Control | Keys.D2)]
        public void SetFalse()
        {
            FillCellSelection(false);
        }

        [PopupMenu("s_set_value/TRUE", Shortcut = Keys.Control | Keys.D1)]
        public void SetTrue()
        {
            FillCellSelection(true);
        }

        [PopupMenu("s_set_value/s_current_date")]
        public void SetCurrentDate()
        {
            FillCellSelection(m_settings.Now.ToString(m_fmtSettings.DateFormat));
        }

        [PopupMenu("s_set_value/s_current_time")]
        public void SetCurrentTime()
        {
            FillCellSelection(m_settings.Now.ToString(m_fmtSettings.TimeFormat));
        }

        [PopupMenu("s_set_value/s_current_datetime")]
        public void SetCurrentDateTime()
        {
            FillCellSelection(m_settings.Now.ToString(m_fmtSettings.DateTimeFormat));
        }

        [PopupMenu("s_set_value/s_current_iso_datetime")]
        public void SetIsoCurrentDateTime()
        {
            FillCellSelection(m_settings.Now.ToString("o"));
        }

        [PopupMenu("s_set_value/GUID")]
        public void SetGuid()
        {
            FillCellSelection(Guid.NewGuid().ToString());
        }

        [PopupMenu("s_set_value/s_clipboard_content")]
        public void SetClipboard()
        {
            FillCellSelection(Clipboard.GetText());
        }

        [PopupMenu("s_set_value/s_ask_value")]
        public void SetAskedValue()
        {
            string def = "";
            if (dataGridView1.CurrentCell != null) def = (dataGridView1.CurrentCell.Value ?? "").ToString();
            string value = InputBox.Run(Texts.Get("s_type_cell_value"), def);
            if (value != null) FillCellSelection(value);
        }

        [PopupMenuVisible("s_set_value")]
        public bool PopupMenuVisible()
        {
            return TabularData != null && !IsReadOnly && dataGridView1.CurrentCell != null && dataGridView1.HighlightColumn < 0 && dataGridView1.HighlightRow < 0;
        }

        [PopupMenuVisible("s_revert_row_changes")]
        public bool RevertRowChangesVisible()
        {
            if (dataGridView1.CurrentCell == null && dataGridView1.HighlightRow < 0) return false;
            if (dataGridView1.HighlightColumn >= 0) return false;
            if (dataGridView1.ReadOnly) return false;
            return true;
        }

        [PopupMenu("s_revert_row_changes", Shortcut = Keys.Control | Keys.R, ImageName = CoreIcons.undoName, GroupName = "row")]
        public void RevertRowChanges()
        {
            dataGridView1.RevertPopupRowChanges();
            UpdateCellDataView();
            UpdateLabels();
        }

        [PopupMenu("s_show_cell_data", ImageName = CoreIcons.dataName, GroupName = "celldata")]
        public void ShowCellData()
        {
        }

        [PopupMenuVisible("s_show_cell_data")]
        [PopupMenuVisible("s_load_cell_data")]
        public bool CellDataReadVisible()
        {
            if (dataGridView1.CurrentCell == null) return false;
            if (dataGridView1.HighlightColumn >= 0) return false;
            if (dataGridView1.HighlightRow >= 0) return false;
            return true;
        }

        [PopupMenuVisible("s_save_cell_data")]
        public bool CellDataWriteVisible()
        {
            if (dataGridView1.CurrentCell == null) return false;
            if (dataGridView1.HighlightColumn >= 0) return false;
            if (dataGridView1.HighlightRow >= 0) return false;
            if (IsReadOnly) return false;
            return true;
        }

        [PopupMenu("s_save_cell_data", ImageName = CoreIcons.saveName, GroupName = "celldata")]
        public void SaveCellData()
        {
            var holder = new BedValueHolder();
            var data = GetCurrentDataHolder();
            if (data == null) return;
            data.GetData(holder);
            if (saveFileDialog1.ShowDialogEx() == DialogResult.OK)
            {
                if (holder.GetFieldType() == TypeStorage.ByteArray)
                {
                    byte[] bdata = holder.GetByteArray();
                    using (FileStream fw = new FileStream(saveFileDialog1.FileName, FileMode.Create))
                    {
                        fw.Write(bdata, 0, bdata.Length);
                    }
                }
                else
                {
                    data.BedConvertor.Formatter.ReadFrom(holder);
                    using (StreamWriter sw = new StreamWriter(saveFileDialog1.FileName))
                    {
                        sw.Write(data.BedConvertor.Formatter.GetText());
                    }
                }
            }
        }

        [PopupMenu("s_load_cell_data", ImageName = CoreIcons.openName, GroupName = "celldata")]
        public void OpenCellData()
        {
            var data = GetCurrentDataHolder();
            if (data == null) return;
            var targetType = data.GetTargetType();
            var holder = new BedValueHolder();
            if (openFileDialog1.ShowDialogEx() == DialogResult.OK)
            {
                if (targetType == TypeStorage.String)
                {
                    using (StreamReader sr = new StreamReader(openFileDialog1.FileName))
                    {
                        holder.SetString(sr.ReadToEnd());
                        data.SetData(holder);
                    }
                }
                else if (targetType == TypeStorage.ByteArray)
                {
                    using (FileStream fr = new FileStream(openFileDialog1.FileName, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        holder.SetByteArray(IOTool.ReadToEnd(fr));
                        data.SetData(holder);
                    }
                }
            }
            UpdateCellDataView();
        }

        private void DoFilterOp(string expr, string nullexpr)
        {
            if (dataGridView1.CurrentCell == null) return;
            if (TabularData == null) return;
            if (!TabularData.TabDataCaps.Filtering) return;
            Usage.AddSub("filter", expr, nullexpr);
            //m_statFilterCount++;
            BedRow dbrow = dataGridView1.GetPopupRow();
            int colindex = dataGridView1.CurrentCell.ColumnIndex;
            string colname = dataGridView1.Columns[colindex].Name;
            cbfilter.Checked = true;
            object value = dbrow != null ? dbrow[colindex] : null;
            if (nullexpr != null && (value == null || DBNull.Value == value))
            {
                tbxFilter.Text = String.Format(nullexpr, GetDialect().QuoteIdentifier(colname));
            }
            else
            {
                tbxFilter.Text = String.Format(expr, GetDialect().QuoteIdentifier(colname), value);
            }
            toolStripFilter.Visible = true;
            RefreshRowCount();
            LoadDataPage();
        }

        [PopupMenuVisible("s_generate_sql")]
        public bool GenerateSqlVisible()
        {
            return TabularData != null;
        }

        [PopupMenu("s_generate_sql", ImageName = CoreIcons.generate_sqlName)]
        public void GenerateSql()
        {
            if (TabularData == null) return;
            GenerateSqlForm.Run(this);
        }

        [PopupMenuVisible("s_export")]
        public bool ExportVisible()
        {
            return TabularData != null;
        }

        [PopupMenu("s_export", ImageName = CoreIcons.exportName)]
        public void ExportMenu()
        {
            if (TabularData == null) return;
            DoExport();
        }

        [PopupMenu("s_filter", ImageName = CoreIcons.filterName, GroupName = "cell")]
        public void _FilterMenu()
        {
        }

        [PopupMenuVisible("s_filter")]
        public bool FilterVisible()
        {
            return TabularData != null && TabularData.TabDataCaps.Filtering && dataGridView1.PopupColumn >= 0 && !HideToolbars;
        }

        [PopupMenuVisible("s_filter/s_value_eq_cell")]
        [PopupMenuVisible("s_filter/s_value_ne_cell")]
        [PopupMenuVisible("s_filter/s_value_lt_cell")]
        [PopupMenuVisible("s_filter/s_value_gt_cell")]
        [PopupMenuVisible("s_filter/s_value_like_cell")]
        [PopupMenuVisible("s_filter/s_value_nlike_cell")]
        public bool FilterWithValueVisible()
        {
            return dataGridView1.CurrentCell != null && dataGridView1.HighlightColumn < 0 && dataGridView1.HighlightRow < 0 && FilterVisible();
        }

        [PopupMenuVisible("s_filter/s_ask_value")]
        [PopupMenuVisible("s_filter/s_cell_is_null")]
        [PopupMenuVisible("s_filter/s_cell_is_not_null")]
        public bool FilterWithoutValueVisible()
        {
            return dataGridView1.PopupColumn >= 0 && FilterVisible();
        }

        [PopupMenu("s_filter/s_ask_value")]
        public void DoFilterAsk()
        {
            string def = "";
            if (dataGridView1.CurrentCell != null) def = (dataGridView1.CurrentCell.Value ?? "").ToString();
            string value = InputBox.Run(Texts.Get("s_type_cell_value"), def);
            if (value != null) DoFilterOp("{0} = '" + value + "'", null);
        }

        [PopupMenu("s_filter/s_value_eq_cell")]
        public void DoFilterEq()
        {
            DoFilterOp("{0} = '{1}'", "{0} IS NULL");
        }

        [PopupMenu("s_filter/s_value_ne_cell")]
        public void DoFilterNe()
        {
            DoFilterOp("{0} <> '{1}'", "{0} IS NOT NULL");
        }

        [PopupMenu("s_filter/s_value_lt_cell")]
        public void DoFilterLt()
        {
            DoFilterOp("{0} < '{1}'", null);
        }

        [PopupMenu("s_filter/s_value_gt_cell")]
        public void DoFilterGt()
        {
            DoFilterOp("{0} > '{1}'", null);
        }

        [PopupMenu("s_filter/s_value_like_cell")]
        public void DoFilterLike()
        {
            DoFilterOp("{0} LIKE '%{1}%'", null);
        }

        [PopupMenu("s_filter/s_value_nlike_cell")]
        public void DoFilterNLike()
        {
            DoFilterOp("NOT( {0} LIKE '%{1}%')", null);
        }

        [PopupMenu("s_filter/s_cell_is_null")]
        public void DoFilterIsNull()
        {
            DoFilterOp("{0} IS NULL", null);
        }

        [PopupMenu("s_filter/s_cell_is_not_null")]
        public void DoFilterIsNotNull()
        {
            DoFilterOp("{0} IS NOT NULL", null);
        }

        private void RefreshPagingEnabling()
        {
            int start = Int32.Parse(nbstart.Text);
            int count = Int32.Parse(nbcount.Text);
            btnNextPage.Enabled = !Detached;
            btnPrevPage.Enabled = !Detached && start > 0;
            btnFirstPage.Enabled = !Detached && start > 0;
            btnLastPage.Enabled = !Detached && m_rowCount != null;
            if (btnNextPage.Enabled && m_rowCount != null && start + count >= m_rowCount.Value)
            {
                btnNextPage.Enabled = false;
                btnLastPage.Enabled = false;
            }
        }

        protected override void ReloadDetachedState()
        {
            nbstart.Enabled = !Detached;
            nbcount.Enabled = !Detached;

            RefreshPagingEnabling();

            if (Detached)
            {
                stateLabel.ForeColor = Color.Red;
                stateLabel.Text = Texts.Get("s_detached");
            }
            else
            {
                stateLabel.ForeColor = Color.FromName(KnownColor.ControlText.ToString());
                stateLabel.Text = "";
            }
        }

        public void DoRefreshData()
        {
            SetReferencesEnabled(false);
            Detached = false;
            if (TabularData == null) return;
            if (TabularData.Connection != null) TabularData.Connection.Cache.BeginRefresh();
            TabularData.NotifyRefresh();
            RefreshRowCount();
            LoadDataPage();
        }

        protected void RefreshRowCount()
        {
            m_rowCount = null;
        }

        public void RefreshData()
        {
            if (MainWindow.Instance.ProcessRefreshMessage()) return;
            DoRefreshData();
        }
        //private void ClearStructureCache()
        //{
        //    m_structure = null;
        //}

        protected void LoadDataPage(bool testclose)
        {
            tbxFilter.AddCurrentToHistory();
            if (testclose && !AllowClose()) return;
            //dataGridView1.DataSource = null;
            //if (!m_preservePaging) nbstart.Text = "0";
            //m_preservePaging = false;
            m_loadResult = null;
            m_listDrawers.Clear();
            m_lookupInfo.Clear();
            //ClearListCache();
            //ClearStructureCache();
            if (TabularData != null)
            {
                if (TabularData.Connection != null)
                {
                    InvalidateCurrentContent();
                    if (TabularData.Connection.IsOpened)
                    {
                        m_loadingData = TabularData.Connection.BeginInvoke((Action)DoLoadData, Async.CreateInvokeCallback(m_invoker, LoadedData));
                        IsLoadingIcon = true;
                    }
                    else
                    {
                        m_table = null;
                        FinishLoadData();
                    }
                }
                else
                { // synchronni verze
                    try
                    {
                        DoLoadData();
                        if (m_settings.LoadRowCount) DoLoadRowCount();
                        FinishLoadData();
                    }
                    catch (Exception e)
                    {
                        Report(e);
                    }
                }
                //ConnTools.InvokeVoid(m_tabdata.Connection, DoLoadData, m_invoker, LoadedData);
            }
            UpdateState();
        }

        public void LoadDataPage()
        {
            LoadDataPage(true);
        }

        private ITabularDataStore GetExportData()
        {
            if (TabularData == null) return null;
            if (dataGridView1.SelectedCells.Count > 1)
            {
                var res = GetExportDataForm.Run();
                switch (res)
                {
                    case GetExportDataForm.Result.All:
                        return TabularData.GetStoreAndClone(GetDataProperties());
                    case GetExportDataForm.Result.SelRows:
                        {
                            var tbl = new BedTable(m_table.Structure);
                            foreach (int i in dataGridView1.GetSelectedOrHighlightedRows().Sorted())
                            {
                                tbl.AddRow(dataGridView1.GetRow(i));
                            }
                            return new DataTableTabularDataStore(TabularData.Connection, (conn) => tbl);
                        }
                    case GetExportDataForm.Result.SelRowsSelCols:
                        {
                            var cols = dataGridView1.GetSelectedOrHighlightedCols().Sorted().ToArray();
                            var ts = new TableStructure();
                            foreach (int col in cols)
                            {
                                ts.AddColumn(m_table.Structure.Columns[col], true);
                            }
                            var tbl = new BedTable(ts);
                            foreach (int i in dataGridView1.GetSelectedOrHighlightedRows().Sorted())
                            {
                                var row = dataGridView1.GetRow(i);
                                tbl.AddRow(new ArrayDataRecord(ts, row.GetValuesByCols(cols)));
                            }
                            return new DataTableTabularDataStore(TabularData.Connection, (conn) => tbl);
                        }
                }
                return null;
            }
            var props = GetDataProperties();
            return TabularData.GetStoreAndClone(props);
        }

        public void DoExport()
        {
            var data = GetExportData();
            if (data == null) return;
            BulkCopyWizard.Run(data, null);
        }

        void UpdateStateAndToolbars()
        {
            RefreshPagingEnabling();
            UpdateLabels();
            UpdateState();
            try
            {
                string lab = TabularData.TableSource.FullName.ToString();
                if (DataState.AdditionalCondition != null) lab += "[" + DataState.AdditionalCondition + "]";
                labMainContent.Text = lab;
            }
            catch (Exception)
            {
                labMainContent.Text = "";
            }
            if (tbxSearch.Focused) dataGridView1.Focus();
            if (m_table != null)
            {
                bool possibleBadPage = m_table.Rows.Count == 0 && DataState.FirstRecord > 0;
                bool visiblePaging = TabularData.TabDataCaps.Paging && (m_rowsRequiresPaging || DataState.FirstRecord > 0 || m_pagingExplicitlyOn);
                cbpaging.Checked = visiblePaging;
                cbpaging.Enabled = TabularData.TabDataCaps.Paging;
                //btnChooseColumns.Enabled = 
                btnPerspective.Enabled = TabularData.TabDataCaps.Perspectives;
                toolStripPaging.Visible = visiblePaging;
                nbstart.BackColor = (possibleBadPage || IsErrorInNumberFormat(nbstart.Text)) ? DataGridStyleBase.ModernRed : SystemColors.Window;
                toolStripPaging.Refresh();
                if (toolStripPaging.Visible) toolStripMain.SendToBack();
            }
            tbxSearch.BackColor = tbxSearch.Text.IsEmpty() ? SystemColors.Window : DataGridStyleBase.ModernYellow;
            btnChooseSearchColumns.Image = (DataState.SearchColumns != null || DataState.SearchExactMatch) ? ImageTool.CombineImages(CoreIcons.more_down, CoreIcons.tick_overlay) : CoreIcons.more_down;
            //btnChooseSearchColumns.BackColor = DataState.SearchColumns == null ? SystemColors.Control : Color.Yellow;
            if (tbxFilter.Text.IsEmpty())
            {
                tbxFilter.BackColor = SystemColors.Window;
            }
            else
            {
                if (m_isFilterError)
                {
                    tbxFilter.BackColor = DataGridStyleBase.ModernRed;
                }
                else
                {
                    tbxFilter.BackColor = DataGridStyleBase.ModernYellow;
                }
                cbfilter.Checked = true;
                toolStripFilter.Visible = true;
            }
        }

        private static bool IsErrorInNumberFormat(string s)
        {
            int res;
            if (!Int32.TryParse(s, out res)) return true;
            return res < 0;
        }

        void SetDataSource(BedTable ds)
        {
            dataGridView1.SetDataSource(ds, DataState.FirstRecord);
            UpdateFrameEnabling();
            //btnAddRow.Enabled = btnRemoveRow.Enabled = btnSave.Enabled = SupportsSave;
            //btnRevertChanges.Enabled = SupportsSave;
            //btnRefresh.Enabled = TabularData != null;
            if (!m_loadedPerspectives)
            {
                ReloadPerspectives();
                m_loadedPerspectives = ds != null;
            }
        }

        void FinishLoadData()
        {
            try
            {
                m_programaticallySelectCell = true;
                m_isFilterError = false;
                SetDataSource(m_table);
                LoadFromCache();
                RestoreCurrentCell();
                dataGridView1.ReadOnly = IsReadOnly;
                if (TabularData != null && TabularData.TabDataCaps.Sorting)
                {
                    foreach (DataGridViewColumn col in dataGridView1.Columns)
                    {
                        col.SortMode = DataGridViewColumnSortMode.Programmatic;
                    }
                    dataGridView1.SqlSortOrder = DataState.SortOrder;
                }
                UpdateStateAndToolbars();
                RestoreCurrentCell();
                LoadLookupData();
            }
            finally
            {
                m_programaticallySelectCell = false;
            }
        }

        public bool IsLoadingData { get { return m_loadingData != null; } }

        public TablePageProperties GetDataProperties()
        {
            TablePageProperties props = new TablePageProperties();
            props.Start = DataState.FirstRecord;
            props.Count = DataState.RecordCount + 1;
            props.FilterSqlCondition = DataState.CreateSqlCondition(m_table != null ? m_table.GetColumnDisplay() : null, GetDialect(), GetDmlfHandler(), true);
            props.FilterSqlConditionNoUserInput = DataState.CreateSqlCondition(m_table != null ? m_table.GetColumnDisplay() : null, GetDialect(), GetDmlfHandler(), false);
            if (DataState.SortOrder != null)
            {
                props.SortOrder = DataState.SortOrder.ToSql(GetDialect(), GetDmlfHandler());
            }
            props.Perspective = DataState.Perspective;
            return props;
        }

        void DoLoadData()
        {
            //m_rowCount = null;
            ITabularDataView tabdata = TabularData;
            if (tabdata == null) return;
            var props = GetDataProperties();
            m_table = tabdata.LoadTableData(props);
            m_rowsRequiresPaging = m_table != null && m_table.Rows.Count > DataState.RecordCount;
            while (m_table != null && m_table.Rows.Count > DataState.RecordCount)
            {
                m_table.Rows.RawRemoveAt(m_table.Rows.Count - 1);
            }
        }
        void DoLoadRowCount()
        {
            ITabularDataView tabdata = TabularData;
            if (tabdata == null) return;
            var props = GetDataProperties();
            m_rowCount = tabdata.LoadRowCount(props);
        }
        void LoadedRowCount(IAsyncResult async)
        {
            try
            {
                IsLoadingIcon = false;
                m_loadingData = null;
                async.StandaloneEndInvoke();
            }
            catch (Exception e)
            {
                // row count cannot be loaded - let data shown, only log error
                m_rowCount = null;
                Errors.ReportSilent(e);
                //HandleLoadException(e);
            }
            UpdateState();
            RefreshPagingEnabling();
        }
        void HandleLoadException(Exception e)
        {
            m_loadResult = "s_error";
            dataGridView1.SetDataSource(null, 0);
            m_table = null;

            if (e is ThreadAbortException)
            {
                m_loadResult = "s_canceled";
            }
            else if (Errors.ExtractImportantException(e) is UserInputSyntaxError)
            {
                m_isFilterError = true;
                StdDialog.ShowError(Texts.Get("s_sql_error_in_filter$message", "message", Errors.ExtractMessage(e)));
            }
            else
            {
                if (ReportError != null)
                {
                    ReportError(this, new ReportErrorEventArgs { Error = e });
                }
                else
                {
                    Errors.ReportSilent(e);
                    var res = LoadDataErrorForm.Run(e);
                    switch (res)
                    {
                        case LoadDataErrorFormResult.Cancel:
                            break;
                        case LoadDataErrorFormResult.ClearSettings:
                            SoftResetView();
                            break;
                        case LoadDataErrorFormResult.LoadAgain:
                            LoadDataPage();
                            break;
                    }
                }
            }
        }

        void LoadedData(IAsyncResult async)
        {
            try
            {
                IsLoadingIcon = false;
                m_loadingData = null;
                async.StandaloneEndInvoke();
                FinishLoadData();
                if (TabularData == null) return;

                if (TabularData.Connection != null && m_settings.LoadRowCount && m_rowCount == null)
                {
                    m_loadingData = TabularData.Connection.BeginInvoke((Action)DoLoadRowCount, Async.CreateInvokeCallback(m_invoker, LoadedRowCount));
                    IsLoadingIcon = true;
                }

                //if (m_tabdata != null && m_tabdata.Connection != null)
                //{
                //    m_tabdata.Connection.BeginInvoke((Action)delegate() { DoLoadStructure(); }, Async.CreateInvokeCallback(m_invoker, LoadedStructure));
                //}
            }
            catch (Exception e)
            {
                HandleLoadException(e);
            }
            if (m_table != null && m_table.Rows.Count == 0)
            {
                DispatchDetailRow(null);
                ShowCurrentDetailsContent();
            }
            Controls.ShowProgress(false, null, null);
            //UpdateState();
            UpdateStateAndToolbars();
            RefreshCurrentPerspective();
            UpdateCellDataView();
        }

        private void ShowCurrentDetailsContent()
        {
            foreach (var det in DetailFrames)
            {
                var td = det as TableDataFrame;
                if (td == null) continue;
                td.Controls.ShowProgress(false, null, null);
                td.ShowCurrentDetailsContent();
            }
        }

        internal ITableStructure CGetStructure(Action guiCallback)
        {
            if (TabularData == null || TabularData.Connection == null || TabularData.TableSource == null) return null;
            ITableStructure tbl = TabularData.TableSource.CGetStructure(guiCallback);
            return tbl;
        }

        internal ITableStructure CPeekStructure()
        {
            if (TabularData == null || TabularData.Connection == null || TabularData.TableSource == null) return null;
            ITableStructure tbl = TabularData.TableSource.CPeekStructure();
            return tbl;
        }

        internal ITableStructure CGetStructure(NameWithSchema table, Action guiCallback)
        {
            if (TabularData == null || TabularData.Connection == null || TabularData.DatabaseSource == null) return null;
            ITableStructure tbl = TabularData.DatabaseSource.GetTable(table).CGetStructure(guiCallback);
            return tbl;
        }

        public ISqlDialect GetDialect()
        {
            if (TabularData != null && TabularData.Connection != null && TabularData.Connection.Dialect != null) return TabularData.Connection.Dialect;
            return GenericDialect.Instance;
        }

        private IDialectDataAdapter GetDDA()
        {
            return GetDialect().CreateDataAdapter();
        }

        private void LoadLookupData()
        {
            try
            {
                ITableStructure tbl = CGetStructure(LoadLookupData);
                if (tbl == null) return;
                
                var tss = new Dictionary<NameWithSchema, ITableStructure>();
                foreach (var tlink in dataGridView1.ColumnDisplay.GetLinkedTables())
                {
                    var tslink = CGetStructure(tlink, LoadLookupData);
                    if (tslink == null) return;
                    tss[tlink] = tslink;
                }

                m_dispModel = new TD_DisplayModel();
                var disp = m_table.GetColumnDisplay();
                for (int i = 0; i < disp.Count; i++)
                {
                    string colname;
                    ITableStructure ts;
                    bool islinked = false;
                    if (m_table.ResultFields != null)
                    {
                        var col = m_table.ResultFields[i].Column;
                        if (col == null) continue; // result field is not column
                        ts = tbl;
                        if (col.Source != null && col.Source.TableOrView != null) ts = tss[col.Source.TableOrView];
                        colname = col.ColumnName;
                        islinked = col.Source != null && col.Source != DmlfSource.BaseTable;
                    }
                    else
                    {
                        ts = tbl;
                        colname = m_table.Structure.Columns[i].ColumnName;
                    }
                    var cs = ts.Columns[colname];

                    bool ispk = ts.GetKeyWithColumn<IPrimaryKey>(cs) != null;
                    var fk = ts.GetKeyWithColumn<IForeignKey>(cs);
                    if (fk != null && fk.Columns.Count > 1) fk = null;

                    var dcol = new TD_DisplayColumn
                    {
                        IsLinked = islinked,
                        IsNullable = cs.IsNullable,
                        IsPrimaryKey = ispk,
                        Reference = fk,
                    };
                    m_dispModel.Add(dcol);
                }

                dataGridView1.HeaderModel = m_dispModel;
                SetReferencesEnabled(true);
                if (m_missingReloadPerspectives) ReloadPerspectives();

                LoadLists();
            }
            catch (Exception err)
            {
                Logging.Warning("Error loading lookup data:" + err.ToString());
                TabularData.Connection.Cache.EndRefresh();
            }
        }

        //private void LoadLookupData()
        //{
        //    try
        //    {
        //        if (TabularData == null || TabularData.Connection == null || TabularData.TableSource == null) return;
        //        ITableStructure tbl = TabularData.TableSource.CGetStructure(LoadLookupData);
        //        if (tbl == null) return;
        //        cbxDetailTable.Items.Clear();
        //        List<RefDef> refdefs = new List<RefDef>();
        //        foreach (IForeignKey fk in tbl.GetReferencedFrom())
        //        {
        //            if (fk.Columns.Count == 1)
        //            {
        //                refdefs.Add(new RefDef { Fk = fk, Text = String.Format("{0} ({1})", fk.Table, fk.Columns[0]) });
        //            }
        //        }
        //        refdefs.SortByKey(rd => rd.Text);
        //        foreach (var rd in refdefs) cbxDetailTable.Items.Add(rd);
        //        if (cbxDetailTable.Items.Count > 0)
        //        {
        //            if (DataState.DetailTableIndex >= 0 && DataState.DetailTableIndex < cbxDetailTable.Items.Count)
        //            {
        //                cbxDetailTable.SelectedIndex = DataState.DetailTableIndex;
        //            }
        //            else
        //            {
        //                cbxDetailTable.SelectedIndex = 0;
        //            }
        //        }
        //        //m_trySelectDetailTable = -1;
        //        //FireLoadLists();
        //        LoadLists();
        //    }
        //    catch (Exception err)
        //    {
        //        Logging.Warning("Error loading lookup data:" + err.ToString());
        //    }
        //}

        //private void DoLoadStructure()
        //{
        //    if (m_tabdata != null && m_tabdata.TableSource != null)
        //    {
        //        m_structure = m_tabdata.TableSource.LoadTableStructure();
        //    }
        //}

        //private void LoadedStructure(IAsyncResult async)
        //{
        //    try
        //    {
        //        async.InvokerEndInvoke();
        //        cbxDetailTable.Items.Clear();
        //        if (m_structure != null)
        //        {
        //            foreach (IForeignKey fk in m_structure.ReferencedFrom)
        //            {
        //                if (fk.Columns.Count == 1)
        //                {
        //                    cbxDetailTable.Items.Add(String.Format("{0} ({1})", fk.Table, fk.Columns[0]));
        //                }
        //            }
        //            if (cbxDetailTable.Items.Count > 0)
        //            {
        //                if (m_trySelectDetailTable >= 0 && m_trySelectDetailTable < cbxDetailTable.Items.Count)
        //                {
        //                    cbxDetailTable.SelectedIndex = m_trySelectDetailTable;
        //                }
        //                else
        //                {
        //                    cbxDetailTable.SelectedIndex = 0;
        //                }
        //            }
        //            m_trySelectDetailTable = -1;
        //            FireLoadLists();
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Logging.Error("Error loading reference defs:" + e.ToString());
        //        Report(e);
        //    }
        //}

        //private void LoadedReferenceData(IAsyncResult async)
        //{
        //    try
        //    {
        //        async.StandaloneEndInvoke();
        //        dataGridView2.SetDataSource(m_detailTable);
        //        labDetailContent.Text = CurrentReference != null ? CurrentReference.ToString() : "";
        //    }
        //    catch (Exception e)
        //    {
        //        Logging.Error("Error loading references:" + e.ToString());
        //    }
        //}

        private void btnLoadPage_Click(object sender, EventArgs e)
        {
            //m_preservePaging = true;
            ChangedPaging();
            LoadDataPage();
        }

        private void ChangedPaging()
        {
            try
            {
                DataState.FirstRecord = Int32.Parse(nbstart.Text);
                nbstart.BackColor = SystemColors.Window;
            }
            catch
            {
                DataState.FirstRecord = 0;
                nbstart.BackColor = Color.Red;
            }
            try
            {
                DataState.RecordCount = Int32.Parse(nbcount.Text);
                nbcount.BackColor = SystemColors.Window;
            }
            catch
            {
                DataState.RecordCount = 200;
                nbcount.BackColor = Color.Red;
            }
        }

        private void btnPrevPage_Click(object sender, EventArgs e)
        {
            nbstart.Text = Math.Max(0, Int32.Parse(nbstart.Text) - Int32.Parse(nbcount.Text)).ToString();
            ChangedPaging();
            LoadDataPage();
        }

        private void btnNextPage_Click(object sender, EventArgs e)
        {
            nbstart.Text = (Int32.Parse(nbstart.Text) + Int32.Parse(nbcount.Text)).ToString();
            ChangedPaging();
            LoadDataPage();
        }

        public override bool SupportsSave
        {
            get
            {
                if (TabularData == null) return false;
                return !IsReadOnly;
            }
        }
        public override bool Save()
        {
            SaveChanges();
            return true;
        }

        public void SaveChanges()
        {
            if (TabularData == null) return;
            if (!IsReadOnly)
            {
                if (dataGridView1.Focused || dataGridView1.EditingControl != null)
                {
                    tbxSearch.Focus();
                }

                string updateSql = null;
                bool ispk = TabularData.GetStructure(DataState.Perspective).FindConstraint<IPrimaryKey>() != null;
                m_settings.WantLoaded();
                if (TabularData.DatabaseSource != null && TabularData.DatabaseSource.DatabaseCaps.ExecuteSql && (m_settings.ShowSqlConfirm || !ispk))
                {
                    var log = new CachingLogger(LogLevel.Warning);
                    if (!ispk) log.Warning(Texts.Get("s_table_has_not_primary_key_update_can_affect_more_rows"));
                    updateSql = TabularData.Connection.Dialect.GenerateScript(dmp => TabularData.SaveChanges(m_table, dmp));
                    if (!SqlConfirmForm.Run(updateSql, TabularData.Connection.Dialect, log, TableDataSettings.ShowSqlConfirmKey))
                    {
                        dataGridView1.Focus();
                        return;
                    }
                }

                IsLoadingIcon = true;
                m_saving = true;
                Usage.AddSub("save");
                //m_statCommitCount++;
                //m_preservePaging = true;
                if (TabularData.Connection != null)
                {
                    m_saveProgress = new SaveDataProgress();
                    TabularData.Connection.BeginInvoke((Action)DoSaveChanges, Async.CreateInvokeCallback(m_invoker, OnSaved));
                    m_saveDataForm = new SaveDataForm(m_saveProgress, TableDataSettings.ShowSavedInfoKey);
                    m_saveDataForm.ShowDialogEx();
                    if (m_saveDataForm != null && !m_saveDataForm.IsFinished())
                    {
                        dataGridView1.Visible = false;
                        m_saveDataFrame = new SaveDataFrame();
                        m_saveDataFrame.Progress = m_saveProgress;
                        this.panel1.Controls.Add(m_saveDataFrame);
                    }
                    dataGridView1.Focus();
                }
                else
                {
                    DoSaveChanges();
                    OnSaved(null);
                }
                UpdateState();
            }
            //ConnTools.InvokeVoid(m_tabdata.Connection, DoSaveChanges, m_invoker, OnSaved);
        }

        private void DoSaveChanges()
        {
            TabularData.SaveChanges(m_table, m_saveProgress);
        }

        protected virtual void SaveFinished()
        {
            Detached = false;
        }

        private void OnSaved(IAsyncResult async)
        {
            try
            {
                m_saving = false;
                IsLoadingIcon = false;
                if (async != null) async.StandaloneEndInvoke();
                if (m_saveProgress != null) m_saveProgress.NotifyFinished();
                if (m_saveDataForm != null)
                {
                    m_settings.WantLoaded();
                    m_saveDataForm.SaveFinished(m_settings.ShowSavedInfo);
                    m_saveDataForm = null;
                }
                if (m_saveDataFrame != null)
                {
                    m_saveDataFrame.Dispose();
                    m_settings.WantLoaded();
                    if (m_settings.ShowSavedInfo)
                    {
                        var win = new SaveDataForm(m_saveProgress, TableDataSettings.ShowSavedInfoKey);
                        win.ShowResultDialog();
                    }
                    m_saveDataFrame = null;
                    dataGridView1.Visible = true;
                }
                m_saveProgress = null;
                //if (m_settings.ShowSavedInfo) StdDialog.ShowInfo("s_saved_ok");
                SaveFinished();
                LoadDataPage(false);
            }
            catch (Exception e)
            {
                Report(e);
                if (m_saveDataForm != null)
                {
                    m_saveDataForm.Dispose();
                    m_saveDataForm = null;
                }
                if (m_saveDataFrame != null)
                {
                    m_saveDataFrame.Dispose();
                    m_saveDataFrame = null;
                    dataGridView1.Visible = true;
                }
            }
            UpdateState();
        }

        private void UpdateLabels()
        {
            if (m_table != null)
            {
                int inserted = 0;
                int deleted = 0;
                int updated = 0;
                foreach (var row in m_table.Rows)
                {
                    switch (row.RowState)
                    {
                        case BedRowState.Added:
                            inserted++;
                            break;
                        case BedRowState.Modified:
                            updated++;
                            break;
                        case BedRowState.Deleted:
                            deleted++;
                            break;
                    }
                }
                labInsertedRows.Text = inserted.ToString();
                labInsertedRows.Enabled = inserted > 0;
                labDeletedRows.Text = deleted.ToString();
                labDeletedRows.Enabled = deleted > 0;
                labUpdatedRows.Text = updated.ToString();
                labUpdatedRows.Enabled = updated > 0;
                tbxLoadedRows.Text = m_table.Rows.Count.ToString();
            }
            else
            {
                tbxLoadedRows.Text = "";
            }
        }

        private void dataGridView1_RowValidated(object sender, DataGridViewCellEventArgs e)
        {
            dataGridView1.InvalidateRow(e.RowIndex);
            UpdateLabels();
        }

        public override string MenuBarTitle
        {
            get { return "s_table"; }
        }

        public override void OnClose()
        {
            base.OnClose();
            if (TabularData != null)
            {
                if (m_ownsConnection && TabularData.Connection != null)
                {
                    IAsyncResult res = TabularData.Connection.BeginClose(null);
                    Async.WaitFor(res);
                    TabularData.Connection.EndClose(res);
                }
                TabularData.CloseView();
            }
        }

        //protected virtual void DisallowedClose() { }

        private bool AllowCloseTable()
        {
            if (Modified)
            {
                DialogResult dr = MessageBox.Show(Texts.Get("s_close_table_save_q"), VersionInfo.ProgramTitle, MessageBoxButtons.YesNoCancel);
                if (dr == DialogResult.Yes)
                {
                    try
                    {
                        if (TabularData.Connection != null)
                        {
                            IAsyncResult res = TabularData.Connection.BeginInvoke((Action)DoSaveChanges, null);
                            Async.WaitFor(res);
                            res.StandaloneEndInvoke();

                        }
                        else
                        {
                            DoSaveChanges();
                        }
                    }
                    catch (Exception e)
                    {
                        Report(e);
                        return false;
                    }
                    return true;
                }
                if (dr == DialogResult.No) return true;
                //DisallowedClose();
                return false;
            }
            return true;
        }

        public bool Modified
        {
            get
            {
                if (m_table != null)
                {
                    if (TabularData != null && IsReadOnly) return false;
                    foreach (var row in m_table.Rows)
                    {
                        if (row.RowState != BedRowState.Unchanged) return true;
                        //if (row.RowState != BedRowState.Unchanged) {
                        //    if (Core.IsMono) {
                        //        if (row.IsAnyChange()) return true;
                        //    } else {
                        //        return true;
                        //    }
                        //}
                    }
                }
                return false;
            }
        }

        public override bool AllowClose()
        {
            return AllowCloseTable();
        }

        protected void UpdateState()
        {
            btnAddToFavorites.Enabled = TabularData != null && TabularData.SupportsSerialize;
            if (TabularData == null)
            {
                stateLabel.Text = Texts.Get("s_no_data");
                countLabel.Text = "???";
                btnCancelLoading.Enabled = false;
            }
            else
            {
                btnCancelLoading.Enabled = m_loadingData != null && m_loadingData is ICancelable;
                switch (TabularData.State)
                {
                    case TabularDataViewState.Error:
                        stateLabel.Text = Texts.Get("s_error");
                        break;
                    case TabularDataViewState.Prepared:
                        if (m_saving) stateLabel.Text = Texts.Get("s_saving");
                        else if (m_loadingData != null) stateLabel.Text = Texts.Get("s_loading");
                        else if (m_loadResult != null) stateLabel.Text = Texts.Get(m_loadResult);
                        else if (IsReadOnly) stateLabel.Text = Texts.Get("s_readonly");
                        else if (Modified) stateLabel.Text = Texts.Get("s_modified");
                        else stateLabel.Text = Texts.Get("s_prepared");
                        break;
                    case TabularDataViewState.Loading:
                        stateLabel.Text = Texts.Get("s_loading");
                        break;
                }
                string colstr = Texts.Get("s_columns") + ":" + (m_table != null ? m_table.Structure.Columns.Count.ToString() : "???");
                if (m_rowCount == null) countLabel.Text = Texts.Get("s_records") + ":" + "???" + "; " + colstr;
                else countLabel.Text = Texts.Get("s_records") + ":" + m_rowCount.Value.FormatInt() + "; " + colstr;
            }
            if (ChangedDataState != null) ChangedDataState(this, EventArgs.Empty);

            btnOpenForEditing.Visible = HideToolbars && TabularData != null && TabularData.TableSource != null;
            if (toolStripMain.Visible != !HideToolbars) toolStripMain.Visible = !HideToolbars;
            if (HideToolbars)
            {
                cbpaging.Checked = false;
                cbfilter.Checked = false;
                cbhistory.Checked = false;
                btnPerspective.Checked = false;
            }
            btnChooseSearchColumns.Enabled = m_table != null && TabularData != null && TabularData.TabDataCaps.Filtering;

            btnRefresh.Enabled = TabularData != null;
            btnGenerateSql.Enabled = TabularData != null;
            btnDuplicateRow.Enabled = !IsReadOnly;
            btnRevertChanges.Enabled = !IsReadOnly;
            btnExport.Enabled = btnQuickExport.Enabled = TabularData != null;
            btnChart.Enabled = TabularData != null;

            btnAddRow.Enabled = btnRemoveRow.Enabled = btnSave.Enabled = !IsReadOnly;
            btnRevertChanges.Enabled = !IsReadOnly;
            btnRefresh.Enabled = TabularData != null;        }

        private void LoadPageIfNeeded()
        {
            if (m_table == null ||
                (m_rowCount != null && DataState.FirstRecord + m_table.Rows.Count < m_rowCount
                && m_table.Rows.Count < DataState.RecordCount))
            {
                LoadDataPage();
            }
        }

        public ITabularDataView TabularData
        {
            get { return DataState.TableData; }
            set
            {
                if (value == null && DataStatePer.TableData == null) return;
                m_loadedPerspectives = false;
                HCellData.CallInvalidateData(this);
                State = new TableRelatedState();
                DataStatePer.TableData = value;
                if (value != null) SetSettings(value.Settings.TableData(), value.Settings.DataFormat());
                else SetSettings(null, null);
                if (value != null && value.FixedPerspective != null)
                {
                    DataStatePer.UseFixedPerspective(value.FixedPerspective);
                }
                RefreshRowCount();
                RefreshCurrentView();
                SetReferencesEnabled(false);
                //UpdateState();
            }
        }

        private void SetSettings(TableDataSettings value, DataFormatSettings fvalue)
        {
            if (value != null) m_settings = value;
            else m_settings = m_defSettings;
            if (fvalue != null) m_fmtSettings = fvalue;
            else m_fmtSettings = m_defFmtSettings;
            dataGridView1.SetSettings(m_settings, m_fmtSettings);
        }

        void OnLoadedNextData(object sender, LoadedNextDataArgs e)
        {
            while (!IsHandleCreated) System.Threading.Thread.Sleep(100);
            m_rowCount = e.LoadedRowCount;
            Invoke((Action)LoadPageIfNeeded);
            Invoke((Action)UpdateStateAndToolbars);
        }

        //private void CallReloadDetail(int colindex, object value, int rowindex)
        //{
        //    if (TabularData == null || TabularData.Connection == null) return;
        //    if (!cbreferences.Checked) return;
        //    RefDef rd = cbdetail.Checked ? (RefDef)cbxDetailTable.SelectedItem : null;
        //    // !!! assume primary key column is first
        //    if (rd != null) value = dataGridView1.Rows[rowindex].Cells[0].Value;
        //    TabularData.Connection.BeginInvoke((Action)delegate() { DoLoadReferenceData(colindex, value, rd); }, Async.CreateInvokeCallback(m_invoker, LoadedReferenceData));
        //}

        //private void ReloadDetail()
        //{
        //    try
        //    {
        //        if (dataGridView1.CurrentCell == null) return;
        //        CallReloadDetail(dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.Value, dataGridView1.CurrentCell.RowIndex);
        //    }
        //    catch (Exception err)
        //    {
        //        Logging.Warning("Error reloading detail: {0}", err);
        //    }
        //}

        protected void ResetPaging()
        {
            nbstart.Text = "0";
            ChangedPaging();
        }

        private void ApplyFilter()
        {
            RefreshRowCount();
            ResetPaging();
            if (TabularData == null) return;
            if (TabularData.TabDataCaps.Filtering)
            {
                LoadDataPage();
            }
            else
            {
                try
                {
                    if (tbxSearch.Text != "" && m_table != null)
                    {
                        BedTable filtered = m_table.Filter(row => row.ContainsText(tbxSearch.Text));
                        Usage.AddSub("search");
                        //m_statSearchCount++;
                        dataGridView1.SetDataSource(filtered, DataState.FirstRecord);
                        dataGridView1.ReadOnly = true;
                    }
                    if (tbxSearch.Text == "")
                    {
                        dataGridView1.SetDataSource(m_table, DataState.FirstRecord);
                        dataGridView1.ReadOnly = IsReadOnly;
                    }
                }
                catch (Exception)
                {
                    dataGridView1.SetDataSource(m_table, DataState.FirstRecord);
                    dataGridView1.ReadOnly = IsReadOnly;
                }
            }
        }

        private void btfilter_Click(object sender, EventArgs e)
        {
            RefreshRowCount();
            LoadDataPage();
            ResetPaging();
        }

        private void btclear_Click(object sender, EventArgs e)
        {
            RefreshRowCount();
            tbxFilter.Text = "";
            ResetPaging();
            LoadDataPage();
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("test");
        }

        private void FillPopupMenu()
        {
            mnuGrid.Items.Clear();
            MenuBuilder mb = new MenuBuilder();
            mb.AddObject(this);

            foreach (var holder in CellDataEditorAddonType.Instance.CommonSpace.GetAllAddons())
            {
                var celli = (ICellDataEditor)holder.CreateInstance();
                mb.AddItem("s_show_cell_data/" + celli.MenuTitle, new CellDataMenu { Editor = celli }.OnClick, celli.Icon);
            }

            mb.GetMenuItems(mnuGrid.Items);
        }

        class CellDataMenu
        {
            internal ICellDataEditor Editor;
            internal void OnClick()
            {
                MainWindow.Instance.ShowDocker(new CellDataDockerFactory(Editor));
            }
        }

        private void tbsearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                ApplyFilter();
                e.Handled = true;
            }
            if (e.KeyCode == Keys.Escape)
            {
                tbxSearch.Text = "";
                ApplyFilter();
                e.Handled = true;
            }
        }

        private void toolStrip2_Resize(object sender, EventArgs e)
        {
            tbxFilter.Width = toolStripFilter.Width - toolStripLabel1.Width - btfilter.Width - btclear.Width - 20;
        }

        private void btsearch_Click(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        private void cbfilter_Click(object sender, EventArgs e)
        {
            toolStripFilter.Visible = cbfilter.Checked;
            if (cbpaging.Checked) toolStripMain.SendToBack();
        }

        private void cbpaging_Click(object sender, EventArgs e)
        {
            toolStripPaging.Visible = cbpaging.Checked;
            m_pagingExplicitlyOn = cbpaging.Checked;
            if (cbpaging.Checked) toolStripMain.SendToBack();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            tbxSearch.Text = "";
            ApplyFilter();
        }

        private void tbfilter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) btfilter_Click(sender, e);
        }

        //private void ShowReferencesTable()
        //{
        //    toolStripDependend.Visible = cbreferences.Checked;
        //    splitContainer1.Panel2Collapsed = !cbreferences.Checked;
        //    DataState.VisibleReferencesTable = cbreferences.Checked;
        //    SelectMaster(false);
        //    UpdateHistoryEnabling();
        //    //ReloadDetail();
        //}

        //private void SelectMaster(bool ismaster)
        //{
        //    cbmaster.Checked = ismaster;
        //    cbdetail.Checked = !ismaster;
        //    cbxDetailTable.Enabled = !ismaster;
        //    DataState.IsMaster = ismaster;
        //    ReloadDetail();
        //}

        private void cbmaster_Click(object sender, EventArgs e)
        {
            //SelectMaster(true);
        }

        private void cbdetail_Click(object sender, EventArgs e)
        {
            //SelectMaster(false);
        }

        //private void lbdetailtable_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    DataState.DetailTableIndex = cbxDetailTable.SelectedIndex;
        //    //if (cbreferences.Checked) ReloadDetail();
        //}

        public override bool AllowCloseConnection(string connkey)
        {
            if (TabularData != null && TabularData.GetConnectionKey() == connkey)
            {
                if (!AllowClose())
                {
                    return false;
                }
                MainWindow.Instance.RunInMainWindow(CloseContent);
            }
            return true;
        }

        protected virtual void OnSelectionChanged()
        {
        }

        public void NotifySelectionChanged()
        {
            if (dataGridView1.IgnoreSelectionChanged) return;
            if (dataGridView1.CurrentCell != null)
            {
                SaveCurrentCell();
                try
                {
                    BedRow row = dataGridView1.GetCurrentRow();
                    if (dataGridView1.Focused)
                    {
                        HCellData.CallShowData(this, GetCurrentDataHolder());
                    }
                    //dataGridView1.Focus();

                    DispatchDetailRow(row);
                    //foreach (var refgrid in DataState.RefGrids) refgrid.SelectedRow(row);

                    //if (cbreferences.Checked)
                    //{
                    //    CallReloadDetail(dataGridView1.CurrentCell.ColumnIndex, value, dataGridView1.CurrentCell.RowIndex);
                    //}
                }
                catch (Exception) { }
            }
            else
            {
                DispatchDetailRow(null);
            }
            UpdateLabels();
            OnSelectionChanged();
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            NotifySelectionChanged();
        }

        private void DispatchDetailRow(BedRow row)
        {
            foreach (var detail in DetailFrames)
            {
                // HACK: test m_docWrapper = null - ensure that content is visible 
                // (see handling in IDockWrapper.ReplaceContent)
                if (!detail.IsContentVisible || (detail.ParentFrame ?? detail).m_dockWrapper == null) continue;
                var det = detail as ReferencesTableDataFrame;
                if (det == null) continue;
                det.SelectedRow(row);
            }
        }

        TablePerspective SelectedPerspective
        {
            get
            {
                if (DataStatePer.IsFixedPerspective) return DataStatePer.Perspective;
                if (AsyncTool.IsMainThread)
                {
                    return cbxPerspective.SelectedItem as TablePerspective;
                }
                else
                {
                    while (!IsHandleCreated) Thread.Sleep(100);
                    return Invoke((Func<object>)(() => cbxPerspective.SelectedItem)) as TablePerspective;
                }
            }
        }

        private DmlfHandler _GetDmlfHandler()
        {
            if (TabularData == null || TabularData.TableSource == null) return null;
            return new DmlfHandler { BaseTable = new DmlfSource { Alias = "basetbl", TableOrView = TabularData.TableSource.FullName } };
        }

        private DmlfHandler GetDmlfHandler()
        {
            if (SelectedPerspective != null)
            {
                return _GetDmlfHandler();
            }
            return null;
        }

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (TabularData != null && TabularData.TabDataCaps.Sorting && e.Button == MouseButtons.Left)
            {
                ResetPaging();
                SortOrder order = dataGridView1.Columns[e.ColumnIndex].HeaderCell.SortGlyphDirection;
                DataGridViewColumn scol = dataGridView1.Columns[e.ColumnIndex];
                //foreach (DataGridViewColumn col in dataGridView1.Columns)
                //{
                //    col.HeaderCell.SortGlyphDirection = SortOrder.None;
                //}
                if (order == SortOrder.Ascending) order = SortOrder.Descending;
                else order = SortOrder.Ascending;
                var disp = m_table.GetColumnDisplay();
                var dcol = disp[e.ColumnIndex];
                //var dialect = GetDialect();
                //string colsqlref = dcol.ValueRef.Expr.ToSql(dialect, GetDmlfHandler());
                //string neworder = colsqlref + " " + (order == SortOrder.Ascending ? "ASC" : "DESC");

                if (DataState.SortOrder == null)
                {
                    DataState.SortOrder = DmlfSortOrderCollection.BuildFromExpression(dcol.ValueRef.Expr);
                }
                else
                {
                    int existingindex = DataState.SortOrder.GetExpressionIndex(dcol.ValueRef.Expr);

                    if ((ModifierKeys & Keys.Control) == Keys.Control)
                    {
                        if (existingindex >= 0)
                        {
                            DataState.SortOrder[existingindex].OrderType = DataState.SortOrder[existingindex].OrderType.GetOpposite();
                        }
                        else
                        {
                            DataState.SortOrder.Add(new DmlfSortOrderItem
                            {
                                Expr = dcol.ValueRef.Expr,
                                OrderType = DmlfSortOrderType.Ascending
                            });
                        }
                    }
                    else
                    {
                        if (existingindex >= 0)
                        {
                            DataState.SortOrder[existingindex].OrderType = DataState.SortOrder[existingindex].OrderType.GetOpposite();
                        }
                        else
                        {
                            DataState.SortOrder = DmlfSortOrderCollection.BuildFromExpression(dcol.ValueRef.Expr);
                        }
                    }
                }

                LoadDataPage();
            }
        }

        private void dataGridView1_ColumnWidthChanged(object sender, DataGridViewColumnEventArgs e)
        {
            SaveToCache();
        }

        IEnumerable CountCacheKey()
        {
            List<object> key = new List<object>();
            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                key.Add(col.Name);
            }
            return key;
        }

        private void SaveToCache()
        {
            if (!dataGridView1.PersistentColWidths) return;
            DataTableCache cache = new DataTableCache();
            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                cache.ColumnWidths.Add(col.Width);
            }

            UICache.Put(CountCacheKey(), cache);
        }

        private void LoadFromCache()
        {
            if (!dataGridView1.PersistentColWidths) return;
            DataTableCache cache = UICache.Get<DataTableCache>(CountCacheKey());
            if (cache != null && cache.ColumnWidths.Count == dataGridView1.Columns.Count)
            {
                for (int i = 0; i < dataGridView1.Columns.Count; i++)
                {
                    dataGridView1.Columns[i].Width = cache.ColumnWidths[i];
                }
            }
        }

        public class DataTableCache
        {
            public List<int> ColumnWidths = new List<int>();
        }

        protected void StateLabelClick()
        {
            if (Detached)
            {
                if (AllowClose())
                {
                    Detached = false;

                    RefreshRowCount();
                    CallLoadFromDependencySource(m_proposedDependencySource);
                }
            }
        }

        private void stateLabel_Click(object sender, EventArgs e)
        {
            StateLabelClick();
        }

        //private void btnGoTo_Click(object sender, EventArgs e)
        //{
        //    m_mainMenu.GoToReference();
        //    cbhistory.Checked = true;
        //    toolStripHistory.Visible = true;
        //}

        private void btnGoBack_Click(object sender, EventArgs e)
        {
            CallGoBack();
        }

        internal void CallGoBack()
        {
            //m_statBackCount++;
            State.GoBack();
            RefreshCurrentView();
        }

        private void btnGoForward_Click(object sender, EventArgs e)
        {
            State.GoForward();
            RefreshCurrentView();
        }

        private void cbhistory_Click(object sender, EventArgs e)
        {
            toolStripHistory.Visible = cbhistory.Checked;
        }

        private void cbxHistory_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxHistory.SelectedIndex != State.HistoryPosition && cbxHistory.SelectedIndex >= 0)
            {
                State.HistoryPosition = cbxHistory.SelectedIndex;
                RefreshCurrentView();
            }
        }

        private void dataGridView1_GetLookupInfo(object sender, GetLookupEventArgs e)
        {
            if (m_listDrawers.ContainsKey(e.ColumnIndex) && m_listDrawers[e.ColumnIndex] != null)
            {
                e.LookupValue = m_listDrawers[e.ColumnIndex].GetValue(e.Value);
            }
            if (dataGridView1.ColumnDisplay != null)
            {
                var lcol = dataGridView1.ColumnDisplay[e.ColumnIndex].LookupRef;
                if (lcol != null)
                {
                    e.LookupValue = dataGridView1.GetRow(e.RowIndex)[dataGridView1.ColumnDisplay[e.ColumnIndex].LookupSourceIndex].SafeToString() ?? "";
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (m_wantRepaint)
            {
                m_wantRepaint = false;
                dataGridView1.Refresh();
            }
        }

        private IDatabaseSource Database
        {
            get
            {
                try { return TabularData.TableSource.Database; }
                catch { return null; }
            }
        }

        public void CancelLoading()
        {
            try
            {
                ((ICancelable)m_loadingData).Cancel();
            }
            catch (Exception e)
            {
                Logging.Warning("Error canceling load data:" + e.ToString());
            }
        }

        private void btnCancelLoading_Click(object sender, EventArgs e)
        {
            CancelLoading();
        }

        public override Bitmap Image
        {
            get { return CoreIcons.table_data; }
        }

        private void tbsearch_Click(object sender, EventArgs e)
        {
            DataState.SearchText = tbxSearch.Text;
            // hack for Windows 7
            tbxSearch.Visible = false;
            tbxSearch.Visible = true;
            tbxSearch.Focus();
        }

        private void tbxFilter_Click(object sender, EventArgs e)
        {
            DataState.Filter = tbxFilter.Text;
        }

        private void nbstart_Click(object sender, EventArgs e)
        {
            ChangedPaging();
        }

        private void nbcount_Click(object sender, EventArgs e)
        {
            ChangedPaging();
        }

        internal virtual void ResetView()
        {
            if (MessageBox.Show(Texts.Get("s_really_reset_view"), VersionInfo.ProgramTitle, MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes) return;
            if (!AllowClose()) return;
            ITabularDataView data = TabularData;
            State = new TableRelatedState();
            DataStatePer.TableData = data;
            ReloadPerspectives();
            RefreshCurrentView();
        }

        internal void SoftResetView()
        {
            DataStatePer.SelectPerspective(null);
            DataState.SortOrder = null;
            DataState.SearchText = "";
            DataState.Filter = "";
            RefreshCurrentView();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            ResetView();
        }

        private void dataGridView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                dataGridView1.HighlightColumn = -1;
                dataGridView1.HighlightRow = -1;
                var hinfo = dataGridView1.HitTest(e.X, e.Y);
                switch (hinfo.Type)
                {
                    case DataGridViewHitTestType.Cell:
                        {
                            var cell = dataGridView1.Rows[hinfo.RowIndex].Cells[hinfo.ColumnIndex];
                            if (cell.IsInEditMode) return;
                            if (!cell.Selected)
                            {
                                dataGridView1.CurrentCell = cell;
                            }
                        }
                        break;
                    case DataGridViewHitTestType.ColumnHeader:
                        dataGridView1.HighlightColumn = hinfo.ColumnIndex;
                        dataGridView1.HightlightVisible = true;
                        dataGridView1.InvalidateColumn(hinfo.ColumnIndex);
                        //dataGridView1.InvalidateCell(hinfo.ColumnIndex, hinfo.RowIndex);
                        break;
                    case DataGridViewHitTestType.RowHeader:
                        dataGridView1.HighlightRow = hinfo.RowIndex;
                        dataGridView1.HightlightVisible = true;
                        dataGridView1.InvalidateRow(dataGridView1.HighlightRow);
                        //dataGridView1.InvalidateCell(hinfo.ColumnIndex, hinfo.RowIndex);
                        break;
                }
                FillPopupMenu();
                mnuGrid.Show(dataGridView1, new Point(e.X, e.Y));
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveChanges();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            DoRefreshData();
        }

        public void Find()
        {
            tbxSearch.Focus();
        }

        public override void GetMenu(MenuBuilder bld)
        {
            bld.AddObject(m_mainMenu);
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            DoExport();
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                RefreshData();
                e.Handled = true;
            }
            if (e.KeyCode == Keys.Enter && e.Control)
            {
                ShowCellDataDefault();
                e.Handled = true;
            }
            if (e.KeyCode == Keys.Enter && !IsReadOnly && dataGridView1.CurrentCell != null && !e.Handled)
            {
                dataGridView1.BeginEdit(true);
                e.Handled = true;
            }
            if (e.Handled) return;

            var mb = new MenuBuilder();
            mb.AddObject(this);
            mb.ProcessKeyDown(e.KeyCode | e.Modifiers);
            if (dataGridView1.EditingControl == null)
            {
                if (e.Control && e.KeyCode == Keys.V || e.Shift && e.KeyCode == Keys.Insert)
                {
                    PasteMultiCell();
                }
            }
        }

        private void btnRemoveRow_Click(object sender, EventArgs e)
        {
            DeleteRow();
        }

        private void btnAddRow_Click(object sender, EventArgs e)
        {
            InsertRow();
        }

        internal void ClosedReference(ReferencesTableDataFrame frame)
        {
            //DataState.RefGrids.Remove(frame);
        }

        protected override void OnRemovedDockingLayer()
        {
            DataState.DockPanelFrame = null;
        }

        private void SetReferencesEnabled(bool value)
        {
            btnReferences.Enabled = value && MasterDetailViewsFeature.Allowed;
            btnGoTo.Enabled = value && MasterDetailViewsFeature.Allowed;
            SetPerspectivesEnabled(value);
            btnRelated.Visible = false;
            if (value && !HideToolbars && MasterDetailViewsFeature.Allowed)
            {
                // handle "Related" menu in statusbar
                var ts = CPeekStructure();
                if (ts != null)
                {
                    var detail = ts.GetReferencedFrom();
                    var master = new List<IForeignKey>(ts.GetConstraints<IForeignKey>());
                    detail.SortByKey(fk => FormatRelatedTableName(fk, false));
                    master.SortByKey(fk => FormatRelatedTableName(fk, true));
                    btnRelated.Visible = master.Count > 0 || detail.Count > 0;
                    if (btnRelated.Visible && !m_openedRelated)
                    {
                        btnRelated.Text = String.Format("{0} ({1}/{2})", Texts.Get("s_related"), detail.Count, master.Count);
                        btnRelated.DropDownItems.Clear();
                        foreach (var fk in detail)
                        {
                            AddRelatedView(fk, false);
                        }
                        btnRelated.DropDownItems.Add(new ToolStripSeparator());
                        foreach (var fk in master)
                        {
                            AddRelatedView(fk, true);
                        }
                    }
                }
            }
        }

        private string FormatRelatedTableName(IForeignKey fk, bool master)
        {
            var tblname = master ? fk.PrimaryKeyTable.ToString() : fk.Table.FullName.ToString();
            return String.Format("{0} - {1} ({2})", tblname, fk.Name, (from c in fk.Columns select c.ColumnName).CreateDelimitedText(" ,"));
        }

        private void AddRelatedView(IForeignKey fk, bool master)
        {
            ReferenceViewDefinition refdef;
            if (master) refdef = SelectReferenceForm.CreateMasterReference(fk, TabularData.Dialect);
            else refdef = SelectReferenceForm.CreateDetailReference(fk, TabularData.Dialect);
            //var tblname = fk.PrimaryKeyTable == ts.FullName ? fk.Table.FullName.ToString() : fk.PrimaryKeyTable.ToString();
            string text = FormatRelatedTableName(fk, master);
            var item = btnRelated.DropDownItems.Add(text);
            item.Tag = refdef;
            item.Click += new EventHandler(item_Click);
        }

        void item_Click(object sender, EventArgs e)
        {
            var item = (ToolStripItem)sender;
            var refdef = (ReferenceViewDefinition)item.Tag;

            if (ParentFrame != null) DoCallShowReferencesCore(SelectedPerspective, refdef);
            else MakePerspectiveAction(per => DoCallShowReferencesCore(per, refdef));
        }

        private void SetPerspectivesEnabled(bool value)
        {
            cbxPerspective.Enabled = value && !DataStatePer.IsFixedPerspective;
            btnAddPerspective.Enabled = value && !DataStatePer.IsFixedPerspective;
            btnChooseColumns.Enabled = value && TabularData != null && TabularData.TabDataCaps.Perspectives;
            var per = cbxPerspective.SelectedItem as TablePerspective;
            btnDesignPerspective.Enabled = value && per != null && per.FileName != null;
            btnRemovePerspective.Enabled = value && per != null && !DataStatePer.IsFixedPerspective;
            btnSavePerspective.Enabled = value && per != null;
        }

        private void SetPerspectivesEnabled()
        {
            bool enabled = TabularData != null && m_dispModel != null;
            if (m_missingReloadPerspectives) enabled = false;
            SetPerspectivesEnabled(enabled);
        }

        private ReferenceViewDefinition AskRefDef()
        {
            var ts = CGetStructure(null);
            if (ts == null) return null;
            if (dataGridView1.CurrentCell == null) return null;
            int colindex = dataGridView1.CurrentCell.ColumnIndex;
            string colname = dataGridView1.Columns[colindex].Name;
            List<string> availcols = null;
            if (m_table.ResultFields != null) availcols = m_table.ResultFields.GetBaseColumns();
            var refdef = SelectReferenceForm.Run(ts, colname, availcols, TabularData.Connection.Dialect);
            return refdef;
        }

        private void btnReferences_Click(object sender, EventArgs e)
        {
            CallShowReferences();
        }

        [PopupMenuVisible("s_show_referenced_data")]
        public bool CallShowReferencesVisible()
        {
            return TabularData != null && !HideToolbars;
        }

        [PopupMenu("s_show_referenced_data", GroupName = "column", ImageName = CoreIcons.referencesName, RequiredFeature = MasterDetailViewsFeature.Test)]
        public void CallShowReferences()
        {
            if (ParentFrame != null) DoCallShowReferences(SelectedPerspective);
            else MakePerspectiveAction(DoCallShowReferences);
        }

        private bool DoCallShowReferencesCore(TablePerspective per, ReferenceViewDefinition refdef)
        {
            if (refdef == null) return false;
            var reftable = new ReferencesTableDataFrame(this, refdef, dataGridView1.GetPopupRow());
            if (ParentFrame != null) reftable.RefreshCurrentView();
            //DataState.RefGrids.Add(reftable);
            if (ParentFrame != null) ParentFrame.OpenContent(reftable, this, DocumentDockPosition.Bottom);
            else OpenDetailInNewDock(reftable, DocumentDockPosition.Bottom);
            var parent = ParentFrame;
            Usage.AddSub("showrefs");
            DataStatePer.ModifyDataStatePer(per, st =>
            {
                st.DockPanelFrame = parent;
            });
            return true;
        }

        private bool DoCallShowReferences(TablePerspective per)
        {
            var refdef = AskRefDef();
            return DoCallShowReferencesCore(per, refdef);
        }

        private void btnGoTo_Click(object sender, EventArgs e)
        {
            CallGoToReference();
        }

        internal void CallGoToReference()
        {
            var refdef = AskRefDef();
            if (refdef == null) return;
            State.GoToReference(refdef, dataGridView1.GetPopupRow());
            //m_statGoToCount++;
            toolStripHistory.Visible = cbhistory.Checked = true;
            RefreshCurrentView();
        }

        private void nbstart_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) btnLoadPage_Click(sender, e);
        }

        private void nbcount_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) btnLoadPage_Click(sender, e);
        }

        //private void btnGenerateSelect_Click(object sender, EventArgs e)
        //{
        //    GenerateAllSelect();
        //}

        //private void btnGenerateDelete_Click(object sender, EventArgs e)
        //{
        //    GenerateAllDelete();
        //}

        //private void sbackupselectedcellsToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    GenerateSql(DoBackupSelectedCells);
        //}

        public void PasteMultiCell()
        {
            string data = Clipboard.GetText();
            if (String.IsNullOrEmpty(data)) return;
            if (dataGridView1.CurrentCell == null) return;

            string[] lines = data.Split('\n');

            int row = dataGridView1.CurrentCell.RowIndex, col = dataGridView1.CurrentCell.ColumnIndex;
            if (dataGridView1.SelectedCells.Count > 1)
            {
                row = -1; col = -1;
                foreach (DataGridViewCell cell in dataGridView1.SelectedCells)
                {
                    if (row < 0 || cell.RowIndex < row) row = cell.RowIndex;
                    if (col < 0 || cell.ColumnIndex < col) col = cell.ColumnIndex;
                }
            }

            for (int rowi = 0; rowi < lines.Length && row + rowi < dataGridView1.RowCount; rowi++)
            {
                string[] cells = lines[rowi].Split('\t');
                for (int coli = 0; coli < cells.Length && col + coli < dataGridView1.ColumnCount; coli++)
                {
                    if (cells[coli].IsEmpty()) continue;
                    if (dataGridView1.SelectedCells.Count > 1 && !dataGridView1.Rows[row + rowi].Cells[col + coli].Selected)
                    {
                        continue;
                    }
                    var rowobj = dataGridView1.GetRow(row + rowi);
                    rowobj[col + coli] = cells[coli].TrimEnd();
                }
            }
            dataGridView1.Refresh();
        }

        private void sToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoInsertRow(0);
        }

        private void sinsertrowafterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DoInsertRow(1);
        }

        private void sappendmorerowsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string cnt = InputBox.Run("s_type_inserted_row_count", "1");
            if (cnt != null)
            {
                DoAddRows(Int32.Parse(cnt));
            }
        }

        private void btnDuplicateRow_Click(object sender, EventArgs e)
        {
            DuplicateRow();
        }

        private GridDataHolder GetCurrentDataHolder()
        {
            if (dataGridView1.CurrentCell == null) return null;
            BedRow row = dataGridView1.GetCurrentRow();
            //object value = row[dataGridView1.CurrentCell.ColumnIndex];
            return new GridDataHolder(row, dataGridView1.CurrentCell.ColumnIndex, dataGridView1.CurrentCell.RowIndex, dataGridView1, GetLookupInfoForCol(dataGridView1.CurrentCell.ColumnIndex), m_fmtSettings);
        }

        protected void UpdateCellDataView()
        {
            BedRow row = dataGridView1.GetCurrentRow();
            if (row == null || (!dataGridView1.Focused && !m_surelyFocused)) HCellData.CallInvalidateData(this);
            else HCellData.CallShowData(this, GetCurrentDataHolder());
        }

        public override void OnShowContent()
        {
            base.OnShowContent();
            UpdateCellDataView();
        }

        public override void OnHideContent()
        {
            base.OnHideContent();
            HCellData.CallInvalidateData(this);
        }

        public TableDataGrid DataGrid { get { return dataGridView1; } }

        private void btnPerspective_Click(object sender, EventArgs e)
        {
            toolStripPerspective.Visible = btnPerspective.Checked;
            if (cbpaging.Checked) toolStripMain.SendToBack();
        }

        private void btnManagePerspectives_Click(object sender, EventArgs e)
        {
            TablePerspectiveManager.RunManageDialog();
            TablePerspectiveManager.ClearCache();
            ReloadPerspectives();
            RefreshCurrentView();
        }

        protected void ReloadPerspectives()
        {
            ReloadPerspectives(DataState.Perspective);
        }

        int m_ignoreChangePerspective;
        private void ReloadPerspectives(TablePerspective selected)
        {
            cbxPerspective.Items.Clear();
            if (TabularData == null) return;
            if (DataStatePer.IsFixedPerspective)
            {
                cbxPerspective.Items.Add(DataStatePer.Perspective);
                cbxPerspective.SelectedIndex = 0;
                return;
            }

            var tbl = GetTableStructure();
            if (tbl == null)
            {
                m_missingReloadPerspectives = true;
                SetPerspectivesEnabled();
                return;
            }

            m_missingReloadPerspectives = false;
            m_ignoreChangePerspective++;
            try
            {
                cbxPerspective.Items.Add("(" + Texts.Get("s_default") + ")");

                foreach (var per in DataStatePer.InMemoryPerspectives)
                {
                    cbxPerspective.Items.Add(per);
                }

                if (AdvancedPerspectivesFeature.Allowed)
                {
                    foreach (var per in TablePerspectiveManager.GetPerspectives(
                        TabularData.Connection,
                        TabularData.DatabaseSource != null ? TabularData.DatabaseSource.DatabaseName : null,
                        TabularData.TableSource != null ? TabularData.TableSource.FullName : null,
                        tbl.Columns.GetNames()))
                    {
                        cbxPerspective.Items.Add(per);
                    }
                }

                _SelectPerspective(selected);
            }
            finally
            {
                m_ignoreChangePerspective--;
            }
            SetPerspectivesEnabled();
        }

        private void _SelectPerspective(TablePerspective selected)
        {
            bool isselected = false;
            if (selected != null)
            {
                for (int index = 1; index < cbxPerspective.Items.Count; index++)
                {
                    var per = (TablePerspective)cbxPerspective.Items[index];
                    if ((per.FileName != null && per.FileName == selected.FileName) || per == selected)
                    {
                        cbxPerspective.SelectedIndex = index;
                        DataStatePer.SelectPerspective(per);
                        isselected = true;
                        break;
                    }
                }
            }
            if (!isselected)
            {
                cbxPerspective.SelectedIndex = cbxPerspective.Items.Count > 0 ? 0 : -1;
                DataStatePer.SelectPerspective(null);
            }
        }

        private void RefreshCurrentPerspective()
        {
            if (DataStatePer.IsFixedPerspective) return;
            m_ignoreChangePerspective++;
            try
            {
                _SelectPerspective(DataStatePer.Perspective);
            }
            finally
            {
                m_ignoreChangePerspective--;
            }
        }

        private void FillPerspectiveFromCurrent(TablePerspective per)
        {
            var cur = SelectedPerspective;
            if (cur == null)
            {
                var ts = TabularData.GetStructure(null);
                var pk = ts.FindConstraint<IPrimaryKey>();
                var pkcols = new HashSetEx<string>();
                if (pk != null) pkcols.AddRange(pk.Columns.GetNames());
                foreach (var col in ts.Columns)
                {
                    var dcol = DmlfResultField.BuildFromColumn(col.ColumnName);
                    if (pkcols.Contains(col.ColumnName)) dcol.DisplayInfo.IsPrimaryKey = true;
                    per.Select.Columns.Add(dcol);
                }
                per.Select.Columns.NormalizeBaseTables();
            }
            else
            {
                var doc = XmlTool.CreateDocument("Perspective");
                cur.SaveToXml(doc.DocumentElement);
                per.LoadParts(doc.DocumentElement, true, false, true);
            }
        }

        int m_lastper = 0;
        private TablePerspective CreateInMemoryPerspective()
        {
            if (!AllowClose()) return null;
            string pname = String.Format("{0} {1}", Texts.Get("s_perspective"), ++m_lastper);
            var per = new TablePerspective { InMemoryName = pname };
            FillPerspectiveFromCurrent(per);
            DataStatePer.InMemoryPerspectives.Add(per);
            string filter = DataState.Filter;
            string search = DataState.SearchText;
            string[] searchcols = DataState.SearchColumns;
            bool exmatch = DataState.SearchExactMatch;
            int curcol = DataState.CurrentCol;
            int currow = DataState.CurrentRow;
            bool savedcur = DataState.SavedCurrentCell;
            string savedpkcol = DataState.SavedPkCol;
            object savedpkvalue = DataState.SavedPkValue;
            var order = DataState.SortOrder;
            DataStatePer.ModifyDataStatePer(per, st =>
            {
                st.Filter = filter;
                st.SearchColumns = searchcols;
                st.SearchText = search;
                st.SearchExactMatch = exmatch;
                st.CurrentCol = curcol;
                st.CurrentRow = currow;
                st.SavedPkCol = savedpkcol;
                st.SavedPkValue = savedpkvalue;
                st.SortOrder = order;
                st.SavedCurrentCell = savedcur;
            });
            return per;
        }

        private string GetNewPerspectiveFile()
        {
            string pname = InputBox.Run("s_input_perspective_name", "perspective");
            if (pname == null) return null;
            string fn = Path.Combine(Core.TablePerspectivesDirectory, pname + ".per");
            if (File.Exists(fn))
            {
                if (!StdDialog.ReallyOverwriteFile(fn)) return null;
            }
            return fn;
        }

        private void AddPerspective(bool reuseIfMemory)
        {
            try
            {
                string fn = GetNewPerspectiveFile();
                if (fn == null) return;

                var cur = SelectedPerspective;
                TablePerspective per;
                if (cur != null && reuseIfMemory && cur.FileName == null)
                {
                    per = cur;
                    per.InMemoryName = null;
                    DataStatePer.InMemoryPerspectives.Remove(per);
                    per.DockPanelDesign = SaveDockPanel();
                }
                else
                {
                    per = new TablePerspective();
                    FillPerspectiveFromCurrent(per);
                }
                per.Conditions.DatabaseName.PredefinedValue = TabularData.DatabaseSource.DatabaseName;
                per.Conditions.DatabaseName.Enabled = false;
                per.Conditions.TableName.PredefinedValue = TabularData.TableSource.FullName.Name;
                per.Conditions.TableName.Enabled = true;
                per.Conditions.TableSchema.PredefinedValue = TabularData.TableSource.FullName.Schema;
                per.Conditions.TableSchema.Enabled = false;
                per.Conditions.Columns.PredefinedValue = TabularData.GetStructure(null).Columns.GetNames();
                per.Conditions.Columns.Enabled = true;

                per.FileName = fn;
                per.SaveToFile();
                TablePerspectiveManager.ClearCache();
                ReloadPerspectives(per);
            }
            catch
            {
                // perspective cannot be created
            }
        }

        private void btnAddPerspective_Click(object sender, EventArgs e)
        {
            AddPerspective(false);
        }

        private void smanageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TemplateQuickExport.RunManageDialog();
        }

        private void btnDesignPerspective_Click(object sender, EventArgs e)
        {
            if (cbxPerspective.SelectedItem != null)
            {
                var per = cbxPerspective.SelectedItem as TablePerspective;
                if (per == null) return;
                var tbl = GetTableStructure();
                if (tbl == null) return;
                TablePerspectiveEditorForm.Run(per, tbl, TabularData.DatabaseSource);
                TablePerspectiveManager.ClearCache();
                ReloadPerspectives();
                LoadDataPage();
            }
        }

        private void btnRemovePerspective_Click(object sender, EventArgs e)
        {
            var per = SelectedPerspective;
            if (per != null)
            {
                if (per.FileName != null)
                {
                    if (StdDialog.ReallyDeleteFile(per.FileName))
                    {
                        File.Delete(per.FileName);
                        TablePerspectiveManager.ClearCache();
                    }
                }
                if (per.InMemoryName != null)
                {
                    DataStatePer.InMemoryPerspectives.Remove(per);
                }
                ReloadPerspectives();
                RefreshCurrentView();
            }
        }

        public void RevertChanges()
        {
            if (dataGridView1.DataSource == null) return;
            if (MessageBox.Show(Texts.Get("s_revert_all_changes_q"), VersionInfo.ProgramTitle, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                dataGridView1.RevertAllChanges();
            }
        }

        private void btnRevertChanges_Click(object sender, EventArgs e)
        {
            RevertChanges();
        }

        private void btnAddToFavorites_Click(object sender, EventArgs e)
        {
            if (TabularData == null || !TabularData.SupportsSerialize) return;
            var doc = XmlTool.CreateDocument("TableData");
            TabularData.SaveToXml(doc.DocumentElement);
            //foreach (var refgrid in DataState.RefGrids)
            //{
            //    refgrid.SaveReferenceXml(doc.DocumentElement.AddChild("Reference"));
            //}
            AddToFavoriteForm.Run(new TableDataFavorite { SerializedState = doc.DocumentElement }, TabularData.ToString("S"));
        }

        public TableDataFrame(XmlElement xml)
            : this()
        {
            var loader = (ITabularDataViewLoader)TabularDataViewLoaderAddonType.Instance.LoadAddon(xml);
            TabularData = loader.CreateTabularDataView();
            UpdateState();

            //LoadRefererences(xml);
        }

        private void cbxPerspective_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DataStatePer.IsFixedPerspective) return;
            if (m_ignoreChangePerspective == 0 && !AllowClose())
            {
                try
                {
                    m_ignoreChangePerspective++;
                    _SelectPerspective(DataStatePer.Perspective);
                }
                finally
                {
                    m_ignoreChangePerspective--;
                }
                return;
            }
            SetPerspectivesEnabled();
            if (m_ignoreChangePerspective > 0) return;
            var oldper = DataState.Perspective;
            DataStatePer.SelectPerspective(cbxPerspective.SelectedItem as TablePerspective);
            if (oldper != DataState.Perspective)
            {
                //if (DataState.Perspective != null && DataState.Perspective.DockPanelDesign != null)
                //{
                //    m_loadDockPanelDesign = DataState.Perspective.DockPanelDesign;
                //}
                RefreshCurrentView();
            }
        }

        private void btnChooseColumns_Click(object sender, EventArgs e)
        {
            MakePerspectiveAction(DoChooseColumns);
        }

        private bool DoChooseColumns(TablePerspective per)
        {
            var avail = new List<DmlfExpression>();
            var handler = _GetDmlfHandler();
            if (handler == null) return false;
            foreach (var src in per.Select.From.GetAllSources())
            {
                ITableStructure ts;
                if (src.TableOrView != null)
                {
                    ts = CGetStructure(src.TableOrView, null);
                    handler.Tables[src.TableOrView] = ts;
                }
                else
                {
                    ts = CGetStructure(null);
                    handler.BaseStructure = ts;
                }
                if (ts == null) continue;
                foreach (string colname in ts.Columns.GetNames())
                {
                    avail.Add(new DmlfColumnRefExpression
                    {
                        Column = new DmlfColumnRef
                        {
                            Source = src,
                            ColumnName = colname
                        }
                    });
                }
            }
            foreach (var r in per.Select.Columns)
            {
                if (!avail.Contains(r.Expr)) avail.Add(r.Expr);
            }
            var cols = ChooseVisibleColumnsForm.Run(per.Select.Columns, avail);
            if (cols == null) return false;
            per.Select.Columns = cols;
            per.Select.CompleteUpdatingInfo(handler);
            return true;
        }

        public TableDataGrid Grid { get { return dataGridView1; } }

        private void mnuCustomSqlGenerator_Click(object sender, EventArgs e)
        {
            if (TabularData == null) return;
            GenerateSqlForm.Run(this);
        }

        public string[] GetSelectedColumns()
        {
            var res = new List<string>();
            foreach (DataGridViewCell cell in dataGridView1.SelectedCells)
            {
                string colname = m_table.Structure.Columns[cell.ColumnIndex].ColumnName;
                if (!res.Contains(colname)) res.Add(colname);
            }
            return res.ToArray();
        }

        public BedTable GetSelectedTable()
        {
            if (m_table == null)
            {
                return new BedTable(new TableStructure());
            }
            var usedRows = new HashSetEx<int>();
            var res = new BedTable(m_table.Structure);
            foreach (DataGridViewCell cell in dataGridView1.SelectedCells)
            {
                usedRows.Add(cell.RowIndex);
            }
            var irows = new List<int>(usedRows);
            irows.Sort();
            foreach (int rowindex in irows)
            {
                res.AddRow(m_table.Rows[rowindex]);
            }
            return res;
        }

        public int GetSelectedRowCount()
        {
            return GetSelectedTable().Rows.Count;
        }

        public List<IDataSqlGenerator> GetSqlGens()
        {
            var res = new List<IDataSqlGenerator>();
            res.Add(new DataFrameSelectSqlGenerator(this));
            res.Add(new DataFrameSaveChangesSqlGenerator(this));
            return res;
        }

        private void mnuGrid_Closed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            dataGridView1.UnhighlightHeaders();
        }

        private void btnSavePerspective_Click(object sender, EventArgs e)
        {
            Usage.AddSub("save_perspective");
            var cur = SelectedPerspective;
            if (cur != null && cur.FileName != null)
            {
                if (DataState.DockPanelFrame != null) cur.DockPanelDesign = SaveDockPanel();
                cur.SaveToFile();
            }
            else
            {
                AddPerspective(true);
            }
        }

        // this prevents hanging-up !!!
        bool m_surelyFocused;
        private void dataGridView1_Enter(object sender, EventArgs e)
        {
            m_surelyFocused = true;
            UpdateCellDataView();
            m_surelyFocused = false;
        }

        private void dataGridView1_CellValuePushed(object sender, DataGridViewCellValueEventArgs e)
        {
            UpdateCellDataView();
        }

        private void btnOpenForEditing_Click(object sender, EventArgs e)
        {
            if (TabularData == null || TabularData.TableSource == null) return;
            var appobj = new TableAppObject();
            appobj.FillFromTable(TabularData.TableSource);
            appobj.ConnPack = new ConnectionPack(this);
            appobj.OpenData();
        }

        ChooseSearchColumnsFrame m_chooseSearchColumnsFrame;
        private void btnChooseSearchColumns_Click(object sender, EventArgs e)
        {
            if (m_chooseSearchColumnsFrame != null)
            {
                m_chooseSearchColumnsFrame.ProcessOk();
                return;
            }
            if (m_table == null) return;
            var disp = m_table.GetColumnDisplay();
            if (disp == null) return;
            var win = new ChooseSearchColumnsFrame();
            Controls.Add(win);
            win.FillColumns(disp, DataState.SearchColumns, DataState.SearchExactMatch);
            var r = btnChooseSearchColumns.Bounds;
            win.ControlClosed += colswin_ControlClosed;
            win.Left = r.Right - win.Width;
            win.Top = r.Bottom;
            if (win.Bottom > Height - statusStrip1.Height) win.Height -= win.Bottom - Height + statusStrip1.Height;
            win.Show();
            win.BringToFront();
            win.Focus();
            m_chooseSearchColumnsFrame = win;
        }

        void colswin_ControlClosed(object sender, EventArgs e)
        {
            var win = (ChooseSearchColumnsFrame)sender;
            if (win.ClosedOk)
            {
                DataState.SearchColumns = win.GetCheckedColumns();
                DataState.SearchExactMatch = win.GetExactMatch();
                ApplyFilter();
            }
            m_chooseSearchColumnsFrame = null;
        }

        private void btnRelated_DropDownOpened(object sender, EventArgs e)
        {
            m_openedRelated = true;
        }

        private void btnRelated_DropDownClosed(object sender, EventArgs e)
        {
            m_openedRelated = false;
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;
            ShowCellDataDefault();
        }

        [PopupMenu("s_show_cell_data/s_autodetect_format", ShortcutDisplayString = "Ctrl+Enter", GroupName = "editor", ImageName = CoreIcons.autodetectName, Weight = -1)]
        public void ShowCellDataDefault()
        {
            var data = GetCurrentDataHolder();
            if (data == null) return;

            var dataHolder = new BedValueHolder();
            data.GetData(dataHolder);

            int maxlevel = 0;
            ICellDataEditor editor = null;
            foreach (var holder in CellDataEditorAddonType.Instance.CommonSpace.GetAllAddons())
            {
                var celli = (ICellDataEditor)holder.CreateInstance();
                int level = celli.SupportLevel(data, dataHolder);
                if (level > maxlevel)
                {
                    maxlevel = level;
                    editor = celli;
                }
            }
            if (editor != null)
            {
                var pars = new ShowDockerPars { ModalLikeMode = true, ModalParent = this };
                MainWindow.Instance.ShowDocker(new CellDataDockerFactory(editor), pars);
                HCellData.CallShowData(this, data);
            }
        }

        private void dataGridView1_ZoomChanged(object sender, EventArgs e)
        {
            btnZoom.Text = dataGridView1.ZoomName;
        }

        private void btnChart_Click(object sender, EventArgs e)
        {
            var frame = m_charting.CreateFrame(TabularData.CloneView());
            MainWindow.Instance.OpenContent(frame);
        }

        private void btnFirstPage_Click(object sender, EventArgs e)
        {
            nbstart.Text = "0";
            ChangedPaging();
            LoadDataPage();
        }

        private void btnLastPage_Click(object sender, EventArgs e)
        {
            if (m_rowCount == null) return;

            int page = m_rowCount.Value / Int32.Parse(nbcount.Text);
            nbstart.Text = (page * Int32.Parse(nbcount.Text)).ToString();
            ChangedPaging();
            LoadDataPage();
        }
    }
}

