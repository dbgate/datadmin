using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.ComponentModel;
using System.Drawing;

namespace DatAdmin
{
    public abstract class WidgetBase : AddonBase, IWidget, IConnectionPackHolder
    {
        ConnectionPack m_connPack;
        protected WidgetBaseFrame m_ctrl;
        AppObject m_appobj;
        bool m_isDesigning;
        Bitmap m_icon;
        string m_title;

        #region IWidget Members

        [Browsable(false)]
        public abstract string DefaultPageTitle { get; }

        [Browsable(false)]
        public virtual int Weight { get { return 0; } }
        [Browsable(false)]
        public virtual Bitmap DefaultImage { get { return null; } }


        protected abstract WidgetBaseFrame CreateControl();

        public virtual WidgetBaseFrame GetControl()
        {
            if (m_ctrl == null)
            {
                SetControl(CreateControl());
            }
            return m_ctrl;
        }

        private void SetControl(WidgetBaseFrame ctrl)
        {
            m_ctrl = ctrl;
            if (ConnPack != null) m_ctrl.ConnPack = ConnPack;
            m_ctrl.LoadWidgetData(m_appobj);
        }

        public abstract Type GetControlType();

        public virtual void LoadWidgetData(AppObject ao)
        {
            m_appobj = ao;
            if (m_ctrl != null) m_ctrl.LoadWidgetData(ao);
        }

        [Browsable(false)]
        public ConnectionPack ConnPack
        {
            get { return m_connPack; }
            set
            {
                m_connPack = value;
                if (m_ctrl != null)
                {
                    m_ctrl.ConnPack = value;
                    if (value != null)
                    {
                        m_ctrl.LoadWidgetData(m_appobj);
                    }
                }
            }
        }

        [Browsable(false)]
        public virtual string ToolDisplayName
        {
            get
            {
                var attr = XmlTool.GetRegisterAttr(this);
                if (attr != null) return attr.Title;
                return null;
            }
        }

        [Browsable(false)]
        public virtual string ToolCategory
        {
            get
            {
                var attr = XmlTool.GetRegisterAttr(this) as WidgetAttribute;
                if (attr != null) return attr.Category;
                return null;
            }
        }


        #endregion

        [Browsable(false)]
        public override AddonType AddonType
        {
            get { return WidgetAddonType.Instance; }
        }

        public virtual bool CanReplaceWith(IWidget widget)
        {
            return widget is WidgetBase && widget.GetControlType() == GetControlType();
        }

        public virtual void ReplaceControl(IWidget widget)
        {
            if (!CanReplaceWith(widget)) throw new InternalError("DAE-00051 Incompatible widget to replace");
            if (m_ctrl != null)
            {
                m_ctrl.SetWidget(widget);
                ((WidgetBase)widget).SetControl(m_ctrl);
                m_ctrl = null;
            }
        }

        [Browsable(false)]
        public bool IsDesigning
        {
            get { return m_isDesigning; }
            set
            {
                if (m_isDesigning != value)
                {
                    m_isDesigning = value;
                    OnChangedDesigning();
                    if (value) OnBeginDesign();
                    else OnFinishDesign();
                }
            }
        }

        protected virtual void OnBeginDesign()
        {
            if (m_ctrl != null) m_ctrl.OnBeginDesign();
        }

        protected virtual void OnFinishDesign()
        {
            if (m_ctrl != null) m_ctrl.OnFinishDesign();
            HDesigner.CallHideProperties(this);
        }

        protected virtual void OnChangedDesigning()
        {
            if (m_ctrl != null) m_ctrl.OnChangedDesigning();
        }

        protected void CallChangedData()
        {
            if (m_ctrl != null) m_ctrl.LoadWidgetData(m_appobj);
        }

        [Browsable(false)]
        public string PageTitle
        {
            get
            {
                if (!String.IsNullOrEmpty(Text)) return Text;
                return DefaultPageTitle;
            }
        }
        [Browsable(false)]
        public Bitmap Image { get { return m_icon ?? DefaultImage; } }

        public bool UseIcon
        {
            get { return Icon != null; }
            set
            {
                if (!value) Icon = null;
            }
        }

        [XmlElem]
        public string Text
        {
            get { return m_title; }
            set
            {
                m_title = value;
                HDesigner.CallChangedWidget(this, HDesigner.WidgetPart.Title);
            }
        }

        [XmlElem]
        public Bitmap Icon
        {
            get { return m_icon; }
            set
            {
                m_icon = value;
                HDesigner.CallChangedWidget(this, HDesigner.WidgetPart.Icon);
            }
        }
    }
}
