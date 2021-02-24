using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DatAdmin
{
    public static class DataTableExtension
    {
        public static int GetOrdinal(this DataColumnCollection cols, string name)
        {
            int i = 0;
            foreach (DataColumn col in cols)
            {
                if (col.ColumnName.ToUpper() == name.ToUpper()) return i;
                i++;
            }
            return -1;
        }

        public static int SafeOrdinal(this DataTable table, params string[] fieldVariants)
        {
            foreach (string field in fieldVariants)
            {
                int ord = table.Columns.GetOrdinal(field);
                if (ord >= 0) return ord;
            }
            return -1;
        }

        public static ITableStructure GetTableStructure(this DataColumnCollection columns, string name)
        {
            TableStructure res = new TableStructure();
            //res.FilledMembers |= TableStructureMembers.ColumnNames | TableStructureMembers.ColumnTypes;
            foreach (DataColumn col in columns.SortedByKey<DataColumn, int>(col => col.Ordinal))
            {
                var c = res.AddColumn(col.ColumnName, TypeTool.GetDatAdminType(col.DataType));
                c.IsNullable = col.AllowDBNull;
                c.DefaultValue = SqlExpression.ParseDefaultValue(col.DefaultValue.SafeToString(), null);
            }
            return res;
        }

        public static DataTable SelectNewTable(this DataTable src, string cond, string sort)
        {
            DataRow[] rows = src.Select(cond, sort);
            DataTable res = src.Clone();
            foreach (DataRow row in rows) res.ImportRow(row);
            return res;
        }

        public static BedTable ToBinaryTable(this DataTable table)
        {
            var ts = table.Columns.GetTableStructure("table");
            BedTable res = new BedTable(ts);
            foreach (DataRow row in table.Rows)
            {
                res.AddRow(new DataRecordAdapter(new DataRowAdapter(row), ts));
            }
            return res;
        }
    }
}
