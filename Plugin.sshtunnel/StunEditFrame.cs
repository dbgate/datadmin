using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DatAdmin;

namespace Plugin.sshtunnel
{
    public partial class StunEditFrame : TunnelConnFrameBase
    {
        public StunEditFrame(StunStoredConnection conn)
            : base(conn)
        {
            InitializeComponent();
            cbxAuthentization.Items.Add(Texts.Get("s_password"));
            cbxAuthentization.Items.Add(Texts.Get("s_public_key"));
            tbxServer.Text = Params.SshHost;
            tbxPort.Text = Params.SshPort.ToString();
            tbxLogin.Text = Params.SshLogin;
            switch (Params.Authentization)
            {
                case SshAuthentization.Password:
                    cbxAuthentization.SelectedIndex = 0;
                    tbxPassword.Text = Params.SshPassword;
                    break;
                case SshAuthentization.PublicKey:
                    cbxAuthentization.SelectedIndex = 1;
                    tbxPassword.Text = Params.SshPassphrase;
                    break;
            }
            tbxPrivateKeyFile.Text = Params.IdentifyFile;
        }

        public StunConnectionStringBuilder Params
        {
            get { return (StunConnectionStringBuilder)m_conn.Params; }
        }

        public override void SaveConnection()
        {
            Params.SshHost = tbxServer.Text;
            Params.SshPort = Int32.Parse(tbxPort.Text);
            Params.SshLogin = tbxLogin.Text;
            switch (cbxAuthentization.SelectedIndex)
            {
                case 0:
                    Params.Authentization = SshAuthentization.Password;
                    Params.SshPassword = tbxPassword.Text;
                    break;
                case 1:
                    Params.Authentization = SshAuthentization.PublicKey;
                    Params.SshPassphrase = tbxPassword.Text;
                    break;
            }
            Params.IdentifyFile = tbxPrivateKeyFile.Text;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = tbxPrivateKeyFile.Text;
            if (openFileDialog1.ShowDialogEx() == DialogResult.OK)
            {
                tbxPrivateKeyFile.Text = openFileDialog1.FileName;
            }
        }

        private void cbxAuthentization_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbxPrivateKeyFile.Enabled = btnBrowse.Enabled = cbxAuthentization.SelectedIndex == 1;
        }
    }
}
