using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace DatAdmin
{
    public class ConnectionParameter
    {
        [CommandLineParameter(Name = "connection", Description="connection string to database")]
        public string Connection { get; set; }

        [CommandLineParameter(Name = "driver", Description = "driver which should be used. List of available drivers can be obtained with command \"daci list driver\"")]
        public string Driver { get; set; }

        [CommandLineParameter(Name = "dbfile", Description="File with stored connection. Can be relative or absolute path, or path to connection tree (starts with \"data\", eg. data:test.con")]
        public string DbFile { get; set; }

        [CommandLineParameter(Name = "database", Description="Database name, if it can not be deducted from other parameters")]
        public string Database { get; set; }

        public IDatabaseSource GetConnection()
        {
            if (Driver != null)
            {
                if (Connection == null) throw new CommandLineError("DAE-00266 missing connection parameter");
                var sc = (IStoredConnection)StoredConnectionAddonType.Instance.FindHolder(Driver).CreateInstance();
                var conn = sc.CreatePhysicalConnection(Connection);
                if (Database != null)
                {
                    var srv = new GenericServerSource(conn);
                    var db = new GenericDatabaseSource(srv, conn, Database);
                    return db;
                }
                else
                {
                    var db = new GenericDatabaseSource(null, conn, null);
                    return db;
                }
            }
            if (DbFile != null)
            {
                string fn;
                if (DbFile.StartsWith("data:"))
                {
                    fn = Path.Combine(Core.DataDirectory, DbFile.Substring(5));
                }
                else
                {
                    fn = DbFile;
                }
                if (fn.ToLower().EndsWith(".con"))
                {
                    var doc = new XmlDocument();
                    doc.Load(fn);
                    var sc = (IStoredConnection)StoredConnectionAddonType.Instance.LoadAddon(doc.DocumentElement);
                    var conn = sc.CreatePhysicalConnection();
                    if (sc.DatabaseMode == ConnectionDatabaseMode.All)
                    {
                        if (Database == null) throw new CommandLineError("DAE-00267 database parameter missing");
                        var srv = new GenericServerSource(conn);
                        var db = new GenericDatabaseSource(srv, conn, Database);
                        return db;
                    }
                    else
                    {
                        return new GenericDatabaseSource(null, conn, null);
                    }
                }
                foreach (var hld in FileHandlerAddonType.Instance.CommonSpace.GetAllAddons())
                {
                    IFileHandler hnd = FileHandlerAddonType.FindFileHandler(new DiskFile(fn), han => han.Caps.OpenDatabase);
                    if (hnd != null) return hnd.OpenDatabase();
                }
                throw new CommandLineError("DAE-00268 Not registered dbfile extension, file must have extension .con, or one of listed in command \"daci list dbfilehandler\"");
            }
            throw new CommandLineError("DAE-00269 You must provide one of driver or dbfile parameters");
        }
    }
}
