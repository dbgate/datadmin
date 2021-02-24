using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.Common;
using System.IO;
using DatAdmin;

namespace Plugin.dbmodel
{
    public partial class SynchronizeStructureForm : FormEx
    {
        IDatabaseSource m_srcDb;
        IDatabaseSource m_dstDb;

        DatabaseStructure m_downloadedSrc;
        bool m_srcFixedDataLoaded;

        DatabaseStructure m_downloadedDst;
        bool m_dstFixedDataLoaded;

        internal IDatabaseStructure m_src;
        internal IDatabaseStructure m_dst;

        //TreeStructureData m_srcData;
        //TreeStructureData m_dstData;

        internal DatabaseDiff m_diff;
        DbDiffAction m_shownAction;

        //ControlInvoker m_invoker;

        //int m_checkCounter = 0;
        internal int m_selectCounter = 0;

        SynchronizeSettings m_settings;

        ImageCache m_imgCache;
        SynchronizeExtData m_synExtData;
        bool m_initialized;

        public SynchronizeStructureForm(IDatabaseSource src, IDatabaseSource dst, SynchronizeExtData extInfo)
        {
            m_synExtData = extInfo;
            InitializeComponent();
            m_imgCache = new ImageCache(imageList1, Color.White);
            //btnUncheckAll.Image = ImageTool.CombineImages(CoreIcons.check, CoreIcons.delete_overlay);
            ConnPack.Cache = new CachePack
            {
                ReuseAnalyserCache = true
            };
            //m_srcData = new TreeStructureData(treeSource, Color.Green, this, src, true);
            //m_dstData = new TreeStructureData(treeTarget, Color.Red, this, dst, false);
            //m_srcData.OtherTree = m_dstData;
            //m_dstData.OtherTree = m_srcData;
            Disposed += new EventHandler(SynchronizeStructureForm_Disposed);
            m_srcDb = src;
            m_dstDb = dst;
            if (m_srcDb != null) m_srcDb.ChangeConnection(ConnPack);
            if (m_dstDb != null) m_dstDb.ChangeConnection(ConnPack);
            //m_invoker = new ControlInvoker(this);
            m_settings = new SynchronizeSettings();
            SettingsTool.CopySettingsPage((SynchronizeSettings)GlobalSettings.Pages.PageByName("synchronize"), m_settings);
            //m_trace = new TraceVisibilityHandler(btnTrace);

            UpdateTitles();
            DownloadWhenNeededAndRefresh();

            colObjectType.ImageGetter = row => ((DbDiffAction)row).GetObjectTypeImage(m_imgCache);
            colRelation.ImageGetter = row => ((DbDiffAction)row).GetRelationImage(m_imgCache);
            colColumnRelation.ImageGetter = row => ((DbDiffAction)row).GetRelationImage(m_imgCache);
            colConstraintRelation.ImageGetter = row => ((DbDiffAction)row).GetRelationImage(m_imgCache);
            colConstraintType.ImageGetter = row => ((DbDiffAction)row).GetObjectTypeImage(m_imgCache);
            colObjType.ImageGetter = row => ((DbDiffAction)row).GetObjectTypeImage(m_imgCache);

            if (extInfo != null && extInfo.Dbs != null)
            {
                FillDbs(btnSelectSourceDb, cbxSelectSource);
                if (extInfo.SelectedSource != null)
                {
                    cbxSelectSource.SelectedIndex = cbxSelectSource.Items.IndexOf(extInfo.SelectedSource);
                }

                FillDbs(btnSelectTargetDb, cbxSelectTarget);
                if (extInfo.SelectedTarget != null)
                {
                    cbxSelectTarget.SelectedIndex = cbxSelectTarget.Items.IndexOf(extInfo.SelectedTarget);
                }
            }
            else
            {
                cbxSelectSource.Visible = false;
                cbxSelectTarget.Visible = false;
            }

            m_initialized = true;
        }

        private void FillDbs(Button btn, ComboBox cbx)
        {
            cbx.Items.Clear();
            btn.Visible = false;
            foreach (string db in m_synExtData.Dbs)
            {
                cbx.Items.Add(db);
            }
        }

        private void UpdateSourceLeft()
        {
            labSourceServer.Left = labSourceServer.Parent.Width - labSourceServer.Width;
            labSourceDb.Left = labSourceDb.Parent.Width - labSourceDb.Width;
        }

        private void UpdateTitles()
        {
            if (m_srcDb != null)
            {
                labSourceServer.Text = m_srcDb.Connection.PhysicalFactory.GetDataSource();
                labSourceDb.Text = m_srcDb.DatabaseName ?? "";
            }
            else
            {
                labSourceServer.Text = "";
                labSourceDb.Text = "";
            }
            UpdateSourceLeft();

            if (m_dstDb != null)
            {
                labTargetServer.Text = m_dstDb.Connection.PhysicalFactory.GetDataSource();
                labTargetDb.Text = m_dstDb.DatabaseName ?? "";
                codeEditor1.Dialect = m_dstDb.Dialect;
            }
            else
            {
                labTargetServer.Text = "";
                labTargetDb.Text = "";
                codeEditor1.Dialect = null;
            }

            btnAddToFavorites.Enabled = btnSynchronize.Enabled = mnuSynchronize.Enabled = (m_srcDb != null && m_dstDb != null);
            int added = 0, removed = 0, changed = 0, equal = 0;
            if (m_diff != null)
            {
                m_diff.Actions.CountActions(ref added, ref  removed, ref changed, ref equal);
                equal = GetEqualObjs().Count;
            }
            btnAdded.Text = FormatCount("s_added", added);
            btnRemoved.Text = FormatCount("s_removed", removed);
            btnChanged.Text = FormatCount("s_changed", changed);
            btnEqual.Text = FormatCount("s_equal", equal);

            UpdateSynCount();
        }

        private static string FormatCount(string s, int count)
        {
            if (count > 0) return String.Format("{0} ({1})", Texts.Get(s), count);
            return Texts.Get(s);
        }

        private void RefreshData()
        {
            RefreshData(true, true);
        }

        private void RefreshData(bool refreshSource, bool refreshTarget)
        {
            if (refreshSource)
            {
                m_downloadedSrc = null;
                m_srcFixedDataLoaded = false;
                if (m_srcDb != null) m_srcDb.Connection.Cache.Clear();
            }
            if (refreshTarget)
            {
                m_downloadedDst = null;
                m_dstFixedDataLoaded = false;
                if (m_dstDb != null) m_dstDb.Connection.Cache.Clear();
            }
            DownloadWhenNeededAndRefresh();

            //    m_src = null;
            //    m_dst = null;

            //    if (m_srcDb != null && m_dstDb != null)
            //    {
            //        m_downloadedSrc = m_srcDb.InvokeLoadStructure(DatabaseStructureMembers.FullStructure, null);
            //        m_downloadedDst = m_dstDb.InvokeLoadStructure(DatabaseStructureMembers.FullStructure, null);
            //    }
            //}
            //RefreshNotDownload();
        }

        void DownloadWhenNeededAndRefresh()
        {
            using (WaitContext wc = new WaitContext())
            {
                if (m_srcDb != null) Async.SafeOpen(m_srcDb.Connection);
                if (m_dstDb != null) Async.SafeOpen(m_dstDb.Connection);
            }

            if (m_srcDb != null)
            {
                if (m_downloadedSrc == null)
                {
                    Controls.ShowProgress(true);
                    m_srcDb.Connection.BeginInvoke((Action)DoDownloadSource, Async.CreateInvokeCallback(m_invoker, DownloadedSource));
                    return;
                }
            }
            if (m_dstDb != null)
            {
                if (m_downloadedDst == null)
                {
                    Controls.ShowProgress(true);
                    m_dstDb.Connection.BeginInvoke((Action)DoDownloadTarget, Async.CreateInvokeCallback(m_invoker, DownloadedTarget));
                    return;
                }
            }
            if (m_srcDb != null && m_dstDb != null && m_downloadedSrc != null && m_downloadedDst != null)
            {
                if (!m_srcFixedDataLoaded && m_srcDb.DatabaseCaps.ExecuteSql && m_dstDb.DatabaseCaps.FixedDataDefiner)
                {
                    Controls.ShowProgress(true);
                    m_srcDb.Connection.BeginInvoke((Action)DoDownloadSourceData, Async.CreateInvokeCallback(m_invoker, DownloadedSource));
                    return;
                }
                if (!m_dstFixedDataLoaded && m_dstDb.DatabaseCaps.ExecuteSql && m_srcDb.DatabaseCaps.FixedDataDefiner)
                {
                    Controls.ShowProgress(true);
                    m_dstDb.Connection.BeginInvoke((Action)DoDownloadTargetData, Async.CreateInvokeCallback(m_invoker, DownloadedTarget));
                    return;
                }
            }
            RefreshNotDownload();
        }

        void DownloadedSource(IAsyncResult res)
        {
            try
            {
                Controls.ShowProgress(false);
                m_srcDb.Connection.EndInvoke(res);
                DownloadWhenNeededAndRefresh();
            }
            catch (Exception err)
            {
                Errors.Report(err);
            }
        }
        void DownloadedTarget(IAsyncResult res)
        {
            try
            {
                Controls.ShowProgress(false);
                m_dstDb.Connection.EndInvoke(res);
                DownloadWhenNeededAndRefresh();
            }
            catch (Exception err)
            {
                Errors.Report(err);
            }
        }

        void DoDownloadSource()
        {
            m_downloadedSrc = new DatabaseStructure(m_srcDb.LoadDatabaseStructure(DatabaseStructureMembers.FullStructure, null));
        }

        void DoDownloadSourceData()
        {
            DownloadFixedData(m_srcDb, m_downloadedSrc, m_downloadedDst);
            m_srcFixedDataLoaded = true;
        }

        void DoDownloadTarget()
        {
            m_downloadedDst = new DatabaseStructure(m_dstDb.LoadDatabaseStructure(DatabaseStructureMembers.FullStructure, null));
        }

        void DoDownloadTargetData()
        {
            DownloadFixedData(m_dstDb, m_downloadedDst, m_downloadedSrc);
            m_dstFixedDataLoaded = true;
        }

        private void DownloadFixedData(IDatabaseSource db, DatabaseStructure dbs, IDatabaseStructure pattern)
        {
            foreach (var tbl in pattern.Tables)
            {
                if (tbl.FixedData == null) continue;
                if (dbs.Tables.GetIndex(tbl.FullName) < 0) continue;
                var t2 = (TableStructure)dbs.Tables[tbl.FullName];
                var pk1 = tbl.FindConstraint<IPrimaryKey>();
                var pk2 = t2.FindConstraint<IPrimaryKey>();
                if (pk1 == null || pk2 == null) continue;
                if (pk1.Columns.Count != pk2.Columns.Count) continue;
                using (DbCommand cmd = db.Connection.SystemConnection.CreateCommand())
                {
                    StringWriter sw = new StringWriter();
                    ISqlDumper dmp = db.Dialect.CreateDumper(sw);
                    dmp.Put("^select * ^from %f ^where 0=1", t2);
                    foreach (var row in tbl.FixedData.Rows)
                    {
                        dmp.Put(" ^or ( ");
                        for (int i = 0; i < pk1.Columns.Count; i++)
                        {
                            if (i > 0) dmp.Put(" ^and ");
                            dmp.Put(" %s = %v ",
                                pk1.Columns[i].ColumnName,
                                row.GetValue(tbl.Columns.GetIndex(pk1.Columns[i].ColumnName)));
                        }
                        dmp.Put(" ) ");
                    }
                    cmd.CommandText = sw.ToString();
                    using (var reader = db.GetAnyDDA().AdaptReader(cmd.ExecuteReader()))
                    {
                        t2.FixedData = new InMemoryTable(t2, reader);
                    }
                }
            }
        }

        private DbDiffOptions CreateDbDiffOptions()
        {
            var opts = new DbDiffOptions();
            var log = new CachingLogger(LogLevel.All);
            messageLogFrame1.Source = log;
            opts.DiffLogger = log;
            if (m_dstDb.Dialect != null) opts.LeftImplicitSchema = m_dstDb.Dialect.DefaultSchema;
            if (m_srcDb.Dialect != null) opts.RightImplicitSchema = m_srcDb.Dialect.DefaultSchema;
            if (!m_dstDb.DatabaseCaps.MultipleSchema) opts.SchemaMode = DbDiffSchemaMode.Ignore;
            if (!m_srcDb.DatabaseCaps.MultipleSchema) opts.SchemaMode = DbDiffSchemaMode.Ignore;
            if (m_settings.IgnoreSchema) opts.SchemaMode = DbDiffSchemaMode.Ignore;
            opts.IgnoreAllTableProperties = m_settings.IgnoreAllTableProperties;
            opts.IgnoreColumnOrder = m_settings.IgnoreColumnOrder;
            opts.IgnoreConstraintNames = m_settings.IgnoreConstraintNames;
            opts.IgnoreTableProperties = m_settings.IgnoreTableProperties;
            opts.IgnoreDataTypeProperties = m_settings.IgnoreDataTypeProperties;
            opts.IgnoreColumnCharacterSet = m_settings.IgnoreColumnCharacterSet;
            opts.IgnoreColumnCollation = m_settings.IgnoreColumnCollation;
            opts.IgnoreCase = m_settings.IgnoreCase;
            opts.AllowRecreateConstraint = true;
            opts.AllowRecreateSpecificObject = true;
            opts.AllowRecreateTable = true;
            opts.AllowPairRenamedTables = m_settings.AllowPairRenamedTables;
            return opts;
        }

        private void RefreshNotDownload()
        {
            //m_srcData.Clear();
            //m_dstData.Clear();

            m_shownAction = null;
            codeEditor1.Text = "";
            //treeActions.Nodes.Clear();

            m_src = null;
            m_dst = null;

            if (m_downloadedSrc != null && m_downloadedDst != null)
            {
                var opts = CreateDbDiffOptions();
                if (m_diff != null) m_diff.ChangedAction -= m_diff_ChangedAction;
                m_diff = new DatabaseDiff(m_downloadedSrc, m_downloadedDst, opts, m_dstDb.Dialect);
                m_diff.ChangedAction += new Action<DbDiffAction>(m_diff_ChangedAction);
                opts.DiffLogger = NopLogger.Instance;
                m_src = m_diff.Source;
                m_dst = m_diff.Target;
                //treeSource.Root = new DbDefViewTreeNode(m_src, m_srcDb.Dialect);
                //treeTarget.Root = new DbDefViewTreeNode(m_dst, m_dstDb.Dialect);
                RefreshObjectList();
                //m_diff.Actions.FillTreeNodes(treeActions.Nodes);
                //UsageStats.Usage("compare_db",
                //    "src", m_src.ToString(),
                //    "dst", m_dst.ToString(),
                //    "actions", m_diff.Actions.Elements.Count.ToString());
            }
            else
            {
                if (m_diff != null) m_diff.ChangedAction -= m_diff_ChangedAction;
                m_diff = null;
                //treeSource.Root = null;
                //treeTarget.Root = null;
                //if (m_downloadedSrc != null) treeSource.Root = new DbDefViewTreeNode(m_downloadedSrc, m_srcDb.Dialect);
                //if (m_downloadedDst != null) treeTarget.Root = new DbDefViewTreeNode(m_downloadedDst, m_srcDb.Dialect);
            }
            UpdateTitles();
        }

        void m_diff_ChangedAction(DbDiffAction obj)
        {
            objectListIndexesKeys.RefreshObject(obj);
            objectListColumns.RefreshObject(obj);
            objectListViewActions.RefreshObject(obj);
            objectListTables.RefreshObject(obj);
            if (m_shownAction != null && m_shownAction.Elements.Contains(obj)) UpdateAlterCode();
            UpdateSynCount();
        }

        private void UpdateSynCount()
        {
            int sum = 0, sel = 0;
            if (m_diff != null)
            {
                foreach (var elem in m_diff.Actions.Elements)
                {
                    sum++;
                    if (elem.IsChecked) sel++;
                }
            }
            labSynActions.Text = String.Format("{0}/{1}", sel, sum);
        }

        private List<IAbstractObjectStructure> GetEqualObjs()
        {
            var grps = new HashSetEx<string>();
            var objs = new List<IAbstractObjectStructure>();
            foreach (var elem in m_diff.Actions.Elements)
            {
                grps.Add(elem.GroupId);
            }

            foreach (var obj in m_src.Tables)
            {
                if (grps.Contains(obj.GroupId)) continue;
                objs.Add(obj);
            }
            foreach (var spec in m_src.GetAllSpecificObjects())
            {
                if (grps.Contains(spec.GroupId)) continue;
                objs.Add(spec);
            }
            return objs;
        }

        private void RefreshObjectList()
        {
            var objs = new List<object>();
            foreach (var elem in m_diff.Actions.Elements)
            {
                switch (elem.ActionType)
                {
                    case DbDiffActionType.Add:
                        if (!btnAdded.Checked) continue;
                        break;
                    case DbDiffActionType.Change:
                        if (!btnChanged.Checked) continue;
                        break;
                    case DbDiffActionType.Remove:
                        if (!btnRemoved.Checked) continue;
                        break;
                }
                objs.Add(elem);
            }
            if (btnEqual.Checked)
            {
                foreach (var obj in GetEqualObjs())
                {
                    objs.Add(new DbDiffAction(m_diff) { GroupId = obj.GroupId });
                }
            }
            objectListTables.SetObjects(objs);
        }

        private void sswapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var t1 = m_srcDb;
            m_srcDb = m_dstDb;
            m_dstDb = t1;
            var t2 = m_downloadedSrc;
            m_downloadedSrc = m_downloadedDst;
            m_downloadedDst = t2;

            RefreshNotDownload();
            UpdateTitles();
        }

        void SynchronizeStructureForm_Disposed(object sender, EventArgs e)
        {
            if (m_srcDb != null) Async.SafeClose(m_srcDb.Connection);
            if (m_dstDb != null) Async.SafeClose(m_dstDb.Connection);
            if (m_diff != null) m_diff.ChangedAction -= m_diff_ChangedAction;
        }

        public static void Run(IDatabaseSource src, IDatabaseSource dst, SynchronizeExtData extData)
        {
            if (!LicenseTool.FeatureAllowedMsg(DbStructSynchronizationFeature.Test)) return;
            SynchronizeStructureForm win = new SynchronizeStructureForm(src, dst, extData);
            win.Show();
        }

        public static void Run(IDatabaseSource src, IDatabaseSource dst)
        {
            Run(src, dst, null);
        }

        public static void RunNoParam()
        {
            Run(null, null);
        }

        private void scloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SynchronizeStructureForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) Close();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void tbnChangeSource_Click(object sender, EventArgs e)
        {
            var newdb = TreeSelectForm.SelectDatabase();
            if (newdb != null)
            {
                SetNewSourceDb(newdb);
            }
        }

        private void SetNewSourceDb(IDatabaseSource newdb)
        {
            if (m_srcDb != null) Async.SafeClose(m_srcDb.Connection);
            m_srcDb = newdb;
            if (m_srcDb != null) m_srcDb.ChangeConnection(ConnPack);
            m_downloadedSrc = null;
            DownloadWhenNeededAndRefresh();
        }

        private void btnChangeTarget_Click(object sender, EventArgs e)
        {
            var newdb = TreeSelectForm.SelectDatabase();
            if (newdb != null)
            {
                SetNewTargetDb(newdb);
            }
        }

        private void SetNewTargetDb(IDatabaseSource newdb)
        {
            if (m_dstDb != null) Async.SafeClose(m_dstDb.Connection);
            m_dstDb = newdb;
            if (m_dstDb != null) m_dstDb.ChangeConnection(ConnPack);
            m_downloadedDst = null;
            DownloadWhenNeededAndRefresh();
        }

        private bool RunActionAsSql(DbDiffAction action)
        {
            string sql = action.GenerateSql(m_dstDb);
            if (SqlConfirmForm.Run(sql))
            {
                m_dstDb.Connection.Invoke(() =>
                {
                    var con = m_dstDb.Connection.SystemConnection;
                    var tran = con.BeginTransaction();
                    var sqlo = new ConnectionSqlOutputStream(con, tran, m_dstDb.Dialect);
                    try
                    {
                        con.ExecuteNonQueries(sql, m_dstDb.Connection.Dialect, tran, null);
                    }
                    catch
                    {
                        tran.Rollback();
                        throw;
                    }
                    tran.Commit();
                });
                //var dmp = m_dstDb.Dialect.CreateDumper(sqlo, new SqlFormatProperties());
                //dmp.TargetDb = m_dstDb;
                //m_dstDb.Connection.Invoke(() => action.GenerateScript(dmp));
                return true;
            }
            return false;
        }

        private void btnSynchronize_Click(object sender, EventArgs e)
        {
            if (m_dstDb == null || m_diff == null) return;

            if (objectListTables.CheckedItems.Count == 0)
            {
                StdDialog.ShowInfo("s_no_synchronize_action_checked");
                return;
            }

            try
            {
                bool ok = false;
                if (m_dstDb.DatabaseCaps.ExecuteSql)
                {
                    // synchronize using SQL dumping
                    ok = RunActionAsSql(m_diff.Actions);
                }
                else
                {
                    // synchronize using data-sources alter/create/drop methods
                    if (MessageBox.Show(Texts.Get("s_really_change_destination_structure"), VersionInfo.ProgramTitle, MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        var plan = m_diff.Actions.GetPlanForThis(m_dstDb, true);
                        var opts = CreateDbDiffOptions();
                        m_dstDb.AlterDatabase(plan, opts);
                        ok = true;
                    }
                }
                if (ok)
                {
                    RefreshData(false, true);
                    Usage.AddSub("synchronize_ok", m_src.ToString(), m_dst.ToString());
                }
            }
            catch (Exception err)
            {
                Errors.Report(err);
            }
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            EditPropertiesForm.Run(m_settings, false);
            RefreshNotDownload();
        }

        private void btnRefreshSource_Click(object sender, EventArgs e)
        {
            RefreshData(true, false);
        }

        private void btnRefreshTarget_Click(object sender, EventArgs e)
        {
            RefreshData(false, true);
        }

        private void btnAddToFavorites_Click(object sender, EventArgs e)
        {
            AddToFavoriteForm.Run(
                new DBStructSynchronizeFavorite { Source = m_srcDb, Target = m_dstDb },
                String.Format("{0}-{1}", m_srcDb, m_dstDb)
            );
        }

        private void objectListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_shownAction = (DbDiffAction)objectListTables.SelectedObject;
            UpdateDiff();
            UpdateColumns();
            UpdateConstraints();
            UpdateAlterCode();
            UpdateActions();
            objectListTables.Focus();
        }

        private List<DbDiffAction> GetSubActions()
        {
            var res = new List<DbDiffAction>();
            if (m_shownAction == null) return res;
            if (!(m_shownAction.SourceObject is ITableStructure) || !(m_shownAction.TargetObject is ITableStructure)) return res;
            foreach (var elem in m_shownAction.Elements)
            {
                res.Add(elem);
            }
            return res;
        }

        private void UpdateActions()
        {
            objectListViewActions.SetObjects(GetSubActions());
        }

        private void UpdateAlterCode()
        {
            if (m_shownAction != null)
            {
                try
                {
                    codeEditor1.Text = m_shownAction.GenerateSql(m_dstDb);
                }
                catch (Exception err)
                {
                    codeEditor1.Text = Texts.Get("s_error") + ":" + err.Message;
                }
            }
            else
            {
                m_shownAction = null;
                codeEditor1.Text = "";
            }
        }

        private List<DbDiffAction> GetColumnActions()
        {
            var res = new List<DbDiffAction>();
            if (m_shownAction == null) return res;
            if (!(m_shownAction.SourceObject is ITableStructure) || !(m_shownAction.TargetObject is ITableStructure)) return res;

            var grps = new HashSetEx<string>();
            foreach (var elem in m_shownAction.Elements)
            {
                if (!(elem.AnyObject is IColumnStructure)) continue;
                res.Add(elem);
                grps.Add(elem.GroupId);
            }
            foreach (var scol in ((ITableStructure)m_shownAction.SourceObject).Columns)
            {
                if (grps.Contains(scol.GroupId)) continue;
                grps.Add(scol.GroupId);
                res.Add(new DbDiffAction(m_diff) { GroupId = scol.GroupId });
            }

            res.Sort((a, b) => ((IColumnStructure)a.AnyObject).ColumnOrder - ((IColumnStructure)b.AnyObject).ColumnOrder);

            return res;
        }

        private void UpdateColumns()
        {
            objectListColumns.SetObjects(GetColumnActions());
        }

        private List<DbDiffAction> GetConstraintActions()
        {
            var res = new List<DbDiffAction>();
            if (m_shownAction == null) return res;
            if (!(m_shownAction.SourceObject is ITableStructure) || !(m_shownAction.TargetObject is ITableStructure)) return res;

            var grps = new HashSetEx<string>();
            foreach (var elem in m_shownAction.Elements)
            {
                if (!(elem.AnyObject is IConstraint)) continue;
                res.Add(elem);
                grps.Add(elem.GroupId);
            }
            foreach (var scol in ((ITableStructure)m_shownAction.SourceObject).Constraints)
            {
                if (grps.Contains(scol.GroupId)) continue;
                grps.Add(scol.GroupId);
                res.Add(new DbDiffAction(m_diff) { GroupId = scol.GroupId });
            }

            return res;
        }

        private void UpdateConstraints()
        {
            objectListIndexesKeys.SetObjects(GetConstraintActions());
        }

        private void UpdateDiff()
        {
            string sql1 = "", sql2 = "";
            if (m_shownAction != null)
            {
                if (m_shownAction.SourceObject != null) sql1 = m_dstDb.Dialect.GenerateScript(dmp => dmp.CreateObject(m_shownAction.SourceObject));
                if (m_shownAction.TargetObject != null) sql2 = m_dstDb.Dialect.GenerateScript(dmp => dmp.CreateObject(m_shownAction.TargetObject));
            }
            diffControl1.ShowDiff(sql1, sql2);
        }

        private void btnObjFilter_Click(object sender, EventArgs e)
        {
            RefreshObjectList();
        }

        private void btnGenerateSql_Click(object sender, EventArgs e)
        {
            if (m_dstDb == null || m_diff == null) return;
            var appobj = new DbDiffAppObject
            {
                DbDiff = m_diff,
                TargetDb = m_dstDb
            };
            GenerateSqlForm.Run(new AppObject[] { appobj });
        }

        private void SetAllChecked(bool value)
        {
            if (m_diff == null) return;
            foreach (var elem in m_diff.Actions.Elements)
            {
                elem.IsChecked = value;
            }
        }

        private void btnCheckAll_Click(object sender, EventArgs e)
        {
            SetAllChecked(true);
        }

        private void btnUncheckAll_Click(object sender, EventArgs e)
        {
            SetAllChecked(false);
        }

        private void cbxSelectSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!m_initialized) return;
            SetNewSourceDb(m_synExtData.GetDb(cbxSelectSource.SelectedItem.ToString()));
        }

        private void cbxSelectTarget_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!m_initialized) return;
            SetNewTargetDb(m_synExtData.GetDb(cbxSelectTarget.SelectedItem.ToString()));
        }

        private void panel3_Resize(object sender, EventArgs e)
        {
            UpdateSourceLeft();
        }
    }

    [Favorite(Name = "structsynwin", Title = "Structure synchronize window", RequiredFeature = DbStructSynchronizationFeature.Test)]
    public class DBStructSynchronizeFavorite : FavoriteBase
    {
        [XmlSubElem]
        public IDatabaseSource Source { get; set; }

        [XmlSubElem]
        public IDatabaseSource Target { get; set; }

        public override Bitmap Image
        {
            get { return DbModIcons.synchronize; }
        }

        public override void Open()
        {
            SynchronizeStructureForm.Run(Source, Target);
        }

        public override string Description
        {
            get { return "s_synchronize_structure"; }
        }

        public override void DisplayProps(Action<string, string> display)
        {
            base.DisplayProps(display);
        }
    }

    public class SynchronizeExtData
    {
        public string[] Dbs;

        public string SelectedSource;
        public string SelectedTarget;

        public Func<string, IDatabaseSource> GetDb;
    }
}
