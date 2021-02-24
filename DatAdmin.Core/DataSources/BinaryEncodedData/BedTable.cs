using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;

namespace DatAdmin
{
    public class BedTable : IInMemoryTable<BedRow>
    {
        TableStructure m_structure;
        BedValueConvertor m_convertor;
        BedValueConvertor m_defConvertor;

        //private ColumnDisplayInfoCollection m_columnDisplay;

        public ITableStructure Structure { get { return m_structure; } }

        public BedRowCollection Rows { get; private set; }

        public DmlfResultFieldCollection ResultFields { get; set; }

        //public ColumnDisplayInfoCollection ColumnDisplay
        //{
        //    get { return m_columnDisplay; }
        //    set
        //    {
        //        m_columnDisplay = value;
        //        RealColumnsEx = null;
        //        if (m_columnDisplay != null)
        //        {
        //            RealColumnsEx = new List<DmlfColumnRef>();
        //            for (int i = 0; i < Math.Min(m_columnDisplay.Count, m_structure.Columns.Count); i++)
        //            {
        //                RealColumnsEx.Add(new DmlfColumnRef { Source = m_columnDisplay[i].BaseTable, ColumnName = ColumnDisplay[i].BaseColumnName });
        //            }
        //        }
        //    }
        //}
        //// readundant information, for more comfortable work
        //public List<DmlfColumnRef> RealColumnsEx { get; private set; }

        IRowCollection<BedRow> IInMemoryTable<BedRow>.Rows { get { return this.Rows; } }

        public event BedRowEventHandler AddedRow;
        public event BedRowEventHandler RemovedRow;

        public BedTable(ITableStructure structure)
        {
            m_structure = new TableStructure(structure);
            Rows = new BedRowCollection(this);
            m_defConvertor = new BedValueConvertor(new DataFormatSettings());
        }

        public BedTable(InMemoryTable table)
        {
            m_structure = new TableStructure(table.Structure);
            Rows = new BedRowCollection(this);
            foreach (var row in table.Rows) Rows.AddInternal(new BedRow(this, row, BedRowState.Unchanged, m_structure));
            m_defConvertor = new BedValueConvertor(new DataFormatSettings());
        }

        internal BedValueConvertor BedConvertor
        {
            get { return m_convertor ?? m_defConvertor; }
            set { m_convertor = value; }
        }

        public BedRow NewRow()
        {
            return new BedRow(this, null, BedRowState.Detached, m_structure);
        }

        internal void NotifyAddedRow(BedRow row)
        {
            if (AddedRow != null) AddedRow(this, new BedRowEventArgs { Row = row });
        }

        internal void NotifyRemovedRow(BedRow row)
        {
            if (RemovedRow != null) RemovedRow(this, new BedRowEventArgs { Row = row });
        }

        public void AddRow(IBedRecord record)
        {
            Rows.AddInternal(new BedRow(this, new ArrayDataRecord(record), BedRowState.Unchanged, m_structure));
        }

        internal void AddRowInternal(ArrayDataRecord rec)
        {
            Rows.AddInternal(new BedRow(this, rec, BedRowState.Unchanged, m_structure));
        }

        public BedTable Filter(Func<BedRow, bool> filter)
        {
            BedTable res = new BedTable(m_structure);
            res.m_convertor = m_convertor;
            foreach (var row in Rows)
            {
                if (filter(row)) res.AddRow(row);
            }
            return res;
        }

        public InMemoryTable ToInMemoryTable()
        {
            return InMemoryTable.FromEnumerable(m_structure,
                from row in Rows
                where row.RowState != BedRowState.Detached && row.RowState != BedRowState.Deleted
                select row
                );
        }

        public void RunScript(DataScript script)
        {
            foreach (var del in script.Deletes)
            {
                BedRow row = this.FindRow(del.CondCols, del.CondValues);
                if (row != null) Rows.Remove(row);
            }
            foreach (var upd in script.Updates)
            {
                BedRow row = this.FindRow(upd.CondCols, upd.CondValues);
                if (row != null) row[upd.Columns] = upd.Values;
            }
            foreach (var ins in script.Inserts)
            {
                BedRow row = NewRow();
                row[ins.Columns] = ins.Values;
                Rows.Add(row);
            }
        }

        private DmlfColumnRef[] GetBaseWhereCols()
        {
            if (ResultFields != null)
            {
                return ResultFields.GetPrimaryKey(DmlfSource.BaseTable).ToArray();
            }
            else
            {
                IPrimaryKey pk = Structure.FindConstraint<IPrimaryKey>();
                return DmlfColumnRef.BuildFromArray(pk != null ? pk.Columns.GetNames() : Structure.Columns.GetNames(), null);
            }
        }

        public DataScript GetBaseModifyScript()
        {
            DataScript res = new DataScript();
            DmlfColumnRef[] wherecols = GetBaseWhereCols();
            foreach (var row in Rows)
            {
                if (row.RowState == BedRowState.Unchanged) continue;
                // modified rows in multitable views are solved in GetLinkedDataScript()
                if (row.RowState == BedRowState.Modified && ResultFields != null && ResultFields.IsMultiTable()) continue;
                string[] changed = row.GetChangedColumns(false);
                string[] changedNotNull = row.GetChangedColumns(true);
                if (changed.Length == 0 && row.RowState != BedRowState.Deleted) continue;
                switch (row.RowState)
                {
                    case BedRowState.Added:
                        res.Insert(changedNotNull, row.GetValuesByCols(changedNotNull));
                        break;
                    case BedRowState.Modified:
                        res.Update(wherecols.GetNames(), row.Original.GetValuesByCols(wherecols, ResultFields), changed, row[changed]);
                        break;
                    case BedRowState.Deleted:
                        res.Delete(wherecols.GetNames(), row.Original.GetValuesByCols(wherecols, ResultFields));
                        break;
                }
            }
            return res;
        }

        public MultiTableUpdateScript GetLinkedDataScript(NameWithSchema basetable)
        {
            var res = new MultiTableUpdateScript();
            if (ResultFields == null || !ResultFields.IsMultiTable()) return res;
            var pks = new Dictionary<DmlfSource, List<DmlfColumnRef>>();

            foreach (var row in Rows)
            {
                if (row.RowState != BedRowState.Modified) continue;
                var changed = row.GetChangedColumnRefs();
                if (changed.Length == 0) continue;
                var tbls = new List<DmlfSource>();
                foreach (var ch in changed)
                {
                    if (!tbls.Contains(ch.Source)) tbls.Add(ch.Source);
                }
                foreach (var src in tbls)
                {
                    if (pks.ContainsKey(src)) continue;
                    pks[src] = ResultFields.GetPrimaryKey(src);
                }

                foreach (var src in tbls)
                {
                    var cols = new List<DmlfColumnRef>();
                    foreach (var ch in changed)
                    {
                        if (ch.Source != src) continue;
                        cols.Add(ch);
                    }
                    var pk = pks[src];
                    res.Update(src == DmlfSource.BaseTable ? basetable : src.TableOrView,
                        (from c in pk select c.ColumnName).ToArray(),
                        row.Original.GetValuesByCols(pk.ToArray(), ResultFields),
                        (from c in cols select c.ColumnName).ToArray(),
                        row.GetValuesByCols(cols.ToArray()));
                }
            }

            return res;
        }

        public void RevertAllChanges()
        {
            foreach (var row in Rows)
            {
                row.RevertChanges();
            }
            for (int i = 0; i < Rows.Count; )
            {
                var row = Rows[i];
                if (row.RowState == BedRowState.Added) Rows.RemoveAt(i);
                else i++;
            }
        }

        public TColumnDisplay<IColumnStructure> GetColumnDisplay()
        {
            var res = new TColumnDisplay<IColumnStructure>();
            var pk = Structure.FindConstraint<IPrimaryKey>();
            var pkcols = new List<string>();
            if (pk != null) pkcols = new List<string>(pk.Columns.GetNames());
            if (ResultFields == null)
            {
                for (int i = 0; i < Structure.Columns.Count; i++)
                {
                    var col = Structure.Columns[i];
                    var di = new ColumnDisplayInfo { IsPrimaryKey = pkcols.Contains(col.ColumnName) };
                    res.AddColumn(col.ColumnName, i, col);
                }
            }
            else
            {
                for (int i = 0; i < Math.Min(Structure.Columns.Count, ResultFields.Count); i++)
                {
                    var col = Structure.Columns[i];
                    res.AddColumn(ResultFields[i], i, col);
                }
            }
            return res;
        }

        public BedTable GetFirstRows(int count)
        {
            var res = new BedTable(Structure);
            for (int i = 0; i < Math.Min(count, Rows.Count); i++)
            {
                res.AddRow(Rows[i]);
            }
            return res;
        }
    }
}
