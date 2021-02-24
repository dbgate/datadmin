using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Collections.ObjectModel;
using System.Collections;
using System.Xml;
using System.Linq;

namespace DatAdmin
{
    /// <summary>
    /// read-only in-memory table
    /// because of readonly principle is not neccessary to clone this object
    /// </summary>
    public class InMemoryTable : IInMemoryTable<ArrayDataRecord>
    {
        //List<BufferDataRecord> m_rows = new List<BufferDataRecord>();
        //ReadOnlyCollection<BufferDataRecord> m_roRows;
        InMemoryRows m_rows;
        TableStructure m_structure;

        private void Initialize()
        {
            m_rows = new InMemoryRows();
            //m_roRows = new ReadOnlyCollection<BufferDataRecord>(m_rows);
        }

        public InMemoryTable(InMemoryTable oldTable, InMemoryTableOperation op)
        {
            Initialize();
            m_structure = new TableStructure(op.m_table);
            var colindexes = op.ColIndexes;
            foreach (var row in oldTable.Rows)
            {
                m_rows.Add(new ArrayDataRecord(row, colindexes, m_structure));
            }
        }

        public InMemoryTable(ITableStructure table)
        {
            Initialize();
            m_structure = new TableStructure(table);
        }

        public InMemoryTable(InMemoryTable oldTable, DataScript script)
        {
            Initialize();
            m_structure = new TableStructure(oldTable.Structure);
            BedTable bt = new BedTable(oldTable);
            bt.RunScript(script);
            foreach (IBedRecord rec in bt.Rows)
            {
                m_rows.Add(new ArrayDataRecord(rec));
            }
        }

        public InMemoryTable(ITableStructure table, XmlElement xml)
        {
            Initialize();
            m_structure = new TableStructure(table);
            using (XmlNodeReader xr = new XmlNodeReader(xml))
            {
                foreach (var rec in BedTool.LoadFromXml(m_structure, xr))
                {
                    m_rows.Add(new ArrayDataRecord(rec));
                }
            }
        }

        private InMemoryTable()
        {
            Initialize();
        }

        public static InMemoryTable FromEnumerable<T>(ITableStructure table, IEnumerable<T> rows)
            where T : IBedRecord
        {
            var res = new InMemoryTable();
            res.m_structure = new TableStructure(table);
            foreach (IBedRecord rec in rows)
            {
                res.m_rows.Add(new ArrayDataRecord(rec));
            }
            return res;
        }

        public InMemoryTable(ITableStructure table, IBedReader reader)
        {
            Initialize();
            m_structure = new TableStructure(table);
            while (reader.Read())
            {
                m_rows.Add(new ArrayDataRecord(reader));
            }
        }

        public InMemoryRows Rows { get { return m_rows; } }
        public ITableStructure Structure { get { return m_structure; } }
        IRowCollection<ArrayDataRecord> IInMemoryTable<ArrayDataRecord>.Rows { get { return this.Rows; } }

        public void SaveToXml(XmlElement xml)
        {
            using (XmlWriter xw = xml.CreateNavigator().AppendChild())
            {
                BedTool.SaveToXml(m_structure, Rows, xw);
                xw.Flush();
            }
        }
    }

    public class InMemoryRows : ListProxy<ArrayDataRecord>, IRowCollection<ArrayDataRecord>
    {
    }

    public class InMemoryTableOperation
    {
        internal TableStructure m_table;
        internal List<int> m_colIndexes;

        public InMemoryTableOperation(ITableStructure table)
        {
            m_table = new TableStructure(table);
            m_colIndexes = new List<int>(PyList.Range(m_table.Columns.Count));
        }
        public void DropColumn(string name)
        {
            int index = m_table._Columns.GetIndex(name);
            m_table._Columns.RemoveAt(index);
            m_colIndexes.RemoveAt(index);
        }
        public void CreateColumn(IColumnStructure column)
        {
            m_table.AddColumn(column, true);
            m_colIndexes.Add(-1);
        }

        public void RenameColumn(string oldName, string newName)
        {
            m_table.RenameColumn(oldName, newName);
        }

        public int[] ColIndexes { get { return m_colIndexes.ToArray(); } }
    }
}
