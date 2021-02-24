using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.ComponentModel;

namespace Plugin.sshtunnel
{
    public class StunConnectionStringBuilder : TunnelConnectionStringBuilder
    {
        public StunConnectionStringBuilder()
        {
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

        [DatAdmin.DisplayName("s_host")]
        [Category("SSH")]
        [XmlElem]
        public string SshHost
        {
            get { return this["SshHost"]; }
            set { this["SshHost"] = value; }
        }

        [Category("SSH")]
        [XmlElem]
        public string SshLogin
        {
            get { return this["SshLogin"]; }
            set { this["SshLogin"] = value; }
        }


        [Category("SSH")]
        [XmlElem]
        public int SshPort
        {
            get
            {
                if (ContainsKey("SshPort")) return Int32.Parse(this["SshPort"]);
                return 22;
            }
            set
            {
                if (value == 0 || value == 22) Remove("SshPort");
                else this["SshPort"] = value.ToString();
            }
        }

        [Category("SSH")]
        [PasswordPropertyText(true)]
        [XmlElem]
        public string SshPassword
        {
            get { return this["SshPassword"]; }
            set { this["SshPassword"] = value; }
        }

        [Category("SSH")]
        [PasswordPropertyText(true)]
        [XmlElem]
        public string SshPassphrase
        {
            get { return this["SshPassphrase"]; }
            set { this["SshPassphrase"] = value; }
        }

        [DatAdmin.DisplayName("s_authentization")]
        [Category("SSH")]
        [XmlElem]
        public SshAuthentization Authentization
        {
            get { return (SshAuthentization)Enum.Parse(typeof(SshAuthentization), Get("SshAuthentization", SshAuthentization.Password.ToString()), true); }
            set { this["SshAuthentization"] = value.ToString(); }
        }

        [Category("SSH")]
        [PasswordPropertyText(true)]
        [XmlElem]
        public string IdentifyFile
        {
            get { return this["IdentifyFile"]; }
            set { this["IdentifyFile"] = value; }
        }

        public override HashSetEx<string> SupportedKeys
        {
            get
            {
                var res = base.SupportedKeys;
                res.Add("Encoding");
                res.Add("SshHost");
                res.Add("SshLogin");
                res.Add("SshPassword");
                res.Add("SshPort");
                res.Add("SshAuthentization");
                res.Add("IdentifyFile");
                res.Add("SshPassphrase");
                return res;
            }
        }
    }
}
