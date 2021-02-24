using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    public sealed class BedValueConvertor : IBedValueConvertor
    {
        BedValueParser m_parser;
        BedValueFormatter m_formatter;
        BedValueHolder m_holder1 = new BedValueHolder();
        BedValueHolder m_holder2 = new BedValueHolder();
        IProgressInfo m_progress;

        public BedValueConvertor(DataFormatSettings settings)
        {
            m_parser = new BedValueParser(settings);
            m_formatter = new BedValueFormatter(settings);
        }

        #region IBedValueConvertor Members

        public IBedValueFormatter Formatter { get { return m_formatter; } }
        public IBedValueParser Parser { get { return m_parser; } }

        public IProgressInfo ProgressInfo
        {
            get { return m_progress; }
            set
            {
                m_progress = value;
                Formatter.ProgressInfo = value;
                Parser.ProgressInfo = value;
            }
        }

        public void ConvertValue(IBedValueReader reader, TypeStorage dsttype, IBedValueWriter writer)
        {
            var srctype = reader.GetFieldType();
            if (srctype == dsttype)
            {
                // no conversion needed
                writer.ReadFrom(reader);
                return;
            }
            if (srctype.IsNumber() && dsttype.IsNumber())
            {
                if (dsttype.IsInteger())
                {
                    writer.SetIntegerValue(dsttype, reader.GetIntegerValue());
                }
                else
                {
                    writer.SetRealValue(dsttype, reader.GetRealValue());
                }
                return;
            }
            if (srctype.IsDateRelated() && dsttype.IsDateRelated())
            {
                writer.SetDateTimeValue(dsttype, reader.GetDateTimeValue());
                return;
            }
            if (srctype == TypeStorage.Boolean && dsttype.IsNumber())
            {
                bool val = reader.GetBoolean();
                writer.SetIntegerValue(dsttype, val ? 1 : 0);
                return;
            }
            if (srctype.IsNumber() && dsttype == TypeStorage.Boolean)
            {
                long val = reader.GetIntegerValue();
                writer.SetBoolean(val != 0);
                return;
            }
            if (srctype == TypeStorage.String)
            {
                // parse
                m_parser.ParseValue(reader.GetString(), dsttype, writer);
                return;
            }
            if (dsttype == TypeStorage.String)
            {
                // format
                m_formatter.ReadFrom(reader);
                writer.SetString(m_formatter.GetText());
                return;
            }
            {
                // most generic case - format and than parse
                m_formatter.ReadFrom(reader);
                m_parser.ParseValue(m_formatter.GetText(), dsttype, writer);
                return;
            }
        }

        #endregion

        public object ConvertValue(TypeStorage type, object value)
        {
            m_holder1.ReadFrom(value);
            var srctype = m_holder1.GetFieldType();
            if (srctype == type || srctype == TypeStorage.Null) return value;
            ConvertValue(m_holder1, type, m_holder2);
            return m_holder2.BoxTypedValue();
        }
    }
}
