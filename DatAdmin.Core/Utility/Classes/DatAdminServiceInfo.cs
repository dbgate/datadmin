using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml;

namespace DatAdmin
{
    public class DatAdminServiceInfo
    {
        [XmlElem]
        public string AppDataDirectory { get; set; }

        public void Fill()
        {
            AppDataDirectory = Core.AppDataDirectory;
        }

        public void Save()
        {
            var xml = XmlTool.CreateDocument("ServiceInfo");
            this.SavePropertiesCore(xml.DocumentElement);
            xml.Save(Path.Combine(Core.ProgramDirectory, "ServiceInfo.xml"));
        }

        public void Load()
        {
            var xml = new XmlDocument();
            xml.Load(Path.Combine(Core.ProgramDirectory, "ServiceInfo.xml"));
            this.LoadPropertiesCore(xml.DocumentElement);
        }

        public void Apply()
        {
            Core.AppDataOverride = AppDataDirectory;
        }
    }
}
