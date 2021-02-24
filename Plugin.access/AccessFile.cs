using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.IO;
using System.Data.Common;
using System.Data.OleDb;

namespace Plugin.access
{
    public abstract class AccessFileHandler : FileBasedDatabaseHandler
    {
        public abstract string Provider
        {
            get;
        }

        protected override DbFileStoredConnection CreateStoredConnection()
        {
            AccessStoredConnection stored = new AccessStoredConnection();
            stored.Provider = AccessStoredConnection.GetProviderForFile(m_file.DataDiskPath);
            return stored;
        }

        //public override void Load(string path)
        //{
        //    AccessStoredConnection con = new AccessStoredConnection();
        //    con.DbFilename = path;
        //    con.Provider = Provider;
        //    string ufn = IOTool.GetUniqueFileName(Path.Combine(Core.DataDirectory, Path.GetFileNameWithoutExtension(path) + ".con"));
        //    con.Filename = ufn;
        //    con.Save();
        //}
    }

    [FileHandler(Name = "mdb")]
    public class MdbFileHandler : AccessFileHandler
    {
        public override string Extension
        {
            get { return "mdb"; }
        }

        public override string Description
        {
            get { return "Aceess JET (*.mdb)"; }
        }

        public override string Provider
        {
            get { return "Microsoft.Jet.OLEDB.4.0"; }
        }
    }

    [FileHandler(Name = "accdb")]
    public class AccdbFileHandler : AccessFileHandler
    {
        public override string Extension
        {
            get { return "accdb"; }
        }

        public override string Description
        {
            get { return "Aceess ACE (*.accdb)"; }
        }

        public override string Provider
        {
            get { return "Microsoft.ACE.OLEDB.12.0"; }
        }
    }

    //[DbFileHandler(Name = "access_handler", Title = "Access .mdb and .accdb file handler")]
    //public class AccessDbFileHandler : DbFileHandlerBase
    //{
    //    public override IDatabaseSource OpenFile(string filename)
    //    {
    //        return OpenDatabase(filename, filename + ".con");
    //    }

    //    public override bool CanOpenFile(string filename)
    //    {
    //        return filename.ToLower().EndsWith(".mdb") || filename.ToLower().EndsWith(".accdb");
    //    }

    //    public static IDatabaseSource OpenDatabase(string dbfile, string confile)
    //    {
    //        DbProviderFactory fact = OleDbFactory.Instance;
    //        AccessStoredConnection stored = LoadStoredConnection<AccessStoredConnection>(confile, dbfile);
    //        stored.Provider = AccessStoredConnection.GetProviderForFile(dbfile);
    //        string conns = stored.GenerateConnectionString(true);
    //        var physconn = stored.CreatePhysicalConnection();
    //        stored.InstallHooks(physconn);
    //        IDatabaseSource conn = new GenericDatabaseSource(null, physconn, null);
    //        return conn;
    //    }
    //}
}
