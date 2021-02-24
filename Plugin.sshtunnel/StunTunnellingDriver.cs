using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;

namespace Plugin.sshtunnel
{
    public class StunTunnellingDriver : ITunnellingDriver
    {
        public static readonly StunTunnellingDriver Instance = new StunTunnellingDriver();

        #region ITunnellingDriver Members

        public string[] SupportedEngines
        {
            get { return StunDefaults.Instance.Engines; }
        }

        public TunnelConnFrameBase CreateEditorFrame(TunnelStoredConnection conn)
        {
            return new StunEditFrame((StunStoredConnection)conn);
        }

        public IStoredConnection CreateStoredConnection()
        {
            return new StunStoredConnection();
        }

        public System.Data.Common.DbProviderFactory ProviderFactory
        {
            get { return StunProviderFactory.Instance; }
        }

        #endregion
    }
}
