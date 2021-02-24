using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.Common;
using System.IO;
using System.Threading;
using System.Text.RegularExpressions;
using System.Xml;

namespace DatAdmin
{
    public partial class QueryFrame : ContentFrame
    {
        IPhysicalConnection m_conn;
        //DisposeList m_dispose = new DisposeList();
        OpenQueryParameters m_pars;
        bool m_initialized = false;
        //string m_filename = null;
        IVirtualFile m_file;
        QueryDesignFrame m_designFrame;
        IAsyncResult m_bgQuery;
        bool m_generatingSql = false;
        ReformatToolkit m_reformat;
        internal CachingLogger m_logger;
        IPhysicalConnection m_connToOpen;
        QueryExecuteParams m_currentQueryParams;
        DateTime? m_queryStart;
        TimeSpan? m_lastQueryLength;
        bool? m_lastQueryResult;
        DateTime? m_lastFileWrite;
        DbTransaction m_transaction;
        bool m_checkedTransaction;
        bool m_autoCommit;
        bool m_continueOnErrors;

        //public QueryFrame(IPhysicalConnection conn, OpenQueryParameters pars, IVirtualFolder vfolder)
        public QueryFrame(IPhysicalConnection conn, OpenQueryParameters pars)
        {
            InitializeComponent();

            OnlineHelpManager.RegisterHelpButton(btnOnlineHelp, "query");

            m_pars = pars;

            m_designFrame = new QueryDesignFrame(this);
            m_designFrame.Visible = false;
            this.Controls.Add(m_designFrame);
            m_designFrame.Dock = DockStyle.Fill;
            m_designFrame.RunSqlClick += new EventHandler(m_designFrame_RunSqlClick);
            m_designFrame.ShowSqlClick += new EventHandler(m_designFrame_ShowSqlClick);
            toolStrip2.Visible = LicenseTool.FeatureAllowed(QueryDesignerFeature.Test) && (m_pars == null || !m_pars.HideDesign);

            btnGetSchemaTable.Visible = VersionInfo.IsDevVersion;
            splitContainer1.Panel2Collapsed = true;
            m_connToOpen = conn;
            m_reformat = new ReformatToolkit(new GenericDialect(), tbquery);
            if (m_pars != null)
            {
                if (m_pars.GenerateSql != null && m_pars.GeneratingSql)
                {
                    tbquery.Enabled = false;
                    m_generatingSql = true;
                    tbquery.Modified = false;
                }
                if (m_pars.File != null) LoadFromFile(m_pars.File);
                if (m_pars.SqlText != null)
                {
                    tbquery.Text = m_pars.SqlText;
                    tbquery.Modified = false;
                }
                if (m_pars.SavedContext != null)
                {
                    LoadContext(m_pars.SavedContext);
                }
            }
            UpdateState();
            HLicense.ChangedLicenses += ReloadExtendableToolbar;
            Disposed += new EventHandler(QueryFrame_Disposed);

            ReloadExtendableToolbar();
        }

        void QueryFrame_Disposed(object sender, EventArgs e)
        {
            HLicense.ChangedLicenses -= ReloadExtendableToolbar;
        }

        List<ToolStripItem> m_extendableToolbarItems = new List<ToolStripItem>();
        private void ReloadExtendableToolbar()
        {
            foreach (var item in m_extendableToolbarItems) item.Dispose();
            m_extendableToolbarItems.Clear();
            foreach (var hold in MenuExtenderAddonType.Instance.CommonSpace.GetAllAddons())
            {
                var ext = hold.InstanceModel as IMenuExtender;
                ext.GetToolbarItems("query", m_extendableToolbarItems);
            }
            foreach (var item in m_extendableToolbarItems)
            {
                toolStrip1.Items.Add(item);
            }
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            if (m_connToOpen != null)
            {
                if (m_conn == null)
                {
                    SetConnection(m_connToOpen);
                }
                m_connToOpen = null;
            }
            if (m_pars.GoToDesign)
            {
                GoToQueryView(false);
            }
            if (m_pars.AddDesignTable != null)
            {
                m_designFrame.AddTable(m_pars.AddDesignTable);
            }
            if (m_pars.SavedDesign != null)
            {
                m_designFrame.Load(m_pars.SavedDesign.DocumentElement);
            }
            UpdateState();
        }

        public IPhysicalConnection Connection
        {
            get { return m_conn; }
        }

        private void SetConnection(IPhysicalConnection conn)
        {
            m_conn = conn;
            if (m_conn == null) return;
            conn.Owner = this;
            conn.InfoMessage += new InfoMessageDelegate(conn_InfoMessage);
            m_conn.BeginOpen(Async.CreateInvokeCallback(m_invoker, OpenedConnection));
            AfterChangeConnection();
        }

        private void AfterChangeConnection()
        {
            tbquery.Dialect = m_conn.GetAnyDialect();
            m_reformat = new ReformatToolkit(m_conn.GetAnyDialect(), tbquery);
            if (m_conn == null) tbxServer.Text = "";
            else if (m_conn.StoredConnection != null && m_conn.StoredConnection.FileName != null) tbxServer.Text = Path.GetFileNameWithoutExtension(m_conn.StoredConnection.FileName) ?? "";
            else tbxServer.Text = m_conn.ToString("S") ?? "";
            m_lastQueryResult = null;
            m_lastQueryLength = null;
            UpdateState();
        }

        [PopupMenu("s_close_query_connection", GroupName = "conn")]
        public void CloseQueryConnection()
        {
            resultTabs.TruncateTabs(1);
            m_designFrame.GetTabs().TruncateTabs(m_designFrame.BaseTabCount);
            splitContainer1.Panel2Collapsed = true;
            cbxDatabase.Enabled = false;
            cbxDatabase.Items.Clear();

            CloseDbConnection();
            //UpdateState();
        }

        [PopupMenuEnabled("s_show_code_completion")]
        [PopupMenuEnabled("s_insert_sql_join")]
        public bool CodeCompletionEnabled()
        {
            return tbquery.CompletationHandler != null && tbquery.CompletationHandler.CompletationEnabled && GetDbContext().Settings.CodeCompletion().UseCompletion;
        }

        [PopupMenu("s_show_code_completion", GroupName = "codecompletion", ShortcutDisplayString = "Ctrl+Space", RequiredFeature = CodeCompletionFeature.Test)]
        public void ShowCodeCompletion()
        {
            if (!CodeCompletionEnabled()) return;
            tbquery.CompletationHandler.ShowCompletationWindow();
        }

        [PopupMenu("s_insert_sql_join", GroupName = "codecompletion", ShortcutDisplayString = "Ctrl+J", RequiredFeature = CodeCompletionFeature.Test)]
        public void InsertJoin()
        {
            if (!CodeCompletionEnabled()) return;
            tbquery.CompletationHandler.ShowInsertJoin();
        }

        private void CloseDbConnection()
        {
            try
            {
                if (m_conn != null)
                {
                    if (m_transaction != null)
                    {
                        m_transaction.Dispose();
                        m_transaction = null;
                    }
                    IAsyncResult res = m_conn.BeginClose(null);
                    Async.WaitFor(res);
                    m_conn.EndClose(res);
                }
            }
            catch (Exception e)
            {
                Logging.Warning("Error closing connection:" + e.ToString());
            }
            m_conn = null;
            AfterChangeConnection();
            //UpdateState();
        }

        class Commenter
        {
            bool? addcomment = null;
            internal string ProcessLine(string line)
            {
                if (addcomment == null)
                {
                    addcomment = !line.StartsWith("--");
                }
                if (addcomment.Value)
                {
                    return "-- " + line;
                }
                else
                {
                    if (line.StartsWith("-- ")) return line.Substring(3);
                    if (line.StartsWith("--")) return line.Substring(2);
                    return line;
                }
            }
        }
        private string GetIndentString()
        {
            var opt = GlobalSettings.Pages.Editor();
            if (opt.UseTabs) return "\t";
            var sb = new StringBuilder();
            for (int i = 0; i < opt.TabWidth; i++) sb.Append(" ");
            return sb.ToString();
        }
        private string DoIndent(string line)
        {
            return GetIndentString() + line;
        }
        private string DoDeindent(string line)
        {
            string id = GetIndentString();
            if (line.StartsWith(id)) return line.Substring(id.Length);
            return line;
        }
        [PopupMenu("s_edit/s_edcomment", Shortcut = Keys.Shift | Keys.Control | Keys.A, GroupName = "edit")]
        public void Comment()
        {
            var com = new Commenter();
            tbquery.ForEachLine(com.ProcessLine);
        }
        [PopupMenu("s_edit/s_indent", Shortcut = Keys.Shift | Keys.Control | Keys.I, GroupName = "edit")]
        public void Indent()
        {
            tbquery.ForEachLine(DoIndent);
        }
        [PopupMenu("s_edit/s_deindent", Shortcut = Keys.Shift | Keys.Control | Keys.U, GroupName = "edit")]
        public void Deindent()
        {
            tbquery.ForEachLine(DoDeindent);
        }

        //[PopupMenu("s_change_query_context")]
        //public void ChangeQueryContext()
        //{
        //    CloseQueryConnection();
        //    IPhysicalConnection conn = MainWindowExtension.SelectedConnection;
        //    if (conn == null)
        //    {
        //        StdDialog.ShowInfo("s_query_context_not_set_please_select_connection");
        //    }
        //    if (conn != null) conn = conn.Clone();
        //    OpenQueryParameters pars = new OpenQueryParameters();
        //    string dbname = MainWindowExtension.SelectedDatabaseName;
        //    if (dbname != null) conn.AfterOpen += ConnTools.ChangeDatabaseCallback(dbname);
        //    m_pars = pars;
        //    m_initialized = false;
        //    SetConnection(conn);
        //}

        bool HasContextFile
        {
            get
            {
                return m_file != null && m_file.DataDiskPath != null && !m_pars.DisableContext && File.Exists(m_file.DataDiskPath + ".ctx");
            }
        }

        bool HasDesignFile
        {
            get
            {
                return m_file != null && m_file.DataDiskPath != null && File.Exists(m_file.DataDiskPath + ".design");
            }
        }

        private void LoadContext(XmlDocument doc)
        {
            var dbelem = doc.DocumentElement.FindElement("Database");
            if (dbelem != null)
            {
                var db = (IDatabaseSource)DatabaseSourceAddonType.Instance.LoadAddon(dbelem);
                db.Connection.AfterOpen += ConnTools.ChangeDatabaseCallback(db.DatabaseName);
                SetConnection(db.Connection);
            }
        }

        private void InvalidateCompletion()
        {
            if (tbquery.CompletationHandler != null) tbquery.CompletationHandler.InvalidateCompletion();
        }

        private void LoadFromFile(IVirtualFile file)
        {
            m_file = file;
            ChangedWinId();
            tbquery.Text = m_file.GetText();
            tbquery.Modified = false;
            InvalidateCompletion();
            if (m_file.DataDiskPath != null)
            {
                m_lastFileWrite = File.GetLastWriteTime(m_file.DataDiskPath);
            }
            else
            {
                m_lastFileWrite = null;
            }
            if (HasContextFile)
            {
                var doc = new XmlDocument();
                doc.Load(m_file.DataDiskPath + ".ctx");
                LoadContext(doc);
            }
            if (HasDesignFile)
            {
                m_designFrame.Load(m_file.DataDiskPath + ".design");
                GoToQueryView(false);
            }
        }

        void conn_InfoMessage(object sender, InfoMessageEventArgs e)
        {
            //string msg = String.Format("{0}: {1} [{2}]\n", e.Source, e.Code, e.Message);
            //LogMessage(e.Message);
            m_logger.LogMessageEx(new LogMessageRecord { Category = e.Source, Message = e.Message, Level = LogLevel.Info });
            //m_logger.LogMessage(null);
            //m_logger.Info(e.Message);
        }

        //private void LogMessage(string msg)
        //{
        //    LogMessage(msg, Color.Black);
        //}

        //private void LogMessage(string msg, Color color)
        //{
        //    if (IsHandleCreated)
        //    {
        //        Invoke((Action<string, Color>)DoLogMessage, msg, color);
        //    }
        //}

        //private void DoLogMessage(string msg, Color color)
        //{
        //    tbmessages.SelectionStart = tbmessages.TextLength;
        //    tbmessages.SelectionColor = color;
        //    tbmessages.SelectedText = msg + "\n";
        //    tbmessages.SelectionStart = tbmessages.TextLength;
        //    tbmessages.ScrollToCaret();
        //}

        class MatchEval
        {
            internal Dictionary<string, string> vars;
            internal string Replace(Match m)
            {
                string[] vals = m.Groups[1].Value.Split(new string[] { "||" }, StringSplitOptions.None); ;
                return vars[vals[0]];
            }
        }
        Dictionary<string, string> m_lastParams = new Dictionary<string, string>();

        public static string AskQueryParams(string sql, Dictionary<string, string> lastParams)
        {
            var re = new Regex(@"###(.+?)###");
            var vars = new Dictionary<string, string>();
            foreach (Match m in re.Matches(sql))
            {
                string[] vals = m.Groups[1].Value.Split(new string[] { "||" }, StringSplitOptions.None); ;
                if (!vars.ContainsKey(vals[0])) vars[vals[0]] = "";
                if (vals.Length > 1) vars[vals[0]] = vals[1];
            }
            if (vars.Count > 0)
            {
                if (lastParams != null) foreach (var tpl in lastParams) if (vars.ContainsKey(tpl.Key)) vars[tpl.Key] = tpl.Value;
                if (!QueryParamsForm.Run(vars)) return null;
                if (lastParams != null) lastParams.AddAll(vars);
                sql = re.Replace(sql, new MatchEval { vars = vars }.Replace);
            }
            return sql;
        }

        private void ExecuteQuery(string sql)
        {
            sql = AskQueryParams(sql, m_lastParams);
            if (sql == null) return;
            m_currentQueryParams = new QueryExecuteParams();
            m_currentQueryParams.Dialect = m_conn.Dialect;
            m_currentQueryParams.ExecutedAt = DateTime.Now;
            m_currentQueryParams.DbName = cbxDatabase.Text;
            m_currentQueryParams.DbServer = m_conn.ToString("S");
            m_currentQueryParams.FileName = m_file != null ? m_file.DataDiskPath : null;
            m_currentQueryParams.Sql = sql;
            m_currentQueryParams.QueryContext = GetContextFile().OuterXml;

            m_logger = new CachingLogger(LogLevel.All);
            ActiveLogger.Source = m_logger;
            SetChangedStateRedirect(null);
            ActiveTabControl.TruncateTabs(BaseTabCount);
            if (cbQueryBuilder.Checked)
            {
                if (!m_designFrame.CheckQuery(m_logger))
                {
                    m_designFrame.ShowMessagesTab();
                    return;
                }
            }
            if (!cbQueryBuilder.Checked) splitContainer1.Panel2Collapsed = false;
            timerQueryDuration.Enabled = true;
            if (btnTableStyle.Checked || cbQueryBuilder.Checked)
            {
                if (!QueryTools.IsSimpleSelect(sql)) m_logger.Warning("Executed query seems not to be browsable");
                m_conn.InvokeReconnectIfBroken();
                AddSimpleSelect(sql);
            }
            else
            {
                if (QueryTools.IsSimpleSelect(sql)) m_logger.Info("Executed query seems not be browsable, to better performance switch browsable result on");
                m_bgQuery = m_conn.BeginInvoke((Action<string>)ProcessQueryOnBackground, Async.CreateInvokeCallback(m_invoker, QueryFinished), sql);
            }
            //if (QueryTools.IsSimpleSelect(sql))
            //{
            //    m_conn.InvokeReconnectIfBroken();
            //    AddSimpleSelect(sql);
            //}
            //else
            //{
            //    m_bgQuery = m_conn.BeginInvoke((Action<string, bool>)ProcessQueryOnBackground, Async.CreateInvokeCallback(m_invoker, QueryFinished), sql, btnTransaction.Checked);
            //}
            UpdateState();
            //ITabularData tabdata = new GenericTabularData(m_conn, sql);
            //tableDataFrame1.TabularData = tabdata;
            //splitContainer1.Panel2Collapsed = false;
        }

        bool IsBackgroundWork()
        {
            if (m_lastDataFrame != null)
            {
                return m_lastDataFrame.IsLoadingData;
            }
            else
            {
                return m_bgQuery != null;
            }
        }

        [PopupMenu("s_find", Shortcut = Keys.Control | Keys.F, ImageName = CoreIcons.findName, GroupName = "edit")]
        public void ShowFindDialog()
        {
            tbquery.ShowFindDialog();
        }

        [PopupMenu("s_replace", Shortcut = Keys.Control | Keys.H, GroupName = "edit")]
        public void ShowReplaceDialog()
        {
            tbquery.ShowReplaceDialog();
        }

        [PopupMenu("s_find_next", Shortcut = Keys.F3, GroupName = "edit")]
        public void FindNext()
        {
            tbquery.FindNext();
        }

        //[PopupMenuEnabled("s_execute")]
        public bool ExecuteEnabled()
        {
            return !IsBackgroundWork();
        }

        [PopupMenu("s_execute", Shortcut = Keys.F9, ImageName = CoreIcons.query_executeName, GroupName = "execute")]
        public void Execute()
        {
            if (!ExecuteEnabled()) return;
            if (cbQueryBuilder.Checked)
            {
                ExecuteDesigned();
                return;
            }
            if (m_conn == null) return;
            string sql = tbquery.GetSelectedText();
            if (!String.IsNullOrEmpty(sql)) ExecuteQuery(sql);
            else ExecuteQuery(tbquery.Text);
        }

        //[PopupMenuEnabled("s_execute_line")]
        public bool ExecuteLineEnabled()
        {
            return ExecuteEnabled();
        }

        [PopupMenu("s_execute_line", Shortcut = Keys.F6, GroupName = "execute")]
        public void ExecuteLine()
        {
            if (!ExecuteEnabled()) return;
            if (m_conn == null) return;
            string sql = tbquery.GetSelectedLine();
            if (!String.IsNullOrEmpty(sql))
            {
                ExecuteQuery(sql);
            }
        }

        [PopupMenu("s_execute_to_cursor", Shortcut = Keys.Shift | Keys.F6, GroupName = "execute")]
        public void ExecuteToCursor()
        {
            if (!ExecuteEnabled()) return;
            if (m_conn == null) return;
            string sql = tbquery.GetTextBeforeCursor();
            if (!String.IsNullOrEmpty(sql))
            {
                ExecuteQuery(sql);
            }
        }

        [PopupMenu("s_execute_from_cursor", Shortcut = Keys.Control | Keys.F6, GroupName = "execute")]
        public void ExecuteFromCursor()
        {
            if (!ExecuteEnabled()) return;
            if (m_conn == null) return;
            string sql = tbquery.GetTextAfterCursor();
            if (!String.IsNullOrEmpty(sql))
            {
                ExecuteQuery(sql);
            }
        }

        //[PopupMenuEnabled("s_cancel")]
        public bool CancelQueryEnabled()
        {
            return m_bgQuery != null && m_bgQuery is ICancelable && ((ICancelable)m_bgQuery).CanCancel;
        }

        [PopupMenu("s_cancel", ImageName = CoreIcons.stopName, GroupName = "conn")]
        public void CancelQuery()
        {
            if (!CancelQueryEnabled()) return;
            try
            {
                ((ICancelable)m_bgQuery).Cancel();
            }
            catch (Exception e)
            {
                Errors.Report(e);
            }
        }

        [PopupMenu("s_export", ImageName = CoreIcons.exportName, GroupName = "conn")]
        public void DoExport()
        {
            ITabularDataView data = resultTabs.SelectedTab.Tag as ITabularDataView;
            if (data != null)
            {
                BulkCopyWizard.Run(data.GetStoreAndClone(null), null);
            }
        }

        //[PopupMenuVisible("s_reformat")]
        //public bool ReformatVisible()
        //{
        //    return VersionInfo.IsDevVersion;
        //}

        //[PopupMenu("s_reformat", Shortcut = Keys.Control | Keys.R)]
        //public void Reformat()
        //{
        //    m_reformat.Reformat();
        //}

        //[PopupMenu("s_save_to_scripts_folder")]
        //public void SaveToScriptsFolder()
        //{
        //    saveFileDialog1.InitialDirectory = Core.ScriptsDirectory;
        //    if (saveFileDialog1.ShowDialogEx() == DialogResult.OK)
        //    {
        //        m_file = new FileSystemFile(saveFileDialog1.FileName);
        //        Save();
        //        UpdateTitle();
        //        tbquery.Modified = false;
        //        HTree.CallRefreshRoot();
        //    }
        //}

        private Job CreateJob()
        {
            //return new RunSqlJobCommand(m_conn.StoredConnection, cbxDatabase.SelectedText, tbquery.Text);
            return RunSqlJob.CreateJob(m_conn.StoredConnection, cbxDatabase.Text, tbquery.Text);
        }

        //[PopupMenuEnabled("s_save_as_job")]
        public bool SaveAsJobEnabled()
        {
            return m_conn != null && m_conn.StoredConnection != null;
        }

        [PopupMenu("s_save_as_job", ImageName = CoreIcons.jobName, GroupName = "conn")]
        public void SaveAsJob()
        {
            Job.AskAndExportToFile(CreateJob);
        }

        private void UpdateState()
        {
            btnExecute.Enabled = ExecuteEnabled();
            btnCancel.Enabled = CancelQueryEnabled();
            btnTransaction.Enabled = ExecuteEnabled();
            IsLoadingIcon = IsBackgroundWork() || m_generatingSql;
            if (m_generatingSql) labStatus.Text = Texts.Get("s_generating_sql");
            else if (IsBackgroundWork()) labStatus.Text = Texts.Get("s_loading");
            else
            {
                if (m_lastQueryResult == null)
                {
                    labStatus.Text = Texts.Get("s_connected");
                    labStatus.Image = CoreIcons.connect;
                }
                else if (m_lastQueryResult == true)
                {
                    labStatus.Text = Texts.Get("s_finished");
                    labStatus.Image = CoreIcons.ok;
                }
                else if (m_lastQueryResult == false)
                {
                    labStatus.Text = Texts.Get("s_error_finished");
                    labStatus.Image = CoreIcons.error;
                }
            }
            statusStrip2.Visible = m_conn != null;
            if (statusStrip2.Visible) statusStrip2.SendToBack();
            if (m_conn != null)
            {
                labServer.Text = tbxServer.Text;
                labUser.Text = "";
                if (m_conn.StoredConnection != null && m_conn.StoredConnection.GetLogin() != null)
                {
                    labUser.Text = m_conn.StoredConnection.GetLogin();
                }
                labTime.Text = "";
                if (m_lastQueryLength != null) labTime.Text = m_lastQueryLength.Value.NiceFormat();
            }
        }

        public override bool RequiresLoadingAnimation()
        {
            return true;
        }

        public override void SetLoadingAnimationIcon(Bitmap bmp, Icon icon)
        {
            if (IsLoadingIcon) labStatus.Image = bmp;
        }

        //[PopupMenu("s_save_as")]
        public override bool SupportsSaveAs { get { return true; } }
        public override bool SaveAs()
        {
            if (!Directory.Exists(Core.ScriptsDirectory)) Directory.CreateDirectory(Core.ScriptsDirectory);
            saveFileDialog1.InitialDirectory = Core.ScriptsDirectory;
            saveFileDialog1.CustomPlaces.Add(Core.ScriptsDirectory);
            if (saveFileDialog1.ShowDialogEx() == DialogResult.OK)
            {
                m_file = new DiskFile(saveFileDialog1.FileName);
                ChangedWinId();
                Save();
                UpdateTitle();
                tbquery.Modified = false;
                if (!IOTool.RelativePathTo(Core.ScriptsDirectory, saveFileDialog1.FileName).StartsWith(".."))
                {
                    HTree.CallRefreshRoot();
                }
                return true;
            }
            return false;
        }

        private void ChangedWinId()
        {
            WinId = null;
            if (m_file != null && m_file.DataDiskPath != null)
            {
                WinId = IOTool.NormalizePath(m_file.DataDiskPath) + "#query";
            }
        }

        public override bool SupportsSave { get { return true; } }
        public override bool Save()
        {
            if (m_file == null)
            {
                return SaveAs();
            }
            else
            {
                DoSave();
                return true;
            }
        }

        private bool IsInScriptsFolder
        {
            get
            {
                var realf = m_file as DiskFile;
                return realf != null && !IOTool.RelativePathTo(Core.ScriptsDirectory, realf.Name).StartsWith("..");
            }
        }

        private void DoSave()
        {
            m_file.SaveText(tbquery.Text);
            tbquery.Modified = false;
            if (m_file.DataDiskPath != null)
            {
                m_lastFileWrite = File.GetLastWriteTime(m_file.DataDiskPath);
                if (!m_pars.DisableContext && (IsInScriptsFolder || HasContextFile))
                {
                    SaveContext();
                }
                if (m_designFrame != null && m_designFrame.IsDesign)
                {
                    m_designFrame.Save(m_file.DataDiskPath + ".design");
                }
            }
        }

        private IDatabaseSource GetDbContext()
        {
            if (m_conn != null)
            {
                var db = new GenericDatabaseSource(null, m_conn, cbxDatabase.Text);
                return db;
            }
            return null;
        }

        private XmlDocument GetContextFile()
        {
            var doc = XmlTool.CreateDocument("Context");
            var db = GetDbContext();
            if (db != null)
            {
                db.SaveToXml(doc.DocumentElement.AddChild("Database"));
            }
            return doc;
        }

        [PopupMenuVisible("s_save_query_context")]
        public bool SaveContextVisible()
        {
            return !m_pars.DisableContext;
        }

        [PopupMenu("s_save_query_context", GroupName = "conn")]
        public void SaveContext()
        {
            if (m_file == null)
            {
                StdDialog.ShowError("s_please_save_query_first");
                return;
            }
            var doc = GetContextFile();
            doc.Save(m_file.DataDiskPath + ".ctx");
        }

        private void ProcessReaderOnBackground(IDataReader reader, DateTime start)
        {
            do
            {
                bool logged = false;
                if (reader.FieldCount > 0)
                {
                    using (var bedread = m_conn.GetAnyDDA().AdaptReader(reader) as DataReaderAdapter)
                    {
                        bedread.DisposeReader = false;
                        ITableStructure schema = bedread.Structure;

                        if (schema != null)
                        {
                            //GridTable gt = new GridTable(DbStructureFactory.TableFromGetSchema(null, schema, m_conn.Dialect));
                            GridTable gt = new GridTable(schema, Texts.Get("s_query"));
                            gt.Dialect = m_conn.Dialect;
                            gt.Fill(bedread);

                            double len = (DateTime.Now - start).TotalSeconds;
                            var table = gt.GetStructure(null);
                            if (table.Columns.Count > 0)
                            {
                                logged = true;
                                m_logger.Debug(Texts.Get("s_readed$rows$cols$len",
                                    "rows", gt.RowCount,
                                    "cols", table.Columns.Count,
                                    "len", len.ToString("0.00")));
                                InvokeAddTabularData(gt);
                            }
                        }
                    }
                }
                if (!logged)
                {
                    double len = (DateTime.Now - start).TotalSeconds;
                    m_logger.Debug(Texts.Get("s_query_executed$affected$len",
                        "affected", reader.RecordsAffected,
                        "len", len.ToString("0.00")));
                }
                start = DateTime.Now;
            }
            while (reader.NextResult());
        }

        private void ProcessQueryOnBackground(string query)
        {
            m_conn.ReconnectIfBroken(m_transaction);
            if (m_transaction == null && m_checkedTransaction)
            {
                m_transaction = m_conn.SystemConnection.BeginTransaction();
                Invoke((Action)UpdateTransEnabling);
            }
            DateTime fullstart = DateTime.Now;
            m_queryStart = fullstart;
            int okcount = 0, failcount = 0;
            bool isdump = false;
            try
            {
                if (m_conn.GetAnyDialect().QueryIsDump(query) || query.StartsWith("--DUMP"))
                {
                    isdump = true;
                    var loader = m_conn.Dialect.CreateDumpLoader();
                    loader.ProgressInfo = new QueryFrameProgressInfo(this);
                    loader.Connection = m_conn.SystemConnection;
                    loader.Transaction = m_transaction;
                    m_logger.Debug(Texts.Get("s_importing_sql_dump"));
                    loader.Run(new StringReader(query));
                }
                else
                {
                    foreach (var item in QueryTools.GoSplit(query))
                    {
                        m_logger.Debug(Texts.Get("s_executing$query", "query", item));
                        try
                        {
                            DateTime start = DateTime.Now;
                            using (IDataReader reader = m_conn.SystemConnection.RunOneSqlCommandReader(m_conn.Dialect, item.Data, m_transaction, GlobalSettings.Pages.Connection().CommandTimeout, m_conn))
                            {
                                ProcessReaderOnBackground(reader, start);
                            }
                            okcount++;
                        }
                        catch (Exception e)
                        {
                            LogError(e, item.StartLine);
                            failcount++;
                            if (m_transaction != null && (!m_continueOnErrors || m_autoCommit))
                            {
                                double len0 = (DateTime.Now - fullstart).TotalSeconds;
                                m_currentQueryParams.DurationInMilisecs = (int)(len0 * 1000);
                                HQuery.CallQueryExecute(m_currentQueryParams);
                                if (m_autoCommit)
                                {
                                    m_transaction.Rollback();
                                    m_logger.Error(Texts.Get("s_transaction_rollback_nothing_changed"));
                                }
                                return;
                            }
                            //if (tran != null)
                            //{
                            //    tran.Rollback();
                            //    m_logger.Error(Texts.Get("s_transaction_rollback_nothing_changed"));
                            //    return;
                            //}
                        }
                    }
                }
                if (m_transaction != null && m_autoCommit) m_transaction.Commit();
            }
            finally
            {
                if (m_transaction != null && m_autoCommit)
                {
                    m_transaction.Dispose();
                    m_transaction = null;
                }
            }
            var slen = DateTime.Now - fullstart;
            double len = slen.TotalSeconds;
            m_queryStart = null;
            m_lastQueryLength = slen;
            m_lastQueryResult = failcount == 0;
            m_currentQueryParams.DurationInMilisecs = (int)(len * 1000);
            if (isdump)
            {
                m_logger.Debug(Texts.Get("s_sql_dump_imported$len",
                    "len", len.ToString("0.00")));
            }
            else
            {
                m_logger.Debug(Texts.Get("s_query_executed$okcount$failcount$len",
                    "okcount", okcount,
                    "failcount", failcount,
                    "len", len.ToString("0.00")));
            }
            HQuery.CallQueryExecute(m_currentQueryParams);
        }

        private void LogError(Exception e, int startLine)
        {
            e = m_conn.Dialect.TranslateException(e);
            DatabaseError dberr = e as DatabaseError;
            if (dberr != null)
            {
                bool skip = dberr.Items.Count == 1 && dberr.Items[0].Message == dberr.Message;
                if (!skip)
                {
                    var rec = new LogMessageRecord
                    {
                        Level = LogLevel.Error,
                        Message = String.Format("{0}: [#{1}]: {2}",
                        Texts.Get("s_query_at$line", "line", startLine),
                        dberr.ErrorCode, dberr.Message)
                    };
                    rec.CustomData["line"] = startLine.ToString();
                    m_logger.LogMessageEx(rec);
                }
                foreach (var item in dberr.Items)
                {
                    string procdata = "";
                    if (!String.IsNullOrEmpty(item.Procedure)) procdata = String.Format(" (procedure {0})", item.Procedure);
                    var rec = new LogMessageRecord
                    {
                        Level = LogLevel.Error,
                        Message = String.Format("{0}{1} {2}{4}: {3}", skip ? "" : "--", Texts.Get("s_line"), item.LineNumber + startLine + 1, item.Message, procdata)
                    };
                    rec.CustomData["line"] = (startLine + item.LineNumber).ToString();
                    m_logger.LogMessageEx(rec);
                }
            }
            else
            {
                var rec = new LogMessageRecord
                {
                    Level = LogLevel.Error,
                    Message = String.Format("{0}: {1}", Texts.Get("s_query_at$line", "line", startLine), e.Message)
                };
                rec.CustomData["line"] = startLine.ToString();
                m_logger.LogMessageEx(rec);
            }
        }

        private void InvokeAddTabularData(ITabularDataView data)
        {
            Invoke((Action<ITabularDataView, bool>)AddTabularData, data, false);
        }


        TableDataFrame m_lastDataFrame;
        private void SetChangedStateRedirect(TableDataFrame tdf)
        {
            if (m_lastDataFrame != null) m_lastDataFrame.ChangedDataState -= new EventHandler(SimpleDataFrame_ChangedDataState);
            m_lastDataFrame = tdf;
            if (m_lastDataFrame != null) m_lastDataFrame.ChangedDataState += new EventHandler(SimpleDataFrame_ChangedDataState);
        }

        void SimpleDataFrame_ChangedDataState(object sender, EventArgs e)
        {
            UpdateState();
        }

        private TabControl ActiveTabControl
        {
            get
            {
                return cbQueryBuilder.Checked ? m_designFrame.GetTabs() : resultTabs;
            }
        }

        private MessageLogFrame ActiveLogger
        {
            get
            {
                return cbQueryBuilder.Checked ? m_designFrame.MessageLog : messageLogFrame1;
            }
        }

        private int BaseTabCount
        {
            get
            {
                return cbQueryBuilder.Checked ? m_designFrame.BaseTabCount : 1;
            }
        }

        private void AddTabularData(ITabularDataView data, bool issimple)
        {
            TabControl tabs = ActiveTabControl;
            TableDataFrame tdf = new TableDataFrame();
            if (issimple) SetChangedStateRedirect(tdf);
            tdf.ReportError += new ReportErrorDelegate(tdf_ReportError);
            tabs.Enabled = false;
            TabPage page = new TabPage();
            page.Tag = data;
            page.ImageIndex = 0;
            page.Text = Texts.Get("s_result");
            page.Controls.Add(tdf);
            tabs.TabPages.Add(page);
            tdf.TabularData = data;
            tdf.Dock = DockStyle.Fill;
            if (tabs.TabPages.Count == BaseTabCount + 1) tabs.SelectedIndex = BaseTabCount;
            tabs.Enabled = true;
            if (!cbQueryBuilder.Checked) tbquery.Focus();
        }

        void tdf_ReportError(object sender, ReportErrorEventArgs e)
        {
            ActiveTabControl.Enabled = false;
            var removePages = new List<TabPage>();
            foreach (TabPage tab in ActiveTabControl.TabPages)
            {
                if (tab.Controls.Contains(sender as Control))
                {
                    removePages.Add(tab);
                }
            }
            MainWindow.Instance.RunInMainWindow(() =>
            {
                foreach (var tab in removePages)
                {
                    ActiveTabControl.TabPages.Remove(tab);
                    tab.Dispose();
                }
            });
            LogError(e.Error, 0);
            ActiveTabControl.SelectedIndex = 0;
            ActiveTabControl.Enabled = true;
            if (cbQueryBuilder.Checked)
            {
                m_designFrame.ShowMessagesTab();
            }
            else
            {
                tbquery.Focus();
            }
            m_lastQueryResult = false;
        }

        private void AddSimpleSelect(string sql)
        {
            m_lastQueryResult = null;
            m_lastQueryLength = null;
            m_queryStart = DateTime.Now;
            timerQueryDuration.Enabled = true;
            string dbname = m_conn.SystemConnection.Database;
            var tabdata = new GenericTabularDataView(m_conn, dbname, sql, "select count(*) from (" + sql + ") tcounted", null, Texts.Get("s_query"), false, false, null, null);
            tabdata.ProgressInfo = new QueryFrameProgressInfo(this);
            tabdata.LoadedDataInfo += new LoadedTableInfoDelegate(tabdata_LoadedDataInfo);
            AddTabularData(tabdata, true);
            HQuery.CallQueryExecute(m_currentQueryParams);
        }

        void tabdata_LoadedDataInfo(object sender, LoadedTableInfoArgs e)
        {
            timerQueryDuration.Enabled = false;
            m_queryStart = null;
            m_lastQueryLength = e.Duration;
            m_lastQueryResult = e.Error == null;
        }

        private void QueryFinished(IAsyncResult async)
        {
            try
            {
                m_bgQuery = null;
                m_conn.EndInvoke(async);
                string curdb = m_conn.Dialect.GetCurrentDatabase(m_conn);
                if (curdb != null)
                {
                    cbxDatabase.SelectedIndex = cbxDatabase.Items.IndexOf(curdb);
                    cbxDatabase.ToolTipText = curdb ?? "";
                    labDatabase.Text = curdb ?? "";
                }
                else
                {
                    // select database, which was selected before
                    lbdatabase_SelectedIndexChanged(this, EventArgs.Empty);
                }
                if (m_pars != null && m_pars.ExecutedCallback != null) m_pars.ExecutedCallback();
            }
            catch (ThreadAbortException e)
            {
                Logging.Debug("Query canceled by used");
                Errors.ReportSilent(e);
            }
            catch (Exception e)
            {
                cbxDatabase.SelectedIndex = -1;
                cbxDatabase.ToolTipText = "";
                labDatabase.Text = "";
                Errors.ReportSilent(e);
            }
            timerQueryDuration.Enabled = false;
            UpdateState();
        }

        public override string MenuBarTitle
        {
            get { return "s_query"; }
        }

        /*
        private void QueryFrame_Load(object sender, EventArgs e)
        {
            tbquery.Seperators.Add(' ');
            tbquery.Seperators.Add('\r');
            tbquery.Seperators.Add('\n');
            tbquery.Seperators.Add(',');
            tbquery.Seperators.Add('.');
            tbquery.Seperators.Add('-');
            tbquery.Seperators.Add('+');

            string[] keywords = new string[] { 
                "select", "update", "from", "where", "order", "by", 
                "create", "table", "column", "delete", "database", "alter", "add", "remove",
                "index", "constraint", "drop", "primary", "foreign", "key",
                "inner", "outer", "full", "left", "right", "join", "on", "cross", "natural"};
            foreach (string kw in keywords)
            {
                tbquery.HighlightDescriptors.Add(new HighlightDescriptor(kw, Color.Blue, null, DescriptorType.Word, DescriptorRecognition.WholeWord, true));
            }
        }
        */

        private void OpenedConnection(IAsyncResult async)
        {
            try
            {
                m_conn.EndOpen(async);
                m_designFrame.NotifyChangedConnection();
                if (!m_initialized)
                {
                    if (m_pars != null && (m_pars.GeneratingSql || m_pars.GenerateSql != null)) labStatus.Text = Texts.Get("s_generating_sql");
                    m_conn.BeginInvoke((Action)DoInitialize, m_invoker.CreateInvokeCallback(Initialized));
                }
                else
                {
                    InvalidateCompletion();
                }
            }
            catch (Exception e)
            {
                Errors.Report(e);
                CloseDbConnection();
            }
            UpdateTitle();
        }

        private void DoInitialize()
        {
            try
            {
                string cmd = null;
                if (m_pars != null && m_pars.GenerateSql != null) cmd = m_pars.GenerateSql(m_conn);
                if (cmd != null)
                {
                    tbquery.Invoke((Action)delegate()
                    {
                        tbquery.Text = cmd;
                        tbquery.Enabled = true;
                        tbquery.Modified = false;
                    });
                    m_generatingSql = false;
                }
                try
                {
                    if (m_conn.StoredConnection != null && m_conn.StoredConnection.DatabaseMode != ConnectionDatabaseMode.All)
                    {
                        Invoke((Action)delegate()
                        {
                            cbxDatabase.Enabled = false;
                        });
                    }
                    else
                    {
                        DoReloadDatabases();
                    }
                }
                catch (Exception)
                {
                    Invoke((Action)delegate() { cbxDatabase.Enabled = false; });
                }
            }
            catch (Exception e)
            {
                tbquery.Invoke((Action)delegate()
                {
                    Errors.Report(e);
                    tbquery.Text = "[" + Texts.Get("s_error") + "]";
                    tbquery.Modified = false;
                });
            }
        }

        private void DoReloadDatabases()
        {
            List<string> dbs = m_conn.Dialect.GetDatabaseNames(m_conn);
            Invoke((Action)delegate()
            {
                cbxDatabase.Items.Clear();
                foreach (string db in dbs.Sorted())
                {
                    cbxDatabase.Items.Add(db);
                }
                string curdb = m_conn.Dialect.GetCurrentDatabase(m_conn);
                if (curdb == null) cbxDatabase.SelectedIndex = -1;
                else cbxDatabase.SelectedIndex = cbxDatabase.Items.IndexOf(curdb);
                cbxDatabase.Enabled = true;
                cbxDatabase.ToolTipText = curdb ?? "";
                labDatabase.Text = curdb ?? "";
            });
        }

        private void Initialized(IAsyncResult async)
        {
            m_initialized = true;
            try
            {
                m_conn.EndInvoke(async);
            }
            catch (Exception e)
            {
                Errors.Report(e);
            }
            UpdateState();
            InvalidateCompletion();
        }

        public override void OnClose()
        {
            base.OnClose();
            CloseDbConnection();
        }

        public override string PageTitle
        {
            get
            {
                if (m_file != null) return m_file.Name;
                return String.Format("{0:S}", m_conn);
            }
        }

        public override string PageToolTip
        {
            get
            {
                string res = String.Format("{0:M}", m_conn);
                if (m_file != null) res += "\n" + m_file.Name;
                return res;
            }
        }

        public override string PageTypeTitle
        {
            get
            {
                return "s_query";
            }
        }

        private void lbdatabase_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(cbxDatabase.Text))
            {
                try
                {
                    cbxDatabase.ToolTipText = cbxDatabase.Text;
                    m_conn.SystemConnection.ChangeDatabase(cbxDatabase.Text);
                    labDatabase.Text = cbxDatabase.Text;
                }
                catch
                {
                    Async.SafeReconnect(m_conn);
                    m_conn.BeginInvoke((Action)DoReloadDatabases, null);
                    StdDialog.ShowError("s_connection_broken_reconnected");
                }
            }
            if (m_designFrame != null && m_designFrame.IsHandleCreated) m_designFrame.RefreshTables();
        }

        public override bool AllowClose()
        {
            if (tbquery.Modified)
            {
                DialogResult dr = MessageBox.Show(Texts.Get("s_file_modified_save$file", "file", m_file != null ? m_file.Name : "???"), VersionInfo.ProgramTitle, MessageBoxButtons.YesNoCancel);
                if (dr == DialogResult.Yes)
                {
                    return Save();
                }
                if (dr == DialogResult.No) return true;
                return false;
            }
            return true;
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Execute();
        }

        //string DoInsertSqlFragment(Func<IPhysicalConnection, string> func)
        //{
        //    string sql = func(m_conn);
        //    return sql;
        //}

        public void GeneratedSqlFragment(IAsyncResult res)
        {
            try
            {
                string sql = (string)m_conn.EndInvoke(res);
                tbquery.Enabled = true;
                tbquery.InsertTextOnCursor(sql);
            }
            catch (Exception e)
            {
                Errors.Report(e);
            }
            m_generatingSql = false;
            tbquery.Enabled = true;
            UpdateState();
        }

        public void InsertSqlFragment(string sql)
        {
            tbquery.InsertTextOnCursor(sql);
            UpdateState();
        }

        void m_designFrame_ShowSqlClick(object sender, EventArgs e)
        {
            tbquery.Text = m_designFrame.GenerateSql();
            GoToQueryView(true);
        }

        void m_designFrame_RunSqlClick(object sender, EventArgs e)
        {
            ExecuteDesigned();
        }

        private void ExecuteDesigned()
        {
            string query = m_designFrame.GenerateSql();
            ExecuteQuery(query);
        }

        private void cbQueryCode_Click(object sender, EventArgs e)
        {
            GoToQueryView(true);
        }

        private void cbQueryBuilder_Click(object sender, EventArgs e)
        {
            GoToQueryView(false);
        }

        private void GoToQueryView(bool iscode)
        {
            if (!LicenseTool.FeatureAllowed(QueryDesignerFeature.Test)) iscode = true;
            cbQueryCode.Checked = iscode;
            cbQueryBuilder.Checked = !iscode;
            splitContainer1.Visible = iscode;
            m_designFrame.Visible = !iscode;
            toolStrip1.SendToBack();
            toolStrip2.SendToBack();
            btnTableStyle.Enabled = iscode;
            btnInsertVariable.Enabled = iscode;
            statusStrip2.SendToBack();
        }

        public override Bitmap Image
        {
            get { return CoreIcons.sql; }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            CancelQuery();
        }

        //private void UpdatePosition()
        //{
        //    labLine.Text = String.Format("{0}:{1}", Texts.Get("s_line"), tbquery.GetLine() + 1);
        //    labCol.Text = String.Format("{0}:{1}", Texts.Get("s_column"), tbquery.GetColumn() + 1);
        //}

        //private void tbquery_ChangedPosition(object sender, EventArgs e)
        //{
        //    UpdatePosition();
        //}

        //private void tbmessages_DoubleClick(object sender, EventArgs e)
        //{
        //    string line = tbmessages.GetCurrentLineData();
        //    if (line != null)
        //    {
        //        var m = Regex.Match(line, @"\[(\d+)\]:");
        //        if (m.Success)
        //        {
        //            int linenum = Int32.Parse(m.Groups[1].Value) - 1;
        //            tbquery.GoToLine(linenum);
        //            tbquery.Focus();
        //            UpdatePosition();
        //        }
        //    }
        //}

        private void btnGetSchemaTable_Click(object sender, EventArgs e)
        {
            TabPage page = new TabPage();
            page.Text = "GetSchemaTable";
            var cmd = m_conn.SystemConnection.CreateCommand();
            cmd.CommandText = tbquery.Text;
            using (var reader = cmd.ExecuteReader(CommandBehavior.SchemaOnly | CommandBehavior.KeyInfo))
            {
                DataGridView tbl = new DataGridView();
                page.Controls.Add(tbl);
                tbl.Dock = DockStyle.Fill;
                tbl.DataSource = reader.GetSchemaTable();
            }
            splitContainer1.Panel2Collapsed = false;
            resultTabs.TabPages.Add(page);
        }

        private void messageLogFrame1_MessageDoubleClick(object sender, LogMessageEventArgs e)
        {
            var line = e.LogRecord.CustomData.Get("line");
            if (line != null)
            {
                int linenum = Int32.Parse(line);
                tbquery.GoToLine(linenum);
                tbquery.Focus();
                //UpdatePosition();
                e.Handled = true;
            }
        }

        private void btnChangeServer_Click(object sender, EventArgs e)
        {
            var newconn = TreeSelectForm.SelectQueryableConnection();
            if (newconn == null) return;

            CloseQueryConnection();
            if (newconn != null) newconn = newconn.Clone();
            OpenQueryParameters pars = new OpenQueryParameters();
            m_pars = pars;
            m_initialized = false;
            SetConnection(newconn);
        }

        [PopupMenu("s_insert_variable", ImageName = CoreIcons.variableName, GroupName = "edit")]
        public void InsertVariable()
        {
            tbquery.InsertTextOnCursor("###NAME||VALUE###");
        }

        private void btnInsertVariable_Click(object sender, EventArgs e)
        {
            InsertVariable();
        }

        private void btnAddToFavorite_Click(object sender, EventArgs e)
        {
            AddQueryToFavoriteForm.Run(GetContextFile(), m_file, m_designFrame, tbquery.Text, cbQueryBuilder.Checked);
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tbquery_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                Execute();
            }
        }

        public void GenerateSqlFinished(string sqltext)
        {
            tbquery.Text = sqltext;
            tbquery.Enabled = true;
            tbquery.Modified = false;
            m_generatingSql = false;
            UpdateState();
        }

        private void statusStrip2_Resize(object sender, EventArgs e)
        {
            statusStrip2.ResizeControlToWidth();
        }

        private void timerQueryDuration_Tick(object sender, EventArgs e)
        {
            labTime.Text = "";
            if (m_queryStart != null)
            {
                var delta = DateTime.Now - m_queryStart.Value;
                labTime.Text = delta.NiceFormat();
            }
        }

        public static bool FindOpenedFile(string filename)
        {
            if (filename == null) return false;
            string winid = IOTool.NormalizePath(filename) + "#query";
            if (MainWindow.Instance.HasContent(winid))
            {
                MainWindow.Instance.ActivateContent(winid);
                return true;
            }
            return false;
        }

        bool m_processingActivateEvent = false;
        public override void OnAppActivate()
        {
            base.OnAppActivate();

            if (m_processingActivateEvent) return;
            m_processingActivateEvent = true;
            try
            {
                if (m_lastFileWrite != null && m_file != null && m_file.DataDiskPath != null && m_lastFileWrite.Value != File.GetLastWriteTime(m_file.DataDiskPath))
                {
                    if (StdDialog.YesNoDialog("s_file_changed_reload$file", "file", m_file.DataDiskPath))
                    {
                        LoadFromFile(m_file);
                    }
                    else
                    {
                        m_lastFileWrite = File.GetLastWriteTime(m_file.DataDiskPath);
                    }
                }
            }
            finally
            {
                m_processingActivateEvent = false;
            }
        }

        private void btnTransaction_Click(object sender, EventArgs e)
        {
            toolStripTransaction.Visible = btnTransaction.Checked;
            m_checkedTransaction = btnTransaction.Checked;
            toolStrip1.SendToBack();
            UpdateTransEnabling();
        }

        private void UpdateTransEnabling()
        {
            btnRollback.Enabled = btnCommit.Enabled = !m_autoCommit && m_transaction != null;
        }

        private void btnAutoCommit_Click(object sender, EventArgs e)
        {
            m_autoCommit = btnAutoCommit.Checked;
        }

        private void btnContinueOnErrors_Click(object sender, EventArgs e)
        {
            m_continueOnErrors = btnContinueOnErrors.Checked;
        }

        private void btnCommit_Click(object sender, EventArgs e)
        {
            m_conn.BeginInvoke((Action)DoCommit, m_invoker.CreateInvokeCallback(CommitedOrRollbacked));
        }

        private void btnRollback_Click(object sender, EventArgs e)
        {
            m_conn.BeginInvoke((Action)DoRollback, m_invoker.CreateInvokeCallback(CommitedOrRollbacked));
        }

        private void DoCommit()
        {
            Errors.CheckNotNull("DAE-00361", m_transaction);
            m_transaction.Commit();
            m_transaction.Dispose();
            m_transaction = null;
            m_logger.Info("COMMIT");
        }

        private void DoRollback()
        {
            Errors.CheckNotNull("DAE-00362", m_transaction);
            try
            {
                m_transaction.Rollback();
            }
            catch (Exception err)
            {
                m_transaction.Dispose();
                m_transaction = null;
                err.Data["errcode"] = "DAE-00363";
                throw err;
            }
            m_transaction.Dispose();
            m_transaction = null;
            m_logger.Info("ROLLBACK");
        }

        private void CommitedOrRollbacked(IAsyncResult async)
        {
            try
            {
                m_conn.EndInvoke(async);
            }
            catch (Exception err)
            {
                Errors.Report(err);
            }
            UpdateTransEnabling();
        }

        private void tbquery_DatabaseConnectionNeeded(object sender, EventArgs e)
        {
            tbquery.Connection = GetDbContext();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            var mb = new MenuBuilder();
            mb.AddObject(this);
            contextMenuStrip1.Items.Clear();
            mb.GetMenuItems(contextMenuStrip1.Items);
            e.Cancel = false;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            Save();
        }
    }

    //public class QueryContext
    //{
    //    public IStoredConnection Connection;
    //    [XmlElem]
    //    public string Database { get; set; }
    //    public void SaveToXml(XmlElement xml)
    //    {
    //        this.SaveProperties(xml);
    //        var db=GenericDatabaseSource 
    //    }
    //}

    public class QueryFrameProgressInfo : IProgressInfo
    {
        QueryFrame m_frame;
        public QueryFrameProgressInfo(QueryFrame frame)
        {
            m_frame = frame;
        }
        #region IProgressInfo Members

        public void SetCurWork(string title)
        {
        }

        public void RaiseError(string error)
        {
        }

        public void SetCloseOnFinish(int severity, bool close)
        {
        }

        #endregion

        #region ILogger Members

        public void LogMessage(LogMessageRecord message)
        {
            var log = m_frame.m_logger;
            if (log != null) log.LogMessage(message);
        }

        #endregion
    }
}

