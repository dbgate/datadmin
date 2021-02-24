using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace DatAdmin
{
    public partial class ContentWrapper : DockContent, IDockWrapper
    {
        ContentFrame m_frame;
        string m_persistString;

        public ContentWrapper(ContentFrame frame)
        {
            InitializeComponent();
            m_frame = frame;
            Controls.Add(frame);
            frame.Dock = DockStyle.Fill;
            frame.PerformLayout();
            Text = Texts.Get(frame.PageTitle);
            ToolTipText = Texts.Get(frame.PageToolTip);
            if (frame.Image != null) Icon = Icon.FromHandle(frame.Image.GetHicon());
            m_frame.m_dockWrapper = this;
        }

        public ContentWrapper(ContentFrame frame, string persistString)
            : this(frame)
        {
            m_persistString = persistString;
        }

        //public event EventHandler ClosedEvent;

        public ContentFrame Frame { get { return m_frame; } }

        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);
            if (m_frame != null) m_frame.IsContentVisible = Visible;
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            if (m_frame != null && !m_frame.AllowClose()) e.Cancel = true;
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            if (m_frame != null) m_frame.OnClose();
            //if (ClosedEvent != null) ClosedEvent(this, EventArgs.Empty);
        }

        protected override string GetPersistString()
        {
            return m_persistString;
        }

        public string PersistString { get { return m_persistString; } }

        #region ICloseQueryable Members

        public bool AllowClose()
        {
            return m_frame.AllowClose();
        }

        #endregion

        public void UpdateTitle()
        {
            Text = Texts.Get(m_frame.PageTitle);
            ToolTipText = Texts.Get(m_frame.PageToolTip);
        }
        public void UpdateIcon()
        {
            Icon = Icon.FromHandle(m_frame.Image.GetHicon());
        }

        public void ReplaceContent(ContentFrame newframe)
        {
            if (m_frame != null)
            {
                Controls.Remove(m_frame);
                m_frame.m_dockWrapper = null;
            }
            m_frame = newframe;
            if (m_frame != null)
            {
                Controls.Add(m_frame);
                Text = Texts.Get(m_frame.PageTitle);
                ToolTipText = Texts.Get(m_frame.PageToolTip);
                if (m_frame.Image != null) Icon = Icon.FromHandle(m_frame.Image.GetHicon());
                m_frame.m_dockWrapper = this;
                m_frame.Dock = DockStyle.Fill;
                m_frame.PerformLayout();
            }
        }

        public void OnCloseWindow()
        {
            //if (ClosedEvent != null) ClosedEvent(this, EventArgs.Empty);
        }
    }
}
