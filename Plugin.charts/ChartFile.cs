using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;

namespace Plugin.charts
{
    [FileHandler(Name = "chart", RequiredFeature = ChartsFeature.Test)]
    public class ChartFileFormat : FileHandlerBase
    {
        public override string Extension
        {
            get { return "cha"; }
        }

        public override string Description
        {
            get { return Texts.Get("s_chart"); }
        }

        public override void OpenAction()
        {
            if (ChartFrame.FindOpenedFile(m_file.DataDiskPath)) return;
            MainWindow.Instance.OpenContent(new ChartFrame(m_file));
        }

        public override ITreeNode CreateNode(ITreeNode parent)
        {
            return new Chart_TreeNode(parent, this);
        }

        public override FileHandlerCaps Caps
        {
            get
            {
                return new FileHandlerCaps
                {
                    AllFlags = false,
                    CreateNode = true,
                    OpenAction = true
                };
            }
        }
    }

    public class Chart_TreeNode : VirtualFileTreeNodeBase//, IFileSystemTreeNode
    {
        public Chart_TreeNode(ITreeNode parent, IFileHandler han)
            : base(parent, han)
        {
        }

        public override ITreeNode[] GetChildren()
        {
            return new ITreeNode[] { };
        }

        public override string TypeTitle
        {
            get { return "s_chart"; }
        }

        [PopupMenu("s_open", Weight = MenuWeights.EDIT)]
        public void OpenChart()
        {
            if (ChartFrame.FindOpenedFile(m_file.DataDiskPath)) return;
            MainWindow.Instance.OpenContent(new ChartFrame(m_file));
        }

        public override bool DoubleClick()
        {
            OpenChart();
            return true;
        }

        public override System.Drawing.Bitmap Image
        {
            get
            {
                return CoreIcons.chart;
            }
        }
    }
}
