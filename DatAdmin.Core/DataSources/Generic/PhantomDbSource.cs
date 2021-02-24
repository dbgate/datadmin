using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DatAdmin
{
    public class PhantomDatabaseSource : GenericConnectionUsage, IDatabaseSource
    {
        string m_directory;
        IServerSource m_server;

        public PhantomDatabaseSource(IServerSource server, string directory)
            : base(server.Connection)
        {
            m_directory = directory;
            m_server = server;
        }

        #region IDatabaseSource Members

        public IDatabaseStructure LoadDatabaseStructure(DatabaseStructureMembers members, IProgressInfo progress)
        {
            return null;
        }

        public ITableSource GetTable(NameWithSchema table)
        {
            return null;
        }

        public IPhysicalConnection CloneConnection()
        {
            return m_server.Connection.Clone();
        }

        public IServerSource Server
        {
            get { return m_server; }
        }

        public string DatabaseName
        {
            get { return Path.GetFileName(m_directory); }
        }

        public IDatabaseSource CloneSource()
        {
            return new PhantomDatabaseSource(new GenericServerSource(CloneConnection()), m_directory);
        }

        public void ClearCaches()
        {
        }

        public IDatabaseSource MergeToConnection(IPhysicalConnection conn)
        {
            return this;
        }

        public string GetPrivateSubFolder(string name)
        {
            return Path.Combine(m_directory, name);
        }

        public string GetPrivateFolder() { return m_directory; }

        public void SaveToXml(System.Xml.XmlElement xml)
        {
        }

        public void LoadFromXml(System.Xml.XmlElement xml)
        {
        }

        public List<ISpecificObjectType> GetSpecificTypes()
        {
            return new List<ISpecificObjectType>();
        }

        public string DefaultSchema
        {
            get { return null; }
        }

        public DatabaseSourceCaps OfflineDatabaseCaps
        {
            get { return DatabaseCaps; }
        }

        public DatabaseSourceCaps DatabaseCaps
        {
            get
            {
                return new DatabaseSourceCaps
                {
                    AllFlags = false,
                    IsPhantom = true,
                };
            }
        }

        public TableSourceCaps TableCaps
        {
            get { return new TableSourceCaps { AllFlags = false }; }
        }

        public AlterProcessorCaps AlterCaps
        {
            get { return new AlterProcessorCaps { AllFlags = false }; }
        }

        public ISqlDialect Dialect
        {
            get { return GenericDialect.Instance; }
        }

        public void AlterDatabase(IDatabaseStructure src, IDatabaseStructure dst, DbDiffOptions opts)
        {
        }

        public void AlterDatabase(AlterPlan plan, DbDiffOptions opts)
        {
        }

        public SettingsPageCollection Settings
        {
            get { return null; }
        }

        #endregion
    }
}
