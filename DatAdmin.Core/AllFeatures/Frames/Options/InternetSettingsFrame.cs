using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class InternetSettingsFrame : UserControl
    {
        InternetSettings m_settings;

        public InternetSettingsFrame(InternetSettings settings)
        {
            InitializeComponent();
            m_settings = settings;
            chbUseProxyServer.Checked = m_settings.UseProxy;
            chbUseSystemDefaultProxy.Checked = m_settings.UseSystemDefaultProxy;
            tbxLogin.Text = m_settings.ProxyLogin;
            tbxPassword.Text = m_settings.ProxyPassword;
            tbxServer.Text = m_settings.ProxyServer;
            UpdateEnabling();
        }

        private void UpdateEnabling()
        {
            chbUseSystemDefaultProxy.Enabled = tbxLogin.Enabled = tbxPassword.Enabled = chbUseProxyServer.Checked;
            tbxServer.Enabled = chbUseProxyServer.Checked && !chbUseSystemDefaultProxy.Checked;
        }

        private void chbUseProxyServer_CheckedChanged(object sender, EventArgs e)
        {
            m_settings.UseProxy = chbUseProxyServer.Checked;
            UpdateEnabling();
        }

        private void chbUseSystemDefaultProxy_CheckedChanged(object sender, EventArgs e)
        {
            m_settings.UseSystemDefaultProxy = chbUseSystemDefaultProxy.Checked;
            UpdateEnabling();
        }

        private void tbxServer_TextChanged(object sender, EventArgs e)
        {
            m_settings.ProxyServer = tbxServer.Text;
        }

        private void tbxLogin_TextChanged(object sender, EventArgs e)
        {
            m_settings.ProxyLogin = tbxLogin.Text;
        }

        private void tbxPassword_TextChanged(object sender, EventArgs e)
        {
            m_settings.ProxyPassword = tbxPassword.Text;
        }
    }
}
