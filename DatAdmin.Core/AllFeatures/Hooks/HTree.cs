using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    [Flags]
    public enum SelectNodeFlags
    {
        Expand = 1,
        FocusTree = 2,
        ScrollInView = 4,
    }

    public static class HTree
    {
        public static event Action<ITreeNode> AfterDeleteNode;
        public static event Action SelectedNodeChanged;
        public static event Action FocusedNodeChanged;
        public static event Action RefreshRoot;
        public static event Action<List<ITreeNode>> GetSelectedNode;
        public static event Action<List<ITreeNode>> GetFocusedNode;
        public static event Action<string, SelectNodeFlags> SelectNode;
        public static event Action<List<IVirtualFile>> FilterFolderFiles;
        //public static event Action<AppObject, SelectNodeFlags> SelectAppObject;

        public static void CallAfterDeleteNode(ITreeNode node)
        {
            if (AfterDeleteNode != null) AfterDeleteNode(node);
        }
        public static void CallSelectedNodeChanged()
        {
            if (SelectedNodeChanged != null) SelectedNodeChanged();
        }
        public static void CallFocusedNodeChanged()
        {
            if (FocusedNodeChanged != null) FocusedNodeChanged();
        }
        public static void CallGetSelectedNode(List<ITreeNode> nodes)
        {
            if (GetSelectedNode != null) GetSelectedNode(nodes);
        }
        public static void CallGetFocusedNode(List<ITreeNode> nodes)
        {
            if (GetFocusedNode != null) GetFocusedNode(nodes);
        }
        public static void CallRefreshRoot()
        {
            if (RefreshRoot != null) RefreshRoot();
        }
        public static ITreeNode CallGetFocusedNode()
        {
            var nodes = new List<ITreeNode>();
            CallGetFocusedNode(nodes);
            if (nodes.Count > 0) return nodes[0];
            return null;
        }
        public static ITreeNode CallGetSelectedNode()
        {
            var nodes = new List<ITreeNode>();
            CallGetSelectedNode(nodes);
            if (nodes.Count > 0) return nodes[0];
            return null;
        }
        public static void CallSelectNode(string path, SelectNodeFlags flags)
        {
            if (SelectNode != null) SelectNode(path, flags);
        }
        public static void CallFilterFolderFiles(List<IVirtualFile> files)
        {
            if (FilterFolderFiles != null) FilterFolderFiles(files);
        }
        //public static void CallSelectAppObject(AppObject appobj, SelectNodeFlags flags)
        //{
        //    if (SelectAppObject != null) SelectAppObject(appobj, flags);
        //}
    }
}
