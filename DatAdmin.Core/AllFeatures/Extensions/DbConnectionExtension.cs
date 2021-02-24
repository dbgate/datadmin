using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Collections;

namespace DatAdmin
{
    public static class DbConnectionExtension
    {
        public static IDataReader RunOneSqlCommandReader(this DbConnection conn, ISqlDialect dialect, string sql)
        {
            return RunOneSqlCommandReader(conn, dialect, sql, null, null, null);
        }

        public static IDataReader RunOneSqlCommandReader(this DbConnection conn, ISqlDialect dialect, string sql, DbTransaction tran, int? timeout, IInvoker invoker)
        {
            if (sql.StartsWith("@getschema"))
            {
                string[] items = sql.Split(' ');
                string entity = items[1].ToLower().Trim();
                if (entity == "databases") return new DataTableReader(conn.GetSchema("Databases"));
                if (entity == "tables") return new DataTableReader(conn.GetSchema("Tables"));
                if (entity == "columns") return new DataTableReader(conn.GetSchema("Columns", new string[] { null, null, items[2].Trim() }).SelectNewTable("1=1", "ORDINAL_POSITION ASC"));
            }
            DbCommand c = conn.CreateCommand();
            c.Connection = conn;
            if (timeout != null) c.CommandTimeout = timeout.Value;
            c.CommandText = sql;
            if (tran != null) c.Transaction = tran;
            Logging.Debug("Executing SQL:" + sql);
            IDisposable canc = null;
            if (invoker != null) canc = invoker.AddOnCancel(c.Cancel);
            try
            {
                return new CommandDataReader(c.ExecuteReader(), c, canc);
            }
            catch (Exception err)
            {
                err.Data["sql"] = c.CommandText;
                if (c.Connection != null) c.Connection.FillInfo(err.Data);
                if (dialect != null) dialect.FillInfo(err.Data);
                throw;
            }
        }

        public static IDataReader RunSqlCommandsReader(this DbConnection conn, ISqlDialect dialect, string sql)
        {
            List<string> items = new List<string>();
            foreach (string item in QueryTools.GoSplit(sql)) items.Add(item);

            for (int i = 0; i < items.Count; i++)
            {
                if (i < items.Count - 1)
                {
                    using (DbCommand c = conn.CreateCommand())
                    {
                        c.Connection = conn;
                        c.CommandText = items[i];
                        Logging.Debug("Executing SQL:" + items[i]);
                        c.ExecuteNonQueryEx(dialect);
                    }
                }
                else
                {
                    return RunOneSqlCommandReader(conn, dialect, items[i]);
                }
            }

            throw new InternalError("DAE-00160 No return reader");
        }

        public static void RunSqlCommands(this DbConnection conn, ISqlDialect dialect, string sql)
        {
            DbTransaction trans = null;
            if (dialect.DialectCaps.NestedTransactions)
            {
                try
                {
                    trans = conn.BeginTransaction();
                }
                catch (Exception)
                {
                    trans = null;
                }
            }

            try
            {
                foreach (string sql_item in QueryTools.GoSplit(sql))
                {
                    using (DbCommand c = conn.CreateCommand())
                    {
                        if (trans != null) c.Transaction = trans;
                        c.Connection = conn;
                        c.CommandText = sql_item;
                        Logging.Debug("Executing SQL:" + sql_item);
                        c.ExecuteNonQueryEx(dialect);
                    }
                }
            }
            catch
            {
                if (trans != null) trans.Rollback();
                throw;
            }

            if (trans != null) trans.Commit();
        }

        public static void ExecuteNonQueryEx(this DbCommand cmd, ISqlDialect dialect)
        {
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception err)
            {
                err.Data["sql"] = cmd.CommandText;
                if (cmd.Connection != null) cmd.Connection.FillInfo(err.Data);
                if (dialect != null) dialect.FillInfo(err.Data);
                throw;
            }
        }

        public static void ExecuteNonQuery(this DbConnection conn, string sql)
        {
            ExecuteNonQuery(conn, sql, null, null, null);
        }

        public static void ExecuteNonQuery(this DbConnection conn, string sql, ISqlDialect dialect)
        {
            ExecuteNonQuery(conn, sql, dialect, null, null);
        }

        public static void LogExecuteQuery(string sql)
        {
            if (sql.Length > 1000)
            {
                Logging.Debug("Executing query (length:{0}): {1} ...", sql.Length, sql.Substring(0, 1000));
            }
            else
            {
                Logging.Debug("Executing query:" + sql);
            }
        }

        public static void ExecuteNonQuery(this DbConnection conn, string sql, ISqlDialect dialect, DbTransaction trans, int? timeout)
        {
            if (sql.IsEmpty()) return;
            if (dialect != null && dialect.RunSpecialNonQuery(conn, sql)) return;
            if (sql.StartsWith("@use"))
            {
                string[] items = sql.Split(' ');
                conn.ChangeDatabase(items[1].Trim());
            }
            else
            {
                using (DbCommand cmd = conn.CreateCommand())
                {
                    LogExecuteQuery(sql);
                    cmd.CommandText = sql;
                    if (timeout != null) cmd.CommandTimeout = timeout.Value;
                    if (trans != null) cmd.Transaction = trans;
                    cmd.ExecuteNonQueryEx(dialect);
                }
            }
        }

        public static List<string> GetDatabaseNames(this DbConnection conn)
        {
            Logging.Debug("Getting database list");
            DataTable dbs = conn.GetSchema("Databases");
            List<string> lst = new List<string>();
            if (dbs.Columns.IndexOf("database_name") >= 0)
            {
                foreach (DataRow row in dbs.Rows)
                {
                    lst.Add(row["database_name"].ToString());
                }
            }
            else
            {
                foreach (DataRow row in dbs.Rows)
                {
                    foreach (object val in row.ItemArray)
                    {
                        if (!String.IsNullOrEmpty(val.SafeToString()))
                        {
                            lst.Add(val.ToString());
                            break;
                        }
                    }
                    
                }
            }
            lst.Sort();
            return lst;
        }

        //public static List<string> LoadColumnNames(this DbConnection conn, NameWithSchema table)
        //{
        //    Logging.Debug("Getting column list, tablename={0}", table);
        //    DataTable data = conn.GetSchema("Columns", new string[] { null, table.Schema, table.Name }).SelectNewTable("1=1", "ORDINAL_POSITION ASC");
        //    List<string> lst = new List<string>();
        //    foreach (DataRow row in data.Rows) lst.Add(row["COLUMN_NAME"].ToString());
        //    return lst;
        //}

        //public static List<string> LoadSchemaNames(this DbConnection conn, DbProviderFactory factory)
        //{
        //    List<string> res = new List<string>();
        //    using (DbCommand cmd = factory.CreateCommand())
        //    {
        //        cmd.Connection = conn;
        //        cmd.CommandText = "SELECT * FROM INFORMATION_SCHEMA.SCHEMATA";
        //        using (DbDataReader reader = cmd.ExecuteReader())
        //        {
        //            while (reader.Read())
        //            {
        //                res.Add(reader["SCHEMA_NAME"].ToString());
        //            }
        //        }
        //    }
        //    return res;
        //}

        public static void ExecuteNonQueries(this DbConnection conn, string sql, ISqlDialect dialect, DbTransaction tran, int? timeout)
        {
            foreach (string cmd in QueryTools.GoSplit(sql))
            {
                ExecuteNonQuery(conn, cmd, dialect, tran, timeout);
            }
        }

        public static void ExecuteNonQueries(this DbConnection conn, string sql, ISqlDialect dialect)
        {
            foreach (string cmd in QueryTools.GoSplit(sql))
            {
                ExecuteNonQuery(conn, cmd, dialect);
            }
        }

        public static object ExecuteScalar(this DbConnection conn, string sql, DbTransaction tran)
        {
            using (DbCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = sql;
                cmd.Transaction = tran;
                return cmd.ExecuteScalar();
            }
        }

        public static T ExecuteScalar<T>(this DbConnection conn, string sql, DbTransaction tran)
        {
            using (DbCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = sql;
                cmd.Transaction = tran;
                return (T)Convert.ChangeType(cmd.ExecuteScalar(), typeof(T));
            }
        }

        public static object ExecuteScalar(this DbConnection conn, string sql)
        {
            using (DbCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = sql;
                return cmd.ExecuteScalar();
            }
        }

        public static string ExecuteLongTextScalar(this DbConnection conn, string sql)
        {
            using (DbCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = sql;
                StringBuilder sb = new StringBuilder();
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        sb.Append(reader[0].SafeToString());
                    }
                }
                return sb.ToString();
            }
        }

        public static T ExecuteScalar<T>(this DbConnection conn, string sql)
        {
            using (DbCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = sql;
                object res = cmd.ExecuteScalar();
                if (typeof(T) == typeof(int?))
                {
                    if (res == null || res == DBNull.Value) return (T)(object)null;
                    return (T)(object)Int32.Parse(res.ToString());
                }
                return (T)Convert.ChangeType(res, typeof(T));
            }
        }

        public static void SafeChangeDatabase(this DbConnection conn, string dbname)
        {
            try
            {
                if (!String.IsNullOrEmpty(dbname) && conn != null) conn.ChangeDatabase(dbname);
            }
            catch (Exception err)
            {
                throw new DatabaseNotAccessibleError(dbname, err);
            }
        }

        public static void SafeChangeDatabase(this DbConnection conn, ObjectPath objpath)
        {
            if (objpath != null) SafeChangeDatabase(conn, objpath.DbName);
        }

        public static DataTable LoadTableFromQuery(this DbConnection conn, string sql)
        {
            return LoadTableFromQuery(conn, sql, null);
        }

        public static DataTable LoadTableFromQuery(this DbConnection conn, string sql, int? maximumRecords)
        {
            using (DbCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = sql;
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    return reader.ToDataTable(maximumRecords);
                }
            }
        }

        public static void FillInfo(this DbConnection conn, IDictionary data)
        {
            if (conn == null) return;
            try
            {
                data["sysconn_version"] = conn.ServerVersion;
            }
            catch (Exception err)
            {
                data["sysconn_version"] = err.Message;
            }
            data["sysconn_class"] = conn.GetType().FullName;
            data["sysconn_state"] = conn.State.ToString();
            data["sysconn_string"] = Logging.MangleConnectionString_RemovePassword(conn.ConnectionString);
        }
    }
}
