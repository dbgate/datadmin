using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class ConnectionEditFrame : UserControl
    {
        // editing this connection
        protected IStoredConnection m_conn;
        public ConnectionEditFrame(IStoredConnection conn)
        {
            m_conn = conn;
            InitializeComponent();
        }
        public ConnectionEditFrame()
        {
            InitializeComponent();
        }

        public virtual void SaveConnection()
        {
        }

        public IStoredConnection Connection { get { return m_conn; } }

        public event EventHandler ConnectionChanged;

        protected void CallConnectionChanged()
        {
            if (ConnectionChanged != null) ConnectionChanged(this, EventArgs.Empty);
        }
    }
}
