using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    partial class SqlParser
    {
        protected virtual void ReadColRefs(List<IColumnReference> colrefs)
        {
            SkipSymbol("(");
            bool was = false;
            while (!IsSymbol(")"))
            {
                if (was) SkipKeyword(",");
                var cr = new ColumnReference(ReadName());
                colrefs.Add(cr);
                ReadColRefProps(cr);
                if (IsKeyword("asc") || IsKeyword("desc")) NextToken();
                was = true;
            }
            SkipSymbol(")");
        }

        protected virtual void ReadColRefProps(ColumnReference cr)
        {
        }

        protected virtual Constraint ReadConstraint(string name)
        {
            if (SkipTokenIf("primary"))
            {
                SkipKeyword("key");
                PrimaryKey pk = new PrimaryKey();
                pk.Name = name;
                ReadPkParams(pk.SpecificData);
                ReadColRefs(pk.Columns);
                while (!IsTerminal(")") && !IsTerminal(",")) NextToken();
                return pk;
            }
            if (SkipTokenIf("check"))
            {
                CheckConstraint chk = new CheckConstraint();
                chk.Name = name;
                chk.Expression = ReadExprInBracket();
                return chk;
            }
            if (SkipTokenIf("foreign"))
            {
                SkipSymbol("key");
                ForeignKey fk = new ForeignKey();
                fk.Name = name;
                SkipToOneOf("(");
                ReadColRefs(fk.Columns);
                SkipSymbol("references");
                ReadReferencesClause(fk);
            }
            return null;
        }

        protected virtual void ReadFkSpecial(ForeignKey fk)
        {
        }

        protected virtual void ReadReferencesClause(ForeignKey fk)
        {
            fk.PrimaryKeyTable = ReadFullName();
            ReadColRefs(fk.PrimaryKeyColumns);
            ReadFkSpecial(fk);
            while (SkipTokenIf("on"))
            {
                string action = SkipToken();
                if (SkipMultiIf("restrict")) SetFkAction(fk, action, ForeignKeyAction.Restrict);
                else if (SkipMultiIf("cascade")) SetFkAction(fk, action, ForeignKeyAction.Cascade);
                else if (SkipMultiIf("set", "null")) SetFkAction(fk, action, ForeignKeyAction.SetNull);
                else if (SkipMultiIf("no", "action")) SetFkAction(fk, action, ForeignKeyAction.NoAction);
            }
        }

        protected void SetFkAction(ForeignKey fk, string ev, ForeignKeyAction action)
        {
            if (ev.ToLower() == "delete") fk.OnDeleteAction = action;
            if (ev.ToLower() == "update") fk.OnUpdateAction = action;
        }

        protected virtual Constraint ReadConstraint()
        {
            var specParams = new Dictionary<string, string>();
            if (SkipTokenIf("constraint"))
            {
                string name = ReadName();
                return ReadConstraint(name);
            }
            return null;
        }

        protected virtual void ReadPkParams(Dictionary<string, string> specParams)
        {
            SkipToOneOf("(");
        }

        public virtual TableStructure ReadCreateTable()
        {
            SkipKeyword("create");
            SkipTokenIf("temporary");
            SkipTokenIf("temp");
            SkipKeyword("table");
            TableStructure res = new TableStructure();
            res.FullName = ReadFullName();
            SkipSymbol("(");
            bool was = false;
            while (!IsSymbol(")"))
            {
                if (was) SkipKeyword(",");
                Constraint cnt = ReadConstraint();
                if (cnt != null)
                {
                    res._Constraints.Add(cnt);
                }
                else
                {
                    ColumnStructure col = new ColumnStructure();
                    res._Columns.Add(col);
                    ReadColumn(col);
                }
                was = true;
            }
            SkipSymbol(")");
            return res;
        }

        protected virtual void ReadCharSetCollate(ColumnStructure col)
        {
            bool any = true;
            while (any)
            {
                any = false;
                if (SkipTokenIf("collate"))
                {
                    col.Collation = SkipToken();
                    any = true;
                }
                if (SkipMultiIf("character", "set"))
                {
                    col.CharacterSet = SkipToken();
                    any = true;
                }
            }
        }

        protected virtual void ReadColumn(ColumnStructure col)
        {
            col.ColumnName = ReadName();
            col.DataType = ReadDataType();
            col.IsNullable = true;
            if (SkipMultiIf("not", "null")) col.IsNullable = false;
            if (SkipMultiIf("null")) col.IsNullable = true;
            if (SkipMultiIf("default"))
            {
                col.DefaultValue = ParseExpression();
            }
            ReadColumnSpecific(col);
        }

        protected virtual void ReadColumnSpecific(ColumnStructure col)
        {
            SkipToOneOf(",", ")");
        }

        protected virtual DbTypeBase ReadDataType()
        {
            throw new NotImplementedError("DAE-00118");
        }

    }
}
