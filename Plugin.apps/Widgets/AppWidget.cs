using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using DatAdmin;
using System.Drawing;
using System.ComponentModel;

namespace Plugin.apps
{
    public abstract class AppWidget : AddonBase
    {
        int m_top, m_left, m_width, m_height;
        string m_name;
        AnchorStyles m_anchor = AnchorStyles.Left | AnchorStyles.Top;
        internal AppDesigner Designer;

        protected static Pen m_blackPen = new Pen(Color.Black, 1);
        protected static Brush m_blackBrush = new SolidBrush(Color.Black);
        protected static Brush m_controlBrush = new SolidBrush(SystemColors.Control);
        protected static Font m_infoFont = new Font(FontFamily.GenericSerif, 10);

        [XmlElem]
        [Category("s_layout")]
        public AnchorStyles Anchor
        {
            get { return m_anchor; }
            set
            {
                m_anchor = value;
                if (Designer != null) Designer.ChangedWidget(this);
            }
        }

        [XmlElem]
        [Category("s_layout")]
        public int Height
        {
            get { return m_height; }
            set
            {
                m_height = value;
                if (Designer != null) Designer.ChangedWidget(this);
            }
        }

        [XmlElem]
        [Category("s_layout")]
        public int Width
        {
            get { return m_width; }
            set
            {
                m_width = value;
                if (Designer != null) Designer.ChangedWidget(this);
            }
        }

        [XmlElem]
        [Category("s_layout")]
        public int Left
        {
            get { return m_left; }
            set
            {
                m_left = value;
                if (Designer != null) Designer.ChangedWidget(this);
            }
        }

        [XmlElem]
        [Category("s_layout")]
        public int Top
        {
            get { return m_top; }
            set
            {
                m_top = value;
                if (Designer != null) Designer.ChangedWidget(this);
            }
        }

        [XmlElem]
        [Category("s_design")]
        public string Name
        {
            get { return m_name; }
            set
            {
                if (Designer != null)
                {
                    m_name = Designer.CreateUniqueName(value);
                    Designer.ChangedWidget(this);
                }
                else
                {
                    m_name = value;
                }
            }
        }

        [Browsable(false)]
        public abstract string NameTemplate { get; }

        [Browsable(false)]
        public int Right { get { return Left + Width; } }

        [Browsable(false)]
        public int Bottom { get { return Top + Height; } }

        [Browsable(false)]
        public Interval HInterval { get { return new Interval(Left, Right); } }

        [Browsable(false)]
        public Interval VInterval { get { return new Interval(Top, Bottom); } }

        public virtual Image GetImage() { return null; }

        private int XMid { get { return m_left + m_width / 2; } }
        private int XQ1 { get { return m_left + m_width / 4; } }
        private int XQ3 { get { return m_left + 3 * m_width / 4; } }
        private int YMid { get { return m_top + m_height / 2; } }
        private int YQ1 { get { return m_top + m_height / 4; } }
        private int YQ3 { get { return m_top + 3 * m_height / 4; } }

        public abstract IWidgetControl CreateControl(AppPageInstance pagei);

        [Browsable(false)]
        public override AddonType AddonType
        {
            get { return ApplicationWidgetAddonType.Instance; }
        }

        private Point? GetPoint(HitTestResult htr)
        {
            switch (htr)
            {
                case HitTestResult.BottomAnchor:
                    return new Point(XQ1, Bottom);
                case HitTestResult.BottomMover:
                    return new Point(XQ3, Bottom);
                case HitTestResult.BottomSizer:
                    return new Point(XMid, Bottom);
                case HitTestResult.LBSizer:
                    return new Point(Left, Bottom);
                case HitTestResult.LeftAnchor:
                    return new Point(Left, YQ1);
                case HitTestResult.LeftMover:
                    return new Point(Left, YQ3);
                case HitTestResult.LeftSizer:
                    return new Point(Left, YMid);
                case HitTestResult.LTSizer:
                    return new Point(Left, Top);
                case HitTestResult.RBSizer:
                    return new Point(Right, Bottom);
                case HitTestResult.RightAnchor:
                    return new Point(Right, YQ1);
                case HitTestResult.RightMover:
                    return new Point(Right, YQ3);
                case HitTestResult.RightSizer:
                    return new Point(Right, YMid);
                case HitTestResult.RTSizer:
                    return new Point(Right, Top);
                case HitTestResult.TopAnchor:
                    return new Point(XQ1, Top);
                case HitTestResult.TopMover:
                    return new Point(XQ3, Top);
                case HitTestResult.TopSizer:
                    return new Point(XMid, Top);
            }
            return null;
        }

        public virtual void DrawControlInner(Graphics g)
        {
        }

        public virtual void DrawDesign(Graphics g, bool selected)
        {
            g.FillRectangle(m_controlBrush, Left, Top, Width, Height);
            DrawControlInner(g);
            g.DrawRectangle(m_blackPen, Left, Top, Width, Height);
            g.DrawString(Name, m_infoFont, m_blackBrush, new Point(Left, Top));

            if (selected)
            {
                foreach (HitTestResult ht in Enum.GetValues(typeof(HitTestResult)))
                {
                    var hpt = GetPoint(ht);
                    if (hpt == null) continue;
                    switch (ht)
                    {
                        case HitTestResult.BottomSizer:
                        case HitTestResult.LBSizer:
                        case HitTestResult.LeftSizer:
                        case HitTestResult.LTSizer:
                        case HitTestResult.RBSizer:
                        case HitTestResult.RightSizer:
                        case HitTestResult.RTSizer:
                        case HitTestResult.TopSizer:
                            g.FillRectangle(m_blackBrush, hpt.Value.X - 3, hpt.Value.Y - 3, 7, 7);
                            break;
                        case HitTestResult.BottomAnchor:
                            g.DrawImage((Anchor & AnchorStyles.Bottom) != 0 ? CoreIcons.anchor : CoreIcons.anchor_gray, new Point(hpt.Value.X - 8, hpt.Value.Y - 8));
                            break;
                        case HitTestResult.LeftAnchor:
                            g.DrawImage((Anchor & AnchorStyles.Left) != 0 ? CoreIcons.anchor : CoreIcons.anchor_gray, new Point(hpt.Value.X - 8, hpt.Value.Y - 8));
                            break;
                        case HitTestResult.RightAnchor:
                            g.DrawImage((Anchor & AnchorStyles.Right) != 0 ? CoreIcons.anchor : CoreIcons.anchor_gray, new Point(hpt.Value.X - 8, hpt.Value.Y - 8));
                            break;
                        case HitTestResult.TopAnchor:
                            g.DrawImage((Anchor & AnchorStyles.Top) != 0 ? CoreIcons.anchor : CoreIcons.anchor_gray, new Point(hpt.Value.X - 8, hpt.Value.Y - 8));
                            break;
                        case HitTestResult.BottomMover:
                            g.DrawImage(CoreIcons.down1, new Point(hpt.Value.X - 8, hpt.Value.Y - 8));
                            break;
                        case HitTestResult.TopMover:
                            g.DrawImage(CoreIcons.up1, new Point(hpt.Value.X - 8, hpt.Value.Y - 8));
                            break;
                        case HitTestResult.LeftMover:
                            g.DrawImage(CoreIcons.left1, new Point(hpt.Value.X - 8, hpt.Value.Y - 8));
                            break;
                        case HitTestResult.RightMover:
                            g.DrawImage(CoreIcons.right1, new Point(hpt.Value.X - 8, hpt.Value.Y - 8));
                            break;
                    }
                }
            }
        }

        public HitTestResult HitTest(Point pt)
        {
            foreach (HitTestResult ht in Enum.GetValues(typeof(HitTestResult)))
            {
                var hpt = GetPoint(ht);
                if (hpt == null) continue;
                int maxdist = 3;
                if (ht >= HitTestResult.MinAnchor && ht <= HitTestResult.MaxAnchor) maxdist = 8;
                if (Math.Max(Math.Abs(hpt.Value.X - pt.X), Math.Abs(hpt.Value.Y - pt.Y)) < maxdist)
                {
                    return ht;
                }
            }
            if (pt.X >= Left && pt.Y >= Top && pt.X <= Right && pt.Y <= Bottom) return HitTestResult.In;
            return HitTestResult.Out;
        }

        public void FixAnchoredSize(int oldw, int oldh, int neww, int newh)
        {
            if ((Anchor & AnchorStyles.Left) != 0)
            {
                if ((Anchor & AnchorStyles.Right) != 0)
                {
                    Width += neww - oldw;
                }
            }
            else
            {
                if ((Anchor & AnchorStyles.Right) != 0)
                {
                    Left += neww - oldw;
                }
            }

            if ((Anchor & AnchorStyles.Top) != 0)
            {
                if ((Anchor & AnchorStyles.Bottom) != 0)
                {
                    Height += newh - oldh;
                }
            }
            else
            {
                if ((Anchor & AnchorStyles.Bottom) != 0)
                {
                    Top += newh - oldh;
                }
            }
        }

        public override string ToString()
        {
            return Name ?? "???";
        }

        [XmlElem]
        [Category("s_behaviour")]
        public string NotifyWidgets { get; set; }
    }

    public enum HitTestResult
    {
        Out, In,
        LeftSizer, RightSizer, TopSizer, BottomSizer,
        LTSizer, LBSizer, RTSizer, RBSizer,
        LeftAnchor, MinAnchor = LeftAnchor, LeftMover,
        RightAnchor, RightMover,
        TopAnchor, TopMover,
        BottomAnchor, BottomMover,
        MaxAnchor = BottomMover,
    };
}

namespace DatAdmin.Scripting
{
    public class AppWidget
    {
        protected readonly Plugin.apps.AppWidget m_widget;

        public AppWidget(Plugin.apps.AppWidget widget)
        {
            m_widget = widget;
        }
    }
}
