using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DatAdmin;

namespace Plugin.mssql
{
    public partial class MsSqlConnFrame : ConnectionEditFrame
    {
        public MsSqlConnFrame(MsSqlStoredConnection conn)
            : base(conn)
        {
            InitializeComponent();
            tbxDataSource.Text = conn.DataSource;
            cbxAuthentization.SelectedIndex = (conn.Authentization == MsSqlAuth.SQL) ? 1 : 0;
            tbxPassword.Text = conn.Password;
            tbxLogin.Text = conn.Login;
        }

        public override void SaveConnection()
        {
            MsSqlStoredConnection conn = (MsSqlStoredConnection)m_conn;
            conn.Authentization = (cbxAuthentization.SelectedIndex == 1) ? MsSqlAuth.SQL : MsSqlAuth.SSPI;
            conn.DataSource = tbxDataSource.Text;
            conn.Login = tbxLogin.Text;
            conn.Password = tbxPassword.Text;
        }

        private void datasource_TextChanged(object sender, EventArgs e)
        {
            CallConnectionChanged();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool enabledcr = cbxAuthentization.SelectedIndex == 1;
            tbxLogin.Enabled = enabledcr;
            tbxPassword.Enabled = enabledcr;
            datasource_TextChanged(sender, e);
        }
    }
}
