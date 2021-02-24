using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Xml;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace DatAdmin
{
    public class DatabaseStructure : AbstractObjectStructure, IDatabaseStructure, IAlterProcessor, IExplicitXmlPersistent
    {
        public TableCollection Tables;
        public TableCollection ViewAsTables;
        public Dictionary<string, ISpecificObjectCollection> SpecificObjects = new Dictionary<string, ISpecificObjectCollection>();
        public SchemaCollection Schemata;
        public CollationCollection Collations;
        public DomainCollection Domains;
        public CharacterSetCollection CharacterSets;

        private bool m_initialized;

        //public DatabaseStructureMembers FilledMembers = new DatabaseStructureMembers();

        public ISqlDialect Dialect { get; set; }
        public bool ForceSingleSchema { get; set; }

        public DatabaseStructure()
        {
            Initialize();
        }

        private void Initialize()
        {
            if (m_initialized) return;
            Tables = new TableCollection(this);
            ViewAsTables = new TableCollection(this);
            Schemata = new SchemaCollection(this);
            Collations = new CollationCollection(this);
            Domains = new DomainCollection(this);
            CharacterSets = new CharacterSetCollection(this);
            m_initialized = true;
        }

        public TableStructure AddTable(ITableStructure ts, bool reuseGroupId)
        {
            TableStructure tbl = new TableStructure(ts);
            if (!reuseGroupId) tbl.GroupId = Guid.NewGuid().ToString();
            Tables.Add(tbl);
            return tbl;
        }

        public Constraint FindOrCreateConstraint(IConstraint cnt)
        {
            var t = FindTable(cnt.Table.FullName);
            if (t == null) t = AddTable(new TableStructure { FullName = cnt.Table.FullName }, true);
            Constraint res = t.FindConstraint(cnt) as Constraint;
            if (res == null)
            {
                res = Constraint.CreateCopy(cnt);
                t._Constraints.Add(res);
            }
            return res;
        }

        public ColumnStructure FindOrCreateColumn(IColumnStructure col)
        {
            var t = FindTable(col.Table.FullName);
            if (t == null) t = AddTable(new TableStructure { FullName = col.Table.FullName }, false);
            ColumnStructure res = t.FindColumn(col.ColumnName) as ColumnStructure;
            if (res == null)
            {
                res = new ColumnStructure(col);
                res.GroupId = Guid.NewGuid().ToString();
                t._Columns.Add(res);
            }
            return res;
        }

        public void AddObject(IAbstractObjectStructure obj, bool reuseGrouId)
        {
            var col = obj as IColumnStructure;
            if (col != null)
            {
                var t = FindTable(col.Table.FullName);
                if (t == null) t = AddTable(new TableStructure { FullName = col.Table.FullName }, true);
                t.AddColumn(col, reuseGrouId);
                return;
            }
            var cnt = obj as IConstraint;
            if (cnt != null)
            {
                var t = FindTable(cnt.Table.FullName);
                if (t == null) t = AddTable(new TableStructure { FullName = cnt.Table.FullName }, true);
                var newcnt = Constraint.CreateCopy(cnt);
                if (!reuseGrouId) newcnt.GroupId = Guid.NewGuid().ToString();
                t._Constraints.Add(newcnt);
                return;
            }
            var tbl = obj as ITableStructure;
            if (tbl != null)
            {
                AddTable(tbl, reuseGrouId);
                return;
            }
            var spe = obj as ISpecificObjectStructure;
            if (spe != null)
            {
                AddSpecificObject(spe, reuseGrouId);
                return;
            }
            var sch = obj as ISchemaStructure;
            if (sch != null)
            {
                AddSchema(sch, reuseGrouId);
                return;
            }
            var dom = obj as IDomainStructure;
            if (dom != null)
            {
                AddDomain(dom, reuseGrouId);
                return;
            }
        }

        //public void AlterTable(ITableStructure oldTable, ITableStructure newTable, DbDiffOptions opts)
        //{
        //    DbDiffTool.AlterTable(oldTable, newTable, opts, this);
        //}

        //public TableStructure AlterTable(NameWithSchema tableName, ITableStructure newTable)
        //{
        //    TableStructure tbl = (TableStructure)Tables[tableName];
        //    tbl.LoadFrom(newTable);
        //    tbl.RepairInconsistency();
        //    return tbl;
        //}

        public void DropTable(NameWithSchema tableName)
        {
            Tables.RemoveIf(t => t.FullName == tableName);
            foreach (TableStructure t in Tables)
            {
                t.DropReferencesTo(tableName);
            }
        }

        public void Save(string file)
        {
            using (FileStream fw = new FileStream(file, FileMode.Create))
            {
                Save(fw);
            }
        }

        public void Save(XmlElement xml)
        {
            if (Dialect != null) xml.SetAttribute("dialect", XmlTool.ValueToString(typeof(ISqlDialect), Dialect));
            if (ForceSingleSchema) xml.SetAttribute("singleschema", "1");
            SaveBase(xml);
            foreach (TableStructure table in Tables)
            {
                XmlElement tx = XmlTool.AddChild(xml, "Table");
                table.Save(tx);
            }
            foreach (string objtype in SpecificObjects.Keys)
            {
                ISpecificRepresentation repr = SpecificRepresentationAddonType.Instance.FindRepresentation(objtype);
                foreach (SpecificObjectStructure obj in SpecificObjects[objtype])
                {
                    XmlElement tx = XmlTool.AddChild(xml, repr.XmlElementName ?? "Specific");
                    if (repr.XmlElementName == null) tx.SetAttribute("objtype", objtype);
                    obj.Save(tx);
                }
            }
            foreach (DomainStructure dom in Domains)
            {
                XmlElement dx = XmlTool.AddChild(xml, "Domain");
                dom.Save(dx);
            }
        }

        public void Save(Stream fw)
        {
            XmlDocument doc = XmlTool.CreateDocument("Database");
            XmlElement root = doc.DocumentElement;
            Save(root);
            doc.Save(fw);
        }

        public SpecificObjectCollection SpecByType(string type)
        {
            if (!SpecificObjects.ContainsKey(type)) SpecificObjects[type] = new SpecificObjectCollection(this);
            return (SpecificObjectCollection)SpecificObjects[type];
        }

        public SpecificObjectStructure AddSpecificObject(ISpecificObjectStructure obj, bool reuseGroupId)
        {
            SpecificObjectStructure res = new SpecificObjectStructure(obj);
            if (!reuseGroupId) res.GroupId = Guid.NewGuid().ToString();
            SpecByType(res.ObjectType).Add(res);
            return res;
        }

        public SpecificObjectStructure AddSpecificObject(string objtype, string dialect, NameWithSchema name)
        {
            SpecificObjectStructure res = new SpecificObjectStructure();
            res.ObjectName = name;
            res.ObjectType = objtype;
            res.SpecificDialect = dialect;
            SpecByType(res.ObjectType).Add(res);
            return res;
        }

        /// <summary>
        /// creates deep copy of database structure and filters database members
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dbmem"></param>
        public DatabaseStructure(IDatabaseStructure src, DatabaseStructureMembers dbmem)
            : base(src)
        {
            Initialize();
            Dialect = src.Dialect;
            ForceSingleSchema = src.ForceSingleSchema;
            foreach (ITableStructure o in src.Tables) if (dbmem == null || dbmem.HasTableDetails(o.FullName)) Tables.Add(new TableStructure(o));
            foreach (ISchemaStructure o in src.Schemata) Schemata.Add(new SchemaStructure(o));
            foreach (ICollationStructure o in src.Collations) Collations.Add(new CollationStructure(o));
            foreach (ICharacterSetStructure o in src.CharacterSets) CharacterSets.Add(new CharacterSetStructure(o));
            foreach (IDomainStructure o in src.Domains) Domains.Add(new DomainStructure(o));
            foreach (string objtype in src.SpecificObjects.Keys)
            {
                var col = new SpecificObjectCollection(this);
                SpecificObjects[objtype] = col;
                foreach (ISpecificObjectStructure o in src.SpecificObjects[objtype])
                {
                    if (dbmem == null || dbmem.HasSpecificObjectDetails(objtype, o.ObjectName))
                    {
                        col.Add(new SpecificObjectStructure(o));
                    }
                }
            }
            //FilledMembers = dbmem ?? src.FilledMembers.Clone();
        }
        public DatabaseStructure(IDatabaseStructure src)
            : this(src, null)
        {
            Initialize();
        }

        public DatabaseStructure(XmlElement xml)
            : base(xml)
        {
            Initialize();
            if (xml.HasAttribute("dialect")) Dialect = (ISqlDialect)XmlTool.ValueFromString(typeof(ISqlDialect), xml.GetAttribute("dialect"));
            if (xml.HasAttribute("singleschema")) ForceSingleSchema = xml.GetAttribute("singleschema") == "1";
            foreach (XmlElement child in xml)
            {
                switch (child.Name)
                {
                    case "Table":
                        Tables.Add(new TableStructure(child));
                        break;
                    case "Specific":
                        AddSpecificObject(new SpecificObjectStructure(child, child.GetAttribute("objtype")), true);
                        break;
                    case "Schema":
                        Schemata.Add(new SchemaStructure(child));
                        break;
                    case "Domain":
                        Domains.Add(new DomainStructure(child));
                        break;
                    case "Collation":
                        Collations.Add(new CollationStructure(child));
                        break;
                    case "CharacterSet":
                        CharacterSets.Add(new CharacterSetStructure(child));
                        break;
                    default:
                        {
                            ISpecificRepresentation repr = SpecificRepresentationAddonType.Instance.FindByElement(child.Name);
                            if (repr == null) throw new Exception("DAE-00243 Unexpected element:" + child.Name);
                            AddSpecificObject(new SpecificObjectStructure(child, repr.ObjectType), true);
                        }
                        break;
                }
            }
        }

        public static DatabaseStructure Load(string file)
        {
            using (FileStream fr = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                return Load(fr);
            }
        }

        public static DatabaseStructure Load(Stream fr)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(fr);
            return Load(doc.DocumentElement);
        }

        public static DatabaseStructure Load(XmlElement xml)
        {
            DatabaseStructure res = null;
            res = new DatabaseStructure(xml);
            res.MarkAllFilled();
            //res.AfterLoadFix();
            return res;
        }

        //private void AfterLoadFix()
        //{
        //    foreach (TableStructure table in Tables)
        //    {
        //        table.AfterLoadFix();
        //    }
        //}

        #region IDatabaseStructure Members

        ITableCollection IDatabaseStructure.Tables
        {
            get { return Tables; }
        }

        ITableCollection IDatabaseStructure.ViewAsTables
        {
            get { return ViewAsTables; }
        }

        IDictionary<string, ISpecificObjectCollection> IDatabaseStructure.SpecificObjects
        {
            get { return SpecificObjects; }
        }

        ISchemaCollection IDatabaseStructure.Schemata
        {
            get { return Schemata; }
        }

        ICollationCollection IDatabaseStructure.Collations
        {
            get { return Collations; }
        }

        IDomainCollection IDatabaseStructure.Domains
        {
            get { return Domains; }
        }

        ICharacterSetCollection IDatabaseStructure.CharacterSets
        {
            get { return CharacterSets; }
        }

        //DatabaseStructureMembers IDatabaseStructure.FilledMembers
        //{
        //    get { return FilledMembers; }
        //}

        #endregion

        public TableStructure AddTable(NameWithSchema name)
        {
            TableStructure res = new TableStructure();
            //res.FilledMembers = TableStructureMembers.Name;
            res.FullName = name;
            Tables.Add(res);
            return res;
        }

        //public void AutoFillRefs()
        //{
        //    foreach (TableStructure tbl in Tables)
        //    {
        //        tbl._ReferencedFrom.Clear();
        //    }
        //    foreach (TableStructure src in Tables)
        //    {
        //        foreach (IForeignKey fk in src.GetConstraints<IForeignKey>())
        //        {
        //            try
        //            {
        //                TableStructure dst = (TableStructure)Tables[fk.PrimaryKeyTable];
        //                dst._ReferencedFrom.Add(new ForeignKey(fk));
        //            }
        //            catch
        //            {
        //                continue;
        //            }
        //        }
        //    }
        //}

        public void RunNameTransformation(INameTransformation nameTransform)
        {
            foreach (TableStructure tbl in Tables)
            {
                NameWithSchema newname = nameTransform.RenameObject(tbl.FullName, "table"); ;
                foreach (ColumnStructure col in tbl.Columns)
                {
                    string newcolname = nameTransform.RenameColumn(tbl.FullName, col.ColumnName);
                    col.ColumnName = newcolname;
                    if (col.Domain != null) col.Domain = nameTransform.RenameObject(col.Domain, "domain");
                }
                tbl.FullName = nameTransform.RenameObject(tbl.FullName, "table");
            }
            foreach (DomainStructure dom in Domains)
            {
                dom.FullName = nameTransform.RenameObject(dom.FullName, "domain");
            }
            foreach (string objtype in SpecificObjects.Keys)
            {
                foreach (SpecificObjectStructure spec in SpecificObjects[objtype])
                {
                    NameWithSchema newname = nameTransform.RenameObject(spec.ObjectName, objtype);
                    spec.ObjectName = newname;
                    if (spec.RelatedTable != null)
                    {
                        spec.RelatedTable = nameTransform.RenameObject(spec.RelatedTable, "table");
                    }
                    if (spec.DependsOn != null)
                    {
                        foreach (var dep in spec.DependsOn)
                        {
                            dep.Name = nameTransform.RenameObject(dep.Name, dep.ObjectType);
                        }
                    }
                }
            }
            foreach (TableStructure tbl in Tables)
            {
                foreach (Constraint cnt in tbl.Constraints)
                {
                    NameWithSchema oldtable = cnt.Table.FullName;
                    //cnt.Table = nameTransform.RenameObject(cnt.Table, "table");
                    var col = cnt as ColumnsConstraint;
                    if (col != null)
                    {
                        for (int i = 0; i < col.Columns.Count; i++)
                        {
                            try
                            {
                                col.Columns[i] = new ColumnReference(nameTransform.RenameColumn(oldtable, col.Columns[i].ColumnName), col.Columns[i].SpecificData);
                            }
                            catch (KeyNotFoundException)
                            {
                                Logging.Warning("column {0}.{1} referenced but not exists", oldtable, col.Columns[i]);
                            }
                        }
                    }
                    var fk = cnt as ForeignKey;
                    if (fk != null)
                    {
                        for (int i = 0; i < fk.PrimaryKeyColumns.Count; i++)
                        {
                            try
                            {
                                fk.PrimaryKeyColumns[i] = new ColumnReference(
                                    nameTransform.RenameColumn(fk.PrimaryKeyTable, fk.PrimaryKeyColumns[i].ColumnName),
                                    fk.PrimaryKeyColumns[i].SpecificData);
                            }
                            catch (KeyNotFoundException)
                            {
                                Logging.Warning("column {0}.{1} referenced but not exists", fk.PrimaryKeyTable, fk.PrimaryKeyColumns[i]);
                            }
                        }
                        try
                        {
                            fk.PrimaryKeyTable = nameTransform.RenameObject(fk.PrimaryKeyTable, "table");
                        }
                        catch (KeyNotFoundException)
                        {
                            Logging.Warning("table {0} referenced but not exists", fk.PrimaryKeyTable);
                        }
                    }
                    cnt.Name = nameTransform.RenameConstraint(cnt);
                }
            }
        }

        public SpecificObjectStructure FindSpecificObject(ISpecificObjectStructure obj)
        {
            return (SpecificObjectStructure)DatabaseStructureExtension.FindSpecificObject(this, obj);
        }

        public void AlterSpecificObject(ISpecificObjectStructure olddef, ISpecificObjectStructure newdef)
        {
            SpecificObjectStructure old = FindSpecificObject(olddef);
            old.Comment = newdef.Comment;
            old.CreateSql = newdef.CreateSql;
            old.ObjectName = newdef.ObjectName;
            old.SpecificData.Clear();
            old.SpecificData.AddAll(newdef.SpecificData);
            old.SpecificDialect = newdef.SpecificDialect;
            old.DependsOn = null; // clear dependency information
        }

        public void DropSpecificObject(ISpecificObjectStructure obj)
        {
            SpecificObjectStructure spec = FindSpecificObject(obj);
            if (spec != null) ((SpecificObjectCollection)SpecificObjects[spec.ObjectType]).Remove(spec);
        }

        public void RenameSpecificObject(ISpecificObjectStructure obj, string newname)
        {
            SpecificObjectStructure spec = FindSpecificObject(obj);
            if (spec != null) spec.ObjectName = new NameWithSchema(spec.ObjectName.Schema, newname);
        }

        public void ChangeSpecificObjectSchema(ISpecificObjectStructure obj, string newschema)
        {
            SpecificObjectStructure spec = FindSpecificObject(obj);
            if (spec != null) spec.ObjectName = new NameWithSchema(newschema, spec.ObjectName.Name);
        }

        public override void AddAllObjects(List<IAbstractObjectStructure> res)
        {
            base.AddAllObjects(res);
            foreach (SpecificObjectStructure obj in this.GetAllSpecificObjects()) obj.AddAllObjects(res);
            Tables.AddAllObjects(res);
            Schemata.AddAllObjects(res);
            Collations.AddAllObjects(res);
            Domains.AddAllObjects(res);
            CharacterSets.AddAllObjects(res);
        }

        #region IAlterProcessor Members

        public void ChangeTableSchema(NameWithSchema tbl, string schema)
        {
            foreach (AbstractObjectStructure obj in this.GetAllObjects()) obj.NotifyRenameTable(tbl, new NameWithSchema(schema, tbl.Name));
        }

        public void RenameTable(NameWithSchema tbl, string newname)
        {
            foreach (AbstractObjectStructure obj in this.GetAllObjects()) obj.NotifyRenameTable(tbl, new NameWithSchema(tbl.Schema, newname));
        }

        public void CreateColumn(IColumnStructure column, IEnumerable<IConstraint> constrains)
        {
            var t = FindTable(column.Table.FullName);
            if (t != null)
            {
                t.AddColumn(column, false);
            }
            if (constrains != null) foreach (var cnt in constrains) CreateConstraint(cnt);
        }

        public void DropColumn(IColumnStructure column)
        {
            ColumnStructure colcopy = new ColumnStructure(column);
            colcopy.SetDummyTable(column.Table.FullName);
            // we must send copy of column, because when dropping column, column.Table will be null
            foreach (AbstractObjectStructure obj in this.GetAllObjects()) obj.NotifyDropColumn(colcopy);
            foreach (TableStructure tbl in Tables) tbl.DropEmptyConstraints();
        }

        public void RenameColumn(IColumnStructure column, string newcol)
        {
            string oldcol = column.ColumnName;
            foreach (AbstractObjectStructure obj in this.GetAllObjects()) obj.NotifyRenameColumn(column.Table.FullName, oldcol, newcol);
        }

        public TableStructure FindTable(NameWithSchema name)
        {
            return (TableStructure)DatabaseStructureExtension.FindTable(this, name);
        }

        public SchemaStructure FindSchema(string name)
        {
            return (SchemaStructure)DatabaseStructureExtension.FindSchema(this, name);
        }

        public TableStructure FindOrCreateTable(NameWithSchema name)
        {
            var res = FindTable(name);
            if (res == null) res = AddTable(name);
            return res;
        }

        public DomainStructure FindDomain(NameWithSchema name)
        {
            return (DomainStructure)DatabaseStructureExtension.FindDomain(this, name);
        }

        public void ChangeColumn(IColumnStructure oldcol, IColumnStructure newcol, IEnumerable<IConstraint> constraints)
        {
            var col = this.FindColumn(oldcol) as ColumnStructure;
            col.AssignFrom(newcol);
            this.CreateConstraints(constraints);
            //if (oldcol.ColumnName != newcol.ColumnName) RenameColumn(oldcol, newcol.ColumnName);
            //FindTable(oldcol.Table.FullName).ChangeColumnDefinition(newcol);
            //foreach (AbstractObjectStructure obj in this.GetAllObjects()) obj.NotifyChangeColumnType_RefsOnly(newcol);
        }

        public void ReorderColumns(NameWithSchema table, List<string> newColumnOrder)
        {
            var t = FindTable(table);
            if (t != null)
            {
                t.ReorderColumns(newColumnOrder);
            }
        }

        public void CreateConstraint(IConstraint constraint)
        {
            var newcnt = Constraint.CreateCopy(constraint);
            newcnt.GroupId = Guid.NewGuid().ToString();
            var t = FindTable(constraint.Table.FullName);
            if (t != null)
            {
                t._Constraints.Add(newcnt);
            }
        }

        public void DropConstraint(IConstraint constraint)
        {
            var t = FindTable(constraint.Table.FullName);
            if (t != null)
            {
                t._Constraints.RemoveIf(c => c.Name == constraint.Name && c.Type == constraint.Type);
            }
        }

        public void RenameConstraint(IConstraint constraint, string newname)
        {
            ((Constraint)this.FindConstraint(constraint)).Name = newname;
        }

        public void RecreateTable(ITableStructure src, ITableStructure dst)
        {
            DbDiffTool.DecomposeAlterTable(this, src, dst, new DbDiffOptions());
        }

        public AlterProcessorCaps AlterCaps
        {
            get { return new AlterProcessorCaps { AllFlags = true, RecreateTable = false, ForceAbsorbPrimaryKey = false }; }
        }

        public void ChangeSpecificObject(ISpecificObjectStructure osrc, ISpecificObjectStructure odst)
        {
            var obj = FindSpecificObject(osrc) as SpecificObjectStructure;
            obj.AssignFrom(odst);
        }

        public void CustomAction(string query) { }

        public void CreateTable(ITableStructure tsrc)
        {
            AddTable(tsrc, false);
        }

        public void DropTable(ITableStructure tdst)
        {
            DropTable(tdst.FullName);
        }

        public void CreateSpecificObject(ISpecificObjectStructure osrc)
        {
            AddSpecificObject(osrc ,false);
        }

        public void AlterTableOptions(ITableStructure table, Dictionary<string, string> options)
        {
            var t = FindTable(table.FullName);
            if (t != null)
            {
                t.SpecificData.AddAll(options);
            }
        }

        public void AlterDatabaseOptions(string dbname, Dictionary<string, string> options)
        {
            SpecificData.AddAll(options);
        }

        public void DropSchema(ISchemaStructure schema)
        {
            Schemata.RemoveIf(s => s.SchemaName == schema.SchemaName);
        }

        public SchemaStructure AddSchema(ISchemaStructure schema, bool reuseGroupId)
        {
            var newsch = new SchemaStructure(schema);
            if (!reuseGroupId) newsch.GroupId = Guid.NewGuid().ToString();
            Schemata.Add(newsch);
            return newsch;
        }

        public void CreateSchema(ISchemaStructure schema)
        {
            AddSchema(schema, false);
        }

        public void RenameSchema(ISchemaStructure schema, string newname)
        {
            var sch = Schemata.FirstOrDefault(s => s.SchemaName == schema.SchemaName) as SchemaStructure;
            sch.SchemaName = newname;
            RunNameTransformation(new RenameSchemaTransform(schema.SchemaName, newname));
        }

        public void AlterTable(ITableStructure src, ITableStructure dst, out bool processed)
        {
            processed = false;
        }

        public void ChangeDomainSchema(NameWithSchema dom, string schema)
        {
            foreach (AbstractObjectStructure obj in this.GetAllObjects()) obj.NotifyRenameDomain(dom, new NameWithSchema(schema, dom.Name));
        }

        public void RenameDomain(NameWithSchema dom, string newname)
        {
            foreach (AbstractObjectStructure obj in this.GetAllObjects()) obj.NotifyRenameDomain(dom, new NameWithSchema(dom.Schema, newname));
        }

        public void ChangeDomain(IDomainStructure dold, IDomainStructure dnew)
        {
            var dom = FindDomain(dold.FullName);
            if (dom == null) throw new InternalError("DAE-00037 Domain doesn't exist: " + dold.FullName.ToString());
            dom.AssignFrom(dnew);

            //Domains.RemoveIf(dom => dom.FullName == dold.FullName);
            //Domains.Add(new DomainStructure(dnew));

            // change domain references
            foreach (TableStructure tbl in Tables)
            {
                foreach (ColumnStructure col in tbl.Columns)
                {
                    if (col.Domain == dold.FullName)
                    {
                        col.Domain = dnew.FullName;
                        col.DataType = dnew.DataType;
                    }
                }
            }
        }

        public void UpdateData(ITableStructure table, DataScript script, ISaveDataProgress progress)
        {
            var tbl = Tables[table.FullName] as TableStructure;
            tbl.UpdateData(script);
        }

        public void UpdateData(MultiTableUpdateScript script, ISaveDataProgress progress)
        {
            throw new NotImplementedError("DAE-00104");
        }

        #endregion

        public void AddDomain(IDomainStructure domain, bool reuseGroupId)
        {
            var newdom = new DomainStructure(domain);
            if (!reuseGroupId) newdom.GroupId = Guid.NewGuid().ToString();
            Domains.Add(newdom);
        }

        public void CreateDomain(IDomainStructure domain)
        {
            AddDomain(domain, false);
        }

        public void DropDomain(IDomainStructure domain)
        {
            Domains.RemoveIf(dom => dom.FullName == domain.FullName);
            // remove references to domain
            foreach (TableStructure tbl in Tables)
            {
                foreach (ColumnStructure col in tbl.Columns)
                {
                    if (col.Domain == domain.FullName) col.Domain = null;
                }
            }
        }

        /// cleanUp operation - deletes incorrect references
        public void CleanUp()
        {
            var tbls = new Dictionary<NameWithSchema, TableStructure>();
            foreach (TableStructure tbl in Tables) tbls[tbl.FullName] = tbl;
            foreach (TableStructure tbl in Tables) tbl.CleanUp(tbls);
        }

        public DbDefProperties GetProps()
        {
            return new DbDefProperties
            {
                Dialect = Dialect,
                ForceSingleSchema = ForceSingleSchema
            };
        }

        public void SetProps(DbDefProperties props)
        {
            Dialect = props.Dialect;
            ForceSingleSchema = props.ForceSingleSchema;
        }

        //public IDatabaseSource TargetDb { get { return null; } }

        public DomainStructure FindOrCreateDomain(IDomainStructure domain)
        {
            DomainStructure res = FindDomain(domain.FullName);
            if (res != null) return res;
            res = new DomainStructure(domain);
            Domains.Add(res);
            return res;
        }

        public void ChangeConstraint(IConstraint csrc, IConstraint cdst)
        {
            if (csrc.Table == null) return;
            var obj = (Constraint)this.FindConstraint(csrc);
            obj.AssignFrom(cdst);
        }

        public void ChangeSchema(ISchemaStructure ssrc, ISchemaStructure sdst)
        {
            var obj = (SchemaStructure)this.FindSchema(ssrc.SchemaName);
            obj.AssignFrom(sdst);
        }

        public SpecificObjectStructure FindOrCreateSpecificObject(ISpecificObjectStructure obj)
        {
            var res = FindSpecificObject(obj);
            if (res == null) res = AddSpecificObject(obj, false);
            return res;
        }

        public SchemaStructure FindOrCreateSchema(ISchemaStructure schema)
        {
            var res = FindSchema(schema.SchemaName);
            if (res == null) res = AddSchema(schema, false);
            return res;
        }

        public void MarkAllFilled()
        {
            foreach (TableStructure tbl in Tables)
            {
                tbl.FilledMembers = TableStructureMembers.All;
            }
        }

        public IEnumerable<SpecificObjectStructure> GetAllSpecificObjects()
        {
            foreach (string type in SpecificObjects.Keys)
            {
                foreach (var spec in SpecificObjects[type])
                {
                    yield return (SpecificObjectStructure)spec;
                }
            }
        }

        public void ClearDependencies()
        {
            foreach (var spec in GetAllSpecificObjects())
            {
                spec.DependsOn = null;
            }
        }

        public void DetectDependencies(bool missingOnly)
        {
            var dialect = Dialect;

            foreach (var spec in GetAllSpecificObjects())
            {
                if (missingOnly && spec.DependsOn != null) continue;
                spec.DependsOn = null;
                if (dialect != null)
                {
                    spec.DependsOn = dialect.DetectDependencies(spec);
                }
            }
        }

        public override FullDatabaseRelatedName GetName()
        {
            return new FullDatabaseRelatedName();
        }

        #region IExplicitXmlPersistent Members

        void IExplicitXmlPersistent.SaveToXml(XmlElement xml)
        {
            Save(xml);
        }

        void IExplicitXmlPersistent.LoadFromXml(XmlElement xml)
        {
            Load(xml);
        }

        #endregion
    }

    public class CleanUpLog : List<string>
    {
    }

    public class DbDefProperties : PropertyPageBase
    {
        [XmlElem]
        [TypeConverter(typeof(DialectTypeConverter))]
        [Category("s_general")]
        [DisplayName("s_dialect")]
        [ExpandDialectVersions]
        public ISqlDialect Dialect { get; set; }

        [XmlElem]
        [TypeConverter(typeof(YesNoTypeConverter))]
        [Category("s_general")]
        [DisplayName("s_force_single_schema")]
        public bool ForceSingleSchema { get; set; }
    }
}
