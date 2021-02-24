using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data;
using DatAdmin;

namespace Plugin.sshtunnel
{
    public class StunCommand : TunnelCommand
    {
        protected override ITunnelResultSet GetResult(CommandBehavior behaviour, GetResultCaller caller)
        {
            int limit = (behaviour & CommandBehavior.SchemaOnly) != 0 ? 0 : -1;
            return ((StunConnection)Connection).ExecuteResultSet(CommandText);
        }
    }
}
