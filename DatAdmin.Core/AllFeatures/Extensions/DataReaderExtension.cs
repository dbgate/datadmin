using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;

namespace DatAdmin
{
    public static class DataReaderExtension
    {
        //public static BedTable ToBinaryTable(this IDataReader reader, int? maximumRecords)
        //{
        //    ITableStructure ts = reader.GetTableStructure(null);
        //    BedTable dt = new BedTable(ts);
        //    int allow_recs = maximumRecords != null ? maximumRecords.Value : -1;
        //    while (reader.Read() && (maximumRecords == null || allow_recs > 0))
        //    {
        //        dt.AddRow(reader);
        //        allow_recs--;
        //    }
        //    return dt;
        //}

        //public static BedTable ToBinaryTable(this IDataReader reader)
        //{
        //    return ToBinaryTable(reader, null);
        //}

        public static DataTable ToDataTable(this IDataReader reader, int? maximumRecords)
        {
            ITableStructure ts = reader.GetTableStructure(null);
            DataTable dt = ConnTools.DataTableFromStructure(ts);
            int allow_recs = maximumRecords != null ? maximumRecords.Value : -1;
            while (reader.Read() && (maximumRecords == null || allow_recs > 0))
            {
                DataRow row = dt.NewRow();
                for (int i = 0; i < ts.Columns.Count; i++)
                {
                    try
                    {
                        row[i] = reader[i];
                    }
                    catch (Exception)
                    {
                        row[i] = DBNull.Value;
                    }
                }
                dt.Rows.Add(row);
                allow_recs--;
            }
            return dt;
        }

        public static DataTable ToDataTable(this IDataReader reader)
        {
            return ToDataTable(reader, null);
        }

        public static DbTypeBase ReaderDataType(DataRow row)
        {
            try
            {
                string tp = row["DataTypeName"].SafeToString();
                if (tp == "xml") return new DbTypeXml();
                int size = row.SafeString("ColumnSize").SafeIntParse();
                if (tp == "varchar") return new DbTypeString { Length = size };
                if (tp == "nvarchar") return new DbTypeString { Length = size, IsUnicode = true };
                if (tp == "text") return new DbTypeText();
                if (tp == "ntext") return new DbTypeText { IsUnicode = true };
            }
            catch { }
            return TypeTool.GetDatAdminType((Type)row["DataType"]);
        }

        public static TableStructure SchemaTableToStructure(DataTable schemaTable, ISqlDialect dialect)
        {
            TableStructure res = new TableStructure();
            PrimaryKey pk = new PrimaryKey();
            foreach (DataRow row in schemaTable.Rows.SortedByKey<DataRow, int>(row => Int32.Parse(row["ColumnOrdinal"].ToString())))
            {
                if (row.SafeBool("IsHidden", false)) continue;

                var col = new ColumnStructure();
                col.ColumnName = row.SafeString("ColumnName");
                col.IsNullable = (bool)row["AllowDBNull"];
                if (dialect != null) col.DataType = dialect.ReaderDataType(row);
                else col.DataType = ReaderDataType(row);

                string schema = row.SafeString("BaseSchemaName");
                string table = row.SafeString("BaseTableName");
                if (table != null && res.FullName == null) res.FullName = new NameWithSchema(schema, table);
                if (row.SafeBool("IsAutoIncrement", false)) col.DataType.SetAutoincrement(true);

                if (row.SafeBool("IsKey", false))
                {
                    pk.Columns.Add(new ColumnReference(col.ColumnName));
                }
                res._Columns.Add(col);
            }
            if (pk.Columns.Count > 0) res._Constraints.Add(pk);
            return res;

        }

        public static TableStructure GetTableStructure(this IDataReader reader, ISqlDialect dialect)
        {
            DataTable columns;
            try
            {
                columns = reader.GetSchemaTable();
            }
            catch
            {
                return null;
            }
            if (columns == null) return null;
            return SchemaTableToStructure(columns, dialect);
        }

        public static string[] GetFieldNames(this IDataRecord record)
        {
            string[] res = new string[record.FieldCount];
            for (int i = 0; i < res.Length; i++) res[i] = record.GetName(i);
            return res;
        }

        public static object[] GetValues(this IDataRecord record)
        {
            object[] values = new object[record.FieldCount];
            record.GetValues(values);
            return values;
        }

        public static object[] GetValuesByCols(this IDataRecord record, string[] cols)
        {
            object[] values = new object[cols.Length];
            for (int i = 0; i < cols.Length; i++) values[i] = record[cols[i]];
            return values;
        }

        public static string SafeString(this IDataRecord record, string field)
        {
            try
            {
                int ord = record.GetOrdinal(field);
                if (ord >= 0) return record[ord].SafeToString();
                return null;
            }
            catch
            {
                return null;
            }
        }

        public static string SafeString(this IDataRecord record, params string[] fieldVariants)
        {
            foreach (string field in fieldVariants)
            {
                try
                {
                    int ord = record.GetOrdinal(field);
                    if (ord >= 0) return record[ord].SafeToString();
                }
                catch
                {
                    continue;
                }
            }
            return null;
        }

        public static string SafeString(this IDataRecord record, int ord)
        {
            if (ord < 0) return null;
            return record[ord].SafeToString();
        }
    }
}
