using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.Common;
using System.IO;
using System.Threading;
using System.Runtime.InteropServices;
using System.Collections;
using WeifenLuo.WinFormsUI.Docking;
using System.Drawing.Imaging;

namespace DatAdmin
{
    public partial class MainForm : Form, IMainWindow
    {
        //bool m_closed = false;
        List<Action> m_idleWork = new List<Action>();
        //ITreeNode m_savedNode;
        //ViewCellDataForm m_viewCellWindow;
        //ObjectView m_objectView;
        //DataBrowser m_dataBrowser;
        //bool m_closing = false;
        Action m_autoRun;
        IInvoker m_invoker;
        //List<TabPage> m_openTabsHistory = new List<TabPage>();
        //bool m_removingContentTab = false;
        //ImageCache m_imgCache;
        //List<DockContent> m_loadingFrames = new List<DockContent>();
        List<ContentFrame> m_loadingFrames = new List<ContentFrame>();
        //string m_buyUrl;
        //bool m_bottomPanelAutoCollapsed;

        Dictionary<string, DockerWrapper> m_dockerWraps = new Dictionary<string, DockerWrapper>();
        List<IDockerFactory> m_dockerFacts = new List<IDockerFactory>();
        List<IDockContent> m_openTabsHistory = new List<IDockContent>();

        //TreeDocker m_dataTree;
        //TreeDocker m_addonsTree;
        //LogDocker m_logDocker;
        List<Icon> m_loadingIcons = new List<Icon>();
        List<Bitmap> m_loadingBitmaps = new List<Bitmap>();
        bool m_loadingLayouts;
        string[] m_cmdlineArgs;

        public MainForm(Action autoRun, string[] cmdlineArgs)
        {
            m_autoRun = autoRun;
            m_cmdlineArgs = cmdlineArgs;
            //m_buyUrl = RegNowTool.GetBuyURL();
            InitializeComponent();

            foreach (var holder in DockerFactoryAddonType.Instance.CommonSpace.GetAllAddons())
            {
                var f = (IDockerFactory)holder.CreateInstance();
                m_dockerFacts.Add(f);

                var menu = new ToolStripMenuItem();
                menu.Text = Texts.Get(f.MenuTitle);
                menu.Image = f.Icon;
                menu.ShortcutKeys = f.Shortcut;
                menu.Tag = f;
                menu.Click += new EventHandler(dockerView_Click);
                mnuView.DropDownItems.Add(menu);
            }

            var mnuCellData = new ToolStripMenuItem();
            mnuCellData.Text = Texts.Get("s_cell_data");
            mnuCellData.Image = CoreIcons.data;
            mnuView.DropDownItems.Add(mnuCellData);

            foreach (var holder in CellDataEditorAddonType.Instance.CommonSpace.GetAllAddons())
            {
                var c = (ICellDataEditor)holder.CreateInstance();
                var f = new CellDataDockerFactory(c);
                m_dockerFacts.Add(f);

                var menu = new ToolStripMenuItem();
                menu.Text = Texts.Get(c.MenuTitle);
                menu.Image = c.Icon;
                menu.ShortcutKeys = c.Shortcut;
                menu.Tag = f;
                menu.Click += new EventHandler(dockerView_Click);
                mnuCellData.DropDownItems.Add(menu);
            }

            //m_dataTree = new TreeDocker(AfterDeletedNode, "data:");
            //m_addonsTree = new TreeDocker(AfterDeletedNode, "addons:");

            //treAddonsTree.TreeBehaviour.AfterDeletedNode = AfterDeletedNode;
            //treDataTree.TreeBehaviour.AfterDeletedNode = AfterDeletedNode;
            MainWindow.Instance = this;
            m_invoker = new ControlInvoker(this);

            AddLoadingIcon(CoreIcons.busy01);
            AddLoadingIcon(CoreIcons.busy02);
            AddLoadingIcon(CoreIcons.busy03);
            AddLoadingIcon(CoreIcons.busy04);
            AddLoadingIcon(CoreIcons.busy05);
            AddLoadingIcon(CoreIcons.busy06);
            AddLoadingIcon(CoreIcons.busy07);
            AddLoadingIcon(CoreIcons.busy08);

            //treDataTree.RootPath = "data:";
            //treAddonsTree.RootPath = "addons:";
            Translating.TranslateControl(this);
            GlobalSettings.OnChange += ReloadSettings;
            //messageLogFrame1.Source = m_mainWinLog;

            Application.Idle += new EventHandler(Application_Idle);
            //m_viewCellWindow = new ViewCellDataForm();
            //if (Registration.Instance == null) RegisterForm.Run();
            UpdateTitle();
            //ChangedContent();
            RunInMainWindow(FocusTreeViewOnStartup);
            //CreateFixedContent();
            ReloadSettings();
            mnuRecordMacro.Visible = VersionInfo.IsDevVersion;
            mnuDebug.Visible = VersionInfo.IsDevVersion;

            timAfterStart.Enabled = true;

            ExtendMenus();

            btnBuyNow.Visible = mnuBuyNow.Visible = !LicenseTool.HidePurchaseLinks();

            foreach (LangInfo lang in LangManager.Languages)
            {
                ToolStripMenuItem mnu = (ToolStripMenuItem)mnuLanguage.DropDownItems.Add(lang.Name);
                mnu.Tag = lang;
                mnu.Click += mnuLang_Click;
                mnu.Checked = Texts.Language == lang.Identifier;
            }

            HDocker.ClosedDocker += ClosedDocker;

            ReloadExtendableToolbar();

            ReloadFavorites();
            HFavorites.Changed += ReloadFavorites;
            RefreshSettings();

            ReloadLayouts();

            ProcessArgs(m_cmdlineArgs);

            Icon = Icon.FromHandle(Icons.mainicon.GetHicon());

            mnuDashboardManager.Visible = CustomDashboardsFeature.Allowed;
            mnuTablePerspectives.Visible = AdvancedPerspectivesFeature.Allowed;
            HLicense.ChangedLicenses += new Action(HLicense_ChangedLicenses);

            if (Settings.Default.ScreenAreas == GetScreenAreas() && Settings.Default.WindowPositionStored)
            {
                Location = Settings.Default.WindowPosition;
                Size = Settings.Default.WindowSize;
                StartPosition = FormStartPosition.Manual;
                WindowState = Settings.Default.WindowState;
            }
        }

        private string GetScreenAreas()
        {
            var sb = new StringBuilder();
            foreach (Screen s in Screen.AllScreens)
            {
                sb.Append(s.WorkingArea);
            }
            return sb.ToString();
        }

        void HLicense_ChangedLicenses()
        {
            ReloadExtendableToolbar();
            ExtendMenus();
        }

        private void ExtendMenus()
        {
            ExtendMenu(mnuTools, "tools");
            ExtendMenu(mnuOptions, "options");
            ExtendMenu(mnuHelp, "help");
            ExtendMenu(mnuFile, "file");
            ExtendMenu(mnuView, "view");
            ExtendMenu(mnuWindow, "window");
        }

        List<ToolStripItem> m_extendableToolbarItems = new List<ToolStripItem>();
        private void ReloadExtendableToolbar()
        {
            foreach (var item in m_extendableToolbarItems) item.Dispose();
            m_extendableToolbarItems.Clear();
            foreach (var hold in MenuExtenderAddonType.Instance.CommonSpace.GetAllAddons())
            {
                var ext = hold.InstanceModel as IMenuExtender;
                ext.GetToolbarItems("main", m_extendableToolbarItems);
            }
            foreach (var item in m_extendableToolbarItems)
            {
                tstMain.Items.Add(item);
            }
        }

        private void ReloadLayouts()
        {
            m_loadingLayouts = true;
            string curitem = cbxCurrentLayout.SelectedItem.SafeToString() ?? "";
            cbxCurrentLayout.Items.Clear();
            cbxCurrentLayout.Items.Add("(" + Texts.Get("s_select") + ")");
            foreach (string file in Directory.GetFiles(Core.LayoutsDirectory))
            {
                cbxCurrentLayout.Items.Add(Path.GetFileNameWithoutExtension(file));
            }
            int idx = cbxCurrentLayout.Items.IndexOf(curitem);
            if (idx > 0) cbxCurrentLayout.SelectedIndex = idx;
            else cbxCurrentLayout.SelectedIndex = 0;
            m_loadingLayouts = false;
        }

        private void FillFavorites(ToolStripItemCollection items, FavoriteGroup group, Type itemType)
        {
            while (items.Count > 2) items.RemoveAt(2);
            foreach (var fav in group.GetItems())
            {
                ToolStripItem item = (ToolStripItem)itemType.CreateNewInstance();
                item.Image = fav.Favorite.Image;
                item.Text = fav.Name;
                item.Tag = fav;
                item.Click += new EventHandler(favitem_Click);
                item.MouseDown += new MouseEventHandler(favitem_MouseDown);
                items.Add(item);
            }
        }

        void favitem_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var item = (ToolStripItem)sender;
                var fav = (FavoriteHolder)item.Tag;
                var mb = new MenuBuilder();
                var appobj = new FavoriteAppObject { FileName = fav.File };
                mb.AddItem("s_delete", appobj.DeleteWithQueryVoid, CoreIcons.delete);
                mb.AddItem("s_rename", appobj.RenameWithQueryVoid, CoreIcons.rename);
                var menu = new ContextMenuStripEx();
                mb.GetMenuItems(menu.Items);
                menu.ShowOnCursor();
            }
        }

        void favitem_Click(object sender, EventArgs e)
        {
            var item = (ToolStripItem)sender;
            var fav = (FavoriteHolder)item.Tag;
            fav.Favorite.Open();
        }

        private void ReloadFavorites()
        {
            FillFavorites(toolStripFavorites.Items, Favorites.GroupByName("toolbar"), typeof(ToolStripButton));
            FillFavorites(mnuFavorites.DropDownItems, Favorites.GroupByName("menu"), typeof(ToolStripMenuItem));
            RefreshSettings();
        }

        void mnuLang_Click(object sender, EventArgs e)
        {
            var mnu = (ToolStripMenuItem)sender;
            var lang = (LangInfo)mnu.Tag;
            GlobalSettings.Pages.BeginEdit();
            GlobalSettings.Pages.General().Language = lang.Identifier;
            GlobalSettings.Pages.EndEdit();
            StdDialog.ShowInfo("s_restart_datadmin_for_changes");
            foreach (ToolStripMenuItem mi in mnuLanguage.DropDownItems)
            {
                mi.Checked = false;
            }
            mnu.Checked = true;
        }

        private void AddLoadingIcon(Bitmap bitmap)
        {
            m_loadingBitmaps.Add(bitmap);
            m_loadingIcons.Add(Icon.FromHandle(bitmap.GetHicon()));
        }

        private DockerWrapper CreateDockContent(IDockerFactory fact, out bool iscreated)
        {
            iscreated = false;
            if (!m_dockerWraps.ContainsKey(fact.GetPersistString()))
            {
                var wrap = new DockerWrapper(fact);
                m_dockerWraps[fact.GetPersistString()] = wrap;
                iscreated = true;
            }
            return m_dockerWraps[fact.GetPersistString()];
        }

        private IDockContent GetContentFromPersistString(string persistString)
        {
            foreach (var fact in m_dockerFacts)
            {
                if (fact.GetPersistString() == persistString)
                {
                    try
                    {
                        bool iscreated;
                        return CreateDockContent(fact, out iscreated);
                    }
                    catch (Exception err)
                    {
                        Errors.Report(err);
                        return null;
                    }
                }
            }
            return null;
        }

        void dockerView_Click(object sender, EventArgs e)
        {
            var menu = sender as ToolStripMenuItem;
            var fact = menu.Tag as IDockerFactory;
            ShowDocker(fact);
        }

        public void ShowDocker(IDockerFactory fact, ShowDockerPars pars)
        {
            bool iscreated;
            var content = CreateDockContent(fact, out iscreated);
            var state = content.DockState;
            if (state == DockState.Hidden || state == DockState.Unknown)
            {
                state = (DockState)fact.InitialState;
                iscreated = true;
            }
            if (pars.ModalLikeMode)
            {
                content.Show(dockPanel1, state);
                if (iscreated)
                {
                    content.SetCloseOnEsc();
                }
                else
                {
                    content.SetOnEscControl(pars.ModalParent);
                }
            }
            else
            {
                content.Show(dockPanel1, state);
            }
        }

        public void ShowDocker(IDockerFactory fact)
        {
            ShowDocker(fact, new ShowDockerPars());
        }

        Dictionary<string, List<ToolStripItem>> m_extendableMenus = new Dictionary<string, List<ToolStripItem>>();
        private void ExtendMenu(ToolStripMenuItem menu, string name)
        {
            if (!m_extendableMenus.ContainsKey(name)) m_extendableMenus[name] = new List<ToolStripItem>();
            foreach (var item in m_extendableMenus[name]) item.Dispose();
            m_extendableMenus[name].Clear();
            foreach (var hold in MenuExtenderAddonType.Instance.CommonSpace.GetAllAddons())
            {
                var ext = hold.InstanceModel as IMenuExtender;
                var mb = new MenuBuilder();
                ext.GetMainMenu(name, mb);
                mb.GetMenuItems(m_extendableMenus[name]);
            }
            foreach (var item in m_extendableMenus[name]) menu.DropDownItems.Add(item);
        }

        //void AfterDeletedNode(ITreeNode node)
        //{
        //    m_objectView.SelectedObject = null;
        //    m_dataBrowser.SelectedObject = null;
        //}

        //void CreateFixedContent()
        //{
        //    m_objectView = new ObjectView();
        //    m_dataBrowser = new DataBrowser();
        //    OpenContent(m_objectView);
        //    OpenContent(m_dataBrowser);
        //    m_dataBrowser.TabularData = null;
        //    m_dataBrowser.IsContentVisible = false;
        //    contentTabs.SelectedIndex = 0;
        //    m_objectView.IsContentVisible = true;
        //    lastContentFrame = m_objectView;
        //    ChangedContent();
        //}

        bool m_treeFocused = false;
        private void FocusTreeViewOnStartup()
        {
            if (!m_treeFocused)
            {
                //if (treDataTree.Visible) treDataTree.Focus();
            }
            m_treeFocused = true;
        }

        public void UpdateTitle()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(VersionInfo.ProgramTitle + " ");
            sb.Append(VersionInfo.VERSION);
            if (!String.IsNullOrEmpty(VersionInfo.VersionTypeName))
            {
                sb.Append(" ");
                sb.Append(VersionInfo.VersionTypeName);
            }
            string edtext = LicenseTool.EditionText();
            if (edtext != "") sb.Append(" " + edtext);
            if (!VersionInfo.DenyCustomLicenses)
            {
                if (!String.IsNullOrEmpty(LicenseTool.RegisteredToUser()))
                {
                    sb.Append(" [" + Texts.Get("s_registered_to$user", "user", LicenseTool.RegisteredToUser()) + "]");
                }
                else
                {
                    sb.Append(" [" + Texts.Get("s_unregistered") + "]");
                }
            }
            Text = sb.ToString();
        }

        void Application_Idle(object sender, EventArgs e)
        {
            List<Action> idle = new List<Action>();
            lock (m_idleWork)
            {
                idle.AddRange(m_idleWork);
                m_idleWork.Clear();
            }
            foreach (Action func in idle)
            {
                try
                {
                    func();
                }
                catch (Exception ex)
                {
                    Errors.Report(ex);
                }
            }
            if (m_autoRun != null && !timAutoRun.Enabled)
            {
                timAutoRun.Enabled = true;
            }
        }

        private void RefreshSettings()
        {
            toolStripFavorites.Visible = MainWindowSettings.Page.ShowFavoritesToolBar.GetVisibility(Favorites.GroupByName("toolbar"));
            mnuFavorites.Visible = MainWindowSettings.Page.ShowFavoritesMenu.GetVisibility(Favorites.GroupByName("menu"));
            tstMain.Visible = MainWindowSettings.Page.ShowToolBar;
        }

        void ReloadSettings()
        {
            HSettings.CallReloadSettings();
            RefreshSettings();
            //try
            //{
            //    m_mainWinLog.MinLogLevel = GlobalSettings.Pages.Log.WindowLogLevel;
            //    GlobalSettings.Pages.BeginEdit();
            //    DataViewerShowage dataShow = GlobalSettings.Pages.View.DataViewer;
            //    if (dataShow == DataViewerShowage.Tab)
            //    {
            //        GlobalSettings.Pages.View.ShowBottomToolPanel = true;
            //        splitContainer2.Panel2Collapsed = false;
            //        if (tbcBottomTabs.TabPages[0] != tabData)
            //        {
            //            tbcBottomTabs.TabPages.Insert(0, tabData);
            //            tbcBottomTabs.SelectedIndex = 0;
            //        }
            //    }
            //    else
            //    {
            //        if (tbcBottomTabs.TabPages[0] == tabData) tbcBottomTabs.TabPages.RemoveAt(0);
            //    }
            //    if (dataShow == DataViewerShowage.Window)
            //    {
            //        m_viewCellWindow.Show();
            //        mnuViewBottomToolPanel.Checked = true;
            //    }
            //    else
            //    {
            //        m_viewCellWindow.Hide();
            //    }
            //    mnuDataTab.Checked = dataShow == DataViewerShowage.Tab;
            //    mnuDataWindow.Checked = dataShow == DataViewerShowage.Window;
            //    mnuDataNone.Checked = dataShow == DataViewerShowage.None;

            //    mnuViewWindowList.Checked = GlobalSettings.Pages.View.ShowWindowList;
            //    mnuViewBottomToolPanel.Checked = GlobalSettings.Pages.View.ShowBottomToolPanel;
            //    splitContainer3.Panel2Collapsed = !GlobalSettings.Pages.View.ShowWindowList;
            //    splitContainer2.Panel2Collapsed = !GlobalSettings.Pages.View.ShowBottomToolPanel;
            //}
            //finally
            //{
            //    GlobalSettings.Pages.EndEdit();
            //}
        }

        public void OpenContent(ContentFrame frame, DocumentDockPosition position)
        {
            DockPane pane = null;
            if (dockPanel1.ActiveDocument != null) pane = dockPanel1.ActiveDocument.DockHandler.Pane;
            ContentWrapper docker = new ContentWrapper(frame);
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

        public void ShowContent(ContentFrame frame, bool visible)
        {
            if (visible)
            {
                var cnt = GetDockContent(frame);
                if (cnt != null) cnt.DockHandler.Show();
            }
            else
            {
                var cnt = GetDockContent(frame);
                if (cnt != null) cnt.DockHandler.Hide();
            }
        }

        public void ActivateContent(ContentFrame frame)
        {
            GetDockContent(frame).DockHandler.Activate();
        }

        public void OpenContent(ContentFrame frame)
        {
            OpenContent(frame, DocumentDockPosition.Center);
            //ReloadWindows();
            //ChangedContent();

            //TabPage page = new TabPage(Texts.Get(frame.PageTitle));
            //frame.IsUsedAsContent = true;
            //frame.Parent = page;
            //frame.Dock = DockStyle.Fill;
            //contentTabs.TabPages.Add(page);
            //contentTabs.SelectedTab = page;
            //page.ImageIndex = m_imgCache.GetImageIndex(frame.Image);
        }

        public ContentFrame GetCurrentContent() { return ActiveFrame; }

        private IDockContent GetDockContent(ContentFrame frame)
        {
            foreach (var doc in dockPanel1.Contents)
            {
                if (GetContentFrame(doc) == frame) return doc;
            }
            return null;
        }

        public void ActivateDocker(DockerBase docker)
        {
            foreach (var doc in dockPanel1.Contents)
            {
                var wrap = doc as DockerWrapper;
                if (wrap != null && wrap.Docker == docker) wrap.Activate();
            }
        }

        private ContentFrame GetContentFrame(IDockContent doc)
        {
            if (doc != null)
            {
                var cnt = doc as ContentWrapper;
                if (cnt != null) return cnt.Frame;
                var wrap = doc as DockerWrapper;
                if (wrap != null) return wrap.AsContent;
            }
            return null;
        }

        public ContentFrame ActiveFrame
        {
            get
            {
                var res = GetContentFrame(dockPanel1.ActiveContent);
                if (res != null) return res.ActiveFrame;
                res = GetContentFrame(dockPanel1.ActiveDocument);
                if (res != null) return res.ActiveFrame;
                return res;
            }
        }

        #region IWindowToolkit Members

        public Form Window { get { return this; } }

        public void RunInMainWindow(Action callback)
        {
            lock (m_idleWork)
            {
                m_idleWork.Add(callback);
            }
        }

        public void UpdateContentTitle(ContentFrame contentFrame)
        {
            var doc = GetDockContent(contentFrame) as IDockWrapper;
            if (doc != null) doc.UpdateTitle();
        }

        public void CloseContent(ContentFrame contentFrame)
        {
            var doc = GetDockContent(contentFrame);
            if (doc != null) doc.DockHandler.Close();
        }

        public void SetLoadingFrame(ContentFrame frame, bool isloading)
        {
            var dock = GetDockContent(frame) as DockContent;
            var dockw = dock as IDockWrapper;
            if (isloading)
            {
                if (!m_loadingFrames.Contains(frame) && frame.RequiresLoadingAnimation()) m_loadingFrames.Add(frame);
                if (dock != null) dock.Text = Texts.Get("s_loading") + " ...";
            }
            else
            {
                if (m_loadingFrames.Contains(frame))
                {
                    m_loadingFrames.Remove(frame);
                    frame.SetLoadingAnimationIcon(null, null);
                }
                if (dockw != null) dockw.UpdateTitle();
            }
            timLoadingFrame.Enabled = m_loadingFrames.Count > 0;
            //foreach (IDockWrapper wrap in dockPanel1.Contents)
            //{
            //    wrap.UpdateIcon();
            //}
            //dockPanel1.Refresh();
        }

        #endregion

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            HDocker.ClosedDocker -= ClosedDocker;
            foreach (var cntunknown in dockPanel1.Contents)
            {
                var cnt = cntunknown as IDockWrapper;
                if (cnt == null) continue;
                cnt.OnCloseWindow();
            }
            var thr = new Thread(StopInXSeconds);
            thr.IsBackground = true;
            thr.Start();
        }

        private static void StopInXSeconds()
        {
            Thread.Sleep(3 * 1000);
            Environment.Exit(0);
        }

        private void snewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateDialog dlg = new CreateDialog(null);
            dlg.ShowDialogEx();
            HTree.CallRefreshRoot();
        }

        //private Dictionary<string, IFileFormat> GetFormatsByExt()
        //{
        //    Dictionary<string, IFileFormat> fmt_by_ext = new Dictionary<string, IFileFormat>();
        //    foreach (AddonHolder item in FileFormatAddonType.Instance.CommonSpace.GetFilteredAddons(RegisterItemUsage.DirectUse))
        //    {
        //        IFileFormat fmt = (IFileFormat)item.InstanceModel;
        //        if (fmt.CanLoad)
        //        {
        //            fmt_by_ext[fmt.Extension] = fmt;
        //        }
        //    }
        //    return fmt_by_ext;
        //}

        private void sopenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            List<string> fltItems = new List<string>();
            foreach (AddonHolder item in FileHandlerAddonType.Instance.CommonSpace.GetFilteredAddons(RegisterItemUsage.DirectUse))
            {
                IFileHandler fmt = (IFileHandler)item.CreateInstance();
                if (fmt.Caps.OpenAction)
                {
                    fltItems.Add(String.Format("{0} (*.{1})|*.{1}", fmt.Description, fmt.Extension));
                }
            }
            fd.Filter = String.Join("|", fltItems.ToArray());
            fd.CustomPlaces.Add(Core.ScriptsDirectory);
            fd.CustomPlaces.Add(Core.ChartsDirectory);
            if (fd.ShowDialogEx() == DialogResult.OK)
            {
                DoOpen(fd.FileName);
            }
            HTree.CallRefreshRoot();
        }

        private void DoOpen(string filename)
        {
            var han = FileHandlerAddonType.FindFileHandler(new DiskFile(filename), h => h.Caps.OpenAction);
            if (han == null)
            {
                Logging.Warning("File {0} cannot be opened, unregistered extension", filename);
                StdDialog.ShowError(Texts.Get("s_file_cannot_be_opened$extension", "extension", Path.GetExtension(filename)));
                return;
            }
            han.OpenAction();
            //string ext = System.IO.Path.GetExtension(filename).ToLower();
            //if (ext.StartsWith(".")) ext = ext.Substring(1);
            //Dictionary<string, IFileFormat> fmt_by_ext = GetFormatsByExt();
            //if (!fmt_by_ext.ContainsKey(ext))
            //{
            //    Logging.Warning("File {0} cannot be opened, unregistered extension", filename);
            //    StdDialog.ShowError(Texts.Get("s_file_cannot_be_opened$extension", "extension", ext));
            //    return;
            //}
            //fmt_by_ext[ext].Load(filename);
        }

        private void scloseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CloseCurrentTab();
        }

        private void CloseCurrentTab()
        {
            if (dockPanel1.ActiveDocument != null)
            {
                dockPanel1.ActiveDocument.DockHandler.Close();
            }
        }

        public void UpdateFrameEnabling(ContentFrame frame)
        {
            if (frame != ActiveFrame) return;

            mnuCurrentFrame.Visible = false;
            btnSaveAs.Enabled = mnuSaveAs.Enabled = false;
            btnSave.Enabled = mnuSave.Enabled = false;

            if (frame != null)
            {
                string title = frame.MenuBarTitle;
                if (title != null)
                {
                    mnuCurrentFrame.DropDownItems.Clear();
                    mnuCurrentFrame.Text = Texts.Get(title);
                    var mb = new MenuBuilder();
                    ActiveFrame.GetMenu(mb);
                    mb.GetMenuItems(mnuCurrentFrame.DropDownItems);
                    mnuCurrentFrame.Visible = true;
                }
                btnSave.Enabled = mnuSave.Enabled = ActiveFrame.SupportsSave;
                btnSaveAs.Enabled = mnuSaveAs.Enabled = ActiveFrame.SupportsSaveAs;
            }
        }

        private void ChangedContent()
        {
            UpdateFrameEnabling(ActiveFrame);
            HWindow.CallChangedContent();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            UsageStats.ClosingApp(); // start send usage stats
            e.Cancel = false;
            foreach (var cntunknown in dockPanel1.Contents)
            {
                var cnt = cntunknown as IDockWrapper;
                if (cnt == null) continue;
                if (!cnt.AllowClose())
                {
                    e.Cancel = true;
                    return;
                }
            }

            if (!e.Cancel)
            {
                dockPanel1.SaveAsXml(Core.LayoutFile);
            }
        }

        private void soptionsToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OptionsForm.Run();
            if (GlobalSettings.Pages.General().Language != Texts.Language) StdDialog.ShowInfo("s_restart_datadmin_for_changes");
        }

        private void srecordmarcoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MacroRecorder rec = new MacroRecorder(this);
            MacroRecordForm win = new MacroRecordForm(rec);
            win.Show();
            rec.Start();
        }

        ITreeNode FocusedNode
        {
            get
            {
                //if (treDataTree.FocusedTree) return treDataTree.Selected;
                //if (treAddonsTree.FocusedTree) return treAddonsTree.Selected;
                return null;
            }
        }

        private void contentTabs_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void contentTabs_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                IDataObject obj = e.Data;
                string[] files = (string[])obj.GetData("FileDrop");
                foreach (string f in files) DoOpen(f);
            }
            catch (Exception) { }
        }

        private void saboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutForm().ShowDialogEx();
        }

        public void CreateNewConnectionDialog()
        {
            CreateDialog dlg = new CreateDialog(null, "connections", null);
            dlg.ShowDialogEx();
            HTree.CallRefreshRoot();
        }

        private void sconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateNewConnectionDialog();
        }

        private void ssaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveFrame != null) ActiveFrame.Save();
        }

        private void ssaveasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ActiveFrame != null) ActiveFrame.SaveAs();
        }

        private void swwwpageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://datadmin.com");
        }

        private void sforumToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://forum.datadmin.com");
        }

        private void sbulkcopywizardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            BulkCopyWizard.Run(null, null);
        }

        private void squeryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IPhysicalConnection conn = MainWindowExtension.SelectedConnection;
            if (conn != null) conn = conn.Clone();
            OpenQueryParameters pars = new OpenQueryParameters();
            string dbname = MainWindowExtension.SelectedDatabaseName;
            if (dbname != null) conn.AfterOpen += ConnTools.ChangeDatabaseCallback(dbname);
            OpenContent(new QueryFrame(conn, pars));
        }

        //private void sexportaddonsToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    glgSaveFile.Filter = Texts.Get("s_addons_package") + "|*.adp";
        //    if (glgSaveFile.ShowDialogEx() == DialogResult.OK)
        //    {
        //        AddonDbTool.ExportLog log = AddonDbTool.ExportAddons(glgSaveFile.FileName);
        //        StdDialog.ShowInfo(Texts.Get("s_addons_exported$exported", "exported", log.Exported.Count));
        //    }
        //}

        //private void simportaddonsToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    dlgOpenFile.Filter = Texts.Get("s_addons_package") + "|*.adp";
        //    if (dlgOpenFile.ShowDialogEx() == DialogResult.OK)
        //    {
        //        AddonDbTool.ImportLog log = AddonDbTool.ImportAddons(dlgOpenFile.FileName);
        //        StdDialog.ShowInfo(Texts.Get("s_addons_imported$imported$skipped", "imported", log.Imported.Count, "skipped", log.Skipped.Count));
        //    }
        //}

        private void sdrivermanagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DriverManagerForm.Run();
        }

        //private void toolStripButton6_Click(object sender, EventArgs e)
        //{
        //    mnuViewWindowList.Checked = false;
        //    mnuViewWindowList_Click(sender, e);
        //}

        //private void mnuViewWindowList_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        GlobalSettings.Pages.BeginEdit();
        //        GlobalSettings.Pages.View.ShowWindowList = !GlobalSettings.Pages.View.ShowWindowList;
        //    }
        //    finally
        //    {
        //        GlobalSettings.Pages.EndEdit();
        //    }
        //    ReloadSettings();
        //}

        private void mnuHelpContent_Click(object sender, EventArgs e)
        {
            Core.ShowHelp();
        }

        //private void mnuViewBottomToolPanel_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        GlobalSettings.Pages.BeginEdit();
        //        GlobalSettings.Pages.View.ShowBottomToolPanel = !GlobalSettings.Pages.View.ShowBottomToolPanel;
        //    }
        //    finally
        //    {
        //        GlobalSettings.Pages.EndEdit();
        //    }
        //    ReloadSettings();
        //}

        private void sgarbagecollectorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GC.Collect();
        }

        private void timAutoRun_Tick(object sender, EventArgs e)
        {
            Action action = m_autoRun;
            m_autoRun = null;
            timAutoRun.Enabled = false;
            action();
        }

        private void squitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        public IInvoker Invoker { get { return m_invoker; } }

        int m_loadingImageIndex = 0;
        private void timLoadingFrame_Tick(object sender, EventArgs e)
        {
            foreach (var frm in m_loadingFrames)
            {
                frm.SetLoadingAnimationIcon(m_loadingBitmaps[m_loadingImageIndex], m_loadingIcons[m_loadingImageIndex]);
                //frm.Icon = m_loadingIcons[m_loadingImageIndex];
                //dockPanel1.Refresh();
            }
            m_loadingImageIndex++;
            m_loadingImageIndex %= m_loadingIcons.Count;
        }

        public void SendVersionInfo(string text, string newVersion)
        {
            while (!IsHandleCreated) Thread.Sleep(100);
            Invoke((Action)(() => { labNewVersion.Text = text; }));
            if (newVersion != null)
            {
                Invoke((Action)(() =>
                {
                    btnInstall.Text = Texts.Get("s_install") + " " + newVersion;
                    btnInstall.Visible = true;
                }));
            }
        }

        public bool ProcessRefreshMessage()
        {
            //if (treDataTree.FocusedTree)
            //{
            //    treDataTree.Selected.CompleteRefresh();
            //    return true;
            //}
            //if (treAddonsTree.FocusedTree)
            //{
            //    treAddonsTree.Selected.CompleteRefresh();
            //    return true;
            //}`
            return false;
        }

        private void scopydbwizardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CopyDbWizard.RunExport(null);
        }

        private void eRRORToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                throw new Exception("DAE-00358 Test error");
            }
            catch (Exception err)
            {
                Errors.Report(err);
            }
        }

        [DllImport("User32.dll")]
        public static extern Int32 SetForegroundWindow(int hWnd);

        private void timAfterStart_Tick(object sender, EventArgs e)
        {
            try
            {
                SetForegroundWindow(Handle.ToInt32());
                SplashForm.EnsureNoSplash();
            }
            catch (Exception err)
            {
                Logging.Warning("Error when closing spashscreen:" + err.ToString());
            }
            timAfterStart.Enabled = false;
        }


        private void mnuBuyNow_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://datadmin.com/en/purchase");
        }

        private void ssendfeedbackToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SupportConnector.SendFeedback();
            //SendFeedbackForm.Run();
        }

        private void btnSendFeedback_Click(object sender, EventArgs e)
        {
            SendFeedbackForm.Run();
        }

        //private void TestKey(KeyEventArgs e, Keys key, int index)
        //{
        //}

        private void SelectNextDocument(bool prev)
        {
            List<IDockContent> docs = new List<IDockContent>(dockPanel1.Documents);
            int active = docs.FindIndex(d => d.DockHandler.IsActivated);
            if (active >= 0)
            {
                if (prev) active = (active + docs.Count - 1) % docs.Count;
                else active = (active + 1) % docs.Count;
                docs[active].DockHandler.Show();
            }
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            HMainWindow.CallKeyDown(sender, e);

            if (e.Control && e.KeyCode == Keys.Tab)
            {
                SelectNextDocument(e.Shift);
            }

            if (e.Control && e.KeyCode == Keys.W)
            {
                CloseCurrentTab();
            }

            //TestKey(e, Keys.D1, 0);
            //TestKey(e, Keys.D2, 1);
            //TestKey(e, Keys.D3, 2);
            //TestKey(e, Keys.D4, 3);
            //TestKey(e, Keys.D5, 4);
            //TestKey(e, Keys.D6, 5);
            //TestKey(e, Keys.D7, 6);
            //TestKey(e, Keys.D8, 7);
            //TestKey(e, Keys.D9, 8);
        }

        private void MainForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            HMainWindow.CallKeyPress(sender, e);
        }

        private void MainForm_KeyUp(object sender, KeyEventArgs e)
        {
            HMainWindow.CallKeyUp(sender, e);
        }

        private void dockPanel1_ActiveContentChanged(object sender, EventArgs e)
        {
            ChangedContent();
        }

        void ClosedDocker(IDocker docker)
        {
            m_dockerWraps.Remove(docker.Factory.GetPersistString());
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            //bool loadedLayout = false;
            if (File.Exists(Core.LayoutFile))
            {
                try
                {
                    dockPanel1.LoadFromXml(Core.LayoutFile, GetContentFromPersistString);
                    //loadedLayout = true;
                }
                catch (Exception err)
                {
                    Logging.Error("Error loading layout, using default layout:" + err.Message);
                }
            }
            // if (!loadedLayout)
            else
            {
                //while (dockPanel1.Contents.Count > 0) dockPanel1.Contents[0].DockHandler.Close();
                var ms = new MemoryStream();
                var data = Encoding.UTF8.GetBytes(MainWinRes.layout);
                ms.Write(data, 0, data.Length);
                ms.Position = 0;
                dockPanel1.LoadFromXml(ms, GetContentFromPersistString);
            }
            InstallConfigForm.ShowIfNeccessary();
        }

        private void mnuWindowCloseAll_Click(object sender, EventArgs e)
        {
            var docs = new List<IDockContent>(dockPanel1.Documents);
            foreach (var doc in docs)
            {
                doc.DockHandler.Close();
                if (doc.DockHandler.DockState != DockState.Unknown) return;
            }
        }

        public void CloseAllContents()
        {
            var docs = new List<IDockContent>(dockPanel1.Contents);
            foreach (var doc in docs)
            {
                var frm = GetContentFrame(doc);
                if (frm != null)
                {
                    doc.DockHandler.Close();
                    if (doc.DockHandler.DockState != DockState.Unknown) return;
                }
            }
        }

        private void scloseallbutthisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var docs = new List<IDockContent>(dockPanel1.Documents);
            foreach (var doc in docs)
            {
                if (doc.DockHandler.IsActivated) continue;
                doc.DockHandler.Close();
                if (doc.DockHandler.DockState != DockState.Unknown) return;
            }
        }

        private void snextdocumentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectNextDocument(false);
        }

        private void spreviousdocumentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SelectNextDocument(true);
        }

        public List<ContentFrame> GetContents()
        {
            var res = new List<ContentFrame>();
            foreach (var doc in dockPanel1.Contents)
            {
                var cnt = GetContentFrame(doc);
                if (cnt != null) res.Add(cnt);
            }
            return res;
        }

        private void dockPanel1_ContentAdded(object sender, DockContentEventArgs e)
        {
            HWindow.CallChangedContentWindows();
        }

        private void dockPanel1_ContentRemoved(object sender, DockContentEventArgs e)
        {
            HWindow.CallChangedContentWindows();
            m_openTabsHistory.Remove(e.Content);
            if (m_openTabsHistory.Count > 0)
            {
                try { m_openTabsHistory[m_openTabsHistory.Count - 1].DockHandler.Activate(); }
                catch { } // nemusi se vzdy povest
            }
        }

        private void dockPanel1_ActiveDocumentChanged(object sender, EventArgs e)
        {
            var active = dockPanel1.ActiveDocument ?? dockPanel1.ActiveContent;
            m_openTabsHistory.Remove(active);
            if (active != null) m_openTabsHistory.Add(active);
        }

        private void squickexportmanagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TemplateQuickExport.RunManageDialog();
        }

        private void sinstalllicenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AboutForm.RunLicenses();
        }

        private void btnFavorites_Click(object sender, EventArgs e)
        {
            ShowDocker(new FavoritesTreeDockerFactory());
            HFavorites.CallChanged();
        }

        private void btnSaveLayout_Click(object sender, EventArgs e)
        {
            string name = InputBox.Run("s_select_layout_name", cbxCurrentLayout.SelectedIndex > 0 ? cbxCurrentLayout.SelectedItem.ToString() : "new_layout");
            if (name != null)
            {
                string fn = Path.Combine(Core.LayoutsDirectory, name + ".lay");
                if (File.Exists(fn))
                {
                    if (MessageBox.Show(Texts.Get("s_layout_exists_overwrite"), VersionInfo.ProgramTitle, MessageBoxButtons.YesNo) != DialogResult.Yes) return;
                }
                dockPanel1.SaveAsXml(fn);
                ReloadLayouts();
                int idx = cbxCurrentLayout.Items.IndexOf(name);
                if (idx > 0)
                {
                    m_loadingLayouts = true;
                    cbxCurrentLayout.SelectedIndex = idx;
                    m_loadingLayouts = false;
                }
            }
        }

        private void cbxCurrentLayout_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_loadingLayouts) return;
            if (cbxCurrentLayout.SelectedIndex == 0) return;
            string layout = cbxCurrentLayout.SelectedItem.ToString();
            dockPanel1.LoadFromXml(Path.Combine(Core.LayoutsDirectory, layout + ".lay"), GetContentFromPersistString);
        }

        private void btnInstall_Click(object sender, EventArgs e)
        {
            btnInstall.BackColor = SystemColors.Control;
            if (MessageBox.Show(Texts.Get("s_installing_new_version_requires_restart_continue"), VersionInfo.ProgramTitle, MessageBoxButtons.YesNo) != DialogResult.Yes) return;
            Close();
            Core.ExecuteAfterFinalize = CheckAutoUpdate.InstallExeFile;
        }

        public void CloseMainWindow()
        {
            Close();
        }

        private void mnuExportConfig_Click(object sender, EventArgs e)
        {
            ConfigExportForm.Run(AppDataDiskFileSystem.Instance);
        }

        private void mnuImportConfig_Click(object sender, EventArgs e)
        {
            if (openFileDialogConfig.ShowDialogEx() == DialogResult.OK)
            {
                ConfigImportForm.Run(openFileDialogConfig.FileName);
            }
        }

        public void ProcessArgs(IEnumerable<string> args)
        {
            bool skip = false;
            foreach (string arg in args)
            {
                if (arg.StartsWith("--"))
                {
                    skip = true;
                    continue;
                }
                if (skip)
                {
                    skip = false;
                    continue;
                }
                string fn = arg.Trim();
                if (!fn.IsEmpty() && File.Exists(fn))
                {
                    DoOpen(fn);
                }
            }
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_USER + 1)
            {
                foreach (string fn in Directory.GetFiles(Core.IpcDirectory))
                {
                    if (Path.GetFileName(fn).ToLower().StartsWith("args_"))
                    {
                        using (var sr = new StreamReader(fn))
                        {
                            ProcessArgs(sr.ReadToEnd().Split('\n'));
                        }
                        File.Delete(fn);
                    }
                }
            }
        }

        const int WM_USER = 0x0400;

        private void btnSendToColleague_Click(object sender, EventArgs e)
        {
            ConfigSendForm.Run(AppDataDiskFileSystem.Instance);
        }

        private void mnuDashboardManager_Click(object sender, EventArgs e)
        {
            DashboardManager.RunManageDialog();
        }

        private void mnuTablePerspectives_Click(object sender, EventArgs e)
        {
            TablePerspectiveManager.RunManageDialog();
        }

        private void MainForm_Activated(object sender, EventArgs e)
        {
            foreach (var cnt in GetContents())
            {
                cnt.OnAppActivate();
            }
        }

        public Bitmap TakeScreenshot()
        {
            try
            {
                var res = new Bitmap(Width, Height, PixelFormat.Format32bppArgb);
                using (var g = Graphics.FromImage(res))
                {
                    g.CopyFromScreen(Left, Top, 0, 0, Size, CopyPixelOperation.SourceCopy);
                }
                return res;
            }
            catch
            {
                return null;
            }
        }

        private void ssupportrequestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SupportConnector.SupportRequest();
        }

        private void MainForm_Resize(object sender, EventArgs e)
        {
            if (WindowState != FormWindowState.Minimized && IsHandleCreated)
            {
                Settings.Default.WindowPositionStored = true;
                Settings.Default.WindowPosition = Location;
                Settings.Default.WindowSize = Size;
                Settings.Default.ScreenAreas = GetScreenAreas();
                Settings.Default.WindowState = WindowState;
                Settings.Default.Save();
            }
        }
    }
}
