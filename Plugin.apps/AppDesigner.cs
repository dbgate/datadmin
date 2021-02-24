using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DatAdmin;

namespace Plugin.apps
{
    public partial class AppDesigner : UserControl
    {
        AppPage m_page;
        AppWidget m_selectedWidget;

        enum MoveMode { None, Near, Move, Far }
        Point? m_moveOrig;
        MoveMode m_horMove = MoveMode.None, m_verMove = MoveMode.None;

        public AppDesigner()
        {
            InitializeComponent();
            if (ApplicationWidgetAddonType.Instance == null || ApplicationWidgetAddonType.Instance.CommonSpace == null)
            {
                return;
            }
            foreach (var hld in ApplicationWidgetAddonType.Instance.CommonSpace.GetAllAddons())
            {
                var btn = new ToolStripButton();
                var widget = (AppWidget)hld.InstanceModel;
                btn.DisplayStyle = ToolStripItemDisplayStyle.ImageAndText;
                btn.Text = Texts.Get(hld.Title);
                btn.Image = widget.GetImage();
                btn.Click += new EventHandler(btn_Click);
                btn.Tag = hld;
                toolStrip1.Items.Add(btn);
            }
        }

        void btn_Click(object sender, EventArgs e)
        {
            var hld = (AddonHolder)((ToolStripButton)sender).Tag;
            var widget = (AppWidget)hld.CreateInstance();
            widget.Name = CreateUniqueName(widget.NameTemplate);
            widget.Left = 10;
            widget.Top = 10;
            widget.Height = 100;
            widget.Width = 100;
            m_page.Widgets.Add(widget);
            widget.Designer = this;
            panel1.Invalidate();
        }

        public AppWidget SelectedWidget
        {
            get { return m_selectedWidget; }
            set
            {
                if (m_selectedWidget != value)
                {
                    m_selectedWidget = value;
                    if (ChangedSelectedWidget != null) ChangedSelectedWidget(this, EventArgs.Empty);
                    panel1.Invalidate();
                    cbxSelectedWidget.Items.Clear();
                    foreach (var widget in m_page.Widgets) cbxSelectedWidget.Items.Add(widget);
                    cbxSelectedWidget.SelectedItem = value;
                }
            }
        }

        public event EventHandler ChangedSelectedWidget;

        public AppPage Page
        {
            get { return m_page; }
            set
            {
                if (m_page != null)
                {
                    foreach (var widget in m_page.Widgets) widget.Designer = null;
                }
                m_page = value;
                if (m_page != null)
                {
                    foreach (var widget in m_page.Widgets) widget.Designer = this;
                }
                FixAnchoredSizes();
            }
        }

        private void NegateAnchor(AppWidget widget, AnchorStyles anchor)
        {
            if ((widget.Anchor & anchor) != 0) widget.Anchor = widget.Anchor & (~anchor);
            else widget.Anchor = widget.Anchor | anchor;
        }

        private void DoWidgetResize(Point pt)
        {
            if (m_selectedWidget == null) return;
            if (m_moveOrig != null)
            {
                int dx = pt.X - m_moveOrig.Value.X;
                int dy = pt.Y - m_moveOrig.Value.Y;
                switch (m_horMove)
                {
                    case MoveMode.Far:
                        m_selectedWidget.Width += dx;
                        break;
                    case MoveMode.Near:
                        m_selectedWidget.Left += dx;
                        m_selectedWidget.Width -= dx;
                        break;
                    case MoveMode.Move:
                        m_selectedWidget.Left += dx;
                        break;
                }
                switch (m_verMove)
                {
                    case MoveMode.Far:
                        m_selectedWidget.Height += dy;
                        break;
                    case MoveMode.Near:
                        m_selectedWidget.Top += dy;
                        m_selectedWidget.Height -= dy;
                        break;
                    case MoveMode.Move:
                        m_selectedWidget.Top += dy;
                        break;
                }
                panel1.Invalidate();
                if (m_selectedWidget.Height < 0) m_selectedWidget.Height = 0;
                if (m_selectedWidget.Width < 0) m_selectedWidget.Width = 0;
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            if (m_page == null) return;
            foreach (var widget in m_page.Widgets)
            {
                widget.DrawDesign(e.Graphics, widget == m_selectedWidget);
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                bool processed = false;
                if (m_selectedWidget != null)
                {
                    var ht = m_selectedWidget.HitTest(e.Location);
                    processed = true;
                    switch (ht)
                    {
                        case HitTestResult.LeftAnchor:
                            NegateAnchor(m_selectedWidget, AnchorStyles.Left);
                            break;
                        case HitTestResult.RightAnchor:
                            NegateAnchor(m_selectedWidget, AnchorStyles.Right);
                            break;
                        case HitTestResult.TopAnchor:
                            NegateAnchor(m_selectedWidget, AnchorStyles.Top);
                            break;
                        case HitTestResult.BottomAnchor:
                            NegateAnchor(m_selectedWidget, AnchorStyles.Bottom);
                            break;
                        case HitTestResult.LeftMover:
                            ExpandLeft(m_selectedWidget);
                            break;
                        case HitTestResult.RightMover:
                            ExpandRight(m_selectedWidget);
                            break;
                        case HitTestResult.TopMover:
                            ExpandTop(m_selectedWidget);
                            break;
                        case HitTestResult.BottomMover:
                            ExpandBottom(m_selectedWidget);
                            break;
                        case HitTestResult.LeftSizer:
                            m_moveOrig = e.Location;
                            m_horMove = MoveMode.Near;
                            m_verMove = MoveMode.None;
                            break;
                        case HitTestResult.TopSizer:
                            m_moveOrig = e.Location;
                            m_horMove = MoveMode.None;
                            m_verMove = MoveMode.Near;
                            break;
                        case HitTestResult.RightSizer:
                            m_moveOrig = e.Location;
                            m_horMove = MoveMode.Far;
                            m_verMove = MoveMode.None;
                            break;
                        case HitTestResult.BottomSizer:
                            m_moveOrig = e.Location;
                            m_horMove = MoveMode.None;
                            m_verMove = MoveMode.Far;
                            break;
                        case HitTestResult.LTSizer:
                            m_moveOrig = e.Location;
                            m_horMove = MoveMode.Near;
                            m_verMove = MoveMode.Near;
                            break;
                        case HitTestResult.RTSizer:
                            m_moveOrig = e.Location;
                            m_horMove = MoveMode.Far;
                            m_verMove = MoveMode.Near;
                            break;
                        case HitTestResult.LBSizer:
                            m_moveOrig = e.Location;
                            m_horMove = MoveMode.Near;
                            m_verMove = MoveMode.Far;
                            break;
                        case HitTestResult.RBSizer:
                            m_moveOrig = e.Location;
                            m_horMove = MoveMode.Far;
                            m_verMove = MoveMode.Far;
                            break;
                        case HitTestResult.In:
                            m_moveOrig = e.Location;
                            m_horMove = MoveMode.Move;
                            m_verMove = MoveMode.Move;
                            break;
                        default:
                            processed = false;
                            break;
                    }
                }

                if (!processed)
                {
                    AppWidget newsel = null;
                    foreach (var widget in m_page.Widgets)
                    {
                        var ht = widget.HitTest(e.Location);
                        if (ht != HitTestResult.Out)
                        {
                            newsel = widget;
                            break;
                        }
                    }
                    SelectedWidget = newsel;
                }
            }
            OnClick(EventArgs.Empty);
            panel1.Invalidate();
        }

        private void ExpandLeft(AppWidget widget)
        {
            int maxx = 0;
            foreach (var w in m_page.Widgets)
            {
                if (w == widget) continue;
                if (w.Right > widget.Left) continue;
                if (w.VInterval.Intersection(widget.VInterval).IsEmpty) continue;
                if (w.Right > maxx) maxx = w.Right;
            }
            widget.Left = maxx;
        }
        private void ExpandTop(AppWidget widget)
        {
            int maxy = 0;
            foreach (var w in m_page.Widgets)
            {
                if (w == widget) continue;
                if (w.Bottom > widget.Top) continue;
                if (w.HInterval.Intersection(widget.HInterval).IsEmpty) continue;
                if (w.Bottom > maxy) maxy = w.Bottom;
            }
            widget.Top = maxy;
        }
        private void ExpandRight(AppWidget widget)
        {
            int minx = m_page.SavedWidth;
            foreach (var w in m_page.Widgets)
            {
                if (w == widget) continue;
                if (w.Left < widget.Right) continue;
                if (w.VInterval.Intersection(widget.VInterval).IsEmpty) continue;
                if (w.Left < minx) minx = w.Left;
            }
            widget.Width = minx - widget.Left;
        }
        private void ExpandBottom(AppWidget widget)
        {
            int miny = m_page.SavedHeight;
            foreach (var w in m_page.Widgets)
            {
                if (w == widget) continue;
                if (w.Top < widget.Bottom) continue;
                if (w.HInterval.Intersection(widget.HInterval).IsEmpty) continue;
                if (w.Top < miny) miny = w.Top;
            }
            widget.Height = miny - widget.Top;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (m_selectedWidget != null && m_moveOrig == null)
            {
                var ht = m_selectedWidget.HitTest(e.Location);
                switch (ht)
                {
                    case HitTestResult.TopSizer:
                    case HitTestResult.BottomSizer:
                        Cursor = Cursors.SizeNS;
                        break;
                    case HitTestResult.LeftSizer:
                    case HitTestResult.RightSizer:
                        Cursor = Cursors.SizeWE;
                        break;
                    case HitTestResult.LTSizer:
                    case HitTestResult.RBSizer:
                        Cursor = Cursors.SizeNWSE;
                        break;
                    case HitTestResult.RTSizer:
                    case HitTestResult.LBSizer:
                        Cursor = Cursors.SizeNESW;
                        break;
                    case HitTestResult.BottomAnchor:
                    case HitTestResult.BottomMover:
                    case HitTestResult.LeftAnchor:
                    case HitTestResult.LeftMover:
                    case HitTestResult.RightAnchor:
                    case HitTestResult.RightMover:
                    case HitTestResult.TopAnchor:
                    case HitTestResult.TopMover:
                        Cursor = Cursors.Hand;
                        break;
                    default:
                        Cursor = Cursors.Default;
                        break;
                }
            }
            if (m_moveOrig != null)
            {
                DoWidgetResize(e.Location);
                m_moveOrig = e.Location;
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            DoWidgetResize(e.Location);
            m_moveOrig = null;
            m_horMove = m_verMove = MoveMode.None;
        }

        private void btnRemoveWidget_Click(object sender, EventArgs e)
        {
            if (m_selectedWidget != null)
            {
                m_page.Widgets.Remove(m_selectedWidget);
                SelectedWidget = null;
            }
        }

        public void ChangedWidget(AppWidget widget)
        {
            panel1.Invalidate();
        }

        private void FixAnchoredSizes()
        {
            if (m_page == null) return;
            foreach (var widget in m_page.Widgets) widget.FixAnchoredSize(m_page.SavedWidth, m_page.SavedHeight, panel1.Width, panel1.Height);
            m_page.SavedWidth = panel1.Width;
            m_page.SavedHeight = panel1.Height;
        }

        private void panel1_Resize(object sender, EventArgs e)
        {
            FixAnchoredSizes();
        }

        public string CreateUniqueName(string value)
        {
            if (m_page.FindWidget(value) == null) return value;
            int suffix = 1;
            while (m_page.FindWidget(value + suffix.ToString()) != null) suffix++;
            return value + suffix.ToString();
        }

        private void AppDesigner_MouseDown(object sender, MouseEventArgs e)
        {
            SelectedWidget = null;
            OnClick(EventArgs.Empty);
            panel1.Invalidate();
        }

        private void cbxSelectedControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedWidget = (AppWidget)cbxSelectedWidget.SelectedItem;
        }

        public void ToggleNextWidget()
        {
            if (cbxSelectedWidget.Items.Count >= 2)
            {
                int index = (cbxSelectedWidget.SelectedIndex + 1) % cbxSelectedWidget.Items.Count;
                cbxSelectedWidget.SelectedIndex = index;
            }
        }
    }
}
