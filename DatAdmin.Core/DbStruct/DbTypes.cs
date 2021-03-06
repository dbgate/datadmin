using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.IO;
using System.Data;

namespace DatAdmin
{
    public enum DbTypeCode
    {
        Int,
        String,
        Logical,
        Datetime,
        Numeric,
        Blob,
        Text,
        Float,
        //Enum,
        //Set,
        Xml,
        Array,
        Generic,
    };

    public enum DbDatetimeSubType
    {
        Datetime,
        Date,
        Time,
        Year,
        Interval
    }

    public enum DbSizeType
    {
        Normal,
        Small,
        Medium,
        Tiny,
        Long
    }

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public abstract class DbTypeBase : PropertyPageBase, ISpecificType
    {
        Dictionary<string, string> m_specificData = new Dictionary<string, string>();

        [Browsable(false)]
        public abstract DbTypeCode Code { get; }
        [Browsable(false)]
        public abstract Type DotNetType { get; }
        [Browsable(false)]
        public abstract string XsdType { get; }
        [Browsable(false)]
        public abstract TypeStorage DefaultStorage { get; }

        //[Browsable(false)]
        //public ArrayDimensions ArraySpec { get; set; }

        //[DisplayName("s_specific_data")]
        [Browsable(false)]
        public Dictionary<string, string> SpecificData
        {
            get { return m_specificData; }
            set { m_specificData = value; }
        }

        public DbTypeBase()
        {
            //ArraySpec = new ArrayDimensions();
        }

        public string GetSpecificAttribute(string dialect, string name)
        {
            string key = dialect + "." + name;
            if (SpecificData.ContainsKey(key)) return SpecificData[key];
            return null;
        }
        public void SetSpecificAttribute(string dialect, string name, string value)
        {
            string key = dialect + "." + name;
            SpecificData[key] = value;
        }

        public static DbTypeBase CreateType(DbTypeCode code)
        {
            switch (code)
            {
                case DbTypeCode.Int: return new DbTypeInt();
                case DbTypeCode.String: return new DbTypeString();
                case DbTypeCode.Logical: return new DbTypeLogical();
                case DbTypeCode.Datetime: return new DbTypeDatetime();
                case DbTypeCode.Numeric: return new DbTypeNumeric();
                case DbTypeCode.Blob: return new DbTypeBlob();
                case DbTypeCode.Text: return new DbTypeText();
                case DbTypeCode.Float: return new DbTypeFloat();
                //case DbTypeCode.Enum: return new DbTypeEnum();
                //case DbTypeCode.Set: return new DbTypeSet();
                case DbTypeCode.Xml: return new DbTypeXml();
                case DbTypeCode.Array: return new DbTypeArray();
                case DbTypeCode.Generic: return new DbTypeGeneric();
            }
            throw new Exception(String.Format("DAE-00239 Unknown db type code: {0}", code));
        }

        public virtual void SetAutoincrement(bool value) { }
        public virtual bool IsAutoIncrement() { return false; }

        public virtual void SetLength(int value) { }
        public virtual int GetLength() { return 0; }

        public virtual void SetScale(int value) { }
        public virtual int GetScale() { return 0; }

        //public static bool EqualsTypes(DbTypeBase a, DbTypeBase b)
        //{
        //    if (a.GetType() != b.GetType()) return false;

        //    MemoryStream sa = new MemoryStream(), sb = new MemoryStream();
        //    XmlTool.SerializeObject(sa, a);
        //    XmlTool.SerializeObject(sb, b);
        //    byte[] ma = sa.ToArray(), mb = sb.ToArray();
        //    if (!ArrayTool.EqualsArrays(ma, mb)) return false;
        //    return true;
        //}

        public virtual void SaveToXml(XmlElement xml)
        {
            xml.SetAttribute("datatype", Code.ToString().ToLower());
            //if (ArraySpec.IsArray) xml.SetAttribute("array", ArraySpec.ToString());
            XmlTool.SaveSpecificAttributes(SpecificData, "typespec.", xml);
            XmlTool.SaveProperties(this, xml);
        }

        protected virtual void LoadFromXml(XmlElement xml)
        {
            XmlTool.LoadProperties(this, xml);
            XmlTool.LoadSpecificAttributes(SpecificData, "typespec.", xml);
            //if (xml.HasAttribute("array")) ArraySpec = new ArrayDimensions(xml.GetAttribute("array"));
        }

        public static DbTypeBase Load(XmlElement xml)
        {
            DbTypeCode code = (DbTypeCode)Enum.Parse(typeof(DbTypeCode), xml.GetAttribute("datatype"), true);
            DbTypeBase res = DbTypeBase.CreateType(code);
            res.LoadFromXml(xml);
            return res;
        }

        public virtual int GetSize() { return 0; }

        public abstract DbType GetProviderType();

        public DbTypeBase Clone()
        {
            var res = (DbTypeBase)MemberwiseClone();
            res.SpecificData = new Dictionary<string, string>(SpecificData);
            return res;
        }

        #region ISpecificType Members

        public DbTypeBase ToGenericType()
        {
            return this;
        }

        object ISpecificType.Code
        {
            get { return this.Code; }
        }

        #endregion
    }

    [AttributeUsage(AttributeTargets.Property)]
    public class IsIdentityAttribute : Attribute
    {
    }

    public abstract class DbTypeNumber : DbTypeBase
    {
        protected bool m_unsigned = false;
        [XmlAttrib("unsigned")]
        [DisplayName("s_unsigned")]
        [TypeConverter(typeof(YesNoTypeConverter))]
        public bool Unsigned
        {
            get { return m_unsigned; }
            set { m_unsigned = value; }
        }
    }

    public class DbTypeInt : DbTypeNumber
    {
        int m_bytes = 4;
        [DisplayName("s_bytes")]
        [XmlAttrib("bytes")]
        public int Bytes
        {
            get { return m_bytes; }
            set { m_bytes = value; }
        }

        bool m_autoincrement = false;
        [XmlAttrib("autoincrement")]
        [DisplayName("s_autoincrement")]
        [TypeConverter(typeof(YesNoTypeConverter))]
        [IsIdentity]
        public bool Autoincrement
        {
            get { return m_autoincrement; }
            set { m_autoincrement = value; }
        }

        public override DbTypeCode Code
        {
            get { return DbTypeCode.Int; }
        }
        public override Type DotNetType
        {
            get
            {
                if (m_unsigned)
                {
                    switch (m_bytes)
                    {
                        case 1: return typeof(byte);
                        case 2: return typeof(ushort);
                        case 4: return typeof(uint);
                        case 8: return typeof(ulong);

                    }
                }
                else
                {
                    switch (m_bytes)
                    {
                        case 1: return typeof(sbyte);
                        case 2: return typeof(short);
                        case 4: return typeof(int);
                        case 8: return typeof(long);

                    }
                }
                return typeof(int);
            }
        }
        public override TypeStorage DefaultStorage
        {
            get
            {
                if (m_unsigned)
                {
                    switch (m_bytes)
                    {
                        case 1: return TypeStorage.Byte;
                        case 2: return TypeStorage.UInt16;
                        case 4: return TypeStorage.UInt32;
                        case 8: return TypeStorage.UInt64;
                    }
                }
                else
                {
                    switch (m_bytes)
                    {
                        case 1: return TypeStorage.SByte;
                        case 2: return TypeStorage.Int16;
                        case 4: return TypeStorage.Int32;
                        case 8: return TypeStorage.Int64;
                    }
                }
                return TypeStorage.Int32;
            }
        }
        public static DbTypeInt Create(int bytes, bool unsigned)
        {
            DbTypeInt res = new DbTypeInt();
            res.Bytes = bytes;
            res.Unsigned = unsigned;
            return res;
        }

        public override string ToString()
        {
            return "integer";
        }
        public override void SetAutoincrement(bool value) { m_autoincrement = value; }
        public override bool IsAutoIncrement() { return m_autoincrement; }

        public override DbType GetProviderType()
        {
            switch (m_bytes)
            {
                case 2: return DbType.Int16;
                case 4: return DbType.Int32;
                case 8: return DbType.Int64;
            }
            return DbType.Int32;
        }

        public override string XsdType
        {
            get
            {
                if (m_unsigned)
                {
                    switch (m_bytes)
                    {
                        case 1: return "xs:unsignedByte";
                        case 2: return "xs:unsignedShort";
                        case 4: return "xs:unsignedInt";
                        case 8: return "xs:unsignedLong";

                    }
                }
                else
                {
                    switch (m_bytes)
                    {
                        case 1: return "xs:byte";
                        case 2: return "xs:short";
                        case 4: return "xs:int";
                        case 8: return "xs:long";

                    }
                }
                return "xs:int";
            }
        }
    }

    public class DbTypeString : DbTypeBase
    {
        public DbTypeString() { }
        public DbTypeString(int length)
        {
            m_length = length;
        }

        int m_length = 50;
        [DisplayName("s_length")]
        [XmlAttrib("length")]
        public int Length
        {
            get { return m_length; }
            set { m_length = value; }
        }

        bool m_isUnicode = false;
        [DisplayName("s_is_unicode")]
        [XmlAttrib("unicode")]
        [TypeConverter(typeof(YesNoTypeConverter))]
        public bool IsUnicode
        {
            get { return m_isUnicode; }
            set { m_isUnicode = value; }
        }

        bool m_isBinary = false;
        [DisplayName("s_is_binary")]
        [XmlAttrib("binary")]
        [TypeConverter(typeof(YesNoTypeConverter))]
        public bool IsBinary
        {
            get { return m_isBinary; }
            set { m_isBinary = value; }
        }

        bool m_isVarLength = true;
        [DisplayName("s_is_varlength")]
        [XmlAttrib("varlength")]
        [TypeConverter(typeof(YesNoTypeConverter))]
        public bool IsVarLength
        {
            get { return m_isVarLength; }
            set { m_isVarLength = value; }
        }

        public override DbTypeCode Code
        {
            get { return DbTypeCode.String; }
        }
        public override Type DotNetType
        {
            get
            {
                if (IsBinary) return typeof(byte[]);
                return typeof(String);
            }
        }

        public static DbTypeBase Varchar(int len, bool unicode)
        {
            DbTypeString res = new DbTypeString();
            res.Length = len;
            res.IsVarLength = true;
            res.IsUnicode = unicode;
            return res;
        }
        public static DbTypeBase Varchar(int len)
        {
            return Varchar(len, false);
        }

        public static DbTypeBase Char(int len, bool unicode)
        {
            DbTypeString res = new DbTypeString();
            res.Length = len;
            res.IsVarLength = false;
            res.IsUnicode = unicode;
            return res;
        }
        public static DbTypeBase Char(int len)
        {
            return Char(len, false);
        }

        public override string ToString()
        {
            string sqlname;
            if (IsVarLength)
            {
                if (IsUnicode) sqlname = "nvarchar";
                else sqlname = "varchar";
            }
            else
            {
                if (IsUnicode) sqlname = "nchar";
                else sqlname = "char";
            }
            return String.Format("{0}({1})", sqlname, Length < 0 ? "max" : Length.ToString());
        }

        public override void SetLength(int value) { Length = value; }
        public override int GetLength() { return Length; }

        public override int GetSize()
        {
            return Length;
        }

        public override DbType GetProviderType()
        {
            return DbType.String;
        }

        public override string XsdType
        {
            get { return "xs:string"; }
        }

        public override TypeStorage DefaultStorage
        {
            get
            {
                foreach (var pair in SpecificData)
                {
                    // HACK
                    if (pair.Key.EndsWith(".subtype"))
                    {
                        if (pair.Value.Contains("uniqueidentifier") || pair.Value.Contains("guid")) return TypeStorage.Guid;
                    }
                }
                //TypeStorage? res = HDialect.GetSpecificTypeStorage(this);
                //if (res != null) return res;
                if (IsBinary) return TypeStorage.ByteArray;
                return TypeStorage.String;
            }
        }

        public DbTypeBase ConvertToBlobVariant()
        {
            if (IsBinary) return new DbTypeBlob();
            return new DbTypeText { IsUnicode = IsUnicode };
        }
    }

    public class DbTypeLogical : DbTypeBase
    {
        public override DbTypeCode Code
        {
            get { return DbTypeCode.Logical; }
        }
        public override Type DotNetType
        {
            get { return typeof(Boolean); }
        }
        public override string ToString()
        {
            return "logical";
        }
        public override DbType GetProviderType()
        {
            return DbType.Boolean;
        }
        public override string XsdType
        {
            get { return "xs:boolean"; }
        }
        public override TypeStorage DefaultStorage
        {
            get { return TypeStorage.Boolean; }
        }
    }

    public class DbTypeDatetime : DbTypeBase
    {
        DbDatetimeSubType m_subType = DbDatetimeSubType.Datetime;
        [DisplayName("s_subtype")]
        [XmlAttrib("subtype")]
        public DbDatetimeSubType SubType
        {
            get { return m_subType; }
            set { m_subType = value; }
        }

        //bool m_isInterval = false;
        //[DisplayName("s_is_interval")]
        //public bool IsInterval
        //{
        //    get { return m_isInterval; }
        //    set { m_isInterval = value; }
        //}

        bool m_hasTimeZone = false;
        [DisplayName("s_has_time_zone")]
        [XmlAttrib("hastimezone")]
        [TypeConverter(typeof(YesNoTypeConverter))]
        public bool HasTimeZone
        {
            get { return m_hasTimeZone; }
            set { m_hasTimeZone = value; }
        }

        public override DbTypeCode Code
        {
            get { return DbTypeCode.Datetime; }
        }
        public override Type DotNetType
        {
            get { return typeof(DateTime); }
        }
        public override string ToString()
        {
            return "datetime";
        }
        public override DbType GetProviderType()
        {
            return DbType.DateTime;
        }
        public override string XsdType
        {
            get
            {
                switch (m_subType)
                {
                    case DbDatetimeSubType.Interval: return "xs:duration";
                }
                return "xs:dateTime";
            }
        }
        public override TypeStorage DefaultStorage
        {
            get
            {
                switch (m_subType)
                {
                    case DbDatetimeSubType.Date: 
                        return TypeStorage.DateEx;
                    case DbDatetimeSubType.Datetime:
                        return TypeStorage.DateTimeEx;
                    case DbDatetimeSubType.Time:
                        return TypeStorage.TimeEx;
                    case DbDatetimeSubType.Year:
                        return TypeStorage.Int16;
                    case DbDatetimeSubType.Interval:
                        return TypeStorage.String;
                }
                return TypeStorage.DateTimeEx;
            }
        }
    }

    public class DbTypeNumeric : DbTypeNumber
    {
        int m_precision = 12;
        [DisplayName("s_precision")]
        [XmlAttrib("precision")]
        public int Precision
        {
            get { return m_precision; }
            set { m_precision = value; }
        }

        int m_scale = 3;
        [DisplayName("s_scale")]
        [XmlAttrib("scale")]
        public int Scale
        {
            get { return m_scale; }
            set { m_scale = value; }
        }

        bool m_autoincrement = false;
        [DisplayName("s_autoincrement")]
        [XmlAttrib("autoincrement")]
        [TypeConverter(typeof(YesNoTypeConverter))]
        [IsIdentity]
        public bool Autoincrement
        {
            get { return m_autoincrement; }
            set { m_autoincrement = value; }
        }

        public override DbTypeCode Code
        {
            get { return DbTypeCode.Numeric; }
        }
        public override Type DotNetType
        {
            get { return typeof(Decimal); }
        }
        public static DbTypeBase Create(int scale, int prec)
        {
            DbTypeNumeric res = new DbTypeNumeric();
            res.Scale = scale;
            res.Precision = prec;
            return res;
        }
        public override string ToString()
        {
            return String.Format("numeric({0},{1})", Precision, Scale);
        }
        public override void SetAutoincrement(bool value) { m_autoincrement = value; }
        public override bool IsAutoIncrement() { return m_autoincrement; }
        public override int GetSize()
        {
            return Precision;
        }
        public override DbType GetProviderType()
        {
            return DbType.Decimal;
        }
        public override string XsdType
        {
            get { return "xs:decimal"; }
        }
        public override TypeStorage DefaultStorage
        {
            get { return TypeStorage.Decimal; }
        }

        public override void SetLength(int value) { Precision = value; }
        public override int GetLength() { return Precision; }

        public override void SetScale(int value) { Scale = value; }
        public override int GetScale() { return Scale; }
    }

    public class DbTypeBlob : DbTypeBase
    {
        DbSizeType m_size = DbSizeType.Normal;
        [DisplayName("s_size")]
        [XmlAttrib("size")]
        public DbSizeType Size
        {
            get { return m_size; }
            set { m_size = value; }
        }

        public override DbTypeCode Code
        {
            get { return DbTypeCode.Blob; }
        }
        public override Type DotNetType
        {
            get { return typeof(Byte[]); }
        }
        public override string ToString()
        {
            return "blob";
        }
        public override DbType GetProviderType()
        {
            return DbType.Binary;
        }
        public override string XsdType
        {
            get { return "xs:base64Binary"; }
        }
        public override TypeStorage DefaultStorage
        {
            get { return TypeStorage.ByteArray; }
        }
    }

    public class DbTypeText : DbTypeBase
    {
        DbSizeType m_size = DbSizeType.Normal;
        [DisplayName("s_size")]
        [XmlAttrib("size")]
        public DbSizeType Size
        {
            get { return m_size; }
            set { m_size = value; }
        }

        bool m_isUnicode = false;
        [XmlAttrib("unicode")]
        [DisplayName("s_is_unicode")]
        [TypeConverter(typeof(YesNoTypeConverter))]
        public bool IsUnicode
        {
            get { return m_isUnicode; }
            set { m_isUnicode = value; }
        }

        public override DbTypeCode Code
        {
            get { return DbTypeCode.Text; }
        }
        public override Type DotNetType
        {
            get { return typeof(String); }
        }
        public override string ToString()
        {
            return "text";
        }
        public override DbType GetProviderType()
        {
            return DbType.String;
        }
        public override string XsdType
        {
            get { return "xs:string"; }
        }
        public override TypeStorage DefaultStorage
        {
            get { return TypeStorage.String; }
        }
    }

    public class DbTypeFloat : DbTypeNumber
    {
        int m_bytes = 8;
        [DisplayName("s_bytes")]
        [XmlAttrib("bytes")]
        public int Bytes
        {
            get { return m_bytes; }
            set { m_bytes = value; }
        }

        bool m_isMoney;
        [DisplayName("s_is_money")]
        [XmlAttrib("money")]
        [TypeConverter(typeof(YesNoTypeConverter))]
        public bool IsMoney
        {
            get { return m_isMoney; }
            set { m_isMoney = value; }
        }

        public override DbTypeCode Code
        {
            get { return DbTypeCode.Float; }
        }
        public override Type DotNetType
        {
            get
            {
                if (IsMoney) return typeof(decimal);
                return typeof(double);
            }
        }

        public static DbTypeBase Create(int bytes)
        {
            DbTypeFloat res = new DbTypeFloat();
            res.Bytes = bytes;
            return res;
        }
        public override string ToString()
        {
            return "float";
        }
        public override DbType GetProviderType()
        {
            if (IsMoney) return DbType.Currency;
            return DbType.Double;
        }
        public override string XsdType
        {
            get
            {
                switch (m_bytes)
                {
                    case 4: return "xs:float";
                    case 8: return "xs:double";
                }
                return "xs:double";
            }
        }
        public override TypeStorage DefaultStorage
        {
            get
            {
                switch (m_bytes)
                {
                    case 4: return TypeStorage.Float;
                    case 8: return TypeStorage.Double;
                }
                return TypeStorage.Double;
            }
        }
    }

    public class DbTypeXml : DbTypeBase
    {
        public override DbTypeCode Code
        {
            get { return DbTypeCode.Xml; }
        }
        public override Type DotNetType
        {
            get { return typeof(String); }
        }
        public override string ToString()
        {
            return "xml";
        }
        public override DbType GetProviderType()
        {
            return DbType.Xml;
        }
        public override string XsdType
        {
            get { return "xs:string"; }
        }
        public override TypeStorage DefaultStorage
        {
            get { return TypeStorage.String; }
        }
    }

    public abstract class DbTypeStructured : DbTypeBase
    {
    }

    public class DbTypeArray : DbTypeStructured
    {
        DbTypeBase m_elementType;
        public DbTypeBase ElementType
        {
            get { return m_elementType; }
            set { m_elementType = value; }
        }

        ArrayDimensions m_dims = new ArrayDimensions(new ArrayDimension());
        public ArrayDimensions Dims
        {
            get { return m_dims; }
            set { m_dims = value; }
        }

        public override string ToString()
        {
            return String.Format("{0}[{1}]", ElementType, Dims);
        }
        public override DbTypeCode Code
        {
            get { return DbTypeCode.Array; }
        }
        public override TypeStorage DefaultStorage
        {
            get { return TypeStorage.String; }
        }
        public override string XsdType
        {
            get { return "xs:string"; }
        }
        public override Type DotNetType
        {
            get { return ElementType.DotNetType.MakeArrayType(); }
        }
        public override void SaveToXml(XmlElement xml)
        {
            base.SaveToXml(xml);
            xml.SetAttribute("dims", Dims.ToString());
            ElementType.SaveToXml(xml.AddChild("Element"));
        }
        protected override void LoadFromXml(XmlElement xml)
        {
            base.LoadFromXml(xml);
            Dims = new ArrayDimensions(xml.GetAttribute("dims"));
            ElementType = DbTypeBase.Load(xml.FindElement("Element"));
        }
        public override DbType GetProviderType()
        {
            return DbType.AnsiString;
        }
    }

    public class DbTypeGeneric : DbTypeBase
    {
        string m_sql;
        [DisplayName("SQL")]
        [XmlAttrib("sql")]
        public string Sql
        {
            get { return m_sql; }
            set { m_sql = value; }
        }

        public override DbTypeCode Code
        {
            get { return DbTypeCode.Generic; }
        }
        public override Type DotNetType
        {
            get { return typeof(String); }
        }
        public override string ToString()
        {
            return m_sql;
        }
        public override DbType GetProviderType()
        {
            return DbType.String;
        }
        public override string XsdType
        {
            get { return "xs:string"; }
        }
        public override TypeStorage DefaultStorage
        {
            get { return TypeStorage.String; }
        }

        public static DbTypeGeneric Unknown()
        {
            return new DbTypeGeneric { Sql = "unknown" };
        }
    }


    //public abstract class _DbTypeEnumSet : DbTypeBase
    //{
    //    List<string> m_values = new List<string>();

    //    [Editor(typeof(CollectionEditor), typeof(UITypeEditor))]
    //    [DisplayName("s_values")]
    //    public List<string> Values
    //    {
    //        get { return m_values; }
    //        set { m_values = value; }
    //    }
    //}

    //public class DbTypeEnum : _DbTypeEnumSet
    //{
    //    public override DbTypeCode Code
    //    {
    //        get { return DbTypeCode.Enum; }
    //    }
    //    public override Type DotNetType
    //    {
    //        get { return typeof(Int32); }
    //    }
    //}

    //public class DbTypeSet : _DbTypeEnumSet
    //{
    //    public override DbTypeCode Code
    //    {
    //        get { return DbTypeCode.Set; }
    //    }
    //    public override Type DotNetType
    //    {
    //        get { return typeof(Int32); }
    //    }
    //}
}
