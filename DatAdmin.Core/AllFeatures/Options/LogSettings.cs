using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing.Design;

namespace DatAdmin
{
    [SettingsPage(Name = "log", Title = "s_log", Targets = SettingsTargets.Global, ImageName = CoreIcons.logName)]
    public class LogSettings : SettingsPageBase
    {
        //private bool m_showLog = true;
        //[TypeConverter(typeof(YesNoTypeConverter))]
        //[DisplayName("s_show_log")]
        //public bool ShowLog
        //{
        //    get { return m_showLog; }
        //    set { m_showLog = value; }
        //}

        private int m_windowCacheSize = 1000;
        [DisplayName("s_window_log_cache_size")]
        [SettingsKey("log.window_cache_size")]
        public int WindowCacheSize
        {
            get { return m_windowCacheSize; }
            set { m_windowCacheSize = value; }
        }

        private LogLevel m_fileLogLevel = LogLevel.Off;
        [DisplayName("s_file_log_level")]
        [TypeConverter(typeof(EnumDescConverter))]
        [SettingsKey("log.file_log_level")]
        public LogLevel FileLogLevel
        {
            get { return m_fileLogLevel; }
            set { m_fileLogLevel = value; }
        }

        private LogLevel m_cliFileLogLevel = LogLevel.Off;
        [DisplayName("s_cli_file_log_level")]
        [TypeConverter(typeof(EnumDescConverter))]
        [SettingsKey("log.cli_file_log_level")]
        public LogLevel CliFileLogLevel
        {
            get { return m_cliFileLogLevel; }
            set { m_cliFileLogLevel = value; }
        }

        private LogLevel m_windowLogLevel = LogLevel.Off;
        [DisplayName("s_window_log_level")]
        [TypeConverter(typeof(EnumDescConverter))]
        [SettingsKey("log.window_log_level")]
        public LogLevel WindowLogLevel
        {
            get { return m_windowLogLevel; }
            set { m_windowLogLevel = value; }
        }
    }

    public static class SettingsPageCollection_Log
    {
        public static LogSettings Log(this SettingsPageCollection col)
        {
            return (LogSettings)col.PageByName("log");
        }
    }
}
