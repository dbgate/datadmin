using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace DatAdmin
{
    public static class LoggerExtension
    {
        public static void LogMessage(this ILogger logger, LogLevel level, string message)
        {
            LogMessageRecord rec = new LogMessageRecord(level, message);
            (logger ?? Logging.Root).LogMessage(rec);
        }
        public static void LogMessage(this ILogger logger, string category, LogLevel level, string format, params object[] args)
        {
            string msg = format;
            if (args.Length > 0) msg = String.Format(format, args);
            (logger ?? Logging.Root).LogMessage(new LogMessageRecord { Category = category, Message = msg, Level = level });
        }
        public static void LogMessageDetail(this ILogger logger, string category, LogLevel level, string message, string detail)
        {
            (logger ?? Logging.Root).LogMessage(new LogMessageRecord { Category = category, Message = message, Detail = detail, Level = level });
        }
        public static void LogMessageEx(this ILogger logger, LogMessageRecord rec)
        {
            (logger ?? Logging.Root).LogMessage(rec);
        }
        public static void Trace(this ILogger logger, string message)
        {
            (logger ?? Logging.Root).LogMessage(LogLevel.Trace, message);
        }
        public static void Trace(this ILogger logger, string message, params object[] args)
        {
            (logger ?? Logging.Root).LogMessage(LogLevel.Trace, String.Format(message, args));
        }

        public static void Debug(this ILogger logger, string message)
        {
            (logger ?? Logging.Root).LogMessage(LogLevel.Debug, message);
        }
        public static void Debug(this ILogger logger, string message, params object[] args)
        {
            (logger ?? Logging.Root).LogMessage(LogLevel.Debug, String.Format(message, args));
        }

        public static void Info(this ILogger logger, string message)
        {
            (logger ?? Logging.Root).LogMessage(LogLevel.Info, message);
        }
        public static void Info(this ILogger logger, string message, params object[] args)
        {
            (logger ?? Logging.Root).LogMessage(LogLevel.Info, String.Format(message, args));
        }

        public static void Warning(this ILogger logger, string message)
        {
            (logger ?? Logging.Root).LogMessage(LogLevel.Warning, message);
        }
        public static void Warning(this ILogger logger, string message, params object[] args)
        {
            (logger ?? Logging.Root).LogMessage(LogLevel.Warning, String.Format(message, args));
        }

        public static void Error(this ILogger logger, string message)
        {
            (logger ?? Logging.Root).LogMessage(LogLevel.Error, message);
        }
        public static void Error(this ILogger logger, string message, params object[] args)
        {
            (logger ?? Logging.Root).LogMessage(LogLevel.Error, String.Format(message, args));
        }
    }
}
