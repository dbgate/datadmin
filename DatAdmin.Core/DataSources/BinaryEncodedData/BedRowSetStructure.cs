using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DatAdmin
{
    //public sealed class BedRowSetStructure : IBedRowSetStructure
    //{
    //    bool m_hasPrimaryKey;
    //    //TableStructure m_structure;
    //    List<BedColumnInfo> m_columns;
    //    Dictionary<string, int> m_columnOrdinals = new Dictionary<string, int>();

    //    #region IBedRowSetStructure Members

    //    //public ITableStructure TableStructure
    //    //{
    //    //    get { return m_structure; }
    //    //}

    //    public BedColumnInfo this[int index]
    //    {
    //        get { return m_columns[index]; }
    //    }

    //    public int ColumnCount
    //    {
    //        get { return m_columns.Count; }
    //    }

    //    public bool HasPrimaryKey
    //    {
    //        get { return m_hasPrimaryKey; }
    //    }

    //    #endregion

    //    private BedRowSetStructure() { }

    //    public static BedRowSetStructure FromDataReader(IDataReader reader)
    //    {
    //        BedRowSetStructure res = new BedRowSetStructure();
    //        DataTable schemaTable = reader.GetSchemaTable();
    //        res.m_structure = DataReaderExtension.SchemaTableToStructure(schemaTable, "table");
    //        res.m_columns = new List<BedColumnInfo>();
    //        res.m_hasPrimaryKey = false;
    //        int ordinal = 0;
    //        foreach (DataRow row in schemaTable.Rows.SortedByKey<DataRow, int>(row => Int32.Parse(row["ColumnOrdinal"].ToString())))
    //        {
    //            if (row.SafeBool("IsHidden", false)) continue;
    //            var col = new BedColumnInfo();
    //            res.m_columns.Add(col);

    //            string schema = row.SafeString("BaseSchemaName");
    //            string table = row.SafeString("BaseTableName");
    //            if (table != null) col.BaseTable = new NameWithSchema(schema, table);
    //            col.IsAutoIncrement = row.SafeBool("IsAutoIncrement", false);
    //            col.IsPrimaryKey = row.SafeBool("IsKey", false);
    //            col.BaseColumn = row.SafeString("BaseColumnName");
    //            col.ColumnName = row.SafeString("ColumnName");
    //            if (col.IsPrimaryKey) res.m_hasPrimaryKey = true;
    //            col.Ordinal = ordinal;
    //            res.m_columnOrdinals[col.ColumnName] = ordinal;
    //            ordinal++;
    //        }
    //        if (res.m_hasPrimaryKey)
    //        {
    //            PrimaryKey pk = new PrimaryKey();
    //            foreach (var col in res.m_columns)
    //            {
    //                if (col.IsPrimaryKey) pk.Columns.Add(new ColumnReference(col.ColumnName));
    //            }
    //            res.m_structure._Constraints.Add(pk);
    //        }
    //        return res;
    //    }

    //    public int GetOrdinal(string colName)
    //    {
    //        return m_columnOrdinals.Get(colName);
    //    }
    //}
}
