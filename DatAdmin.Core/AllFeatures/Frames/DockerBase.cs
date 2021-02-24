using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class DockerBase : UserControl, IDocker
    {
        IDockerFactory m_factory;

        public DockerBase()
        {
            InitializeComponent();
        }

        public DockerBase(IDockerFactory factory)
            : this()
        {
            m_factory = factory;
        }

        //public static object GetInterface(object obj, Type intf)
        //{
        //    if (obj.GetType().IsSubclassOf(intf)) return obj;
        //    return null;
        //}

        //public virtual object GetInterface(Type intf)
        //{
        //    return GetInterface(this, intf);
        //}

        public Control DockerControl
        {
            get { return this; }
        }

        public virtual void DockerVisibleChanged(bool visible)
        {
        }

        public virtual bool AllowClose()
        {
            return true;
        }

        public virtual void OnClose()
        {
        }

        public IDockerFactory Factory { get { return m_factory; } }
    }
}
