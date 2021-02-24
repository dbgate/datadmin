using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.Data.Common;
using System.Data.EffiProz;
using System.IO;
using System.ComponentModel;
using System.Threading;
using System.Globalization;

namespace Plugin.effiproz
{
    [StoredConnection(Name = "effiproz", Title = "EffiProz")]
    public class EfzStoredConnection : DbFileStoredConnection
    {
        string m_password;
        [PasswordPropertyText(true)]
        [Category("s_autentization")]
        public string Password
        {
            get { return m_password; }
            set { m_password = value; }
        }

        [XmlElem("Password")]
        [Browsable(false)]
        public string XmlPassword
        {
            get { return XmlTool.SafeEncodeString(Password); }
            set { Password = XmlTool.SafeDecodeString(value); }
        }

        string m_login;
        [XmlElem("Login")]
        [Category("s_autentization")]
        public string Login
        {
            get { return m_login; }
            set { m_login = value; }
        }

        public override DbProviderFactory GetFactory()
        {
            return new EfzFactory();
        }

        public override ISqlDialect GetDefaultDialect()
        {
            return new EfzDialect();
        }

        public override string GenerateConnectionString(bool includepwd)
        {
            return EfzTool.GetConnectionString(DbFilename, Login, Password);
        }

        protected override void AfterCreateConnection(IPhysicalConnection conn)
        {
            base.AfterCreateConnection(conn);
            conn.AfterOpen += new PhysicalConnectionDelegate(conn_AfterOpen);
            conn.BeforeOpen += new PhysicalConnectionDelegate(conn_BeforeOpen);
        }

        void conn_BeforeOpen(IPhysicalConnection conn)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
        }

        void conn_AfterOpen(IPhysicalConnection conn)
        {
            ((EfzConnection)conn.SystemConnection).AutoCommit = true;
        }
    }

    //[NodeFactory(Name = "effiproz")]
    //public class ConnectionFactory : NodeFactoryBase
    //{
    //    public override ITreeNode FromFile(ITreeNode parent, string file)
    //    {
    //        //DbProviderFactory fact = SqliteDriver.GetFactory();
    //        string dbfile = file;
    //        if (IOTool.FileIsLink(file)) dbfile = IOTool.GetLinkContent(file);
    //        if (dbfile.ToLower().EndsWith(".properties"))
    //        {
    //            //SQLiteConnection sql = new SQLiteConnection(String.Format("Data Source={0}", file));
    //            IDatabaseSource conn = EfzDbFileHandler.OpenDatabase(dbfile, file + ".con");
    //            return new Database_SourceConnectionTreeNode(conn, parent, file, conn.Connection.StoredConnection, false);
    //        }
    //        return null;
    //    }
    //}

    [DialectAutoDetector(Name = "effiproz")]
    public class EfzDialectAutoDetector : DialectAutoDetectorBase
    {
        public override bool TestCommand(DbConnection conn)
        {
            conn.ExecuteScalar("select count(*) from sqlite_master");
            return true;
        }
        public override ISqlDialect GetDialect()
        {
            return new EfzDialect();
        }
        public override ISqlDialect DetectDialect(string displayName)
        {
            if (displayName.ToLower().StartsWith("effiproz")) return new EfzDialect();
            return null;
        }
    }
}
