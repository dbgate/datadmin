using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using System.ComponentModel;

namespace DatAdmin
{
    [TabularDataStore(Name = "sql_file", Title = "s_sql_file", Description = "s_sql_file_texp_desc")]
    public class SqlDataStore : FileDataStoreBase, ITabularDataOuputStream
    {
        //char m_quoteCharacter = '\'';

        //[DisplayName("s_quote_character")]
        //[Description("s_quote_character_desc")]
        //[XmlElem]
        //public char QuoteCharacter
        //{
        //    get { return m_quoteCharacter; }
        //    set { m_quoteCharacter = value; }
        //}

        //string m_escapedQuote = "''";

        //[DisplayName("s_escaped_quote")]
        //[Description("s_escaped_quote_desc")]
        //[XmlElem]
        //public string EscapedQuote
        //{
        //    get { return m_escapedQuote; }
        //    set { m_escapedQuote = value; }
        //}

        ISqlDialect m_dialect;
        [DisplayName("s_dialect")]
        [TypeConverter(typeof(DialectTypeConverter))]
        [XmlElem]
        public ISqlDialect Dialect
        {
            get { return m_dialect; }
            set { m_dialect = value; }
        }

        string m_statementSeparator = ";\\n";
        [DisplayName("s_statement_separator")]
        [Description("s_statement_separator_desc")]
        [XmlElem]
        public string StatementSeparator
        {
            get { return m_statementSeparator; }
            set { m_statementSeparator = value; }
        }

        string m_tableName = "data";

        [DisplayName("s_table_name")]
        [XmlElem]
        public string TableName
        {
            get { return m_tableName; }
            set { m_tableName = value; }
        }

        [Browsable(false)]
        public override AddonType AddonType
        {
            get { return TabularDataStoreAddonType.Instance; }
        }

        protected override ITableStructure DoGetRowFormat()
        {
            throw new NotImplementedError("DAE-00229");
        }

        protected override void DoRead(IDataQueue queue)
        {
            throw new NotImplementedError("DAE-00230");
        }

        public override bool SupportsMode(TabularDataStoreMode mode)
        {
            return mode == TabularDataStoreMode.Write || mode == TabularDataStoreMode.WriteStream;
        }

        public override void ClearLoadCaches()
        {
            base.ClearLoadCaches();
        }

        public override string FileExtension
        {
            get { return "sql"; }
        }

        [Browsable(false)]
        public override ITabularDataOuputStream StreamApi
        {
            get { return this; }
        }

        IDialectDataAdapter m_dda;

        #region ITabularDataOuputStream Members

        public void WriteStart(StreamWriter fw, ITableStructure table, ref object manager)
        {
        }

        public void WriteRecord(StreamWriter fw, ITableStructure table, IBedRecord record, int index, object manager)
        {
            if (m_dda == null) m_dda = (m_dialect ?? GenericDialect.Instance).CreateDataAdapter();
            fw.Write("INSERT INTO ");
            fw.Write(m_tableName);
            fw.Write(" (");
            bool was = false;
            for (int i = 0; i < record.FieldCount; i++)
            {
                if (was) fw.Write(",");
                fw.Write(record.GetName(i));
                was = true;
            }
            fw.Write(") VALUES (");
            was = false;
            for (int i = 0; i < record.FieldCount; i++)
            {
                if (was) fw.Write(",");
                record.ReadValue(i);
                fw.Write(m_dda.GetSqlLiteral(record));
                //TypeStorage type = record.GetFieldType();

                //if (record.IsDBNull(i))
                //{
                //    fw.Write("NULL");
                //}
                //else
                //{
                //    fw.Write(m_quoteCharacter);
                //    string val = XmlTool.ObjectToString(record.GetValue(i));
                //    val = val.Replace("" + m_quoteCharacter, m_escapedQuote);
                //    fw.Write(val);
                //    fw.Write(m_quoteCharacter);
                //}
                was = true;
            }
            fw.Write(")");
            fw.Write(m_statementSeparator.ReplaceCEscapes());
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
    }
}
