using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;

namespace Plugin.sqlite
{
    public class SqliteDataSynAdapter : DataSynAdapterBase
    {
        public SqliteDataSynAdapter(ISqlDialect dialect)
            : base(dialect)
        {
        }

        public override string Md5(string expr)
        {
            return String.Format("md5({0})", expr);
        }

        public override string Concat(IEnumerable<string> exprs)
        {
            return exprs.CreateDelimitedText(" || ");
        }

        public override string GetHashableString(string expr, DbTypeBase type)
        {
            if (type is DbTypeDatetime)
            {
                return String.Format("((strftime('%Y',{0})+0) || '-' || "
                    + "(strftime('%m',{0})+0) || '-' || "
                    + "(strftime('%d',{0})+0) || '-' || "
                    + "(strftime('%H',{0})+0) || '-' || "
                    + "(strftime('%M',{0})+0) || '-' || "
                    + "(strftime('%S',{0})+0))", expr);
            }
            return base.GetHashableString(expr, type);
        }
    }
}
