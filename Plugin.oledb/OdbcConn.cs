using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using Microsoft.Win32;
using System.Data.Odbc;
using System.Data.Common;
using System.ComponentModel;

namespace Plugin.oledb
{
    [StoredConnection(Name = "odbc", Title = "ODBC")]
    public class OdbcStoredConnection : ProviderStoredConnection
    {
        public override IEnumerable<string> GetProviders()
        {
            List<string> provs = new List<string>();
            RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE\ODBC\ODBCINST.INI\ODBC Drivers");
            foreach (string val in key.GetValueNames())
            {
                provs.Add(val);
            }
            key.Close();
            return provs;
        }

        public override string GenerateConnectionString(bool includepwd)
        {
            string res;
            res = String.Format("Driver={{{0}}};Server={1};UID={2};PWD={3}", Provider, DataSource, Login, includepwd ? Password : "******");
            if (DatabaseMode == ConnectionDatabaseMode.Explicit) res += ";Database=" + ExplicitDatabaseName;
            if (Port > 0) res += ";Port=" + Port.ToString();
            return res;
        }

        public override DbProviderFactory GetFactory()
        {
            return OdbcFactory.Instance;
        }

        public override IProviderHooks CreateHooks()
        {
            return new OdbcHooks();
        }
    }

    public class OdbcHooks : ProviderHooksBase
    {
        public override List<string> GetDatabaseNames(IPhysicalConnection conn)
        {
            var odbc = conn.StoredConnection as OdbcStoredConnection;
            if (odbc != null)
            {
                SQLInfoEnumerator sen = new SQLInfoEnumerator();
                string[] ar = sen.EnumerateSQLServersDatabases(odbc.GenerateConnectionString(true));
                if (ar != null) return new List<string>(ar);
            }
            return base.GetDatabaseNames(conn);
        }
    }

    [StoredConnection(Name = "odbc_dsn", Title = "ODBC DSN")]
    public class OdbcDsnStoredConnection : MultiDatabaseStoredConnection
    {
        string m_dataSourceName;

        [XmlElem]
        public string DataSourceName
        {
            get { return m_dataSourceName; }
            set { m_dataSourceName = value; }
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

        public override ConnectionEditFrame CreateEditor()
        {
            return new OdbcDsnConnFrame(this);
        }

        public override ISqlDialect GetDefaultDialect()
        {
            return new GenericDialect();
        }

        public override IDialectDetector GetDefaultDialectDetector()
        {
            return new DialectAutoDetector();
        }

        public override string GetLogin()
        {
            return Login;
        }

        public override string GetDataSource()
        {
            return DataSourceName;
        }

        public override DbProviderFactory GetFactory()
        {
            return OdbcFactory.Instance;
        }

        public override string GenerateConnectionString(bool includepwd)
        {
            string res;
            res = String.Format("Dsn={0}", DataSourceName);
            if (!String.IsNullOrEmpty(Login)) res += String.Format(";Uid={0}", Login);
            if (!String.IsNullOrEmpty(Password)) res += String.Format(";Pwd={0}", includepwd ? Password : "******");
            if (DatabaseMode == ConnectionDatabaseMode.Explicit) res += ";Database=" + ExplicitDatabaseName;
            return res;
        }

        protected override void GetConnectionKey(CacheKeyBuilder ckb)
        {
            base.GetConnectionKey(ckb);
            ckb.Add(DataSourceName);
            ckb.Add(Login);
            ckb.Add(Password);
        }
    }

    [CreateFactoryItem(Name = "odbc")]
    public class OdbcCreateWizard : GenericConnectionCreateWizard
    {
        public OdbcCreateWizard()
            : base("odbc", "ODBC", "s_file_desc_odbc")
        {
        }
        public override IStoredConnection CreateStoredConnection()
        {
            return new OdbcStoredConnection();
        }
        public override System.Drawing.Bitmap Bitmap
        {
            get { return StdIcons.odbcicon; }
        }
    }

    [CreateFactoryItem(Name = "odbc_dsn")]
    public class OdbcDsnCreateWizard : GenericConnectionCreateWizard
    {
        public OdbcDsnCreateWizard()
            : base("odbc_dsn", "ODBC DSN", "s_file_desc_odbc_dsn")
        {
        }
        public override IStoredConnection CreateStoredConnection()
        {
            return new OdbcDsnStoredConnection();
        }
        public override System.Drawing.Bitmap Bitmap
        {
            get { return StdIcons.odbcicon; }
        }
    }
}
