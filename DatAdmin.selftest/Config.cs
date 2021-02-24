using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace DatAdmin.selftest
{
    public class ServerConnection
    {
        public string CreateMacro;
        public string ConFileName;
    }

    public class Config
    {
        public string AppDataDirectory;

        [XmlElement("ServerConnection")]
        public List<ServerConnection> Connections = new List<ServerConnection>();

        public static void Load(string file)
        {
            XmlSerializer ser = new XmlSerializer(typeof(Config));
            using (FileStream fr = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                Instance = (Config)ser.Deserialize(fr);
            }
        }

        public static Config Instance;
    }
}
