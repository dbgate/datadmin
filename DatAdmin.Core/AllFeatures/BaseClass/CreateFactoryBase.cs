using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace DatAdmin
{
    public abstract class CreateFactoryBase : AddonBase, ICreateFactory
    {
        #region ICreateFactory Members

        public abstract ICreateFactoryItem[] GetItems(ITreeNode parent);

        #endregion

        [Browsable(false)]
        public override AddonType AddonType
        {
            get { return CreateFactoryAddonType.Instance; }
        }
    }

    public abstract class CreateFactoryItemBase : AddonBase, ICreateFactoryItem
    {
        #region ICreateFactoryItem Members

        public abstract string Name { get; }
        public abstract string GroupName { get; }
        public abstract string Title { get; }
        public abstract string Group { get; }
        public abstract System.Drawing.Bitmap Bitmap { get; }
        public virtual bool CreateFile(string file) { return false; }
        public virtual bool AllowCreateFiles { get { return false; } }
        public virtual string InfoText { get { return ""; } }
        public virtual string FileExtensions { get { return "sql"; } }
        public virtual int Weight { get { return 0; } }

        public virtual bool Create(ITreeNode parent, string name)
        {
            if (name != null)
            {
                try
                {
                    string fn = System.IO.Path.Combine(parent.FileSystemPath, IOTool.AddFirstExtension(name, FileExtensions));
                    return CreateFile(fn);
                }
                catch (Exception e)
                {
                    Errors.Report(e);
                }
            }
            return false;
        }

        #endregion

        [Browsable(false)]
        public override AddonType AddonType
        {
            get { return CreateFactoryItemAddonType.Instance; }
        }
    }
}
