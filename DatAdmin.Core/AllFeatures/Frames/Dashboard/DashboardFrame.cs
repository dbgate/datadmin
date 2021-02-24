using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using WeifenLuo.WinFormsUI.Docking;
using System.Xml;
using IP.Components;

namespace DatAdmin
{
    public partial class DashboardFrame : ContentFrame, IToolboxGenerator
    {
        IDockContent m_lastActiveContent;
        IWidget m_widgetInPropertyGrid;
        //IWidget m_draggingWidget;
        string m_privateLayoutFile;
        bool m_enabledDesign;
        bool m_suspendedDashboard;

        class Item
        {
            internal WidgetBaseFrame Frame;
            [XmlSubElem]
            public IWidget Widget { get; set; }
            [XmlElem]
            public string PersistString { get; set; }

            internal void BeforeSave()
            {
                if (Frame != null) Frame.BeforeSave();
            }
        }

        AppObject m_appobj;

        DockPanelDashboard m_dashboard;
        List<Item> m_widgets = new List<Item>();
        public DashboardFrame(DockPanelDashboard dashboard, DashboardInstanceParams pars)
        {
            InitializeComponent();
            ConnPack.Cache = CachePack.TreeCache;
            m_dashboard = dashboard;
            HDesigner.UseToolBoxItem += HToolbox_UseToolBoxItem;
            HDesigner.ChangedWidget += HDesigner_ChangedWidget;
            if (pars.LayoutName != null)
            {
                m_privateLayoutFile = Path.Combine(m_dashboard.PrivateLayoutDirectory, pars.LayoutName + ".dly");
            }
            Disposed += DashboardFrame_Disposed;
            btnDesign.Visible = btnSettings.Visible = CustomDashboardsFeature.Allowed;
        }

        void HDesigner_ChangedWidget(IWidget obj, HDesigner.WidgetPart part)
        {
            if (m_widgetInPropertyGrid != obj) return;
            var dock = FindDocker(obj) as IDockWrapper;
            if (dock != null)
            {
                if ((part & HDesigner.WidgetPart.Title) != 0) dock.UpdateTitle();
                if ((part & HDesigner.WidgetPart.Icon) != 0) dock.UpdateIcon();
                if ((part & HDesigner.WidgetPart.Data) != 0) obj.LoadWidgetData(m_appobj);
            }
            dockPanel1.Refresh();
        }

        void HToolbox_UseToolBoxItem(IToolboxItem obj)
        {
            if (!EnabledDesign) return;
            if (!IsContentVisible) return;
            var wt = obj as WidgetToolboxItem;
            if (wt == null) return;
            AddWidget(wt.CreateWidget());
        }

        public event EventHandler ChangedWidgetSet;
        private void OnChangedWidgetSet()
        {
            if (ChangedWidgetSet != null) ChangedWidgetSet(this, EventArgs.Empty);
            UpdateRevertMenu(btnRevert);
        }

        protected override void OnChangeConnPack()
        {
            base.OnChangeConnPack();
            foreach (var widget in m_widgets)
            {
                widget.Widget.ConnPack = ConnPack;
                if (widget.Frame != null)
                {
                    widget.Frame.ConnPack = ConnPack;
                }
            }
        }

        void DashboardFrame_Disposed(object sender, EventArgs e)
        {
            HDesigner.UseToolBoxItem -= HToolbox_UseToolBoxItem;
            HDesigner.ChangedWidget -= HDesigner_ChangedWidget;
        }

        public override void OnClose()
        {
            base.OnClose();
            SavePrivateLayout();
        }

        public void AddWidget(IWidget widget)
        {
            AddWidget(widget, DockState.Document, null);
        }

        public void AddWidget(IWidget widget, WeifenLuo.WinFormsUI.Docking.DockState dockstate, string persistString)
        {
            var ctrl = widget.GetControl();
            widget.IsDesigning = EnabledDesign;
            ctrl.ConnPack = ConnPack;
            string ps = persistString ?? Guid.NewGuid().ToString();
            var docker = new ContentWrapper(ctrl, ps);
            docker.Show(dockPanel1, dockstate);
            //docker.ClosedEvent += new EventHandler(docker_ClosedEvent);
            docker.FormClosed += docker_ClosedEvent;
            m_widgets.Add(new Item
            {
                Frame = ctrl,
                Widget = widget,
                PersistString = ps
            });
            if (m_appobj != null) widget.LoadWidgetData(m_appobj);
            OnChangedWidgetSet();
        }

        void docker_ClosedEvent(object sender, EventArgs e)
        {
            var docker = (ContentWrapper)sender;
            m_widgets.RemoveIf(item => item.PersistString == docker.PersistString);
            OnChangedWidgetSet();
        }

        public void SetSelectedObject(AppObject obj)
        {
            m_appobj = obj;

            if (m_suspendedDashboard) return;

            foreach (var item in m_widgets)
            {
                item.Widget.LoadWidgetData(obj);
            }

            UpdateEnablingAndToolbars();
        }

        private void ShowWidgetProperties(IWidget widget)
        {
            bool value = EnabledDesign && IsContentVisible;
            m_widgetInPropertyGrid = value ? widget : null;
            HDesigner.CallShowProperties(m_widgetInPropertyGrid);
        }

        private void UpdateToolbarVisibility()
        {
            toolStrip1.Visible = !EnabledDesign && !HideToolbar;
            toolStripDesign.Visible = EnabledDesign && !HideToolbar;
            if (toolStrip1.Visible) toolStrip1.SendToBack();
            if (toolStripDesign.Visible) toolStripDesign.SendToBack();
        }

        private void UpdateEnablingAndToolbars()
        {
            UpdateToolbarVisibility();
            bool value = EnabledDesign && IsContentVisible;
            //btnShowProperties.Enabled = btnSaveDesign.Enabled = btnShowToolbox.Enabled = btnSettings.Enabled = EnabledDesign;
            HDesigner.CallShowInToolbox(value ? this : null);
            if (value && IsContentVisible) ShowWidgetProperties(SelectedWidget);
            MainWindow.Instance.UpdateFrameEnabling(this);
        }

        public override void OnHideContent()
        {
            base.OnHideContent();
        }

        public override void OnShowContent()
        {
            base.OnShowContent();
        }

        public void SaveLayout(DockingDesign design)
        {
            design.Clear();
            SaveLayout(design._Root);
        }

        public bool LoadLayoutOverride()
        {
            if (m_privateLayoutFile != null && File.Exists(m_privateLayoutFile))
            {
                var layout = new DockingDesign();
                layout.LoadFromFile(m_privateLayoutFile);
                LoadLayout(layout);
                return true;
            }
            return false;
        }

        public void LoadLayoutOrOverride(DockingDesign design)
        {
            if (!LoadLayoutOverride())
            {
                LoadLayout(design);
            }
        }

        public void LoadLayout(DockingDesign design)
        {
            LoadLayout(design._Root);
        }

        private void SaveLayout(XmlElement xml)
        {
            var ms = new MemoryStream();
            dockPanel1.SaveAsXml(ms, Encoding.UTF8);
            var ms2 = new MemoryStream(ms.ToArray());
            var doc = new XmlDocument();
            doc.Load(ms2);
            xml.AddChild("Layout").AppendChild(xml.OwnerDocument.ImportNode(doc.DocumentElement, true));
            foreach (var item in m_widgets)
            {
                item.SaveProperties(xml.AddChild("Widget"));
            }
        }

        private IDockContent GetContentFromPersistString(string persistString)
        {
            foreach (var item in m_widgets)
            {
                if (item.PersistString == persistString)
                {
                    var cnt = new ContentWrapper(item.Frame, item.PersistString);
                    //cnt.ClosedEvent += docker_ClosedEvent;
                    cnt.FormClosed += docker_ClosedEvent;
                    return cnt;
                }
            }
            return null;
        }

        private void LoadLayout(XmlElement xml)
        {
            ClearPanels();
            foreach (XmlElement xi in xml.SelectNodes("Widget"))
            {
                try
                {
                    var item = new Item();
                    item.LoadProperties(xi);
                    var ctrl = item.Widget.GetControl();
                    item.Frame = ctrl;
                    ctrl.ConnPack = ConnPack;
                    m_widgets.Add(item);
                    item.Widget.IsDesigning = EnabledDesign;
                }
                catch (Exception err)
                {
                    Errors.ReportSilent(err);
                }
            }
            var doc = new XmlDocument();
            var layx = xml.FindElement("Layout");
            if (layx != null)
            {
                doc.AppendChild(doc.ImportNode(layx, true));
                var ms = new MemoryStream();
                doc.Save(ms);
                ms.Position = 0;
                dockPanel1.LoadFromXml(ms, GetContentFromPersistString);
            }
            OnChangedWidgetSet();
        }

        private void tbnShowToolbox_Click(object sender, EventArgs e)
        {
            MainWindow.Instance.ShowDocker(new ToolboxDockerFactory());
        }

        #region IToolboxGenerator Members

        public ToolboxItemCollection GetToolbox()
        {
            var res = new ToolboxItemCollection();
            if (m_appobj != null)
            {
                var lst = new List<IWidget>();
                m_appobj.GetWidgetsEx(lst, ConnPack);
                foreach (var wid in lst)
                {
                    res.Add(new WidgetToolboxItem(wid) { CategoryOverride = "Current object" });
                }
                foreach (var holder in WidgetAddonType.Instance.CommonSpace.GetAllAddons())
                {
                    var wid = (IWidget)holder.InstanceModel;
                    res.Add(new WidgetToolboxItem(wid));
                }
            }
            return res;
        }

        #endregion

        public void SetEnableDesignFlag(bool value)
        {
            if (value == EnabledDesign) return;
            m_enabledDesign = value;
            UpdateEnablingAndToolbars();
            foreach (var widget in m_widgets)
            {
                widget.Widget.IsDesigning = value;
            }
        }

        private IWidget ExtractWidget(IDockContent cnt)
        {
            var wrap = cnt as ContentWrapper;
            if (wrap == null) return null;
            var wfrm = wrap.Frame as WidgetBaseFrame;
            if (wfrm == null) return null;
            return wfrm.Widget;
        }

        private IWidget SelectedWidget
        {
            get
            {
                return ExtractWidget(m_lastActiveContent);
            }
        }

        private void dockPanel1_ActiveContentChanged(object sender, EventArgs e)
        {
            if (dockPanel1.ActiveContent != null) m_lastActiveContent = dockPanel1.ActiveContent;
            if (IsContentVisible) ShowWidgetProperties(SelectedWidget);
        }

        private IDockContent FindDocker(IWidget widget)
        {
            foreach (var cnt in dockPanel1.Contents)
            {
                if (ExtractWidget(cnt) == widget) return cnt;
            }
            return null;
        }

        private void btnShowProperties_Click(object sender, EventArgs e)
        {
            MainWindow.Instance.ShowDocker(new PropertiesDockerFactory());
        }

        private void RevertDesign()
        {
            LoadLayout(m_dashboard.m_design);
            SetSelectedObject(m_appobj);
        }

        private void SavePrivateLayout()
        {
            if (m_privateLayoutFile != null)
            {
                var design = new DockingDesign();
                SaveLayout(design);
                design.SaveToFile(m_privateLayoutFile);
            }
        }

        public void ClearPanels()
        {
            m_widgets.Clear();
            while (dockPanel1.Contents.Count > 0) dockPanel1.Contents[0].DockHandler.Close();
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            var win = new DashboardEditorFrame(m_dashboard);
            win.ShowGenericDialog(Texts.Get("s_dashboard") + " - " + Path.GetFileName(m_dashboard.AddonFileName), GenericDialogType.Close);
            if (m_dashboard.IsInCfgDirectory()) m_dashboard.SaveToFile(m_dashboard.AddonFileName);
        }

        public override string MenuBarTitle
        {
            get { return "s_dashboard"; }
        }

        public bool EnabledDesign { get { return m_enabledDesign; } }

        [PopupMenuVisible("s_enable_design")]
        public bool MenuEnableDesignVisible()
        {
            return !EnabledDesign;
        }

        [PopupMenu("s_enable_design")]
        public void CallEnableDesign()
        {
            m_dashboard.SetDesignFrame(this);
        }

        [PopupMenuVisible("s_disable_design")]
        public bool MenuDisableDesignVisible()
        {
            return EnabledDesign;
        }

        [PopupMenu("s_disable_design")]
        public void CallDisableDesign()
        {
            if (m_dashboard.DesignFrame == this) m_dashboard.SetDesignFrame(null);
        }

        public override string PageTitle
        {
            get { return m_appobj.ToString() + " - " + Path.GetFileNameWithoutExtension(m_dashboard.AddonFileName); }
        }

        public override Bitmap Image
        {
            get { return CoreIcons.dashboard; }
        }

        public override bool SupportsSave
        {
            get { return EnabledDesign; }
        }

        public override bool Save()
        {
            foreach (var item in m_widgets)
            {
                item.BeforeSave();
            }
            SaveLayout(m_dashboard.m_design);
            m_dashboard.SaveToFile();
            m_dashboard.ClearPrivateLayouts();
            return true;
        }

        bool m_hideToolbar;
        public bool HideToolbar
        {
            get { return m_hideToolbar; }
            set
            {
                m_hideToolbar = value;
                UpdateToolbarVisibility();
            }
        }

        private void btnDesign_Click(object sender, EventArgs e)
        {
            CallEnableDesign();
            if (EnabledDesign) RevertDesign();
        }

        private void btnAddToFavorites_Click(object sender, EventArgs e)
        {
            var fav = new OpenDashboardFavorite
            {
                DashboardFile = Path.GetFileName(m_dashboard.AddonFileName),
                RelatedObject = m_appobj,
            };
            AddToFavoriteForm.Run(fav, m_appobj.ToString() + "-" + Path.GetFileNameWithoutExtension(fav.DashboardFile));
        }

        private void dockPanel1_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.None;
            var item = e.Data.GetData(typeof(HostToolbox.HostItem)) as HostToolbox.HostItem;
            if (item == null) return;
            var wt = item.Tag as WidgetToolboxItem;
            if (wt == null) return;
            e.Effect = DragDropEffects.Copy;
        }

        //private void dockPanel1_DragEnter(object sender, DragEventArgs e)
        //{

        //    Capture = true;
        //    Capture = false;
        //    wrap.Show(dockPanel1, DockState.Float);
        //    dockPanel1.BeginDrag(wrap.DockHandler);
        //}

        private void dockPanel1_DragDrop(object sender, DragEventArgs e)
        {
            var item = e.Data.GetData(typeof(HostToolbox.HostItem)) as HostToolbox.HostItem;
            if (item == null) return;
            var wt = item.Tag as WidgetToolboxItem;
            if (wt == null) return;

            var widget = wt.CreateWidget();
            AddWidget(widget, DockState.Document, null);
        }

        public void UpdateRevertMenu(ToolStripDropDownButton button)
        {
            var layout = new DockingDesign();
            SaveLayout(layout);
            var mwids = layout.GetMissingWidgets(m_dashboard.m_design);
            button.Text = Texts.Get("s_revert");
            if (mwids.Count > 0) button.Text += " (" + mwids.Count.ToString() + ")";
            button.DropDownItems.Clear();
            var mb = new MenuBuilder();
            mb.AddItem(null, "s_original_design", new MenuItemData { Weight = -1, GroupName = "revert", Callable = new ActionCallable(RevertDesign) });
            foreach (var wid in mwids)
            {
                mb.AddItem(null, wid.Widget.PageTitle, new MenuItemData { Weight = 1, GroupName = "widget", Callable = new ActionCallable(wid.GetOpenWidgetCallback(this)), Image = wid.Widget.Image });
            }
            mb.GetMenuItems(button.DropDownItems);
        }

        private void btnSaveCloseDesign_Click(object sender, EventArgs e)
        {
            Save();
            CallDisableDesign();
            RevertDesign();
        }

        private void btnCloseDesign_Click(object sender, EventArgs e)
        {
            CallDisableDesign();
            LoadLayoutOverride();
            SetSelectedObject(m_appobj);
        }

        public void RefreshDashboard()
        {
            var cache = m_appobj.FindDatabaseCache();
            if (cache != null) cache.Clear();
            SetSelectedObject(m_appobj);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshDashboard();
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
                if (IsContentVisible) SetSelectedObject(m_appobj);
            }
        }
    }
}
