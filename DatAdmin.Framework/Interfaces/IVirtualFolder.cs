using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DatAdmin
{
    //public enum FileDataType {Xml,Text,Binary};

    public interface IVirtualFileSystem : IAddonInstance, IDisposable
    {
        /// <summary>
        /// root folder
        /// </summary>
        IVirtualFolder Root { get; }
        /// <summary>
        /// finds file (absolute path)
        /// </summary>
        /// <param name="path"></param>
        /// <returns>file path (call Exists() to test existence)</returns>
        IVirtualFile GetFile(string path);
        /// <summary>
        /// finds folder (absolute path)
        /// </summary>
        /// <param name="path"></param>
        /// <returns>folder path (call Exists() to test existence)</returns>
        IVirtualFolder GetFolder(string path);
        /// <summary>
        /// saves all pending changes to filesystem
        /// </summary>
        void Flush();
    }

    public class VirtualPathCaps
    {
        public bool Remove;
        public bool Rename;
    }

    /// <summary>
    /// represents virtual path
    /// </summary>
    public interface IVirtualPath
    {
        string Name { get; }
        /// <summary>
        /// parent folder; if null, folder is root
        /// </summary>
        IVirtualFolder Parent { get; }
        /// <summary>
        /// path on disk; if null, path has not disk image (eg. "c:\folder\file.txt")
        /// </summary>
        string DiskPath { get; }
        /// <summary>
        /// full path begining without "/" character at begining (relative to root)
        /// </summary>
        string FullPath { get; }
        /// <summary>
        /// virtual file system, which owns this path
        /// </summary>
        IVirtualFileSystem FileSystem { get; }
        /// <summary>
        /// returns whether path exists
        /// </summary>
        /// <returns></returns>
        bool Exists();
        /// <summary>
        /// deletes file or folder
        /// </summary>
        void Remove();
        /// <summary>
        /// renames file or folder, this instance could point to old name, so it could be invalid
        /// </summary>
        void RenameTo(string newname);
        /// <summary>
        /// capabilities of this path
        /// </summary>
        VirtualPathCaps Caps { get; }
    }

    /// <summary>
    /// represents folder name
    /// </summary>
    public interface IVirtualFolder : IVirtualPath
    {
        List<IVirtualFile> LoadFiles();
        IVirtualFile GetFile(string name);

        List<IVirtualFolder> LoadFolders();
        IVirtualFolder GetFolder(string name);

        void Create();
        string FolderDiskPath { get; }
    }

    /// <summary>
    /// represents file name
    /// </summary>
    public interface IVirtualFile : IVirtualPath
    {
        void SaveText(string data);
        void SaveBinary(byte[] data);
        string GetText();
        byte[] GetBinary();
        string DataFileExtension { get; }
        string DataDiskPath { get; }
    }

    public class VirtualFileSystemAttribute : RegisterAttribute { }

    [AddonType]
    public class VirtualFileSystemAddonType : AddonType
    {
        public override string Name
        {
            get { return "virtualfilesystem"; }
        }

        public override Type InterfaceType
        {
            get { return typeof(IVirtualFileSystem); }
        }

        public override Type RegisterAttributeType
        {
            get { return typeof(VirtualFileSystemAttribute); }
        }

        public static readonly VirtualFileSystemAddonType Instance = new VirtualFileSystemAddonType();
    }
}
