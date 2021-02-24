using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.IO;
using DatAdmin;
using System.Collections;
using System.Data;

namespace DatAdmin
{
    public class TunnelDataReader : DbDataReader, IEnumerator
    {
        ITunnelResultSet m_result;
        ITableStructure m_structure;
        object[] m_row = null;

        public TunnelDataReader(ITunnelResultSet result)
        {
            m_result = result;
            m_structure = m_result.GetStructure();
        }

        public override void Close()
        {
            m_result.Close();
        }

        public override int Depth
        {
            get { return 0; }
        }

        public override int FieldCount
        {
            get { return m_structure.Columns.Count; }
        }

        public override bool GetBoolean(int ordinal)
        {
            return Convert.ToBoolean(GetValue(ordinal));
        }

        public override byte GetByte(int ordinal)
        {
            return Convert.ToByte(GetValue(ordinal));
        }

        public override long GetBytes(int ordinal, long dataOffset, byte[] buffer, int bufferOffset, int length)
        {
            throw new NotImplementedError("DAE-00303");
        }

        public override char GetChar(int ordinal)
        {
            return Convert.ToChar(GetValue(ordinal));
        }

        public override long GetChars(int ordinal, long dataOffset, char[] buffer, int bufferOffset, int length)
        {
            throw new NotImplementedError("DAE-00304");
        }

        public override string GetDataTypeName(int ordinal)
        {
            return typeof(String).Name;
        }

        public override DateTime GetDateTime(int ordinal)
        {
            return Convert.ToDateTime(GetValue(ordinal));
        }

        public override decimal GetDecimal(int ordinal)
        {
            return Convert.ToDecimal(GetValue(ordinal));
        }

        public override double GetDouble(int ordinal)
        {
            return Convert.ToDouble(GetValue(ordinal));
        }

        public override IEnumerator GetEnumerator()
        {
            return this;
        }

        public override Type GetFieldType(int ordinal)
        {
            return m_structure.Columns[ordinal].DataType.DotNetType;
        }

        public override float GetFloat(int ordinal)
        {
            return Convert.ToSingle(GetValue(ordinal));
        }

        public override Guid GetGuid(int ordinal)
        {
            return Guid.NewGuid();
        }

        public override short GetInt16(int ordinal)
        {
            return Convert.ToInt16(GetValue(ordinal));
        }

        public override int GetInt32(int ordinal)
        {
            return Convert.ToInt32(GetValue(ordinal));
        }

        public override long GetInt64(int ordinal)
        {
            return Convert.ToInt64(GetValue(ordinal));
        }

        public override string GetName(int ordinal)
        {
            return m_structure.Columns[ordinal].ColumnName;
        }

        public override int GetOrdinal(string name)
        {
            return m_structure.Columns.GetIndex(name);
        }

        public override DataTable GetSchemaTable()
        {
            DataTable res = ConnTools.SchemaFromStructure(m_structure);
            for (int i = 0; i < m_structure.Columns.Count; i++)
            {
                DataRow row = res.Rows[i];
                row[SchemaTableColumn.BaseTableName] = m_structure.FullName != null ? m_structure.FullName.Name : null;
                row[SchemaTableColumn.IsKey] = m_structure.GetKeyWithColumn<IPrimaryKey>(m_structure.Columns[i]) != null;
            }
            return res;
        }

        public override string GetString(int ordinal)
        {
            return m_row[ordinal].SafeToString();
        }

        public override object GetValue(int ordinal)
        {
            return m_row[ordinal];
        }

        public override int GetValues(object[] values)
        {
            if (m_row == null) return 0;
            int len = Math.Min(values.Length, m_row.Length);
            for (int i = 0; i < len; i++) values[i] = m_row[i];
            return len;
        }

        public override bool HasRows
        {
            get { return m_structure.Columns.Count > 0; }
        }

        public override bool IsClosed
        {
            get { return false; }
        }

        public override bool IsDBNull(int ordinal)
        {
            return m_row[ordinal] == null;
        }

        public override bool NextResult()
        {
            return false;
        }

        public override bool Read()
        {
            m_row = m_result.FetchRow();
            return m_row != null;
        }

        public override int RecordsAffected
        {
            get { return m_result.RecordsAffected; }
        }

        public override object this[string name]
        {
            get { return this[GetOrdinal(name)]; }
        }

        public override object this[int ordinal]
        {
            get { return m_row[ordinal]; }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            m_result.Dispose();
        }

        #region IEnumerator Members

        public object Current
        {
            get { return this; }
        }

        public bool MoveNext()
        {
            return Read();
        }

        public void Reset()
        {
        }

        #endregion
    }
}
