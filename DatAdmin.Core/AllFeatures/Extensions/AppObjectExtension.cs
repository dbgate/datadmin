using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    public static class AppObjectExtension
    {
        public static IServerSource FindServerConnection(this AppObject appobj, ConnectionPack connpack)
        {
            var srv = appobj as ServerFieldsAppObject;
            if (srv == null) return null;
            if (connpack == null) return null;
            return srv.GetServerConnection(connpack);
        }

        public static IServerSource FindServerConnection(this AppObject appobj)
        {
            if (appobj == null || appobj.ConnPack == null) return null;
            return appobj.FindServerConnection(appobj.ConnPack);
        }

        public static IDatabaseSource FindDatabaseConnection(this AppObject appobj, ConnectionPack connpack)
        {
            if (appobj == null) return null;
            var db = appobj as DatabaseFieldsAppObject;
            if (db == null) return null;
            return db.GetDatabaseConnection(connpack);
        }

        public static IDatabaseSource FindDatabaseConnection(this AppObject appobj)
        {
            return appobj.FindDatabaseConnection(appobj.ConnPack);
        }

        public static IDatabaseSource CreateDatabaseConnection(this AppObject appobj)
        {
            var connpack = new ConnectionPack(typeof(AppObjectExtension));
            return FindDatabaseConnection(appobj, connpack).CloneSource();
        }

        public static string FindDatabaseName(this AppObject appobj)
        {
            if (appobj == null) return null;
            return appobj.GetDatabaseName();
        }

        public static IPhysicalConnection FindPhysicalConnection(this AppObject appobj, ConnectionPack connpack)
        {
            if (connpack == null) return null;
            return connpack.GetConnection(appobj.GetConnection(), false); ;
        }

        public static IPhysicalConnection FindPhysicalConnection(this AppObject appobj)
        {
            return appobj.FindPhysicalConnection(appobj.ConnPack);
}

        public static IPhysicalConnection CreatePhysicalConnection(this AppObject appobj)
        {
            var connpack = new ConnectionPack(typeof(AppObjectExtension));
            return FindPhysicalConnection(appobj, connpack).Clone();
        }

        public static ITableSource FindTableConnection(this AppObject appobj, ConnectionPack connpack)
        {
            var tbl = appobj as TableFieldsAppObject;
            if (tbl == null) return null;
            return tbl.GetDatabaseConnection(connpack).GetTable(tbl.DbObjectName);
        }

        public static DatabaseCache FindDatabaseCache(this AppObject appobj)
        {
            var cache = appobj.FindDatabaseConnection().GetCache();
            return cache;
        }
    }
}
