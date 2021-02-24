using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace DatAdmin
{
    //public static class BedArray
    //{
    //    private static void Increment(int[] indices, int[] dims)
    //    {
    //        int dim = dims.Length - 1;
    //        while (dim >= 0)
    //        {
    //            if (indices[dim] < dims[dim] - 1)
    //            {
    //                indices[dim]++;
    //                break;
    //            }
    //            dim--;
    //        }
    //        if (dim < 0) throw new InternalError("BedArray.Increment: index out of bounds");
    //        for (int i = dim + 1; i < dims.Length; i++)
    //        {
    //            indices[dim] = 0;
    //        }
    //    }

    //    public static Array FromStream(BinaryReader stream, bool allowDAExtensions)
    //    {
    //        var elemTypeCode = (TypeStorage)stream.ReadByte();
    //        int rank = stream.ReadInt32();
    //        int[] dims = new int[rank];
    //        for (int i = 0; i < rank; i++) dims[i] = stream.ReadInt32();
    //        Type elemType = TypeTool.GetDotNetType(elemTypeCode, allowDAExtensions);
    //        var resType = elemType.MakeArrayType(rank);
    //        var lstTypes = new List<Type>();
    //        var c = resType.GetConstructor((from d in dims select d.GetType()).ToArray());
    //        Array res = (Array)c.Invoke((from d in dims select (object)d).ToArray());
    //        int count = 1;
    //        for (int i = 0; i < rank; i++) count *= dims[i];
    //        var hld = new BedValueHolder();
    //        int[] indices = new int[rank];
    //        for (int i = 0; i < count; i++)
    //        {
    //            hld.ReadValue(stream);
    //            res.SetValue(hld.GetValue(), indices);
    //            Increment(indices, dims);
    //        }
    //        return res;
    //    }

    //    public static void WriteToStream(Array value, BinaryWriter stream)
    //    {
    //        int rank = value.Rank;
    //        int[] dims = value.GetDimensions();
    //        Type elemType = value.GetType().GetElementType();
    //        TypeStorage stor = elemType.GetTypeStorage();
    //        stream.Write((byte)stor);
    //        stream.Write(dims.Length);
    //        for (int i = 0; i < dims.Length; i++) stream.Write(dims[i]);
    //        int[] indices = new int[dims.Length];
    //        int count = 1;
    //        for (int i = 0; i < rank; i++) count *= dims[i];
    //        var hld = new BedValueHolder();
    //        var sw = new StreamValueWriter(stream);
    //        for (int i = 0; i < count; i++)
    //        {
    //            hld.ReadFrom(value.GetValue(indices));
    //            hld.WriteTo(sw);
    //            Increment(indices, dims);
    //        }
    //    }

    //    public static Array Parse(string text)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public static string ToString(Array array)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}
