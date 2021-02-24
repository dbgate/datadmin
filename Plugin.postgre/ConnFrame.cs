using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DatAdmin;

namespace Plugin.postgre
{
    public partial class ConnFrame : ConnectionEditFrame
    {
        public ConnFrame(PostgreSqlStoredConnection conn)
            : base(conn)
        {
            InitializeComponent();
            tbxDataSource.Text = conn.DataSource;
            tbxLogin.Text = conn.Login;
            tbxPassword.Text = conn.Password;
            tbxPort.Text = conn.Port.ToString();
        }

        public override void SaveConnection()
        {
            PostgreSqlStoredConnection conn = (PostgreSqlStoredConnection)m_conn;
            conn.DataSource = tbxDataSource.Text;
            conn.Login = tbxLogin.Text;
            conn.Password = tbxPassword.Text;
            try { conn.Port = Int32.Parse(tbxPort.Text); }
            catch { conn.Port = 0; }
        }

        private void datasource_TextChanged(object sender, EventArgs e)
        {
            CallConnectionChanged();
        }
    }
}
