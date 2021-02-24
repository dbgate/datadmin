using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.ComponentModel;
using System.Linq;

namespace Plugin.mssql
{
    public class MsSqlDumpWriter : SqlDumpWriterBase
    {
        public MsSqlDumpWriter(ISqlDialect dialect)
            : base(dialect)
        {
        }

        protected override void GetFormatProps(SqlFormatProperties formatProps)
        {
            formatProps.CommandSeparator = "\nGO\n";
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
                        IBedRecord row = queue.GetRecord();
                        m_dmp.Put("^insert ^into %f (%,i) ^values (%,v)\n", table, colnames, row);
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


        MsSqlDumpWriterConfig Cfg { get { return Config as MsSqlDumpWriterConfig; } }
    }

    [SqlDumpWriterConfig(Name = "mssql")]
    public class MsSqlDumpWriterConfig : SqlDumpWriterConfig
    {

        public MsSqlDumpWriterConfig()
        {
            DumpColumnCollation = true;
            JoinInserts = true;
            BatchLimit = 64;
        }

        [XmlElem]
        [TypeConverter(typeof(YesNoTypeConverter))]
        [DatAdmin.DisplayName("s_dump_column_collation")]
        public bool DumpColumnCollation { get; set; }

        [XmlElem]
        [TypeConverter(typeof(YesNoTypeConverter))]
        [DatAdmin.DisplayName("s_join_inserts")]
        public bool JoinInserts { get; set; }

        [XmlElem]
        [DatAdmin.DisplayName("s_batch_limit")]
        public int BatchLimit { get; set; }
    }
}
