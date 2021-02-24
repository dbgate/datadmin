using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.Xml;

namespace Plugin.dbmodel
{
    public class TableDefDataView : GenericConnectionUsage, ITabularDataView, IFormattable
    {
        TableDefSource m_table;

        public TableDefDataView(TableDefSource table)
            : base((IPhysicalConnection)null)
        {
            m_table = table;
        }

        public override string ToString()
        {
            return ToString("L", null);
        }

        #region ITabularDataView Members

        public ITabularDataView CloneView()
        {
            return new TableDefDataView(m_table);
        }

        public string FixedPerspective { get { return null; } }

        public ISqlDialect Dialect { get { return null; } }

        public IProgressInfo ProgressInfo { get; set; }

        public void NotifyPerspectiveChanged(TablePerspective per) { }
        public void NotifyRefresh() { }

        public BedTable LoadTableData(TablePageProperties props)
        {
            var data = m_table.LoadFixedData();
            if (data != null) return new BedTable(data);
            return new BedTable(m_table.m_table);
        }

        public int? LoadRowCount(TableDataSetProperties props)
        {
            var data = m_table.LoadFixedData();
            if (data != null) return data.Rows.Count;
            return null;
        }

        public void SaveChanges(BedTable table, ISaveDataProgress progress)
        {
            m_table.SaveFixedData(table.ToInMemoryTable());
        }

        public void SaveChanges(BedTable table, ISqlDumper dmp)
        {
            throw new NotImplementedError("DAE-00143");
        }

        public bool Readonly
        {
            get { return false; }
        }

        public event LoadedNextDataDelegate LoadedNextData;
        public event LoadedTableInfoDelegate LoadedDataInfo;

        public TabularDataViewState State
        {
            get { return TabularDataViewState.Prepared; }
        }

        public ITabularDataStore GetStoreAndClone(TableDataSetProperties props)
        {
            return new TableDefDataStore(m_table);
        }

        public void CloseView()
        {
        }

        public void GenerateScript(TableDataScript script, TableDataSetProperties props, ISqlDumper dmp)
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
            return m_table.m_table;
        }

        public SettingsPageCollection Settings
        {
            get { return GlobalSettings.Pages; }
        }

        public bool SupportsSerialize { get { return false; } }
        public void SaveToXml(XmlElement xml)
        {
            throw new NotImplementedError("DAE-00144");
        }

        public void LoadAllRows(TableDataSetProperties props, Action<IBedRecord> forEachRow)
        {
            var data = m_table.LoadFixedData();
            if (data == null) return;
            foreach (var row in data.Rows) forEachRow(row);
        }

        #endregion

        #region IFormattable Members

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return m_table.ToString(format);
        }

        #endregion
    }
}
