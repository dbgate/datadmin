using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.Data.SQLite;

namespace Plugin.querytool
{
    public class QueryDbConnection : InternalDbConnection
    {
        public QueryDbConnection()
            : base(SQLiteFactory.Instance, "queryhist", Generated.DbCreator.UpdateDb)
        {
        }
    }
}
