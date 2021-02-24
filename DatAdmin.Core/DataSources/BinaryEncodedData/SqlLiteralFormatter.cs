using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace DatAdmin
{
    public class SqlLiteralFormatter : IBedValueFormatter
    {
        protected string m_text;
        protected ISqlDialect m_dialect;
        public DbTypeBase TargetType; // type to be formatted for

        #region IBedValueWriter Members

        public SqlLiteralFormatter(ISqlDialect dialect)
        {
            m_dialect = dialect;
        }

        public virtual void SetBoolean(bool value)
        {
            m_text = value ? "1" : "0";
        }

        public virtual void SetByte(byte value)
        {
            m_text = value.ToString(CultureInfo.InvariantCulture);
        }

        public virtual void SetSByte(sbyte value)
        {
            m_text = value.ToString(CultureInfo.InvariantCulture);
        }

        public virtual void SetByteArray(byte[] value)
        {
            if (value.Length == 0) m_text = "''";
            else m_text = "0x" + StringTool.EncodeHex(value);
        }

        public virtual void SetDateTime(DateTime value)
        {
            m_text = "'" + value.ToString("s") + "'";
        }

        public virtual void SetDateTimeEx(DateTimeEx value)
        {
            m_text = "'" + value.ToStringNormalized() + "'";
        }

        public virtual void SetDateEx(DateEx value)
        {
            m_text = "'" + value.ToStringNormalized() + "'";
        }

        public virtual void SetTimeEx(TimeEx value)
        {
            m_text = "'" + value.ToStringNormalized() + "'";
        }

        public virtual void SetDecimal(decimal value)
        {
            m_text = value.ToString(CultureInfo.InvariantCulture);
        }

        public virtual void SetDouble(double value)
        {
            m_text = value.ToString(CultureInfo.InvariantCulture);
        }

        public virtual void SetFloat(float value)
        {
            m_text = value.ToString(CultureInfo.InvariantCulture);
        }

        public virtual void SetGuid(Guid value)
        {
            m_text = "'" + value.ToString() + "'";
        }

        public virtual void SetInt16(short value)
        {
            m_text = value.ToString(CultureInfo.InvariantCulture);
        }

        public virtual void SetInt32(int value)
        {
            m_text = value.ToString(CultureInfo.InvariantCulture);
        }

        public virtual void SetInt64(long value)
        {
            m_text = value.ToString(CultureInfo.InvariantCulture);
        }

        public virtual void SetUInt16(ushort value)
        {
            m_text = value.ToString(CultureInfo.InvariantCulture);
        }

        public virtual void SetUInt32(uint value)
        {
            m_text = value.ToString(CultureInfo.InvariantCulture);
        }

        public virtual void SetUInt64(ulong value)
        {
            m_text = value.ToString(CultureInfo.InvariantCulture);
        }

        public virtual void SetString(string value)
        {
            StringBuilder sb = new StringBuilder(value.Length + 10);
            sb.Append('\'');
            EscapeString(value, sb);
            sb.Append('\'');
            m_text = sb.ToString();
        }

        //public virtual void SetArray(Array value)
        //{
        //    SetString(BedArray.ToString(value));
        //}

        public virtual void SetNull()
        {
            m_text = "NULL";
        }

        #endregion

        #region IBedValueFormatter Members

        public IProgressInfo ProgressInfo { get; set; }

        public string GetText()
        {
            return m_text;
        }

        #endregion

        protected void EscapeString(string value, StringBuilder sb)
        {
            m_dialect.EscapeString(value, sb);
        }
    }
}
