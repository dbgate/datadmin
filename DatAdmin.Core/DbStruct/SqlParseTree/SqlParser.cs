using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DatAdmin
{
    public partial class SqlParser : ParserBase, ISqlParser
    {
        new ISqlTokenizer m_tokenizer;
        //ISqlDialect m_dialect;
        protected OperatorGroup[] m_operatorTable;

        public SqlParser(ISqlTokenizer tokenizer, ISqlDialect dialect)
            : base(tokenizer)
        {
            tokenizer.SetSqlReservedWords(dialect.NoContextReservedWords);
            AllowOperators = true;
            AllowSpecialConstantReplacement = false;
            m_tokenizer = tokenizer;
        }

        protected OperatorGroup[] OperatorTable
        {
            get
            {
                if (m_operatorTable == null && AllowOperators) InitOperatorTable();
                return m_operatorTable;
            }
        }
        private void InitOperatorTable()
        {
            m_operatorTable = GetOperatorTable();
            for (int i = 1; i < m_operatorTable.Length; i++)
            {
                m_operatorTable[i - 1].HigherPriority = m_operatorTable[i];
            }
            HashSetEx<string> ops = new HashSetEx<string>();
            foreach (var g in m_operatorTable)
            {
                foreach (var op in g.EnumOperators())
                {
                    if (op.IsSymbol)
                    {
                        foreach (var sym in op.Tokens)
                        {
                            ops.Add(sym);
                        }
                    }
                }
            }
            m_tokenizer.SetMultiSymbolOperators(ops);
        }

        public bool AllowOperators { get; set; }
        public bool AllowSpecialConstantReplacement { get; set; }

        public static SqlExpression ParseDefaultValue(string expr, ISqlDialect dialect)
        {
            if (expr == null) return null;
            if (expr.Trim() == "") return null;
            if (dialect == null) dialect = new GenericDialect();
            try
            {
                ISqlParser par = dialect.CreateParser(expr);
                par.AllowOperators = false;
                par.AllowSpecialConstantReplacement = true;
                var res = par.ParseExpression();
                if (res != null && res.XmlPersistent) return res;
            }
            catch { }
            return new SpecificSqlExpression(expr, dialect != null ? dialect.DialectName : null, SymbolPosition.WholeString(expr));
        }

        public bool IsEof { get { return m_tokenizer.IsEof; } }

        protected virtual SqlSpecialConstant GetFunctionAsConstant(string function)
        {
            return SqlSpecialConstant.None;
        }

        protected virtual SqlSpecialConstant GetSymbolAsConstant(string data)
        {
            return SqlSpecialConstant.None;
        }

        public virtual OperatorGroup[] GetOperatorTable()
        {
            return new OperatorGroup[]
            {
                new UnaryPrefixOperatorGroup("-"),
                new BinaryOperatorGroup("*", "/", "%"),
                new BinaryOperatorGroup("+", "-"),
                new BinaryOperatorGroup("=", "<>", "<=", ">=", "<", ">", "IS", "LIKE", "IS NOT", "IN"),
                new ComplexOperatorGroup(ComplexOperatorType.Between | ComplexOperatorType.Case),
                new UnaryPrefixOperatorGroup("NOT"),
                new BinaryOperatorGroup("AND"),
                new BinaryOperatorGroup("OR"),
                new UnaryPrefixOperatorGroup("DISTINCT", "ALL", "ANY")
            };
        }

        public ISqlTokenizer Tokernizer
        {
            get { return m_tokenizer; }
        }

        //public SqlExpression ParseExpression()
        //{
        //    if (!IsEof)
        //    {
        //        if (Current == SqlTokenType.Number)
        //        {
        //            NextToken();
        //            if (!IsEof) return null;
        //            return new ConstNumberSqlExpression(Double.Parse(m_tokenizer.CurrentData), null);
        //        }
        //        if (Current == SqlTokenType.String)
        //        {
        //            NextToken();
        //            if (!IsEof) return null;
        //            return new ConstStringSqlExpression(m_tokenizer.CurrentData, null);
        //        }
        //        if (Current == SqlTokenType.IdentOrKeyword)
        //        {
        //            if (m_tokenizer.CurrentData.ToLower() == "null") return new NullSqlExpression(m_tokenizer.Position);
        //        }
        //        if (Current == SqlTokenType.IdentOrKeyword || m_tokenizer.Current == SqlTokenType.QuotedIdent)
        //        {
        //            string data = CurrentData;
        //            NextToken();
        //            if (IsEof)
        //            {
        //                SqlSpecialConstant sc = GetSymbolAsConstant(data);
        //                if (sc != SqlSpecialConstant.None) return new SpecialConstantSqlExpression(sc, null);
        //                return null;
        //            }
        //            if (IsSymbol("("))
        //            {
        //                SkipSymbol("(");
        //                SkipSymbol(")");
        //                if (!IsEof) return null;
        //                SqlSpecialConstant sc = GetFunctionAsConstant(data);
        //                if (sc != SqlSpecialConstant.None) return new SpecialConstantSqlExpression(sc, null);
        //            }
        //        }
        //    }
        //    return null;
        //}

        public SqlIdentifier ParseIdentifier()
        {
            if (Current == TokenType.IdentOrKeyword || Current == TokenType.QuotedIdent)
            {
                var res = new SqlIdentifier(Current == TokenType.QuotedIdent, CurrentData, CurrentOriginal);
                NextToken();
                return res;
            }
            throw CreateParseError("DAE-00251 identifier expected");
        }

        //public SqlNameWithSchema ParseNameWithSchema()
        //{
        //    SqlIdentifier first = ParseIdentifier();
        //    if (IsSymbol("."))
        //    {
        //        SymbolPosition dot = CurrentOriginal;
        //        NextToken();
        //        SqlIdentifier second = ParseIdentifier();
        //        return new SqlNameWithSchema(first, dot, second);
        //    }
        //    return new SqlNameWithSchema(first);
        //}

        //private SqlCondition ParseTermCondition()
        //{
        //    if (IsSymbol("(")) return ParseCondition();
        //    else
        //    {
        //        SqlExpression left = ParseExpression();
        //        if (IsSymbol(SqlTokenizer.REL_OPERATORS))
        //        {
        //            SymbolPosition rel = CurrentOriginal;
        //            string op = CurrentData;
        //            NextToken();
        //            SqlExpression right = ParseExpression();
        //            return new SqlRelExpression(left, rel, op, right);
        //        }
        //    }
        //}

        //private SqlCondition ParseNotCondition()
        //{
        //    if (IsKeyword("NOT"))
        //    {
        //        SymbolPosition notpos = CurrentOriginal;
        //        NextToken();
        //        SqlCondition cond = ParseTermCondition();
        //        return new SqlNotCondition(notpos, cond);
        //    }
        //    return ParseTermCondition();
        //}

        //private SqlCondition ParseAndCondition()
        //{
        //    SqlCondition left = ParseNotCondition();
        //    if (IsKeyword("AND"))
        //    {
        //        SymbolPosition conj = CurrentOriginal;
        //        NextToken();
        //        SqlCondition right = ParseNotCondition();
        //        return new SqlAndCondition(left, conj, right);
        //    }
        //    return left;
        //}

        //private SqlCondition ParseOrCondition()
        //{
        //    SqlCondition left = ParseAndCondition();
        //    if (IsKeyword("OR"))
        //    {
        //        SymbolPosition conj = CurrentOriginal;
        //        NextToken();
        //        SqlCondition right = ParseAndCondition();
        //        return new SqlOrCondition(left, conj, right);
        //    }
        //    return left;
        //}

        //public SqlCondition ParseCondition()
        //{
        //    if (IsSymbol("("))
        //    {
        //        SymbolPosition leftb = CurrentOriginal;
        //        NextToken();
        //        SqlCondition cond = ParseCondition();
        //        SymbolPosition rightb = CurrentOriginal;
        //        SkipSymbol(")");
        //        return new SqlConditionInBracket(leftb, cond, rightb);
        //    }
        //    return ParseOrCondition();
        //}

        // if isEndToken is NULL, list must have at least element
        private SqlExpressionList ParseExprList(Func<bool> isEndToken)
        {
            List<SqlExprListItem> args = new List<SqlExprListItem>();
            for(;;)
            {
                if (isEndToken != null && isEndToken()) break;
                SymbolPosition commapos = null;
                if (args.Count > 0)
                {
                    if (isEndToken == null && !IsSymbol(",")) break; // no next expression
                    commapos = SkipSymbol(",");
                }
                SqlExpression arg = ParseExpression();
                var sqlarg = new SqlExprListItem(args.Count == 0, commapos, arg);
                args.Add(sqlarg);
            }
            return new SqlExpressionList(args);
        }

        private bool IsCloseBracket() { return IsSymbol(")"); }

        protected SqlExpression ParseExprInBracket()
        {
            if (!IsSymbol("(")) return null;
            var leftb = SkipSymbol("(");
            if (IsKeyword("select"))
            {
                SqlSelect select = ParseSelect();
                SymbolPosition rightb = SkipSymbol(")");
                return new SqlSelectExpression(leftb, select, rightb);
            }
            else
            {
                SqlExpressionList inner = ParseExprList(IsCloseBracket);
                SymbolPosition rightb = SkipSymbol(")");
                return new SqlExpressionInBracket(leftb, inner, rightb);
            }
        }

        protected ConstStringSqlExpression ParseString()
        {
            if (Current != TokenType.StringSingle) return null;
            SymbolPosition pos = CurrentOriginal;
            var data = CurrentData;
            object specdata = CurrentSpecData; // specdata zatim neumime vyuzit
            NextToken();
            return new ConstStringSqlExpression(data, pos);
        }

        private SqlExpression ParseExprTerm()
        {
            if (IsSymbol("("))
            {
                return ParseExprInBracket();
            }
            else
            {
                switch (Current)
                {
                    case TokenType.IdentOrKeyword:
                    case TokenType.QuotedIdent:
                        var name = ParseName();
                        var ident = name as SqlIdentifier;
                        bool canbespecial = ident != null && !ident.IsQuoted;

                        if (IsSymbol("(")) // function call
                        {
                            SymbolPosition leftb = CurrentOriginal;
                            NextToken();
                            SqlExpressionList args = ParseExprList(IsCloseBracket);
                            SymbolPosition rightb = SkipSymbol(")");
                            if (AllowSpecialConstantReplacement && canbespecial && args.Items.Count == 0)
                            {
                                // maybe it is special function mappable to constant
                                SqlSpecialConstant sc = GetFunctionAsConstant(ident.Identifier);
                                if (sc != SqlSpecialConstant.None)
                                {
                                    SymbolPosition pos = new SymbolPosition
                                    {
                                        Start = ident.Original.Start,
                                        Stop = rightb.Stop,
                                        Original = Tokernizer.SliceProvider,
                                    };
                                    return new SpecialConstantSqlExpression(sc, pos);
                                }
                            }
                            return new SqlFunctionCall(name, leftb, args, rightb);
                        }

                        if (AllowSpecialConstantReplacement && canbespecial)
                        {
                            if (ident.Identifier.ToUpper() == "NULL") return new NullSqlExpression(ident.Original);
                            // maybe it is special constant
                            SqlSpecialConstant sc = GetSymbolAsConstant(ident.Identifier);
                            if (sc != SqlSpecialConstant.None)
                            {
                                return new SpecialConstantSqlExpression(sc, ident.Original);
                            }
                        }
                        return name;

                    case TokenType.Symbol:
                        {
                            var starpos = SkipSymbol("*");
                            return new SqlStarSymbol(starpos);
                        }
                    case TokenType.Number:
                        {
                            SymbolPosition pos = CurrentOriginal;
                            string numdata = CurrentData;
                            NextToken();
                            return new ConstNumberSqlExpression(Double.Parse(numdata), pos);
                        }
                        break;
                    case TokenType.StringSingle:
                        {
                            return ParseString();
                        }
                        break;
                    default:
                        throw CreateParseError("DAE-00252 Unexpected token:" + Current.ToString());
                        break;
                }
            }
        }

        protected SqlNameExpression ParseName()
        {
            //bool quoted = Current == SqlTokenType.QuotedIdent;
            //string first = CurrentData;
            List<SqlQualificator> quals = new List<SqlQualificator>();
            SqlIdentifierOrExprSymbol ident = ParseIdentifier();
            while (IsSymbol("."))
            {
                SymbolPosition dotpos = CurrentOriginal;
                NextToken();
                SqlIdentifierOrExprSymbol nextid = null;
                if (Current == TokenType.IdentOrKeyword || Current == TokenType.QuotedIdent)
                {
                    nextid = ParseIdentifier();
                }
                else if (IsSymbol("*"))
                {
                    nextid = new SqlStarSymbol(SkipSymbol("*"));
                }
                quals.Add(new SqlQualificator(ident, dotpos));
                ident = nextid;
            }

            SqlNameExpression name;
            if (quals.Count > 0) name = new SqlQualifiedName(quals, ident);
            else name = ident;

            return name;
        }

        public SqlExpression ParseExpression()
        {
            var ot = OperatorTable;
            if (ot != null && ot.Length > 0)
            {
                return ot[0].ParseFunc(this);
            }
            else
            {
                return ParseExprTerm();
            }
        }

        internal SqlExpression ParseUnaryPrefixExpr(UnaryPrefixOperatorGroup group)
        {
            foreach (var op in group.Operators)
            {
                SymbolPosition[] marks = TestOperator(op);
                if (marks != null)
                {
                    SqlExpression inner = ParseInnerExpression(group);
                    return new SqlUnaryPrefixOperatorExpression(op, marks, inner);
                }
            }
            return ParseInnerExpression(group);
        }

        internal SqlExpression ParseBinaryExpr(BinaryOperatorGroup group)
        {
            SqlExpression left = ParseInnerExpression(group);
            for (; ; )
            {
                bool found = false;
                foreach (var op in group.Operators)
                {
                    SymbolPosition[] marks = TestOperator(op);
                    if (marks != null)
                    {
                        SqlExpression right = ParseInnerExpression(group);
                        left = new SqlBinaryOperatorExpression(left, op, marks, right);
                        found = true;
                    }
                }
                if (!found) break;
            }
            return left;
        }

        private SqlExpression ParseInnerExpression(OperatorGroup group)
        {
            if (group.HigherPriority != null) return group.HigherPriority.ParseFunc(this);
            return ParseExprTerm();
        }

        private SymbolPosition[] TestOperator(OperatorDef op)
        {
            int steps = 0;
            List<SymbolPosition> res = new List<SymbolPosition>();
            while (steps < op.Tokens.Length && op.AcceptTokenType(Current) && CurrentData.ToUpper() == op.Tokens[steps].ToUpper())
            {
                res.Add(CurrentOriginal);
                steps++;
                NextToken();
            }
            if (op.Tokens.Length == steps) return res.ToArray();
            GoBack(steps);
            return null;
        }

        internal SqlExpression ParseComplexExpr(ComplexOperatorGroup group)
        {
            // LL(1) parsing CASE
            if ((group.Type & ComplexOperatorType.Case) != 0 && IsKeyword("CASE"))
            {
                SymbolPosition casepos = CurrentOriginal;
                NextToken();
                SqlExpression caseval = ParseInnerExpression(group);
                List<SqlCaseWhenClause> whens = new List<SqlCaseWhenClause>();
                while (IsKeyword("WHEN"))
                {
                    SymbolPosition whenpos = CurrentOriginal;
                    NextToken();
                    SqlExpression cmpval = ParseInnerExpression(group);
                    SymbolPosition thenpos = SkipKeyword("THEN");
                    SqlExpression thenval = ParseInnerExpression(group);
                    SqlCaseWhenClause when = new SqlCaseWhenClause(whenpos, cmpval, thenpos, thenval);
                    whens.Add(when);
                }
                SqlExpression elseExpr = null;
                SymbolPosition elsePos = null;
                if (IsKeyword("ELSE"))
                {
                    elsePos = CurrentOriginal;
                    NextToken();
                    elseExpr = ParseInnerExpression(group);
                }
                SymbolPosition endpos = CurrentOriginal;
                SkipKeyword("END");
                return new SqlCaseExpression(casepos, caseval, whens, elsePos, elseExpr, endpos);
            }
            SqlExpression left = ParseInnerExpression(group);
            if ((group.Type & ComplexOperatorType.Between) != 0 && IsKeyword("BETWEEN"))
            {
                SymbolPosition betpos = CurrentOriginal;
                NextToken();
                SqlExpression lower = ParseInnerExpression(group);
                SymbolPosition andpos = SkipKeyword("AND");
                SqlExpression upper = ParseInnerExpression(group);
                return new SqlBetweenExpression(left, betpos, lower, andpos, upper);
            }
            return left;
        }

        protected SymbolPosition SkipKeyword(string keyword)
        {
            return SkipKeyword(keyword, true);
        }

        protected SymbolPosition SkipKeyword(string keyword, bool mandatory)
        {
            if (Current != TokenType.IdentOrKeyword && CurrentData.ToUpper() != keyword.ToUpper())
            {
                if (!mandatory) return null;
                throw CreateParseError("DAE-00253 " + keyword + " expected");
            }
            SymbolPosition res = CurrentOriginal;
            NextToken();
            return res;
        }

        public SqlCommand ParseCommand()
        {
            if (IsKeyword("select")) return ParseSelect();
            SqlCommand res = ParseSpecificCommand();
            if (res != null) return res;
            throw CreateParseError("DAE-00254 sql command expected");
        }

        protected virtual SqlCommand ParseSpecificCommand()
        {
            return null;
        }

        public SqlQuotedStringExpression ParseQuotedString(params TokenType[] tokens)
        {
            if (Array.IndexOf(tokens, Current) < 0) return null;
            var res = new SqlQuotedStringExpression(CurrentOriginal, CurrentData, Current);
            NextToken();
            return res;
        }
    }
}

