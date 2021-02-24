using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;

namespace Plugin.postgre
{
    public class PostgreDumpWriter : SqlDumpWriterBase
    {
        public PostgreDumpWriter(ISqlDialect dialect)
            : base(dialect)
        {
        }

        protected override void GetFormatProps(SqlFormatProperties formatProps)
        {
            formatProps.CommandSeparator = ";\n"; // override dialect default value
        }

        protected override void WriteHeader()
        {
            m_fw.Write("-- DatAdmin Native PostgreSQL Dump\n\n");
        }
    }
}
