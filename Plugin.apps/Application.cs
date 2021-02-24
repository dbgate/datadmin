using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.Xml;
using System.Windows.Forms;
using System.IO;

namespace Plugin.apps
{
    public class Application
    {
        public static Dictionary<string, Application> Cache = new Dictionary<string, Application>();

        public AppTreeTplNode Root = new AppTreeTplNode();
        public IDatabaseSource Database;

        public void SaveToXml(XmlElement xml)
        {
            Root.SaveToXml(xml.AddChild("Tree"));
            if (Database != null) Database.SaveToXml(xml.AddChild("Database"));
        }
        public void SaveToFile(string file)
        {
            var doc = XmlTool.CreateDocument("Application");
            SaveToXml(doc.DocumentElement);
            doc.Save(file);
        }

        public void LoadFromFile(string file)
        {
            var doc = new XmlDocument();
            doc.Load(file);
            LoadFromXml(doc.DocumentElement);
        }

        public void LoadFromXml(XmlElement xml)
        {
            Root = new AppTreeTplNode();
            Root.LoadFromXml(xml.FindElement("Tree"));
            if (xml.FindElement("Database") != null)
            {
                Database = (IDatabaseSource)DatabaseSourceAddonType.Instance.LoadAddon(xml.FindElement("Database"));
            }
        }

        public static Application[] GetAppsForDb(IDatabaseSource db)
        {
            var res = new List<Application>();
            if (db.Settings == null) return res.ToArray();
            var page = db.Settings.PageByName("app") as ApplicationSettings;
            if (page != null && !page.AppList.IsEmpty())
            {
                foreach (string apps in page.AppList.Split('\n'))
                {
                    string appid = apps.Trim();
                    if (!Cache.ContainsKey(appid))
                    {
                        try
                        {
                            var app = new Application();
                            app.LoadFromFile(Path.Combine(Core.AppsDirectory, appid + ".app"));
                            Cache[appid] = app;
                        }
                        catch
                        {
                            Cache[appid] = null;
                        }
                    }
                    if (Cache[appid] != null) res.Add(Cache[appid]);
                }
            }
            return res.ToArray();
        }
    }
}
