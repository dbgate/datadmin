using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data;
using DatAdmin;

namespace Plugin.httptunnel
{
    public class HtunCommand : TunnelCommand
    {
        protected override ITunnelResultSet GetResult(CommandBehavior behaviour, GetResultCaller caller)
        {
            int limit = (behaviour & CommandBehavior.SchemaOnly) != 0 ? 0 : -1;
            Dictionary<string, string> pars = new Dictionary<string, string>();
            foreach (TunnelParameter par in Parameters)
            {
                pars[par.ParameterName] = par.Value == null ? null : par.Value.ToString();
            }
            WebResultSet res = WebResultSet.CreateRequest((HtunConnection)Connection, CommandText, limit, pars);
            return res;
        }
    }
}
