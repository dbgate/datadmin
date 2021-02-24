using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace DatAdmin
{
    public class SummaryDocker : IDocker
    {
        SummaryContentFrame m_objectView;
        IDockerFactory m_factory;

        public SummaryDocker(IDockerFactory factory)
        {
            m_factory = factory;
            m_objectView = new SummaryContentFrame();
            m_objectView.SelectedObject = TreeTool.GetFocusedNode();

            HTree.AfterDeleteNode += HTree_AfterDeleteNode;
            HTree.FocusedNodeChanged += HTree_FocusedNodeChanged;
        }

        void HTree_FocusedNodeChanged()
        {
            var curnode = TreeTool.GetFocusedNode();
            MainWindow.Instance.RunInMainWindow(() => m_objectView.SelectedObject = curnode);
        }

        void HTree_AfterDeleteNode(ITreeNode obj)
        {
            m_objectView.SelectedObject = null;
        }

        #region IDocker Members

        public System.Windows.Forms.Control DockerControl
        {
            get { return m_objectView; }
        }

        public IDockerFactory Factory { get { return m_factory; } }

        public void DockerVisibleChanged(bool visible)
        {
            m_objectView.IsContentVisible = visible;
        }

        public bool AllowClose() { return true; }
        public void OnClose()
        {
            m_objectView.OnClose();
            HTree.AfterDeleteNode -= HTree_AfterDeleteNode;
            HTree.FocusedNodeChanged -= HTree_FocusedNodeChanged;
        }

        #endregion

        public SummaryContentFrame ObjectView { get { return m_objectView; } }

        #region ITreeEventListener Members

        public void AfterDeleteNode(ITreeNode node)
        {
            m_objectView.SelectedObject = null;
        }

        #endregion
    }

    [DockerFactory(Title = "Object view", Name = "object_view")]
    public class ObjectViewDockerFactory : DockerFactoryBase
    {
        public override IDocker CreateDocker()
        {
            return new SummaryDocker(this);
        }

        public override string MenuTitle
        {
            get { return "s_summary"; }
        }

        public override Bitmap Icon
        {
            get { return CoreIcons.info; }
        }

        public override Keys Shortcut
        {
            get { return Keys.Control | Keys.Alt | Keys.O; }
        }
    }

    [MenuExtender(Name = "summaryext")]
    public class SummaryExtenders : MenuExtenderBase
    {
        public override void GetToolbarItems(string toolbarName, List<ToolStripItem> items)
        {
            if (toolbarName == "main")
            {
                var btn = new ToolStripButton(Texts.Get("s_dashboard"), CoreIcons.dashboard);
                btn.Click += new EventHandler(btn_Click);
                items.Add(btn);
            }
        }

        void btn_Click(object sender, EventArgs e)
        {
            MainWindow.Instance.ShowDocker(new ObjectViewDockerFactory());
        }
    }
}
