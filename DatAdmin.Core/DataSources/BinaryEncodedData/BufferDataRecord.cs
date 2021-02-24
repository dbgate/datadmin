using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using System.Globalization;
using System.Linq;

namespace DatAdmin
{
    //public class BufferDataRecord : IBedRecord
    //{
    //    BinaryReader m_stream;
    //    long m_position;
    //    string[] m_fieldNames;

    //    public struct FieldDesc
    //    {
    //        public TypeStorage Storage;
    //        //public int Length;
    //        public int Offset;
    //        public object Cache;
    //    }
    //    FieldDesc[] m_fields;

    //    public BufferDataRecord(BinaryReader stream, long position, string[] fields)
    //    {
    //        m_position = position;
    //        m_stream = stream;
    //        m_fieldNames = fields;

    //        LoadInfo();
    //    }

    //    public static BufferDataRecord FromRecord(IDataRecord record)
    //    {
    //        MemoryStream ms = new MemoryStream();
    //        BinaryWriter bw = new BinaryWriter(ms);
    //        BedTool.SaveRecord(record.FieldCount, record, bw);
    //        return new BufferDataRecord(new BinaryReader(ms), 0, record.GetFieldNames());
    //    }

    //    public static BufferDataRecord FromRecord(IDataRecord record, int[] colindexes, string[] colnames)
    //    {
    //        if (colindexes.Length != colnames.Length) throw new InternalError("BufferDataRecord.FromRecord: colnames.count != colindexes.count");
    //        MemoryStream ms = new MemoryStream();
    //        BinaryWriter bw = new BinaryWriter(ms);
    //        for (int i = 0; i < colindexes.Length; i++)
    //        {
    //            if (colindexes[i] >= 0)
    //            {
    //                BedTool.SaveField(record, colindexes[i], bw);
    //            }
    //            else
    //            {
    //                bw.Write((byte)TypeStorage.Null);
    //            }
    //        }
    //        return new BufferDataRecord(new BinaryReader(ms), 0, colnames.ToArray());
    //    }

    //    private void LoadInfo()
    //    {
    //        m_stream.BaseStream.Seek(m_position, SeekOrigin.Begin);
    //        m_fields = new FieldDesc[m_fieldNames.Length];
    //        for (int i = 0; i < m_fields.Length; i++)
    //        {
    //            m_fields[i].Storage = (TypeStorage)m_stream.ReadByte();
    //            m_fields[i].Offset = (int)(m_stream.BaseStream.Position - m_position);
    //            switch (m_fields[i].Storage)
    //            {
    //                case TypeStorage.String:
    //                    m_fields[i].Cache = m_stream.ReadString();
    //                    break;
    //                case TypeStorage.DateTime:
    //                    m_stream.BaseStream.Seek(8, SeekOrigin.Current);
    //                    break;
    //                case TypeStorage.Guid:
    //                    m_stream.BaseStream.Seek(16, SeekOrigin.Current);
    //                    break;
    //                case TypeStorage.Boolean:
    //                    m_stream.BaseStream.Seek(1, SeekOrigin.Current);
    //                    break;
    //                case TypeStorage.Byte:
    //                    m_stream.BaseStream.Seek(1, SeekOrigin.Current);
    //                    break;
    //                case TypeStorage.ByteArray:
    //                    int len = m_stream.Read7BitEncodedInt();
    //                    m_stream.BaseStream.Seek(len, SeekOrigin.Current);
    //                    break;
    //                case TypeStorage.Decimal:
    //                    m_stream.BaseStream.Seek(16, SeekOrigin.Current);
    //                    break;
    //                case TypeStorage.Double:
    //                    m_stream.BaseStream.Seek(8, SeekOrigin.Current);
    //                    break;
    //                case TypeStorage.Float:
    //                    m_stream.BaseStream.Seek(4, SeekOrigin.Current);
    //                    break;
    //                case TypeStorage.Int16:
    //                    m_stream.BaseStream.Seek(2, SeekOrigin.Current);
    //                    break;
    //                case TypeStorage.Int32:
    //                    m_stream.BaseStream.Seek(4, SeekOrigin.Current);
    //                    break;
    //                case TypeStorage.Int64:
    //                    m_stream.BaseStream.Seek(8, SeekOrigin.Current);
    //                    break;
    //                case TypeStorage.Null:
    //                    break;
    //                default:
    //                    throw new InternalError("Bad storage code in BED:" + m_fields[i].Storage.ToString());
    //            }
    //        }
    //    }

    //    #region IDataRecord Members

    //    public int FieldCount
    //    {
    //        get { return m_fields.Length; }
    //    }

    //    public bool GetBoolean(int i)
    //    {
    //        if (m_fields[i].Storage == TypeStorage.Boolean)
    //        {
    //            m_stream.BaseStream.Seek(m_position + m_fields[i].Offset, SeekOrigin.Begin);
    //            return m_stream.ReadBoolean();
    //        }
    //        return (bool)Convert.ChangeType(GetValue(i), typeof(bool));
    //    }

    //    public byte GetByte(int i)
    //    {
    //        if (m_fields[i].Storage == TypeStorage.Byte)
    //        {
    //            m_stream.BaseStream.Seek(m_position + m_fields[i].Offset, SeekOrigin.Begin);
    //            return m_stream.ReadByte();
    //        }
    //        return (byte)Convert.ChangeType(GetValue(i), typeof(byte));
    //    }

    //    public DateTime GetDateTime(int i)
    //    {
    //        if (m_fields[i].Storage == TypeStorage.DateTime)
    //        {
    //            m_stream.BaseStream.Seek(m_position + m_fields[i].Offset, SeekOrigin.Begin);
    //            return DateTime.FromBinary(m_stream.ReadInt64());
    //        }
    //        return (DateTime)Convert.ChangeType(GetValue(i), typeof(DateTime));
    //    }

    //    public decimal GetDecimal(int i)
    //    {
    //        if (m_fields[i].Storage == TypeStorage.Decimal)
    //        {
    //            m_stream.BaseStream.Seek(m_position + m_fields[i].Offset, SeekOrigin.Begin);
    //            return m_stream.ReadDecimal();
    //        }
    //        return (decimal)Convert.ChangeType(GetValue(i), typeof(decimal));
    //    }

    //    public double GetDouble(int i)
    //    {
    //        if (m_fields[i].Storage == TypeStorage.Double)
    //        {
    //            m_stream.BaseStream.Seek(m_position + m_fields[i].Offset, SeekOrigin.Begin);
    //            return m_stream.ReadDouble();
    //        }
    //        return (double)Convert.ChangeType(GetValue(i), typeof(double));
    //    }

    //    public Type GetFieldType(int i)
    //    {
    //        switch (m_fields[i].Storage)
    //        {
    //            case TypeStorage.DateTime: return typeof(DateTime);
    //            case TypeStorage.Boolean: return typeof(bool);
    //            case TypeStorage.Byte: return typeof(byte);
    //            case TypeStorage.Decimal: return typeof(decimal);
    //            case TypeStorage.Double: return typeof(double);
    //            case TypeStorage.Float: return typeof(float);
    //            case TypeStorage.Guid: return typeof(Guid);
    //            case TypeStorage.Int16: return typeof(Int16);
    //            case TypeStorage.Int32: return typeof(Int32);
    //            case TypeStorage.Int64: return typeof(Int64);
    //            case TypeStorage.Null: return typeof(object);
    //            case TypeStorage.String: return typeof(String);
    //            case TypeStorage.ByteArray: return typeof(byte[]);
    //        }
    //        return null;
    //    }

    //    public float GetFloat(int i)
    //    {
    //        if (m_fields[i].Storage == TypeStorage.Float)
    //        {
    //            m_stream.BaseStream.Seek(m_position + m_fields[i].Offset, SeekOrigin.Begin);
    //            return m_stream.ReadSingle();
    //        }
    //        return (float)Convert.ChangeType(GetValue(i), typeof(float));
    //    }

    //    public Guid GetGuid(int i)
    //    {
    //        if (m_fields[i].Storage == TypeStorage.Guid)
    //        {
    //            m_stream.BaseStream.Seek(m_position + m_fields[i].Offset, SeekOrigin.Begin);
    //            return new Guid(m_stream.ReadBytes(16));
    //        }
    //        return (Guid)Convert.ChangeType(GetValue(i), typeof(Guid));
    //    }

    //    public short GetInt16(int i)
    //    {
    //        if (m_fields[i].Storage == TypeStorage.Int16)
    //        {
    //            m_stream.BaseStream.Seek(m_position + m_fields[i].Offset, SeekOrigin.Begin);
    //            return m_stream.ReadInt16();
    //        }
    //        return (short)Convert.ChangeType(GetValue(i), typeof(short));
    //    }

    //    public int GetInt32(int i)
    //    {
    //        if (m_fields[i].Storage == TypeStorage.Int32)
    //        {
    //            m_stream.BaseStream.Seek(m_position + m_fields[i].Offset, SeekOrigin.Begin);
    //            return m_stream.ReadInt32();
    //        }
    //        return (int)Convert.ChangeType(GetValue(i), typeof(int));
    //    }

    //    public long GetInt64(int i)
    //    {
    //        if (m_fields[i].Storage == TypeStorage.Int64)
    //        {
    //            m_stream.BaseStream.Seek(m_position + m_fields[i].Offset, SeekOrigin.Begin);
    //            return m_stream.ReadInt64();
    //        }
    //        return (long)Convert.ChangeType(GetValue(i), typeof(long));
    //    }

    //    public string GetName(int i)
    //    {
    //        return m_fieldNames[i];
    //    }

    //    public int GetOrdinal(string name)
    //    {
    //        return Array.IndexOf(m_fieldNames, name);
    //    }

    //    public string GetString(int i)
    //    {
    //        if (m_fields[i].Storage == TypeStorage.String) return (string)m_fields[i].Cache;
    //        return GetValue(i).SafeToString();
    //    }

    //    public object GetValue(int i)
    //    {
    //        if (i >= m_fields.Length)
    //        {
    //            throw new BedTableError("BufferRecord: index out of range");
    //        }
    //        if (m_fields[i].Storage == TypeStorage.Null) return null;
    //        if (m_fields[i].Cache != null) return m_fields[i].Cache;
    //        m_stream.BaseStream.Seek(m_position + m_fields[i].Offset, SeekOrigin.Begin);
    //        switch (m_fields[i].Storage)
    //        {
    //            case TypeStorage.Boolean:
    //                m_fields[i].Cache = m_stream.ReadBoolean();
    //                break;
    //            case TypeStorage.DateTime:
    //                m_fields[i].Cache = DateTime.FromBinary(m_stream.ReadInt64());
    //                break;
    //            case TypeStorage.Guid:
    //                m_fields[i].Cache = new Guid(m_stream.ReadBytes(16));
    //                break;
    //            case TypeStorage.Byte:
    //                m_fields[i].Cache = m_stream.ReadByte();
    //                break;
    //            case TypeStorage.Decimal:
    //                m_fields[i].Cache = m_stream.ReadDecimal();
    //                break;
    //            case TypeStorage.Double:
    //                m_fields[i].Cache = m_stream.ReadDouble();
    //                break;
    //            case TypeStorage.Float:
    //                m_fields[i].Cache = m_stream.ReadSingle();
    //                break;
    //            case TypeStorage.Int16:
    //                m_fields[i].Cache = m_stream.ReadInt16();
    //                break;
    //            case TypeStorage.Int32:
    //                m_fields[i].Cache = m_stream.ReadInt32();
    //                break;
    //            case TypeStorage.Int64:
    //                m_fields[i].Cache = m_stream.ReadInt64();
    //                break;
    //            case TypeStorage.ByteArray:
    //                int len = m_stream.Read7BitEncodedInt();
    //                m_fields[i].Cache = m_stream.ReadBytes(len);
    //                break;
    //            default:
    //                throw new InternalError("Unexpected type code:" + m_fields[i].Storage.ToString());
    //        }
    //        return m_fields[i].Cache;
    //    }

    //    public int GetValues(object[] values)
    //    {
    //        for (int i = 0; i < m_fields.Length; i++) values[i] = GetValue(i);
    //        return m_fields.Length;
    //    }

    //    public bool IsDBNull(int i)
    //    {
    //        return m_fields[i].Storage == TypeStorage.Null;
    //    }

    //    public object this[string name]
    //    {
    //        get { return GetValue(GetOrdinal(name)); }
    //    }

    //    public object this[int i]
    //    {
    //        get { return GetValue(i); }
    //    }

    //    #endregion
    //}
}
