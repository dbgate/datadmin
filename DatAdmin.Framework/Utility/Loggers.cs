using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;

namespace DatAdmin
{
    public abstract class LoggerBase : ILogger
    {
        #region ILogger Members

        public abstract void LogMessage(LogMessageRecord message);

        #endregion
    }

    public abstract class FileLoggerBase : LoggerBase, IDisposable
    {
        Func<LogLevel> m_minloglevel;
        StreamWriter m_filelog = null;

        protected abstract string GetFileName();

        public FileLoggerBase(Func<LogLevel> minloglevel)
        {
            m_minloglevel = minloglevel;
        }

        public override void LogMessage(LogMessageRecord rec)
        {
            if (rec.Level >= m_minloglevel())
            {
                if (m_filelog == null)
                {
                    try
                    {
                        m_filelog = new StreamWriter(GetFileName(), true);
                    }
                    catch (Exception)
                    {
                        m_filelog = null;
                    }
                }
                if (m_filelog != null)
                {
                    m_filelog.WriteLine(rec.FormatLine());
                    if (rec.Detail != "") m_filelog.WriteLine(rec.Detail);
                    m_filelog.Flush();
                }
            }
        }


        #region IDisposable Members

        public void Dispose()
        {
            if (m_filelog != null)
            {
                m_filelog.Close();
                m_filelog = null;
            }
        }

        #endregion
    }

    public class LogsDirectoryFileLogger : FileLoggerBase
    {
        string m_filemask;

        public LogsDirectoryFileLogger(string filemask, Func<LogLevel> minloglevel)
            : base(minloglevel)
        {
            m_filemask = filemask;
        }

        protected override string GetFileName()
        {
            return Path.Combine(Framework.LogsDirectory, DateTime.Now.ToString(m_filemask));
        }
    }

    public class AbsoluteFileLogger : FileLoggerBase
    {
        string m_filename;

        public AbsoluteFileLogger(string filename, Func<LogLevel> minloglevel)
            : base(minloglevel)
        {
            m_filename = filename;
        }

        protected override string GetFileName()
        {
            return m_filename;
        }
    }

    public class MultiCastLogger : LoggerBase
    {
        List<ILogger> m_loggers;

        public MultiCastLogger(IEnumerable<ILogger> loggers)
        {
            m_loggers = new List<ILogger>(loggers);
        }

        public MultiCastLogger()
        {
            m_loggers = new List<ILogger>();
        }

        public override void LogMessage(LogMessageRecord message)
        {
            lock (m_loggers)
            {
                foreach (var log in m_loggers) log.LogMessage(message);
            }
        }

        public void AddLogger(ILogger logger)
        {
            lock (m_loggers)
            {
                m_loggers.Add(logger);
            }
        }
    }

    public class ThreadInvokeLogger : LoggerBase
    {
        Thread m_thread;
        WaitQueue<LogMessageRecord> m_queue = new WaitQueue<LogMessageRecord>();
        ILogger m_target;
        
        void LoggerThreadProc()
        {
            for (; ; )
            {
                LogMessageRecord rec = m_queue.Get();
                if (rec == null) return;
                m_target.LogMessage(rec);
            }
        }

        public ThreadInvokeLogger(ILogger logger)
        {
            m_target = logger;
            m_thread = new Thread(LoggerThreadProc);
            m_thread.Name = "Logging Thread";
            m_thread.Start();
        }

        public void QuitLogging()
        {
            m_queue.Put(null);
        }

        public override void LogMessage(LogMessageRecord message)
        {
            m_queue.Put(message);
        }

        public void WaitForQuit()
        {
            m_thread.Join();
        }
    }

    public class CachingLogger : LoggerBase, ILogMessageSource
    {
        int? m_cacheSize;
        List<LogMessageRecord> m_cache = new List<LogMessageRecord>();
        public LogLevel MinLogLevel { get; set; }

        public CachingLogger(LogLevel minloglevel, int? cacheSize)
        {
            m_cacheSize = cacheSize;
            MinLogLevel = minloglevel;
        }

        public CachingLogger(LogLevel minloglevel)
            : this(minloglevel, null)
        {
        }

        public override void LogMessage(LogMessageRecord rec)
        {
            if (rec.Level >= MinLogLevel)
            {
                if (OnMessage != null) OnMessage(this, new LogMessageEventArgs { LogRecord = rec });
                lock (m_cache)
                {
                    while (m_cacheSize != null && m_cache.Count >= m_cacheSize) m_cache.RemoveAt(0);
                    m_cache.Add(rec);
                }
            }
        }

        #region ILogMessageSource Members

        public event LogMessageEventHandler OnMessage;

        public List<LogMessageRecord> GetMessages()
        {
            lock (m_cache)
            {
                return new List<LogMessageRecord>(m_cache);
            }
        }

        public int? Capacity { get { return m_cacheSize; } }
        public int Count { get { return m_cache.Count; } }

        public LogMessageRecord SeekRecord(LogMessageRecord current, SeekOrigin origin, int pos, bool fixEdge)
        {
            lock (m_cache)
            {
                int curpos = m_cache.IndexOf(current);
                int index = -1;
                switch (origin)
                {
                    case SeekOrigin.Begin:
                        index = pos;
                        break;
                    case SeekOrigin.Current:
                        index = curpos + pos;
                        break;
                    case SeekOrigin.End:
                        index = m_cache.Count - 1 - pos;
                        break;
                }
                if (fixEdge)
                {
                    if (index >= m_cache.Count) index = m_cache.Count - 1;
                    if (index < 0) index = 0;
                }
                if (index >= 0 && index < m_cache.Count) return m_cache[index];
                return null;
            }
        }

        #endregion
    }

    public class ConsoleLogger : LoggerBase
    {
        public override void LogMessage(LogMessageRecord rec)
        {
            Console.Error.WriteLine("{0}: {1}", rec.Level, rec.Message);
        }
    }

    public class NopLogger : LoggerBase
    {
        public static readonly NopLogger Instance = new NopLogger();

        private NopLogger() { }
        public override void LogMessage(LogMessageRecord message) { }
    }
}
