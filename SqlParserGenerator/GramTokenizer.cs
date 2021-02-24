using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.IO;

namespace SqlParserGenerator
{
    public class GramTokenizer : TokenizerBase
    {
        public GramTokenizer(TextReader reader, IStringSliceProvider sliceProvider)
            : base(reader, sliceProvider)
        {
            m_specIdentStart = '@';
            m_checkIdentCase = true;
            m_supportsStringDouble = true;
            m_supportsStringSingle = true;
        }

        protected override HashSetEx<char> GetSymbols()
        {
            return new HashSetEx<char>('[', ']', '{', '}', '|', '=', '(', ')', ';', ',');
        }
    }
}
