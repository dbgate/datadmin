using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;

namespace Plugin.diagrams
{
    public class Diagrams_TreeNode : VirtualFolderTreeNode
    {
        internal IDatabaseSource m_conn;
        string m_titlePostfix;
        public Diagrams_TreeNode(IDatabaseSource conn, IVirtualFolder folder, ITreeNode parent, string titlePostfix, string namePostfix)
            : base(parent, folder, "diagrams" + namePostfix)
        {
            m_conn = conn;
            m_titlePostfix = titlePostfix;
        }

        public override System.Drawing.Bitmap Image { get { return CoreIcons.diagram; } }
        public override System.Drawing.Bitmap ExpandedImage { get { return CoreIcons.diagram; } }

        public override bool AllowCreate(string group, string name)
        {
            return name == "diagram";
        }

        public override string Title
        {
            get { return Texts.Get("s_diagrams") + m_titlePostfix; }
        }
    }

    public class Diagram_TreeNode : VirtualFileTreeNodeBase
    {
        internal IDatabaseSource m_conn = null;
        public Diagram_TreeNode(ITreeNode parent, IFileHandler fhandler)
            : base(parent, fhandler)
        {
            if (parent is Diagrams_TreeNode)
            {
                m_conn = ((Diagrams_TreeNode)parent).m_conn;
            }
        }

        public override ITreeNode[] GetChildren()
        {
            return new ITreeNode[] { };
        }

        public override string TypeTitle
        {
            get { return "s_diagram"; }
        }

        [PopupMenu("s_edit")]
        public void EditDiagram()
        {
            MainWindow.Instance.OpenContent(new DiagramEditFrame(m_file, m_conn != null ? m_conn.CloneSource() : null));
        }

        public override bool DoubleClick()
        {
            EditDiagram();
            return true;
        }

        public override System.Drawing.Bitmap Image { get { return CoreIcons.diagram; } }
    }

    [TreeExtender(Name = "diagrams")]
    public class DiagramsTreeExtender : TreeExtenderBase
    {
        private bool CanBeUsed(ITreeNode node)
        {
            return node is IDatabaseTreeNode;
        }

        public override void GetExtendedChildren(ITreeNode parent, List<ITreeNode> children)
        {
            if (!DiagramsFeature.Allowed) return;
            if (!CanBeUsed(parent)) return;
            IDatabaseSource dbconn = ((IDatabaseTreeNode)parent).DatabaseConnection;
            TreeNodeExtension.AddFolderNodes(children, "diagrams", (folder, postfix, namePostfix) => new Diagrams_TreeNode(dbconn, folder, parent, postfix, namePostfix), dbconn);
        }
    }

    [ConfigNodeHandler(Name = "diagrams")]
    public class ConfigTreeConfigLoader : ConfigNodeHandlerBase
    {
        public ConfigTreeConfigLoader()
        {
            if (!DiagramsFeature.Allowed) return;
            DefineSubFolder("data", "diagrams", "s_diagrams", CoreIcons.diagram);
            DefineFile("data", ".dia", CoreIcons.diagram, true);
        }
    }
}
