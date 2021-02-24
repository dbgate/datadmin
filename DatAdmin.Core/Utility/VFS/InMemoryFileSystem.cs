using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    public class InMemoryFile : IVirtualFile
    {
        string m_filename;
        byte[] m_data;

        public InMemoryFile(string filename)
        {
            m_filename = filename;
        }

        #region IVirtualFile Members

        public string DiskPath { get { return null; } }
        public string DataDiskPath { get { return null; } }
        public string DataFileExtension { get { return null; } }

        public void SaveText(string data)
        {
            m_data = Encoding.UTF8.GetBytes(data);
        }

        public void SaveBinary(byte[] data)
        {
            m_data = data;
        }

        public string GetText()
        {
            return Encoding.UTF8.GetString(m_data);
        }

        public byte[] GetBinary()
        {
            return m_data;
        }

        public bool Exists() { return true; }

        public void Remove()
        {
        }

        public string Name
        {
            get { return m_filename; }
        }

        //public bool IsInFolder(IVirtualFolder folder)
        //{
        //    return false;
        //}

        public IVirtualFolder Parent { get { return null; } }

        public void RenameTo(string newname)
        {
        }

        public IVirtualFileSystem FileSystem { get { return null; } }

        public string FullPath { get { return null; } }

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

        #endregion
    }
}
