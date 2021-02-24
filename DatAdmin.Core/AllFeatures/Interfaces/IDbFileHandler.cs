using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace DatAdmin
{
    //public class DbFileHandlerAttribute : RegisterAttribute { }

    //public interface IDbFileHandler
    //{
    //    IDatabaseSource OpenFile(string filename);
    //    bool CanOpenFile(string filename);
    //}

    //[AddonType]
    //public class DbFileHandlerAddonType : AddonType
    //{
    //    public override string Name
    //    {
    //        get { return "dbfilehandler"; }
    //    }

    //    public override Type InterfaceType
    //    {
    //        get { return typeof(IDbFileHandler); }
    //    }

    //    public override Type RegisterAttributeType
    //    {
    //        get { return typeof(DbFileHandlerAttribute); }
    //    }

    //    public static readonly DbFileHandlerAddonType Instance = new DbFileHandlerAddonType();
    //}

    //public abstract class DbFileHandlerBase : AddonBase, IDbFileHandler
    //{
    //    public abstract IDatabaseSource OpenFile(string filename);
    //    public abstract bool CanOpenFile(string filename);

    //    public override AddonType AddonType
    //    {
    //        get { return DbFileHandlerAddonType.Instance; }
    //    }

    //    public static T LoadStoredConnection<T>(string confile, string dbfile)
    //        where T : DbFileStoredConnection, new()
    //    {
    //        T res = null;
    //        if (File.Exists(confile))
    //        {
    //            res = StoredConnection.LoadFromFile(confile) as T;
    //        }
    //        if (res == null) res = new T { DbFilename = dbfile };
    //        res.DbFilename = dbfile;
    //        res.Filename = confile;
    //        return res;
    //    }
    //}
}
