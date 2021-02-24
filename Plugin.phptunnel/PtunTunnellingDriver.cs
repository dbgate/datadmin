using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;

namespace Plugin.phptunnel
{
    public class PtunTunnellingDriver : ITunnellingDriver
    {
        public static readonly PtunTunnellingDriver Instance = new PtunTunnellingDriver();

        #region ITunnellingDriver Members

        public string[] SupportedEngines
        {
            get { return PtunDefaults.Instance.Engines; }
        }

        public TunnelConnFrameBase CreateEditorFrame(TunnelStoredConnection conn)
        {
            return new PtunEditFrame((PtunStoredConnection)conn);
        }

        public IStoredConnection CreateStoredConnection()
        {
            return new PtunStoredConnection();
        }

        public System.Data.Common.DbProviderFactory ProviderFactory
        {
            get { return PtunProviderFactory.Instance; }
        }

        #endregion
    }
}
