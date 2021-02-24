using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace DatAdmin
{
    public abstract class VirtualFileTreeNodeBase : TreeNodeBase
    {
        protected IVirtualFile m_file;
        AppObject m_fileAppobj;

        public VirtualFileTreeNodeBase(ITreeNode parent, IFileHandler fhandler)
            : base(parent, fhandler.FileObject.Name.ToLower() + fhandler.FileObject.GetNodeNamePostfix())
        {
            m_fileAppobj = FileAppObject.FromHandler(fhandler);
            SetAppObject(m_fileAppobj);
            m_file = fhandler.FileObject;
        }
        public override string Title
        {
            get
            {
                return m_file.Name;
            }
        }

        //public FileAppObject AppObject { get { return (FileAppObject)m_primaryAppobj; } }

        //public override bool DoDelete()
        //{
        //    if (StdDialog.ReallyDeleteFile(m_file.DiskPath ?? m_file.ToString()))
        //    {
        //        m_fileAppobj.DoDelete();
        //        return true;
        //    }
        //    return false;
        //}

        public IVirtualFile FileObject { get { return m_file; } }

        public override string FileSystemPath
        {
            get { return m_file.DiskPath; }
        }

        //#region ISubContentTreeNode Members

        //public void AddSubFile(string relpath)
        //{
        //    SubVirtFiles.Add(m_file.Parent.GetFile(relpath));
        //}

        //public void AddSubFolder(string relpath)
        //{
        //}

        //#endregion
    }

    public class UnknownVirtualFileNode : VirtualFileTreeNodeBase
    {
        public UnknownVirtualFileNode(ITreeNode parent, IFileHandler fhandler)
            : base(parent, fhandler)
        {
        }

        public override string TypeTitle
        {
            get { return "s_file"; }
        }

        public override ITreeNode[] GetChildren()
        {
            return new ITreeNode[] { };
        }

        public override Bitmap Image
        {
            get { return CoreIcons.unknownfile; }
        }
    }
}
