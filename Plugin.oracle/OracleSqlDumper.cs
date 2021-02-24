using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.Text.RegularExpressions;

namespace Plugin.oracle
{
    public class OracleSqlDumper : SqlDumper
    {
        public OracleSqlDumper(ISqlOutputStream stream, ISqlDialect dialect, SqlFormatProperties props)
            : base(stream, dialect, props)
        {
        }
        public override void RenameTable(NameWithSchema oldName, string newName)
        {
            PutCmd("^alter ^table %f ^rename ^to %i", oldName, newName);
        }

        public override void RenameColumn(IColumnStructure column, string newcol)
        {
            PutCmd("^alter ^table %f ^rename ^column %i ^to %i", column.Table.FullName, column.ColumnName, newcol);
        }

        public override void ChangeColumn(IColumnStructure oldcol, IColumnStructure newcol, IEnumerable<IConstraint> constrains)
        {
            if (oldcol.ColumnName != newcol.ColumnName) RenameColumn(oldcol, newcol.ColumnName);
            if (oldcol.IsNullable != newcol.IsNullable)
            {
                PutCmd("^alter ^table %f ^modify %i %k", newcol.Table, newcol.ColumnName, newcol.IsNullable ? "null" : "not null");
            }
            if (!DbDiffTool.EqualDefaultValues(oldcol, newcol))
            {
                if (newcol.DefaultValue == null)
                {
                    PutCmd("^alter ^table %f ^modify %i ^default ^null", newcol.Table, newcol.ColumnName);
                }
                else
                {
                    Put("^alter ^table %f ^modify %i ", newcol.Table, newcol.ColumnName);
                    ColumnDefinition_Default(newcol);
                    this.EndCommand();
                }
            }
            if (!DbDiffTool.EqualTypes(oldcol.DataType, newcol.DataType, new DbDiffOptions()))
            {
                Put("^alter ^table %f ^modify %i ", newcol.Table, newcol.ColumnName);
                ColumnDefinition(newcol, false, false, false);
            }
            EndCommand();
        }

        public override void RenameConstraint(IConstraint constraint, string newname)
        {
            if (constraint is IIndex)
            {
                PutCmd("^alter ^index %i ^rename ^to %i", constraint.Name, newname);
            }
            else
            {
                PutCmd("^alter ^table %f ^rename ^constraint %i ^to %i", constraint.Table.FullName, constraint.Name, newname);
            }
        }

        public override void DropConstraint(IConstraint constraint, DropFlags flags)
        {
            if (constraint.Type == ConstraintType.Index)
            {
                PutCmd("^drop ^index %i", constraint.Name);
            }
            else
            {
                PutCmd("^alter ^table %f ^drop ^constraint %i", constraint.Table, constraint.Name);
            }
        }

        private string MatchEval(Match m)
        {
            return m.Value.Substring(0, m.Groups[2].Index) + m.Value.Substring(m.Groups[2].Index + m.Groups[2].Length);
        }

        public override void CreateSpecificObject(ISpecificObjectStructure obj)
        {
            string createsql = obj.CreateSql;

            if (FormatProperties.CleanupSpecificObjectCode)
            {
                string regex = "create\\s*(or\\s*replace)?\\s*" + obj.ObjectType + "\\s*(\\\"[^\\\"]+\\\"\\.)\\\"[^\\\"]+\\\"";
                createsql = Regex.Replace(createsql, regex, MatchEval, RegexOptions.IgnoreCase);
            }
            WriteRaw(createsql);
            EndCommand();
        }
    }
}
