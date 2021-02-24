using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Reflection;
using System.Data.SQLite;
using DatAdmin;

namespace Plugin.sqlite
{
    public static class SqliteDriver
    {
        public static DbConnection GetConnection(string conn)
        {
			var f = GetFactory();
			var con = f.CreateConnection();
			con.ConnectionString = conn;
			return con;
        }

        public static DbProviderFactory GetFactory()
        {
			return System.Data.SQLite.SQLiteFactory.Instance;
//            if (Core.IsMono)
//            {
//				//var name = new AssemblyName("Mono.Data.Sqlite");
//				//name.Version = new Version(2,0);
//				//Assembly asm = Assembly.Load("Mono.Data.Sqlite.dll");
//				Assembly asm = Assembly.Load("System.Data.SQLite");
//				Type t = Type.GetType("Mono.Data.Sqlite.SqliteFactory", true);
//				return (DbProviderFactory)t.GetStaticPropertyOrField("Instance");
//            }
//            else
//            {
//				Type t = Type.GetType("System.Data.SQLite.SQLiteFactory");
//				return (DbProviderFactory)t.GetStaticPropertyOrField("Instance");
//            }
		}

    }
}
