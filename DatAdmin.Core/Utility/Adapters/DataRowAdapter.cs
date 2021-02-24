using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DatAdmin
{
    public class DataRowAdapter : IDataRecord
    {
        #region Members
        protected DataRow m_row;
        #endregion

        #region Properties
        public DataRow Row
        {
            get { return m_row; }
        }
        #endregion

        #region Constructors
        public DataRowAdapter(DataRow row)
        {
            m_row = row;
        }
        #endregion

        #region IDataRecord Implementation
        public object this[string name]
        {
            get { return m_row[name]; }
        }

        public object this[int i]
        {
            get { return m_row[i]; }
        }

        public virtual int FieldCount
        {
            get { return m_row.Table.Columns.Count; }
        }

        public bool GetBoolean(int i)
        {
            return Convert.ToBoolean(m_row[i]);
        }

        public byte GetByte(int i)
        {
            return Convert.ToByte(m_row[i]);
        }

        public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        {
            throw new NotSupportedException("DAE-00270 GetBytes is not supported.");
        }

        public char GetChar(int i)
        {
            return Convert.ToChar(m_row[i]);
        }

        public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
        {
            throw new NotSupportedException("DAE-00271 GetChars is not supported.");
        }

        public IDataReader GetData(int i)
        {
            throw new NotSupportedException("DAE-00272 GetData is not supported.");
        }

        public string GetDataTypeName(int i)
        {
            return m_row[i].GetType().Name;
        }

        public DateTime GetDateTime(int i)
        {
            return Convert.ToDateTime(m_row[i]);
        }

        public decimal GetDecimal(int i)
        {
            return Convert.ToDecimal(m_row[i]);
        }

        public double GetDouble(int i)
        {
            return Convert.ToDouble(m_row[i]);
        }

        public Type GetFieldType(int i)
        {
            return m_row[i].GetType();
        }

        public float GetFloat(int i)
        {
            return Convert.ToSingle(m_row[i]);
        }

        public Guid GetGuid(int i)
        {
            return (Guid)m_row[i];
        }

        public short GetInt16(int i)
        {
            return Convert.ToInt16(m_row[i]);
        }

        public int GetInt32(int i)
        {
            return Convert.ToInt32(m_row[i]);
        }

        public long GetInt64(int i)
        {
            return Convert.ToInt64(m_row[i]);
        }

        public virtual string GetName(int i)
        {
            return m_row.Table.Columns[i].ColumnName;
        }

        public int GetOrdinal(string name)
        {
            return m_row.Table.Columns.IndexOf(name);
        }

        public string GetString(int i)
        {
            return m_row[i].ToString();
        }

        public object GetValue(int i)
        {
            return m_row[i];
        }

        public int GetValues(object[] values)
        {
            object[] src = m_row.ItemArray;
            Array.Copy(src, values, src.Length);
            return src.Length;
        }

        public bool IsDBNull(int i)
        {
            return Convert.IsDBNull(m_row[i]);
        }
        #endregion
    }
}
