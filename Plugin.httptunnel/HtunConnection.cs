using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data;
using DatAdmin;

namespace Plugin.httptunnel
{
    public enum EncodingStyle { DEFAULT = 0, DATABASE = 1, EXPLICIT = 2};

    public class HtunConnection : TunnelConnection, IScriptAcceptConnection, IBinaryCommandConnection, IStatelessConnection
    {
        string m_currentDatabase;
        HtunConnectionFailRecoverParams m_failoverParms = new HtunConnectionFailRecoverParams();

        private string m_version = null;
        //PtunConnectionStringBuilder m_params = new PtunConnectionStringBuilder();

        public HtunConnection()
            : base(new HtunDriver())
        {
        }
        public HtunConnection(string conns)
            : base(new HtunDriver(), conns)
        {
        }
        //public HtunConnection(HtunConnectionStringBuilder pars)
        //    : base(HtunProviderFactory.Instance, pars)
        //{
        //}

        //public HtunConnectionStringBuilder Params
        //{
        //    get { return (HtunConnectionStringBuilder)m_params; }
        //}
        //public string EncodingStyleName { get { return m_encodingstyle.ToString(); } }

        public Encoding ResultSetEncoding
        {
            get
            {
                switch (Driver.EncodingStyle)
                {
                    case EncodingStyle.DATABASE
                    : return Encoding.UTF8;
                    case EncodingStyle.DEFAULT:
                    case EncodingStyle.EXPLICIT:
                        return Driver.RealEncoding;
                }
                return Encoding.UTF8;
            }
        }

        public override void ChangeDatabase(string databaseName)
        {
            m_currentDatabase = databaseName;
        }

        public override string Database
        {
            get { return m_currentDatabase ?? m_params.InitialDatabase; }
        }

        public override void Close()
        {
        }

        //public ISqlDialect Dialect
        //{
        //    get { return PtunDefaults.Instance.Dialect(m_params.Engine); }
        //}

        public override void Open()
        {
            using (var ping = WebResultDbPing.CreatePing(this))
            {
                m_version = ping.DbVersion;
            }
        }

        public override string ServerVersion
        {
            get { return m_version; }
        }

        public override System.Data.ConnectionState State
        {
            get { return ConnectionState.Open; }
        }

        public override void PingTunnel()
        {
            using (var ping = WebResultPing.CreatePing(this)) { }
        }

        public override void PingDb()
        {
            using (var ping = WebResultDbPing.CreatePing(this)) { }
        }

        public HtunConnectionFailRecoverParams FailoverParams
        {
            get { return m_failoverParms; }
        }

        #region IScriptAcceptConnection Members

        public void SendScript(IEnumerable<string> commands)
        {
            using (var webcmd = WebResultSendScript.CreateSendScript(this, commands)) { }
        }

        public bool SendScriptSupported
        {
            get { return true; }
        }

        #endregion

        public new HtunDriver Driver
        {
            get { return (HtunDriver)m_driver; }
        }

        #region IBinaryCommandConnection Members

        public Encoding BinaryCommandEncoding { get; set; }

        #endregion

        #region IStatelessConnection Members

        public string SessionInitScript { get; set; }

        #endregion
    }

    public class HtunConnectionFailRecoverParams
    {
        [XmlElem]
        public int MaxRequestCount { get; set; }
        [XmlElem]
        public int HttpRequestTimeoutMilisecs { get; set; }
        [XmlElem]
        public int RepeatedHttpRequestTimeoutMilisecs { get; set; }
        [XmlElem]
        public int RepeatedRequestTimeoutMilisecs { get; set; }
        [XmlElem]
        public int DelayBeforeRepeatedRequestMilisecs { get; set; }

        public HtunConnectionFailRecoverParams()
        {
            MaxRequestCount = 10;
            HttpRequestTimeoutMilisecs = 5000;
            RepeatedHttpRequestTimeoutMilisecs = 20000;
            RepeatedRequestTimeoutMilisecs = 90000;
            DelayBeforeRepeatedRequestMilisecs = 3000;
        }
    }
}
