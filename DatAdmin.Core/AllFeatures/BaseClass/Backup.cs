using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing.Design;
using System.Xml;

namespace DatAdmin
{
    public sealed class BackupContainer : PropertyPageBase
    {
        string m_filename;

        #region IBackupContainer Members

        [XmlAttrib("created")]
        public DateTime Created { get; set; }

        public IBackupFormat Format { get; private set; }

        #endregion

        public BackupContainer() { }

        public BackupContainer(string filename)
        {
            m_filename = filename;
            XmlDocument doc = new XmlDocument();
            doc.Load(filename);
            this.LoadPropertiesCore(doc.DocumentElement);
            Format = (IBackupFormat)BackupFormatAddonType.Instance.LoadAddon(doc.DocumentElement.FindElement("Format"));
        }

        public BackupContainer(string filename, IBackupFormat format)
        {
            m_filename = filename;
            Format = format;
            Created = DateTime.UtcNow;
        }

        public void Save()
        {
            XmlDocument doc = XmlTool.CreateDocument("Backup");
            this.SavePropertiesCore(doc.DocumentElement);
            Format.SaveToXml(doc.DocumentElement.AddChild("Format"));
            doc.Save(m_filename);
        }

        public IDatabaseLoader GetLoader(IDatabaseSource dst)
        {
            return Format.GetLoader(m_filename + Format.Extension, dst);
        }

        public void SaveToXml_ForJob(XmlElement xml)
        {
            this.SavePropertiesCore(xml, true);
            xml.AddChild("Filename").InnerText = m_filename;
            Format.SaveToXml(xml.AddChild("Format"));
        }

        public void LoadFromXml_ForJob(XmlElement xml)
        {
            this.LoadPropertiesCore(xml);
            m_filename = xml.FindElement("Filename").InnerText;
            Format = (IBackupFormat)BackupFormatAddonType.Instance.LoadAddon(xml.FindElement("Format"));
        }

        public ITreeNode[] GetChildTreeNodes(ITreeNode parent)
        {
            return Format.GetChildTreeNodes(parent, m_filename + Format.Extension);
        }
    }

    public abstract class BackupFormatBase : AddonBase, IBackupFormat
    {
        protected BackupFormatBase()
        {
            FileNameFormat = "yyyy-MM-dd-HH-mm-ss";
        }

        #region IBackupFormat Members

        public virtual void CheckBackupConfiguration(IDatabaseSource src) { }
        public virtual void CheckRestoreConfiguration(IDatabaseSource dst) { }
        public abstract IDatabaseWriter GetWriter(string file, IDatabaseSource src);
        public abstract IDatabaseLoader GetLoader(string file, IDatabaseSource dst);
        public abstract bool BackupSuitableFor(IDatabaseSource dst);

        [Browsable(false)]
        public abstract string Extension { get; }

        [DisplayName("s_file_name_format")]
        [XmlElem]
        public virtual string FileNameFormat { get; set; }

        [DisplayName("s_backup_folder")]
        [Description("s_backup_folder_desc")]
        [Editor(typeof(FolderNameEditor), typeof(UITypeEditor))]
        [XmlElem]
        public virtual string BackupFolder { get; set; }

        public virtual ITreeNode[] GetChildTreeNodes(ITreeNode parent, string filename)
        {
            return new ITreeNode[] { };
        }

        #endregion

        [Browsable(false)]
        public override AddonType AddonType
        {
            get { return BackupFormatAddonType.Instance; }
        }
    }
}
