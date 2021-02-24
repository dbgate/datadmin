using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data.Common;
using System.Data;

namespace DatAdmin
{
    public class InternalDbConnection
    {
        DbConnection m_conn;
        DbProviderFactory m_factory;

        public InternalDbConnection(DbProviderFactory factory, string dbname, Action<DbConnection> updateFunc)
        {
            m_factory = factory;
            m_conn = m_factory.CreateConnection();
            m_conn.ConnectionString = "Data Source=" + Path.Combine(Core.ConfigDirectory, dbname + ".db3");
            m_conn.Open();
            updateFunc(m_conn);
        }

        private void FillParams(DbCommand cmd, params object[] args)
        {
            for (int i = 0; i < args.Length; i++)
            {
                var par = m_factory.CreateParameter();
                par.ParameterName = "@p" + (i + 1).ToString();
                par.DbType = DbType.String;
                par.Value = args[i];
                cmd.Parameters.Add(par);
            }
        }

        public void ExecuteNonQuery(string sql, params object[] args)
        {
            using (var cmd = m_conn.CreateCommand())
            {
                cmd.CommandText = sql;
                FillParams(cmd, args);
                cmd.ExecuteNonQuery();
            }
        }

        public object ExecuteScalar(string sql, params object[] args)
        {
            using (var cmd = m_conn.CreateCommand())
            {
                cmd.CommandText = sql;
                FillParams(cmd, args);
                return cmd.ExecuteScalar();
            }
        }

        public object[] ExecuteRow(string sql, params object[] args)
        {
            using (var cmd = m_conn.CreateCommand())
            {
                cmd.CommandText = sql;
                FillParams(cmd, args);
                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        var res = new object[reader.FieldCount];
                        reader.GetValues(res);
                        return res;
                    }
                }
            }
            return null;
        }

        public DataTable LoadQuery(string sql)
        {
            var da = m_factory.CreateDataAdapter();
            var cmd = m_conn.CreateCommand();
            cmd.CommandText = sql;
            da.SelectCommand = cmd;
            var res = new DataTable();
            da.Fill(res);
            return res;
        }

        public int GetInsertId()
        {
            return Int32.Parse(m_conn.ExecuteScalar("select last_insert_rowid()").ToString());
        }
    }
}
