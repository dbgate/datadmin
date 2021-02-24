using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing.Design;
using System.IO;

namespace DatAdmin
{
    //public class FileDatabaseWriterFileNameEditor : FileNameEditorBase
    //{
    //    protected override void GetDialogProps(ITypeDescriptorContext context, out FileDialogType dialogType, out string filter)
    //    {
    //        var writer = (FileDatabaseWriter)context.Instance;
    //        dialogType = FileDialogType.Save;
    //        filter = writer.Filter;
    //    }
    //}

    [DefaultPropertyTab("s_details", TabWeight = 1)]
    public abstract class FileDatabaseWriter : DatabaseWriterBase, IExtendedFileNameHolderInfo
    {
        IFilePlace m_filePlace = new FilePlaceFileSystem();

        [Browsable(false)]
        [TabbedEditor(typeof(FileNameEditorFrame), TabWeight = 10)]
        public IFilePlace FilePlace
        {
            get { return m_filePlace; }
            set
            {
                m_filePlace = value;
                m_filePlace.SetFileHolderInfo(this);
            }
        }

        [Browsable(false)]
        [XmlElem("File")]
        public string XmlFileName
        {
            get { return m_filePlace.GetVirtualFile(); }
            set
            {
                m_filePlace = FilePlaceAddonType.PlaceFromVirtualFile(value, this);
                m_filePlace.SetFileHolderInfo(this);
            }
        }

        [Browsable(false)]
        public override bool ConfigurationNeeded
        {
            get { return true; }
        }

        public override void CheckConfiguration(IDatabaseSource source)
        {
            FilePlace.CheckOutput();
        }

        [Browsable(false)]
        public virtual string Filter
        {
            get
            {
                return String.Format("{0} {1} (*.{2})|*.{2}", FileExtension.ToUpper(), Texts.Get("s_files"), FileExtension.ToLower());
            }
        }
        [Browsable(false)]
        public abstract string FileExtension { get; }

        public override string ToString()
        {
            return FilePlace.ToString();
        }

        public override void ProcessFailed()
        {
            CloseConnection();
            ProgressInfo.Info(Texts.Get("s_deleting_target$file", "file", FilePlace));
            try
            {
                FilePlace.DeleteFile();
            }
            catch (Exception err)
            {
                ProgressInfo.LogError(err);
            }
        }

        protected string GetWorkingFileName()
        {
            return m_filePlace.GetWorkingFileName();
        }

        protected void FinalizeFileName()
        {
            m_filePlace.FinalizeFileName();
        }

        #region IExtendedFileNameHolderInfo Members

        [Browsable(false)]
        public bool DirectionIsSave
        {
            get { return true; }
        }

        [Browsable(false)]
        public AppObject RelatedObject { get { return null; } }

        [Browsable(false)]
        public IPhysicalConnectionFactory RelatedConnection { get { return null; } }

        [Browsable(false)]
        public string RelatedDatabase { get { return null; } }

        #endregion

        public override void LoadFromXml(System.Xml.XmlElement xml)
        {
            base.LoadFromXml(xml);
            // backward compatibility
            if (xml.HasAttribute("filename")) XmlFileName = xml.GetAttribute("filename");
        }
    }
}
