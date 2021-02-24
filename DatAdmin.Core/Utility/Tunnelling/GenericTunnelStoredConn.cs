using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using System.Xml;

namespace DatAdmin
{
    [StoredConnection(Name = "generic_tunnel", Title = "s_generic_tunnel", RequiredFeature = GenericTunnelFeature.Test)]
    public class GenericTunnelStoredConnection : MultiDatabaseStoredConnection, ITunellableStoredConnection
    {
        [XmlSubElem]
        [Browsable(false)]
        public ITunnelDriver TunnelDriver { get; set; }

        public override string GenerateConnectionString(bool includepwd)
        {
            if (TunnelDriver == null) return "";
            var pars = new TunnelConnectionStringBuilder();
            TunnelDriver.SaveConnectionParams(pars);
            SaveConnectionParams(pars);
            string res = pars.ConnectionString;
            if (!includepwd) res = Logging.MangleConnectionString_RemovePassword(res);
            return res;
        }

        public override System.Data.Common.DbProviderFactory GetFactory()
        {
            if (TunnelDriver != null) return TunnelDriver.ProviderFactory;
            return null;
        }

        public override ISqlDialect GetDefaultDialect()
        {
            return GenericDialect.Instance;
        }

        void SaveConnectionParams(TunnelConnectionStringBuilder pars)
        {
            pars.Host = Host;
            pars.Login = Login;
            pars.InitialDatabase = InitialDatabase;
            pars.Password = Password;
            pars.Engine = Engine;
            pars.Port = Port;
        }

        [XmlElem]
        [DisplayName("s_host")]
        public string Host { get; set; }

        [XmlElem]
        [DisplayName("s_login")]
        public string Login { get; set; }

        [PasswordPropertyText(true)]
        [DisplayName("s_password")]
        public string Password { get; set; }

        [XmlElem("Password")]
        [Browsable(false)]
        public string XmlPassword
        {
            get { return XmlTool.SafeEncodeString(Password); }
            set { Password = XmlTool.SafeDecodeString(value); }
        }

        [DisplayName("s_database")]
        public string InitialDatabase
        {
            get
            {
                if (DatabaseMode == ConnectionDatabaseMode.Explicit) return ExplicitDatabaseName;
                return "";
            }
            set
            {
                if (value.IsEmpty())
                {
                    DatabaseMode = ConnectionDatabaseMode.All;
                    ExplicitDatabaseName = null;
                }
                else
                {
                    DatabaseMode = ConnectionDatabaseMode.Explicit;
                    ExplicitDatabaseName = value;
                }
            }
        }

        [XmlElem]
        [DisplayName("s_engine")]
        public string Engine { get; set; }

        [XmlElem]
        [DisplayName("s_port")]
        public int Port { get; set; }

        protected override void GetConnectionKey(CacheKeyBuilder ckb)
        {
            base.GetConnectionKey(ckb);
            ckb.Add(Host);
            ckb.Add(Login);
            ckb.Add(Password);
            ckb.Add(Engine);
            ckb.Add(Port);
            if (TunnelDriver != null) TunnelDriver.GetConnectionKey(ckb);
        }

        public override ConnectionEditFrame CreateEditor()
        {
            return new GenericTunnelFrame { Connection = this };
        }

        public override bool SupportsDatabaseSelect
        {
            get { return false; }
        }

        public bool AllowDirectConnection { get { return false; } }

        public override IDialectDetector GetDefaultDialectDetector()
        {
            return new DialectAutoDetector();
        }
    }

    [CreateFactoryItem(Name = "generic_tunnel", RequiredFeature = GenericTunnelFeature.Test)]
    public class GenericTunnelConnectionCreateWizard : GenericConnectionCreateWizard
    {
        public GenericTunnelConnectionCreateWizard()
            : base("generic_tunnel", "s_generic_tunnel", "s_generic_tunnel_desc")
        {
        }

        public override IStoredConnection CreateStoredConnection()
        {
            return new GenericTunnelStoredConnection();
        }

        protected override Form CreateWizard(IStoredConnection conn)
        {
            return new TunnelConnWizard((GenericTunnelStoredConnection)conn);
        }

        public override System.Drawing.Bitmap Bitmap
        {
            get { return CoreIcons.tunnel32; }
        }
    }
}
