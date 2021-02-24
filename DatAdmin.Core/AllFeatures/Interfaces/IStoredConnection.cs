using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Xml;

namespace DatAdmin
{
    public interface IOnlineStoredConnection
    {
        string DataSource { get; set; }
        int Port { get; set; }
        string Login { get; set; }
        string Password { get; set; }
        string DatabaseName { get; set; }
    }

    /// <summary>
    /// connection saved in XML file in data directory
    /// </summary>
    public interface IStoredConnection : IFileBasedAddonInstance
    {
        void Save();
        bool Edit();
        IPhysicalConnection CreatePhysicalConnection();
        IPhysicalConnection CreatePhysicalConnection(string connectionString);
        IProviderHooks CreateHooks();
        void InstallHooks(IPhysicalConnection conn);
        DbConnection CreateSystemConnection();
        DbProviderFactory GetFactory();
        string GenerateConnectionString(bool includepwd);
        string DialectOverride { get; set; }
        ISqlDialect GetDialect();
        IDialectDetector GetDialectDetector();
        string ConnectionKey { get; }

        bool SupportsDatabaseSelect { get; }
        ConnectionDatabaseMode DatabaseMode { get; set; }
        string ExplicitDatabaseName { get; set; }
        ConnectionEditFrame CreateEditor();
        string ConnectionTypeTitle { get; }
        string HelpTopic { get; }

        string FileName { get; set; }
        string DataTreeName { get; set; }

        string GetLogin();
        string GetDataSource();

        SettingsPageCollection ConnectionSettings { get; }
        SettingsPageCollection GetDatabaseSettings(string dbname);
        string GetDatabasePrivateFolder(string dbname);
        string GetDatabasePrivateSubFolder(string dbname, string folderName);
    }

    public enum ConnectionDatabaseMode { All, Default, Explicit }

    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class StoredConnectionAttribute : RegisterAttribute
    {
    }

    [AddonType]
    public class StoredConnectionAddonType : AddonType
    {
        public override string Name
        {
            get { return "driver"; }
        }

        public override Type InterfaceType
        {
            get { return typeof(IStoredConnection); }
        }

        public override Type RegisterAttributeType
        {
            get { return typeof(StoredConnectionAttribute); }
        }

        public static readonly StoredConnectionAddonType Instance = new StoredConnectionAddonType();
    }
}
