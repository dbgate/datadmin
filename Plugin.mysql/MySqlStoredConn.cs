using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using DatAdmin;
using System.Xml.Serialization;
using System.Data.Common;
using System.IO;
using System.ComponentModel;

namespace Plugin.mysql
{
    [StoredConnection(Name = "mysql", Title = "MySQL")]
    public class MySqlStoredConnection : MultiDatabaseStoredConnection, ITunellableStoredConnection, IOnlineStoredConnection
    {
        string m_dataSource;

        [XmlElem]
        public string DataSource
        {
            get { return m_dataSource; }
            set { m_dataSource = value; }
        }
        string m_login;

        [XmlElem]
        public string Login
        {
            get { return m_login; }
            set { m_login = value; }
        }
        string m_password;

        [PasswordPropertyText(true)]
        public string Password
        {
            get { return m_password; }
            set { m_password = value; }
        }

        [XmlElem("Password")]
        [Browsable(false)]
        public string XmlPassword
        {
            get { return XmlTool.SafeEncodeString(Password); }
            set { Password = XmlTool.SafeDecodeString(value); }
        }

        int m_port = 3306;
        [XmlElem]
        public int Port
        {
            get { return m_port; }
            set { m_port = value; }
        }

        Encoding m_characterSet;

        [XmlElem]
        [Browsable(false)]
        public Encoding CharacterSet
        {
            get { return m_characterSet; }
            set { m_characterSet = value; }
        }

        [DatAdmin.DisplayName("s_encoding")]
        [TypeConverter(typeof(EncodingTypeConverter))]
        public string GuiEncoding
        {
            get
            {
                if (CharacterSet == null) return null;
                return CharacterSet.WebName;
            }
            set
            {
                CharacterSet = System.Text.Encoding.GetEncoding(value);
            }
        }

        [XmlSubElem]
        public ITunnelDriver TunnelDriver { get; set; }

        public override string GenerateConnectionString(bool includepwd)
        {
            if (TunnelDriver != null)
            {
                var pars = new TunnelConnectionStringBuilder();
                TunnelDriver.SaveConnectionParams(pars);
                pars.Login = Login;
                pars.Password = Password;
                pars.Port = Port;
                pars.Host = DataSource;
                pars.Engine = "mysql";
                if (DatabaseMode == ConnectionDatabaseMode.Explicit) pars.InitialDatabase = ExplicitDatabaseName;
                return pars.ConnectionString;
            }
            else
            {
                string res = String.Format("Data Source={0};User ID={1};Password={2}", DataSource, Login, includepwd ? Password : "******");
                if (DatabaseMode == ConnectionDatabaseMode.Explicit) res += ";Database=" + ExplicitDatabaseName;
                if (Port > 0 && Port != 3306) res += ";Port=" + Port.ToString();
                if (CharacterSet != null) res += ";Character Set=" + CharacterSet.WebName.Replace("-", "");
                res += ";Allow Zero Datetime=Yes";
                res += ";Allow User Variables=true";
                res += ";Persist Security Info=true";
                res += ";Default Command Timeout=3600";
                return res;
            }
        }

        protected override void GetConnectionKey(CacheKeyBuilder ckb)
        {
            base.GetConnectionKey(ckb);
            ckb.Add(DataSource);
            ckb.Add(Login);
            ckb.Add(CharacterSet);
            ckb.Add(Password);
            ckb.Add(Port);
            if (TunnelDriver != null) TunnelDriver.GetConnectionKey(ckb);
        }

        public override string ConnectionTypeTitle { get { return "MySQL"; } }

        public override ConnectionEditFrame CreateEditor()
        {
            return new MySqlConnFrame(this);
        }

        public override DbProviderFactory GetFactory()
        {
            if (TunnelDriver != null) return TunnelDriver.ProviderFactory;
            return MySql.Data.MySqlClient.MySqlClientFactory.Instance;
        }

        public override ISqlDialect GetDefaultDialect()
        {
            return new MySqlDialect();
        }

        protected override void AfterCreateConnection(IPhysicalConnection conn)
        {
            MySqlConnection s = conn.SystemConnection as MySqlConnection;
            if (s == null) return;
            s.InfoMessage += delegate(object sender, MySqlInfoMessageEventArgs e)
            {
                InfoMessageEventArgs ev = new InfoMessageEventArgs(e.ToString(), "MySql", 0);
                conn.DispatchInfo(ev);
            };
        }

        public override string GetLogin()
        {
            return Login;
        }

        public override string GetDataSource()
        {
            return DataSource;
        }

        public bool AllowDirectConnection { get { return true; } }
    }

    [CreateFactoryItem(Name = "mysql")]
    public class MySqlCreateWizard : ConnectionCreateWizard
    {
        public MySqlCreateWizard()
            : base("mysql", "MySQL", "s_file_desc_mysql")
        {
        }
        public override IStoredConnection CreateStoredConnection()
        {
            return new MySqlStoredConnection();
        }
        public override System.Drawing.Bitmap Bitmap
        {
            get { return StdIcons.mysql32; }
        }
        public override int Weight
        {
            get { return 10; }
        }
    }

    [DialectAutoDetector(Name = "mysql")]
    public class MySqlDialectAutoDetector : DialectAutoDetectorBase
    {
        public override bool TestCommand(DbConnection conn)
        {
            conn.ExecuteScalar("show variables like 'version'");
            return true;
        }
        public override ISqlDialect GetDialect()
        {
            return new MySqlDialect();
        }
        public override ISqlDialect DetectDialect(string displayName)
        {
            if (displayName.ToLower().StartsWith("mysql"))
            {
                var res = new MySqlDialect();
                int spacepos = displayName.IndexOf(' ');
                if (spacepos > 0) res.SetVersion(new SqlServerVersion(displayName.Substring(spacepos + 1)));
                return res;
            }
            return null;
        }
    }
}
