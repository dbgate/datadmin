using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.Windows.Forms;
using System.Data.Common;
using System.IO;
using System.Xml;
using System.Data;

namespace Plugin.textio
{
    public class WriterFileInfo
    {
        [XmlElem]
        public string Sql { get; set; }
        [XmlElem]
        public string FileNameTemplate { get; set; }
        public WriterFileContentBase Content { get; set; }

        public WriterFileInfo()
        {
            Sql = "";
            FileNameTemplate = "";
            Content = new TemplateFileContent();
        }

        public WriterFileInfo(XmlElement xml)
        {
            this.LoadPropertiesCore(xml);
            Content = WriterFileContentBase.Load(xml.FindElement("Content"));
        }

        private void ProcessRecord(IDatabaseSource db, IBedRecord record)
        {
            string filename = FileNameTemplate;
            Dictionary<string, string> vars = new Dictionary<string, string>();
            Dictionary<string, object> extnames = new Dictionary<string, object>();
            for (int fi = 0; fi < record.FieldCount; fi++)
            {
                vars["#" + record.GetName(fi).ToUpper() + "#"] = record.GetValue(fi).SafeToString();
                extnames["_" + record.GetName(fi).ToUpper() + "_"] = record.GetValue(fi).SafeToString();
            }
            filename = filename.ReplaceAll(vars);
            WriteContent(db, filename, vars, extnames);
        }

        internal void DoRun(IDatabaseSource db)
        {
            if (!Sql.IsEmpty())
            {
                // run SQL and export for each file
                using (DbCommand cmd = db.Connection.SystemConnection.CreateCommand())
                {
                    cmd.CommandText = Sql;
                    IBedReader reader = db.GetAnyDDA().AdaptReader(cmd.ExecuteReader());
                    reader.RunForEachRecordAndDispose(db.Connection.Dialect.DialectCaps.MARS, (rec, index) => ProcessRecord(db, rec));
                }
            }
            else
            {
                WriteContent(db, FileNameTemplate, null, null);
            }
        }

        private void WriteContent(IDatabaseSource db, string filename, Dictionary<string, string> vars, Dictionary<string, object> extnames)
        {
            try { Directory.CreateDirectory(Path.GetDirectoryName(filename)); }
            catch { }
            Content.WriteContent(filename, db, vars, extnames);
        }

        public override string ToString()
        {
            if (FileNameTemplate.IsEmpty()) return "???";
            return FileNameTemplate;
        }

        public void Save(XmlElement xml)
        {
            this.SavePropertiesCore(xml);
            Content.Save(xml.AddChild("Content"));
        }
    }
}
