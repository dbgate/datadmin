using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DatAdmin
{
    [CommandLineCommand(Name = "runjob", Description = "Runs saved job")]
    public class RunJobCommand : CommandLineCommandBase
    {
        public override ICommandLineCommandInstance CreateInstance()
        {
            return new Instance();
        }

        public override bool AllowExtParams { get { return true; } }

        class Instance : CommandLineCommandInstanceBase
        {
            string m_jobname;

            [CommandLineParameter(Name = "jobname", Positional = true, Description = "Name of job, without .djb extension")]
            public string Jobname
            {
                get { return m_jobname; }
                set { m_jobname = value; }
            }

            string m_filtercommands;

            [CommandLineParameter(Name = "commands", Positional = false, Description = "| delimited command identifiers; If ommited, all commands are executed")]
            public string Filtercommands
            {
                get { return m_filtercommands; }
                set { m_filtercommands = value; }
            }

            public override void RunCommand()
            {
                if (!LicenseTool.FeatureAllowedMsg(JobsFeature.Test))
                {
                    Logging.Error("Proffesional edition required");
                    return;
                }
                if (m_jobname.ToLower().EndsWith(".djb")) m_jobname = m_jobname.Substring(0, m_jobname.Length - 4);
                Job job = Job.LoadFromFile(Path.Combine(Core.JobsDirectory, m_jobname + ".djb"));
                Logging.Info("Running job: " + job.ToString());
                if (!String.IsNullOrEmpty(Filtercommands))
                {
                    var job2 = new Job();
                    var ids = new HashSetEx<string>(Filtercommands.Split('|'));
                    foreach (var cmd in job.Root.Commands)
                    {
                        if (ids.Contains(cmd.GroupId)) job2.AddCommand(cmd.Clone(false));
                    }
                    job2 = job;
                }
                job.Run(ExtParams);
                Logging.Info("Job finished");
            }
        }
    }
}
