using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    public abstract class ParserBase
    {
        protected ITokenizer m_tokenizer;

        public ParserBase(ITokenizer tokenizer)
        {
            m_tokenizer = tokenizer;
        }

        protected TokenType Current { get { return m_tokenizer.Current; } }
        protected void NextToken() { m_tokenizer.NextToken(); }
        protected string CurrentData { get { return m_tokenizer.CurrentData; } }
        protected object CurrentSpecData { get { return m_tokenizer.CurrentSpecData; } }
        protected SymbolPosition SkipSymbol(string symbol)
        {
            SymbolPosition res = CurrentOriginal;
            m_tokenizer.SkipSymbol(symbol);
            return res;
        }
        protected SymbolPosition CurrentOriginal { get { return m_tokenizer.Position; } }
        protected ParseError CreateParseError(string msg) { return m_tokenizer.CreateParseError(msg); }
        protected bool IsSymbol(string symbol) { return Current == TokenType.Symbol && CurrentData == symbol; }
        protected bool IsSymbol(string[] symbols)
        {
            if (Current != TokenType.Symbol) return false;
            return Array.IndexOf(symbols, CurrentData) >= 0;
        }
        protected bool IsKeyword(string keyword)
        {
            return (Current == TokenType.IdentOrKeyword || Current == TokenType.Reserved)
                && CurrentData.ToUpper() == keyword.ToUpper();
        }
        protected void GoBack(int steps) { m_tokenizer.GoBack(steps); }
        protected bool IsTerminal(string keyword)
        {
            if (Char.IsLetter(keyword[0])) return IsKeyword(keyword);
            return IsSymbol(keyword);
        }
        protected bool IsTerminal(params string[] keywords)
        {
            foreach (var k in keywords)
            {
                if (IsTerminal(k)) return true;
            }
            return false;
        }

        protected string SkipToken(TokenType token, string err)
        {
            if (Current != token) throw CreateParseError(err);
            return SkipToken();
        }
        protected bool SkipMultiIf(params string[] tokens)
        {
            int beg = MarkPosition();
            foreach (string tok in tokens)
            {
                if (!SkipTokenIf(tok))
                {
                    GoToMark(beg);
                    return false;
                }
            }
            return true;
        }
        protected bool SkipTokenIf(string token)
        {
            if (IsTerminal(token))
            {
                NextToken();
                return true;
            }
            return false;
        }

        protected string SkipToken()
        {
            string res = CurrentData;
            NextToken();
            return res;
        }

        protected void SkipToOneOf(params string[] tokens)
        {
            int level = 0;
            while (level > 0 || !IsTerminal(tokens))
            {
                if (IsSymbol("(")) level++;
                if (IsSymbol(")")) level--;
                NextToken();
            }
        }

        public int MarkPosition()
        {
            return m_tokenizer.MarkPosition();
        }

        public void GoToMark(int mark)
        {
            m_tokenizer.GoToMark(mark);
        }
    }
}
