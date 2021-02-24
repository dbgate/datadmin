using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;

namespace DatAdmin
{
    //public static class ConnTools
    //{
    //    public static void InvokeVoid(IPhysicalConnection conn, SimpleCallback callback, IInvoker invoker, SimpleCallback onfinish)
    //    {
    //        IAsyncVoid async = conn.Invoker.InvokeVoid(callback);
    //        async.OnFinish(onfinish, invoker);
    //    }
    //    public static void InvokeVoid(IPhysicalConnection conn, SimpleCallback callback)
    //    {
    //        IAsyncVoid async = conn.Invoker.InvokeVoid(callback);
    //    }
    //    public static void InvokeVoid(ICommonSource conn, SimpleCallback callback, IInvoker invoker, SimpleCallback onfinish)
    //    {
    //        InvokeVoid(conn.Connection, callback, invoker, onfinish);
    //    }
    //}

    public static class ConnTools
    {
        public static PhysicalConnectionDelegate ChangeDatabaseCallback(string dbname)
        {
            return delegate(IPhysicalConnection conn) { conn.SystemConnection.SafeChangeDatabase(dbname); };
        }

        public static DataTable DataTableFromStructure(ITableStructure tableStruct)
        {
            DataTable table = new DataTable();
            foreach (IColumnStructure col in tableStruct.Columns)
            {
                DataColumn column = new DataColumn(col.ColumnName, col.DataType.DotNetType);
                table.Columns.Add(column);
            }
            return table;
        }

        //public static DataTable DataTableFromSchema(DataTable schemaTable)
        //{
        //    return DbStructureFactory.TableFromReader(null, schemaTable);
        //    //DataTable table = new DataTable();
        //    //for (int i = 0; i < schemaTable.Rows.Count; i++)
        //    //{
        //    //    DataRow row = schemaTable.Rows[i];
        //    //    string columnName = (string)row["ColumnName"];
        //    //    DataColumn column = new DataColumn(columnName, (Type)row["DataType"]);
        //    //    table.Columns.Add(column);
        //    //}
        //    //return table;
        //}

        public static DataTable CreateEmptySchema()
        {
            DataTable schema = new DataTable();

            schema.Columns.Add(SchemaTableColumn.ColumnName, typeof(String));
            schema.Columns.Add(SchemaTableColumn.ColumnOrdinal, typeof(Int32));
            schema.Columns.Add(SchemaTableColumn.ColumnSize, typeof(Int32));
            schema.Columns.Add(SchemaTableColumn.NumericPrecision, typeof(Int32));
            schema.Columns.Add(SchemaTableColumn.NumericScale, typeof(Int32));
            schema.Columns.Add(SchemaTableColumn.DataType, typeof(Type));
            schema.Columns.Add(SchemaTableColumn.ProviderType, typeof(Int32));
            schema.Columns.Add(SchemaTableColumn.IsLong, typeof(Boolean));
            schema.Columns.Add(SchemaTableColumn.AllowDBNull, typeof(Boolean));
            schema.Columns.Add(SchemaTableOptionalColumn.IsReadOnly, typeof(Boolean));
            schema.Columns.Add(SchemaTableOptionalColumn.IsRowVersion, typeof(Boolean));
            schema.Columns.Add(SchemaTableColumn.IsUnique, typeof(Boolean));
            schema.Columns.Add(SchemaTableColumn.IsKey, typeof(Boolean));
            schema.Columns.Add(SchemaTableOptionalColumn.IsAutoIncrement, typeof(Boolean));
            schema.Columns.Add(SchemaTableColumn.BaseSchemaName, typeof(String));
            schema.Columns.Add(SchemaTableOptionalColumn.BaseCatalogName, typeof(String));
            schema.Columns.Add(SchemaTableColumn.BaseTableName, typeof(String));
            schema.Columns.Add(SchemaTableColumn.BaseColumnName, typeof(String));

            return schema;
        }

        public static DataTable SchemaFromStructure(this ITableStructure tableStruct)
        {
            DataTable schema = CreateEmptySchema();
            int index = 1;
            foreach (IColumnStructure col in tableStruct.Columns)
            {
                DataRow row = schema.NewRow();

                row[SchemaTableColumn.ColumnName] = col.ColumnName;
                row[SchemaTableColumn.ColumnOrdinal] = index;
                row[SchemaTableColumn.ColumnSize] = 0;
                row[SchemaTableColumn.NumericPrecision] = 0;
                row[SchemaTableColumn.NumericScale] = 0;
                row[SchemaTableColumn.DataType] = col.DataType.DotNetType;
                row[SchemaTableColumn.ProviderType] = (int)TypeTool.GetProviderType(col.DataType.DotNetType);
                row[SchemaTableColumn.IsLong] = false;
                row[SchemaTableColumn.AllowDBNull] = true;
                row[SchemaTableOptionalColumn.IsReadOnly] = false;
                row[SchemaTableOptionalColumn.IsRowVersion] = false;
                row[SchemaTableColumn.IsUnique] = false;
                row[SchemaTableColumn.IsKey] = false;
                row[SchemaTableOptionalColumn.IsAutoIncrement] = false;
                row[SchemaTableColumn.BaseSchemaName] = "";
                row[SchemaTableOptionalColumn.BaseCatalogName] = "";
                row[SchemaTableColumn.BaseTableName] = "";
                row[SchemaTableColumn.BaseColumnName] = col.ColumnName;

                schema.Rows.Add(row);

                index++;
            }
            return schema;
        }

        public static string GenerateTempTableName(int id)
        {
            return "temp_table_" + id.ToString() + "_" + DateTime.UtcNow.ToFileTime().ToString();
        }


        //public static List<string> GetTableNames(IDatabaseSource conn)
        //{
        //    List<string> res = null;
        //    IAsyncResult async = conn.Connection.BeginInvoke(
        //        (SimpleCallback)delegate() { res = new List<string>(conn.TableNames); },
        //        null);
        //    Async.WaitFor(async);
        //    conn.Connection.EndInvoke(async);
        //    return res;
        //}


    }
}
