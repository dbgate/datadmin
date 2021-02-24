using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DatAdmin
{
    public static class VirtualFileExtension
    {
        public const string DIAGRAMS_FOLDER_NAME = "diagrams";
        public const string SQLSCRIPTS_FOLDER_NAME = "sqlscripts";

        public static void CopyFileTo(this IVirtualFile srcfile, IVirtualFile dstfile, CopyFileMode mode)
        {
            if (srcfile.DiskPath != null && dstfile.DiskPath != null && IOTool.FileIsLink(srcfile.DiskPath))
            {
                IOTool.CopyFile(srcfile.DiskPath, dstfile.DiskPath, mode);
                return;
            }
            if (srcfile.DataDiskPath != null && dstfile.DataDiskPath != null)
            {
                IOTool.CopyFile(srcfile.DataDiskPath, dstfile.DataDiskPath, mode);
                return;
            }

            string text = null;
            byte[] data = null;
            try
            {
                text = srcfile.GetText();
            }
            catch (Exception)
            {
                data = srcfile.GetBinary();
            }
            if (text != null) dstfile.SaveText(text);
            else dstfile.SaveBinary(data);
            if (mode == CopyFileMode.Move) srcfile.Remove();
        }

        public static void CopyPathTo(this IVirtualPath path, IVirtualFileSystem dstfs, string newname)
        {
            if (path is IVirtualFile)
            {
                var dstf = dstfs.GetFile(newname);
                ((IVirtualFile)path).CopyFileTo(dstf, CopyFileMode.Copy);
            }
            else
            {
                var dstf = dstfs.GetFolder(newname);
                if (!dstf.Exists()) dstf.Create();
            }
        }

        public static void CopyPathTo(this IVirtualPath path, IVirtualFileSystem dstfs)
        {
            path.CopyPathTo(dstfs, path.FullPath);
        }

        public static string NormalizePath(string path)
        {
            return path.ToLower().Replace("\\", "/");
        }

        public static bool PathEquals(this IVirtualPath vp, string path)
        {
            return NormalizePath(vp.FullPath) == NormalizePath(path);
        }

        public static bool PathStarts(this IVirtualPath vp, string path)
        {
            return NormalizePath(vp.FullPath).StartsWith(NormalizePath(path));
        }

        public static string GetExtension(this IVirtualPath vp)
        {
            return Path.GetExtension(vp.FullPath);
        }

        public static bool NameEnds(this IVirtualPath vp, string s)
        {
            return NormalizePath(vp.FullPath).EndsWith(s.ToLower());
        }

        public static IVirtualPath GetPath(this IVirtualFileSystem fs, IVirtualPath path)
        {
            if (path is IVirtualFile) return fs.GetFile(path.FullPath);
            if (path is IVirtualFolder) return fs.GetFolder(path.FullPath);
            return null;
        }

        public static IVirtualPath ChangeName(this IVirtualPath path, string newpath)
        {
            if (path is IVirtualFile)
            {
                return path.FileSystem.GetFile(newpath);
            }
            if (path is IVirtualFolder)
            {
                return path.FileSystem.GetFolder(newpath);
            }
            return null;
        }

        public static string GetNodeNamePostfix(this IVirtualFile file)
        {
            if (file is DiskFile) return "";
            return ":" + file.GetType().FullName;
        }

        public static void CopyFolderContent(this IVirtualFolder folder, IVirtualFolder dstfolder, CopyFileMode mode)
        {
            foreach (var file in folder.LoadFiles())
            {
                file.CopyFileTo(dstfolder.GetFile(file.Name), mode);
            }
            foreach (var fld in folder.LoadFolders())
            {
                fld.CopyResursiveFolderTo(dstfolder.GetFolder(fld.Name), mode);
            }
        }

        public static void CopyResursiveFolderTo(this IVirtualFolder folder, IVirtualFolder dstfolder, CopyFileMode mode)
        {
            if (!dstfolder.Exists()) dstfolder.Create();
            folder.CopyFolderContent(dstfolder, mode);
        }
    }
}
