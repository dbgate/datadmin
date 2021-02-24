using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    public static class ConnectionRegister
    {
        static List<IPhysicalConnection> Connections = new List<IPhysicalConnection>();

        public static void AddConnection(IPhysicalConnection conn)
        {
            lock (Connections)
            {
                Connections.Add(conn);
                HConnection.CallAddConnection(conn);
            }
        }

        public static void RemoveConnection(IPhysicalConnection conn)
        {
            lock (Connections)
            {
                Connections.Remove(conn);
                HConnection.CallRemoveConnection(conn);
            }
        }

        public static void ChangedConnection(IPhysicalConnection conn)
        {
            HConnection.CallChangeConnection(conn);
        }

        public static List<IPhysicalConnection> GetConnections()
        {
            lock (Connections)
            {
                return new List<IPhysicalConnection>(Connections);
            }
        }
    }
}
