using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace DatAdmin
{
    public class SplitContainerEx : SplitContainer
    {
        Point[] m_ptBuf = new Point[3];
        const int SPLITTER_LEN = 15;
        const int SPLITTER_SPACE = 10;
        //int m_x0, m_y0;
        int? m_panel1MinSizeCache;
        int? m_panel2MinSizeCache;
        int m_splitterDistanceCache;
        bool m_panel1DynamicCollapsed = false;
        bool m_panel2DynamicCollapsed = false;
        Cursor m_origCursor;
        public enum PanelType { First, Second }

        public SplitContainerEx()
        {
            SplitterWidth = 6;
        }

        public PanelType MoreFixedPanel { get; set; }

        public int Panel1MinDynamicSize { get; set; }
        public int Panel2MinDynamicSize { get; set; }

        public bool Panel1DynamicCollapsed
        {
            get { return m_panel1DynamicCollapsed; }
            set
            {
                if (m_panel1DynamicCollapsed == value) return;
                m_panel1DynamicCollapsed = value;
                if (value)
                {
                    m_panel1MinSizeCache = Panel1MinSize;
                    if (MoreFixedPanel == PanelType.First) m_splitterDistanceCache = SplitterDistance;
                    else m_splitterDistanceCache = ClientSize.Height - SplitterDistance;
                    Panel1MinSize = Panel1MinDynamicSize;
                    SplitterDistance = 0;
                }
                else
                {
                    if (m_panel1MinSizeCache != null) Panel1MinSize = m_panel1MinSizeCache.Value;
                    if (MoreFixedPanel == PanelType.First) SplitterDistance = m_splitterDistanceCache;
                    else SplitterDistance = ClientSize.Height - m_splitterDistanceCache;
                    //SplitterDistance = m_splitterDistanceCache;
                    m_panel1MinSizeCache = null;
                }
            }
        }

        public bool Panel2DynamicCollapsed
        {
            get { return m_panel2DynamicCollapsed; }
            set
            {
                if (m_panel2DynamicCollapsed == value) return;
                m_panel2DynamicCollapsed = value;
                if (value)
                {
                    m_panel2MinSizeCache = Panel2MinSize;
                    //m_splitterDistanceCache = SplitterDistance;
                    if (MoreFixedPanel == PanelType.First) m_splitterDistanceCache = SplitterDistance;
                    else m_splitterDistanceCache = ClientSize.Height - SplitterDistance;
                    Panel2MinSize = Panel2MinDynamicSize;
                    SplitterDistance = (Orientation == Orientation.Horizontal ? Height : Width) - SplitterWidth;
                }
                else
                {
                    if (m_panel2MinSizeCache != null) Panel2MinSize = m_panel2MinSizeCache.Value;
                    //SplitterDistance = m_splitterDistanceCache;
                    if (MoreFixedPanel == PanelType.First) SplitterDistance = m_splitterDistanceCache;
                    else SplitterDistance = ClientSize.Height - m_splitterDistanceCache;
                    m_panel2MinSizeCache = null;
                }
            }
        }

        private void DrawTriangle(Graphics g, int x1, int y1, int x2, int y2, int x3, int y3)
        {
            m_ptBuf[0] = new Point(x1, y1);
            m_ptBuf[1] = new Point(x2, y2);
            m_ptBuf[2] = new Point(x3, y3);
            g.FillPolygon(Brushes.Black, m_ptBuf);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            //m_x0 = e.ClipRectangle.Left;
            //m_y0 = e.ClipRectangle.Top;
            int x0 = Orientation == Orientation.Vertical ? SplitterDistance : 0;
            int y0 = Orientation == Orientation.Horizontal ? SplitterDistance : 0;
            //int y0 = m_y0;
            if (Orientation == Orientation.Vertical)
            {
                y0 += SPLITTER_SPACE;
                DrawTriangle(e.Graphics, x0 + SplitterWidth, y0, x0, y0 + SPLITTER_LEN / 2, x0 + SplitterWidth, y0 + SPLITTER_LEN);
                y0 += SPLITTER_SPACE + SPLITTER_LEN;
                DrawTriangle(e.Graphics, x0, y0, x0 + SplitterWidth, y0 + SPLITTER_LEN / 2, x0, y0 + SPLITTER_LEN);
            }
            else
            {
                x0 += SPLITTER_SPACE;
                DrawTriangle(e.Graphics, x0, y0 + SplitterWidth, x0 + SPLITTER_LEN / 2, y0, x0 + SPLITTER_LEN, y0 + SplitterWidth);
                x0 += SPLITTER_SPACE + SPLITTER_LEN;
                DrawTriangle(e.Graphics, x0, y0, x0 + SPLITTER_LEN / 2, y0 + SplitterWidth, x0 + SPLITTER_LEN, y0);
            }
        }

        private void MoveLeftClick()
        {
            if (Panel2DynamicCollapsed) Panel2DynamicCollapsed = false;
            else if (!Panel1DynamicCollapsed) Panel1DynamicCollapsed = true;
        }

        private void MoveRightClick()
        {
            if (Panel1DynamicCollapsed) Panel1DynamicCollapsed = false;
            else if (!Panel2DynamicCollapsed) Panel2DynamicCollapsed = true;
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            bool handled = false;
            if (Orientation == Orientation.Vertical)
            {
                if (e.Y >= SPLITTER_SPACE && e.Y <= SPLITTER_SPACE + SPLITTER_LEN)
                {
                    MoveLeftClick();
                    handled = true;
                }
                if (e.Y >= 2 * SPLITTER_SPACE + SPLITTER_LEN && e.Y <= 2 * SPLITTER_SPACE + 2 * SPLITTER_LEN)
                {
                    MoveRightClick();
                    handled = true;
                }
            }
            else
            {
                if (e.X >= SPLITTER_SPACE && e.X <= SPLITTER_SPACE + SPLITTER_LEN)
                {
                    MoveLeftClick();
                    handled = true;
                }
                if (e.X >= 2 * SPLITTER_SPACE + SPLITTER_LEN && e.X <= 2 * SPLITTER_SPACE + 2 * SPLITTER_LEN)
                {
                    MoveRightClick();
                    handled = true;
                }
            }
            if (handled)
            {
                Control c = this;
                c.Refresh();
            }
            else
            {
                base.OnMouseDown(e);
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            m_origCursor = Cursor;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            int highlightArrow = -1;
            if (Orientation == Orientation.Vertical)
            {
                if (e.Y >= SPLITTER_SPACE && e.Y <= SPLITTER_SPACE + SPLITTER_LEN) highlightArrow = 0;
                if (e.Y >= 2 * SPLITTER_SPACE + SPLITTER_LEN && e.Y <= 2 * SPLITTER_SPACE + 2 * SPLITTER_LEN) highlightArrow = 1;
            }
            else
            {
                if (e.X >= SPLITTER_SPACE && e.X <= SPLITTER_SPACE + SPLITTER_LEN) highlightArrow = 0;
                if (e.X >= 2 * SPLITTER_SPACE + SPLITTER_LEN && e.X <= 2 * SPLITTER_SPACE + 2 * SPLITTER_LEN) highlightArrow = 1;
            }
            if (highlightArrow >= 0) Cursor = Cursors.Hand;
            else Cursor = m_origCursor;
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            Cursor = m_origCursor;
            m_origCursor = null;
        }
    }
}
