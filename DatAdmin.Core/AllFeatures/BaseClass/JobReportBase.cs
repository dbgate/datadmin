using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace DatAdmin
{
    public abstract class JobReportFactoryBase : IJobReportFactory
    {
        public JobCommand RelatedCommand { get; set; }
        public abstract IJobReportConfiguration CreateConfig();
    }

    [DefaultPropertyTab("s_details", TabWeight = 1)]
    public abstract class JobReportConfigurationBase : AddonBase, IJobReportConfiguration, IExtendedFileNameHolderInfo
    {
        public override AddonType AddonType
        {
            get { return JobReportConfigurationAddonType.Instance; }
        }

        #region IJobReportConfiguration Members

        public virtual IJobReportConfiguration Clone()
        {
            var res = (JobReportConfigurationBase)MemberwiseClone();
            res.FilePlace = FilePlaceAddonType.PlaceFromVirtualFile(FilePlace.GetVirtualFile());
            return res;
        }

        public abstract IJobReportProcessor CreateProcessor(JobCommand cmd);

        #endregion

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

        #region IExtendedFileNameHolderInfo Members

        [Browsable(false)]
        public virtual string Filter
        {
            get { return String.Format("*.{0}|*.{0}", FileExtension); }
        }

        [Browsable(false)]
        public abstract string FileExtension
        {
            get;
        }

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

        public override string ToString()
        {
            string res = FilePlace.ToString();
            if (res.IsEmpty()) return "???";
            return res;
        }
    }

    public abstract class FormattedJobReportConfigurationBase : JobReportConfigurationBase
    {
        private AddonHolder m_textFormatter = TextFormatterAddonType.Instance.FindHolder("html");
        [DatAdmin.DisplayName("s_text_formatter")]
        [RegisterItemType(typeof(TextFormatterAddonType))]
        [TypeConverter(typeof(RegisterItemTypeConverter))]
        [XmlElem("TextFormatter")]
        public AddonHolder TextFormatter
        {
            get { return m_textFormatter; }
            set { m_textFormatter = value; }
        }

        string m_language = Texts.Language;
        [DatAdmin.DisplayName("s_language")]
        [TypeConverter(typeof(LanguageTypeConverter))]
        public string Language
        {
            get { return m_language; }
            set { m_language = value; }
        }

        public ITextFormatter GetFormatter()
        {
            return (ITextFormatter)m_textFormatter.CreateInstance();
        }

        public override string FileExtension
        {
            get { return GetFormatter().FileExtension; }
        }
    }
}
