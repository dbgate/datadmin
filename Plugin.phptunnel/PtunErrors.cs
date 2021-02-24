using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;

namespace Plugin.phptunnel
{
    public class BadVersionError : ExpectedError
    {
        public BadVersionError(int readedVersion)
            : base(Texts.Get("s_bad_version_of_php_tunnel_please_update"), null)
        {
        }
    }

    public class StreamFormatError : UnexpectedError
    {
        public StreamFormatError(string message)
            : base(message, null)
        {
        }
    }

    public class DatabaseError : UnexpectedError
    {
        public DatabaseError(string message)
            : base(message, null)
        {
        }
    }
}
