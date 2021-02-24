using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.Data;
using System.Data.OleDb;

namespace Plugin.access
{
    public class AccessAnalyser : DatabaseAnalyser
    {
        protected override void LoadTableColumns(TableStructure table)
        {
            OleDbConnection c = (OleDbConnection)GetDbConn();

            Dictionary<int, string> typenames = GetTypeNames(m_conn);
            DataTable cols = c.GetSchema("Columns", new string[] { null, null, table.FullName.Name }).SelectNewTable("1=1", "ORDINAL_POSITION ASC");
            foreach (DataRow row in cols.Rows)
            {
                ColumnStructure col = new ColumnStructure();
                col.ColumnName = row["COLUMN_NAME"].ToString();
                col.IsNullable = (bool)row["IS_NULLABLE"];
                col.DefaultValue = SqlExpression.ParseDefaultValue((row.ColumnDefault() ?? "").ToString(), m_dialect);
                col.DataType = MakeSpecificAccessType(new DataRowAdapter(row), typenames).ToGenericType();
                int colflags = Int32.Parse(row["COLUMN_FLAGS"].ToString());
                //if ((colflags & 0x20) == 0) col.DataType.SetAutoincrement(true);
                table._Columns.Add(col);
            }

            //table.FilledMembers |= TableStructureMembers.ColumnNames | TableStructureMembers.ColumnTypes;
        }

        protected override void LoadConstraints(TableStructure table)
        {
            OleDbConnection c = (OleDbConnection)GetDbConn();

            TableAnalyser tad = new TableAnalyser();
            DataTable pks = c.GetOleDbSchemaTable(OleDbSchemaGuid.Primary_Keys, null);
            foreach (DataRow row in pks.Rows)
            {
                if (row["TABLE_NAME"].ToString() != table.FullName.Name) continue;
                TableAnalyser.Col col = new TableAnalyser.Col();
                col.keytype = "PRIMARY KEY";
                col.ordinal = row["ORDINAL"].ToString();
                col.keyname = row["PK_NAME"].ToString();
                col.colname = row["COLUMN_NAME"].ToString();
                col.tblname = row["TABLE_NAME"].ToString();
                tad.cols.Add(col);
            }

            DataTable fks = c.GetOleDbSchemaTable(OleDbSchemaGuid.Foreign_Keys, null);
            foreach (DataRow row in fks.Rows)
            {
                TableAnalyser.Col col = new TableAnalyser.Col();
                col.keytype = "FOREIGN KEY";
                col.keyname = row["FK_NAME"].ToString();
                col.ordinal = row["ORDINAL"].ToString();
                col.colname = row["FK_COLUMN_NAME"].ToString();
                col.tblname = row["FK_TABLE_NAME"].ToString();
                col.dstcolname = row["PK_COLUMN_NAME"].ToString();
                col.dsttblname = row["PK_TABLE_NAME"].ToString();
                tad.cols.Add(col);
            }

            tad.SaveConstraints(table, this);
            //table.FilledMembers |= m_members.TableMembers.FilterConstraints(false);
        }

        private Dictionary<int, string> GetTypeNames(IPhysicalConnection conn)
        {
            var res = (Dictionary<int, string>)conn.Cache.Get("access_typenames");
            if (res != null) return res;
            DataTable tbl = conn.SystemConnection.GetSchema("DataTypes");
            Dictionary<int, string> names = new Dictionary<int, string>();
            foreach (DataRow row in tbl.Rows)
            {
                int code = Int32.Parse(row["NativeDataType"].ToString());
                string tp = row["TypeName"].ToString();
                names[code] = tp;
            }
            conn.Cache.Put("access_typenames", names);
            return names;
        }

        private AccessTypeBase MakeSpecificAccessType(IDataRecord row, Dictionary<int, string> typenames)
        {
            string dt = typenames[Int32.Parse(row["DATA_TYPE"].ToString())];
            int len = row.CharLength(), prec = row.Precision(), scale = row.Scale();
            return MakeSpecificAccessType(dt, len, prec, scale);
        }

        private AccessTypeBase MakeSpecificAccessType(string dt, int len, int prec, int scale)
        {
            switch (dt.ToLower())
            {
                case "short":
                    return new AccessTypeSmallInt();
                case "long":
                    return new AccessTypeInteger();
                case "single":
                    return new AccessTypeReal();
                case "double":
                    return new AccessTypeFloat();
                case "currency":
                    return new AccessTypeMoney();
                case "datetime":
                    return new AccessTypeDatetime();
                case "bit":
                    return new AccessTypeBit();
                case "byte":
                    return new AccessTypeTinyInt();
                case "guid":
                    return new AccessTypeUniqueIdentifier();
                case "longbinary":
                case "bigbinary":
                    return new AccessTypeImage();
                case "varbinary":
                    return new AccessTypeBinary();
                case "longtext":
                    return new AccessTypeText();
                case "varchar":
                    {
                        if (len == 0) return new AccessTypeText();
                        AccessTypeCharacter res = new AccessTypeCharacter();
                        res.Length = len;
                        return res;
                    }
                case "decimal":
                    {
                        AccessTypeDecimal res = new AccessTypeDecimal();
                        res.Precision = prec;
                        res.Scale = scale;
                        return res;
                    }
            }
            ReportUnknownType(dt);
            return new AccessTypeGeneric { Sql = dt };
            //throw new DbAnalyseError(String.Format("Unknown MS Access type:{0}", dt));
        }

        protected override DbTypeBase AnalyseType(IDataRecord row, IPhysicalConnection conn, bool isdomain)
        {
            AccessTypeBase res = MakeSpecificAccessType(row, GetTypeNames(conn));
            return res.ToGenericType();
        }

        protected override bool IsColumnNullable(IDataRecord row)
        {
            return (bool)row["IS_NULLABLE"];
        }
    }
}
