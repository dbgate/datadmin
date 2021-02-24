using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DatAdmin
{
    public class DataQueueReaderAdapter : DataRecordProxy, IDataReader
    {
        IDataQueue m_queue;
        ITableStructure m_getFormat;
        bool m_isClosed;
        BedRecordAdapter m_recada;

        public DataQueueReaderAdapter(IDataQueue queue)
        {
            m_queue = queue;
            m_getFormat = m_queue.GetRowFormat;
            m_recada = new BedRecordAdapter();
        }

        public int ReadedRows { get; private set; }

        #region IDataReader Members

        public void Close()
        {
            m_queue.CloseReading();
            m_isClosed = true;
        }

        public int Depth
        {
            get { return 0; }
        }

        public DataTable GetSchemaTable()
        {
            return m_getFormat.SchemaFromStructure();
        }

        public bool IsClosed
        {
            get { return m_isClosed; }
        }

        public bool NextResult()
        {
            return false;
        }

        public bool Read()
        {
            if (m_queue.IsEof)
            {
                base.m_obj = null;
                m_recada.Record = null;
                return false;
            }
            else
            {
                m_recada.Record = m_queue.GetRecord();
                base.m_obj = m_recada;
                ReadedRows++;
                return true;
            }
        }

        public int RecordsAffected
        {
            get { return 0; }
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            if (!IsClosed) Close();
        }

        #endregion

        #region IDataRecord Members

        public override int FieldCount
        {
            get { return m_getFormat.Columns.Count; }
        }

        public override Type GetFieldType(int i)
        {
            return m_getFormat.Columns[i].DataType.DotNetType;
        }

        #endregion
    }
}
