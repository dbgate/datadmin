using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace DatAdmin
{
    public static class SystemLogTool
    {
        public static void WriteEntry(string message, EventLogEntryType type)
        {
            try
            {
                if (!EventLog.SourceExists("DatAdmin"))
                {
                    EventLog.CreateEventSource("DatAdmin", "Application");
                }
                EventLog.WriteEntry("DatAdmin", message, type);
            }
            catch { }
        }
        public static void Info(string message)
        {
            WriteEntry(message, EventLogEntryType.Information);
        }
        public static void Warning(string message)
        {
            WriteEntry(message, EventLogEntryType.Warning);
        }
        public static void Error(string message)
        {
            WriteEntry(message, EventLogEntryType.Error);
        }
    }
}
