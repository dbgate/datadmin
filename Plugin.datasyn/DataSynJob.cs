using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.Xml;
using System.ComponentModel;

namespace Plugin.datasyn
{
    [JobCommand(Name = "datasyn")]
    public class DataSynJobCommand : JobCommand
    {
        IDatabaseSource m_src;
        IDatabaseSource m_dst;
        DataSynDef m_datasyn;
        DataSynGuiEnv m_guienv;
        string m_outFile;

        public DataSynJobCommand(IDatabaseSource src, IDatabaseSource dst, DataSynDef datasyn, string outFile, List<IJobReportConfiguration> reports, DataSynGuiEnv guienv)
        {
            m_src = src;
            m_dst = dst;
            m_datasyn = datasyn;
            m_outFile = outFile;
            m_guienv = guienv;
            if (reports != null) ReportConfigs.AddRange(reports);
        }

        public DataSynJobCommand() { }

        protected override void DoRun(IJobRunEnv env)
        {
            DataSynJob.Synchronize(m_src, m_dst, ProgressInfo, m_datasyn, env.GetReportEnv(GroupId) as DataSynReportEnv, m_outFile, m_guienv);
        }

        public override string ToString()
        {
            return String.Format("{0}->{1}", m_src, m_dst);
        }

        public override void LoadFromXml(XmlElement xml)
        {
            base.LoadFromXml(xml);
            m_src = (IDatabaseSource)DatabaseSourceAddonType.Instance.LoadAddon(xml.FindElement("Source"));
            m_dst = (IDatabaseSource)DatabaseSourceAddonType.Instance.LoadAddon(xml.FindElement("Target"));
            m_datasyn = new DataSynDef();
            m_datasyn.LoadFromXml(xml.FindElement("Options"));
        }

        public override void SaveToXml(XmlElement xml)
        {
            base.SaveToXml(xml);
            xml.SetAttribute("type", "datasyn");
            m_src.SaveToXml(xml.AddChild("Source"));
            m_dst.SaveToXml(xml.AddChild("Target"));
            m_datasyn.SaveToXml(xml.AddChild("Options"));
        }

        public override JobReportEnvBase CreateReportEnv()
        {
            return new DataSynReportEnv(this);
        }

        public override void Edit(JobConnection jobconn)
        {
            DataSynForm.Run(jobconn, this);
        }

        [TypeConverter(typeof(ExpandableObjectConverter))]
        [DatAdmin.DisplayName("s_source")]
        public IDatabaseSource Source
        {
            get { return m_src; }
            internal set { m_src = value; }
        }

        [TypeConverter(typeof(ExpandableObjectConverter))]
        [DatAdmin.DisplayName("s_target")]
        public IDatabaseSource Target
        {
            get { return m_dst; }
            internal set { m_dst = value; }
        }

        public override void GetReportFactories(List<IJobReportFactory> res)
        {
            base.GetReportFactories(res);
            res.Add(new DataSynSqlReportFactory { RelatedCommand = this });
            res.Add(new DataSynInfoReportFactory { RelatedCommand = this });
        }

        public DataSynDef DataSyn { get { return m_datasyn; } }

        public override string TypeTitle
        {
            get { return "s_synchronize_data"; }
        }

        public override System.Drawing.Bitmap Image
        {
            get { return DataSynIcons.sync; }
        }
    }

    public static class DataSynJob
    {
        public static Job CreateJob(IDatabaseSource src, IDatabaseSource dst, DataSynDef datasyn, string outFile, List<IJobReportConfiguration> reports, JobProperties jobProps, DataSynGuiEnv guienv)
        {
            //UsageStats.Usage("datasyn_job", "src", src.ToString(), "dst", dst.ToString());
            return Job.FromCommand(new DataSynJobCommand(src, dst, datasyn, outFile, reports, guienv), jobProps);
        }

        public static void Synchronize(IDatabaseSource src, IDatabaseSource dst, IProgressInfo progress, DataSynDef datasyn, DataSynReportEnv repenv, string outFile, DataSynGuiEnv guienv)
        {
            Synchronizer syn = new Synchronizer(src, dst, progress, datasyn, repenv, outFile, guienv);
            syn.Progress = progress;
            syn.Run();
        }
    }

    public class DataSynGuiEnv
    {
        public bool CompareOnly;

        public event EventHandler<SynItemEventArgs> ItemEvent;
        public List<NameWithSchema> Filter = null;

        public void OnItemEvent(SynItem it, SynItemState ev)
        {
            MainWindow.Instance.RunInMainWindow(() =>
            {
                if (ItemEvent != null) ItemEvent(this, new SynItemEventArgs { State = ev, Item = it });
            });
        }

        public void SetFilter(params NameWithSchema[] args)
        {
            Filter = new List<NameWithSchema>(args);
        }
    }

    public class SynItemStats
    {
        public long Source;
        public long Target;
        public long OnlyInSource;
        public long OnlyInTarget;
        public long Modified;
        public long Equal;
    }

    public enum SynItemState { None, Start, Compared, Synchronized, Error }

    public class SynItemEventArgs : EventArgs
    {
        public SynItem Item;
        public SynItemState State;
    }
}
