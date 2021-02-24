using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Linq;

namespace Plugin.diagrams
{
    public abstract class TablePainter
    {
        public abstract Size MeasureCell(int row, int col);
        public abstract int RowCount { get; }
        public abstract int ColumnCount { get; }
        public abstract void DrawCell(int row, int col, Rectangle r);

        public Size TableSize;
        int[] m_colwis;
        int[] m_rowhis;


        public void CountSizes()
        {
            int rowcnt = RowCount, colcnt = ColumnCount;

            m_colwis = new int[colcnt];
            m_rowhis = new int[rowcnt];

            for (int row = 0; row < rowcnt; row++)
            {
                for (int col = 0; col < colcnt; col++)
                {
                    var size = MeasureCell(row, col);
                    if (size.Width > m_colwis[col]) m_colwis[col] = size.Width;
                    if (size.Height > m_rowhis[row]) m_rowhis[row] = size.Height;
                }
            }

            TableSize.Width = m_colwis.Sum();
            TableSize.Height = m_rowhis.Sum();
        }

        public void DoDraw()
        {
            int rowcnt = RowCount, colcnt = ColumnCount;
            
            int acty = 0;
            for (int row = 0; row < rowcnt; row++)
            {
                int actx = 0;
                for (int col = 0; col < colcnt; col++)
                {
                    var r = new Rectangle(actx, acty, m_colwis[col], m_rowhis[row]);
                    DrawCell(row, col, r);
                    actx += m_colwis[col];
                }
                acty += m_rowhis[row];
            }
        }

        public void ExpandWidth(int newWidth)
        {
            float k = (float)newWidth / (float)TableSize.Width;
            for (int i = 0; i < m_colwis.Length; i++)
            {
                m_colwis[i] = (int)(m_colwis[i] * k);
            }
            m_colwis[m_colwis.Length] += (newWidth - m_colwis.Sum());
            TableSize.Width = newWidth;
        }
    }
}
