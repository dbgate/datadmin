using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace DatAdmin
{
    public class DatabaseWriterAttribute : RegisterAttribute { }

    public interface IDatabaseWriter
    {
        bool DirectCopy(IDatabaseSource source);
        void RunDirectCopy(IDatabaseSource source, DatabaseCopyOptions copyOpts);
        void WriteStructureBeforeData(IDatabaseStructure db);
        void WriteStructureAfterData(IDatabaseStructure db);
        void BeforeFillData();
        void FillTable(ITableStructure table, IDataQueue queue, TableCopyOptions opts);
        void AfterFillData();
        void SetSourceInfo(DatabaseWriterSourceInfo info);
        DatabaseWriterCaps WriterCaps { get; }
        bool ConfigurationNeeded { get; }
        /// <summary>
        /// opens connection, if it is allready opened, does nothing
        /// </summary>
        void OpenConnection();
        void CloseConnection();
        void CheckConfiguration(IDatabaseSource source);
        void LoadFromXml(XmlElement xml);
        void SaveToXml(XmlElement xml);
        void InitializeFromInput(IDatabaseSource input);
        IDatabaseWriter GetRedirectedWriter();
        IDatabaseStructure InvokeLoadStructure(DatabaseStructureMembers members, IProgressInfo progress);
        ISqlDialect Dialect { get; }
        IProgressInfo ProgressInfo { get; set; }
        /// <summary>
        /// does necessary cleanup, if database-write process is failed (eg. canceled by user
        /// or because of fatal error)
        /// </summary>
        void ProcessFailed();
    }

    [AddonType]
    public class DatabaseWriterAddonType : AddonType
    {
        public override string Name
        {
            get { return "dbwriter"; }
        }

        public override Type InterfaceType
        {
            get { return typeof(IDatabaseWriter); }
        }

        public override Type RegisterAttributeType
        {
            get { return typeof(DatabaseWriterAttribute); }
        }

        public static readonly DatabaseWriterAddonType Instance = new DatabaseWriterAddonType();
    }

    public class DatabaseWriterSourceInfo
    {
        public ISqlDialect Dialect;
        //public DbCopyMode CopyMode;
        public DbCopySchemaMode SchemaMode;
    }
}
