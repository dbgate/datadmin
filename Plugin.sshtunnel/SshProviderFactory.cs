using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using DatAdmin;

namespace Plugin.sshtunnel
{
    public class PtunProviderFactory : TunnelProviderFactory
    {
        public static PtunProviderFactory Instance = new PtunProviderFactory();

        public override DbConnection CreateConnection()
        {
            return new StunConnection();
        }

        public override DbCommand CreateCommand()
        {
            return new StunCommand();
        }

        public override DbConnectionStringBuilder CreateConnectionStringBuilder()
        {
            return new StunConnectionStringBuilder();
        }
    }
}
