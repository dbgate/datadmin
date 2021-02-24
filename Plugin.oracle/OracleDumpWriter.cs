using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.ComponentModel;
using System.Linq;

namespace Plugin.oracle
{
    public class OracleDumpWriter: SqlDumpWriterBase
    {
        public OracleDumpWriter(ISqlDialect dialect)
            : base(dialect)
        {
        }

        protected override void GetFormatProps(SqlFormatProperties formatProps)
        {
            formatProps.CommandSeparator = "\n/\n";
        }

        public override void FillTable(ITableStructure table, IDataQueue queue, TableCopyOptions opts)
        {
            var colnames = from c in queue.GetRowFormat.Columns select c.ColumnName;
            bool autoinc = queue.GetRowFormat.FindAutoIncrementColumn() != null;
            if (autoinc) m_dmp.AllowIdentityInsert(table.FullName, true);

            try
            {
                if (Cfg != null && Cfg.JoinInserts)
                {
                    int rowsInBatch = 0;
                    while (!queue.IsEof)
                    {
                        if (Cfg.BatchLimit > 0 && rowsInBatch >= Cfg.BatchLimit)
                        {
                            m_dmp.EndCommand();
                            rowsInBatch = 0;
                        }
                        if (rowsInBatch > 0) m_dmp.Put(";\n");
                        IBedRecord row = queue.GetRecord();
                        m_dmp.Put("^insert ^into %f (%,i) ^values (%,v)", table, colnames, row);
                        rowsInBatch++;
                    }
                    if (rowsInBatch > 0)
                    {
                        m_dmp.EndCommand();
                    }
                }
                else
                {
                    while (!queue.IsEof)
                    {
                        IBedRecord row = queue.GetRecord();
                        m_dmp.PutCmd("^insert ^into %f (%,i) ^values (%,v)", table, colnames, row);
                    }
                }
            }
            finally
            {
                queue.CloseReading();
            }
            if (autoinc) m_dmp.AllowIdentityInsert(table.FullName, false);
        }


        OracleDumpWriterConfig Cfg { get { return Config as OracleDumpWriterConfig; } }
    }

    [SqlDumpWriterConfig(Name = "oracle")]
    public class OracleDumpWriterConfig : SqlDumpWriterConfig
    {

        public OracleDumpWriterConfig()
        {
            JoinInserts = true;
            BatchLimit = 64;
        }

        [XmlElem]
        [TypeConverter(typeof(YesNoTypeConverter))]
        public bool JoinInserts { get; set; }

        [XmlElem]
        public int BatchLimit { get; set; }
    }
}
