using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;

namespace DatAdmin
{
    public static partial class BedTool
    {
        [Flags]
        public enum ColFlags : byte
        {
            ISPK = 1,
        }

        public const int SIGNATURE = 0x56f17a56;

        public static void WriteQueueToStream(IDataQueue queue, Stream fw, BedDataStats stats)
        {
            BinaryWriter bw = new BinaryWriter(fw);

            bw.Write(SIGNATURE);
            bw.Write((int)2); // version
            var ts = queue.GetRowFormat;
            bw.Write7BitEncodedInt(ts.Columns.Count);

            var pk = ts.FindConstraint<IPrimaryKey>();
            foreach (IColumnStructure col in ts.Columns)
            {
                bw.Write(col.ColumnName);
                bw.Write((byte)col.DataType.DefaultStorage);
                ColFlags flags = 0;
                if (pk != null && pk.Columns.IndexOfIf(c => c.ColumnName == col.ColumnName) >= 0)
                {
                    flags |= ColFlags.ISPK;
                }
                bw.Write((byte)flags);
            }

            MemoryStream rowdata = new MemoryStream();
            BinaryWriter bwrow = new BinaryWriter(rowdata);
            try
            {
                while (!queue.IsEof)
                {
                    rowdata.Position = 0;
                    rowdata.SetLength(0);
                    IBedRecord row = queue.GetRecord();
                    BedTool.SaveRecord(ts.Columns.Count, row, bwrow);
                    bw.Write7BitEncodedInt((int)rowdata.Length);
                    rowdata.WriteTo(bw.BaseStream);
                    if (stats != null)
                    {
                        stats.Rows++;
                        stats.Bytes += (int)rowdata.Length;
                        stats.Bytes += 4;
                    }
                }
            }
            finally
            {
                queue.CloseReading();
            }
            // write EOF mark
            bw.Write7BitEncodedInt(-1);
        }
    }

    public class BedDataStats
    {
        public int Rows = 0;
        public int Bytes = 0;
    }
}
