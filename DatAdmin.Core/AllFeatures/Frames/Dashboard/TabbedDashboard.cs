using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class TabbedDashboardFrame : ContentFrame
    {
        ITreeNode m_selectedObject;
        int hi0;
        bool m_updatingTabs;
        ImageCache m_imgCache;
        bool m_suspendedDashboard;

        public bool UpdatingTabs
        {
            get { return m_updatingTabs; }
        }

        internal struct PageRecord
        {
            internal WidgetBaseFrame ctrl;
            internal bool isloaded;
            internal IWidget widget;

            internal void Dispose()
            {
                if (ctrl != null)
                {
                    ctrl.Dispose();
                    ctrl = null;
                    widget = null;
                }
            }

            internal void ReplaceWidget(IWidget wg)
            {
                if (widget != null)
                {
                    widget.ReplaceControl(wg);
                }
                widget = wg;
            }
        }
        PageRecord[] m_loadedPages = new PageRecord[0];

        public TabbedDashboardFrame()
        {
            InitializeComponent();
            SelectedObject = null;
            m_imgCache = new ImageCache(imageList1, SystemColors.ButtonFace);
        }

        public ITreeNode SelectedObject
        {
            get
            {
                return m_selectedObject;
            }
            set
            {
                if (m_selectedObject != null) m_selectedObject.ChangedProperties -= new EventHandler(m_selectedObject_ChangedProperties);
                m_selectedObject = value;
                if (m_selectedObject != null) m_selectedObject.ChangedProperties += new EventHandler(m_selectedObject_ChangedProperties);
                UpdateTabs();
            }
        }

        void m_selectedObject_ChangedProperties(object sender, EventArgs e)
        {
            UpdateTabs();
        }

        public override void OnShowContent()
        {
            UpdateTabs();
        }

        private void UpdateTabs()
        {
            if (!IsContentVisible || m_suspendedDashboard) return;
            //tabControl1.TabPages.Clear();
            try
            {
                m_updatingTabs = true;
                tabControl1.Enabled = false;

                var widgets = TreeNodeExtension.GetAllWidgets(m_selectedObject);

                int pgindex = 0;
                foreach (IWidget widget in widgets)
                {
                    TabPage pg = FindPage(widget, pgindex);
                    if (pg != null)
                    {
                        if (tabControl1.TabPages.IndexOf(pg) != pgindex)
                        {
                            int origindex = tabControl1.TabPages.IndexOf(pg);
                            tabControl1.TabPages.RemoveAt(origindex);
                            tabControl1.TabPages.Insert(pgindex, pg);
                            List<PageRecord> tmp = new List<PageRecord>(m_loadedPages);
                            PageRecord orig = tmp[origindex];
                            tmp[pgindex].Dispose();
                            tmp.RemoveAt(origindex);
                            tmp.Insert(pgindex, orig);
                            m_loadedPages = tmp.ToArray();
                        }
                        m_loadedPages[pgindex].isloaded = false;
                        m_loadedPages[pgindex].ReplaceWidget(widget);
                        pg.Text = Texts.Get(widget.PageTitle);
                        pg.ImageIndex = m_imgCache.GetImageIndex(widget.Image);
                    }
                    else
                    {
                        List<PageRecord> tmp = new List<PageRecord>(m_loadedPages);
                        PageRecord newrec = new PageRecord();
                        newrec.widget = widget;
                        tabControl1.TabPages.Insert(pgindex, Texts.Get(widget.PageTitle));
                        tabControl1.TabPages[pgindex].ImageIndex = m_imgCache.GetImageIndex(widget.Image);
                        tmp.Insert(pgindex, newrec);
                        m_loadedPages = tmp.ToArray();
                    }
                    pgindex++;
                }
                try
                {
                    while (tabControl1.TabPages.Count > widgets.Count)
                    {
                        tabControl1.SelectedIndex = 0;
                        tabControl1.TabPages.RemoveAt(widgets.Count);
                    }
                    List<PageRecord> tmp = new List<PageRecord>(m_loadedPages);
                    for (int i = widgets.Count; i < tmp.Count; i++) tmp[i].Dispose();
                    tmp.RemoveRange(widgets.Count, tmp.Count - widgets.Count);
                    m_loadedPages = tmp.ToArray();
                }
                catch (Exception)
                {
                    tabControl1.TabPages.Clear();
                    foreach (PageRecord pgrec in m_loadedPages) pgrec.Dispose();
                    m_loadedPages = new PageRecord[] { };
                    UpdateTabs();
                }
            }
            finally
            {
                m_updatingTabs = false;
                tabControl1.Enabled = true;
            }
            LoadCurPage();
        }

        TabPage FindPage(IWidget newwidget, int start)
        {
            for (int i = start; i < Math.Min(tabControl1.TabPages.Count, m_loadedPages.Length); i++)
            {
                TabPage pg = tabControl1.TabPages[i];
                if (m_loadedPages[i].widget == null || m_loadedPages[i].widget.CanReplaceWith(newwidget)) return pg;
            }
            return null;
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_updatingTabs) return;
            LoadCurPage();
        }

        private AppObject SelectedAppObject
        {
            get { return m_selectedObject != null ? m_selectedObject.GetFirstValidAppObject() : null; }
        }

        private void LoadCurPage()
        {
            int index = tabControl1.SelectedIndex;
            if (index >= 0 && index < m_loadedPages.Length && !m_loadedPages[index].isloaded)
            {
                if (m_loadedPages[index].ctrl == null)
                {
                    var ctrl = m_loadedPages[index].widget.GetControl();
                    tabControl1.TabPages[index].Controls.Add(ctrl);
                    ctrl.Dock = DockStyle.Fill;
                    m_loadedPages[index].ctrl = ctrl;
                }

                try
                {
                    m_loadedPages[index].widget.LoadWidgetData(SelectedAppObject);
                }
                catch (Exception e)
                {
                    Errors.Report(e);
                }

                m_loadedPages[index].isloaded = true;
            }
        }

        public void RefreshDashboard()
        {
            var cache = m_selectedObject.FindDatabaseCache();
            if (cache != null) cache.Clear();
            UpdateTabs();
        }

        public void SuspendDashboard()
        {
            m_suspendedDashboard = true;
        }
        public void ResumeDashboard()
        {
            if (m_suspendedDashboard)
            {
                m_suspendedDashboard = false;
                UpdateTabs();
            }
        }
    }

    public class TabbedDashboard : DashboardBase
    {
        public override bool SuitableFor(AppObject appobj)
        {
            return true;
        }

        //protected override void SetSelectedObject(AppObject obj)
        //{
        //}

        public override Control CreateControl(DashboardInstanceParams pars)
        {
            return new TabbedDashboardFrame();
        }

        //public ITreeNode SelectedNode
        //{
        //    get { CreateControl(); return m_frame.SelectedObject; }
        //    set { CreateControl(); m_frame.SelectedObject = value; }
        //}

        [Browsable(false)]
        public override int Priority { get { return -5; } }
    }
}
