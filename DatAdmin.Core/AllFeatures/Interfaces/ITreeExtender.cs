using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace DatAdmin
{
    [AttributeUsage(AttributeTargets.Class)]
    public class TreeExtenderAttribute : RegisterAttribute
    {
    }

    public interface ITreeExtender
    {
        void GetNodeExtendObjects(ITreeNode node, List<object> objs);
        Bitmap GetImageOverride(ITreeNode node);
        void GetExtendedWidgets(ITreeNode node, List<IWidget> objviews);
        void GetExtendedChildren(ITreeNode parent, List<ITreeNode> children);
    }

    [AddonType]
    public class TreeExtenderAddonType : AddonType
    {
        public override string Name
        {
            get { return "treeextender"; }
        }

        public override Type InterfaceType
        {
            get { return typeof(ITreeExtender); }
        }

        public override Type RegisterAttributeType
        {
            get { return typeof(TreeExtenderAttribute); }
        }

        public static readonly TreeExtenderAddonType Instance = new TreeExtenderAddonType();
    }
}

