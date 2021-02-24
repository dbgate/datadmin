using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;

namespace DatAdmin
{
    public class FileHandlerAttribute : RegisterAttribute { }

    public class FileHandlerCaps
    {
        public bool AcceptAllExtensions;
        public bool OpenAction;
        public bool OpenDatabase;
        public bool CreateNode;

        public bool AllFlags
        {
            set
            {
                AcceptAllExtensions = value;
                OpenAction = value;
                OpenDatabase = value;
                CreateNode = value;
            }
        }
    }

    public interface IFileHandler : IAddonInstance
    {
        bool AssignFile(IVirtualFile file);
        /// <summary>
        /// action called when selecting file in open dialog
        /// </summary>
        void OpenAction();


        IDatabaseSource OpenDatabase();

        ITreeNode CreateNode(ITreeNode parent);

        /// <summary>
        /// File extension - lowercase without dot (eg. "xml")
        /// </summary>
        string Extension { get; }
        string Description { get; }

        FileHandlerCaps Caps { get; }
        Bitmap Image { get; }

        IVirtualFile FileObject { get; }
        string DiskPath { get; }

        int Priority { get; }

        /// <summary>
        /// performs content-related actions before deleting (eg. close connection)
        /// </summary>
        void BeforeDelete();
        /// <summary>
        /// performs content-related actions before renaming (eg. close connection)
        /// </summary>
        void BeforeRename();
    }

    [AddonType]
    public class FileHandlerAddonType : AddonType
    {
        public override string Name
        {
            get { return "filehandler"; }
        }

        public override Type InterfaceType
        {
            get { return typeof(IFileHandler); }
        }

        public override Type RegisterAttributeType
        {
            get { return typeof(FileHandlerAttribute); }
        }

        public static readonly FileHandlerAddonType Instance = new FileHandlerAddonType();

        public static IFileHandler FindFileHandler(IVirtualFile file, Func<IFileHandler, bool> accept)
        {
            var lst = new List<IFileHandler>();
            foreach (var hld in FileHandlerAddonType.Instance.CommonSpace.GetAllAddons())
            {
                var han = (IFileHandler)hld.CreateInstance();
                if (accept == null || accept(han)) lst.Add(han);
            }
            lst.SortByKey(h => -h.Priority);

            foreach (var han in lst)
            {
                try
                {
                    if (han.AssignFile(file)) return han;
                }
                catch
                {
                    continue;
                }
            }
            return null;
        }
    }

    public abstract class FileHandlerBase : AddonBase, IFileHandler
    {
        protected IVirtualFile m_file;

        public override AddonType AddonType
        {
            get { return FileHandlerAddonType.Instance; }
        }

        protected virtual void AfterAssignFile() { }

        #region IFileHandler Members

        public virtual bool AssignFile(IVirtualFile file)
        {
            string ext = file.DataFileExtension;
            if (Caps.AcceptAllExtensions || Extension == ext)
            {
                m_file = file;
                AfterAssignFile();
                return true;
            }
            return false;
        }

        public abstract string Extension
        {
            get;
        }

        public virtual string Description
        {
            get { return null; }
        }


        public virtual int Priority { get { return 0; } }

        public virtual void OpenAction()
        {
            throw new NotImplementedError("DAE-00085");
        }

        public virtual IDatabaseSource OpenDatabase()
        {
            throw new NotImplementedError("DAE-00086");
        }

        public virtual ITreeNode CreateNode(ITreeNode parent)
        {
            throw new NotImplementedError("DAE-00087");
        }

        public abstract FileHandlerCaps Caps { get; }

        public virtual Bitmap Image
        {
            get { return null; }
        }

        public virtual IVirtualFile FileObject
        {
            get { return m_file; }
            set { m_file = value; }
        }

        public virtual string DiskPath
        {
            get
            {
                if (m_file != null) return m_file.DiskPath;
                return null;
            }
        }

        #endregion

        protected void OpenActionCreateLink()
        {
            IOTool.CreateLink(m_file.DiskPath, Core.DataDirectory);
        }

        public virtual void BeforeDelete() { }
        public virtual void BeforeRename() { }
    }

    public abstract class FileBasedDatabaseHandler : FileHandlerBase
    {
        protected abstract DbFileStoredConnection CreateStoredConnection();

        public override void OpenAction()
        {
            var con = CreateStoredConnection();
            con.DbFilename = DiskPath;
            string ufn = IOTool.GetUniqueFileName(Path.Combine(Core.DataDirectory, Path.GetFileNameWithoutExtension(DiskPath) + ".con"));
            con.FileName = ufn;
            con.Save();
        }

        public override IDatabaseSource OpenDatabase()
        {
            var stored = CreateStoredConnection();
            stored.DbFilename = DiskPath;
            return DbFromStored(stored);
        }

        public IDatabaseSource DbFromStored(DbFileStoredConnection stored)
        {
            var physconn = stored.CreatePhysicalConnection();
            IDatabaseSource conn = new GenericDatabaseSource(null, physconn, null);
            return conn;
        }

        public override ITreeNode CreateNode(ITreeNode parent)
        {
            DbFileStoredConnection res = null;
            string dbfile = m_file.DataDiskPath;
            string confile = m_file.DiskPath + ".con";
            if (File.Exists(confile))
            {
                res = StoredConnection.LoadFromFile(confile) as DbFileStoredConnection;
            }
            if (res == null)
            {
                res = CreateStoredConnection();
            }
            res.DbFilename = dbfile;
            res.FileName = confile;
            var conn = DbFromStored(res);

            return new Database_SourceConnectionTreeNode(conn, parent, this, conn.Connection.StoredConnection, false);
        }

        public override FileHandlerCaps Caps
        {
            get
            {
                return new FileHandlerCaps
                {
                    AllFlags = false,
                    OpenAction = true,
                    CreateNode = true,
                    OpenDatabase = true
                };
            }
        }
    }

    [FileHandler(Name = "unknown")]
    public class UnknownFileHandler : FileHandlerBase
    {
        public override int Priority
        {
            get { return -100; }
        }

        public override FileHandlerCaps Caps
        {
            get
            {
                return new FileHandlerCaps
                {
                    AllFlags = false,
                    CreateNode = true,
                    AcceptAllExtensions = true,
                };
            }
        }

        public override string Extension
        {
            get { return null; }
        }

        public override ITreeNode CreateNode(ITreeNode parent)
        {
            return new UnknownVirtualFileNode(parent, this);
        }
    }
}
