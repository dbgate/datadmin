using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace DatAdmin
{
    public static class DbObjectNameTool
    {
        public static string FkName(NameWithSchema table, IEnumerable<IColumnReference> fkColumns)
        {
            StringBuilder name = new StringBuilder();
            name.Append("FK_");
            name.Append(table.Name);
            foreach (var col in fkColumns)
            {
                name.Append("_");
                name.Append(col.ColumnName.Replace(" ", "_"));
            }
            return name.ToString();
        }

        public static string PkName(NameWithSchema table)
        {
            return "PK_" + table.Name;
        }

        private static string WithColsName(string prefix, NameWithSchema table, IEnumerable<IColumnReference> columns)
        {
            StringBuilder name = new StringBuilder();
            name.Append(prefix);
            name.Append(table.Name);
            foreach (var col in columns)
            {
                name.Append("_");
                name.Append(col.ColumnName.Replace(" ", "_"));
            }
            return name.ToString();
        }


        public static string IndexName(NameWithSchema table, IEnumerable<IColumnReference> columns)
        {
            return WithColsName("IX_", table, columns);
        }

        public static string UniqueName(NameWithSchema table, IEnumerable<IColumnReference> columns)
        {
            return WithColsName("UQ_", table, columns);
        }

        public static string CheckName(NameWithSchema table)
        {
            StringBuilder name = new StringBuilder();
            name.Append("CHK_");
            name.Append(table.Name);
            return name.ToString();
        }

        public static string ConstraintName(IConstraint cnt)
        {
            if (cnt is IPrimaryKey) return PkName(cnt.Table.FullName);
            else if (cnt is IForeignKey) return FkName(cnt.Table.FullName, ((IForeignKey)cnt).Columns);
            else if (cnt is ICheck) return CheckName(cnt.Table.FullName);
            else if (cnt is IUnique) return UniqueName(cnt.Table.FullName, ((IUnique)cnt).Columns);
            else if (cnt is IIndex) return IndexName(cnt.Table.FullName, ((IIndex)cnt).Columns);
            return null;
        }

        //public static void RecreateName(this IConstraint cnt)
        //{
        //    if (cnt is IPrimaryKey) cnt.Name = PkName(cnt.Table);
        //    else if (cnt is ForeignKey) cnt.Name = FkName(cnt.Table, ((ForeignKey)cnt).Columns);
        //    else if (cnt is CheckConstraint) cnt.Name = CheckName(cnt.Table);
        //    else if (cnt is UniqueConstraint) cnt.Name = UniqueName(cnt.Table, ((UniqueConstraint)cnt).Columns);
        //}

        public static void RenameTable(TableStructure tbl, IEnumerable tables, NameWithSchema newname)
        {
            var oldname = tbl.FullName;
            tbl.FullName = newname;
            foreach (TableStructure t in tables)
            {
                foreach (Constraint cnt in t.Constraints)
                {
                    var fk = cnt as ForeignKey;
                    if (fk != null)
                    {
                        if (fk.PrimaryKeyTable == oldname)
                        {
                            fk.PrimaryKeyTable = newname;
                            if (fk.Columns.Count == 1 && fk.Columns[0] .ColumnName.StartsWith(oldname.Name))
                            {
                                string postfix = fk.Columns[0].ColumnName.Substring(oldname.Name.Length);
                                fk.Columns[0] = new ColumnReference(newname.Name + postfix);
                                ((ColumnStructure)t.Columns[oldname.Name + postfix]).ColumnName = newname.Name + postfix;
                            }
                            fk.Name = DbObjectNameTool.ConstraintName(fk);
                        }
                    }
                }
            }
            foreach (Constraint cnt in tbl.Constraints)
            {
                //cnt.Table = newname;
                cnt.Name = DbObjectNameTool.ConstraintName(cnt);
            }
        }
    }
}
