using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;

namespace Plugin.mssql
{
    [DatabaseLoader(Name = "mssql_dumploader", SupportsDirectUse = false)]
    public class MsSqlDatabaseLoader : SqlDumpLoaderBase
    {
        public override string GetTitle()
        {
            return "DatAdmin MS SQL Dump Loader";
        }
    }
}
