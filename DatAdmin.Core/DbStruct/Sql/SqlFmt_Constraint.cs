using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace DatAdmin
{
    partial class SqlDumper
    {
        public virtual void CreateConstraint(IConstraint constraint)
        {
            var caps = m_dialect.DialectCaps;
            if (constraint is IPrimaryKey && caps.PrimaryKeys) CreatePrimaryKeyOrUnique((IPrimaryKey)constraint);
            if (constraint is IForeignKey && caps.ForeignKeys) CreateForeignKey((IForeignKey)constraint);
            if (constraint is IUnique && caps.Uniques) CreatePrimaryKeyOrUnique((IUnique)constraint);
            if (constraint is IIndex && caps.Indexes) CreateIndex((IIndex)constraint);
            if (constraint is ICheck && caps.Checks) CreateCheck((ICheck)constraint);
        }

        protected virtual void CreateCheck(ICheck check)
        {
            Put("^alter ^table %f ^add", check.Table);
            if (check.Name != null) Put(" ^constraint %i", check.Name);
            Put(" ^check (%s)", check.Expression);
            EndCommand();
        }

        protected virtual void ColumnRef(IColumnReference colref)
        {
            WriteRaw(QuoteIdentifier(colref.ColumnName, null));
        }

        protected virtual void ColumnRefs(IEnumerable<IColumnReference> colrefs)
        {
            bool was = false;
            foreach (var colref in colrefs)
            {
                if (was) WriteRaw(",");
                ColumnRef(colref);
                was = true;
            }
        }

        protected virtual void CreateIndex(IIndex index)
        {
            Put("^create ");
            if (index.IsUnique) Put("^unique ");
            if (index.Name == null) throw new Exception("DAE-00247 Cannot create index without name");
            Put("^index %i ^on %f (", index.Name, index.Table);
            ColumnRefs(index.Columns);
            Put(")");
            EndCommand();
        }

        protected virtual void CreateForeignKeyCore(IForeignKey fk)
        {
            if (fk.Name != null) Put("^constraint %i ", fk.Name);
            Put("^foreign ^key (");
            ColumnRefs(fk.Columns);
            Put(") ^references %f", fk.PrimaryKeyTable);
            if (fk.PrimaryKeyColumns != null)
            {
                WriteRaw("(");
                ColumnRefs(fk.PrimaryKeyColumns);
                WriteRaw(")");
            }
            string ondelete = m_dialect.OnDeleteSqlName(fk.OnDeleteAction);
            string onupdate = m_dialect.OnUpdateSqlName(fk.OnUpdateAction);
            if (ondelete != null) Put(" ^on ^delete %k", ondelete);
            if (onupdate != null) Put(" ^on ^update %k", onupdate);
        }

        protected virtual void CreateForeignKey(IForeignKey fk)
        {
            Put("^alter ^table %f ^add ", fk.Table);
            CreateForeignKeyCore(fk);
            EndCommand();
        }

        protected virtual void CreatePrimaryKeyOrUnique(IColumnsConstraint constraint)
        {
            if ((m_dialect.DialectCaps.AnonymousPrimaryKey && constraint is IPrimaryKey) || constraint.Name == null)
            {
                Put("^alter ^table %f ^add %k", constraint.Table, constraint.Type.GetSqlName());
            }
            else
            {
                Put("^alter ^table %f ^add ^constraint %i %k", constraint.Table, constraint.Name, constraint.Type.GetSqlName());
            }
            WriteRaw(" (");
            ColumnRefs(constraint.Columns);
            WriteRaw(")");
            EndCommand();
        }

        public void DropConstraint(IConstraint constraint)
        {
            DropConstraint(constraint, DropFlags.None);
        }

        public virtual void DropConstraint(IConstraint constraint, DropFlags flags)
        {
            if (constraint.Type == ConstraintType.Index)
            {
                PutCmd("^drop ^index %i on %f", constraint.Name, constraint.Table);
            }
            else
            {
                if (m_dialect.DialectCaps.ExplicitDropConstraint)
                {
                    if (constraint.Type == ConstraintType.PrimaryKey)
                    {
                        PutCmd("^alter ^table %f ^drop ^primary ^key", constraint.Table);
                    }
                    else
                    {
                        PutCmd("^alter ^table %f ^drop %k %i", constraint.Table, constraint.Type.GetSqlName(), constraint.Name);
                    }
                }
                else
                {
                    if (constraint != null)
                    {
                        PutCmd("^alter ^table %f ^drop ^constraint %i", constraint.Table, constraint.Name);
                    }
                }
            }
        }

        protected void DropConstraints(IEnumerable constraints, DropFlags flags)
        {
            SqlDumperExtension.DropConstraints(this, constraints, flags);
        }

        protected void CreateConstraints(IEnumerable constraints)
        {
            AlterProcessorExtension.CreateConstraints(this, constraints);
        }
    }
}
