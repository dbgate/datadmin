using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    public class RemoveConntectionByKeyArgs
    {
        public string ConnKey;
        public bool Canceled;
    }

    public static class HConnection
    {
        public static event Action<RemoveConntectionByKeyArgs> RemoveByKey;

        public static event Action<IPhysicalConnection> AddConnection;
        public static event Action<IPhysicalConnection> RemoveConnection;
        public static event Action<IPhysicalConnection> ChangeConnection;

        public static void CallRemoveByKey(RemoveConntectionByKeyArgs args)
        {
            if (RemoveByKey != null) RemoveByKey(args);
        }
        public static bool CallRemoveByKey(string connkey)
        {
            var args = new RemoveConntectionByKeyArgs();
            args.ConnKey = connkey;
            CallRemoveByKey(args);
            return !args.Canceled;
        }
        public static void CallAddConnection(IPhysicalConnection conn)
        {
            if (AddConnection != null) AddConnection(conn);
        }
        public static void CallRemoveConnection(IPhysicalConnection conn)
        {
            if (RemoveConnection != null) RemoveConnection(conn);
        }
        public static void CallChangeConnection(IPhysicalConnection conn)
        {
            if (ChangeConnection != null) ChangeConnection(conn);
        }
    }
}
