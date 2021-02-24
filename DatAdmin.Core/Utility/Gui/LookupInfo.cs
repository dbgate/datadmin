using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data;

namespace DatAdmin
{
    public class LookupCannotBeCreatedException : Exception { }

    public class LookupInfo
    {
        //ITableStructure m_table;
        //int m_pkColIndex;
        //int m_valIndex = -1;
        string m_pkColName;
        string m_valueColName;
        int m_valueColIndex = -1;
        Dictionary<string, string> m_fkCaptions = new Dictionary<string, string>();
        bool m_wasError = false;
        bool m_loadedFullList = false;
        Type m_pkType;
        bool m_isNumericPk;

        public string this[string pkValue]
        {
            get
            {
                if (String.IsNullOrEmpty(pkValue)) return null;
                string res;
                if (m_fkCaptions.TryGetValue(pkValue, out res)) return res;
                return null;
            }
        }

        public bool CGetData(ITableSource table, IEnumerable<string> keys, Action guiCallback)
        {
            if (m_wasError) return true;
            if (m_loadedFullList) return true;
            List<string> notloaded = new List<string>();
            lock (m_fkCaptions)
            {
                foreach (string key in keys)
                {
                    if (!m_fkCaptions.ContainsKey(key) && !String.IsNullOrEmpty(key)) notloaded.Add(key);
                }
            }
            if (notloaded.Count == 0) return true;
            GetAction act = new GetAction { GuiCallback = guiCallback, Info = this, Keys = notloaded, Table = table};
            table.Connection.BeginInvoke(PriorityLevel.Low, true, (Action)act.Run, MainWindow.Instance.Invoker.CreateInvokeCallback(act.OnFinish));
            return false;
        }

        class GetAction
        {
            internal Action GuiCallback;
            internal LookupInfo Info;
            internal IEnumerable<string> Keys;
            internal ITableSource Table;
            //internal ITableStructure TableDef;

            internal void Run()
            {
                bool isall = true;
                lock (Info.m_fkCaptions)
                {
                    foreach (string key in Keys) if (!Info.m_fkCaptions.ContainsKey(key)) isall = false;
                }
                if (isall) return;
                ISqlDialect dialect = Table.Connection.Dialect;
                StringBuilder sb = new StringBuilder();
                string qpkcolname = dialect.QuoteIdentifier(Info.m_pkColName);
                sb.Append("SELECT ");
                sb.Append(qpkcolname);
                sb.Append(" , ");
                sb.Append(dialect.QuoteIdentifier(Info.m_valueColName));
                sb.Append(" FROM ");
                sb.Append(dialect.QuoteFullName(Table.FullName));
                sb.Append(" WHERE ");
                bool was = false;
                foreach (string key in Keys)
                {
                    if (was) sb.Append(" OR ");
                    sb.Append(qpkcolname);
                    sb.Append("=");
                    if (Info.m_isNumericPk) sb.Append(key);
                    else sb.Append(dialect.GetSqlLiteral(key));
                    was = true;
                }
                Dictionary<string, string> res = new Dictionary<string, string>();
                using (DbCommand cmd = Table.Connection.SystemConnection.CreateCommand())
                {
                    cmd.CommandText = sb.ToString();
                    Table.Connection.SystemConnection.SafeChangeDatabase(Table.Database.DatabaseName);
                    using (DbDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            res[reader[0].ToString()] = reader[1].SafeToString();
                        }
                    }
                }
                lock (Info.m_fkCaptions)
                {
                    foreach (string key in Keys)
                    {
                        if (res.ContainsKey(key)) Info.m_fkCaptions[key] = res[key];
                        else Info.m_fkCaptions[key] = null;
                    }
                }
            }

            internal void OnFinish(IAsyncResult async)
            {
                try
                {
                    async.StandaloneEndInvoke();
                }
                catch (Exception e)
                {
                    if (!Info.m_wasError)
                    {
                        Errors.Report(e);
                        Info.m_wasError = true;
                    }
                }
                if (GuiCallback != null) GuiCallback();
            }
        }

        private void InitFromDataTable(DataTable tbl)
        {
            for (int i = 0; i < tbl.Columns.Count; i++)
            {
                if (tbl.Columns[i].ColumnName == m_pkColName)
                {
                    m_pkType = tbl.Columns[i].DataType;
                    continue;
                }
                if (tbl.Columns[i].DataType != typeof(string)) continue;
                m_valueColName = tbl.Columns[i].ColumnName;
                m_valueColIndex = i;
                break;
            }
            m_isNumericPk = m_pkType == typeof(SByte) || m_pkType == typeof(Int16) || m_pkType == typeof(Int32) || m_pkType == typeof(Int64)
                || m_pkType == typeof(Byte) || m_pkType == typeof(UInt16) || m_pkType == typeof(UInt32) || m_pkType == typeof(UInt64)
                || m_pkType == typeof(float) || m_pkType == typeof(double) || m_pkType == typeof(decimal);
            if (m_valueColName == null)
            {
                throw new LookupCannotBeCreatedException();
            }
        }


        public LookupInfo(ITableSource table, string pkcolname)
        {
            m_pkColName = pkcolname;
            using (DbCommand cmd = table.Connection.SystemConnection.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM " + table.Connection.Dialect.QuoteFullName(table.FullName);
                using (DbDataReader reader = cmd.ExecuteReader(CommandBehavior.SchemaOnly))
                {
                    DataTable tbl = reader.ToDataTable();
                    InitFromDataTable(tbl);
                }
            }
        }

        public LookupInfo(string pkColName, DataTable listData)
        {
            m_pkColName = pkColName;
            InitFromDataTable(listData);
            foreach (DataRow row in listData.Rows)
            {
                string pkval = row[m_pkColName].SafeToString();
                if (pkval != null) m_fkCaptions[pkval] = row[m_valueColIndex].SafeToString();
            }
            m_loadedFullList = true;
        }

        //public static LookupInfo Create(ITableSource table, string pkcolname)
        //{
        //    return new 

        //            tbl.Columns
        //            ITableStructure ts = DbStructureFactory.TableFromReader(table.FullName.Name, reader.GetSchemaTable());

        //        }
        //    }
        //    m_table = table;
        //    m_pkColIndex = table.Columns.GetOrdinal(table.PrimaryKeyColumnName());
        //    for (int i = 0; i < table.Columns.Count; i++)
        //    {
        //        if (i == m_pkColIndex) continue;
        //        if (table.Columns[i].DataType.DotNetType != typeof(string)) continue;
        //        m_valIndex = i;
        //        break;
        //    }
        //    if (m_valIndex < 0) throw new LookupCannotBeCreatedException();

        //    throw new NotImplementedException();
        //}
    }
}
