using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace DatAdmin
{
    public class GridPosition : IDisposable
    {
        DataGridView m_grid;
        Point m_addr;

        public GridPosition(DataGridView grid)
        {
            m_grid = grid;
            m_addr = m_grid.CurrentCellAddress;
        }

        #region IDisposable Members

        public void Dispose()
        {
            if (m_addr.X >= 0 && m_addr.Y >= 0 && m_addr.X < m_grid.ColumnCount && m_addr.Y < m_grid.RowCount)
            {
                m_grid.CurrentCell = m_grid.Rows[m_addr.Y].Cells[m_addr.X];
            }
        }

        #endregion
    }
}
