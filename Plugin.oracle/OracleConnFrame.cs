using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DatAdmin;

namespace Plugin.oracle
{
    public partial class OracleConnFrame : ConnectionEditFrame
    {
        public OracleConnFrame(OracleStoredConnection conn)
            : base(conn)
        {
            InitializeComponent();
            cbxConnectionType.Items.Add("Basic");
            cbxConnectionType.Items.Add("TNS");
            tbxLogin.Text = conn.Login;
            tbxPassword.Text = conn.Password;
            tbxHost.Text = conn.DataSource;
            tbxPort.Text = conn.Port.ToString();
            tbxService.Text = conn.ServiceName;
            chbServiceName.Checked = conn.ServiceMode == OracleServiceMode.ServiceName;
            chbSID.Checked = conn.ServiceMode == OracleServiceMode.SID;
            if (conn.ConnectionType == OracleConnectionType.Basic) cbxConnectionType.SelectedIndex = 0;
            if (conn.ConnectionType == OracleConnectionType.TNS) cbxConnectionType.SelectedIndex = 1;
            cbxConnectionType_SelectedIndexChanged(this, EventArgs.Empty);
        }

        private void cbxConnectionType_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool botvis = true;
            if (cbxConnectionType.SelectedIndex == 0)
            {
                labService.Text = Texts.Replace("{s_service_name}/SID");
                botvis = true;
            }
            if (cbxConnectionType.SelectedIndex == 1)
            {
                labService.Text = Texts.Replace("Net Service Name");
                botvis = false;
            }
            panel1.Visible = labHost.Visible = labPort.Visible = tbxHost.Visible = tbxPort.Visible = botvis;
        }

        public override void SaveConnection()
        {
            OracleStoredConnection conn = (OracleStoredConnection)m_conn;
            conn.ConnectionType = cbxConnectionType.SelectedIndex == 0 ? OracleConnectionType.Basic : OracleConnectionType.TNS;
            conn.ServiceMode = chbServiceName.Checked ? OracleServiceMode.ServiceName : OracleServiceMode.SID;
            conn.Login = tbxLogin.Text;
            conn.Password = tbxPassword.Text;
            try { conn.Port = Int32.Parse(tbxPort.Text); }
            catch { conn.Port = 1521; }
            conn.ServiceName = tbxService.Text;
            conn.DataSource = tbxHost.Text;
        }
    }
}
