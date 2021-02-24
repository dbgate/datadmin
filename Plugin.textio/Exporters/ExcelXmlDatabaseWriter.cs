using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using DatAdmin;

namespace Plugin.textio
{
    [DatabaseWriter(Name = "excel", Title = "Excel XML", Description = "s_excel_dbexp_desc", RequiredFeature = ExcelExportFeature.Test)]
    public class ExcelXmlDatabaseWriter : FileDatabaseWriter
    {
        DataFormatSettings m_formatSettings = new DataFormatSettings();

        [TabbedProperty("s_format_settings")]
        [Browsable(false)]
        [XmlSubElem("DataFormat")]
        public DataFormatSettings FormatSettings
        {
            get { return m_formatSettings; }
            set { m_formatSettings = value; }
        }

        [Browsable(false)]
        public override DatabaseWriterCaps WriterCaps
        {
            get
            {
                return new DatabaseWriterCaps
                {
                    AllFlags = false,
                    MultipleSchema = true,
                    AcceptData = true,
                    AcceptStructure = true,
                };
            }
        }

        [Browsable(false)]
        public override string FileExtension
        {
            get { return "xml"; }
        }

        Color m_headerColor = Color.Blue;

        [DatAdmin.DisplayName("s_header_color")]
        [XmlElem]
        public Color HeaderColor
        {
            get { return m_headerColor; }
            set { m_headerColor = value; }
        }

        StreamWriter m_fw;

        public override void WriteStructureBeforeData(IDatabaseStructure db)
        {
            m_fw = new StreamWriter(GetWorkingFileName());
            m_fw.Write(IoRes.excelxml_start.Replace("#HDRCOLOR#", m_headerColor.ToWebName()));
        }

        public override void FillTable(ITableStructure table, IDataQueue queue, TableCopyOptions opts)
        {
            var fmt = new BedValueFormatter(FormatSettings);
            ExcelXmlDataStore.WriteHeaders(m_fw, table, table.FullName.ToString());
            try
            {
                while (!queue.IsEof)
                {
                    var record = queue.GetRecord();
                    ExcelXmlDataStore.WriteRow(m_fw, table, record, fmt);
                }
            }
            finally
            {
                queue.CloseReading();
            }
            m_fw.Write("</ss:Table>\n");
            m_fw.Write("</Worksheet>\n");
        }

        public override void WriteStructureAfterData(IDatabaseStructure db)
        {
            m_fw.Write("</Workbook>\n");
            m_fw.Close();
            m_fw = null;
        }

        public override void CloseConnection()
        {
            if (m_fw != null)
            {
                m_fw.Close();
                m_fw = null;
            }
            FinalizeFileName();
        }
    }
}