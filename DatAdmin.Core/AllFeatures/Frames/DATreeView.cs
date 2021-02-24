using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Resources;
using System.IO;
using System.Linq;

namespace DatAdmin
{
    public partial class DATreeView : UserControl
    {
        DATreeNode m_root = null;
        internal ControlInvoker m_invoker;
        ImageCache m_imgCache;
        TreeStyle m_treeStyle = TreeStyle.TreeView;

        bool m_focused2 = false;
        DateTime? m_lastDoubleClick;
        //NodeAdapter m_draggingNode;

        public event EventHandler ActiveNodeChange;
        public event EventHandler TreeEnter;
        public event EventHandler TreeLeave;
        public event NodeEventHandler AddedNode;
        public event NodeEventHandler ExpandedNode;

        public bool ShowingRenameEditor = false;

        public event MouseEventHandler TreeDoubleClick;

        public ITreeNode Root
        {
            get
            {
                if (m_root != null) return m_root.m_node;
                return null;
            }
            set
            {
                SetRoot(value);
            }
        }

        public TreeStyle TreeStyle
        {
            get { return m_treeStyle; }
            set
            {
                m_treeStyle = value;
                SetTreeBehaviour();
                tree.CheckBoxes = value == TreeStyle.SelectMany;
            }
        }

        public string DialogLabel
        {
            get
            {
                if (!panel1.Visible) return null;
                return label1.Text;
            }
            set
            {
                if (!value.IsEmpty())
                {
                    label1.Text = Texts.Get(value);
                    panel1.Visible = true;
                }
                else
                {
                    panel1.Visible = false;
                }
            }
        }

        private void SetTreeBehaviour()
        {
            bool simple = TreeStyle != TreeStyle.TreeView;
            TreeBehaviour = new TreeBehaviour
                {
                    ForceAutoConnect = simple,
                    AllowDragDrop = !simple,
                    AllowDoubleClickNodeHandling = !simple,
                    AllowPopupMenu = !simple,
                    AllowRenameNode = !simple,
                    HideToolbar = simple,
                    InfluenceGlobalTrees = !simple,
                };
        }

        public string RootPath
        {
            get
            {
                if (m_root != null) return Root.Path;
                return null;
            }
            set
            {
                if (value != null)
                {
                    Root = NodeFactory.GetNodeFromPath(value);
                }
                else
                {
                    Root = null;
                }
            }
        }

        public DATreeView()
        {
            m_invoker = new ControlInvoker(this);
            InitializeComponent();
            m_imgCache = new ImageCache(imageList1, Color.White);
            //m_manager = new ResourceManager("StdIcons", typeof(DATreeView).Assembly);
            SetTreeBehaviour();
            HObjectClipboard.Changed += HObjectClipboard_Changed;
            this.Disposed += new EventHandler(DATreeView_Disposed);
            HConnection.RemoveByKey += HConnection_RemoveByKey;
            OnlineHelpManager.RegisterHelpButton(btnOnlineHelp, "contree", true);
        }

        void HObjectClipboard_Changed()
        {
            UpdateToolbarEnabling();
        }

        void HConnection_RemoveByKey(RemoveConntectionByKeyArgs obj)
        {
            if (Root == null || Root.ConnPack == null) return;
            Root.ConnPack.BeginRemoveByKey(obj.ConnKey, Async.CreateInvokeCallback(m_invoker, async => RemovedConnection(async, obj.ConnKey)));
        }

        private void DispatchRemovedConnection(TreeNodeCollection nodes, string connkey)
        {
            foreach (TreeNode node in nodes)
            {
                DispatchRemovedConnection(node.Nodes, connkey);
                var dnode = node as DATreeNode;
                if (dnode != null) dnode.m_node.RemovedConnection(connkey);
            }
        }

        private void RemovedConnection(IAsyncResult async, string connkey)
        {
            ((IStandaloneAsyncResult)async).EndInvoke();
            DispatchRemovedConnection(tree.Nodes, connkey);
        }

        void DATreeView_Disposed(object sender, EventArgs e)
        {
            if (m_root != null) m_root.CloseAllResources();
            HConnection.RemoveByKey -= HConnection_RemoveByKey;
            HObjectClipboard.Changed -= HObjectClipboard_Changed;
        }

        private void tree_BeforeExpand(object sender, TreeViewCancelEventArgs e)
        {
            try
            {
                if (!((DATreeNode)e.Node).AllowExpand())
                {
                    e.Cancel = true;
                }
            }
            catch (Exception err)
            {
                Errors.Report(err);
            }
        }

        bool VisibleRoot { get { return m_treeStyle == TreeStyle.TreeView; } }

        internal bool FilterNode(ITreeNode node)
        {
            if (TreeBehaviour.ShowFilter != null && !TreeBehaviour.ShowFilter(node)) return false;
            return true;
        }

        private void SetRoot(ITreeNode node)
        {
            if (node != null)
            {
                m_root = new DATreeNode(node, this);
            }
            else
            {
                m_root = null;
            }
            tree.Nodes.Clear();
            if (m_root != null)
            {
                if (VisibleRoot)
                {
                    tree.Nodes.Add(m_root);
                    m_root.UpdateImageIndex();
                    m_root.WantChildren();
                    MainWindow.Instance.RunInMainWindow(tree.Nodes[0].Expand);
                }
                else
                {
                    foreach (ITreeNode child in m_root.m_node.GetChildrenNow())
                    {
                        var ada = new DATreeNode(child, this);
                        ada.UpdateImageIndex();
                        if (FilterNode(child)) tree.Nodes.Add(ada);
                        ada.WantChildren();
                    }
                }
            }
            UpdateToolbarEnabling();
        }

        public int GetImageIndex(Bitmap image)
        {
            return m_imgCache.GetImageIndex(image);
        }

        private void tree_AfterExpand(object sender, TreeViewEventArgs e)
        {
            ((DATreeNode)e.Node).AfterExpand();
            if (ExpandedNode != null) ExpandedNode(this, new NodeEventArgs(((DATreeNode)e.Node)));
            foreach (var child in ((DATreeNode)e.Node).Nodes)
            {
                var danode = child as DATreeNode;
                if (danode != null) danode.WantChildren();
            }
        }

        private void tree_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            ((DATreeNode)e.Node).AfterCollapse();
        }

        public ITreeNode Selected
        {
            get
            {
                if (tree.SelectedNode == null) return null;
                if (tree.SelectedNode is DATreeNode) return ((DATreeNode)tree.SelectedNode).m_node;
                return null;
            }
        }

        public void SelectNode(IRealTreeNode node, SelectNodeFlags flags)
        {
            if (node == null)
            {
                tree.SelectedNode = null;
                return;
            }
            tree.SelectedNode = (DATreeNode)node;
            if ((flags & SelectNodeFlags.Expand) != 0)
            {
                node.ExpandNode();
            }
            if ((flags & SelectNodeFlags.FocusTree) != 0)
            {
                tree.Focus();
                if (TreeBehaviour.InfluenceGlobalTrees) HTree.CallFocusedNodeChanged();
            }
            if ((flags & SelectNodeFlags.ScrollInView) != 0)
            {
                node.EnsureVisibleNode();
            }
        }

        public void SelectNodeByPath(string path, SelectNodeFlags flags)
        {
            var node = FindNode(path, true);
            if (node != null) SelectNode(node, flags);
        }

        private void tree_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            m_lastDoubleClick = DateTime.Now;
            if (TreeBehaviour.AllowDoubleClickNodeHandling)
            {
                try
                {
                    TreeNode node = tree.GetNodeAt(e.X, e.Y);
                    if (node != null)
                    {
                        if (Control.ModifierKeys != Keys.None)
                        {
                            ((DATreeNode)node).ModifiedDoubleClick(Control.ModifierKeys);
                        }
                        else
                        {
                            ((DATreeNode)node).DoubleClick();
                        }
                        MacroManager.DoubleClickNode(((DATreeNode)node).m_node);
                    }
                }
                catch (Exception ex)
                {
                    Errors.Report(ex);
                }
            }
            else
            {
                if (TreeDoubleClick != null) TreeDoubleClick(sender, e);
            }
        }

        public TreeView Tree { get { return tree; } }

        private void tree_MouseUp(object sender, MouseEventArgs e)
        {
            if (!TreeBehaviour.AllowPopupMenu) return;
            try
            {
                if (e.Button == MouseButtons.Right)
                {
                    // select the clicked node
                    tree.SelectedNode = tree.GetNodeAt(e.Location);
                    // show the context menu in the right position
                    if (tree.SelectedNode != null)
                    {
                        ContextMenuStrip menu = ((DATreeNode)tree.SelectedNode).m_menu;
                        if (menu != null)
                        {
                            MacroManager.SetPopupMenuObject(((DATreeNode)tree.SelectedNode).m_node);
                            ((DATreeNode)tree.SelectedNode).FillPopupMenu();
                            menu.ShowOnCursor();
                        }
                    }
                    //tree.ContextMenuStrip.Show(treeView, e.Location);
                }
            }
            catch (Exception ex)
            {
                Errors.Report(ex);
            }
        }

        private void tree_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (Selected == null) return;
                bool processed = false;
                if (e.KeyCode == Keys.Delete)
                {
                    var sel = Selected;
                    var par = sel.Parent;
                    if (!processed) processed = sel.Delete();
                    MacroManager.DeleteNode(sel);
                    //par.CompleteRefresh();
                }
                else if (e.KeyCode == Keys.Enter)
                {
                    if (!processed) processed = Selected.DoubleClick();
                    MacroManager.DoubleClickNode(Selected);
                }
                else if (e.KeyCode == Keys.F5)
                {
                    if (!processed)
                    {
                        Selected.CompleteRefresh(true);
                        processed = true;
                    }
                }
                if (e.KeyCode == Keys.C && e.Control)
                {
                    CallCopy();
                    processed = true;
                }
                if (e.KeyCode == Keys.V && e.Control && btnPaste.Enabled)
                {
                    btnPaste.ShowDropDown();
                    processed = true;
                }
                if (e.KeyCode == Keys.G && e.Control && Selected != null)
                {
                        var appobj=Selected.GetFirstValidAppObject();
                    if (appobj!= null)
                    {
                        if (appobj.GenerateSqlVisible())
                        {
                            appobj.GenerateSql(new object[] { appobj });
                        }
                    }
                    processed = true;
                }

                if (!processed)
                {
                    var mb = new MenuBuilder();
                    Selected.GetPopupMenu(mb);
                    mb.ProcessKeyDown(e.KeyCode);
                }
            }
            catch (Exception ex)
            {
                Errors.Report(ex);
            }
        }

        private void tree_ItemDrag(object sender, ItemDragEventArgs e)
        {
            if (!TreeBehaviour.AllowDragDrop) return;
            var appobjs = new List<AppObject>(((DATreeNode)e.Item).m_node.GetValidAppObjects());
            if (appobjs.Count == 0) return;
            try
            {
                DoDragDrop(DragObjectContainer.Create(appobjs.ToArray()), DragDropEffects.Copy);
            }
            catch (Exception ex)
            {
                Errors.Report(ex);
            }
        }

        private void tree_DragDrop(object sender, DragEventArgs e)
        {
            if (!TreeBehaviour.AllowDragDrop) return;
            try
            {
                TreeNode dropNode = tree.GetNodeAt(tree.PointToClient(new Point(e.X, e.Y)));
                DragObjectContainer cnt = (DragObjectContainer)e.Data.GetData(typeof(DragObjectContainer));
                if (cnt != null && dropNode != null && cnt.Data is AppObject[])
                {
                    ITreeNode targetNode = ((DATreeNode)dropNode).m_node;
                    AppObject[] draggedObjs = (AppObject[])cnt.Data;
                    targetNode.DragDrop(draggedObjs);
                    //MacroManager.DragDropNode(targetNode, draggedObjs);
                }

                try
                {
                    IDataObject obj = e.Data;
                    string[] files = (string[])obj.GetData("FileDrop");
                    foreach (string f in files)
                    {
                        string fn = IOTool.GetUniqueFileName(Path.Combine(Root.FileSystemPath, Path.GetFileNameWithoutExtension(f) + ".lnk"));
                        using (StreamWriter sw = new StreamWriter(fn)) sw.Write(f);
                        RefreshRoot();
                        MacroManager.DropFileIntoTree(tree, f);
                    }
                }
                catch (Exception) { }

                tree.SelectedNode = dropNode;
            }
            catch (Exception ex)
            {
                Errors.Report(ex);
            }
        }

        public void RefreshRoot()
        {
            if (Root != null)
            {
                Root.DataRefresh();
                TreeNodeBase.CallRefresh(Root);
            }
        }

        private void tree_DragOver(object sender, DragEventArgs e)
        {
            try
            {
                TreeNode dropNode = tree.GetNodeAt(tree.PointToClient(new Point(e.X, e.Y)));
                DragObjectContainer cnt = (DragObjectContainer)e.Data.GetData(typeof(DragObjectContainer));
                if (cnt != null && dropNode != null && cnt.Data is AppObject[])
                {
                    if (((DATreeNode)dropNode).m_node.AllowDragDrop((AppObject[])cnt.Data))
                    {
                        e.Effect = DragDropEffects.Copy;
                    }
                    else
                    {
                        e.Effect = DragDropEffects.None;
                    }
                }
                string[] files = (string[])e.Data.GetData("FileDrop");
                if (files != null) e.Effect = DragDropEffects.Copy;
            }
            catch (Exception ex)
            {
                Errors.Report(ex);
            }
        }

        public bool FocusedTree
        {
            get { return tree.Focused; } // && m_focused2; }
        }

        private void tree_Click(object sender, EventArgs e)
        {
            if (ActiveNodeChange != null) ActiveNodeChange(sender, e);
        }

        private void tree_Enter(object sender, EventArgs e)
        {
            m_focused2 = true;
            if (TreeEnter != null)
            {
                TreeEnter(sender, e);
            }
            if (TreeBehaviour.InfluenceGlobalTrees) HTree.CallFocusedNodeChanged();
        }

        private void tree_Leave(object sender, EventArgs e)
        {
            m_focused2 = false;
            if (TreeLeave != null)
            {
                TreeLeave(sender, e);
            }
        }

        private void tree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (ActiveNodeChange != null) ActiveNodeChange(this, e);
            UpdateToolbarEnabling();
            OnClick(EventArgs.Empty);
        }

        private void tree_BeforeLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            if (m_lastDoubleClick != null && (DateTime.Now - m_lastDoubleClick.Value).TotalSeconds < 3)
            {
                e.CancelEdit = true;
                return;
            }
            if (!TreeBehaviour.AllowRenameNode)
            {
                e.CancelEdit = true;
                return;
            }
            if (ShowingRenameEditor) return;
            if (e.Node is DATreeNode)
            {
                DATreeNode ada = (DATreeNode)e.Node;
                if (ada.m_node.AllowRename())
                {
                    MainWindow.Instance.RunInMainWindow(ada.ShowRenameEditor);
                    e.CancelEdit = true;
                    return;
                }
            }
            e.CancelEdit = true;
        }

        private void tree_AfterLabelEdit(object sender, NodeLabelEditEventArgs e)
        {
            try
            {
                if (e.Node is DATreeNode)
                {
                    DATreeNode ada = (DATreeNode)e.Node;

                    if (e.Label == null)
                    {
                        ada.Text = ada.m_node.Title;
                        return;
                    }

                    string newname = e.Label;
                    e.CancelEdit = true;// revert to old name
                    ada.Text = ada.m_node.Title;
                    MacroManager.RenameNode(ada.m_node, newname);
                    ada.m_node.RenameNode(newname);
                    TreeNodeExtension.CompleteRefresh(ada.m_node.Parent);
                }
            }
            catch (Exception ex)
            {
                Errors.Report(ex);
            }
        }

        TreeBehaviour b_behaviour;
        public TreeBehaviour TreeBehaviour
        {
            get { return b_behaviour; }
            set
            {
                b_behaviour = value;
                toolStrip1.Visible = !b_behaviour.HideToolbar;
                if (toolStrip1.Visible) toolStrip1.SendToBack();
            }
        }

        private void tree_AfterCheck(object sender, TreeViewEventArgs e)
        {
            var tnode = e.Node as DATreeNode;
            if (tnode != null)
            {
                tnode.DispatchAfterUserCheck();
            }
        }

        public void DispatchAddedNode(DATreeNode node)
        {
            if (AddedNode != null) AddedNode(this, new NodeEventArgs(node));
        }

        public IRealTreeNode FindNode(string path, bool allowPrepareChildren)
        {
            try
            {
                tree.BeginUpdate();
                using (var wc = new WaitContext())
                {
                    string[] p = path.Split('/');
                    if (p[0] != RootPath) return null;
                    IRealTreeNode item = Root.RealNode;
                    for (int i = 1; i < p.Length; i++)
                    {
                        if (item == null) return null;
                        bool found = false;
                        if (allowPrepareChildren && !item.LogicalNode.PreparedChildren)
                        {
                            item.LogicalNode.GetChildrenNow();
                        }
                        item.RefreshChilds(false);
                        foreach (var rchild in item.RealChildren)
                        {
                            if (rchild.LogicalNode.Name == p[i] || rchild.LogicalNode.Name + ".con" == p[i])
                            {
                                item = rchild;
                                found = true;
                                break;
                            }
                        }
                        if (!found)
                        {
                            return null;
                        }
                    }
                    return item;
                }
            }
            finally
            {
                tree.EndUpdate();
            }
        }

        //public void FillAllChildren()
        //{
        //    if (m_root != null) m_root.FillAllChildren();
        //}

        public void SelectAppObject(AppObject appobj, SelectNodeFlags flags)
        {
            string path = appobj.GetTreePath();
            if (path != null)
            {
                SelectNodeByPath(path, flags);
            }
        }

        private void FillAndExpandAll(TreeNodeCollection nodes)
        {
            foreach (var dnode in nodes)
            {
                var node = dnode as DATreeNode;
                if (node == null) continue;
                node.RefreshChilds(false);
                node.Expand();
                FillAndExpandAll(node.Nodes);
            }
        }

        public void FillAndExpandAll()
        {
            FillAndExpandAll(tree.Nodes);
        }

        private void UpdateToolbarEnabling()
        {
            btnRefresh.Enabled = Selected != null;
            btnDelete.Enabled = Selected != null && Selected.AllowDelete();
            btnRename.Enabled = Selected != null && Selected.AllowRename();
            btnCopy.Enabled = Selected != null && Selected.GetValidAppObjects().Any();
            btnNew.Enabled = Root != null && Root.AllowNewChild();
            DragDropBuilder bld = null;
            if (Selected != null && ObjectClipboard.Memory != null) bld = Selected.GetDragDropBuilder(ObjectClipboard.Memory);
            ObjectClipboard.EnableAndFillPasteButton(bld, btnPaste);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (Selected != null) Selected.CompleteRefresh(true);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (Selected != null) Selected.Delete();
        }

        private void btnRename_Click(object sender, EventArgs e)
        {
            if (Selected != null) Selected.RealNode.ShowRenameEditor();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            CallCopy();
        }

        private void CallCopy()
        {
            if (Selected == null) return;
            Selected.CallCopy();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            if (Root != null && Root.AllowNewChild())
            {
                Root.NewChild();
            }
        }
    }

    public enum TreeStyle { TreeView, SelectOne, SelectMany }

    public class NodeEventArgs : EventArgs
    {
        public NodeEventArgs(IRealTreeNode node)
        {
            RealNode = node;
        }
        public readonly IRealTreeNode RealNode;
        public ITreeNode Node { get { return RealNode.LogicalNode; } }
    }
    public delegate void NodeEventHandler(object sender, NodeEventArgs e);
}
