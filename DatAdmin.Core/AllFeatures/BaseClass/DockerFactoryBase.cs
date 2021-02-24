using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace DatAdmin
{
    public abstract class DockerFactoryBase : AddonBase, IDockerFactory
    {
        #region IDockerFactory Members

        public abstract IDocker CreateDocker();
        public abstract string MenuTitle
        {
            get;
        }

        public abstract Bitmap Icon
        {
            get;
        }

        public virtual string GetPersistString()
        {
            return XmlTool.GetRegisterType(this);
        }

        public virtual Keys Shortcut
        {
            get { return Keys.None; }
        }

        public virtual DockerState InitialState
        {
            get { return DockerState.Document; }
        }

        #endregion

        public override AddonType AddonType
        {
            get { return DockerFactoryAddonType.Instance; }
        }
    }
}
