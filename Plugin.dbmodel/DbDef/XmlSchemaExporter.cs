using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using DatAdmin;
using System.Xml;

namespace Plugin.dbmodel
{
    [DatabaseWriter(Name = "xsd_writer", Title = "s_xml_schema", Description = "s_xsd_writer_desc", SupportsCreateTemplate = false)]
    public class XmlSchemaExporter : FileDatabaseWriter
    {
        public override string FileExtension
        {
            get { return ".xsd"; }
        }

        [XmlElem]
        [DatAdmin.DisplayName("s_schema_id")]
        public string SchemaId { get; set; }

        [XmlElem]
        [DatAdmin.DisplayName("s_target_namespace")]
        public string TargetNamespace { get; set; }

        public override void WriteStructureAfterData(IDatabaseStructure db)
        {
            const string XSNS = "http://www.w3.org/2001/XMLSchema";
            using (XmlWriter xw = XmlWriter.Create(GetWorkingFileName(), new XmlWriterSettings { Indent = true }))
            {
                xw.WriteStartDocument();
                xw.WriteStartElement("xs", "schema", XSNS);
                xw.WriteStartElement("element", XSNS);
                xw.WriteAttributeString("name", SchemaId);
                xw.WriteStartElement("complexType", XSNS);
                xw.WriteStartElement("choice", XSNS);
                xw.WriteAttributeString("minOccurs", "0");
                xw.WriteAttributeString("maxOccurs", "unbounded");
                foreach (var tbl in db.Tables)
                {
                    xw.WriteStartElement("element", XSNS);
                    xw.WriteAttributeString("name", tbl.FullName.Name);
                    xw.WriteStartElement("complexType", XSNS);
                    xw.WriteStartElement("sequence", XSNS);
                    foreach (var col in tbl.Columns)
                    {
                        xw.WriteStartElement("element", XSNS);
                        xw.WriteAttributeString("name", col.ColumnName);
                        if (col.IsNullable) xw.WriteAttributeString("minOccurs", "0");
                        xw.WriteAttributeString("type", col.DataType.XsdType);
                        xw.WriteEndElement(); // element
                    }
                    xw.WriteEndElement(); // sequence
                    xw.WriteEndElement(); // complexType
                    xw.WriteEndElement(); // element
                }
                xw.WriteEndElement(); // choice
                xw.WriteEndElement(); // complexType
                xw.WriteEndElement(); // element
                xw.WriteEndElement(); // schema
                xw.WriteEndDocument();
            }
        }

        public override void CloseConnection()
        {
            base.CloseConnection();
            FinalizeFileName();
        }
    }
}
