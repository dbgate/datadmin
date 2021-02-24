using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.IO;
using DatAdmin;

namespace Plugin.textio
{
    [DatabaseWriter(Name = "multitable", Title = "s_multiple_tables", Description = "s_multitable_export_desc", RequiredFeature = MultiTableExportFeature.Test)]
    public class MultiTableDatabaseWriter : DatabaseWriterBase, ICustomPropertyPage
    {
        public ITabularDataStore DataStore { get; set; }
        [XmlAttrib("filename")]
        public string FileNameTemplate { get; set; }

        public MultiTableDatabaseWriter()
        {
            FileNameTemplate = "#SCHEMA#-#TABLE#.dat";
        }

        public override void SaveToXml(XmlElement xml)
        {
            base.SaveToXml(xml);
            if (DataStore != null) DataStore.SaveToXml(xml.AddChild("DataStore"));
        }

        public override void LoadFromXml(XmlElement xml)
        {
            base.LoadFromXml(xml);
            var ds = xml.FindElement("DataStore");
            if (ds != null) DataStore = (ITabularDataStore)TabularDataStoreAddonType.Instance.LoadAddon(ds);
        }

        public override void FillTable(ITableStructure table, IDataQueue queue, TableCopyOptions opts)
        {
            ITableStructure ts = queue.GetRowFormat;
            string fn = FileNameTemplate;
            fn = fn.Replace("#TABLE#", table.FullName.Name);
            fn = fn.Replace("#SCHEMA#", table.FullName.Schema ?? "");
            string dir = Path.GetDirectoryName(fn);
            if (!Directory.Exists(dir)) Directory.CreateDirectory(dir);
            var api = DataStore.StreamApi;
            using (StreamWriter fw = new StreamWriter(fn))
            {
                object manager = null;
                api.WriteStart(fw, ts, ref manager);
                int index = 0;
                try
                {
                    while (!queue.IsEof)
                    {
                        var record = queue.GetRecord();
                        api.WriteRecord(fw, ts, record, index, manager);
                        index++;
                    }
                }
                finally
                {
                    queue.CloseReading();
                }
                api.WriteEnd(fw, ts, manager);
            }
        }

        #region ICustomPropertyPage Members

        public Control CreatePropertyPage()
        {
            return new MultiTableDatabaseWriterFrame(this);
        }

        #endregion

        public override DatabaseWriterCaps WriterCaps
        {
            get
            {
                return new DatabaseWriterCaps
                {
                    AllFlags = false,
                    AcceptData = true,
                    AcceptStructure = true,
                    MultipleSchema = true
                };
            }
        }
    }
}
