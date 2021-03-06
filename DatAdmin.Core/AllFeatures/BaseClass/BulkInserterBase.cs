﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data;

namespace DatAdmin
{
    public class BulkInserterBase : IBulkInserter
    {
        public BulkInserterBase()
        {
            BatchSize = 100;
            CopyOptions = new TableCopyOptions();
        }

        #region IBulkInserter Members

        public TableCopyOptions CopyOptions { get; set; }
        //public bool TruncateBeforeCopy { get; set; }
        //public bool AllowBulkCopy { get; set; }
        public ITableStructure DestinationTable { get; set; }
        public string DatabaseName { get; set; }
        public IProgressInfo ProgressInfo { get; set; }
        public IPhysicalConnection Connection { get; set; }
        public int BatchSize { get; set; }

        public virtual void Run(IDataQueue queue)
        {
            BeforeRun();
            try
            {
                if (CopyOptions.AllowBulkCopy)
                {
                    RunBulkCopy(queue);
                }
                else
                {
                    RunInserts(queue);
                }
            }
            finally
            {
                queue.CloseReading();
            }
            AfterRun();
        }

        #endregion

        protected virtual void BeforeRun()
        {
            if (CopyOptions.TruncateBeforeCopy)
            {
                try
                {
                    Connection.RunScript(dmp => dmp.TruncateTable(DestinationTable.FullName));
                }
                catch (Exception err)
                {
                    ProgressInfo.LogMessage("TRUNCATE", LogLevel.Warning, "Error truncating table:" + err.Message);
                }
            }
            if (CopyOptions.DisableConstraints)
            {
                Connection.RunScript(dmp => dmp.EnableConstraints(DestinationTable.FullName, false));
            }
        }

        protected virtual void AfterRun()
        {
            if (CopyOptions.DisableConstraints)
            {
                Connection.RunScript(dmp => dmp.EnableConstraints(DestinationTable.FullName, true));
            }
        }

        protected bool HasIdentity(IDataQueue queue)
        {
            ITableStructure ts = queue.GetRowFormat;
            ITableStructure dst_ts = DestinationTable;

            IColumnStructure autoinc = dst_ts.FindAutoIncrementColumn();
            bool hasident = false;
            if (autoinc != null)
            {
                if (ts.Columns.Count != dst_ts.Columns.Count)
                {
                    // determine whether auto-inc column is inserted
                    hasident = ts.Columns.IndexOfIf(col => col.ColumnName == autoinc.ColumnName) >= 0;
                }
                else
                {
                    hasident = true;
                }
            }
            return hasident;
        }

        protected virtual void RunInserts(IDataQueue queue)
        {
            Connection.SystemConnection.SafeChangeDatabase(DatabaseName);
            var dda = Connection.GetAnyDDA();
            using (DbCommand inscmd = Connection.DbFactory.CreateCommand())
            {
                List<string> colnames = new List<string>();
                List<string> vals = new List<string>();
                ITableStructure ts = queue.GetRowFormat;
                ITableStructure dst_ts = DestinationTable;
                foreach (IColumnStructure col in ts.Columns)
                {
                    vals.Add("{" + colnames.Count.ToString() + "}");
                    colnames.Add(col.ColumnName);
                }
                string[] values = new string[colnames.Count];
                NameWithSchema table = DestinationTable.FullName;
                string insertTemplate = SqlDumper.Format(Connection.Dialect, "^insert ^into %f (%,i) ^values (%,s)", table, colnames, vals);

                bool hasident = HasIdentity(queue);

                DbTransaction trans = Connection.SystemConnection.BeginTransaction();
                inscmd.Connection = Connection.SystemConnection;
                inscmd.Transaction = trans;

                int okRowCount = 0, failRowCount = 0;
                List<string> insertErrors = new List<string>();
                try
                {
                    if (hasident) Connection.RunScript(dmp => { dmp.AllowIdentityInsert(table, true); }, trans, ProgressInfo);
                    try
                    {
                        int rowcounter = 0;
                        while (!queue.IsEof)
                        {
                            rowcounter++;
                            IBedRecord row = queue.GetRecord();
                            for (int i = 0; i < row.FieldCount; i++)
                            {
                                row.ReadValue(i);
                                values[i] = dda.GetSqlLiteral(row);
                            }
                            inscmd.CommandText = String.Format(insertTemplate, values);

                            if (rowcounter > 10000)
                            {
                                // next transaction
                                trans.Commit();
                                trans.Dispose();
                                trans = Connection.SystemConnection.BeginTransaction();
                                inscmd.Transaction = trans;
                                rowcounter = 0;
                            }
                            try
                            {
                                inscmd.ExecuteNonQuery();
                                okRowCount++;
                            }
                            catch (Exception err)
                            {
                                if (insertErrors.Count < 10)
                                {
                                    StringBuilder msg = new StringBuilder();
                                    msg.Append(err.Message);
                                    insertErrors.Add(msg.ToString());
                                }
                                failRowCount++;
                            }
                        }
                    }
                    finally
                    {
                        if (hasident) Connection.RunScript(dmp => { dmp.AllowIdentityInsert(table, false); }, trans, ProgressInfo);
                    }
                    trans.Commit();
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
                catch (Exception)
                {
                    trans.Rollback();
                    throw;
                }
            }
        }

        protected virtual void RunBulkCopy(IDataQueue queue)
        {
            RunInserts(queue);
        }
    }
}
