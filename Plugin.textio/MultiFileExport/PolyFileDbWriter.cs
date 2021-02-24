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
    [DatabaseWriter(Name = "multifile", Title = "s_multiple_files", Description = "s_multifile_export_desc", RequiredFeature = MultiFileExportFeature.Test)]
    public class PolyFileDbWriter : DatabaseWriterBase, ICustomPropertyPage
    {
        List<WriterFileInfo> m_files = new List<WriterFileInfo>();

        public override bool DirectCopy(IDatabaseSource source)
        {
            return true;
        }

        public override DatabaseWriterCaps WriterCaps
        {
            get
            {
                return new DatabaseWriterCaps
                {
                    AllFlags = false,
                };
            }
        }

        public override void RunDirectCopy(IDatabaseSource source, DatabaseCopyOptions copyOpts)
        {
            source.Connection.Invoke(() => DoRunDirectCopy(source));
        }

        private void DoRunDirectCopy(IDatabaseSource db)
        {
            db.Connection.SystemConnection.SafeChangeDatabase(db.DatabaseName);
            foreach (var file in m_files)
            {
                file.DoRun(db);
            }
        }

        public List<WriterFileInfo> Files { get { return m_files; } }

        #region ICustomPropertyPage Members

        public Control CreatePropertyPage()
        {
            return new PolyFileDbWriterFrame(this);
        }

        #endregion

        public override void SaveToXml(XmlElement xml)
        {
            base.SaveToXml(xml);
            foreach (var file in m_files)
            {
                file.Save(xml.AddChild("File"));
            }
        }

        public override void LoadFromXml(XmlElement xml)
        {
            base.LoadFromXml(xml);
            foreach (XmlElement x in xml.SelectNodes("File"))
            {
                m_files.Add(new WriterFileInfo(x));
            }
        }
    }
}

