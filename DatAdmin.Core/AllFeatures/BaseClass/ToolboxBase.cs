using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    public abstract class ToolboxItemBase : IToolboxItem
    {
        protected RegisterAttribute RegAttrib
        {
            get
            {
                var reg = XmlTool.GetRegisterAttr(ItemType);
                return reg;
            }
        }

        #region IToolboxItem Members

        public virtual string Category
        {
            get { return "s_general"; }
        }

        public virtual string DisplayName
        {
            get
            {
                if (RegAttrib != null) return RegAttrib.Title; 
                return ItemType.FullName;
            }
        }

        public virtual string Description
        {
            get
            {
                if (RegAttrib != null) return RegAttrib.Description;
                return ItemType.FullName;
            }
        }

        public virtual System.Drawing.Bitmap Image
        {
            get { return null; }
        }

        public abstract Type ItemType
        {
            get;
        }

        #endregion
    }

    public class WidgetToolboxItem : ToolboxItemBase
    {
        IWidget m_widget;
        public string CategoryOverride;
        
        public WidgetToolboxItem(IWidget widget)
        {
            m_widget = widget;
        }

        public override Type ItemType
        {
            get { return m_widget.GetType(); }
        }

        public override System.Drawing.Bitmap Image
        {
            get { return m_widget.Image; }
        }

        public override string DisplayName
        {
            get { return m_widget.ToolDisplayName; }
        }

        public override string Category
        {
            get { return CategoryOverride ?? m_widget.ToolCategory ?? "General"; }
        }

        public IWidget Widget { get { return m_widget; } }

        public IWidget CreateWidget()
        {
            return Widget.CloneUsingXml();
        }
    }
}
