using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.Xml;
using System.IO;

namespace Plugin.versiondb
{
    public class VariantDef
    {
        NamedAddonCollection m_modelTransforms = new NamedAddonCollection(DbModelTransformAddonType.Instance);

        public VariantDef() { }

        public VariantDef(IVirtualFile file)
        {
            Load(file);
        }

        public NamedAddonCollection ModelTransforms
        {
            get { return m_modelTransforms; }
        }

        public void Load(IVirtualFile file)
        {
            var doc = new XmlDocument();
            doc.LoadXml(file.GetText());
            LoadFromXml(doc.DocumentElement);
        }

        public void Save(IVirtualFile file)
        {
            XmlDocument doc = XmlTool.CreateDocument("Variant");
            SaveToXml(doc.DocumentElement);
            using (var sw = new StringWriter())
            {
                doc.Save(sw);
                file.SaveText(sw.ToString());
            }
        }

        public void SaveToXml(XmlElement xml)
        {
            ModelTransforms.SaveToXml(xml);
        }

        public void LoadFromXml(XmlElement xml)
        {
            ModelTransforms.LoadFromXml(xml);
        }

        public void RunTransform(DatabaseStructure dbs)
        {
            foreach (var addon in ModelTransforms)
            {
                var mt = addon.Instance as IDbModelTransform;
                mt.RunTransform(dbs);
            }
        }
    }

    public class VdbVariantVersionDef
    {
        public IVirtualFile VariantFile;
        public VersionDef Version;

        public VdbVariantVersionDef(VersionDef version, IVirtualFile varfile)
        {
            Version = version;
            VariantFile = varfile;
        }

        //public VdbVariantVersionDef GetVersion(int d)
        //{
        //    var ver = Version.GetVersion(d);
        //    if (ver != null) return new VdbVariantVersionDef(ver, VariantFile);
        //    return null;
        //}

        public string GetAlterSql()
        {
            StringWriter sw = new StringWriter();
            ISqlDumper dmp = Version.Db.Dialect.CreateDumper(sw);
            GetAlterSql(dmp);
            return sw.ToString();
        }

        public DatabaseStructure LoadStructure()
        {
            var dbs = DatabaseStructure.Load(Version.GetFile());
            var mod = new VariantDef(VariantFile);
            mod.RunTransform(dbs);
            return dbs;
        }

        public void GetAlterSql(ISqlDumper dmp)
        {
            VersionDef current = Version;
            VersionDef prev = Version.GetVersion(-1);

            var vardef = new VariantDef(VariantFile);
            DatabaseStructure curstruct = current.LoadStructure(vardef);
            DatabaseStructure prevstruct = prev.LoadStructure(vardef);
            ISqlDialect dialect = curstruct.Dialect ?? Version.Db.Dialect;

            DbDiffOptions opts = new DbDiffOptions();
            //if (prev != null) dmp.TargetDb = new DbDefSource(DatabaseStructure.Load(prev.GetFile()), DbDefSource.ReadOnly.Flag);
            opts.AllowRecreateTable = true;
            if (prevstruct == null) dmp.CreateDatabaseObjects(curstruct);
            else dmp.AlterDatabase(prevstruct, curstruct, opts, new Plugin.dbmodel.DbDefSource(prevstruct, Plugin.dbmodel.DbDefSource.ReadOnly.Flag), null);

            current.SetVersionSql(dmp);
        }
    }
}
