using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.ComponentModel;
using System.IO;

namespace Plugin.textio
{
    [DatabaseWriter(Name = "dbdocs", Title = "s_database_documentation", Description = "s_database_documentation_desc", RequiredFeature = DatabaseDocsWriterFeature.Test)]
    public class DatabaseDocsWriter : FileDatabaseWriter
    {
        private AddonHolder m_textFormatter = TextFormatterAddonType.Instance.FindHolder("html");
        private ITextFormatter m_fmt = new HtmlGenerator();

        [Browsable(false)]
        public override DatabaseWriterCaps WriterCaps
        {
            get
            {
                return new DatabaseWriterCaps
                {
                    AllFlags = false,
                    AcceptStructure = true,
                    MultipleSchema = true
                };
            }
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

        string m_language = Texts.Language;
        [DatAdmin.DisplayName("s_language")]
        [TypeConverter(typeof(LanguageTypeConverter))]
        public string Language
        {
            get { return m_language; }
            set { m_language = value; }
        }

        public override void WriteStructureBeforeData(IDatabaseStructure db)
        {
            using (StreamWriter sw = new StreamWriter(GetWorkingFileName()))
            {
                m_fmt.Writer = sw;
                m_fmt.Language = Language;
                m_fmt.BeginFile("s_database");
                m_fmt.H2("s_tables");
                foreach (var tbl in db.Tables.SortedByKey<ITableStructure, NameWithSchema>(tbl => tbl.FullName))
                {
                    m_fmt.H3(String.Format("{0} {1}", Texts.LangGet("s_table", m_language), tbl));
                    m_fmt.BeginTable(new TableStyle { Border = 1, WidthPercent = 100 });

                    m_fmt.BeginRow(true);
                    m_fmt.HeadingCell("s_name"); m_fmt.HeadingCell("s_type"); m_fmt.HeadingCell("s_nullable");
                    m_fmt.HeadingCell("s_default_value"); m_fmt.HeadingCell("s_keys");
                    m_fmt.EndRow(true);

                    foreach (var col in tbl.Columns)
                    {
                        m_fmt.BeginRow(false);
                        if (col.IsNullable) m_fmt.Cell(col.ColumnName);
                        else m_fmt.BeginCell(false).Bold(col.ColumnName).EndCell(false);
                        m_fmt.Cell(col.DataType);
                        m_fmt.Cell(col.IsNullable ? "s_yes" : "s_no");
                        m_fmt.Cell(col.DefaultValue.SafeGetSql(Dialect));

                        List<string> keys = new List<string>();
                        foreach (IPrimaryKey pk in tbl.GetConstraints<IPrimaryKey>())
                        {
                            if (pk.Columns.IndexOfIf(c => c.ColumnName == col.ColumnName) >= 0) keys.Add("PK");
                        }
                        foreach (IForeignKey fk in tbl.GetConstraints<IForeignKey>())
                        {
                            if (fk.Columns.IndexOfIf(c => c.ColumnName == col.ColumnName) >= 0) keys.Add("FK->" + fk.PrimaryKeyTable.ToString());
                        }
                        m_fmt.Cell(keys.CreateDelimitedText(", "));
                        m_fmt.EndRow(false);
                    }
                    m_fmt.EndTable();
                }
                m_fmt.EndFile();
                m_fmt.Writer = null;
            }
        }

        public override void CloseConnection()
        {
            base.CloseConnection();
            FinalizeFileName();
        }

        public override string FileExtension
        {
            get { return m_fmt.FileExtension; }
        }
    }
}
