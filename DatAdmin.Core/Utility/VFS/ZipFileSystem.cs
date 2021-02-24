using System;
using System.Collections.Generic;
using System.Text;
using Ionic.Zip;
using System.IO;

namespace DatAdmin
{
    [VirtualFileSystem(Name = "zip")]
    public class ZipFileSystem : VirtualFileSystemBase
    {
        ZipFile m_zip;
        ZFolder m_directory;
        string m_file;
        Dictionary<string, ZDirItem> m_dirIndex = new Dictionary<string, ZDirItem>();

        public ZipFileSystem(string file)
        {
            m_file = file;
            m_zip = new ZipFile(file);
        }

        public ZipFileSystem()
        {
        }

        public ZipFile Zip { get { return m_zip; } }

        [XmlElem]
        public string ZipFileName
        {
            get { return m_file; }
            set
            {
                m_file = value;
                if (m_zip != null) m_zip.Dispose();
                m_zip = new ZipFile(value);
            }
        }

        public override IVirtualFolder Root
        {
            get { return new ZipVirtualFolder(this, ""); }
        }

        public override IVirtualFile GetFile(string path)
        {
            return new ZipVirtualFile(this, path);
            //if (Zip.ContainsEntry(path)) return new ZipVirtualFile(this, path);
            //return null;
        }

        public override IVirtualFolder GetFolder(string path)
        {
            return new ZipVirtualFolder(this, path);
            //if (Zip.ContainsEntry(path)) return new ZipVirtualFolder(this, path);
            //return null;
        }

        public override void Flush()
        {
            Zip.Save();
        }

        public ZFolder Directory
        {
            get
            {
                WantDirectory();
                return m_directory;
            }
        }

        public ZFolder FindFolder(string path)
        {
            WantDirectory();
            if (String.IsNullOrEmpty(path)) return Directory;
            string key = VirtualFileExtension.NormalizePath(path);
            if (m_dirIndex.ContainsKey(path)) return m_dirIndex[path] as ZFolder;
            return null;
        }

        private void WantDirectory()
        {
            if (m_directory == null) LoadDirectory();
        }

        private void LoadDirectory()
        {
            m_dirIndex = new Dictionary<string, ZDirItem>();
            m_directory = new ZFolder();
            foreach (var entry in Zip.Entries)
            {
                var npath = VirtualFileExtension.NormalizePath(entry.FileName);
                var items = npath.Split('/');
                var fld = FindZFolder(items, m_directory, 0, entry.IsDirectory ? items.Length : items.Length - 1);
                if (!entry.IsDirectory)
                {
                    var zfile = new ZFile { Path = npath };
                    fld.Files.Add(zfile);
                    m_dirIndex[npath] = zfile;
                }
            }
        }

        private ZFolder FindZFolder(string[] items, ZFolder root, int start, int count)
        {
            if (count <= 0) return root;
            foreach (var child in root.Folders)
            {
                if (child.Name == items[start]) return FindZFolder(items, child, start + 1, count - 1);
            }
            string pth = PyList.Slice(items, 0, start + 1).CreateDelimitedText("/");
            var newf = new ZFolder { Path = pth };
            root.Folders.Add(newf);
            m_dirIndex[pth] = newf;
            return FindZFolder(items, newf, start + 1, count - 1);
        }

        public abstract class ZDirItem
        {
            public string Path;
            public abstract bool IsFile { get; }
            public abstract bool IsFolder { get; }
            public string Name { get { return System.IO.Path.GetFileName(Path); } }
        }

        public class ZFolder : ZDirItem
        {
            public List<ZFolder> Folders = new List<ZFolder>();
            public List<ZFile> Files = new List<ZFile>();
            public override bool IsFile { get { return false; } }
            public override bool IsFolder { get { return true; } }
        }

        public class ZFile : ZDirItem
        {
            public override bool IsFile { get { return true; } }
            public override bool IsFolder { get { return false; } }
        }
    }

    public abstract class ZipVirtualPath : IVirtualPath
    {
        protected ZipFileSystem m_fs;
        protected string m_path;
        protected string m_normaPath;

        protected ZipVirtualPath(ZipFileSystem fs, string path)
        {
            m_fs = fs;
            m_path = path;
            m_normaPath = VirtualFileExtension.NormalizePath(m_path);
        }

        #region IVirtualPath Members

        public string Name
        {
            get { return Path.GetFileName(m_path); }
        }

        public IVirtualFolder Parent
        {
            get
            {
                if (m_path == "") return null;
                var p = Path.GetDirectoryName(m_path);
                if (p != null) return new ZipVirtualFolder(m_fs, p);
                return m_fs.Root;
            }
        }

        public string DiskPath
        {
            get { return null; }
        }

        public string FullPath
        {
            get { return m_path; }
        }

        public IVirtualFileSystem FileSystem
        {
            get { return m_fs; }
        }

        public abstract void Remove();
        public abstract bool Exists();
        public abstract void RenameTo(string newname);

        #endregion

        public ZipEntry GetEntry()
        {
            return Zip[m_normaPath];
        }

        public ZipFile Zip { get { return m_fs.Zip; } }
        public abstract VirtualPathCaps Caps { get; }
    }

    public class ZipVirtualFolder : ZipVirtualPath, IVirtualFolder
    {
        public ZipVirtualFolder(ZipFileSystem fs, string path)
            : base(fs, path)
        {
        }

        ZipFileSystem.ZFolder ZFolder
        {
            get { return m_fs.FindFolder(m_normaPath); }
        }

        #region IVirtualFolder Members

        public List<IVirtualFile> LoadFiles()
        {
            var res = new List<IVirtualFile>();
            foreach (var zf in ZFolder.Files)
            {
                res.Add(new ZipVirtualFile(m_fs, zf.Path));
            }
            return res;
            //var res = new List<IVirtualFile>();
            //var npath = VirtualFileExtension.NormalizePath(m_path);
            //foreach (var entry in Zip.Entries)
            //{
            //    if (entry.IsDirectory) continue;
            //    string dir = VirtualFileExtension.NormalizePath(Path.GetDirectoryName(entry.FileName));
            //    if (dir == npath)
            //    {
            //        res.Add(new ZipVirtualFile(m_fs, entry.FileName));
            //    }
            //}
            //return res;
        }

        public IVirtualFile GetFile(string name)
        {
            throw new NotImplementedError("DAE-00141");
        }

        public List<IVirtualFolder> LoadFolders()
        {
            var res = new List<IVirtualFolder>();
            foreach (var zf in ZFolder.Folders)
            {
                res.Add(new ZipVirtualFolder(m_fs, zf.Path));
            }
            return res;

            //var res = new List<IVirtualFolder>();
            //var npath = VirtualFileExtension.NormalizePath(m_path);
            //var processedDirs = new HashSetEx<string>();
            //foreach (var entry in Zip.Entries)
            //{
            //    string fnorm = VirtualFileExtension.NormalizePath(entry.FileName);
            //    string[] path = fnorm.Split('/');
            //    // create parent directories
            //    for (int i = 0; i < path.Length - 1; i++)
            //    {
            //    }
            //    if (fnorm.StartsWith(npath))
            //    {
            //        if (entry.IsDirectory)
            //        {
            //            res.Add(new ZipVirtualFolder(m_fs, entry.FileName));
            //        }
            //        else
            //        {
            //            string dir = Path.GetDirectoryName(fnorm);
            //            if (dir != null && !processedDirs.Contains(dir))
            //            {
            //                processedDirs.Add(dir);
            //                res.Add(new ZipVirtualFolder(m_fs, dir));
            //            }
            //        }
            //    }
            //}
            //return res;
        }

        public IVirtualFolder GetFolder(string name)
        {
            return new ZipVirtualFolder(m_fs, String.IsNullOrEmpty(m_path) ? name : m_path + "/" + name);
        }

        public void Create()
        {
        }

        public override void Remove()
        {
            var entry = GetEntry();
            if (entry != null) Zip.RemoveEntry(entry);
        }

        public override bool Exists()
        {
            var entry = GetEntry();
            return entry != null && entry.IsDirectory;
        }

        public override void RenameTo(string newname)
        {
        }

        public override VirtualPathCaps Caps
        {
            get
            {
                return new VirtualPathCaps
                {
                    Remove = true,
                    Rename = false,
                };
            }
        }

        public string FolderDiskPath { get { return null; } }

        #endregion
    }

    public class ZipVirtualFile : ZipVirtualPath, IVirtualFile
    {
        public ZipVirtualFile(ZipFileSystem fs, string path)
            : base(fs, path)
        {
        }

        #region IVirtualFile Members

        public void SaveText(string data)
        {
            if (Zip.ContainsEntry(m_normaPath)) Zip.RemoveEntry(m_normaPath);
            Zip.AddEntry(m_normaPath, Encoding.UTF8.GetBytes(data));
        }

        public void SaveBinary(byte[] data)
        {
            Zip.AddEntry(m_normaPath, data);
        }

        public string GetText()
        {
            using (var fr = GetEntry().OpenReader())
            {
                using (var sr = new StreamReader(fr, Encoding.UTF8))
                {
                    return sr.ReadToEnd();
                }
            }
        }

        public byte[] GetBinary()
        {
            using (var fr = GetEntry().OpenReader())
            {
                return fr.ReadAllBytes();
            }
        }

        public override void Remove()
        {
            Zip.RemoveEntry(GetEntry());
        }

        public override bool Exists()
        {
            return Zip.ContainsEntry(m_normaPath);
        }

        public override void RenameTo(string newname)
        {
        }

        public string DataFileExtension
        {
            get { return null; }
        }

        public string DataDiskPath
        {
            get { return null; }
        }

        public override VirtualPathCaps Caps
        {
            get
            {
                return new VirtualPathCaps
                {
                    Remove = true,
                    Rename = false,
                };
            }
        }

        #endregion
    }
}
