using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Xml;
using System.ComponentModel;

namespace DatAdmin
{
    public abstract class GenericConnectionUsage : PropertyPageBase, IConnectionUsage
    {
        protected IPhysicalConnection m_conn;
        public GenericConnectionUsage() { }

        public GenericConnectionUsage(IPhysicalConnection conn)
        {
            m_conn = conn;
        }

        protected DbConnection DbConn { get { return m_conn.SystemConnection; } }

        #region ICommonSource Members

        [Browsable(false)]
        public IPhysicalConnection Connection
        {
            get { return m_conn; }
            set { PhysicalConnectionExtension.SafeChangeConnection(ref m_conn, value); }
        }

        #endregion
    }

    public class GenericServerSource : GenericConnectionUsage, IServerSource
    {
        public GenericServerSource(IPhysicalConnection conn)
            : base(conn)
        {
        }

        #region IServerConnection Members

        public IEnumerable<string> Databases
        {
            get
            {
                m_conn.ReconnectIfBroken();
                return m_conn.Dialect.GetDatabaseNames(m_conn);
            }
        }

        public IDatabaseSource GetDatabase(string name)
        {
            return new GenericDatabaseSource(this, m_conn, name);
        }

        //public IServerSource Clone()
        //{
        //    return new GenericServerSource(m_conn.Clone());
        //}

        //public IQueryConnection CloneAndQuery()
        //{
        //    return new GenericQueryConnection(m_conn.Clone());
        //}

        public IPhysicalConnection CloneConnection()
        {
            return m_conn.Clone();
        }

        public IDatabaseSource CreateDatabase(string dbname, Dictionary<string, string> props)
        {
            m_conn.InvokeScript(dmp => { dmp.CreateDatabase(dbname, props); }, null);
            return new GenericDatabaseSource(this, m_conn, dbname);
        }

        public IServerSource MergeToConnection(IPhysicalConnection conn)
        {
            if (conn.GetConnKey() != m_conn.GetConnKey()) throw new InternalError("DAE-00030 Cannot merge connections with different keys");
            GenericServerSource res = new GenericServerSource(conn);
            return res;
        }

        #endregion
    }

    [DatabaseSource(Name = "generic")]
    public class GenericDatabaseSource : GenericConnectionUsage, IDatabaseSource, IAddonInstance
    {
        string m_dbname = null;
        IServerSource m_server;

        public GenericDatabaseSource(IServerSource server, IPhysicalConnection conn, string dbname)
            : base(conn)
        {
            m_dbname = dbname;
            m_server = server;
        }

        [DisplayName("s_database")]
        public string GuiDatabase
        {
            get { return dbname; }
            set { dbname = value; }
        }

        #region Serialize support

        [XmlAttrib]
        [Browsable(false)]
        public string dbname
        {
            get { return m_dbname; }
            set { m_dbname = value; }
        }

        public GenericDatabaseSource()
        {
        }

        public void SaveToXml(XmlElement xml)
        {
            if (m_conn != null && m_conn.StoredConnection != null)
            {
                m_conn.StoredConnection.SaveToXml(xml.AddChild("Connection"));
            }
            this.SavePropertiesCore(xml, true);
        }

        public void LoadFromXml(XmlElement xml)
        {
            this.LoadPropertiesCore(xml);
            var xconn = xml.FindElement("Connection");
            if (xconn != null)
            {
                IStoredConnection sconn = (IStoredConnection)StoredConnectionAddonType.Instance.LoadAddon(xconn);
                m_conn = sconn.CreatePhysicalConnection();
            }
        }

        #endregion

        [DisplayName("s_connection")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public IStoredConnection GuiStored
        {
            get { return m_conn.StoredConnection; }
        }

        #region IDatabaseSource Members

        [Browsable(false)]
        public ISqlDialect Dialect { get { return Connection.Dialect; } }

        public string GetPrivateSubFolder(string name)
        {
            return m_conn.PhysicalFactory.GetDatabasePrivateSubFolder(m_dbname, name);
        }

        public string GetPrivateFolder()
        {
            return m_conn.PhysicalFactory.GetDatabasePrivateFolder(m_dbname);
        }

        [Browsable(false)]
        public SettingsPageCollection Settings
        {
            get { return m_conn.FindSettings(m_dbname); }
        }

        public IDatabaseStructure LoadDatabaseStructure(DatabaseStructureMembers members, IProgressInfo progress)
        {
            return m_conn.Dialect.AnalyseDatabase(m_conn, m_dbname, members, progress);
        }

        public ITableSource GetTable(NameWithSchema table)
        {
            return new GenericTableSource(this, m_conn, m_dbname, table.Schema, table.Name);
        }

        public void ClearCaches() { }

        [Browsable(false)]
        public IServerSource Server
        {
            get { return m_server; }
        }

        public IPhysicalConnection CloneConnection()
        {
            IPhysicalConnection conn = m_conn.Clone();
            conn.SetOnOpenDatabase(m_dbname);
            return conn;
        }

        public ITableSource CreateTable(ITableStructure table)
        {
            m_conn.InvokeScript(dmp => { dmp.CreateTable(table); }, m_dbname);
            return GetTable(table.FullName);
        }

        [Browsable(false)]
        public DatabaseSourceCaps OfflineDatabaseCaps
        {
            get
            {
                return new DatabaseSourceCaps
                {
                    AllFlags = true,
                    PreferStructure = false,
                    IsPhantom = false,
                    ReadOnly = false,
                    FixedDataDefiner = false,
                };
            }
        }

        [Browsable(false)]
        public DatabaseSourceCaps DatabaseCaps
        {
            get
            {
                var res = OfflineDatabaseCaps;
                res.MultipleSchema = m_conn.Dialect.DialectCaps.MultipleSchema;
                res.Domains = m_conn.Dialect.DialectCaps.Domains;
                return res;
            }
        }
        [Browsable(false)]
        public TableSourceCaps TableCaps
        {
            get { return GenericDatabaseSource.CreateCaps(m_conn.Dialect); }
        }

        [Browsable(false)]
        public string DatabaseName { get { return m_dbname; } }

        [Browsable(false)]
        public string DefaultSchema
        {
            get
            {
                if (m_conn.Dialect.DialectCaps.UseDatabaseAsSchema) return m_dbname;
                return Dialect.DefaultSchema;
            }
        }

        public IDatabaseSource CloneSource()
        {
            IPhysicalConnection conn = CloneConnection();
            return new GenericDatabaseSource(new GenericServerSource(conn), conn, m_dbname);
        }

        public IDatabaseSource MergeToConnection(IPhysicalConnection conn)
        {
            if (conn.GetConnKey() != m_conn.GetConnKey()) throw new InternalError("DAE-00031 Cannot merge connections with different groups");
            IServerSource ressrv = null;
            if (Server != null) ressrv = Server.MergeToConnection(conn);
            GenericDatabaseSource res = new GenericDatabaseSource(ressrv, conn, m_dbname);
            return res;
        }

        [Browsable(false)]
        public virtual List<string> DefinedSpecificObjects
        {
            get { return m_conn.Dialect.GetDefinedSpecificObjects(); }
        }

        public ObjectOperationCaps GetSupportedOperations(string objtype)
        {
            return m_conn.Dialect.DumperCaps[objtype];
        }

        #endregion

        internal static TableSourceCaps CreateCaps(ISqlDialect dialect)
        {
            return new TableSourceCaps
            {
                AllFlags = true,
            };
        }

        public override string ToString()
        {
            return String.Format("{0}[{1}]", m_conn, m_dbname);
        }

        public List<ISpecificObjectType> GetSpecificTypes()
        {
            //string dbversion;
            //try { dbversion = m_conn.SystemConnection.ServerVersion; }
            //catch { dbversion = null; }
            return m_conn.Dialect.GetSpecificTypes();
        }

        [Browsable(false)]
        public AlterProcessorCaps AlterCaps
        {
            get { return Dialect.DumperCaps; }
        }

        public void AlterDatabase(IDatabaseStructure src, IDatabaseStructure dst, DbDiffOptions opts)
        {
            this.InvokeScript(dmp => { dmp.AlterDatabase(src, dst, opts, this); }, null);
        }

        public void AlterDatabase(AlterPlan plan, DbDiffOptions opts)
        {
            this.InvokeScript(dmp => plan.CreateRunner().Run(dmp, opts), null, plan.DenyTransaction);
        }

        #region IAddonInstance Members

        [Browsable(false)]
        public AddonType AddonType
        {
            get { return DatabaseSourceAddonType.Instance; }
        }

        #endregion
    }
}
