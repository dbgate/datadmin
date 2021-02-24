using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DatAdmin
{
    public static class AnalysingExtensions
    {
        public static readonly string[] COLDEFAULT = new string[] { "COLUMN_DEFAULT", "COLUMN_DEF", "DATA_DEFAULT" };
        public static readonly string[] SCHEMACOL = new string[] { "TABLE_SCHEMA", "TABLE_SCHEM", "OWNER", "TABLE_OWNER" };
        public static readonly string[] TABLECOL = new string[] { "TABLE_NAME" };
        public static readonly string[] TABLETYPECOL = new string[] { "TABLE_TYPE", "TYPE" };
        public static readonly string[] CHARLENGTH = new string[] { "CHARACTER_MAXIMUM_LENGTH", "LENGTH", "COLUMN_SIZE", "CHAR_LENGTH" };
        public static readonly string[] SCALE = new string[] { "SCALE", "NUMERIC_SCALE", "DECIMAL_DIGITS", "DATA_SCALE" };
        public static readonly string[] PRECISION = new string[] { "PRECISION", "NUMERIC_PRECISION", "RADIX", "NUM_PREC_RADIX", "DATA_PRECISION" };

        public static string ColumnDefault(this IDataRecord row)
        {
            return row.SafeString(COLDEFAULT);
        }
        public static string ColumnDefault(this DataRow row)
        {
            return row.SafeString(COLDEFAULT);
        }

        public static int CharLength(this IDataRecord row)
        {
            return row.SafeString(CHARLENGTH).SafeIntParse();
        }
        public static int CharLength(this DataRow row)
        {
            return row.SafeString(CHARLENGTH).SafeIntParse();
        }

        public static int Precision(this IDataRecord row)
        {
            return row.SafeString(PRECISION).SafeIntParse(); 
        }
        public static int? NPrecision(this IDataRecord row)
        {
            return row.SafeString(PRECISION).SafeNIntParse();
        }
        public static int Precision(this DataRow row)
        {
            return row.SafeString(PRECISION).SafeIntParse();
        }
        public static int? NPrecision(this DataRow row)
        {
            return row.SafeString(PRECISION).SafeNIntParse();
        }

        public static int Scale(this IDataRecord row)
        {
            return row.SafeString(SCALE).SafeIntParse();
        }
        public static int? NScale(this IDataRecord row)
        {
            return row.SafeString(SCALE).SafeNIntParse();
        }
        public static int Scale(this DataRow row)
        {
            return row.SafeString(SCALE).SafeIntParse();
        }
        public static int? NScale(this DataRow row)
        {
            return row.SafeString(SCALE).SafeNIntParse();
        }

        public static string DataTypeName(this IDataRecord row)
        {
            string res = row.SafeString("data_type");
            if (res.SafeIntParse() != 0) res = row.SafeString("type_name");
            return res ?? "";
        }
        public static string DataTypeName(this DataRow row)
        {
            string res = row.SafeString("data_type");
            if (res.SafeIntParse() != 0) res = row.SafeString("type_name");
            return res ?? "";
        }
    }
}
