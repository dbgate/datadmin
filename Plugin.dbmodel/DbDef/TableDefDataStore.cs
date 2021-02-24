using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;

namespace Plugin.dbmodel
{
    public class TableDefDataStore : StreamDataStoreBase
    {
        TableDefSource m_table;

        public TableDefDataStore(TableDefSource table)
        {
            m_table = table;
        }

        protected override void DoRead(IDataQueue queue)
        {
            if (m_table.m_table.FixedData != null)
            {
                foreach (var row in m_table.m_table.FixedData.Rows) queue.PutRecord(row);
            }
            queue.PutEof();
        }

        protected override void DoWrite(IDataQueue queue)
        {
            m_table.SaveFixedData(queue);
        }

        public override void ClearLoadCaches()
        {
            base.ClearLoadCaches();
        }
        protected override ITableStructure DoGetRowFormat()
        {
            if (m_table.m_table.FixedData != null) return m_table.m_table.FixedData.Structure;
            return null;
        }

        public override bool ConfigurationNeeded
        {
            get { return false; }
        }
    }
}
