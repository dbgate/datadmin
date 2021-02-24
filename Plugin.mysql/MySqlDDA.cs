using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.Data;
using MySql.Data.MySqlClient;
using MySql.Data.Types;
using System.Globalization;

namespace Plugin.mysql
{
    public class MySqlDDA : DialectDataAdapterBase
    {
        static DateTimeEx m_mintimestamp = new DateTimeEx(1970, 1, 1, 0, 0, 1);
        static DateTimeEx m_maxtimestamp = new DateTimeEx(2038, 1, 9, 3, 14, 7);

        public MySqlDDA(ISqlDialect dialect)
            : base(dialect)
        {
            m_allowZeroInDate = true;
        }

        public override SqlLiteralFormatter CreateLiteralFormatter()
        {
            return new MySqlLiteralFormatter(m_dialect);
        }

        protected override void ApplyTypeRestrictions(BedValueHolder holder, DbTypeBase type, ILogger logger)
        {
            var htype = holder.GetFieldType();
            var dtype = type as DbTypeDatetime;
            if (dtype != null && type.GetSpecificAttribute("mysql", "subtype") == "timestamp" && htype.IsDateRelated())
            {
                var val = holder.GetDateTimeValue();
                val.MakeRestriction(m_mintimestamp, m_maxtimestamp);
                val.MakeValidDate();
                holder.SetDateTimeEx(val);
            }

            base.ApplyTypeRestrictions(holder, type, logger);
        }

        public override IBedReader AdaptReader(IDataReader reader)
        {
            return new MySqlDataReaderAdapter(reader, m_dialect);
        }

        public override string FilterNotDumpableCharacters(string value)
        {
            var sb = new StringBuilder();
            foreach (char ch in value)
            {
                var cat = Char.GetUnicodeCategory(ch);
                if (cat != System.Globalization.UnicodeCategory.OtherSymbol)
                {
                    sb.Append(ch);
                }
            }
            return sb.ToString();
        }
    }

    public class MySqlLiteralFormatter : SqlLiteralFormatter
    {
        public MySqlLiteralFormatter(ISqlDialect dialect)
            : base(dialect)
        {
        }

        public override void SetDateTime(DateTime value)
        {
            m_text = "'" + value.ToString("s") + "'";
        }

        public override void SetDateTimeEx(DateTimeEx value)
        {
            m_text = "'" + value.ToString("yyyy-MM-ddTHH:mm:ss.ffffff", DateTimeFormatInfo.InvariantInfo) + "'";
        }

        public override void SetDateEx(DateEx value)
        {
            m_text = "'" + value.ToStringNormalized() + "'";
        }

        public override void SetTimeEx(TimeEx value)
        {
            m_text = "'" + value.ToString("HH:mm:ss.ffffff", DateTimeFormatInfo.InvariantInfo) + "'";
        }
    }

    public class MySqlDataReaderAdapter : DataReaderAdapter
    {
        MySqlDataReader m_myReader;
        public MySqlDataReaderAdapter(IDataReader reader, ISqlDialect dialect)
            : base(reader, dialect)
        {
            m_myReader = reader as MySqlDataReader;
        }

        public override void ReadValue(int i)
        {
            if (m_myReader != null)
            {
                var type = m_myReader.GetFieldType(i);
                if (type == typeof(MySqlDateTime))
                {
                    var val = m_myReader.GetValue(i);
                    if (val == DBNull.Value || val == null)
                    {
                        SetNull();
                        return;
                    }
                    var dt = (MySqlDateTime)val;
                    DateTimeEx dtex = new DateTimeEx();
                    dtex.Year = dt.Year;
                    dtex.Month = dt.Month;
                    dtex.Day = dt.Day;
                    dtex.Hour = dt.Hour;
                    dtex.Minute = dt.Minute;
                    dtex.Second = dt.Second;
                    dtex._Millisecond = dt.Millisecond;
                    SetDateTimeEx(dtex);
                    return;
                }
            }
            base.ReadValue(i);
        }
    }
}
