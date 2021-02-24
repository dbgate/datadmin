using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Data;
using System.Drawing.Design;
using System.Windows.Forms;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace DatAdmin
{
    //public class FileDataStoreFileNameEditor : FileNameEditorBase
    //{
    //    protected override void GetDialogProps(ITypeDescriptorContext context, out FileDialogType dialogType, out string filter)
    //    {
    //        var store = (FileDataStoreBase)context.Instance;
    //        dialogType = FileDialogType.Open;
    //        if (store.Mode == TabularDataStoreMode.Read)
    //        {
    //            dialogType = FileDialogType.Open;
    //        }
    //        if (store.Mode == TabularDataStoreMode.Write)
    //        {
    //            dialogType = FileDialogType.Save;
    //        }
    //        filter = store.Filter;
    //    }
    //}

    public abstract class StreamDataStoreBase : AddonBase, ITabularDataStore
    {
        protected TabularDataStoreMode m_mode;
        protected ITableStructure m_rowFormat = null;
        Func<ITableStructure> m_getformat;

        protected abstract ITableStructure DoGetRowFormat();
        protected abstract void DoRead(IDataQueue queue);

        protected virtual void DoWrite(IDataQueue queue)
        {
            var api = StreamApi;
            if (api == null) throw new NotImplementedError("DAE-00100");
            StreamWriter fw = GetOutputStream();
            if (fw == null) throw new NotImplementedError("DAE-00101");
            try
            {
                ITableStructure ts = queue.GetRowFormat;
                object manager = null;
                api.WriteStart(fw, ts, ref manager);
                try
                {
                    int index = 0;
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
            finally
            {
                fw.Close();
            }
            FinalizeBulkCopy();
        }

        public virtual void ClearLoadCaches()
        {
        }

        protected virtual StreamWriter GetOutputStream()
        {
            return null;
        }

        #region ITabularDataStore Members

        [Browsable(false)]
        public TableCopyOptions CopyOptions { get; set; }

        private IProgressInfo m_progressInfo;
        [XmlIgnore]
        [Browsable(false)]
        public IProgressInfo ProgressInfo
        {
            get { return m_progressInfo; }
            set { m_progressInfo = value; OnChangedProgressInfo(); }
        }

        protected virtual void OnChangedProgressInfo() { }

        [Browsable(false)]
        public TabularDataStoreMode Mode
        {
            get { return m_mode; }
            set { m_mode = value; }
        }

        public virtual bool SupportsMode(TabularDataStoreMode mode)
        {
            return mode == TabularDataStoreMode.Read || mode == TabularDataStoreMode.Write || (mode == TabularDataStoreMode.WriteStream && StreamApi != null);
        }

        public IAsyncResult BeginRead(AsyncCallback callback, IDataQueue queue)
        {
            return Async.BeginCancelableInvoke((Action<IDataQueue>)DoRead, callback, new object[] { queue });
        }

        public void EndRead(IAsyncResult async)
        {
            ((IStandaloneAsyncResult)async).EndInvoke();
        }

        public IAsyncResult BeginWrite(AsyncCallback callback, IDataQueue queue)
        {
            return Async.BeginCancelableInvoke((Action<IDataQueue>)DoWrite, callback, new object[] { queue });
        }

        public void EndWrite(IAsyncResult async)
        {
            ((IStandaloneAsyncResult)async).EndInvoke();
        }

        public IAsyncResult BeginGetRowFormat(AsyncCallback callback)
        {
            m_getformat = CallDoGetRowFormat;
            return m_getformat.BeginInvoke(callback, null);
        }

        public ITableStructure EndGetRowFormat(IAsyncResult async)
        {
            return m_getformat.EndInvoke(async);
        }

        public void SetRowFormat(ITableStructure rowFormat)
        {
            m_rowFormat = rowFormat;
        }

        [Browsable(false)]
        public virtual bool ConfigurationNeeded { get { return true; } }

        [Browsable(false)]
        public bool AvailableRowFormat { get { return m_mode == TabularDataStoreMode.Read; } }

        public virtual void CheckConfiguration() { }

        public void ClearCaches()
        {
            m_rowFormat = null;
        }

        public void CloseAllResources() { }

        [Browsable(false)]
        public virtual ITabularDataOuputStream StreamApi { get { return null; } }

        #endregion

        private ITableStructure CallDoGetRowFormat()
        {
            if (m_mode == TabularDataStoreMode.Read && m_rowFormat == null && IsConfiguredSource())
            {
                m_rowFormat = DoGetRowFormat();
            }
            return m_rowFormat;
        }

        #region IConnectionUsage Members

        [Browsable(false)]
        public IPhysicalConnection Connection
        {
            get { return null; }
            set { }
        }

        #endregion

        public virtual bool IsConfiguredSource() { return true; }

        [Browsable(false)]
        public override AddonType AddonType
        {
            get { return TabularDataStoreAddonType.Instance; }
        }

        protected virtual void FinalizeBulkCopy() { }
    }

    [DefaultPropertyTab("s_details", TabWeight = 1)]
    public abstract class FileDataStoreBase : StreamDataStoreBase, ITabularDataStore, IExtendedFileNameHolderInfo
    {
        IFilePlace m_filePlace = new FilePlaceFileSystem();

        public override void CheckConfiguration()
        {
            if (m_mode == TabularDataStoreMode.Read)
            {
                FilePlace.CheckInput();
                //if (m_filename.IsEmpty()) throw new CheckConfigError(Texts.Get("s_input_file_is_not_defined"));
                //if (!File.Exists(m_filename))
                //{
                //    throw new CheckConfigError(Texts.Get("s_input_file_does_not_exist$file", "file", m_filename));
                //}
            }
            if (m_mode == TabularDataStoreMode.Write)
            {
                FilePlace.CheckOutput();
                //IOTool.CheckOutputFileName(ref m_filename);
            }
        }

        protected override void OnChangedProgressInfo()
        {
            base.OnChangedProgressInfo();
            if (FilePlace != null) FilePlace.ProgressInfo = ProgressInfo;
        }

        public bool EnableFileEditor()
        {
            return m_mode != TabularDataStoreMode.WriteStream;
        }

        protected string GetWorkingFileName()
        {
            //if (m_fileHandler == null) m_fileHandler = FilePlaceAddonType.PlaceFromVirtualFile(VirtualFilename, this);
            return m_filePlace.GetWorkingFileName();
        }

        protected override void FinalizeBulkCopy()
        {
            m_filePlace.FinalizeFileName();
        }

        [Browsable(false)]
        [TabbedEditor(typeof(FileNameEditorFrame), TabWeight = 10, EnabledFunc = "EnableFileEditor")]
        public IFilePlace FilePlace
        {
            get { return m_filePlace; }
            set { m_filePlace = value; m_filePlace.ProgressInfo = ProgressInfo; }
        }

        //[XmlAttrib("filename")]
        //public string VirtualFilename
        //{
        //    get
        //    {
        //        if (m_mode == TabularDataStoreMode.WriteStream) return "N/A";
        //        return m_filename;
        //    }
        //    set
        //    {
        //        m_filename = value;
        //        if (m_mode == TabularDataStoreMode.Read)
        //        {
        //            ClearLoadCaches();
        //            m_rowFormat = null;
        //        }
        //    }
        //}

        [Browsable(false)]
        [XmlElem("File")]
        public string XmlFileName
        {
            get { return m_filePlace.GetVirtualFile(); }
            set
            {
                m_filePlace = FilePlaceAddonType.PlaceFromVirtualFile(value, this);
                m_filePlace.SetFileHolderInfo(this);
                m_filePlace.ProgressInfo = ProgressInfo;
            }
        }

        protected Encoding m_encoding = System.Text.Encoding.UTF8;
        [DatAdmin.DisplayName("s_encoding")]
        [TypeConverter(typeof(EncodingTypeConverter))]
        [XmlAttrib("encoding")]
        public string Encoding
        {
            get { return m_encoding.WebName; }
            set { m_encoding = System.Text.Encoding.GetEncoding(value); }
        }

        public override string ToString()
        {
            return String.Format("{0} {1}: {2}", FileExtension.ToUpper(), Texts.Get("s_file"), FilePlace);
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
        public abstract string FileExtension { get;}

        public override bool IsConfiguredSource()
        {
            return !String.IsNullOrEmpty(FilePlace.ToString());
        }

        protected override StreamWriter GetOutputStream()
        {
            return new StreamWriter(GetWorkingFileName(), false, m_encoding);
        }

        public override void ClearLoadCaches()
        {
            base.ClearLoadCaches();
            if (FilePlace != null)
            {
                FilePlace.CleanUp();
            }
        }

        #region IExtendedFileNameHolder Members

        [Browsable(false)]
        public bool DirectionIsSave
        {
            get { return Mode != TabularDataStoreMode.Read; }
        }

        [Browsable(false)]
        public AppObject RelatedObject { get { return null; } }

        [Browsable(false)]
        public IPhysicalConnectionFactory RelatedConnection { get { return null; } }

        [Browsable(false)]
        public string RelatedDatabase { get { return null; } }

        #endregion

        public override void LoadFromXml(XmlElement xml)
        {
            base.LoadFromXml(xml);
            // backward compatibility
            if (xml.HasAttribute("filename")) XmlFileName = xml.GetAttribute("filename");
        }
    }

    public abstract class FileWithFormatDataStoreBase : FileDataStoreBase, IDataFormatHolder
    {
        DataFormatSettings m_formatSettings = new DataFormatSettings();

        [Category("s_format_settings")]
        [DisplayName("s_format_settings")]
        [TabbedProperty("s_format_settings", TabWeight = -1)]
        [XmlSubElem("DataFormat")]
        [Browsable(false)]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public DataFormatSettings FormatSettings
        {
            get { return m_formatSettings; }
            set { m_formatSettings = value; }
        }
    }
}
