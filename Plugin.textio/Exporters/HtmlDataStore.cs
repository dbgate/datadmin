using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using System.ComponentModel;
using System.Drawing.Design;
using DatAdmin;

namespace Plugin.textio
{
    public abstract class GenericTextDataStoreBase : FileWithFormatDataStoreBase, ITabularDataOuputStream
    {
        [Category("s_file")]
        [DatAdmin.DisplayName("s_file_begin")]
        [Editor(typeof(TemplateTextEditor), typeof(UITypeEditor))]
        [XmlElem]
        public string FileBegin { get; set; }

        [Category("s_file")]
        [DatAdmin.DisplayName("s_file_end")]
        [Editor(typeof(TemplateTextEditor), typeof(UITypeEditor))]
        [XmlElem]
        public string FileEnd { get; set; }

        [Category("s_data")]
        [DatAdmin.DisplayName("s_row_begin")]
        [Editor(typeof(TemplateTextEditor), typeof(UITypeEditor))]
        [XmlElem]
        public string RowBegin { get; set; }

        [Category("s_data")]
        [DatAdmin.DisplayName("s_row_end")]
        [Editor(typeof(TemplateTextEditor), typeof(UITypeEditor))]
        [XmlElem]
        public string RowEnd { get; set; }

        [Category("s_data")]
        [DatAdmin.DisplayName("s_cell_begin")]
        [Editor(typeof(TemplateTextEditor), typeof(UITypeEditor))]
        [XmlElem]
        public string CellBegin { get; set; }

        [Category("s_data")]
        [DatAdmin.DisplayName("s_cell_end")]
        [Editor(typeof(TemplateTextEditor), typeof(UITypeEditor))]
        [XmlElem]
        public string CellEnd { get; set; }

        [Category("s_data")]
        [DatAdmin.DisplayName("s_cell_value")]
        [Editor(typeof(TemplateTextEditor), typeof(UITypeEditor))]
        [XmlElem]
        public string CellValue { get; set; }

        [Category("s_data")]
        [DatAdmin.DisplayName("s_cell_separator")]
        [Editor(typeof(TemplateTextEditor), typeof(UITypeEditor))]
        [XmlElem]
        public string CellSeparator { get; set; }

        [Category("s_data")]
        [DatAdmin.DisplayName("s_row_separator")]
        [Editor(typeof(TemplateTextEditor), typeof(UITypeEditor))]
        [XmlElem]
        public string RowSeparator { get; set; }

        [Category("s_header")]
        [DatAdmin.DisplayName("s_header_row_begin")]
        [Editor(typeof(TemplateTextEditor), typeof(UITypeEditor))]
        [XmlElem]
        public string HeaderRowBegin { get; set; }

        [Category("s_header")]
        [DatAdmin.DisplayName("s_header_row_end")]
        [Editor(typeof(TemplateTextEditor), typeof(UITypeEditor))]
        [XmlElem]
        public string HeaderRowEnd { get; set; }

        [Category("s_header")]
        [DatAdmin.DisplayName("s_cell_begin")]
        [Editor(typeof(TemplateTextEditor), typeof(UITypeEditor))]
        [XmlElem]
        public string HeaderCellBegin { get; set; }

        [Category("s_header")]
        [DatAdmin.DisplayName("s_cell_end")]
        [Editor(typeof(TemplateTextEditor), typeof(UITypeEditor))]
        [XmlElem]
        public string HeaderCellEnd { get; set; }

        [Category("s_header")]
        [DatAdmin.DisplayName("s_cell_value")]
        [Editor(typeof(TemplateTextEditor), typeof(UITypeEditor))]
        [XmlElem]
        public string HeaderCellValue { get; set; }

        [Category("s_header")]
        [DatAdmin.DisplayName("s_cell_separator")]
        [Editor(typeof(TemplateTextEditor), typeof(UITypeEditor))]
        [XmlElem]
        public string HeaderCellSeparator { get; set; }

        protected override ITableStructure DoGetRowFormat()
        {
            throw new NotImplementedError("DAE-00354");
        }

        protected override void DoRead(IDataQueue queue)
        {
            throw new NotImplementedError("DAE-00355");
        }

        private string ProcessTemplate(string value, params string[] args)
        {
            if (value == null) return "";
            value = value.ReplaceCEscapes();
            value = value.Replace("$[NL]", "\r\n");
            for (int i = 0; i < args.Length; i += 2)
            {
                value = value.Replace(args[i], args[i + 1]);
            }
            return value;
        }

        public override bool SupportsMode(TabularDataStoreMode mode)
        {
            return mode == TabularDataStoreMode.Write || mode == TabularDataStoreMode.WriteStream;
        }

        public override void ClearLoadCaches()
        {
            base.ClearLoadCaches();
        }

        [Browsable(false)]
        public override ITabularDataOuputStream StreamApi
        {
            get { return this; }
        }

        #region ITabularDataOuputStream Members

        public void WriteStart(StreamWriter fw, ITableStructure table, ref object manager)
        {
            manager = new Manager { formatter = new BedValueFormatter(FormatSettings) };
            fw.Write(ProcessTemplate(FileBegin));

            bool wascell = false;
            fw.Write(ProcessTemplate(HeaderRowBegin));
            foreach (IColumnStructure col in table.Columns)
            {
                if (wascell) fw.Write(ProcessTemplate(HeaderCellSeparator));
                fw.Write(ProcessTemplate(HeaderCellBegin));
                fw.Write(ProcessTemplate(HeaderCellValue, "$[NAME]", col.ColumnName));
                fw.Write(ProcessTemplate(HeaderCellEnd));
            }
            fw.Write(ProcessTemplate(HeaderRowEnd));
        }

        public void WriteRecord(StreamWriter fw, ITableStructure table, IBedRecord record, int index, object manager)
        {
            var mgr = (Manager)manager;
            if (index > 0) fw.Write(ProcessTemplate(RowSeparator));
            fw.Write(ProcessTemplate(RowBegin));
            bool was = false;
            for (int i = 0; i < record.FieldCount; i++)
            {
                record.ReadValue(i);
                mgr.formatter.ReadFrom(record);
                if (was) fw.Write(ProcessTemplate(CellSeparator));
                fw.Write(ProcessTemplate(CellBegin));
                string val = XmlTool.ObjectToString(mgr.formatter.GetText());
                fw.Write(ProcessTemplate(CellValue, "$[VALUE]", val, "$[NAME]", record.GetName(i)));
                fw.Write(ProcessTemplate(CellEnd));
                was = true;
            }
            fw.Write(ProcessTemplate(RowEnd));
        }

        public void WriteEnd(StreamWriter fw, ITableStructure table, object manager)
        {
            fw.Write(ProcessTemplate(FileEnd));
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

    [TabularDataStore(Name = "html_file", Title = "s_html_file", Description = "s_html_file_texp_desc", RequiredFeature = TableTextExportFeature.Test)]
    public class HtmlDataStore : GenericTextDataStoreBase
    {
        public HtmlDataStore()
        {
            FileBegin = "<html><body><table border='1'>";
            FileEnd = "</table></body></html>";
            RowBegin = "<tr>";
            RowEnd = "</tr>";
            CellBegin = "<td>";
            CellEnd = "</td>";
            CellValue = "$[VALUE]";
            RowSeparator = "$[NL]";
            HeaderRowBegin = "<tr>";
            HeaderRowEnd = "</tr>";
            HeaderCellBegin = "<th>";
            HeaderCellEnd = "</th>";
            HeaderCellValue = "$[NAME]";
        }

        public override string FileExtension
        {
            get { return "html"; }
        }
    }

    [TabularDataStore(Name = "generic_text_file", Title = "s_generic_text_file", Description = "s_generic_text_file_texp_desc", RequiredFeature = TableTextExportFeature.Test)]
    public class GenericTextDataStore : GenericTextDataStoreBase
    {
        public GenericTextDataStore()
        {
            CellSeparator = ";";
            CellValue = "$[VALUE]";
            RowSeparator = "$[NL]";
        }

        public override string FileExtension
        {
            get { return "txt"; }
        }
    }
}
