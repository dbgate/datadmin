using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Threading;
using System.ComponentModel;
using System.IO;

namespace DatAdmin
{
    public enum LogLevel
    {
        [Description("s_all")]
        All = 0,
        [Description("s_log_trace")]
        Trace = 10,
        [Description("s_log_debug")]
        Debug = 20,
        [Description("s_log_info")]
        Info = 30,
        [Description("s_log_warning")]
        Warning = 40,
        [Description("s_log_error")]
        Error = 50,
        [Description("s_log_fatal")]
        Fatal = 60,
        [Description("s_log_off")]
        Off = 100
    };

    public interface ILogger
    {
        void LogMessage(LogMessageRecord message);
    }

    public interface ILogMessageSource
    {
        event LogMessageEventHandler OnMessage;
        List<LogMessageRecord> GetMessages();
        int? Capacity { get; }
        LogMessageRecord SeekRecord(LogMessageRecord current, SeekOrigin origin, int pos, bool fixEdge);
    }

    public class LogMessageRecord
    {
        [XmlElem]
        public LogLevel Level { get; set; }
        [XmlElem]
        public string Message { get; set; }
        [XmlElem]
        public DateTime Created { get; set; }
        [XmlElem]
        public string ThreadName { get; set; }
        [XmlElem]
        public string Detail { get; set; }
        [XmlElem]
        public string Category { get; set; }

        public Dictionary<string, string> CustomData { get; set; }

        public LogMessageRecord()
        {
            ThreadName = Thread.CurrentThread.ManagedThreadId.ToString();
            Created = DateTime.Now;
            Detail = "";
            CustomData = new Dictionary<string, string>();
        }

        public LogMessageRecord(LogLevel level, string message)
            : this()
        {
            Level = level;
            Message = message;
        }

        public string FormatLine()
        {
            return String.Format("{0:T} [{1}] {2} {3}", Created, ThreadName, Level, Message);
        }

        public void SaveToXml(XmlElement xml)
        {
            this.SavePropertiesCore(xml);
        }
    }

    public class LogMessageEventArgs : EventArgs
    {
        public bool Handled;
        public LogMessageRecord LogRecord;
    }

    public delegate void LogMessageEventHandler(object sender, LogMessageEventArgs e);

}
