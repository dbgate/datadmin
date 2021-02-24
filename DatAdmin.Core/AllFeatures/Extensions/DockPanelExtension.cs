using System;
using System.Collections.Generic;
using System.Text;
using WeifenLuo.WinFormsUI.Docking;
using System.IO;
using System.Xml;

namespace DatAdmin
{
    public static class DockPanelExtension
    {
        public static void SaveToXml(this DockPanel panel, XmlElement xml)
        {
            var ms = new MemoryStream();
            panel.SaveAsXml(ms, Encoding.UTF8);
            var doc = new XmlDocument();
            doc.Load(new MemoryStream(ms.ToArray()));
            xml.AppendChild(xml.OwnerDocument.ImportNode(doc.DocumentElement, true));
        }

        public static void LoadFromXml(this DockPanel panel, XmlElement xml, DeserializeDockContent deserializeContent)
        {
            var ms = new MemoryStream();
            var sw = new StreamWriter(ms);
            sw.Write(xml.InnerXml);
            sw.Flush();
            panel.LoadFromXml(new MemoryStream(ms.ToArray()), deserializeContent);
        }
    }
}
