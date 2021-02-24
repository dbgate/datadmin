using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;

namespace Plugin.effiproz
{
    public class EfzSqlDumper : SqlDumper
    {
        public EfzSqlDumper(ISqlOutputStream stream, ISqlDialect dialect, SqlFormatProperties props)
            : base(stream, dialect, props)
        {
        }

        public override void RenameTable(NameWithSchema oldName, string newName)
        {
            PutCmd("^alter ^table %f ^rename ^to %i", oldName, newName);
        }

        public override void RenameColumn(IColumnStructure column, string newcol)
        {
            PutCmd("^alter ^table %f ^alter ^column %i ^rename ^to %i", column.Table.FullName, column.ColumnName, newcol);
        }

        public override void RenameConstraint(IConstraint constraint, string newname)
        {
            PutCmd("^alter ^constraint %i ^rename ^to %i", constraint.Name, newname);
        }

        private void CreateDefault(IColumnStructure col)
        {
            if (col.DefaultValue == null) return;
            string defsql = col.DefaultValue.GenerateSql(m_dialect, col.DataType, null);
            if (defsql != null)
            {
                PutCmd("^alter ^table %f ^alter ^column %i ^set ^default %s", col.Table, col.ColumnName, defsql);
            }
        }

        public override void ChangeColumn(IColumnStructure oldcol, IColumnStructure newcol, IEnumerable<IConstraint> constraints)
        {
            if (oldcol.DefaultValue != null) PutCmd("^alter ^table %f ^alter ^column %i ^drop ^default", newcol.Table, newcol.ColumnName);
            if (oldcol.ColumnName != newcol.ColumnName) RenameColumn(oldcol, newcol.ColumnName);
            // change data type
            if (!DbDiffTool.EqualTypes(oldcol.DataType, newcol.DataType, new DbDiffOptions()))
            {
                Put("^alter ^table %f ^alter ^column %i ", newcol.Table, newcol.ColumnName);
                // remove autoincrement flag
                ColumnStructure newcol2 = new ColumnStructure(newcol);
                newcol2.SetDummyTable(newcol.Table.FullName);
                newcol2.DataType.SetAutoincrement(false);
                ColumnDefinition(newcol2, false, false, false);
                EndCommand();
            }
            if (oldcol.IsNullable != newcol.IsNullable)
            {
                PutCmd("^alter ^table %f ^alter ^column %i ^set %k ^null", oldcol.Table, newcol.ColumnName, newcol.IsNullable ? "" : "not");
            }
            CreateDefault(newcol);
            if (oldcol.DataType.IsAutoIncrement() != newcol.DataType.IsAutoIncrement())
            {
                if (oldcol.DataType.IsAutoIncrement())
                {
                    PutCmd("^alter ^table %f ^alter ^column %i ^drop ^generated", newcol.Table.FullName, newcol.ColumnName);
                }
                if (newcol.DataType.IsAutoIncrement())
                {
                    PutCmd("^alter ^table %f ^alter ^column %i %s ^generated ^always ^as ^identity (^start ^with %s)",
                        newcol.Table.FullName, newcol.ColumnName, m_dialect.GenericTypeToSpecific(newcol.DataType), 1);
                }
            }
            this.CreateConstraints(constraints);
        }

        public override void DropConstraint(IConstraint constraint, DropFlags flags)
        {
            if (constraint.Type == ConstraintType.Index)
            {
                PutCmd("^drop ^index %i", constraint.Name);
            }
            else
            {
                base.DropConstraint(constraint, flags);
            }
        }
    }
}
