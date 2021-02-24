using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;

namespace Plugin.mssql
{
    public class MsSqlDataSynAdapter : DataSynAdapterBase
    {
        public MsSqlDataSynAdapter(ISqlDialect dialect)
            : base(dialect)
        {
        }

        public override string Md5(string expr)
        {
            return String.Format("HashBytes('MD5', ({0}))", expr);
        }

        public override string GetHashableString(string expr, DbTypeBase type)
        {
            if (type is DbTypeDatetime)
            {
                return String.Format("convert(varchar,datepart(year, {0})) + '-' + " 
                    + "convert(varchar,datepart(month, {0})) + '-' +"
                    + "convert(varchar,datepart(day, {0})) + '-' +"
                    + "convert(varchar,datepart(hour, {0})) + '-' +"
                    + "convert(varchar,datepart(minute, {0})) + '-' +"
                    + "convert(varchar,datepart(second, {0}))", expr);
            }
            if (type is DbTypeText)
            {
                if (((DbTypeText)type).IsUnicode) return String.Format("convert(nvarchar,{0})", expr);
                else return String.Format("convert(varchar,{0})", expr);
            }
            if (type is DbTypeNumber || type is DbTypeLogical)
            {
                return String.Format("convert(varchar,{0})", expr);
            }
            if (type is DbTypeBlob)
            {
                return String.Format("convert(varchar(8000),convert(varbinary(8000), {0}))", expr);
            }
            if (type is DbTypeXml)
            {
                return String.Format("convert(varchar(8000),convert(nvarchar(max),{0}))", expr);
            }
            var stype = type as DbTypeString;
            if (stype != null)
            {
                if (stype.IsUnicode) return String.Format("convert(nvarchar(4000),{0})", expr);
                return String.Format("convert(varchar(8000),{0})", expr);
            }
            return base.GetHashableString(expr, type);
        }
    }
}
