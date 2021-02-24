using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class WindowsDocker : DockerBase
    {
        ImageCache m_imgCache;

        private bool m_machineChanging;

        public WindowsDocker(IDockerFactory factory)
            : base(factory)
        {
            InitializeComponent();
            HWindow.ChangedContentWindows += HWindow_ChangedContentWindows;
            HMainWindow.KeyDown += HMainWindow_KeyDown;
            HWindow.ChangedContent += HWindow_ChangedContent;
            m_imgCache = new ImageCache(imageList1, Color.White);
            ReloadWindows();
        }

        private void SelectActiveContent()
        {
            foreach (ListViewItem item in lsvWindows.Items) item.Selected = false;
            var cont = MainWindow.Instance.GetCurrentContent();
            foreach (ListViewItem item in lsvWindows.Items)
            {
                if (item.Tag == cont) item.Selected = true;
            }
        }

        void HWindow_ChangedContent()
        {
            m_machineChanging = true;
            SelectActiveContent();
            m_machineChanging = false;
        }

        void HMainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Alt)
            {
                int index = GetKeyIndex(e.KeyCode);
                if (index == 0) MainWindow.Instance.ActivateDocker(this);
                if (index > 0 && index <= lsvWindows.Items.Count)
                {
                    var cnt = (ContentFrame)lsvWindows.Items[index - 1].Tag;
                    MainWindow.Instance.ActivateContent(cnt);
                }
            }
        }

        private int GetKeyIndex(Keys keys)
        {
            switch (keys)
            {
                case Keys.D0: return 0;
                case Keys.D1: return 1;
                case Keys.D2: return 2;
                case Keys.D3: return 3;
                case Keys.D4: return 4;
                case Keys.D5: return 5;
                case Keys.D6: return 6;
                case Keys.D7: return 7;
                case Keys.D8: return 8;
                case Keys.D9: return 9;
            }
            return -1;
        }

        public override void OnClose()
        {
            HWindow.ChangedContentWindows -= HWindow_ChangedContentWindows;
            HMainWindow.KeyDown -= HMainWindow_KeyDown;
            HWindow.ChangedContent -= HWindow_ChangedContent;
            base.OnClose();
        }

        void HWindow_ChangedContentWindows()
        {
            ReloadWindows();
        }

        private void ReloadWindows()
        {
            try
            {
                m_machineChanging = true;
                lsvWindows.BeginUpdate();
                lsvWindows.Items.Clear();
                int index = 1;
                foreach (var cnt in MainWindow.Instance.GetContents())
                {
                    if (cnt.ParentFrame != null) continue; // don't show child frames
                    var item = lsvWindows.Items.Add(Texts.Get(cnt.PageTitle));
                    item.SubItems.Add(Texts.Get(cnt.PageTypeTitle));
                    item.SubItems.Add(index.ToString());
                    item.ImageIndex = m_imgCache.GetImageIndex(cnt.Image);
                    item.Tag = cnt;
                    index++;
                }
                lsvWindows.EndUpdate();
            }
            finally
            {
                m_machineChanging = false;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (lsvWindows.SelectedItems.Count > 0)
            {
                var cnt = (ContentFrame)lsvWindows.SelectedItems[0].Tag;
                MainWindow.Instance.CloseContent(cnt);
            }
        }

        private void btnCloseAll_Click(object sender, EventArgs e)
        {
            MainWindow.Instance.CloseAllContents();
        }

        private void lsvWindows_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_machineChanging) return;
            if (lsvWindows.SelectedItems.Count > 0)
            {
                var cnt = (ContentFrame)lsvWindows.SelectedItems[0].Tag;
                MainWindow.Instance.ActivateContent(cnt);
            }
        }
    }


    [DockerFactory(Title = "Window list window", Name = "window_list")]
    public class WindowlistDockerFactory : DockerFactoryBase
    {
        public override IDocker CreateDocker()
        {
            return new WindowsDocker(this);
        }

        public override string MenuTitle
        {
            get { return "s_window_list"; }
        }

        public override Bitmap Icon
        {
            get { return CoreIcons.windowlist; }
        }

        public override DockerState InitialState
        {
            get { return DockerState.DockLeft; }
        }

        public override Keys Shortcut
        {
            get { return Keys.Alt | Keys.D0; }
        }
    }
}
