using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace DatAdmin
{
    public partial class SummaryContentFrame : ContentFrame
    {
        ITreeNode m_selectedObject;
        int hi0;
        TabbedDashboard m_tabbedDashboard;
        DatAdminInfoDashboard m_infoDashboard;
        List<DashboardBase> m_dashboards = new List<DashboardBase>();
        // if user want to see dashboard <value> instead of dashboard <key>
        Dictionary<DashboardBase, DashboardBase> m_userWantToSee = new Dictionary<DashboardBase, DashboardBase>();
        Dictionary<DashboardBase, Control> m_dashboardControls = new Dictionary<DashboardBase, Control>();
        DisposeList m_titleBarLabels = new DisposeList();


        DashboardBase m_shownDashboard;
        bool m_machineChangeDashboard = true;

        public SummaryContentFrame()
        {
            InitializeComponent();
            ConnPack.Cache = CachePack.TreeCache;
            hi0 = panel1.Height;
            m_tabbedDashboard = new TabbedDashboard();
            m_infoDashboard = new DatAdminInfoDashboard();

            cbxDashboardType.Items.Add(VersionInfo.ProgramTitle);
            m_dashboards.Add(m_infoDashboard);
            m_dashboards.Add(m_tabbedDashboard);
            cbxDashboardType.SelectedIndex = 0;
            m_machineChangeDashboard = false;

            if (!CustomDashboardsFeature.Allowed)
            {
                btnMore.Enabled = false;
                btnMore.ToolTipText = Texts.Get("s_professional_edition_required");
            }
        }

        public override string PageTitle { get { return "s_summary"; } }
        public override string PageTypeTitle { get { return "s_summary"; } }

        public override Bitmap Image
        {
            get { return CoreIcons.summary; }
        }

        public ITreeNode SelectedObject
        {
            get { return m_selectedObject; }
            set
            {
                ChangeDependencySource(value);
            }
        }

        protected override void LoadFromDependencySource(object val)
        {
            var value = (ITreeNode)val;
            if (m_selectedObject != null) m_selectedObject.ChangedProperties -= new EventHandler(m_selectedObject_ChangedProperties);
            m_selectedObject = value;
            if (m_selectedObject != null) m_selectedObject.ChangedProperties += new EventHandler(m_selectedObject_ChangedProperties);
            //m_tabbedDashboard.SelectedNode = value;

            try
            {
                SuspendLayout();

                foreach (var ctrl in m_dashboardControls.Values)
                {
                    var tab = ctrl as TabbedDashboardFrame;
                    if (tab != null) tab.SuspendDashboard();
                    var das = ctrl as DashboardFrame;
                    if (das != null) das.SuspendDashboard();
                }

                SendSelectedObject();
                UpdateDashboardList();
                UpdateToolbar();

                foreach (var ctrl in m_dashboardControls.Values)
                {
                    var tab = ctrl as TabbedDashboardFrame;
                    if (tab == null) continue;
                    tab.SelectedObject = value;
                }
            }
            finally
            {
                foreach (var ctrl in m_dashboardControls.Values)
                {
                    var tab = ctrl as TabbedDashboardFrame;
                    if (tab != null) tab.ResumeDashboard();
                    var das = ctrl as DashboardFrame;
                    if (das != null) das.ResumeDashboard();
                }

                ResumeLayout();
            }
        }

        private void SendSelectedObject()
        {
            var d = m_shownDashboard as DashboardBase;
            if (d != null && m_dashboardControls.ContainsKey(d) && !m_dashboardControls[d].IsDisposed)
            {
                var ctrl = m_dashboardControls[d] as DashboardFrame;
                if (ctrl != null) ctrl.SetSelectedObject(SelectedAppObject);
            }
        }

        void m_selectedObject_ChangedProperties(object sender, EventArgs e)
        {
            UpdateDashboardList();
        }

        public override void OnShowContent()
        {
            base.OnShowContent();
            UpdateDashboardList();
        }

        private void UpdateDashboardList()
        {
            if (!IsContentVisible) return;
            UpdateTitleBar();

            m_dashboards.Clear();
            cbxDashboardType.Items.Clear();

            cbxDashboardType.Items.Add(VersionInfo.ProgramTitle);
            m_dashboards.Add(m_infoDashboard);

            if (m_selectedObject != null && m_selectedObject.GetAllWidgets().Count > 0)
            {
                cbxDashboardType.Items.Add(Texts.Get("s_tabs"));
                m_dashboards.Add(m_tabbedDashboard);
            }

            if (m_selectedObject != null)
            {
                var appobj = m_selectedObject.GetPrimaryAppObject();
                if (appobj != null)
                {
                    AppObject aclone;
                    if (appobj.SupportSerialize)
                    {
                        aclone = appobj.CloneUsingXml();
                        aclone.ConnPack = ConnPack;
                    }
                    else
                    {
                        aclone = appobj;
                    }
                    foreach (var item in DashboardManager.Instance.GetDashboards(aclone))
                    {
                        m_dashboards.Add(item);
                        cbxDashboardType.Items.Add(item);
                    }
                }
            }

            DashboardBase maxdash = m_dashboards.MaxKey(d => d.Priority);
            if (m_userWantToSee.ContainsKey(maxdash)) maxdash = m_userWantToSee[maxdash];

            m_machineChangeDashboard = true;
            cbxDashboardType.SelectedIndex = m_dashboards.IndexOf(maxdash);
            m_machineChangeDashboard = false;
        }

        private void UpdateTitleBar()
        {
            lbtitle.Text = m_selectedObject != null ? Texts.Get(m_selectedObject.Title) : "???";
            lbtype.Text = (m_selectedObject != null ? Texts.Get(m_selectedObject.TypeTitle) : "???") + ":";
            lbtitle.Left = lbtype.Left + lbtype.Width + 10;
            pictureBox1.Image = m_selectedObject != null ? m_selectedObject.Image : null;

            ITreeNode act = m_selectedObject;
            var pth = new List<ITreeNode>();
            m_titleBarLabels.DisposeAndClear();
            while (act != null && act.Parent != null)
            {
                pth.Insert(0, act.Parent);
                act = act.Parent;
            }
            if (pth.Count == 0)
            {
                panel1.Height = hi0 - labPathRoot.Height;
                labPathRoot.Text = "";
            }
            else
            {
                labPathRoot.Text = Texts.Get(pth[0].Title);
                labPathRoot.Tag = pth[0].Path;
                labPathRoot.Click += labPathRoot_Click;

                const int LABDIST = 5;

                Label lastlab = labPathRoot;
                for (int i = 1; i < pth.Count; i++)
                {
                    var labsep = new Label();
                    labsep.Text = ">";
                    labPathRoot.Parent.Controls.Add(labsep);
                    labsep.Left = lastlab.Right + LABDIST;
                    labsep.Top = labPathRoot.Top;
                    labsep.Font = labPathRoot.Font;
                    labsep.AutoSize = true;
                    m_titleBarLabels.Add(labsep);
                    lastlab = labsep;

                    var labnode = new LinkLabel();
                    labnode.Text = Texts.Get(pth[i].Title);
                    labnode.Tag = pth[i].Path;
                    labPathRoot.Parent.Controls.Add(labnode);
                    labnode.Left = lastlab.Right + LABDIST;
                    labnode.Top = labPathRoot.Top;
                    labnode.Click += labPathRoot_Click;
                    labnode.Font = labPathRoot.Font;
                    labnode.AutoSize = true;
                    m_titleBarLabels.Add(labnode);
                    lastlab = labnode;
                }

                panel1.Height = hi0;
            }
        }

        void labPathRoot_Click(object sender, EventArgs e)
        {
            var lab = (LinkLabel)sender;
            HTree.CallSelectNode(lab.Tag.ToString(), SelectNodeFlags.FocusTree | SelectNodeFlags.ScrollInView);
        }

        private void UpdateToolbar()
        {
            if (m_selectedObject != null)
            {
                MenuBuilder mb = new MenuBuilder();
                mb.IgnoreShortcuts = true;
                m_selectedObject.GetPopupMenu(mb);
                toolStrip1.Items.Clear();
                mb.RemoveItem("s_refresh");
                mb.RemoveItem("s_refresh_this_object");
                mb.AddItem("s_refresh", RefreshCurrentDashboard, CoreIcons.refresh, MenuWeights.REFRESH);
                mb.GetMenuItems(toolStrip1.Items);
            }
        }

        private void cbxDashboardType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DashboardBase maxdash = m_dashboards.MaxKey(d => d.Priority);
            DashboardBase current = null;
            if (cbxDashboardType.SelectedIndex >= 0) current = m_dashboards[cbxDashboardType.SelectedIndex];
            if (current == m_shownDashboard) return;
            ShowDashboard(m_shownDashboard, false);
            ShowDashboard(current, true);
            m_shownDashboard = current;
            toolStripDashboard.Visible = m_shownDashboard is DockPanelDashboard;
            UpdateRevertMenu();
            //btnOpen.Visible = btnMore.Visible = m_shownDashboard is DockPanelDashboard && CustomDashboardsFeature.Allowed;
            //if (m_shownDashboard is DockPanelDashboard)
            //{
            //    var frm = ((DockPanelDashboard)m_shownDashboard).DesignFrame;
            //    btnMore.Checked = frm != null && frm == _GetDashboardControl(m_shownDashboard);
            //    //((DockPanelDashboard)m_shownDashboard).EnableDesign(btnDesign.Checked);
            //}
            if (m_shownDashboard != null && !m_machineChangeDashboard) m_userWantToSee[maxdash] = m_shownDashboard;
            SendSelectedObject();
            toolStrip1.Visible = m_shownDashboard.Caps.ShowNodeToolbar;
        }

        private void UpdateRevertMenu()
        {
            if (!toolStripDashboard.Visible) return;
            var dash = _GetDashboardControl(m_shownDashboard) as DashboardFrame;
            if (dash == null) return;
            dash.UpdateRevertMenu(btnRevert);
        }

        private Control _GetDashboardControl(DashboardBase dab)
        {
            if (!m_dashboardControls.ContainsKey(dab) || m_dashboardControls[dab].IsDisposed)
            {
                return null;
            }
            return m_dashboardControls[dab];
        }

        private Control GetDashboardControl(DashboardBase dab)
        {
            if (!m_dashboardControls.ContainsKey(dab) || m_dashboardControls[dab].IsDisposed)
            {
                var pars = new DashboardInstanceParams { LayoutName = ".summary" };
                var ctrl = dab.CreateControl(pars);
                var cc = ctrl as IConnectionPackHolder;
                if (cc != null) cc.ConnPack = ConnPack;
                var das = ctrl as DashboardFrame;
                if (das != null)
                {
                    das.HideToolbar = true;
                    das.ChangedWidgetSet += das_ChangedWidgetSet;
                }
                m_dashboardControls[dab] = ctrl;
            }
            return m_dashboardControls[dab];
        }

        void das_ChangedWidgetSet(object sender, EventArgs e)
        {
            UpdateRevertMenu();
        }

        private void ShowDashboard(DashboardBase dab, bool visible)
        {
            if (dab == null) return;
            Control ctrl = GetDashboardControl(dab);
            if (ctrl != null)
            {
                ctrl.Visible = visible;
                if (!Controls.Contains(ctrl))
                {
                    Controls.Add(ctrl);
                    ctrl.Dock = DockStyle.Fill;
                }
                if (visible) ctrl.BringToFront();
            }
            var cnt = ctrl as ContentFrame;
            if (cnt != null) cnt.IsContentVisible = visible;
        }

        private AppObject SelectedAppObject
        {
            get
            {
                if (m_selectedObject != null)
                {
                    return m_selectedObject.GetPrimaryAppObject();
                }
                return null;
            }
        }

        private void RefreshCurrentDashboard()
        {
            if (m_shownDashboard == null) return;
            var ctrl = m_dashboardControls.Get(m_shownDashboard);
            var da = ctrl as DashboardFrame;
            if (da != null) da.RefreshDashboard();
            var ta = ctrl as TabbedDashboardFrame;
            if (ta != null) ta.RefreshDashboard();
        }

        //[PopupMenu("s_save", ImageName = CoreIcons.saveName)]
        //public void SaveDashboard()
        //{
        //    var dp = m_shownDashboard as DockPanelDashboard;
        //    if (dp != null)
        //    {
        //        dp.SaveToFile(dp.AddonFileName);
        //    }
        //}

        //private void btnDesign_Click(object sender, EventArgs e)
        //{
        //    var dap = m_shownDashboard as DockPanelDashboard;
        //    if (dap == null) return;
        //    contextMenuStrip1.Items.Clear();
        //    var mb = new MenuBuilder(contextMenuStrip1.Items);
        //    mb.AddObject(this);
        //    var appobj = SelectedAppObject;
        //    if (appobj != null)
        //    {
        //        appobj.GetAddWidgetMenu(mb, dap, ConnPack);
        //    }
        //    contextMenuStrip1.Show(btnDesign, new Point(0, btnDesign.Height));
        //}

        private void btnMore_CheckedChanged(object sender, EventArgs e)
        {
            var dp = m_shownDashboard as DockPanelDashboard;
            if (dp != null)
            {
                var win = GetDashboardControl(dp) as DashboardFrame;
                win.HideToolbar = !btnMore.Checked;
                //dp.SetDesignFrame(btnMore.Checked ? win : null);
                //dp.EnableDesign(btnDesign.Checked);
                //if (btnDesign.Checked)
                //{
                //    MainWindow.Instance.ShowDocker(new ToolboxDockerFactory());
                //}
            }
            toolStrip1.Visible = !btnMore.Checked;
            btnMore.Image = btnMore.Checked ? CoreIcons.less_up : CoreIcons.more_down;
            btnMore.Text = Texts.Get(btnMore.Checked ? "s_less" : "s_more");
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            var das = m_shownDashboard as DockPanelDashboard;
            if (das == null) return;
            das.OpenAsNewWindow(SelectedAppObject);
        }

        public override void OnClose()
        {
            base.OnClose();
            foreach (var ctrl in m_dashboardControls.Values)
            {
                var cnt = ctrl as ContentFrame;
                if (cnt != null) cnt.OnClose();
                var das = ctrl as DashboardFrame;
                if (das != null) das.ChangedWidgetSet += das_ChangedWidgetSet;
            }
        }

        private void toolStripDashboard_Resize(object sender, EventArgs e)
        {
            cbxDashboardType.Width = toolStripDashboard.Width;
            cbxDashboardType.Left = toolStripDashboard.Left;
        }

        //private void panel1_Paint(object sender, PaintEventArgs e)
        //{
        //    var c1 = Color.FromArgb(220, 255, 255);
        //    var c2 = Color.FromArgb(220, 220, 220);
        //    using (var br = new LinearGradientBrush(panel1.Bounds, c1, c2, System.Drawing.Drawing2D.LinearGradientMode.Vertical))
        //    {
        //        e.Graphics.FillRectangle(br, panel1.Bounds);
        //    }
        //}
    }
}

