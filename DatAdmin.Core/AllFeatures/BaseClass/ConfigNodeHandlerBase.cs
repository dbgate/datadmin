using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace DatAdmin
{
    public class ConfigNodeHandlerBase : AddonBase, IConfigNodeHandler
    {
        class FolderDef
        {
            internal string Title;
            internal Bitmap Image;
            internal string TestData;
            internal Func<IVirtualPath, string, bool> TestFunc;
        }
        List<FolderDef> m_folders = new List<FolderDef>();

        class FileDef
        {
            internal string Folder;
            internal string Extension;
            internal Bitmap Image;
            internal bool AutoSubFile;
        }
        List<FileDef> m_files = new List<FileDef>();

        class SubFolder
        {
            internal string Folder;
            internal string SubFolderName;
            internal string Title;
            internal Bitmap Image;
        }
        List<SubFolder> m_subFolders = new List<SubFolder>();

        class SubFile
        {
            internal string Folder;
            internal string FileExtension;
            internal string Title;
            internal Bitmap Image;
        }
        List<SubFile> m_subFiles = new List<SubFile>();

        protected void DefineFolder(string title, Bitmap img, string folder, bool allowSubfolders)
        {
            m_folders.Add(new FolderDef
            {
                Image = img,
                Title = title,
                TestData = folder,
                TestFunc = VirtualFileExtension.PathEquals,
            });
            if (allowSubfolders)
            {
                m_folders.Add(new FolderDef
                {
                    Image = CoreIcons.img_folder,
                    TestData = folder + "/",
                    TestFunc = VirtualFileExtension.PathStarts,
                });
            }
        }

        protected void DefineFile(string folder, string extension, Bitmap img, bool autoSubFile)
        {
            m_files.Add(new FileDef
            {
                Extension = extension,
                Folder = folder,
                Image = img,
                AutoSubFile = autoSubFile,
            });
        }

        protected void DefineSubFolder(string folder, string subFolder, string title, Bitmap image)
        {
            m_subFolders.Add(new SubFolder
            {
                Folder = folder,
                Image = image,
                SubFolderName = subFolder,
                Title = title
            });
        }

        protected void DefineSubFile(string folder, string extension, string title, Bitmap image)
        {
            m_subFiles.Add(new SubFile
            {
                Folder = folder,
                Image = image,
                FileExtension = extension,
                Title = title
            });
        }

        public override AddonType AddonType
        {
            get { return ConfigNodeHandlerAddonType.Instance; }
        }

        #region IConfigNodeHandler Members

        public virtual ConfigFileNode LoadFile(ITreeNode parent, IVirtualFile file)
        {
            foreach (var frec in m_files)
            {
                if (file.PathStarts(frec.Folder + "/") && file.GetExtension() == frec.Extension)
                {
                    return new ConfigFileNode(parent, file)
                    {
                        _Title = file.Name,
                        _Image = frec.Image,
                        AutoSubFiles = frec.AutoSubFile,
                    };
                }
            }
            return null;
        }

        public virtual ConfigFolderNode LoadSubFolder(ConfigFileNode parent, IVirtualFolder folder)
        {
            foreach (var fld in m_subFolders)
            {
                if (folder.PathStarts(fld.Folder + "/") && folder.NameEnds("." + fld.SubFolderName))
                {
                    return new ConfigFolderNode(parent, folder)
                    {
                        _Title = fld.Title ?? folder.Name,
                        _Image = fld.Image,
                    };
                }
            }
            return null;
        }

        public virtual ConfigFileNode LoadSubFile(ConfigFileNode parent, IVirtualFile file)
        {
            foreach (var fld in m_subFiles)
            {
                if (file.PathStarts(fld.Folder + "/") && file.NameEnds(fld.FileExtension))
                {
                    return new ConfigFileNode(parent, file)
                    {
                        _Title = fld.Title,
                        _Image = fld.Image,
                    };
                }
            }
            return null;
        }

        public virtual ConfigFolderNode LoadFolder(ITreeNode parent, IVirtualFolder folder)
        {
            foreach (var fld in m_folders)
            {
                if (fld.TestFunc(folder, fld.TestData)) return new ConfigFolderNode(parent, folder)
                {
                    _Title = fld.Title ?? folder.Name,
                    _Image = fld.Image,
                };
            }
            return null;
        }

        #endregion
    }
}
