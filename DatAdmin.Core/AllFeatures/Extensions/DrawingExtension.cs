using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace DatAdmin
{
    public static class DrawingExtension
    {
        public static Size RoundSize(this SizeF size)
        {
            return new Size((int)(size.Width + 0.5), (int)(size.Height + 0.5));
        }
        public static Size GetSmallestUpperBound(this SizeF size)
        {
            var res = new Size((int)size.Width, (int)size.Height);
            if (res.Width < size.Width) res.Width++;
            if (res.Height < size.Height) res.Height++;
            return res;
        }
        public static int GetMidX(this Rectangle r)
        {
            return (r.Left + r.Right) / 2;
        }
        public static int GetMidY(this Rectangle r)
        {
            return (r.Top + r.Bottom) / 2;
        }
        public static Rectangle GetBoundingRectangle(Point pt0, params Point[] pts)
        {
            var res = new Rectangle(pt0.X, pt0.Y, 0, 0);
            foreach (var pt in pts)
            {
                if (pt.X < res.Left)
                {
                    res.Width += res.Left - pt.X;
                    res.X = pt.X;
                }
                if (pt.X > res.Right)
                {
                    res.Width += pt.X - res.Right;
                }
                if (pt.Y < res.Top)
                {
                    res.Height += res.Top - pt.Y;
                    res.Y = pt.Y;
                }
                if (pt.Y > res.Bottom)
                {
                    res.Height += pt.Y - res.Bottom;
                }
            }
            return res;
        }

        public static Interval GetHorInterv(this Rectangle r)
        {
            return new Interval(r.Left, r.Right);
        }

        public static Interval GetVerInterv(this Rectangle r)
        {
            return new Interval(r.Top, r.Bottom);
        }
    }
}
