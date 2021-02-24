using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    public partial class SqlParser
    {
        public SqlCommandClause ParseSelectClause()
        {
            if (IsKeyword("from")) return ParseFromClause(true);
            if (IsKeyword("where")) return ParseWhereClause();
            if (IsKeyword("order")) return ParseOrderByClause();
            if (IsKeyword("group")) return ParseGroupByClause();
            if (IsKeyword("having")) return ParseHavingClause();
            return null;
        }

        protected SqlMultiKeyWord ParseMultiKeyword(params string[] words)
        {
            int steps = 0;
            List<SymbolPosition> res = new List<SymbolPosition>();
            while (steps < words.Length)
            {
                if (!IsKeyword(words[steps]))
                {
                    GoBack(steps);
                    return null;
                }
                res.Add(CurrentOriginal);
                NextToken();
                steps++;
            }
            return new SqlMultiKeyWord(res, words);
        }

        private SqlColRefList ParseCoRefList()
        {
            List<SqlColRefListItem> args = new List<SqlColRefListItem>();
            for (; ; )
            {
                SymbolPosition commapos = null;
                if (args.Count > 0)
                {
                    if (!IsSymbol(",")) break; // no next expression
                    commapos = SkipSymbol(",");
                }
                SqlColRefListItem arg = ParseColListItem(commapos);
                args.Add(arg);
            }
            return new SqlColRefList(args);
        }

        protected SqlColRefListItem ParseColListItem(SymbolPosition commapos)
        {
            var expr = ParseExpression();
            SqlMultiKeyWord modifier = ParseMultiKeyword("asc") ?? ParseMultiKeyword("desc");
            return new SqlColRefListItem(commapos == null, commapos, expr, modifier);
        }

        private SqlCommandClause ParseGroupByClause()
        {
            var prefix = ParseMultiKeyword("group", "by");
            if (prefix == null) return null;
            return new SqlGroupByClause(prefix, ParseCoRefList());
        }

        private SqlCommandClause ParseOrderByClause()
        {
            var prefix = ParseMultiKeyword("order", "by");
            if (prefix == null) return null;
            return new SqlOrderByClause(prefix, ParseCoRefList());
        }

        private SqlCommandClause ParseWhereClause()
        {
            var prefix = ParseMultiKeyword("where");
            if (prefix == null) return null;
            return new SqlWhereClause(prefix, ParseExpression());
        }

        private SqlCommandClause ParseHavingClause()
        {
            var prefix = ParseMultiKeyword("having");
            if (prefix == null) return null;
            return new SqlHavingClause(prefix, ParseExpression());
        }

        private SqlIdentifier ParseAlias()
        {
            if (Current == TokenType.IdentOrKeyword || Current == TokenType.QuotedIdent)
            {
                return ParseIdentifier();
            }
            return null;
        }

        protected SqlSourceItem ParseSourceItem()
        {
            if (IsSymbol("("))
            {
                SymbolPosition leftb = SkipSymbol("(");
                if (IsKeyword("select"))
                {
                    SqlSelect select = ParseSelect();
                    SymbolPosition rightb = SkipSymbol(")");
                    var expr = new SqlSelectExpression(leftb, select, rightb);
                    var alias = ParseAlias();
                    return new SqlSelectSourceItem(expr, alias);
                }
                else
                {
                    var lst = ParseSourceList();
                    SymbolPosition rightb = SkipSymbol(")");
                    var alias = ParseAlias();
                    return new SqlSourceListSourceItem(leftb, lst, rightb, alias);
                }
            }
            else
            {
                var expr = ParseExpression();
                var alias = ParseAlias();
                return new SqlTableSourceItem(expr, alias);
            }
        }

        private SqlSourceList ParseSourceList()
        {
            var src1 = ParseSourceItem();
            List<SqlJoin> joins = new List<SqlJoin>();
            for (; ; )
            {
                SqlJoin join;
                if (IsSymbol(","))
                {
                    var cpos = SkipSymbol(",");
                    var src = ParseSourceItem();
                    var cpre = new SqlMultiKeyWord(new SymbolPosition[] { cpos }, new string[] { "," });
                    join = new SqlJoin(cpre, src, null, null);
                }
                else
                {
                    SymbolPosition natpos = null;
                    if (IsKeyword("natural")) natpos = SkipKeyword("natural");
                    var pre = ParseMultiKeyword("left", "join")
                        ?? ParseMultiKeyword("right", "join")
                        ?? ParseMultiKeyword("left", "outer", "join")
                        ?? ParseMultiKeyword("right", "outer", "join")
                        ?? ParseMultiKeyword("inner", "join")
                        ?? ParseMultiKeyword("outer", "join")
                        ?? ParseMultiKeyword("cross", "join")
                        ?? ParseMultiKeyword("join");
                    if (pre == null)
                    {
                        if (natpos != null) GoBack(1);
                        break;
                    }
                    if (natpos != null)
                    {
                        pre.ClauseWords.Insert(0, "natural");
                        pre.ClauseWordsPos.Insert(0, natpos);
                    }
                    var src = ParseSourceItem();
                    SymbolPosition onpos = null;
                    SqlExpression cond = null;
                    if (IsKeyword("on"))
                    {
                        onpos = SkipKeyword("on");
                        cond = ParseExpression();
                    }
                    join = new SqlJoin(pre, src, onpos, cond);
                }
                joins.Add(join);
            }
            return new SqlSourceList(src1, joins);
        }

        private SqlCommandClause ParseFromClause(bool mustHaveFrom)
        {
            SqlMultiKeyWord prefix = null;
            if (mustHaveFrom)
            {
                prefix = ParseMultiKeyword("from");
                if (prefix == null) return null;
            }
            var list = ParseSourceList();
            return new SqlFromClause(prefix, list);
        }

        protected SqlResultField ParseResultField(SymbolPosition commapos)
        {
            var expr = ParseExpression();
            if (IsKeyword("as"))
            {
                var aspos = SkipKeyword("as");
                var alias = ParseIdentifier();
                return new SqlResultField(commapos != null, commapos, expr, aspos, alias);
            }
            return new SqlResultField(commapos != null, commapos, expr, null, null);
        }

        protected List<SqlResultField> ParseResultFields()
        {
            List<SqlResultField> res = new List<SqlResultField>();
            res.Add(ParseResultField(null));
            while (IsSymbol(","))
            {
                var cpos = SkipSymbol(",");
                res.Add(ParseResultField(cpos));
            }
            return res;
        }

        public SqlSelect ParseSelect()
        {
            SymbolPosition selectpos = SkipKeyword("select");
            var resflds = ParseResultFields();
            List<SqlCommandClause> clauses = new List<SqlCommandClause>();
            for (; ; )
            {
                SqlCommandClause clause = ParseSelectClause();
                if (clause == null) break;
                clauses.Add(clause);
            }
            return new SqlSelect(selectpos, resflds, clauses);
        }
    }
}
