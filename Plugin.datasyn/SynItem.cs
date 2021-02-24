using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.Linq;
using System.IO;

namespace Plugin.datasyn
{
    public enum SynTableData : int
    {
        OnlyInSource = 0,
        OnlyInTarget = 1,
        Equal = 2,
        Modified = 3
    }

    public partial class SynItem : IDisposable
    {
        DataSynDefItem m_item;
        Synchronizer m_syn;
        string m_srcCache;
        string m_dstCache;
        SynSourceInfo m_srcInfo;
        SynSourceInfo m_dstInfo;
        BedValueHolder m_holder = new BedValueHolder();
        BedValueConvertor m_conv = new BedValueConvertor(new DataFormatSettings());
        DataSynOptions m_options;
        internal List<IRecordToDbAdapter> m_outputAdapters = new List<IRecordToDbAdapter>();
        SynItemStats m_stats;

        GridTable[] m_gridFills;
        GridTable[] m_grids;

        IDatabaseSource m_gridSourceConn;
        IDatabaseSource m_gridTargetConn;

        IDataSynAdapter m_srcSada;
        IDataSynAdapter m_dstSada;

        public SynItem(Synchronizer syn, DataSynDefItem item)
        {
            m_item = item;
            m_options = m_item.GetOptions();
            m_syn = syn;
            m_srcCache = Path.GetTempFileName();
            m_dstCache = Path.GetTempFileName();
            m_srcSada = m_syn.m_srcSada;
            m_dstSada = m_syn.m_dstSada;
            var vals = Enum.GetValues(typeof(SynTableData));
            m_gridFills = new GridTable[vals.Length];
            m_grids = new GridTable[vals.Length];
        }

        public SynItemState State = SynItemState.None;

        public void InitAndLoadFootprints()
        {
            m_item.BuildInfo(m_syn.m_source, m_syn.m_target, out m_srcInfo, out m_dstInfo);

            var ts = new TableStructure();
            foreach (string col in m_dstInfo.KeyCols) ts._Columns.Add(new ColumnStructure(m_dstInfo.Model.Columns[col]));
            foreach (string col in m_dstInfo.DataCols) ts._Columns.Add(new ColumnStructure(m_dstInfo.Model.Columns[col]));

            m_outputAdapters.Add(new RecordToDbAdapter(ts, ts, m_syn.m_target.Dialect, new DataFormatSettings()));

            // load source and target footprints
            if (m_syn.Progress != null) m_syn.Progress.SetCurWork(String.Format("Table {0}, loading source footprint", m_dstInfo.Model.FullName));
            LoadFootprint(m_syn.m_source, m_srcInfo, m_srcCache);
            if (m_syn.Progress != null) m_syn.Progress.SetCurWork(String.Format("Table {0}, loading target footprint", m_dstInfo.Model.FullName));
            LoadFootprint(m_syn.m_target, m_dstInfo, m_dstCache);
        }

        public void Run()
        {
            InitAndLoadFootprints();

            if (m_syn.Progress != null) m_syn.Progress.SetCurWork(String.Format("Table {0}, sync data", m_dstInfo.Model.FullName));
            ProcessFootprints();
        }

        private SynFootprint ReadFootprint(BinaryReader br)
        {
            byte eof = br.ReadByte();
            if (eof == 1) return null;
            return SynFootprint.FromStream(br, m_srcInfo.KeyCols.Length);
        }

        private bool WantSynchronize
        {
            get
            {
                return m_syn != null && (m_syn.m_guienv == null || !m_syn.m_guienv.CompareOnly);
            }
        }

        public void ProcessFootprints()
        {
            try
            {
                using (var srcfr = new FileStream(m_srcCache, FileMode.Open, FileAccess.Read))
                {
                    using (var dstfr = new FileStream(m_dstCache, FileMode.Open, FileAccess.Read))
                    {
                        using (var srcbr = new BinaryReader(srcfr))
                        {
                            using (var dstbr = new BinaryReader(dstfr))
                            {
                                ProcessFootprints(srcbr, dstbr);
                            }
                        }
                    }
                }
                if (m_syn!= null && WantSynchronize) m_syn.Progress.Info("Synchronized table {0}, updated {1}, inserted {2}, deleted {3} rows", m_item.Target.Table, m_updated, m_inserted, m_deleted);
                if (m_syn != null && m_syn.m_reportEnvObj != null) m_syn.m_reportEnvObj.LogResult(m_item.Target.Table, m_updated, m_inserted, m_deleted);
            }
            catch (Exception err)
            {
                if (m_syn != null && m_syn.m_reportEnvObj != null) m_syn.m_reportEnvObj.LogResultError(m_item.Target.Table, err);
                throw;
            }
        }

        private void ProcessFootprints(BinaryReader srcbr, BinaryReader dstbr)
        {
            var srcf = ReadFootprint(srcbr);
            var dstf = ReadFootprint(dstbr);
            m_stats = new SynItemStats();

            while (srcf != null || dstf != null)
            {
                if (srcf == null)
                {
                    while (dstf != null)
                    {
                        ProcessOnlyInTarget(dstf.KeyData);
                        dstf = ReadFootprint(dstbr);
                    }
                }
                if (dstf == null)
                {
                    while (srcf != null)
                    {
                        ProcessOnlyInSource(srcf.KeyData);
                        srcf = ReadFootprint(srcbr);
                    }
                }
                if (srcf != null && dstf != null)
                {
                    if (srcf.EqualKey(dstf))
                    {
                        if (srcf.Hash != dstf.Hash)
                        {
                            ProcessModified(srcf.KeyData);
                        }
                        else
                        {
                            ProcessEqual(srcf.KeyData);
                        }
                        srcf = ReadFootprint(srcbr);
                        dstf = ReadFootprint(dstbr);
                    }
                    else if (srcf < dstf)
                    {
                        ProcessOnlyInSource(srcf.KeyData);
                        srcf = ReadFootprint(srcbr);
                    }
                    else if (dstf < srcf)
                    {
                        ProcessOnlyInTarget(dstf.KeyData);
                        dstf = ReadFootprint(dstbr);
                    }
                }
            }
            SendBuffer();
        }

        private void ProcessEqual(string[] keydata)
        {
            m_stats.Equal++;
            m_stats.Source++;
            m_stats.Target++;

            m_equals.Add(keydata);
            SendBufferIfBig();
        }

        private void ProcessModified(string[] keydata)
        {
            m_stats.Modified++;
            m_stats.Source++;
            m_stats.Target++;

            m_updates.Add(keydata);
            SendBufferIfBig();
        }

        private void ProcessOnlyInSource(string[] keydata)
        {
            m_stats.Source++;
            m_stats.OnlyInSource++;

            m_inserts.Add(keydata);
            SendBufferIfBig();
        }

        private void ProcessOnlyInTarget(string[] keydata)
        {
            m_stats.Target++;
            m_stats.OnlyInTarget++;

            m_deletes.Add(keydata);
            SendBufferIfBig();
        }

        // loads keys and data hash into cache file, data is ordered by keycols
        private void LoadFootprint(IDatabaseSource conn, SynSourceInfo info, string cacheFile)
        {
            conn.Connection.Invoke(() => DoLoadFootprint(conn, info, cacheFile));
        }

        private void DoLoadFootprint(IDatabaseSource conn, SynSourceInfo info, string cacheFile)
        {
            // HACK: external sorting must be implemented
            using (var fw = new FileStream(cacheFile, FileMode.Create))
            {
                var fts = new List<SynFootprint>();
                using (var bw = new BinaryWriter(fw))
                {
                    var sada = conn.Dialect.CreateDataSynAdapter();
                    if (sada == null) throw new ExpectedError("DAE-00365 " + Texts.Get("s_dialect_doesnt_support_sync$dialect", "dialect", conn.Dialect.DisplayName));
                    var qbuf = new StringWriter();
                    var dmp = conn.Dialect.CreateDumper(qbuf);
                    var qtype = m_options.Update == true ? SynQueryType.SelectKeyMd5 : SynQueryType.SelectKeyNull;
                    info.Query.GenerateSql(dmp, sada, info, qtype, d2 =>
                    {
                        if (!info.SqlCondition.IsEmpty())
                        {
                            d2.Put(" ^where ");
                            d2.WriteRaw(info.SqlCondition);
                        }
                    });
                    using (var cmd = conn.Connection.SystemConnection.CreateCommand())
                    {
                        cmd.CommandText = qbuf.ToString();

                        using (IBedReader reader = conn.GetAnyDDA().AdaptReader(cmd.ExecuteReader()))
                        {
                            while (reader.Read())
                            {
                                var ft = SynFootprint.FromReader(reader, info.KeyCols.Length, m_holder, m_conv, sada);
                                fts.Add(ft);
                            }
                        }
                    }

                    fts.Sort();
                    foreach (var ft in fts)
                    {
                        bw.Write((byte)0);
                        ft.SaveToStream(bw);
                    }
                    bw.Write((byte)1);
                }
            }
        }

        public DataSynDefItem Item { get { return m_item; } }
        public SynItemStats Stats { get { return m_stats; } }

        public GridTable GetGridData(IDatabaseSource sourcedb, IDatabaseSource targetdb, SynTableData data, bool denyLoad)
        {
            if (m_grids[(int)data] == null && !denyLoad)
            {
                m_gridSourceConn = sourcedb;
                m_gridTargetConn = targetdb;

                switch (data)
                {
                    case SynTableData.OnlyInSource:
                        m_gridFills[(int)data] = new GridTable(m_srcInfo.GetTableStructure(SynQueryType.SelectAll), sourcedb.ToString());
                        break;
                    case SynTableData.OnlyInTarget:
                        m_gridFills[(int)data] = new GridTable(m_dstInfo.GetTableStructure(SynQueryType.SelectAll), targetdb.ToString());
                        break;
                    case SynTableData.Equal:
                        m_gridFills[(int)data] = new GridTable(m_srcInfo.GetTableStructure(SynQueryType.SelectAll), sourcedb.ToString());
                        break;
                    case SynTableData.Modified:
                        var ts = new TableStructure();
                        m_srcInfo.FillKey(ts);
                        m_srcInfo.FillDataColumns(ts);
                        m_dstInfo.FillDataColumns(ts);

                        for (int i = 0; i < m_srcInfo.KeyCols.Length; i++)
                        {
                            var col = (ColumnStructure)ts.Columns[i];
                            col.ColumnName = "K-" + col.ColumnName;
                        }
                        for (int i = 0; i < m_srcInfo.DataCols.Length; i++)
                        {
                            var col = (ColumnStructure)ts.Columns[i + m_srcInfo.KeyCols.Length];
                            col.ColumnName = "1-" + col.ColumnName;
                        }
                        for (int i = 0; i < m_dstInfo.DataCols.Length; i++)
                        {
                            var col = (ColumnStructure)ts.Columns[i + m_srcInfo.KeyCols.Length + m_srcInfo.DataCols.Length];
                            col.ColumnName = "2-" + col.ColumnName;
                        }

                        m_gridFills[(int)data] = new GridTable(ts, sourcedb.ToString());
                        break;
                }
                try
                {
                    ProcessFootprints();
                    m_grids[(int)data] = m_gridFills[(int)data];
                }
                finally
                {
                    m_gridFills[(int)data] = null;
                    m_gridSourceConn = null;
                    m_gridTargetConn = null;
                }
            }
            return m_grids[(int)data];
        }

        #region IDisposable Members

        public void Dispose()
        {
            try { File.Delete(m_srcCache); }
            catch { }
            try { File.Delete(m_dstCache); }
            catch { }
        }

        #endregion


        internal void DetachFromSynchronizer()
        {
            m_syn = null;
        }
    }
}
