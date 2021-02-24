using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    public enum TypeStorage : byte
    {
        Null = 0,
        Byte = 1,
        Int16 = 2,
        Int32 = 3,
        Int64 = 4,
        SByte = 5,
        UInt16 = 6,
        UInt32 = 7,
        UInt64 = 8,
        String = 9,
        Boolean = 10,
        DateTime = 11,
        Decimal = 12,
        Float = 13,
        Double = 14,
        Guid = 15,
        ByteArray = 16,
        DateTimeEx = 17,
        DateEx = 18,
        TimeEx = 19,
        //Array = 20,
        Undefined = 255,
    }

    public interface IBedValueReader
    {
        TypeStorage GetFieldType();

        bool GetBoolean();
        byte GetByte();
        sbyte GetSByte();
        byte[] GetByteArray();
        DateTime GetDateTime();
        DateTimeEx GetDateTimeEx();
        DateEx GetDateEx();
        TimeEx GetTimeEx();
        decimal GetDecimal();
        double GetDouble();
        float GetFloat();
        Guid GetGuid();
        short GetInt16();
        int GetInt32();
        long GetInt64();
        ushort GetUInt16();
        uint GetUInt32();
        ulong GetUInt64();
        string GetString();
        //Array GetArray();
        object GetValue();
    }

    public interface IBedValueWriter
    {
        void SetBoolean(bool value);
        void SetByte(byte value);
        void SetSByte(sbyte value);
        void SetByteArray(byte[] value);
        void SetDateTime(DateTime value);
        void SetDateTimeEx(DateTimeEx value);
        void SetDateEx(DateEx value);
        void SetTimeEx(TimeEx value);
        void SetDecimal(decimal value);
        void SetDouble(double value);
        void SetFloat(float value);
        void SetGuid(Guid value);
        void SetInt16(short value);
        void SetInt32(int value);
        void SetInt64(long value);
        void SetUInt16(ushort value);
        void SetUInt32(uint value);
        void SetUInt64(ulong value);
        void SetString(string value);
        //void SetArray(Array value);
        void SetNull();
    }

    public interface IBedRecordWriter : IBedValueWriter
    {
        void SeekValue(int i);
    }

    public interface IBedRecord : IBedValueReader
    {
        ITableStructure Structure { get; }
        int FieldCount { get; }
        int GetOrdinal(string colName);
        string GetName(int i);
        int GetValues(object[] values); // quick read all record values

        void ReadValue(int i);
    }

    public interface IBedReader : IBedRecord, IDisposable
    {
        bool Read();
    }

    public interface IBedValueParser
    {
        IProgressInfo ProgressInfo { get; set; }
        void ParseValue(string text, TypeStorage type, IBedValueWriter writer);
    }

    public interface IBedValueFormatter : IBedValueWriter
    {
        IProgressInfo ProgressInfo { get; set; }
        string GetText();
    }

    public interface IBedValueConvertor
    {
        void ConvertValue(IBedValueReader reader, TypeStorage type, IBedValueWriter writer);
        IBedValueParser Parser { get; }
        IBedValueFormatter Formatter { get; }
        IProgressInfo ProgressInfo { get; set; }
    }
}
