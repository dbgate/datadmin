using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using DatAdmin;

namespace Plugin.access
{
    // base on Access 2007 datatypes, see http://msdn.microsoft.com/en-us/library/bb208866.aspx
    public enum AccessTypeCode
    {
        Binary,
        Bit,
        TinyInt,
        Money,
        Datetime,
        UniqueIdentifier,
        Real,
        Float,
        SmallInt,
        Integer,
        Decimal,
        Text,
        Image,
        Character,

        Generic
    }

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public abstract class AccessTypeBase : PropertyPageBase, ISpecificType
    {
        protected const int DEFMAXLEN = 500;

        public static AccessTypeBase CreateType(AccessTypeCode code)
        {
            switch (code)
            {
                case AccessTypeCode.Binary: return new AccessTypeBinary();
                case AccessTypeCode.Bit: return new AccessTypeBit();
                case AccessTypeCode.TinyInt: return new AccessTypeTinyInt();
                case AccessTypeCode.Money: return new AccessTypeMoney();
                case AccessTypeCode.Datetime: return new AccessTypeDatetime();
                case AccessTypeCode.UniqueIdentifier: return new AccessTypeUniqueIdentifier();
                case AccessTypeCode.Real: return new AccessTypeReal();
                case AccessTypeCode.Float: return new AccessTypeFloat();
                case AccessTypeCode.SmallInt: return new AccessTypeSmallInt();
                case AccessTypeCode.Integer: return new AccessTypeInteger();
                case AccessTypeCode.Decimal: return new AccessTypeDecimal();
                case AccessTypeCode.Text: return new AccessTypeText();
                case AccessTypeCode.Image: return new AccessTypeImage();
                case AccessTypeCode.Character: return new AccessTypeCharacter();
                case AccessTypeCode.Generic: return new AccessTypeGeneric();
            }
            throw new Exception(String.Format("DAE-00318 Unknown access type code: {0}", code));
        }

        #region ISpecificType Members

        public abstract DbTypeBase ToGenericType();
        [Browsable(false)]
        public abstract object Code { get;}

        #endregion
    }

    public abstract class AccessTypeIntBase : AccessTypeBase
    {
        bool m_isAutoIncrement;

        [DatAdmin.DisplayName("s_is_auto_increment")]
        [TypeConverter(typeof(YesNoTypeConverter))]
        [IsIdentity]
        public bool IsAutoIncrement
        {
            get { return m_isAutoIncrement; }
            set { m_isAutoIncrement = value; }
        }

        protected abstract int Bytes { get;}

        public override DbTypeBase ToGenericType()
        {
            DbTypeInt res = new DbTypeInt();
            res.Bytes = Bytes;
            res.Autoincrement = IsAutoIncrement;
            return res;
        }
        protected abstract string SqlName { get;}
        public override string ToString()
        {
            string res = SqlName;
            if (IsAutoIncrement) res += " IDENTITY";
            return res;
        }
    }

    public class AccessTypeTinyInt : AccessTypeIntBase
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
            get { return AccessTypeCode.TinyInt; }
        }
    }

    public class AccessTypeSmallInt : AccessTypeIntBase
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
            get { return AccessTypeCode.SmallInt; }
        }
    }

    public class AccessTypeInteger : AccessTypeIntBase
    {
        protected override int Bytes
        {
            get { return 4; }
        }
        protected override string SqlName
        {
            get { return "integer"; }
        }
        public override object Code
        {
            get { return AccessTypeCode.Integer; }
        }
    }

    public class AccessTypeMoney : AccessTypeBase
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
            get { return AccessTypeCode.Money; }
        }
    }

    public class AccessTypeBinary : AccessTypeBase
    {
        int m_length = 510;
        [DatAdmin.DisplayName("s_length")]
        public int Length
        {
            get { return m_length; }
            set { m_length = value; }
        }

        public override DbTypeBase ToGenericType()
        {
            DbTypeString res = new DbTypeString();
            res.IsBinary = true;
            res.IsVarLength = true;
            return res;
        }
        public override string ToString()
        {
            return String.Format("binary({0})", Length);
        }
        public override object Code
        {
            get { return AccessTypeCode.Binary; }
        }
    }

    public class AccessTypeReal : AccessTypeBase
    {
        public override DbTypeBase ToGenericType()
        {
            DbTypeFloat res = new DbTypeFloat();
            res.Bytes=4;
            return res;
        }
        public override string ToString()
        {
            return "real";
        }
        public override object Code
        {
            get { return AccessTypeCode.Real; }
        }
    }

    public class AccessTypeFloat : AccessTypeBase
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
            get { return AccessTypeCode.Float; }
        }
    }

    public class AccessTypeBit : AccessTypeBase
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
            get { return AccessTypeCode.Bit; }
        }
    }

    public class AccessTypeDecimal : AccessTypeBase
    {
        bool m_isAutoIncrement;

        [DatAdmin.DisplayName("s_is_auto_increment")]
        [TypeConverter(typeof(YesNoTypeConverter))]
        [IsIdentity]
        public bool IsAutoIncrement
        {
            get { return m_isAutoIncrement; }
            set { m_isAutoIncrement = value; }
        }

        int m_precision;
        [DatAdmin.DisplayName("s_precision")]
        public int Precision
        {
            get { return m_precision; }
            set { m_precision = value; }
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
            res.Autoincrement = IsAutoIncrement;
            return res;
        }

        public override string ToString()
        {
            string res = String.Format("decimal({0},{1})", Precision, Scale);
            if (IsAutoIncrement) res += " IDENTITY";
            return res;
        }

        public override object Code
        {
            get { return AccessTypeCode.Decimal; }
        }
    }

    public class AccessTypeImage : AccessTypeBase
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
            get { return AccessTypeCode.Image; }
        }
    }

    public class AccessTypeText : AccessTypeBase
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
            get { return AccessTypeCode.Text; }
        }
    }

    public class AccessTypeDatetime : AccessTypeBase
    {
        public override DbTypeBase ToGenericType()
        {
            DbTypeDatetime res = new DbTypeDatetime();
            return res;
        }
        public override string ToString()
        {
            return "datetime";
        }
        public override object Code
        {
            get { return AccessTypeCode.Datetime; }
        }
    }

    public class AccessTypeUniqueIdentifier : AccessTypeBase
    {
        public override DbTypeBase ToGenericType()
        {
            DbTypeString res = new DbTypeString();
            res.IsVarLength = true;
            res.IsUnicode = false;
            res.SetSpecificAttribute("access", "subtype", "uniqueidentifier");
            return res;
        }
        public override string ToString()
        {
            return "uniqueidentifier";
        }
        public override object Code
        {
            get { return AccessTypeCode.UniqueIdentifier; }
        }
    }

    public class AccessTypeCharacter : AccessTypeBase
    {
        int m_length;
        [DatAdmin.DisplayName("s_length")]
        public int Length
        {
            get { return m_length; }
            set { m_length = value; }
        }
        public override DbTypeBase ToGenericType()
        {
            DbTypeString res = new DbTypeString();
            res.IsBinary = false;
            res.IsUnicode = true;
            res.IsVarLength = true;
            res.Length = Length;
            return res;
        }
        public override string ToString()
        {
            return String.Format("character({0})", Length < 0 ? DEFMAXLEN : Length);
        }
        public override object Code
        {
            get { return AccessTypeCode.Character; }
        }
    }

    public class AccessTypeGeneric : AccessTypeBase
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
            get { return AccessTypeCode.Generic; }
        }
    }
}
