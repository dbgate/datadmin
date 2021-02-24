using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.ComponentModel;
using DatAdmin;

namespace Plugin.textio
{
    [TabularDataStore(Name = "excel_xml_file", Title = "Excel XML", Description = "s_excel_xml_table_desc", RequiredFeature = ExcelExportFeature.Test)]
    public class ExcelXmlDataStore : FileWithFormatDataStoreBase, ITabularDataOuputStream
    {
        string m_sheetName = "Sheet1";

        [DatAdmin.DisplayName("s_sheet_name")]
        [XmlElem]
        public string SheetName
        {
            get { return m_sheetName; }
            set { m_sheetName = value; }
        }

        Color m_headerColor = Color.Blue;

        [DatAdmin.DisplayName("s_header_color")]
        [XmlElem]
        public Color HeaderColor
        {
            get { return m_headerColor; }
            set { m_headerColor = value; }
        }

        protected override ITableStructure DoGetRowFormat()
        {
            throw new NotImplementedError("DAE-00352");
        }

        protected override void DoRead(IDataQueue queue)
        {
            throw new NotImplementedError("DAE-00353");
        }

        public override void ClearLoadCaches()
        {
            base.ClearLoadCaches();
        }

        public override bool SupportsMode(TabularDataStoreMode mode)
        {
            return mode == TabularDataStoreMode.Write || mode == TabularDataStoreMode.WriteStream;
        }

        public override string FileExtension
        {
            get { return "xml"; }
        }

        [Browsable(false)]
        public override ITabularDataOuputStream StreamApi
        {
            get { return this; }
        }

        public static void WriteHeaders(StreamWriter fw, ITableStructure table, string sheetName)
        {
            fw.Write("<Worksheet ss:Name=\"{0}\">\n<ss:Table>\n", sheetName);
            fw.Write("<ss:Row>\n");
            List<string> dataTypeNames = new List<string>();
            foreach (var col in table.Columns)
            {
                fw.Write(" <ss:Cell ss:StyleID=\"sHeader\"><Data ss:Type=\"String\">{0}</Data></ss:Cell>\n", col.ColumnName);
                if (col.DataType is DbTypeInt)
                {
                    dataTypeNames.Add("Number");
                }
                else if (col.DataType is DbTypeDatetime)
                {
                    dataTypeNames.Add("DateTime");
                }
            }
            fw.Write("</ss:Row>\n");
        }

        public static void WriteRow(StreamWriter fw, ITableStructure table, IBedRecord record, IBedValueFormatter formatter)
        {
            fw.Write("<ss:Row>\n");
            for (int i = 0; i < record.FieldCount; i++)
            {
                string sval = null;
                string stype = "String";
                string style = "";
                record.ReadValue(i);
                var type = record.GetFieldType();
                if (type.IsDateRelated())
                {
                    stype = "DateTime";
                    style = " ss:StyleID=\"sDateTime\" ";
                }
                else if (type.IsNumber())
                {
                    stype = "Number";
                }
                formatter.ReadFrom(record);
                sval = formatter.GetText();
                //object val = record.GetValue(i);
                //if (val is DateTime)
                //{
                //    stype = "DateTime";
                //    sval = ((DateTime)val).ToString("s", CultureInfo.InvariantCulture);
                //    style = " ss:StyleID=\"sDateTime\" ";
                //}
                //else if (val != null && val.GetType().IsNumberType())
                //{
                //    stype = "Number";
                //    sval = Convert.ToString(val, CultureInfo.InvariantCulture);
                //}
                //if (sval == null) sval = XmlTool.ObjectToString(val);
                fw.Write("<ss:Cell {2}><Data ss:Type=\"{0}\">{1}</Data></ss:Cell>\n", stype, XmlTool.QuoteEntities(sval), style);
            }
            fw.Write("</ss:Row>\n");
        }

        #region ITabularDataOuputStream Members

        public void WriteStart(StreamWriter fw, ITableStructure table, ref object manager)
        {
            var fmtset = new DataFormatSettings();
            SettingsTool.CopySettingsPage(FormatSettings, fmtset);
            fmtset.DateTimeFormat = "s";
            fw.Write(IoRes.excelxml_start.Replace("#HDRCOLOR#", m_headerColor.ToWebName()));
            WriteHeaders(fw, table, m_sheetName);
            manager = new Manager { formatter = new BedValueFormatter(fmtset) };
        }

        public void WriteRecord(StreamWriter fw, ITableStructure table, IBedRecord record, int index, object manager)
        {
            var mgr = (Manager)manager;
            WriteRow(fw, table, record, mgr.formatter);
        }

        public void WriteEnd(StreamWriter fw, ITableStructure table, object manager)
        {
            fw.Write("</ss:Table>\n");
            fw.Write("</Worksheet>\n");
            fw.Write("</Workbook>\n");
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
        }
    }

    [QuickExport(Name = "open_in_excel", Title = "s_open_in_excel")]
    public class ExcelQuickExport : QuickExportBase
    {
        public override ITabularDataStore GetDataStore()
        {
            var res = new ExcelXmlDataStore();
            var place = new FilePlaceExternalApp();
            place.SetFileHolderInfo(res);
            res.FilePlace = place;
            return res;
        }
    }
}
