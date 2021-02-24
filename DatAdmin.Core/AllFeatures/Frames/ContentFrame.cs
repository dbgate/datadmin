using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections.ObjectModel;

namespace DatAdmin
{
    public partial class ContentFrame : UserControl, IConnectionPackHolder
    {
        bool m_isContentVisible;
        bool m_isLoadingIcon;
        protected ControlInvoker m_invoker;
        public IDockWrapper m_dockWrapper;
        ConnectionPack m_connPack;

        internal DockPanelContentFrame m_parentFrame;
        List<ContentFrame> m_detailFrames = new List<ContentFrame>();
        ReadOnlyCollection<ContentFrame> m_readDetailFrames;
        ContentFrame m_masterFrame;
        public string PersistString;

        public string WinId { get; set; }

        bool m_detached = false;
        //bool m_changingDepSource = false;

        object m_currentDependencySource;
        protected object m_proposedDependencySource;

        public virtual string UsageEventName { get { return "content:" + GetType().FullName; } }
        public UsageBuilder Usage;

        public ContentFrame()
        {
            Usage = new UsageBuilder(UsageEventName);
            PersistString = Guid.NewGuid().ToString();
            m_invoker = new ControlInvoker(this);
            m_readDetailFrames = new ReadOnlyCollection<ContentFrame>(m_detailFrames);
            ConnPack = new ConnectionPack(this);
            Disposed += ContentFrame_Disposed;
            InitializeComponent();
            HConnection.RemoveByKey += HConnection_RemoveByKey;
            HUsage.ClosingApp += HUsage_ClosingApp;
        }

        void HUsage_ClosingApp()
        {
            Usage.Send();
        }

        void ContentFrame_Disposed(object sender, EventArgs e)
        {
            HConnection.RemoveByKey -= HConnection_RemoveByKey;
            HUsage.ClosingApp -= HUsage_ClosingApp;
            if (ParentFrame != null)
            {
                var pf = ParentFrame;
                MainWindow.Instance.RunInMainWindow(() => pf.NotifyDisposed(this));
            }
            Usage.Dispose();
        }

        void HConnection_RemoveByKey(RemoveConntectionByKeyArgs e)
        {
            if (ConnPack == null) return;
            if (!ConnPack.Contains(e.ConnKey)) return;
            if (!AllowCloseConnection(e.ConnKey))
            {
                e.Canceled = true;
                return;
            }
            var async = ConnPack.BeginRemoveByKey(e.ConnKey, Async.CreateInvokeCallback(m_invoker, RemovedConnection));
            if (async.CompletedSynchronously) RemovedConnection(async);
        }

        private void RemovedConnection(IAsyncResult async)
        {
            try
            {
                ((IStandaloneAsyncResult)async).EndInvoke();
            }
            catch (Exception err)
            {
                Errors.Report(err);
            }
        }

        public ConnectionPack ConnPack
        {
            get { return m_connPack; }
            set
            {
                if (m_connPack != null) m_connPack.Release();
                m_connPack = value;
                if (m_connPack != null) m_connPack.AddRef();
                OnChangeConnPack();
            }
        }

        protected virtual void OnChangeConnPack()
        {
        }

        public virtual bool AllowCloseConnection(string connkey)
        {
            return true;
        }

        public virtual string PageTitle { get { return "content"; } }
        public virtual string PageToolTip { get { return PageTitle; } }

        public virtual string PageTitleForParent { get { return PageTitle; } }
        public virtual string PageToolTipForParent { get { return PageToolTip; } }
        public virtual Bitmap ImageForParent { get { return Image; } }

        public virtual string PageTypeTitle { get { return "content"; } }

        public DockPanelContentFrame ParentFrame
        {
            get { return m_parentFrame; }
        }

        [Browsable(false)]
        public ContentFrame MasterFrame
        {
            get { return m_masterFrame; }
            set
            {
                if (m_masterFrame != null)
                {
                    m_masterFrame.RemoveDetail(this);
                }
                m_masterFrame = value;
                if (m_masterFrame != null) m_masterFrame.m_detailFrames.Add(this);
                OnChangedMasterFrame();
            }
        }

        private void RemoveDetail(ContentFrame frame)
        {
            m_detailFrames.Remove(frame);
            MainWindow.Instance.RunInMainWindow(HandleRemovedDetail);
        }

        private void HandleRemovedDetail()
        {
            //if (ParentFrame != null &&
            //    (
            //        (ParentFrame.ContentCount == 2 && ParentFrame.ContainsContent(this) && ParentFrame.ContainsContent(frame))
            //        || (ParentFrame.ContentCount == 1 && ParentFrame.ContainsContent(this))
            //    ))
            if (ParentFrame != null && ParentFrame.ContentCount == 1 && ParentFrame.ContainsContent(this))
            {
                var frm = ParentFrame;
                var dock = frm.m_dockWrapper;
                frm.ReleaseFrame(this);
                dock.ReplaceContent(this);
                frm.Dispose();
                MainWindow.Instance.UpdateFrameEnabling(this);
                OnRemovedDockingLayer();
            }
        }

        protected virtual void OnRemovedDockingLayer() { }
        protected virtual void OnChangedMasterFrame() { }

        public IList<ContentFrame> DetailFrames
        {
            get { return m_readDetailFrames; }
        }

        protected void GetThisAndDetails(List<ContentFrame> wins)
        {
            wins.Add(this);
            foreach (var det in DetailFrames) det.GetThisAndDetails(wins);
        }

        public override string ToString()
        {
            return Texts.Get(PageTypeTitle) + ":" + Texts.Get(PageTitle);
        }

        private void ContentFrame_Load(object sender, EventArgs e)
        {
            Translating.TranslateControl(this);
        }

        public virtual bool AllowClose()
        {
            return true;
        }

        public virtual void OnClose()
        {
            ConnPack = null;
            foreach (var det in DetailFrames) det.DisposeContentAndDetails();

            // remove from masters's detail list
            MasterFrame = null;
        }

        public virtual void GetMenu(MenuBuilder bld)
        {
            bld.AddObject(this);
        }
        // null: menubar os not shown
        public virtual string MenuBarTitle { get { return null; } }
        public void CloseContent()
        {
            Errors.Assert(m_dockWrapper != null);
            MainWindow.Instance.CloseContent(this);
        }
        protected void UpdateTitle()
        {
            MainWindow.Instance.UpdateContentTitle(this);
        }

        protected void UpdateFrameEnabling()
        {
            MainWindow.Instance.UpdateFrameEnabling(this);
        }

        public virtual bool SupportsSave
        {
            get { return false; }
        }
        public virtual bool SupportsSaveAs
        {
            get { return false; }
        }
        public virtual Bitmap Image { get { return null; } }
        public virtual bool Save() { return true; }
        public virtual bool SaveAs() { return true; }
        public bool IsContentVisible
        {
            get { return m_isContentVisible; }
            set
            {
                if (value != m_isContentVisible)
                {
                    m_isContentVisible = value;
                    if (value) OnShowContent();
                    else OnHideContent();
                }
            }
        }
        public virtual void OnShowContent()
        {
            if (m_proposedDependencySource == m_currentDependencySource) return;
            if (!AllowClose()) return;
            CallLoadFromDependencySource(m_proposedDependencySource);
        }

        protected void CallLoadFromDependencySource(object value)
        {
            m_currentDependencySource = value;
            LoadFromDependencySource(value);
        }

        protected void ChangeDependencySource(object value)
        {
            if (m_proposedDependencySource == value) return;
            m_proposedDependencySource = value;
            if (Detached) return;
            if (!AllowClose())
            {
                StdDialog.ShowInfo("s_info_about_detached_data");
                Detached = true;
                return;
            }
            if (IsContentVisible) CallLoadFromDependencySource(m_proposedDependencySource);
        }

        protected virtual void LoadFromDependencySource(object value) { }

        //protected virtual bool DependendFrameVisible(ContentFrame frame)
        //{
        //    return true;
        //}
        public virtual void OnHideContent()
        {
            //foreach (var refwin in DetailFrames)
            //{
            //    refwin.ShowContentAndDetails(false);
            //}
        }

        private void DisposeContentAndDetails()
        {
            foreach (var refwin in DetailFrames) refwin.DisposeContentAndDetails();
            Dispose();
        }

        //public void ShowContentAndDetails(bool visible)
        //{
        //    MainWindow.Instance.ShowContent(this, visible);
        //    foreach (var refwin in DetailFrames) refwin.ShowContentAndDetails(visible);
        //}


        public bool IsLoadingIcon
        {
            get
            {
                return m_isLoadingIcon;
            }
            set
            {
                if (m_isLoadingIcon == value) return;
                m_isLoadingIcon = value;
                if (MainWindow.Instance != null)
                {
                    MainWindow.Instance.SetLoadingFrame(this, m_isLoadingIcon);
                }
            }
        }

        public virtual bool RequiresLoadingAnimation() { return false; }
        public virtual void SetLoadingAnimationIcon(Bitmap bmp, Icon icon) { }

        public void OpenDetailInNewDock(ContentFrame frame, DocumentDockPosition position)
        {
            var master = new DockPanelContentFrame();
            master.HeaderRedirectFrame = this;
            master.PrimaryContent = this;
            m_dockWrapper.ReplaceContent(master);
            master.OpenContent(this, null, DocumentDockPosition.Center);
            master.OpenContent(frame, this, position);
            MainWindow.Instance.UpdateFrameEnabling(master);
        }

        protected void InvalidateCurrentContent()
        {
            if (IsContentVisible)
            {
                Controls.ShowProgress(true, null, null);
                foreach (var detail in DetailFrames) detail.InvalidateCurrentContent();
            }
        }

        public bool Detached
        {
            get { return m_detached; }
            set
            {
                m_detached = value;
                try
                {
                    ReloadDetachedState();
                }
                catch
                { }
            }
        }

        protected virtual void ReloadDetachedState() { }

        public virtual void OnAppActivate()
        {
        }

        public virtual ContentFrame ActiveFrame { get { return this; } }
    }
}
