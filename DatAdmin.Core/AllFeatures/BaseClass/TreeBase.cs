using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using System.Reflection;
using System.ComponentModel;

namespace DatAdmin
{
    public abstract class TreeNodeBase : ITreeNode, IConnectionPackHolder
    {
        protected IRealTreeNode m_realnode;
        protected readonly ITreeNode m_parent;
        protected readonly string m_name;
        protected readonly string m_fullpath;
        ConnectionPack m_connpack;
        protected AppObject m_primaryAppobj;
        protected List<AppObject> m_appobjs = new List<AppObject>();
        List<object> m_nodeExtenders = new List<object>();

        protected TreeNodeBase(string protocol)
        {
            m_connpack = new ConnectionPack(this);
            m_name = "root";
            m_fullpath = protocol + ":";
            //CopyDerivedProperties();
            InitializeExtenders();
        }

        protected TreeNodeBase(ITreeNode parent, string name)
        {
            m_connpack = parent.ConnPack;
            if (parent == null) throw new InternalError("DAE-00007 Parent cannot be null");
            m_name = name;
            m_parent = parent;
            m_fullpath = parent.Path + "/" + name;
            //CopyDerivedProperties();
            InitializeExtenders();
        }

        public override string ToString()
        {
            return Title;
        }

        private void InitializeExtenders()
        {
            foreach (var hold in TreeExtenderAddonType.Instance.CommonSpace.GetAllAddons())
            {
                var ext = hold.InstanceModel as ITreeExtender;
                ext.GetNodeExtendObjects(this, m_nodeExtenders);
            }
        }

        public static void CallRefreshChilds(ITreeNode node, bool deep)
        {
            if (node.RealNode != null) node.RealNode.RefreshChilds(deep);
        }

        public static void CallRefreshSelf(ITreeNode node)
        {
            if (node.RealNode != null) node.RealNode.RefreshSelf();
        }

        public static void CallRefresh(ITreeNode node, bool deep)
        {
            CallRefreshSelf(node);
            CallRefreshChilds(node, deep);
        }

        public static void CallRefresh(ITreeNode node)
        {
            CallRefresh(node, false);
        }

        public event EventHandler ChangedProperties;

        protected void DispatchChangedProperties()
        {
            if (ChangedProperties != null) ChangedProperties(this, EventArgs.Empty);
        }

        [Browsable(false)]
        public virtual List<IVirtualFolder> VirtualFolders { get { return new List<IVirtualFolder>(); } }

        public void CallRefresh()
        {
            TreeNodeBase.CallRefresh(this);
        }

        [PopupMenu("s_refresh", ImageName = CoreIcons.refreshName, Weight = MenuWeights.REFRESH, GroupName = "node")]
        public void DeepRefresh()
        {
            this.CompleteRefresh(true);
        }

        [PopupMenu("s_refresh_this_object", ImageName = CoreIcons.refresh3Name, Weight = MenuWeights.REFRESH + 1, GroupName = "node")]
        public void CmdRefresh()
        {
            this.CompleteRefresh();
        }

        [PopupMenuEnabled("s_rename")]
        public bool RenameEnabled()
        {
            return AllowRename();
        }

        [PopupMenu("s_rename", ImageName = CoreIcons.renameName, Shortcut = Keys.F2, Weight = MenuWeights.RENAME, GroupName = "node")]
        public void CmdRename()
        {
            RealNode.ShowRenameEditor();
        }

        [PopupMenuEnabled("s_delete")]
        public bool DeleteEnabled()
        {
            return AllowDelete();
        }

        [PopupMenuVisible("s_copy_to_clipboard")]
        public bool MenuCallCopyViisble()
        {
            return this.CallCopyEnabled();
        }

        [PopupMenu("s_copy_to_clipboard", ImageName = CoreIcons.copyName, GroupName = "node", Weight = MenuWeights.COPY)]
        public void MenuCallCopy()
        {
            this.CallCopy();
        }

        public virtual bool AllowNewChild() { return false; }
        public virtual bool NewChild() { return false; }

        public virtual bool AllowDelete()
        {
            foreach (var appobj in GetValidAppObjects()) if (appobj.AllowDelete()) return true;
            return false;
        }

        public virtual bool DoDelete()
        {
            foreach (var appobj in GetValidAppObjects())
            {
                if (appobj.AllowDelete())
                {
                    return appobj.DeleteWithQuery();
                    //if (StdDialog.YesNoDialog("s_really_delete$object", "object", Title))
                    //{
                    //    m_primaryAppobj.DoDelete();
                    //    return true;
                    //}
                    //return false;
                }
            }
            return false;
        }

        [PopupMenu("s_delete", ImageName = CoreIcons.deleteName, Weight = MenuWeights.RENAME, GroupName = "node")]
        public void CmdDelete()
        {
            Delete();
        }

        public bool Delete()
        {
            if (!AllowDelete()) return false;
            if (!DoDelete()) return false;
            TreeNodeExtension.CompleteRefresh(Parent);
            if (RealNode.TreeBehaviour.AfterDeletedNode != null) RealNode.TreeBehaviour.AfterDeletedNode(this);
            return true;
        }

        #region ITreeNode Members

        [Browsable(false)]
        public IRealTreeNode RealNode
        {
            get { return m_realnode; }
            set
            {
                m_realnode = value;
                OnSetRealNode();
            }
        }

        public virtual void OnSetRealNode() { }

        [Browsable(false)]
        public abstract string Title { get; }
        [Browsable(false)]
        public abstract string TypeTitle { get; }
        [Browsable(false)]
        public string Name
        {
            get { return m_name; }
        }

        [Browsable(false)]
        public virtual Bitmap Image
        {
            get
            {
                var appobj = GetPrimaryAppObject();
                if (appobj != null) return appobj.Image;
                return null;
            }
        }
        [Browsable(false)]
        public virtual Bitmap ExpandedImage { get { return null; } }

        public abstract ITreeNode[] GetChildren();
        public virtual IAsyncResult BeginLoadChildren(AsyncCallback callback)
        {
            return new ValueAsyncResult(null, null);
        }
        public virtual void EndLoadChildren(IAsyncResult async) { }
        [Browsable(false)]
        public virtual bool PreparedChildren { get { return true; } }
        protected virtual bool ExplicitShowSqlGenerators() { return false; }
        public virtual void GetPopupMenu(MenuBuilder menu)
        {
            menu.AddObject(this);
            foreach (var appobj in GetValidAppObjects()) appobj.GetPopupMenu(menu);
            foreach (var ext in m_nodeExtenders) menu.AddObject(ext);
        }

        public IEnumerable<AppObject> GetValidAppObjects()
        {
            var appobj = GetPrimaryAppObject();
            if (appobj != null) yield return appobj;
            foreach (var ao in m_appobjs)
            {
                if (AppObjectAvailable(ao)) yield return ao;
            }
        }

        [Browsable(false)]
        public virtual bool AutoConnect { get { return false; } }

        [Browsable(false)]
        public virtual bool AllowExpand() { return true; }

        [Browsable(false)]
        public ITreeNode Parent
        {
            get { return m_parent; }
        }

        public virtual void DataRefresh()
        {
        }

        public virtual void NotifyDeepRefresh()
        {
        }

        [Browsable(false)]
        public virtual string Path
        {
            get { return m_fullpath; }
        }

        public ITreeNode GetNamedChild(string child_name)
        {
            foreach (ITreeNode node in GetChildren())
            {
                if (node.Name == child_name) return node;
            }
            throw new TreeNodeNotFoundException(child_name);
        }

        [Browsable(false)]
        public virtual string FileSystemPath
        {
            get { return null; }
        }

        public virtual string GetPrivateSubFolder(string name)
        {
            if (Parent != null) return Parent.GetPrivateSubFolder(name);
            return null;
        }

        public virtual bool DoubleClick()
        {
            var appobj = GetPrimaryAppObject();
            if (appobj != null) return appobj.DefaultAction();
            return false;
        }

        public virtual void ModifiedDoubleClick(Keys keys)
        {
            var appobj = GetPrimaryAppObject();
            if (appobj != null) appobj.ModifiedDoubleClick(keys);
        }

        public virtual void NotifyExpanded()
        {
        }

        public virtual void CloseAllResources()
        {
        }

        public virtual IPhysicalConnection GetConnection()
        {
            return null;
        }

        public virtual bool AllowReuse()
        {
            return true;
        }

        protected virtual IEnumerable<object> GetDragDropOperationObjects()
        {
            foreach (var x in m_nodeExtenders) yield return x;
            yield return this;
            foreach (var appobj in GetValidAppObjects())
            {
                foreach (var ddo in appobj.GetDragDropOperationObjects())
                {
                    yield return ddo;
                }
            }
        }

        public DragDropBuilder GetDragDropBuilder(AppObject[] draggingObjects)
        {
            var bld = new DragDropBuilder();
            foreach (object obj in GetDragDropOperationObjects())
            {
                bld.AddObject(obj, draggingObjects);
            }
            return bld;
        }

        public virtual void DragDrop(AppObject[] draggingObjects)
        {
            var bld = GetDragDropBuilder(draggingObjects);
            bld.ShowMenu();
        }

        public virtual bool AllowDragDrop(AppObject[] draggingObjects)
        {
            var bld = GetDragDropBuilder(draggingObjects);
            return bld.ContainsOperation();
        }

        public virtual bool AllowCreate(string group, string name) { return true; }

        public virtual ITabularDataView GetTabularData()
        {
            var appobj = GetPrimaryAppObject();
            if (appobj != null) return appobj.GetTabularData(ConnPack);
            throw new NotImplementedError("DAE-00158");
        }

        protected virtual bool HasOwnTabularData
        {
            get
            {
                return false;
            }
        }

        [Browsable(false)]
        public virtual bool HasTabularData
        {
            get
            {
                var appobj = GetPrimaryAppObject();
                if (appobj != null) return appobj.HasTabularData;
                return HasOwnTabularData;
            }
        }

        public virtual ObjectPath GetObjectPath() { return null; }

        public virtual List<IWidget> GetWidgets()
        {
            return new List<IWidget>();
        }

        public virtual void RenameNode(string newname)
        {
            foreach (var appobj in GetValidAppObjects())
            {
                if (appobj.AllowRename())
                {
                    appobj.RenameObject(newname);
                    return;
                }
            }
            throw new InternalError("DAE-00008 rename not supported");
        }
        public virtual bool AllowRename()
        {
            foreach (var appobj in GetValidAppObjects()) if (appobj.AllowRename()) return true;
            return false;
        }

        public virtual string GetRenamingText()
        {
            foreach (var appobj in GetValidAppObjects())
            {
                if (appobj.AllowRename()) return appobj.GetRenamingText();
            }
            return Title;
        }

        public virtual void AfterUserCheck() { }

        protected virtual bool AppObjectAvailable(AppObject appobj)
        {
            return true;
        }

        public virtual AppObject GetPrimaryAppObject()
        {
            if (m_primaryAppobj != null && AppObjectAvailable(m_primaryAppobj)) return m_primaryAppobj;
            return null;
        }

        public virtual AppObject GetFirstValidAppObject()
        {
            return GetValidAppObjects().FirstOrDefault();
        }

        void m_appobj_CompleteChanged(object sender, EventArgs e)
        {
            Parent.CompleteRefresh();
            this.CompleteRefresh();
        }

        protected void SetAppObject(AppObject appobj)
        {
            SetAppObject(appobj, true);
        }

        protected void SetAppObject(AppObject appobj, bool isprimary)
        {
            if (m_primaryAppobj != null && isprimary)
            {
                m_appobjs.Add(m_primaryAppobj);
                m_primaryAppobj = null;
                //    appobj.CompleteChanged -= new EventHandler(m_appobj_CompleteChanged);
                //    appobj.ConnPack = null;
            }
            if (isprimary)
            {
                m_primaryAppobj = appobj;
            }
            else
            {
                m_appobjs.Add(appobj);
            }
            if (m_primaryAppobj != null)
            {
                appobj.Owner = this;
                appobj.CompleteChanged += new EventHandler(m_appobj_CompleteChanged);
                appobj.ConnPack = ConnPack;
            }
        }

        #endregion

        public ConnectionPack ConnPack
        {
            get { return m_connpack; }
            set
            {
                m_connpack = value;
                AfterChangeConnPack();
            }
        }

        protected virtual void AfterChangeConnPack()
        {
        }

        public virtual void RemovedConnection(string connkey) { }

        protected bool OpenTheBestDashboard()
        {
            var appobj = GetPrimaryAppObject();
            if (appobj != null) return appobj.OpenTheBestDashboard();
            return false;
        }

        [PopupMenuVisible("s_open_data")]
        public bool OpenDataVisible()
        {
            return HasOwnTabularData;
        }

        [PopupMenu("s_open_data", ImageName = CoreIcons.table_dataName, Weight = MenuWeights.OPEN1, MultiMode = MultipleMode.Sequencable, GroupName = "sql")]
        public void OpenData()
        {
            var tabdata = GetTabularData();
            tabdata.CloneConnection();
            var content = new TableDataFrame(tabdata);
            MainWindow.Instance.OpenContent(content);
        }

        public virtual bool ContainsDatabaseNode() { return false; }
    }

    public abstract class LateLoadNoConnTreeNode : TreeNodeBase
    {
        protected ITreeNode[] m_children = null;
        Action m_loadChildren;

        public LateLoadNoConnTreeNode(ITreeNode parent, string name)
            : base(parent, name)
        {
        }

        public override bool PreparedChildren
        {
            get
            {
                return m_children != null;
            }
        }
        protected abstract void DoGetChildren();
        public override IAsyncResult BeginLoadChildren(AsyncCallback callback)
        {
            m_loadChildren = DoGetChildren;
            return m_loadChildren.BeginInvoke(callback, null);
        }
        public override void EndLoadChildren(IAsyncResult async)
        {
            m_loadChildren.EndInvoke(async);
        }
        public override void DataRefresh()
        {
            m_children = null;
        }
        public override ITreeNode[] GetChildren()
        {
            if (m_children == null) return new ITreeNode[] { };
            return m_children;
        }
    }
}

