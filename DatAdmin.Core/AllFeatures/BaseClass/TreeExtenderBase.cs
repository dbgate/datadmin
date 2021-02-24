using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace DatAdmin
{
    public abstract class TreeExtenderBase : AddonBase, ITreeExtender
    {
        #region ITreeExtender Members

        public virtual void GetNodeExtendObjects(ITreeNode node, List<object> objs) { }
        public virtual Bitmap GetImageOverride(ITreeNode node)
        {
            return null;
        }
        public virtual void GetExtendedWidgets(ITreeNode node, List<IWidget> objviews) { }
        public virtual void GetExtendedChildren(ITreeNode parent, List<ITreeNode> children) { }

        #endregion

        public override AddonType AddonType
        {
            get { return TreeExtenderAddonType.Instance; }
        }

        public class NodeExtenderBase
        {
            public readonly ITreeNode Node;
            public NodeExtenderBase(ITreeNode node)
            {
                Node = node;
            }
        }
    }
}
