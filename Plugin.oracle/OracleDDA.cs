using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Data.OracleClient;
using System.Data;

namespace Plugin.oracle
{
    public class OracleDDA : DialectDataAdapterBase
    {
        public OracleDDA(ISqlDialect dialect)
            : base(dialect)
        {
        }

        public override SqlLiteralFormatter CreateLiteralFormatter()
        {
            return new OracleLiteralFormatter(m_dialect);
        }

        public override IBedReader AdaptReader(System.Data.IDataReader reader)
        {
            return new OracleDataReaderAdapter(reader, m_dialect);
        }
    }

    public class OracleLiteralFormatter : SqlLiteralFormatter
    {
        static Regex m_dayRegex = new Regex(@"^(\d*\.)?(\d\d?\:\d\d?\:\d\d?)(\.\d*)?$");

        public OracleLiteralFormatter(ISqlDialect dialect)
            : base(dialect)
        {
        }

        public override void SetString(string value)
        {
            if (TargetType != null)
            {
                var spec = m_dialect.GenericTypeToSpecific(TargetType);
                if (spec is OracleTypeIntervalDayToSecond)
                {
                    var m1 = m_dayRegex.Match(value);
                    if (m1.Success)
                    {
                        if (m1.Groups[1].Success)
                        {
                            m_text = "INTERVAL '";
                            m_text += m1.Groups[1].Value.Slice(0, -1);
                            m_text += " ";
                            m_text += m1.Groups[2].Value;
                            if (m1.Groups[3].Success) m_text += m1.Groups[3].Value;
                            m_text += "' DAY TO SECOND";
                        }
                        else
                        {
                            m_text = "INTERVAL '";
                            m_text += m1.Groups[2].Value;
                            if (m1.Groups[3].Success) m_text += m1.Groups[3].Value;
                            m_text += "' HOUR TO SECOND";
                        }
                        return;
                    }
                    int days;
                    if (Int32.TryParse(value, out days))
                    {
                        m_text = "INTERVAL '" + days.ToString() + "' DAY";
                        return;
                    }
                }
                if (spec is OracleTypeIntervalYearToMonth)
                {
                    int months;
                    if (Int32.TryParse(value, out months))
                    {
                        m_text = "INTERVAL '" + months.ToString() + "' MONTH";
                        return;
                    }
                }
            }
            base.SetString(value);
        }

        public override void SetInt32(int value)
        {
            if (TargetType != null)
            {
                var spec = m_dialect.GenericTypeToSpecific(TargetType);
                if (spec is OracleTypeIntervalYearToMonth)
                {
                    m_text = "INTERVAL '" + value.ToString() + "' MONTH";
                    return;
                }
            }
            base.SetInt32(value);
        }

        public override void SetDateTime(DateTime value)
        {
            m_text = "timestamp'" + value.ToString("yyyy-MM-dd HH:mm:ss.fff", DateTimeFormatInfo.InvariantInfo) + "'";
        }

        public override void SetDateTimeEx(DateTimeEx value)
        {
            m_text = "timestamp'" + value.ToString("yyyy-MM-dd HH:mm:ss.fff", DateTimeFormatInfo.InvariantInfo) + "'";
        }

        public override void SetDateEx(DateEx value)
        {
            m_text = "date'" + value.ToString("yyyy-MM-dd", DateTimeFormatInfo.InvariantInfo) + "'";
        }

        public override void SetTimeEx(TimeEx value)
        {
            m_text = "timestamp'" + value.ToString("0001-01-01 HH:mm:ss.fff", DateTimeFormatInfo.InvariantInfo) + "'";
        }
    }

    public class OracleDataReaderAdapter : DataReaderAdapter
    {
        OracleDataReader m_myReader;
        public OracleDataReaderAdapter(IDataReader reader, ISqlDialect dialect)
            : base(reader, dialect)
        {
            m_myReader = reader as OracleDataReader;
        }
    }
}
