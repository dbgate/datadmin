using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data;
using DatAdmin;

namespace Plugin.mssql
{
    // based of MSSQL 2008 datatypes, see http://msdn.microsoft.com/en-us/library/ms187752.aspx
    public enum SqlTypeCode
    {
        BigInt,
        Binary,
        Bit,
        Char,
        Date, // 2008
        DateTime,
        DateTime2, // 2008
        DateTimeOffset, // 2008
        Decimal,
        Float,
        Image,
        Int,
        Money,
        NChar,
        NText,
        NVarChar,
        Real,
        UniqueIdentifier,
        SmallDateTime,
        SmallInt,
        SmallMoney,
        Text,
        Time, // 2008
        Timestamp,
        TinyInt,
        VarBinary,
        VarChar,
        Variant,
        Xml, // 2005
        Numeric,

        Generic,
        //Udt = 29,
        //Structured = 30,
        //Date = 31,
        //Time = 32,
        //DateTime2 = 33,
        //DateTimeOffset = 34,
    }

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public abstract class SqlTypeBase : PropertyPageBase, ISpecificType
    {
        public static SqlTypeBase CreateType(SqlTypeCode code)
        {
            switch (code)
            {
                case SqlTypeCode.BigInt: return new SqlTypeBigInt();
                case SqlTypeCode.Binary: return new SqlTypeBinary();
                case SqlTypeCode.Bit: return new SqlTypeBit();
                case SqlTypeCode.Date: return new SqlTypeDate();
                case SqlTypeCode.DateTime: return new SqlTypeDatetime();
                case SqlTypeCode.DateTime2: return new SqlTypeDatetime2();
                case SqlTypeCode.DateTimeOffset: return new SqlTypeDatetimeOffset();
                case SqlTypeCode.Decimal: return new SqlTypeDecimal();
                case SqlTypeCode.Float: return new SqlTypeFloat();
                case SqlTypeCode.Char: return new SqlTypeChar();
                case SqlTypeCode.Image: return new SqlTypeImage();
                case SqlTypeCode.Int: return new SqlTypeInt();
                case SqlTypeCode.Money: return new SqlTypeMoney();
                case SqlTypeCode.NChar: return new SqlTypeNChar();
                case SqlTypeCode.NText: return new SqlTypeNText();
                case SqlTypeCode.Numeric: return new SqlTypeNumeric();
                case SqlTypeCode.NVarChar: return new SqlTypeNVarChar();
                case SqlTypeCode.Real: return new SqlTypeReal();
                case SqlTypeCode.SmallDateTime: return new SqlTypeSmallDatetime();
                case SqlTypeCode.SmallInt: return new SqlTypeSmallInt();
                case SqlTypeCode.SmallMoney: return new SqlTypeSmallMoney();
                case SqlTypeCode.Text: return new SqlTypeText();
                case SqlTypeCode.Time: return new SqlTypeTime();
                case SqlTypeCode.Timestamp: return new SqlTypeTimestamp();
                case SqlTypeCode.TinyInt: return new SqlTypeTinyInt();
                case SqlTypeCode.UniqueIdentifier: return new SqlTypeUniqueIdentifier();
                case SqlTypeCode.VarBinary: return new SqlTypeVarBinary();
                case SqlTypeCode.VarChar: return new SqlTypeVarChar();
                case SqlTypeCode.Variant: return new SqlTypeVariant();
                case SqlTypeCode.Xml: return new SqlTypeXml();
                case SqlTypeCode.Generic: return new SqlTypeGeneric();
            }
            throw new InternalError(String.Format("DAE-00334 Unknown sql type code: {0}", code));
        }

        #region ISpecificType Members

        public abstract DbTypeBase ToGenericType();
        [Browsable(false)]
        public abstract object Code { get;}

        #endregion
    }

    public interface IMsSqlAutoIncrement
    {
        bool IsIdentity { get; set; }
        int IdentityIncrement { get; set; }
        int IdentitySeed { get; set; }
    }

    public abstract class SqlTypeInteger : SqlTypeBase, IMsSqlAutoIncrement
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

        protected abstract int Bytes { get;}

        public override DbTypeBase ToGenericType()
        {
            DbTypeInt res = new DbTypeInt();
            res.Bytes = Bytes;
            res.Unsigned = IsUnsigned;
            if (IsIdentity)
            {
                res.Autoincrement = true;
                res.SetSpecificAttribute("mssql", "identity_increment", IdentityIncrement.ToString());
                res.SetSpecificAttribute("mssql", "identity_seed", IdentitySeed.ToString());
            }
            return res;
        }
        protected abstract string SqlName { get;}
        public override string ToString()
        {
            string res = SqlName;
            if (IsIdentity) res += String.Format(" IDENTITY({0}, {1})", Math.Max(IdentitySeed, 0), Math.Max(IdentityIncrement, 1));
            return res;
        }
        public virtual bool IsUnsigned
        {
            get { return false; }
        }
    }

    public class SqlTypeInt : SqlTypeInteger
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
            get { return SqlTypeCode.Int; }
        }
    }

    public class SqlTypeTinyInt : SqlTypeInteger
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
            get { return SqlTypeCode.TinyInt; }
        }
        public override bool IsUnsigned
        {
            get { return true; }
        }
    }

    public class SqlTypeSmallInt : SqlTypeInteger
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
            get { return SqlTypeCode.SmallInt; }
        }
    }

    public class SqlTypeBigInt : SqlTypeInteger
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
            get { return SqlTypeCode.BigInt; }
        }
    }

    public abstract class SqlTypeCharacter : SqlTypeBase
    {
        int m_length = 50;
        [DatAdmin.DisplayName("s_length")]
        public int Length
        {
            get { return m_length; }
            set { m_length = value; }
        }
        protected abstract bool IsUnicode { get;}
        protected abstract bool IsVarLength { get;}
        protected abstract bool IsBinary { get;}
        public override DbTypeBase ToGenericType()
        {
            DbTypeString res = new DbTypeString();
            res.IsBinary = IsBinary;
            res.IsUnicode = IsUnicode;
            res.IsVarLength = IsVarLength;
            res.Length = Length;
            return res;
        }
    }

    public class SqlTypeChar : SqlTypeCharacter
    {
        protected override bool IsUnicode
        {
            get { return false; }
        }
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
            get { return SqlTypeCode.Char; }
        }
        public override string ToString()
        {
            if (Length >= 0) return String.Format("char({0})", Length);
            return "char(max)";
        }
    }

    public class SqlTypeVarChar : SqlTypeCharacter
    {
        protected override bool IsUnicode
        {
            get { return false; }
        }
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
            get { return SqlTypeCode.VarChar; }
        }
        public override string ToString()
        {
            if (Length >= 0) return String.Format("varchar({0})", Length);
            return "varchar(max)";
        }
    }

    public class SqlTypeNChar : SqlTypeCharacter
    {
        protected override bool IsUnicode
        {
            get { return true; }
        }
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
            get { return SqlTypeCode.NChar; }
        }
        public override string ToString()
        {
            if (Length >= 0) return String.Format("nchar({0})", Length);
            return "nchar(max)";
        }
    }

    public class SqlTypeNVarChar : SqlTypeCharacter
    {
        protected override bool IsUnicode
        {
            get { return true; }
        }
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
            get { return SqlTypeCode.NVarChar; }
        }
        public override string ToString()
        {
            if (Length >= 0) return String.Format("nvarchar({0})", Length);
            return "nvarchar(max)";
        }
    }

    public class SqlTypeBit : SqlTypeBase
    {
        public override DbTypeBase ToGenericType()
        {
            return new DbTypeLogical();
        }
        public override string ToString()
        {
            return "bit";
        }
        public override object Code
        {
            get { return SqlTypeCode.Bit; }
        }
    }

    public class SqlTypeBinary : SqlTypeCharacter
    {
        protected override bool IsUnicode
        {
            get { return false; }
        }
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
            get { return SqlTypeCode.Binary; }
        }
        public override string ToString()
        {
            if (Length >= 0) return String.Format("binary({0})", Length);
            return "binary(max)";
        }
    }

    public class SqlTypeVarBinary : SqlTypeCharacter
    {
        protected override bool IsUnicode
        {
            get { return false; }
        }
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
            get { return SqlTypeCode.VarBinary; }
        }
        public override string ToString()
        {
            if (Length >= 0) return String.Format("varbinary({0})", Length);
            return "varbinary(max)";
        }
    }

    public class SqlTypeDatetime : SqlTypeBase
    {
        public override DbTypeBase ToGenericType()
        {
            DbTypeDatetime res = new DbTypeDatetime();
            res.SetSpecificAttribute("mssql", "subtype", "datetime");
            return res;
        }
        public override string ToString()
        {
            return "datetime";
        }
        public override object Code
        {
            get { return SqlTypeCode.DateTime; }
        }
    }

    public class SqlTypeDatetime2 : SqlTypeBase
    {
        public override DbTypeBase ToGenericType()
        {
            DbTypeDatetime res = new DbTypeDatetime();
            res.SetSpecificAttribute("mssql", "subtype", "datetime2");
            return res;
        }
        public override string ToString()
        {
            return "datetime2";
        }
        public override object Code
        {
            get { return SqlTypeCode.DateTime2; }
        }
    }

    public class SqlTypeDate : SqlTypeBase
    {
        public override DbTypeBase ToGenericType()
        {
            DbTypeDatetime res = new DbTypeDatetime();
            res.SubType = DbDatetimeSubType.Date;
            return res;
        }
        public override string ToString()
        {
            return "date";
        }
        public override object Code
        {
            get { return SqlTypeCode.Date; }
        }
    }

    public class SqlTypeTime : SqlTypeBase
    {
        public override DbTypeBase ToGenericType()
        {
            DbTypeDatetime res = new DbTypeDatetime();
            res.SubType = DbDatetimeSubType.Time;
            return res;
        }
        public override string ToString()
        {
            return "time";
        }
        public override object Code
        {
            get { return SqlTypeCode.Time; }
        }
    }

    public class SqlTypeDatetimeOffset : SqlTypeBase
    {
        public override DbTypeBase ToGenericType()
        {
            DbTypeDatetime res = new DbTypeDatetime();
            res.HasTimeZone = true;
            return res;
        }
        public override string ToString()
        {
            return "datetimeoffset";
        }
        public override object Code
        {
            get { return SqlTypeCode.DateTimeOffset; }
        }
    }

    public class SqlTypeSmallDatetime : SqlTypeBase
    {
        public override DbTypeBase ToGenericType()
        {
            DbTypeDatetime res = new DbTypeDatetime();
            res.SetSpecificAttribute("mssql", "subtype", "smalldatetime");
            return res;
        }
        public override string ToString()
        {
            return "smalldatetime";
        }
        public override object Code
        {
            get { return SqlTypeCode.SmallDateTime; }
        }
    }

    public class SqlTypeText : SqlTypeBase
    {
        public override DbTypeBase ToGenericType()
        {
            DbTypeText res = new DbTypeText();
            res.IsUnicode = false;
            return res;
        }
        public override string ToString()
        {
            return "text";
        }
        public override object Code
        {
            get { return SqlTypeCode.Text; }
        }
    }

    public class SqlTypeNText : SqlTypeBase
    {
        public override DbTypeBase ToGenericType()
        {
            DbTypeText res = new DbTypeText();
            res.IsUnicode = true;
            return res;
        }
        public override string ToString()
        {
            return "ntext";
        }
        public override object Code
        {
            get { return SqlTypeCode.NText; }
        }
    }

    public abstract class SqlTypeNumericBase : SqlTypeBase, IMsSqlAutoIncrement
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

        protected abstract string SqlName { get;}

        public override DbTypeBase ToGenericType()
        {
            DbTypeNumeric res = new DbTypeNumeric();
            res.Precision = Precision;
            res.Scale = Scale;
            res.SetSpecificAttribute("mssql", "subtype", SqlName);
            if (IsIdentity)
            {
                res.Autoincrement = true;
                res.SetSpecificAttribute("mssql", "identity_increment", IdentityIncrement.ToString());
                res.SetSpecificAttribute("mssql", "identity_seed", IdentitySeed.ToString());
            }
            return res;
        }

        public override string ToString()
        {
            string res = String.Format("{0}({1},{2})", SqlName, Precision, Scale);
            if (IsIdentity) res += String.Format(" IDENTITY({0}, {1})", Math.Max(IdentitySeed, 0), Math.Max(IdentityIncrement, 1));
            return res;
        }
    }

    public class SqlTypeNumeric : SqlTypeNumericBase
    {
        protected override string SqlName
        {
            get { return "numeric"; }
        }
        public override object Code
        {
            get { return SqlTypeCode.Numeric; }
        }
    }

    public class SqlTypeDecimal : SqlTypeNumericBase
    {
        protected override string SqlName
        {
            get { return "decimal"; }
        }
        public override object Code
        {
            get { return SqlTypeCode.Decimal; }
        }
    }

    public class SqlTypeTimestamp : SqlTypeBase
    {
        public override DbTypeBase ToGenericType()
        {
            DbTypeDatetime res = new DbTypeDatetime();
            res.SetSpecificAttribute("mssql", "subtype", "timestamp");
            return res;
        }
        public override string ToString()
        {
            return "timestamp";
        }
        public override object Code
        {
            get { return SqlTypeCode.Timestamp; }
        }
    }

    public class SqlTypeImage : SqlTypeBase
    {
        public override DbTypeBase ToGenericType()
        {
            return new DbTypeBlob();
        }
        public override string ToString()
        {
            return "image";
        }
        public override object Code
        {
            get { return SqlTypeCode.Image; }
        }
    }

    public class SqlTypeFloat : SqlTypeBase
    {
        public override DbTypeBase ToGenericType()
        {
             DbTypeFloat res = new DbTypeFloat();
             res.Bytes = 8;
             return res;
        }
        public override string ToString()
        {
            return "float";
        }
        public override object Code
        {
            get { return SqlTypeCode.Float; }
        }
    }

    public class SqlTypeReal : SqlTypeBase
    {
        public override DbTypeBase ToGenericType()
        {
            DbTypeFloat res = new DbTypeFloat();
            res.Bytes = 4;
            return res;
        }
        public override string ToString()
        {
            return "real";
        }
        public override object Code
        {
            get { return SqlTypeCode.Real; }
        }
    }

    public class SqlTypeMoney : SqlTypeBase
    {
        public override DbTypeBase ToGenericType()
        {
            DbTypeFloat res = new DbTypeFloat();
            res.Bytes = 10;
            res.IsMoney = true;
            return res;
        }
        public override string ToString()
        {
            return "money";
        }
        public override object Code
        {
            get { return SqlTypeCode.Money; }
        }
    }

    public class SqlTypeSmallMoney : SqlTypeBase
    {
        public override DbTypeBase ToGenericType()
        {
            DbTypeFloat res = new DbTypeFloat();
            res.Bytes = 8;
            res.IsMoney = true;
            return res;
        }
        public override string ToString()
        {
            return "smallmoney";
        }
        public override object Code
        {
            get { return SqlTypeCode.SmallMoney; }
        }
    }

    public class SqlTypeVariant : SqlTypeBase
    {
        public override DbTypeBase ToGenericType()
        {
            DbTypeBlob res = new DbTypeBlob();
            res.SetSpecificAttribute("mssql", "subtype", "variant");
            return res;
        }
        public override string ToString()
        {
            return "sql_variant";
        }
        public override object Code
        {
            get { return SqlTypeCode.Variant; }
        }
    }

    public class SqlTypeUniqueIdentifier : SqlTypeBase
    {
        public override DbTypeBase ToGenericType()
        {
            DbTypeString res = new DbTypeString(50);
            res.IsVarLength = true;
            res.IsUnicode = false;
            res.SetSpecificAttribute("mssql", "subtype", "uniqueidentifier");
            return res;
        }
        public override string ToString()
        {
            return "uniqueidentifier";
        }
        public override object Code
        {
            get { return SqlTypeCode.UniqueIdentifier; }
        }
    }

    public class SqlTypeXml : SqlTypeBase
    {
        public override DbTypeBase ToGenericType()
        {
            return new DbTypeXml();
        }
        public override string ToString()
        {
            return "xml";
        }
        public override object Code
        {
            get { return SqlTypeCode.Xml; }
        }
    }

    public class SqlTypeGeneric : SqlTypeBase
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
            get { return SqlTypeCode.Generic; }
        }
    }
}
