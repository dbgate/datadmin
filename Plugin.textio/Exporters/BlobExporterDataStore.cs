using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.ComponentModel;
using System.IO;

namespace Plugin.textio.Exporters
{
    [TabularDataStore(Name = "blob_exporter", Title = "s_blob_exporter", Description = "s_blob_exporter_desc", RequiredFeature = BlobExportFeature.Test)]
    public class BlobExporterDataStore : AddonBase, ITabularDataStore
    {
        [DatAdmin.DisplayName("s_output_folder")]
        public string OutputDirectory { get; set; }

        [DatAdmin.DisplayName("s_output_file_template")]
        [Description("s_output_file_template_desc")]
        public string OutputFileNameTemplate { get; set; }

        protected Encoding m_encoding = System.Text.Encoding.UTF8;
        [DatAdmin.DisplayName("s_encoding")]
        [TypeConverter(typeof(EncodingTypeConverter))]
        [XmlAttrib("encoding")]
        public string Encoding
        {
            get { return m_encoding.WebName; }
            set { m_encoding = System.Text.Encoding.GetEncoding(value); }
        }

        public BlobExporterDataStore()
        {
            OutputFileNameTemplate = "#FILE#.dat";
        }

        public override AddonType AddonType
        {
            get { return TabularDataStoreAddonType.Instance; }
        }

        #region ITabularDataStore Members

        [Browsable(false)]
        public TabularDataStoreMode Mode
        {
            get
            {
                return TabularDataStoreMode.Write;
            }
            set
            {
                if (value != TabularDataStoreMode.Write) throw new Exception("DAE-00375 Unallowed mode");
            }
        }

        [Browsable(false)]
        public IProgressInfo ProgressInfo { get; set; }

        public IAsyncResult BeginRead(AsyncCallback callback, IDataQueue queue)
        {
            throw new NotImplementedError("DAE-00389");
        }

        public void EndRead(IAsyncResult async)
        {
            throw new NotImplementedError("DAE-00390");
        }

        protected virtual void DoWrite(IDataQueue queue)
        {
            var fmt = new BedValueFormatter(new DataFormatSettings());
            try
            {
                int index = 0;
                while (!queue.IsEof)
                {
                    var record = queue.GetRecord();
                    record.ReadValue(0);
                    fmt.ReadFrom(record);
                    string name = OutputFileNameTemplate;
                    name = name.Replace("#FILE#", fmt.GetText());
                    name = name.Replace("#INDEX#", (index + 1).ToString());
                    string fn = Path.Combine(OutputDirectory, name);
                    record.ReadValue(1);
                    switch (record.GetFieldType())
                    {
                        case TypeStorage.String:
                            using (var tw = new StreamWriter(fn, false, m_encoding))
                            {
                                tw.Write(record.GetString());
                            }
                            break;
                        case TypeStorage.ByteArray:
                            using (var fw = new FileInfo(fn).OpenWrite())
                            {
                                var data = record.GetByteArray();
                                fw.Write(data, 0, data.Length);
                            }
                            break;
                    }
                    index++;
                }
            }
            finally
            {
                queue.CloseReading();
            }
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
            var ts = new TableStructure();
            ts._Columns.Add(new ColumnStructure { ColumnName = Texts.Get("s_filename"), DataType = new DbTypeString { Length = 250 } });
            ts._Columns.Add(new ColumnStructure { ColumnName = Texts.Get("s_data"), DataType = new DbTypeBlob() });
            return new ValueAsyncResult(ts, null);
        }

        public ITableStructure EndGetRowFormat(IAsyncResult async)
        {
            return (ITableStructure)((ValueAsyncResult)async).Value;
        }

        public void SetRowFormat(ITableStructure rowFormat)
        {
        }

        [Browsable(false)]
        public bool ConfigurationNeeded
        {
            get { return true; }
        }

        [Browsable(false)]
        public bool AvailableRowFormat
        {
            get { return true; }
        }

        public bool SupportsMode(TabularDataStoreMode mode)
        {
            return mode == TabularDataStoreMode.Write;
        }

        public void CheckConfiguration()
        {
            if (String.IsNullOrEmpty(OutputDirectory) || !Directory.Exists(OutputDirectory))
            {
                throw new CheckConfigError("DAE-00376 " + Texts.Get("s_output_directory_does_not_exist"));
            }
        }

        public void ClearCaches()
        {
        }

        public void CloseAllResources()
        {
        }

        [Browsable(false)]
        public ITabularDataOuputStream StreamApi
        {
            get { return null; }
        }

        [Browsable(false)]
        public TableCopyOptions CopyOptions { get; set; }

        #endregion

        #region IConnectionUsage Members

        [Browsable(false)]
        public IPhysicalConnection Connection
        {
            get { return null; }
            set { }
        }

        #endregion

        public override string ToString()
        {
            return "BLOB:" + OutputDirectory + "/" + OutputFileNameTemplate;
        }
    }
}
