using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;

namespace Plugin.access
{
    public class AccessDDA : DialectDataAdapterBase
    {
        public AccessDDA(ISqlDialect dialect)
            : base(dialect)
        {
        }

        public override string GetFulltextSearchExpr(string expr, string substring, FulltextSearchParams pars)
        {
            substring = substring.Replace("*", "[*]").Replace("?", "[?]").Replace("_", "[_]").Replace("*", "[*]");
            return String.Format("({0} LIKE {1})", expr, this.GetSqlLiteral(pars.LikePrefix + substring + pars.LikePostfix));
        }
    }
}
