using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.Xml.Serialization;
using System.Data.Common;
using System.IO;
using System.ComponentModel;
using Npgsql;

namespace Plugin.postgre
{
    [StoredConnection(Name = "postgre", Title = "PostgreSQL")]
    public class PostgreSqlStoredConnection : MultiDatabaseStoredConnection, IOnlineStoredConnection
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

        int m_port = 0;

        [XmlElem]
        public int Port
        {
            get { return m_port; }
            set { m_port = value; }
        }

        [XmlElem("Password")]
        [Browsable(false)]
        public string XmlPassword
        {
            get { return XmlTool.SafeEncodeString(Password); }
            set { Password = XmlTool.SafeDecodeString(value); }
        }

        public override string GenerateConnectionString(bool includepwd)
        {
            string res = String.Format("Host={0};User ID={1};Password={2}", DataSource, Login, includepwd ? Password : "******");
            if (DatabaseMode == ConnectionDatabaseMode.Explicit) res += ";Database=" + ExplicitDatabaseName;
            if (Port > 0) res += ";Port=" + Port.ToString();
            //res += ";SyncNotification=true";
            return res;
        }

        public override string ConnectionTypeTitle { get { return "Postgre SQL"; } }

        public override ConnectionEditFrame CreateEditor()
        {
            return new ConnFrame(this);
        }

        protected override void GetConnectionKey(CacheKeyBuilder ckb)
        {
            base.GetConnectionKey(ckb);
            ckb.Add(DataSource);
            ckb.Add(Login);
            ckb.Add(Password);
            ckb.Add(Port);
        }

        protected override void AfterCreateConnection(IPhysicalConnection conn)
        {
            NpgsqlConnection s = (NpgsqlConnection)conn.SystemConnection;
            s.Notice += delegate(object sender, NpgsqlNoticeEventArgs e)
            {
                InfoMessageEventArgs ev = new InfoMessageEventArgs(e.Notice.ToString(), "PostgreSql", 0);
                conn.DispatchInfo(ev);
            };
            s.Notification += delegate(object sender, NpgsqlNotificationEventArgs e)
            {
                InfoMessageEventArgs ev = new InfoMessageEventArgs(e.ToString(), "PostgreSql", 0);
                conn.DispatchInfo(ev);
            };
        }

        public override ISqlDialect GetDefaultDialect()
        {
            return new PostgreDialect();
        }

        public override DbProviderFactory GetFactory()
        {
            return Npgsql.NpgsqlFactory.Instance;
        }

        public override string GetLogin()
        {
            return Login;
        }

        public override string GetDataSource()
        {
            return DataSource;
        }
    }

    [CreateFactoryItem(Name="postgre")]
    public class PostgreSqlCreateWizard : ConnectionCreateWizard
    {
        public PostgreSqlCreateWizard()
            : base("postgre", "Postgre SQL", "s_file_desc_postgre")
        {
        }
        public override IStoredConnection CreateStoredConnection()
        {
            return new PostgreSqlStoredConnection();
        }
        public override System.Drawing.Bitmap Bitmap
        {
            get { return StdIcons.postgresql32; }
        }
        public override int Weight
        {
            get { return 6; }
        }
    }
}
