using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using DatAdmin;

namespace Plugin.effiproz
{
    // http://www.effiproz.com/sql_ref.aspx#datatypes
    public enum EfzTypeCode
    {
        Boolean,
        TinyInt,
        SmallInt,
        Int,
        BigInt,
        Double,
        Number,
        Char,
        VarChar,
        VarChar2,
        Binary,
        VarBinary,
        Clob,
        Blob,
        Date,
        Timestamp,
        TimestampTz,
        IntervalYearToMonth,
        IntervalDayToSecond,
        UniqueIdentifier,

        Generic,
    }

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public abstract class EfzTypeBase : PropertyPageBase, ISpecificType
    {
        public static EfzTypeBase CreateType(EfzTypeCode code)
        {
            switch (code)
            {
                case EfzTypeCode.BigInt:
                    return new EfzTypeBigInt();
                case EfzTypeCode.Binary:
                    return new EfzTypeBinary();
                case EfzTypeCode.Blob:
                    return new EfzTypeBlob();
                case EfzTypeCode.Boolean:
                    return new EfzTypeBoolean();
                case EfzTypeCode.Clob:
                    return new EfzTypeClob();
                case EfzTypeCode.Date:
                    return new EfzTypeDate();
                case EfzTypeCode.Double:
                    return new EfzTypeDouble();
                case EfzTypeCode.Char:
                    return new EfzTypeChar();
                case EfzTypeCode.Int:
                    return new EfzTypeInt();
                case EfzTypeCode.IntervalDayToSecond:
                    return new EfzTypeIntervalDayToSecond();
                case EfzTypeCode.IntervalYearToMonth:
                    return new EfzTypeIntervalYearToMonth();
                case EfzTypeCode.Number:
                    return new EfzTypeNumber();
                case EfzTypeCode.SmallInt:
                    return new EfzTypeSmallInt();
                case EfzTypeCode.Timestamp:
                    return new EfzTypeTimestamp();
                case EfzTypeCode.TimestampTz:
                    return new EfzTypeTimestampTz();
                case EfzTypeCode.TinyInt:
                    return new EfzTypeTinyInt();
                case EfzTypeCode.UniqueIdentifier:
                    return new EfzTypeUniqueIdentifier();
                case EfzTypeCode.VarBinary:
                    return new EfzTypeVarBinary();
                case EfzTypeCode.VarChar:
                    return new EfzTypeVarChar();
                case EfzTypeCode.VarChar2:
                    return new EfzTypeVarChar2();
                case EfzTypeCode.Generic:
                    return new EfzTypeGeneric();
            }
            throw new Exception(String.Format("DAE-00324 Unknown effiproz type code: {0}", code));
        }

        #region ISpecificType Members

        public abstract DbTypeBase ToGenericType();
        [Browsable(false)]
        public abstract object Code { get; }

        #endregion
    }

    public interface IEfzAutoIncrement
    {
        bool IsIdentity { get; set; }
        int IdentityIncrement { get; set; }
        int IdentitySeed { get; set; }
    }

    public class EfzTypeBoolean : EfzTypeBase
    {
        public override DbTypeBase ToGenericType()
        {
            return new DbTypeLogical();
        }
        public override string ToString()
        {
            return "boolean";
        }
        public override object Code
        {
            get { return EfzTypeCode.Boolean; }
        }
    }

    public abstract class EfzTypeInteger : EfzTypeBase, IEfzAutoIncrement
    {
        bool m_isIdentity;
        [DatAdmin.DisplayName("s_is_identity")]
        [TypeConverter(typeof(YesNoTypeConverter))]
        [IsIdentity]
        public bool IsIdentity
        {
            get { return m_isIdentity; }
            set { m_isIdentity = value; }
        }

        int m_identityIncrement = 1;
        [DatAdmin.DisplayName("s_identity_increment")]
        [IsIdentity]
        public int IdentityIncrement
        {
            get { return m_identityIncrement; }
            set { m_identityIncrement = value; }
        }

        int m_identitySeed = 1;
        [DatAdmin.DisplayName("s_identity_seed")]
        [IsIdentity]
        public int IdentitySeed
        {
            get { return m_identitySeed; }
            set { m_identitySeed = value; }
        }

        protected abstract int Bytes { get; }

        public override DbTypeBase ToGenericType()
        {
            DbTypeInt res = new DbTypeInt();
            res.Bytes = Bytes;
            res.Unsigned = false;
            if (IsIdentity)
            {
                res.Autoincrement = true;
                res.SetSpecificAttribute("effiproz", "identity_increment", IdentityIncrement.ToString());
                res.SetSpecificAttribute("effiproz", "identity_seed", IdentitySeed.ToString());
            }
            return res;
        }
        protected abstract string SqlName { get; }
        public override string ToString()
        {
            string res = SqlName;
            if (IsIdentity) res += String.Format(" GENERATED BY DEFAULT AS IDENTITY(START WITH {0} INCREMENT BY {1})", Math.Max(IdentitySeed, 0), Math.Max(IdentityIncrement, 1));
            return res;
        }
    }

    public class EfzTypeTinyInt : EfzTypeInteger
    {
        protected override int Bytes
        {
            get { return 1; }
        }
        protected override string SqlName
        {
            get { return "tinyint"; }
        }
        public override object Code
        {
            get { return EfzTypeCode.TinyInt; }
        }
    }

    public class EfzTypeSmallInt : EfzTypeInteger
    {
        protected override int Bytes
        {
            get { return 2; }
        }
        protected override string SqlName
        {
            get { return "smallint"; }
        }
        public override object Code
        {
            get { return EfzTypeCode.SmallInt; }
        }
    }

    public class EfzTypeInt : EfzTypeInteger
    {
        protected override int Bytes
        {
            get { return 4; }
        }
        protected override string SqlName
        {
            get { return "int"; }
        }
        public override object Code
        {
            get { return EfzTypeCode.Int; }
        }
    }

    public class EfzTypeBigInt : EfzTypeInteger
    {
        protected override int Bytes
        {
            get { return 8; }
        }
        protected override string SqlName
        {
            get { return "bigint"; }
        }
        public override object Code
        {
            get { return EfzTypeCode.BigInt; }
        }
    }

    public class EfzTypeDouble : EfzTypeBase
    {
        public override DbTypeBase ToGenericType()
        {
            DbTypeFloat res = new DbTypeFloat();
            res.Bytes = 8;
            return res;
        }
        public override string ToString()
        {
            return "double";
        }
        public override object Code
        {
            get { return EfzTypeCode.Double; }
        }
    }

    public class EfzTypeNumber : EfzTypeBase, IEfzAutoIncrement
    {
        int m_precision = 10;
        [DatAdmin.DisplayName("s_precision")]
        public int Precision
        {
            get { return m_precision; }
            set { m_precision = value; }
        }

        bool m_isIdentity;
        [DatAdmin.DisplayName("s_is_identity")]
        [TypeConverter(typeof(YesNoTypeConverter))]
        [IsIdentity]
        public bool IsIdentity
        {
            get { return m_isIdentity; }
            set { m_isIdentity = value; }
        }

        int m_identityIncrement = 1;
        [DatAdmin.DisplayName("s_identity_increment")]
        [IsIdentity]
        public int IdentityIncrement
        {
            get { return m_identityIncrement; }
            set { m_identityIncrement = value; }
        }

        int m_identitySeed = 1;
        [DatAdmin.DisplayName("s_identity_seed")]
        [IsIdentity]
        public int IdentitySeed
        {
            get { return m_identitySeed; }
            set { m_identitySeed = value; }
        }

        int m_scale;
        [DatAdmin.DisplayName("s_scale")]
        public int Scale
        {
            get { return m_scale; }
            set { m_scale = value; }
        }

        public override DbTypeBase ToGenericType()
        {
            DbTypeNumeric res = new DbTypeNumeric();
            res.Precision = Precision;
            res.Scale = Scale;
            if (IsIdentity)
            {
                res.Autoincrement = true;
                res.SetSpecificAttribute("effiproz", "identity_increment", IdentityIncrement.ToString());
                res.SetSpecificAttribute("effiproz", "identity_seed", IdentitySeed.ToString());
            }
            return res;
        }

        public override string ToString()
        {
            string res = String.Format("{0}({1},{2})", "number", Precision, Scale);
            if (IsIdentity) res += String.Format(" GENERATED BY DEFAULT AS IDENTITY(START WITH {0} INCREMENT BY {1})", Math.Max(IdentitySeed, 0), Math.Max(IdentityIncrement, 1));
            return res;
        }

        public override object Code
        {
            get { return EfzTypeCode.Number; }
        }
    }

    public abstract class EfzTypeCharacter : EfzTypeBase
    {
        int m_length = 50;
        [DatAdmin.DisplayName("s_length")]
        public int Length
        {
            get { return m_length; }
            set { m_length = value; }
        }
        protected virtual bool IsUnicode { get { return false; } }
        protected abstract bool IsVarLength { get; }
        protected abstract bool IsBinary { get; }
        public override DbTypeBase ToGenericType()
        {
            DbTypeString res = new DbTypeString();
            res.IsBinary = IsBinary;
            res.IsUnicode = IsUnicode;
            res.IsVarLength = IsVarLength;
            res.Length = Length;
            ChangeGenericType(res);
            return res;
        }
        protected virtual void ChangeGenericType(DbTypeString res) { }
    }

    public class EfzTypeChar : EfzTypeCharacter
    {
        protected override bool IsVarLength
        {
            get { return false; }
        }
        protected override bool IsBinary
        {
            get { return false; }
        }
        public override object Code
        {
            get { return EfzTypeCode.Char; }
        }
        public override string ToString()
        {
            if (Length >= 0) return String.Format("char({0})", Length);
            return "char(max)";
        }
    }

    public class EfzTypeVarChar : EfzTypeCharacter
    {
        protected override bool IsVarLength
        {
            get { return true; }
        }
        protected override bool IsBinary
        {
            get { return false; }
        }
        public override object Code
        {
            get { return EfzTypeCode.VarChar; }
        }
        public override string ToString()
        {
            if (Length >= 0) return String.Format("varchar({0})", Length);
            return "varchar(max)";
        }
    }

    public class EfzTypeVarChar2 : EfzTypeCharacter
    {
        protected override bool IsVarLength
        {
            get { return true; }
        }
        protected override bool IsBinary
        {
            get { return false; }
        }
        public override object Code
        {
            get { return EfzTypeCode.VarChar2; }
        }
        public override string ToString()
        {
            if (Length >= 0) return String.Format("varchar2({0})", Length);
            return "varchar2(max)";
        }
        protected override void ChangeGenericType(DbTypeString res)
        {
            res.SetSpecificAttribute("effiproz", "subtype", "varchar2");
        }
    }

    public class EfzTypeBinary : EfzTypeCharacter
    {
        protected override bool IsVarLength
        {
            get { return false; }
        }
        protected override bool IsBinary
        {
            get { return true; }
        }
        public override object Code
        {
            get { return EfzTypeCode.Binary; }
        }
        public override string ToString()
        {
            if (Length >= 0) return String.Format("binary({0})", Length);
            return "binary(max)";
        }
    }

    public class EfzTypeVarBinary : EfzTypeCharacter
    {
        protected override bool IsVarLength
        {
            get { return true; }
        }
        protected override bool IsBinary
        {
            get { return true; }
        }
        public override object Code
        {
            get { return EfzTypeCode.VarBinary; }
        }
        public override string ToString()
        {
            if (Length >= 0) return String.Format("varbinary({0})", Length);
            return "varbinary(max)";
        }
    }

    public abstract class EfzLobBase : EfzTypeBase
    {
        int m_maxBytes = 16 * 1024 * 1024;
        [DatAdmin.DisplayName("s_max_bytes")]
        public int MaxBytes
        {
            get { return m_maxBytes; }
            set { m_maxBytes = value; }
        }
    }

    public class EfzTypeClob : EfzLobBase
    {
        public override DbTypeBase ToGenericType()
        {
            DbTypeText res = new DbTypeText();
            res.IsUnicode = false;
            res.SetSpecificAttribute("effiproz", "maxbytes", MaxBytes.ToString());
            return res;
        }
        public override string ToString()
        {
            return String.Format("clob({0})", MaxBytes);
        }
        public override object Code
        {
            get { return EfzTypeCode.Clob; }
        }
    }

    public class EfzTypeBlob : EfzLobBase
    {
        public override DbTypeBase ToGenericType()
        {
            var res = new DbTypeBlob();
            res.SetSpecificAttribute("effiproz", "maxbytes", MaxBytes.ToString());
            return res;
        }
        public override string ToString()
        {
            return String.Format("blob({0})", MaxBytes);
        }
        public override object Code
        {
            get { return EfzTypeCode.Blob; }
        }
    }

    public class EfzTypeDate : EfzTypeBase
    {
        public override DbTypeBase ToGenericType()
        {
            DbTypeDatetime res = new DbTypeDatetime();
            res.SubType = DbDatetimeSubType.Datetime;
            res.SetSpecificAttribute("effiproz", "subtype", "date");
            return res;
        }
        public override string ToString()
        {
            return "date";
        }
        public override object Code
        {
            get { return EfzTypeCode.Date; }
        }
    }

    public abstract class EfzTimestampBase : EfzTypeBase
    {
        int m_precision = 6;
        [DatAdmin.DisplayName("s_precision")]
        public int Precision
        {
            get { return m_precision; }
            set { m_precision = value; }
        }

        [Browsable(false)]
        protected abstract bool HasTimeZeone { get; }
        [Browsable(false)]
        protected abstract string SubType { get; }

        public override DbTypeBase ToGenericType()
        {
            DbTypeDatetime res = new DbTypeDatetime();
            res.SubType = DbDatetimeSubType.Datetime;
            res.HasTimeZone = HasTimeZeone;
            res.SetSpecificAttribute("effiproz", "subtype", SubType);
            res.SetSpecificAttribute("effiproz", "precision", Precision.ToString());
            return res;
        }
    }

    public class EfzTypeTimestamp : EfzTimestampBase 
    {
        public override string ToString()
        {
            return String.Format("timestamp({0})", Precision);
        }
        public override object Code
        {
            get { return EfzTypeCode.Timestamp; }
        }
        protected override string SubType
        {
            get { return "timestamp"; }
        }
        protected override bool HasTimeZeone
        {
            get { return false; }
        }
    }

    public class EfzTypeTimestampTz : EfzTimestampBase 
    {
        public override string ToString()
        {
            return String.Format("timestamp({0}) with time zone", Precision);
        }
        public override object Code
        {
            get { return EfzTypeCode.TimestampTz; }
        }
        protected override string SubType
        {
            get { return "timestamptz"; }
        }
        protected override bool HasTimeZeone
        {
            get { return true; }
        }
    }

    public abstract class EfzIntervalBase : EfzTypeBase
    {
        //int m_precision = 3;
        //[DatAdmin.DisplayName("s_precision")]
        //public int Precision
        //{
        //    get { return m_precision; }
        //    set { m_precision = value; }
        //}

        public override DbTypeBase ToGenericType()
        {
            DbTypeDatetime res = new DbTypeDatetime();
            res.SubType = DbDatetimeSubType.Interval;
            res.SetSpecificAttribute("effiproz", "subtype", SubType);
            //res.SetSpecificAttribute("effiproz", "precision", Precision.ToString());
            return res;
        }

        protected abstract string SubType { get; }
    }

    public class EfzTypeIntervalYearToMonth : EfzIntervalBase
    {
        public override string ToString()
        {
            //return String.Format("interval year({0}) to month", Precision);
            return String.Format("interval year to month");
        }
        public override object Code
        {
            get { return EfzTypeCode.IntervalYearToMonth; }
        }
        protected override string SubType
        {
            get { return "yeartomonth"; }
        }
    }

    public class EfzTypeIntervalDayToSecond : EfzIntervalBase
    {
        //int m_fractionalSecondPrecision = 6;
        //[DatAdmin.DisplayName("s_fractional_second_precision")]
        //public int FractionalSecondPrecision
        //{
        //    get { return m_fractionalSecondPrecision; }
        //    set { m_fractionalSecondPrecision = value; }
        //}

        public override string ToString()
        {
            //return String.Format("interval day({0}) to second({1})", Precision, FractionalSecondPrecision);
            return String.Format("interval day to second");
        }
        public override object Code
        {
            get { return EfzTypeCode.IntervalDayToSecond; }
        }
        protected override string SubType
        {
            get { return "daytosecond"; }
        }
    }

    public class EfzTypeUniqueIdentifier : EfzTypeBase
    {
        public override DbTypeBase ToGenericType()
        {
            DbTypeString res = new DbTypeString(50);
            res.IsVarLength = true;
            res.IsUnicode = false;
            res.SetSpecificAttribute("effiproz", "subtype", "uniqueidentifier");
            return res;
        }
        public override string ToString()
        {
            return "uniqueidentifier";
        }
        public override object Code
        {
            get { return EfzTypeCode.UniqueIdentifier; }
        }
    }

    public class EfzTypeGeneric : EfzTypeBase
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
            get { return EfzTypeCode.Generic; }
        }
    }
}
