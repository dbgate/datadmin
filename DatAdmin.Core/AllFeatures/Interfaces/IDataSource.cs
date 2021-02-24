using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data;
using System.IO;
using System.Xml;

namespace DatAdmin
{
    public interface IConnectionUsage
    {
        /// <summary>
        /// used Connection object; if setting, new connection must have the same connection key
        /// as the old one
        /// </summary>
        IPhysicalConnection Connection { get; set; }
    }

    public interface IServerSource : IConnectionUsage
    {
        IEnumerable<string> Databases { get;}
        IDatabaseSource GetDatabase(string name);
        IPhysicalConnection CloneConnection();
        IDatabaseSource CreateDatabase(string dbname, Dictionary<string, string> props);
        IServerSource MergeToConnection(IPhysicalConnection conn);
    }

    public class DatabaseSourceAttribute : RegisterAttribute { }

    public interface IDatabaseSource : IConnectionUsage
    {
        IDatabaseStructure LoadDatabaseStructure(DatabaseStructureMembers members, IProgressInfo progress);
        ITableSource GetTable(NameWithSchema table);
        //ITableSource CreateTable(ITableStructure table);
        IPhysicalConnection CloneConnection();
        IServerSource Server { get;}
        string DatabaseName { get;}
        IDatabaseSource CloneSource();
        void ClearCaches();
        IDatabaseSource MergeToConnection(IPhysicalConnection conn);
        /// <summary>
        /// returns private folder or NULL, when it has not sense (but GetPrivateSubFolder can return valid path)
        /// </summary>
        /// <returns></returns>
        string GetPrivateFolder();
        string GetPrivateSubFolder(string name);
        void SaveToXml(XmlElement xml);
        void LoadFromXml(XmlElement xml);
        List<ISpecificObjectType> GetSpecificTypes();
        string DefaultSchema { get; }

        DatabaseSourceCaps DatabaseCaps { get; }
        /// <summary>
        /// returns all database caps available without "longer" actions
        /// </summary>
        DatabaseSourceCaps OfflineDatabaseCaps { get; }
        TableSourceCaps TableCaps { get; }
        AlterProcessorCaps AlterCaps { get; }

        ISqlDialect Dialect { get; }

        void AlterDatabase(IDatabaseStructure src, IDatabaseStructure dst, DbDiffOptions opts);
        void AlterDatabase(AlterPlan plan, DbDiffOptions opts);

        SettingsPageCollection Settings { get; }
    }

    [AddonType]
    public class DatabaseSourceAddonType : AddonType
    {
        public override string Name
        {
            get { return "dbsource"; }
        }

        public override Type InterfaceType
        {
            get { return typeof(IDatabaseSource); }
        }

        public override Type RegisterAttributeType
        {
            get { return typeof(DatabaseSourceAttribute); }
        }

        public static readonly DatabaseSourceAddonType Instance = new DatabaseSourceAddonType();
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class MigrationProfileAttribute : RegisterAttribute
    {
    }

    public interface IMigrationProfile
    {
        void SaveToXml(XmlElement xml);
        void LoadFromXml(XmlElement xml);
    }

    [AddonType]
    public class MigrationProfileAddonType : AddonType
    {
        public override string Name
        {
            get { return "migrationprofile"; }
        }

        public override Type InterfaceType
        {
            get { return typeof(IMigrationProfile); }
        }

        public override Type RegisterAttributeType
        {
            get { return typeof(MigrationProfileAttribute); }
        }

        public static readonly MigrationProfileAddonType Instance = new MigrationProfileAddonType();
    }

    public interface ITableSource : IConnectionUsage
    {
        ITabularDataView GetTabularData();
        ITabularDataStore GetDataStoreAndClone();
        ITabularDataStore GetDataStoreAndReuse();
        ITableStructure LoadTableStructure(TableStructureMembers members); // should be called from connection thread
        void TruncateTable();
        IDatabaseSource Database { get; }
        TableSourceCaps TableCaps { get;}
        NameWithSchema FullName { get;}
        ITableSource MergeToConnection(IPhysicalConnection conn);
    }
}
