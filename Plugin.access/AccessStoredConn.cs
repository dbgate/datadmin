using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.Common;
using System.IO;
using System.ComponentModel;

namespace Plugin.access
{
    [StoredConnection(Name = "access", Title = "MS Access")]
    public class AccessStoredConnection : DbFileStoredConnection
    {
        string m_provider;

        [XmlElem]
        [DatAdmin.DisplayName("s_provider")]
        public string Provider
        {
            get { return m_provider; }
            set { m_provider = value; }
        }

        private bool m_useAutentization;
        [Category("s_autentization")]
        [DatAdmin.DisplayName("s_use_autentization")]
        [XmlElem]
        public bool UseAutentization
        {
            get { return m_useAutentization; }
            set { m_useAutentization = value; }
        }

        string m_password;
        [PasswordPropertyText(true)]
        [Category("s_autentization")]
        [DatAdmin.DisplayName("s_password")]
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
        [XmlElem]
        [Category("s_autentization")]
        [DatAdmin.DisplayName("s_login")]
        public string Login
        {
            get { return m_login; }
            set { m_login = value; }
        }

        string m_systemDatabase;
        [XmlElem]
        [Category("s_autentization")]
        [DatAdmin.DisplayName("s_system_database")]
        public string SystemDatabase
        {
            get { return m_systemDatabase; }
            set { m_systemDatabase = value; }
        }

        public override DbProviderFactory GetFactory()
        {
            return OleDbFactory.Instance;
        }

        public override ISqlDialect GetDefaultDialect()
        {
            return new AccessDialect();
        }

        public override string GenerateConnectionString(bool includepwd)
        {
            if (UseAutentization)
            {
                string res = String.Format("Provider={0};Data Source={1};Database Password={2}", Provider, DbFilename, includepwd ? Password : "*****");
                if (!Login.IsEmpty()) res += ";User ID=" + Login;
                if (!SystemDatabase.IsEmpty()) res += ";System Database=" + SystemDatabase;
                return res;
            }
            else
            {
                return String.Format("Provider={0};Data Source={1}", Provider, DbFilename);
            }
        }

        public string GenerateNoPwdNoShareConnectionString()
        {
            return String.Format("Provider={0};Data Source={1};Mode=Share Exclusive", Provider, DbFilename);
        }

        public const string MdbProvider = "Microsoft.Jet.OLEDB.4.0";
        public const string AccdbProvider = "Microsoft.ACE.OLEDB.12.0";

        public static string GetProviderForFile(string filename)
        {
            if (filename.ToLower().EndsWith(".accdb")) return AccdbProvider;
            else return MdbProvider;
        }
    }

    //[NodeFactory(Name = "access")]
    //public class AccessConnectionFactory : NodeFactoryBase
    //{
    //    public override ITreeNode FromFile(ITreeNode parent, string file)
    //    {
    //        string dbfile = file;
    //        if (IOTool.FileIsLink(file)) dbfile = IOTool.GetLinkContent(file);
    //        if (dbfile.ToLower().EndsWith(".mdb") || dbfile.ToLower().EndsWith(".accdb"))
    //        {
    //            //SQLiteConnection sql = new SQLiteConnection(String.Format("Data Source={0}", file));
    //            IDatabaseSource conn = AccessDbFileHandler.OpenDatabase(dbfile, file + ".con");
    //            return new Database_SourceConnectionTreeNode(conn, parent, file, conn.Connection.StoredConnection, false);
    //        }
    //        return null;


    //        //DbProviderFactory fact = OleDbFactory.Instance;
    //        //string dbfile = file;
    //        //if (IOTool.FileIsLink(file)) dbfile = IOTool.GetLinkContent(file);

    //        //if (dbfile.ToLower().EndsWith(".accdb"))
    //        //{
    //        //    AccessStoredConnection stored = new AccessStoredConnection { DbFilename = dbfile, Provider = "Microsoft.ACE.OLEDB.12.0" };
    //        //    string conns = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0}", dbfile);
    //        //    GenericDbConnection physconn = new GenericDbConnection(conns, fact, Path.GetFileNameWithoutExtension(file), stored.GetDialect(), stored);
    //        //    stored.InstallHooks(physconn);
    //        //    IDatabaseSource conn = new GenericDatabaseSource(null, physconn, null);
    //        //    return new Database_SourceConnectionTreeNode(conn, parent, file, stored, false);
    //        //}

    //        //if (dbfile.ToLower().EndsWith(".mdb"))
    //        //{
    //        //    AccessStoredConnection stored = new AccessStoredConnection { DbFilename = dbfile, Provider = "Microsoft.Jet.OLEDB.4.0" };
    //        //    string conns = String.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0}", dbfile);
    //        //    GenericDbConnection physconn = new GenericDbConnection(conns, fact, Path.GetFileNameWithoutExtension(file), stored.GetDialect(), stored);
    //        //    stored.InstallHooks(physconn);
    //        //    IDatabaseSource conn = new GenericDatabaseSource(null, physconn, null);
    //        //    return new Database_SourceConnectionTreeNode(conn, parent, file, stored, false);
    //        //}

    //        //return null;
    //    }
    //}
}
