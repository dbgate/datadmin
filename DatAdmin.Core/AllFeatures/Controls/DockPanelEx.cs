using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace DatAdmin
{
    public class DockPanelEx : DockPanel
    {
        Form m_form;
        FormWindowState m_lastState;
        //DateTime m_lastResized = DateTime.Now;

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            BindResize();
        }

        private void BindResize()
        {
            UnbindResize();
            var ctrl = Parent;
            while (ctrl != null && !(ctrl is Form)) ctrl = ctrl.Parent;
            if (ctrl is Form)
            {
                m_form = (Form)ctrl;
                m_form.Resize += m_form_Resize;
                //m_form.WindowState
            }
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            base.OnHandleDestroyed(e);
            UnbindResize();
        }

        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            BindResize();
        }

        void m_form_Resize(object sender, EventArgs e)
        {
            if (m_form == null || MainWindow.Instance == null) return;
            if (m_form.WindowState != FormWindowState.Minimized && m_lastState == FormWindowState.Minimized
                ||
                m_form.WindowState == FormWindowState.Maximized && m_lastState != FormWindowState.Maximized)
            {
                MainWindow.Instance.RunInMainWindow(Resized);
            }
            m_lastState = m_form.WindowState;
        }

        private void Resized()
        {
            Height -= 7;
            //if ((DateTime.Now - m_lastResized).TotalSeconds < 1) return;
            //Height -= 10;
            //Height += 10;
            Invalidate();
            //m_lastResized = DateTime.Now;
        }

        private void UnbindResize()
        {
            if (m_form == null) return;
            m_form.Resize -= m_form_Resize;
            m_form = null;
        }
    }
}
