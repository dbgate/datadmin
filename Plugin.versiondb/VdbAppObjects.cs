using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.IO;
using System.Linq;
using Plugin.dbmodel;

namespace Plugin.versiondb
{
    [AppObject(Name = "vdbversion")]
    public class VersionDbAppObject : AppObject
    {
        VersionDb m_vdb;

        public VersionDbAppObject() { }
        public VersionDbAppObject(VersionDb vdb)
        {
            m_vdb = vdb;
            VdbFile = m_vdb.FileName;
        }

        [XmlElem]
        public string VdbFile { get; set; }

        public override string TypeName
        {
            get { return "versiondb"; }
        }
        public override string TypeTitle
        {
            get { return "s_version_db"; }
        }
        public override System.Drawing.Bitmap Image
        {
            get { return CoreIcons.versiondb; }
        }

        public VersionDb GetVdb()
        {
            if (m_vdb == null && !String.IsNullOrEmpty(VdbFile))
            {
                m_vdb = new VersionDb(VdbFile);
            }
            return m_vdb;
        }

        public override ISqlDialect Dialect
        {
            get
            {
                var vdb = GetVdb();
                return vdb.Dialect;
            }
        }

        private bool CheckGenerator(JobCommand cmd)
        {
            var gen = cmd as DbCreatorGeneratorCommand;
            try
            {
                gen.Check();
            }
            catch (Exception err)
            {
                Errors.Report(err);
                return false;
            }
            return true;
        }

        [PopupMenu("s_generate_db_creator")]
        public void GenerateDbCreator()
        {
            DbCreatorGeneratorCommand gen = new DbCreatorGeneratorCommand(m_vdb);
            RunOrSaveJobForm.Run(gen, CheckGenerator, "s_db_creator_properties");
        }

        [PopupMenu("s_properties", ImageName = CoreIcons.propertiesName, Weight = MenuWeights.PROPERTIES)]
        public void EditProperties()
        {
            m_vdb.Reload();
            if (VersionDbPropsForm.Run(m_vdb))
            {
                m_vdb.ChangedProps();
                m_vdb.Save();
            }
            else
            {
                m_vdb.Reload();
            }
        }

        private IDatabaseSource FindDb(string name)
        {
            var ver = m_vdb.FindVersion(name);
            if (ver != null) return new DbDefSource(ver.GetFile());
            return new DbDefSource(new DatabaseStructure(), DbDefSource.ReadOnly.Flag);
        }

        [PopupMenu("s_compare_versions", ImageName = DbModIcons.synchronizeName)]
        public void CompareVersions()
        {
            var ed = new SynchronizeExtData();
            var dbs = new List<string>();
            dbs.Add(Texts.Get("s_none"));
            dbs.AddRange(from v in m_vdb.Versions select v.Name);
            ed.Dbs = dbs.ToArray();
            ed.GetDb = FindDb;
            SynchronizeStructureForm.Run(null, null, ed);
        }
    }

    [AppObject(Name = "vdbversion")]
    public class VersionAppObject : AppObject
    {
        [XmlElem]
        public string VdbFile { get; set; }
        [XmlElem]
        public string Version { get; set; }

        VersionDef m_version;

        public VersionDef GetVersion()
        {
            if (m_version == null)
            {
                var vdb = new VersionDb(Path.Combine(Core.DataDirectory, VdbFile));
                m_version = new VersionDef(vdb, Version);
            }
            return m_version;
        }

        public VersionAppObject() { }
        public VersionAppObject(VersionDef version)
        {
            m_version = version;
            VdbFile = IOTool.RelativePathTo(Core.DataDirectory, m_version.Db.FileName);
            Version = m_version.Name;
        }
        public override void GetWidgets(List<IWidget> res)
        {
            res.Add(new VersionDiffWidget());
        }
        public override string TypeName
        {
            get { return "vdbversion"; }
        }
        public override string TypeTitle
        {
            get { return "s_version"; }
        }
        public override System.Drawing.Bitmap Image
        {
            get { return CoreIcons.versiondb; }
        }
    }

    [AppObject(Name = "vdbvariantversion")]
    public class VariantVersionAppObject : AppObject
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

        public VariantVersionAppObject() { }
        public VariantVersionAppObject(VdbVariantVersionDef version)
        {
            m_version = version;
            VdbFile = version.Version.Db.FileName;
            Variant = IOTool.RelativePathTo(Path.GetDirectoryName(VdbFile), version.VariantFile.DiskPath);
            Version = version.Version.Name;
        }

        public override void GetWidgets(List<IWidget> res)
        {
            base.GetWidgets(res);
            res.Add(new VersionVariantDiffWidget());
        }

        public override string TypeName
        {
            get { return "vdbversion"; }
        }
        public override string TypeTitle
        {
            get { return "s_version"; }
        }
        public override System.Drawing.Bitmap Image
        {
            get { return CoreIcons.versiondb; }
        }
    }
}
