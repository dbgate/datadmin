using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;

namespace DatAdmin
{
    public class JobRunEnv : Dictionary<string, object>, IJobRunEnv
    {
        Dictionary<string, JobReportEnvBase> m_reportEnvs = new Dictionary<string, JobReportEnvBase>();

        public JobReportEnvBase GetReportEnv(string command)
        {
            return m_reportEnvs.Get(command, null);
        }
        public void SetReportEnv(string command, JobReportEnvBase repenv)
        {
            m_reportEnvs[command] = repenv;
        }
    }

    public abstract class JobCommand : AddonBase, IConnectionPackHolder
    {
        internal Job m_job;
        List<IJobReportConfiguration> m_reportConfigs = new List<IJobReportConfiguration>();
        public List<IJobReportConfiguration> ReportConfigs { get { return m_reportConfigs; } }

        ConnectionPack m_connPack;
        public ConnectionPack ConnPack
        {
            get { return m_connPack; }
            set
            {
                if (m_connPack != null) m_connPack.Release();
                m_connPack = value;
                if (m_connPack != null) m_connPack.AddRef();
            }
        }

        #region IJobCommand Members

        [Browsable(false)]
        public virtual bool CanCancel { get { return true; } }
        public virtual void Cancel() { }

        public virtual string UsageEventName { get { return "jobcmd:" + GetType().FullName; } }

        public virtual void Run(IJobRunEnv env)
        {
            JobReportEnvBase repenv = null;
            if (m_job.Root != this)
            {
                repenv = CreateReportEnv();
            }
            if (repenv != null)
            {
                try
                {
                    env.SetReportEnv(GroupId, repenv);
                    repenv.OnStart();
                }
                catch (Exception err)
                {
                    ProgressInfo.Warning("Error initializing reports:" + err.Message);
                }
            }
            try
            {
                using (var ub = new UsageBuilder(UsageEventName))
                {
                    GetUsageParams(ub);

                    try
                    {
                        if (m_job.m_process != null)
                        {
                            JobCommand oldcmd = m_job.m_process.CurrentCommand;
                            try
                            {
                                m_job.m_process.CurrentCommand = this;
                                DoRun(env);
                            }
                            finally
                            {
                                m_job.m_process.CurrentCommand = oldcmd;
                                // close connections
                                ConnPack = null;
                            }
                        }
                        else
                        {
                            try
                            {
                                DoRun(env);
                            }
                            finally
                            {
                                // close connections
                                ConnPack = null;
                            }
                        }
                        ub["result"] = "ok";
                    }
                    catch (Exception err)
                    {
                        ub["result"] = "error";
                        ub["error"] = err.Message;
                        throw;
                    }
                }
            }
            finally
            {
                if (repenv != null)
                {
                    try
                    {
                        repenv.OnFinish();
                    }
                    catch (Exception err)
                    {
                        ProgressInfo.Warning("Error finishing reports:" + err.Message);
                    }
                    try
                    {
                        repenv.Dispose();
                    }
                    catch (Exception err)
                    {
                        ProgressInfo.Warning("Error disposing reports:" + err.Message);
                    }
                }
            }
        }

        #endregion

        public JobCommand()
        {
            GroupId = Guid.NewGuid().ToString();
            ConnPack = new ConnectionPack(this);
        }

        protected abstract void DoRun(IJobRunEnv env);

        public void SetCurWork(string value)
        {
            if (m_job.m_process != null) m_job.m_process.SetCurWork(value);
        }
        [Browsable(false)]
        public IProgressInfo ProgressInfo { get { return m_job.m_process; } }

        [Browsable(false)]
        public override AddonType AddonType
        {
            get { return JobCommandAddonType.Instance; }
        }

        [XmlElem]
        [Browsable(false)]
        public string GroupId { get; set; }

        public virtual JobCommand Clone(bool newGroupId)
        {
            JobCommand res = (JobCommand)this.GetType().GetConstructor(new Type[] { }).Invoke(new object[] { });
            XmlDocument doc = XmlTool.CreateDocument("Command");
            SaveToXml(doc.DocumentElement);
            res.LoadFromXml(doc.DocumentElement);
            if (newGroupId) res.GroupId = Guid.NewGuid().ToString();
            return res;
        }

        public void StartProcess()
        {
            Job.FromCommand(this, new JobProperties()).StartProcess();
        }

        public virtual void Edit(JobConnection jobconn)
        {
            MainWindow.Instance.OpenContent(new JobCommandEditorFrame(jobconn, this));
        }

        public virtual void GetAllCommands(List<JobCommand> res)
        {
            res.Add(this);
        }

        public virtual void GetReportFactories(List<IJobReportFactory> res)
        {
        }

        [Browsable(false)]
        public abstract string TypeTitle { get; }
        [Browsable(false)]
        public virtual Bitmap Image { get { return null; } }

        public override void SaveToXml(XmlElement xml)
        {
            base.SaveToXml(xml);

            foreach (var cfg in ReportConfigs)
            {
                cfg.SaveToXml(xml.AddChild("Report"));
            }
        }

        public override void LoadFromXml(XmlElement xml)
        {
            base.LoadFromXml(xml);

            foreach (XmlElement x in xml.SelectNodes("Report"))
            {
                m_reportConfigs.Add((IJobReportConfiguration)JobReportConfigurationAddonType.Instance.LoadAddon(x));
            }
        }

        public virtual JobCommand FindCommand(string groupid)
        {
            if (GroupId == groupid) return this;
            return null;
        }

        public virtual JobReportEnvBase CreateReportEnv() { return null; }

        public virtual void GetUsageParams(UsageBuilder ub) { }
    }

    public class JobPolyCommand : JobCommand
    {
        internal List<JobCommand> m_commands = new List<JobCommand>();

        public override void LoadFromXml(XmlElement xml)
        {
            base.LoadFromXml(xml);
            foreach (XmlElement child in xml.SelectNodes("Command"))
            {
                try
                {
                    JobCommand cmd = (JobCommand)JobCommandAddonType.Instance.LoadAddon(child);
                    cmd.m_job = m_job;
                    m_commands.Add(cmd);
                }
                catch (Exception err)
                {
                    var cmd = new JobErrorCommand { Message = err.Message };
                    cmd.m_job = m_job;
                    m_commands.Add(cmd);
                    Errors.LogError(err);
                }
            }
        }

        public override void SaveToXml(XmlElement xml)
        {
            base.SaveToXml(xml);
            foreach (var cmd in m_commands)
            {
                cmd.SaveToXml(XmlTool.AddChild(xml, "Command"));
            }
        }

        protected override void DoRun(IJobRunEnv env)
        {
            foreach (var cmd in m_commands)
            {
                cmd.Run(env);
            }
        }

        public void ReplaceCommand(JobCommand command)
        {
            for (int i = 0; i < m_commands.Count; i++)
            {
                if (m_commands[i].GroupId == command.GroupId)
                {
                    m_commands[i] = command;
                    return;
                }
                var p = m_commands[i] as JobPolyCommand;
                if (p != null) p.ReplaceCommand(command);
            }
        }

        public override JobCommand FindCommand(string groupid)
        {
            var res = base.FindCommand(groupid);
            if (res != null) return res;
            for (int i = 0; i < m_commands.Count; i++)
            {
                res = m_commands[i].FindCommand(groupid);
                if (res != null) return res;
            }
            return null;
        }

        public List<JobCommand> Commands { get { return m_commands; } }

        public override void GetAllCommands(List<JobCommand> res)
        {
            base.GetAllCommands(res);
            foreach (var cmd in Commands) cmd.GetAllCommands(res);
        }

        public override string TypeTitle
        {
            get { return "s_commands"; }
        }

        public override void GetReportFactories(List<IJobReportFactory> res)
        {
            base.GetReportFactories(res);
            foreach (var cmd in Commands) cmd.GetReportFactories(res);
        }

        public override string UsageEventName
        {
            get { return null; }
        }
    }

    [JobCommand(Name = "error")]
    public class JobErrorCommand : JobCommand
    {
        [XmlElem]
        public string Message { get; set; }

        protected override void DoRun(IJobRunEnv env)
        {
            ProgressInfo.Warning("Error when loading job command");
            ProgressInfo.Error(Message);
        }

        public override Bitmap Image
        {
            get { return CoreIcons.error; }
        }

        public override string TypeTitle
        {
            get { return "s_error"; }
        }

        public override string ToString()
        {
            return Message;
        }
    }
    
    [JobCommand(Name = "calljob")]
    public class CallJobJobCommand : JobCommand
    {
        [XmlElem]
        public string JobFile { get; set; }

        public override string TypeTitle
        {
            get { return "s_call_job"; }
        }
        public override Bitmap Image
        {
            get { return CoreIcons.job; }
        }
        protected override void DoRun(IJobRunEnv env)
        {
            var job = Job.LoadFromFile(Path.Combine(Core.JobsDirectory, JobFile));
            job.Run(new Dictionary<string, string>());
        }
    }

    public class JobProperties
    {
        public bool ContinueOnErrors = true;
    }


    public class Job
    {
        internal string m_title;
        internal Process m_process;
        private JobProperties m_jobProps;
        private string m_fileName;
        
        JobPolyCommand m_rootCommand = new JobPolyCommand();

        public Job()
        {
            m_rootCommand.m_job = this;
        }

        public JobPolyCommand Root { get { return m_rootCommand; } }

        public void Run(Dictionary<string, string> args)
        {
            JobRunEnv env = new JobRunEnv();
            foreach (string arg in args.Keys) env[arg] = args[arg];
            m_rootCommand.Run(env);
        }
        public static Job LoadFromFile(string file)
        {
            Job res = new Job();
            res.m_fileName = file;
            XmlDocument doc = new XmlDocument();
            doc.Load(file);
            res.m_rootCommand.LoadFromXml(doc.DocumentElement);
            //res.m_title = doc.DocumentElement.GetAttribute("title");
            res.m_title = Path.GetFileNameWithoutExtension(file);
            return res;
        }

        public string SavedName
        {
            get
            {
                if (m_fileName != null) return Path.GetFileNameWithoutExtension(m_fileName);
                return null;
            }
        }

        public string Title
        {
            get { return m_title; }
            set { m_title = value; }
        }

        public void SaveToFile(string file)
        {
            XmlDocument doc = XmlTool.CreateDocument("Job");
            m_rootCommand.SaveToXml(doc.DocumentElement);
            //doc.DocumentElement.SetAttribute("title", m_title);
            doc.Save(file);
        }
        public void AddCommand(JobCommand command)
        {
            m_rootCommand.m_commands.Add(command);
            command.m_job = this;
            if (m_title == null) m_title = command.ToString();
        }

        public Process CreateProcess(Dictionary<string,string> args)
        {
            if (m_process != null) throw new Exception("DAE-00238 Job.m_process allready set");
            m_process = new Process(this, args, m_jobProps);
            return m_process;
        }

        public static Job FromCommand(JobCommand command, JobProperties jobProps)
        {
            Job res = new Job();
            res.m_jobProps = jobProps;
            res.AddCommand(command);
            using (var ub = new UsageBuilder("create_job:" + command.GetType().FullName))
            {
                command.GetUsageParams(ub);
            }
            return res;
        }

        public static Job FromCommands(IEnumerable<JobCommand> commands, JobProperties jobProps)
        {
            Job res = new Job();
            res.m_jobProps = jobProps;
            foreach (var cmd in commands)
            {
                res.AddCommand(cmd.Clone(false));
            }
            return res;
        }

        public void StartProcess()
        {
            Process process = CreateProcess(new Dictionary<string,string>());
            process.Start();
        }

        public static SaveJobResult AskAndExportToFile(Func<Job> createJob)
        {
            return SaveJobForm.Run(createJob);
            //if (!Registration.TryCheckEdition(SoftwareEdition.Professional, "export to job")) return;
            //string name = InputBox.Run(Texts.Get("s_select_job_name"), "job");
            //if (name != null)
            //{
            //    string fn = Path.Combine(Core.JobsDirectory, name + ".djb");
            //    if (File.Exists(fn))
            //    {
            //        if (MessageBox.Show(Texts.Get("s_file_exists_overwrite$file", "file", fn), "DatAdmin", MessageBoxButtons.YesNo) != DialogResult.Yes) return;
            //    }
            //    try
            //    {
            //        Job job = createJob();
            //        job.SaveToFile(fn);
            //        UsageStats.Usage("export_as_job", "jobname", job.ToString());
            //    }
            //    catch (Exception err)
            //    {
            //        Errors.Report(err);
            //    }
            //}
        }

        public override string ToString()
        {
            return m_title;
        }

        public List<JobCommand> GetAllCommands()
        {
            var res = new List<JobCommand>();
            Root.GetAllCommands(res);
            return res;
        }

        public List<JobCommand> GetAllElementaryCommands()
        {
            var res = new List<JobCommand>();
            Root.GetAllCommands(res);
            res.RemoveIf(c => c is JobPolyCommand);
            return res;
        }

        public void GetReportFactories(List<IJobReportFactory> res)
        {
            res.Add(new JobLogFactory { RelatedCommand = Root });
            Root.GetReportFactories(res);
        }

        public List<IJobReportFactory> GetReportFactories()
        {
            var res = new List<IJobReportFactory>();
            GetReportFactories(res);
            return res;
        }

        public List<IJobReportConfiguration> GetAllReportConfigs()
        {
            var res = new List<IJobReportConfiguration>();
            foreach (var cmd in GetAllCommands()) res.AddRange(cmd.ReportConfigs);
            return res;
        }

        public JobCommand FindCommand(string groupid)
        {
            return Root.FindCommand(groupid);
        }
    }

    /// <summary>
    /// Connection to job file, handles simultanous write acces to one job file
    /// One connection must be used by one object, but more objects can safely use more connections to one file
    /// Also is wrapper for effective late loading
    /// </summary>
    public class JobConnection
    {
        internal string m_file;
        DateTime? m_fileTimeStamp = null;
        Job m_job;

        public JobConnection(string file)
        {
            m_file = file;
        }

        public string FileName { get { return m_file; } }

        public string ShortName { get { return Path.GetFileNameWithoutExtension(m_file); } }

        public void Reload()
        {
            if (m_file == null) return;
            if (m_job != null && new FileInfo(m_file).LastWriteTime <= m_fileTimeStamp) return;
            m_job = Job.LoadFromFile(m_file);
            m_fileTimeStamp = new FileInfo(m_file).LastWriteTime;
        }

        public void SaveToFile()
        {
            m_job.SaveToFile(m_file);
            m_fileTimeStamp = new FileInfo(m_file).LastWriteTime;
        }

        public Job GetJob()
        {
            Reload();
            return m_job;
        }

        public void SaveCommand(JobCommand command)
        {
            Reload();
            m_job.Root.ReplaceCommand(command);
            SaveToFile();
        }

        public override string ToString()
        {
            return m_file;
        }

        public void SaveReports(JobCommand cmd)
        {
            m_job = null;
            Reload();
            var cmd2 = m_job.Root.FindCommand(cmd.GroupId);
            cmd2.ReportConfigs.Clear();
            foreach (var rep in cmd.ReportConfigs)
            {
                cmd2.ReportConfigs.Add(rep.Clone());
            }
            SaveToFile();
        }

        public void SaveReports(Job job)
        {
            m_job = null;
            Reload();
            foreach (var cmd in job.GetAllCommands())
            {
                var cmd2 = m_job.FindCommand(cmd.GroupId);
                cmd2.ReportConfigs.Clear();
                foreach (var rep in cmd.ReportConfigs)
                {
                    cmd2.ReportConfigs.Add(rep.Clone());
                }
            }
            SaveToFile();
        }

        public JobCommand GetCommand(string groupid)
        {
            Reload();
            return m_job.FindCommand(groupid);
        }
    }
}
