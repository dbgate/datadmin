using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class ConfigSelectionFrame : UserControl
    {
        IVirtualFileSystem m_fs;
        ConfigNode m_root;

        public ConfigSelectionFrame()
        {
            InitializeComponent();
        }

        public IVirtualFileSystem FileSystem
        {
            get { return m_fs; }
            set
            {
                m_fs = value;
                if (m_fs != null)
                {
                    m_root = ConfigLoader.LoadConfig(m_fs);
                    daTreeView1.Root = m_root;
                    //daTreeView1.FillAllChildren();
                }
                else
                {
                    daTreeView1.Root = null;
                }
            }
        }

        public ConfigNode Root
        {
            get { return m_root; }
        }

        public List<ConfigNode> GetAllNodes()
        {
            var res = new List<ConfigNode>();
            if (Root != null) Root.GetAllNodes(res);
            return res;
        }

        private void btnCheckAll_Click(object sender, EventArgs e)
        {
            CheckAll();
        }

        private void btnUncheckAll_Click(object sender, EventArgs e)
        {
            foreach (var node in GetAllNodes())
            {
                node.SelectThis(false);
            }
        }

        private void btnExpandAll_Click(object sender, EventArgs e)
        {
            daTreeView1.Tree.ExpandAll();
        }

        private void btnCollapseAll_Click(object sender, EventArgs e)
        {
            daTreeView1.Tree.CollapseAll();
        }

        public void CheckAll()
        {
            foreach (var node in GetAllNodes())
            {
                node.SelectThis(true);
            }
        }
    }
}
