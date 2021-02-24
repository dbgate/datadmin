using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data;
using DatAdmin;

namespace Plugin.phptunnel
{
    public enum EncodingStyle { DEFAULT = 0, DATABASE = 1, EXPLICIT = 2};

    public class PtunConnection : TunnelConnection, IScriptAcceptConnection
    {
        string m_currentDatabase;
        PtunConnectionFailRecoverParams m_failoverParms = new PtunConnectionFailRecoverParams();

        private string m_version = null;
        //PtunConnectionStringBuilder m_params = new PtunConnectionStringBuilder();

        public PtunConnection()
            : base(PtunTunnellingDriver.Instance)
        {
        }
        public PtunConnection(string conns)
            : base(PtunTunnellingDriver.Instance, conns)
        {
        }
        public PtunConnection(PtunConnectionStringBuilder pars)
            : base(PtunTunnellingDriver.Instance, pars)
        {
        }

        public PtunConnectionStringBuilder Params
        {
            get { return (PtunConnectionStringBuilder)m_params; }
        }
        //public string EncodingStyleName { get { return m_encodingstyle.ToString(); } }

        public Encoding ResultSetEncoding
        {
            get
            {
                switch (Params.EncodingStyle)
                {
                    case EncodingStyle.DATABASE
                    : return Encoding.UTF8;
                    case EncodingStyle.DEFAULT:
                    case EncodingStyle.EXPLICIT:
                        return Params.RealEncoding;
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

        public ISqlDialect Dialect
        {
            get { return PtunDefaults.Instance.Dialect(m_params.Engine); }
        }

        public override void Open()
        {
            m_version = Dialect.SelectVersion(this);
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
            using (var ping = WebResultPing.CreatePingUrl(this)) { }
        }

        public override void PingDb()
        {
            using (var ping = WebResultPing.CreatePingDb(this)) { }
        }

        public PtunConnectionFailRecoverParams FailoverParams
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
    }

    public class PtunConnectionFailRecoverParams
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

        public PtunConnectionFailRecoverParams()
        {
            MaxRequestCount = 10;
            HttpRequestTimeoutMilisecs = 5000;
            RepeatedHttpRequestTimeoutMilisecs = 20000;
            RepeatedRequestTimeoutMilisecs = 90000;
            DelayBeforeRepeatedRequestMilisecs = 3000;
        }
    }
}
