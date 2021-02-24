using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DatAdmin;
using System.IO;

namespace Plugin.datasyn
{
    public partial class DataSynForm : FormEx
    {
        DataSynDef m_datasyn;
        IDatabaseSource m_source;
        IDatabaseSource m_target;

        DatabaseStructure m_srcModel;
        DatabaseStructure m_dstModel;

        JobConnection m_jobconn;
        DataSynJobCommand m_cmd;
        ImageCache m_imgCache;

        Dictionary<NameWithSchema, SynItem> m_items = new Dictionary<NameWithSchema, SynItem>();

        public DataSynForm(DataSynDef datasyn, IDatabaseSource src, IDatabaseSource dst)
        {
            Initialize();
            m_datasyn = datasyn;
            SetSource(src);
            SetTarget(dst);
            DownloadWhenNeededAndRefresh();
            Disposed += new EventHandler(DataSynForm_Disposed);
            OnlineHelpManager.RegisterHelpButton(btnOnlineHelp, "datasyn", true);
            UpdateEnabling();
            m_imgCache = new ImageCache(imageList1, Color.White);

            colSource.ImageGetter = row =>
            {
                if (row is ItemWrapper)
                {
                    var ds = ((ItemWrapper)row).m_item;
                    if (ds.Source is DataSynTableSource) return m_imgCache.GetImageIndex(CoreIcons.table);
                    if (ds.Source is DataSynViewSource) return m_imgCache.GetImageIndex(CoreIcons.view);
                    if (ds.Source is DataSynQuerySource) return m_imgCache.GetImageIndex(CoreIcons.sql);
                    return -1;
                }
                return m_imgCache.GetImageIndex(CoreIcons.cancel);
            };
            colTarget.ImageIndex = m_imgCache.GetImageIndex(CoreIcons.table);
            colState.ImageGetter = row =>
            {
                var wrap = row as ItemWrapper;
                if (wrap != null && wrap.StateImage != null) return m_imgCache.GetImageIndex(wrap.StateImage);
                return -1;
            };
        }

        private void ClearItems()
        {
            foreach (var item in m_items.Values) item.Dispose();
            m_items.Clear();
        }

        private void UpdateEnabling()
        {
            int itidx = -1;
            if (SelectedItem != null) itidx = m_datasyn.Items.IndexOf(SelectedItem);
            btnMoveUp.Enabled = itidx > 0;
            btnMoveDown.Enabled = itidx >= 0 && itidx < m_datasyn.Items.Count - 1;
            UpdateSynCount();
        }

        private bool DbSuitable(IDatabaseSource db)
        {
            if (db == null) return false;
            if (!db.DatabaseCaps.ExecuteSql)
            {
                StdDialog.ShowError("DAE-00368 " + Texts.Get("s_only_sql_databasources_can_be_sync"));
                return false;
            }
            Errors.CheckNotNull("DAE-00366", db.Dialect);
            if (db.Dialect.CreateDataSynAdapter() == null)
            {
                StdDialog.ShowError("DAE-00367 " + Texts.Get("s_dialect_doesnt_support_sync$dialect", "dialect", db.Dialect.DisplayName));
                return false;
            }
            return true;
        }

        private bool SetTarget(IDatabaseSource dst)
        {
            if (!DbSuitable(dst)) return false;
            m_target = dst;
            ClearItems();
            return true;
        }

        private bool SetSource(IDatabaseSource src)
        {
            if (!DbSuitable(src)) return false;
            m_source = src;
            ClearItems();
            return true;
        }


        public DataSynForm(JobConnection jobconn, DataSynJobCommand cmd)
        {
            Initialize();
            Disposed += new EventHandler(DataSynForm_Disposed);
            LoadFromJob(jobconn, cmd);
        }

        private void LoadFromJob(JobConnection jobconn, DataSynJobCommand cmd)
        {
            m_datasyn = cmd.DataSyn;
            m_source = cmd.Source;
            m_target = cmd.Target;
            m_jobconn = jobconn;
            m_cmd = cmd;
            m_srcModel = null;
            m_dstModel = null;
            DownloadWhenNeededAndRefresh();
        }

        private void Initialize()
        {
            InitializeComponent();
            dataSynDefItemFrame1.MainForm = this;
        }

        public DatabaseStructure SourceModel { get { return m_srcModel; } }
        public DatabaseStructure TargetModel { get { return m_dstModel; } }
        public IDatabaseSource SourceDb { get { return m_source; } }
        public IDatabaseSource TargetDb { get { return m_target; } }

        void DownloadWhenNeededAndRefresh()
        {
            using (WaitContext wc = new WaitContext())
            {
                if (m_source != null) Async.SafeOpen(m_source.Connection);
                if (m_target != null) Async.SafeOpen(m_target.Connection);
                if (m_source != null && m_srcModel == null)
                {
                    ReloadSourceModel();
                    if (m_srcModel != null) m_datasyn.NotifyChangeSourceModel(m_srcModel);
                }
                if (m_target != null && m_dstModel == null)
                {
                    ReloadTargetModel();
                    if (m_dstModel != null) m_datasyn.NotifyChangeTargetModel(m_dstModel);
                }
            }

            if (m_source != null)
            {
                labSourceServer.Text = m_source.Connection.PhysicalFactory.GetDataSource();
                labSourceDb.Text = m_source.DatabaseName ?? "";
            }
            else
            {
                labSourceServer.Text = "";
                labSourceDb.Text = "";
            }
            UpdateSourceLeft();

            if (m_target != null)
            {
                labTargetServer.Text = m_target.Connection.PhysicalFactory.GetDataSource();
                labTargetDb.Text = m_target.DatabaseName ?? "";
            }
            else
            {
                labTargetServer.Text = "";
                labTargetDb.Text = "";
            }

            RefreshData();
        }

        private void UpdateSourceLeft()
        {
            labSourceServer.Left = labSourceServer.Parent.Width - labSourceServer.Width;
            labSourceDb.Left = labSourceDb.Parent.Width - labSourceDb.Width;
        }

        void DataSynForm_Disposed(object sender, EventArgs e)
        {
            CloseConnections();
        }

        void CloseConnections()
        {
            ClearItems();
            if (m_source != null) Async.SafeClose(m_source.Connection);
            if (m_target != null) Async.SafeClose(m_target.Connection);
            m_source = null;
            m_target = null;
        }

        public static void Run(DataSynDef datasyn, IDatabaseSource src, IDatabaseSource dst)
        {
            var win = new DataSynForm(datasyn, src, dst);
            win.Show();
        }

        public static void Run(JobConnection jobconn, DataSynJobCommand cmd)
        {
            var win = new DataSynForm(jobconn, cmd);
            win.Show();
        }

        private void ReloadTargetModel()
        {
            var dbmem = new DatabaseStructureMembers { TableList = true };
            m_dstModel = new DatabaseStructure(m_target.InvokeLoadStructure(dbmem, null));
        }

        private void ReloadSourceModel()
        {
            var dbmem = new DatabaseStructureMembers { TableList = true };
            dbmem.SpecificObjectOverride["view"] = new SpecificObjectMembers { ObjectList = true };
            m_srcModel = new DatabaseStructure(m_source.InvokeLoadStructure(dbmem, null));
        }

        private void RefreshData()
        {
            RefreshData(null, null);
        }

        private void RefreshData(NameWithSchema selectTable, DataSynDefItem selectItem)
        {
            var items = new List<object>();
            foreach (var item in m_datasyn.Items) items.Add(new ItemWrapper(this, item));
            TargetTableWrapper selwrap = null;
            if (m_dstModel != null)
            {
                var tbls = new List<NameWithSchema>();
                tbls.AddRange(from t in m_dstModel.Tables select t.FullName);
                tbls.Sort();
                foreach (var tbl in tbls)
                {
                    if (m_datasyn.Items.Find(it => it.Target.Table == tbl) != null) continue;
                    var wrap = new TargetTableWrapper(this, tbl);
                    items.Add(wrap);
                    if (selectTable != null && selectTable == tbl) selwrap = wrap;
                }
            }
            items.SortByKey(obj =>
            {
                var wrap = obj as ItemWrapper;
                if (wrap != null) return wrap.m_item.Target.Table;
                var tbl = obj as TargetTableWrapper;
                if (tbl != null) return tbl.Table;
                return new NameWithSchema("");
            });
            objectListView1.SetObjects(items);
            if (selwrap != null) objectListView1.SelectedObject = selwrap;
            if (selectItem != null) objectListView1.SelectedObject = selectItem;

            //using (var gp = new GridPosition(dataGridView1))
            //{
            //    dataGridView1.Rows.Clear();
            //    foreach (var item in m_datasyn.Items)
            //    {
            //        var index = dataGridView1.Rows.Add(item.SourceToString(), item.TargetToString(), item.GetColumnsDescription());
            //        dataGridView1.Rows[index].Tag = item;
            //    }
            //}
            ShowCurrent();
            UpdateEnabling();
        }

        //private void toolStripButton1_Click(object sender, EventArgs e)
        //{
        //    if (m_dstModel == null)
        //    {
        //        StdDialog.ShowError("s_please_select_valid_target");
        //        return;
        //    }
        //    var names = DataSynAddTableForm.Run(m_dstModel);
        //    if (names != null)
        //    {
        //        foreach (var name in names)
        //        {
        //            var nitem = new DataSynDefItem(m_datasyn);
        //            nitem.Target = new DataSynTarget { Table = name };
        //            if (m_srcModel != null)
        //            {
        //                var srctable = m_srcModel.FindSimilarTable(name);
        //                if (srctable != null) nitem.Source = new DataSynTableSource { Name = srctable.FullName };
        //            }
        //            m_datasyn.Items.Add(nitem);
        //        }
        //        RefreshData();
        //    }
        //}

        private void UpdateSynCount()
        {
            int sum = 0, sel = 0;
            if (m_datasyn != null)
            {
                foreach (var elem in m_datasyn.Items)
                {
                    sum++;
                    if (elem.IsChecked) sel++;
                }
            }
            labSynActions.Text = String.Format("{0}/{1}", sel, sum);
        }

        private Job CreateJobNoReports()
        {
            return CreateJob(null, null, null);
        }

        private Job CreateJob(string outFile, List<IJobReportConfiguration> reports, DataSynGuiEnv guienv)
        {
            return DataSynJob.CreateJob(m_source.CloneSource(), m_target.CloneSource(), m_datasyn, outFile, reports, null, guienv);
        }

        private void btnSynchronize_Click(object sender, EventArgs e)
        {
            List<IJobReportConfiguration> reports = null;
            if (m_cmd != null)
            {
                var cmd2 = m_jobconn.GetCommand(m_cmd.GroupId);
                if (cmd2 != null) reports = cmd2.ReportConfigs;
            }
            CreateJob(null, reports, null).StartProcess();
        }

        private void ssavejobToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_jobconn != null)
            {
                m_jobconn.SaveCommand(m_cmd);
            }
            else
            {
                SaveAs();
            }
        }

        private void SaveAs()
        {
            var res = Job.AskAndExportToFile(CreateJobNoReports);
            if (res != null)
            {
                CloseConnections();
                LoadFromJob(res.JobConn, (DataSynJobCommand)res.Commands[0]);
            }
        }

        private void scloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private SynItem SelectedSynItem
        {
            get
            {
                var item = SelectedItem;
                if (item == null) return null;
                return m_items.Get(item.Target.Table);
            }
        }

        private DataSynDefItem SelectedItem
        {
            get
            {
                var wrap = objectListView1.SelectedObject as ItemWrapper;
                if (wrap == null) return null;
                return wrap.m_item;
                //if (dataGridView1.CurrentCell != null && dataGridView1.CurrentCell.RowIndex >= 0)
                //{
                //    return (DataSynDefItem)dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Tag;
                //}
                //return null;
            }
        }

        private void objectListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_showedCurrent = false;
            MainWindow.Instance.RunInMainWindow(CondShowCurrent);
            UpdateEnabling();
        }

        bool m_showedCurrent;

        private void CondShowCurrent()
        {
            if (m_showedCurrent) return;
            ShowCurrent();
            m_showedCurrent = true;
        }

        private void ShowCurrent()
        {
            dataSynDefItemFrame1.SetItem(SelectedItem, SelectedSynItem);
            propertyFrame1.SelectedObject = SelectedItem != null ? SelectedItem.Options : null;
            if (objectListView1.SelectedObject is TargetTableWrapper)
            {
                var wrap = (TargetTableWrapper)objectListView1.SelectedObject;
                dataSynDefItemFrame1.SetTableNameHint(wrap.SourceTable, wrap.Table);
            }
        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            int itidx = -1;
            if (SelectedItem != null) itidx = m_datasyn.Items.IndexOf(SelectedItem);
            if (itidx >= 0 && itidx < m_datasyn.Items.Count - 1)
            {
                m_datasyn.Items.Exchange(itidx, itidx + 1);
                RefreshData(null, m_datasyn.Items[itidx + 1]);
            }
        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            int itidx = -1;
            if (SelectedItem != null) itidx = m_datasyn.Items.IndexOf(SelectedItem);
            if (itidx > 0)
            {
                m_datasyn.Items.Exchange(itidx, itidx - 1);
                RefreshData(null, m_datasyn.Items[itidx - 1]);
            }
        }

        private void dataSynDefItemFrame1_ChangedItem(object sender, EventArgs e)
        {
            objectListView1.RefreshObject(dataSynDefItemFrame1.Item);
            //foreach (DataGridViewRow row in dataGridView1.Rows)
            //{
            //    var item = row.Tag as DataSynDefItem;
            //    if (item == dataSynDefItemFrame1.Item)
            //    {
            //        row.Cells[0].Value = item.SourceToString();
            //        row.Cells[1].Value = item.TargetToString();
            //        row.Cells[2].Value = item.GetColumnsDescription();
            //    }
            //}
        }

        //private void toolStripButton2_Click(object sender, EventArgs e)
        //{
        //    if (dataGridView1.CurrentCell != null && dataGridView1.CurrentCell.RowIndex >= 0)
        //    {
        //        m_datasyn.Items.RemoveAt(dataGridView1.CurrentCell.RowIndex);
        //        RefreshData();
        //    }
        //}

        bool m_refreshed = false;
        private void CondRefreshData()
        {
            if (m_refreshed) return;
            RefreshData();
            m_refreshed = true;
        }

        private void dataGridView1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            m_datasyn.Items.RemoveAt(e.Row.Index);
            m_refreshed = false;
            MainWindow.Instance.RunInMainWindow(CondRefreshData);
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            EditPropertiesForm.Run(m_datasyn.Options, false);
        }

        private void mnuOpen_Click(object sender, EventArgs e)
        {
            var cmd = LoadJobCommandForm.Run(c => c is DataSynJobCommand);
            if (cmd != null)
            {
                LoadFromJob(cmd.Connection, (DataSynJobCommand)cmd.Command);
            }
        }

        private void mnuSaveAs_Click(object sender, EventArgs e)
        {
            SaveAs();
        }

        private void mnuSaveScript_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialogEx() == DialogResult.OK)
            {
                CreateJob(saveFileDialog1.FileName, null, null).StartProcess();
            }
        }

        private void btnAddToFavorites_Click(object sender, EventArgs e)
        {
            if (m_jobconn == null)
            {
                StdDialog.ShowInfo("s_please_save_job_first");
                return;
            }
            AddToFavoriteForm.Run(
                new DataSynFavorite { File = m_jobconn.FileName, Command = m_cmd.GroupId },
                Path.GetFileNameWithoutExtension(m_jobconn.FileName)
                );
        }

        private void sreportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (m_jobconn == null)
            {
                StdDialog.ShowInfo("s_please_save_job_first");
                return;
            }
            JobReportForm.Run(m_jobconn.FileName, m_cmd);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            var newdb = TreeSelectForm.SelectDatabase();
            if (DbSuitable(newdb))
            {
                if (m_source != null) Async.SafeClose(m_source.Connection);
                m_source = newdb;
                if (m_cmd != null) m_cmd.Source = newdb;
                m_datasyn.NotifyChangeSourceDatabase(newdb);
                m_srcModel = null;
                DownloadWhenNeededAndRefresh();
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            var newdb = TreeSelectForm.SelectDatabase();
            if (DbSuitable(newdb))
            {
                if (m_target != null) Async.SafeClose(m_target.Connection);
                m_target = newdb;
                if (m_cmd != null) m_cmd.Target = newdb;
                m_datasyn.NotifyChangeTargetDatabase(newdb);
                m_dstModel = null;
                DownloadWhenNeededAndRefresh();
            }
        }

        private void panel18_Resize(object sender, EventArgs e)
        {
            UpdateSourceLeft();
        }

        class ItemWrapper
        {
            internal DataSynDefItem m_item;
            DataSynForm m_form;

            internal ItemWrapper(DataSynForm form, DataSynDefItem item)
            {
                m_item = item;
                m_form = form;
            }

            public string SourceString
            {
                get
                {
                    if (m_item.Source != null) return m_item.Source.ToString();
                    return "???";
                }
            }

            public string TargetString
            {
                get
                {
                    if (m_item.Target != null) return m_item.Target.ToString();
                    return "???";
                }
            }

            public string ColumnsDescription
            {
                get { return m_item.ColumnsDescription; }
            }

            public bool IsChecked
            {
                get { return m_item.IsChecked; }
                set
                {
                    m_item.IsChecked = value;
                    m_form.UpdateEnabling();
                }
            }

            SynItem GetItem()
            {
                var key = m_item.Target.Table;
                SynItem res = null;
                m_form.m_items.TryGetValue(key, out res);
                return res;
            }

            public string SourceRows
            {
                get
                {
                    var item = GetItem();
                    if (item == null || item.Stats == null) return "";
                    return String.Format("{0} / {1}", item.Stats.OnlyInSource, item.Stats.Source);
                }
            }

            public string TargetRows
            {
                get
                {
                    var item = GetItem();
                    if (item == null || item.Stats == null) return "";
                    return String.Format("{0} / {1}", item.Stats.OnlyInTarget, item.Stats.Target);
                }
            }

            public string BothRows
            {
                get
                {
                    var item = GetItem();
                    if (item == null || item.Stats == null) return "";
                    return String.Format("{0} / {1}", item.Stats.Modified, item.Stats.Equal);
                }
            }

            public Bitmap StateImage
            {
                get
                {
                    var item = GetItem();
                    if (item == null) return null;
                    switch (item.State)
                    {
                        case SynItemState.Error: return CoreIcons.error;
                        case SynItemState.Start: return CoreIcons.hourglass;
                        case SynItemState.Compared: return CoreIcons.compare;
                    }
                    return null;
                }
            }

            public string State { get { return ""; } }
        }

        class TargetTableWrapper
        {
            NameWithSchema m_name;
            DataSynForm m_form;

            internal TargetTableWrapper(DataSynForm form, NameWithSchema name)
            {
                m_name = name;
                m_form = form;
            }

            public NameWithSchema SourceTable
            {
                get
                {
                    if (m_form.m_srcModel != null)
                    {
                        var srctable = m_form.m_srcModel.FindSimilarTable(m_name);
                        if (srctable != null) return srctable.FullName;
                    }
                    return null;
                }
            }

            public string SourceString
            {
                get
                {
                    var tbl = SourceTable;
                    if (tbl != null) return tbl.ToString();
                    return "n/a";
                }
            }
            public string TargetString { get { return m_name.ToString(); } }
            public string ColumnsDescription { get { return "n/a"; } }
            public bool IsChecked
            {
                get { return false; }
                set
                {
                    if (SourceTable != null)
                    {
                        m_form.CreateTableItem(SourceTable, m_name);
                    }
                    else
                    {
                        StdDialog.ShowError("s_please_select_valid_source");
                    }
                }
            }

            public NameWithSchema Table { get { return m_name; } }
            public string State { get { return ""; } }
            public string SourceRows { get { return ""; } }
            public string TargetRows { get { return ""; } }
            public string BothRows { get { return ""; } }
        }

        private void dataSynDefItemFrame1_CreatedItem(object sender, CreateDataSynItemEventArgs e)
        {
            var table = objectListView1.SelectedObject as TargetTableWrapper;
            if (table == null) return;

            var nitem = new DataSynDefItem(m_datasyn);
            nitem.Target = new DataSynTarget { Table = table.Table };
            nitem.Source = e.Source;

            if (m_srcModel != null && nitem.Source is DataSynTableSource)
            {
                var srctable = m_srcModel.FindSimilarTable(table.Table);
                if (srctable != null) nitem.Source = new DataSynTableSource { Name = srctable.FullName };
            }
            m_datasyn.Items.Add(nitem);

            RefreshData(null, nitem);
        }

        private void CreateTableItem(NameWithSchema source, NameWithSchema target)
        {
            var nitem = new DataSynDefItem(m_datasyn);
            nitem.Source = new DataSynTableSource { Name = source };
            nitem.Target = new DataSynTarget { Table = target };
            m_datasyn.Items.Add(nitem);
            RefreshData(null, nitem);
        }

        private void dataSynDefItemFrame1_RemovedItem(object sender, EventArgs e)
        {
            var item = SelectedItem;
            if (item == null) return;
            m_datasyn.Items.Remove(item);
            RefreshData(item.Target.Table, null);
        }

        private DataSynGuiEnv CreateGuiEnv(bool compare, DataSynDefItem item)
        {
            var res = new DataSynGuiEnv();
            res.CompareOnly = compare;
            res.ItemEvent += new EventHandler<SynItemEventArgs>(res_ItemEvent);
            if (item != null) res.SetFilter(item.Target.Table);
            return res;
        }
        
        private ItemWrapper FindWrapper(NameWithSchema table)
        {
            foreach (var obj in objectListView1.Objects)
            {
                var item = obj as ItemWrapper;
                if (item != null && item.m_item.Target.Table == table)
                {
                    return item;
                }
            }
            return null;
        }

        void res_ItemEvent(object sender, SynItemEventArgs e)
        {
            var key = e.Item.Item.Target.Table;
            if (m_items.ContainsKey(key) && m_items[key] != e.Item)
            {
                m_items[key].Dispose();
            }
            m_items[key] = e.Item;
            var wrap = FindWrapper(key);
            if (wrap != null) objectListView1.RefreshObject(wrap);
            if (e.Item.Item == SelectedItem)
            {
                ShowCurrent();
            }
        }

        private void btnCompare_Click(object sender, EventArgs e)
        {
            CreateJob(null, null, CreateGuiEnv(true, null)).StartProcess();
        }

        private void btnCompareItem_Click(object sender, EventArgs e)
        {
            if (SelectedItem != null)
            {
                CreateJob(null, null, CreateGuiEnv(true, SelectedItem)).StartProcess();
            }
        }

        private void btnSynchronizeItem_Click(object sender, EventArgs e)
        {
            if (SelectedItem != null)
            {
                CreateJob(null, null, CreateGuiEnv(false, SelectedItem)).StartProcess();
            }
        }

        private void btnSwap_Click(object sender, EventArgs e)
        {
            PyList.Exchange(ref m_source, ref m_target);
            PyList.Exchange(ref m_srcModel, ref m_dstModel);
            DownloadWhenNeededAndRefresh();
        }

        private void btnUncheckAll_Click(object sender, EventArgs e)
        {
            foreach (var item in m_datasyn.Items)
            {
                item.IsChecked = false;
            }
            RefreshData();
        }

        private void btnCheckAll_Click(object sender, EventArgs e)
        {
            using (var wc = new WaitContext())
            {
                foreach (object item in objectListView1.Objects)
                {
                    var target = item as TargetTableWrapper;
                    if (target != null && target.SourceTable != null)
                    {
                        target.IsChecked = true;
                    }
                }
                foreach (var item in m_datasyn.Items)
                {
                    item.IsChecked = true;
                }
                RefreshData();
            }
        }
    }

    [Favorite(Name = "datasynwin", Title = "Data synchronize window", RequiredFeature = DataSynchronizationFeature.Test)]
    public class DataSynFavorite : FavoriteBase
    {
        [XmlElem]
        public string File { get; set; }

        [XmlElem]
        public string Command { get; set; }

        public override Bitmap Image
        {
            get { return DataSynIcons.sync; }
        }

        public override void Open()
        {
            var jobconn = new JobConnection(File);
            var job = jobconn.GetJob();
            var cmd = job.Root.FindCommand(Command);
            DataSynForm.Run(jobconn, (DataSynJobCommand)cmd);
        }

        public override string Description
        {
            get { return "s_data_synchronization"; }
        }

        public override void DisplayProps(Action<string, string> display)
        {
            base.DisplayProps(display);
            display("s_job_file", File);
            var job = Job.LoadFromFile(File);
            var cmd = job.FindCommand(Command);
            if (cmd != null)
            {
                display("s_command", cmd.ToString());
            }
        }
    }
}
