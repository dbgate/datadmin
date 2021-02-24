using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.IO;
using System.Data.Common;

namespace Plugin.sqlite
{
    public abstract class SqliteFileHandler : FileBasedDatabaseHandler
    {
        //public override ITreeNode CreateNode(ITreeNode parent)
        //{
        //    IDatabaseSource conn = OpenDatabase(m_file.DataDiskPath, m_file.DiskPath + ".con");
        //    return new Database_SourceConnectionTreeNode(conn, parent, m_file.DiskPath, conn.Connection.StoredConnection, false);
        //}

        //public override IDatabaseSource OpenDatabase()
        //{
        //    return OpenDatabase(DiskPath, DiskPath + ".con");
        //}

        public override string Description
        {
            get { return "Sqlite database"; }
        }

        protected override DbFileStoredConnection CreateStoredConnection()
        {
            return new SQLiteStoredConnection();
        }

        //public static IDatabaseSource OpenDatabase(string dbfile, string confile)
        //{
        //    SQLiteStoredConnection stored = LoadStoredConnection<SQLiteStoredConnection>(confile, dbfile);
        //    var physconn = stored.CreatePhysicalConnection();
        //    IDatabaseSource conn = new GenericDatabaseSource(null, physconn, null);
        //    return conn;
        //}
    }

    [FileHandler(Name = "sqlite_db3")]
    public class Sqlite3FileFormat_Db3 : SqliteFileHandler
    {
        public override string Extension
        {
            get { return "db3"; }
        }

    }

    [FileHandler(Name = "sqlite_sqlite")]
    public class Sqlite3FileFormat_Sqlite : SqliteFileHandler
    {
        public override string Extension
        {
            get { return "sqlite"; }
        }
    }

    //[FileFormat(Name="sqliteall")]
    //public class SqliteAllFileFormat : SqliteFileFormat
    //{
    //    public override string Extension
    //    {
    //        get { return "*"; }
    //    }

    //    public override string Description
    //    {
    //        get { return "Sqlite database"; }
    //    }
    //}

    //[DbFileHandler(Name = "sqlite_handler", Title = "Sqlite .db3 and .sqlite file handler")]
    //public class SqliteDbFileHandler : DbFileHandlerBase
    //{
    //    public override IDatabaseSource OpenFile(string filename)
    //    {
    //        return OpenDatabase(filename, filename + ".con");
    //    }

    //    public override bool CanOpenFile(string filename)
    //    {
    //        return filename.ToLower().EndsWith(".db3") || filename.ToLower().EndsWith(".sqlite");
    //    }

    //    public static IDatabaseSource OpenDatabase(string dbfile, string confile)
    //    {
    //        SQLiteStoredConnection stored = LoadStoredConnection<SQLiteStoredConnection>(confile, dbfile);
    //        var physconn = stored.CreatePhysicalConnection();
    //        IDatabaseSource conn = new GenericDatabaseSource(null, physconn, null);
    //        return conn;
    //    }
    //}
}
