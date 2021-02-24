using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;

namespace DatAdmin.Scripting
{
    public class Connection
    {
        IPhysicalConnection m_conn;

        public Connection(IPhysicalConnection conn)
        {
            m_conn = conn;
        }

        //public Cursor cursor()
        //{
        //    return new Cursor(m_conn);
        //}

        public object execute_scalar(string sql)
        {
            using (DbCommand cmd = m_conn.SystemConnection.CreateCommand())
            {
                cmd.Connection = m_conn.SystemConnection;
                cmd.CommandText = sql;
                return cmd.ExecuteScalar();
            }
        }

        public RowSet execute_rowset(string sql)
        {
            DbCommand cmd = m_conn.SystemConnection.CreateCommand();
            cmd.Connection = m_conn.SystemConnection;
            cmd.CommandText = sql;
            return new RowSet(cmd);
        }
    }

    //public class Cursor
    //{
    //    IPhysicalConnection m_conn;
    //    DbCommand m_cmd;
    //    DbDataReader m_reader;

    //    public Cursor(IPhysicalConnection conn)
    //    {
    //    }

    //    public void execute(string sql)
    //    {
    //        m_cmd = m_conn.SystemConnection.CreateCommand();
    //        m_cmd.Connection = m_conn.SystemConnection;
    //        m_cmd.CommandText = sql;
    //        m_reader = m_cmd.ExecuteReader();
    //    }
    //}

    public class RowSet
    {
        DbDataReader m_reader;
        DbCommand m_cmd;
        bool m_eod;

        public RowSet(DbCommand cmd)
        {
            m_cmd = cmd;
            m_reader = m_cmd.ExecuteReader();
        }

        public Row read()
        {
            if (m_eod) return null;
            if (m_reader.Read())
            {
                return new Row(m_reader);
            }
            m_eod = true;
            return null;
        }

        public bool EOD { get { return m_eod; } }
        public void close()
        {
            if (m_reader != null)
            {
                m_reader.Dispose();
                m_cmd.Dispose();
            }
            m_reader = null;
            m_cmd = null;
        }
    }

    public class Row
    {
        object[] m_values;

        public Row(DbDataReader record)
        {
            m_values = new object[record.FieldCount];
            record.GetValues(m_values);
        }

        public object this[int index]
        {
            get { return m_values[index]; }
        }
    }
}
