using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace DatAdmin
{
    [Favorite(Name = "tabledata", Title = "Table data")]
    public class TableDataFavorite : FavoriteBase
    {
        public XmlElement SerializedState;

        public override void SaveToXml(XmlElement xml)
        {
            base.SaveToXml(xml);
            xml.AppendChild(xml.OwnerDocument.ImportNode(SerializedState, true));
        }

        public override void LoadFromXml(XmlElement xml)
        {
            base.LoadFromXml(xml);
            SerializedState = xml.FindElement("TableData");
        }

        public override System.Drawing.Bitmap Image
        {
            get { return CoreIcons.table_data; }
        }

        public override void Open()
        {
            MainWindow.Instance.OpenContent(new TableDataFrame(SerializedState));
        }

        public override string Description
        {
            get { return "s_table_data"; }
        }

        public override void DisplayProps(Action<string, string> display)
        {
            base.DisplayProps(display);
            var tx = SerializedState.SelectSingleNode("//Table") as XmlElement;
            if (tx != null) display("s_table", NameWithSchema.LoadFromXml(tx).ToString());
            var dbx = SerializedState.SelectSingleNode("Database") as XmlElement;
            if (dbx != null && dbx.HasAttribute("dbname")) display("s_database", dbx.GetAttribute("dbname"));
            var dsx = SerializedState.SelectSingleNode("DataSource") as XmlElement;
            if (dsx != null) display("s_server", dsx.InnerText);
        }
    }
}
