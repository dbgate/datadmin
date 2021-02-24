using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.ComponentModel;

namespace Plugin.textio
{
    [TabularDataStore(Name = "formatted_text", Title = "s_formatted_text", Description = "s_tsrc_formatted_text_desc", RequiredFeature = TableTextExportFeature.Test)]
    public class FormattedTextDataStore : FileWithFormatDataStoreBase, ITabularDataOuputStream
    {
        private AddonHolder m_textFormatter = TextFormatterAddonType.Instance.FindHolder("html");
        private ITextFormatter m_fmt = new HtmlGenerator();

        public override ITabularDataOuputStream StreamApi
        {
            get { return this; }
        }

        [DatAdmin.DisplayName("s_text_formatter")]
        [RegisterItemType(typeof(TextFormatterAddonType))]
        [TypeConverter(typeof(RegisterItemTypeConverter))]
        [XmlElem("TextFormatter")]
        public AddonHolder TextFormatter
        {
            get { return m_textFormatter; }
            set { m_textFormatter = value; m_fmt = (ITextFormatter)m_textFormatter.CreateInstance(); }
        }

        #region ITabularDataOuputStream Members

        public void WriteStart(System.IO.StreamWriter fw, ITableStructure table, ref object manager)
        {
            m_fmt.Writer = fw;
            m_fmt.BeginFile("");
            m_fmt.BeginTable(new TableStyle { Border = 1 });
            m_fmt.BeginRow(true);
            foreach (var col in table.Columns) m_fmt.HeadingCell(col.ColumnName);
            m_fmt.EndRow(true);
            m_fmt.Writer = null;
            var mgr = new Manager();
            mgr.formatter = new BedValueFormatter(FormatSettings);
            manager = mgr;
        }

        public void WriteRecord(System.IO.StreamWriter fw, ITableStructure table, IBedRecord record, int index, object manager)
        {
            var mgr = (Manager)manager;
            m_fmt.Writer = fw;
            m_fmt.BeginRow(false);
            for (int i = 0; i < record.FieldCount; i++)
            {
                record.ReadValue(i);
                mgr.formatter.ReadFrom(record);
                m_fmt.Cell(mgr.formatter.GetText());
            }
            m_fmt.EndRow(false);
            m_fmt.Writer = null;
        }

        public void WriteEnd(System.IO.StreamWriter fw, ITableStructure table, object manager)
        {
            m_fmt.Writer = fw;
            m_fmt.EndTable();
            m_fmt.EndFile();
            m_fmt.Writer = null;
        }

        [Browsable(false)]
        public bool RequireSingleStream
        {
            get { return false; }
        }

        #endregion

        public override bool SupportsMode(TabularDataStoreMode mode)
        {
            return mode == TabularDataStoreMode.Write || mode == TabularDataStoreMode.WriteStream;
        }

        [Browsable(false)]
        public override string FileExtension
        {
            get { return m_fmt.FileExtension; }
        }

        protected override ITableStructure DoGetRowFormat()
        {
            throw new NotImplementedError("DAE-00148");
        }

        protected override void DoRead(IDataQueue queue)
        {
            throw new NotImplementedError("DAE-00149");
        }

        public override void ClearLoadCaches()
        {
            base.ClearLoadCaches();
        }

        class Manager
        {
            internal IBedValueFormatter formatter;
        }
    }
}
