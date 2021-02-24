using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using DatAdmin;
using System.IO;
using System.ComponentModel;

namespace Plugin.sqlite
{
    [StoredConnection(Name = "sqlite", Title = "SQLite")]
    public class SQLiteStoredConnection : DbFileStoredConnection
    {
        public override DbProviderFactory GetFactory()
        {
            return SqliteDriver.GetFactory();
        }

        public override ISqlDialect GetDefaultDialect()
        {
            return new SqliteDialect();
        }

        public override string GenerateConnectionString(bool includepwd)
        {
            return String.Format("Data Source={0}", DbFilename);
        }
    }

    //[NodeFactory(Name = "sqlite")]
    //public class ConnectionFactory : NodeFactoryBase
    //{
    //    public override ITreeNode FromFile(ITreeNode parent, string file)
    //    {
    //        //DbProviderFactory fact = SqliteDriver.GetFactory();
    //        string dbfile = file;
    //        if (IOTool.FileIsLink(file)) dbfile = IOTool.GetLinkContent(file);
    //        if (dbfile.ToLower().EndsWith(".db3") || dbfile.ToLower().EndsWith(".sqlite"))
    //        {
    //            //SQLiteConnection sql = new SQLiteConnection(String.Format("Data Source={0}", file));
    //            IDatabaseSource conn = SqliteDbFileHandler.OpenDatabase(dbfile, file + ".con");
    //            return new Database_SourceConnectionTreeNode(conn, parent, file, conn.Connection.StoredConnection, false);
    //        }
    //        return null;
    //    }
    //}

    [DialectAutoDetector(Name = "sqlite")]
    public class SqliteDialectAutoDetector : DialectAutoDetectorBase
    {
        public override bool TestCommand(DbConnection conn)
        {
            conn.ExecuteScalar("select count(*) from sqlite_master");
            return true;
        }
        public override ISqlDialect GetDialect()
        {
            return new SqliteDialect();
        }
        public override ISqlDialect DetectDialect(string displayName)
        {
            if (displayName.ToLower().StartsWith("sqlite")) return new SqliteDialect();
            return null;
        }
    }
}