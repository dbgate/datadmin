using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace DatAdmin
{
    public class DataBrowserDocker : IDocker
    {
        DataBrowser m_dataBrowser;
        IDockerFactory m_factory;

        public DataBrowserDocker(IDockerFactory factory)
        {
            m_dataBrowser = new DataBrowser();
            m_factory = factory;

            HTree.AfterDeleteNode += HTree_AfterDeleteNode;
            HTree.FocusedNodeChanged += HTree_FocusedNodeChanged;
        }

        public DataBrowser DataBrowser { get { return m_dataBrowser; } }

        void HTree_FocusedNodeChanged()
        {
            m_dataBrowser.SelectedObject = TreeTool.GetFocusedNode();
        }

        void HTree_AfterDeleteNode(ITreeNode node)
        {
            m_dataBrowser.SelectedObject = null;
        }

        #region IDocker Members

        public System.Windows.Forms.Control DockerControl
        {
            get { return m_dataBrowser; }
        }

        public IDockerFactory Factory
        {
            get { return m_factory; }
        }
        
        public void DockerVisibleChanged(bool visible)
        {
            m_dataBrowser.IsContentVisible = visible;
        }

        public bool AllowClose()
        {
            return m_dataBrowser.AllowClose();
        }

        public void OnClose()
        {
            m_dataBrowser.OnClose();

            HTree.AfterDeleteNode -= HTree_AfterDeleteNode;
            HTree.FocusedNodeChanged -= HTree_FocusedNodeChanged;
        }

        #endregion
    }

    [DockerFactory(Title = "Data browser", Name = "data_browser")]
    public class DataBrowserDockerFactory : DockerFactoryBase
    {
        public override IDocker CreateDocker()
        {
            return new DataBrowserDocker(this);
        }

        public override string MenuTitle
        {
            get { return "s_data_browser"; }
        }

        public override Bitmap Icon
        {
            get { return CoreIcons.table_data; }
        }

        public override Keys Shortcut
        {
            get { return Keys.Control | Keys.Alt | Keys.D; }
        }
    }
}
