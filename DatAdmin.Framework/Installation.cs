using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;

namespace DatAdmin
{
    public enum InstallationMode
    {
        Unknown, Personal, Professional
    }

    public class InstallationInfo
    {
        static InstallationInfo m_instance;

        public InstallationInfo()
        {
            InstallID = Guid.NewGuid().ToString();
            InstallMode = InstallationMode.Unknown;
        }

        public static InstallationInfo Instance
        {
            get
            {
                if (m_instance == null)
                {
                    m_instance = new InstallationInfo();
                    if (File.Exists(Framework.InstallInfoFile)) m_instance.Load();
                    else m_instance.Save();
                    //Framework.Instance.InitializeInstallInfo(m_instance);
                }
                return m_instance;
            }
        }

        private void Load()
        {
            var doc = new XmlDocument();
            doc.Load(Framework.InstallInfoFile);
            this.LoadProperties(doc.DocumentElement);
        }

        public void Save()
        {
            var doc = XmlTool.CreateDocument("InstallationInfo");
            this.SaveProperties(doc.DocumentElement);
            doc.Save(Framework.InstallInfoFile);
        }

        [XmlElem]
        public string InstallID { get; set; }

        [XmlElem]
        public InstallationMode InstallMode { get; set; }

        [XmlElem]
        public DateTime LastShown { get; set; }
    }
}
