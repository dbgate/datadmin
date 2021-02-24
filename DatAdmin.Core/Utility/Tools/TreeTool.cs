using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace DatAdmin
{
    public static class TreeTool
    {
        public static string GetProtocol(ITreeNode node)
        {
            int index = node.Path.IndexOf(':');
            if (index >= 0) return node.Path.Substring(0, index);
            return null;
        }

        public static ITreeNode FindNode(TreeView tree, string path)
        {
            int index = path.IndexOf(':');
            if (index >= 0) path = path.Substring(index + 1);
            return FindNode(tree, path.Split('/'));
        }

        public static ITreeNode FindNode(TreeView tree, IEnumerable path)
        {
            TreeNode curnode = tree.Nodes[0];
            foreach (object oelem in path)
            {
                string elem = oelem.ToString();
                if (String.IsNullOrEmpty(elem)) continue;
                TreeNode newcurnode = null;
                foreach (TreeNode child in curnode.Nodes)
                {
                    DATreeNode ada = child as DATreeNode;
                    if (ada != null && ada.m_node.Name == elem)
                    {
                        newcurnode = ada;
                        break;
                    }
                }
                if (newcurnode == null) return null;
                curnode = newcurnode;
            }
            if (curnode is DATreeNode) return ((DATreeNode)curnode).m_node;
            return null;
        }

        public static ITreeNode GetFocusedNode()
        {
            var nodes = new List<ITreeNode>();
            HTree.CallGetFocusedNode(nodes);
            if (nodes.Count > 0) return nodes[0];
            return null;
        }
    }
}
