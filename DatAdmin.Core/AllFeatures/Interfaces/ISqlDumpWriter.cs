using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace DatAdmin
{
    /// <summary>
    /// writes SQL dump to file, is dialect-dependend
    /// has methods equal as DatabaseWriter, is called from SqlDatabaseWriter
    /// </summary>
    public interface ISqlDumpWriter
    {
        SqlDumpWriterConfig Config { get; set; }
        string FileName { get; set; }
        void OpenFile();
        void CloseFile();
        void WriteStructureBeforeData(IDatabaseStructure db);
        void WriteStructureAfterData(IDatabaseStructure db);
        void BeforeFillData();
        void FillTable(ITableStructure table, IDataQueue queue, TableCopyOptions opts);
        void AfterFillData();
    }

    // config is addon so that it can be saved to XML
    [SqlDumpWriterConfig(Name = "base")]
    public class SqlDumpWriterConfig : AddonBase
    {
        public override AddonType AddonType
        {
            get { return SqlDumpWriterConfigAddonType.Instance; }
        }

        [DisplayName("s_include_drop_statement")]
        [XmlElem]
        [TypeConverter(typeof(YesNoTypeConverter))]
        public bool IncludeDropStatement { get; set; }
    }

    public class SqlDumpWriterConfigAttribute : RegisterAttribute { }

    [AddonType]
    public class SqlDumpWriterConfigAddonType : AddonType
    {
        public override Type InterfaceType
        {
            get { return typeof(SqlDumpWriterConfig); }
        }
        public override string Name
        {
            get { return "sqldumpwriterconfig"; }
        }
        public override Type RegisterAttributeType
        {
            get { return typeof(SqlDumpWriterConfigAttribute); }
        }
        public static readonly SqlDumpWriterConfigAddonType Instance = new SqlDumpWriterConfigAddonType();
    }

}
