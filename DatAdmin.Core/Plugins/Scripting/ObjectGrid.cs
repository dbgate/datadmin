using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin.Scripting
{
    public class ObjectGrid
    {
        List<string> m_columns = new List<string>();
        List<object[]> m_rows = new List<object[]>();

        public List<string> Columns
        {
            get { return m_columns; }
        }

        public void AddColumn(string name)
        {
            m_columns.Add(name);
        }

        public void add_column(string name)
        {
            AddColumn(name);
        }

        public void AddRow(params object[] cells)
        {
            m_rows.Add(cells);
        }

        public void add_row(params object[] cells)
        {
            AddRow(cells);
        }

        public List<object[]> Rows
        {
            get { return m_rows; }
        }
    }
}
