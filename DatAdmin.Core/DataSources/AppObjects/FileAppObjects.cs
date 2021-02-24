using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace DatAdmin
{
    public abstract class FileAppObjectBase : AppObject
    {
        public abstract IVirtualFile GetFile();
        public virtual List<IVirtualFile> GetSubFiles() { return new List<IVirtualFile>(); }
        public virtual List<IVirtualFolder> GetSubFolders() { return new List<IVirtualFolder>(); }
        protected IFileHandler m_fileHandler;

        protected override string GetConfirmDeleteMessage()
        {
            return base.GetConfirmDeleteMessage();
        }

        private void SaveRemove(IVirtualPath path)
        {
            try { path.Remove(); }
            catch (DeleteError) { }
        }

        public override void DoDelete()
        {
            FileHandler.BeforeDelete();
            SaveRemove(GetFile());
            foreach (var sub in GetSubFiles()) SaveRemove(sub);
            foreach (var sub in GetSubFolders()) SaveRemove(sub);
        }

        public override bool AllowRename()
        {
            return GetFile().Caps.Rename;
        }

        public override bool AllowDelete()
        {
            return GetFile().Caps.Remove;
        }

        public override void RenameObject(string newname)
        {
            FileHandler.BeforeRename();
            DoRenameFile(newname);
        }

        public override string GetRenamingText()
        {
            return System.IO.Path.GetFileNameWithoutExtension(GetFile().Name);
        }

        public virtual void DoRenameFile(string newname)
        {
            string newfullname = newname + System.IO.Path.GetExtension(GetFile().Name);
            foreach (var sub in GetSubFiles()) sub.RenameTo(newfullname + GetNamePostfix(sub.Name, GetFile().Name));
            foreach (var sub in GetSubFolders()) sub.RenameTo(newfullname + GetNamePostfix(sub.Name, GetFile().Name));
            GetFile().RenameTo(newname + System.IO.Path.GetExtension(GetFile().Name));
        }

        private static string GetNamePostfix(string name, string prefix)
        {
            return name.Substring(prefix.Length);
        }

        [PopupMenu("s_create_copy", ImageName = CoreIcons.copyName)]
        public void CreateCopy()
        {
            string ext = System.IO.Path.GetExtension(GetFile().Name);
            string newname = InputBox.Run(Texts.Get("s_name_of_copy"), System.IO.Path.GetFileNameWithoutExtension(GetFile().Name));
            if (newname != null)
            {
                CopyFileTo(GetFile().Parent.GetFile(newname + ext), CopyFileMode.Copy);
                CallCompleteChanged();
            }
        }

        public void CopyFileTo(IVirtualFile target, CopyFileMode mode)
        {
            GetFile().CopyFileTo(target, mode);
            foreach (IVirtualFile subfile in GetSubFiles())
            {
                string postfix = subfile.Name.Substring(GetFile().Name.Length);
                IVirtualFile newsub = target.Parent.GetFile(target.Name + postfix);
                subfile.CopyFileTo(newsub, mode);
            }
            foreach (IVirtualFolder subfolder in GetSubFolders())
            {
                string postfix = subfolder.Name.Substring(GetFile().Name.Length);
                IVirtualFolder newsub = target.Parent.GetFolder(target.Name + postfix);
                subfolder.CopyResursiveFolderTo(newsub, mode);
                //subfile.CopyFileTo(newsub, mode);
            }
        }

        public IFileHandler FileHandler
        {
            get
            {
                if (m_fileHandler == null)
                {
                    m_fileHandler = FileHandlerAddonType.FindFileHandler(GetFile(), null);
                }
                return m_fileHandler;
            }
        }
    }

    [AppObject(Name = "file")]
    public class FileAppObject : FileAppObjectBase
    {
        public List<IVirtualFile> SubFiles { get; set; }
        public List<IVirtualFolder> SubFolders { get; set; }
        public IVirtualFile File { get; set; }

        public FileAppObject()
        {
            SubFiles = new List<IVirtualFile>();
            SubFolders = new List<IVirtualFolder>();
        }

        public override bool SupportSerialize
        {
            get { return File == null || File.FileSystem != null; }
        }

        public override IVirtualFile GetFile()
        {
            return File;
        }

        public override List<IVirtualFile> GetSubFiles()
        {
            return SubFiles;
        }

        public override List<IVirtualFolder> GetSubFolders()
        {
            return SubFolders;
        }

        public static FileAppObject FromFile(IVirtualFile file)
        {
            return new FileAppObject { File = file };
        }

        public override void SaveToXml(XmlElement xml)
        {
            base.SaveToXml(xml);
            if (File != null)
            {
                xml.AddChild("Path").InnerText = File.FullPath;
                File.FileSystem.SaveToXml(xml.AddChild("FileSystem"));
            }
            foreach (var f in SubFiles)
            {
                xml.AddChild("SubFile").InnerText = f.FullPath;
            }
        }

        public override void LoadFromXml(XmlElement xml)
        {
            base.LoadFromXml(xml);
            var fsx = xml.FindElement("FileSystem");
            if (fsx != null)
            {
                var fs = (IVirtualFileSystem)VirtualFileSystemAddonType.Instance.LoadAddon(fsx);
                var px = xml.FindElement("Path");
                if (px != null)
                {
                    File = fs.GetFile(px.InnerText);
                }
                foreach (XmlElement x in xml.SelectNodes("SubFile"))
                {
                    SubFiles.Add(fs.GetFile(x.InnerText));
                }
            }
        }

        public static AppObject FromHandler(IFileHandler fhandler)
        {
            return new FileAppObject
            {
                File = fhandler.FileObject,
                m_fileHandler = fhandler,
            };
        }

        public override string TypeName
        {
            get { return "file"; }
        }

        public override string TypeTitle
        {
            get { return "s_file"; }
        }

        public override System.Drawing.Bitmap Image
        {
            get { return CoreIcons._new; }
        }

        public override string ToString()
        {
            return File != null ? File.Name : null;
        }

        protected override string GetConfirmDeleteMessage()
        {
            return Texts.Get("s_really_delete$file", "file", File.ToString("F")) + "\n" + GetSubContentText();
        }

        protected string GetSubContentText()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var path in SubFolders) sb.AppendLine("[" + path.ToString() + "]");
            foreach (var path in SubFiles) sb.AppendLine(path.ToString());
            return sb.ToString();
        }
    }
}
