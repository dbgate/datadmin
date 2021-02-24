using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace DatAdmin
{
    public static class LogLevelExtension
    {
        public static string GetTitle(this LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Trace: return "TRACE";
                case LogLevel.Debug: return "DEBUG";
                case LogLevel.Info: return "s_info";
                case LogLevel.Warning: return "s_warning";
                case LogLevel.Error: return "s_error";
                case LogLevel.Fatal: return "FATAL";
            }
            return "???";
        }
        public static Bitmap GetImage(this LogLevel level)
        {
            switch (level)
            {
                case LogLevel.Trace: return CoreIcons.trace;
                case LogLevel.Debug: return CoreIcons.debug;
                case LogLevel.Info: return CoreIcons.info;
                case LogLevel.Warning: return CoreIcons.warning;
                case LogLevel.Error: return CoreIcons.error;
                case LogLevel.Fatal: return CoreIcons.fatal;
            }
            return null;
        }
    }
}
