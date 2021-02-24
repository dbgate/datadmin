using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Data;
using System.Xml;
using System.Xml.Serialization;
using System.ComponentModel;
using Ionic.Zip;

namespace DatAdmin
{
    [DatabaseWriter(Name = "dbk_writer", Title = "s_data_archive", Description = "s_dbk_desc", SupportsCreateTemplate = false)]
    public class DataArchiveWriter : FileDatabaseWriter
    {
        ZipOutputStream m_zip;

        [Browsable(false)]
        public override DatabaseWriterCaps WriterCaps
        {
            get
            {
                return new DatabaseWriterCaps
                {
                    AllFlags = false,
                    AcceptData = true,
                    AcceptStructure = true,
                    MultipleSchema = true,
                };
            }
        }

        public override void OpenConnection()
        {
            if (m_zip != null) return;
            m_zip = new ZipOutputStream(GetWorkingFileName());
            //m_zip = new ZipFile( new ZipOutputStream(new FileStream(GetWorkingFileName(), FileMode.Create));
            //m_zip.SetLevel(9);
        }

        public override void CloseConnection()
        {
            if (m_zip != null)
            {
                m_zip.Close();
                m_zip = null;
            }
            FinalizeFileName();
        }

        public override void WriteStructureAfterData(IDatabaseStructure db)
        {
            var index_e = m_zip.PutNextEntry("_index_.xml");
            var dbcopy = new DatabaseStructure(db);
            dbcopy.Save(m_zip);
        }

        public override void FillTable(ITableStructure table, IDataQueue queue, TableCopyOptions opts)
        {
            var name = new NameWithSchema(table.FullName.Schema, table.FullName.Name, false);
            var entry = m_zip.PutNextEntry(XmlTool.NormalizeIdentifier(name.ToString()) + ".drs");
            BedDataStats stats = new BedDataStats();
            BedTool.WriteQueueToStream(queue, m_zip, stats);
            ProgressInfo.LogMessage("s_export", LogLevel.Info, Texts.Get("s_exported$table$rows$bytes", "rows", stats.Rows, "bytes", stats.Bytes, "table", table.FullName));
            //ZipEntry entry = new ZipEntry(XmlTool.NormalizeIdentifier(table.FullName.ToString()) + ".xml");
            //m_zip.PutNextEntry(entry);
            //XmlTool.WriteQueueToXml(queue, m_zip, "DataSet", "DataRow");
        }

        [Browsable(false)]
        public override string FileExtension
        {
            get { return "dbk"; }
        }
    }
}
