using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;
using System.IO.Compression;
using Ionic.Zlib;

namespace DatAdmin
{
    public static class Compression
    {
        public static byte[] ZlibCompress(byte[] data)
        {
            MemoryStream res = new MemoryStream();
            var fw = new Ionic.Zlib.ZlibStream(res, Ionic.Zlib.CompressionMode.Compress);
            fw.Write(data, 0, data.Length);
            fw.Close();
            res.Flush();
            return res.ToArray();
        }

        public static byte[] ZlibDecompress(byte[] data)
        {
            MemoryStream src = new MemoryStream(data);
            var fr = new Ionic.Zlib.ZlibStream(src, Ionic.Zlib.CompressionMode.Decompress);
            MemoryStream fw = new MemoryStream();
            IOTool.CopyStream(fr, fw);
            return fw.ToArray();
        }
    }
}
