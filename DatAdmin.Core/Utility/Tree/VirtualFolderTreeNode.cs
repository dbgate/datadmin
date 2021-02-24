using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing;
using System.Linq;

namespace DatAdmin
{
    public class VirtualFolderTreeNode : TreeNodeBase
    {
        ITreeNode[] m_children = null;
        Action m_DoGetChildren;
        IVirtualFolder m_folder;

        public VirtualFolderTreeNode(ITreeNode parent, IVirtualFolder folder, string name)
            : base(parent, name)
        {
            m_folder = folder;
            m_DoGetChildren = DoGetChildren;
            SetAppObject(new FolderAppObject { Folder = folder });
        }

        public VirtualFolderTreeNode(string protocol, IVirtualFolder folder)
            : base(protocol)
        {
            m_folder = folder;
            m_DoGetChildren = DoGetChildren;
            SetAppObject(new FolderAppObject { Folder = folder });
        }

        public IVirtualFolder Folder { get { return m_folder; } }

        public override IAsyncResult BeginLoadChildren(AsyncCallback callback)
        {
            //SimpleCallback func = DoGetChildren;
            //return m_DoGetChildren.BeginInvoke(Async.CreateInvokeCallback(RealNode.Invoker, callback), null);
            return m_DoGetChildren.BeginInvoke(callback, null);
            //Async.InvokeVoid(DoGetChildren, RealNode.Invoker, callback);
        }
        public override void EndLoadChildren(IAsyncResult async)
        {
            //SimpleCallback func = DoGetChildren;
            m_DoGetChildren.EndInvoke(async);
        }

        public override bool PreparedChildren
        {
            get { return m_children != null; }
        }

        //public override bool AllowDragDrop(ITreeNode draggingNode)
        //{
        //    return draggingNode is FileTreeNodeBase || draggingNode is VirtualFileTreeNodeBase;
        //}

        //private void DragDropFile(ITreeNode draggingNode, CopyFileMode mode)
        //{
        //    IVirtualFile draggingFile = null;
        //    if (draggingNode is FileTreeNodeBase)
        //    {
        //        FileTreeNodeBase node = (FileTreeNodeBase)draggingNode;
        //        draggingFile = new DiskFile(node.FileSystemPath);
        //    }
        //    if (draggingNode is VirtualFileTreeNodeBase)
        //    {
        //        VirtualFileTreeNodeBase node = (VirtualFileTreeNodeBase)draggingNode;
        //        draggingFile = node.FileObject;
        //    }
        //    if (draggingFile != null)
        //    {
        //        IVirtualFile newfile = m_folder.GetFile(draggingFile.Name);
        //        if (newfile.Exists())
        //        {
        //            if (!StdDialog.ReallyOverwriteFile(newfile.Name)) return;
        //        }
        //        draggingFile.CopyFileTo(newfile, mode);
        //        DataRefresh();
        //        CallRefresh();
        //    }
        //}

        public virtual bool AllowGenericFolderExtenders() { return true; }

        public override bool AllowCreate(string group, string name)
        {
            if (m_parent != null) return m_parent.AllowCreate(group, name);
            return base.AllowCreate(group, name);
        }

        //public override void DragDrop(ITreeNode draggingNode)
        //{
        //    if (draggingNode is FileTreeNodeBase || draggingNode is VirtualFileTreeNodeBase)
        //    {
        //        ContextMenuStripEx menu = new ContextMenuStripEx();
        //        var copy = menu.Items.Add(Texts.Get("s_copy_here"));
        //        copy.Click += (sender, ev) => DragDropFile(draggingNode, CopyFileMode.Copy);
        //        var move = menu.Items.Add(Texts.Get("s_move_here"));
        //        move.Click += (sender, ev) => DragDropFile(draggingNode, CopyFileMode.Move);
        //        menu.ShowOnCursor();
        //    }
        //}

        private void DoGetChildren()
        {
            var loaded = new Dictionary<string, ITreeNode>();
            List<ITreeNode> result = new List<ITreeNode>();
            var files = m_folder.LoadFiles();
            HTree.CallFilterFolderFiles(files);
            files.Sort((a, b) => a.FullPath.Length - b.FullPath.Length);
            foreach (IVirtualFile file in files)
            {
                if (PrefixTest(loaded, file)) continue;

                ITreeNode node = null;
                if (file.DiskPath != null)
                {
                    if (DeletedFileRegistrer.DeletedInvisible.Contains(file.DiskPath.ToLower())) continue;
                    if (DeletedFileRegistrer.DeletedVisible.Contains(file.DiskPath.ToLower())) node = new DeletedPathNode(this, file.DiskPath);
                }

                try
                {
                    if (node == null) node = NodeFactory.FromFile(this, file);
                }
                catch
                {
                    node = new UnknownVirtualFileNode(this, new UnknownFileHandler { FileObject = file });
                }
                //if (node == null) node = new UnknownVirtualFileNode(this, file);

                if (node != null) result.Add(node);
                if (node is VirtualFileTreeNodeBase) loaded[file.Name] = (VirtualFileTreeNodeBase)node;
            }

            var dirs = m_folder.LoadFolders();
            dirs.Sort((a, b) => a.FullPath.Length - b.FullPath.Length);

            foreach (var dir in dirs)
            {
                //if (System.IO.Path.GetFileName(dir).ToLower().StartsWith(".private")) continue;
                if (dir.DiskPath != null && IOTool.IsVersioningDirectory(dir.DiskPath)) continue;
                if (PrefixTest(loaded, dir)) continue;

                ITreeNode node = null;
                if (dir.DiskPath != null)
                {
                    if (DeletedFileRegistrer.DeletedInvisible.Contains(dir.DiskPath.ToLower())) continue;
                    if (DeletedFileRegistrer.DeletedVisible.Contains(dir.DiskPath.ToLower())) node = new DeletedPathNode(this, dir.DiskPath);
                }

                if (node == null)
                {
                    var newnode = new VirtualFolderTreeNode(this, dir, dir.Name);
                    loaded[dir.Name] = newnode;
                    node = newnode;
                }
                result.Add(node);
            }

            result.Sort(CompareNodes);
            PostprocessChildren(result);
            m_children = result.ToArray();
        }

        protected virtual void PostprocessChildren(List<ITreeNode> children) { }

        private int CompareNodes(ITreeNode a, ITreeNode b)
        {
            if (a is VirtualFolderTreeNode && !(b is VirtualFolderTreeNode)) return -1;
            if (b is VirtualFolderTreeNode && !(a is VirtualFolderTreeNode)) return 1;
            return String.Compare(a.Title, b.Title);
        }

        private bool PrefixTest(Dictionary<string, ITreeNode> loaded, IVirtualPath path)
        {
            foreach (string prefix in loaded.Keys)
            {
                if (path.Name.StartsWith(prefix + "."))
                {
                    var obj = loaded[prefix].GetValidAppObjects().FirstWithType<FileAppObject>();
                    if (obj != null)
                    {
                        if (path is IVirtualFile) if (obj.SubFiles != null) obj.SubFiles.Add((IVirtualFile)path);
                        if (path is IVirtualFolder) if (obj.SubFolders != null) obj.SubFolders.Add((IVirtualFolder)path);
                    }
                    return true;
                }
            }
            return false;
        }

        public override string Title
        {
            get { return m_folder.Name; }
        }

        public override bool AllowNewChild()
        {
            return true;
        }

        public override bool NewChild()
        {
            NewWizard();
            return true;
        }

        [PopupMenu("s_new", ImageName = CoreIcons._newName)]
        public void NewWizard()
        {
            CreateDialog dlg = new CreateDialog(this);
            dlg.ShowDialogEx();
            CmdRefresh();
        }

        [PopupMenuVisible("s_browse")]
        public bool BrowseVisible()
        {
            return FileSystemPath != null && Directory.Exists(FileSystemPath);
        }

        [PopupMenu("s_browse", ImageName = CoreIcons.winexploreName, Weight = MenuWeights.OPEN1)]
        public void Browse()
        {
            if (Directory.Exists(FileSystemPath))
            {
                System.Diagnostics.Process.Start(FileSystemPath);
            }
        }

        public override ITreeNode[] GetChildren()
        {
            if (m_children == null) return new ITreeNode[] { };
            return m_children;
        }

        public override void DataRefresh()
        {
            m_children = null;
        }

        public override string TypeTitle
        {
            get { return Title; }
        }

        protected virtual int CompareFileNodes(ITreeNode a, ITreeNode b)
        {
            if (a is VirtualFolderTreeNode && !(b is VirtualFolderTreeNode)) return -1;
            if (b is VirtualFolderTreeNode && !(a is VirtualFolderTreeNode)) return 1;
            return String.Compare(a.Title, b.Title);
        }

        public override string FileSystemPath
        {
            get { return m_folder.DiskPath; }
        }

        public override Bitmap Image
        {
            get { return CoreIcons.img_folder; }
        }

        public override Bitmap ExpandedImage
        {
            get { return CoreIcons.img_folder_expanded; }
        }

        public override bool ContainsDatabaseNode()
        {
            return GetType() == typeof(VirtualFolderTreeNode);
        }
    }

    //[FileHandler(Name = "folderlink")]
    //public class FolderLinkFileHandler : FileHandlerBase
    //{
    //    string m_folder;

    //    public override ITreeNode CreateNode(ITreeNode parent)
    //    {
    //        if (m_folder != null)
    //        {
    //            return new VirtualFolderTreeNode(parent, new DiskFolder(m_file.DiskPath), Path.GetFileName(m_file.DiskPath));
    //        }
    //        return null;
    //    }

    //    public override bool AssignFile(IVirtualFile file)
    //    {
    //        string fn = file.DiskPath;
    //        if (fn == null) return false;
    //        if (!IOTool.FileIsLink(fn)) return false;
    //        string folder = IOTool.GetLinkContent(fn);
    //        if (Directory.Exists(folder))
    //        {
    //            m_file = file;
    //            m_folder = folder;
    //            return true;
    //        }
    //        return false;
    //    }

    //    public override FileHandlerCaps Caps
    //    {
    //        get
    //        {
    //            return new FileHandlerCaps
    //            {
    //                AllFlags = false,
    //                CreateNode = true
    //            };
    //        }
    //    }

    //    public override string Extension
    //    {
    //        get { return "lnk"; }
    //    }
    //}
}
