using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.Windows.Forms;
using System.Data.Common;
using System.IO;
using System.Xml;

namespace Plugin.textio
{
    public abstract class WriterFileContentBase
    {
        public abstract void WriteContent(string filename, IDatabaseSource db, Dictionary<string, string> vars, Dictionary<string, object> extnames);
        public abstract void Save(XmlElement xml);

        public static WriterFileContentBase Load(XmlElement xml)
        {
            switch (xml.GetAttribute("type"))
            {
                case "python": return new PythonFileContent(xml);
                case "template": return new TemplateFileContent(xml);
                case "blob": return new BlobFileContent(xml);
                case "datastore": return new DataStoreFileContent(xml);
            }
            throw new InternalError("DAE-00059 Unknown file content:" + xml.GetAttribute("type"));
        }
    }

    public abstract class TextWriterContentBase : WriterFileContentBase
    {
        [XmlElem]
        public string Code { get; set; }
        public override void WriteContent(string filename, IDatabaseSource db, Dictionary<string, string> vars, Dictionary<string, object> extnames)
        {
            RunTextWriterDelegate run = TextWriterCompiler.CompileScript(Code, Language);
            using (FileStream fw = new FileStream(filename, FileMode.Create))
            {
                LanguageQueryFunc func = sql =>
                {
                    var cmd = db.Connection.SystemConnection.CreateCommand();
                    cmd.CommandText = sql;
                    return new CommandDataReader(cmd.ExecuteReader(), cmd);
                };
                extnames["query"] = func;
                extnames["dbcontext"] = db;
                run(fw, extnames, null);
            }
        }
        protected abstract TextGeneratorLanguage Language { get; }
    }

    public class TemplateFileContent : TextWriterContentBase
    {
        protected override TextGeneratorLanguage Language
        {
            get { return TextGeneratorLanguage.Template; }
        }
        public override void Save(XmlElement xml)
        {
            xml.SetAttribute("type", "template");
            this.SavePropertiesCore(xml);
        }
        public TemplateFileContent() { }
        public TemplateFileContent(XmlElement xml)
        {
            this.LoadPropertiesCore(xml);
        }
    }

    public class PythonFileContent : TextWriterContentBase
    {
        protected override TextGeneratorLanguage Language
        {
            get { return TextGeneratorLanguage.Python; }
        }
        public override void Save(XmlElement xml)
        {
            xml.SetAttribute("type", "python");
            this.SavePropertiesCore(xml);
        }
        public PythonFileContent() { }
        public PythonFileContent(XmlElement xml)
        {
            this.LoadPropertiesCore(xml);
        }
    }

    public class BlobFileContent : WriterFileContentBase
    {
        [XmlElem]
        public string Sql { get; set; }

        public BlobFileContent()
        {
            Sql = "";
        }
        public BlobFileContent(XmlElement xml)
        {
            this.LoadPropertiesCore(xml);
        }
        public override void Save(XmlElement xml)
        {
            xml.SetAttribute("type", "blob");
            this.SavePropertiesCore(xml);
        }
        public override void WriteContent(string filename, IDatabaseSource db, Dictionary<string, string> vars, Dictionary<string, object> extnames)
        {
            using (DbCommand cmd = db.Connection.SystemConnection.CreateCommand())
            {
                cmd.CommandText = Sql.ReplaceAll(vars);
                object val = cmd.ExecuteScalar();
                if (val is byte[])
                {
                    byte[] b = (byte[])val;
                    using (FileStream fw = new FileStream(filename, FileMode.Create))
                    {
                        fw.Write(b, 0, b.Length);
                    }
                }
                else
                {
                    using (StreamWriter sw = new StreamWriter(filename))
                    {
                        sw.Write(val.SafeToString());
                    }
                }
            }
        }
    }

    public class DataStoreFileContent : WriterFileContentBase
    {
        [XmlElem]
        public string Sql { get; set; }

        public ITabularDataStore DataStore { get; set; }

        public DataStoreFileContent()
        {
            Sql = "";
        }

        public DataStoreFileContent(XmlElement xml)
        {
            var ds = xml.FindElement("DataStore");
            this.LoadPropertiesCore(xml);
            if (ds != null) DataStore = (ITabularDataStore)TabularDataStoreAddonType.Instance.LoadAddon(ds);
        }
        public override void Save(XmlElement xml)
        {
            xml.SetAttribute("type", "datastore");
            this.SavePropertiesCore(xml);
            if (DataStore != null) DataStore.SaveToXml(xml.AddChild("DataStore"));
        }
        public override void WriteContent(string filename, IDatabaseSource db, Dictionary<string, string> vars, Dictionary<string, object> extnames)
        {
            var fw = DataStore as ITabularDataOuputStream;
            if (fw == null) throw new InternalError(String.Format("DAE-00060 Cannot write data to {0}", DataStore));

            using (StreamWriter sw = new StreamWriter(filename))
            {
                using (DbCommand cmd = db.Connection.SystemConnection.CreateCommand())
                {
                    cmd.CommandText = Sql.ReplaceAll(vars);
                    using (IBedReader reader = db.GetAnyDDA().AdaptReader(cmd.ExecuteReader()))
                    {
                        ITableStructure table = reader.Structure;
                        //Path.GetFileNameWithoutExtension(filename));
                        object manager = null;
                        fw.WriteStart(sw, table, ref manager);
                        int index = 0;
                        while (reader.Read())
                        {
                            fw.WriteRecord(sw, table, reader, index, manager);
                            index++;
                        }
                        fw.WriteEnd(sw, table, manager);
                    }
                }
            }
        }
    }
}
