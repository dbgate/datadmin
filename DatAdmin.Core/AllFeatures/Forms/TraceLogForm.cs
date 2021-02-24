using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class TraceLogForm : FormEx
    {
        public TraceLogForm()
        {
            InitializeComponent();
        }

        public ILogMessageSource Source
        {
            get { return messageLogFrame1.Source; }
            set { messageLogFrame1.Source = value; }
        }
    }

    public class TraceVisibilityHandler : IDisposable
    {
        ToolStripButton m_button;
        TraceLogForm m_form;
        ILogMessageSource m_source;

        public TraceVisibilityHandler(ToolStripButton button)
        {
            m_button = button;
            m_button.Click += new EventHandler(m_button_Click);
        }

        void m_form_Disposed(object sender, EventArgs e)
        {
            m_form = null;
        }

        void m_button_Click(object sender, EventArgs e)
        {
            if (m_form != null)
            {
                m_form.Show();
                m_form.BringToFront();
            }
            else
            {
                m_form = new TraceLogForm();
                m_form.Source = m_source;
                m_form.Show();
                m_form.Disposed += new EventHandler(m_form_Disposed);
            }
        }

        public ILogMessageSource Source
        {
            get { return m_source; }
            set
            {
                m_source = value;
                if (m_form != null) m_form.Source = m_source;
            }
        }


        #region IDisposable Members

        public void Dispose()
        {
            if (m_form != null) m_form.Dispose();
        }

        #endregion
    }
}
