using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DatAdmin
{
    public static partial class DbDiffTool
    {
        public static void AlterFixedData(AlterPlan plan, ITableStructure oldTable, ITableStructure newTable, DbDiffOptions opts)
        {
            NameWithSchema newTableName = GenerateNewName(oldTable.FullName, newTable.FullName, opts);
            DataScript script = AlterFixedData(oldTable.FixedData, newTable.FixedData, null, opts);
            if (script != null) plan.UpdateData(newTableName, script);
        }

        public static DataScript AlterFixedData(InMemoryTable oldData, InMemoryTable newData, InMemoryTableOperation ops, DbDiffOptions opts)
        {
            if (newData == null) return null;
            IPrimaryKey newpk = newData.Structure.FindConstraint<IPrimaryKey>();
            if (newpk == null) return null;
            string[] newcolnames = newData.Structure.Columns.GetNames();
            DataScript script = new DataScript();
            if (oldData == null)
            {
                foreach (var row in newData.Rows)
                {
                    script.Insert(newcolnames, row.GetValues());
                }
            }
            else
            {
                if (ops != null) oldData = new InMemoryTable(oldData, ops);
                IPrimaryKey oldpk = oldData.Structure.FindConstraint<IPrimaryKey>();
                if (oldpk == null) return null;
                string[] newpkcols = newpk.Columns.GetNames();
                string[] oldpkcols = oldpk.Columns.GetNames();
                string[] newnopkcols = newData.Structure.GetNoPkColumns().GetNames();
                string[] oldnopkcols = oldData.Structure.GetNoPkColumns().GetNames();

                foreach (var row in newData.Rows)
                {
                    object[] newpkvals = row.GetValuesByCols(newpkcols);
                    ArrayDataRecord oldrow = oldData.FindRow(oldpkcols, newpkvals);
                    if (oldrow != null)
                    {
                        object[] oldnopkvals=oldrow.GetValuesByCols(oldnopkcols);
                        object[] newnopkvals=row.GetValuesByCols(newnopkcols);
                        if (BedTool.EqualRecords(oldnopkvals, newnopkvals)) continue;
                        // UPDATE
                        List<string> changedcols = new List<string>();
                        List<object> changedvals = new List<object>();
                        for (int i = 0; i < oldnopkcols.Length; i++)
                        {
                            if (!BedTool.EqualValues(oldnopkvals[i], newnopkvals[i]))
                            {
                                changedcols.Add(newnopkcols[i]);
                                changedvals.Add(newnopkvals[i]);
                            }
                        }
                        script.Update(newpkcols.ToArray(), newpkvals.ToArray(), changedcols.ToArray(), changedvals.ToArray());
                    }
                    else
                    {
                        script.Insert(newcolnames, row.GetValues());
                    }
                }
            }
            if (script.IsEmpty()) return null;
            return script;
        }

        public static void AlterTable(AlterPlan plan, ITableStructure oldTable, ITableStructure newTable, DbDiffOptions opts, DbObjectPairing pairing)
        {
            //plan.BeginFixedOrder();
            if (oldTable == null) throw new ArgumentNullException("oldTable", "DAE-00240 oldTable is null");
            if (newTable == null) throw new ArgumentNullException("newTable", "DAE-00241 newTable is null");

            //bool processed;
            //proc.AlterTable(oldTable, newTable, out processed);
            //if (processed) return;

            InMemoryTableOperation dataOps = null;
            if (oldTable.FixedData != null) dataOps = new InMemoryTableOperation(oldTable.FixedData.Structure);

            NameWithSchema newTableName = GenerateNewName(oldTable.FullName, newTable.FullName, opts);

            bool permuteColumns = false;
            bool insertColumns = false;
            //bool renameColumns = false;

            List<int> columnMap = new List<int>();
            List<int> constraintMap = new List<int>();

            foreach (var col in newTable.Columns)
            {
                columnMap.Add(oldTable.Columns.IndexOfIf(c => c.GroupId == col.GroupId));
            }
            foreach (var cnt in newTable.Constraints)
            {
                int cindex = oldTable.Constraints.IndexOfIf(c => c.GroupId == cnt.GroupId);
                if (cindex < 0 && cnt is IPrimaryKey)
                {
                    // primary keys for one table are equal
                    cindex = oldTable.Constraints.IndexOfIf(c => c is IPrimaryKey);
                }
                constraintMap.Add(cindex);
            }

            if (!opts.IgnoreColumnOrder)
            {
                // count alter requests
                int lastcol = -1;
                foreach (int col in columnMap)
                {
                    if (col < 0) continue;
                    if (col < lastcol) permuteColumns = true;
                    lastcol = col;
                }

                bool wasins = false;
                foreach (int col in columnMap)
                {
                    if (col < 0) wasins = true;
                    if (col >= 0 && wasins) insertColumns = true;
                }
            }

            int index;

            // drop constraints
            index = 0;

            foreach (IConstraint cnt in oldTable.Constraints)
            {
                if (constraintMap.IndexOf(index) < 0) plan.DropConstraint(cnt);
                index++;
            }

            // drop columns
            index = 0;
            foreach (IColumnStructure col in oldTable.Columns)
            {
                if (columnMap.IndexOf(index) < 0)
                {
                    plan.DropColumn(col);
                    if (dataOps != null) dataOps.DropColumn(col.ColumnName);
                }
                index++;
            }

            if (!DbDiffTool.EqualFullNames(oldTable.FullName, newTable.FullName, opts))
            {
                plan.RenameTable(oldTable, newTable.FullName);
            }

            // create columns
            index = 0;
            foreach (IColumnStructure col in newTable.Columns)
            {
                if (columnMap[index] < 0)
                {
                    ColumnStructure newcol = new ColumnStructure(col);
                    plan.CreateColumn(oldTable, newcol);
                    if (dataOps != null) dataOps.CreateColumn(newcol);
                }
                index++;
            }

            // change columns
            index = 0;
            foreach (IColumnStructure col in newTable.Columns)
            {
                if (columnMap[index] >= 0)
                {
                    IColumnStructure src = oldTable.Columns[columnMap[index]];
                    if (!DbDiffTool.EqualsColumns(src, col, true, opts, pairing))
                    {
                        using (var ctx = new DbDiffChangeLoggerContext(opts, NopLogger.Instance, DbDiffOptsLogger.DiffLogger))
                        {
                            if (DbDiffTool.EqualsColumns(src, col, false, opts, pairing))
                            {
                                plan.RenameColumn(src, col.ColumnName);
                            }
                            else
                            {
                                plan.ChangeColumn(src, col);
                            }
                            if (dataOps != null && src.ColumnName != col.ColumnName) dataOps.RenameColumn(src.ColumnName, col.ColumnName);
                        }
                    }
                }
                index++;
            }

            // create fixed data script
            var script = AlterFixedData(oldTable.FixedData, newTable.FixedData, dataOps, opts);
            if (script != null) plan.UpdateData(oldTable.FullName, script);

            // change constraints
            index = 0;
            foreach (IConstraint cnt in newTable.Constraints)
            {
                if (constraintMap[index] >= 0)
                {
                    IConstraint src = oldTable.Constraints[constraintMap[index]];
                    if (DbDiffTool.EqualsConstraints(src, cnt, opts, false, pairing) && src.Name != cnt.Name)
                    {
                        if (cnt is IPrimaryKey && (pairing.Source.Dialect.DialectCaps.AnonymousPrimaryKey || pairing.Target.Dialect.DialectCaps.AnonymousPrimaryKey))
                        {
                            // do nothing
                        }
                        else
                        {
                            plan.RenameConstraint(src, cnt.Name);
                        }
                    }
                    else
                    {
                        if (!DbDiffTool.EqualsConstraints(src, cnt, opts, true, pairing))
                        {
                            plan.ChangeConstraint(src, cnt);
                        }
                    }
                }
                index++;
            }

            // create constraints
            index = 0;
            foreach (IConstraint cnt in newTable.Constraints)
            {
                if (constraintMap[index] < 0)
                {
                    plan.CreateConstraint(oldTable, cnt);
                }
                index++; ;
            }

            if (permuteColumns || insertColumns)
            {
                plan.ReorderColumns(oldTable, new List<string>((from c in newTable.Columns select c.ColumnName)));
            }

            var alteredOptions = GetTableAlteredOptions(oldTable, newTable, opts);
            if (alteredOptions.Count > 0) plan.ChangeTableOptions(oldTable, alteredOptions);
            //plan.EndFixedOrder();
        }

        public static Dictionary<string, string> GetTableAlteredOptions(ITableStructure oldTable, ITableStructure newTable, DbDiffOptions opts)
        {
            Dictionary<string, string> alteredOptions = new Dictionary<string, string>();
            if (opts.IgnoreAllTableProperties) return alteredOptions;
            foreach (string key in newTable.SpecificData.Keys)
            {
                if (opts.IgnoreTableProperties.Contains(key)) continue;
                if (oldTable.SpecificData.Get(key) != newTable.SpecificData[key])
                    alteredOptions[key] = newTable.SpecificData[key];
            }
            return alteredOptions;
        }
    }
}
