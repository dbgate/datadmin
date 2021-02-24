using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace DatAdmin
{
    public abstract class DataGridStyleBase : AddonBase, IDataGridStyle
    {
        public override AddonType AddonType
        {
            get { return DataGridStyleAddonType.Instance; }
        }

        public static readonly Color ModernYellow = Color.FromArgb(255, 255, 219);
        public static readonly Color ModernRed = Color.FromArgb(255, 219, 219);

        public static readonly Color OddRowColor = Color.FromArgb(235, 235, 235);
        public static readonly Color EvenRowColor = Color.FromArgb(235, 245, 255);
        public static readonly Color ModifiedRowColor = ModernYellow;
        public static readonly Color ModifiedCellColor = ModernRed;
        public static readonly Color RemovedCellColor = Color.FromArgb(255, 219, 255);
        public static readonly Color AddedCellColor = Color.FromArgb(219, 255, 219);

        public static readonly Brush EvenRowBrush = new SolidBrush(EvenRowColor);
        public static readonly Brush OddRowBrush = new SolidBrush(OddRowColor);
        public static readonly Brush ModifiedRowBrush = new SolidBrush(ModifiedRowColor);
        public static readonly Brush ModifiedCellBrush = new SolidBrush(ModifiedCellColor);
        public static readonly Brush RemovedCellBrush = new SolidBrush(RemovedCellColor);
        public static readonly Brush AddedCellBrush = new SolidBrush(AddedCellColor);

        public static readonly Brush HighlightBrush = Brushes.DarkOrange;

        public virtual Color LookupHintColor { get { return Color.Gray; } }
        public abstract void PaintRowNumberBackground(DataGridViewCellPaintingEventArgs e, DataGridHeaderCellType ctype);
        public abstract void PaintColumnHeaderBackground(DataGridViewCellPaintingEventArgs e, DataGridHeaderCellType ctype);
        public abstract void PaintCellBackground(DataGridViewCellPaintingEventArgs e, DataGridCellType ctype);
        public virtual Color GridColor { get { return Color.FromArgb(210, 210, 210); } }

        protected Brush GetNonRegularBrush(DataGridCellType ctype)
        {
            switch (ctype)
            {
                case DataGridCellType.AddedRow:
                    return AddedCellBrush;
                case DataGridCellType.ModifiedCell:
                    return ModifiedCellBrush;
                case DataGridCellType.ModifiedRow:
                    return ModifiedRowBrush;
                case DataGridCellType.RemovedRow:
                    return RemovedCellBrush;
                case DataGridCellType.Highlight:
                    return HighlightBrush;
            }
            return null;
        }

        public static void FillGradientHeaderCellRectangle(Rectangle r, Graphics g, DataGridHeaderCellType ctype)
        {
            if (ctype == DataGridHeaderCellType.Highlight)
            {
                g.FillRectangle(HighlightBrush, r);
                return;
            }
            Color c1, c2;
            if (ctype == DataGridHeaderCellType.Current)
            {
                c1 = Color.FromArgb(220, 240, 255);
                c2 = Color.FromArgb(155, 170, 180);
            }
            else
            {
                c1 = Color.FromArgb(255, 255, 255);
                c2 = Color.FromArgb(200, 200, 200);
            }
            using (var lb1 = new LinearGradientBrush(r, c1, c2, System.Drawing.Drawing2D.LinearGradientMode.Vertical))
            {
                g.FillRectangle(lb1, r);
            }
        }
    }

    [DataGridStyle(Name = "default", Title = "s_default")]
    public class DefaultDataGridStyle : DataGridStyleBase
    {
        public static DefaultDataGridStyle Instance = new DefaultDataGridStyle();

        public override void PaintRowNumberBackground(DataGridViewCellPaintingEventArgs e, DataGridHeaderCellType ctype)
        {
            FillGradientHeaderCellRectangle(e.CellBounds, e.Graphics, ctype);
        }

        public override void PaintColumnHeaderBackground(DataGridViewCellPaintingEventArgs e, DataGridHeaderCellType ctype)
        {
            FillGradientHeaderCellRectangle(e.CellBounds, e.Graphics, ctype);
        }

        public override void PaintCellBackground(DataGridViewCellPaintingEventArgs e, DataGridCellType ctype)
        {
            Brush bg = Brushes.White;
            Brush fg = Brushes.Black;

            if (e.RowIndex > 0 && e.RowIndex % 6 == 2) bg = OddRowBrush;
            if (e.RowIndex > 0 && e.RowIndex % 6 == 5) bg = EvenRowBrush;

            bg = GetNonRegularBrush(ctype) ?? bg;

            if (ctype == DataGridCellType.Selected) bg = Brushes.Navy;

            e.Graphics.FillRectangle(bg, e.CellBounds);
        }
    }

    [DataGridStyle(Name = "alternative", Title = "Alternative")]
    public class AlternativeDataGridStyle : DataGridStyleBase
    {
        public override void PaintRowNumberBackground(DataGridViewCellPaintingEventArgs e, DataGridHeaderCellType ctype)
        {
            FillGradientHeaderCellRectangle(e.CellBounds, e.Graphics, ctype);
        }

        public override void PaintColumnHeaderBackground(DataGridViewCellPaintingEventArgs e, DataGridHeaderCellType ctype)
        {
            FillGradientHeaderCellRectangle(e.CellBounds, e.Graphics, ctype);
        }

        public override void PaintCellBackground(DataGridViewCellPaintingEventArgs e, DataGridCellType ctype)
        {
            Brush bg = Brushes.White;
            Brush fg = Brushes.Black;

            if (e.RowIndex > 0 && e.RowIndex % 2 == 1) bg = EvenRowBrush;

            bg = GetNonRegularBrush(ctype) ?? bg;

            if (ctype == DataGridCellType.Selected) bg = Brushes.Navy;

            e.Graphics.FillRectangle(bg, e.CellBounds);
        }
    }

    [DataGridStyle(Name = "windows", Title = "Windows default")]
    public class WindowsDataGridStyle : DataGridStyleBase
    {
        public override void PaintRowNumberBackground(DataGridViewCellPaintingEventArgs e, DataGridHeaderCellType ctype)
        {
            e.PaintBackground(e.CellBounds, false);
        }

        public override void PaintColumnHeaderBackground(DataGridViewCellPaintingEventArgs e, DataGridHeaderCellType ctype)
        {
            e.PaintBackground(e.CellBounds, false);
        }

        public override void PaintCellBackground(DataGridViewCellPaintingEventArgs e, DataGridCellType ctype)
        {
            Brush bg = SystemBrushes.Window;

            bg = GetNonRegularBrush(ctype) ?? bg;

            if (ctype == DataGridCellType.Selected) bg = SystemBrushes.Highlight;

            e.Graphics.FillRectangle(bg, e.CellBounds);
        }

        public override Color GridColor { get { return SystemColors.ControlDark; } }
    }
}
