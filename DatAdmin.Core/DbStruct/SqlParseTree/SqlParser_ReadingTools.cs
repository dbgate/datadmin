using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    partial class SqlParser
    {
        public virtual string ReadName()
        {
            if (Current == TokenType.IdentOrKeyword || Current == TokenType.QuotedIdent)
            {
                string n1 = CurrentData;
                NextToken();
                return n1;
            }
            throw CreateParseError("DAE-00255 Full name expected");
        }

        public virtual NameWithSchema ReadFullName()
        {
            if (Current == TokenType.IdentOrKeyword || Current == TokenType.QuotedIdent)
            {
                string n1 = CurrentData;
                NextToken();
                if (IsSymbol("."))
                {
                    SkipSymbol(".");
                    if (Current != TokenType.QuotedIdent && Current != TokenType.IdentOrKeyword)
                    {
                        throw CreateParseError("DAE-00256 Identifier expected");                        
                    }
                    string n2 = SkipToken();
                    return new NameWithSchema(n1, n2);
                }
                return new NameWithSchema(n1);
            }
            throw CreateParseError("DAE-00257 Full name expected");
        }

        protected virtual string ReadExpr(params string[] endsym)
        {
            int level = 0;
            StringBuilder sb = new StringBuilder();
            bool was = false;
            while (level > 0 || !IsSymbol(endsym))
            {
                if (was) sb.Append(" ");
                if (IsSymbol("(")) level++;
                else if (IsSymbol(")")) level--;
                sb.Append(CurrentOriginal.GetOriginalToken());
                NextToken();
            }
            return sb.ToString();
        }
        protected virtual string ReadExprInBracket()
        {
            SkipSymbol("(");
            int level = 0;
            StringBuilder sb = new StringBuilder();
            bool was = false;
            for(;;)
            {
                if (was) sb.Append(" ");
                if (level == 0 && IsSymbol(")")) return sb.ToString();
                if (IsSymbol("(")) level++;
                else if (IsSymbol(")")) level--;
                sb.Append(CurrentOriginal.GetOriginalToken());
                NextToken();
            }
        }
    }
}
