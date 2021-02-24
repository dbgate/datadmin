using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.Data.Common;
using DatAdmin;
using System.Globalization;
using Npgsql;

namespace Plugin.postgre
{
    public class PostgreBulkInserter : BulkInserterBase
    {
        protected override void RunBulkCopy(IDataQueue queue)
        {
            int okRowCount = 0, failRowCount = 0;
            List<string> insertErrors = new List<string>();

            ITableStructure dst = queue.GetRowFormat;

            var conn = (NpgsqlConnection)Connection.SystemConnection;
            NpgsqlCommand command = new NpgsqlCommand(Connection.Dialect.GenerateScript(d => d.Put("^copy %f (%,i) ^from ^stdin", DestinationTable.FullName, from c in dst.Columns select c.ColumnName)), conn);
            NpgsqlCopyIn cin = new NpgsqlCopyIn(command, conn);

            try
            {
                cin.Start();
                var fw = new BinaryWriter(cin.CopyStream);
                while (!queue.IsEof)
                {
                    IBedRecord rec = queue.GetRecord();
                    for (int i = 0; i < rec.FieldCount; i++)
                    {
                        if (i > 0) fw.Write((byte)'\t');
                        rec.ReadValue(i);
                        WriteField(rec, fw);
                    }
                    fw.Write((byte)'\r');
                    fw.Write((byte)'\n');
                    okRowCount++;
                }
                fw.Flush();
                cin.End();
            }
            catch (Exception err)
            {
                cin.Cancel("canceled");
                ProgressInfo.LogMessageDetail(
                    "INSERT", DatAdmin.LogLevel.Error,
                    String.Format("{0}", Texts.Get("s_error_inserting_into_table$table", "table", DestinationTable.FullName)), err.ToString());
                throw;
            }

            if (failRowCount > 0)
            {
                ProgressInfo.LogMessageDetail(
                    "INSERT", DatAdmin.LogLevel.Error,
                    String.Format("{0}, OK:{1}, FAIL:{2}", Texts.Get("s_error_inserting_into_table$table", "table", DestinationTable.FullName), okRowCount, failRowCount),
                    insertErrors.CreateDelimitedText("\r\n")
                    );
            }
            else
            {
                ProgressInfo.LogMessage("INSERT", DatAdmin.LogLevel.Info, Texts.Get("s_inserted_into_table$table$rows", "table", DestinationTable.FullName, "rows", okRowCount));
            }
        }

        public static void WriteField(IBedValueReader value, BinaryWriter fw)
        {
            var type = value.GetFieldType();
            if (type == TypeStorage.Null)
            {
                fw.Write((byte)'\\');
                fw.Write((byte)'N');
            }
            else if (type == TypeStorage.ByteArray)
            {
                foreach (byte b in value.GetByteArray())
                {
                    var sb = new StringBuilder();
                    StringTool.EncodeOct(b, sb);
                    fw.Write('\\');
                    fw.Write('\\');
                    fw.Write(Encoding.UTF8.GetBytes(sb.ToString()));
                }
            }
            else if (type.IsNumber())
            {
                if (type.IsInteger())
                {
                    fw.Write(Encoding.UTF8.GetBytes(value.GetIntegerValue().ToString()));
                }
                else
                {
                    fw.Write(Encoding.UTF8.GetBytes(value.GetRealValue().ToString(CultureInfo.InvariantCulture)));
                }
            }
            else
            {
                string s = String.Format(CultureInfo.InvariantCulture, "{0}", value.GetValue());
                s = s.Replace("\\", "\\\\");
                s = s.Replace("\t", "\\t");
                s = s.Replace("\n", "\\n");
                s = s.Replace("\r", "\\r");
                fw.Write(Encoding.UTF8.GetBytes(s));
            }
        }
    }
}
