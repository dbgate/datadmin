using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.Xml.Serialization;
using System.IO;
using System.Data.Common;
using System.ComponentModel;

namespace Plugin.sshtunnel
{
    [StoredConnection(Name = "sshtunnel", Title = "SSH Tunnel")]
    public class StunStoredConnection : TunnelStoredConnection
    {
        StunConnectionStringBuilder Params
        {
            get { return (StunConnectionStringBuilder)base.Params; }
        }

        public override ITunnellingDriver TunnellingDriver
        {
            get { return StunTunnellingDriver.Instance; }
        }

        public StunStoredConnection()
        {
            System.Guid guid = System.Guid.NewGuid();
        }

        public override string GetLogin()
        {
            return Params.Login;
        }

        public override string GetDataSource()
        {
            return Params.SshHost + "@" + Params.Host;
        }

        public override ISqlDialect GetDefaultDialect()
        {
            return StunDefaults.Instance.Dialect(Params.Engine);
        }

        public override DbProviderFactory GetFactory()
        {
            return StunProviderFactory.Instance;
        }
    }

    [CreateFactoryItem(Name = "sshtunnel")]
    public class StunConnectionCreateWizard : TunnelConnectionCreateWizard
    {
        public StunConnectionCreateWizard()
            : base("sshtunnel", "SSH Tunnel", "s_file_desc_sshtunnel")
        {
        }

        public override System.Drawing.Bitmap Bitmap
        {
            get { return StdIcons.ssh32; }
        }

        protected override ITunnellingDriver TunnellingDriver
        {
            get { return StunTunnellingDriver.Instance; }
        }
    }
}
