using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.ComponentModel;
using System.Data.Common;
using System.Data.OracleClient;

namespace Plugin.oracle
{
    public enum OracleConnectionType { Basic, TNS };
    public enum OracleServiceMode { SID, ServiceName };

    [StoredConnection(Name = "oracle", Title = "Oracle")]
    public class OracleStoredConnection : StoredConnection, IOnlineStoredConnection
    {
        //public bool OneDatabase;
        OracleConnectionType m_connType = OracleConnectionType.Basic;

        [XmlElem]
        public OracleConnectionType ConnectionType
        {
            get { return m_connType; }
            set { m_connType = value; }
        }

        OracleServiceMode m_serviceMode = OracleServiceMode.SID;

        [XmlElem]
        public OracleServiceMode ServiceMode
        {
            get { return m_serviceMode; }
            set { m_serviceMode = value; }
        }

        string m_dataSource = "localhost";

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

        string m_serviceName = "orcl";
        [XmlElem]
        public string ServiceName
        {
            get { return m_serviceName; }
            set { m_serviceName = value; }
        }

        int m_port = 1521;
        [XmlElem]
        public int Port
        {
            get { return m_port; }
            set { m_port = value; }
        }

        string IOnlineStoredConnection.DatabaseName
        {
            get { return ServiceName; }
            set { ServiceName = value; }
        }

        public override string GenerateConnectionString(bool includepwd)
        {
            OracleSettings.Page.CheckConfigured();
            if (ConnectionType == OracleConnectionType.Basic)
            {
                return "Data Source=(DESCRIPTION=(ADDRESS=(PROTOCOL=TCP)(HOST=" + DataSource + ")(PORT=" + Port.ToString() + "))(CONNECT_DATA=(SERVICE_NAME=" + ServiceName + ")));User Id=" + Login + ";Password=" + Password + "";
            }
            if (ConnectionType == OracleConnectionType.TNS)
            {
                return "Data Source=" + ServiceName + ";User Id=" + Login + ";Password=" + Password + "";
            }
            return "";
        }

        public override string ConnectionTypeTitle { get { return "Oracle"; } }

        public override ConnectionEditFrame CreateEditor()
        {
            return new OracleConnFrame(this);
        }

        public override DbProviderFactory GetFactory()
        {
            return System.Data.OracleClient.OracleClientFactory.Instance;
        }

        public override ISqlDialect GetDefaultDialect()
        {
            return new OracleDialect();
        }

        public override string GetLogin()
        {
            return Login;
        }

        public override string GetDataSource()
        {
            return DataSource;
        }

        protected override void GetConnectionKey(CacheKeyBuilder ckb)
        {
            base.GetConnectionKey(ckb);
            ckb.Add(DataSource);
            ckb.Add(Login);
            ckb.Add(Password);
            ckb.Add(Port);
            ckb.Add(ConnectionType);
            ckb.Add(ServiceMode);
            ckb.Add(ServiceName);
        }

        protected override void AfterCreateConnection(IPhysicalConnection conn)
        {
            OracleConnection s = (OracleConnection)conn.SystemConnection;

            s.InfoMessage += delegate(object sender, OracleInfoMessageEventArgs e)
            {
                InfoMessageEventArgs ev = new InfoMessageEventArgs(e.ToString(), e.Source, 0);
                conn.DispatchInfo(ev);
            };
        }

        [Browsable(false)]
        public override string ExplicitDatabaseName
        {
            get { return null; }
            set { }
        }

        [Browsable(false)]
        public override ConnectionDatabaseMode DatabaseMode
        {
            get { return ConnectionDatabaseMode.Default; }
            set { }
        }
    }

    [CreateFactoryItem(Name = "oracle")]
    public class OracleCreateWizard : ConnectionCreateWizard
    {
        public OracleCreateWizard()
            : base("oracle", "Oracle", "s_file_desc_oracle")
        {
        }
        public override IStoredConnection CreateStoredConnection()
        {
            return new OracleStoredConnection();
        }
        public override System.Drawing.Bitmap Bitmap
        {
            get { return StdIcons.oracle32; }
        }
        public override int Weight
        {
            get { return 8; }
        }
    }
}
