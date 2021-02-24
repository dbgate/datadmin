using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;

namespace DatAdmin
{
    public abstract class TunnelConnection : DbConnection
    {
        protected readonly TunnelConnectionStringBuilder m_params;
        protected readonly ITunnelDriver m_driver;
        protected readonly DbProviderFactory m_factory;

        public TunnelConnection(ITunnelDriver driver)
        {
            m_driver = driver;
            m_factory = driver.ProviderFactory;
            m_params = (TunnelConnectionStringBuilder)m_factory.CreateConnectionStringBuilder();
        }

        public TunnelConnection(ITunnelDriver driver, TunnelConnectionStringBuilder pars)
        {
            m_driver = driver;
            m_params = pars;
            m_factory = driver.ProviderFactory;
            m_driver.LoadConnectionParams(pars);
        }

        public TunnelConnection(ITunnelDriver driver, string conns)
        {
            m_driver = driver;
            m_factory = driver.ProviderFactory;
            m_params = (TunnelConnectionStringBuilder)m_factory.CreateConnectionStringBuilder();
            m_params.ConnectionString = conns;
            m_driver.LoadConnectionParams(m_params);
        }

        public abstract void PingTunnel();
        public abstract void PingDb();

        protected override DbTransaction BeginDbTransaction(System.Data.IsolationLevel isolationLevel)
        {
            return new TunnelTransaction(this);
        }

        protected override DbCommand CreateDbCommand()
        {
            var res = m_factory.CreateCommand();
            res.Connection = this;
            return res;
        }

        public override string DataSource
        {
            get { return m_params.Host; }
        }

        public override string ConnectionString
        {
            get { return m_params.ConnectionString; }
            set
            {
                m_params.ConnectionString = value;
                m_driver.LoadConnectionParams(m_params);
            }
        }

        public ITunnelDriver Driver
        {
            get { return m_driver; }
        }

        public TunnelConnectionStringBuilder Params
        {
            get { return m_params; }
        }

        /// <summary>
        /// returns params, which are not defined in driver
        /// </summary>
        /// <param name="driver"></param>
        /// <param name="pars">result params</param>
        public void GetDatabaseParams(Dictionary<string, string> pars)
        {
            var dpars = Driver.GetSupportedKeys();
            foreach (string key in Params.Keys)
            {
                if (dpars.Contains(key)) continue;
                pars[key.ToLower()] = Params[key];
            }
        }
    }
}
