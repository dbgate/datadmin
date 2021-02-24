using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DatAdmin
{
    public class BedRowCollection : ListProxy<BedRow>, IRowCollection<BedRow>
    {
        BedTable m_table;

        internal BedRowCollection(BedTable table)
        {
            m_table = table;
        }

        public override void Add(BedRow item)
        {
            if (item.RowState != BedRowState.Detached) throw new BadBedRowStateError("DAE-00214", BedRowState.Detached, item.RowState);
            base.Add(item);
            item.RowState = BedRowState.Added;
            m_table.NotifyAddedRow(item);
        }

        public override void Insert(int index, BedRow item)
        {
            if (item.RowState != BedRowState.Detached) throw new BadBedRowStateError("DAE-00215", BedRowState.Detached, item.RowState);
            base.Insert(index, item);
            item.RowState = BedRowState.Added;
            m_table.NotifyAddedRow(item);
        }

        public override bool Remove(BedRow item)
        {
            int index = IndexOf(item);
            if (index >= 0)
            {
                RemoveAt(index);
                return true;
            }
            return false;
        }

        public void RawRemoveAt(int index)
        {
            base.RemoveAt(index);
        }

        public override void RemoveAt(int index)
        {
            var item = this[index];

            if (item.RowState == BedRowState.Added)
            {
                item.RowState = BedRowState.Detached;
                base.RemoveAt(index);
            }
            else
            {
                item.RowState = BedRowState.Deleted;
            }

            m_table.NotifyRemovedRow(item);
        }

        internal void AddInternal(BedRow row)
        {
            base.Add(row);
        }
    }
}
