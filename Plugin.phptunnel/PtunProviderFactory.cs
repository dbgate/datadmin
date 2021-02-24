using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using DatAdmin;

namespace Plugin.phptunnel
{
    public class PtunProviderFactory : TunnelProviderFactory
    {
        public static PtunProviderFactory Instance = new PtunProviderFactory();

        public override DbConnection CreateConnection()
        {
            return new PtunConnection();
        }

        public override DbCommand CreateCommand()
        {
            return new PtunCommand();
        }

        public override DbConnectionStringBuilder CreateConnectionStringBuilder()
        {
            return new PtunConnectionStringBuilder();
        }
    }
}
