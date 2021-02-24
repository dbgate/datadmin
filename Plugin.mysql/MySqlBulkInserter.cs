using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.IO;
using System.Data;
using System.Linq;
using System.Data.Common;

namespace Plugin.mysql
{
    public class MySqlBulkInserter : BulkInserterBase
    {
        public override void Run(IDataQueue queue)
        {
            string oldmode = Connection.SystemConnection.ExecuteScalar("SELECT @@SQL_MODE").ToString();
            try
            {
                Connection.SystemConnection.ExecuteNonQuery("SET SQL_MODE='NO_AUTO_VALUE_ON_ZERO'");
                base.Run(queue);
            }
            finally
            {
                Connection.SystemConnection.ExecuteNonQuery("SET SQL_MODE='" + oldmode + "'");
            }
        }

        protected override void RunBulkCopy(IDataQueue queue)
        {
            int recsInBatch = 0;
            int okRowCount = 0, failRowCount = 0;
            List<string> insertErrors = new List<string>();
            StringWriter buf = null;
            ISqlDumper dmp = null;
            int maxpacket = Int32.Parse(Connection.SystemConnection.ExecuteScalar("select @@max_allowed_packet").ToString());
            using (DbTransaction tran = Connection.SystemConnection.BeginTransaction())
            {
                try
                {
                    ITableStructure dst = queue.GetRowFormat;
                    while (!queue.IsEof)
                    {
                        IBedRecord rec = queue.GetRecord();
                        if (recsInBatch == 0)
                        {
                            buf = new StringWriter();
                            dmp = Connection.Dialect.CreateDumper(buf);
                            dmp.Put("^insert ^into %f (%,i) ^values ", DestinationTable.FullName,
                                from c in dst.Columns select c.ColumnName);
                        }
                        else
                        {
                            dmp.Put(", ");
                        }
                        dmp.Put("(%,v)", rec);
                        recsInBatch++;
                        if (recsInBatch >= BatchSize || buf.GetStringBuilder().Length > maxpacket / 4)
                        {
                            string sql = buf.ToString();
                            int bytes = (int)(Encoding.UTF32.GetByteCount(sql) + 0x200);
                            try
                            {
                                Connection.SystemConnection.ExecuteNonQuery(sql, Connection.Dialect);
                                okRowCount += recsInBatch;
                            }
                            catch (Exception err)
                            {
                                failRowCount += recsInBatch;
                                if (insertErrors.Count < 10) insertErrors.Add(err.Message);
                            }
                            recsInBatch = 0;
                        }
                    }
                    if (recsInBatch > 0)
                    {
                        Connection.SystemConnection.ExecuteNonQuery(buf.ToString(), Connection.Dialect);
                        okRowCount += recsInBatch;
                    }
                }
                catch (Exception err)
                {
                    tran.Rollback();
                    ProgressInfo.LogMessageDetail(
                        "INSERT", LogLevel.Error,
                        String.Format("{0}", Texts.Get("s_error_inserting_into_table$table", "table", DestinationTable.FullName)), err.ToString());
                    throw;
                }
                tran.Commit();

                if (failRowCount > 0)
                {
                    ProgressInfo.LogMessageDetail(
                        "INSERT", LogLevel.Error,
                        String.Format("{0}, OK:{1}, FAIL:{2}", Texts.Get("s_error_inserting_into_table$table", "table", DestinationTable.FullName), okRowCount, failRowCount),
                        insertErrors.CreateDelimitedText("\r\n")
                        );
                }
                else
                {
                    ProgressInfo.LogMessage("INSERT", LogLevel.Info, Texts.Get("s_inserted_into_table$table$rows", "table", DestinationTable.FullName, "rows", okRowCount));
                }

            }
        }
    }
}
