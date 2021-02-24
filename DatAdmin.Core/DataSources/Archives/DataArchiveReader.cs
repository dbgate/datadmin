using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using Ionic.Zip;
using System.ComponentModel;

namespace DatAdmin
{
    public class DataArchiveConnectionUsage : GenericConnectionUsage
    {
        protected DataArchiveConnection Conn { get { return (DataArchiveConnection)m_conn; } }
        protected DataArchiveConnectionUsage(DataArchiveConnection conn)
            : base(conn)
        {
        }
        protected DataArchiveConnectionUsage() { }
    }

    public class DataArchiveTable : DataArchiveConnectionUsage, ITableSource
    {
        TableStructure m_table;
        IDatabaseSource m_database;

        public DataArchiveTable(IDatabaseSource database, DataArchiveConnection conn, TableStructure table)
            : base(conn)
        {
            m_table = table;
            m_database = database;
        }

        #region ITableSource Members

        public NameWithSchema FullName { get { return m_table.FullName; } }

        public ITabularDataView GetTabularData()
        {
            Async.SafeOpen(Connection); // get tabular data requires allready opened connection
            GridTable tbl = new GridTable(m_table, m_table.FullName.ToString());
            IDataQueue queue = new GenericDataQueue(m_table, m_table, new IdentityTransform(m_table));
            tbl.FillOnBackground(queue);
            Conn.BeginReadTable(m_table.FullName, queue, FinishedReadTable);
            return tbl;
        }

        private void FinishedReadTable(IAsyncResult res)
        {
            try
            {
                m_conn.EndInvoke(res);
            }
            catch (Exception e)
            {
                Errors.Report(e);
            }
        }

        public ITabularDataStore GetDataStoreAndClone()
        {
            return new DataArchiveReaderDataStore((DataArchiveConnection)m_conn.Clone(), m_table.FullName);
        }

        public ITabularDataStore GetDataStoreAndReuse()
        {
            return new DataArchiveReaderDataStore(Conn, m_table.FullName);
        }

        public void TruncateTable()
        {
            throw new NotImplementedError("DAE-00088");
        }

        public ITableStructure LoadTableStructure(TableStructureMembers members) { return m_table; }

        //public ITableStructure Structure
        //{
        //    get { return m_table; }
        //}

        public IDatabaseSource Database
        {
            get { return m_database; }
        }
        public TableSourceCaps TableCaps
        {
            get { return CreateCaps(); }
        }

        //public void CreateConstraints(IEnumerable<IConstraint> constraints)
        //{
        //    foreach (IConstraint cnt in constraints)
        //    {
        //        m_table.Constraints.Add(Constraint.CreateCopy(cnt));
        //    }
        //}

        public ITableSource MergeToConnection(IPhysicalConnection conn)
        {
            if (conn.GetConnKey() != m_conn.GetConnKey()) throw new InternalError("DAE-00018 Cannot merge connections with different groups");
            return new DataArchiveTable(Database.MergeToConnection(conn), (DataArchiveConnection)conn, m_table);
        }

        #endregion

        internal static TableSourceCaps CreateCaps()
        {
            return new TableSourceCaps { AllFlags = false, DataStoreForReading = true, TabularData = true };
            //return TableBehaviour.Fill(false, null)  .SetHasDataStoreForReading(true).SetHasTabularData(true);
        }
    }

    [DatabaseSource(Name = "dbkreader")]
    public class DataArchiveReader : DataArchiveConnectionUsage, IDatabaseSource, IAddonInstance
    {
        string m_file;

        public DataArchiveReader(string file)
            : base(new DataArchiveConnection(file))
        {
            m_file = file;
        }

        public DataArchiveReader(DataArchiveConnection conn)
            : base(conn)
        {
            m_file = conn.m_file;
        }

        [DisplayName("s_file")]
        public string GuiFile
        {
            get { return file; }
            set { file = value; }
        }

        #region Serialize support

        [XmlAttrib]
        [Browsable(false)]
        public string file
        {
            get { return m_file; }
            set { m_file = value; }
        }

        public DataArchiveReader()
        {
        }

        #endregion

        #region IDatabaseSource Members

        [Browsable(false)]
        public ISqlDialect Dialect
        {
            get
            {
                return m_conn.Dialect;
            }
        }

        [Browsable(false)]
        public SettingsPageCollection Settings
        {
            get { return m_conn.FindSettings(null); }
        }

        [Browsable(false)]
        public string DefaultSchema { get { return null; } }

        public ObjectOperationCaps GetSupportedOperations(string objtype)
        {
            return new ObjectOperationCaps { AllFlags = false };
        }

        [Browsable(false)]
        public IServerSource Server { get { return null; } }

        public string GetPrivateSubFolder(string name)
        {
            return m_file + "." + name;
        }
        public string GetPrivateFolder() { return null; }

        public void CreateSchema(string name)
        {
            throw new NotImplementedError("DAE-00089");
        }

        public void DropSchema(string name)
        {
            throw new NotImplementedError("DAE-00090");
        }

        //private IEnumerable<NameWithSchema> LoadTableNames()
        //{
        //    WantStructure();
        //    foreach (ITableStructure table in m_db.Tables)
        //    {
        //        yield return table.FullName;
        //    }
        //}

        private DataArchiveConnection Conn { get { return (DataArchiveConnection)m_conn; } }

        public IDatabaseStructure LoadDatabaseStructure(DatabaseStructureMembers members, IProgressInfo progress)
        {
            return Conn.GetStructure();
        }

        public void ClearCaches() { }

        public ITableSource GetTable(NameWithSchema table)
        {
            var db = Conn.GetStructure();
            return new DataArchiveTable(this, Conn, (TableStructure)db.Tables[table]);
        }

        public IPhysicalConnection CloneConnection()
        {
            return m_conn.Clone();
        }

        [Browsable(false)]
        public TableSourceCaps TableCaps { get { return DataArchiveTable.CreateCaps(); } }

        [Browsable(false)]
        public string DatabaseName { get { return System.IO.Path.GetFileNameWithoutExtension(m_file); } }

        public IDatabaseSource CloneSource()
        {
            return new DataArchiveReader(m_file);
        }

        public IDatabaseSource MergeToConnection(IPhysicalConnection conn)
        {
            if (conn.GetConnKey() != m_conn.GetConnKey()) throw new InternalError("DAE-00193 Cannot merge connections with different groups");
            return new DataArchiveReader((DataArchiveConnection)conn);
        }

        [Browsable(false)]
        public virtual List<string> DefinedSpecificObjects
        {
            get
            {
                var db = Conn.GetStructure();
                return new List<string>(db.SpecificObjects.Keys);
            }
        }


        #endregion

        public override string ToString()
        {
            return System.IO.Path.GetFileName(m_file);
        }

        public List<ISpecificObjectType> GetSpecificTypes()
        {
            if (!m_conn.IsOpened) return new List<ISpecificObjectType>();
            var db = Conn.GetStructure();
            List<ISpecificObjectType> res = new List<ISpecificObjectType>();
            foreach (string objtype in db.SpecificObjects.Keys)
            {
                res.Add(new GenericSpecificObjectType(objtype));
            }
            return res;
        }

        #region IDatabaseSource Members

        [Browsable(false)]
        public DatabaseSourceCaps OfflineDatabaseCaps
        {
            get { return DatabaseCaps; }
        }

        [Browsable(false)]
        public DatabaseSourceCaps DatabaseCaps
        {
            get
            {
                return new DatabaseSourceCaps
                {
                    AllFlags = false,
                    MultipleSchema = true,
                    Domains = true,
                    ReadOnly = true
                };
            }
        }

        [Browsable(false)]
        public AlterProcessorCaps AlterCaps
        {
            get { return new AlterProcessorCaps { AllFlags = false }; }
        }

        public void AlterDatabase(IDatabaseStructure src, IDatabaseStructure dst, DbDiffOptions opts)
        {
            throw new NotImplementedError("DAE-00091");
        }
        public void AlterDatabase(AlterPlan plan, DbDiffOptions opts)
        {
            throw new NotImplementedError("DAE-00092");
        }

        #endregion

        [Browsable(false)]
        public AddonType AddonType
        {
            get { return DatabaseSourceAddonType.Instance; }
        }

        #region IAddonInstance Members

        public void LoadFromXml(XmlElement xml)
        {
            this.LoadPropertiesCore(xml);
            m_conn = new DataArchiveConnection(m_file);
        }

        public void SaveToXml(XmlElement xml)
        {
            this.SavePropertiesCore(xml, true);
            xml.SetAttribute("adtype", AddonType.Name);
        }

        #endregion
    }

    public class DataArchiveConnection : PhysicalConnectionBase
    {
        ZipFile m_zip;
        internal string m_file;
        DatabaseStructure m_db = null;

        public DataArchiveConnection(string file)
            : base(new DataArchiveFileConnectionFactory { FileName = file })
        {
            m_file = file;
        }

        internal DatabaseStructure GetStructure()
        {
            lock (this)
            {
                WantZip();
                if (m_db == null) m_db = LoadStructure();
                return m_db;
            }
        }

        private void WantZip()
        {
            lock (this)
            {
                if (m_zip == null)
                {
                    m_zip = new ZipFile(m_file);
                }
            }
        }

        public override string ThreadName
        {
            get { return "DBK read:" + Path.GetFileName(m_file); }
        }

        private void DoReadTable(NameWithSchema table, IDataQueue queue)
        {
            try
            {
                string fnbase = XmlTool.NormalizeIdentifier(table.ToString());
                ZipEntry xmlEntry;
                try { xmlEntry = m_zip[fnbase + ".xml"]; }
                catch { xmlEntry = null; }
                ZipEntry drsEntry;
                try { drsEntry = m_zip[fnbase + ".drs"]; }
                catch { drsEntry = null; }
                if (drsEntry == null && xmlEntry == null)
                {
                    var dbs = GetStructure();
                    if (dbs.Tables.GetIndex(table) < 0)
                    {
                        throw new InternalError("DAE-00019 Table not found in data archive:" + table.ToString());
                    }
                    // table is empty, only has no drs nor xml file
                    queue.PutEof();
                    return;
                }
                using (Stream fr = (drsEntry ?? xmlEntry).OpenReader())
                {
                    if (drsEntry != null)
                    {
                        BedTool.LoadQueue(fr, queue);
                    }
                    else if (xmlEntry != null)
                    {
                        using (XmlReader reader = XmlReader.Create(fr, new XmlReaderSettings { CheckCharacters = false }))
                        {
                            XmlDataTool.ReadXmlToQueue(reader, queue, "DataRow");
                        }
                    }
                }
            }
            finally
            {
                queue.CloseWriting();
            }
        }

        public IAsyncResult BeginReadTable(NameWithSchema table, IDataQueue queue, AsyncCallback async)
        {
            return this.BeginInvoke((Action<NameWithSchema, IDataQueue>)DoReadTable, async, table, queue);
        }
        public void EndReadTable(IAsyncResult async)
        {
            EndInvoke(async);
        }

        protected override void DoOpen()
        {
            WantZip();
        }

        public override void CloseResources()
        {
            if (m_zip != null)
            {
                m_zip.Dispose();
                m_zip = null;
            }
        }

        protected override void DoClose()
        {
            CloseResources();
        }

        public override void Dispose()
        {
            CloseResources();
        }

        public DatabaseStructure LoadStructure()
        {
            lock (this)
            {
                if (!m_zip.ContainsEntry("_index_.xml")) throw new ArchiveError("DAE-00194 Bad data archive: missing file _index_.xml");
                ZipEntry entry = m_zip["_index_.xml"];
                using (Stream fr = entry.OpenReader())
                {
                    return DatabaseStructure.Load(fr);
                }
            }
        }

        public override string ToString()
        {
            return System.IO.Path.GetFileName(m_file);
        }

        public override ISqlDialect Dialect
        {
            get
            {
                try
                {
                    return GetStructure().Dialect;
                }
                catch
                {
                    return GenericDialect.Instance;
                }
            }
        }
    }

    [PhysicalConnectionFactory(Name = "dbk_file")]
    public class DataArchiveFileConnectionFactory : PhysicalConnectionFactoryBase
    {
        [XmlElem]
        public string FileName { get; set; }

        public override string GetDataSource()
        {
            return FileName;
        }

        public override string GetConnectionKey()
        {
            return "dbk:" + FileName;
        }

        public override IPhysicalConnection CreateConnection()
        {
            return new DataArchiveConnection(FileName);
        }

        public override IDatabaseSource CreateDatabaseSource(IPhysicalConnection conn, string dbname)
        {
            return new DataArchiveReader((DataArchiveConnection)conn);
        }

        public override string GetDatabasePrivateFolder(string dbname)
        {
            return null;
        }

        public override string GetDatabasePrivateSubFolder(string dbname, string folderName)
        {
            return FileName + "." + folderName;
        }

        public override string GetFileName()
        {
            return FileName;
        }
    }

    [TabularDataStore(Name = "data_archive_reader", SupportsDirectUse = false, SupportsCreateTemplate = false)]
    public class DataArchiveReaderDataStore : AddonBase, ITabularDataStore
    {
        protected IPhysicalConnection m_conn;
        NameWithSchema m_table;

        public DataArchiveReaderDataStore(DataArchiveConnection conn, NameWithSchema table)
        {
            m_conn = conn;
            m_table = table;
        }

        protected DataArchiveConnection Conn { get { return (DataArchiveConnection)m_conn; } }

        public DataArchiveReaderDataStore() { }

        public NameWithSchema Table
        {
            get { return m_table; }
            set { m_table = value; }
        }

        [XmlAttrib("archive")]
        public string Archive
        {
            get { return Conn.m_file; }
            set
            {
                m_conn = new DataArchiveConnection(value);
            }
        }

        private ITableStructure DoGetRowFormat()
        {
            DatabaseStructure s = Conn.LoadStructure();
            return s.Tables[m_table];
        }

        #region ITabularDataStore Members

        [Browsable(false)]
        public TableCopyOptions CopyOptions { get; set; }

        public IProgressInfo ProgressInfo { get; set; }

        public void CloseAllResources() { }

        public override void LoadFromXml(XmlElement xml)
        {
            base.LoadFromXml(xml);
            m_table = NameWithSchema.LoadFromXml(xml.FindElement("Table"));
        }
        public override void SaveToXml(XmlElement xml)
        {
            base.SaveToXml(xml);
            if (m_table != null) m_table.SaveToXml(xml.AddChild("Table"));
        }

        public void ClearCaches() { }

        public TabularDataStoreMode Mode
        {
            get
            {
                return TabularDataStoreMode.Read;
            }
            set
            {
                if (value != TabularDataStoreMode.Read) throw new Exception("DAE-00195 Unallowed mode");
            }
        }

        public IAsyncResult BeginRead(AsyncCallback callback, IDataQueue queue)
        {
            return Conn.BeginReadTable(m_table, queue, callback);
        }

        public void EndRead(IAsyncResult async)
        {
            Conn.EndReadTable(async);
        }

        public IAsyncResult BeginWrite(AsyncCallback callback, IDataQueue queue)
        {
            throw new NotImplementedError("DAE-00093");
        }

        public void EndWrite(IAsyncResult async)
        {
            throw new NotImplementedError("DAE-00094");
        }

        public IAsyncResult BeginGetRowFormat(AsyncCallback callback)
        {
            return m_conn.BeginInvoke((Func<ITableStructure>)DoGetRowFormat, null);
        }

        public ITableStructure EndGetRowFormat(IAsyncResult async)
        {
            return (ITableStructure)m_conn.EndInvoke(async);
        }

        public void SetRowFormat(ITableStructure rowFormat)
        {
            throw new NotImplementedError("DAE-00095");
        }

        public bool ConfigurationNeeded
        {
            get { return false; }
        }

        public bool AvailableRowFormat
        {
            get { return true; }
        }

        public void CheckConfiguration() { }

        public bool SupportsMode(TabularDataStoreMode mode) { return mode == TabularDataStoreMode.Read; }

        [Browsable(false)]
        public ITabularDataOuputStream StreamApi
        {
            get { return null; }
        }

        #endregion

        #region IConnectionUsage Members

        [Browsable(false)]
        public IPhysicalConnection Connection
        {
            get { return m_conn; }
            set { PhysicalConnectionExtension.SafeChangeConnection(ref m_conn, value); }
        }

        #endregion

        [Browsable(false)]
        public override AddonType AddonType
        {
            get { return TabularDataStoreAddonType.Instance; }
        }
    }
}
