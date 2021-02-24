using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public static class DataGridViewExtension
    {
        public static void MoveCurrentCell(this DataGridView grid, int dx, int dy)
        {
            if (grid.CurrentCell == null) return;
            int newrow = grid.CurrentCell.RowIndex + dy;
            int newcol = grid.CurrentCell.ColumnIndex + dx;
            if (newcol < 0 || newcol >= grid.ColumnCount || newrow < 0 || newrow >= grid.RowCount) return;
            grid.CurrentCell = grid.Rows[newrow].Cells[newcol];
        }

        public static void ExchangeRows(this DataGridView grid, int a, int b)
        {
            for (int j = 0; j < grid.Columns.Count; j++)
            {
                object tmp = grid[j, a].Value;
                grid[j, a].Value = grid[j, b].Value;
                grid[j, b].Value = tmp;
            }
            object t2 = grid.Rows[a].Tag;
            grid.Rows[a].Tag = grid.Rows[b].Tag;
            grid.Rows[b].Tag = t2;
        }

        public static void MoveRowUp(this DataGridView grid)
        {
            if (grid.CurrentCell != null && grid.CurrentCell.RowIndex > 0)
            {
                grid.ExchangeRows(grid.CurrentCell.RowIndex, grid.CurrentCell.RowIndex - 1);
                grid.MoveCurrentCell(0, -1);
            }
        }

        public static void MoveRowDown(this DataGridView grid)
        {
            if (grid.CurrentCell != null && grid.CurrentCell.RowIndex < grid.RowCount - 1)
            {
                grid.ExchangeRows(grid.CurrentCell.RowIndex, grid.CurrentCell.RowIndex + 1);
                grid.MoveCurrentCell(0, 1);
            }
        }

        public static DataGridViewRow DeleteCurrentRow(this DataGridView grid)
        {
            if (grid.CurrentCell != null)
            {
                var row = grid.Rows[grid.CurrentCell.RowIndex];
                grid.Rows.RemoveAt(grid.CurrentCell.RowIndex);
                return row;
            }
            return null;
        }

        public static void DeleteRowsWithSelectedCells(this DataGridView grid)
        {
            var rows = new HashSetEx<int>();
            foreach (DataGridViewCell cell in grid.SelectedCells)
            {
                rows.Add(cell.RowIndex);
            }
            var irows = new List<int>(rows);
            irows.Sort();
            irows.Reverse();
            foreach (int rowindex in irows) grid.Rows.RemoveAt(rowindex);
        }
    }
}
