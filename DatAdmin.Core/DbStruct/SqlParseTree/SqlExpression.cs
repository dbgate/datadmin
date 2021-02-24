using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Globalization;
using System.Collections;

namespace DatAdmin
{
    public class SqlExprRef
    {
        public string StringData;
        public double? NumData;
        public SqlSelect Select;
        public string Operator;
        public string Identifier;
        public bool IsNull;
        public SqlFunctionCall CallExpr;
        public IList ExprList;
        public SqlSpecialConstant? SpecConst;

        public bool EqualTo(SqlExprRef other, SqlEqualityTestProps props)
        {
            if (StringData != null)
            {
                return other.StringData != null && StringData == other.StringData;
            }
            if (NumData != null)
            {
                return other.NumData != null && NumData == other.NumData;
            }
            if (Select != null)
            {
                return other.Select != null && Select.EqualTo(other.Select, props);
            }
            if (Operator != null)
            {
                return other.Operator != null && Operator.ToLower() == other.Operator.ToLower();
            }
            if (Identifier != null)
            {
                return other.Identifier != null && Identifier.ToLower() == other.Identifier.ToLower();
            }
            if (IsNull) return other.IsNull;
            if (CallExpr != null)
            {
                return other.CallExpr != null && CallExpr.EqualTo(other.CallExpr, props);
            }
            if (ExprList != null)
            {
                if (other.ExprList == null) return false;
                if (other.ExprList.Count != ExprList.Count) return false;
                for (int i = 0; i < ExprList.Count; i++)
                {
                    if (!((SqlExpression)ExprList[i]).FlatEqualTo((SqlExpression)other.ExprList[i], props)) return false;
                }
                return true;
            }
            if (SpecConst != null)
            {
                return other.SpecConst != null && SpecConst == other.SpecConst;
            }
            return false;
        }
    }

    public abstract class SqlExpression : SqlNode
    {
        public abstract void GenerateSql(ISqlDumper dmp, DbTypeBase targetType);
        public override void GenerateSql(ISqlDumper dmp)
        {
            GenerateSql(dmp, null);
        }
        // GenerateSql can return NULL, if SQL cannot be generated for given dialect
        public string GenerateSql(ISqlDialect dialect, DbTypeBase targetType, IProgressInfo progress)
        {
            try
            {
                return dialect.GenerateScript(dmp => { dmp.ProgressInfo = progress; GenerateSql(dmp, targetType); });
            }
            catch (SyntaxNotSupportedError)
            {
                return null;
            }
        }

        public static SqlExpression Load(XmlElement xml)
        {
            switch (xml.GetAttribute("type"))
            {
                case "string": return new ConstStringSqlExpression(xml);
                case "number": return new ConstNumberSqlExpression(xml);
                case "null": return new NullSqlExpression(xml);
                case "specvalue": return new SpecialConstantSqlExpression(xml);
                case "specific": return new SpecificSqlExpression(xml);
            }
            throw new Exception("DAE-00248 Unknown sql expression type:" + xml.GetAttribute("type"));
        }

        public static SqlExpression ParseDefaultValue(string expr, ISqlDialect dialect)
        {
            return SqlParser.ParseDefaultValue(expr, dialect);
        }

        public abstract void GetFlatList(List<SqlExprRef> res);
        //public abstract bool EqualTo(SqlExpression other, SqlEqualityTestProps props);

        public bool FlatEqualTo(SqlExpression other, SqlEqualityTestProps props)
        {
            List<SqlExprRef> my = new List<SqlExprRef>(), ol = new List<SqlExprRef>();
            GetFlatList(my);
            other.GetFlatList(ol);
            return my.EqualSequence(ol, (a, b) => a.EqualTo(b, props));
        }

        public virtual bool XmlPersistent { get { return false; } }
    }

    public static class SqlExpressionExtension
    {
        public static string SafeGetSql(this SqlExpression expr, ISqlDialect dialect)
        {
            if (expr == null) return null;
            if (dialect == null) dialect = new GenericDialect();
            try
            {
                return dialect.GenerateScript(expr.GenerateSql);
            }
            catch
            {
                return null;
            }
        }
    }

    public abstract class SqlLeafExpression : SqlExpression
    {
        protected readonly SymbolPosition Original;
        public SqlLeafExpression(SymbolPosition original)
        {
            this.Original = original;
        }
        public SqlLeafExpression(XmlElement xml)
        {
        }
        public override void EnumSymbols(ISymbolEnumerator en)
        {
            en.EnumSymbol(Original, this);
        }
    }

    public class ConstStringSqlExpression : SqlLeafExpression
    {
        string m_value;
        public ConstStringSqlExpression(string value, SymbolPosition original)
            : base(original)
        {
            m_value = value;
        }
        public ConstStringSqlExpression(string value)
            : base((SymbolPosition)null)
        {
            m_value = value;
        }
        public ConstStringSqlExpression(XmlElement xml)
            : base(xml)
        {
            m_value = xml.GetAttribute("value");
        }

        public override bool XmlPersistent { get { return true; } }

        public override void SaveToXml(XmlElement xml)
        {
            base.SaveToXml(xml);
            xml.SetAttribute("type", "string");
            xml.SetAttribute("value", m_value);
        }

        public override void GenerateSql(ISqlDumper dmp, DbTypeBase targetType)
        {
            dmp.Put("%:v", Original, m_value);
        }

        //public override string GenerateSql(ISqlDialect dialect, DbTypeBase type, IProgressInfo progress)
        //{
        //    return dialect.QuoteValue(m_value);
        //}

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            return obj.GetType() == typeof(ConstStringSqlExpression) && ((ConstStringSqlExpression)obj).m_value == m_value;
        }
        public override int GetHashCode()
        {
            return m_value.GetHashCode();
        }
        public override void GetFlatList(List<SqlExprRef> res)
        {
            res.Add(new SqlExprRef { StringData = m_value });
        }
        //public override bool EqualTo(SqlExpression other, SqlEqualityTestProps props)
        //{
        //    var o = other as ConstStringSqlExpression;
        //    return o != null && o.m_value == m_value;
        //}
        public override string ToString()
        {
            return new GenericDialect().GetSqlLiteral(m_value);
        }
    }

    public class NullSqlExpression : SqlLeafExpression
    {
        public NullSqlExpression(SymbolPosition original)
            : base(original)
        {
        }

        public NullSqlExpression()
            : base((SymbolPosition)null)
        {
        }

        public NullSqlExpression(XmlElement xml)
            : base(xml)
        {
        }

        public override void SaveToXml(XmlElement xml)
        {
            base.SaveToXml(xml);
            xml.SetAttribute("type", "null");
        }

        public override void GenerateSql(ISqlDumper dmp, DbTypeBase targetType)
        {
            dmp.Put("^:null", Original);
        }

        public override bool XmlPersistent { get { return true; } }

        //public override string GenerateSql(ISqlDialect dialect, DbTypeBase type, IProgressInfo progress)
        //{
        //    return "NULL";
        //}

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            return obj.GetType() == typeof(NullSqlExpression);
        }
        public override int GetHashCode()
        {
            return "null".GetHashCode();
        }
        //public override bool EqualTo(SqlExpression other, SqlEqualityTestProps props)
        //{
        //    return other is NullSqlExpression;
        //}
        public override void GetFlatList(List<SqlExprRef> res)
        {
            res.Add(new SqlExprRef { IsNull = true });
        }
        public override string ToString()
        {
            return "NULL";
        }
    }

    public class ConstNumberSqlExpression : SqlLeafExpression
    {
        double m_value;
        public ConstNumberSqlExpression(double value, SymbolPosition original)
            : base(original)
        {
            m_value = value;
        }
        public ConstNumberSqlExpression(double value)
            : base((SymbolPosition)null)
        {
            m_value = value;
        }

        public ConstNumberSqlExpression(XmlElement xml)
            : base(xml)
        {
            m_value = Double.Parse(xml.GetAttribute("value"));
        }

        public override void SaveToXml(XmlElement xml)
        {
            base.SaveToXml(xml);
            xml.SetAttribute("type", "number");
            xml.SetAttribute("value", m_value.ToString());
        }

        public override bool XmlPersistent { get { return true; } }

        public override void GenerateSql(ISqlDumper dmp, DbTypeBase targetType)
        {
            if (targetType is DbTypeLogical) dmp.Put("%:s", Original, dmp.Dialect.GetSqlLiteral(m_value != 0));
            else dmp.Put("&s%:s", Original, m_value.ToString(CultureInfo.InvariantCulture));
        }

        //public override string GenerateSql(ISqlDialect dialect, DbTypeBase type, IProgressInfo progress)
        //{
        //    if (type is DbTypeLogical) return dialect.EscapeLogical(m_value != 0);
        //    return m_value.ToString(CultureInfo.InvariantCulture);
        //}

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            return obj.GetType() == typeof(ConstNumberSqlExpression) && ((ConstNumberSqlExpression)obj).m_value == m_value;
        }
        public override int GetHashCode()
        {
            return m_value.GetHashCode();
        }
        //public override bool EqualTo(SqlExpression other, SqlEqualityTestProps props)
        //{
        //    var o = other as ConstNumberSqlExpression;
        //    return o != null & o.m_value == m_value;
        //}
        public override void GetFlatList(List<SqlExprRef> res)
        {
            res.Add(new SqlExprRef { NumData = m_value });
        }
        public override string ToString()
        {
            return m_value.ToString(CultureInfo.InvariantCulture);
        }
    }

    public class SpecialConstantSqlExpression : SqlLeafExpression
    {
        SqlSpecialConstant m_specConst;

        public SpecialConstantSqlExpression(XmlElement xml)
            : base(xml)
        {
            m_specConst = (SqlSpecialConstant)Enum.Parse(typeof(SqlSpecialConstant), xml.GetAttribute("constant"), true);
        }

        public SpecialConstantSqlExpression(SqlSpecialConstant specConst, SymbolPosition original)
            : base(original)
        {
            m_specConst = specConst;
        }
        public SpecialConstantSqlExpression(SqlSpecialConstant specConst)
            : base((SymbolPosition)null)
        {
            m_specConst = specConst;
        }

        public override void SaveToXml(XmlElement xml)
        {
            base.SaveToXml(xml);
            xml.SetAttribute("type", "specvalue");
            xml.SetAttribute("constant", m_specConst.ToString().ToLower());
        }

        public override bool XmlPersistent { get { return true; } }

        public override void GenerateSql(ISqlDumper dmp, DbTypeBase targetType)
        {
            string expr = dmp.Dialect.GetSpecificExpression(m_specConst, targetType, dmp.ProgressInfo);
            if (expr != null) dmp.Put("%:s", Original, expr);
            else throw new SyntaxNotSupportedError("DAE-00249 Specific constant not supported");
        }

        //public override string GenerateSql(ISqlDialect dialect, DbTypeBase type, IProgressInfo progress)
        //{
        //    string expr = dialect.GetSpecificExpression(m_specConst, type, progress);
        //    if (expr != null) return expr;
        //    return null;
        //}

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            return obj.GetType() == typeof(SpecialConstantSqlExpression) && ((SpecialConstantSqlExpression)obj).m_specConst == m_specConst;
        }
        public override int GetHashCode()
        {
            return m_specConst.GetHashCode();
        }

        //public override bool EqualTo(SqlExpression other, SqlEqualityTestProps props)
        //{
        //    var o = other as SpecialConstantSqlExpression;
        //    return o != null & o.m_specConst == m_specConst;
        //}
        public override void GetFlatList(List<SqlExprRef> res)
        {
            res.Add(new SqlExprRef { SpecConst = m_specConst });
        }
        public override string ToString()
        {
            return "{" + m_specConst.ToString() + "}";
        }
    }

    public class SpecificSqlExpression : SqlLeafExpression
    {
        string m_expr;
        string m_dialect;

        public SpecificSqlExpression(XmlElement xml)
            : base(xml)
        {
            m_expr = xml.GetAttribute("expr");
            m_dialect = xml.GetAttribute("dialect");
        }

        public SpecificSqlExpression(string expr, string dialect, SymbolPosition original)
            : base(original)
        {
            m_expr = expr;
            m_dialect = dialect;
        }

        public override void SaveToXml(XmlElement xml)
        {
            base.SaveToXml(xml);
            xml.SetAttribute("type", "specific");
            xml.SetAttribute("expr", m_expr);
            if (m_dialect != null) xml.SetAttribute("dialect", m_dialect);
        }

        public override bool XmlPersistent { get { return true; } }

        public override void GenerateSql(ISqlDumper dmp, DbTypeBase targetType)
        {
            if (m_dialect == null)
            {
                dmp.Put("%:s", Original, m_expr);
                return;
            }
            if (dmp.Dialect.DialectName != m_dialect) throw new SyntaxNotSupportedError("DAE-00250 Specific expression has other target dialect");
            dmp.Put("%:s", Original, m_expr);
        }

        //public override string GenerateSql(ISqlDialect dialect, DbTypeBase type, IProgressInfo progress)
        //{
        //    if (m_dialect == null) return m_expr;
        //    if (dialect.DialectName != m_dialect) return null;
        //    return m_expr;
        //}

        public override bool Equals(object obj)
        {
            var o = obj as SpecificSqlExpression;
            return o != null && o.m_dialect == m_dialect && o.m_expr == m_expr;
        }
        public override int GetHashCode()
        {
            unchecked
            {
                return m_expr.GetHashCode() + m_dialect.GetHashCode();
            }
        }
        public override void GetFlatList(List<SqlExprRef> res)
        {
            res.Add(new SqlExprRef { StringData = m_expr });
        }
        public override string ToString()
        {
            return m_expr;
        }
    }

    public class SqlUnaryPrefixOperatorExpression : SqlExpression
    {
        public OperatorDef Operator;
        public SymbolPosition[] UnaryMarks;
        public SqlExpression Inner;
        public SqlUnaryPrefixOperatorExpression(OperatorDef op, SymbolPosition[] unaryMarks, SqlExpression inner)
        {
            Operator = op;
            Inner = inner;
            UnaryMarks = unaryMarks;
            if (unaryMarks.Length != op.Tokens.Length) throw new InternalError("DAE-00049 operator token count mitchmatch");
            Inner.Parent = this;
        }
        public override void GenerateSql(ISqlDumper dmp, DbTypeBase targetType)
        {
            for (int i = 0; i < UnaryMarks.Length; i++)
            {
                dmp.Put("&s%:k", UnaryMarks[i], Operator.Tokens[i]);
            }
            Inner.GenerateSql(dmp, targetType);
        }
        public override void EnumSymbols(ISymbolEnumerator en)
        {
            foreach (var mark in UnaryMarks) en.EnumSymbol(mark, this);
            Inner.EnumSymbols(en);
        }
        public override void GetFlatList(List<SqlExprRef> res)
        {
            res.Add(new SqlExprRef { Operator = Operator.Tokens.CreateDelimitedText(" ") });
            Inner.GetFlatList(res);
        }
    }

    public class SqlBinaryOperatorExpression : SqlExpression
    {
        public OperatorDef Operator;
        public SymbolPosition[] BinaryMarks;
        public SqlExpression Left, Right;
        public SqlBinaryOperatorExpression(SqlExpression left, OperatorDef op, SymbolPosition[] binaryMarks, SqlExpression right)
        {
            Operator = op;
            Left = left;
            Right = right;
            BinaryMarks = binaryMarks;
            if (binaryMarks.Length != op.Tokens.Length) throw new InternalError("DAE-00050 operator token count mitchmatch");
            Left.Parent = this;
            Right.Parent = this;
        }
        public override void GenerateSql(ISqlDumper dmp, DbTypeBase targetType)
        {
            Left.GenerateSql(dmp, targetType);
            for (int i = 0; i < BinaryMarks.Length; i++)
            {
                dmp.Put("&s%:k", BinaryMarks[i], Operator.Tokens[i]);
            }
            Right.GenerateSql(dmp, targetType);
        }
        public override void EnumSymbols(ISymbolEnumerator en)
        {
            Left.EnumSymbols(en);
            foreach (var mark in BinaryMarks) en.EnumSymbol(mark, this);
            Right.EnumSymbols(en);
        }
        public override void GetFlatList(List<SqlExprRef> res)
        {
            Left.GetFlatList(res);
            res.Add(new SqlExprRef { Operator = Operator.Tokens.CreateDelimitedText(" ") });
            Right.GetFlatList(res);
        }
    }

    public class SqlExpressionInBracket : SqlExpression
    {
        public SqlExpressionList Inner;
        public SymbolPosition LeftBracket, RightBracket;
        public SqlExpressionInBracket(SymbolPosition leftb, SqlExpressionList inner, SymbolPosition rightb)
        {
            LeftBracket = leftb;
            RightBracket = rightb;
            Inner = inner;
            Inner.Parent = this;
        }
        public override void EnumSymbols(ISymbolEnumerator en)
        {
            en.EnumSymbol(LeftBracket, this);
            Inner.EnumSymbols(en);
            en.EnumSymbol(RightBracket, this);
        }
        public override void GenerateSql(ISqlDumper dmp, DbTypeBase targetType)
        {
            dmp.Put("%:s", LeftBracket, "(");
            Inner.GenerateSql(dmp);
            dmp.Put("%:s", RightBracket, ")");
        }
        public override void GetFlatList(List<SqlExprRef> res)
        {
            if (Inner.Children.Count > 1)
            {
                res.Add(new SqlExprRef { ExprList = Inner.Children });
            }
            else
            {
                Inner.GetFlatList(res);
            }
        }
    }

    public class SqlCaseWhenClause : SqlNode
    {
        public SymbolPosition WhenPos;
        public SqlExpression WhenExpr;
        public SymbolPosition ThenPos;
        public SqlExpression ThenExpr;
        public SqlCaseWhenClause(SymbolPosition whenpos, SqlExpression whenexpr, SymbolPosition thenpos, SqlExpression thenexpr)
        {
            WhenPos = whenpos;
            WhenExpr = whenexpr;
            ThenPos = thenpos;
            ThenExpr = thenexpr;
            WhenExpr.Parent = this;
            ThenExpr.Parent = this;
        }
        public override void GenerateSql(ISqlDumper dmp)
        {
            dmp.Put("&s%:k", WhenPos, "when");
            WhenExpr.GenerateSql(dmp);
            dmp.Put("&s%:k", ThenPos, "then");
            ThenExpr.GenerateSql(dmp);
        }
        public override void EnumSymbols(ISymbolEnumerator en)
        {
            en.EnumSymbol(WhenPos, this);
            WhenExpr.EnumSymbols(en);
            en.EnumSymbol(ThenPos, null);
            ThenExpr.EnumSymbols(en);
        }

        public void GetFlatList(List<SqlExprRef> res)
        {
            res.Add(new SqlExprRef { Operator = "when" });
            WhenExpr.GetFlatList(res);
            res.Add(new SqlExprRef { Operator = "then" });
            ThenExpr.GetFlatList(res);
        }
    }

    public class SqlCaseExpression : SqlExpression
    {
        SymbolPosition CasePos;
        SqlExpression CaseVal;
        List<SqlCaseWhenClause> Whens;
        SymbolPosition ElsePos;
        SqlExpression ElseVal;
        SymbolPosition EndPos;
        public SqlCaseExpression(SymbolPosition casepos, SqlExpression caseval, IEnumerable<SqlCaseWhenClause> whens, SymbolPosition elsepos, SqlExpression elseval, SymbolPosition endpos)
        {
            CasePos = casepos;
            CaseVal = caseval;
            Whens = new List<SqlCaseWhenClause>(whens);
            ElsePos = elsepos;
            ElseVal = elseval;
            EndPos = endpos;
            foreach (var when in Whens) when.Parent = this;
            CaseVal.Parent = this;
            if (ElseVal != null) ElseVal.Parent = this;
        }
        public override void GenerateSql(ISqlDumper dmp, DbTypeBase targetType)
        {
            dmp.Put("&s%:k", CasePos, "case");
            CaseVal.GenerateSql(dmp);
            foreach (var when in Whens)
            {
                when.GenerateSql(dmp);
            }
            if (ElseVal != null)
            {
                dmp.Put("&s%:k", ElsePos, "else");
                ElseVal.GenerateSql(dmp);
            }
            dmp.Put("&s%:k", EndPos, "end");
        }
        public override void EnumSymbols(ISymbolEnumerator en)
        {
            en.EnumSymbol(CasePos, this);
            foreach (var when in Whens)
            {
                when.EnumSymbols(en);
            }
            if (ElseVal != null)
            {
                en.EnumSymbol(ElsePos, this);
                ElseVal.EnumSymbols(en);
            }
            en.EnumSymbol(EndPos, this);
        }
        public override void GetFlatList(List<SqlExprRef> res)
        {
            res.Add(new SqlExprRef { Operator = "case" });
            CaseVal.GetFlatList(res);
            foreach (var when in Whens) when.GetFlatList(res);
            if (ElseVal != null)
            {
                res.Add(new SqlExprRef { Operator = "else" });
                ElseVal.GetFlatList(res);
            }
            res.Add(new SqlExprRef { Operator = "end" });
        }
    }

    public class SqlBetweenExpression : SqlExpression
    {
        SqlExpression Value;
        SymbolPosition BetweenPos;
        SqlExpression LowerBound;
        SymbolPosition AndPos;
        SqlExpression UpperBound;
        public SqlBetweenExpression(SqlExpression value, SymbolPosition betweenpos, SqlExpression lowerbound, SymbolPosition andpos, SqlExpression upperbound)
        {
            Value = value;
            BetweenPos = betweenpos;
            LowerBound = lowerbound;
            AndPos = andpos;
            UpperBound = upperbound;
            Value.Parent = this;
            LowerBound.Parent = this;
            UpperBound.Parent = this;
        }
        public override void GenerateSql(ISqlDumper dmp, DbTypeBase targetType)
        {
            Value.GenerateSql(dmp);
            dmp.Put("&s%:k", BetweenPos, "between");
            LowerBound.GenerateSql(dmp);
            dmp.Put("&s%:k", AndPos, "and");
            UpperBound.GenerateSql(dmp);
        }
        public override void EnumSymbols(ISymbolEnumerator en)
        {
            Value.EnumSymbols(en);
            en.EnumSymbol(BetweenPos, this);
            LowerBound.EnumSymbols(en);
            en.EnumSymbol(AndPos, this);
            UpperBound.EnumSymbols(en);
        }
        public override void GetFlatList(List<SqlExprRef> res)
        {
            Value.GetFlatList(res);
            res.Add(new SqlExprRef { Operator = "between" });
            LowerBound.GetFlatList(res);
            res.Add(new SqlExprRef { Operator = "bet_and" });
            UpperBound.GetFlatList(res);
        }
    }


    public abstract class SqlNameExpression : SqlExpression
    {
        public abstract string NameWithoutQualifiers { get; }

        public virtual bool EqualTo(SqlNameExpression other, SqlEqualityTestProps props)
        {
            return NameWithoutQualifiers == other.NameWithoutQualifiers;
        }
    }

    public abstract class SqlIdentifierOrExprSymbol : SqlNameExpression
    {
        public SymbolPosition Original;
    }

    public class SqlIdentifier : SqlIdentifierOrExprSymbol
    {
        public string Identifier;
        public bool IsQuoted;
        public SqlIdentifier(bool isQuoted, string ident, SymbolPosition original)
        {
            Identifier = ident;
            Original = original;
            IsQuoted = isQuoted;
        }
        public override void GenerateSql(ISqlDumper dmp, DbTypeBase targetType)
        {
            if (dmp.Dialect.PossibleKeywords.Contains(Identifier.ToUpper()))
            {
                // we cannot change quoting
                if (IsQuoted) dmp.Put("&s%:s", Original, dmp.Dialect.QuoteIdentBegin + Identifier + dmp.Dialect.QuoteIdentEnd);
                else dmp.Put("&s%:s", Original, Identifier);
            }
            else
            {
                dmp.Put("&s%:i", Original, Identifier);
            }
        }
        public override void EnumSymbols(ISymbolEnumerator en)
        {
            en.EnumSymbol(Original, this);
        }
        public override string NameWithoutQualifiers
        {
            get { return Identifier; }
        }
        public override void GetFlatList(List<SqlExprRef> res)
        {
            res.Add(new SqlExprRef { Identifier = Identifier });
        }
    }

    public class SqlStarSymbol : SqlIdentifierOrExprSymbol
    {
        public SqlStarSymbol(SymbolPosition starpos)
        {
            Original = starpos;
        }
        public override void GenerateSql(ISqlDumper dmp, DbTypeBase targetType)
        {
            dmp.Put("&s%:s", Original, "*");
        }
        public override void EnumSymbols(ISymbolEnumerator en)
        {
            en.EnumSymbol(Original, this);
        }
        public override string NameWithoutQualifiers
        {
            get { return "*"; }
        }
        public override void GetFlatList(List<SqlExprRef> res)
        {
            res.Add(new SqlExprRef { Identifier = "*" });
        }
    }

    public class SqlQualificator : SqlNode
    {
        SqlIdentifierOrExprSymbol Ident;
        SymbolPosition DotPos;

        public SqlQualificator(SqlIdentifierOrExprSymbol ident, SymbolPosition dotpos)
        {
            Ident = ident;
            DotPos = dotpos;
            if (Ident != null) Ident.Parent = this;
        }
        public override void GenerateSql(ISqlDumper dmp)
        {
            if (Ident != null) Ident.GenerateSql(dmp);
            dmp.Put("%:s", DotPos, ".");
        }
        public override void EnumSymbols(ISymbolEnumerator en)
        {
            if (Ident != null) Ident.EnumSymbols(en);
            en.EnumSymbol(DotPos, this);
        }
    }

    public class SqlQualifiedName : SqlNameExpression
    {
        List<SqlQualificator> Qualificators;
        SqlIdentifierOrExprSymbol Name;
        public SqlQualifiedName(IEnumerable<SqlQualificator> qualificators, SqlIdentifierOrExprSymbol name)
        {
            Qualificators = new List<SqlQualificator>(qualificators);
            Name = name;
            foreach (var qal in Qualificators) qal.Parent = this;
            Name.Parent = this;
        }
        public override void GenerateSql(ISqlDumper dmp, DbTypeBase targetType)
        {
            foreach (var qal in Qualificators) qal.GenerateSql(dmp);
            Name.GenerateSql(dmp);
        }
        public override void EnumSymbols(ISymbolEnumerator en)
        {
            foreach (var qal in Qualificators) qal.EnumSymbols(en);
            Name.EnumSymbols(en);
        }
        public override string NameWithoutQualifiers
        {
            get { return Name.NameWithoutQualifiers; }
        }
        public override void GetFlatList(List<SqlExprRef> res)
        {
            res.Add(new SqlExprRef { Identifier = NameWithoutQualifiers });
        }
    }

    public class SqlExprListItem : SqlNode
    {
        SymbolPosition CommaPos;
        public SqlExpression Argument;
        bool IsFirst;
        public SqlExprListItem(bool isfirst, SymbolPosition commapos, SqlExpression argument)
        {
            CommaPos = commapos;
            Argument = argument;
            IsFirst = isfirst;
            Argument.Parent = this;
        }
        public override void GenerateSql(ISqlDumper dmp)
        {
            if (!IsFirst) dmp.Put("%:s", CommaPos, ",");
            Argument.GenerateSql(dmp);
        }
        public override void EnumSymbols(ISymbolEnumerator en)
        {
            if (!IsFirst) en.EnumSymbol(CommaPos, this);
            Argument.EnumSymbols(en);
        }
    }

    public class SqlExpressionList : SqlNode
    {
        public List<SqlExprListItem> Items;
        public SqlExpressionList(IEnumerable<SqlExprListItem> items)
        {
            Items = new List<SqlExprListItem>(items);
            foreach (var item in Items) item.Parent = this;
        }
        public override void GenerateSql(ISqlDumper dmp)
        {
            foreach (var item in Items) item.GenerateSql(dmp);
        }
        public override void EnumSymbols(ISymbolEnumerator en)
        {
            foreach (var item in Items) item.EnumSymbols(en);
        }

        public void GetFlatList(List<SqlExprRef> res)
        {
            foreach (var item in Items) item.Argument.GetFlatList(res);
        }
    }

    public class SqlFunctionCall : SqlExpression
    {
        SqlNameExpression FunctionName;
        SymbolPosition LeftBracket;
        SqlExpressionList Arguments;
        SymbolPosition RightBracket;
        public SqlFunctionCall(SqlNameExpression name, SymbolPosition leftb, SqlExpressionList args, SymbolPosition rignhtb)
        {
            FunctionName = name;
            LeftBracket = leftb;
            Arguments = args;
            RightBracket = rignhtb;
            FunctionName.Parent = this;
            Arguments.Parent = this;
        }
        public override void GenerateSql(ISqlDumper dmp, DbTypeBase targetType)
        {
            FunctionName.GenerateSql(dmp);
            dmp.Put("%:s", LeftBracket, "(");
            Arguments.GenerateSql(dmp);
            dmp.Put("%:s", RightBracket, ")");
        }
        public override void EnumSymbols(ISymbolEnumerator en)
        {
            FunctionName.EnumSymbols(en);
            en.EnumSymbol(LeftBracket, this);
            Arguments.EnumSymbols(en);
            en.EnumSymbol(RightBracket, this);
        }
        public override void GetFlatList(List<SqlExprRef> res)
        {
            res.Add(new SqlExprRef { CallExpr = this });
        }

        public bool EqualTo(SqlFunctionCall other, SqlEqualityTestProps props)
        {
            if (!FunctionName.EqualTo(other.FunctionName, props)) return false;
            if (!Arguments.Items.EqualSequence(other.Arguments.Items, (a, b) => a.Argument.FlatEqualTo(b.Argument, props))) return false;
            return true;
        }
    }

    public class SqlQuotedStringExpression : SqlLeafExpression
    {
        public string Data;
        public TokenType Token;

        public SqlQuotedStringExpression(SymbolPosition original, string data, TokenType token)
            : base(original)
        {
            Data = data;
            Token = token;
        }

        public override void GenerateSql(ISqlDumper dmp, DbTypeBase targetType)
        {
            switch (Token)
            {
                case TokenType.StringSingle:
                    dmp.Put("%:v", Original, Data);
                    break;
                case TokenType.QuotedIdent:
                    var old = dmp.FormatProperties.IdentifierQuoteMode;
                    dmp.FormatProperties.IdentifierQuoteMode = SqlIdentifierQuoteMode.Quoted;
                    dmp.Put("%:v", Original, Data);
                    dmp.FormatProperties.IdentifierQuoteMode = old;
                    break;
                case TokenType.StringDouble:
                    dmp.Put("%:s", Original, '"' + Data + '"');
                    break;
            }
        }

        public override void GetFlatList(List<SqlExprRef> res)
        {
            res.Add(new SqlExprRef { StringData = Data });
        }
    }

    public class SqlSelectExpression : SqlExpression
    {
        public SymbolPosition LeftBracket;
        public SqlSelect Select;
        public SymbolPosition RightBracket;

        public SqlSelectExpression(SymbolPosition leftb, SqlSelect select, SymbolPosition rightb)
        {
            LeftBracket = leftb;
            Select = select;
            RightBracket = rightb;
        }

        public override void GenerateSql(ISqlDumper dmp, DbTypeBase targetType)
        {
            dmp.Put("%:s", LeftBracket, "(");
            Select.GenerateSql(dmp);
            dmp.Put("%:s", RightBracket, "(");
        }

        public override void EnumSymbols(ISymbolEnumerator en)
        {
            en.EnumSymbol(LeftBracket, this);
            Select.EnumSymbols(en);
            en.EnumSymbol(RightBracket, this);
        }

        public override void GetFlatList(List<SqlExprRef> res)
        {
            res.Add(new SqlExprRef { Select = Select });
        }
    }
}
