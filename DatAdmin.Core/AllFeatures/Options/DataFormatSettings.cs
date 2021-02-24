using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Linq;
using System.Globalization;

namespace DatAdmin
{
    public enum DataFormatBlobMode { InfoText, Hexa, Base64 }

    public enum OnDataErrorMode
    {
        [Description("s_propagate_to_higher_level")]
        Propagate,
        [Description("s_use_defaut_value")]
        UseDefault,
        [Description("s_use_null")]
        UseNull,
    }

    [SettingsPage(Name = "dataformat", Title = "s_data_format", Targets = SettingsTargets.All)]
    public class DataFormatSettings : SettingsPageBase
    {
        string m_dateFormat = "yyyy-MM-dd";
        [DisplayName("s_date_format")]
        [Category("s_datetime")]
        [SettingsKey("dataformat.date_format")]
        public string DateFormat
        {
            get { return m_dateFormat; }
            set { m_dateFormat = value; }
        }

        string m_timeFormat = "HH:mm:ss";
        [DisplayName("s_time_format")]
        [Category("s_datetime")]
        [SettingsKey("dataformat.time_format")]
        public string TimeFormat
        {
            get { return m_timeFormat; }
            set { m_timeFormat = value; }
        }

        string m_dateTimeFormat = "yyyy-MM-dd HH:mm:ss";
        [DisplayName("s_datetime_format")]
        [Category("s_datetime")]
        [SettingsKey("dataformat.datetime_format")]
        public string DateTimeFormat
        {
            get { return m_dateTimeFormat; }
            set { m_dateTimeFormat = value; }
        }

        string m_decimalSeparator = ".";
        [DisplayName("s_decimal_separator")]
        [Category("s_numbers")]
        [SettingsKey("dataformat.decimal_separator")]
        public string DecimalSeparator
        {
            get { return m_decimalSeparator; }
            set { m_decimalSeparator = value; }
        }

        string[] m_nullValues = new string[] { "(NULL)" };
        [DisplayName("s_null_values")]
        [Category("s_general")]
        [SettingsKey("dataformat.null_values")]
        public string NullValues
        {
            get { return String.Join(", ", m_nullValues); }
            set { m_nullValues = (from s in value.Split(',') select s.Trim()).ToArray(); }
        }

        string[] m_trueValues = new string[] { "true", "yes", "1", "on" };
        [DisplayName("s_true_values")]
        [Category("s_general")]
        [SettingsKey("dataformat.true_values")]
        public string TrueValues
        {
            get { return String.Join(", ", m_trueValues); }
            set { m_trueValues = (from s in value.Split(',') select s.Trim()).ToArray(); }
        }


        string[] m_falseValues = new string[] { "false", "no", "0", "off" };
        [DisplayName("s_false_values")]
        [Category("s_general")]
        [SettingsKey("dataformat.false_values")]
        public string FalseValues
        {
            get { return String.Join(", ", m_falseValues); }
            set { m_falseValues = (from s in value.Split(',') select s.Trim()).ToArray(); }
        }

        string m_blobInfo = "(BLOB)";
        [DisplayName("s_blob_info_message")]
        [Category("s_binary_data")]
        [SettingsKey("dataformat.blob_info_message")]
        public string BlobInfo
        {
            get { return m_blobInfo; }
            set { m_blobInfo = value; }
        }

        DataFormatBlobMode m_blobMode = DataFormatBlobMode.Base64;
        [DisplayName("s_blob_mode")]
        [Category("s_binary_data")]
        [SettingsKey("dataformat.blob_mode")]
        public DataFormatBlobMode BlobMode
        {
            get { return m_blobMode; }
            set { m_blobMode = value; }
        }

        int m_hexBytesOnLine = 0;
        [DisplayName("s_hex_bytes_on_line")]
        [Category("s_binary_data")]
        [SettingsKey("dataformat.hex_bytes_on_line")]
        public int HexBytesOnLine
        {
            get { return m_hexBytesOnLine; }
            set { m_hexBytesOnLine = value; }
        }

        bool m_logAllErrors = false;
        [DisplayName("s_log_all_errors")]
        [Category("s_errors")]
        [SettingsKey("dataformat.log_all_errors")]
        public bool LogAllErrors
        {
            get { return m_logAllErrors; }
            set { m_logAllErrors = value; }
        }

        OnDataErrorMode m_onErrorMode = OnDataErrorMode.Propagate;
        [DisplayName("s_on_error")]
        [Category("s_errors")]
        [SettingsKey("dataformat.on_error_mode")]
        [TypeConverter(typeof(EnumDescConverter))]
        public OnDataErrorMode OnErrorMode
        {
            get { return m_onErrorMode; }
            set { m_onErrorMode = value; }
        }

        int m_defaultNumber;
        [DisplayName("s_default_number")]
        [Category("s_errors")]
        [SettingsKey("dataformat.default_number")]
        public int DefaultNumber
        {
            get { return m_defaultNumber; }
            set { m_defaultNumber = value; }
        }

        bool m_defautlLogical;
        [DisplayName("s_default_logical")]
        [Category("s_errors")]
        [SettingsKey("dataformat.default_logical")]
        public bool DefautlLogical
        {
            get { return m_defautlLogical; }
            set { m_defautlLogical = value; }
        }

        DateTimeEx m_defaultDateTime = new DateTimeEx(2000, 1, 1, 0, 0, 0);
        [DisplayName("s_default_datetime")]
        [Category("s_errors")]
        [SettingsKey("dataformat.default_datetime")]
        [TypeConverter(typeof(DateTimeExTypeConverter))]
        public DateTimeEx DefaultDateTime
        {
            get { return m_defaultDateTime; }
            set { m_defaultDateTime = value; }
        }

        public string[] GetNullValues() { return m_nullValues; }
        public string[] GetTrueValues() { return m_trueValues; }
        public string[] GetFalseValues() { return m_falseValues; }

        public string GetTrueValue()
        {
            if (m_trueValues.Length > 0) return m_trueValues[0]; return "true";
        }
        public string GetFalseValue()
        {
            if (m_falseValues.Length > 0) return m_falseValues[0]; return "false";
        }
        public string GetNullValue()
        {
            if (m_nullValues.Length > 0) return m_nullValues[0]; return "(NULL)";
        }

        public NumberFormatInfo GetNumberFormat()
        {
            var res = new NumberFormatInfo();
            res.NumberDecimalSeparator = DecimalSeparator;
            return res;
        }

        public string FormatBlob(byte[] data)
        {
            switch (BlobMode)
            {
                case DataFormatBlobMode.InfoText:
                    return BlobInfo;
                case DataFormatBlobMode.Base64:
                    return Convert.ToBase64String(data) + "=";
                case DataFormatBlobMode.Hexa:
                    if (HexBytesOnLine > 0) return StringTool.EncodeHex(data, HexBytesOnLine);
                    else return StringTool.EncodeHex(data);
            }
            return "";
        }

        public bool? ParseBoolean(string text)
        {
            foreach (string val in m_trueValues) if (String.Compare(text, val, true) == 0) return true;
            foreach (string val in m_falseValues) if (String.Compare(text, val, true) == 0) return false;
            return null;
        }

        public void WriteErrorDefault(TypeStorage type, IBedValueWriter writer)
        {
            if (type.IsNumber()) writer.SetIntegerValue(type, m_defaultNumber);
            else if (type.IsDateRelated()) writer.SetDateTimeValue(type, m_defaultDateTime);
            else if (type == TypeStorage.Boolean) writer.SetBoolean(m_defautlLogical);
            else writer.SetNull();
        }
    }

    public static class SettingsPageCollection_DataFormat
    {
        public static DataFormatSettings DataFormat(this SettingsPageCollection col)
        {
            return (DataFormatSettings)col.PageByName("dataformat");
        }
    }
}
