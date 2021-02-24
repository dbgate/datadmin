using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace DatAdmin
{
    public class RootJobReportEnv : JobReportEnvBase
    {
        Job m_job;

        public RootJobReportEnv(Job job)
            : base(job.Root)
        {
            m_job = job;
        }

        public void LogMessage(LogMessageRecord msg)
        {
            foreach (var proc in m_processors)
            {
                var p = proc as JobLogProcessor;
                if (p != null) p.LogMessage(msg);
            }
        }
    }

    public class JobLogFactory : JobReportFactoryBase
    {
        public override string ToString()
        {
            return Texts.Get("s_log");
        }
        public override IJobReportConfiguration CreateConfig()
        {
            return new JobLogConfiguration();
        }
    }

    [JobReportConfiguration(Name = "joblog", Title = "Job log")]
    public class JobLogConfiguration : JobReportConfigurationBase
    {
        LogLevel m_logLevel = LogLevel.All;
        [TypeConverter(typeof(EnumDescConverter))]
        [XmlElem]
        public LogLevel LogLevel
        {
            get { return m_logLevel; }
            set { m_logLevel = value; }
        }

        public override string FileExtension
        {
            get { return "txt"; }
        }

        public override string ToString()
        {
            return Texts.Get("s_log") + ":" + FilePlace.ToString();
        }

        public override IJobReportProcessor CreateProcessor(JobCommand cmd)
        {
            return new JobLogProcessor(this, cmd);
        }

        //public ILogger CreateLogger(List<IFilePlace> placesToClose)
        //{
        //    placesToClose.Add(FilePlace);
        //    string file = FilePlace.GetWorkingFileName();
        //    return new AbsoluteFileLogger(file, () => m_logLevel);
        //}
    }

    public class JobLogProcessor : JobReportProcessorBase
    {
        AbsoluteFileLogger m_logger;

        public JobLogProcessor(JobLogConfiguration config, JobCommand cmd)
            : base(config, cmd)
        {
        }

        public override void OnStart()
        {
            base.OnStart();
            m_logger = new AbsoluteFileLogger(WorkingFileName, () => ((JobLogConfiguration)Config).LogLevel);
        }

        public override void OnFinish()
        {
            m_logger.Dispose();
            base.OnFinish();
        }

        public void LogMessage(LogMessageRecord msg)
        {
            m_logger.LogMessage(msg);
        }
    }
}
