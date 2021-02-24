using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DatAdmin
{
    public class DialectDataAdapterBase : IDialectDataAdapter
    {
        protected bool m_allowZeroInDate = false;
        protected SqlLiteralFormatter m_literalFormatter;
        protected ISqlDialect m_dialect;
        BedValueHolder m_holder = new BedValueHolder();

        public static readonly DialectDataAdapterBase Instance = new DialectDataAdapterBase(GenericDialect.Instance);

        //string EscapeString(string value);
        //string QuoteString(string value);
        //string EscapeDateTime(DateTime value);
        //string EscapeBinary(byte[] value);
        //string EscapeNumber(object number);
        //string EscapeLogical(bool value);

        public DialectDataAdapterBase(ISqlDialect dialect)
        {
            m_dialect = dialect;
            m_literalFormatter = CreateLiteralFormatter();
        }

        public virtual SqlLiteralFormatter CreateLiteralFormatter()
        {
            return new SqlLiteralFormatter(m_dialect);
        }

        protected virtual void ApplyTypeRestrictions(BedValueHolder holder, DbTypeBase type, ILogger logger)
        {
            var stype = type as DbTypeString;
            var htype = holder.GetFieldType();
            if (stype != null && htype == TypeStorage.String)
            {
                string sval = holder.GetString();
                if (stype.Length > 0 && sval.Length > stype.Length)
                {
                    sval = sval.Substring(0, stype.Length);
                    holder.SetString(sval);
                }
            }
            if (htype.IsDateRelated() && !m_allowZeroInDate)
            {
                var dt = holder.GetDateTimeValue();
                dt.MakeValidDate();
                m_holder.SetDateTimeEx(dt);
            }
        }

        #region IDialectDataAdapter Members

        public void AdaptValue(IBedValueReader reader, DbTypeBase type, IBedValueWriter writer, IBedValueConvertor converter, ILogger logger)
        {
            if (reader.GetFieldType() == TypeStorage.Null)
            {
                m_holder.SetNull();
            }
            else
            {
                converter.ConvertValue(reader, type.DefaultStorage, m_holder);
                ApplyTypeRestrictions(m_holder, type, logger);
            }
            writer.ReadFrom(m_holder);
        }

        public virtual IBedReader AdaptReader(IDataReader reader)
        {
            return new DataReaderAdapter(reader, m_dialect);
        }

        public virtual string GetFulltextSearchExpr(string expr, string substring, FulltextSearchParams pars)
        {
            substring = substring.Replace("@", "@@").Replace("%", "@%").Replace("_", "@_");
            return String.Format("({0} LIKE {1} ESCAPE '@')", expr, this.GetSqlLiteral(pars.LikePrefix + substring + pars.LikePostfix));
        }

        public string GetSqlLiteral(IBedValueReader reader, DbTypeBase type)
        {
            m_literalFormatter.TargetType = type;
            m_literalFormatter.ReadFrom(reader);
            return m_literalFormatter.GetText();
        }

        public string GetSqlLiteral(object value, DbTypeBase type)
        {
            m_holder.ReadFrom(value);
            return GetSqlLiteral(m_holder, type);
        }

        public virtual string FilterNotDumpableCharacters(string value)
        {
            return value;
        }

        #endregion
    }
}
