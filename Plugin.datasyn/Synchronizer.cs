using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.Linq;
using System.IO;

namespace Plugin.datasyn
{
    public class Synchronizer
    {
        internal IDatabaseSource m_source;
        internal IDatabaseSource m_target;
        internal IProgressInfo m_progress;
        internal DataSynDef m_datasyn;
        internal IDataSynAdapter m_srcSada;
        internal IDataSynAdapter m_dstSada;
        internal IDialectDataAdapter m_dstDDA;
        internal string m_outFile;
        internal TextWriter m_outBuffer;
        internal IDataSynReportEnv m_reportEnv;
        internal DataSynReportEnv m_reportEnvObj;
        internal DataSynGuiEnv m_guienv;

        public Synchronizer(IDatabaseSource src, IDatabaseSource dst, IProgressInfo progress, DataSynDef datasyn, DataSynReportEnv repenv, string outFile, DataSynGuiEnv guienv)
        {
            m_source = src;
            m_target = dst;
            m_progress = progress;
            m_datasyn = datasyn;
            m_outFile = outFile;
            m_srcSada = m_source.Dialect.CreateDataSynAdapter();
            m_dstSada = m_target.Dialect.CreateDataSynAdapter();
            m_dstDDA = m_target.GetAnyDDA();
            m_reportEnvObj = repenv;
            m_reportEnv = repenv;
            m_guienv = guienv;
            if (m_reportEnv == null) m_reportEnv = new DummyDataSynReportEnv();
        }

        public IProgressInfo Progress { get; set; }

        public void Run()
        {
            if (m_outFile != null)
            {
                m_outBuffer = new StreamWriter(m_outFile);
                m_target.GetAnyDialect().BeginUnicodeDumpFile(m_outBuffer);
            }
            Async.SafeOpen(m_source.Connection);
            Async.SafeOpen(m_target.Connection);
            try
            {
                foreach (var item in m_datasyn.Items)
                {
                    if (!item.IsChecked) continue;
                    try
                    {
                        RunItem(item);
                    }
                    catch (DataSynError err)
                    {
                        Progress.Error(err.Message);
                        Progress.Warning(String.Format("Processing of table {0} skipped", item.Target.Table));
                    }
                    catch (Exception err)
                    {
                        Progress.LogError(err);
                    }
                }
            }
            finally
            {
                Async.SafeClose(m_source.Connection);
                Async.SafeClose(m_target.Connection);
                if (m_outBuffer != null)
                {
                    m_outBuffer.Close();
                    Progress.Info(String.Format("Script saved to {0}, database is not changed", m_outFile));
                }
            }
        }

        private void SetItemState(SynItem item, SynItemState state)
        {
            item.State = state;
            if (m_guienv != null) m_guienv.OnItemEvent(item, state);
        }

        private void RunItem(DataSynDefItem item)
        {
            if (m_guienv != null)
            {
                if (m_guienv.Filter != null && !m_guienv.Filter.Contains(item.Target.Table)) return;

                var it = new SynItem(this, item);
                SetItemState(it, SynItemState.Start);
                it.InitAndLoadFootprints();
                it.ProcessFootprints();
                if (m_guienv.CompareOnly)
                {
                    SetItemState(it, SynItemState.Compared);
                }
                else
                {
                    SetItemState(it, SynItemState.Synchronized);
                }
                it.DetachFromSynchronizer();
            }
            else
            {
                using (var it = new SynItem(this, item))
                {
                    it.Run();
                }
            }
        }
    }

    public class SynSourceInfo
    {
        public SelectQueryBase Query;
        //public string ReadQuery;
        //public NameWithSchema SourceTable;
        public string SqlCondition;
        public ITableStructure Model;
        public string[] KeyCols;
        public string[] DataCols;
        public string[] CompareCols;

        public void FillKey(TableStructure table)
        {
            foreach (string keycol in KeyCols)
            {
                table.AddColumn(Model.Columns[keycol], false);
            }
            if (KeyCols.Length > 0)
            {
                var pk = new PrimaryKey();
                foreach (string keycol in KeyCols)
                {
                    pk.Columns.Add(new ColumnReference(keycol));
                }
                table._Constraints.Add(pk);
            }
        }

        public void FillDataColumns(TableStructure table)
        {
            foreach (string datacol in DataCols)
            {
                table.AddColumn(Model.Columns[datacol], false);
            }
        }

        public ITableStructure GetTableStructure(SynQueryType qtype)
        {
            var res = new TableStructure();
            FillKey(res);
            switch (qtype)
            {
                case SynQueryType.SelectAll:
                    FillDataColumns(res);
                    break;
                case SynQueryType.SelectKeyNull:
                case SynQueryType.SelectKeyMd5:
                    res.AddColumn(new ColumnStructure { DataType = new DbTypeString(), ColumnName = "HashColumn" }, true);
                    break;
            }
            return res;
        }
    }

    public class SynColMapping
    {
        public class SynItem
        {
            public string Source;
            public string Target;
        }
        public List<SynItem> Columns = new List<SynItem>();
        public static SynColMapping CreateMapping(ITableStructure source, ITableStructure target)
        {
            if (source.Columns.Count != target.Columns.Count)
            {
                throw new DataSynError(String.Format("DAE-00321 Table {0}: column count mitchmatch", target.FullName));
            }
            var res = new SynColMapping();
            foreach (var dstcol in target.Columns)
            {
                var srccol = source.FindColumn(dstcol.ColumnName, true);
                if (srccol == null)
                {
                    throw new DataSynError(String.Format("DAE-00322 Table {0}: cannout find source for column {1}", target.FullName, dstcol.ColumnName));
                }
                res.Columns.Add(new SynItem
            {
                Source = srccol.ColumnName,
                Target = dstcol.ColumnName,
            });
            }
            return res;
        }
        public string this[string dst]
        {
            get
            {
                return (from c in Columns where c.Target == dst select c.Source).FirstOrDefault();
            }
        }
        public string[] this[IEnumerable<string> cols]
        {
            get
            {
                var res = new List<string>();
                foreach (var col in cols) res.Add(this[col]);
                return res.ToArray();
            }
        }
        public string[] SourceCols
        {
            get
            {
                var res = new List<string>();
                foreach (var item in Columns) res.Add(item.Source);
                return res.ToArray();
            }
        }
        public string[] TargetCols
        {
            get
            {
                var res = new List<string>();
                foreach (var item in Columns) res.Add(item.Target);
                return res.ToArray();
            }
        }

        public string[] Complement(List<string> cols)
        {
            var incols = new HashSetEx<string>(cols);
            var res = new List<string>();
            foreach (var item in Columns)
            {
                if (!incols.Contains(item.Target)) res.Add(item.Target);
            }
            return res.ToArray();
        }

        public static SynColMapping CreateMapping(Dictionary<string, string> dict)
        {
            var res=new SynColMapping();
            foreach (var item in dict)
            {
                res.Columns.Add(new SynItem
                {
                    Source = item.Value,
                    Target = item.Key,
                });
            }
            return res;
        }
    }

}
