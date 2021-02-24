using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;

namespace DatAdmin
{
    public class TunnelProviderFactory : DbProviderFactory
    {
        //public override DbCommandBuilder CreateCommandBuilder()
        //{
        //    return new TunnelCommandBuilder();
        //}

        public override DbDataAdapter CreateDataAdapter()
        {
            return new TunnelDataAdapter();
        }

        public override DbParameter CreateParameter()
        {
            return new TunnelParameter();
        }

        public override DbConnectionStringBuilder CreateConnectionStringBuilder()
        {
            return new TunnelConnectionStringBuilder();
        }
    }
}
