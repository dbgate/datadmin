using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DatAdmin
{
    public abstract class DataRecordAdapterBase : BedValueHolder, IBedRecord
    {
        protected IDataRecord m_record;
        protected ITableStructure m_structure;

        #region IBedRecord Members

        public ITableStructure Structure
        {
            get { return m_structure; }
        }

        public int FieldCount
        {
            get { return m_structure.Columns.Count; }
        }

        public int GetOrdinal(string colName)
        {
            return m_structure.Columns.GetIndex(colName);
        }

        public string GetName(int i)
        {
            return m_structure.Columns[i].ColumnName;
        }

        public virtual void ReadValue(int i)
        {
            this.ReadFrom(m_record, i);
        }

        public int GetValues(object[] data)
        {
            int cnt = Math.Min(data.Length, FieldCount);
            for (int i = 0; i < cnt; i++)
            {
                data[i] = this.GetValue(i);
            }
            return cnt;
        }

        #endregion
    }

    public class DataRecordAdapter : DataRecordAdapterBase
    {
        public DataRecordAdapter(IDataRecord record, ITableStructure table)
        {
            m_record = record;
            m_structure = table;
        }

        public IDataRecord Record
        {
            get { return m_record; }
            set { m_record = value; }
        }

        public new ITableStructure Structure
        {
            get { return m_structure; }
            set { m_structure = value; }
        }
    }

    public class DataReaderAdapter : DataRecordAdapterBase, IBedReader
    {
        IDataReader m_reader;
        ISqlDialect m_dialect;

        public bool DisposeReader = true;

        public DataReaderAdapter(IDataReader reader, ISqlDialect dialect)
        {
            m_reader = reader;
            m_dialect = dialect;
            m_structure = reader.GetTableStructure(m_dialect);
        }

        #region IBedReader Members

        public bool Read()
        {
            if (m_reader.Read())
            {
                m_record = m_reader;
                return true;
            }
            else
            {
                m_record = null;
                return false;
            }
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            if (DisposeReader) m_reader.Dispose();
        }

        #endregion
    }
}
