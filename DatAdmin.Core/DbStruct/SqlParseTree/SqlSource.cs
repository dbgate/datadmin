using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    public class SqlJoinRef
    {
        public string JoinType;
        public SqlExpression Source;
        public SqlExpression Condition;

        public bool EqualTo(SqlJoinRef other, SqlEqualityTestProps props)
        {
            if (JoinType != null)
            {
                return other.JoinType != null && EqualJoinTypes(JoinType, other.JoinType, props);
            }
            if (Source != null)
            {
                return other.Source != null && Source.FlatEqualTo(other.Source, props);
            }
            if (Condition != null)
            {
                return other.Condition != null && Condition.FlatEqualTo(other.Condition, props);
            }
            return false;
        }

        public static bool EqualJoinTypes(string j1, string j2, SqlEqualityTestProps props)
        {
            j1 = j1.ToLower();
            j2 = j2.ToLower();
            if (TryEq(j1, j2, ",", "cross join")) return true;
            if (TryEq(j1, j2, "join", "inner join")) return true;
            if (TryEq(j1, j2, "left join", "left outer join")) return true;
            if (TryEq(j1, j2, "right join", "right outer join")) return true;
            return j1 == j2;
        }

        private static bool TryEq(string j1, string j2, string t1, string t2)
        {
            return t1 == j1 && t2 == j2 || t1 == j2 && t2 == j1;
        }
    }

    public abstract class SqlSourceItem : SqlNode
    {
        public abstract void GetFlatList(List<SqlJoinRef> res);
    }

    public abstract class SqlDataSourceItem : SqlSourceItem
    {
        public SqlExpression SourceData;
        public SqlIdentifier Shortcut;

        public SqlDataSourceItem(SqlExpression sourceData, SqlIdentifier shortcut)
        {
            SourceData = sourceData;
            Shortcut = shortcut;
        }

        public override void EnumSymbols(ISymbolEnumerator en)
        {
            SourceData.EnumSymbols(en);
            if (Shortcut != null) Shortcut.EnumSymbols(en);
        }

        public override void GenerateSql(ISqlDumper dmp)
        {
            SourceData.GenerateSql(dmp);
            if (Shortcut != null)
            {
                dmp.Put("&s");
                Shortcut.GenerateSql(dmp);
            }
        }

        public override void GetFlatList(List<SqlJoinRef> res)
        {
            res.Add(new SqlJoinRef { Source = SourceData });
        }
    }

    public class SqlTableSourceItem : SqlDataSourceItem
    {
        public SqlTableSourceItem(SqlExpression sourceData, SqlIdentifier shortcut)
            : base(sourceData, shortcut)
        {
        }
    }

    public class SqlSelectSourceItem : SqlDataSourceItem
    {
        public SqlSelectSourceItem(SqlSelectExpression sourceData, SqlIdentifier shortcut)
            : base(sourceData, shortcut)
        {
        }
    }

    public class SqlSourceListSourceItem : SqlSourceItem
    {
        public SymbolPosition LeftBracket;
        public SqlSourceList Inner;
        public SymbolPosition RightBracket;
        public SqlIdentifier Shortcut;

        public SqlSourceListSourceItem(SymbolPosition leftb, SqlSourceList inner, SymbolPosition rightb, SqlIdentifier shortcut)
        {
            LeftBracket = leftb;
            Inner = inner;
            RightBracket = rightb;
            Shortcut = shortcut;
        }

        public override void GenerateSql(ISqlDumper dmp)
        {
            dmp.Put("%:s", LeftBracket, "(");
            Inner.GenerateSql(dmp);
            dmp.Put("%:s", RightBracket, ")");
            if (Shortcut != null)
            {
                dmp.Put("&s");
                Shortcut.GenerateSql(dmp);
            }
        }

        public override void EnumSymbols(ISymbolEnumerator en)
        {
            en.EnumSymbol(LeftBracket, this);
            Inner.EnumSymbols(en);
            en.EnumSymbol(RightBracket, this);
            if (Shortcut != null) Shortcut.EnumSymbols(en);
        }

        public override void GetFlatList(List<SqlJoinRef> res)
        {
            Inner.GetFlatList(res);
        }
    }

    public class SqlJoin : SqlNode
    {
        public SqlMultiKeyWord JoinPrefix;
        public SqlSourceItem Source;
        public SymbolPosition OnPos;
        public SqlExpression Condition;

        public SqlJoin(SqlMultiKeyWord joinPrefix, SqlSourceItem source, SymbolPosition onpos, SqlExpression cond)
        {
            JoinPrefix = joinPrefix;
            Source = source;
            OnPos = onpos;
            Condition = cond;
            JoinPrefix.Parent = this;
            Source.Parent = this;
            if (Condition != null) Condition.Parent = this;
        }

        public override void GenerateSql(ISqlDumper dmp)
        {
            JoinPrefix.GenerateSql(dmp);
            Source.GenerateSql(dmp);
            if (Condition != null)
            {
                dmp.Put("&s%:k", OnPos, "on");
                Condition.GenerateSql(dmp);
            }
        }

        public override void EnumSymbols(ISymbolEnumerator en)
        {
            JoinPrefix.EnumSymbols(en);
            Source.EnumSymbols(en);
            if (Condition != null)
            {
                en.EnumSymbol(OnPos, this);
                Condition.EnumSymbols(en);
            }
        }

        public void GetFlatList(List<SqlJoinRef> res)
        {
            res.Add(new SqlJoinRef { JoinType = JoinPrefix.ClauseWords.CreateDelimitedText(" ") });
            Source.GetFlatList(res);
            if (Condition != null) res.Add(new SqlJoinRef { Condition = Condition });
        }
    }

    public class SqlSourceList : SqlNode
    {
        public SqlSourceItem Source;
        public List<SqlJoin> Joins;

        public SqlSourceList(SqlSourceItem source, IEnumerable<SqlJoin> joins)
        {
            Source = source;
            if (joins != null) Joins = new List<SqlJoin>(joins);
            else Joins = new List<SqlJoin>();
            Source.Parent = this;
            foreach (var j in joins) j.Parent = this;
        }

        public override void GenerateSql(ISqlDumper dmp)
        {
            Source.GenerateSql(dmp);
            dmp.Put("&3n");
            foreach (var j in Joins)
            {
                j.GenerateSql(dmp);
                dmp.Put("&3n");
            }
        }

        public override void EnumSymbols(ISymbolEnumerator en)
        {
            Source.EnumSymbols(en);
            foreach (var j in Joins) j.EnumSymbols(en);
        }

        public void GetFlatList(List<SqlJoinRef> res)
        {
            Source.GetFlatList(res);
            foreach (var join in Joins) join.GetFlatList(res);
        }
    }
}
