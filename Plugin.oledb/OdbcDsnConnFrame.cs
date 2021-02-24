using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DatAdmin;

namespace Plugin.oledb
{
    public partial class OdbcDsnConnFrame : ConnectionEditFrame
    {
        public OdbcDsnConnFrame()
        {
            InitializeComponent();
        }

        public OdbcDsnConnFrame(OdbcDsnStoredConnection conn)
            : base(conn)
        {
            InitializeComponent();
            cbxDataSource.Text = conn.DataSourceName;
            tbxPassword.Text = conn.Password;
            tbxLogin.Text = conn.Login;
            foreach (string ds in DsnEnumerator.GetAllDataSourceNames())
            {
                cbxDataSource.Items.Add(ds);
            }
        }

        public override void SaveConnection()
        {
            base.SaveConnection();
            var conn = (OdbcDsnStoredConnection)m_conn;
            conn.DataSourceName = cbxDataSource.Text;
            conn.Login = tbxLogin.Text;
            conn.Password = tbxPassword.Text;
        }

        private void tbxLogin_TextChanged(object sender, EventArgs e)
        {
            CallConnectionChanged();
        }
    }
}
