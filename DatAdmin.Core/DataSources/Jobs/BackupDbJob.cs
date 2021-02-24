using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;
using System.ComponentModel;

namespace DatAdmin
{
    [JobCommand(Name = "backupdb")]
    public class BackupDbJobCommand : JobCommand
    {
        IDatabaseSource m_src;
        IBackupFormat m_format;
        DatabaseStructureMembers m_members;

        public BackupDbJobCommand(IDatabaseSource src, IBackupFormat format, DatabaseStructureMembers members)
        {
            m_src = src;
            m_format = format;
            m_members = members;
        }

        public BackupDbJobCommand() { }

        public override string ToString()
        {
            return String.Format("{0}:{1}", Texts.Get("s_backup"), m_src);
        }

        [TypeConverter(typeof(ExpandableObjectConverter))]
        [DisplayName("s_database")]
        public IDatabaseSource Database { get { return m_src; } }

        [TypeConverter(typeof(ExpandableObjectConverter))]
        [DisplayName("s_format")]
        public IBackupFormat Format { get { return m_format; } }

        private string CreateFileName()
        {
            return DateTime.Now.ToString(m_format.FileNameFormat) + ".bck";
        }

        private string GetBackupDir()
        {
            return String.IsNullOrEmpty(m_format.BackupFolder) ? m_src.GetPrivateSubFolder("backups") : m_format.BackupFolder;
        }

        protected override void DoRun(IJobRunEnv env)
        {
            string dir = GetBackupDir();
            try { Directory.CreateDirectory(dir); }
            catch { }
            string bckfile = Path.Combine(dir, CreateFileName());
            IDatabaseWriter writer = m_format.GetWriter(bckfile + m_format.Extension, m_src);
            var copyOpts = new DatabaseCopyOptions
            {
                CopyMembers = m_members,
                DataMode = DbCopyDataMode.All,
                CopyStructure = true,
                SchemaMode = m_src.Dialect.DialectCaps.MultipleSchema ? DbCopySchemaMode.Original : DbCopySchemaMode.Explicit,
                ExplicitSchema = null
            };
            CopyDbJob.CopyDatabase(m_src, writer, ProgressInfo, copyOpts);
            SetCurWork(Texts.Get("s_finishing"));
            BackupContainer bc = new BackupContainer(bckfile, m_format);
            bc.Save();
        }

        public override void LoadFromXml(XmlElement xml)
        {
            base.LoadFromXml(xml);
            m_format = (IBackupFormat)BackupFormatAddonType.Instance.LoadAddon(xml.FindElement("Format"));
            m_members = new DatabaseStructureMembers();
            m_members.LoadFromXml_ForJob(xml.FindElement("Members"));
            m_src = (IDatabaseSource)DatabaseSourceAddonType.Instance.LoadAddon(xml.FindElement("Source"));
        }

        public override void SaveToXml(XmlElement xml)
        {
            base.SaveToXml(xml);
            xml.SetAttribute("type", "backupdb");

            // save real backup folder also if it is created from stored connection path
            string oldfolder = m_format.BackupFolder;
            m_format.BackupFolder = GetBackupDir();
            m_format.SaveToXml(xml.AddChild("Format"));
            m_format.BackupFolder = oldfolder;

            m_members.SaveToXml_ForJob(xml.AddChild("Members"));
            m_src.SaveToXml(xml.AddChild("Source"));
        }

        public override string TypeTitle
        {
            get { return "s_backup"; }
        }

        public override System.Drawing.Bitmap Image
        {
            get { return CoreIcons.backup; }
        }

        public override void GetUsageParams(UsageBuilder ub)
        {
            ub["src"] = m_src.SafeToString();
            ub["format_type"] = m_format != null ? m_format.GetType().FullName : "";
        }
    }

    public static class BackupDbJob
    {
        public static Job CreateJob(IDatabaseSource src, IBackupFormat format, DatabaseStructureMembers members, JobProperties jobProps)
        {
            return Job.FromCommand(new BackupDbJobCommand(src, format, members), jobProps);
        }
    }
}
