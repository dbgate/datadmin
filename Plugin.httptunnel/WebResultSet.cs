using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using DatAdmin;
using System.Net;
using System.Web;

namespace Plugin.httptunnel
{
    public class WebResultSet : WebResultBase, ITunnelResultSet
    {
        //List<string> m_columnNames = new List<string>();
        int m_rowcount;
        bool m_eof = false;
        int m_affected = 0;

        struct FieldInfo
        {
            internal string FieldName;
            internal string BaseTable;
            internal bool IsKey;
            internal bool IsBlob;
        }

        FieldInfo[] m_fields;

        public WebResultSet(WebResponse resp, Encoding encoding)
            : base(resp,encoding)
        {
            try
            {
                ReadHeader("WRES");
                ReadVersion();
                int retcode = ReadInt4();
                if (retcode != 0) throw new StreamFormatError("DAE-00330 Bad retcode");
                m_affected = ReadInt4();
                int colcount = ReadInt4();
                if (colcount > 0)
                {
                    m_fields = new FieldInfo[colcount];
                    for (int i = 0; i < colcount; i++)
                    {
                        m_fields[i].FieldName = ReadString();
                        m_fields[i].BaseTable = ReadString();
                        m_fields[i].IsKey = ReadBool();
                        m_fields[i].IsBlob = ReadBool();
                    }
                    m_rowcount = ReadInt4();
                }
            }
            catch (Exception)
            {
                Close();
                throw;
            }
        }

        //public List<string> ColumnNames { get { return m_columnNames; } }

        public int RecordsAffected { get { return m_affected; } }

        public static WebResultSet CreateRequest(HtunConnection conn, string sql)
        {
            return CreateRequest(conn, sql, -1, new Dictionary<string,string>());
        }

        public static WebResultSet CreateRequest(HtunConnection conn, string sql, int limit, Dictionary<string, string> queryPars)
        {
            List<string> qnames = new List<string>(queryPars.Keys);
            qnames.Sort(delegate(string a, string b) { return b.Length - a.Length; });
            foreach (string qpar in qnames)
            {
                string val = queryPars[qpar];
                if (val == null) sql = sql.Replace(qpar, "NULL");
                else sql = sql.Replace(qpar, "'" + val.Replace("'", "\\'") + "'");
            }

            Dictionary<string, byte[]> extpars = new Dictionary<string, byte[]>();

            extpars["LIMIT"] = Encoding.ASCII.GetBytes(limit.ToString());
            extpars["COMMAND"] = EncodeCommand(conn, sql);

            WebResponse resp = CreateWebResponse(conn, "QUERY", extpars, sql);

            return new WebResultSet(resp, conn.ResultSetEncoding);
        }

        public object[] FetchRow()
        {
            if (m_eof) return null;
            ReadBuffer4();
            if (IsInBuffer4("WEND"))
            {
                m_eof = true;
                return null;
            }
            if (!IsInBuffer4("WROW")) throw new StreamFormatError("DAE-00331 Unexpected data");
            object[] row = new object[m_fields.Length];
            for (int i = 0; i < row.Length; i++)
            {
                if (m_fields[i].IsBlob) row[i] = ReadBytes();
                else row[i] = ReadString();
            }
            return row;
        }

        //public bool IsBlob(int ordinal)
        //{
        //    return m_fields[ordinal].IsBlob;
        //}

        //public bool IsKey(int ordinal)
        //{
        //    return m_fields[ordinal].IsKey;
        //}

        //public string GetBaseTable(int ordinal)
        //{
        //    return m_fields[ordinal].BaseTable;
        //}

        public ITableStructure GetStructure()
        {
            TableStructure res = new TableStructure();
            if (m_fields == null) return res;
            PrimaryKey pk = new PrimaryKey();
            foreach (var fld in m_fields)
            {
                ColumnStructure col = new ColumnStructure();
                col.ColumnName = fld.FieldName;
                col.DataType = fld.IsBlob ? (DbTypeBase)new DbTypeBlob() : new DbTypeString();
                if (fld.IsKey) pk.Columns.Add(new ColumnReference(col.ColumnName));
                res._Columns.Add(col);
            }
            if (m_fields.Length > 0) res.FullName = new NameWithSchema(m_fields[0].BaseTable);
            if (pk.Columns.Count > 0) res._Constraints.Add(pk);
            return res;
        }
    }
}
