using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using DatAdmin;

namespace Plugin.postgre
{
    public enum PostgreSqlTypeCode
    {
        BigInt,
        BigSerial,
        Bit,
        VarBit,
        Boolean,
        Box,
        Bytea,
        VarChar,
        Char,
        Cidr,
        Circle,
        Date,
        Double,
        Inet,
        Integer,
        Interval,
        Line,
        LineSeg,
        MacAddr,
        Money,
        Numeric,
        Path,
        Point,
        Polygon,
        Real,
        SmallInt,
        Serial,
        Text,
        Time,
        TimeTz,
        Timestamp,
        TimestampTz,
        BpChar,
        Oid,
        //Array,
        // PostGIS Types
        // http://postgis.refractions.net/docs/reference.html#PostGIS_Types
        Box2D,
        Box3D,
        Box3D_Extent,
        Geometry,
        Geometry_Dump,
        Geography,

        Generic,
    }

    [TypeConverter(typeof(ExpandableObjectConverter))]
    public abstract class PostgreSqlTypeBase : PropertyPageBase, ISpecificType
    {
        protected const int DEFMAXLEN = 500;

        [DatAdmin.DisplayName("s_type_is_array")]
        [TypeConverter(typeof(YesNoTypeConverter))]
        public bool IsArray { get; set; }

        public static PostgreSqlTypeBase CreateType(PostgreSqlTypeCode code)
        {
            switch (code)
            {
                case PostgreSqlTypeCode.BigInt: return new PostgreSqlTypeBigInt();
                case PostgreSqlTypeCode.BigSerial: return new PostgreSqlTypeBigSerial();
                case PostgreSqlTypeCode.Bit: return new PostgreSqlTypeBit();
                case PostgreSqlTypeCode.VarBit: return new PostgreSqlTypeVarBit();
                case PostgreSqlTypeCode.Boolean: return new PostgreSqlTypeBoolean();
                case PostgreSqlTypeCode.Box: return new PostgreSqlTypeBox();
                case PostgreSqlTypeCode.Bytea: return new PostgreSqlTypeBytea();
                case PostgreSqlTypeCode.VarChar: return new PostgreSqlTypeVarChar();
                case PostgreSqlTypeCode.Char: return new PostgreSqlTypeChar();
                case PostgreSqlTypeCode.Cidr: return new PostgreSqlTypeCidr();
                case PostgreSqlTypeCode.Circle: return new PostgreSqlTypeCircle();
                case PostgreSqlTypeCode.Date: return new PostgreSqlTypeDate();
                case PostgreSqlTypeCode.Double: return new PostgreSqlTypeDouble();
                case PostgreSqlTypeCode.Inet: return new PostgreSqlTypeInet();
                case PostgreSqlTypeCode.Integer: return new PostgreSqlTypeInteger();
                case PostgreSqlTypeCode.Interval: return new PostgreSqlTypeInterval();
                case PostgreSqlTypeCode.Line: return new PostgreSqlTypeLine();
                case PostgreSqlTypeCode.LineSeg: return new PostgreSqlTypeLineSeg();
                case PostgreSqlTypeCode.MacAddr: return new PostgreSqlTypeMacAddr();
                case PostgreSqlTypeCode.Money: return new PostgreSqlTypeMoney();
                case PostgreSqlTypeCode.Numeric: return new PostgreSqlTypeNumeric();
                case PostgreSqlTypeCode.Path: return new PostgreSqlTypePath();
                case PostgreSqlTypeCode.Point: return new PostgreSqlTypePoint();
                case PostgreSqlTypeCode.Polygon: return new PostgreSqlTypePolygon();
                case PostgreSqlTypeCode.Real: return new PostgreSqlTypeReal();
                case PostgreSqlTypeCode.SmallInt: return new PostgreSqlTypeSmallInt();
                case PostgreSqlTypeCode.Serial: return new PostgreSqlTypeSerial();
                case PostgreSqlTypeCode.Text: return new PostgreSqlTypeText();
                case PostgreSqlTypeCode.Time: return new PostgreSqlTypeTime();
                case PostgreSqlTypeCode.TimeTz: return new PostgreSqlTypeTimeTz();
                case PostgreSqlTypeCode.Timestamp: return new PostgreSqlTypeTimestamp();
                case PostgreSqlTypeCode.TimestampTz: return new PostgreSqlTypeTimestampTz();
                case PostgreSqlTypeCode.Oid: return new PostgreSqlTypeOid();
                //case PostgreSqlTypeCode.Array: return new PostgreSqlTypeArray();
                case PostgreSqlTypeCode.BpChar: return new PostgreSqlTypeBpChar();
                case PostgreSqlTypeCode.Box2D: return new PostGISTypeBox2D();
                case PostgreSqlTypeCode.Box3D: return new PostGISTypeBox3D();
                case PostgreSqlTypeCode.Box3D_Extent: return new PostGISTypeBox3D_Extent();
                case PostgreSqlTypeCode.Geometry: return new PostGISTypeGeometry();
                case PostgreSqlTypeCode.Geometry_Dump: return new PostGISTypeGeometry_Dump();
                case PostgreSqlTypeCode.Geography: return new PostGISTypeGeography();
                case PostgreSqlTypeCode.Generic: return new PostgreSqlTypeGeneric();
            }
            throw new DbAnalyseError(String.Format("DAE-00350 Unknown sql type code: {0}", code));
        }

        #region ISpecificType Members

        public DbTypeBase ToGenericType()
        {
            DbTypeBase res = ToGenericTypeNoArray();
            if (IsArray)
            {
                var ar = new DbTypeArray();
                ar.ElementType = res;
                return ar;
            }
            else
            {
                return res;
            }
        }
        [Browsable(false)]
        public abstract object Code { get;}

        //public abstract DbTypeBase ToGenericType();

        #endregion

        protected abstract string ToStringNoArray();
        protected abstract DbTypeBase ToGenericTypeNoArray();

        public override string ToString()
        {
            string res = ToStringNoArray();
            if (IsArray) res = res + "[]";
            return res;
        }
    }

    public class PostgreSqlTypeSmallInt : PostgreSqlTypeBase
    {
        protected override DbTypeBase ToGenericTypeNoArray()
        {
            DbTypeInt res = new DbTypeInt();
            res.Bytes = 2;
            return res;
        }
        protected override string ToStringNoArray()
        {
            return "smallint";
        }
        public override object Code
        {
            get { return PostgreSqlTypeCode.SmallInt; }
        }
    }

    public class PostgreSqlTypeInteger : PostgreSqlTypeBase
    {
        protected override DbTypeBase ToGenericTypeNoArray()
        {
            DbTypeInt res = new DbTypeInt();
            res.Bytes = 4;
            return res;
        }
        protected override string ToStringNoArray()
        {
            return "integer";
        }
        public override object Code
        {
            get { return PostgreSqlTypeCode.Integer; }
        }
    }

    public class PostgreSqlTypeBigInt : PostgreSqlTypeBase
    {
        protected override DbTypeBase ToGenericTypeNoArray()
        {
            DbTypeInt res = new DbTypeInt();
            res.Bytes = 8;
            return res;
        }
        protected override string ToStringNoArray()
        {
            return "bigint";
        }
        public override object Code
        {
            get { return PostgreSqlTypeCode.BigInt; }
        }
    }

    public class PostgreSqlTypeSerial : PostgreSqlTypeBase
    {
        protected override DbTypeBase ToGenericTypeNoArray()
        {
            DbTypeInt res = new DbTypeInt();
            res.Bytes = 4;
            res.Autoincrement = true;
            return res;
        }
        protected override string ToStringNoArray()
        {
            return "serial";
        }
        public override object Code
        {
            get { return PostgreSqlTypeCode.Serial; }
        }
    }

    public class PostgreSqlTypeBigSerial : PostgreSqlTypeBase
    {
        protected override DbTypeBase ToGenericTypeNoArray()
        {
            DbTypeInt res = new DbTypeInt();
            res.Bytes = 8;
            res.Autoincrement = true;
            return res;
        }
        protected override string ToStringNoArray()
        {
            return "bigserial";
        }
        public override object Code
        {
            get { return PostgreSqlTypeCode.BigSerial; }
        }
    }

    public class PostgreSqlTypeOid : PostgreSqlTypeBase
    {
        protected override DbTypeBase ToGenericTypeNoArray()
        {
            DbTypeInt res = new DbTypeInt();
            res.Bytes = 4;
            res.SetSpecificAttribute("pgsql", "subtype", "oid");
            return res;
        }
        protected override string ToStringNoArray()
        {
            return "oid";
        }
        public override object Code
        {
            get { return PostgreSqlTypeCode.Oid; }
        }
    }

    public class PostgreSqlTypeNumeric : PostgreSqlTypeBase
    {
        int m_precision = 10;
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

        protected override DbTypeBase ToGenericTypeNoArray()
        {
            DbTypeNumeric res = new DbTypeNumeric();
            res.Precision = Precision;
            res.Scale = Scale;
            res.Autoincrement = true;
            return res;
        }
        protected override string ToStringNoArray()
        {
            string res = String.Format("numeric({0},{1})", Precision, Scale);
            return res;
        }
        public override object Code
        {
            get { return PostgreSqlTypeCode.Numeric; }
        }
    }

    public class PostgreSqlTypeReal : PostgreSqlTypeBase
    {
        protected override DbTypeBase ToGenericTypeNoArray()
        {
            DbTypeFloat res = new DbTypeFloat();
            res.Bytes = 4;
            return res;
        }
        protected override string ToStringNoArray()
        {
            return "real";
        }
        public override object Code
        {
            get { return PostgreSqlTypeCode.Real; }
        }
    }

    public class PostgreSqlTypeDouble : PostgreSqlTypeBase
    {
        protected override DbTypeBase ToGenericTypeNoArray()
        {
            DbTypeFloat res = new DbTypeFloat();
            res.Bytes = 8;
            return res;
        }
        protected override string ToStringNoArray()
        {
            return "double precision";
        }
        public override object Code
        {
            get { return PostgreSqlTypeCode.Double; }
        }
    }

    public class PostgreSqlTypeMoney : PostgreSqlTypeBase
    {
        protected override DbTypeBase ToGenericTypeNoArray()
        {
            DbTypeFloat res = new DbTypeFloat();
            res.Bytes = 10;
            res.IsMoney = true;
            return res;
        }
        protected override string ToStringNoArray()
        {
            return "money";
        }
        public override object Code
        {
            get { return PostgreSqlTypeCode.Money; }
        }
    }

    public abstract class PostgreSqlTypeString : PostgreSqlTypeBase
    {
        int m_length = 50;
        [DatAdmin.DisplayName("s_length")]
        public int Length
        {
            get { return m_length; }
            set { m_length = value; }
        }
    }

    public class PostgreSqlTypeBit : PostgreSqlTypeString
    {
        protected override DbTypeBase ToGenericTypeNoArray()
        {
            DbTypeString res = new DbTypeString();
            res.IsBinary = true;
            res.Length = Length;
            return res;
        }
        protected override string ToStringNoArray()
        {
            return "bit";
        }
        public override object Code
        {
            get { return PostgreSqlTypeCode.Bit; }
        }
    }

    public class PostgreSqlTypeVarBit : PostgreSqlTypeString
    {
        protected override DbTypeBase ToGenericTypeNoArray()
        {
            DbTypeString res = new DbTypeString();
            res.IsBinary = true;
            res.IsVarLength = true;
            res.Length = Length;
            return res;
        }
        protected override string ToStringNoArray()
        {
            return String.Format("varbit({0})", Length);
        }
        public override object Code
        {
            get { return PostgreSqlTypeCode.VarBit; }
        }
    }

    public class PostgreSqlTypeChar : PostgreSqlTypeString
    {
        protected override DbTypeBase ToGenericTypeNoArray()
        {
            DbTypeString res = new DbTypeString();
            res.IsVarLength = false;
            res.Length = Length;
            return res;
        }
        protected override string ToStringNoArray()
        {
            return String.Format("char({0})", Length < 0 ? DEFMAXLEN : Length);
        }
        public override object Code
        {
            get { return PostgreSqlTypeCode.Char; }
        }
    }

    public class PostgreSqlTypeBpChar : PostgreSqlTypeString
    {
        protected override DbTypeBase ToGenericTypeNoArray()
        {
            DbTypeString res = new DbTypeString();
            res.IsVarLength = false;
            res.Length = Length;
            res.SetSpecificAttribute("pgsql", "subtype", "bpchar");
            return res;
        }
        protected override string ToStringNoArray()
        {
            return String.Format("bpchar({0})", Length < 0 ? DEFMAXLEN : Length);
        }
        public override object Code
        {
            get { return PostgreSqlTypeCode.BpChar; }
        }
    }

    public class PostgreSqlTypeVarChar : PostgreSqlTypeString
    {
        protected override DbTypeBase ToGenericTypeNoArray()
        {
            DbTypeString res = new DbTypeString();
            res.IsVarLength = true;
            res.Length = Length;
            return res;
        }
        protected override string ToStringNoArray()
        {
            return String.Format("varchar({0})", Length < 0 ? DEFMAXLEN : Length);
        }
        public override object Code
        {
            get { return PostgreSqlTypeCode.VarChar; }
        }
    }

    public class PostgreSqlTypeDate : PostgreSqlTypeBase
    {
        protected override DbTypeBase ToGenericTypeNoArray()
        {
            DbTypeDatetime res = new DbTypeDatetime();
            res.SubType = DbDatetimeSubType.Date;
            return res;
        }
        protected override string ToStringNoArray()
        {
            return "date";
        }
        public override object Code
        {
            get { return PostgreSqlTypeCode.Date; }
        }
    }

    public class PostgreSqlTypeTime : PostgreSqlTypeBase
    {
        protected override DbTypeBase ToGenericTypeNoArray()
        {
            DbTypeDatetime res = new DbTypeDatetime();
            res.SubType = DbDatetimeSubType.Time;
            return res;
        }
        protected override string ToStringNoArray()
        {
            return "time";
        }
        public override object Code
        {
            get { return PostgreSqlTypeCode.Time; }
        }
    }

    public class PostgreSqlTypeTimeTz : PostgreSqlTypeBase
    {
        protected override DbTypeBase ToGenericTypeNoArray()
        {
            DbTypeDatetime res = new DbTypeDatetime();
            res.SubType = DbDatetimeSubType.Time;
            res.HasTimeZone = true;
            return res;
        }
        protected override string ToStringNoArray()
        {
            return "timetz";
        }
        public override object Code
        {
            get { return PostgreSqlTypeCode.TimeTz; }
        }
    }

    public class PostgreSqlTypeTimestamp : PostgreSqlTypeBase
    {
        protected override DbTypeBase ToGenericTypeNoArray()
        {
            DbTypeDatetime res = new DbTypeDatetime();
            res.SubType = DbDatetimeSubType.Datetime;
            return res;
        }
        protected override string ToStringNoArray()
        {
            return "timestamp";
        }
        public override object Code
        {
            get { return PostgreSqlTypeCode.Timestamp; }
        }
    }

    public class PostgreSqlTypeTimestampTz : PostgreSqlTypeBase
    {
        protected override DbTypeBase ToGenericTypeNoArray()
        {
            DbTypeDatetime res = new DbTypeDatetime();
            res.SubType = DbDatetimeSubType.Datetime;
            res.HasTimeZone = true;
            return res;
        }
        protected override string ToStringNoArray()
        {
            return "timestamptz";
        }
        public override object Code
        {
            get { return PostgreSqlTypeCode.TimestampTz; }
        }
    }

    public class PostgreSqlTypeInterval : PostgreSqlTypeBase
    {
        protected override DbTypeBase ToGenericTypeNoArray()
        {
            DbTypeDatetime res = new DbTypeDatetime();
            res.SubType = DbDatetimeSubType.Interval;
            return res;
        }
        protected override string ToStringNoArray()
        {
            return "interval";
        }
        public override object Code
        {
            get { return PostgreSqlTypeCode.Interval; }
        }
    }

    public class PostgreSqlTypeBoolean : PostgreSqlTypeBase
    {
        protected override DbTypeBase ToGenericTypeNoArray()
        {
            DbTypeLogical res = new DbTypeLogical();
            return res;
        }
        protected override string ToStringNoArray()
        {
            return "boolean";
        }
        public override object Code
        {
            get { return PostgreSqlTypeCode.Boolean; }
        }
    }

    public class PostgreSqlTypeBytea : PostgreSqlTypeBase
    {
        protected override DbTypeBase ToGenericTypeNoArray()
        {
            DbTypeBlob res = new DbTypeBlob();
            return res;
        }
        protected override string ToStringNoArray()
        {
            return "bytea";
        }
        public override object Code
        {
            get { return PostgreSqlTypeCode.Bytea; }
        }
    }

    public class PostgreSqlTypeText : PostgreSqlTypeBase
    {
        protected override DbTypeBase ToGenericTypeNoArray()
        {
            DbTypeText res = new DbTypeText();
            return res;
        }
        protected override string ToStringNoArray()
        {
            return "text";
        }
        public override object Code
        {
            get { return PostgreSqlTypeCode.Text; }
        }
    }

    //public class PostgreSqlTypeArray : PostgreSqlTypeBase
    //{
    //    PostgreSqlTypeBase m_elementType;
    //    public PostgreSqlTypeBase ElementType
    //    {
    //        get { return m_elementType; }
    //        set { m_elementType = value; }
    //    }

    //    //ArrayDimensions m_dims = new ArrayDimensions(new ArrayDimension());
    //    //public ArrayDimensions Dims
    //    //{
    //    //    get { return m_dims; }
    //    //    set { m_dims = value; }
    //    //}

    //    public override DbTypeBase ToGenericType()
    //    {
    //        var res = new DbTypeArray();
    //        //res.Dims = Dims;
    //        res.ElementType = ElementType.ToGenericType();
    //        return res;
    //    }
    //    public override string ToString()
    //    {
    //        return ElementType.ToString() + "[]";
    //    }
    //    public override object Code
    //    {
    //        get { return PostgreSqlTypeCode.Array; }
    //    }
    //}

    // ******** geometric types ***************

    public abstract class PostgreSqlGeometric : PostgreSqlTypeBase
    {
        protected override DbTypeBase ToGenericTypeNoArray()
        {
            DbTypeString res = new DbTypeString();
            res.Length = 255;
            res.SetSpecificAttribute("pgsql", "subtype", SqlName);
            return res;
        }
        protected abstract string SqlName { get;}
        protected override string ToStringNoArray()
        {
            return SqlName;
        }
    }

    public class PostgreSqlTypeBox : PostgreSqlGeometric
    {
        public override object Code
        {
            get { return PostgreSqlTypeCode.Box; }
        }
        protected override string SqlName
        {
            get { return "box"; }
        }
    }
    public class PostgreSqlTypeCircle : PostgreSqlGeometric
    {
        public override object Code
        {
            get { return PostgreSqlTypeCode.Circle; }
        }
        protected override string SqlName
        {
            get { return "circle"; }
        }
    }
    public class PostgreSqlTypeLine : PostgreSqlGeometric
    {
        public override object Code
        {
            get { return PostgreSqlTypeCode.Line; }
        }
        protected override string SqlName
        {
            get { return "line"; }
        }
    }
    public class PostgreSqlTypeLineSeg : PostgreSqlGeometric
    {
        public override object Code
        {
            get { return PostgreSqlTypeCode.LineSeg; }
        }
        protected override string SqlName
        {
            get { return "lseg"; }
        }
    }
    public class PostgreSqlTypePath : PostgreSqlGeometric
    {
        public override object Code
        {
            get { return PostgreSqlTypeCode.Path; }
        }
        protected override string SqlName
        {
            get { return "path"; }
        }
    }
    public class PostgreSqlTypePoint : PostgreSqlGeometric
    {
        public override object Code
        {
            get { return PostgreSqlTypeCode.Point; }
        }
        protected override string SqlName
        {
            get { return "point"; }
        }
    }
    public class PostgreSqlTypePolygon : PostgreSqlGeometric
    {
        public override object Code
        {
            get { return PostgreSqlTypeCode.Polygon; }
        }
        protected override string SqlName
        {
            get { return "polygon"; }
        }
    }

    // ******** network types ********

    public abstract class PostgreSqlNetwork: PostgreSqlTypeBase
    {
        protected override DbTypeBase ToGenericTypeNoArray()
        {
            DbTypeString res = new DbTypeString();
            res.Length = 255;
            res.SetSpecificAttribute("pgsql", "subtype", SqlName);
            return res;
        }
        protected abstract string SqlName { get;}
        protected override string ToStringNoArray()
        {
            return SqlName;
        }
    }

    public class PostgreSqlTypeCidr : PostgreSqlNetwork
    {
        public override object Code
        {
            get { return PostgreSqlTypeCode.Cidr; }
        }
        protected override string SqlName
        {
            get { return "cidr"; }
        }
    }

    public class PostgreSqlTypeInet : PostgreSqlNetwork
    {
        public override object Code
        {
            get { return PostgreSqlTypeCode.Inet; }
        }
        protected override string SqlName
        {
            get { return "inet"; }
        }
    }
    public class PostgreSqlTypeMacAddr : PostgreSqlNetwork
    {
        public override object Code
        {
            get { return PostgreSqlTypeCode.MacAddr; }
        }
        protected override string SqlName
        {
            get { return "macaddr"; }
        }
    }

    public abstract class PostGISType : PostgreSqlTypeBase
    {
        protected override DbTypeBase ToGenericTypeNoArray()
        {
            DbTypeText res = new DbTypeText();
            res.SetSpecificAttribute("pgsql", "subtype", SqlName);
            return res;
        }
        protected abstract string SqlName { get; }
        protected override string ToStringNoArray()
        {
            return SqlName;
        }
    }

    public class PostGISTypeBox3D : PostGISType
    {
        public override object Code
        {
            get { return PostgreSqlTypeCode.Box3D; }
        }
        protected override string SqlName
        {
            get { return "box3d"; }
        }
    }

    public class PostGISTypeBox2D : PostGISType
    {
        public override object Code
        {
            get { return PostgreSqlTypeCode.Box2D; }
        }
        protected override string SqlName
        {
            get { return "box2d"; }
        }
    }

    public class PostGISTypeBox3D_Extent : PostGISType
    {
        public override object Code
        {
            get { return PostgreSqlTypeCode.Box3D_Extent; }
        }
        protected override string SqlName
        {
            get { return "box3d_extent"; }
        }
    }

    public class PostGISTypeGeometry : PostGISType
    {
        public override object Code
        {
            get { return PostgreSqlTypeCode.Geometry; }
        }
        protected override string SqlName
        {
            get { return "geometry"; }
        }
    }

    public class PostGISTypeGeometry_Dump : PostGISType
    {
        public override object Code
        {
            get { return PostgreSqlTypeCode.Geometry_Dump; }
        }
        protected override string SqlName
        {
            get { return "geometry_dump"; }
        }
    }

    public class PostGISTypeGeography : PostGISType
    {
        public override object Code
        {
            get { return PostgreSqlTypeCode.Geography; }
        }
        protected override string SqlName
        {
            get { return "geography"; }
        }
    }

    public class PostgreSqlTypeGeneric : PostgreSqlTypeBase
    {
        string m_sql;
        [DatAdmin.DisplayName("SQL")]
        [XmlAttrib("sql")]
        public string Sql
        {
            get { return m_sql; }
            set { m_sql = value; }
        }

        protected override DbTypeBase ToGenericTypeNoArray()
        {
            return new DbTypeGeneric { Sql = Sql };
        }
        public override object Code
        {
            get { return PostgreSqlTypeCode.Generic; }
        }
        protected override string ToStringNoArray()
        {
            return Sql;
        }
    }
}
