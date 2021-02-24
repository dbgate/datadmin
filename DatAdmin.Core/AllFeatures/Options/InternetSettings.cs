using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.ComponentModel;

namespace DatAdmin
{
    [SettingsPage(Name = "internet", Title = "s_internet", Targets = SettingsTargets.Global, ImageName = CoreIcons.internetName)]
    public class InternetSettings : SettingsPageBase, ICustomPropertyPage
    {
        bool m_useProxy;
        [SettingsKey("internet.proxy.use_proxy")]
        public bool UseProxy
        {
            get { return m_useProxy; }
            set { m_useProxy = value; }
        }

        bool m_useSystemDefaultProxy;
        [SettingsKey("internet.proxy.use_system_default")]
        public bool UseSystemDefaultProxy
        {
            get { return m_useSystemDefaultProxy; }
            set { m_useSystemDefaultProxy = value; }
        }

        string m_proxyServer;
        [SettingsKey("internet.proxy.proxy_server")]
        public string ProxyServer
        {
            get { return m_proxyServer; }
            set { m_proxyServer = value; }
        }

        string m_proxyLogin;
        [SettingsKey("internet.proxy.proxy_login")]
        public string ProxyLogin
        {
            get { return m_proxyLogin; }
            set { m_proxyLogin = value; }
        }

        string m_proxyPassword;
        public string ProxyPassword
        {
            get { return m_proxyPassword; }
            set { m_proxyPassword = value; }
        }

        [Browsable(false)]
        [SettingsKey("internet.proxy.proxy_password")]
        public string XmlProxyPassword
        {
            get { return XmlTool.SafeEncodeString(ProxyPassword); }
            set { ProxyPassword = XmlTool.SafeDecodeString(value); }
        }

        #region ICustomPropertyPage Members

        public System.Windows.Forms.Control CreatePropertyPage()
        {
            return new InternetSettingsFrame(this);
        }

        #endregion

        public IWebProxy GetProxy()
        {
            if (!UseProxy) return null;
            IWebProxy res = null;
            try
            {
                if (UseSystemDefaultProxy)
                {
                    res = HttpWebRequest.GetSystemWebProxy();
                }
                else
                {
                    res = new WebProxy { Address = new Uri(m_proxyServer) };
                }
                if (!m_proxyLogin.IsEmpty())
                {
                    res.Credentials = new NetworkCredential(m_proxyLogin, m_proxyPassword);
                }
                return res;
            }
            catch
            {
                return null;
            }
        }

        public static void Initialize()
        {
            HSettings.ReloadSettings += new Action(HSettings_ReloadSettings);
            HSettings_ReloadSettings();
        }

        static void HSettings_ReloadSettings()
        {
            HttpWebRequest.DefaultWebProxy = GlobalSettings.Pages.Internet().GetProxy();
        }
    }

    public static class SettingsPageCollection_Internet
    {
        public static InternetSettings Internet(this SettingsPageCollection col)
        {
            return (InternetSettings)col.PageByName("internet");
        }
    }
}
