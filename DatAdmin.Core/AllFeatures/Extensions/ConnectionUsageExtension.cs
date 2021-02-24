using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    public static class ConnectionUsageExtension
    {
        public static T ChangeConnection<T>(this T usage, ConnectionPack connpack)
            where T : IConnectionUsage
        {
            if (usage.Connection == null) throw new InternalError("DAE-00009 Connection property not set");
            var conn = connpack.GetConnection(usage.Connection.PhysicalFactory, true);
            usage.Connection = conn;
            return usage;
        }
        public static string GetConnectionKey(this IConnectionUsage usage)
        {
            if (usage == null || usage.Connection == null) return "";
            return usage.Connection.GetConnKey();
        }
        public static void CloneConnection(this IConnectionUsage usage)
        {
            if (usage.Connection == null) return;
            usage.Connection = usage.Connection.Clone();
        }
    }
}
