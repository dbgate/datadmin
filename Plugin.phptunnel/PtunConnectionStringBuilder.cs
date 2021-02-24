using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.ComponentModel;

namespace Plugin.phptunnel
{
    public class PtunConnectionStringBuilder : TunnelConnectionStringBuilder
    {
        public PtunConnectionStringBuilder()
        {
            Url = "http://koks.wz.cz/phptunnel.php";
            RealEncoding = System.Text.Encoding.UTF8;
            Engine = "mysql";
        }

        [Browsable(false)]
        public System.Text.Encoding RealEncoding
        {
            get { return System.Text.Encoding.GetEncoding(Encoding); }
            set { Encoding = value.WebName; }
        }

        [DatAdmin.DisplayName("s_encoding")]
        [TypeConverter(typeof(EncodingTypeConverter))]
        [Category("s_database")]
        [XmlElem]
        public string Encoding
        {
            get { return this["Encoding"]; }
            set { this["Encoding"] = value; }
        }

        [DatAdmin.DisplayName("s_encoding_style")]
        [Category("s_database")]
        [XmlElem]
        public EncodingStyle EncodingStyle
        {
            get { return (EncodingStyle)Enum.Parse(typeof(EncodingStyle), Get("EncodingStyle", EncodingStyle.DEFAULT.ToString()), true); }
            set { this["EncodingStyle"] = value.ToString(); }
        }

        [DatAdmin.DisplayName("s_url")]
        [Category("s_database")]
        [XmlElem]
        public string Url
        {
            get { return this["Url"]; }
            set { this["Url"] = value; }
        }

        [Browsable(false)]
        [XmlElem]
        public string Check
        {
            get { return this["Check"]; }
            set { this["Check"] = value; }
        }

        [Category("HTTP Authentization")]
        [XmlElem]
        public string HttpLogin
        {
            get { return this["HttpLogin"]; }
            set { this["HttpLogin"] = value; }
        }

        [Category("HTTP Authentization")]
        [PasswordPropertyText(true)]
        [XmlElem]
        public string HttpPassword
        {
            get { return this["HttpPassword"]; }
            set { this["HttpPassword"] = value; }
        }


        [TypeConverter(typeof(YesNoTypeConverter))]
        [Description("s_http_extended_safety")]
        [XmlElem]
        public bool ExtendedSafety
        {
            get { return (bool?)XmlTool.ValueFromString(typeof(bool), this["ExtendedSafety"]) ?? false; }
            set { this["ExtendedSafety"] = value ? "1" : "0"; }
        }

        public override HashSetEx<string> SupportedKeys
        {
            get
            {
                var res = base.SupportedKeys;
                res.Add("Encoding");
                res.Add("EncodingStyle");
                res.Add("Url");
                res.Add("Check");
                res.Add("ProxyServer");
                res.Add("ProxyLogin");
                res.Add("ProxyPassword");
                res.Add("HttpLogin");
                res.Add("HttpPassword");
                res.Add("ExtendedSafety");
                return res;
            }
        }

        public byte[] GetPhpTunnelFile()
        {
            string cnt = System.Text.Encoding.ASCII.GetString(Resource1.phptunnel);
            cnt = cnt.Replace("#CHECK#", Check);
            cnt = cnt.Replace("#EXTENDEDSAFETY#", ExtendedSafety ? "1" : "0");
            cnt = cnt.Replace("#VERSION#", WebResultSet.VERSION.ToString());
            return System.Text.Encoding.ASCII.GetBytes(cnt);
        }
    }
}
