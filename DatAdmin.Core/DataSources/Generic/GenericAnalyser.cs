using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DatAdmin
{
    public class GenericAnalyser : DatabaseAnalyser
    {
        protected override DbTypeBase AnalyseType(System.Data.IDataRecord row, IPhysicalConnection conn, bool isdomain)
        {
            try
            {
                string dt = row.DataTypeName().ToLower();
                int len = row.CharLength(), prec = row.Precision(), scale = row.Scale();
                if (dt.Contains("numeric") || dt.Contains("decimal"))
                {
                    var res = new DbTypeNumeric();
                    res.Precision = prec;
                    res.Scale = scale;
                    return res;
                }
                if (dt.Contains("int"))
                {
                    var res = new DbTypeInt();
                    return res;
                }
                if (dt.Contains("varchar"))
                {
                    var res = new DbTypeString();
                    res.Length = len;
                    res.IsVarLength = true;
                    if (res.Length < 1) res.Length = 50;
                    if (dt.Contains("nvarchar")) res.IsUnicode = true;
                    return res;
                }
                if (dt.Contains("char"))
                {
                    var res = new DbTypeString();
                    res.Length = len;
                    res.IsVarLength = false;
                    if (res.Length < 1) res.Length = 50;
                    if (dt.Contains("nchar")) res.IsUnicode = true;
                    return res;
                }
                if (dt.Contains("time") || dt.Contains("date"))
                {
                    var res = new DbTypeDatetime();
                    return res;
                }
                if (dt.Contains("float") || dt.Contains("single") || dt.Contains("double") || dt.Contains("real"))
                {
                    var res = new DbTypeFloat();
                    return res;
                }
                if (dt.Contains("text"))
                {
                    var res = new DbTypeText();
                    if (dt.Contains("ntext")) res.IsUnicode = true;
                    return res;
                }
                if (dt.Contains("blob") || dt.Contains("image") || dt.Contains("bin"))
                {
                    var res = new DbTypeBlob();
                    return res;
                }
                if (dt.Contains("bool") || dt.Contains("bit") || dt.Contains("logical"))
                {
                    var res = new DbTypeLogical();
                    return res;
                }
                if (dt.Contains("xml"))
                {
                    var res = new DbTypeXml();
                    return res;
                }
                ReportUnknownType(dt);
                return new DbTypeGeneric { Sql = dt };
            }
            catch (Exception)
            {
                return DbTypeGeneric.Unknown();
            }
        }

        protected override bool IsColumnNullable(IDataRecord row)
        {
            try
            {
                object nullable = row["IS_NULLABLE"];

                if (nullable is string)
                {
                    string snullable = nullable.ToString().ToUpper();
                    return snullable == "YES" || snullable == "1" || snullable == "TRUE";
                }
                else
                {
                    if (nullable is bool) return (bool)nullable;
                }

                return true;
            }
            catch (Exception)
            {
                return true;
            }
        }

    }
}
