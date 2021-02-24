using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

namespace DatAdmin
{
    ////[NodeFilter("s_folder")]
    //public class FolderTreeNode : FileSystemTreeNodeBase
    //{
    //    protected string m_filepath;
    //    ITreeNode[] m_children = null;
    //    Action m_DoGetChildren;

    //    public override string FileSystemPath { get { return m_filepath; } }

    //    /// constructor called for non-root item
    //    public FolderTreeNode(ITreeNode parent, string filepath)
    //        : base(parent, System.IO.Path.GetFileName(filepath).ToLower())
    //    {
    //        m_filepath = filepath;
    //        m_DoGetChildren = DoGetChildren;
    //    }

    //    /// constructor called for non-root renamed item
    //    public FolderTreeNode(ITreeNode parent, string filepath, string name)
    //        : base(parent, name)
    //    {
    //        m_filepath = filepath;
    //        m_DoGetChildren = DoGetChildren;
    //    }

    //    /// constructor called for root item
    //    public FolderTreeNode(string protocol, string filepath)
    //        : base(protocol)
    //    {
    //        m_filepath = filepath;
    //        m_DoGetChildren = DoGetChildren;
    //    }

    //    public override string Title
    //    {
    //        get { return System.IO.Path.GetFileName(m_filepath); }
    //    }

    //    public override Bitmap Image
    //    {
    //        get { return CoreIcons.img_folder; }
    //    }

    //    public override Bitmap ExpandedImage
    //    {
    //        get { return CoreIcons.img_folder_expanded; }
    //    }

    //    public override ITreeNode[] GetChildren()
    //    {
    //        if (m_children == null) return new ITreeNode[] { };
    //        return m_children;
    //    }

    //    public virtual bool AllowGenericFolderExtenders() { return true; }

    //    private void DoGetChildren()
    //    {
    //        var loaded = new Dictionary<string, ISubContentTreeNode>();
    //        List<ITreeNode> result = new List<ITreeNode>();
    //        if (Directory.Exists(FileSystemPath))
    //        {
    //            var files = new List<string>(Directory.GetFiles(FileSystemPath));
    //            HTree.CallFilterFolderFiles(files);
    //            files.Sort((a, b) => a.Length - b.Length);
    //            foreach (string file in files)
    //            {
    //                if (PrefixTest(loaded, file, false)) continue;
    //                //ITreeNode node = NodeFactory.FromFile(this, System.IO.Path.Combine(FileSystemPath, System.IO.Path.GetFileName(dstfile)));

    //                ITreeNode node = null;
    //                if (DeletedFileRegistrer.DeletedInvisible.Contains(file.ToLower())) continue;
    //                if (DeletedFileRegistrer.DeletedVisible.Contains(file.ToLower())) node = new DeletedPathNode(this, file);
    //                if (node == null) node = NodeFactory.FromFile(this, file);
    //                if (node == null) node = NodeFactory.FromVirtualFile(this, new DiskFile(file));
    //                if (node == null) node = new UnknownFileNode(this, file);
    //                if (node is ISubContentTreeNode) loaded[file] = (ISubContentTreeNode)node;
    //                result.Add(node);
    //            }

    //            string[] dirs = Directory.GetDirectories(FileSystemPath);
    //            Array.Sort(dirs, (a, b) => a.Length - b.Length);
    //            foreach (string dir in dirs)
    //            {
    //                //if (System.IO.Path.GetFileName(dir).ToLower().StartsWith(".private")) continue;
    //                if (IOTool.IsVersioningDirectory(dir)) continue;
    //                if (PrefixTest(loaded, dir, true)) continue;

    //                ITreeNode node = null;
    //                if (DeletedFileRegistrer.DeletedInvisible.Contains(dir.ToLower())) continue;
    //                if (DeletedFileRegistrer.DeletedVisible.Contains(dir.ToLower())) node = new DeletedPathNode(this, dir);

    //                if (node == null)
    //                {
    //                    var newnode = new FolderTreeNode(this, System.IO.Path.Combine(m_filepath, System.IO.Path.GetFileName(dir)));
    //                    loaded[dir] = newnode;
    //                    node = newnode;
    //                }
    //                result.Add(node);
    //            }
    //        }
    //        result.Sort(CompareFileNodes);
    //        PostprocessChildren(result);
    //        m_children = result.ToArray();
    //    }

    //    protected virtual void PostprocessChildren(List<ITreeNode> children) { }
        
    //    protected virtual int CompareFileNodes(ITreeNode a, ITreeNode b)
    //    {
    //        if (a is FolderTreeNode && !(b is FolderTreeNode)) return -1;
    //        if (b is FolderTreeNode && !(a is FolderTreeNode)) return 1;
    //        return String.Compare(a.Title, b.Title);
    //    }

    //    private bool PrefixTest(Dictionary<string, ISubContentTreeNode> loaded, string name, bool isfolder)
    //    {
    //        foreach (string prefix in loaded.Keys)
    //        {
    //            if (name.StartsWith(prefix + "."))
    //            {
    //                var node = loaded[prefix];
    //                if (isfolder)
    //                {
    //                    node.AddSubFolder(IOTool.RelativePathTo(m_filepath, name));
    //                }
    //                else
    //                {
    //                    node.AddSubFile(IOTool.RelativePathTo(m_filepath, name));
    //                }
    //                return true;
    //            }
    //        }
    //        return false;
    //    }

    //    public override IAsyncResult BeginLoadChildren(AsyncCallback callback)
    //    {
    //        //SimpleCallback func = DoGetChildren;
    //        //return m_DoGetChildren.BeginInvoke(Async.CreateInvokeCallback(RealNode.Invoker, callback), null);
    //        return m_DoGetChildren.BeginInvoke(callback, null);
    //        //Async.InvokeVoid(DoGetChildren, RealNode.Invoker, callback);
    //    }
    //    public override void EndLoadChildren(IAsyncResult async)
    //    {
    //        //SimpleCallback func = DoGetChildren;
    //        m_DoGetChildren.EndInvoke(async);
    //    }

    //    public override bool PreparedChildren
    //    {
    //        get { return m_children != null; }
    //    }

    //    public override void DataRefresh()
    //    {
    //        m_children = null;
    //    }

    //    public override bool AllowDelete()
    //    {
    //        return Parent != null;
    //    }

    //    [PopupMenu("s_new", ImageName = CoreIcons._newName, Weight = MenuWeights.NEWOBJECT)]
    //    public void NewWizard()
    //    {
    //        CreateDialog dlg = new CreateDialog(this);
    //        dlg.ShowDialogEx();
    //        CmdRefresh();
    //    }

    //    [PopupMenuEnabled("s_browse")]
    //    public bool BrowseEnabled()
    //    {
    //        return Directory.Exists(FileSystemPath);
    //    }

    //    [PopupMenu("s_browse", ImageName = CoreIcons.winexploreName, Weight = MenuWeights.OPEN1)]
    //    public void Browse()
    //    {
    //        if (Directory.Exists(FileSystemPath))
    //        {
    //            System.Diagnostics.Process.Start(FileSystemPath);
    //        }
    //    }

    //    public override bool DoDelete()
    //    {
    //        if (StdDialog.YesNoDialog("s_really_delete$folder", "folder", FileSystemPath))
    //        {
    //            try
    //            {
    //                Directory.Delete(FileSystemPath, true);
    //            }
    //            catch (Exception err)
    //            {
    //                StdDialog.ShowError("s_path_cannot_be_deleted_delete_scheduled");
    //                DeletedFileRegistrer.AddPath(m_filepath, true);
    //                throw new DeleteError(FileSystemPath, err);
    //            }
    //            return true;
    //        }
    //        return false;
    //    }

    //    public override string TypeTitle
    //    {
    //        get { return "s_folder"; }
    //    }

    //    //public override bool AllowDragDrop(ITreeNode draggingNode)
    //    //{
    //    //    return draggingNode is FileTreeNodeBase || draggingNode is VirtualFileTreeNodeBase || draggingNode is IDatabaseTreeNode;
    //    //}

    //    [DragDropOperationVisible(Name = "movefile")]
    //    [DragDropOperationVisible(Name = "copyfile")]
    //    public virtual bool DragDrop_FileVisible(AppObject appobj)
    //    {
    //        return appobj is FileAppObject || appobj is VirtualFileAppObject;
    //    }

    //    [DragDropOperation(Name = "movefile", Title = "s_move_here")]
    //    public void DragDrop_MoveFile(AppObject appobj)
    //    {
    //        DragDropFile(appobj, CopyFileMode.Move);
    //    }

    //    [DragDropOperation(Name = "copyfile", Title = "s_copy_here")]
    //    public void DragDrop_CopyFile(AppObject appobj)
    //    {
    //        DragDropFile(appobj, CopyFileMode.Copy);
    //    }

    //    [DragDropOperation(Name = "create_data_archive", Title = "s_create_data_archive")]
    //    public void DragDrop_CreateDataArchive(AppObject appobj)
    //    {
    //        var dobj = appobj as DatabaseAppObject;
    //        if (dobj == null) return;
    //        string dbname = dobj.DatabaseName;
    //        string fn = System.IO.Path.Combine(FileSystemPath, (dbname ?? "") + ".dbk");
    //        CopyDbWizard.Run(dobj.FindDatabaseConnection(ConnPack).CloneSource(),
    //            new DataArchiveWriter { FilePlace = FilePlaceAddonType.PlaceFromVirtualFile(fn) }
    //            );
    //    }

    //    [DragDropOperationVisible(Name = "create_data_archive")]
    //    [DragDropOperationVisible(Name = "save_structure")]
    //    public bool DragDrop_DbVisible(AppObject appobj)
    //    {
    //        return appobj is DatabaseAppObject;
    //    }

    //    private void DragDropFile(AppObject appobj, CopyFileMode mode)
    //    {
    //        if (appobj is FileAppObject)
    //        {
    //            var fobj = (FileAppObject)appobj;
    //            string newpath = System.IO.Path.Combine(this.FileSystemPath, System.IO.Path.GetFileName(fobj.FilePath));
    //            if (System.IO.File.Exists(newpath))
    //            {
    //                if (!StdDialog.ReallyOverwriteFile(newpath)) return;
    //            }

    //            node.CopyFileTo(newpath, mode);
    //            DataRefresh();
    //            CallRefresh();
    //        }
    //        if (draggingNode is VirtualFileTreeNodeBase)
    //        {
    //            VirtualFileTreeNodeBase node = (VirtualFileTreeNodeBase)draggingNode;
    //            string newpath = System.IO.Path.Combine(this.FileSystemPath, System.IO.Path.GetFileName(node.FileObject.Name));
    //            if (System.IO.File.Exists(newpath))
    //            {
    //                if (!StdDialog.ReallyOverwriteFile(newpath)) return;
    //            }
    //            node.CopyFileTo(new DiskFile(newpath), mode);
    //            //using (FileStream fw = new FileStream(newpath, FileMode.Create))
    //            //{
    //            //    byte[] data = node.FileObject.GetBinary();
    //            //    fw.Write(data, 0, data.Length);
    //            //}
    //            DataRefresh();
    //            CallRefresh();
    //        }
    //    }

    //    //public override void DragDrop(ITreeNode draggingNode)
    //    //{
    //    //    if (draggingNode is FileTreeNodeBase || draggingNode is VirtualFileTreeNodeBase)
    //    //    {
    //    //        ContextMenuStrip menu = new ContextMenuStrip();
    //    //        var copy = menu.Items.Add(Texts.Get("s_copy_here"));
    //    //        copy.Click += (sender, ev) => DragDropFile(draggingNode, CopyFileMode.Copy);
    //    //        var move = menu.Items.Add(Texts.Get("s_move_here"));
    //    //        move.Click += (sender, ev) => DragDropFile(draggingNode, CopyFileMode.Move);
    //    //        menu.ShowOnCursor();
    //    //    }
    //    //}

    //    public override string GetPrivateSubFolder(string name)
    //    {
    //        return System.IO.Path.Combine(FileSystemPath, "." + name);
    //    }

    //    public override bool AllowCreate(string group, string name)
    //    {
    //        if (m_parent != null) return m_parent.AllowCreate(group, name);
    //        return base.AllowCreate(group, name);
    //    }

    //    public override bool AllowRename()
    //    {
    //        return true;
    //    }

    //    public override string GetRenamingText()
    //    {
    //        return System.IO.Path.GetFileName(m_filepath);
    //    }

    //    public virtual void DoRenameFolder(string newname)
    //    {
    //        Directory.Move(m_filepath,
    //            System.IO.Path.Combine(
    //                System.IO.Path.GetDirectoryName(m_filepath),
    //                newname));
    //    }

    //    public override void RenameNode(string newname)
    //    {
    //        DoRenameFolder(newname);
    //    }
    //}

    ////public class PrivateFolderTreeNodeBase : FolderTreeNode
    ////{
    ////    public PrivateFolderTreeNodeBase(ITreeNode parent, string name)
    ////        : base(parent, parent.GetPrivateSubFolder(name))
    ////    {
    ////    }
    ////}

    //[NodeFactory(Name = "folder")]
    //public class FolderNodeFactory : NodeFactoryBase
    //{
    //    public override ITreeNode FromFile(ITreeNode parent, string file)
    //    {
    //        if (file.ToLower().EndsWith(".lnk"))
    //        {
    //            string folder = IOTool.GetLinkContent(file);
    //            if (Directory.Exists(folder))
    //            {
    //                return new FolderTreeNode(parent, folder);
    //            }
    //        }
    //        return null;
    //    }
    //}
}


