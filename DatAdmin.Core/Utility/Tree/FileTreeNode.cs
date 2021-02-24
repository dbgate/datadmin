using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.IO;

namespace DatAdmin
{
    //public abstract class FileSystemTreeNodeBase : TreeNodeBase, ISubContentTreeNode
    //{
    //    protected FileSystemTreeNodeBase(ITreeNode parent, string name)
    //        : base(parent, name)
    //    {
    //        Init();
    //    }

    //    protected FileSystemTreeNodeBase(string protocol)
    //        : base(protocol)
    //    {
    //        Init();
    //    }

    //    private void Init()
    //    {
    //        SubFiles = new List<string>();
    //        SubFolders = new List<string>();
    //    }

    //    public List<string> SubFiles { get; set; }
    //    public List<string> SubFolders { get; set; }

    //    public void AddSubFile(string relpath)
    //    {
    //        SubFiles.Add(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(FileSystemPath), relpath));
    //    }
    //    public void AddSubFolder(string relpath)
    //    {
    //        SubFolders.Add(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(FileSystemPath), relpath));
    //    }

    //    protected string GetSubContentText()
    //    {
    //        StringBuilder sb = new StringBuilder();
    //        foreach (string path in SubFolders) sb.AppendLine("[" + path + "]");
    //        foreach (string path in SubFiles) sb.AppendLine(path);
    //        return sb.ToString();
    //    }

    //    protected void DeleteSubContent()
    //    {
    //        foreach (string path in SubFiles)
    //        {
    //            try
    //            {
    //                File.Delete(path);
    //            }
    //            catch
    //            {
    //                DeletedFileRegistrer.AddPath(path, false);
    //            }
    //        }
    //        foreach (string path in SubFolders)
    //        {
    //            try
    //            {
    //                Directory.Delete(path, true);
    //            }
    //            catch
    //            {
    //                DeletedFileRegistrer.AddPath(path, false);
    //            }
    //        }
    //    }
    //}

    ////[NodeFilter("s_file")]
    //public abstract class FileTreeNodeBase : FileSystemTreeNodeBase
    //{
    //    protected string m_filepath;

    //    public FileTreeNodeBase(ITreeNode parent, string filepath)
    //        : base(parent, System.IO.Path.GetFileName(filepath).ToLower())
    //    {
    //        m_filepath = filepath;
    //        SetAppObject(new FileAppObject { FilePath = filepath });
    //    }
    //    public override string Title
    //    {
    //        get
    //        {
    //            return System.IO.Path.GetFileName(m_filepath);
    //        }
    //    }

    //    public override string FileSystemPath { get { return m_filepath; } }

    //    protected virtual string GetConfirmDeleteMessage()
    //    {
    //        return Texts.Get("s_really_delete$file", "file", m_filepath) + "\n" + GetSubContentText();
    //    }

    //    public override bool DoDelete()
    //    {
    //        if (MessageBox.Show(GetConfirmDeleteMessage(), "DatAdmin", MessageBoxButtons.YesNo) == DialogResult.Yes)
    //        {
    //            try { DoDeleteFile(); }
    //            catch (DeleteError) { } // it's OK

    //            DeleteSubContent();
    //            return true;
    //        }
    //        return false;
    //    }

    //    public override bool AllowDelete()
    //    {
    //        return true;
    //    }

    //    public virtual void DoDeleteFile()
    //    {
    //        try
    //        {
    //            File.Delete(m_filepath);
    //        }
    //        catch (Exception err)
    //        {
    //            StdDialog.ShowError("s_path_cannot_be_deleted_delete_scheduled");
    //            DeletedFileRegistrer.AddPath(m_filepath, true);
    //            throw new DeleteError(m_filepath, err);
    //        }
    //    }

    //    public override bool AllowRename()
    //    {
    //        return true;
    //    }

    //    public override string GetRenamingText()
    //    {
    //        return System.IO.Path.GetFileNameWithoutExtension(m_filepath);
    //    }

    //    public virtual void DoRenameFile(string newname)
    //    {
    //        CopyFileTo(
    //            System.IO.Path.Combine(
    //                System.IO.Path.GetDirectoryName(m_filepath),
    //                System.IO.Path.ChangeExtension(newname, System.IO.Path.GetExtension(m_filepath)))
    //        , CopyFileMode.Move);
    //    }

    //    public override void RenameNode(string newname)
    //    {
    //        DoRenameFile(newname);
    //    }

    //    public override string GetPrivateSubFolder(string name)
    //    {
    //        return m_filepath + "." + name;
    //    }

    //    [PopupMenu("s_create_copy", ImageName = CoreIcons.copyName, Weight = MenuWeights.DUPLICATE)]
    //    public void CreateCopy()
    //    {
    //        string dir = System.IO.Path.GetDirectoryName(m_filepath);
    //        string ext = System.IO.Path.GetExtension(m_filepath);
    //        string newname = InputBox.Run(Texts.Get("s_name_of_copy"), System.IO.Path.GetFileNameWithoutExtension(m_filepath));
    //        if (newname != null)
    //        {
    //            CopyFileTo(System.IO.Path.Combine(dir, newname + ext), CopyFileMode.Copy);
    //            //File.Copy(m_filepath, System.IO.Path.Combine(dir, newname + ext));
    //            Parent.DataRefresh();
    //            TreeNodeBase.CallRefresh(Parent);
    //        }
    //    }

    //    public static string GetNamePostfix(string name, string prefix)
    //    {
    //        return name.Substring(prefix.Length);
    //    }

    //    public void CopyFileTo(string newpath, CopyFileMode mode)
    //    {
    //        CopySystemFile(m_filepath, newpath, mode);
    //        foreach (string subfile in SubFiles)
    //        {
    //            CopySystemFile(subfile, newpath + GetNamePostfix(subfile, m_filepath), mode);
    //        }
    //        foreach (string subdir in SubFolders)
    //        {
    //            CopySystemFolder(subdir, newpath + GetNamePostfix(subdir, m_filepath), mode);
    //        }
    //    }

    //    private void CopySystemFolder(string origpath, string newpath, CopyFileMode mode)
    //    {
    //        switch (mode)
    //        {
    //            case CopyFileMode.Copy:
    //                IOTool.CopyDirectory(origpath, newpath);
    //                break;
    //            case CopyFileMode.Move:
    //                Directory.Move(origpath,newpath);
    //                break;
    //        }
    //    }

    //    private static void CopySystemFile(string origpath, string newpath, CopyFileMode mode)
    //    {
    //        try { Directory.CreateDirectory(System.IO.Path.GetDirectoryName(newpath)); }
    //        catch { }
    //        switch (mode)
    //        {
    //            case CopyFileMode.Copy:
    //                System.IO.File.Copy(origpath, newpath, true);
    //                break;
    //            case CopyFileMode.Move:
    //                if (File.Exists(newpath)) File.Delete(newpath);
    //                System.IO.File.Move(origpath, newpath);
    //                break;
    //        }
    //    }
    //}

    //public class UnknownFileNode : FileTreeNodeBase
    //{
    //    public UnknownFileNode(ITreeNode parent, string filepath)
    //        : base(parent, filepath)
    //    {
    //    }

    //    public override string TypeTitle
    //    {
    //        get { return "s_file"; }
    //    }

    //    public override ITreeNode[] GetChildren()
    //    {
    //        return new ITreeNode[] { };
    //    }

    //    public override Bitmap Image
    //    {
    //        get { return CoreIcons.question; }
    //    }
    //}

    public class DeletedPathNode : TreeNodeBase
    {
        string m_path;
        public DeletedPathNode(ITreeNode parent, string path)
            : base(parent, System.IO.Path.GetFileName(path))
        {
            m_path = path;
        }

        public override string Title
        {
            get { return System.IO.Path.GetFileName(m_path); }
        }

        public override string TypeTitle
        {
            get { return "s_deleted"; }
        }

        public override Bitmap Image
        {
            get { return CoreIcons.deleted_file; }
        }

        public override ITreeNode[] GetChildren()
        {
            return new ITreeNode[] { };
        }
    }
}
