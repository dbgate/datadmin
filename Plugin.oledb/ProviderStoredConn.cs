using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.ComponentModel;

namespace Plugin.oledb
{
    public abstract class ProviderStoredConnection : MultiDatabaseStoredConnection
    {
        string m_provider;
        [XmlElem]
        public string Provider
        {
            get { return m_provider; }
            set { m_provider = value; }
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

        int m_port;
        [XmlElem]
        public int Port
        {
            get { return m_port; }
            set { m_port = value; }
        }

        public override ConnectionEditFrame CreateEditor()
        {
            return new ProviderConnFrame(this);
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
            return DataSource;
        }

        protected override void GetConnectionKey(CacheKeyBuilder ckb)
        {
            base.GetConnectionKey(ckb);
            ckb.Add(DataSource);
            ckb.Add(Port);
            ckb.Add(Login);
            ckb.Add(Provider);
            ckb.Add(Password);
        }

        public abstract IEnumerable<string> GetProviders();
    }
}
