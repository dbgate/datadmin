using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    //[AppObject(Name = "stored_connection", Title = "s_stored_connection")]
    //public class StoredConnectionAppObject : ConnectionAppObject
    //{
    //    [XmlSubElem]
    //    public IStoredConnection StoredConn { get; set; }

    //    public override string GetDataSource()
    //    {
    //        return StoredConn.GetDataSource();
    //    }

    //    public override string ConnectionKey
    //    {
    //        get
    //        {
    //            if (StoredConn != null) return StoredConn.ConnectionKey;
    //            return null;
    //        }
    //    }

    //    public override IPhysicalConnection CreatePhysicalConnection()
    //    {
    //        return StoredConn.CreatePhysicalConnection();
    //    }

    //    public override string GetDatabasePrivateFolder(string dbname)
    //    {
    //        return StoredConn.GetDatabasePrivateFolder(dbname);
    //    }

    //    public override string GetDatabasePrivateSubFolder(string dbname, string folderName)
    //    {
    //        return StoredConn.GetDatabasePrivateSubFolder(dbname, folderName);
    //    }
    //}

    [PhysicalConnectionFactory(Name = "generic")]
    public class GenericDbConnectionFactory : PhysicalConnectionFactoryBase
    {
        [XmlSubElem]
        public IStoredConnection Stored { get; set; }

        public override IPhysicalConnection CreateConnection()
        {
            return new GenericDbConnection(this);
        }

        public override IServerSource CreateServerSource(IPhysicalConnection conn)
        {
            return new GenericServerSource(conn);
        }

        public override IDatabaseSource CreateDatabaseSource(IPhysicalConnection conn, string dbname)
        {
            return new GenericDatabaseSource(null, conn, dbname);
        }

        public override string GetDatabasePrivateFolder(string dbname)
        {
            return Stored.GetDatabasePrivateFolder(dbname);
        }

        public override string GetDatabasePrivateSubFolder(string dbname, string folderName)
        {
            return Stored.GetDatabasePrivateSubFolder(dbname, folderName);
        }

        public override string GetConnectionKey()
        {
            return Stored.ConnectionKey;
        }

        public override string GetDataSource()
        {
            return Stored.GetDataSource();
        }

        public override string GetDataTreeName()
        {
            return Stored.DataTreeName;
        }

        public override string GetFileName()
        {
            return Stored.FileName;
        }
    }

    [PhysicalConnectionFactory(Name = "generic_direct")]
    public class GenericDirectDbConnectionFactory : GenericDbConnectionFactory
    {
        [XmlElem]
        public string ConnectionString { get; set; }

        public override IPhysicalConnection CreateConnection()
        {
            var res = new GenericDbConnection(this);
            res.OverrideConnectionString(ConnectionString);
            return res;
        }

        public override string GetConnectionKey()
        {
            return Stored.ConnectionKey + "#" + ConnectionString;
        }

        public override string GetDataSource()
        {
            return Stored.GetDataSource();
        }
    }
}
