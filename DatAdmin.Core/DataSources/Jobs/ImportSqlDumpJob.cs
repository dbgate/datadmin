using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.ComponentModel;

namespace DatAdmin
{
    [JobCommand(Name = "importsqldump")]
    public class ImportSqlDumpJobCommand : JobCommand
    {
        IDatabaseSource m_dst;
        IDatabaseLoader m_loader;

        public ImportSqlDumpJobCommand(IDatabaseLoader loader, IDatabaseSource dst)
        {
            m_loader = loader;
            m_dst = dst;
        }

        public ImportSqlDumpJobCommand() { }

        protected override void DoRun(IJobRunEnv env)
        {
            try
            {
                Async.SafeOpen(m_dst.Connection);
                m_loader.ProgressInfo = ProgressInfo;
                m_dst.Connection.Invoke1(m_loader.LoadDatabase, m_dst);
            }
            finally
            {
                Async.SafeClose(m_dst.Connection);
            }
        }

        public override void LoadFromXml(XmlElement xml)
        {
            base.LoadFromXml(xml);
            m_loader = (IDatabaseLoader)DatabaseLoaderAddonType.Instance.LoadAddon(xml.FindElement("Loader"));
            m_dst = (IDatabaseSource)DatabaseSourceAddonType.Instance.LoadAddon(xml.FindElement("Target"));
        }

        public override void SaveToXml(XmlElement xml)
        {
            base.SaveToXml(xml);
            //xml.SetAttribute("type", "importsqldump");
            m_loader.SaveToXml(xml.AddChild("Loader"));
            m_dst.SaveToXml(xml.AddChild("Target"));
        }

        public override string ToString()
        {
            return String.Format("{0}->{1}", m_loader, m_dst);
        }

        [DisplayName("s_source")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public IDatabaseLoader GuiLoader { get { return m_loader; } }

        [DisplayName("s_target")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public IDatabaseSource GuiTarget { get { return m_dst; } }

        public override string TypeTitle
        {
            get { return "s_import_sql_dump"; }
        }

        public override System.Drawing.Bitmap Image
        {
            get { return CoreIcons.import; }
        }

        public override void GetUsageParams(UsageBuilder ub)
        {
            ub["loader"] = m_loader.SafeToString();
            ub["dst"] = m_dst.SafeToString();
        }
    }

    public class ImportSqlDumpJob
    {
        public static Job CreateJob(IDatabaseLoader loader, IDatabaseSource dst, JobProperties jobProps)
        {
            return Job.FromCommand(new ImportSqlDumpJobCommand(loader, dst), jobProps);
        }
    }
}
