using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    public abstract class VirtualFileSystemBase : AddonBase, IVirtualFileSystem
    {
        #region IVirtualFileSystem Members

        public abstract IVirtualFolder Root { get; }
        public abstract IVirtualFile GetFile(string path);
        public abstract IVirtualFolder GetFolder(string path);
        public virtual void Flush() { }

        #endregion

        public override AddonType AddonType
        {
            get { return VirtualFileSystemAddonType.Instance; }
        }

        #region IDisposable Members

        public virtual void Dispose()
        {
        }

        #endregion
    }
}
