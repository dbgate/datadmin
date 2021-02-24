using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;

namespace Plugin.effiproz
{
    public class EfzDumpLoader : DumpLoaderBase
    {
        public EfzDumpLoader(ISqlDialect dialect)
            : base(dialect)
        {
        }

        protected override bool UseSpecificSplitter
        {
            get { return true; }
        }
    }

    [DatabaseLoader(Name = "effiproz_dumploader", SupportsDirectUse = false)]
    public class EfzDatabaseLoader : SqlDumpLoaderBase
    {
        public override string GetTitle()
        {
            return "EffiProz Dump Loader";
        }
    }
}
