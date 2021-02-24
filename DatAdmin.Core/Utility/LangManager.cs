using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.IO;

namespace DatAdmin
{
    public class LangInfo
    {
        public readonly string Name;
        public readonly string LocalName;
        public readonly string Identifier;

        public LangInfo(string filename)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filename);

            Name = doc.DocumentElement.GetAttribute("name");
            LocalName = doc.DocumentElement.GetAttribute("locname");
            Identifier = doc.DocumentElement.GetAttribute("id");
        }
    }

    public static class LangManager
    {
        public readonly static List<LangInfo> Languages = new List<LangInfo>();

        static LangManager()
        {
            foreach (string filename in Directory.GetFiles(Core.LangDirectory))
            {
                if (filename.ToLower().EndsWith(".xml"))
                {
                    Languages.Add(new LangInfo(filename));
                }
            }
        }

    }
}
