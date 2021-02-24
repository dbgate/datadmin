using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using System.Data.Common;
using System.ComponentModel;
using System.Xml;

namespace DatAdmin
{
    public abstract class StoredConnection : AddonBase, IStoredConnection
    {
        //public void Save(string file)
        //{
        //    XmlSerializer ser = new XmlSerializer(typeof(T));
        //    using (FileStream fw = new FileStream(file, FileMode.Create))
        //    {
        //        ser.Serialize(fw, this);
        //    }
        //}
        string m_filename;
        //Dictionary<string, SettingsPageCollection> m_dbSettings;

        //SettingsPageCollection m_connectionSetings;

        [Browsable(false)]
        public string FileName
        {
            get { return m_filename; }
            set { m_filename = value; }
            //set
            //{
            //    if (value == m_filename) return;
            //    if (m_filename != null) throw new Exception("Filename can be set only once");
            //    m_filename = value;
            //}
        }

        [Browsable(false)]
        public string AddonFileName
        {
            get { return m_filename; }
            set { m_filename = value; }
        }

        [Browsable(false)]
        [XmlElem]
        public string DataTreeName
        {
            get { return TreeNodeExtension.FileNameToDataTreeName(m_filename); }
            set { m_filename = TreeNodeExtension.DataTreeNameToFileName(value); }
        }

        public override void SaveToXml(XmlElement xml)
        {
            base.SaveToXml(xml);
            if (DataTreeName == null && FileName != null) xml.AddChild("RealFileName").InnerText = FileName;
        }

        public override void LoadFromXml(XmlElement xml)
        {
            base.LoadFromXml(xml);
            var rfn = xml.FindElement("RealFileName");
            if (rfn != null) FileName = rfn.InnerText;
        }

        public string GetDatabasePrivateFolder(string dbname)
        {
            if (dbname != null && DatabaseMode == ConnectionDatabaseMode.All)
            {
                return Path.Combine(m_filename + ".databases", dbname);
            }
            return null;
        }

        public string GetDatabasePrivateSubFolder(string dbname, string folderName)
        {
            if (dbname != null && DatabaseMode == ConnectionDatabaseMode.All)
            {
                return Path.Combine(Path.Combine(m_filename + ".databases", dbname), folderName);
            }
            else
            {
                return m_filename + "." + folderName;
            }
        }

        [Browsable(false)]
        public SettingsPageCollection ConnectionSettings
        {
            get
            {
                if (m_filename != null)
                {
                    return SettingsPageCollection.Get(GlobalSettings.DialectSettings[GetDialect().DialectName], m_filename + ".settings");
                }
                return GlobalSettings.DialectSettings[GetDialect().DialectName];
            }
        }

        public SettingsPageCollection GetDatabaseSettings(string dbname)
        {
            if (DatabaseMode != ConnectionDatabaseMode.All) return null;
            if (dbname == null) return null;
            return SettingsPageCollection.Get(ConnectionSettings, Path.Combine(Path.Combine(FileName + ".databases", dbname), "settings.xml"));
        }

        //public override void SaveToXml(XmlElement xml)
        //{
        //    foreach (StoredConnectionAttribute attr in this.GetType().GetCustomAttributes(typeof(StoredConnectionAttribute), false))
        //    {
        //        xml.SetAttribute("type", attr.Name);
        //    }
        //    this.SaveProperties(xml);
        //}
        //public override void LoadFromXml(XmlElement xml)
        //{
        //    this.LoadProperties(xml);
        //}

        [Browsable(false)]
        public abstract ConnectionDatabaseMode DatabaseMode { get; set; }
        //[Browsable(false)]
        //public abstract bool IsSingleDatabase { get; }
        [Browsable(false)]
        public abstract string ExplicitDatabaseName { get; set; }

        [Browsable(false)]
        public virtual ConnectionEditFrame CreateEditor() { return new PropertyGridConnFrame(this); }
        public abstract string GenerateConnectionString(bool includepwd);
        [Browsable(false)]
        public virtual string ConnectionTypeTitle { get { return "s_connection"; } }
        public abstract ISqlDialect GetDefaultDialect();
        [Browsable(false)]
        public virtual bool SupportsDatabaseSelect { get { return false; } }
        [Browsable(false)]
        public virtual string HelpTopic { get { return null; } }

        AddonHolder m_sqlDialect = null;

        [Category("SQL")]
        [DisplayName("s_dialect")]
        [RegisterItemType(typeof(AddonHolder))]
        [TypeConverter(typeof(RegisterItemTypeConverter))]
        [Browsable(false)]
        public AddonHolder SqlDialect
        {
            get { return m_sqlDialect; }
            set { m_sqlDialect = value; }
        }

        public virtual bool Edit()
        {
            return ConnWizardForm.Edit(this);
        }

        //public static T Load(string file)
        //{
        //    XmlDocument doc = new XmlDocument();
        //    doc.Load(file);
        //    T con = (T)StoredConnectionRegister.Instance.LoadConnection(doc.DocumentElement);
        //    con.ConnectionFilePath = IOTool.RelativePathTo(Core.DataDirectory, file);
        //    return con;
        //}

        public virtual string GetLogin() { return null; }
        public virtual string GetDataSource() { return null; }

        public abstract DbProviderFactory GetFactory();

        public virtual DbConnection CreateSystemConnection()
        {
            DbProviderFactory fact = GetFactory();
            DbConnection conn = fact.CreateConnection();
            conn.ConnectionString = GenerateConnectionString(true);
            return conn;
        }

        public virtual ISqlDialect GetDialect()
        {
            if (m_sqlDialect != null) return (ISqlDialect)m_sqlDialect.CreateInstance();
            return GetDefaultDialect();
        }

        public virtual IDialectDetector GetDialectDetector()
        {
            if (m_sqlDialect != null) return new FixedDialectDetector((ISqlDialect)m_sqlDialect.CreateInstance());
            return GetDefaultDialectDetector();
        }

        public virtual IDialectDetector GetDefaultDialectDetector()
        {
            return new FixedDialectDetector(GetDefaultDialect());
        }

        public virtual IProviderHooks CreateHooks() { return null; }

        public virtual IPhysicalConnection CreatePhysicalConnection(string connectionString)
        {
            var fact = new GenericDirectDbConnectionFactory { Stored = this, ConnectionString = connectionString };
            return fact.CreateConnection();
        }

        public virtual IPhysicalConnection CreatePhysicalConnection()
        {
            var res = new GenericDbConnection(this);
            return res;
        }

        public virtual void InstallHooks(IPhysicalConnection conn)
        {
            conn.AfterCreateConnection += new PhysicalConnectionDelegate(AfterCreateConnection);
        }

        protected virtual void AfterCreateConnection(IPhysicalConnection conn)
        {
        }

        //protected virtual void GetConnectionProperties(IPhysicalConnection conn, IDictionary<string, object> properties)
        //{
        //    foreach (StoredConnectionAttribute attr in GetType().GetCustomAttributes(typeof(StoredConnectionAttribute), true))
        //    {
        //        properties["dbengine"] = attr.Name;
        //    }
        //}

        [XmlElem("dialect")]
        [Browsable(false)]
        public string DialectOverride
        {
            get
            {
                if (m_sqlDialect != null) return m_sqlDialect.Name;
                return null;
            }
            set
            {
                if (value == null) m_sqlDialect = null;
                else m_sqlDialect = DialectAddonType.Instance.FindHolder(value);
                if (ConnectionSettings != null && ConnectionSettings != GlobalSettings.DialectSettings[GetDialect().DialectName]) ConnectionSettings.SetBasePages(GlobalSettings.DialectSettings[GetDialect().DialectName]);
            }
        }

        public void Save()
        {
            XmlDocument doc = XmlTool.CreateDocument("Connection");
            SaveToXml(doc.DocumentElement);
            doc.Save(FileName);
        }

        public static IStoredConnection LoadFromFile(string file)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(file);
            IStoredConnection con = (IStoredConnection)StoredConnectionAddonType.Instance.LoadAddon(doc.DocumentElement);
            con.FileName = file;
            return con;
        }

        [Browsable(false)]
        public override AddonType AddonType
        {
            get { return StoredConnectionAddonType.Instance; }
        }

        protected virtual void GetConnectionKey(CacheKeyBuilder ckb)
        {
            ckb.Add(DialectOverride);
            ckb.Add(DatabaseMode);
            ckb.Add(ExplicitDatabaseName);
            ckb.Add(GetType().FullName);
        }

        public virtual string ConnectionKey
        {
            get
            {
                var ckb = new CacheKeyBuilder();
                GetConnectionKey(ckb);
                return ckb.CacheKey;
            }
        }

        public override string ToString()
        {
            if (m_filename != null) return Path.GetFileNameWithoutExtension(m_filename);
            if (GetDataSource() != null) return GetDataSource();
            return "(" + XmlTool.GetRegisterType(this) + ")";
        }
    }

    public abstract class MultiDatabaseStoredConnection : StoredConnection
    {
        private string m_explicitDatabaseName;

        [Browsable(false)]
        [XmlElem("Database")]
        public override string ExplicitDatabaseName
        {
            get { return m_explicitDatabaseName; }
            set { m_explicitDatabaseName = value; }
        }

        ConnectionDatabaseMode m_databaseMode;

        [Browsable(false)]
        [XmlElem("DatabaseMode")]
        public override ConnectionDatabaseMode DatabaseMode
        {
            get { return m_databaseMode; }
            set { m_databaseMode = value; }
        }

        //[Browsable(false)]
        //public override bool IsSingleDatabase
        //{
        //    get { return m_explicitDatabaseName != null; }
        //}

        [Browsable(false)]
        public override bool SupportsDatabaseSelect { get { return true; } }

        public string DatabaseName
        {
            get
            {
                if (DatabaseMode == ConnectionDatabaseMode.Explicit) return ExplicitDatabaseName;
                return null;
            }
            set
            {
                if (value == null)
                {
                    DatabaseMode = ConnectionDatabaseMode.All;
                }
                else if (value == "")
                {
                    DatabaseMode = ConnectionDatabaseMode.Default;
                    ExplicitDatabaseName = value;
                }
                else
                {
                    DatabaseMode = ConnectionDatabaseMode.Explicit;
                    ExplicitDatabaseName = value;
                }
            }
        }
    }

    public abstract class DbFileStoredConnection : StoredConnection
    {
        string m_dbFilename;

        [XmlElem("Filename")]
        public string DbFilename
        {
            get { return m_dbFilename; }
            set { m_dbFilename = value; }
        }

        protected override void GetConnectionKey(CacheKeyBuilder ckb)
        {
            base.GetConnectionKey(ckb);
            ckb.Add(IOTool.NormalizePath(FileName));
        }

        public override string ToString()
        {
            return Path.GetFileNameWithoutExtension(m_dbFilename);
        }

        [Browsable(false)]
        public override string ExplicitDatabaseName
        {
            get { return null; }
            set { }
        }

        [Browsable(false)]
        public override ConnectionDatabaseMode DatabaseMode
        {
            get { return ConnectionDatabaseMode.Default; }
            set { }
        }

        //[Browsable(false)]
        //public override bool IsSingleDatabase
        //{
        //    get { return true; }
        //}

        public override string GetDataSource()
        {
            return Path.GetFileName(DbFilename);
        }
    }

    [FileHandler(Name = "storedconn")]
    public class StoredConnectionFileHandler : FileHandlerBase
    {
        public override FileHandlerCaps Caps
        {
            get
            {
                return new FileHandlerCaps
                {
                    AllFlags = false,
                    CreateNode = true,
                };
            }
        }

        public override string Extension
        {
            get { return "con"; }
        }

        public override ITreeNode CreateNode(ITreeNode parent)
        {
            IStoredConnection con = StoredConnection.LoadFromFile(m_file.DataDiskPath);
            return ConnectionTreeNode.FromStoredConnection(parent, con, this);
        }
    }

    //[NodeFactory(Name = "storedconn")]
    //public class StoredConnectionFactory : NodeFactoryBase
    //{
    //    public override ITreeNode FromFile(ITreeNode parent, string file)
    //    {
    //        if (file.ToLower().EndsWith(".con"))
    //        {
    //            try
    //            {
    //                IStoredConnection con = StoredConnection.LoadFromFile(file);
    //                return ConnectionTreeNode.FromStoredConnection(parent, con, file);
    //            }
    //            catch (Exception)
    //            {
    //                return null;
    //            }
    //        }
    //        return null;
    //    }
    //}
}
