using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;
using System.Xml;
using System.IO;

namespace DatAdmin
{
    public partial class DockPanelContentFrame : ContentFrame
    {
        public ContentFrame HeaderRedirectFrame;
        public ContentFrame PrimaryContent;
        public bool RedirectMenuToActiveContent = true;

        public DockPanelContentFrame()
        {
            InitializeComponent();
        }

        public void OpenContent(ContentFrame frame, ContentFrame relframe, DocumentDockPosition position)
        {
            frame.m_parentFrame = this;
            DockPane pane = null;
            if (dockPanel1.ActiveDocument != null) pane = dockPanel1.ActiveDocument.DockHandler.Pane;
            if (relframe != null)
            {
                var cw = GetDockContent(relframe);
                if (cw != null) pane = cw.DockHandler.Pane;
            }
            if (frame == PrimaryContent) frame.PersistString = "primary";
            ContentWrapper docker = new ContentWrapper(frame, frame.PersistString);
            if (frame == PrimaryContent)
            {
                docker.DockHandler.CloseButtonVisible = false;
            }
            if (pane != null)
            {
                switch (position)
                {
                    case DocumentDockPosition.Left:
                        docker.Show(pane, DockAlignment.Left, 0.5);
                        return;
                    case DocumentDockPosition.Right:
                        docker.Show(pane, DockAlignment.Right, 0.5);
                        return;
                    case DocumentDockPosition.Top:
                        docker.Show(pane, DockAlignment.Top, 0.5);
                        return;
                    case DocumentDockPosition.Bottom:
                        docker.Show(pane, DockAlignment.Bottom, 0.5);
                        return;
                }
            }
            // fallback
            docker.Show(dockPanel1);
        }

        public override string PageTitle
        {
            get
            {
                if (HeaderRedirectFrame != null) return HeaderRedirectFrame.PageTitleForParent;
                return base.PageTitle;
            }
        }

        public override string PageToolTip
        {
            get
            {
                if (HeaderRedirectFrame != null) return HeaderRedirectFrame.PageToolTipForParent;
                return base.PageToolTip;
            }
        }

        public override Bitmap Image
        {
            get
            {
                if (HeaderRedirectFrame != null) return HeaderRedirectFrame.ImageForParent;
                return base.Image;
            }
        }

        private ContentFrame ActiveContent
        {
            get
            {
                var cnt = dockPanel1.ActiveContent as ContentWrapper;
                if (cnt != null) return cnt.Frame;
                return null;
            }
        }

        public override string MenuBarTitle
        {
            get
            {
                if (RedirectMenuToActiveContent && ActiveContent != null)
                {
                    return ActiveContent.MenuBarTitle;
                }
                return base.MenuBarTitle;
            }
        }

        public override ContentFrame ActiveFrame
        {
            get
            {
                if (RedirectMenuToActiveContent && ActiveContent != null) return ActiveContent;
                return this;
            }
        }

        public override void GetMenu(MenuBuilder bld)
        {
            if (RedirectMenuToActiveContent && ActiveContent != null)
            {
                ActiveContent.GetMenu(bld);
            }
            else
            {
                base.GetMenu(bld);
            }
        }

        private void dockPanel1_ActiveContentChanged(object sender, EventArgs e)
        {
            MainWindow.Instance.UpdateFrameEnabling(this);
        }

        public int ContentCount
        {
            get
            {
                return dockPanel1.Contents.Count;
            }
        }

        private ContentWrapper GetDockContent(ContentFrame frame)
        {
            foreach (var doc in dockPanel1.Contents)
            {
                var cw = doc as ContentWrapper;
                if (cw == null) continue;
                if (cw.Frame == frame) return cw;
            }
            return null;
        }

        public void ReleaseFrame(ContentFrame frame)
        {
            var cw = GetDockContent(frame);
            if (cw != null)
            {
                cw.ReplaceContent(null);
                frame.m_parentFrame = null;
            }
        }

        public bool ContainsContent(ContentFrame frame)
        {
            return GetDockContent(frame) != null;
        }

        public void NotifyDisposed(ContentFrame frame)
        {
            var cw = GetDockContent(frame);
            if (cw != null) cw.DockHandler.Close();
        }

        public void ReuseReleasedFrame(TableDataFrame frame)
        {
            foreach (var doc in dockPanel1.Contents)
            {
                var cw = doc as ContentWrapper;
                if (cw == null) continue;
                if (cw.Frame != null) continue;
                frame.m_parentFrame = this;
                cw.ReplaceContent(frame);
            }
        }

        public void LoadFromXml(XmlElement layoutXml, Dictionary<string, ContentFrame> frames)
        {
            var loader = new FrameLoader { frames = frames, dockframe = this };
            dockPanel1.LoadFromXml(layoutXml, loader.GetContentFromPersistString);
        }

        class FrameLoader
        {
            internal Dictionary<string, ContentFrame> frames;
            internal DockPanelContentFrame dockframe;
            internal IDockContent GetContentFromPersistString(string persistString)
            {
                var frame = frames.Get(persistString, null);
                if (frame != null)
                {
                    frame.m_parentFrame = dockframe;
                    ContentWrapper docker = new ContentWrapper(frame, frame.PersistString);
                    if (frame == dockframe.PrimaryContent)
                    {
                        docker.DockHandler.CloseButtonVisible = false;
                    }
                    return docker;
                }
                return null;
            }
        }

        public void SaveLayout(XmlElement xml)
        {
            dockPanel1.SaveToXml(xml);
        }
    }
}
