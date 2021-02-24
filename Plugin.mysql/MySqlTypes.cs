using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Data;
using DatAdmin;
using System.Drawing.Design;
using System.ComponentModel.Design;

namespace Plugin.mysql
{
    public enum MySqlTypeCode
    {
        TinyInt = 0,
        SmallInt = 1,
        MediumInt = 2,
        Int = 3,
        BigInt = 4,
        Float = 5,
        Double = 6,
        Decimal = 7,
        Bit = 8,
        Datetime = 9,
        Date = 10,
        Time = 11,
        Timestamp = 12,
        Year = 13,
        Char = 14,
        VarChar = 15,
        Binary = 16,
        VarBinary = 17,
        TinyText = 19,
        Text = 20,
        MediumText = 21,
        LongText = 22,
        Enum = 23,
        Set = 24,
        TinyBlob = 30,
        Blob = 31,
        MediumBlob = 32,
        LongBlob = 33,

        // spatial types, see http://dev.mysql.com/doc/refman/5.0/en/mysql-spatial-datatypes.html
        Geometry = 34,
        Point = 35,
        LineString = 36,
        Polygon = 37,

        MultiPoint = 38,
        MultiLineString = 39,
        MultiPolygon = 40,
        GeometryCollection = 41,

        Generic
    }

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public abstract class MySqlTypeBase : PropertyPageBase, ISpecificType
    {
        protected const int DEFMAXLEN = 500;

        public static MySqlTypeBase CreateType(MySqlTypeCode code)
        {
            switch (code)
            {
                case MySqlTypeCode.TinyInt: return new MySqlTypeTinyInt();
                case MySqlTypeCode.SmallInt: return new MySqlTypeSmallInt();
                case MySqlTypeCode.MediumInt: return new MySqlTypeMediumInt();
                case MySqlTypeCode.Int: return new MySqlTypeInt();
                case MySqlTypeCode.BigInt: return new MySqlTypeBigInt();
                case MySqlTypeCode.Float: return new MySqlTypeFloat();
                case MySqlTypeCode.Double: return new MySqlTypeDouble();
                case MySqlTypeCode.Decimal: return new MySqlTypeDecimal();
                case MySqlTypeCode.Bit: return new MySqlTypeBit();
                case MySqlTypeCode.Datetime: return new MySqlTypeDatetime();
                case MySqlTypeCode.Date: return new MySqlTypeDate();
                case MySqlTypeCode.Time: return new MySqlTypeTime();
                case MySqlTypeCode.Timestamp: return new MySqlTypeTimestamp();
                case MySqlTypeCode.Year: return new MySqlTypeYear();
                case MySqlTypeCode.Char: return new MySqlTypeChar();
                case MySqlTypeCode.VarChar: return new MySqlTypeVarChar();
                case MySqlTypeCode.Binary: return new MySqlTypeBinary();
                case MySqlTypeCode.VarBinary: return new MySqlTypeVarBinary();
                case MySqlTypeCode.TinyText: return new MySqlTypeTinyText();
                case MySqlTypeCode.Text: return new MySqlTypeText();
                case MySqlTypeCode.MediumText: return new MySqlTypeMediumText();
                case MySqlTypeCode.LongText: return new MySqlTypeLongText();
                case MySqlTypeCode.Enum: return new MySqlTypeEnum();
                case MySqlTypeCode.Set: return new MySqlTypeSet();
                case MySqlTypeCode.TinyBlob: return new MySqlTypeTinyBlob();
                case MySqlTypeCode.Blob: return new MySqlTypeBlob();
                case MySqlTypeCode.MediumBlob: return new MySqlTypeMediumBlob();
                case MySqlTypeCode.LongBlob: return new MySqlTypeLongBlob();
                case MySqlTypeCode.Geometry: return new MySqlTypeGeometry();
                case MySqlTypeCode.Point: return new MySqlTypePoint();
                case MySqlTypeCode.LineString: return new MySqlTypeLineString();
                case MySqlTypeCode.Polygon: return new MySqlTypePolygon();
                case MySqlTypeCode.MultiPoint: return new MySqlTypeMultiPoint();
                case MySqlTypeCode.MultiLineString: return new MySqlTypeMultiLineString();
                case MySqlTypeCode.MultiPolygon: return new MySqlTypeMultiPolygon();
                case MySqlTypeCode.GeometryCollection: return new MySqlTypeGeometryCollection();
                case MySqlTypeCode.Generic: return new MySqlTypeGeneric();
            }
            throw new Exception(String.Format("DAE-00339 Unknown sql type code: {0}", code));
        }

        #region ISpecificType Members

        public abstract DbTypeBase ToGenericType();
        [Browsable(false)]
        public abstract object Code { get;}

        #endregion

        internal virtual void SetLength(string value) { }
        internal virtual void SetDecimals(string value) { }

        public static string MakeStringConstant(string value)
        {
            StringBuilder bld = new StringBuilder();
            bld.Append("'");
            foreach (char c in value)
            {
                if (c == '\'') bld.Append('\\');
                bld.Append(c);
            }
            bld.Append("'");
            return bld.ToString();
        }

        public static List<string> parseValues(string values)
        {
            string[] vals = values.Split(',');
            List<string> res = new List<string>();
            foreach (string val in vals)
            {
                res.Add(val.Substring(1, val.Length - 2));
            }
            return res;
        }
    }

    public abstract class MySqlTypeNumber : MySqlTypeBase
    {
        bool m_unsigned = false;
        [XmlAttrib("unsigned")]
        [DatAdmin.DisplayName("s_unsigned")]
        [TypeConverter(typeof(YesNoTypeConverter))]
        public bool Unsigned
        {
            get { return m_unsigned; }
            set { m_unsigned = value; }
        }

        bool m_zerofill;
        [XmlAttrib("zerofill")]
        [DatAdmin.DisplayName("ZEROFILL")]
        [TypeConverter(typeof(YesNoTypeConverter))]
        public bool Zerofill
        {
            get { return m_zerofill; }
            set { m_zerofill = value; }
        }

        int m_length = 0;
        [XmlAttrib("length")]
        [DatAdmin.DisplayName("s_length")]
        public int Length
        {
            get { return m_length; }
            set { m_length = value; }
        }

        internal override void SetLength(string value)
        {
            m_length = Int32.Parse(value);
        }

        protected string NumberLength
        {
            get
            {
                if (Length > 0) return "(" + Length.ToString() + ")";
                return "";
            }
        }

        protected string NumberFlags
        {
            get
            {
                string res = "";
                if (Unsigned) res += " UNSIGNED";
                if (Zerofill) res += " ZEROFILL";
                return res;
            }
        }

        protected virtual void SaveType(DbTypeNumber res)
        {
            res.Unsigned = Unsigned;
            if (Zerofill) res.SetSpecificAttribute("mysql", "zerofill", "1");
            if (Length > 0) res.SetSpecificAttribute("mysql", "length", Length.ToString());
        }
    }

    public abstract class MySqlTypeInteger : MySqlTypeNumber
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
            SaveType(res);
            return res;
        }
        protected abstract string SqlName { get;}
        protected abstract int DefaultLength { get; }
        public override string ToString()
        {
            string res = SqlName;
            if (Length != 0 && Length != DefaultLength) res += "(" + Length.ToString() + ")";
            res += NumberFlags;
            if (IsAutoIncrement) res += " AUTO_INCREMENT";
            return res;
        }
    }

    public class MySqlTypeTinyInt : MySqlTypeInteger
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
            get { return MySqlTypeCode.TinyInt; }
        }
        protected override int DefaultLength
        {
            get { return 4; }
        }
    }

    public class MySqlTypeSmallInt : MySqlTypeInteger
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
            get { return MySqlTypeCode.SmallInt; }
        }
        protected override int DefaultLength
        {
            get { return 6; }
        }
    }

    public class MySqlTypeMediumInt : MySqlTypeInteger
    {
        protected override int Bytes
        {
            get { return 3; }
        }
        protected override string SqlName
        {
            get { return "mediumint"; }
        }
        public override object Code
        {
            get { return MySqlTypeCode.MediumInt; }
        }
        protected override int DefaultLength
        {
            get { return 9; }
        }
    }

    public class MySqlTypeInt : MySqlTypeInteger
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
            get { return MySqlTypeCode.Int; }
        }
        protected override int DefaultLength
        {
            get { return 11; }
        }
    }

    public class MySqlTypeBigInt : MySqlTypeInteger
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
            get { return MySqlTypeCode.BigInt; }
        }
        protected override int DefaultLength
        {
            get { return 20; }
        }
    }

    public abstract class MySqlTypeCharacter : MySqlTypeBase
    {
        int m_length = 50;
        [DatAdmin.DisplayName("s_length")]
        public int Length
        {
            get { return m_length; }
            set { m_length = value; }
        }
        protected abstract bool IsVarLength { get;}
        protected abstract bool IsBinary { get;}
        public override DbTypeBase ToGenericType()
        {
            DbTypeString res = new DbTypeString();
            res.IsBinary = IsBinary;
            res.IsVarLength = IsVarLength;
            res.Length = Length;
            res.IsUnicode = false;
            return res;
        }
        internal override void SetLength(string value)
        {
            m_length = Int32.Parse(value);
        }
    }

    public class MySqlTypeChar : MySqlTypeCharacter
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
            get { return MySqlTypeCode.Char; }
        }
        public override string ToString()
        {
            return String.Format("char({0})", Length < 0 ? DEFMAXLEN : Length);
        }
    }

    public class MySqlTypeVarChar : MySqlTypeCharacter
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
            get { return MySqlTypeCode.VarChar; }
        }
        public override string ToString()
        {
            return String.Format("varchar({0})", Length < 0 ? DEFMAXLEN : Length);
        }
    }


    public class MySqlTypeBit : MySqlTypeBase
    {
        int m_length = 1;
        [DatAdmin.DisplayName("s_length")]
        public int Length
        {
            get { return m_length; }
            set { m_length = value; }
        }
        public override DbTypeBase ToGenericType()
        {
            if (m_length == 1)
            {
                var res = new DbTypeLogical();
                res.SetSpecificAttribute("mysql", "subtype", "bit");
                return res;
            }
            else
            {
                var res = new DbTypeInt { Bytes = 8, Unsigned = true };
                res.SetSpecificAttribute("mysql", "subtype", "bit");
                res.SetSpecificAttribute("mysql", "bitlength", Length.ToString());
                return res;
            }
        }
        public override string ToString()
        {
            if (m_length > 1) return String.Format("bit({0})", m_length);
            return "bit";
        }
        internal override void SetLength(string value)
        {
            m_length = Int32.Parse(value);
        }
        public override object Code
        {
            get { return MySqlTypeCode.Bit; }
        }
    }

    public class MySqlTypeBinary : MySqlTypeCharacter
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
            get { return MySqlTypeCode.Binary; }
        }
        public override string ToString()
        {
            return String.Format("binary({0})", Length);
        }
    }

    public class MySqlTypeVarBinary : MySqlTypeCharacter
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
            get { return MySqlTypeCode.VarBinary; }
        }
        public override string ToString()
        {
            return String.Format("varbinary({0})", Length);
        }
    }

    public class MySqlTypeDatetime : MySqlTypeBase
    {
        public override DbTypeBase ToGenericType()
        {
            DbTypeDatetime res = new DbTypeDatetime();
            res.SubType = DbDatetimeSubType.Datetime;
            res.SetSpecificAttribute("mysql", "subtype", "datetime");
            return res;
        }
        public override string ToString()
        {
            return "datetime";
        }
        public override object Code
        {
            get { return MySqlTypeCode.Datetime; }
        }
    }

    public class MySqlTypeTimestamp: MySqlTypeBase
    {
        public override DbTypeBase ToGenericType()
        {
            DbTypeDatetime res = new DbTypeDatetime();
            res.SubType = DbDatetimeSubType.Datetime;
            res.SetSpecificAttribute("mysql", "subtype", "timestamp");
            return res;
        }
        public override string ToString()
        {
            return "timestamp";
        }
        public override object Code
        {
            get { return MySqlTypeCode.Timestamp; }
        }
    }

    public class MySqlTypeDate : MySqlTypeBase
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
            get { return MySqlTypeCode.Date; }
        }
    }

    public class MySqlTypeTime: MySqlTypeBase
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
            get { return MySqlTypeCode.Time; }
        }
    }

    public class MySqlTypeYear : MySqlTypeBase
    {
        public override DbTypeBase ToGenericType()
        {
            DbTypeDatetime res = new DbTypeDatetime();
            res.SubType = DbDatetimeSubType.Year;
            return res;
        }
        public override string ToString()
        {
            return "year";
        }
        public override object Code
        {
            get { return MySqlTypeCode.Year; }
        }
    }

    public abstract class MySqlTypeTextBase : MySqlTypeBase
    {
    }

    public class MySqlTypeTinyText : MySqlTypeTextBase
    {
        public override DbTypeBase ToGenericType()
        {
            DbTypeText res = new DbTypeText();
            res.IsUnicode = false;
            res.SetSpecificAttribute("mysql", "subtype", "tinytext");
            return res;
        }
        public override string ToString()
        {
            return "tinytext";
        }
        public override object Code
        {
            get { return MySqlTypeCode.TinyText; }
        }
    }

    public class MySqlTypeText : MySqlTypeTextBase
    {
        public override DbTypeBase ToGenericType()
        {
            DbTypeText res = new DbTypeText();
            res.IsUnicode = false;
            res.SetSpecificAttribute("mysql", "subtype", "text");
            return res;
        }
        public override string ToString()
        {
            return "text";
        }
        public override object Code
        {
            get { return MySqlTypeCode.Text; }
        }
    }

    public class MySqlTypeMediumText : MySqlTypeTextBase
    {
        public override DbTypeBase ToGenericType()
        {
            DbTypeText res = new DbTypeText();
            res.IsUnicode = false;
            res.SetSpecificAttribute("mysql", "subtype", "mediumtext");
            return res;
        }
        public override string ToString()
        {
            return "mediumtext";
        }
        public override object Code
        {
            get { return MySqlTypeCode.MediumText; }
        }
    }

    public class MySqlTypeLongText : MySqlTypeTextBase
    {
        public override DbTypeBase ToGenericType()
        {
            DbTypeText res = new DbTypeText();
            res.IsUnicode = false;
            res.SetSpecificAttribute("mysql", "subtype", "longtext");
            return res;
        }
        public override string ToString()
        {
            return "longtext";
        }
        public override object Code
        {
            get { return MySqlTypeCode.LongText; }
        }
    }

    public class MySqlTypeTinyBlob : MySqlTypeBase
    {
        public override DbTypeBase ToGenericType()
        {
            DbTypeBlob res = new DbTypeBlob();
            res.SetSpecificAttribute("mysql", "subtype", "tinyblob");
            return res;
        }
        public override string ToString()
        {
            return "tinyblob";
        }
        public override object Code
        {
            get { return MySqlTypeCode.TinyBlob; }
        }
    }

    public class MySqlTypeBlob : MySqlTypeBase
    {
        public override DbTypeBase ToGenericType()
        {
            DbTypeBlob res = new DbTypeBlob();
            res.SetSpecificAttribute("mysql", "subtype", "blob");
            return res;
        }
        public override string ToString()
        {
            return "blob";
        }
        public override object Code
        {
            get { return MySqlTypeCode.Blob; }
        }
    }

    public class MySqlTypeMediumBlob : MySqlTypeBase
    {
        public override DbTypeBase ToGenericType()
        {
            DbTypeBlob res = new DbTypeBlob();
            res.SetSpecificAttribute("mysql", "subtype", "meduimblob");
            return res;
        }
        public override string ToString()
        {
            return "mediumblob";
        }
        public override object Code
        {
            get { return MySqlTypeCode.MediumBlob; }
        }
    }

    public class MySqlTypeLongBlob : MySqlTypeBase
    {
        public override DbTypeBase ToGenericType()
        {
            DbTypeBlob res = new DbTypeBlob();
            res.SetSpecificAttribute("mysql", "subtype", "longblob");
            return res;
        }
        public override string ToString()
        {
            return "longblob";
        }
        public override object Code
        {
            get { return MySqlTypeCode.LongBlob; }
        }
    }

    public abstract class MySqlTypeNumericBase : MySqlTypeNumber
    {
        //int m_precision;
        //[DatAdmin.DisplayName("s_precision")]
        //public int Precision
        //{
        //    get { return m_precision; }
        //    set { m_precision = value; }
        //}

        int m_decimals = 0;
        [DatAdmin.DisplayName("s_scale")]
        public int Decimals
        {
            get { return m_decimals; }
            set { m_decimals = value; }
        }

        protected abstract string SqlName { get;}

        public override DbTypeBase ToGenericType()
        {
            DbTypeNumeric res = new DbTypeNumeric();
            res.Precision = Length;
            res.Scale = Decimals;
            res.SetSpecificAttribute("mysql", "subtype", SqlName);
            SaveType(res);
            return res;
        }

        internal override void SetDecimals(string value)
        {
            m_decimals = Int32.Parse(value);
        }

        public override string ToString()
        {
            string res = String.Format("{0}({1},{2}){3}", SqlName, Length, Decimals, NumberFlags);
            return res;
        }
    }

    public class MySqlTypeNumeric : MySqlTypeNumericBase
    {
        protected override string SqlName
        {
            get { return "numeric"; }
        }
        public override object Code
        {
            get { return MySqlTypeCode.Decimal; }
        }
    }

    public class MySqlTypeDecimal : MySqlTypeNumericBase
    {
        protected override string SqlName
        {
            get { return "decimal"; }
        }
        public override object Code
        {
            get { return MySqlTypeCode.Decimal; }
        }
    }

    public abstract class MySqlTypeReal : MySqlTypeNumber
    {
        int m_decimals = 0;
        [DatAdmin.DisplayName("s_scale")]
        public int Decimals
        {
            get { return m_decimals; }
            set { m_decimals = value; }
        }
        protected override void SaveType(DbTypeNumber res)
        {
            base.SaveType(res);
            if (Length > 0)
            {
                res.SetSpecificAttribute("mysql", "length", Length.ToString());
                res.SetSpecificAttribute("mysql", "decimals", Decimals.ToString());
            }
        }
        internal override void SetDecimals(string value)
        {
            Decimals = Int32.Parse(value);
        }
        protected abstract string SqlName { get; }
        public override string ToString()
        {
            string res = SqlName;
            if (Length > 0) res += String.Format("({0},{1})", Length, Decimals);
            res += NumberFlags;
            return res;
        }
    }

    public class MySqlTypeFloat : MySqlTypeReal
    {
        public override DbTypeBase ToGenericType()
        {
            DbTypeFloat res = new DbTypeFloat();
            res.Bytes = 4;
            SaveType(res);
            return res;
        }
        public override object Code
        {
            get { return MySqlTypeCode.Float; }
        }
        protected override string SqlName
        {
            get { return "float"; }
        }
    }

    public class MySqlTypeDouble : MySqlTypeReal
    {
        public override DbTypeBase ToGenericType()
        {
            DbTypeFloat res = new DbTypeFloat();
            res.Bytes = 8;
            SaveType(res);
            return res;
        }
        public override object Code
        {
            get { return MySqlTypeCode.Double; }
        }
        protected override string SqlName
        {
            get { return "real"; }
        }
    }

    public abstract class MySqlTypeEnumSet : MySqlTypeBase
    {
        List<string> m_values = new List<string>();

        [Editor(typeof(LinesEditor), typeof(UITypeEditor))]
        [TypeConverter(typeof(TrimLinesTypeConverter))]
        [DatAdmin.DisplayName("s_values")]
        public List<string> Values
        {
            get { return m_values; }
            set { m_values = value; }
        }
        protected abstract string SqlName { get;}

        public override DbTypeBase ToGenericType()
        {
            DbTypeString res = new DbTypeString();
            res.SetSpecificAttribute("mysql", "subtype", SqlName);
            res.SetSpecificAttribute("mysql", "values", String.Join(",", Values.ToArray()));
            return res;
        }

        public override string ToString()
        {
            StringBuilder bld = new StringBuilder();
            bld.Append(SqlName);
            bool was = false;
            bld.Append("(");
            foreach (string val in m_values)
            {
                if (was) bld.Append(',');
                bld.Append(MakeStringConstant(val));
                was = true;
            }
            bld.Append(")");
            return bld.ToString();
        }
    }

    public class MySqlTypeEnum : MySqlTypeEnumSet
    {
        public override object Code
        {
            get { return MySqlTypeCode.Enum; }
        }
        protected override string SqlName
        {
            get { return "enum"; }
        }
    }

    public class MySqlTypeSet : MySqlTypeEnumSet
    {
        public override object Code
        {
            get { return MySqlTypeCode.Set; }
        }
        protected override string SqlName
        {
            get { return "set"; }
        }
    }

    public abstract class MySqlSpatialType : MySqlTypeBase
    {
        public override DbTypeBase ToGenericType()
        {
            DbTypeText res = new DbTypeText();
            res.SetSpecificAttribute("mysql", "subtype", SqlName);
            return res;
        }
        protected abstract string SqlName { get; }
        public override string ToString()
        {
            return SqlName;
        }
    }

    public class MySqlTypeGeometry : MySqlSpatialType
    {
        public override object Code
        {
            get { return MySqlTypeCode.Geometry; }
        }
        protected override string SqlName
        {
            get { return "geometry"; }
        }
    }

    public class MySqlTypePoint : MySqlSpatialType
    {
        public override object Code
        {
            get { return MySqlTypeCode.Point; }
        }
        protected override string SqlName
        {
            get { return "point"; }
        }
    }

    public class MySqlTypeLineString : MySqlSpatialType
    {
        public override object Code
        {
            get { return MySqlTypeCode.LineString; }
        }
        protected override string SqlName
        {
            get { return "linestring"; }
        }
    }

    public class MySqlTypePolygon : MySqlSpatialType
    {
        public override object Code
        {
            get { return MySqlTypeCode.Polygon; }
        }
        protected override string SqlName
        {
            get { return "polygon"; }
        }
    }

    public class MySqlTypeMultiPoint : MySqlSpatialType
    {
        public override object Code
        {
            get { return MySqlTypeCode.MultiPoint; }
        }
        protected override string SqlName
        {
            get { return "multipoint"; }
        }
    }

    public class MySqlTypeMultiLineString : MySqlSpatialType
    {
        public override object Code
        {
            get { return MySqlTypeCode.MultiLineString; }
        }
        protected override string SqlName
        {
            get { return "multilinestring"; }
        }
    }

    public class MySqlTypeMultiPolygon : MySqlSpatialType
    {
        public override object Code
        {
            get { return MySqlTypeCode.MultiPolygon; }
        }
        protected override string SqlName
        {
            get { return "multipolygon"; }
        }
    }

    public class MySqlTypeGeometryCollection : MySqlSpatialType
    {
        public override object Code
        {
            get { return MySqlTypeCode.GeometryCollection; }
        }
        protected override string SqlName
        {
            get { return "geometrycollection"; }
        }
    }

    public class MySqlTypeGeneric : MySqlTypeBase
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
            get { return MySqlTypeCode.Generic; }
        }
    }
}
