using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using System.Xml;
using System.Linq;
using System.ComponentModel;

namespace DatAdmin
{
    public class GridTable : GenericConnectionUsage, ITabularDataView, ITabularDataStore
    {
        Stream m_cache;
        string m_file;
        const int BUFFER_SIZE = 50;
        int m_serializedRows = 0;
        List<long> m_directory = new List<long>();
        ITableStructure m_table;
        //DataTable m_schema;
        //ISqlDialect m_dialect;
        Action<IDataQueue> m_fillProc;
        TabularDataViewState m_state;
        Dictionary<IAsyncResult, Action<IDataQueue>> m_readProcs = new Dictionary<IAsyncResult, Action<IDataQueue>>();
        string m_title;
        //string[] m_colnames;
        int m_refCount = 1;

        class Chunk
        {
            internal List<int> Lengths = new List<int>();
            internal MemoryStream Data = new MemoryStream();
            internal int Count { get { return Lengths.Count; } }
            BinaryWriter m_writer;

            internal Chunk()
            {
                m_writer = new BinaryWriter(Data);
            }

            internal void SaveRecord(IBedRecord rec)
            {
                long pos0 = Data.Length;
                BedTool.SaveRecord(rec.FieldCount, rec, m_writer);
                Lengths.Add((int)(Data.Length - pos0));
            }

            internal void Save(Stream fw)
            {
                BinaryWriter bw = new BinaryWriter(fw);
                bw.Write(Count);
                foreach (int i in Lengths) bw.Write(i);
                byte[] data = Data.ToArray();
                bw.Write(data);
            }
        }

        class ChunkInfo
        {
            internal int[] Lengths;
            internal int ChunkSize;
            internal int Count { get { return Lengths.Length; } }
            internal static ChunkInfo LoadInfo(BinaryReader br)
            {
                int size = br.ReadInt32();
                var res = new ChunkInfo();
                res.Lengths = new int[size];
                for (int i = 0; i < size; i++)
                {
                    int s = br.ReadInt32();
                    res.Lengths[i] = s;
                    res.ChunkSize += s;
                }
                return res;
            }
        }

        public GridTable(ITableStructure table, string title)
            : base((IPhysicalConnection)null)
        {
            m_title = title;
            m_file = Path.GetTempFileName();
            m_cache = new FileStream(m_file, FileMode.Create);
            m_table = table;
            m_state = TabularDataViewState.Prepared;
            //m_colnames = new List<string>(from c in m_table.Columns select c.ColumnName).ToArray();
            //m_schema = schema;
            //m_dialect = dialect;
        }

        public void GenerateScript(TableDataScript script, TableDataSetProperties props, ISqlDumper dmp)
        {
        }

        public override string ToString()
        {
            return m_title;
        }

        private void DoFillOnBackground(IDataQueue queue)
        {
            try
            {
                m_state = TabularDataViewState.Loading;
                //DataTable curbuf = null;
                Chunk curchunk = null;
                try
                {
                    while (!queue.IsEof)
                    {
                        if (curchunk == null)
                        {
                            curchunk = new Chunk();
                        }

                        IBedRecord rec = queue.GetRecord();
                        curchunk.SaveRecord(rec);

                        if (curchunk.Count >= BUFFER_SIZE)
                        {
                            FlushChunk(curchunk);
                            curchunk = null;
                        }
                    }
                }
                finally
                {
                    queue.CloseReading();
                }
                if (curchunk != null) FlushChunk(curchunk);
                m_state = TabularDataViewState.Prepared;
            }
            catch (Exception e)
            {
                Errors.Report(e);
                m_state = TabularDataViewState.Error;
                queue.PutError(e);
            }
            finally
            {
                queue.CloseWriting();
            }
            if (LoadedNextData != null) LoadedNextData(this, new LoadedNextDataArgs(m_serializedRows));
        }

        public void FillOnBackground(IDataQueue queue)
        {
            m_fillProc = DoFillOnBackground;
            m_fillProc.BeginInvoke(queue, FillFinished, null);
        }

        private void FillFinished(IAsyncResult async)
        {
            try
            {
                m_fillProc.EndInvoke(async);
            }
            catch (Exception e)
            {
                Errors.Report(e);
            }
        }

        public void Fill(IEnumerable<IBedRecord> rows)
        {
            Chunk curchunk = null;
            foreach(var row in rows)
            {
                if (curchunk == null)
                {
                    curchunk = new Chunk();
                }

                curchunk.SaveRecord(row);

                if (curchunk.Count >= BUFFER_SIZE)
                {
                    FlushChunk(curchunk);
                    curchunk = null;
                }
            }
            if (curchunk != null) FlushChunk(curchunk);
        }

        public void Fill(IBedReader reader)
        {
            Fill(reader.EnumRows());   
        }

        private void FlushChunk(Chunk chunk)
        {
            lock (m_directory)
            {
                m_cache.Seek(0, SeekOrigin.End);
                m_directory.Add(m_cache.Position);
                chunk.Save(m_cache);
                m_serializedRows += chunk.Count;
            }
            if (LoadedNextData != null) LoadedNextData(this, new LoadedNextDataArgs(m_serializedRows));
        }

        #region ITabularData Members

        public ITabularDataView CloneView()
        {
            m_refCount++;
            return this;
        }

        public string FixedPerspective { get { return null; } }

        public void NotifyPerspectiveChanged(TablePerspective per) { }
        public void NotifyRefresh() { }

        public ISqlDialect Dialect { get; set; }

        public TableCopyOptions CopyOptions { get; set; }

        public SettingsPageCollection Settings { get { return GlobalSettings.Pages; } }

        public IProgressInfo ProgressInfo { get; set; }

        public void LoadFromXml(XmlElement xml)
        {
            throw new SerializeNotSupportedException("DAE-00286");
        }
        public void SaveToXml(XmlElement xml)
        {
            throw new SerializeNotSupportedException("DAE-00287");
        }

        public ITableStructure GetStructure(TablePerspective per)
        {
            return m_table;
        }

        public ITableSource TableSource { get { return null; } }
        public IDatabaseSource DatabaseSource { get { return null; } }

        public bool SupportsMode(TabularDataStoreMode mode) { return mode == TabularDataStoreMode.Read; }

        public BedTable LoadTableData(TablePageProperties props)
        {
            lock (m_directory)
            {
                BedTable table = new BedTable(m_table);

                int start = 0;
                int count = m_serializedRows;
                if (props.Count != null) count = props.Count.Value;
                if (props.Start != null) start = props.Start.Value;

                if (start >= m_serializedRows) return table;
                int curdic = start / BUFFER_SIZE, skiprec = start % BUFFER_SIZE;
                Errors.Assert(curdic < m_directory.Count);
                m_cache.Seek(m_directory[curdic], SeekOrigin.Begin);

                int availtables = m_directory.Count - curdic;
                BinaryReader br = new BinaryReader(m_cache);
                while (table.Rows.Count < count && availtables >= 1)
                {
                    ChunkInfo info = ChunkInfo.LoadInfo(br);
                    if (skiprec > 0)
                    {
                        int skipbytes = 0;
                        for (int i = 0; i < skiprec; i++) skipbytes += info.Lengths[i];
                        m_cache.Seek(skipbytes, SeekOrigin.Current);
                    }
                    int rec = skiprec;

                    while (rec < info.Count)
                    {
                        table.AddRowInternal(BedTool.LoadRecord(br, m_table));
                        rec++;
                    }
                    availtables--;
                    skiprec = 0;
                }
                return table;
            }
        }

        public IEnumerable<IBedRecord> EnumRows(TableDataSetProperties props)
        {
            int page = 0;
            do
            {
                while (page < m_directory.Count)
                {
                    lock (m_directory)
                    {
                        BinaryReader br = new BinaryReader(m_cache);
                        m_cache.Seek(m_directory[page], SeekOrigin.Begin);
                        ChunkInfo info = ChunkInfo.LoadInfo(br);
                        for (int i = 0; i < info.Count; i++)
                        {
                            yield return BedTool.LoadRecord(br, m_table);
                        }
                    }
                    page++;
                }
                if (State == TabularDataViewState.Loading) System.Threading.Thread.Sleep(100);
            } while (State == TabularDataViewState.Loading);
        }

        public void LoadAllRows(TableDataSetProperties props, Action<IBedRecord> forEachRow)
        {
            foreach (var row in EnumRows(props))
            {
                forEachRow(row);
            }
        }

        public int RowCount { get { return m_serializedRows; } }

        public void SaveChanges(BedTable table, ISaveDataProgress progress)
        {
            throw new NotImplementedError("DAE-00288");
        }

        public void SaveChanges(BedTable table, ISqlDumper dmp)
        {
            throw new NotImplementedError("DAE-00289");
        }

        public bool Readonly
        {
            get { return true; }
        }

        public int? LoadRowCount(TableDataSetProperties props)
        {
            return m_serializedRows;
        }

        public event LoadedNextDataDelegate LoadedNextData;
        public event LoadedTableInfoDelegate LoadedDataInfo;

        public TabularDataViewState State { get { return TabularDataViewState.Prepared; } }

        public ITabularDataStore GetStoreAndClone(TableDataSetProperties props)
        {
            m_refCount++;
            return this;
        }

        public void CloseView()
        {
            m_refCount--;
            if (m_refCount > 0) return;
            m_directory = null;
            m_cache.Close();
            File.Delete(m_file);
        }

        public void CloseAllResources()
        {
            CloseView();
        }

        public TabularDataViewCaps TabDataCaps
        {
            get
            {
                return new TabularDataViewCaps
                {
                    AllFlags = false,
                    Paging = true,
                };
            }
        }

        [Browsable(false)]
        public ITabularDataOuputStream StreamApi
        {
            get { return null; }
        }

        #endregion

        #region ITabularDataStore Members

        public TabularDataStoreMode Mode
        {
            get
            {
                return TabularDataStoreMode.Read;
            }
            set
            {
                Errors.Assert(value == TabularDataStoreMode.Read);
            }
        }

        private void DoRead(IDataQueue queue)
        {
            try
            {
                int page = 0;
                do
                {
                    while (page < m_directory.Count)
                    {
                        lock (m_directory)
                        {
                            BinaryReader br = new BinaryReader(m_cache);
                            m_cache.Seek(m_directory[page], SeekOrigin.Begin);
                            ChunkInfo info = ChunkInfo.LoadInfo(br);
                            for (int i = 0; i < info.Count; i++)
                            {
                                queue.PutRecord(BedTool.LoadRecord(br, m_table));
                            }
                        }
                        page++;
                    }
                    if (State == TabularDataViewState.Loading) System.Threading.Thread.Sleep(100);
                } while (State == TabularDataViewState.Loading);
                queue.PutEof();
            }
            finally
            {
                queue.CloseWriting();
            }
        }
        
        public IAsyncResult BeginRead(AsyncCallback callback, IDataQueue queue)
        {
            Action<IDataQueue> readproc = DoRead;
            IAsyncResult res = readproc.BeginInvoke(queue, callback, null);
            lock (m_readProcs) { m_readProcs[res] = readproc; }
            return res;
        }

        public void EndRead(IAsyncResult async)
        {
            lock (m_readProcs)
            {
                try
                {
                    m_readProcs[async].EndInvoke(async);
                }
                finally
                {
                    m_readProcs.Remove(async);
                }
            }
        }

        public IAsyncResult BeginWrite(AsyncCallback callback, IDataQueue queue)
        {
            throw new NotImplementedError("DAE-00290");
        }

        public void EndWrite(IAsyncResult async)
        {
            throw new NotImplementedError("DAE-00291");
        }

        public IAsyncResult BeginGetRowFormat(AsyncCallback callback)
        {
            return new ValueAsyncResult(m_table, null);
        }

        public void ClearCaches() { }

        public ITableStructure EndGetRowFormat(IAsyncResult async)
        {
            if (async is ValueAsyncResult) return (ITableStructure)((ValueAsyncResult)async).EndInvoke();
            throw new NotImplementedError("DAE-00292");
        }

        public void SetRowFormat(ITableStructure rowFormat)
        {
            throw new NotImplementedError("DAE-00293");
        }

        public bool ConfigurationNeeded
        {
            get { return false; }
        }

        public bool AvailableRowFormat
        {
            get { return true; }
        }

        public void CheckConfiguration() { }

        public bool SupportsSerialize { get { return false; } }

        #endregion
    }
}
