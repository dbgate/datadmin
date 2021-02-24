using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class SettingsPageFrame : UserControl
    {
        SettingsPageCollection m_pages;
        public SettingsTargets Target;
        ImageCache m_imgCache;

        public SettingsPageFrame()
        {
            InitializeComponent();
            m_imgCache = new ImageCache(imageList1, SystemColors.ButtonFace);
        }

        public SettingsPageCollection Pages
        {
            get { return m_pages; }
            set
            {
                if (m_pages != null) m_pages.EndEdit();
                m_pages = value;
                if (m_pages != null) m_pages.BeginEdit();
                FillPages();
            }
        }

        private TreeNode FindOrCreate(TreeNode parent, TreeNodeCollection parentNodes, string[] subpath, SettingsPageStruct page)
        {
            if (subpath.Length == 0) return parent;
            //TreeNode[] childs = parentNodes.Find(Texts.Get(subpath[0]), false);
            TreeNode child = null;
            foreach (TreeNode n in parentNodes)
            {
                if (n.Text == Texts.Get(subpath[0]))
                {
                    child = n;
                    break;
                }
            }
            //if (childs.Length > 0) child = childs[0];
            if (child != null)
            {
                return FindOrCreate(child, child.Nodes, PyList.SliceFrom(subpath, 1), page);
            }
            child = new TreeNode();
            if (page != null && page.Attribute.ImageName != null) child.ImageIndex = m_imgCache.GetImageIndex(ImageTool.ImageFromName(page.Attribute.ImageName, CoreIcons.settings));
            else child.ImageIndex = m_imgCache.GetImageIndex(CoreIcons.settings);
            child.SelectedImageIndex = child.ImageIndex;
            parentNodes.Add(child);
            child.Text = Texts.Get(subpath[0]);
            if (subpath.Length == 1)
            {
                child.Tag = page;
                if ((page.Attribute.Targets & Target) == 0) parentNodes.Remove(child);
            }
            return FindOrCreate(child, child.Nodes, PyList.SliceFrom(subpath, 1), page);
        }

        private void DeleteEmptySubNodes(TreeNodeCollection nodes)
        {
            List<TreeNode> rem = new List<TreeNode>();
            foreach (TreeNode node in nodes)
            {
                DeleteEmptySubNodes(node.Nodes);
                if (node.Nodes.Count == 0 && node.Tag == null) rem.Add(node);
            }
            foreach (var node in rem) nodes.Remove(node);
        }

        private void FillPages()
        {
            treeView1.Nodes.Clear();
            propertyFrame1.SelectedObject = null;
            if (m_pages == null) return;
            foreach (var pg in m_pages.SettingsPages)
            {
                FindOrCreate(null, treeView1.Nodes, pg.Attribute.Title.Split('/'), pg);
            }
            if (treeView1.Nodes.Count > 0)
            {
                treeView1.SelectedNode = treeView1.Nodes[0];
            }
            DeleteEmptySubNodes(treeView1.Nodes);
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            var node = treeView1.SelectedNode;
            if (node == null)
            {
                propertyFrame1.SelectedObject = null;
                return;
            }
            while (node.Nodes.Count > 0 && node.Tag == null)
            {
                node = node.Nodes[0];
            }
            if (node.Tag != null)
            {
                propertyFrame1.SelectedObject = ((SettingsPageStruct)node.Tag).SettingsPage;
            }
            else
            {
                propertyFrame1.SelectedObject = null;
            }
        }

        public void SelectSettingsPage(string cfgpath)
        {
            treeView1.SelectedNode = FindOrCreate(null, treeView1.Nodes, cfgpath.Split('/'), null);
        }
    }
}
