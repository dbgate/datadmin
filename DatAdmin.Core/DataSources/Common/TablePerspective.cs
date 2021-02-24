using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.IO;

namespace DatAdmin
{
    public class TablePerspectiveAddonType : AddonType
    {
        public override string Name
        {
            get { return "tblper"; }
        }

        public override Type InterfaceType
        {
            get { return null; }
        }

        public override Type RegisterAttributeType
        {
            get { return null; }
        }

        public override string FileExtension
        {
            get { return ".per"; }
        }

        public override string GetDirectory()
        {
            return Core.TablePerspectivesDirectory;
        }

        public override IAddonInstance LoadAddon(XmlElement xml)
        {
            var res = new TablePerspective();
            res.LoadFromXml(xml);
            return res;
        }

        public static TablePerspectiveAddonType Instance = new TablePerspectiveAddonType();
    }

    [ObjectFilter(Name = "table_perspective", Title = "Table perspective")]
    public class TablePerspectiveConditions : TableWithColumnsObjectFilter
    {
        public bool Accept(IPhysicalConnection conn, string dbname, NameWithSchema table, string[] columns)
        {
            var props = new Dictionary<string, string>();

            string srv = null;
            if (conn != null && conn.StoredConnection != null) srv = conn.StoredConnection.GetDataSource();
            props["server"] = srv;
            props["database"] = dbname;

            string schema = null, tbl = null;
            if (table != null)
            {
                schema = table.Schema;
                tbl = table.Name;
            }
            props["dbobjname"] = tbl;
            props["dbobjschema"] = schema;

            props["columns"] = columns.CreateDelimitedText("|");
            return base.Accept(props);
        }

        public override void GetItems(List<ObjectFilterItemBase> items)
        {
            base.GetItems(items);
            items.RemoveIf(it => it.PropertyName == "objtype");
        }

        //[XmlElem]
        //public bool ServerChecked { get; set; }
        //[XmlElem]
        //public string Server { get; set; }
        //[XmlElem]
        //public bool ServerRegex { get; set; }

        //[XmlElem]
        //public bool DatabaseChecked { get; set; }
        //[XmlElem]
        //public string Database { get; set; }
        //[XmlElem]
        //public bool DatabaseRegex { get; set; }

        //[XmlElem]
        //public bool SchemaChecked { get; set; }
        //[XmlElem]
        //public string Schema { get; set; }
        //[XmlElem]
        //public bool SchemaRegex { get; set; }

        //[XmlElem]
        //public bool TableChecked { get; set; }
        //[XmlElem]
        //public string Table { get; set; }
        //[XmlElem]
        //public bool TableRegex { get; set; }

        //[XmlElem]
        //public bool ColumnsChecked { get; set; }
        //public string[] Columns { get; set; }

        //public void SaveToXml(XmlElement xml)
        //{
        //    this.SavePropertiesCore(xml);
        //    if (Columns != null)
        //    {
        //        foreach (string col in Columns)
        //        {
        //            xml.AddChild("Column").InnerText = col;
        //        }
        //    }
        //}

        //public void LoadFromXml(XmlElement xml)
        //{
        //    this.LoadPropertiesCore(xml);
        //    var lst = new List<string>();
        //    foreach (XmlElement c in xml.SelectNodes("Column")) lst.Add(c.InnerText);
        //    Columns = lst.ToArray();
        //}

        //public bool Accept(IPhysicalConnection conn, string dbname, NameWithSchema table, string[] columns)
        //{
        //    string srv = null;
        //    if (conn != null && conn.StoredConnection != null) srv = conn.StoredConnection.GetDataSource();
        //    if (!AcceptItem(srv, ServerChecked, Server, ServerRegex)) return false;
        //    if (!AcceptItem(dbname, DatabaseChecked, Database, DatabaseRegex)) return false;
        //    string schema = null, tbl = null;
        //    if (table != null)
        //    {
        //        schema = table.Schema;
        //        tbl = table.Name;
        //    }
        //    if (!AcceptItem(schema, SchemaChecked, Schema, SchemaRegex)) return false;
        //    if (!AcceptItem(tbl, TableChecked, Table, TableRegex)) return false;
        //    if (ColumnsChecked)
        //    {
        //        if (columns == null || Columns == null) return false;
        //        foreach (string col in Columns)
        //        {
        //            bool found = false;
        //            foreach (string col2 in columns)
        //            {
        //                if (String.Compare(col, col2, true) != 0)
        //                {
        //                    found = true;
        //                    break;
        //                }
        //            }
        //            if (!found) return false;
        //        }
        //    }
        //    return true;
        //}

        //private static bool AcceptItem(string value, bool ischecked, string definedVal, bool isregex)
        //{
        //    definedVal = definedVal ?? "";
        //    if (!ischecked) return true;
        //    if (value.IsEmpty()) return definedVal.IsEmpty();
        //    if (isregex)
        //    {
        //        return Regex.Match(value, definedVal).Success;
        //    }
        //    else
        //    {
        //        return value.Trim() == definedVal.Trim();
        //    }
        //}
    }

    public class TablePerspective : IAddonInstance
    {
        public TablePerspectiveConditions Conditions { get; set; }
        public DmlfSelect Select = new DmlfSelect();

        public string FileName { get; set; }
        public string InMemoryName { get; set; }

        public ReferencesDockPanelDesign DockPanelDesign;

        public TablePerspective()
        {
            Initialize();
        }

        private void Initialize()
        {
            Conditions = new TablePerspectiveConditions();
        }

        public TablePerspective(XmlElement xml, string fn)
        {
            Initialize();
            FileName = fn;
            LoadFromXml(xml);
        }

        public void LoadFromXml(XmlElement xml)
        {
            LoadParts(xml, true, true, true);
        }

        public void LoadParts(XmlElement xml, bool select, bool conditions, bool dockpanel)
        {
            if (conditions)
            {
                Conditions = new TablePerspectiveConditions();
                Conditions.LoadFromXml(xml.FindElement("Conditions"));
            }
            if (select)
            {
                Select = new DmlfSelect();
                Select.LoadFromXml(xml.FindElement("Select"));
            }
            if (dockpanel && xml.FindElement("DockPanel") != null)
            {
                DockPanelDesign = new ReferencesDockPanelDesign();
                DockPanelDesign.LoadFromXml(xml.FindElement("DockPanel"));
            }
        }

        public void SaveToXml(XmlElement xml)
        {
            this.SavePropertiesCore(xml);
            Conditions.SaveToXml(xml.AddChild("Conditions"));
            Select.SaveToXml(xml.AddChild("Select"));
            if (DockPanelDesign != null) DockPanelDesign.SaveToXml(xml.AddChild("DockPanel"));
        }

        public static TablePerspective FromFile(string fn)
        {
            var doc = new XmlDocument();
            doc.Load(fn);
            return new TablePerspective(doc.DocumentElement, fn);
        }

        public void SaveToFile()
        {
            var doc = XmlTool.CreateDocument("TablePerspective");
            SaveToXml(doc.DocumentElement);
            doc.Save(FileName);
        }

        public override string ToString()
        {
            if (FileName != null) return Path.GetFileNameWithoutExtension(FileName);
            return InMemoryName;
        }

        public void LoadParts(TablePerspective per, bool select, bool conditions, bool dockpanel)
        {
            var doc = XmlTool.CreateDocument("Perspective");
            per.SaveToXml(doc.DocumentElement);
            LoadParts(doc.DocumentElement, select, conditions, dockpanel);
        }

        public AddonType AddonType
        {
            get { return TablePerspectiveAddonType.Instance; }
        }
    }

    public class ReferencesDockPanelDesign
    {
        XmlDocument m_layout;
        public class FrameDef
        {
            [XmlElem]
            public string SaveId { get; set; }
            [XmlElem]
            public string MasterSaveId { get; set; }
            [XmlSubElem]
            public ReferenceViewDefinition ReferenceDef { get; set; }

            public FrameDef()
            {
                ReferenceDef = new ReferenceViewDefinition();
            }
        }
        public List<FrameDef> Frames = new List<FrameDef>();

        public XmlElement LayoutXml { get { return m_layout.DocumentElement; } }

        public Dictionary<string, ContentFrame> CreateFrames(ContentFrame primary)
        {
            var res = new Dictionary<string, ContentFrame>();
            res["primary"] = primary;
            foreach (var fd in Frames)
            {
                var frame = new ReferencesTableDataFrame((TableDataFrame)res[fd.MasterSaveId], fd.ReferenceDef, null);
                res[fd.SaveId] = frame;
            }
            return res;
        }

        internal void _AddFrameDef(ReferencesTableDataFrame frame)
        {
            var fd = new FrameDef();
            Frames.Add(fd);
            fd.MasterSaveId = frame.MasterFrame.PersistString;
            fd.SaveId = frame.PersistString;
            fd.ReferenceDef = frame.RefDef;
        }

        internal void LoadLayoutFromPanel(DockPanelContentFrame frame)
        {
            m_layout = XmlTool.CreateDocument("Layout");
            frame.SaveLayout(m_layout.DocumentElement);
        }

        public void SaveToXml(XmlElement xml)
        {
            foreach (var fd in Frames)
            {
                fd.SaveProperties(xml.AddChild("Frame"));
            }
            if (m_layout != null) xml.AppendChild(xml.OwnerDocument.ImportNode(m_layout.DocumentElement, true));
        }

        public void LoadFromXml(XmlElement xml)
        {
            foreach (XmlElement fx in xml.SelectNodes("Frame"))
            {
                var fd = new FrameDef();
                fd.LoadProperties(fx);
                Frames.Add(fd);
            }
            var layx = xml.FindElement("Layout");
            if (layx != null)
            {
                m_layout = new XmlDocument();
                m_layout.AppendChild(m_layout.ImportNode(layx, true));
            }
        }
    }
}
