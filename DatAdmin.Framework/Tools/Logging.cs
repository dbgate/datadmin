using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Threading;
using System.IO;
using System.Text.RegularExpressions;
using System.ComponentModel;
using System.Xml;

namespace DatAdmin
{
    public delegate void LogMessageDelegate(LogMessageRecord msg);

    public static class Logging
    {
        const int CACHE_SIZE = 20;

        public static ThreadInvokeLogger Root;
        public static MultiCastLogger MultiCast;
        //static CachingLogger m_feedbackCache = new CachingLogger(LogLevel.Debug, CACHE_SIZE);
        //public static event LogMessageDelegate OnMessage;

        static Logging()
        {
            MultiCast = new MultiCastLogger();
            Root = new ThreadInvokeLogger(MultiCast);
        }

        //static WaitQueue<LogMessageRecord> m_queue = new WaitQueue<LogMessageRecord>();
        //static List<LogMessageRecord> m_lastRecords = new List<LogMessageRecord>();

        //static void LoggerThreadProc()
        //{
        //    for (; ; )
        //    {
        //        LogMessageRecord rec = m_queue.Get();
        //        if (rec == null) return;
        //        if (OnMessage != null) OnMessage(rec);
        //        LogToFile(rec);
        //    }
        //}

        //static StreamWriter m_filelog = null;


        //private static void LogToFile(LogMessageRecord rec, LogLevel minloglevel, string filemask)
        //{
        //    if (rec.Level >= minloglevel)
        //    {
        //        if (m_filelog == null)
        //        {
        //            try
        //            {
        //                m_filelog = new StreamWriter(Path.Combine(Core.LogsDirectory, DateTime.Now.ToString(filemask)), true);
        //            }
        //            catch (Exception)
        //            {
        //                m_filelog = null;
        //            }
        //        }
        //        if (m_filelog != null)
        //        {
        //            m_filelog.WriteLine(rec.FormatLine());
        //            if (rec.Detail != "") m_filelog.WriteLine(rec.Detail);
        //            m_filelog.Flush();
        //        }
        //    }
        //}

        //private static void LogToFile(LogMessageRecord rec)
        //{
        //    if (Core.IsGUI) LogToFile(rec, GlobalSettings.Pages.Log.FileLogLevel, "yyyy'-'MM'-'dd'.log'");
        //    else LogToFile(rec, GlobalSettings.Pages.Log.CliFileLogLevel, "'cli-'yyyy'-'MM'-'dd'-'HH'-'mm'-'ss'.log'");
        //}

        public static List<LogMessageRecord> GetFeedbackLastLogEntries()
        {
            return new List<LogMessageRecord>();
            //return new List<LogMessageRecord>(m_feedbackCache.GetMessages());
        }

        public static string LevelName(LogLevel level)
        {
            return level.ToString();
        }

        //public static Color LevelColor(LogLevel level)
        //{
        //    switch (level)
        //    {
        //        case LogLevel.Trace:
        //            return Color.Aqua;
        //        case LogLevel.Debug:
        //            return Color.Lime;
        //        case LogLevel.Info:
        //            return Color.White;
        //        case LogLevel.Warning:
        //            return Color.Yellow;
        //        case LogLevel.Error:
        //            return Color.Red;
        //        case LogLevel.Fatal:
        //            return Color.Magenta;
        //    }
        //    return Color.White;
        //}

        //public static void LogMessage(LogMessageRecord rec)
        //{
        //    if (rec.Level >= LogLevel.Debug)
        //    {
        //        // dont cache traces...
        //        lock (m_lastRecords)
        //        {
        //            while (m_lastRecords.Count >= CACHE_SIZE) m_lastRecords.RemoveAt(0);
        //            m_lastRecords.Add(rec);
        //        }
        //        if (!Core.IsGUI) Console.Error.WriteLine("{0}: {1}", rec.Level, rec.Message);
        //    }
        //    m_queue.Put(rec);
        //}

        public static void LogMessage(LogMessageRecord rec)
        {
            Root.LogMessage(rec);
        }

        public static void SaveLogs(IEnumerable<LogMessageRecord> log, XmlElement xml)
        {
            if (log == null) return;
            foreach (var elem in log)
            {
                elem.SaveToXml(xml.AddChild("LogRecord"));
            }
        }

        public static void LogMessage(LogLevel level, string message)
        {
            Root.LogMessage(level, message);
        }
        public static void Trace(string message)
        {
            Root.Trace(message);
        }
        public static void Trace(string message, params object[] args)
        {
            Root.Trace(message, args);
        }

        public static void Debug(string message)
        {
            Root.Debug(message);
        }
        public static void Debug(string message, params object[] args)
        {
            Root.Debug(message, args);
        }

        public static void Info(string message)
        {
            Root.Info(message);
        }
        public static void Info(string message, params object[] args)
        {
            Root.Info(message, args);
        }

        public static void Warning(string message)
        {
            Root.Warning(message);
        }
        public static void Warning(string message, params object[] args)
        {
            Root.Warning(message, args);
        }

        public static void Error(string message)
        {
            Root.Error(message);
        }
        public static void Error(string message, params object[] args)
        {
            Root.Error(message, args);
        }

        public static void QuitLogging()
        {
            Root.QuitLogging();
        }

        public static string MangleConnectionString_RemovePassword(string conns)
        {
            conns = Regex.Replace(conns, @"Password=[^;]*", "Password=****", RegexOptions.IgnoreCase);
            conns = Regex.Replace(conns, @"Pwd=[^;]*", "Pwd=****", RegexOptions.IgnoreCase);
            return conns;
        }

        public static void QuitAndWait()
        {
            Root.QuitLogging();
            Root.WaitForQuit();
        }
    }

    //public class LogTrace : IDisposable
    //{
    //    string m_message;
    //    public LogTrace(string message)
    //    {
    //        m_message = message;
    //        Logging.Trace("BEGIN:" + m_message);
    //    }
    //    #region IDisposable Members

    //    public void Dispose()
    //    {
    //        Logging.Trace("END:" + m_message);
    //    }

    //    #endregion
    //}
}
