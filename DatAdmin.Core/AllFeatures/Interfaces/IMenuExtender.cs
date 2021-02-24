using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    [AttributeUsage(AttributeTargets.Class)]
    public class MenuExtenderAttribute : RegisterAttribute
    {
    }

    public interface IMenuExtender
    {
        void GetPopupMenu(ITreeNode node, MenuBuilder mb);
        void GetMainMenu(string menuName, MenuBuilder mb);
        void GetToolbarItems(string toolbarName, List<ToolStripItem> items);
    }

    public abstract class MenuExtenderBase : AddonBase, IMenuExtender
    {
        public override AddonType AddonType
        {
            get { return MenuExtenderAddonType.Instance; }
        }

        #region IMenuExtender Members

        public virtual void GetPopupMenu(ITreeNode node, MenuBuilder mb) { }
        public virtual void GetMainMenu(string menuName, MenuBuilder mb) { }
        public virtual void GetToolbarItems(string toolbarName, List<ToolStripItem> items) { }

        #endregion
    }

    [AddonType]
    public class MenuExtenderAddonType : AddonType
    {
        public override string Name
        {
            get { return "menuextender"; }
        }

        public override Type InterfaceType
        {
            get { return typeof(IMenuExtender); }
        }

        public override Type RegisterAttributeType
        {
            get { return typeof(MenuExtenderAttribute); }
        }

        public static readonly MenuExtenderAddonType Instance = new MenuExtenderAddonType();
    }
}

