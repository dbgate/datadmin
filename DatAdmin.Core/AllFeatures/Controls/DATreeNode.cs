using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Drawing;

namespace DatAdmin
{
    public class DATreeNode : TreeNode, IRealTreeNode
    {
        internal ITreeNode m_node;
        DATreeView m_tree;
        bool m_filledChildren = false;
        //ContextMenu m_menu;
        internal ContextMenuStrip m_menu;
        bool m_expanding;
        Dictionary<string, DATreeNode> m_oldnodes = null;
        bool m_isDead = false;
        bool m_machineCheck = false;

        public DATreeNode(ITreeNode node, DATreeView tree)
        {
            m_tree = tree;
            m_node = node;
            node.RealNode = this;
            Text = Texts.Get(node.Title);
            //UpdateImageIndex();
            m_menu = new ContextMenuStripEx();
            //this.ContextMenuStrip = m_menu;
            //m_menu.Opening += new System.ComponentModel.CancelEventHandler(m_menu_Opening);
            tree.DispatchAddedNode(this);
        }

        public void WantChildren()
        {
            if (m_node.PreparedChildren)
            {
                FillChildren();
            }
            else
            {
                Nodes.Clear();
                Nodes.Add("__dummy__");
            }
        }

        public void ClearChilds()
        {
            Nodes.Clear();
            m_filledChildren = false;
        }
        
        public void ExpandNode()
        {
            WantChildren();
            Expand();
        }

        public void CollapseNode()
        {
            Collapse();
        }

        public void EnsureVisibleNode()
        {
            EnsureVisible();
        }

        public void ShowRenameEditor()
        {
            try
            {
                m_tree.ShowingRenameEditor = true;
                Text = m_node.GetRenamingText();
                BeginEdit();
            }
            finally
            {
                m_tree.ShowingRenameEditor = false;
            }
        }

        //void m_menu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        //{
        //    m_menu.Items.Clear();
        //    m_node.GetPopupMenu(new MenuBuilder(m_menu.Items));
        //}

        //void m_menu_Popup(object sender, EventArgs e)
        //{
        //    //m_menu.MenuItems.Clear();
        //    m_menu.Items.Clear();
        //    m_node.GetPopupMenu(new PopupMenuBuilder(m_menu, m_tree));
        //}

        public bool AllowExpand()
        {
            if (!m_node.AllowExpand()) return false;
            MacroManager.ExpandNode(m_node);
            if (m_filledChildren)
            {
                m_node.NotifyExpanded();
                return true;
            }
            if (!m_node.PreparedChildren)
            {
                ProcessRegister.AddBackgroundTask("s_expand");
                m_node.BeginLoadChildren(Async.CreateInvokeCallback(Invoker, OnPreparedExpand));
                m_expanding = true;
                RefreshSelf();
                return false;
            }
            FillChildren();
            m_node.NotifyExpanded();
            return true;
        }

        private void OnPreparedExpand(IAsyncResult res)
        {
            try
            {
                try
                {
                    m_node.EndLoadChildren(res);
                    m_tree.Tree.BeginUpdate();
                    m_expanding = false;
                    FillChildren();
                    Expand();
                    RefreshSelf();
                    m_node.NotifyExpanded();
                }
                finally
                {
                    m_tree.Tree.EndUpdate();
                }
            }
            catch (Exception e)
            {
                Errors.Report(e);
            }
            ProcessRegister.RemoveBackgroundTask("s_expand");
        }

        private void FillChildren()
        {
            if (m_filledChildren) return;
            m_filledChildren = true;
            Nodes.Clear();
            //List<TreeNode> nodes = new List<TreeNode>();
            List<ITreeNode> children = new List<ITreeNode>();
            children.AddRange(m_node.GetChildren());
            foreach (var item in TreeExtenderAddonType.Instance.CommonSpace.GetAllAddons())
            {
                var ext = (ITreeExtender)item.InstanceModel;
                try
                {
                    ext.GetExtendedChildren(m_node, children);
                }
                catch (Exception e)
                {
                    Logging.Warning("Error generating node:" + e.ToString());
                }
            }
            Dictionary<string, bool> usedKeys = new Dictionary<string, bool>();
            foreach (ITreeNode child in children)
            {
                if (!m_tree.FilterNode(child)) continue;
                if (child.AllowReuse() && m_oldnodes != null && m_oldnodes.ContainsKey(NodeKey(child)) && !usedKeys.ContainsKey(NodeKey(child)))
                {
                    usedKeys[NodeKey(child)] = true;
                    Nodes.Add(m_oldnodes[NodeKey(child)]);
                }
                else
                {
                    var danode = new DATreeNode(child, m_tree);
                    Nodes.Add(danode);
                    if (IsExpanded)
                    {
                        danode.WantChildren();
                        danode.UpdateImageIndex();
                    }
                }
            }
            //Nodes.AddRange(nodes.ToArray());
            m_oldnodes = null;
        }

        internal void UpdateImageIndex()
        {
            Bitmap img = null;
            foreach (var addon in TreeExtenderAddonType.Instance.CommonSpace.GetAllAddons())
            {
                var ex = (ITreeExtender)addon.InstanceModel;
                if (img == null) img = ex.GetImageOverride(m_node);
            }
            if (img == null)
            {
                if (IsExpanded && m_node.ExpandedImage != null) img = m_node.ExpandedImage;
                else img = m_node.Image;
            }
            int index = m_tree.GetImageIndex(img);
            ImageIndex = index;
            SelectedImageIndex = index;
        }

        internal void AfterExpand()
        {
            UpdateImageIndex();
            foreach (var child in Nodes) ((DATreeNode)child).UpdateImageIndex();
        }

        internal void AfterCollapse()
        {
            UpdateImageIndex();
        }

        internal bool DoubleClick()
        {
            return m_node.DoubleClick();
        }

        private static string NodeKey(ITreeNode node)
        {
            return node.Name + "@@" + node.GetType().FullName;
        }

        #region IRealTreeNode Members

        public TreeBehaviour TreeBehaviour { get { return m_tree.TreeBehaviour; } }

        public bool IsDead { get { return m_isDead; } }
        public void KillNode() { m_isDead = true; }

        public void RefreshChilds(bool deep)
        {
            bool wasexpanded = IsExpanded;
            m_oldnodes = new Dictionary<string, DATreeNode>();
            foreach (TreeNode node in Nodes)
            {
                if (!(node is DATreeNode)) continue; // dummy node
                DATreeNode ada = (DATreeNode)node;
                if (ada.IsDead) continue;
                if (!deep) m_oldnodes[NodeKey(ada.m_node)] = ada;
            }
            Nodes.Clear();
            m_filledChildren = false;

            if (m_node.PreparedChildren)
            {
                FillChildren();
            }
            else
            {
                Nodes.Add("__dummy__");
                Collapse();
            }
            if (wasexpanded) Expand();
        }

        public void RefreshSelf()
        {
            string text = Texts.Get(m_node.Title);
            if (m_expanding) text += String.Format("({0}...)", Texts.Get("s_expanding"));
            Text = text;
            UpdateImageIndex();
        }

        public IInvoker Invoker
        {
            get { return m_tree.m_invoker; }
        }

        public IRealTreeNode ChildByName(string name)
        {
            foreach (TreeNode child in Nodes)
            {
                var tnode = child as DATreeNode;
                if (tnode != null && tnode.m_node.Name == name) return tnode;
            }
            return null;
        }

        public IEnumerable<IRealTreeNode> RealChildren
        {
            get
            {
                foreach (TreeNode child in Nodes)
                {
                    if (child is DATreeNode) yield return (DATreeNode)child;
                }
            }
        }

        public ITreeNode LogicalNode { get { return m_node; } }

        public bool NodeChecked
        {
            get { return Checked; }
            set
            {
                m_machineCheck = true;
                Checked = value;
                m_machineCheck = false;
            }
        }

        public Color NodeTextColor
        {
            get { return ForeColor; }
            set { ForeColor = value; }
        }

        #endregion

        internal void CloseAllResources()
        {
            m_node.CloseAllResources();
            foreach (TreeNode child in Nodes)
            {
                var ada = child as DATreeNode;
                if (ada != null) ada.CloseAllResources();
            }
        }

        internal void DispatchAfterUserCheck()
        {
            if (m_machineCheck) return;
            m_node.AfterUserCheck();
        }

        public void FillPopupMenu()
        {
            m_menu.Items.Clear();
            var mb = new MenuBuilder();
            m_node.GetPopupMenu(mb);
            mb.GetMenuItems(m_menu.Items);
        }

        //public void FillAllChildren()
        //{
        //    WantChildren();
        //    foreach (DATreeNode node in Nodes)
        //    {
        //        node.FillAllChildren();
        //    }
        //}

        internal void ModifiedDoubleClick(Keys keys)
        {
            m_node.ModifiedDoubleClick(keys);
        }
    }
}
