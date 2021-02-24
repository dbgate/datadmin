using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DatAdmin
{
    public class FulltextSearchParams
    {
        public bool ExactMatch = false;

        public string LikePrefix { get { return ExactMatch ? "" : "%"; } }
        public string LikePostfix { get { return ExactMatch ? "" : "%"; } }
    }

    public interface IDialectDataAdapter
    {
        IBedReader AdaptReader(IDataReader reader);
        string GetSqlLiteral(IBedValueReader reader, DbTypeBase type);
        string GetSqlLiteral(object value, DbTypeBase type);
        void AdaptValue(IBedValueReader reader, DbTypeBase type, IBedValueWriter writer, IBedValueConvertor converter, ILogger logger);
        string GetFulltextSearchExpr(string expr, string substring, FulltextSearchParams pars);
        string FilterNotDumpableCharacters(string value);
    }
}
