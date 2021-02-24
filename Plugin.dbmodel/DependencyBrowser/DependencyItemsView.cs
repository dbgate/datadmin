using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DatAdmin;

namespace Plugin.dbmodel
{
    public partial class DependencyItemsView : UserControl
    {
        DependencyItem[] m_items;
        Dictionary<string, int> m_iconIndexes = new Dictionary<string, int>();
        Dictionary<string, ISpecificRepresentation> m_reprs = new Dictionary<string, ISpecificRepresentation>();

        public DependencyItemsView()
        {
            InitializeComponent();
            if (SpecificRepresentationAddonType.Instance != null && SpecificRepresentationAddonType.Instance.CommonSpace != null)
            {
                foreach (var holder in SpecificRepresentationAddonType.Instance.CommonSpace.GetAllAddons())
                {
                    var repr = (ISpecificRepresentation)holder.InstanceModel;
                    if (repr.Icon != null)
                    {
                        m_iconIndexes[repr.ObjectType] = imageList1.Images.Count;
                        imageList1.Images.Add(repr.Icon);
                    }
                    else
                    {
                        m_iconIndexes[repr.ObjectType] = -1;
                    }
                    m_reprs[repr.ObjectType] = repr;
                }
            }
            ToolBarVisible = false;
        }

        public DependencyItem[] Items
        {
            get { return m_items; }
            set
            {
                m_items = value;
                ReloadData();
            }
        }

        private void ReloadData()
        {
            try
            {
                treeView1.BeginUpdate();
                treeView1.Nodes.Clear();
                if (m_items == null) return;
                var dct = new Dictionary<string, TreeNode>();
                foreach (var item in m_items.Sorted())
                {
                    if (!dct.ContainsKey(item.ObjectType))
                    {
                        var node = new TreeNode();
                        node.Text = Texts.Get(m_reprs[item.ObjectType].TitlePlural);
                        node.ImageIndex = m_iconIndexes[item.ObjectType];
                        node.SelectedImageIndex = m_iconIndexes[item.ObjectType];
                        treeView1.Nodes.Add(node);
                        dct[item.ObjectType] = node;
                    }
                    var par = dct[item.ObjectType];
                    var child = new TreeNode();
                    child.Text = item.Name.ToString();
                    child.ImageIndex = m_iconIndexes[item.ObjectType];
                    child.SelectedImageIndex = m_iconIndexes[item.ObjectType];
                    child.Tag = item;
                    par.Nodes.Add(child);
                }
                treeView1.ExpandAll();
            }
            finally
            {
                treeView1.EndUpdate();
            }
        }

        public DependencyItem SelectedItem
        {
            get
            {
                if (treeView1.SelectedNode != null && treeView1.SelectedNode.Tag != null)
                {
                    return (DependencyItem)treeView1.SelectedNode.Tag;
                }
                return null;
            }
            set
            {
                foreach (TreeNode node in treeView1.Nodes)
                {
                    foreach (TreeNode subnode in node.Nodes)
                    {
                        if ((DependencyItem)subnode.Tag == value)
                        {
                            treeView1.SelectedNode = subnode;
                            subnode.EnsureVisible();
                            return;
                        }
                    }
                }
            }
        }

        public event EventHandler ChangeSelectedItem;
        public event EventHandler CallSelect;

        public bool ToolBarVisible
        {
            get
            {
                return panel1.Visible;
            }
            set
            {
                panel1.Visible = value;
            }
        }

        public string Title
        {
            get
            {
                return labTitle.Text;
            }
            set
            {
                labTitle.Text = value;
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if (ChangeSelectedItem != null) ChangeSelectedItem(sender, e);
        }

        private void btnGoTo_Click(object sender, EventArgs e)
        {
            if (CallSelect != null) CallSelect(sender, e);
        }

        private void treeView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (CallSelect != null) CallSelect(sender, e);
        }
    }
}
