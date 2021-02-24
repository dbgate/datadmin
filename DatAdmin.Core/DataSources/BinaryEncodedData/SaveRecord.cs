using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;

namespace DatAdmin
{
    public static partial class BedTool
    {
        //public static void SaveField(IDataRecord record, int index, BinaryWriter stream)
        //{
        //    if (record.IsDBNull(index))
        //    {
        //        stream.Write((byte)TypeStorage.Null);
        //        return;
        //    }
        //    Type type = record.GetFieldType(index);
        //    try
        //    {
        //        switch (Type.GetTypeCode(type))
        //        {
        //            case TypeCode.Boolean:
        //                {
        //                    bool val = record.GetBoolean(index);
        //                    stream.Write((byte)TypeStorage.Boolean);
        //                    stream.Write(val);
        //                }
        //                break;
        //            case TypeCode.Byte:
        //                {
        //                    byte val = record.GetByte(index);
        //                    stream.Write((byte)TypeStorage.Byte);
        //                    stream.Write(val);
        //                }
        //                break;
        //            case TypeCode.Int16:
        //                {
        //                    short val = record.GetInt16(index);
        //                    stream.Write((byte)TypeStorage.Int16);
        //                    stream.Write(val);
        //                }
        //                break;
        //            case TypeCode.Int32:
        //                {
        //                    int val = record.GetInt32(index);
        //                    stream.Write((byte)TypeStorage.Int32);
        //                    stream.Write(val);
        //                }
        //                break;
        //            case TypeCode.Int64:
        //                {
        //                    long val = record.GetInt64(index);
        //                    stream.Write((byte)TypeStorage.Int64);
        //                    stream.Write(val);
        //                }
        //                break;
        //            case TypeCode.DateTime:
        //                {
        //                    DateTime val = record.GetDateTime(index);
        //                    stream.Write((byte)TypeStorage.DateTime);
        //                    stream.Write(val.ToBinary());
        //                }
        //                break;
        //            case TypeCode.Decimal:
        //                {
        //                    decimal val = record.GetDecimal(index);
        //                    stream.Write((byte)TypeStorage.Decimal);
        //                    stream.Write(val);
        //                }
        //                break;
        //            case TypeCode.Single:
        //                {
        //                    float val = record.GetFloat(index);
        //                    stream.Write((byte)TypeStorage.Float);
        //                    stream.Write(val);
        //                }
        //                break;
        //            case TypeCode.Double:
        //                {
        //                    double val = record.GetDouble(index);
        //                    stream.Write((byte)TypeStorage.Double);
        //                    stream.Write(val);
        //                }
        //                break;
        //            case TypeCode.String:
        //                {
        //                    string val = record.GetString(index);
        //                    stream.Write((byte)TypeStorage.String);
        //                    stream.Write(val);
        //                }
        //                break;
        //            default:
        //                if (type == typeof(Guid))
        //                {
        //                    Guid val = record.GetGuid(index);
        //                    stream.Write((byte)TypeStorage.Guid);
        //                    stream.Write(val.ToByteArray());
        //                }
        //                else if (type == typeof(byte[]))
        //                {
        //                    byte[] val = (byte[])record.GetValue(index);
        //                    stream.Write((byte)TypeStorage.ByteArray);
        //                    stream.Write7BitEncodedInt(val.Length);
        //                    stream.Write(val);
        //                }
        //                else
        //                {
        //                    // serialize as string
        //                    string val = record.GetValue(index).ToString();
        //                    stream.Write((byte)TypeStorage.String);
        //                    stream.Write(val);
        //                }
        //                break;
        //        }
        //    }
        //    catch
        //    {
        //        try
        //        {
        //            object val = record[index];
        //            // try to write boxed value (not very effective)
        //            SaveField(val, stream);
        //        }
        //        catch
        //        {
        //            string val = record.GetString(index);
        //            stream.Write((byte)TypeStorage.String);
        //            stream.Write(val);
        //        }
        //    }
        //}

        //public static void SaveField(object value, BinaryWriter stream)
        //{
        //    if (value == null || value == DBNull.Value)
        //    {
        //        stream.Write((byte)TypeStorage.Null);
        //        return;
        //    }
        //    Type type = value.GetType();
        //    switch (Type.GetTypeCode(type))
        //    {
        //        case TypeCode.Boolean:
        //            {
        //                bool val = (bool)value;
        //                stream.Write((byte)TypeStorage.Boolean);
        //                stream.Write(val);
        //            }
        //            break;
        //        case TypeCode.Byte:
        //            {
        //                byte val = (byte)value;
        //                stream.Write((byte)TypeStorage.Byte);
        //                stream.Write(val);
        //            }
        //            break;
        //        case TypeCode.Int16:
        //            {
        //                short val = (short)value;
        //                stream.Write((byte)TypeStorage.Int16);
        //                stream.Write(val);
        //            }
        //            break;
        //        case TypeCode.Int32:
        //            {
        //                int val = (int)value;
        //                stream.Write((byte)TypeStorage.Int32);
        //                stream.Write(val);
        //            }
        //            break;
        //        case TypeCode.Int64:
        //            {
        //                long val = (long)value;
        //                stream.Write((byte)TypeStorage.Int64);
        //                stream.Write(val);
        //            }
        //            break;
        //        case TypeCode.DateTime:
        //            {
        //                DateTime val = (DateTime)value;
        //                stream.Write((byte)TypeStorage.DateTime);
        //                stream.Write(val.ToBinary());
        //            }
        //            break;
        //        case TypeCode.Decimal:
        //            {
        //                decimal val = (decimal)value;
        //                stream.Write((byte)TypeStorage.Decimal);
        //                stream.Write(val);
        //            }
        //            break;
        //        case TypeCode.Single:
        //            {
        //                float val = (float)value;
        //                stream.Write((byte)TypeStorage.Float);
        //                stream.Write(val);
        //            }
        //            break;
        //        case TypeCode.Double:
        //            {
        //                double val = (double)value;
        //                stream.Write((byte)TypeStorage.Double);
        //                stream.Write(val);
        //            }
        //            break;
        //        case TypeCode.String:
        //            {
        //                string val = (string)value;
        //                stream.Write((byte)TypeStorage.String);
        //                stream.Write(val);
        //            }
        //            break;
        //        default:
        //            if (type == typeof(Guid))
        //            {
        //                Guid val = (Guid)value;
        //                stream.Write((byte)TypeStorage.Guid);
        //                stream.Write(val.ToByteArray());
        //            }
        //            else if (type == typeof(byte[]))
        //            {
        //                byte[] val = (byte[])value;
        //                stream.Write((byte)TypeStorage.ByteArray);
        //                stream.Write7BitEncodedInt(val.Length);
        //                stream.Write(val);
        //            }
        //            else
        //            {
        //                // serialize as string
        //                string val = value.ToString();
        //                stream.Write((byte)TypeStorage.String);
        //                stream.Write(val);
        //            }
        //            break;
        //    }
        //}

        public static void SaveRecord(int fldcount, IBedRecord record, BinaryWriter stream)
        {
            var fw = new StreamValueWriter(stream);
            if (fldcount != record.FieldCount) throw new InternalError("DAE-00023 field count mitchmatch");
            for (int i = 0; i < fldcount; i++)
            {
                record.ReadValue(i);
                fw.ReadFrom(record);
            }
        }
    }
}
