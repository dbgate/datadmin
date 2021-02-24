using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using DatAdmin;

namespace Plugin.mysql
{
    public class MySqlTokenizer : SqlTokenizer
    {
        public MySqlTokenizer(TextReader reader, IStringSliceProvider sliceProvider, ISqlDialect dialect)
            : base(reader, sliceProvider, dialect)
        {
            m_supportsStringDouble = true;
        }

        protected override HashSetEx<char> GetSymbols()
        {
            var res = base.GetSymbols();
            res.Add('@');
            return res;
        }

        protected override bool ParseSpecialToken()
        {
            if (CurrentCh == '_')
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(CurrentCh);
                GoToNextChar();
                while (Char.IsLetterOrDigit(CurrentCh))
                {
                    sb.Append(CurrentCh);
                    GoToNextChar();
                }
                if (CurrentCh == '\'')
                {
                    CurrentSpecData = sb.ToString();
                    ParseString(TokenType.StringSingle, '\'');
                    return true;
                }
                else
                {
                    ParseIdentOrKeyword(sb.ToString(), TokenType.IdentOrKeyword, true);
                    return true;
                }
            }
            return false;
        }
    }
}
