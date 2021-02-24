using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DatAdmin
{
    public abstract class JobReportEnvBase : IDisposable
    {
        protected JobCommand m_cmd;
        protected List<IJobReportProcessor> m_processors = new List<IJobReportProcessor>();
        protected JobReportEnvBase(JobCommand cmd)
        {
            m_cmd = cmd;
        }

        public virtual void OnStart()
        {
            foreach (var rep in m_cmd.ReportConfigs)
            {
                var proc = rep.CreateProcessor(m_cmd);
                proc.OnStart();
                m_processors.Add(proc);
            }
        }

        public virtual void OnFinish()
        {
            foreach (var proc in m_processors) proc.OnFinish();
        }

        #region IDisposable Members

        public virtual void Dispose()
        {
            foreach (var proc in m_processors) proc.Dispose();
        }

        #endregion
    }

    public abstract class JobReportProcessorBase : IJobReportProcessor
    {
        public IFilePlace FilePlace;
        public string WorkingFileName;
        public JobReportConfigurationBase Config;
        public JobCommand Command;

        public JobReportProcessorBase(JobReportConfigurationBase config, JobCommand command)
        {
            Config = config;
            Command = command;
            FilePlace = FilePlaceAddonType.PlaceFromVirtualFile(config.FilePlace.GetVirtualFile());
            FilePlace.SetFileHolderInfo(Config);
        }

        public virtual void OnStart()
        {
            WorkingFileName = FilePlace.GetWorkingFileName();
        }

        public virtual void OnFinish()
        {
            FilePlace.FinalizeFileName();
        }

        #region IDisposable Members

        public virtual void Dispose()
        {
            FilePlace.Dispose();
        }

        #endregion
    }

    public abstract class FormattedJobReportProcessorBase : JobReportProcessorBase 
    {
        protected ITextFormatter Formatter;
        protected StreamWriter Writer;

        public FormattedJobReportProcessorBase(FormattedJobReportConfigurationBase config, JobCommand command)
            : base(config, command)
        {
        }

        public override void OnStart()
        {
            base.OnStart();
            Writer = new StreamWriter(WorkingFileName);
            var cfg = (FormattedJobReportConfigurationBase)Config;
            Formatter = cfg.GetFormatter();
            Formatter.Language = cfg.Language;
            Formatter.Writer = Writer;
        }

        public override void OnFinish()
        {
            Writer.Close();
            base.OnFinish();
        }

        public override void Dispose()
        {
            base.Dispose();
            Writer.Dispose();
        }
    }
}
