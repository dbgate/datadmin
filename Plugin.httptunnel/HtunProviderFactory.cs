using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using DatAdmin;

namespace Plugin.httptunnel
{
    public class HtunProviderFactory : TunnelProviderFactory
    {
        public static HtunProviderFactory Instance = new HtunProviderFactory();

        public override DbConnection CreateConnection()
        {
            return new HtunConnection();
        }

        public override DbCommand CreateCommand()
        {
            return new HtunCommand();
        }
    }
}
