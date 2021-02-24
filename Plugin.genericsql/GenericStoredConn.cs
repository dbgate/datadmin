using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.Xml.Serialization;
using System.Data.Common;
using System.IO;
using System.ComponentModel;

namespace Plugin.genericsql
{
    [StoredConnection(Name = "generic", Title = "Generic")]
    public class GenericSqlStoredConnection : MultiDatabaseStoredConnection
    {
        //public class EditableStoredConnection : PropertyPageBase, IExplicitSaveableObject
        //{
        //    DbConnectionStringBuilder m_builder;
        //    GenericSqlStoredConnection m_conn;

        //    [TypeConverter(typeof(ExpandableObjectConverter))]
        //    [DatAdmin.DisplayName("s_connection")]
        //    public DbConnectionStringBuilder Builder
        //    {
        //        get { return m_builder; }
        //        set { m_builder = value; }
        //    }

        //    //bool m_isSingleDatabase;
        //    //[TypeConverter(typeof(YesNoTypeConverter))]
        //    //[DatAdmin.DisplayName("s_single_database")]
        //    //public bool IsSingleDatabase
        //    //{
        //    //    get { return m_isSingleDatabase; }
        //    //    set { m_isSingleDatabase = value; }
        //    //}

        //    public void ExplicitSave()
        //    {
        //        m_conn.ConnectionString = m_builder.ConnectionString;
        //        m_conn.IsSingleDatabase_Origin = IsSingleDatabase;
        //    }

        //    public EditableStoredConnection(GenericSqlStoredConnection conn)
        //    {
        //        m_conn = conn;
        //    }
        //}


        //public bool OneDatabase;
        [XmlElem]
        [Browsable(false)]
        public string ConnectionString { get; set; }
        [XmlElem]
        [Browsable(false)]
        public string FactoryName { get; set; }
        [XmlElem]
        [Browsable(false)]
        public string DriverName { get; set; }

        //bool m_isSingleDatabase;

        //public override bool Edit()
        //{
        //    DbProviderFactory factory = GetFactory();
        //    EditableStoredConnection ed = new EditableStoredConnection();
        //    DbConnectionStringBuilder bld = factory.CreateConnectionStringBuilder();
        //    bld.ConnectionString = ConnectionString;
        //    ed.Builder = bld;
        //    ed.IsSingleDatabase = IsSingleDatabase;
        //    if (EditPropertiesForm.Run(ed, true))
        //    {
        //        ConnectionString = bld.ConnectionString;
        //        IsSingleDatabase_Origin = ed.IsSingleDatabase;
        //        return true;
        //    }
        //    return false;
        //}

        public override ConnectionEditFrame CreateEditor()
        {
            return new GenericConnEditor(this);
        }

        public override DbProviderFactory GetFactory()
        {
            if (FactoryName != null) return DbProviderFactories.GetFactory(FactoryName);
            if (DriverName != null) return DbDriverManager.Instance.CreateFactory(DriverName);
            return null;
        }

        public override ISqlDialect GetDefaultDialect()
        {
            return new GenericDialect();
        }

        public override IDialectDetector GetDefaultDialectDetector()
        {
            return new DialectAutoDetector();
        }

        public override string GenerateConnectionString(bool includepwd)
        {
            return ConnectionString;
        }

        protected override void AfterCreateConnection(IPhysicalConnection conn)
        {
            GenericDialect dialect = ((GenericDbConnection)conn).Dialect as GenericDialect;
            //if (dialect != null) dialect.Connection = conn.SystemConnection;
            if (DatabaseMode == ConnectionDatabaseMode.Explicit) conn.SystemConnection.SafeChangeDatabase(ExplicitDatabaseName);
        }

        [Browsable(false)]
        public override bool SupportsDatabaseSelect { get { return true; } }

        //public override bool IsSingleDatabase
        //{
        //    get { return m_isSingleDatabase; }
        //}

        //[XmlElem("IsSingleDatabase")]
        //public bool IsSingleDatabase_Origin
        //{
        //    get { return m_isSingleDatabase; }
        //    set { m_isSingleDatabase = value; }
        //}

        //[XmlElem]
        //public override string ExplicitDatabaseName { get; set; }

        protected override void GetConnectionKey(CacheKeyBuilder ckb)
        {
            base.GetConnectionKey(ckb);
            ckb.Add(ConnectionString);
            ckb.Add(FactoryName);
            ckb.Add(DriverName);
        }
    }
}
