using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Xml;
using System.ComponentModel;
using System.IO;

namespace DatAdmin
{
    public class GenericTabularDataView : GenericConnectionUsage, ITabularDataView, IFormattable
    {
        string m_query;
        string m_countQuery;
        string m_deleteQuery;
        //DataTable m_table;
        //BedAdapter m_adapter = null;
        bool m_writable = false;
        object m_title;
        string m_dbname;
        bool m_supportsFilteringAndSorting;
        bool m_isFullTableSelect;
        string m_preferedPerspective;
        ITableSource m_tableSource;
        Dictionary<TablePerspective, BedAdapter> m_adapters = new Dictionary<TablePerspective, BedAdapter>();
        //Dictionary<TablePerspective, ColumnDisplayInfoCollection> m_columnsEx = new Dictionary<TablePerspective, ColumnDisplayInfoCollection>();
        Dictionary<BedTable, BedAdapter> m_adapterByTable = new Dictionary<BedTable, BedAdapter>();
        TablePerspective nullPer = new TablePerspective();

        public GenericTabularDataView(IPhysicalConnection conn, string dbname, string query, string countQuery, string deleteQuery, object title, bool supportsFilteringAndSorting, bool isFullTableSelect, ITableSource tableSource, string preferedPer)
            : base(conn)
        {
            m_conn = conn;
            m_query = query;
            m_title = title;
            m_dbname = dbname;
            m_countQuery = countQuery;
            m_deleteQuery = deleteQuery;
            m_supportsFilteringAndSorting = supportsFilteringAndSorting;
            m_isFullTableSelect = isFullTableSelect;
            m_tableSource = tableSource;
            m_preferedPerspective = preferedPer;
        }

        private void WantAdapter(TablePerspective per)
        {
            if (m_adapters.ContainsKey(per ?? nullPer)) return;

            m_adapters[per ?? nullPer] = new BedAdapter(m_conn, GetQueryTemplate(per, QueryType.Select), m_isFullTableSelect);
            m_writable = !m_adapters[per ?? nullPer].IsReadOnly;
        }

        #region ITabularData Members

        public ITabularDataView CloneView()
        {
            return new GenericTabularDataView(m_conn.Clone(), m_dbname, m_query, m_countQuery, m_deleteQuery, m_title, m_supportsFilteringAndSorting, m_isFullTableSelect, m_tableSource, m_preferedPerspective);
        }

        public ISqlDialect Dialect
        {
            get
            {
                if (m_conn != null) return m_conn.Dialect;
                return null;
            }
        }

        public IProgressInfo ProgressInfo { get; set; }

        public SettingsPageCollection Settings
        {
            get
            {
                return m_conn.FindSettings(m_dbname);
            }
        }

        //private string GetPerKey(TablePerspective per)
        //{
        //    if (per == null) return "";
        //    return per.UniqueKey();
        //}

        public ITableStructure GetStructure(TablePerspective per)
        {
            //string perkey = GetPerKey(per);
            m_conn.ReconnectIfBroken();
            if (m_conn.Dialect.DialectCaps.MultipleDatabase)
            {
                m_conn.SystemConnection.SafeChangeDatabase(m_dbname);
            }

            WantAdapter(per);
            return m_adapters[per ?? nullPer].GetStructure();
        }

        public ITableSource TableSource { get { return m_tableSource; } }
        public IDatabaseSource DatabaseSource
        {
            get
            {
                if (TableSource != null) return TableSource.Database;
                return null;
            }
        }

        private string GetQueryTemplate(TablePerspective per, QueryType type)
        {
            if (per == null) return type == QueryType.SelectCount ? m_countQuery : m_query;
            var sw = new StringWriter();
            var dmp = (Dialect ?? GenericDialect.Instance).CreateDumper(sw);
            var handler = new DmlfHandler { BaseTable = new DmlfSource { Alias = "basetbl", TableOrView = m_tableSource.FullName } };
            if (type == QueryType.SelectCount)
            {
                per.Select.GenSqlCount(dmp, handler);
            }
            else if (type == QueryType.SelectVisible)
            {
                DmlfResultFieldCollection vis, hid, old;
                per.Select.Columns.SplitVisible(out vis, out hid);
                old = per.Select.Columns;
                try
                {
                    per.Select.Columns = vis;
                    per.Select.GenSql(dmp, handler);
                }
                finally
                {
                    per.Select.Columns = old;
                }
            }
            else
            {
                per.Select.GenSql(dmp, handler);
            }
            return sw.ToString();
        }

        enum QueryType { Select, SelectCount, SelectNoUserInput, SelectVisible }

        private string GetQuery(TableDataSetProperties props, QueryType type)
        {
            string cmdtext = GetQueryTemplate(props.Perspective, type);

            if (m_supportsFilteringAndSorting)
            {
                if (type == QueryType.SelectNoUserInput)
                {
                    if (!props.FilterSqlConditionNoUserInput.IsEmpty())
                    {
                        cmdtext += "\nWHERE " + props.FilterSqlConditionNoUserInput;
                    }
                }
                else
                {
                    if (!props.FilterSqlCondition.IsEmpty())
                    {
                        cmdtext += "\nWHERE " + props.FilterSqlCondition;
                    }
                }
                if (type != QueryType.SelectCount)
                {
                    if (!props.SortOrder.IsEmpty())
                    {
                        cmdtext += "\nORDER BY " + props.SortOrder;
                    }
                }
            }
            return cmdtext;
        }

        public void NotifyPerspectiveChanged(TablePerspective per)
        {
            m_adapters.Remove(per);
        }

        public void NotifyRefresh()
        {
            m_adapters.Clear();
        }

        private void LoadTableDataProlog(TableDataSetProperties props, out BedAdapter adapter, out string cmdtext)
        {
            var per = props.Perspective;
            WantAdapter(per);
            adapter = m_adapters[per ?? nullPer];

            m_conn.ReconnectIfBroken();
            if (m_conn.Dialect.DialectCaps.MultipleDatabase)
            {
                m_conn.SystemConnection.SafeChangeDatabase(m_dbname);
            }

            cmdtext = GetQuery(props, QueryType.Select);
            ProgressInfo.Debug(Texts.Get("s_executing$query", "query", cmdtext));
        }

        private void LoadTableDataPrologHandleError(TableDataSetProperties props, Exception err)
        {
            string cmd2 = GetQuery(props, QueryType.SelectNoUserInput);
            try
            {
                using (var cmd = Connection.SystemConnection.CreateCommand())
                {
                    cmd.CommandText = cmd2;
                    using (var reader = cmd.ExecuteReader(CommandBehavior.SingleRow))
                    {
                        reader.Read();
                    }
                }
            }
            catch
            {
                // FAIL - throw original exception
                throw err;
            }
            // OK - there is syntax error in user input
            throw new UserInputSyntaxError(err.Message, err);
        }

        public BedTable LoadTableData(TablePageProperties props)
        {
            DateTime start = DateTime.Now;
            var per = props.Perspective;
            try
            {
                BedAdapter adapter;
                string cmdtext;
                LoadTableDataProlog(props, out adapter, out cmdtext);
                using (var canc = Connection.AddOnCancel(adapter.CancelLoading))
                {
                    BedTable table = null;
                    try
                    {
                        table = adapter.LoadTableData(props.Start, props.Count, cmdtext);
                    }
                    catch (Exception err)
                    {
                        LoadTableDataPrologHandleError(props, err);
                    }
                    var slen = DateTime.Now - start;
                    double len = slen.TotalSeconds;
                    if (table != null)
                    {
                        ProgressInfo.Debug(Texts.Get("s_readed$rows$cols$len",
                            "rows", table.Rows.Count,
                            "cols", table.Structure.Columns.Count,
                            "len", len.ToString("0.00")));
                    }
                    m_adapterByTable[table] = adapter;
                    if (per != null)
                    {
                        table.ResultFields = per.Select.Columns;
                    }
                    if (LoadedDataInfo != null)
                    {
                        var ea = new LoadedTableInfoArgs();
                        ea.Duration = slen;
                        ea.Table = table;
                        LoadedDataInfo(this, ea);
                    }
                    return table;
                }
            }
            catch (Exception err)
            {
                // probably error in command, clear adapter so that it is recreated next time
                m_adapters.Remove(per ?? nullPer);

                if (LoadedDataInfo != null)
                {
                    var ea = new LoadedTableInfoArgs();
                    ea.Duration = DateTime.Now - start;
                    ea.Error = err;
                    LoadedDataInfo(this, ea);
                }

                throw;
            }
        }

        public void LoadAllRows(TableDataSetProperties props, Action<IBedRecord> forEachRow)
        {
            var per = props.Perspective;
            try
            {
                BedAdapter adapter;
                string cmdtext;
                LoadTableDataProlog(props, out adapter, out cmdtext);
                using (var canc = Connection.AddOnCancel(adapter.CancelLoading))
                {
                    DateTime start = DateTime.Now;
                    try
                    {
                        adapter.LoadTableData(cmdtext, forEachRow);
                    }
                    catch (Exception err)
                    {
                        LoadTableDataPrologHandleError(props, err);
                    }
                    double len = (DateTime.Now - start).TotalSeconds;
                }
            }
            catch (Exception)
            {
                // probably error in command, clear adapter so that it is recreated next time
                m_adapters.Remove(per ?? nullPer);
                throw;
            }
        }

        public void SaveChanges(BedTable table, ISqlDumper dmp)
        {
            try
            {
                m_adapterByTable[table].SaveChanges(table, dmp);
            }
            catch (Exception err)
            {
                throw new DataError("DAE-00359", err);
            }
        }

        public void SaveChanges(BedTable table, ISaveDataProgress progress)
        {
            try
            {
                m_adapterByTable[table].SaveChanges(table, progress);
            }
            catch (Exception err)
            {
                throw new DataError("DAE-00360", err);
            }
        }

        public bool Readonly
        {
            get { return !m_writable; }
        }

        public int? LoadRowCount(TableDataSetProperties props)
        {
            string cmdtext = GetQuery(props, QueryType.SelectCount);
            if (cmdtext.IsEmpty()) return null;
            using (DbCommand cmd = m_conn.DbFactory.CreateCommand())
            {
                cmd.Connection = m_conn.SystemConnection;
                cmd.CommandText = cmdtext;
                using (var reader = cmd.ExecuteReader(CommandBehavior.SingleRow))
                {
                    if (reader.Read()) return Int32.Parse(reader[0].SafeToString());
                }
                return 0;
            }
        }

        public event LoadedNextDataDelegate LoadedNextData;
        public event LoadedTableInfoDelegate LoadedDataInfo;

        public TabularDataViewState State { get { return TabularDataViewState.Prepared; } }

        public ITabularDataStore GetStoreAndClone(TableDataSetProperties props)
        {
            IPhysicalConnection conn = m_conn.Clone();
            if (!String.IsNullOrEmpty(m_dbname)) conn.AfterOpen += ConnTools.ChangeDatabaseCallback(m_dbname);
            return new GenericQueryDataStore(conn, m_dbname, GetQuery(props, QueryType.SelectVisible));
        }

        public void CloseView()
        {
        }

        public TabularDataViewCaps TabDataCaps
        {
            get
            {
                return new TabularDataViewCaps
                {
                    AllFlags = false,
                    Filtering = m_supportsFilteringAndSorting,
                    Sorting = m_supportsFilteringAndSorting,
                    Paging = true,
                    Scriptable = true,
                    Perspectives = m_isFullTableSelect,
                };
            }
        }

        public void GenerateScript(TableDataScript script, TableDataSetProperties props, ISqlDumper dmp)
        {
            switch (script)
            {
                case TableDataScript.Delete:
                    if (!m_deleteQuery.IsEmpty())
                    {
                        dmp.WriteRaw(m_deleteQuery);
                        if (!props.FilterSqlCondition.IsEmpty())
                        {
                            dmp.WriteRaw(" WHERE ");
                            dmp.WriteRaw(props.FilterSqlCondition);
                        }
                        dmp.EndCommand();
                    }
                    break;
                case TableDataScript.Select:
                    dmp.WriteRaw(GetQuery(props, QueryType.Select));
                    dmp.EndCommand();
                    break;
            }
        }

        public bool SupportsSerialize { get { return true; } }

        public void SaveToXml(XmlElement xml)
        {
            xml.SetAttribute("type", "generic");
            xml.AddChild("CountQuery").InnerText = m_countQuery;
            xml.AddChild("Query").InnerText = m_query;
            xml.AddChild("DeleteQuery").InnerText = m_deleteQuery;
            var dbs = new GenericDatabaseSource(null, m_conn, m_dbname);
            dbs.SaveToXml(xml.AddChild("Database"));
            xml.AddChild("Title").InnerText = m_title.ToString("L");
            xml.AddChild("SupportsFilteringAndSorting").InnerText = m_supportsFilteringAndSorting ? "1" : "0";
            xml.AddChild("IsFullTableSelect").InnerText = m_isFullTableSelect ? "1" : "0";
            if (m_tableSource != null) m_tableSource.FullName.SaveToXml(xml.AddChild("Table"));
        }

        #endregion

        public string FixedPerspective
        {
            get { return m_preferedPerspective; }
            set { m_preferedPerspective = value; }
        }

        public override string ToString()
        {
            return ToString("L", null);
        }

        #region IFormattable Members

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (m_title == null) return "";
            switch (format)
            {
                case "M":
                    return String.Format("{0:M}\n{1:M}", m_title, m_conn);
                case "L":
                    return String.Format("{0:L}:{1:L}", m_title, m_conn);
                case "S":
                default:
                    return String.Format("{0:S}", m_title);
            }
        }

        #endregion
    }

    [TabularDataViewLoader(Name = "generic")]
    public class GenericTabularDataViewLoader : TabularDataViewLoaderBase
    {
        [XmlElem]
        public string Query { get; set; }
        [XmlElem]
        public string CountQuery { get; set; }
        [XmlElem]
        public string DeleteQuery { get; set; }
        [XmlElem]
        public string Title { get; set; }
        [XmlElem]
        public bool SupportsFilteringAndSorting { get; set; }
        [XmlElem]
        public bool IsFullTableSelect { get; set; }
        [XmlElem]
        public NameWithSchema Table { get; set; }
        [XmlElem]
        public string PreferedPerspective { get; set; }

        public IDatabaseSource Database;

        public override void LoadFromXml(XmlElement xml)
        {
            base.LoadFromXml(xml);
            Database = (IDatabaseSource)DatabaseSourceAddonType.Instance.LoadAddon(xml.FindElement("Database"));
        }

        public override ITabularDataView CreateTabularDataView()
        {
            ITableSource tbl = Table != null ? Database.GetTable(Table) : null;
            return new GenericTabularDataView(
                Database.Connection,
                Database.DatabaseName,
                Query,
                CountQuery,
                DeleteQuery,
                tbl != null ? (object)tbl : (object)Title,
                SupportsFilteringAndSorting,
                IsFullTableSelect,
                tbl,
                PreferedPerspective
                );
        }
    }

    public class GenericTableSource : GenericConnectionUsage, ITableSource, IFormattable
    {
        string m_dbname;
        string m_schema;
        string m_tblname;
        IDatabaseSource m_database;

        public GenericTableSource(IDatabaseSource database, IPhysicalConnection conn, string dbname, string schema, string tblname)
            : base(conn)
        {
            m_dbname = dbname;
            m_tblname = tblname;
            m_schema = schema;
            m_database = database;
        }

        public NameWithSchema FullName { get { return new NameWithSchema(m_schema, m_tblname); } }

        #region ITableSource Members

        //public string TableName
        //{
        //    get { return m_tblname; }
        //}

        //public string SchemaName
        //{
        //    get { return m_schema; }
        //}

        //public string CatalogName
        //{
        //    get { return m_catalog; }
        //}

        private ITabularDataView GetTabularData(IPhysicalConnection conn)
        {
            NameWithSchema table = FullName;
            if (!conn.Dialect.DialectCaps.MultipleSchema) table = table.GetNameWithNullSchema();
            return new GenericTabularDataView(
                conn, m_dbname, "SELECT * FROM " + DialectExtension.QuoteFullName(m_conn.Dialect, table),
                "SELECT COUNT(*) FROM " + DialectExtension.QuoteFullName(m_conn.Dialect, table),
                "DELETE FROM " + DialectExtension.QuoteFullName(m_conn.Dialect, table),
                this, true, true, this, null);
        }

        public ITabularDataView GetTabularData()
        {
            return GetTabularData(m_conn);
        }

        public ITabularDataStore GetDataStoreAndClone()
        {
            IPhysicalConnection conn = m_conn.Clone();
            if (!String.IsNullOrEmpty(m_dbname)) conn.AfterOpen += ConnTools.ChangeDatabaseCallback(m_dbname);
            return new GenericTabularDataStore(conn, m_dbname, m_schema, m_tblname);
        }

        public ITabularDataStore GetDataStoreAndReuse()
        {
            return new GenericTabularDataStore(m_conn, m_dbname, m_schema, m_tblname);
        }

        public ITableStructure LoadTableStructure(TableStructureMembers members)
        {
            return Database.LoadTableStructure(FullName, members);
        }

        public void TruncateTable()
        {
            m_conn.InvokeScript(dmp => { dmp.PutCmd("^delete ^from %f", FullName); }, m_dbname);
        }

        public IDatabaseSource Database
        {
            get { return m_database; }
        }

        public TableSourceCaps TableCaps
        {
            get { return GenericDatabaseSource.CreateCaps(m_conn.Dialect); }
        }

        public ITableSource MergeToConnection(IPhysicalConnection conn)
        {
            if (conn.GetConnKey() != m_conn.GetConnKey()) throw new InternalError("DAE-00032 Cannot merge connections with different groups");
            IDatabaseSource resdb = null;
            if (Database != null) resdb = Database.MergeToConnection(conn);
            GenericTableSource res = new GenericTableSource(resdb, conn, m_dbname, m_schema, m_tblname);
            return res;
        }

        #endregion

        public override string ToString()
        {
            return ToString("S", null);
        }

        #region IFormattable Members

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (m_dbname == null) return String.Format("{0:" + format + "}", FullName);
            switch (format)
            {
                case "M":
                    return String.Format("{0:L}\n{1}: {2}", FullName, Texts.Get("s_database"), m_dbname);
                case "L":
                    return String.Format("{0:L}:{1}", FullName, m_dbname);
                case "S":
                default:
                    return String.Format("{0:S}", FullName);
            }
        }

        #endregion
    }

    public class GenericViewAsTableSource : GenericConnectionUsage, ITableSource, IFormattable
    {
        string m_dbname;
        string m_schema;
        string m_viewname;
        IDatabaseSource m_database;

        public GenericViewAsTableSource(IDatabaseSource database, IPhysicalConnection conn, string dbname, string schema, string viewname)
            : base(conn)
        {
            m_dbname = dbname;
            m_viewname = viewname;
            m_schema = schema;
            m_database = database;
        }

        public NameWithSchema FullName { get { return new NameWithSchema(m_schema, m_viewname); } }

        #region ITableSource Members

        private ITabularDataView GetTabularData(IPhysicalConnection conn)
        {
            NameWithSchema table = FullName;
            if (!conn.Dialect.DialectCaps.MultipleSchema) table = table.GetNameWithNullSchema();
            return new GenericTabularDataView(
                conn, m_dbname, "SELECT * FROM " + DialectExtension.QuoteFullName(m_conn.Dialect, table),
                "SELECT COUNT(*) FROM " + DialectExtension.QuoteFullName(m_conn.Dialect, table),
                "DELETE FROM " + DialectExtension.QuoteFullName(m_conn.Dialect, table),
                this, true, false, this, null);
        }

        public ITabularDataView GetTabularData()
        {
            return GetTabularData(m_conn);
        }

        public ITabularDataStore GetDataStoreAndClone()
        {
            IPhysicalConnection conn = m_conn.Clone();
            if (!String.IsNullOrEmpty(m_dbname)) conn.AfterOpen += ConnTools.ChangeDatabaseCallback(m_dbname);
            return new GenericTabularDataStore(conn, m_dbname, m_schema, m_viewname);
        }

        public ITabularDataStore GetDataStoreAndReuse()
        {
            return new GenericTabularDataStore(m_conn, m_dbname, m_schema, m_viewname);
        }

        public ITableStructure LoadTableStructure(TableStructureMembers members)
        {
            return Database.InvokeLoadViewStructure(FullName);
        }

        public void TruncateTable()
        {
        }

        public IDatabaseSource Database
        {
            get { return m_database; }
        }

        public TableSourceCaps TableCaps
        {
            get
            {
                return new TableSourceCaps
                {
                    AllFlags = true,
                    DataStoreForReading = true,
                    TabularData = true,
                };
            }
        }

        public ITableSource MergeToConnection(IPhysicalConnection conn)
        {
            if (conn.GetConnKey() != m_conn.GetConnKey()) throw new InternalError("DAE-00371 Cannot merge connections with different groups");
            IDatabaseSource resdb = null;
            if (Database != null) resdb = Database.MergeToConnection(conn);
            var res = new GenericViewAsTableSource(resdb, conn, m_dbname, m_schema, m_viewname);
            return res;
        }

        #endregion

        public override string ToString()
        {
            return ToString("S", null);
        }

        #region IFormattable Members

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (m_dbname == null) return String.Format("{0:" + format + "}", FullName);
            switch (format)
            {
                case "M":
                    return String.Format("{0:L}\n{1}: {2}", FullName, Texts.Get("s_database"), m_dbname);
                case "L":
                    return String.Format("{0:L}:{1}", FullName, m_dbname);
                case "S":
                default:
                    return String.Format("{0:S}", FullName);
            }
        }

        #endregion
    }

    public class GenericQueryDataStore : GenericConnectionUsage, ITabularDataStore
    {
        string m_dbname;
        string m_query;
        TabularDataStoreMode m_mode;
        //DataTable m_rowFormat;
        ITableStructure m_rowFormat;
        //DbCommandBuilder m_builder;

        public GenericQueryDataStore(IPhysicalConnection conn, string dbname, string query)
            : base(conn)
        {
            m_dbname = dbname;
            m_query = query;
        }

        #region ITabularDataStore Members

        public IProgressInfo ProgressInfo { get; set; }

        [Browsable(false)]
        public TableCopyOptions CopyOptions { get; set; }

        public void LoadFromXml(XmlElement xml)
        {
            throw new SerializeNotSupportedException("DAE-00232");
        }
        public void SaveToXml(XmlElement xml)
        {
            throw new SerializeNotSupportedException("DAE-00233");
        }

        public IAsyncResult BeginRead(AsyncCallback callback, IDataQueue queue)
        {
            return m_conn.BeginInvoke((Action<IDataQueue>)DoRead, callback, queue);
        }

        public void EndRead(IAsyncResult async)
        {
            m_conn.EndInvoke(async);
        }

        public void ClearCaches() { }

        public IAsyncResult BeginWrite(AsyncCallback callback, IDataQueue queue)
        {
            throw new NotImplementedError("DAE-00234");
        }

        public void EndWrite(IAsyncResult async)
        {
            throw new NotImplementedError("DAE-00235");
        }

        public bool SupportsMode(TabularDataStoreMode mode) { return mode == TabularDataStoreMode.Read; }

        public TabularDataStoreMode Mode
        {
            get { return TabularDataStoreMode.Read; }
            set { Errors.Assert(value == TabularDataStoreMode.Read); }
        }

        public IAsyncResult BeginGetRowFormat(AsyncCallback callback)
        {
            return m_conn.BeginInvoke((Func<ITableStructure>)DoGetRowFormat, callback);
        }

        public ITableStructure EndGetRowFormat(IAsyncResult async)
        {
            return (ITableStructure)m_conn.EndInvoke(async);
        }

        public void SetRowFormat(ITableStructure rowFormat)
        {
            m_rowFormat = rowFormat;
        }

        public bool ConfigurationNeeded { get { return false; } }
        public bool AvailableRowFormat { get { return true; } }

        public void CheckConfiguration() { }

        public void CloseAllResources() { }

        [Browsable(false)]
        public ITabularDataOuputStream StreamApi
        {
            get { return null; }
        }

        #endregion

        ITableStructure DoGetRowFormat()
        {
            if (m_rowFormat == null)
            {
                using (DbCommand cmd = m_conn.DbFactory.CreateCommand())
                {
                    cmd.CommandText = m_query;
                    cmd.Connection = m_conn.SystemConnection;
                    using (DbDataReader reader = cmd.ExecuteReader(CommandBehavior.SchemaOnly | CommandBehavior.KeyInfo))
                    {
                        return reader.GetTableStructure(m_conn.Dialect);
                    }
                }
            }
            return m_rowFormat;
        }

        void DoWrite(IDataQueue queue)
        {
            throw new NotImplementedError("DAE-00236");
        }

        void DoRead(IDataQueue queue)
        {
            try
            {
                using (DbCommand cmd = m_conn.DbFactory.CreateCommand())
                {
                    cmd.CommandText = m_query;
                    cmd.Connection = m_conn.SystemConnection;
                    using (IBedReader reader = m_conn.GetAnyDDA().AdaptReader(cmd.ExecuteReader()))
                    {
                        while (reader.Read())
                        {
                            queue.PutRecord(reader);
                        }
                    }
                }
            }
            finally
            {
                queue.PutEof();
            }
        }

        public override string ToString()
        {
            return String.Format("{0}:{1}", Texts.Get("s_query"), m_conn);
        }
    }
}
