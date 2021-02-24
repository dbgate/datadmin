using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;

namespace DatAdmin
{
    public static partial class BedTool
    {
        public static void SkipRecord(BinaryReader fr)
        {
            int len = fr.Read7BitEncodedInt();
            fr.BaseStream.Seek(len, SeekOrigin.Current);
        }

        public static ArrayDataRecord LoadRecord(BinaryReader fr, ITableStructure table)
        {
            var res = new ArrayDataRecord(table);
            for (int i = 0; i < table.Columns.Count; i++)
            {
                res.SeekValue(i);
                res.ReadValue(fr);
            }
            return res;
        }

        public static void LoadQueue(Stream fr, IDataQueue queue)
        {
            try
            {
                BinaryReader br = new BinaryReader(fr);
                int sgn = br.ReadInt32();
                if (sgn != SIGNATURE) throw new InternalError("DAE-00021 Bad BED file");
                int ver = br.ReadInt32();
                if (ver != 1 && ver != 2) throw new InternalError("DAE-00022 Bad BED file");
                int colcnt = br.Read7BitEncodedInt();
                TableStructure ts = new TableStructure();
                PrimaryKey pk = new PrimaryKey();
                for (int i = 0; i < colcnt; i++)
                {
                    ColumnStructure col = new ColumnStructure();
                    col.ColumnName = br.ReadString();
                    if (ver >= 2)
                    {
                        var type = (TypeStorage)br.ReadByte();
                        col.DataType = type.GetDatAdminType();
                        var flags = (ColFlags)br.ReadByte();
                        if ((flags & ColFlags.ISPK) != 0) pk.Columns.Add(new ColumnReference(col.ColumnName));
                    }
                    ts._Columns.Add(col);
                }
                if (pk.Columns.Count > 0) ts._Constraints.Add(pk);

                for (; ; )
                {
                    int len = br.Read7BitEncodedInt();
                    if (len < 0) break;
                    var rec = LoadRecord(br, ts);
                    queue.PutRecord(rec);
                }

                queue.PutEof();
            }
            catch (Exception e)
            {
                Errors.Report(e);
                queue.PutError(e);
            }
            finally
            {
                queue.CloseWriting();
            }
        }
    }
}
