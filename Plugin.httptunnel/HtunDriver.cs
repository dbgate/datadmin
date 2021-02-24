using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.ComponentModel;

namespace Plugin.httptunnel
{
    [TunnelDriver(Name = "http_tunnel", Title = "HTTP tunnel")]
    public class HtunDriver : TunnelDriverBase, ICustomPropertyPage
    {
        public HtunDriver()
        {
            Url = "http://server.com/datunnel_mysql.php";
            RealEncoding = System.Text.Encoding.UTF8;
        }

        public override System.Data.Common.DbProviderFactory ProviderFactory
        {
            get { return HtunProviderFactory.Instance; }
        }

        [Browsable(false)]
        public System.Text.Encoding RealEncoding
        {
            get { return System.Text.Encoding.GetEncoding(Encoding); }
            set { Encoding = value.WebName; }
        }

        string m_encoding;
        [DatAdmin.DisplayName("s_encoding")]
        [TypeConverter(typeof(EncodingTypeConverter))]
        [Category("s_database")]
        [XmlElem]
        public string Encoding
        {
            get { return m_encoding; }
            set { m_encoding = value; }
        }

        EncodingStyle m_encodingStyle;
        [DatAdmin.DisplayName("s_encoding_style")]
        [Category("s_database")]
        [XmlElem]
        public EncodingStyle EncodingStyle
        {
            get { return m_encodingStyle; }
            set { m_encodingStyle = value; }
        }

        string m_url;
        [DatAdmin.DisplayName("s_url")]
        [Category("s_database")]
        [XmlElem]
        public string Url
        {
            get { return m_url; }
            set { m_url = value; }
        }

        string m_httpLogin;
        [Category("HTTP Authentization")]
        [XmlElem]
        public string HttpLogin
        {
            get { return m_httpLogin; }
            set { m_httpLogin = value; }
        }

        string m_httpPassword;
        [Category("HTTP Authentization")]
        [PasswordPropertyText(true)]
        [XmlElem]
        public string HttpPassword
        {
            get { return m_httpPassword; }
            set { m_httpPassword = value; }
        }

        public override void GetConnectionKey(CacheKeyBuilder ckb)
        {
            ckb.Add(Encoding);
            ckb.Add(EncodingStyle);
            ckb.Add(Url);
            ckb.Add(HttpLogin);
            ckb.Add(HttpPassword);
        }

        #region ICustomPropertyPage Members

        public System.Windows.Forms.Control CreatePropertyPage()
        {
            return new HtunEditFrame(this);
        }

        #endregion

        public override void SaveConnectionParams(TunnelConnectionStringBuilder pars)
        {
            pars["Encoding"] = Encoding;
            pars["EncodingStyle"] = EncodingStyle.ToString();
            pars["Url"] = Url;
            pars["HttpLogin"] = HttpLogin;
            pars["HttpPassword"] = HttpPassword;
        }

        public override void LoadConnectionParams(TunnelConnectionStringBuilder pars)
        {
            Encoding = pars["Encoding"];
            EncodingStyle = (EncodingStyle)Enum.Parse(typeof(EncodingStyle), pars["EncodingStyle"]);
            Url = pars["Url"];
            HttpLogin = pars["HttpLogin"];
            HttpPassword = pars["HttpPassword"];
        }

        public override HashSetEx<string> GetSupportedKeys()
        {
            var res = new HashSetEx<string>();
            res.Add("Encoding");
            res.Add("EncodingStyle");
            res.Add("Url");
            res.Add("HttpLogin");
            res.Add("HttpPassword");
            return res;
        }
    }
}
