using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.IO;
using System.Xml;
using System.Windows.Forms;

namespace Plugin.versiondb
{
    public class VersionDb
    {
        internal VersionDbProperties m_props = new VersionDbProperties();
        public List<VersionDef> Versions = new List<VersionDef>();
        internal string m_file;
        DateTime? m_fileTimeStamp = null;

        public const string DEFAULT_GET_VERSION = "SELECT Value\nFROM Variables\nWHERE Name='dbversion'";
        public const string DEFAULT_SET_VERSION = "UPDATE Variables\nSET Value='#VERSION#'\nWHERE Name='dbversion'";

        public VersionDb(string file)
        {
            m_file = file;
            Reload();
        }

        public VersionDb(VersionDbProperties props)
        {
            m_props = props;
        }

        public void Reload()
        {
            var fi = new FileInfo(m_file);
            if (!fi.Exists) return;
            if (fi.LastWriteTime <= m_fileTimeStamp) return;

            var defs = new Dictionary<string, VersionDef>();
            foreach (var def in Versions)
            {
                defs[def.GroupId] = def;
            }
            Versions.Clear();
            XmlDocument doc = new XmlDocument();
            doc.Load(m_file);
            foreach (XmlElement xver in doc.DocumentElement.SelectNodes("Version"))
            {
                string gid = xver.GetAttribute("groupid");
                if (gid != null && defs.ContainsKey(gid))
                {
                    defs[gid].Load(xver);
                    Versions.Add(defs[gid]);
                }
                else
                {
                    Versions.Add(new VersionDef(this, xver));
                }
            }
            m_props = new VersionDbProperties();
            m_props.LoadProperties(doc.DocumentElement.FindElement("Properties"));
            m_fileTimeStamp = new FileInfo(m_file).LastWriteTime;
        }

        public void Save()
        {
            XmlDocument doc = XmlTool.CreateDocument("VersionDb");
            foreach (var ver in Versions)
            {
                ver.Save(doc.DocumentElement.AddChild("Version"));
            }
            m_props.SaveProperties(doc.DocumentElement.AddChild("Properties"));
            doc.Save(m_file);
            m_fileTimeStamp = new FileInfo(m_file).LastWriteTime;
        }

        public string GetLastVersionFile()
        {
            Reload();
            if (Versions.Count > 0)
            {
                return Versions[Versions.Count - 1].GetFile();
            }
            return null;
        }

        public void AddVersion(string version)
        {
            Reload();
            DatabaseStructure dbs;
            if (Versions.Count > 0) dbs = DatabaseStructure.Load(Versions[Versions.Count - 1].GetFile());
            else dbs = new DatabaseStructure();
            dbs.SetProps(m_props);
            var vd = new VersionDef(this, version);
            Versions.Add(vd);
            WantVersionsDirectory();
            dbs.Save(vd.GetFile());
            Save();
        }

        internal void WantVersionsDirectory()
        {
            try { Directory.CreateDirectory(VersionsDirectory); }
            catch { }
        }

        public void DeleteVersion(VersionDef version)
        {
            Reload();
            Versions.Remove(version);
            Save();
        }

        public string VersionsDirectory { get { return m_file + ".versions"; } }
        public string DiagramsDirectory { get { return m_file + ".diagrams"; } }
        public string VariantsDirectory { get { return m_file + ".variants"; } }

        public string GetVersionFile(string name)
        {
            return Path.Combine(VersionsDirectory, "version_" + name + ".ddf");
        }

        public ISqlDialect Dialect { get { return m_props.Dialect; } }

        public void ChangedProps()
        {
            Cursor last = Cursor.Current;
            Cursor.Current = Cursors.WaitCursor;
            foreach (var ver in Versions)
            {
                DatabaseStructure dbs = DatabaseStructure.Load(ver.GetFile());
                dbs.SetProps(m_props);
                dbs.Save(ver.GetFile());
            }
            Cursor.Current = last;
        }

        public void CreateDefaultVariableTable()
        {
            AddVersion("1");
            var file = Versions[0].GetFile();
            var dbs = DatabaseStructure.Load(file);
            var tbl = new TableStructure();
            dbs.Tables.Add(tbl);
            tbl.FullName = new NameWithSchema("Variables");
            tbl._Columns.Add(new ColumnStructure { ColumnName = "Name", DataType = new DbTypeString { Length = 50 } });
            tbl._Columns.Add(new ColumnStructure { ColumnName = "Value", DataType = new DbTypeString { Length = 250 } });
            var pk = new PrimaryKey { Name = "PK_Variables" };
            pk.Columns.Add(new ColumnReference("Name"));
            tbl._Constraints.Add(pk);
            tbl.FixedData = new InMemoryTable(tbl);
            tbl.FixedData.Rows.Add(new ArrayDataRecord(tbl, new object[] { "dbversion", "0" }));
            dbs.Save(file);
            if (String.IsNullOrEmpty(m_props.GetVersionSql)) m_props.GetVersionSql = DEFAULT_GET_VERSION;
            if (String.IsNullOrEmpty(m_props.SetVersionSql)) m_props.SetVersionSql = DEFAULT_SET_VERSION;
        }

        public string FileName { get { return m_file; } }

        public VersionDef FindVersion(string name)
        {
            foreach (var ver in Versions)
            {
                if (ver.Name == name) return ver;
            }
            return null;
        }
    }

    public class VersionDef
    {
        [XmlAttrib("name")]
        public string Name { get; set; }
        [XmlAttrib("groupid")]
        public string GroupId { get; set; }

        internal VersionDb Db;

        public VersionDef(VersionDb db, string name)
        {
            Db = db;
            Name = name;
            GroupId = Guid.NewGuid().ToString();
        }

        public VersionDef(VersionDb db, XmlElement xml)
        {
            Db = db;
            Load(xml);
        }

        public void Save(XmlElement xml)
        {
            this.SavePropertiesCore(xml);
        }

        public string GetFile()
        {
            return Db.GetVersionFile(Name);
        }

        public void DeleteVersion()
        {
            Db.DeleteVersion(this);
        }

        public void Rename(string newname)
        {
            Db.Reload();
            File.Move(GetFile(), Db.GetVersionFile(newname));
            Name = newname;
            Db.Save();
        }

        public void Load(XmlElement xml)
        {
            this.LoadPropertiesCore(xml);
            if (GroupId == null) GroupId = Guid.NewGuid().ToString();
        }

        public VersionDef GetVersion(int d)
        {
            int index = Db.Versions.IndexOf(this) + d;
            if (index >= 0 && index < Db.Versions.Count) return Db.Versions[index];
            return null;
        }

        public void SetVersionSql(ISqlDumper dmp)
        {
            if (!String.IsNullOrEmpty(Db.m_props.SetVersionSql)) dmp.PutCmd(Db.m_props.SetVersionSql.Replace("#VERSION#", Name));
        }

        public string[] GetAlterSqls()
        {
            PolyStringSqOutputStream sqlo = new PolyStringSqOutputStream();
            ISqlDumper dmp = Db.Dialect.CreateDumper(sqlo, SqlFormatProperties.Default);
            GetAlterSql(dmp);
            return sqlo.Lines.ToArray();
        }

        public void GetAlterSql(ISqlDumper dmp)
        {
            DumpSubFolder(dmp, "before");
            VersionDef current = this;
            VersionDef prev = current.GetVersion(-1);
            VersionDb vdb = current.Db;

            DatabaseStructure curstruct = DatabaseStructure.Load(current.GetFile());
            DatabaseStructure prevstruct = null;
            if (prev != null) prevstruct = DatabaseStructure.Load(prev.GetFile());

            ISqlDialect dialect = vdb.Dialect;

            DbDiffOptions opts = new DbDiffOptions();
            //if (prev != null) dmp.TargetDb = new DbDefSource(DatabaseStructure.Load(prev.GetFile()), DbDefSource.ReadOnly.Flag);
            opts.AllowRecreateTable = true;
            if (prevstruct == null) dmp.CreateDatabaseObjects(curstruct);
            else dmp.AlterDatabase(prevstruct, curstruct, opts, new Plugin.dbmodel.DbDefSource(prevstruct, Plugin.dbmodel.DbDefSource.ReadOnly.Flag), AddSubFoldersToPlan);

            DumpSubFolder(dmp, "after");

            current.SetVersionSql(dmp);
        }

        private void AddSubFoldersToPlan(AlterPlan plan)
        {
            DumpSubFolder(plan, "before-nofk", AlterPlan.BEFORE_ORDERGROUP);
            DumpSubFolder(plan, "after-nofk", AlterPlan.AFTER_ORDERGROUP);
        }

        private void DumpSubFolder(AlterPlan plan, string subfolder, int order)
        {
            string dir = GetPrivateSubFolder(subfolder);
            if (!Directory.Exists(dir)) return;
            string[] files = Directory.GetFiles(dir);
            Array.Sort(files);
            foreach (string f in files)
            {
                if (!f.ToLower().EndsWith(".sql")) continue;
                using (StreamReader sr = new StreamReader(f))
                {
                    foreach (string sql in QueryTools.GoSplit(sr))
                    {
                        plan.CustomAction(sql, order);
                    }
                }
            }
        }

        private void DumpSubFolder(ISqlDumper dmp, string subfolder)
        {
            string dir = GetPrivateSubFolder(subfolder);
            if (!Directory.Exists(dir)) return;
            string[] files = Directory.GetFiles(dir);
            Array.Sort(files);
            foreach (string f in files)
            {
                if (!f.ToLower().EndsWith(".sql")) continue;
                using (StreamReader sr = new StreamReader(f))
                {
                    foreach (string sql in QueryTools.GoSplit(sr))
                    {
                        dmp.WriteRaw(sql);
                        dmp.EndCommand();
                    }
                }
            }
        }

        public string GetAlterSql()
        {
            StringWriter sw = new StringWriter();
            ISqlDumper dmp = Db.Dialect.CreateDumper(sw);
            GetAlterSql(dmp);
            return sw.ToString();
        }

        public string GetPrivateSubFolder(string scriptsSubDir)
        {
            return GetFile() + "." + scriptsSubDir;
        }
    }
}
