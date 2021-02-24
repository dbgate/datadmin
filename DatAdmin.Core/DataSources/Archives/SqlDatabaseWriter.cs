using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.IO;
using System.Data;
using System.Linq;
using System.Xml;

namespace DatAdmin
{
    [DatabaseWriter(Name = "generic_sql_dump", Title = "SQL Dump", Description = "s_sql_dump_desc")]
    [HideDefaultPropertyTab]
    public class SqlDatabaseWriter : FileDatabaseWriter
    {
        //private AddonHolder m_usedDialect;
        private ISqlDialect m_dialect = new GenericDialect();
        //private bool m_includeDropStatement;
        private SqlDumpWriterConfig m_dumpConfig;
        private ISqlDumpWriter m_dw;

        [XmlElem("Dialect")]
        [Browsable(false)]
        public ISqlDialect UsedDialect
        {
            get { return m_dialect; }
            set { m_dialect = value; }
        }

        [XmlSubElem]
        [Browsable(false)]
        [TabbedEditor(typeof(SqlDumpWriterEditFrame), TabWeight = 5)]
        public SqlDumpWriterConfig DumpConfig
        {
            get { return m_dumpConfig; }
            set { m_dumpConfig = value; }
        }

        public override ISqlDialect Dialect
        {
            get { return m_dialect; }
        }

        //[Category("SQL")]
        //[DisplayName("s_include_drop_statement")]
        //[XmlElem]
        //[TypeConverter(typeof(YesNoTypeConverter))]
        //public bool IncludeDropStatement
        //{
        //    get { return m_includeDropStatement; }
        //    set { m_includeDropStatement = value; }
        //}

        private ISqlDialect AnyDialect { get { return UsedDialect ?? GenericDialect.Instance; } }

        //[TypeConverter(typeof(ExpandableObjectConverter))]
        //[Browsable(false)]
        //public SqlFormatProperties FormatProps
        //{
        //    get { return m_formatProps; }
        //    set { m_formatProps = value; }
        //}

        public override void OpenConnection()
        {
            if (m_dw != null) return;
            m_dw = AnyDialect.CreateDumpWriter();
            m_dw.Config = m_dumpConfig;
            m_dw.FileName = GetWorkingFileName();
            m_dw.OpenFile();
        }

        public override void CloseConnection()
        {
            m_dw.CloseFile();
            FinalizeFileName();
        }

        public override void WriteStructureBeforeData(IDatabaseStructure db)
        {
            m_dw.WriteStructureBeforeData(db);
        }

        public override void WriteStructureAfterData(IDatabaseStructure db)
        {
            m_dw.WriteStructureAfterData(db);
        }

        public override void FillTable(ITableStructure table, IDataQueue queue, TableCopyOptions opts)
        {
            m_dw.FillTable(table, queue, opts);
        }

        public override void BeforeFillData()
        {
            m_dw.BeforeFillData();
        }

        public override void AfterFillData()
        {
            m_dw.AfterFillData();
        }

        public override void InitializeFromInput(IDatabaseSource input)
        {
            try
            {
                var dialect = input.Dialect;
                if (dialect != null)
                {
                    dialect = dialect.CloneDialect();
                    dialect.SetVersion(new SqlServerVersion(null));
                    UsedDialect = dialect;
                    DumpConfig = dialect.CreateDumpWriterConfig();
                }
            }
            catch { }
        }

        public override DatabaseWriterCaps WriterCaps
        {
            get
            {
                return new DatabaseWriterCaps
                {
                    AllFlags = false,
                    AcceptData = true,
                    AcceptStructure = true,
                    MultipleSchema = m_dialect.DialectCaps.MultipleSchema,
                };
            }
        }

        public override string FileExtension
        {
            get { return "sql"; }
        }

        public override void SaveToXml(XmlElement xml)
        {
            base.SaveToXml(xml);
            //FormatProps.SaveToXml(xml.AddChild("FormatProps"));
        }

        public override void LoadFromXml(XmlElement xml)
        {
            base.LoadFromXml(xml);
            //if (xml.FindElement("FormatProps") != null) FormatProps.LoadFromXml(xml.FindElement("FormatProps"));
        }
    }
}
