using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;

namespace Plugin.mssql
{
    public class MsSqlParser : SqlParser
    {
        public MsSqlParser(ISqlTokenizer tokenizer, ISqlDialect dialect)
            : base(tokenizer, dialect)
        {
        }

        protected override SqlSpecialConstant GetFunctionAsConstant(string function)
        {
            switch (function.ToLower())
            {
                case "getdate": return SqlSpecialConstant.Current_Timestamp;
                case "getutcdate": return SqlSpecialConstant.Utc_Timestamp;
            }
            return SqlSpecialConstant.None;
        }
    }
}
