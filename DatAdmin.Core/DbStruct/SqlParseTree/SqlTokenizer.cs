using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DatAdmin
{
    public class SqlTokenizer : TokenizerBase, ISqlTokenizer
    {
        public SqlTokenizer(TextReader reader, IStringSliceProvider sliceProvider, ISqlDialect dialect)
            : base(reader,sliceProvider)
        {
            m_supportsStringSingle = true;
            if (dialect != null)
            {
                m_stringEscape = dialect.StringEscapeChar;
                m_quoteIdentBegin = dialect.QuoteIdentBegin;
                m_quoteIdentEnd = dialect.QuoteIdentEnd;
            }
        }

        public void SetSqlReservedWords(IEnumerable<string> words)
        {
            m_reservedWords = new HashSetEx<string>(words);
        }

    }
}
