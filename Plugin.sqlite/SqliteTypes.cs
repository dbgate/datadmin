using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using DatAdmin;

namespace Plugin.sqlite
{
    public enum SqliteTypeCode
    {
        Int,
        Real,
        Blob,
        Text,
        Numeric,
        Logical,
        DateTime,
    }

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public abstract class SqliteTypeBase : PropertyPageBase, ISpecificType
    {
        public static SqliteTypeBase CreateType(SqliteTypeCode code)
        {
            switch (code)
            {
                case SqliteTypeCode.Blob: return new SqliteTypeBlob();
                case SqliteTypeCode.Real: return new SqliteTypeReal();
                case SqliteTypeCode.Text: return new SqliteTypeText();
                case SqliteTypeCode.Int: return new SqliteTypeInt();
                case SqliteTypeCode.Numeric: return new SqliteTypeNumeric();
                case SqliteTypeCode.Logical: return new SqliteTypeLogical();
                case SqliteTypeCode.DateTime: return new SqliteTypeDateTime();
            }
            throw new InternalError(String.Format("DAE-00351 Unknown sql type code: {0}", code));
        }

        #region ISpecificType Members

        public abstract DbTypeBase ToGenericType();
        [Browsable(false)]
        public abstract object Code { get; }

        #endregion
    }

    public class SqliteTypeText : SqliteTypeBase
    {
        public string SpecificCode { get; set; }
        public override DbTypeBase ToGenericType()
        {
            DbTypeString res = new DbTypeString();
            if (SpecificCode != null) res.SetSpecificAttribute("sqlite", "subtype", SpecificCode);
            return res;
        }
        public override string ToString()
        {
            if (SpecificCode != null) return SpecificCode;
            return "text";
        }
        public override object Code
        {
            get { return SqliteTypeCode.Text; }
        }
    }

    public class SqliteTypeLogical : SqliteTypeBase
    {
        public override DbTypeBase ToGenericType()
        {
            DbTypeLogical res = new DbTypeLogical();
            return res;
        }
        public override string ToString()
        {
            return "logical";
        }
        public override object Code
        {
            get { return SqliteTypeCode.Logical; }
        }
    }

    public class SqliteTypeDateTime : SqliteTypeBase
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
            get { return SqliteTypeCode.DateTime; }
        }
    }

    public class SqliteTypeInt : SqliteTypeBase
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

        public override DbTypeBase ToGenericType()
        {
            DbTypeInt res = new DbTypeInt();
            res.Autoincrement = IsAutoIncrement;
            return res;
        }
        public override string ToString()
        {
            if (IsAutoIncrement) return "integer primary key";
            return "integer";
        }
        public override object Code
        {
            get { return SqliteTypeCode.Int; }
        }
    }

    public class SqliteTypeReal : SqliteTypeBase
    {
        public override DbTypeBase ToGenericType()
        {
            DbTypeFloat res = new DbTypeFloat();
            return res;
        }
        public override string ToString()
        {
            return "real";
        }
        public override object Code
        {
            get { return SqliteTypeCode.Real; }
        }
    }

    public class SqliteTypeBlob : SqliteTypeBase
    {
        public override DbTypeBase ToGenericType()
        {
            DbTypeBlob res = new DbTypeBlob();
            return res;
        }
        public override string ToString()
        {
            return "blob";
        }
        public override object Code
        {
            get { return SqliteTypeCode.Blob; }
        }
    }

    public class SqliteTypeNumeric : SqliteTypeBase
    {
        public override DbTypeBase ToGenericType()
        {
            DbTypeNumeric res = new DbTypeNumeric();
            return res;
        }
        public override string ToString()
        {
            return "numeric";
        }
        public override object Code
        {
            get { return SqliteTypeCode.Numeric; }
        }
    }

}
