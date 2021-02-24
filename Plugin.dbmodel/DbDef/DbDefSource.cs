using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.ComponentModel;
using DatAdmin;

namespace Plugin.dbmodel
{
    public class DbDefConnection : NoThreadPhysicalConnectionBase, IFormattable
    {
        DatabaseStructure m_db;
        internal string m_file;
        DateTime? m_fileTimeStamp = null;
        //Func<string> m_getCurrentFileName;
        //ISqlDialect m_dialect;
        //string m_propsfile;
        //internal bool m_forceSingleSchema;

        public DbDefConnection(string filename)
            : base(new DbDefFileConnectionFactory { FileName = filename })
        {
            m_file = filename;
        }

        //public DbDefConnection(Func<string> getCurrentFileName)
        //    : base(null)
        //{
        //    m_getCurrentFileName = getCurrentFileName;
        //}

        //public DbDefConnection()
        //{
        //}

        public DbDefConnection(IDatabaseStructure db)
            : base(new DbDefStructureConnectionFactory { Structure = new DatabaseStructure(db) })
        {
            m_db = ((DbDefStructureConnectionFactory)PhysicalFactory).Structure;
        }

        public DbDefConnection(DatabaseStructure db)
            : base(new DbDefStructureConnectionFactory { Structure = db })
        {
            m_db = db;
        }

        public DbDefConnection(IPhysicalConnectionFactory factory)
            : base(factory)
        {
        }

        protected virtual string GetCurrentFileName()
        {
            return m_file;
        }

        public virtual IDatabaseStructure CustomGetStructure() { return null; }

        public override IPhysicalConnection Clone()
        {
            DbDefConnection res = (DbDefConnection)base.Clone();
            if (m_db != null)
            {
                res.m_db = new DatabaseStructure(m_db);
                res.m_fileTimeStamp = m_fileTimeStamp;
            }
            return res;
        }

        public override ISqlDialect Dialect
        {
            get
            {
                try
                {
                    return DbStruct.Dialect;
                }
                catch
                {
                    return GenericDialect.Instance;
                }
            }
        }

        //public void ReloadProperties()
        //{
        //    m_dialect = null;
        //    if (m_propsfile != null && System.IO.File.Exists(m_propsfile))
        //    {
        //        DbDefProperties props = new DbDefProperties(m_propsfile);
        //        if (props.Dialect != null) m_dialect = (ISqlDialect)props.Dialect.CreateInstance();
        //        else m_dialect = null;
        //        m_forceSingleSchema = props.ForceSingleSchema;
        //    }
        //}

        public DatabaseStructure DbStruct
        {
            get
            {
                if (m_db == null)
                {
                    if (m_file != null) m_db = DatabaseStructure.Load(m_file);
                    else m_db = new DatabaseStructure();
                    m_db.MarkAllFilled();
                }

                return m_db;
            }
        }

        public void Reload()
        {
            // reload is not neccessary
            var db = CustomGetStructure();
            if (db != null)
            {
                m_db = new DatabaseStructure(db);
                return;
            }
            string newfile = GetCurrentFileName();
            if (newfile == null) return;
            if (newfile == m_file && new FileInfo(m_file).LastWriteTime <= m_fileTimeStamp) return;
            m_file = newfile;
            m_db = DatabaseStructure.Load(m_file);
            m_db.MarkAllFilled();
            m_fileTimeStamp = new FileInfo(m_file).LastWriteTime;
        }

        public void SaveToFile()
        {
            m_db.Save(m_file);
            m_fileTimeStamp = new FileInfo(m_file).LastWriteTime;
        }

        public override string ToString()
        {
            return ToString("S", null);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            switch (format)
            {
                case "L":
                case "M":
                    return m_file ?? Texts.Get("s_structure");
                case "S":
                default:
                    return System.IO.Path.GetFileName(m_file);
            }
        }
    }

    [PhysicalConnectionFactory(Name = "dbdef_file", RequiredFeature = DatabaseModelsFeature.Test)]
    public class DbDefFileConnectionFactory : PhysicalConnectionFactoryBase
    {
        [XmlElem]
        public string FileName { get; set; }

        public override string GetDataSource()
        {
            return FileName;
        }

        public override string GetConnectionKey()
        {
            return "dbdef:" + FileName; 
        }

        public override IPhysicalConnection CreateConnection()
        {
            return new DbDefConnection(FileName);
        }

        public override IDatabaseSource CreateDatabaseSource(IPhysicalConnection conn, string dbname)
        {
            return new DbDefSource((DbDefConnection)conn);
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

    [PhysicalConnectionFactory(Name = "dbdef_structure", RequiredFeature = DatabaseModelsFeature.Test)]
    public class DbDefStructureConnectionFactory : PhysicalConnectionFactoryBase
    {
        [XmlSubElem]
        public DatabaseStructure Structure { get; set; }

        public override string GetDataSource()
        {
            return "(MODEL)";
        }

        public override string GetConnectionKey()
        {
            return "dbdef:" + Structure.GetHashCode();
        }

        public override IPhysicalConnection CreateConnection()
        {
            return new DbDefConnection(Structure);
        }

        public override IDatabaseSource CreateDatabaseSource(IPhysicalConnection conn, string dbname)
        {
            return new DbDefSource((DbDefConnection)conn);
        }

        public override string GetDatabasePrivateFolder(string dbname)
        {
            return null;
        }

        public override string GetDatabasePrivateSubFolder(string dbname, string folderName)
        {
            return null;
        }

        public override string GetFileName()
        {
            return null;
        }
    }

    public class TableDefSource : GenericConnectionUsage, ITableSource, IFormattable
    {
        internal TableStructure m_table;
        DbDefSource m_parent;

        public TableDefSource(DbDefSource parent, TableStructure table)
            : base(parent.Connection)
        {
            m_table = table;
            m_parent = parent;
        }

        #region ITableSource Members

        public NameWithSchema FullName { get { return m_table.FullName; } }

        public ITabularDataView GetTabularData()
        {
            return new TableDefDataView(this);
        }

        public ITabularDataStore GetDataStoreAndClone()
        {
            return new TableDefDataStore(this);
        }

        public ITabularDataStore GetDataStoreAndReuse()
        {
            return new TableDefDataStore(this);
        }

        public ITableSource MergeToConnection(IPhysicalConnection conn)
        {
            return new TableDefSource((DbDefSource)m_parent.MergeToConnection(conn), m_table);
        }

        private void Reload()
        {
            m_parent.Reload();
            m_table = (TableStructure)m_parent.DbStruct.Tables[m_table.FullName];
        }

        private void SaveToFile()
        {
            m_parent.SaveToFile();
        }

        public ITableStructure LoadTableStructure(TableStructureMembers members)
        {
            Reload();
            return m_table;
        }

        public InMemoryTable LoadFixedData()
        {
            Reload();
            return m_table.FixedData;
        }

        public void SaveFixedData(InMemoryTable data)
        {
            Reload();
            m_table.FixedData = data;
            SaveToFile();
        }

        public void SaveFixedData(IDataQueue queue)
        {
            InMemoryTable tbl = InMemoryTable.FromEnumerable(queue.GetRowFormat, queue.EnumRows());
            if (tbl.Rows.Count == 0) tbl = null;
            SaveFixedData(tbl);
        }

        public void TruncateTable()
        {
            Reload();
            m_table.FixedData = null;
            SaveToFile();
        }

        public AlterProcessorCaps AlterTableCaps
        {
            get { return new AlterProcessorCaps { AllFlags = true }; }
        }

        public IDatabaseSource Database
        {
            get { return m_parent; }
        }

        public TableSourceCaps TableCaps
        {
            get { return CreateBehaviour(); }
        }

        #endregion

        internal static TableSourceCaps CreateBehaviour()
        {
            return new TableSourceCaps
            {
                AllFlags = false,
                TabularData = true,
                DataStoreForReading = true,
                DataStoreForWriting = true,
            };
        }

        public override string ToString()
        {
            return ToString("L", null);
        }

        #region IFormattable Members

        public string ToString(string format, IFormatProvider formatProvider)
        {
            switch (format)
            {
                case "M":
                    return String.Format("{0:L}\n{1}", FullName, m_parent);
                case "L":
                    return String.Format("{0:L}:{1}", FullName, m_parent);
                case "S":
                default:
                    return String.Format("{0:S}", FullName);
            }
        }

        #endregion
    }

    [DatabaseSource(Name = "dbdef", RequiredFeature = DatabaseModelsFeature.Test)]
    public class DbDefSource : AddonBase, IDatabaseSource, IFormattable
    {
        public enum ReadOnly { Flag };

        internal DbDefConnection m_conn;

        bool m_openedReadOnly;
        DatabaseStructure m_readOnlyStructure;

        public DbDefSource(string file)
        {
            m_conn = new DbDefConnection(file);
        }

        //public DbDefSource(Func<string> getCurrentFileName)
        //{
        //    m_conn = new DbDefConnection(getCurrentFileName);
        //}

        public DbDefSource(DbDefConnection conn)
        {
            m_conn = conn;
        }

        // must be defined for persisting purposes
        public DbDefSource()
        {
            //m_conn = new DbDefConnection();
        }

        // constructor 3
        public DbDefSource(IDatabaseStructure db, ReadOnly flag)
        {
            m_readOnlyStructure = new DatabaseStructure(db);
            m_readOnlyStructure.MarkAllFilled();
            m_openedReadOnly = true;
            m_conn = new DbDefConnection(m_readOnlyStructure);
        }

        // constructor 3'
        public DbDefSource(IPhysicalConnectionFactory fact, ReadOnly flag)
        {
            m_conn = (DbDefConnection)fact.CreateConnection();
            m_openedReadOnly = true;
        }

        #region Serialize support

        [XmlAttrib]
        public string file
        {
            get { return m_conn.m_file; }
            set
            {
                if (m_conn == null) m_conn = new DbDefConnection(value);
                else m_conn.m_file = value;
            }
        }

        #endregion

        private DatabaseStructure GetReadOnlyStructure()
        {
            if (m_readOnlyStructure == null) m_readOnlyStructure = new DatabaseStructure(m_conn.CustomGetStructure());
            return m_readOnlyStructure;
        }

        public DatabaseStructure DbStruct
        {
            get
            {
                if (m_openedReadOnly) return GetReadOnlyStructure();
                return m_conn.DbStruct;
            }
        }

        public ObjectOperationCaps GetSupportedOperations(string objtype)
        {
            return new ObjectOperationCaps { AllFlags = true };
        }

        public void ClearCaches()
        {
            Reload();
        }

        public SettingsPageCollection Settings
        {
            get
            {
                if (m_conn == null) return GlobalSettings.Pages;
                return m_conn.FindSettings(null);
            }
        }

        public void Reload()
        {
            if (m_conn != null) m_conn.Reload();
        }

        public IServerSource Server { get { return null; } }

        public IDatabaseStructure LoadDatabaseStructure(DatabaseStructureMembers members, IProgressInfo progress)
        {
            Reload();
            return DbStruct;
        }

        public ITableSource GetTable(NameWithSchema table)
        {
            return new TableDefSource(this, (TableStructure)DbStruct.Tables[table]);
        }

        //public ITableSource CreateTable(ITableStructure table)
        //{
        //    Reload();
        //    ITableSource res = new TableDefSource(this, m_db.AddTable(MangleTable(table)));
        //    m_db.SortTables();
        //    SaveToFile();
        //    return res;
        //}

        public void AlterDatabase(IDatabaseStructure src, IDatabaseStructure dst, DbDiffOptions opts)
        {
            Reload();
            if (!SupportsMultipleSchema())
            {
                var tmp = new DatabaseStructure(dst);
                tmp.RunNameTransformation(new SetSchemaNameTransformation(null));
                dst = tmp;
            }
            m_conn.DbStruct.AlterDatabase(src, dst, opts, this);
            SaveToFile();
        }

        public void AlterDatabase(AlterPlan plan, DbDiffOptions opts)
        {
            Reload();
            plan.CreateRunner().Run(m_conn.DbStruct, opts);
            SaveToFile();
        }

        public string GetPrivateSubFolder(string name)
        {
            if (m_conn == null || m_conn.m_file == null) return null;
            return m_conn.m_file + "." + name;
        }

        public string GetPrivateFolder() { return null; }

        internal void SaveToFile()
        {
            m_conn.SaveToFile();
        }

        public IPhysicalConnection CloneConnection()
        {
            return m_conn.Clone();
        }

        public string DatabaseName
        {
            get
            {
                if (m_conn == null) return null;
                return System.IO.Path.GetFileNameWithoutExtension(m_conn.m_file);
            }
        }

        public string DefaultSchema
        {
            get
            {
                if (!SupportsMultipleSchema()) return null;
                if (Dialect != null) return Dialect.DefaultSchema;
                return null;
            }
        }

        public IDatabaseSource CloneSource()
        {
            if (m_conn == null) return this;
            return new DbDefSource((DbDefConnection)m_conn.Clone());
        }

        public ISqlDialect Dialect
        {
            get
            {
                if (m_openedReadOnly) return GetReadOnlyStructure().Dialect;
                //if (m_dialectWhenReadonly != null) return m_dialectWhenReadonly;
                if (m_conn != null && m_conn.Dialect != null) return m_conn.Dialect;
                return null;
            }
        }

        private bool SupportsMultipleSchema()
        {
            try
            {
                if (m_conn != null && DbStruct.ForceSingleSchema) return false;
                if (Dialect != null) return Dialect.DialectCaps.MultipleSchema;
                return true;
            }
            catch
            {
                return true;
            }
        }

        public IDatabaseSource MergeToConnection(IPhysicalConnection conn)
        {
            return new DbDefSource((DbDefConnection)conn);
        }

        public virtual List<string> DefinedSpecificObjects
        {
            get
            {
                if (Dialect != null) return Dialect.GetDefinedSpecificObjects();
                return new List<string>(m_conn.DbStruct.SpecificObjects.Keys);
            }
        }

        //public ISpecificObjectStructure CreateSpecificObject(ISpecificObjectStructure obj)
        //{
        //    Reload();
        //    var res = m_db.AddSpecificObject(obj);
        //    SaveToFile();
        //    return res;
        //}

        //public void AlterSpecificObject(ISpecificObjectStructure olddef, ISpecificObjectStructure newdef)
        //{
        //    Reload();
        //    m_db.AlterSpecificObject(olddef, newdef);
        //    SaveToFile();
        //}

        //public void DropSpecificObject(ISpecificObjectStructure obj)
        //{
        //    Reload();
        //    m_db.DropSpecificObject(obj);
        //    SaveToFile();
        //}

        //public void RenameSpecificObject(ISpecificObjectStructure obj, string newname)
        //{
        //    Reload();
        //    m_db.RenameSpecificObject(obj, newname);
        //    SaveToFile();
        //}

        //public void ChangeSpecificObjectSchema(ISpecificObjectStructure obj, string newschema)
        //{
        //    Reload();
        //    m_db.ChangeSpecificObjectSchema(obj, newschema);
        //    SaveToFile();
        //}

        #region IConnectionUsage Members

        public IPhysicalConnection Connection
        {
            get { return m_conn; }
            set { PhysicalConnectionExtension.SafeChangeConnection(ref m_conn, value); }
        }

        #endregion

        public DatabaseSourceCaps OfflineDatabaseCaps
        {
            get
            {
                return new DatabaseSourceCaps
                {
                    AllFlags = false,
                    CreateTable = true,
                    FixedDataDefiner = true,
                    PreferStructure = true,
                };
            }
        }

        public DatabaseSourceCaps DatabaseCaps
        {
            get
            {
                var res = OfflineDatabaseCaps;
                res.MultipleSchema = SupportsMultipleSchema();
                res.Domains = Dialect != null ? Dialect.DialectCaps.Domains : true;
                return res;
            }
        }

        public AlterProcessorCaps AlterCaps
        {
            get { return DbStruct.AlterCaps; }
        }

        public void RunNameTransformation(INameTransformation transform)
        {
            Reload();
            DbStruct.RunNameTransformation(transform);
            SaveToFile();
        }

        public void RunTransformation(Action<DatabaseStructure> action)
        {
            Reload();
            action(DbStruct);
            SaveToFile();
        }

        public TableSourceCaps TableCaps { get { return TableDefSource.CreateBehaviour(); } }

        public List<ISpecificObjectType> GetSpecificTypes()
        {
            if (Dialect != null) return Dialect.GetSpecificTypes();
            List<ISpecificObjectType> res = new List<ISpecificObjectType>();
            foreach (string objtype in DbStruct.SpecificObjects.Keys)
            {
                res.Add(new GenericSpecificObjectType(objtype));
            }
            return res;
        }

        //public void CreateSchema(string name)
        //{
        //    Reload();
        //    m_db.Schemata.Add(new SchemaStructure { SchemaName = name });
        //    SaveToFile();
        //}

        //public void DropSchema(string name)
        //{
        //    Reload();
        //    m_db.Schemata.RemoveIf(schema => schema.SchemaName == name);
        //    SaveToFile();
        //}

        //internal ITableStructure MangleTable(ITableStructure table)
        //{
        //    if (!SupportsMultipleSchema)
        //    {
        //        TableStructure tmp = new TableStructure(table);
        //        tmp.RunNameTransformation(new SetSchemaNameTransformation(null));
        //        return tmp;
        //    }
        //    return table;
        //}

        [Browsable(false)]
        public override AddonType AddonType
        {
            get { return DatabaseSourceAddonType.Instance; }
        }

        public DbDefProperties LoadProps()
        {
            return m_conn.DbStruct.GetProps();
        }

        public void SaveProps(DbDefProperties props)
        {
            Reload();
            m_conn.DbStruct.SetProps(props);
            SaveToFile();
        }

        public override string ToString()
        {
            return ToString("S", null);
        }

        #region IFormattable Members

        public string ToString(string format, IFormatProvider formatProvider)
        {
            if (m_conn == null) return base.ToString();
            return m_conn.ToString(format, formatProvider);
        }

        #endregion
    }
}
