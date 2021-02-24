using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using DatAdmin;

namespace Plugin.sshtunnel
{
    public class StunProviderFactory : TunnelProviderFactory
    {
        public static StunProviderFactory Instance = new StunProviderFactory();

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
