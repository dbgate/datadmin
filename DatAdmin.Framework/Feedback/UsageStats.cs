using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace DatAdmin
{
    public class UsageSubEvent
    {
        [XmlElem("Used")]
        public DateTime UsedAt { get; set; }
        [XmlElem("Type")]
        public string EventType { get; set; }
        [XmlElem]
        public string Param1 { get; set; }
        [XmlElem]
        public string Param2 { get; set; }
    }

    public class UsageRecord
    {
        public string EventType;
        public Dictionary<string, string> Params = new Dictionary<string, string>();
        public DateTime UsedAt;
        public List<UsageSubEvent> SubEvents = new List<UsageSubEvent>();
        public TimeSpan Duration = TimeSpan.FromSeconds(0);

        public void Save(XmlElement xml)
        {
            xml.AddChild("Type").InnerXml = EventType;
            xml.AddChild("Used").InnerXml = UsedAt.ToString("s");
            xml.AddChild("Duration").InnerXml = Math.Round(Duration.TotalSeconds).ToString();
            XmlTool.SaveParameters(xml, Params);
            foreach (var sub in SubEvents)
            {
                sub.SaveProperties(xml.AddChild("Sub"));
            }
        }
    }

    public class UsageBuilder : IDisposable
    {
        bool m_isSent = false;

        public string EventType;
        public Dictionary<string, string> Params = new Dictionary<string, string>();
        public DateTime UsedAt;
        public List<UsageSubEvent> SubEvents = new List<UsageSubEvent>();

        public UsageBuilder(string eventType)
        {
            EventType = eventType;
            UsedAt = DateTime.UtcNow;
        }

        public string this[string name]
        {
            get { return Params[name]; }
            set { Params[name] = value; }
        }

        #region IDisposable Members

        public void Dispose()
        {
            Send();
        }

        #endregion

        public void Send()
        {
            if (EventType == null) return;
            lock (this)
            {
                if (m_isSent) return;
                UsageStats.Usage(CreateRecord());
                m_isSent = true;
            }
        }

        public UsageRecord CreateRecord()
        {
            var res = new UsageRecord
            {
                EventType = EventType,
                UsedAt = UsedAt,
                Duration = DateTime.UtcNow - UsedAt,
            };
            res.Params.AddAll(Params);
            res.SubEvents.AddRange(SubEvents);
            return res;
        }

        public void AddSub(string type)
        {
            AddSub(type, null, null);
        }

        public void AddSub(string type, string par1)
        {
            AddSub(type, par1, null);
        }

        public void AddSub(string type, string par1, string par2)
        {
            if (m_isSent) return;
            if (EventType == null) return;
            lock (SubEvents)
            {
                SubEvents.Add(new UsageSubEvent
                {
                    EventType = type,
                    Param1 = par1,
                    Param2 = par2,
                    UsedAt = DateTime.UtcNow,
                });
            }
        }
    }

    public static class UsageStats
    {
        public const int MinimalUploadCount = 5;
        static List<UsageRecord> m_usages = new List<UsageRecord>();
        public static DateTime ProgramStartedAt { get; private set; }
        //static DateTime m_startedAt;

        public static void Usage(string eventType, params string[] extpars)
        {
            Dictionary<string, string> pars = new Dictionary<string, string>();
            for (int i = 0; i < extpars.Length; i += 2)
            {
                if (extpars[i] != null && extpars[i + 1] != null)
                {
                    pars[extpars[i]] = extpars[i + 1];
                }
            }
            var usgrec = new UsageRecord { EventType = eventType, Params = pars, UsedAt = DateTime.UtcNow };
            Usage(usgrec);
        }

        public static void Usage(UsageRecord usgrec)
        {
            lock (m_usages)
            {
                m_usages.Add(usgrec);
            }
        }

        public static string GetAndClear()
        {
            if (!Framework.Instance.AllowSendUsageStats()) return "";
            var send = new List<UsageRecord>();
            lock (m_usages)
            {
                send.AddRange(m_usages);
                m_usages.Clear();
            }
            if (send.Count == 0) return "";
            XmlDocument doc = XmlTool.CreateDocument("Statistic");
            XmlElement xml = doc.DocumentElement;
            foreach (var item in send)
            {
                item.Save(xml.AddChild("Usage"));
            }
            return doc.GetPackedDocumentXml();
        }

        public static void ClosingApp()
        {
            HUsage.CallClosingApp();
            SendUsageStatsThread.SendUsage();
        }

        public static void OnStart()
        {
            ProgramStartedAt = DateTime.UtcNow;
        }

        public static void OnFinish()
        {
        }

        //public static void 

        //public static void OnFinish()
        //{
        //    XmlDocument doc = XmlTool.CreateDocument("Execute");
        //    XmlElement xml = doc.DocumentElement;
        //    xml.AddChild("Started").InnerText = m_startedAt.ToString("s");
        //    xml.AddChild("Finished").InnerText = DateTime.UtcNow.ToString("s");
        //    lock (m_usages)
        //    {
        //        foreach (var item in m_usages)
        //        {
        //            item.Save(xml.AddChild("Usage"));
        //        }
        //    }
        //    //doc.Save(Path.Combine(Core.UsageDirectory, DateTime.UtcNow.ToString("yyyy-MM-dd-HH-mm-ss") + ".xml"));
        //}
    }
}
