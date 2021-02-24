using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DatAdmin
{
    public abstract class SqlCommand : SqlNode
    {
    }


    public class SqlResultField : SqlNode
    {
        public bool IsComma;
        public SymbolPosition CommaPos;
        public SqlExpression Value;
        public SymbolPosition AsPos;
        public SqlIdentifier Alias;

        public SqlResultField(bool isComma, SymbolPosition commaPos, SqlExpression value, SymbolPosition aspos, SqlIdentifier alias)
        {
            IsComma = isComma;
            CommaPos = commaPos;
            Value = value;
            AsPos = aspos;
            Alias = alias;
            Value.Parent = this;
            if (Alias != null) Alias.Parent = this;
        }

        public override void GenerateSql(ISqlDumper dmp)
        {
            if (IsComma) dmp.Put("%:s", CommaPos, ",");
            dmp.Put("&3n");
            Value.GenerateSql(dmp);
            if (Alias != null)
            {
                dmp.Put("&s%:k", AsPos, "as");
                Alias.GenerateSql(dmp);
            }
        }

        public override void EnumSymbols(ISymbolEnumerator en)
        {
            if (IsComma) en.EnumSymbol(CommaPos, this);
            Value.EnumSymbols(en);
            if (Alias != null)
            {
                en.EnumSymbol(AsPos, this);
                Alias.EnumSymbols(en);
            }
        }

        public bool EqualTo(SqlResultField other, SqlEqualityTestProps props)
        {
            return Value.FlatEqualTo(other.Value, props);
        }
    }

    public class SqlColRefListItem : SqlNode
    {
        SymbolPosition CommaPos;
        public SqlExpression Argument;
        public SqlMultiKeyWord Modifier;
        bool IsFirst;

        public SqlColRefListItem(bool isfirst, SymbolPosition commapos, SqlExpression argument, SqlMultiKeyWord modifier)
        {
            CommaPos = commapos;
            Argument = argument;
            IsFirst = isfirst;
            Modifier = modifier;
            Argument.Parent = this;
            if (Modifier != null) Modifier.Parent = this;
        }
        public override void GenerateSql(ISqlDumper dmp)
        {
            if (!IsFirst) dmp.Put("%:s", CommaPos, ",");
            Argument.GenerateSql(dmp);
            if (Modifier != null) Modifier.GenerateSql(dmp);
        }
        public override void EnumSymbols(ISymbolEnumerator en)
        {
            if (!IsFirst) en.EnumSymbol(CommaPos, this);
            Argument.EnumSymbols(en);
            Modifier.EnumSymbols(en);
        }
    }

    public class SqlColRefList : SqlNode
    {
        public List<SqlColRefListItem> Items;
        public SqlColRefList(IEnumerable<SqlColRefListItem> items)
        {
            Items = new List<SqlColRefListItem>(items);
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

        public bool EqualTo(SqlColRefList other, SqlEqualityTestProps props)
        {
            return Items.EqualSequence(other.Items, (a, b) => a.Argument.FlatEqualTo(b.Argument, props) && SqlMultiKeyWord.Equals(a.Modifier,b.Modifier,props));
            throw new NotImplementedError("DAE-00116");
        }
    }

    public class SqlMultiKeyWord : SqlNode
    {
        public List<SymbolPosition> ClauseWordsPos;
        public List<string> ClauseWords;

        public SqlMultiKeyWord(IEnumerable<SymbolPosition> clauseWordsPos, IEnumerable<string> clauseWords)
        {
            ClauseWordsPos = new List<SymbolPosition>(clauseWordsPos);
            ClauseWords = new List<string>(clauseWords);
        }

        public override void GenerateSql(ISqlDumper dmp)
        {
            dmp.Put("&s%:K", ClauseWordsPos, ClauseWords);
        }

        public override void EnumSymbols(ISymbolEnumerator en)
        {
            if (ClauseWordsPos != null)
            {
                foreach (var sym in ClauseWordsPos) en.EnumSymbol(sym, this);
            }
        }

        public static bool Equals(SqlMultiKeyWord a, SqlMultiKeyWord b, SqlEqualityTestProps props)
        {
            if (a == null) return b == null;
            if (b == null) return a == null;
            return a.ClauseWords.CreateDelimitedText(" ").ToLower() == b.ClauseWords.CreateDelimitedText(" ").ToLower();
        }
    }

    public abstract class SqlCommandClause : SqlNode
    {
        public SqlMultiKeyWord Prefix;

        public SqlCommandClause(SqlMultiKeyWord prefix)
        {
            Prefix = prefix;
            if (Prefix != null) Prefix.Parent = this;
        }
        public override void GenerateSql(ISqlDumper dmp)
        {
            if (Prefix != null) Prefix.GenerateSql(dmp);
        }
        public override void EnumSymbols(ISymbolEnumerator en)
        {
            if (Prefix != null) Prefix.EnumSymbols(en);
        }

        public abstract bool EqualTo(SqlCommandClause clause, SqlEqualityTestProps props);
    }

    public class SqlFromClause : SqlCommandClause
    {
        SqlSourceList Source;

        public SqlFromClause(SqlMultiKeyWord prefix, SqlSourceList source)
            : base(prefix)
        {
            Source = source;
            Source.Parent = this;
        }
        public override void GenerateSql(ISqlDumper dmp)
        {
            base.GenerateSql(dmp);
            dmp.Put("&3>&3n");
            Source.GenerateSql(dmp);
            dmp.Put("&3<");
        }
        public override void EnumSymbols(ISymbolEnumerator en)
        {
            base.EnumSymbols(en);
            Source.EnumSymbols(en);
        }
        public override bool EqualTo(SqlCommandClause clause, SqlEqualityTestProps props)
        {
            var c = clause as SqlFromClause;
            if (c == null) return false;
            List<SqlJoinRef> myl = new List<SqlJoinRef>(), otherl = new List<SqlJoinRef>();
            Source.GetFlatList(myl);
            c.Source.GetFlatList(otherl);
            return myl.EqualSequence(otherl, (a, b) => a.EqualTo(b, props));
        }
    }

    // order by, group by
    public abstract class SqlColumnListClause : SqlCommandClause
    {
        public SqlColRefList Items;
        public SqlColumnListClause(SqlMultiKeyWord prefix, SqlColRefList items)
            : base(prefix)
        {
            Items = items;
            Items.Parent = this;
        }
        public override void GenerateSql(ISqlDumper dmp)
        {
            base.GenerateSql(dmp);
            Items.GenerateSql(dmp);
        }
        public override void EnumSymbols(ISymbolEnumerator en)
        {
            base.EnumSymbols(en);
            Items.EnumSymbols(en);
        }
        public override bool EqualTo(SqlCommandClause clause, SqlEqualityTestProps props)
        {
            var o = clause as SqlColumnListClause;
            if (o == null) return false;
            if (o.GetType() != GetType()) return false;
            return Items.EqualTo(o.Items, props);
        }
    }

    public class SqlOrderByClause : SqlColumnListClause
    {
        public SqlOrderByClause(SqlMultiKeyWord prefix, SqlColRefList items)
            : base(prefix, items)
        {
        }
    }

    public class SqlGroupByClause : SqlColumnListClause
    {
        public SqlGroupByClause(SqlMultiKeyWord prefix, SqlColRefList items)
            : base(prefix, items)
        {
        }
    }

    // where, having
    public class SqlConditionClause : SqlCommandClause
    {
        public SqlExpression Condition;

        public SqlConditionClause(SqlMultiKeyWord prefix, SqlExpression cond)
            :base(prefix)
        {
            Condition = cond;
            Condition.Parent = this;
        }
        public override void GenerateSql(ISqlDumper dmp)
        {
            base.GenerateSql(dmp);
            Condition.GenerateSql(dmp);
        }
        public override void EnumSymbols(ISymbolEnumerator en)
        {
            base.EnumSymbols(en);
            Condition.EnumSymbols(en);
        }
        public override bool EqualTo(SqlCommandClause clause, SqlEqualityTestProps props)
        {
            var o = clause as SqlConditionClause;
            if (o == null) return false;
            if (o.GetType() != GetType()) return false;
            return Condition.FlatEqualTo(o.Condition, props);
        }
    }

    public class SqlWhereClause : SqlConditionClause
    {
        public SqlWhereClause(SqlMultiKeyWord prefix, SqlExpression cond)
            : base(prefix, cond)
        {
        }
    }

    public class SqlHavingClause : SqlConditionClause
    {
        public SqlHavingClause(SqlMultiKeyWord prefix, SqlExpression cond)
            : base(prefix, cond)
        {
        }
    }

    public class SqlSelect : SqlCommand
    {
        // select clause
        public SymbolPosition SelectPos;
        public List<SqlResultField> ResultFields;

        // other optional clauses
        public List<SqlCommandClause> Clauses = new List<SqlCommandClause>();

        public SqlSelect(SymbolPosition selectpos, IEnumerable<SqlResultField> resflds, IEnumerable<SqlCommandClause> clauses)
        {
            SelectPos = selectpos;
            ResultFields = new List<SqlResultField>(resflds);
            Clauses = new List<SqlCommandClause>(clauses);
            foreach (var clause in Clauses) clause.Parent = this;
            foreach (var resfld in ResultFields) resfld.Parent = this;
        }

        public override void GenerateSql(ISqlDumper dmp)
        {
            dmp.Put("&s%:k", SelectPos, "select");
            dmp.Put("&3>");
            dmp.Put("&3n");
            foreach (var res in ResultFields)
            {
                res.GenerateSql(dmp);
            }
            dmp.Put("&3n");
            dmp.Put("&3<");
            foreach (var clause in Clauses)
            {
                dmp.Put("&5n");
                clause.GenerateSql(dmp);
            }
        }

        public override void EnumSymbols(ISymbolEnumerator en)
        {
            en.EnumSymbol(SelectPos, this);
            foreach (var res in ResultFields) res.EnumSymbols(en);
            foreach (var clause in Clauses) clause.EnumSymbols(en);
        }

        public bool EqualTo(SqlSelect other, SqlEqualityTestProps props)
        {
            if (!ResultFields.EqualSequence(other.ResultFields, (a, b) => a.EqualTo(b, props))) return false;
            if (!Clauses.EqualSequence(other.Clauses, (a, b) => a.EqualTo(b, props))) return false;
            return true;
        }
    }

    public abstract class SqlCreateCommand : SqlCommand
    {
    }

    //public abstract class SqlCreateOption : SqlNode
    //{
    //}

    public abstract class SqlCreateView : SqlCreateCommand
    {
        public abstract SqlSelect SelectStatement { get; }
        //public SqlClausePrefix CommandPos;
        //public SqlNameExpression ViewName;
        //public SqlExpressionInBracket ColumnList;
        //public List<SqlCreateOption> OptionsBeforeSelect;
        //public SqlSelect Select;
        //public List<SqlCreateOption> OptionsAfterSelect;
        //public SqlCreateView(SqlClausePrefix commandpos, SqlNameExpression viewname,
        //    SqlExpressionInBracket columns, IEnumerable<SqlCreateOption> optsbeforeselect,
        //    SqlSelect select, IEnumerable<SqlCreateOption> optsafterselect)
        //{
        //    CommandPos = commandpos;
        //    ViewName = viewname;
        //    ColumnList = columns;
        //    OptionsBeforeSelect = new List<SqlCreateOption>(optsbeforeselect);
        //    Select = select;
        //    OptionsAfterSelect = new List<SqlCreateOption>(optsafterselect);
        //}

        //public override void GenerateSql(ISqlDumper dmp)
        //{
        //    CommandPos.GenerateSql(dmp);
        //    ViewName.GenerateSql(dmp);
        //    if (ColumnList != null) ColumnList.GenerateSql(dmp);
        //    foreach (var opt in OptionsBeforeSelect) opt.GenerateSql(dmp);
        //    Select.GenerateSql(dmp);
        //    foreach (var opt in OptionsAfterSelect) opt.GenerateSql(dmp);
        //}

        //public override void EnumSymbols(ISymbolEnumerator en)
        //{
        //    CommandPos.EnumSymbols(en);
        //    ViewName.EnumSymbols(en);
        //    if (ColumnList != null) ColumnList.EnumSymbols(en);
        //    foreach (var opt in OptionsBeforeSelect) opt.EnumSymbols(en);
        //    Select.EnumSymbols(en);
        //    foreach (var opt in OptionsAfterSelect) opt.EnumSymbols(en);
        //}

    }

    public abstract class SqlCreateTable : SqlCreateCommand
    {
        public SqlMultiKeyWord CreateTablePos;
        public List<SqlCommandClause> BeforeName;
        public SqlNameExpression Name;
        public SymbolPosition LeftBracket;
        public List<SqlCreateDefinition> CreateDefs;
        public SymbolPosition RightBracket;
    }

    public abstract class SqlCreateDefinition : SqlNode
    {
        public SymbolPosition CommaPos;
    }

    public abstract class SqlCreateColumnDefinition : SqlCreateDefinition
    {
        public SqlIdentifier ColumnName;
        public SqlColumnDefinition ColDef;
    }

    public abstract class SqlOptionSet : SqlNode
    {
    }

    public abstract class SqlCreateConstraintDefinition : SqlCreateDefinition
    {
        public SymbolPosition ConstraintPos;
        public SqlNameExpression ConstraintName;
        public SqlMultiKeyWord ConstraintType;
        public SqlMultiKeyWord KeySpec;
        public SymbolPosition LeftBracket;
        public SqlColRefList ColRefs;
        public SqlExpression CheckExpr;
        public SymbolPosition RightBracket;
        public SqlOptionSet Options;

    }

    public abstract class SqlColumnDefinition : SqlNode
    {
        public SqlDataTypeDef DataType;
        public SqlMultiKeyWord NullableSpec;
        public SqlDefaultValueSpec DefValSpec;

    }

    public abstract class SqlDefaultValueSpec : SqlNode
    {
        public SymbolPosition DefaultPos;
        public SqlExpression DefaultValue;
    }

    public abstract class SqlDataTypeDef : SqlNode
    {
        public SqlMultiKeyWord DataType;
        public SqlExpressionInBracket Args;
        public SqlMultiKeyWord DataTypeFlags; // eg. UNSIGNED ZEROFILL COLLATE xxx
    }

    //public class SqlRenameTable : SqlCommand
    //{
    //    string m_oldName, m_newName;
    //    public SqlRenameTable(string oldName, string newName)
    //    {
    //        m_oldName = oldName;
    //        m_newName = newName;
    //    }
    //    public override void GenerateSql(ISqlDialect dialect, TextWriter tw)
    //    {
    //        tw.Write(dialect.GenerateRenameTable(
    //    }
    //}

    //public class SqlInsertSelect : SqlCommand
    //{
    //    NameWithSchema m_dsttable;
    //    IColumnCollection m_dstcolumns;
    //    ISqlSelect m_srcselect;

    //    public SqlInsertSelect(NameWithSchema dsttable, IColumnCollection dstcolumns, ISqlSelect srcselect)
    //    {
    //        m_dsttable = dsttable;
    //        m_dstcolumns = dstcolumns;
    //        m_srcselect = srcselect;
    //    }

    //    public override void GenerateSql(ISqlDialect dialect, ISqlOutputStream tw)
    //    {
    //        tw.Write(String.Format("INSERT INTO {0} ", DialectExtension.QuoteFullName(dialect, m_dsttable)));
    //        tw.Write("\n    (");
    //        bool was = false;
    //        foreach (IColumnStructure col in m_dstcolumns)
    //        {
    //            if (was) tw.Write(",");
    //            tw.Write(dialect.QuoteIdentifier(col.ColumnName));
    //            was = true;
    //        }
    //        tw.Write(")\n");
    //        m_srcselect.GenerateSql_NoEnd(dialect, tw);
    //        tw.EndCommand();
    //    }
    //}

    //public class SqlDropConstraint : SqlCommand
    //{
    //    IConstraint m_constraint;
    //    public SqlDropConstraint(IConstraint constraint)
    //    {
    //        m_constraint = constraint;
    //    }
    //    public override void GenerateSql(ISqlDialect dialect, ISqlOutputStream tw)
    //    {
    //        SqlGenTool.GenerateDropConstraint(m_constraint, dialect, tw);
    //    }
    //}

    //public class SqlCreateConstraint : SqlCommand
    //{
    //    IConstraint m_constraint;
    //    public SqlCreateConstraint(IConstraint constraint)
    //    {
    //        m_constraint = constraint;
    //    }
    //    public override void GenerateSql(ISqlDialect dialect, ISqlOutputStream tw)
    //    {
    //        SqlGenTool.GenerateCreateConstraint(m_constraint, dialect, tw);
    //    }
    //}

    //public class SqlCreateTable : SqlCommand
    //{
    //    ITableStructure m_table;

    //    public SqlCreateTable(ITableStructure table)
    //    {
    //        m_table = table;
    //    }

    //    public override void GenerateSql(ISqlDialect dialect, ISqlOutputStream tw)
    //    {
    //        tw.Write("CREATE TABLE " + dialect.QuoteFullName(m_table.FullName));
    //        tw.Write(" (\n");
    //        bool first = true;
    //        foreach (IColumnStructure col in m_table.Columns)
    //        {
    //            if (!first) tw.Write(", \n");
    //            first = false;
    //            tw.Write("    ");
    //            tw.Write(dialect.QuoteIdentifier(col.ColumnName));
    //            tw.Write(" ");
    //            tw.Write(dialect.GenerateColumnDefinition(col, true));
    //        }
    //        foreach (IConstraint cnt in m_table.Constraints)
    //        {
    //            if (cnt is IPrimaryKey)
    //            {
    //                if (!first) tw.Write(", \n");
    //                first = false;
    //                tw.Write("    ");
    //                GeneratePrimaryKey(dialect, (IPrimaryKey)cnt, tw);
    //            }
    //        }
    //        tw.Write("\n)");
    //        tw.EndCommand();
    //        foreach (IConstraint cnt in m_table.Constraints)
    //        {
    //            if (!(cnt is IPrimaryKey))
    //            {
    //                SqlGenTool.GenerateCreateConstraint(cnt, dialect, tw);
    //            }
    //        }
    //    }

    //    public static void GeneratePrimaryKey(ISqlDialect dialect, IPrimaryKey cnt, ISqlOutputStream tw)
    //    {
    //        if (cnt.Name != null && !dialect.AnonymousPrimaryKey)
    //        {
    //            tw.Write("CONSTRAINT ");
    //            tw.Write(dialect.QuoteIdentifier(cnt.Name));
    //            tw.Write(" ");
    //        }
    //        if (cnt is IPrimaryKey)
    //        {
    //            tw.Write("PRIMARY KEY ");
    //            GenerateConstraintColumns(dialect, ((IColumnsConstraint)cnt).Columns, tw);
    //        }
    //    }

    //    private static void GenerateConstraintColumns(ISqlDialect dialect, IEnumerable<string> columns, ISqlOutputStream tw)
    //    {
    //        bool first = true;
    //        tw.Write("(");
    //        foreach (string col in columns)
    //        {
    //            if (!first) tw.Write(", ");
    //            first = false;
    //            tw.Write(dialect.QuoteIdentifier(col));
    //        }
    //        tw.Write(")");
    //    }
    //}

    //public class SqlDropTable : SqlCommand
    //{
    //    NameWithSchema m_tblname;
    //    public SqlDropTable(NameWithSchema tblname) { m_tblname = tblname; }
    //    public override void GenerateSql(ISqlDialect dialect, ISqlOutputStream tw)
    //    {
    //        tw.Write("DROP TABLE " + DialectExtension.QuoteFullName(dialect, m_tblname) + "");
    //        tw.EndCommand();
    //    }
    //}

    //public class SqlPolyCommand : SqlCommand
    //{
    //    public readonly List<SqlCommand> Commands = new List<SqlCommand>();

    //    public SqlPolyCommand()
    //    {
    //    }

    //    public override void GenerateSql(ISqlDialect dialect, ISqlOutputStream tw)
    //    {
    //        foreach (ISqlCommand cmd in Commands) cmd.GenerateSql(dialect, tw);
    //    }
    //}
}
