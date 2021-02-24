using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.IO;
using System.Data.SqlClient;
using System.Data.Common;
using DatAdmin;
using System.ComponentModel;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Plugin.mssql
{
    public enum MsSqlAuth { SSPI, SQL };

    [StoredConnection(Name = "mssql", Title = "MS SQL")]
    public class MsSqlStoredConnection : MultiDatabaseStoredConnection, IOnlineStoredConnection
    {
        //public bool OneDatabase;
        MsSqlAuth m_authentization = MsSqlAuth.SSPI;

        [XmlElem]
        public MsSqlAuth Authentization
        {
            get { return m_authentization; }
            set { m_authentization = value; }
        }

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

        int IOnlineStoredConnection.Port
        {
            get { return 0; }
            set { }
        }

        protected override void GetConnectionKey(CacheKeyBuilder ckb)
        {
            base.GetConnectionKey(ckb);
            ckb.Add(DataSource);
            ckb.Add(Login);
            ckb.Add(Authentization);
            ckb.Add(Password);
        }

        public override string GenerateConnectionString(bool includepwd)
        {
            string res;
            if (Authentization == MsSqlAuth.SQL)
            {
                res = String.Format("MultipleActiveResultSets=true;Data Source={0};User ID={1};Password={2}", DataSource, Login, includepwd ? Password : "******");
            }
            else
            {
                res = String.Format("MultipleActiveResultSets=true;Data Source={0};Integrated Security=SSPI", DataSource);
            }
            if (DatabaseMode == ConnectionDatabaseMode.Explicit) res += ";Initial Catalog=" + ExplicitDatabaseName;
            return res;
        }

        public override string ConnectionTypeTitle { get { return "Microsoft SQL Server"; } }

        public override ConnectionEditFrame CreateEditor()
        {
            return new MsSqlConnFrame(this);
        }

        public override DbProviderFactory GetFactory()
        {
            return SqlClientFactory.Instance;
        }

        public override ISqlDialect GetDefaultDialect()
        {
            return new MsSqlDialect();
        }

        public override string GetLogin()
        {
            return Login;
        }

        public override string GetDataSource()
        {
            return DataSource;
        }

        protected override void AfterCreateConnection(IPhysicalConnection conn)
        {
            SqlConnection s = (SqlConnection)conn.SystemConnection;
            s.InfoMessage += delegate(object sender, SqlInfoMessageEventArgs e)
            {
                InfoMessageEventArgs ev = new InfoMessageEventArgs(e.ToString(), e.Source, 0);
                conn.DispatchInfo(ev);
            };
        }
    }

    [CreateFactoryItem(Name = "mssql")]
    public class MsSqlCreateWizard : ConnectionCreateWizard
    {
        public MsSqlCreateWizard()
            : base("mssql", "Microsoft SQL", "s_file_desc_mssql")
        {
        }
        public override IStoredConnection CreateStoredConnection()
        {
            return new MsSqlStoredConnection();
        }
        public override System.Drawing.Bitmap Bitmap
        {
            get { return StdIcons.microsoft32; }
        }
        public override int Weight
        {
            get { return 9; }
        }
    }

    [DialectAutoDetector(Name = "mssql")]
    public class MsSqlDialectAutoDetector : DialectAutoDetectorBase
    {
        public override bool TestCommand(DbConnection conn)
        {
            string version = conn.ExecuteScalar("select @@VERSION").ToString();
            return version.Contains("Microsoft SQL Server");
        }
        public override ISqlDialect GetDialect()
        {
            return new MsSqlDialect();
        }
        public override ISqlDialect DetectDialect(string displayName)
        {
            displayName = displayName.ToLower();
            if (displayName.StartsWith("microsoft sql") || displayName.StartsWith("mssql"))
            {
                ISqlDialect dialect = new MsSqlDialect();
                var m = Regex.Match(displayName, @"[^\[]*\[([^\]]+)\]");
                if (m.Success) dialect.SetVersion(new SqlServerVersion(m.Groups[1].Value));
                else if (displayName.Contains("2000")) dialect.SetVersion(new SqlServerVersion("8.0.0"));
                else if (displayName.Contains("2005")) dialect.SetVersion(new SqlServerVersion("9.0.0"));
                else if (displayName.Contains("2008")) dialect.SetVersion(new SqlServerVersion("10.0.0"));
                return dialect;
            }
            return null;
        }
    }
}
