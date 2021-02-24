using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.ComponentModel;
using System.Windows.Forms;
using System.IO;
using System.Drawing;

namespace DatAdmin
{
    public class WidgetInfo
    {
        [XmlSubElem]
        public IWidget Widget { get; set; }
        [XmlElem]
        public string PersistString { get; set; }

        public Action GetOpenWidgetCallback(DashboardFrame frame)
        {
            return () => frame.AddWidget(Widget, WeifenLuo.WinFormsUI.Docking.DockState.Document, PersistString);
        }
    }

    public class DockingDesign
    {
        XmlDocument m_doc = XmlTool.CreateDocument("Design");

        public void LoadFromXml(XmlElement xml)
        {
            Clear();
            foreach (XmlElement x in xml.SelectNodes("Widget|Layout"))
            {
                m_doc.DocumentElement.AppendChild(m_doc.ImportNode(x, true));
            }
        }

        public void SaveToXml(XmlElement xml)
        {
            foreach (XmlElement x in m_doc.DocumentElement.SelectNodes("Widget|Layout"))
            {
                xml.AppendChild(xml.OwnerDocument.ImportNode(x, true));
            }
        }

        public void Clear()
        {
            m_doc = XmlTool.CreateDocument("Design");
        }

        public XmlElement _Root { get { return m_doc.DocumentElement; } }

        public void LoadFromFile(string file)
        {
            var doc = new XmlDocument();
            doc.Load(file);
            LoadFromXml(doc.DocumentElement);
        }

        public void SaveToFile(string file)
        {
            var doc = XmlTool.CreateDocument("DockingLayout");
            SaveToXml(doc.DocumentElement);
            doc.Save(file);
        }

        public List<WidgetInfo> GetMissingWidgets(DockingDesign originalDesign)
        {
            var res = new List<WidgetInfo>();
            foreach (XmlElement widget in originalDesign._Root.SelectNodes("Widget"))
            {
                string ps = widget.FindElement("PersistString").InnerText;
                var mywidget = _Root.SelectSingleNode("Widget[PersistString='" + ps + "']") as XmlElement;
                if (mywidget == null)
                {
                    var item = new WidgetInfo();
                    try
                    {
                        item.LoadProperties(widget);
                        res.Add(item);
                    }
                    catch
                    {
                        continue;
                    }
                }
            }
            return res;
        }
    }

    [Dashboard(Name = "dockpanel")]
    public class DockPanelDashboard : DashboardBase, IFileBasedAddonInstance, ICustomPropertyPage
    {
        internal DockingDesign m_design = new DockingDesign();
        //bool m_enableDesign = true;
        int m_priority = 0;
        DashboardFrame m_designFrame;

        public string AddonFileName { get; set; }

        [XmlSubElem]
        public ObjectFilterBase Filter { get; set; }

        public DockPanelDashboard() { }

        public DashboardFrame DesignFrame { get { return m_designFrame; } }

        public DockPanelDashboard(string file)
        {
            AddonFileName = file;
        }
        public override string ToString()
        {
            return Path.GetFileNameWithoutExtension(AddonFileName);
        }
        public override Control CreateControl(DashboardInstanceParams pars)
        {
            var frame = new DashboardFrame(this, pars);
            frame.LoadLayoutOrOverride(m_design);
            //frame.EnableDesign(m_enableDesign);
            return frame;
        }

        //protected override void SetSelectedObject(AppObject obj)
        //{
        //    m_selectedObject = obj;
        //    if (m_frame != null) m_frame.SetSelectedObject(obj);
        //}

        public override bool SuitableFor(AppObject appobj)
        {
            var props = appobj.GetAppObjectProperties();
            return Filter.Accept(props);
        }

        #region ICustomPropertyPage Members

        public Control CreatePropertyPage()
        {
            return new DashboardEditorFrame(this);
        }

        #endregion

        [Browsable(false)]
        [XmlElem]
        public override int Priority { get { return m_priority; } set { m_priority = value; } }

        public override void SaveToXml(XmlElement xml)
        {
            base.SaveToXml(xml);
            m_design.SaveToXml(xml);
        }

        public override void LoadFromXml(XmlElement xml)
        {
            base.LoadFromXml(xml);
            m_design.LoadFromXml(xml);
        }

        public override DashboardCaps Caps
        {
            get
            {
                return new DashboardCaps
                {
                    ShowNodeToolbar = true
                };
            }
        }

        public void SaveToFile()
        {
            this.RedirectToCfgDirectory();
            SaveToFile(AddonFileName);
        }

        public void SetDesignFrame(DashboardFrame win)
        {
            if (!CustomDashboardsFeature.Allowed) return;
            if (win == null)
            {
                if (m_designFrame != null) m_designFrame.SetEnableDesignFlag(false);
                m_designFrame = null;
                return;
            }
            if (m_designFrame != null && m_designFrame != win)
            {
                throw new DashboardAllreadyDesignedError(this);
            }
            m_designFrame = win;
            m_designFrame.SetEnableDesignFlag(true);
        }

        public void OpenAsNewWindow(AppObject appobj)
        {
            string winid = appobj.GetFileFriendlySignature() + "#dashboard#" + IOTool.NormalizePath(AddonFileName);
            if (MainWindow.Instance.HasContent(winid))
            {
                MainWindow.Instance.ActivateContent(winid);
                return;
            }
            var pars = new DashboardInstanceParams { LayoutName = appobj.GetFileFriendlySignature() };
            var win = CreateControl(pars) as DashboardFrame;
            if (win == null) return;
            win.SetSelectedObject(appobj);
            win.WinId = winid;
            MainWindow.Instance.OpenContent(win);
        }

        public Action OpenAsNewWindowDelegate(AppObject appobj)
        {
            return () => OpenAsNewWindow(appobj);
        }

        public string PrivateLayoutDirectory
        {
            get
            {
                string dir = Path.Combine(Core.DashboardLayoutsDirectory, Path.GetFileNameWithoutExtension(AddonFileName));
                if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
                return dir;
            }
        }

        public void ClearPrivateLayouts()
        {
            foreach (string file in Directory.GetFiles(PrivateLayoutDirectory))
            {
                File.Delete(file);
            }
        }
    }

    [Favorite(Name = "opendashboard", Title = "Open dashboard")]
    public class OpenDashboardFavorite : FavoriteBase
    {
        [XmlSubElem]
        public AppObject RelatedObject { get; set; }

        [XmlElem]
        public string DashboardFile { get; set; }

        public override string Description
        {
            get { return "s_open_dashboard"; }
        }

        public override Bitmap Image
        {
            get { return CoreIcons.dashboard; }
        }

        public override void Open()
        {
            var das = DashboardManager.Instance.FindDashboard(DashboardFile);
            if (das != null)
            {
                das.OpenAsNewWindow(RelatedObject);
            }
        }
    }
}
