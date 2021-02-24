using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Linq;

namespace DatAdmin
{
    public class BedAdapter
    {
        string m_queryTemplate;
        IPhysicalConnection m_conn;
        ITableStructure m_structure;
        bool m_isFullTableSelect;
        DbCommand m_currentCommand;
        object m_currentCommandLock = new object();
        IDialectDataAdapter m_dda = DialectDataAdapterBase.Instance;

        public BedAdapter(IPhysicalConnection conn, string queryTemplate, bool isFullTableSelect)
        {
            m_queryTemplate = queryTemplate;
            m_conn = conn;
            m_isFullTableSelect = isFullTableSelect;
            if (m_conn != null) m_dda = m_conn.Dialect.CreateDataAdapter();
        }

        private bool HasMetaData { get { return m_structure != null; } }

        private void LoadMetaData(DbDataReader reader)
        {
            m_structure = reader.GetTableStructure(m_conn.Dialect);
        }

        private void WantMetadata()
        {
            if (HasMetaData) return;
            using (DbCommand cmd = m_conn.SystemConnection.CreateCommand())
            {
                try
                {
                    lock (m_currentCommandLock) m_currentCommand = cmd;
                    cmd.CommandText = m_queryTemplate;
                    using (DbDataReader reader = cmd.ExecuteReader(CommandBehavior.SchemaOnly | CommandBehavior.KeyInfo))
                    {
                        LoadMetaData(reader);
                    }
                }
                finally
                {
                    lock (m_currentCommandLock) m_currentCommand = null;
                }
            }
        }

        public BedTable LoadTableData(int? start, int? count, string queryInstance)
        {
            int skipcount = 0;
            if (start != null) skipcount = start.Value;

            string sql = queryInstance;
            if (m_conn.Dialect.DialectCaps.RangeSelect && (skipcount > 0 || count != null))
            {
                sql = m_conn.Dialect.GetRangeSelect(queryInstance, skipcount, count.Value);
                skipcount = 0;
            }
            else if (m_conn.Dialect.DialectCaps.LimitSelect && count != null)
            {
                sql = m_conn.Dialect.GetLimitSelect(queryInstance, skipcount + count.Value);
            }

            //WantMetadata();

            using (DbCommand cmd = m_conn.SystemConnection.CreateCommand())
            {
                try
                {
                    lock (m_currentCommandLock) m_currentCommand = cmd;
                    cmd.CommandText = sql;
                    CommandBehavior behaviour = CommandBehavior.Default;
                    if (!HasMetaData) behaviour |= CommandBehavior.KeyInfo;
                    using (IBedReader reader = m_dda.AdaptReader(cmd.ExecuteReader(behaviour)))
                    {
                        if (!HasMetaData) m_structure = reader.Structure;
                        if (m_structure == null) return null;
                        BedTable res = new BedTable(m_structure);
                        while (skipcount > 0)
                        {
                            if (!reader.Read()) return res;
                            skipcount--;
                        }

                        while (count == null || res.Rows.Count < count.Value)
                        {
                            if (!reader.Read()) return res;
                            res.AddRow(reader);
                        }

                        return res;
                    }
                }
                catch (Exception err)
                {
                    err.Data["sql"] = sql;
                    m_conn.FillInfo(err.Data);
                    throw;
                }
                finally
                {
                    lock (m_currentCommandLock) m_currentCommand = null;
                }
            }
        }

        public bool IsReadOnly { get { return !m_isFullTableSelect; } }
        public bool AllowDelete { get { return m_isFullTableSelect; } }
        public bool AllowUpdate { get { return m_isFullTableSelect; } }
        public bool AllowInsert { get { return m_isFullTableSelect; } }

        public void SaveChanges(BedTable table, ISqlDumper dmp)
        {
            SaveChanges(table, dmp, null);
        }

        public void SaveChanges(BedTable table, ISqlDumper dmp, ISaveDataProgress progress)
        {
            if (IsReadOnly) throw new InternalError("DAE-00020 BedAdapter is read only, can not save changes");
            DataScript script = table.GetBaseModifyScript();
            MultiTableUpdateScript lscript = table.GetLinkedDataScript(m_structure.FullName);
            if (progress != null)
            {
                script.ReportCounts(progress);
                lscript.ReportCounts(progress);
            }
            dmp.UpdateData(m_structure, script, progress);
            dmp.UpdateData(lscript, progress);
        }

        public ITableStructure GetStructure()
        {
            WantMetadata();
            return m_structure;
        }

        public void CancelLoading()
        {
            lock (m_currentCommandLock)
            {
                if (m_currentCommand != null) m_currentCommand.Cancel();
            }
        }

        public void SaveChanges(BedTable table, ISaveDataProgress progress)
        {
            using (DbTransaction tran = m_conn.SystemConnection.BeginTransaction())
            {
                try
                {
                    m_conn.RunScript(dmp => SaveChanges(table, dmp, progress), tran, null);
                    tran.Commit();
                }
                catch
                {
                    tran.Rollback();
                    throw;
                }
            }
        }

        public void LoadTableData(string queryInstance, Action<IBedRecord> forEachRow)
        {
            string sql = queryInstance;
            using (DbCommand cmd = m_conn.SystemConnection.CreateCommand())
            {
                try
                {
                    lock (m_currentCommandLock) m_currentCommand = cmd;
                    cmd.CommandText = sql;
                    CommandBehavior behaviour = CommandBehavior.Default;
                    if (!HasMetaData) behaviour |= CommandBehavior.KeyInfo;
                    using (IBedReader reader = m_dda.AdaptReader(cmd.ExecuteReader(behaviour)))
                    {
                        if (!HasMetaData) m_structure = reader.Structure;
                        if (m_structure == null) return;

                        while (reader.Read())
                        {
                            forEachRow(reader);
                        }
                    }
                }
                catch (Exception err)
                {
                    err.Data["sql"] = sql;
                    m_conn.FillInfo(err.Data);
                    throw;
                }
                finally
                {
                    lock (m_currentCommandLock) m_currentCommand = null;
                }
            }
        }
    }
}
