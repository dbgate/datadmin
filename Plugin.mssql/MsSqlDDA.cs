using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.Globalization;

namespace Plugin.mssql
{
    public class MsSqlDDA : DialectDataAdapterBase
    {
        static DateTimeEx m_mindatetime2 = new DateTimeEx(1, 1, 1, 0, 0, 0);
        static DateTimeEx m_maxdatetime2 = new DateTimeEx(9999, 1, 31, 23, 59, 59);

        static DateTimeEx m_mindatetime = new DateTimeEx(1753, 1, 1, 0, 0, 0);
        static DateTimeEx m_maxdatetime = new DateTimeEx(9999, 1, 31, 23, 59, 59);

        static DateTimeEx m_minsmalldatetime = new DateTimeEx(1900, 1, 1, 0, 0, 0);
        static DateTimeEx m_maxsmalldatetime = new DateTimeEx(2079, 6, 6, 23, 59, 59);

        public override SqlLiteralFormatter CreateLiteralFormatter()
        {
            return new MsSqlLiteralFormater(m_dialect);
        }
        public MsSqlDDA(ISqlDialect dialect)
            : base(dialect)
        {
        }

        protected override void ApplyTypeRestrictions(BedValueHolder holder, DbTypeBase type, ILogger logger)
        {
            base.ApplyTypeRestrictions(holder, type, logger);

            var dtype = type as DbTypeDatetime;
            var htype = holder.GetFieldType();
            if (dtype != null && htype != TypeStorage.DateTime)
            {
                var val = holder.GetDateTimeValue();
                if (dtype.GetSpecificAttribute("mssql", "subtype") == "datetime2" && m_dialect.Version.Is_2008())
                {
                    val.MakeRestriction(m_mindatetime2, m_maxdatetime2);
                }
                else if (dtype.GetSpecificAttribute("mssql", "subtype") == "smalldatetime")
                {
                    val.MakeRestriction(m_minsmalldatetime, m_maxsmalldatetime);
                }
                else if (dtype.SubType == DbDatetimeSubType.Time && m_dialect.Version.Is_2008())
                {
                    holder.SetString(val.TimePart.ToStringNormalized());
                    return;
                }
                else
                {
                    val.MakeRestriction(m_mindatetime, m_maxdatetime);
                }
                holder.SetDateTime(val.AsDateTime);
            }
        }
    }

    public class MsSqlLiteralFormater : SqlLiteralFormatter
    {
        public MsSqlLiteralFormater(ISqlDialect dialect)
            : base(dialect)
        {
        }
        public override void SetString(string value)
        {
            base.SetString(value);
            bool unicode = false;
            foreach (var ch in value)
            {
                if (ch >= 128) unicode = true;
            }
            if (unicode) m_text = "N" + m_text;
        }
        public override void SetDateTimeEx(DateTimeEx value)
        {
            m_text = "'" + value.ToString("yyyy-MM-ddTHH:mm:ss.fff", CultureInfo.InvariantCulture) + "'";
        }
        public override void SetDateTime(DateTime value)
        {
            m_text = "'" + value.ToString("yyyy-MM-ddTHH:mm:ss.fff", CultureInfo.InvariantCulture) + "'";
        }
        public override void SetTimeEx(TimeEx value)
        {
            m_text = "'" + value.ToString("HH:mm:ss.fff", CultureInfo.InvariantCulture) + "'";
        }
    }
}
