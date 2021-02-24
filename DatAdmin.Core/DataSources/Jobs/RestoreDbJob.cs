using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;

namespace DatAdmin
{
    [JobCommand(Name = "restoredb")]
    public class RestoreDbJobCommand : JobCommand
    {
        BackupContainer m_backup;
        IDatabaseSource m_dst;

        public RestoreDbJobCommand(BackupContainer backup, IDatabaseSource dst)
        {
            m_dst = dst;
            m_backup = backup;
        }

        public RestoreDbJobCommand() { }

        public override string ToString()
        {
            return String.Format("{0}:{1}", Texts.Get("s_restore"), m_dst);
        }

        protected override void DoRun(IJobRunEnv env)
        {
            IDatabaseLoader loader = m_backup.GetLoader(m_dst);
            loader.LoadDatabase(m_dst);
        }

        public override void LoadFromXml(XmlElement xml)
        {
            base.LoadFromXml(xml);
            m_backup = new BackupContainer();
            m_backup.LoadFromXml_ForJob(xml.FindElement("Backup"));
            m_dst = (IDatabaseSource)DatabaseSourceAddonType.Instance.LoadAddon(xml.FindElement("Target"));
        }

        public override void SaveToXml(XmlElement xml)
        {
            base.SaveToXml(xml);
            xml.SetAttribute("type", "restoredb");
            m_backup.SaveToXml_ForJob(xml.AddChild("Backup"));
            m_dst.SaveToXml(xml.AddChild("Target"));
        }

        public override string TypeTitle
        {
            get { return "s_restore"; }
        }

        public override System.Drawing.Bitmap Image
        {
            get { return CoreIcons.restore; }
        }

        public override void GetUsageParams(UsageBuilder ub)
        {
            ub["backuptype"] = m_backup.SafeToString();
            ub["dst"] = m_dst.SafeToString();
        }
    }

    public static class RestoreDbJob
    {
        public static Job CreateJob(BackupContainer backup, IDatabaseSource dst, JobProperties jobProps)
        {
            return Job.FromCommand(new RestoreDbJobCommand(backup, dst), jobProps);
        }
    }
}
