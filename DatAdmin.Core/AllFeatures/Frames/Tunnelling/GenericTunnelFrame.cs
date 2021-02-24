using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class GenericTunnelFrame : ConnectionEditFrame
    {
        GenericTunnelStoredConnection m_conn;

        public GenericTunnelFrame()
        {
            InitializeComponent();
        }

        public GenericTunnelStoredConnection Connection
        {
            get { return m_conn; }
            set
            {
                m_conn = value;

                if (m_conn == null)
                {
                    tbxHost.Text = "";
                    tbxUser.Text = "";
                    tbxPassword.Text = "";
                    tbxEngine.Text = "";
                    tbxPort.Text = "";
                    return;
                }

                tbxHost.Text = m_conn.Host;
                tbxUser.Text = m_conn.Login;
                tbxPassword.Text = m_conn.Password;
                tbxEngine.Text = m_conn.Engine;
                tbxPort.Text = m_conn.Port.ToString();
                tbxDatabase.Text = m_conn.InitialDatabase;
            }
        }

        public override void SaveConnection()
        {
            m_conn.Host = tbxHost.Text;
            m_conn.Login = tbxUser.Text;
            m_conn.Password = tbxPassword.Text;
            m_conn.Engine = tbxEngine.Text;
            try { m_conn.Port = Int32.Parse(tbxPort.Text); }
            catch { m_conn.Port = 0; }
            m_conn.InitialDatabase = tbxDatabase.Text;
        }
    }
}
