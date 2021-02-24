using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DatAdmin
{
    [VirtualFileSystem(Name = "disk")]
    public class DiskFileSystem : VirtualFileSystemBase
    {
        string m_rootPath;
        static Dictionary<string, DiskFileSystem> m_stdFileSystems = new Dictionary<string, DiskFileSystem>();

        public DiskFileSystem() { }
        
        public DiskFileSystem(string rootPath)
        {
            m_rootPath = rootPath;
        }

        public override IVirtualFolder Root
        {
            get { return new DiskFolder(this, ""); }
        }

        public override IVirtualFile GetFile(string path)
        {
            return new DiskFile(this, path);
            //string fn = Path.Combine(m_rootPath, path);
            //if (File.Exists(fn)) return new DiskFile(this, path);
            //return null;
        }

        public override IVirtualFolder GetFolder(string path)
        {
            return new DiskFolder(this, path);
            //string fn = Path.Combine(m_rootPath, path);
            //if (Directory.Exists(fn)) return new DiskFolder(this, path);
            //return null;
        }

        public static DiskFileSystem GetForRoot(string root)
        {
            lock (m_stdFileSystems)
            {
                if (!m_stdFileSystems.ContainsKey(root)) m_stdFileSystems[root] = new DiskFileSystem(root);
                return m_stdFileSystems[root];
            }
        }

        [XmlElem]
        public string RootPath
        {
            get { return m_rootPath; }
            set { m_rootPath = value; }
        }
    }

    public class AppDataDiskFileSystem : DiskFileSystem
    {
        static AppDataDiskFileSystem m_instance;

        public AppDataDiskFileSystem()
            : base(Core.AppDataDirectory)
        {
        }

        public static AppDataDiskFileSystem Instance
        {
            get
            {
                if (m_instance == null) m_instance = new AppDataDiskFileSystem();
                return m_instance;
            }
        }
    }

    public abstract class DiskPathBase : IVirtualPath, IFormattable
    {
        protected string m_path;
        protected DiskFileSystem m_fileSystem;

        protected DiskPathBase(string path)
        {
            m_fileSystem = DiskFileSystem.GetForRoot(Path.GetPathRoot(path));
            m_path = IOTool.RelativePathTo(m_fileSystem.RootPath, path);
        }

        protected DiskPathBase(DiskFileSystem fs, string path)
        {
            m_fileSystem = fs;
            m_path = path;
        }

        #region IVirtualPath Members

        public virtual string Name
        {
            get { return Path.GetFileName(m_path); }
        }

        public IVirtualFolder Parent
        {
            get
            {
                if (m_path == "") return null;
                var p = Path.GetDirectoryName(m_path);
                if (p != null) return new DiskFolder(m_fileSystem, p);
                return m_fileSystem.Root;
            }
        }

        public string DiskPath
        {
            get { return Path.Combine(m_fileSystem.RootPath, m_path); }
        }

        public string FullPath
        {
            get { return m_path; }
        }

        public IVirtualFileSystem FileSystem
        {
            get { return m_fileSystem; }
        }

        public override string ToString()
        {
            return Name;
        }

        public abstract void Remove();
        public abstract bool Exists();
        public abstract void RenameTo(string newname);

        public virtual VirtualPathCaps Caps
        {
            get
            {
                return new VirtualPathCaps
                {
                    Remove = true,
                    Rename = true,
                };
            }
        }

        #endregion

        #region IFormattable Members

        public string ToString(string format, IFormatProvider formatProvider)
        {
            switch (format)
            {
                case "F":
                case "L":
                    return m_path;
                default:
                    return Path.GetFileName(m_path);
            }
        }

        #endregion

    }

    public class DiskFolder : DiskPathBase, IVirtualFolder
    {
        string m_folderPath;

        public DiskFolder(string path)
            : base(path)
        {
            InitializeLink();
        }

        public DiskFolder(DiskFileSystem fs, string path)
            : base(fs, path)
        {
            InitializeLink();
        }

        private void InitializeLink()
        {
            if (IOTool.FileIsLink(DiskPath))
            {
                m_folderPath = IOTool.GetLinkContent(DiskPath);
            }
            else
            {
                m_folderPath = DiskPath;
            }
        }

        #region IVirtualFolder Members

        public List<IVirtualFile> LoadFiles()
        {
            List<IVirtualFile> res = new List<IVirtualFile>();
            if (!Directory.Exists(m_folderPath)) return res;
            foreach (string file in Directory.GetFiles(m_folderPath))
            {
                if (IOTool.FileIsLink(file))
                {
                    if (!IOTool.FileLinkExists(file)) continue;
                }
                res.Add(new DiskFile(m_fileSystem, IOTool.RelativePathTo(m_fileSystem.RootPath, file)));
            }
            return res;
        }

        public IVirtualFile GetFile(string name)
        {
            return new DiskFile(m_fileSystem, Path.Combine(m_folderPath, name));
        }

        public List<IVirtualFolder> LoadFolders()
        {
            var res = new List<IVirtualFolder>();
            if (Directory.Exists(m_folderPath))
            {
                foreach (string folder in Directory.GetDirectories(m_folderPath))
                {
                    res.Add(GetFolder(Path.GetFileName(folder)));
                }

                foreach (string file in Directory.GetFiles(m_folderPath, "*.lnk"))
                {
                    if (!IOTool.DirectoryLinkExists(file)) continue;
                    res.Add(new DiskFolder(file));
                }
            }
            return res;
        }

        public IVirtualFolder GetFolder(string name)
        {
            return new DiskFolder(m_fileSystem, String.IsNullOrEmpty(m_path) ? name : m_path + Path.DirectorySeparatorChar + name);
        }

        public void Create()
        {
            Directory.CreateDirectory(DiskPath);
        }

        public override void Remove()
        {
            if (IOTool.FileIsLink(DiskPath))
            {
                File.Delete(DiskPath);
            }
            else
            {
                try
                {
                    Directory.Delete(DiskPath, true);
                }
                catch (Exception err)
                {
                    StdDialog.ShowError("s_path_cannot_be_deleted_delete_scheduled");
                    DeletedFileRegistrer.AddPath(DiskPath, true);
                    throw new DeleteError(DiskPath, err);
                }
            }
        }

        public override bool Exists()
        {
            return Directory.Exists(DiskPath);
        }

        public override void RenameTo(string newname)
        {
            string dir = Path.GetDirectoryName(DiskPath);
            string dst = Path.Combine(dir, newname);
            if (IOTool.FileIsLink(DiskPath))
            {
                File.Move(DiskPath, dst + ".lnk");
            }
            else
            {
                Directory.Move(DiskPath, dst);
            }
        }

        public string FolderDiskPath { get { return m_folderPath; } }

        #endregion

        public override string ToString()
        {
            if (IOTool.FileIsLink(m_path)) return Path.GetFileNameWithoutExtension(m_path);
            return Path.GetFileName(m_path);
        }

        public override string Name
        {
            get
            {
                if (IOTool.FileIsLink(m_path)) return Path.GetFileNameWithoutExtension(m_path);
                return Path.GetFileName(m_path);
            }
        }
    }

    public class DiskFile : DiskPathBase, IVirtualFile
    {
        string m_dataFilePath;

        public DiskFile(string path)
            : base(path)
        {
            InitializeDataFile();
        }

        public DiskFile(DiskFileSystem fs, string path)
            : base(fs, path)
        {
            InitializeDataFile();
        }

        private void InitializeDataFile()
        {
            if (IOTool.FileIsLink(DiskPath) && File.Exists(DiskPath))
            {
                m_dataFilePath = IOTool.GetLinkContent(DiskPath);
            }
            else
            {
                m_dataFilePath = DiskPath;
            }
        }

        #region IVirtualFile Members

        public string DataFileExtension
        {
            get { return IOTool.GetDataFileExtension(m_dataFilePath); }
        }

        public string DataDiskPath
        {
            get
            {
                return m_dataFilePath;
            }
        }

        public void SaveText(string data)
        {
            string dir = Path.GetDirectoryName(m_dataFilePath);
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            using (StreamWriter sw = new StreamWriter(m_dataFilePath)) sw.Write(data);
        }

        public void SaveBinary(byte[] data)
        {
            using (FileStream fw = new FileStream(m_dataFilePath, FileMode.Create))
            {
                fw.Write(data, 0, data.Length);
            }
        }

        public override bool Exists()
        {
            return File.Exists(m_dataFilePath);
        }

        public string GetText()
        {
            using (StreamReader fr = new StreamReader(m_dataFilePath)) return fr.ReadToEnd();
        }

        public byte[] GetBinary()
        {
            using (FileStream fr = new FileStream(m_dataFilePath, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                return IOTool.ReadToEnd(fr);
            }
        }

        public override void Remove()
        {
            try
            {
                File.Delete(DiskPath);
            }
            catch (Exception err)
            {
                StdDialog.ShowError("s_path_cannot_be_deleted_delete_scheduled");
                DeletedFileRegistrer.AddPath(DiskPath, true);
                throw new DeleteError(DiskPath, err);
            }
        }

        public override string Name
        {
            get
            {
                return Path.GetFileName(DiskPath);
                //return Path.GetFileNameWithoutExtension(DiskPath) + Path.GetExtension(m_dataFilePath);
            }
        }

        public override void RenameTo(string newname)
        {
            string dir = Path.GetDirectoryName(DiskPath);
            string dst = Path.Combine(dir, newname);
            File.Move(DiskPath, dst);
        }

        #endregion
    }
}
