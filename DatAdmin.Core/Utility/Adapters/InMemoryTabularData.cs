using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    public class InMemoryTabularData : GenericConnectionUsage, ITabularDataView, ITabularDataStore
    {
        BedTable m_table;
        Dictionary<IAsyncResult, Action<IDataQueue>> m_readProcs = new Dictionary<IAsyncResult, Action<IDataQueue>>();

        public InMemoryTabularData(BedTable table)
        {
            m_table = table;
        }

        #region ITabularDataView Members

        public BedTable LoadTableData(TablePageProperties props)
        {
            return m_table;
        }

        public int? LoadRowCount(TableDataSetProperties props)
        {
            return m_table.Rows.Count;
        }

        public void LoadAllRows(TableDataSetProperties props, Action<IBedRecord> forEachRow)
        {
            foreach (var row in m_table.Rows) forEachRow(row);
        }

        public void NotifyPerspectiveChanged(TablePerspective per)
        {
        }

        public void NotifyRefresh()
        {
        }

        public void SaveChanges(BedTable table, ISaveDataProgress progress)
        {
            throw new NotImplementedError("DAE-00379");
        }

        public void SaveChanges(BedTable table, ISqlDumper dmp)
        {
            throw new NotImplementedError("DAE-00380");
        }

        public bool Readonly
        {
            get { return true; }
        }

        public event LoadedNextDataDelegate LoadedNextData;

        public event LoadedTableInfoDelegate LoadedDataInfo;

        public TabularDataViewState State
        {
            get { return TabularDataViewState.Prepared; }
        }

        public ITabularDataStore GetStoreAndClone(TableDataSetProperties props)
        {
            return this;
        }

        public IProgressInfo ProgressInfo { get; set; }

        public void CloseView()
        {
        }

        public TabularDataViewCaps TabDataCaps
        {
            get
            {
                return new TabularDataViewCaps
                {
                    AllFlags = false
                };
            }
        }

        public ITableSource TableSource
        {
            get { return null; }
        }

        public IDatabaseSource DatabaseSource
        {
            get { return null; }
        }

        public ITableStructure GetStructure(TablePerspective per)
        {
            return m_table.Structure;
        }

        public SettingsPageCollection Settings
        {
            get { return GlobalSettings.Pages; }
        }

        public ISqlDialect Dialect
        {
            get { return GenericDialect.Instance; }
        }

        public ITabularDataView CloneView()
        {
            return this;
        }

        public void GenerateScript(TableDataScript script, TableDataSetProperties props, ISqlDumper dmp)
        {
            throw new NotImplementedError("DAE-00381");
        }

        public bool SupportsSerialize
        {
            get { return false; }
        }

        public void SaveToXml(System.Xml.XmlElement xml)
        {
            throw new NotImplementedError("DAE-00382");
        }

        public string FixedPerspective
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
                foreach (var row in m_table.Rows)
                {
                    queue.PutRecord(row);
                }
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
            throw new NotImplementedError("DAE-00383");
        }

        public void EndWrite(IAsyncResult async)
        {
            throw new NotImplementedError("DAE-00384");
        }

        public IAsyncResult BeginGetRowFormat(AsyncCallback callback)
        {
            return new ValueAsyncResult(m_table.Structure, null);
        }

        public ITableStructure EndGetRowFormat(IAsyncResult async)
        {
            if (async is ValueAsyncResult) return (ITableStructure)((ValueAsyncResult)async).EndInvoke();
            throw new NotImplementedError("DAE-00385");
        }

        public void SetRowFormat(ITableStructure rowFormat)
        {
            throw new NotImplementedError("DAE-00386");
        }

        public bool ConfigurationNeeded
        {
            get { return false; }
        }

        public bool AvailableRowFormat
        {
            get { return true; }
        }

        public bool SupportsMode(TabularDataStoreMode mode)
        {
            return mode == TabularDataStoreMode.Read;
        }

        public void CheckConfiguration()
        {
        }

        public void ClearCaches()
        {
        }

        public void LoadFromXml(System.Xml.XmlElement xml)
        {
            throw new NotImplementedError("DAE-00387");
        }

        public void CloseAllResources()
        {
        }

        public ITabularDataOuputStream StreamApi
        {
            get { return null; }
        }

        public TableCopyOptions CopyOptions { get; set; }

        #endregion
    }
}
