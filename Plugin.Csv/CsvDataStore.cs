using System;
using System.Collections.Generic;
using System.Text;
//using System.ComponentModel;
using System.Data;
using System.IO;
using System.Xml;

using DatAdmin;
using LumenWorks.Framework.IO.Csv;
using System.ComponentModel;

namespace Plugin.Csv
{
    [TabularDataStore(Name = "csv", Title = "s_csv_file", Description = "s_csv_file_desc", RequiredFeature = CsvFeature.Test)]
    public class CsvDataStore : FileWithFormatDataStoreBase, ITabularDataOuputStream
    {
        public CsvDataStore()
        {
            DefaultStringLength = 50;
        }

        protected override ITableStructure DoGetRowFormat()
        {
            using (CsvReader cr = CreateReader())
            {
                return GetStructure(cr);
            }
        }

        ITableStructure GetStructure(CsvReader cr)
        {
            TableStructure res = new TableStructure();
            if (m_hasHeaders)
            {
                try
                {
                    foreach (string col in cr.GetFieldHeaders())
                    {
                        res.AddColumn(col, new DbTypeString(DefaultStringLength));
                    }
                }
                catch (CsvDuplicateColumnName err)
                {
                    throw new InvalidInputError(Texts.Get("s_duplicate_column_name_try_change_config$column", "column", err.Column));
                }
            }
            else
            {
                for (int i = 1; i <= cr.FieldCount; i++)
                {
                    res.AddColumn(String.Format("#{0}", i), new DbTypeString(DefaultStringLength));
                }
            }
            return res;
        }

        protected override void DoRead(IDataQueue queue)
        {
            try
            {
                using (CsvReader cr = CreateReader())
                {
                    ITableStructure format = GetStructure(cr);
                    IEnumerable<string[]> reader = cr;
                    foreach (string[] row in reader)
                    {
                        queue.PutRecord(new ArrayDataRecord(format, row));
                    }
                    queue.PutEof();
                }
            }
            catch (Exception e)
            {
                ProgressInfo.LogError(e);
                queue.PutError(e);
            }
            finally
            {
                queue.CloseWriting();
            }
            FinalizeBulkCopy();
        }

        public override void ClearLoadCaches()
        {
            base.ClearLoadCaches();
        }

        public override string FileExtension { get { return "csv"; } }

        CsvReader CreateReader()
        {
            TextReader tr = new StreamReader(GetWorkingFileName(), m_encoding);
            CsvReader res = new CsvReader(tr, m_hasHeaders, m_delimiter, m_quote, m_escape, m_comment, m_trimSpaces);
            res.Disposed += delegate(object o, EventArgs e) { tr.Close(); };
            return res;
        }
        CsvWriter CreateWriter(StreamWriter sw)
        {
            //TextWriter tw = new StreamWriter(m_filename, false, m_encoding);
            CsvWriter res = new CsvWriter(sw, m_hasHeaders, m_delimiter, m_quote, m_escape, m_comment, m_quotingMode, m_line_ends);
            return res;
        }

        [XmlElem]
        [DatAdmin.DisplayName("s_default_string_length")]
        public int DefaultStringLength { get; set; }

        char m_delimiter = ',';
        [DatAdmin.DisplayName("s_delimiter_char")]
        [TypeConverter(typeof(CharacterTypeConverter))]
        [XmlAttrib("delimiter")]
        public char Delimiter
        {
            get { return m_delimiter; }
            set { m_delimiter = value; }
        }

        char m_quote = '"';
        [DatAdmin.DisplayName("s_quote_char")]
        [XmlAttrib("quote")]
        public char Quote
        {
            get { return m_quote; }
            set { m_quote = value; }
        }

        string m_line_ends = "\r\n";
        [DatAdmin.DisplayName("s_line_ends")]
        [XmlAttrib("line_ends")]
        [TypeConverter(typeof(LineEndingTypeConverter))]
        public string LineEnds
        {
            get { return m_line_ends; }
            set { m_line_ends = value; }
        }

        char m_escape = '"';
        [DatAdmin.DisplayName("s_escape_char")]
        [XmlAttrib("escape")]
        public char Escape
        {
            get { return m_escape; }
            set { m_escape = value; }
        }

        char m_comment = '#';
        [DatAdmin.DisplayName("s_comment_char")]
        [XmlAttrib("comment")]
        public char Comment
        {
            get { return m_comment; }
            set { m_comment = value; }
        }

        bool m_hasHeaders = true;
        [DatAdmin.DisplayName("s_has_headers")]
        [TypeConverter(typeof(YesNoTypeConverter))]
        [XmlAttrib("headers", true)]
        public bool HasHeaders
        {
            get { return m_hasHeaders; }
            set { m_hasHeaders = value; }
        }

        bool m_trimSpaces = true;
        [DatAdmin.DisplayName("s_trim_spaces")]
        [TypeConverter(typeof(YesNoTypeConverter))]
        [XmlAttrib("trim_spaces")]
        public bool TrimSpaces
        {
            get { return m_trimSpaces; }
            set { m_trimSpaces = value; }
        }

        CsvQuotingMode m_quotingMode = CsvQuotingMode.OnlyIfNecessary;
        [DatAdmin.DisplayName("s_quoting_mode")]
        [TypeConverter(typeof(EnumDescConverter))]
        [XmlAttrib("quoting_mode")]
        public CsvQuotingMode QuotingMode
        {
            get { return m_quotingMode; }
            set { m_quotingMode = value; }
        }

        [Browsable(false)]
        public override ITabularDataOuputStream StreamApi
        {
            get { return this; }
        }

        #region ITabularDataOuputStream Members

        public void WriteStart(StreamWriter fw, ITableStructure table, ref object manager)
        {
            int fldcnt = table.Columns.Count;
            string[] record = new string[fldcnt];

            CsvWriter cw = CreateWriter(fw);
            if (m_hasHeaders)
            {
                // write header
                for (int i = 0; i < fldcnt; i++) record[i] = table.Columns[i].ColumnName;
                cw.WriteRow(record);
            }
            manager = new Manager
            {
                formatter = new BedValueFormatter(FormatSettings),
                data = record
            };
        }

        public void WriteRecord(StreamWriter fw, ITableStructure table, IBedRecord record, int index, object manager)
        {
            var mgr = (Manager)manager;
            string[] data = mgr.data;

            CsvWriter cw = CreateWriter(fw);

            // write data
            for (int i = 0; i < data.Length; i++)
            {
                record.ReadValue(i);
                mgr.formatter.ReadFrom(record);
                data[i] = mgr.formatter.GetText();
            }
            cw.WriteRow(data);
        }

        public void WriteEnd(StreamWriter fw, ITableStructure table, object manager)
        {
        }

        [Browsable(false)]
        public bool RequireSingleStream
        {
            get { return false; }
        }

        #endregion

        class Manager
        {
            internal IBedValueFormatter formatter;
            internal string[] data;
        }
    }

    public enum CsvQuotingMode
    {
        [Description("s_always")]
        Always,
        [Description("s_always_except_numbers")]
        AlwaysExceptNumbers,
        [Description("s_only_if_necessary")]
        OnlyIfNecessary
    }
}
