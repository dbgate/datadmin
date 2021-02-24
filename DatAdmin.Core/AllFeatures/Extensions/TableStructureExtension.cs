using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Linq;

namespace DatAdmin
{
    public static class TableStructureExtension
    {
        public static string PrimaryKeyColumnName(this ITableStructure table)
        {
            if (table == null) return null;
            foreach (IConstraint cnt in table.Constraints)
            {
                if (cnt is IPrimaryKey)
                {
                    IPrimaryKey pk = (IPrimaryKey)cnt;
                    if (pk.Columns.Count == 1) return pk.Columns[0].ColumnName;
                }
            }
            return null;
        }

        public static T GetKeyWithColumn<T>(this ITableStructure table, IColumnStructure column) where T : class, IColumnsConstraint
        {
            if (table == null) return null;
            foreach (IConstraint cnt in table.Constraints)
            {
                if (cnt is T)
                {
                    T k = (T)cnt;
                    if (k.Columns.IndexOfIf(colref => colref.ColumnName == column.ColumnName) >= 0) return k;
                }
            }
            return null;
        }

        public static T FindConstraint<T>(this ITableStructure table) where T : class, IConstraint
        {
            foreach (IConstraint cnt in table.Constraints) if (cnt is T) return (T)cnt;
            return null;
        }

        public static IConstraint FindConstraint(this ITableStructure table, IConstraint cnt)
        {
            foreach (IConstraint c in table.Constraints) if (cnt.Type == c.Type && cnt.Name == c.Name) return c;
            return null;
        }

        public static IConstraint FindConstraint(this ITableStructure table, string type, string name)
        {
            if (type == "primary_key") return table.FindConstraint<IPrimaryKey>();
            return table.FindConstraint(name);
        }

        public static IColumnStructure FindColumn(this ITableStructure table, string colname)
        {
            foreach (IColumnStructure c in table.Columns) if (colname == c.ColumnName) return c;
            return null;
        }

        public static IColumnStructure FindColumn(this ITableStructure table, string colname, bool ignoreCase)
        {
            foreach (IColumnStructure c in table.Columns) if (String.Compare(colname, c.ColumnName, ignoreCase) == 0) return c;
            return null;
        }

        public static IEnumerable<S> GetConstraints<T, S>(this ITableStructure table) where T : class, IConstraint
        {
            foreach (IConstraint cnt in table.Constraints) if (cnt is T) yield return (S)cnt;
        }

        public static IEnumerable<T> GetConstraints<T>(this ITableStructure table) where T : class, IConstraint
        {
            foreach (IConstraint cnt in table.Constraints) if (cnt is T) yield return (T)cnt;
        }

        public static IConstraint FindConstraint(this ITableStructure table, string name)
        {
            foreach (IConstraint cnt in table.Constraints)
            {
                if (cnt.Name == name) return cnt;
            }
            return null;
        }

        public static void RemoveInvalidReferences(this TableStructure res, DatabaseCopyOptions copyOpts, ISqlDialect dialect)
        {
            ((ConstraintCollection)res.Constraints).RemoveIf(cnt => cnt is IForeignKey && !copyOpts.CopyTableStructure(((IForeignKey)cnt).PrimaryKeyTable));
            //res.FullName = copyOpts.GetMappedTableName(res.FullName);
            //foreach (Constraint cnt in res.Constraints)
            //{
            //    cnt.Table = copyOpts.GetMappedTableName(cnt.Table);
            //}
            //List<Constraint> delCnt = new List<Constraint>();
            //foreach (Constraint cnt in res.Constraints)
            //{
            //    if (cnt is IForeignKey)
            //    {
            //        ForeignKey fk = (ForeignKey)cnt;
            //        if (copyOpts.CopyTable(fk.PrimaryKeyTable))
            //        {
            //            fk.PrimaryKeyTable = copyOpts.GetMappedTableName(fk.PrimaryKeyTable);
            //        }
            //        else
            //        {
            //            delCnt.Add(fk);
            //        }
            //    }
            //}
            //foreach (Constraint cnt in delCnt)
            //{
            //    res.Constraints.Remove(cnt);
            //}
        }

        //public static TableStructure RemoveRe(this ITableStructure src, DatabaseCopyOptions copyOpts, ISqlDialect dialect)
        //{
        //    if (!copyOpts.CopyTable(src.FullName)) return null;
        //    TableStructure res = new TableStructure(src);
        //    res.MangleMappedTable(copyOpts, dialect);
        //    return res;
        //}

        public static bool Contains(this TableStructureMembers members, TableStructureMembers test)
        {
            return members.ContainsAny(test);
        }
        public static bool ContainsAny(this TableStructureMembers members, TableStructureMembers test)
        {
            return (members & test) != 0;
        }
        public static bool ContainsAll(this TableStructureMembers members, TableStructureMembers test)
        {
            return (members & test) == test;
        }

        public static bool ContainsAnyColumns(this TableStructureMembers members)
        {
            return members.Contains(TableStructureMembers.ColumnNames)
                || members.Contains(TableStructureMembers.ColumnTypes);
        }

        public static bool ContainsAnyConstraints(this TableStructureMembers members, bool canBeIndex)
        {
            return members.FilterConstraints(canBeIndex) != 0;
        }

        public static TableStructureMembers FilterConstraints(this TableStructureMembers members, bool canBeIndex)
        {
            return members & (canBeIndex ? TableStructureMembers.Constraints : TableStructureMembers.ConstraintsNoIndexes);
        }

        public static IColumnStructure FindAutoIncrementColumn(this ITableStructure table)
        {
            if (table == null || table.Columns == null) return null;
            foreach (IColumnStructure col in table.Columns)
            {
                if (col.DataType is DbTypeInt)
                {
                    if (((DbTypeInt)col.DataType).Autoincrement) return col;
                }
                if (col.DataType is DbTypeNumeric)
                {
                    if (((DbTypeNumeric)col.DataType).Autoincrement) return col;
                }
            }
            return null;
        }

        public static List<IAbstractObjectStructure> GetStructureList_NonEffective(this ITableSource tbl, Func<ITableStructure, IEnumerable> extractCollection)
        {
            List<IAbstractObjectStructure> res = new List<IAbstractObjectStructure>();
            var s = tbl.InvokeLoadStructure(TableStructureMembers.AllNoRefs);
            var col = extractCollection(s);
            if (col == null) return res;
            foreach (IAbstractObjectStructure obj in col)
            {
                res.Add(obj);
            }
            return res;
        }

        public static IEnumerable<IColumnStructure> GetNoPkColumns(this ITableStructure table)
        {
            IPrimaryKey pk = table.FindConstraint<IPrimaryKey>();
            if (pk == null) return table.Columns;
            var res = new List<IColumnStructure>();
            foreach (var col in table.Columns)
            {
                if (pk.Columns.FindIndex(c => c.ColumnName == col.ColumnName) >= 0) continue;
                res.Add(col);
            }
            return res;
        }

        public static IEnumerable<IColumnStructure> GetPkColumns(this ITableStructure table)
        {
            IPrimaryKey pk = table.FindConstraint<IPrimaryKey>();
            if (pk == null) yield break;
            foreach (var col in pk.Columns)
            {
                yield return table.Columns[col.ColumnName];
            }
        }

        public static List<IForeignKey> GetReferencedFrom(this ITableStructure table)
        {
            var res = new List<IForeignKey>();
            if (table.Database == null) return res;
            foreach (var tbl in table.Database.Tables)
            {
                foreach (var cnt in tbl.Constraints)
                {
                    var fk = cnt as IForeignKey;
                    if (fk == null) continue;
                    if (fk.PrimaryKeyTable == table.FullName)
                    {
                        res.Add(fk);
                    }
                }
            }
            return res;
        }

        public static TableStructure CloneWithDb(this ITableStructure table)
        {
            if (table.Database == null) return new TableStructure(table);
            var db = new DatabaseStructure(table.Database);
            return (from t in db.Tables where t.GroupId == table.GroupId select t).FirstOrDefault() as TableStructure;
        }

        public static string[] GetPkColumnNames(this ITableStructure ts)
        {
            return (from c in ts.GetPkColumns() select c.ColumnName).ToArray();
            //var pk = ts.FindConstraint<IPrimaryKey>();
            //if (pk != null) return pk.Columns.GetNames();
            //return new string[] { };
        }

        public static string[] GetNoPkColumnNames(this ITableStructure ts)
        {
            return (from c in ts.GetNoPkColumns() select c.ColumnName).ToArray();
            //var pk = ts.FindConstraint<IPrimaryKey>();
            //var res = new List<string>();
            //var pkcols = new HashSetEx<string>(pk != null ? pk.Columns.GetNames() : new string[] { });
            //foreach (var col in ts.Columns)
            //{
            //    if (!pkcols.Contains(col.ColumnName)) res.Add(col.ColumnName);
            //}
            //return res.ToArray();
        }
    }
}
