using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DatAdmin
{
    partial class SqlDumper
    {
        public virtual bool CreateTablePrimaryKey(ITableStructure table, IPrimaryKey pk)
        {
            return true;
        }

        public virtual void CreateTable(ITableStructure tableSrc)
        {
            TableStructure table = new TableStructure(tableSrc);
            Put("^create ^table %f ( &>&n", table.FullName);
            bool first = true;
            foreach (IColumnStructure col in table.Columns)
            {
                if (!first) Put(", &n");
                first = false;
                Put("%i ", col.ColumnName);
                ColumnDefinition(col, true, true, true);
            }
            foreach (IConstraint cnt in table.Constraints)
            {
                if (cnt is IPrimaryKey)
                {
                    if (CreateTablePrimaryKey(table, (IPrimaryKey)cnt))
                    {
                        if (!first) Put(", &n");
                        first = false;
                        if (cnt.Name != null && !m_dialect.DialectCaps.AnonymousPrimaryKey)
                        {
                            Put("^constraint %i", cnt.Name);
                        }
                        Put(" ^primary ^key (%,i)", ((IPrimaryKey)cnt).Columns);
                    }
                }
                if (cnt is IForeignKey && m_dialect.DialectCaps.UncheckedReferences)
                {
                    if (!first) Put(", &n");
                    first = false;
                    CreateForeignKeyCore((IForeignKey)cnt);
                }
            }
            Put("&<&n)");
            CreateTableOptions(table);
            EndCommand();
            foreach (IConstraint cnt in table.Constraints)
            {
                if (cnt is IPrimaryKey) continue;
                if (cnt is IForeignKey && m_dialect.DialectCaps.UncheckedReferences) continue;
                CreateConstraint(cnt);
            }
        }

        public virtual void CustomAction(string query)
        {
            WriteRaw(query);
            EndCommand();
        }

        private static List<int> GetColumnMap(ITableStructure oldTable, ITableStructure newTable)
        {
            List<int> columnMap = new List<int>();

            foreach (var col in newTable.Columns)
            {
                columnMap.Add(oldTable.Columns.IndexOfIf(c => c.GroupId == col.GroupId));
            }

            return columnMap;
        }

        private static List<int> GetConstraintMap(ITableStructure oldTable, ITableStructure newTable)
        {
            List<int> constraintMap = new List<int>();

            foreach (var cnt in newTable.Constraints)
            {
                constraintMap.Add(oldTable.Constraints.IndexOfIf(c => c.GroupId == cnt.GroupId));
            }

            return constraintMap;
        }

        //public void AlterTable(ITableStructure oldTable, ITableStructure newTable, DbDiffOptions options)
        //{
        //    if (oldTable == null) CreateTable(newTable);
        //    else DbDiffTool.AlterTable(this, oldTable, newTable, options);
        //}

        //static int m_lastAlterTableId = 0;

        //public virtual void AlterTable(ITableStructure oldTable, ITableStructure newTable, AlterTableMode mode)
        //{
        //    bool permuteColumns = false;
        //    bool insertColumns = false;
        //    bool renameColumns = false;

        //    List<int> columnMap = GetColumnMap(oldTable, newTable);
        //    List<int> constraintMap = GetConstraintMap(oldTable, newTable);

        //    // count alter requests
        //    int lastcol = -1;
        //    foreach (int col in columnMap)
        //    {
        //        if (col < 0) continue;
        //        if (col < lastcol) permuteColumns = true;
        //        lastcol = col;
        //    }

        //    bool wasins = false;
        //    foreach (int col in columnMap)
        //    {
        //        if (col < 0) wasins = true;
        //        if (col >= 0 && wasins) insertColumns = true;
        //    }

        //    for (int i = 0; i < columnMap.Count; i++)
        //    {
        //        int col = columnMap[i];
        //        if (col < 0) continue;
        //        if (oldTable.Columns[col].ColumnName != newTable.Columns[i].ColumnName) renameColumns = true;
        //    }

        //    bool canUseAlter = m_dialect.SupportsAlterTable;
        //    if (permuteColumns) canUseAlter = false;
        //    if (insertColumns) canUseAlter = false;
        //    if (renameColumns) canUseAlter = m_dialect.AlterTableCaps.ChangeColumnsCaps.AllowRename;

        //    bool recreate = !canUseAlter || mode == AlterTableMode.RecreateAllways;

        //    if (recreate)
        //    {
        //        if (mode == AlterTableMode.Conservative) throw new Exception("Table cannot be altered");
        //        int id = System.Threading.Interlocked.Increment(ref m_lastAlterTableId);
        //        string tmptable = ConnTools.GenerateTempTableName(id);

        //        // remove constraints
        //        DropConstraints(oldTable.ReferencedFrom, DropFlags.None);
        //        DropConstraints(oldTable.Constraints, DropFlags.None);

        //        RenameTable(oldTable.FullName, tmptable);

        //        TableStructure old = new TableStructure(oldTable);
        //        old.FullName = new NameWithSchema(oldTable.FullName.Schema, tmptable);
        //        RecreateTable(old, newTable, columnMap);
        //        PutCmd("^drop ^table %i", tmptable);
        //    }
        //    else
        //    {
        //        int index;

        //        // drop constraints
        //        index = 0;

        //        foreach (IConstraint cnt in oldTable.Constraints)
        //        {
        //            if (constraintMap.IndexOf(index) < 0) DropConstraint(cnt, DropFlags.None);
        //            index++;
        //        }

        //        // drop columns
        //        index = 0;
        //        foreach (IColumnStructure col in oldTable.Columns)
        //        {
        //            if (columnMap.IndexOf(index) < 0) DropColumn(oldTable.FullName, col.ColumnName);
        //            index++;
        //        }

        //        // rename table
        //        if (oldTable.FullName != newTable.FullName)
        //        {
        //            if (oldTable.FullName.Schema != newTable.FullName.Schema)
        //            {
        //                ChangeTableSchema(oldTable.FullName, newTable.FullName.Schema);
        //            }
        //            if (oldTable.FullName.Name != newTable.FullName.Name)
        //            {
        //                RenameTable(new NameWithSchema(oldTable.FullName.Schema, newTable.FullName.Name), newTable.FullName.Name);
        //            }
        //        }

        //        // create columns
        //        index = 0;
        //        foreach (IColumnStructure col in newTable.Columns)
        //        {
        //            if (columnMap[index] < 0)
        //            {
        //                CreateColumn(newTable.FullName, col);
        //            }
        //            index++;
        //        }

        //        // change columns
        //        index = 0;
        //        foreach (IColumnStructure col in newTable.Columns)
        //        {
        //            if (columnMap[index] >= 0)
        //            {
        //                IColumnStructure src = oldTable.Columns[columnMap[index]];
        //                if (!DbDiffTool.EqualsColumns(src, col))
        //                {
        //                    ChangeColumn(newTable.FullName, src, col);
        //                }
        //            }
        //            index++;
        //        }

        //        // create constraints
        //        index = 0;
        //        foreach (IConstraint cnt in newTable.Constraints)
        //        {
        //            if (constraintMap[index] < 0)
        //            {
        //                CreateConstraint(cnt);
        //            }
        //            index++; ;
        //        }
        //    }
        //    AlterTableOptions(oldTable, newTable);
        //}

        public static int m_lastAlterTableId = 0;

        public virtual void RecreateTable(ITableStructure oldTable, ITableStructure newTable)
        {
            if (oldTable.GroupId != newTable.GroupId) throw new InternalError("DAE-00040 Recreate is not possible: oldTable.GroupId != newTable.GroupId");
            var columnMap = GetColumnMap(oldTable, newTable);
            int id = System.Threading.Interlocked.Increment(ref m_lastAlterTableId);
            string tmptable = ConnTools.GenerateTempTableName(id);

            // remove constraints
            //DropConstraints(oldTable.GetReferencedFrom(), DropFlags.None);
            DropConstraints(oldTable.Constraints, DropFlags.None);

            RenameTable(oldTable.FullName, tmptable);

            TableStructure old = new TableStructure(oldTable);
            old.FullName = new NameWithSchema(oldTable.FullName.Schema, tmptable);

            CreateTable(newTable);

            var idcol = newTable.FindAutoIncrementColumn();
            bool hasident = idcol != null && columnMap[idcol.ColumnOrder] >= 0;
            if (hasident) AllowIdentityInsert(newTable.FullName, true);
            PutCmd("^insert ^into %f (%,i) select %,s ^from %f", newTable.FullName,
                from c in newTable.Columns 
                where columnMap[c.ColumnOrder] >= 0
                select c.ColumnName,
                from dstindex in 
                    (
                    from i in PyList.Range(newTable.Columns.Count)
                    where columnMap[i] >= 0
                    select i
                    )
                let srcindex = columnMap[dstindex]
                select
                    (srcindex < 0
                    // srcindex < 0 should not occur thanks to filtering
                    ? Format("^null ^as %i", newTable.Columns[dstindex].ColumnName)
                    : Format("^%i ^as %i", old.Columns[srcindex].ColumnName, newTable.Columns[dstindex].ColumnName)),
                old.FullName);
            if (hasident) AllowIdentityInsert(newTable.FullName, false);

            // newTable.Constraints are allready created
            //CreateConstraints(newTable.GetReferencedFrom());

            PutCmd("^drop ^table %i", tmptable);
        }

        public virtual void AlterTableOptions(ITableStructure table, Dictionary<string, string> options) { }

        public virtual void CreateTableOptions(ITableStructure table) { }

        public virtual void ColumnDefinition_Default(IColumnStructure col)
        {
            string defsql = col.DefaultValue.GenerateSql(m_dialect, col.DataType, null);
            if (defsql != null)
            {
                WriteRaw(" DEFAULT ");
                WriteRaw(defsql);
            }
        }

        protected virtual bool AllowWriteColumnCollation() { return true; }

        public virtual void ColumnDefinition(IColumnStructure col, bool includeDefault, bool includeNullable, bool includeCollate)
        {
            if (m_dialect.DialectCaps.Domains && col.Domain != null && m_props.UseDomains)
            {
                Put("%f", col.Domain);
            }
            else
            {
                WriteRaw(m_dialect.GenericTypeToSpecific(col.DataType).ToString());
            }
            WriteRaw(" ");
            if (includeNullable)
            {
                WriteRaw(col.IsNullable ? "NULL" : "NOT NULL");
            }
            if (includeCollate && m_dialect.SupportsColumnCollation(col.DataType) && !String.IsNullOrEmpty(col.Collation) && AllowWriteColumnCollation())
            {
                WriteRaw(" COLLATE ");
                WriteRaw(col.Collation);
            }
            if (includeDefault && col.DefaultValue != null)
            {
                ColumnDefinition_Default(col);
            }
        }

        public void AlterTable(ITableStructure src, ITableStructure dst, out bool processed)
        {
            processed = false;
        }

        private void Where(NameWithSchema table, string[] cols, object[] vals)
        {
            Put(" ^where ");
            bool was = false;
            for (int i = 0; i < cols.Length; i++)
            {
                if (was) Put(" ^and ");
                if (vals[i].IsNullOrDbNull()) Put("%i ^is ^null", cols[i]);
                else Put("%i=%v", cols[i], vals[i]);
                was = true;
            }
        }

        public virtual void TruncateTable(NameWithSchema name)
        {
            PutCmd("^delete ^from %f", name);
        }

        public void UpdateData(ITableStructure table, DataScript script, ISaveDataProgress progress)
        {
            if (script == null) return;
            int delcnt = 0, inscnt = 0, updrows = 0, updflds = 0;
            if (progress != null)
            {
                updrows = progress.GetCurrent(SaveProgressMeasure.UpdatedRows);
                updflds = progress.GetCurrent(SaveProgressMeasure.UpdatedFields);
            }

            foreach (var del in script.Deletes)
            {
                Put("^delete ^from %f", table.FullName);
                Where(table.FullName, del.CondCols, del.CondValues);
                EndCommand();
                delcnt++;
                if (progress != null) progress.SetCurrent(SaveProgressMeasure.DeletedRows, delcnt);
                if (progress != null && progress.IsCanceled) throw new OperationCanceledError();
            }
            foreach (var upd in script.Updates)
            {
                Put("^update %f ^set ", table.FullName);
                for (int i = 0; i < upd.Columns.Length; i++)
                {
                    if (i > 0) Put(", ");
                    Put("%i=%v", upd.Columns[i], new ValueTypeHolder(upd.Values[i], table.Columns[upd.Columns[i]].DataType));
                }
                Where(table.FullName, upd.CondCols, upd.CondValues);
                EndCommand();
                updrows++;
                updflds += upd.Values.Length;
                if (progress != null)
                {
                    progress.SetCurrent(SaveProgressMeasure.UpdatedRows, updrows);
                    progress.SetCurrent(SaveProgressMeasure.UpdatedFields, updflds);
                }
                if (progress != null && progress.IsCanceled) throw new OperationCanceledError();
            }
            IColumnStructure autoinc = table.FindAutoIncrementColumn();
            bool isIdentityInsert = false;
            foreach (var ins in script.Inserts)
            {
                if (autoinc != null)
                {
                    if (ins.Columns.Contains(autoinc.ColumnName))
                    {
                        if (!isIdentityInsert) AllowIdentityInsert(table.FullName, true);
                        isIdentityInsert = true;
                    }
                    else
                    {
                        if (isIdentityInsert) AllowIdentityInsert(table.FullName, false);
                        isIdentityInsert = false;
                    }
                }
                var vals = new ValueTypeHolder[ins.Columns.Length];
                for (int i = 0; i < ins.Columns.Length; i++)
                {
                    vals[i] = new ValueTypeHolder(ins.Values[i], table.Columns[ins.Columns[i]].DataType);
                }
                PutCmd("^insert ^into %f (%,i) ^values (%,v)", table.FullName, ins.Columns, vals);
                inscnt++;
                if (progress != null) progress.SetCurrent(SaveProgressMeasure.InsertedRows, inscnt);
                if (progress != null && progress.IsCanceled) throw new OperationCanceledError();
            }
            if (isIdentityInsert) AllowIdentityInsert(table.FullName, false);
        }

        public void UpdateData(MultiTableUpdateScript script, ISaveDataProgress progress)
        {
            if (script == null) return;
            int updrows = 0, updflds = 0;
            if (progress != null)
            {
                updrows = progress.GetCurrent(SaveProgressMeasure.UpdatedRows);
                updflds = progress.GetCurrent(SaveProgressMeasure.UpdatedFields);
            }
            foreach (var upd in script.Updates)
            {
                Put("^update %f ^set ", upd.Table);
                for (int i = 0; i < upd.Columns.Length; i++)
                {
                    if (i > 0) Put(", ");
                    Put("%i=%v", upd.Columns[i], upd.Values[i]);
                }
                Where(upd.Table, upd.CondCols, upd.CondValues);
                EndCommand();
                updrows++;
                updflds += upd.Values.Length;
                if (progress != null)
                {
                    progress.SetCurrent(SaveProgressMeasure.UpdatedRows, updrows);
                    progress.SetCurrent(SaveProgressMeasure.UpdatedFields, updflds);
                }
                if (progress != null && progress.IsCanceled) throw new OperationCanceledError();
            }
        }
    }
}
