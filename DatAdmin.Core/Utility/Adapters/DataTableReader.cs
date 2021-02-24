using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DatAdmin
{
    public class DataTableReader : DataRowAdapter, IDataReader
    {
        DataTable m_table;
        int m_index = -1;

        public DataTableReader(DataTable table) : base(null)
        {
            m_table = table;
        }
        #region IDataReader Members

        public void Close()
        {
        }

        public int Depth
        {
            get { return 0; }
        }

        public DataTable GetSchemaTable()
        {
            return ConnTools.SchemaFromStructure(m_table.Columns.GetTableStructure("table"));
        }

        public bool IsClosed
        {
            get { return false; }
        }

        public bool NextResult()
        {
            return false;
        }

        public bool Read()
        {
            m_index++;

            if (m_index < m_table.Rows.Count)
            {
                m_row = m_table.Rows[m_index];
                return true;
            }

            return false;
        }

        public int RecordsAffected
        {
            get { return 0; }
        }

        #endregion

        public override int FieldCount
        {
            get
            {
                if (m_row == null) return m_table.Columns.Count;
                return base.FieldCount;
            }
        }

        public override string GetName(int i)
        {
            if (m_row == null) return m_table.Columns[i].ColumnName;
            return base.GetName(i);
        }

        #region IDisposable Members

        public void Dispose()
        {
        }

        #endregion
    }
}
