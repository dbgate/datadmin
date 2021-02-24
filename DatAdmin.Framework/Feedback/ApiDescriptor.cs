using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Net;

namespace DatAdmin
{
    public class ApiDescriptor
    {
        private static ApiDescriptor m_instance;
        private static bool m_loaded;
        private static object m_loadLock = new object();

        [XmlElem("send_error")]
        public string SendError { get; set; }
        [XmlElem("send_usage")]
        public string SendUsage { get; set; }
        [XmlElem("get_update")]
        public string GetUpdate { get; set; }
        [XmlElem("send_feedback")]
        public string SendFeedback { get; set; }
        [XmlElem("request_license")]
        public string RequestLicense { get; set; }

        static readonly string[] URLS = new string[] {
            "http://datadmin.jenasoft.com/apidesc.xml",
            "http://www.datadmin.com/apidesc.xml",
        };

        private bool Load()
        {
            XmlDocument doc = new XmlDocument();
            foreach (string url in URLS)
            {
                try
                {
                    var req = WebRequest.Create(url);
                    using (var resp = req.GetResponse())
                    {
                        using (var fr = resp.GetResponseStream())
                        {
                            doc.Load(fr);
                            this.LoadPropertiesCore(doc.DocumentElement);
                            // loaded OK
                            return true;
                        }
                    }
                }
                catch
                {
                    continue;
                }
            }
            return false;
        }

        public static ApiDescriptor GetInstance()
        {
            if (m_instance == null)
            {
                lock (m_loadLock)
                {
                    if (m_instance == null)
                    {
                        ApiDescriptor desc = new ApiDescriptor();
                        m_loaded = desc.Load();
                        m_instance = desc;
                    }
                }
            }
            if (!m_loaded) throw new HttpApiError("DAE-00306 API descriptor not found");
            return m_instance;
        }
    }
}
