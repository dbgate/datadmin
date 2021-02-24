using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;

namespace Plugin.effiproz
{
    public class EfzQuerySplitter : QuerySplitterBase
    {
        public EfzQuerySplitter(ISqlDialect dialect)
            : base(dialect)
        {
        }

        protected override void ProcessLine(string line)
        {
            if (line.Trim() == "\\" || line.Trim().ToUpper() == "GO")
            {
                YieldCurrentQuery();
                return;
            }
            else
            {
                AddToCurrentQuery(line + "\n");
            }
        }
    }
}
