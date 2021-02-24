using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using DatAdmin;

namespace Plugin.oracle
{
    // ORACLE data types from 
    // http://ss64.com/ora/syntax-datatypes.html
    // http://download.oracle.com/docs/cd/B28359_01/server.111/b28318/datatype.htm
    public enum OracleTypeCode
    {
        VarChar2,
        NVarChar2,
        Char,
        NChar,

        Integer,

        Number,
        BinaryFloat,
        BinaryDouble,

        Long,

        Date,
        Timestamp,
        IntervalYearToMonth,
        IntervalDayToSecond,

        Raw,
        LongRaw,

        RowId,
        URowId,

        MlsLabel,
        Clob,
        NClob,
        Blob,
        BFile,
        XMLType,

        Generic
    }

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public abstract class OracleTypeBase : PropertyPageBase, ISpecificType
    {
        protected const int DEFMAXLEN = 500;

        public static OracleTypeBase CreateType(OracleTypeCode code)
        {
            switch (code)
            {
                case OracleTypeCode.VarChar2:
                    return new OracleTypeVarChar2();
                case OracleTypeCode.NVarChar2:
                    return new OracleTypeNVarChar2();
                case OracleTypeCode.Char:
                    return new OracleTypeChar();
                case OracleTypeCode.NChar:
                    return new OracleTypeNChar();

                case OracleTypeCode.Integer:
                    return new OracleTypeInteger();
                case OracleTypeCode.Number:
                    return new OracleTypeNumber();
                case OracleTypeCode.BinaryDouble:
                    return new OracleTypeBinaryDouble();
                case OracleTypeCode.BinaryFloat:
                    return new OracleTypeBinaryFloat();

                case OracleTypeCode.Long:
                    return new OracleTypeLong();

                case OracleTypeCode.Date:
                    return new OracleTypeDate();
                case OracleTypeCode.Timestamp:
                    return new OracleTypeTimestamp();
                case OracleTypeCode.IntervalYearToMonth:
                    return new OracleTypeIntervalYearToMonth();
                case OracleTypeCode.IntervalDayToSecond:
                    return new OracleTypeIntervalDayToSecond();

                case OracleTypeCode.Raw:
                    return new OracleTypeRaw();
                case OracleTypeCode.LongRaw:
                    return new OracleTypeLongRaw();

                case OracleTypeCode.RowId:
                    return new OracleTypeRowId();
                case OracleTypeCode.URowId:
                    return new OracleTypeURowId();

                case OracleTypeCode.MlsLabel:
                    return new OracleTypeMlsLabel();
                case OracleTypeCode.Clob:
                    return new OracleTypeClob();
                case OracleTypeCode.NClob:
                    return new OracleTypeNClob();
                case OracleTypeCode.Blob:
                    return new OracleTypeBlob();
                case OracleTypeCode.BFile:
                    return new OracleTypeBFile();
                case OracleTypeCode.XMLType:
                    return new OracleTypeXml();

                case OracleTypeCode.Generic:
                    return new OracleTypeGeneric();
            }
            throw new Exception(String.Format("DAE-00345 Unknown Oracle type code: {0}", code));
        }

        #region ISpecificType Members

        public abstract DbTypeBase ToGenericType();
        [Browsable(false)]
        public abstract object Code { get; }

        #endregion

        protected static string FormatPrecision(int? precision)
        {
            if (precision == null) return "";
            return String.Format("({0})", precision);
        }
    }

    /// <summary>
    /// DatAdmin alias for Oracle type NUMBER(*,0) (also recognized by Oracle)
    /// </summary>
    public class OracleTypeInteger : OracleTypeBase
    {
        public override DbTypeBase ToGenericType()
        {
            DbTypeInt res = new DbTypeInt();
            res.Bytes = 4;
            res.Unsigned = false;
            return res;
        }
        public override string ToString()
        {
            return "integer";
        }
        public override object Code
        {
            get { return OracleTypeCode.Integer; }
        }
    }

    public class OracleTypeNumber : OracleTypeBase
    {
        int? m_precision = null;

        public int? Precision
        {
            get { return m_precision; }
            set { m_precision = value; }
        }
        int? m_scale = null;

        public int? Scale
        {
            get { return m_scale; }
            set { m_scale = value; }
        }

        public override string ToString()
        {
            if (Precision == null && Scale == null) return "NUMBER";
            return String.Format("NUMBER({0},{1})", Precision == null ? "*" : Precision.ToString(), Scale ?? 0);
        }

        public override object Code
        {
            get { return OracleTypeCode.Number; }
        }

        public override DbTypeBase ToGenericType()
        {
            if (Precision == null && Scale == null)
            {
                var fres = new DbTypeFloat { Bytes = 8 };
                fres.SetSpecificAttribute("oracle", "subtype", "number");
                return fres;
            }
            if (Precision != null && Scale != null)
            {
                var nres = new DbTypeNumeric();
                nres.Precision = Precision.Value;
                nres.Scale = Scale.Value;
                return nres;
            }
            if ((Scale ?? 0) == 0)
            {
                var ires = new DbTypeInt { Bytes = 4 };
                if (Precision != null)
                {
                    ires.SetSpecificAttribute("oracle", "length", Precision.ToString());
                }
                return ires;
            }
            if (Precision == null)
            {
                var nres = new DbTypeNumeric();
                nres.Scale = Scale.Value;
                nres.SetSpecificAttribute("oracle", "noprec", "1");
                return nres;
            }
            // this should never happen
            return new DbTypeNumeric();
        }
    }

    public class OracleTypeBinaryFloat : OracleTypeBase
    {
        public override object Code
        {
            get { return OracleTypeCode.BinaryFloat; }
        }

        public override string ToString()
        {
            return "BINARY_FLOAT";
        }

        public override DbTypeBase ToGenericType()
        {
            return new DbTypeFloat { Bytes = 4 };
        }
    }

    public class OracleTypeBinaryDouble : OracleTypeBase
    {
        public override object Code
        {
            get { return OracleTypeCode.BinaryDouble; }
        }

        public override string ToString()
        {
            return "BINARY_DOUBLE";
        }

        public override DbTypeBase ToGenericType()
        {
            return new DbTypeFloat { Bytes = 8 };
        }
    }

    public abstract class OracleTypeCharacter : OracleTypeBase
    {
        int m_length = 50;
        [DatAdmin.DisplayName("s_length")]
        public int Length
        {
            get { return m_length; }
            set { m_length = value; }
        }
        protected abstract bool IsUnicode { get; }
        protected abstract bool IsVarLength { get; }

        public override DbTypeBase ToGenericType()
        {
            DbTypeString res = new DbTypeString();
            res.IsUnicode = IsUnicode;
            res.IsVarLength = IsVarLength;
            res.Length = Length;
            return res;
        }

        protected abstract string SqlName { get; }

        public override string ToString()
        {
            return String.Format("{0}({1})", SqlName, Length < 0 ? DEFMAXLEN : Length);
        }
    }

    public class OracleTypeChar : OracleTypeCharacter
    {
        protected override bool IsUnicode
        {
            get { return false; }
        }
        protected override bool IsVarLength
        {
            get { return false; }
        }
        public override object Code
        {
            get { return OracleTypeCode.Char; }
        }
        protected override string SqlName
        {
            get { return "CHAR"; }
        }
    }

    public class OracleTypeVarChar2 : OracleTypeCharacter
    {
        protected override bool IsUnicode
        {
            get { return false; }
        }
        protected override bool IsVarLength
        {
            get { return true; }
        }
        public override object Code
        {
            get { return OracleTypeCode.VarChar2; }
        }
        protected override string SqlName
        {
            get { return "VARCHAR2"; }
        }
    }

    public class OracleTypeNChar : OracleTypeCharacter
    {
        protected override bool IsUnicode
        {
            get { return true; }
        }
        protected override bool IsVarLength
        {
            get { return false; }
        }
        public override object Code
        {
            get { return OracleTypeCode.NChar; }
        }
        protected override string SqlName
        {
            get { return "NCHAR"; }
        }
    }

    public class OracleTypeNVarChar2 : OracleTypeCharacter
    {
        protected override bool IsUnicode
        {
            get { return true; }
        }
        protected override bool IsVarLength
        {
            get { return true; }
        }
        public override object Code
        {
            get { return OracleTypeCode.NVarChar2; }
        }
        protected override string SqlName
        {
            get { return "NVARCHAR2"; }
        }
    }

    public class OracleTypeLong : OracleTypeBase
    {
        public override object Code
        {
            get { return OracleTypeCode.Long; }
        }

        public override string ToString()
        {
            return "LONG";
        }

        public override DbTypeBase ToGenericType()
        {
            var res = new DbTypeText();
            res.SetSpecificAttribute("oracle", "subtype", "long");
            return res;
        }
    }

    public class OracleTypeRowId : OracleTypeBase
    {
        public override object Code
        {
            get { return OracleTypeCode.RowId; }
        }

        public override string ToString()
        {
            return "ROWID";
        }

        public override DbTypeBase ToGenericType()
        {
            var res = new DbTypeString();
            res.Length = 256;
            res.IsVarLength = true;
            res.SetSpecificAttribute("oracle", "subtype", "rowid");
            return res;
        }
    }

    public class OracleTypeURowId : OracleTypeBase
    {
        public override object Code
        {
            get { return OracleTypeCode.URowId; }
        }

        public override string ToString()
        {
            return "UROWID";
        }

        public override DbTypeBase ToGenericType()
        {
            var res = new DbTypeString();
            res.Length = 256;
            res.IsVarLength = true;
            res.SetSpecificAttribute("oracle", "subtype", "urowid");
            return res;
        }
    }

    public class OracleTypeMlsLabel : OracleTypeBase
    {
        public override object Code
        {
            get { return OracleTypeCode.MlsLabel; }
        }

        public override string ToString()
        {
            return "MLSLABEL";
        }

        public override DbTypeBase ToGenericType()
        {
            var res = new DbTypeString();
            res.Length = 256;
            res.IsVarLength = true;
            res.SetSpecificAttribute("oracle", "subtype", "mlslabel");
            return res;
        }
    }

    public class OracleTypeClob : OracleTypeBase
    {
        public override object Code
        {
            get { return OracleTypeCode.Clob; }
        }

        public override string ToString()
        {
            return "CLOB";
        }

        public override DbTypeBase ToGenericType()
        {
            var res = new DbTypeText();
            return res;
        }
    }

    public class OracleTypeNClob : OracleTypeBase
    {
        public override object Code
        {
            get { return OracleTypeCode.NClob; }
        }

        public override string ToString()
        {
            return "NCLOB";
        }

        public override DbTypeBase ToGenericType()
        {
            var res = new DbTypeText();
            res.IsUnicode = true;
            return res;
        }
    }

    public class OracleTypeBlob : OracleTypeBase
    {
        public override object Code
        {
            get { return OracleTypeCode.Blob; }
        }

        public override string ToString()
        {
            return "BLOB";
        }

        public override DbTypeBase ToGenericType()
        {
            var res = new DbTypeBlob();
            return res;
        }
    }

    public class OracleTypeBFile : OracleTypeBase
    {
        public override object Code
        {
            get { return OracleTypeCode.BFile; }
        }

        public override string ToString()
        {
            return "BFILE";
        }

        public override DbTypeBase ToGenericType()
        {
            var res = new DbTypeString();
            res.Length = 256;
            res.IsVarLength = true;
            res.SetSpecificAttribute("oracle", "subtype", "bfile");
            return res;
        }
    }

    public class OracleTypeXml : OracleTypeBase
    {
        public override object Code
        {
            get { return OracleTypeCode.XMLType; }
        }

        public override string ToString()
        {
            return "XMLTYPE";
        }

        public override DbTypeBase ToGenericType()
        {
            var res = new DbTypeXml();
            return res;
        }
    }

    public class OracleTypeDate : OracleTypeBase
    {
        public override object Code
        {
            get { return OracleTypeCode.Date; }
        }

        public override string ToString()
        {
            return "DATE";
        }

        public override DbTypeBase ToGenericType()
        {
            var res = new DbTypeDatetime();
            res.SubType = DbDatetimeSubType.Datetime;
            return res;
        }
    }

    public enum TimeZoneType { None, Local, Explicit }

    public class OracleTypeTimestamp : OracleTypeBase
    {
        TimeZoneType m_timeZone;

        public TimeZoneType TimeZone
        {
            get { return m_timeZone; }
            set { m_timeZone = value; }
        }

        int? m_fractionalPrecision;

        public int? FractionalPrecision
        {
            get { return m_fractionalPrecision; }
            set { m_fractionalPrecision = value; }
        }


        public override object Code
        {
            get { return OracleTypeCode.Timestamp; }
        }

        public override string ToString()
        {
            switch (TimeZone)
            {
                case TimeZoneType.None: return String.Format("TIMESTAMP {0}", FormatPrecision(FractionalPrecision));
                case TimeZoneType.Local: return  String.Format("TIMESTAMP {0} WITH LOCAL TIME ZONE", FormatPrecision(FractionalPrecision));
                case TimeZoneType.Explicit: return String.Format("TIMESTAMP {0} WITH TIME ZONE", FormatPrecision(FractionalPrecision));
            }
            return String.Format("TIMESTAMP {0}", FormatPrecision(FractionalPrecision));
        }

        public override DbTypeBase ToGenericType()
        {
            var res = new DbTypeDatetime();
            res.SubType = DbDatetimeSubType.Datetime;
            res.HasTimeZone = TimeZone != TimeZoneType.None;
            res.SetSpecificAttribute("oracle", "subtype", "timestamp");
            res.SetSpecificAttribute("oracle", "timezone", TimeZone.ToString().ToLower());
            if (FractionalPrecision != null) res.SetSpecificAttribute("oracle", "fractionalprecision", FractionalPrecision.ToString());
            return res;
        }
    }

    public class OracleTypeLongRaw : OracleTypeBase
    {
        public override object Code
        {
            get { return OracleTypeCode.LongRaw; }
        }

        public override string ToString()
        {
            return "LONG RAW";
        }

        public override DbTypeBase ToGenericType()
        {
            var res = new DbTypeBlob();
            res.SetSpecificAttribute("oracle", "subtype", "longraw");
            return res;
        }
    }

    public class OracleTypeRaw : OracleTypeBase
    {
        int m_length = 50;
        [DatAdmin.DisplayName("s_length")]
        public int Length
        {
            get { return m_length; }
            set { m_length = value; }
        }

        public override object Code
        {
            get { return OracleTypeCode.Raw; }
        }

        public override string ToString()
        {
            return String.Format("RAW({0})", Length);
        }

        public override DbTypeBase ToGenericType()
        {
            var res = new DbTypeString();
            res.IsBinary = true;
            res.Length = Length;
            return res;
        }
    }

    public class OracleTypeIntervalYearToMonth : OracleTypeBase
    {
        int? m_yearPrecision = null;

        public int? YearPrecision
        {
            get { return m_yearPrecision; }
            set { m_yearPrecision = value; }
        }

        public override object Code
        {
            get { return OracleTypeCode.IntervalYearToMonth; }
        }

        public override string ToString()
        {
            return String.Format("INTERVAL YEAR {0} TO MONTH", FormatPrecision(YearPrecision));
        }

        public override DbTypeBase ToGenericType()
        {
            var res = new DbTypeDatetime();
            res.SubType = DbDatetimeSubType.Interval;
            res.SetSpecificAttribute("oracle", "subtype", "yeartomonth");
            if (YearPrecision != null) res.SetSpecificAttribute("oracle", "yearprecision", YearPrecision.ToString());
            return res;
        }
    }

    public class OracleTypeIntervalDayToSecond : OracleTypeBase
    {
        int? m_dayPrecision = null;

        public int? DayPrecision
        {
            get { return m_dayPrecision; }
            set { m_dayPrecision = value; }
        }

        int? m_fractionalPrecision;

        public int? FractionalPrecision
        {
            get { return m_fractionalPrecision; }
            set { m_fractionalPrecision = value; }
        }

        public override object Code
        {
            get { return OracleTypeCode.IntervalDayToSecond; }
        }

        public override string ToString()
        {
            return String.Format("INTERVAL DAY {0} TO SECOND {1}", FormatPrecision(DayPrecision), FormatPrecision(FractionalPrecision));
        }

        public override DbTypeBase ToGenericType()
        {
            var res = new DbTypeDatetime();
            res.SubType = DbDatetimeSubType.Interval;
            res.SetSpecificAttribute("oracle", "subtype", "daytosecond");
            if (DayPrecision != null) res.SetSpecificAttribute("oracle", "dayprecision", DayPrecision.ToString());
            if (FractionalPrecision != null) res.SetSpecificAttribute("oracle", "fractionalprecision", FractionalPrecision.ToString());
            return res;
        }
    }

    public class OracleTypeGeneric : OracleTypeBase
    {
        string m_sql;
        [DatAdmin.DisplayName("SQL")]
        [XmlAttrib("sql")]
        public string Sql
        {
            get { return m_sql; }
            set { m_sql = value; }
        }

        public override DbTypeBase ToGenericType()
        {
            return new DbTypeGeneric { Sql = Sql };
        }
        public override string ToString()
        {
            return Sql;
        }
        public override object Code
        {
            get { return OracleTypeCode.Generic; }
        }
    }
}
