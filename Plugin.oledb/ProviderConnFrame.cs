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
    public partial class ProviderConnFrame : ConnectionEditFrame
    {
        public ProviderConnFrame()
        {
            InitializeComponent();
        }


        public ProviderConnFrame(ProviderStoredConnection conn)
            : base(conn)
        {
            InitializeComponent();
            tbxDataSource.Text = conn.DataSource;
            cbxProvider.Text = conn.Provider;
            tbxPassword.Text = conn.Password;
            tbxLogin.Text = conn.Login;
            tbxPort.Text = conn.Port.ToString();
            foreach (string provider in conn.GetProviders())
            {
                cbxProvider.Items.Add(provider);
            }
        }

        public override void SaveConnection()
        {
            ProviderStoredConnection conn = (ProviderStoredConnection)m_conn;
            conn.DataSource = tbxDataSource.Text;
            conn.Login = tbxLogin.Text;
            conn.Password = tbxPassword.Text;
            conn.Provider = cbxProvider.Text;
            conn.Port = tbxPort.Text.SafeIntParse();
        }

        private void tbxDataSource_TextChanged(object sender, EventArgs e)
        {
            CallConnectionChanged();
        }
    }
}
