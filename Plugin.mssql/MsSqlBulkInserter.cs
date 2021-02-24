using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.Data.SqlClient;
using System.Data;

namespace Plugin.mssql
{
    public class MsSqlBulkInserter : BulkInserterBase
    {
        protected override void RunBulkCopy(IDataQueue queue)
        {
            ITableStructure ts = queue.GetRowFormat;
            if (ts.Columns.Count == 1)
            { // SqlBulkCopy has problems when running on tables with one column
                RunInserts(queue);
                return;
            }
            using (SqlBulkCopy bcp = new SqlBulkCopy((SqlConnection)Connection.SystemConnection, SqlBulkCopyOptions.KeepIdentity | SqlBulkCopyOptions.KeepNulls, null))
            {
                bcp.DestinationTableName = Connection.Dialect.QuoteFullName(DestinationTable.FullName);
                ITableStructure dst_ts = DestinationTable;
                if (ts.Columns.Count < dst_ts.Columns.Count)
                {
                    int srcindex = 0;
                    foreach (var src in ts.Columns)
                    {
                        SqlBulkCopyColumnMapping map = new SqlBulkCopyColumnMapping(srcindex, dst_ts.Columns.IndexOfIf(col => col.ColumnName == src.ColumnName));
                        bcp.ColumnMappings.Add(map);
                        srcindex++;
                    }
                }
                DataQueueReaderAdapter reader = new DataQueueReaderAdapter(queue);
                try
                {
                    bcp.BulkCopyTimeout = 0;
                    bcp.WriteToServer(reader);
                    ProgressInfo.LogMessage("INSERT", LogLevel.Info, Texts.Get("s_inserted_into_table$table$rows", "table", DestinationTable.FullName, "rows", reader.ReadedRows));
                }
                catch (Exception err)
                {
                    ILogger logger = ProgressInfo;
                    if (err is QueueClosedError) logger = Logging.Root;
                    logger.LogMessageDetail(
                        "INSERT", LogLevel.Error,
                        String.Format("{0}", Texts.Get("s_error_inserting_into_table$table", "table", DestinationTable.FullName)), err.ToString());
                    throw;
                }
                finally
                {
                    reader.Close();
                }
            }
        }
    }
}
