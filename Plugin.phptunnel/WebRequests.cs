using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace Plugin.phptunnel
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

        public static WebResultPing CreatePingUrl(PtunConnection conn)
        {
            var resp = CreateWebResponse(conn, "PING", null, null);
            return new WebResultPing(resp, conn.ResultSetEncoding);
        }

        public static WebResultPing CreatePingDb(PtunConnection conn)
        {
            var resp = CreateWebResponse(conn, "DBPING", null, null);
            return new WebResultPing(resp, conn.ResultSetEncoding);
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

        public static WebResultSendScript CreateSendScript(PtunConnection conn, IEnumerable<string> script)
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
