using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;

namespace Plugin.oracle
{
    public class OracleDumpLoader  : DumpLoaderBase
    {
        public OracleDumpLoader(ISqlDialect dialect)
            : base(dialect)
        {
        }

        protected override bool UseSpecificSplitter
        {
            get { return true; }
        }
    }

    [DatabaseLoader(Name = "oracle_dumploader", SupportsDirectUse = false)]
    public class OracleDatabaseLoader : SqlDumpLoaderBase
    {
        public override string GetTitle()
        {
            return "Oracle Dump Loader";
        }

    }
}
