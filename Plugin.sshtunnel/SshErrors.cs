using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;

namespace Plugin.sshtunnel
{
    public class SshTunnelQueryError : UnexpectedError
    {
        public SshTunnelQueryError(string message)
            : base(message, null)
        {
        }
    }
}
