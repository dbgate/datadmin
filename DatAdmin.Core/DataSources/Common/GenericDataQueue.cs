using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DatAdmin
{
    public class GenericDataQueue : IDataQueue
    {
        class Element
        {
            internal bool IsEof;
            internal IBedRecord Record;
            //internal Exception Error;
        }

        const int MAX_SIZE = 0x1000;
        WaitQueue<Element> m_queue = new WaitQueue<Element>(MAX_SIZE);
        ITableStructure m_putFormat;
        ITableStructure m_getFormat;
        IRowTransform m_transform;
        bool m_wasEof;
        Exception m_error;
        List<IRecordToDbAdapter> m_outputAdapters = new List<IRecordToDbAdapter>();

        //internal Dictionary<string, int> m_fieldNameToIndex_Get = new Dictionary<string, int>();

        public GenericDataQueue(ITableStructure putFormat, ITableStructure getFormat, IRowTransform transform)
        {
            m_putFormat = putFormat;
            m_getFormat = getFormat;
            m_transform = transform;
            //foreach (IColumnStructure col in getFormat.Columns)
            //{
            //    m_fieldNameToIndex_Get[col.ColumnName] = col.ColumnOrder;
            //}
        }

        public void AddOutputAdapter(IRecordToDbAdapter recordAdapter)
        {
            m_outputAdapters.Add(recordAdapter);
        }

        #region IDataQueue Members

        public IProgressInfo ProgressInfo { get; set; }

        public IBedRecord GetRecord()
        {
            try
            {
                Element res = m_queue.Get();
                if (res.IsEof) throw new Exception("DAE-00223 Eof reached");
                IBedRecord rec = res.Record;
                foreach (var ada in m_outputAdapters)
                {
                    rec = ada.AdaptRecord(rec, ProgressInfo);
                }
                return rec;
            }
            catch (QueueClosedError)
            {
                if (m_error != null) throw new QueueClosedError("DAE-00224", m_error);
                throw new QueueClosedError("DAE-00225");
            }
        }

        public void PutError(Exception e)
        {
            m_error = e;
        }

        public void CloseReading()
        {
            m_queue.Close();
        }

        public void CloseWriting()
        {
            if (m_wasEof) return;
            m_queue.Close();
        }

        public void PutRecord(IBedRecord record)
        {
            object[] values = new object[record.FieldCount];
            record.GetValues(values);
            IBedRecord input = new ArrayDataRecord(m_putFormat, values);
            IBedRecord trans = m_transform.Transform(input);
            m_queue.Put(new Element { Record = trans });
        }

        public ITableStructure PutRowFormat
        {
            get { return m_putFormat; }
        }

        public ITableStructure GetRowFormat
        {
            get { return m_getFormat; }
        }

        public void PutEof()
        {
            m_queue.Put(new Element { IsEof = true });
            m_wasEof = true;
        }

        public bool IsEof
        {
            get { return m_queue.Peek().IsEof; }
        }

        #endregion
    }
}
