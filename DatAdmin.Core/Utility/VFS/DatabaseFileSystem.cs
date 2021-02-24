using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DatAdmin
{
    public class OnServerFile : IVirtualFile
    {
        DXDriver m_driver;
        string m_filename;
        string m_dbname;
        int m_folderid;

        public OnServerFile(DXDriver driver, string filename, string dbname, int folderid)
        {
            m_driver = driver;
            m_filename = filename;
            m_dbname = dbname;
            m_folderid = folderid;
        }

        #region IVirtualFile Members

        public string DiskPath { get { return null; } }
        public string DataDiskPath { get { return null; } }
        public string DataFileExtension { get { return IOTool.GetDataFileExtension(m_filename); } }

        public bool Exists()
        {
            return InvokerExtension.InvokeR3<bool, string, int, string>(m_driver.Connection, m_driver.ExistsFile, m_dbname, m_folderid, m_filename);
        }

        public IVirtualFolder Parent { get { return new OnServerFolder(m_driver, m_folderid, null, m_dbname); } }

        public void SaveText(string data)
        {
            InvokerExtension.Invoke4(m_driver.Connection, m_driver.SaveTextFile, m_dbname, m_folderid, m_filename, data);
        }

        public void SaveBinary(byte[] data)
        {
            InvokerExtension.Invoke4(m_driver.Connection, m_driver.SaveBinaryFile, m_dbname, m_folderid, m_filename, data);
        }

        public string GetText()
        {
            return InvokerExtension.InvokeR3<string, string, int, string>(m_driver.Connection, m_driver.LoadTextFile, m_dbname, m_folderid, m_filename);
        }

        public byte[] GetBinary()
        {
            return InvokerExtension.InvokeR3<byte[], string, int, string>(m_driver.Connection, m_driver.LoadBinaryFile, m_dbname, m_folderid, m_filename);
        }

        public void Remove()
        {
            InvokerExtension.Invoke3(m_driver.Connection, m_driver.RemoveFile, m_dbname, m_folderid, m_filename);
        }
        public string Name { get { return m_filename; } }


        //public bool IsInFolder(IVirtualFolder folder)
        //{
        //    if (folder is OnServerFolder)
        //    {
        //        OnServerFolder other = (OnServerFolder)folder;
        //        if (other.m_driver.Connection.SystemConnection.ConnectionString != m_driver.Connection.SystemConnection.ConnectionString) return false;
        //        if (m_dbname != other.m_dbname) return false;
        //        return InvokerExtension.InvokeR3<bool, string, int, int>(m_driver.Connection, m_driver.HasFile, m_dbname, other.m_folderid, m_fileid);
        //    }
        //    return false;
        //}

        public void RenameTo(string newname)
        {
            InvokerExtension.Invoke4(m_driver.Connection, m_driver.RenameFile, m_dbname, m_folderid, m_filename, newname);
        }

        public override string ToString()
        {
            return Name;
        }

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

        #region IVirtualPath Members


        public string FullPath
        {
            get { return Name; }
        }

        public IVirtualFileSystem FileSystem
        {
            get { return null; }
        }

        #endregion
    }

    public class OnServerFolder : IVirtualFolder
    {
        internal DXDriver m_driver;
        internal int m_folderid;
        internal string m_dbname;
        internal string m_folderName;

        public OnServerFolder(DXDriver driver, int folderid, string foldername, string dbname)
        {
            m_driver = driver;
            m_folderid = folderid;
            m_dbname = dbname;
            m_folderName = foldername;
        }

        #region IVirtualFolder Members

        public List<IVirtualFile> LoadFiles()
        {
            List<IVirtualFile> res = new List<IVirtualFile>();
            foreach (OnServerFile f in InvokerExtension.InvokeR2<List<OnServerFile>, string, int>(m_driver.Connection, m_driver.LoadFiles, m_dbname, m_folderid)) res.Add(f);
            return res;
        }

        public IVirtualFile GetFile(string name)
        {
            return new OnServerFile(m_driver, name, m_dbname, m_folderid);
        }

        //bool IsTheSame(IVirtualFolder folder)
        //{
        //    if (folder is OnServerFolder)
        //    {
        //        OnServerFolder other = (OnServerFolder)folder;
        //        if (other.m_driver.Connection.SystemConnection.ConnectionString != m_driver.Connection.SystemConnection.ConnectionString) return false;
        //        if (m_dbname != other.m_dbname) return false;
        //        return true;
        //    }
        //    return false;
        //}

        #endregion

        public override string ToString()
        {
            return Texts.Get("s_on_server") + ":" + m_folderName;
        }

        #region IVirtualFolder Members


        public List<IVirtualFolder> LoadFolders()
        {
            return new List<IVirtualFolder>();
        }

        public IVirtualFolder GetFolder(string name)
        {
            throw new NotImplementedError("DAE-00134");
        }

        public void Create()
        {
            throw new NotImplementedError("DAE-00135");
        }

        public void Remove()
        {
            throw new NotImplementedError("DAE-00136");
        }

        public bool Exists()
        {
            throw new NotImplementedError("DAE-00137");
        }

        public void RenameTo(string newname)
        {
            throw new NotImplementedError("DAE-00138");
        }

        public virtual VirtualPathCaps Caps
        {
            get
            {
                return new VirtualPathCaps
                {
                    Remove = false,
                    Rename = false,
                };
            }
        }

        public string FolderDiskPath { get { return null; } }

        #endregion

        #region IVirtualPath Members

        public string Name
        {
            get { throw new NotImplementedError("DAE-00139"); }
        }

        public IVirtualFolder Parent
        {
            get { throw new NotImplementedError("DAE-00140"); }
        }

        public string DiskPath { get { return null; } }
        public string FullPath { get { return null; } }

        public IVirtualFileSystem FileSystem
        {
            get { return null; }
        }

        #endregion
    }

}
