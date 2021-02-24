using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Xml;
using System.ComponentModel;
using System.IO;
using System.Data.Common;

namespace DatAdmin
{
    public enum TabularDataStoreMode { Unknown, Read, Write, WriteStream };
    //public enum TabularDataStoreState { Unknown, Read, Write };

    public enum TabularDataViewState { Prepared, Loading, Error };

    public class LoadedNextDataArgs : EventArgs
    {
        public readonly int LoadedRowCount;
        public LoadedNextDataArgs(int rowcount)
        {
            LoadedRowCount = rowcount;
        }
    }

    public delegate void LoadedNextDataDelegate(object sender, LoadedNextDataArgs e);
    public enum TableDataScript { Select, Delete }

    public enum SaveProgressMeasure { DeletedRows, InsertedRows, UpdatedRows, UpdatedFields };
    public interface ISaveDataProgress
    {
        void IncrementCount(SaveProgressMeasure measure, int count);
        void SetCurrent(SaveProgressMeasure measure, int value);
        int GetCurrent(SaveProgressMeasure measure);
        void NotifyFinished();
        bool IsCanceled { get; }
    }

    public class LoadedTableInfoArgs : EventArgs
    {
        public BedTable Table;
        public Exception Error;
        public TimeSpan Duration;
    }
    public delegate void LoadedTableInfoDelegate(object sender, LoadedTableInfoArgs e);

    public interface ITabularDataView : IConnectionUsage
    {
        BedTable LoadTableData(TablePageProperties props);
        int? LoadRowCount(TableDataSetProperties props);
        void LoadAllRows(TableDataSetProperties props, Action<IBedRecord> forEachRow);
        void NotifyPerspectiveChanged(TablePerspective per);
        void NotifyRefresh();
        void SaveChanges(BedTable table, ISaveDataProgress progress);
        void SaveChanges(BedTable table, ISqlDumper dmp);
        bool Readonly { get; }
        event LoadedNextDataDelegate LoadedNextData;
        event LoadedTableInfoDelegate LoadedDataInfo;
        TabularDataViewState State { get; }
        ITabularDataStore GetStoreAndClone(TableDataSetProperties props);
        IProgressInfo ProgressInfo { get; set; }
        void CloseView();
        TabularDataViewCaps TabDataCaps { get; }
        ITableSource TableSource { get; }
        IDatabaseSource DatabaseSource { get; }
        ITableStructure GetStructure(TablePerspective per);
        SettingsPageCollection Settings { get; }
        ISqlDialect Dialect { get; }
        ITabularDataView CloneView();
        void GenerateScript(TableDataScript script, TableDataSetProperties props, ISqlDumper dmp);
        bool SupportsSerialize { get; }
        void SaveToXml(XmlElement xml);
        string FixedPerspective { get; }
        //IPhysicalConnection Connection { get;}
    }


    //[AttributeUsage(AttributeTargets.Class)]
    //public class StaticTabularDataAttribute : Attribute
    //{
    //}

    [AttributeUsage(AttributeTargets.Class)]
    public class TabularDataStoreAttribute : RegisterAttribute
    {
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class TabularDataStoreChooserAttribute : RegisterAttribute
    {
    }

    [AddonType]
    public class TabularDataStoreAddonType : AddonType
    {
        public override string Name
        {
            get { return "datastore"; }
        }

        public override Type InterfaceType
        {
            get { return typeof(ITabularDataStore); }
        }

        public override Type RegisterAttributeType
        {
            get { return typeof(TabularDataStoreAttribute); }
        }

        public static readonly TabularDataStoreAddonType Instance = new TabularDataStoreAddonType();
    }

    //public interface IRowFormat
    //{
    //}
    //public interface ITabularDataStoreConfig
    //{
    //    DataTable RowFormat { get;set;}
    //    ITabularDataStore GetStore();
    //    TabularDataMode Mode { get;set;}
    //}

    public interface IDataQueue
    {
        IBedRecord GetRecord();
        void PutRecord(IBedRecord record);

        ITableStructure PutRowFormat { get; }
        ITableStructure GetRowFormat { get; }

        void PutEof();
        bool IsEof { get; }

        /// <summary>
        /// Closes queue. Should be called after reading finished
        /// </summary>
        void CloseReading();

        /// <summary>
        /// Closes queue. Should be called after writing finished. If called before PutEof,
        /// it signalizes abnormal termination. GetRecord than throws exception QueueClosed
        /// </summary>
        void CloseWriting();

        void PutError(Exception e);

        IProgressInfo ProgressInfo { get; set; }
    }

    public class RowTransformAttribute : RegisterAttribute { }

    public interface IRecordToDbAdapter
    {
        IBedRecord AdaptRecord(IBedRecord record, ILogger logger);
        IProgressInfo ProgressInfo { get; set; }
    }

    public interface IRowTransform
    {
        IBedRecord Transform(IBedRecord record);
        ITableStructure InputFormat { get; }
        ITableStructure OutputFormat { get; }
        void SaveToXml(XmlElement xml);
        void LoadFromXml(XmlElement xml, ITableStructure source, ITableStructure target);
    }

    [AddonType]
    public class RowTransformAddonType : AddonType
    {
        public override string Name
        {
            get { return "rowtransform"; }
        }

        public override Type InterfaceType
        {
            get { return typeof(IRowTransform); }
        }

        public override Type RegisterAttributeType
        {
            get { return typeof(RowTransformAttribute); }
        }

        public static readonly RowTransformAddonType Instance = new RowTransformAddonType();

        public IRowTransform LoadRowTransform(string filename, ITableStructure source, ITableStructure target)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(filename);
            return LoadRowTransform(doc.DocumentElement, source, target);
        }

        public IRowTransform LoadRowTransform(XmlElement xml, ITableStructure source, ITableStructure target)
        {
            IRowTransform res = (IRowTransform)StaticSpace.FindHolder(xml.GetAttribute("type")).CreateInstance();
            res.LoadFromXml(xml, source, target);
            return res;
        }
    }

    public interface ITabularDataOuputStream
    {
        void WriteStart(StreamWriter fw, ITableStructure table, ref object manager);
        void WriteRecord(StreamWriter fw, ITableStructure table, IBedRecord record, int index, object manager);
        void WriteEnd(StreamWriter fw, ITableStructure table, object manager);
        bool RequireSingleStream { get; }
    }

    public interface ITabularDataStore : IConnectionUsage
    {
        TabularDataStoreMode Mode { get; set; }
        IProgressInfo ProgressInfo { get; set; }
        //DataTable RowFormat { get;set;}

        IAsyncResult BeginRead(AsyncCallback callback, IDataQueue queue);
        void EndRead(IAsyncResult async);

        IAsyncResult BeginWrite(AsyncCallback callback, IDataQueue queue);
        void EndWrite(IAsyncResult async);

        IAsyncResult BeginGetRowFormat(AsyncCallback callback);
        ITableStructure EndGetRowFormat(IAsyncResult async);

        void SetRowFormat(ITableStructure rowFormat);
        bool ConfigurationNeeded { get; }
        bool AvailableRowFormat { get; }

        bool SupportsMode(TabularDataStoreMode mode);

        // if configuration is not OK, throws error
        void CheckConfiguration();
        void ClearCaches();

        void SaveToXml(XmlElement xml);
        void LoadFromXml(XmlElement xml);

        void CloseAllResources();
        //DataTable Columns { get;set;}
        //ITabularDataReader GetReader();
        //ITabularDataWriter GetWriter();
        ITabularDataOuputStream StreamApi { get; }
        TableCopyOptions CopyOptions { get; set; }
    }

    public interface IDataFormatHolder
    {
        DataFormatSettings FormatSettings { get; }
    }

    public interface IBulkInserter
    {
        TableCopyOptions CopyOptions { get; set; }
        //bool AllowBulkCopy { get; set; }
        //bool TruncateBeforeCopy { get; set; }
        ITableStructure DestinationTable { get; set; }
        IProgressInfo ProgressInfo { get; set; }
        int BatchSize { get; set; }
        string DatabaseName { get; set; }
        IPhysicalConnection Connection { get; set; }
        void Run(IDataQueue queue);
    }

    public interface IRowCollection<RowType> : IEnumerable<RowType>
        where RowType : class, IBedRecord
    {
        RowType this[int index] { get; }
        int Count { get; }
    }

    public interface IInMemoryTable<RowType>
        where RowType : class, IBedRecord
    {
        ITableStructure Structure { get; }
        IRowCollection<RowType> Rows { get; }
    }

    public class TabularDataViewLoaderAttribute : RegisterAttribute { }

    public interface ITabularDataViewLoader : IAddonInstance
    {
        ITabularDataView CreateTabularDataView();
    }

    [AddonType]
    public class TabularDataViewLoaderAddonType : AddonType
    {
        public override string Name
        {
            get { return "tabulardataviewloader"; }
        }

        public override Type InterfaceType
        {
            get { return typeof(ITabularDataViewLoader); }
        }

        public override Type RegisterAttributeType
        {
            get { return typeof(TabularDataViewLoaderAttribute); }
        }

        public static readonly TabularDataViewLoaderAddonType Instance = new TabularDataViewLoaderAddonType();
    }
}
