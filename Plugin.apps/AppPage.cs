using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.Xml;
using System.Windows.Forms;
using System.ComponentModel;

namespace Plugin.apps
{
    public class AppPage
    {
        string m_title;

        [DatAdmin.DisplayName("s_name")]
        [XmlElem]
        public string Title
        {
            get { return m_title; }
            set { m_title = value; }
        }

        [Browsable(false)]
        [XmlElem]
        public int SavedWidth { get; set; }

        [Browsable(false)]
        [XmlElem]
        public int SavedHeight { get; set; }

        public List<AppWidget> Widgets = new List<AppWidget>();

        public void SaveToXml(XmlElement xml)
        {
            this.SavePropertiesCore(xml);
            foreach (var widget in Widgets)
            {
                widget.SaveToXml(xml.AddChild("Widget"));
            }
        }

        public void LoadFromXml(XmlElement xml)
        {
            this.LoadPropertiesCore(xml);
            foreach (XmlElement x in xml.SelectNodes("Widget"))
            {
                var widget = (AppWidget)ApplicationWidgetAddonType.Instance.LoadAddon(x);
                Widgets.Add(widget);
            }
        }

        public Control CreateControl(IDatabaseSource db, AppEnv env)
        {
            return new AppPageInstance(this, db, env);
        }

        public AppWidget FindWidget(string name)
        {
            foreach (var widget in Widgets) if (widget.Name == name) return widget;
            return null;
        }
    }

    public class AppPageObjectView : WidgetBase
    {
        AppPage m_page;
        AppEnv m_env = new AppEnv();

        public AppPageObjectView(AppPage page)
        {
            m_page = page;
        }
        protected override WidgetBaseFrame CreateControl()
        {
            //var db = appobj.FindDatabaseConnection(connpack);
            //return m_page.CreateControl(db, m_env);
            return null;
        }
        public override Type GetControlType()
        {
            return typeof(AppPageInstance);
        }
        public override string PageTitle
        {
            get { return m_page.Title; }
        }
    }
}
