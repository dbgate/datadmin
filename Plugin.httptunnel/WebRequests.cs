using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace Plugin.httptunnel
{
    public class WebResultPing : WebResultBase
    {
        public WebResultPing(WebResponse resp, Encoding encoding)
            : base(resp, encoding)
        {
            try
            {
                ReadHeader("WPNG");
                ReadVersion();
                ReadHeader("WEND");
            }
            catch (Exception)
            {
                Close();
                throw;
            }
        }

        public static WebResultPing CreatePing(HtunConnection conn)
        {
            var resp = CreateWebResponse(conn, "PING", null, null);
            return new WebResultPing(resp, conn.ResultSetEncoding);
        }
    }

    public class WebResultDbPing : WebResultBase
    {
        public readonly string DbVersion;

        public WebResultDbPing(WebResponse resp, Encoding encoding)
            : base(resp, encoding)
        {
            try
            {
                ReadHeader("WDPG");
                ReadVersion();
                DbVersion = ReadString();
                ReadHeader("WEND");
            }
            catch (Exception)
            {
                Close();
                throw;
            }
        }

        public static WebResultDbPing CreatePing(HtunConnection conn)
        {
            var resp = CreateWebResponse(conn, "DBPING", null, null);
            return new WebResultDbPing(resp, conn.ResultSetEncoding);
        }
    }

    public class WebResultSendScript : WebResultBase
    {
        public WebResultSendScript(WebResponse resp, Encoding encoding)
            : base(resp, encoding)
        {
            try
            {
                ReadHeader("WRUN");
                ReadVersion();
                ReadHeader("WEND");
            }
            catch (Exception)
            {
                Close();
                throw;
            }
        }

        public static WebResultSendScript CreateSendScript(HtunConnection conn, IEnumerable<string> script)
        {
            var extpars = new Dictionary<string, byte[]>();
            int index = 0;
            foreach (string cmd in script)
            {
                extpars["COMMAND" + index.ToString()] = EncodeCommand(conn, cmd);
                index++;
            }
            extpars["CMDCOUNT"] = Encoding.ASCII.GetBytes(index.ToString());
            var resp = CreateWebResponse(conn, "SCRIPT", extpars, null);
            return new WebResultSendScript(resp, conn.ResultSetEncoding);
        }
    }
}
