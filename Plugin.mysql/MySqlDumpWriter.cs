using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.ComponentModel;
using System.Linq;

namespace Plugin.mysql
{
    public class MySqlDumpWriter : SqlDumpWriterBase
    {
        public MySqlDumpWriter(ISqlDialect dialect)
            : base(dialect)
        {
        }

        protected override void GetFormatProps(SqlFormatProperties formatProps)
        {
            formatProps.CommandSeparator = ";\n"; // override dialect default value
            var mycfg = formatProps.DumpWriterConfig as MySqlDumpWriterConfig;
            if (mycfg != null) mycfg.DumpDelimMode = true;
        }

        protected override void WriteHeader()
        {
            m_fw.Write("-- DatAdmin Native MySQL Dump\n\n/*!40101 SET NAMES utf8 */;\n/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;\n\n");
        }

        protected override void WriteFooter()
        {
            m_fw.Write("\n\n;/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;\n");
        }

        public override void FillTable(ITableStructure table, IDataQueue queue, TableCopyOptions opts)
        {
            var colnames = from c in queue.GetRowFormat.Columns select c.ColumnName;

            try
            {
                if (Wcfg != null && Wcfg.ExtendedInserts)
                {
                    int rowsInBatch = 0;
                    while (!queue.IsEof)
                    {
                        if (Wcfg.BatchLimit > 0 && rowsInBatch >= Wcfg.BatchLimit)
                        {
                            m_dmp.EndCommand();
                            rowsInBatch = 0;
                        }
                        if (rowsInBatch == 0)
                        {
                            m_dmp.Put("^insert ^into %f (%,i) ^values\n", table, colnames);
                        }
                        else
                        {
                            m_dmp.Put(",\n");
                        }
                        IBedRecord row = queue.GetRecord();
                        m_dmp.Put("(%,v)", row);
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
        }

        MySqlDumpWriterConfig Wcfg { get { return Config as MySqlDumpWriterConfig; } }
    }

    [SqlDumpWriterConfig(Name = "mysql")]
    public class MySqlDumpWriterConfig : SqlDumpWriterConfig
    {
        public bool DumpDelimMode;

        public MySqlDumpWriterConfig()
        {
            DumpTableCollation = true;
            DumpTableEngine = true;
            DumpAutoIncrementValues = true;
            ExtendedInserts = true;
            BatchLimit = 64;
        }

        [XmlElem]
        [TypeConverter(typeof(YesNoTypeConverter))]
        public bool DumpTableEngine { get; set; }

        [XmlElem]
        [TypeConverter(typeof(YesNoTypeConverter))]
        public bool DumpAutoIncrementValues { get; set; }

        [XmlElem]
        [TypeConverter(typeof(YesNoTypeConverter))]
        public bool DumpTableCollation { get; set; }

        [XmlElem]
        [TypeConverter(typeof(YesNoTypeConverter))]
        public bool ExtendedInserts { get; set; }

        [XmlElem]
        public int BatchLimit { get; set; }
    }
}
