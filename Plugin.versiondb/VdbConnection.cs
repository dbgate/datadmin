using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using Plugin.dbmodel;
using System.IO;

namespace Plugin.versiondb
{
    public class VdbLastVersionConnection : DbDefConnection
    {
        VersionDb m_vdb;
        public VdbLastVersionConnection(VersionDb vdb)
            : base(new VdbLastVersionConnectionFactory { FileName = vdb.FileName })
        {
            m_vdb = vdb;
        }

        protected override string GetCurrentFileName()
        {
            return m_vdb.GetLastVersionFile();
        }
    }

    [PhysicalConnectionFactory(Name = "vdb_last_version")]
    public class VdbLastVersionConnectionFactory : PhysicalConnectionFactoryBase
    {
        [XmlElem]
        public string FileName { get; set; }

        public override string GetDataSource()
        {
            return FileName;
        }

        public override string GetConnectionKey()
        {
            return "vdb_last:" + FileName; 
        }

        public override IPhysicalConnection CreateConnection()
        {
            return new VdbLastVersionConnection(new VersionDb(FileName));
        }

        public override IDatabaseSource CreateDatabaseSource(IPhysicalConnection conn, string dbname)
        {
            return new DbDefSource((VdbLastVersionConnection)conn);
        }

        public override string GetDatabasePrivateFolder(string dbname)
        {
            return null;
        }

        public override string GetDatabasePrivateSubFolder(string dbname, string folderName)
        {
            return FileName + "." + folderName;
        }

        public override string GetFileName()
        {
            return FileName;
        }
    }

    public class VdbVariantVersionConnection : DbDefConnection
    {
        public VdbVariantVersionConnection(VdbVariantVersionConnectionFactory fact)
            : base(fact)
        {
        }

        VdbVariantVersionConnectionFactory Fact { get { return (VdbVariantVersionConnectionFactory)PhysicalFactory; } }

        public override IDatabaseStructure CustomGetStructure()
        {
            return Fact.GetVersion().LoadStructure();
        }
    }

    [PhysicalConnectionFactory(Name = "vdb_variant")]
    public class VdbVariantVersionConnectionFactory : PhysicalConnectionFactoryBase
    {
        [XmlElem]
        public string VdbFile { get; set; }
        [XmlElem]
        public string Version { get; set; }
        [XmlElem]
        public string Variant { get; set; }

        VdbVariantVersionDef m_version;

        public VdbVariantVersionDef GetVersion()
        {
            if (m_version == null)
            {
                var vdb = new VersionDb(VdbFile);
                var ver = new VersionDef(vdb, Version);
                m_version = new VdbVariantVersionDef(ver, new DiskFile(Path.Combine(vdb.VariantsDirectory, Variant)));
            }
            return m_version;
        }

        public override string GetDataSource()
        {
            return "(MODEL)";
        }

        public override string GetConnectionKey()
        {
            return "vdbvarversion:" + VdbFile + Version + Variant;
        }

        public override IPhysicalConnection CreateConnection()
        {
            return new VdbVariantVersionConnection(this);
        }

        public override IDatabaseSource CreateDatabaseSource(IPhysicalConnection conn, string dbname)
        {
            return new DbDefSource((DbDefConnection)conn);
        }

        public override string GetDatabasePrivateFolder(string dbname)
        {
            return null;
        }

        public override string GetDatabasePrivateSubFolder(string dbname, string folderName)
        {
            return null;
        }

        public override string GetFileName()
        {
            return null;
        }
    }
}
