using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;

namespace Plugin.phptunnel
{
    //public class PtunDialect : Plugin.mysql.MySqlDialect
    //{
    //    //public override IEnumerable<string> GetTableNames(DbConnection conn, string dbname, DatAdmin.IConnectionBehaviourDetails behaviour)
    //    //{
    //    //    conn.ChangeDatabase(dbname);
    //    //    using (DataReader reader = ((Connection)conn).ExecuteReader("SHOW TABLES"))
    //    //    {
    //    //        while (reader.Read())
    //    //        {
    //    //            yield return reader[0].ToString();
    //    //        }
    //    //    }
    //    //}

    //    public override string GetCurrentDatabase(DatAdmin.IPhysicalConnection conn)
    //    {
    //        return ((Connection)conn.SystemConnection).Database;
    //    }

    //    public override void CreateNamedParameter(string nameBase, out string sqlName, out string formalName)
    //    {
    //        sqlName = "@" + nameBase;
    //        formalName = "@" + nameBase;
    //    }

    //    public override DatAdmin.SqlDialectCaps DialectCaps
    //    {
    //        get
    //        {
    //            var res = base.DialectCaps;
    //            res.MultiCommand = false;
    //            return res;
    //        }
    //    }
    //}
}
