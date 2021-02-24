using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DatAdmin
{
    //public class QueueToReaderAdapter : IDataReader
    //{
    //    IDataQueue m_queue;
    //    IDataRecord m_record = null;
    //    //bool m_wasNextResult = false;

    //    public QueueToReaderAdapter(IDataQueue queue)
    //    {
    //        m_queue = queue;
    //    }

    //    #region IDataReader Members

    //    public void Close()
    //    {
    //        //m_queue.CloseGet();
    //        m_queue.CloseReading();
    //        m_queue = null;
    //    }

    //    public int Depth
    //    {
    //        get { return 0; }
    //    }

    //    public DataTable GetSchemaTable()
    //    {
    //        return null;
    //        //return ConnTools.SchemaFromStructure(m_queue.GetRowFormat);
    //    }

    //    public bool IsClosed
    //    {
    //        get { return m_queue == null; }
    //    }

    //    public bool NextResult()
    //    {
    //        //if (!m_wasNextResult)
    //        //{
    //        //    m_wasNextResult = true;
    //        //    return true;
    //        //}
    //        return false;
    //    }

    //    public bool Read()
    //    {
    //        if (m_queue.IsEof) return false;
    //        m_record = m_queue.GetRecord();
    //        return true;
    //    }

    //    public int RecordsAffected
    //    {
    //        get { return 0; }
    //    }

    //    #endregion

    //    #region IDisposable Members

    //    public void Dispose()
    //    {
    //        m_queue = null;
    //    }

    //    #endregion

    //    #region IDataRecord Members

    //    public int FieldCount
    //    {
    //        get { return m_queue.GetRowFormat.Columns.Count; }
    //    }

    //    public bool GetBoolean(int i)
    //    {
    //        return m_record.GetBoolean(i);
    //    }

    //    public byte GetByte(int i)
    //    {
    //        return m_record.GetByte(i); 
    //    }

    //    public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
    //    {
    //        return m_record.GetBytes(i, fieldOffset, buffer, bufferoffset, length);
    //    }

    //    public char GetChar(int i)
    //    {
    //        return m_record.GetChar(i);
    //    }

    //    public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
    //    {
    //        return m_record.GetChars(i, fieldoffset, buffer, bufferoffset, length);
    //    }

    //    public IDataReader GetData(int i)
    //    {
    //        return m_record.GetData(i);
    //    }

    //    public string GetDataTypeName(int i)
    //    {
    //        return m_record.GetDataTypeName(i);
    //    }

    //    public DateTime GetDateTime(int i)
    //    {
    //        return m_record.GetDateTime(i);
    //    }

    //    public decimal GetDecimal(int i)
    //    {
    //        return m_record.GetDecimal(i);
    //    }

    //    public double GetDouble(int i)
    //    {
    //        return m_record.GetDouble(i);
    //    }

    //    public Type GetFieldType(int i)
    //    {
    //        return m_queue.GetRowFormat.Columns[i].DataType.DotNetType;
    //    }

    //    public float GetFloat(int i)
    //    {
    //        return m_record.GetFloat(i);
    //    }

    //    public Guid GetGuid(int i)
    //    {
    //        return m_record.GetGuid(i);
    //    }

    //    public short GetInt16(int i)
    //    {
    //        return m_record.GetInt16(i);
    //    }

    //    public int GetInt32(int i)
    //    {
    //        return m_record.GetInt32(i);
    //    }

    //    public long GetInt64(int i)
    //    {
    //        return m_record.GetInt64(i);
    //    }

    //    public string GetName(int i)
    //    {
    //        return m_queue.GetRowFormat.Columns[i].ColumnName;
    //    }

    //    public int GetOrdinal(string name)
    //    {
    //        return m_record.GetOrdinal(name);
    //    }

    //    public string GetString(int i)
    //    {
    //        return m_record.GetString(i);
    //    }

    //    public object GetValue(int i)
    //    {
    //        return m_record.GetValue(i);
    //    }

    //    public int GetValues(object[] values)
    //    {
    //        return m_record.GetValues(values);
    //    }

    //    public bool IsDBNull(int i)
    //    {
    //        return m_record.IsDBNull(i);
    //    }

    //    public object this[string name]
    //    {
    //        get { return m_record[name]; }
    //    }

    //    public object this[int i]
    //    {
    //        get { return m_record[i]; }
    //    }

    //    #endregion
    //}
}
