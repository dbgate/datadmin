using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    public static class TokenizerExtension
    {
        public static ParseError CreateParseError(this ITokenizer tokenizer, string msg)
        {
            return new ParseError(msg);
        }

        public static void SkipSymbol(this ITokenizer tokenizer, string symbol)
        {
            if (tokenizer.Current != TokenType.Symbol || tokenizer.CurrentData != symbol) throw tokenizer.CreateParseError("DAE-00297 Expected symbol:" + symbol);
            tokenizer.NextToken();
        }
    }
}
