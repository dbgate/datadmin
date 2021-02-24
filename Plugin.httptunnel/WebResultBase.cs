using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Net;
using DatAdmin;
using System.Threading;

namespace Plugin.httptunnel
{
    public class WebResultBase : IDisposable
    {
        public const int VERSION = 10;

        Stream m_fr;
        WebResponse m_resp;

        byte[] m_buf4 = new byte[4];
        byte[] m_buf1 = new byte[1];
        byte[] m_werr = Encoding.ASCII.GetBytes("WERR");

        Encoding m_encoding;

        public WebResultBase(WebResponse resp, Encoding encoding)
        {
            try
            {
                m_encoding = encoding;
                m_resp = resp;
                m_fr = resp.GetResponseStream();
            }
            catch (Exception)
            {
                Close();
                throw;
            }
        }

        protected void ReadHeader(string data)
        {
            byte[] b = Encoding.ASCII.GetBytes(data);
            ReadBuffer4();
            TestError();
            if (!ArrayTool.EqualsArrays(m_buf4, b))
            {
                Logging.Error("Bad content of PHP Tunnel:" + Encoding.ASCII.GetString(IOTool.ReadToEnd(m_fr)));
                throw new StreamFormatError(String.Format("DAE-00325 Invalid PHP - Tunnel format, expected {0}, found {1}", data, Encoding.ASCII.GetString(m_buf4)));
            }
        }

        protected void TestError()
        {
            if (ArrayTool.EqualsArrays(m_werr, m_buf4))
            {
                ReadVersion();
                throw new DatabaseError("DAE-00326 " + ReadString());
            }
        }

        protected bool ReadBool()
        {
            ReadBuffer(m_buf1);
            return (int)m_buf1[0] == (int)'1';
        }

        protected byte[] ReadBytes()
        {
            int len = ReadInt4();
            if (len < 0) return null;
            byte[] buf = new byte[len];
            ReadBuffer(buf);
            return buf;
        }

        protected string ReadString()
        {
            byte[] data = ReadBytes();
            if (data == null) return null;
            return m_encoding.GetString(data);
        }

        protected void ReadVersion()
        {
            int realVersion = ReadInt4();
            if (realVersion != VERSION) throw new BadVersionError("DAE-00327", realVersion);
        }

        protected void ReadBuffer(byte[] buf)
        {
            int pos = 0;
            int zerocounter = 0;
            while (pos < buf.Length)
            {
                int l = m_fr.Read(buf, pos, buf.Length - pos);
                if (l == 0) zerocounter++;
                else zerocounter = 0;
                if (zerocounter > 2) throw new StreamFormatError("DAE-00328 Error reading stream, not enough data");
                pos += l;
            }
        }

        protected void ReadBuffer4()
        {
            ReadBuffer(m_buf4);
        }

        protected int ReadInt4()
        {
            ReadBuffer4();
            long res = m_buf4[3] * 256 * 256 * 256 + m_buf4[2] * 256 * 256 + m_buf4[1] * 256 + m_buf4[0];
            return (int)res;
        }

        public void Close()
        {
            if (m_fr != null)
            {
                m_fr.Close();
                m_fr = null;
            }
            if (m_resp != null)
            {
                m_resp.Close();
                m_resp = null;
            }
        }

        protected bool IsInBuffer4(string cmp)
        {
            return ArrayTool.EqualsArrays(m_buf4, Encoding.ASCII.GetBytes(cmp));
        }

        public static WebResponse CreateWebResponse(HtunConnection conn, string op, Dictionary<string, byte[]> extpars, string sqlForLogPurpose)
        {
            var fp = conn.FailoverParams;
            var start = DateTime.Now;
            Exception httpError = null;
            int reqindex = 0;
            for (; reqindex < fp.MaxRequestCount; reqindex++)
            {
                try
                {
                    HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(conn.Driver.Url);

                    if (!String.IsNullOrEmpty(conn.Driver.HttpLogin))
                    {
                        webrequest.Credentials = new NetworkCredential(conn.Driver.HttpLogin, conn.Driver.HttpPassword);
                    }


                    webrequest.ContentType = "application/x-www-form-urlencoded; charset=" + conn.Driver.RealEncoding.WebName;
                    webrequest.Method = "POST";

                    var pars = new Dictionary<string, byte[]>();
                    var spars = new Dictionary<string, string>();
                    conn.GetDatabaseParams(spars);
                    foreach (var tuple in spars)
                    {
                        pars[tuple.Key] = Encoding.ASCII.GetBytes(tuple.Value);
                    }
                    pars["ENCODINGSTYLE"] = Encoding.ASCII.GetBytes(conn.Driver.EncodingStyle.ToString());
                    pars["VERSION"] = Encoding.ASCII.GetBytes(VERSION.ToString());
                    pars["OPERATION"] = Encoding.ASCII.GetBytes(op);
                    pars["DATABASE"] = Encoding.ASCII.GetBytes(conn.Database ?? "");
                    pars["SESSIONINIT"] = EncodeCommand(conn, conn.SessionInitScript ?? "");

                    //pars["HOST"] = Encoding.ASCII.GetBytes(conn.Params.Host ?? "");
                    //pars["USER"] = Encoding.ASCII.GetBytes(conn.Params.Login ?? "");
                    ////pars["PORT"] = Encoding.ASCII.GetBytes(PtunDefaults.Instance.Port(conn.Params.Engine, conn.Params.Port).ToString());
                    //pars["PORT"] = Encoding.ASCII.GetBytes(conn.Params.Port.ToString());
                    //pars["PASSWORD"] = Encoding.ASCII.GetBytes(conn.Params.Password ?? "");
                    //pars["ENGINE"] = Encoding.ASCII.GetBytes(conn.Params.Engine ?? "");
                    //pars["DATABASE"] = Encoding.ASCII.GetBytes(conn.Database ?? "");
                    pars.AddAll(extpars);

                    string pars_enc = StringTool.UrlEncode(pars);
                    byte[] data = conn.Driver.RealEncoding.GetBytes(pars_enc);
                    webrequest.ContentLength = data.Length;

                    webrequest.Timeout = reqindex == 0 ? fp.HttpRequestTimeoutMilisecs : fp.RepeatedHttpRequestTimeoutMilisecs;
                    using (Stream requestStream = webrequest.GetRequestStream())
                    {
                        requestStream.Write(data, 0, data.Length);
                    }

                    try
                    {
                        WebResponse resp = webrequest.GetResponse();
                        return resp;
                    }
                    catch (Exception e)
                    {
                        Logging.Warning(String.Format("HTTP tunnel error (run {0}), trying again:{1}", reqindex, e.Message));
                        Thread.Sleep(fp.DelayBeforeRepeatedRequestMilisecs);
                        httpError = e;
                    }
                    if ((DateTime.Now - start).TotalMilliseconds > fp.RepeatedRequestTimeoutMilisecs)
                    {
                        break;
                    }
                }
                catch (UriFormatException err)
                {
                    throw new ConnectionFailedError("DAE-00329", err);
                }
            }
            Logging.Info(String.Format("Repeated HTTPTUN request failed, repeated={0}", reqindex));
            if (sqlForLogPurpose != null) DbConnectionExtension.LogExecuteQuery(sqlForLogPurpose);
            else Logging.Debug("Executing HTTPTUN operation:" + op);
            throw httpError;
        }

        protected static byte[] EncodeCommand(HtunConnection conn, string sql)
        {
            byte[] cmd = (conn.BinaryCommandEncoding ?? conn.ResultSetEncoding).GetBytes(sql);
            Array.Reverse(cmd);
            return cmd;
        }

        #region IDisposable Members

        public void Dispose()
        {
            Close();
        }

        #endregion
    }
}
