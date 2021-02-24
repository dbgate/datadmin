using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.Xml.Serialization;
using System.IO;
using System.Data.Common;
using System.ComponentModel;

namespace Plugin.phptunnel
{
    [StoredConnection(Name = "phptunnel", Title = "PHP Tunnel")]
    public class PtunStoredConnection : TunnelStoredConnection
    {
        PtunConnectionStringBuilder Params
        {
            get { return (PtunConnectionStringBuilder)base.Params; }
        }

        public override ITunnellingDriver TunnellingDriver
        {
            get { return PtunTunnellingDriver.Instance; }
        }

        //public override string GenerateConnectionString(bool includepwd)
        //{
        //    string res = String.Format("Host={0};Login={1};Password={2};Url={3};Check={4};Encoding={5};"
        //    + "EncodingStyle={6};ProxyServer={7};ProxyLogin={8};ProxyPassword={9};HttpLogin={10};HttpPassword={11};Port={12}",
        //    Host, Login, includepwd ? Password : "******", Url, includepwd ? Check : "******", Encoding, EncodingStyle.ToString(),
        //    ProxyServer, ProxyLogin, includepwd ? ProxyPassword : "******", HttpLogin, includepwd ? HttpPassword : "******", Port);
        //    if (DatabaseMode == ConnectionDatabaseMode.Explicit) res += ";Database=" + ExplicitDatabaseName;
        //    return res;
        //}

        //public override ConnectionEditFrame CreateEditor()
        //{
        //    return new ConnFrame(this);
        //}

        [Browsable(false)]
        public override string HelpTopic
        {
            get { return "phptunnel"; }
        }

        public PtunStoredConnection()
        {
            System.Guid guid = System.Guid.NewGuid();
            Params.Check = guid.ToString();
        }

        public override string GetLogin()
        {
            return Params.Login;
        }

        public override string GetDataSource()
        {
            return Params.Url + "@" + Params.Host;
        }

        public override ISqlDialect GetDefaultDialect()
        {
            return PtunDefaults.Instance.Dialect(Params.Engine);
        }

        public override DbProviderFactory GetFactory()
        {
            return PtunProviderFactory.Instance;
        }
    }

    //[CreateFactoryItem(Name = "mysqlphptunnel")]
    //public class PhpTunnelCreateWizard : ConnectionCreateWizard
    //{
    //    public PhpTunnelCreateWizard()
    //        : base("mysqlphptunnel", "MySQL PHP Tunnel", "s_file_desc_mysqlphp")
    //    {
    //    }
    //    public override IStoredConnection CreateStoredConnection()
    //    {
    //        return new PhpTunnelStoredConnection();
    //    }
    //    public override System.Drawing.Bitmap Bitmap
    //    {
    //        get { return StdIcons.php32; }
    //    }
    //}

    [CreateFactoryItem(Name = "phptunnel")]
    public class PtunConnectionCreateWizard : TunnelConnectionCreateWizard
    {
        public PtunConnectionCreateWizard()
            : base("phptunnel", "PHP Tunnel", "s_file_desc_mysqlphp")
        {
        }

        public override System.Drawing.Bitmap Bitmap
        {
            get { return StdIcons.php32; }
        }

        protected override ITunnellingDriver TunnellingDriver
        {
            get { return PtunTunnellingDriver.Instance; }
        }
    }
}
