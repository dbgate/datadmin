using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace DatAdmin
{
    //public class ProcessEventArgs : EventArgs
    //{
    //    public readonly Process Process;
    //    internal ProcessEventArgs(Process process)
    //    {
    //        Process = process;
    //    }
    //}
    //public delegate void ProcessEventHandler(object sender, ProcessEventArgs e);

    public enum ProcessState { Starting, Running, Finished, Canceled, Error_Finished, Warning_Finished };

    public static class ProcessStateExtension
    {
        public static bool IsEndState(this ProcessState state)
        {
            return state == ProcessState.Finished || state == ProcessState.Canceled || state == ProcessState.Error_Finished || state == ProcessState.Warning_Finished;
        }
    }

    public sealed class Process : IProgressInfo
    {
        ProcessState m_state;
        Thread m_thread;
        string m_curWork;
        DateTime m_started = DateTime.Now;
        DateTime? m_finished = null;
        Job m_job;
        Dictionary<string, string> m_args;
        JobCommand m_currentCommand;
        JobProperties m_jobProps;
        CachingLogger m_logCache;
        RootJobReportEnv m_reportEnv;
        Exception m_failError;
        int m_closeOnFinishSeverity;
        public bool CloseOnFinish { get; private set; }

        public JobCommand CurrentCommand
        {
            get { return m_currentCommand; }
            set { m_currentCommand = value; }
        }

        public Process(Job job, Dictionary<string,string> args, JobProperties jobProps)
        {
            m_job = job;
            m_args = args;
            m_jobProps = jobProps;
            m_logCache = new CachingLogger(LogLevel.Info, null);
            m_reportEnv = new RootJobReportEnv(job);
        }

        public ProcessState State { get { return m_state; } }
        public string CurWork { get { return m_curWork; } }
        public TimeSpan? Estimate { get { return null; } }
        public string Description = "";
        public string Title { get { return m_job.m_title; } }
        public event Action OnFinish;

        public TimeSpan Duration
        {
            get
            {
                if (m_finished != null) return m_finished.Value - m_started;
                return DateTime.Now - m_started;
            }
        }

        private void Run()
        {
            m_job.Run(m_args);
        }

        private void DoRun()
        {
            m_reportEnv.OnStart();
            using (var ub = new UsageBuilder("process"))
            {
                try
                {
                    try
                    {
                        SetState(ProcessState.Running);
                        ProcessRegister.OnStartedProcess(this);
                        Logging.Debug("Starting process {0}", Title);
                        this.LogMessage("s_process", LogLevel.Info, Texts.Get("s_process_started"));
                        Run();
                        this.LogMessage("s_process", LogLevel.Info, Texts.Get("s_process_finished"));
                        SetState(ProcessState.Finished);
                        Logging.Debug("Process {0} finished succesfully", Title);
                    }
                    finally
                    {
                        m_curWork = "s_finished";
                        HProcess.CallFinishedProcess(this);
                        m_finished = DateTime.Now;
                        ub["result"] = "ok";
                    }
                }
                catch (Exception e)
                {
                    m_failError = e;
                    this.LogError(e);
                    //this.LogMessage("s_process", LogLevel.Error, Errors.ExtractMessage(e));
                    this.LogMessage("s_process", LogLevel.Error, Texts.Get("s_process_failed"));
                    Logging.Error("Error when running process {0}, process finished abnormally", Title);
                    SetState(ProcessState.Error_Finished);
                    ub["result"] = "error";
                    ub["error"] = e.Message;
                    //Errors.Report(e);
                }
            }
            if (OnFinish != null) OnFinish();
            m_reportEnv.OnFinish();
            m_reportEnv.Dispose();
        }

        public ILogMessageSource LogSource { get { return m_logCache; } }

        public void Start()
        {
            m_thread = new Thread(DoRun);
            m_thread.Start();
        }

        private void SetState(ProcessState value)
        {
            if (m_state == ProcessState.Canceled) return;
            m_state = value;
            Changed();
        }

        private void Changed()
        {
            HProcess.CallChangedProcess(this);
        }

        public override string ToString()
        {
            return Title;
        }

        public string StateTitle
        {
            get
            {
                switch (m_state)
                {
                    case ProcessState.Canceled: return Texts.Get("s_canceled");
                    case ProcessState.Error_Finished: return Texts.Get("s_error_finished");
                    case ProcessState.Finished: return Texts.Get("s_finished");
                    case ProcessState.Running: return Texts.Get("s_running");
                    case ProcessState.Starting: return Texts.Get("s_starting");
                    case ProcessState.Warning_Finished: return Texts.Get("s_warning_finished");
                }
                return "";
            }
        }

        public void Cancel()
        {
            try
            {
                if (m_currentCommand != null) m_currentCommand.Cancel();
            }
            catch
            {
                Logging.Debug("Error canceling current command");
            }
            if (!CanCancel) throw new Exception("DAE-00298 " + Texts.Get("s_process_cannot_be_canceled"));
            SetState(ProcessState.Canceled);
            Thread cthrerad = new Thread((ThreadStart)m_thread.Abort);
            cthrerad.Start();
        }

        public bool CanCancel
        {
            get { return m_state == ProcessState.Running || m_state == ProcessState.Starting; }
        }

        public void CancelWithQuery()
        {
            if (MessageBox.Show(Texts.Get("s_really_cancel_process"), VersionInfo.ProgramTitle, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Cancel();
            }
        }

        public Job Job { get { return m_job; } }

        #region IProgressInfo Members

        public void LogMessage(LogMessageRecord msg)
        {
            m_logCache.LogMessage(msg);
            Logging.LogMessage(msg);
            m_reportEnv.LogMessage(msg);
        }

        public void SetCurWork(string value)
        {
            m_curWork = value;
            Changed();
        }

        public void RaiseError(string error)
        {
            if (m_jobProps != null && !m_jobProps.ContinueOnErrors)
            {
                throw new ProcessAbortException(error);
            }
        }

        public void SetCloseOnFinish(int severity, bool close)
        {
            if (severity > m_closeOnFinishSeverity || (severity == m_closeOnFinishSeverity && !close))
            {
                m_closeOnFinishSeverity = severity;
                CloseOnFinish = close;
            }
        }

        #endregion


        public Exception GetFailReasonError()
        {
            return m_failError;
        }

        public ProcessFailedError GetFailError()
        {
            var res = new ProcessFailedError(this);
            res.Data["process_name"] = ToString();
            try
            {
                XmlDocument doc = XmlTool.CreateDocument("Job");
                m_job.Root.SaveToXml(doc.DocumentElement);
                XmlTool.RemovePasswords(doc.DocumentElement);
                res.Data["job_definition"] = doc.OuterXml;
            }
            catch
            {
            }
            return res;
        }
    }
}
