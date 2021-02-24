using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.IO;
using System.Data.Common;
using System.Data.EffiProz;

namespace Plugin.effiproz
{
    [FileHandler(Name = "effiproz")]
    public class EfzFileFormat : FileBasedDatabaseHandler
    {
        protected override DbFileStoredConnection CreateStoredConnection()
        {
            var stored = new EfzStoredConnection();
            stored.Login = "sa";
            stored.Password = "";
            return stored;
        }

        //public override void Load(string path)
        //{
        //    EfzStoredConnection con = new EfzStoredConnection();
        //    con.DbFilename = path;
        //    string ufn = IOTool.GetUniqueFileName(Path.Combine(Core.DataDirectory, Path.GetFileNameWithoutExtension(path) + ".con"));
        //    con.Filename = ufn;
        //    con.Save();
        //}

        public override string Extension
        {
            get { return "properties"; }
        }

        public override string Description
        {
            get { return "EffiProz database"; }
        }
    }

    //[DbFileHandler(Name = "effiproz_handler", Title = "EffiProz .properties file handler")]
    //public class EfzDbFileHandler : DbFileHandlerBase
    //{
    //    public override IDatabaseSource OpenFile(string filename)
    //    {
    //        return OpenDatabase(filename, filename + ".con");
    //    }

    //    public override bool CanOpenFile(string filename)
    //    {
    //        return filename.ToLower().EndsWith(".properties");
    //    }

    //    public static IDatabaseSource OpenDatabase(string dbfile, string confile)
    //    {
    //        EfzStoredConnection stored = LoadStoredConnection<EfzStoredConnection>(confile, dbfile);
    //        stored.Login = "sa";
    //        stored.Password = "";
    //        GenericDbConnection physconn = new GenericDbConnection(stored);
    //        stored.InstallHooks(physconn);
    //        IDatabaseSource conn = new GenericDatabaseSource(null, physconn, null);
    //        return conn;
    //    }
    //}
}
