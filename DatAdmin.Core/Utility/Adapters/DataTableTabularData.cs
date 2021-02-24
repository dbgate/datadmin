using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Xml;

namespace DatAdmin
{
    public abstract class DataTableTabularData : GenericConnectionUsage, ITabularDataView
    {
        DataTable m_table;

        protected DataTableTabularData(IPhysicalConnection conn)
            : base(conn)
        {
        }

        public abstract DataTable LoadTable(IPhysicalConnection conn);
        public abstract ITabularDataView CloneView();

        void WantTable()
        {
            if (m_table == null)
            {
                m_table = LoadTable(m_conn);
                if (LoadedNextData != null) LoadedNextData(this, new LoadedNextDataArgs(m_table.Rows.Count));
            }
        }

        #region ITabularDataView Members

        public void NotifyPerspectiveChanged(TablePerspective per) { }
        public void NotifyRefresh() { }

        public void GenerateScript(TableDataScript script, TableDataSetProperties props, ISqlDumper dmp)
        {
        }

        public IProgressInfo ProgressInfo { get; set; }

        public abstract SettingsPageCollection Settings { get; }

        public ITableStructure GetStructure(TablePerspective per)
        {
            return m_table.Columns.GetTableStructure("table");
        }

        public BedTable LoadTableData(TablePageProperties props)
        {
            WantTable();
            return m_table.ToBinaryTable();
        }

        public void LoadAllRows(TableDataSetProperties props, Action<IBedRecord> forEachRow)
        {
            WantTable();
            foreach (var row in m_table.ToBinaryTable().Rows) forEachRow(row);
        }

        public void SaveChanges(BedTable table, ISaveDataProgress progress)
        {
            throw new NotImplementedError("DAE-00273");
        }

        public void SaveChanges(BedTable table, ISqlDumper dmp)
        {
            throw new NotImplementedError("DAE-00274");
        }

        public bool Readonly
        {
            get { return true; }
        }


        public int? LoadRowCount(TableDataSetProperties props)
        {
            WantTable();
            return m_table.Rows.Count;
        }

        public event LoadedNextDataDelegate LoadedNextData;
        public event LoadedTableInfoDelegate LoadedDataInfo;

        public TabularDataViewState State
        {
            get
            {
                if (m_table != null) return TabularDataViewState.Loading;
                return TabularDataViewState.Prepared;
            }
        }

        public ITabularDataStore GetStoreAndClone(TableDataSetProperties props)
        {
            return new DataTableTabularDataStore(m_conn.Clone(), (Func<IPhysicalConnection, DataTable>)LoadTable);
        }

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

        public ISqlDialect Dialect { get { return null; } }

        public bool SupportsSerialize { get { return false; } }
        public void SaveToXml(XmlElement xml)
        {
            throw new NotImplementedError("DAE-00119");
        }

        public string FixedPerspective { get { return null; } }

        #endregion
    }

    public class DataTableTabularDataStore : GenericConnectionUsage, ITabularDataStore
    {
        Func<IPhysicalConnection, DataTable> m_loadFunc;
        Func<IPhysicalConnection, BedTable> m_loadBedFunc;
        
        BedTable m_table;
        ITableStructure m_struct;

        public DataTableTabularDataStore(IPhysicalConnection conn, Func<IPhysicalConnection, DataTable> loadFunc)
            : base(conn)
        {
            m_conn = conn;
            m_loadFunc = loadFunc;
        }

        public DataTableTabularDataStore(IPhysicalConnection conn, Func<IPhysicalConnection, BedTable> loadBedFunc)
            : base(conn)
        {
            m_conn = conn;
            m_loadBedFunc = loadBedFunc;
        }

        void WantTable()
        {
            if (m_table == null)
            {
                if (m_loadFunc != null)
                {
                    m_table = m_loadFunc(m_conn).ToBinaryTable();
                }
                if (m_loadBedFunc != null)
                {
                    m_table = m_loadBedFunc(m_conn);
                }
            }
        }

        #region ITabularDataStore Members

        public TabularDataStoreMode Mode
        {
            get { return TabularDataStoreMode.Read; }
            set { }
        }
    
        public IProgressInfo ProgressInfo { get; set; }

        public IAsyncResult BeginRead(AsyncCallback callback, IDataQueue queue)
        {
            return m_conn.BeginInvoke((Action<IDataQueue>)DoRead, callback, queue);
        }

        public void EndRead(IAsyncResult async)
        {
            m_conn.EndInvoke(async);
        }

        private void DoRead(IDataQueue queue)
        {
            WantTable();
            foreach (var row in m_table.Rows)
            {
                queue.PutRecord(row);
            }
            queue.PutEof();
        }

        public IAsyncResult BeginWrite(AsyncCallback callback, IDataQueue queue)
        {
            throw new NotImplementedError("DAE-00120");
        }

        public void EndWrite(IAsyncResult async)
        {
            throw new NotImplementedError("DAE-00121");
        }

        public IAsyncResult BeginGetRowFormat(AsyncCallback callback)
        {
            return m_conn.BeginInvoke((Action)DoGetRowFormat, callback);
        }

        public ITableStructure EndGetRowFormat(IAsyncResult async)
        {
            m_conn.EndInvoke(async);
            return m_table.Structure;
        }

        private void DoGetRowFormat()
        {
            WantTable();
        }

        public void SetRowFormat(ITableStructure rowFormat)
        {
            throw new NotImplementedError("DAE-00122");
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

        public void SaveToXml(System.Xml.XmlElement xml)
        {
            throw new NotImplementedError("DAE-00123");
        }

        public void LoadFromXml(System.Xml.XmlElement xml)
        {
            throw new NotImplementedError("DAE-00124");
        }

        public void CloseAllResources()
        {
        }

        public ITabularDataOuputStream StreamApi
        {
            get { return null; }
        }

        public TableCopyOptions CopyOptions
        {
            get
            {
                return new TableCopyOptions();
            }
            set
            {
            }
        }

        #endregion
    }
}
