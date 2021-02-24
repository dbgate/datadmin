using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.Drawing.Design;
using System.Windows.Forms;
using System.IO;
using System.Xml;

namespace DatAdmin
{
    [TabularDataStore(Name = "xml_dataset", Title = "s_xml_dataset_file", Description = "s_xml_dataset_texp_desc")]
    public class XmlDataSetDataStore : FileDataStoreBase
    {
        DataTable m_loadedData = null;

        protected override ITableStructure DoGetRowFormat()
        {
            WantLoadedData();
            return m_loadedData.Columns.GetTableStructure(null);
        }

        private void WantLoadedData()
        {
            if (m_loadedData == null)
            {
                m_loadedData = new DataTable();
                using (XmlReader xr = new XmlTextReader(GetWorkingFileName()))
                {
                    LoadSchema(xr);
                }
            }
        }

        private void LoadSchema(XmlReader xr)
        {
            xr.MoveToContent();
            xr.Read();
            xr.MoveToContent();
            m_loadedData.ReadXmlSchema(xr);
        }

        protected override void DoRead(IDataQueue queue)
        {
            using (XmlReader xr = XmlReader.Create(GetWorkingFileName(), new XmlReaderSettings { CheckCharacters = false }))
            {
                m_loadedData = XmlDataTool.ReadXmlToQueue(xr, queue, "DataRow");
            }
            FinalizeBulkCopy();
        }

        protected override void DoWrite(IDataQueue queue)
        {
            using (FileStream fw = new FileStream(GetWorkingFileName(), FileMode.Create))
            {
                XmlDataTool.WriteQueueToXml(queue, fw, "DataSet", "DataRow");
            }
            FinalizeBulkCopy();
        }

        public override void ClearLoadCaches()
        {
            base.ClearLoadCaches();
            m_loadedData = null;
        }

        public override string FileExtension
        {
            get { return "xml"; }
        }
    }

    public enum XmlDataStorageType { Attribute, ColumnNamedElement, InvariantNamedElement };

    [TabularDataStore(Name = "generic_xml", Title = "s_generic_xml_file", Description = "s_generic_xml_texp_desc")]
    public class GenericXmlDataStore : FileWithFormatDataStoreBase, ITabularDataOuputStream
    {
        XmlDataStorageType m_storageType;

        [DisplayName("s_storage_type")]
        [XmlAttrib("storage")]
        public XmlDataStorageType StorageType
        {
            get { return m_storageType; }
            set { m_storageType = value; }
        }

        string m_rootElementName = "root";

        [DisplayName("s_root_element_name")]
        [XmlElem]
        public string RootElementName
        {
            get { return m_rootElementName; }
            set { m_rootElementName = value; }
        }
        string m_rowElementName = "row";

        [DisplayName("s_row_element_name")]
        [XmlElem]
        public string RowElementName
        {
            get { return m_rowElementName; }
            set { m_rowElementName = value; }
        }
        string m_cellElementName = "cell";

        [DisplayName("s_cell_element_name")]
        [XmlElem]
        public string CellElementName
        {
            get { return m_cellElementName; }
            set { m_cellElementName = value; }
        }
        string m_columnAttributeName = "column";

        [DisplayName("s_column_attribute_name")]
        [XmlElem]
        public string ColumnAttributeName
        {
            get { return m_columnAttributeName; }
            set { m_columnAttributeName = value; }
        }

        List<string> m_columnNames = null;

        protected override ITableStructure DoGetRowFormat()
        {
            if (m_columnNames == null)
            {
                m_columnNames = new List<string>();

                using (XmlReader xr = new XmlTextReader(GetWorkingFileName()))
                {
                    xr.MoveToContent();
                    if (xr.Name != m_rootElementName) Logging.Warning("Root element has different name");
                    xr.Read();
                    while (xr.NodeType == XmlNodeType.Element)
                    {
                        if (xr.Name == m_rowElementName)
                        {
                            switch (m_storageType)
                            {
                                case XmlDataStorageType.Attribute:
                                    for (int i = 0; i < xr.AttributeCount; i++)
                                    {
                                        xr.MoveToAttribute(i);
                                        string name = xr.Name;
                                        if (!m_columnNames.Contains(name)) m_columnNames.Add(name);
                                    }
                                    xr.MoveToElement();
                                    xr.Read();
                                    break;
                                case XmlDataStorageType.ColumnNamedElement:
                                    xr.Read();
                                    xr.MoveToContent();

                                    while (xr.NodeType == XmlNodeType.Element)
                                    {
                                        string name = xr.Name;
                                        xr.Skip();
                                        if (!m_columnNames.Contains(name)) m_columnNames.Add(name);
                                    }
                                    xr.MoveToContent();
                                    if (xr.NodeType == XmlNodeType.EndElement) xr.Read();
                                    break;
                                case XmlDataStorageType.InvariantNamedElement:
                                    xr.Read();
                                    xr.MoveToContent();

                                    while (xr.NodeType == XmlNodeType.Element)
                                    {
                                        string name = xr.GetAttribute(m_columnAttributeName);
                                        xr.Skip();
                                        if (!m_columnNames.Contains(name)) m_columnNames.Add(name);
                                    }
                                    xr.MoveToContent();
                                    if (xr.NodeType == XmlNodeType.EndElement) xr.Read();
                                    break;
                            }
                        }
                        else
                        {
                            xr.Skip();
                        }
                    }
                }
            }
            return CreateStructure();
        }

        private TableStructure CreateStructure()
        {
            TableStructure ts = new TableStructure();
            foreach (string colname in m_columnNames)
            {
                ColumnStructure col = new ColumnStructure();
                col.ColumnName = colname;
                col.DataType = new DbTypeString();
                ts._Columns.Add(col);
            }
            return ts;
        }

        protected override void DoRead(IDataQueue queue)
        {
            try
            {
                TableStructure s = CreateStructure();

                Dictionary<string, int> colPos = new Dictionary<string, int>();
                for (int i = 0; i < m_columnNames.Count; i++)
                {
                    colPos[m_columnNames[i]] = i;
                    colPos[XmlTool.NormalizeIdentifier(m_columnNames[i])] = i;
                }

                using (XmlReader xr = new XmlTextReader(GetWorkingFileName()))
                {
                    xr.MoveToContent();
                    if (xr.Name != m_rootElementName) Logging.Warning("Root element has different name");
                    xr.Read();
                    while (xr.NodeType == XmlNodeType.Element)
                    {
                        // process one row

                        object[] values = new object[m_columnNames.Count];
                        for (int i = 0; i < values.Length; i++) values[i] = DBNull.Value;


                        if (xr.Name == m_rowElementName)
                        {
                            switch (m_storageType)
                            {
                                case XmlDataStorageType.Attribute:
                                    for (int i = 0; i < xr.AttributeCount; i++)
                                    {
                                        xr.MoveToAttribute(i);
                                        string name = xr.Name;
                                        if (colPos.ContainsKey(name)) values[colPos[name]] = xr.Value;
                                    }
                                    xr.MoveToElement();
                                    xr.Read();
                                    break;
                                case XmlDataStorageType.ColumnNamedElement:
                                    xr.Read();
                                    xr.MoveToContent();
                                    while (xr.NodeType == XmlNodeType.Element)
                                    {
                                        string name = xr.Name;
                                        string value = xr.ReadElementContentAs(typeof(string), null).ToString();
                                        if (colPos.ContainsKey(name)) values[colPos[name]] = value;
                                    }
                                    xr.MoveToContent();
                                    if (xr.NodeType == XmlNodeType.EndElement) xr.Read();
                                    break;
                                case XmlDataStorageType.InvariantNamedElement:
                                    xr.Read();
                                    xr.MoveToContent();
                                    while (xr.NodeType == XmlNodeType.Element)
                                    {
                                        string name = xr.GetAttribute(m_columnAttributeName);
                                        string value = xr.ReadElementContentAs(typeof(string), null).ToString();
                                        if (colPos.ContainsKey(name)) values[colPos[name]] = value;
                                    }
                                    xr.MoveToContent();
                                    if (xr.NodeType == XmlNodeType.EndElement) xr.Read();
                                    break;
                            }
                        }
                        else
                        {
                            xr.Skip();
                        }

                        queue.PutRecord(new ArrayDataRecord(s, values));
                    }
                }
            }
            finally
            {
                queue.PutEof();
            }
            FinalizeBulkCopy();
        }

        //protected override void DoWrite(IDataQueue queue)
        //{
        //    using (XmlTextWriter xw = new XmlTextWriter(m_filename, System.Text.Encoding.UTF8))
        //    {
        //        xw.WriteStartDocument();
        //        xw.WriteStartElement(m_rootElementName);
        //        List<string> ids = new List<string>();
        //        List<IColumnStructure> cols=new List<IColumnStructure>();
        //        foreach (IColumnStructure col in queue.GetRowFormat.Columns) cols.Add(col);
        //        foreach (IColumnStructure col in cols) ids.Add(XmlTool.NormalizeIdentifier(col.ColumnName));
        //        while (!queue.IsEof)
        //        {
        //            IDataRecord row = queue.GetRecord();
        //            xw.WriteStartElement(m_rowElementName);

        //            for (int i = 0; i < row.FieldCount; i++)
        //            {
        //                if (!row.IsDBNull(i))
        //                {
        //                    switch (m_storageType)
        //                    {
        //                        case XmlDataStorageType.Attribute:
        //                            xw.WriteAttributeString(ids[i], XmlTool.ObjectToString(row[i]));
        //                            break;
        //                        case XmlDataStorageType.ColumnNamedElement:
        //                            xw.WriteElementString(ids[i], XmlTool.ObjectToString(row[i]));
        //                            break;
        //                        case XmlDataStorageType.InvariantNamedElement:
        //                            xw.WriteStartElement(m_cellElementName);
        //                            xw.WriteAttributeString(m_columnAttributeName, cols[i].ColumnName);
        //                            xw.WriteValue(XmlTool.ObjectToString(row[i]));
        //                            xw.WriteEndElement();
        //                            break;
        //                    }
        //                }
        //            }
        //            xw.WriteEndElement();
        //        }
        //        xw.WriteEndElement();
        //        xw.WriteEndDocument();
        //        xw.Flush();
        //    }
        //}

        public override void ClearLoadCaches()
        {
            base.ClearLoadCaches();
            m_columnNames = null;
        }

        public override string FileExtension
        {
            get { return "xml"; }
        }

        #region ITabularDataOuputStream Members

        public void WriteStart(StreamWriter fw, ITableStructure table, ref object manager)
        {
            Manager mgr = new Manager();
            XmlTextWriter xw = new XmlTextWriter(fw);
            mgr.xw = xw;
            mgr.formatter = new BedValueFormatter(FormatSettings);
            xw.WriteStartDocument();
            xw.WriteStartElement(m_rootElementName);
            foreach (IColumnStructure col in table.Columns) mgr.cols.Add(col);
            foreach (IColumnStructure col in mgr.cols) mgr.ids.Add(XmlTool.NormalizeIdentifier(col.ColumnName));
            manager = mgr;
        }

        public void WriteRecord(StreamWriter fw, ITableStructure table, IBedRecord record, int index, object manager)
        {
            Manager mgr = (Manager)manager;
            XmlTextWriter xw = mgr.xw; ;
            xw.WriteStartElement(m_rowElementName);

            for (int i = 0; i < record.FieldCount; i++)
            {
                record.ReadValue(i);
                var type = record.GetFieldType();

                if (type == TypeStorage.Null) continue;
                mgr.formatter.ReadFrom(record);
                switch (m_storageType)
                {
                    case XmlDataStorageType.Attribute:
                        xw.WriteAttributeString(mgr.ids[i], mgr.formatter.GetText());
                        break;
                    case XmlDataStorageType.ColumnNamedElement:
                        xw.WriteElementString(mgr.ids[i], mgr.formatter.GetText());
                        break;
                    case XmlDataStorageType.InvariantNamedElement:
                        xw.WriteStartElement(m_cellElementName);
                        xw.WriteAttributeString(m_columnAttributeName, mgr.cols[i].ColumnName);
                        xw.WriteValue(mgr.formatter.GetText());
                        xw.WriteEndElement();
                        break;
                }
            }
            xw.WriteEndElement();
        }

        public void WriteEnd(StreamWriter fw, ITableStructure table, object manager)
        {
            Manager mgr = (Manager)manager;
            XmlTextWriter xw = mgr.xw; ;
            xw.WriteEndElement();
            xw.WriteEndDocument();
            xw.Flush();
        }

        [Browsable(false)]
        public bool RequireSingleStream
        {
            get { return true; }
        }

        #endregion

        public override ITabularDataOuputStream StreamApi
        {
            get { return this; }
        }

        class Manager
        {
            internal XmlTextWriter xw;
            internal BedValueFormatter formatter;
            internal List<string> ids = new List<string>();
            internal List<IColumnStructure> cols = new List<IColumnStructure>();
        }
    }

}
