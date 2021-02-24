using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Web;
using System.Windows.Forms;
using System.IO;

namespace DatAdmin
{
    public partial class SavedJobsDocker : HtmlContentDocker
    {
        public SavedJobsDocker(IDockerFactory fact)
            : base(fact)
        {
            InitializeComponent();
            Procedures["runcmd"] = ((Action<Dictionary<string, string>>)RunCommand);
            Procedures["editcmd"] = ((Action<Dictionary<string, string>>)EditCommand);
            Procedures["runjob"] = ((Action<Dictionary<string, string>>)RunJob);
            Procedures["deletejob"] = ((Action<Dictionary<string, string>>)DeleteJob);
        }

        private void EditCommand(Dictionary<string, string> args)
        {
            Job job = Job.LoadFromFile(args["job"]);
            JobConnection jobconn = new JobConnection(args["job"]);
            JobCommand cmd = job.Root.FindCommand(args["cmd"]);
            cmd.Edit(jobconn);
        }

        private void RunJob(Dictionary<string, string> args)
        {
            Job job = Job.LoadFromFile(args["job"]);
            if (MessageBox.Show(Texts.Get("s_really_run$job", "job", job), VersionInfo.ProgramTitle, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                job.StartProcess();
            }
        }

        private void DeleteJob(Dictionary<string, string> args)
        {
            string fn = args["job"];
            if (StdDialog.ReallyDeleteFile(fn))
            {
                File.Delete(fn);
                RefreshHtml();
            }
        }

        private void RunCommand(Dictionary<string, string> args)
        {
            Job fulljob = Job.LoadFromFile(args["job"]);
            JobCommand cmd = fulljob.Root.FindCommand(args["cmd"]);
            Job job = Job.FromCommand(cmd, new JobProperties());
            if (MessageBox.Show(Texts.Get("s_really_run$job", "job", job), VersionInfo.ProgramTitle, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                job.StartProcess();
            }
        }

        private void RenderCommand(HtmlGenerator hg, string jobfn, JobCommand cmd)
        {
            if (cmd is JobPolyCommand)
            {
                RenderPolyCommandItems(hg, jobfn, (JobPolyCommand)cmd);
            }
            else
            {
                hg.Write(cmd.ToString());
                hg.Write(" - ");
                hg.Write("<a href=\"callback://runcmd?job:{1}&cmd:{2}\">{0}</a>", Texts.Get("s_run"), HttpUtility.UrlEncode(jobfn), HttpUtility.UrlEncode(cmd.GroupId));
                hg.Write(" | <a href=\"callback://editcmd?job:{1}&cmd:{2}\">{0}</a>", Texts.Get("s_edit"), HttpUtility.UrlEncode(jobfn), HttpUtility.UrlEncode(cmd.GroupId));
            }
        }

        private void RenderPolyCommandItems(HtmlGenerator hg, string jobfn, JobPolyCommand cmd)
        {
            hg.BeginUl();
            foreach (var c in cmd.Commands)
            {
                hg.BeginLi();
                RenderCommand(hg, jobfn, c);
                hg.EndLi();
            }
            hg.EndUl();
        }

        public override string GetHtml()
        {
            var hg = new HtmlGenerator();
            hg.BeginHtml("s_saved_jobs", HtmlGenerator.HtmlObjectViewStyle);
            hg.Heading("s_saved_jobs", 1);
            foreach (string fn in Directory.GetFiles(Core.JobsDirectory, "*.djb", SearchOption.AllDirectories).Sorted())
            {
                string relfn = IOTool.RelativePathTo(Core.JobsDirectory, fn);
                try
                {
                    Job job = Job.LoadFromFile(fn);
                    hg.Heading(relfn, 2);
                    hg.Write("<a href=\"callback://runjob?job:{0}\">{1}</a>", HttpUtility.UrlEncode(fn), Texts.Get("s_run"));
                    hg.Write(" | <a href=\"callback://deletejob?job:{0}\">{1}</a>", HttpUtility.UrlEncode(fn), Texts.Get("s_delete"));
                    RenderCommand(hg, fn, job.Root);
                    hg.HorizontalRule();
                }
                catch (Exception err)
                {
                    hg.Heading(relfn, 2);
                    hg.Write("{0}:{1}", Texts.Get("s_error"), err.Message);
                }
            }
            hg.EndHtml();
            return hg.HtmlText;
        }
    }

    //[DockerFactory(Title = "Saved jobs", Name = "saved_jobs", RequiredFeature = JobsFeature.Test)]
    //public class SavedJobsDockerFactory : DockerFactoryBase
    //{
    //    public override IDocker CreateDocker()
    //    {
    //        return new SavedJobsDocker(this);
    //    }

    //    public override string MenuTitle
    //    {
    //        get { return "s_saved_jobs"; }
    //    }

    //    public override Image Icon
    //    {
    //        get { return CoreIcons.job; }
    //    }

    //    public override DockerState InitialState
    //    {
    //        get { return DockerState.Document; }
    //    }

    //    public override Keys Shortcut
    //    {
    //        get { return Keys.Control | Keys.Alt | Keys.J; }
    //    }
    //}
}
