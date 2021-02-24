using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using DatAdmin;
using System.Windows.Forms;
using System.Linq;
using System.Drawing.Drawing2D;

namespace Plugin.diagrams
{
    public enum BoxModelAlignement { Near, Far, Center, Fill }

    public class GradientDef
    {
        [XmlElem]
        public Color BgColor { get; set; }
        [XmlElem]
        public Color GradientColor { get; set; }
        [XmlElem]
        public bool IsGradient { get; set; }
        [XmlElem]
        public float Angle { get; set; }

        public static GradientDef Gradient(Color c1, Color c2)
        {
            return new GradientDef
            {
                BgColor = c1,
                GradientColor = c2,
                IsGradient = true,
                Angle = 0,
            };
        }

        public static GradientDef Solid(Color color)
        {
            return new GradientDef
            {
                BgColor = color,
                IsGradient = false,
            };
        }
    }

    public abstract class Box
    {
        public BoxModelAlignement HAlign = BoxModelAlignement.Near;
        public BoxModelAlignement VAlign = BoxModelAlignement.Center;
        public int PadLeft, PadTop, PadRight, PadBottom;
        public int BorderLeft, BorderTop, BorderRight, BorderBottom;
        public int MarginLeft, MarginTop, MarginRight, MarginBottom;
        public Brush BorderLeftBrush = SystemBrushes.WindowText, BorderRightBrush = SystemBrushes.WindowText, BorderTopBrush = SystemBrushes.WindowText, BorderBottomBrush = SystemBrushes.WindowText;
        public Brush BackgroundBrush;
        public GradientDef Gradient;

        protected Size? m_innerSize;
        public Rectangle? DrawBounds { get; private set; }

        protected abstract void DoMeasure(Graphics g);
        protected abstract void DoDrawContent(Graphics g, Rectangle r);

        public int MarginAll
        {
            set { MarginLeft = MarginRight = MarginTop = MarginBottom = value; }
        }
        public int PadAll
        {
            set { PadLeft = PadRight = PadTop = PadBottom = value; }
        }
        public int BorderAll
        {
            set { BorderLeft = BorderRight = BorderTop = BorderBottom = value; }
        }
        public Brush BorderAllBrush
        {
            set { BorderLeftBrush = BorderRightBrush = BorderTopBrush = BorderBottomBrush = value; }
        }

        public void SetupGradient(Color c1, Color c2, float angle)
        {
            Gradient = new GradientDef { BgColor = c1, GradientColor = c2, Angle = angle, IsGradient = true };
        }

        protected virtual void FillBackground(Graphics g, Rectangle r)
        {
            if (BackgroundBrush != null)
            {
                g.FillRectangle(BackgroundBrush, r);
            }
            if (Gradient != null)
            {
                if (Gradient.IsGradient)
                {
                    using (var lbr = new LinearGradientBrush(r, Gradient.BgColor, Gradient.GradientColor, Gradient.Angle))
                    {
                        g.FillRectangle(lbr, r);
                    }
                }
                else
                {
                    using (var br = new SolidBrush(Gradient.BgColor))
                    {
                        g.FillRectangle(br, r);
                    }
                }
            }
        }

        public Size GetInnerSize(Graphics g)
        {
            if (m_innerSize == null) DoMeasure(g);
            return m_innerSize.Value;
        }

        public Size GetOuterSize(Graphics g)
        {
            var res = GetInnerSize(g);
            res.Width += PadLeft + PadRight + BorderLeft + BorderRight + MarginLeft + MarginRight;
            res.Height += PadTop + PadBottom + BorderTop + BorderBottom + MarginTop + MarginBottom;
            return res;
        }

        public void CallDraw(Graphics g, Rectangle r)
        {
            DrawBounds = r;
            var bgr = new Rectangle(
                r.Left + MarginLeft + BorderLeft, r.Top + MarginTop + BorderTop,
                r.Width - MarginLeft - BorderLeft - MarginRight - BorderRight,
                r.Height - MarginTop - BorderTop - MarginBottom - BorderBottom);
            FillBackground(g, bgr);
            if (BorderLeft > 0 && BorderLeftBrush != null)
            {
                g.FillRectangle(BorderLeftBrush, new Rectangle(r.Left + MarginLeft, r.Top + MarginTop, BorderLeft, r.Height - MarginTop - MarginBottom));
            }
            if (BorderRight > 0 && BorderRightBrush != null)
            {
                g.FillRectangle(BorderRightBrush, new Rectangle(r.Right - MarginRight - BorderRight, r.Top + MarginTop, BorderRight, r.Height - MarginTop - MarginBottom));
            }

            if (BorderTop > 0 && BorderTopBrush != null)
            {
                g.FillRectangle(BorderTopBrush, new Rectangle(r.Left + MarginLeft, r.Top + MarginTop, r.Width - MarginLeft - MarginRight, BorderTop));
            }
            if (BorderBottom > 0 && BorderBottomBrush != null)
            {
                g.FillRectangle(BorderBottomBrush, new Rectangle(r.Left + MarginLeft, r.Bottom - MarginBottom - BorderBottom, r.Width - MarginLeft - MarginRight, BorderBottom));
            }

            var cr = new Rectangle(bgr.Left + PadLeft, bgr.Top + PadTop, bgr.Width - PadLeft - PadRight, bgr.Height - PadTop - PadBottom);

            var size = GetInnerSize(g);
            switch (HAlign)
            {
                case BoxModelAlignement.Center:
                    cr.X = (cr.Left + cr.Right) / 2 - size.Width / 2;
                    cr.Width = size.Width;
                    break;
                case BoxModelAlignement.Near:
                    cr.Width = size.Width;
                    break;
                case BoxModelAlignement.Far:
                    cr.X = cr.Right - size.Width;
                    cr.Width = size.Width;
                    break;
                case BoxModelAlignement.Fill:
                    break;
            }
            switch (VAlign)
            {
                case BoxModelAlignement.Center:
                    cr.Y = (cr.Top + cr.Bottom) / 2 - size.Height / 2;
                    cr.Height = size.Height;
                    break;
                case BoxModelAlignement.Near:
                    cr.Height = size.Height;
                    break;
                case BoxModelAlignement.Far:
                    cr.Y = cr.Bottom - size.Height;
                    cr.Height = size.Height;
                    break;
                case BoxModelAlignement.Fill:
                    break;
            }

            DoDrawContent(g, cr);
        }
    }

    public class FixedBox : Box
    {
        public Size FixedSize;

        protected override void DoMeasure(Graphics g)
        {
            m_innerSize = FixedSize;
        }

        protected override void DoDrawContent(Graphics g, Rectangle r)
        {
        }
    }

    public class EmptyBox : Box
    {
        protected override void DoMeasure(Graphics g)
        {
            m_innerSize = new Size(0, 0);
        }

        protected override void DoDrawContent(Graphics g, Rectangle r)
        {
        }

        public static EmptyBox Instance = new EmptyBox();
    }

    public class StringBox : Box
    {
        public string Text;
        public Font Font = SystemFonts.DefaultFont;
        public Brush Brush = SystemBrushes.WindowText;

        protected override void DoMeasure(Graphics g)
        {
            m_innerSize = g.MeasureString(Text, Font).GetSmallestUpperBound();
        }

        protected override void DoDrawContent(Graphics g, Rectangle r)
        {
            g.DrawString(Text, Font, Brush, r);
        }
    }

    public class ImageBox : Box
    {
        public Image Image;

        protected override void DoMeasure(Graphics g)
        {
            m_innerSize = Image.Size;
        }

        protected override void DoDrawContent(Graphics g, Rectangle r)
        {
            g.DrawImage(Image, r.Left, r.Top);
        }
    }

    public class PanelBox : Box
    {
        public Orientation Orientation;
        public List<Box> Boxes = new List<Box>();

        protected override void DoMeasure(Graphics g)
        {
            var res = new Size();
            foreach (var box in Boxes)
            {
                var size = box.GetOuterSize(g);
                switch (Orientation)
                {
                    case Orientation.Vertical:
                        res.Height += size.Height;
                        res.Width = Math.Max(res.Width, size.Width);
                        break;
                    case Orientation.Horizontal:
                        res.Width += size.Width;
                        res.Height = Math.Max(res.Height, size.Height);
                        break;
                }
            }
            m_innerSize = res;
        }

        protected override void DoDrawContent(Graphics g, Rectangle r)
        {
            int actpos = Orientation == Orientation.Vertical ? r.Y : r.X;
            foreach (var box in Boxes)
            {
                var size = box.GetOuterSize(g);
                switch (Orientation)
                {
                    case Orientation.Vertical:
                        box.CallDraw(g, new Rectangle(r.Left, actpos, r.Width, size.Height));
                        actpos += size.Height;
                        break;
                    case Orientation.Horizontal:
                        box.CallDraw(g, new Rectangle(actpos, r.Top, size.Width, r.Height));
                        actpos += size.Width;
                        break;
                }
            }
        }
    }

    public class TableBox : Box
    {
        // indexing: delimiter is BEFORE given row index
        public Dictionary<int, Box> RowDelimiterOverrides = new Dictionary<int, Box>();
        public Dictionary<int, Box> ColumnDelimiterOverrides = new Dictionary<int, Box>();

        public Box RowDelimiter;
        public Box ColumnDelimiter;

        public Box LastRowDelimOverride;
        public Box LastColDelimOverride;

        public class Row
        {
            public List<Box> Cells = new List<Box>();
            public Box this[int index]
            {
                get
                {
                    if (index >= 0 && index < Cells.Count) return Cells[index];
                    return EmptyBox.Instance;
                }
            }

            public Box AddCell(Image img)
            {
                var box = new ImageBox { Image = img };
                Cells.Add(box);
                return box;
            }

            public Box AddCell(string text)
            {
                var box = new StringBox { Text = text };
                Cells.Add(box);
                return box;
            }

            public Box AddCell(string text, Font font, Brush brush)
            {
                var box = new StringBox { Text = text, Font = font, Brush = brush };
                Cells.Add(box);
                return box;
            }

            public Box AddCell(Box box)
            {
                Cells.Add(box);
                return box;
            }
        }
        public List<Row> Rows = new List<Row>();

        public void SetupDelimiter(Brush brush, int width, int padding, bool tableBorder)
        {
            var box = new FixedBox { FixedSize = new Size(width, width), BackgroundBrush = brush, MarginAll = padding };
            RowDelimiter = ColumnDelimiter = box;
            if (tableBorder)
            {
                RowDelimiterOverrides[0] = new FixedBox { FixedSize = new Size(width, width), BackgroundBrush = brush, MarginBottom = padding };
                ColumnDelimiterOverrides[0] = new FixedBox { FixedSize = new Size(width, width), BackgroundBrush = brush, MarginLeft = padding };
                LastRowDelimOverride = new FixedBox { FixedSize = new Size(width, width), BackgroundBrush = brush, MarginTop = padding };
                LastColDelimOverride = new FixedBox { FixedSize = new Size(width, width), BackgroundBrush = brush, MarginLeft = padding };
            }
        }

        public bool UseDelimiterAsBorder = false;

        int[] m_rowhis;
        int[] m_colwis;

        int m_delimsWidth;
        int m_delimsHeight;

        private Box GetRowDelimiter(int index)
        {
            if (!UseDelimiterAsBorder)
            {
                if (index == 0 || index >= m_rowhis.Length) return EmptyBox.Instance;
            }
            if (UseDelimiterAsBorder && index == m_rowhis.Length && LastRowDelimOverride != null)
            {
                return LastRowDelimOverride;
            }
            return RowDelimiterOverrides.Get(index, RowDelimiter) ?? EmptyBox.Instance;
        }
        private Box GetColumnDelimiter(int index)
        {
            if (!UseDelimiterAsBorder)
            {
                if (index == 0 || index >= m_colwis.Length) return EmptyBox.Instance;
            }
            if (UseDelimiterAsBorder && index == m_colwis.Length && LastColDelimOverride != null)
            {
                return LastColDelimOverride;
            }
            return ColumnDelimiterOverrides.Get(index, ColumnDelimiter) ?? EmptyBox.Instance;
        }

        protected override void DoMeasure(Graphics g)
        {
            int rowcnt = Rows.Count;
            int colcnt = (from r in Rows select r.Cells.Count).Max();

            m_colwis = new int[colcnt];
            m_rowhis = new int[rowcnt];

            for (int row = 0; row < rowcnt; row++)
            {
                for (int col = 0; col < colcnt; col++)
                {
                    var size = Rows[row][col].GetOuterSize(g);
                    if (size.Width > m_colwis[col]) m_colwis[col] = size.Width;
                    if (size.Height > m_rowhis[row]) m_rowhis[row] = size.Height;
                }
            }

            int w = m_colwis.Sum(), h = m_rowhis.Sum();

            m_delimsHeight = 0;
            for (int i = 0; i <= rowcnt; i++)
            {
                m_delimsHeight += GetRowDelimiter(i).GetOuterSize(g).Height;
            }
            h += m_delimsHeight;

            m_delimsWidth = 0; 
            for (int i = 0; i <= colcnt; i++)
            {
                m_delimsWidth += GetColumnDelimiter(i).GetOuterSize(g).Width;
            }
            w += m_delimsWidth;

            m_innerSize = new Size(w, h);
        }

        protected override void DoDrawContent(Graphics g, Rectangle r)
        {
            int[] colwis = new int[m_colwis.Length];
            Array.Copy(m_colwis, colwis, m_colwis.Length);
            int[] rowhis = new int[m_rowhis.Length];
            Array.Copy(m_rowhis, rowhis, m_rowhis.Length);

            var insize = GetInnerSize(g);

            // adjust cell size, if fill mode is used
            float kx = (float)r.Width / (float)insize.Width;
            for (int i = 0; i < colwis.Length; i++)
            {
                colwis[i] = (int)(colwis[i] * kx);
            }
            colwis[colwis.Length - 1] += (r.Width - colwis.Sum() - m_delimsWidth);

            float ky = (float)r.Height / (float)insize.Height;
            for (int i = 0; i < rowhis.Length; i++)
            {
                rowhis[i] = (int)(rowhis[i] * ky);
            }
            rowhis[rowhis.Length - 1] += (r.Height - rowhis.Sum() - m_delimsHeight);

            int rowcnt = rowhis.Length, colcnt = colwis.Length;

            // paint cells
            int acty = r.Top;
            for (int row = 0; row < rowcnt; row++)
            {
                acty += GetRowDelimiter(row).GetOuterSize(g).Height;
                int actx = r.Left;
                for (int col = 0; col < colcnt; col++)
                {
                    actx += GetColumnDelimiter(col).GetOuterSize(g).Width;
                    var rcell = new Rectangle(actx, acty, colwis[col], rowhis[row]);
                    Rows[row][col].CallDraw(g, rcell);
                    actx += colwis[col];
                }
                acty += rowhis[row];
            }

            // paint delimiters
            int cury = r.Top;
            for (int i = 0; i <= rowcnt; i++)
            {
                var delim = GetRowDelimiter(i);
                var size = delim.GetOuterSize(g);
                var rdelim = new Rectangle(r.Left, cury, r.Width, size.Height);
                delim.CallDraw(g, rdelim);
                cury += size.Height;
                if (i < rowhis.Length) cury += rowhis[i];
            }

            int curx = r.Left;
            for (int i = 0; i <= colcnt; i++)
            {
                var delim = GetColumnDelimiter(i);
                var size = delim.GetOuterSize(g);
                var rdelim = new Rectangle(curx, r.Top, size.Width, r.Height);
                delim.CallDraw(g, rdelim);
                curx += size.Width;
                if (i < colwis.Length) curx += colwis[i];
            }
        }

        public Row AddRow()
        {
            var row = new Row();
            Rows.Add(row);
            return row;
        }
    }
}
