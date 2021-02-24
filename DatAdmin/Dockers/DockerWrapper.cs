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
    public partial class DockerWrapper : DockContent,  IDockWrapper
    {
        IDockerFactory m_factory;
        IDocker m_docker;
        Control m_control;
        ContentFrame m_frame;
        bool m_closeOnEsc;
        Control m_onEscControl;

        public DockerWrapper(IDockerFactory factory)
        {
            InitializeComponent();
            m_factory = factory;
            m_docker = m_factory.CreateDocker();
            m_control = m_docker.DockerControl;
            m_frame = m_control as ContentFrame;
            if (m_frame != null) m_frame.m_dockWrapper = this;
            Controls.Add(m_control);
            Translating.TranslateControl(m_control);
            m_control.Dock = DockStyle.Fill;
            Text = Texts.Get(m_factory.MenuTitle);
            Icon = Icon.FromHandle(new Bitmap(m_factory.Icon).GetHicon());
            //this.HideOnClose = true;
            PerformLayout();
        }

        public IDocker Docker { get { return m_docker; } }
        public ContentFrame AsContent { get { return m_frame; } }

        protected override string GetPersistString()
        {
            return m_factory.GetPersistString();
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            m_docker.DockerVisibleChanged(Visible);
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            if (!AllowClose()) e.Cancel = true;
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            m_docker.OnClose();
            //if (ClosedEvent != null) ClosedEvent(this, EventArgs.Empty);
            HDocker.CallClosedDocker(m_docker);
        }

        #region IDockWrapper Members

        //public event EventHandler ClosedEvent;

        public void OnCloseWindow()
        {
            m_docker.OnClose();
            //if (ClosedEvent != null) ClosedEvent(this, EventArgs.Empty);
        }

        public bool AllowClose()
        {
            return m_docker.AllowClose();
        }

        public void UpdateTitle()
        {
            Text = Texts.Get(m_factory.MenuTitle);
        }
        public void UpdateIcon()
        {
            Icon = Icon.FromHandle(new Bitmap(m_factory.Icon).GetHicon());
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

        #endregion

        public void SetCloseOnEsc()
        {
            m_closeOnEsc = true;
        }

        private void DockerWrapper_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (m_closeOnEsc) DockHandler.Close();
                else if (m_onEscControl != null) m_onEscControl.Focus();
            }
        }

        private void DockerWrapper_Leave(object sender, EventArgs e)
        {
            m_closeOnEsc = false;
            m_onEscControl = null;
        }

        public void SetOnEscControl(Control control)
        {
            m_onEscControl = control;
        }
    }
}
