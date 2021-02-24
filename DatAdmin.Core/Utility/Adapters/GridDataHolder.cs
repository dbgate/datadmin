using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Data;

namespace DatAdmin
{
    public class GridDataHolder : IDataHolder
    {
        BedRow m_row;
        int m_colindex;
        int m_rowindex;
        DataGridView m_dgv;
        DataLookupInfo m_lookupInfo;
        BedValueConvertor m_convertor;

        public GridDataHolder(BedRow row, int colindex, int rowindex, DataGridView dgv, DataLookupInfo lookupInfo, DataFormatSettings dataFormat)
        {
            m_row = row;
            m_colindex = colindex;
            m_rowindex = rowindex;
            m_dgv = dgv;
            m_lookupInfo = lookupInfo;
            m_convertor = new BedValueConvertor(dataFormat ?? new DataFormatSettings());
        }

        #region IDataHolder Members

        public IBedValueConvertor BedConvertor
        {
            get
            {
                return m_convertor;
            }
        }

        public TypeStorage GetTargetType()
        {
            return m_row.GetDefaultStorage(m_colindex);
        }

        public void GetData(IBedValueWriter writer)
        {
            if (m_row == null) return;
            m_row.ReadValue(m_colindex);
            writer.ReadFrom(m_row);
        }

        public void SetData(IBedValueReader reader)
        {
            if (!IsReadOnly)
            {
                m_row.SetValue(m_colindex, reader);
                m_dgv.InvalidateRow(m_rowindex);
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return m_dgv.ReadOnly || m_row == null;
            }
        }

        public DataLookupInfo LookupInfo { get { return m_lookupInfo; } }

        #endregion
    }

    //public class DataGridViewHolder : IDataHolder
    //{
    //    DataGridViewCell m_cell;
    //    DataGridView m_dgv;
    //    DataLookupInfo m_lookupInfo;

    //    public DataGridViewHolder(DataGridViewCell cell, DataGridView dgv, DataLookupInfo lookupInfo)
    //    {
    //        m_cell = cell;
    //        m_dgv = dgv;
    //        m_lookupInfo = lookupInfo;
    //    }

    //    #region IDataHolder Members

    //    public object GetData()
    //    {
    //        return m_cell.Value;
    //    }

    //    public void SetData(object data)
    //    {
    //        if (!m_dgv.ReadOnly)
    //        {
    //            m_cell.Value = data;
    //            //m_dgv.NotifyCurrentCellDirty(true);
    //            m_dgv.InvalidateRow(m_cell.RowIndex);
    //        }
    //    }

    //    public DataLookupInfo LookupInfo { get { return m_lookupInfo; } }

    //    #endregion
    //}
}
