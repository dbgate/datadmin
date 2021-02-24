using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DatAdmin
{
    public static class InMemoryTableExtension
    {
        public static RowType FindRow<RowType>(this IInMemoryTable<RowType> table, string[] pkcols, object[] pkvals)
            where RowType : class, IBedRecord
        {
            foreach (var row in table.Rows)
            {
                if (BedTool.EqualRecords(row.GetValuesByCols(pkcols), pkvals)) return row;
            }
            return null;
        }
    }
}
