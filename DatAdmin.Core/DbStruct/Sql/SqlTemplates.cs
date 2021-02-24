using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;
using System.ComponentModel;

namespace DatAdmin
{
    public class SqlTemplates
    {
        public static void GenerateInsertFixedData(ISqlDumper dmp, ITableStructure ts)
        {
            if (ts.FixedData == null) return;
            foreach (var row in ts.FixedData.Rows)
            {
                dmp.PutCmd("^insert ^into %f (%,i) ^values (%,v)",
                    ts.FullName,
                    from c in ts.Columns select c.ColumnName,
                    row
                    );
            }
        }

        public static void GenerateInsertFixedData(ISqlDumper dmp, IDatabaseStructure db)
        {
            foreach (var ts in db.Tables)
            {
                GenerateInsertFixedData(dmp, ts);
            }
        }

        public static void GenerateInsertFixedData(ITableSource table, TextWriter tw, ISqlDialect dialect)
        {
            ITableStructure ts = table.LoadTableStructure(TableStructureMembers.All);
            ISqlDumper dmp = dialect.CreateDumper(tw);
            GenerateInsertFixedData(dmp, ts);
        }
    }

    public abstract class AppObjectSqlGeneratorBase : AddonBase, IAppObjectSqlGenerator
    {
        public static SqlFormatProperties TemplateFormatProps = new SqlFormatProperties { OmitVersionTests = true, CleanupSpecificObjectCode = false };

        public virtual void GenerateSql(IDatabaseSource db, FullDatabaseRelatedName objname, ISqlDumper dmp, ISqlDialect dialect) { }
        public virtual void GenerateSql(AppObject appobj, ISqlDumper dmp, ISqlDialect dialect)
        {
            GenerateSql(appobj.FindDatabaseConnection(), appobj.GetFullDatabaseRelatedName(), dmp, dialect);
        }

        public abstract bool SuitableFor(AppObject appobj);

        [Browsable(false)]
        public override AddonType AddonType
        {
            get { return AppObjectSqlGeneratorAddonType.Instance; }
        }

        [Browsable(false)]
        public IProgressInfo ProgressInfo { get; set; }

        public override string ToString()
        {
            return XmlTool.GetRegisterAttr(this).Title;
        }

        public event EventHandler ChangedProperties;

        public void CallChangedProperties()
        {
            if (ChangedProperties != null) ChangedProperties(this, EventArgs.Empty);
        }
    }

    [AppObjectSqlGenerator(Name = "create-table", Title = "CREATE TABLE")]
    public class GenSql_CreateTable : AppObjectSqlGeneratorBase
    {
        bool m_primaryKey = true;

        [XmlElem]
        [TypeConverter(typeof(YesNoTypeConverter))]
        public bool PrimaryKey
        {
            get { return m_primaryKey; }
            set { m_primaryKey = value; CallChangedProperties(); }
        }
        bool m_foreignKeys = true;

        [XmlElem]
        [TypeConverter(typeof(YesNoTypeConverter))]
        public bool ForeignKeys
        {
            get { return m_foreignKeys; }
            set { m_foreignKeys = value; CallChangedProperties(); }
        }
        bool m_indexes = true;

        [XmlElem]
        [TypeConverter(typeof(YesNoTypeConverter))]
        public bool Indexes
        {
            get { return m_indexes; }
            set { m_indexes = value; CallChangedProperties(); }
        }
        bool m_uniques = true;

        [XmlElem]
        [TypeConverter(typeof(YesNoTypeConverter))]
        public bool Uniques
        {
            get { return m_uniques; }
            set { m_uniques = value; CallChangedProperties(); }
        }
        bool m_defaultValues = true;

        [XmlElem]
        [TypeConverter(typeof(YesNoTypeConverter))]
        public bool DefaultValues
        {
            get { return m_defaultValues; }
            set { m_defaultValues = value; CallChangedProperties(); }
        }
        bool m_checks = true;

        [XmlElem]
        [TypeConverter(typeof(YesNoTypeConverter))]
        public bool Checks
        {
            get { return m_checks; }
            set { m_checks = value; CallChangedProperties(); }
        }

        public void GenerateCreateTable(ITableStructure ts, ISqlDumper dmp)
        {
            var tbl = new TableStructure(ts);
            RemoveUnwantedTableFeature(tbl);
            dmp.CreateTable(tbl);
        }

        protected void RemoveUnwantedTableFeature(TableStructure tbl)
        {
            tbl._Constraints.RemoveIf(DeleteConstraint);
            foreach (ColumnStructure col in tbl.Columns)
            {
                if (!DefaultValues) col.DefaultValue = null;
            }
        }

        private bool DeleteConstraint(IConstraint cnt)
        {
            if (cnt is IPrimaryKey) return !PrimaryKey;
            if (cnt is IForeignKey) return !ForeignKeys;
            if (cnt is ICheck) return !Checks;
            if (cnt is IIndex) return !Indexes;
            if (cnt is IUnique) return !Uniques;
            return false;
        }

        public void GenerateCreateTable(ITableSource table, ISqlDumper dmp, ISqlDialect dialect)
        {
            IMigrationProfile profile = dialect.CreateMigrationProfile();
            var tbl = new TableStructure(table.InvokeLoadStructure(TableStructureMembers.AllNoRefs));
            dialect.MigrateTable(tbl, profile, null);
            GenerateCreateTable(tbl, dmp);
        }

        public void GenerateCreateTable(ITableSource table, TextWriter tw, ISqlDialect dialect)
        {
            if (dialect == null) dialect = new GenericDialect();
            ISqlDumper dmp = dialect.CreateDumper(tw, TemplateFormatProps);
            GenerateCreateTable(table, dmp, dialect);
        }

        public override void GenerateSql(IDatabaseSource db, FullDatabaseRelatedName objname, ISqlDumper dmp, ISqlDialect dialect)
        {
            GenerateCreateTable(db.GetTable(objname.ObjectName), dmp, dialect);
        }
        public override bool SuitableFor(AppObject appobj)
        {
            return appobj is TableAppObject;
        }
    }

    [AppObjectSqlGenerator(Name = "drop-table", Title = "DROP TABLE")]
    public class SqlGen_DropTable : AppObjectSqlGeneratorBase
    {
        public void GenerateDropTable(ITableSource table, TextWriter tw, ISqlDialect dialect)
        {
            if (dialect == null) dialect = new GenericDialect();
            ISqlDumper dmp = dialect.CreateDumper(tw, TemplateFormatProps);
            GenerateDropTable(table, tw, dialect);
        }
        public void GenerateDropTable(ITableSource table, ISqlDumper dmp, ISqlDialect dialect)
        {
            dmp.DropTable(table.InvokeLoadStructure(TableStructureMembers.ReferencedFrom), DropFlags.DropReferences | DropFlags.TestIfExist);
        }

        public override void GenerateSql(IDatabaseSource db, FullDatabaseRelatedName objname, ISqlDumper dmp, ISqlDialect dialect)
        {
            GenerateDropTable(db.GetTable(objname.ObjectName), dmp, dialect);
        }
        public override bool SuitableFor(AppObject appobj)
        {
            return appobj is TableAppObject;
        }
    }

    [AppObjectSqlGenerator(Name = "recreate-table", Title = "RECREATE TABLE")]
    public class GenSql_RecreateTable : AppObjectSqlGeneratorBase
    {
        public override void GenerateSql(IDatabaseSource db, FullDatabaseRelatedName objname, ISqlDumper dmp, ISqlDialect dialect)
        {
            GenerateRecreateTable(db.GetTable(objname.ObjectName), dmp, dialect);
        }
        public override bool SuitableFor(AppObject appobj)
        {
            return appobj is TableAppObject;
        }
        public void GenerateRecreateTable(ITableSource table, ISqlDumper dmp, ISqlDialect dialect)
        {
            IMigrationProfile profile = dialect.CreateMigrationProfile();
            var tbl = new TableStructure(table.InvokeLoadStructure(TableStructureMembers.All));
            dialect.MigrateTable(tbl, profile, null);
            dmp.RecreateTable(tbl, tbl);
        }

        public void GenerateRecreateTable(ITableSource table, TextWriter tw, ISqlDialect dialect)
        {
            if (dialect == null) dialect = new GenericDialect();
            ISqlDumper dmp = dialect.CreateDumper(tw, TemplateFormatProps);
            GenerateRecreateTable(table, dmp, dialect);
        }
    }

    public abstract class SqlGen_TableDMLWithWhereScriptBase : AppObjectSqlGeneratorBase
    {
        public override bool SuitableFor(AppObject appobj)
        {
            return appobj is TableAppObject;
        }

        protected void DumpColumns(ITableStructure tbl, ISqlDumper dmp)
        {
            bool was = false;
            foreach (var col in tbl.Columns)
            {
                if (was) dmp.Put(",&n");
                dmp.Put("%i", col.ColumnName);
                was = true;
            }
        }

        protected void DumpWhere(ITableStructure tbl, ISqlDumper dmp)
        {
            if (!GenerateWhere) return;
            dmp.Put("^where&>&n");
            bool was = false;
            foreach (var col in tbl.Columns)
            {
                if (was) dmp.Put("&n^and ");
                if (UseWhereParameters) dmp.Put("%i = '###%s###'", col.ColumnName, col.ColumnName);
                else dmp.Put("%i ^is ^null", col.ColumnName);
                was = true;
            }
            dmp.Put("&<&n");
        }

        bool m_generateWhere = true;
        [XmlElem]
        [TypeConverter(typeof(YesNoTypeConverter))]
        public bool GenerateWhere
        {
            get { return m_generateWhere; }
            set { m_generateWhere = value; CallChangedProperties(); }
        }

        bool m_useWhereParameters = false;
        [XmlElem]
        [TypeConverter(typeof(YesNoTypeConverter))]
        public bool UseWhereParameters
        {
            get { return m_useWhereParameters; }
            set { m_useWhereParameters = value; CallChangedProperties(); }
        }
    }

    [AppObjectSqlGenerator(Name = "select-all-where", Title = "SELECT ALL WHERE")]
    public class SqlGen_SelectAllWhere : SqlGen_TableDMLWithWhereScriptBase
    {
        public void GenerateSelectAllWhere(ITableStructure tbl, ISqlDumper dmp, ISqlDialect dialect)
        {
            dmp.Put("^select &>&n");
            DumpColumns(tbl, dmp);
            dmp.Put("&<&n");
            dmp.Put("^from %f&n", tbl.FullName);
            DumpWhere(tbl, dmp);
        }

        public override void GenerateSql(IDatabaseSource db, FullDatabaseRelatedName objname, ISqlDumper dmp, ISqlDialect dialect)
        {
            IMigrationProfile profile = dialect.CreateMigrationProfile();
            var tbl = new TableStructure(db.GetTable(objname.ObjectName).InvokeLoadStructure(TableStructureMembers.AllNoRefs));
            dialect.MigrateTable(tbl, profile, null);
            GenerateSelectAllWhere(tbl, dmp, dialect);
        }
    }

    [AppObjectSqlGenerator(Name = "update-all-where", Title = "UPDATE ALL WHERE")]
    public class SqlGen_UpdateAllWhere : SqlGen_TableDMLWithWhereScriptBase
    {
        public void GenerateUpdateAllWhere(ITableStructure tbl, ISqlDumper dmp, ISqlDialect dialect)
        {
            dmp.Put("^update %f ^set&>&n", tbl.FullName);
            bool was = false;
            foreach (var col in tbl.Columns)
            {
                if (was) dmp.Put(",&n");
                if (UseValueParameters) dmp.Put("%i = '###%s###'", col.ColumnName, col.ColumnName);
                else dmp.Put("%i = ^null", col.ColumnName);
                was = true;
            }
            dmp.Put("&<&n");
            DumpWhere(tbl, dmp);
        }

        public override void GenerateSql(IDatabaseSource db, FullDatabaseRelatedName objname, ISqlDumper dmp, ISqlDialect dialect)
        {
            IMigrationProfile profile = dialect.CreateMigrationProfile();
            var tbl = new TableStructure(db.GetTable(objname.ObjectName).InvokeLoadStructure(TableStructureMembers.AllNoRefs));
            dialect.MigrateTable(tbl, profile, null);
            GenerateUpdateAllWhere(tbl, dmp, dialect);
        }

        bool m_useValueParameters = false;
        [XmlElem]
        [TypeConverter(typeof(YesNoTypeConverter))]
        public bool UseValueParameters
        {
            get { return m_useValueParameters; }
            set { m_useValueParameters = value; CallChangedProperties(); }
        }
    }

    [AppObjectSqlGenerator(Name = "delete-where", Title = "DELETE WHERE")]
    public class SqlGen_DeleteWhere : SqlGen_TableDMLWithWhereScriptBase
    {
        public void GenerateDeleteWhere(ITableStructure tbl, ISqlDumper dmp, ISqlDialect dialect)
        {
            dmp.Put("^delete ^from %f&n", tbl.FullName);
            DumpWhere(tbl, dmp);
        }

        public override void GenerateSql(IDatabaseSource db, FullDatabaseRelatedName objname, ISqlDumper dmp, ISqlDialect dialect)
        {
            IMigrationProfile profile = dialect.CreateMigrationProfile();
            var tbl = new TableStructure(db.GetTable(objname.ObjectName).InvokeLoadStructure(TableStructureMembers.AllNoRefs));
            dialect.MigrateTable(tbl, profile, null);
            GenerateDeleteWhere(tbl, dmp, dialect);
        }
    }

    [AppObjectSqlGenerator(Name = "insert-template", Title = "INSERT TEMPLATE")]
    public class SqlGen_InsertTemplate : AppObjectSqlGeneratorBase
    {
        public void InsertTemplate(ITableStructure tbl, ISqlDumper dmp, ISqlDialect dialect)
        {
            dmp.Put("^insert ^into %f (&>&n", tbl.FullName);
            bool was = false;
            foreach (var col in tbl.Columns)
            {
                if (was) dmp.Put(",&n");
                dmp.Put("%i", col.ColumnName);
                was = true;
            }
            dmp.Put("&<&n) ^values (&>&n");
            int index = 0;
            foreach (var col in tbl.Columns)
            {
                if (UseValueParameters)
                {
                    dmp.Put("'###%s###'", col.ColumnName);
                }
                else
                {
                    dmp.Put("^null", col.ColumnName);
                }
                if (index < tbl.Columns.Count - 1) dmp.Put(",");
                if (!UseValueParameters) dmp.Put(" -- %s", col.ColumnName);
                if (index < tbl.Columns.Count - 1) dmp.Put("&n");

                index++;
            }
            dmp.Put("&<&n)");
        }

        public override void GenerateSql(IDatabaseSource db, FullDatabaseRelatedName objname, ISqlDumper dmp, ISqlDialect dialect)
        {
            IMigrationProfile profile = dialect.CreateMigrationProfile();
            var tbl = new TableStructure(db.GetTable(objname.ObjectName).InvokeLoadStructure(TableStructureMembers.AllNoRefs));
            dialect.MigrateTable(tbl, profile, null);
            InsertTemplate(tbl, dmp, dialect);
        }

        bool m_useValueParameters = false;
        [XmlElem]
        [TypeConverter(typeof(YesNoTypeConverter))]
        public bool UseValueParameters
        {
            get { return m_useValueParameters; }
            set { m_useValueParameters = value; CallChangedProperties(); }
        }

        public override bool SuitableFor(AppObject appobj)
        {
            return appobj is TableAppObject;
        }
    }

    [AppObjectSqlGenerator(Name = "create-all", Title = "CREATE ALL")]
    public class GenSql_CreateAll : AppObjectSqlGeneratorBase
    {
        bool m_domains = true;

        [XmlElem]
        [TypeConverter(typeof(YesNoTypeConverter))]
        public bool Domains
        {
            get { return m_domains; }
            set { m_domains = value; CallChangedProperties(); }
        }
        bool m_fixedData = true;

        [XmlElem]
        [TypeConverter(typeof(YesNoTypeConverter))]
        public bool FixedData
        {
            get { return m_fixedData; }
            set { m_fixedData = value; CallChangedProperties(); }
        }
        bool m_schemata = true;

        [XmlElem]
        [TypeConverter(typeof(YesNoTypeConverter))]
        public bool Schemata
        {
            get { return m_schemata; }
            set { m_schemata = value; CallChangedProperties(); }
        }
        bool m_specificObjects = true;

        [XmlElem]
        [TypeConverter(typeof(YesNoTypeConverter))]
        public bool SpecificObjects
        {
            get { return m_specificObjects; }
            set { m_specificObjects = value; CallChangedProperties(); }
        }
        bool m_tables = true;

        [XmlElem]
        [TypeConverter(typeof(YesNoTypeConverter))]
        public bool Tables
        {
            get { return m_tables; }
            set { m_tables = value; CallChangedProperties(); }
        }

        public void CreateAllObjects(IDatabaseSource conn, ISqlDumper dmp, ISqlDialect dialect)
        {
            var dbmem = new DatabaseStructureMembers
            {
                TableList = true,
                TableMembers = TableStructureMembers.AllNoRefs,
                DomainList = true,
                DomainDetails = true,
                SpecificObjectList = true,
                SpecificObjectDetails = true,
                LoadDependencies = true,
                IgnoreSystemObjects = true,
            };
            var dbs = new DatabaseStructure(conn.InvokeLoadStructure(dbmem, null));
            var props = new CreateDatabaseObjectsProps
            {
                CreateDomains = Domains,
                CreateFixedData = FixedData,
                CreateSchemata = Schemata,
                CreateSpecificObjects = SpecificObjects,
                CreateTables = Tables
            };
            IMigrationProfile profile = dialect.CreateMigrationProfile();
            dialect.MigrateDatabase(dbs, profile, null);

            dmp.CreateDatabaseObjects(dbs, props);
        }

        public void CreateAllObjects(IDatabaseSource conn, TextWriter tw, ISqlDialect dialect)
        {
            ISqlDumper dmp = dialect.CreateDumper(tw);
            CreateAllObjects(conn, dmp, dialect);
        }

        public override void GenerateSql(IDatabaseSource db, FullDatabaseRelatedName objname, ISqlDumper dmp, ISqlDialect dialect)
        {
            CreateAllObjects(db, dmp, dialect);
        }
        public override bool SuitableFor(AppObject appobj)
        {
            return appobj is DatabaseAppObject;
        }
    }

    [AppObjectSqlGenerator(Name = "create-specific", Title = "CREATE SPECIFIC OBJECTS")]
    public class GenSql_CreateSpecific : AppObjectSqlGeneratorBase
    {
        public override void GenerateSql(IDatabaseSource db, FullDatabaseRelatedName objname, ISqlDumper dmp, ISqlDialect dialect)
        {
            CreateSpecificObjects(db, dmp, dialect);
        }
        public override bool SuitableFor(AppObject appobj)
        {
            return appobj is DatabaseAppObject;
        }

        public void CreateSpecificObjects(IDatabaseSource conn, ISqlDumper dmp, ISqlDialect dialect)
        {
            var dbmem = new DatabaseStructureMembers
            {
                SpecificObjectList = true,
                SpecificObjectDetails = true,
                LoadDependencies = true,
                IgnoreSystemObjects = true,
            };
            var dbs = new DatabaseStructure(conn.InvokeLoadStructure(dbmem, null));
            IMigrationProfile profile = dialect.CreateMigrationProfile();
            dialect.MigrateDatabase(dbs, profile, null);

            CreateSpecificObjects(dmp, dbs);
        }

        public void CreateSpecificObjects(ISqlDumper dmp, IDatabaseStructure db)
        {
            dmp.CreateDatabaseObjects(db, new CreateDatabaseObjectsProps
            {
                AllFlags = false,
                CreateSpecificObjects = true,
            });
        }

        public void CreateSpecificObjects(IDatabaseSource conn, TextWriter tw, ISqlDialect dialect)
        {
            ISqlDumper dmp = dialect.CreateDumper(tw);
            CreateSpecificObjects(conn, dmp, dialect);
        }
    }

    [AppObjectSqlGenerator(Name = "create-tables", Title = "CREATE TABLES")]
    public class GenSql_CreateTables : GenSql_CreateTable
    {
        public void CreateTables(IDatabaseSource conn, ISqlDumper dmp, ISqlDialect dialect)
        {
            var dbmem = new DatabaseStructureMembers
            {
                TableList = true,
                TableMembers = TableStructureMembers.AllNoRefs,
                IgnoreSystemObjects = true,
            };
            var dbs = new DatabaseStructure(conn.InvokeLoadStructure(dbmem, null));
            IMigrationProfile profile = dialect.CreateMigrationProfile();
            dialect.MigrateDatabase(dbs, profile, null);

            CreateTables(dmp, dbs);
        }

        public void CreateTables(ISqlDumper dmp, IDatabaseStructure db)
        {
            // create tables without foreign keys
            DatabaseStructure dbcopy = new DatabaseStructure(db);
            foreach (TableStructure tbl in dbcopy.Tables) RemoveUnwantedTableFeature(tbl);
            dmp.CreateDatabaseObjects(dbcopy, new CreateDatabaseObjectsProps
            {
                AllFlags = false,
                CreateTables = true,
            });
        }

        public void CreateTables(IDatabaseSource conn, TextWriter tw, ISqlDialect dialect)
        {
            ISqlDumper dmp = dialect.CreateDumper(tw);
            CreateTables(conn, dmp, dialect);
        }

        public override void GenerateSql(IDatabaseSource db, FullDatabaseRelatedName objname, ISqlDumper dmp, ISqlDialect dialect)
        {
            CreateTables(db, dmp, dialect);
        }
        public override bool SuitableFor(AppObject appobj)
        {
            return appobj is DatabaseAppObject;
        }
    }

    [AppObjectSqlGenerator(Name = "create-domains", Title = "CREATE DOMAINS")]
    public class GenSql_CreateDomains : AppObjectSqlGeneratorBase
    {
        public override void GenerateSql(IDatabaseSource db, FullDatabaseRelatedName objname, ISqlDumper dmp, ISqlDialect dialect)
        {
            CreateDomains(db, dmp, dialect);
        }
        public void CreateDomains(IDatabaseSource conn, ISqlDumper dmp, ISqlDialect dialect)
        {
            var dbmem = new DatabaseStructureMembers
            {
                DomainDetails = true,
                DomainList = true,
                IgnoreSystemObjects = true,
            };
            var dbs = new DatabaseStructure(conn.InvokeLoadStructure(dbmem, null));
            IMigrationProfile profile = dialect.CreateMigrationProfile();
            dialect.MigrateDatabase(dbs, profile, null);

            CreateDomains(dmp, dbs);
        }
        public void CreateDomains(ISqlDumper dmp, IDatabaseStructure db)
        {
            foreach (var dom in db.Domains)
            {
                dmp.CreateDomain(dom);
            }
        }
        public override bool SuitableFor(AppObject appobj)
        {
            return appobj is DatabaseAppObject;
        }
    }

    [AppObjectSqlGenerator(Name = "create-refs", Title = "CREATE REFERENCES")]
    public class GenSql_CreateRefs : AppObjectSqlGeneratorBase
    {
        public override void GenerateSql(IDatabaseSource db, FullDatabaseRelatedName objname, ISqlDumper dmp, ISqlDialect dialect)
        {
            CreateReferences(db, dmp, dialect);
        }

        public void CreateReferences(ISqlDumper dmp, IDatabaseStructure db)
        {
            foreach (var table in db.Tables)
            {
                dmp.CreateConstraints(table.GetConstraints<IForeignKey, IConstraint>());
            }
        }

        public void CreateReferences(IDatabaseSource conn, ISqlDumper dmp, ISqlDialect dialect)
        {
            var dbmem = new DatabaseStructureMembers
            {
                TableList = true,
                TableMembers = TableStructureMembers.AllNoRefs,
                IgnoreSystemObjects = true,
            };
            var dbs = new DatabaseStructure(conn.InvokeLoadStructure(dbmem, null));
            IMigrationProfile profile = dialect.CreateMigrationProfile();
            dialect.MigrateDatabase(dbs, profile, null);
            CreateReferences(dmp, dbs);
        }

        public void CreateReferences(IDatabaseSource conn, TextWriter tw, ISqlDialect dialect)
        {
            ISqlDumper dmp = dialect.CreateDumper(tw);
            CreateReferences(conn, dmp, dialect);
        }

        public override bool SuitableFor(AppObject appobj)
        {
            return appobj is DatabaseAppObject;
        }
    }

    [AppObjectSqlGenerator(Name = "drop-database", Title = "DROP DATABASE")]
    public class GenSql_DropDatabase : AppObjectSqlGeneratorBase
    {
        public override void GenerateSql(IDatabaseSource db, FullDatabaseRelatedName objname, ISqlDumper dmp, ISqlDialect dialect)
        {
            DropDatabase(db, dmp, dialect);
        }
        public void DropDatabase(IDatabaseSource db, ISqlDumper dmp, ISqlDialect dialect)
        {
            dmp.DropDatabase(db.DatabaseName);
        }
        public void DropDatabase(IDatabaseSource db, TextWriter tw, ISqlDialect dialect)
        {
            ISqlDumper dmp = dialect.CreateDumper(tw);
            dmp.DropDatabase(db.DatabaseName);
        }
        public override bool SuitableFor(AppObject appobj)
        {
            var dbobj = appobj as DatabaseAppObject;
            if (dbobj == null) return false;
            return dbobj.DatabaseName != null;
        }
    }

    [AppObjectSqlGenerator(Name = "create-database", Title = "CREATE DATABASE")]
    public class GenSql_CreateDatabase : AppObjectSqlGeneratorBase
    {
        public override void GenerateSql(IDatabaseSource db, FullDatabaseRelatedName objname, ISqlDumper dmp, ISqlDialect dialect)
        {
            CreateDatabase(db, dmp, dialect);
        }
        public void CreateDatabase(IDatabaseSource db, ISqlDumper dmp, ISqlDialect dialect)
        {
            dmp.CreateDatabase(db.DatabaseName, null);
        }
        public void CreateDatabase(IDatabaseSource db, TextWriter tw, ISqlDialect dialect)
        {
            ISqlDumper dmp = dialect.CreateDumper(tw);
            dmp.CreateDatabase(db.DatabaseName, null);
        }
        public override bool SuitableFor(AppObject appobj)
        {
            var dbobj = appobj as DatabaseAppObject;
            if (dbobj == null) return false;
            return dbobj.DatabaseName != null;
        }
    }

    [AppObjectSqlGenerator(Name = "drop-all-tables", Title = "DROP ALL TABLES")]
    public class GenSql_DropAllTables : AppObjectSqlGeneratorBase
    {
        public void GenerateDropAllTables(IDatabaseSource conn, ISqlDumper dmp, ISqlDialect dialect)
        {
            DatabaseStructureMembers dbmem = new DatabaseStructureMembers
            {
                TableList = true,
                TableMembers = TableStructureMembers.ForeignKeys,
                IgnoreSystemObjects = true,
            };
            var dbs = new DatabaseStructure(conn.InvokeLoadStructure(dbmem, null));
            IMigrationProfile profile = dialect.CreateMigrationProfile();
            dialect.MigrateDatabase(dbs, profile, null);
            foreach (ITableStructure tbl in dbs.Tables)
            {
                if (conn.Dialect.IsSystemTable(tbl.FullName)) continue;
                foreach (IForeignKey fk in tbl.GetConstraints<IForeignKey>())
                {
                    dmp.DropConstraint(fk);
                }
            }
            foreach (ITableStructure tbl in dbs.Tables)
            {
                if (conn.Dialect.IsSystemTable(tbl.FullName)) continue;
                dmp.DropTable(tbl, DropFlags.None);
            }
        }
        public void GenerateDropAllTables(IDatabaseSource conn, TextWriter tw, ISqlDialect dialect)
        {
            if (dialect == null) dialect = new GenericDialect();
            ISqlDumper dmp = dialect.CreateDumper(tw, TemplateFormatProps);
            GenerateDropAllTables(conn, dmp, dialect);
        }
        public override void GenerateSql(IDatabaseSource db, FullDatabaseRelatedName objname, ISqlDumper dmp, ISqlDialect dialect)
        {
            GenerateDropAllTables(db, dmp, dialect);
        }
        public override bool SuitableFor(AppObject appobj)
        {
            return appobj is DatabaseAppObject;
        }
    }

    [AppObjectSqlGenerator(Name = "fill-from-other-db", Title = "FILL FROM OTHER DB")]
    public class GenSql_FillFromOtherDb : AppObjectSqlGeneratorBase
    {
        string m_sourceDb = "#SOURCEDB#";

        [XmlElem]
        public string SourceDb
        {
            get { return m_sourceDb; }
            set { m_sourceDb = value; CallChangedProperties(); }
        }

        protected void GenerateFillTable(ITableStructure tbl, ISqlDumper dmp)
        {
            var autocol = tbl.FindAutoIncrementColumn();
            if (autocol != null) dmp.AllowIdentityInsert(tbl.FullName, true);
            var colnames = from c in tbl.Columns select c.ColumnName;
            dmp.PutCmd("^insert ^into %f (%,i) ^select %,i ^from %s.%f",
                tbl.FullName, colnames, colnames, SourceDb, tbl.FullName);
            if (autocol != null) dmp.AllowIdentityInsert(tbl.FullName, false);
        }

        public void GenerateFillTable(ITableSource table, ISqlDumper dmp, ISqlDialect dialect)
        {
            IMigrationProfile profile = dialect.CreateMigrationProfile();
            var tbl = new TableStructure(table.InvokeLoadStructure(TableStructureMembers.AllNoRefs));
            dialect.MigrateTable(tbl, profile, null);
            GenerateFillTable(tbl, dmp);
        }

        public void GenerateFillTable(ITableSource table, TextWriter tw, ISqlDialect dialect)
        {
            if (dialect == null) dialect = new GenericDialect();
            ISqlDumper dmp = dialect.CreateDumper(tw, TemplateFormatProps);
            GenerateFillTable(table, dmp, dialect);
        }

        public override void GenerateSql(IDatabaseSource db, FullDatabaseRelatedName objname, ISqlDumper dmp, ISqlDialect dialect)
        {
            GenerateFillTable(db.GetTable(objname.ObjectName), dmp, dialect);
        }
        public override bool SuitableFor(AppObject appobj)
        {
            return appobj is TableAppObject;
        }
    }

    [AppObjectSqlGenerator(Name = "fill-all-from-other-db", Title = "FILL ALL FROM OTHER DB")]
    public class GenSql_FillAllFromOtherDb : GenSql_FillFromOtherDb
    {
        public void FillAllTables(IDatabaseSource conn, ISqlDumper dmp, ISqlDialect dialect)
        {
            var dbmem = new DatabaseStructureMembers
            {
                TableList = true,
                TableMembers = TableStructureMembers.Columns,
            };
            var dbs = new DatabaseStructure(conn.InvokeLoadStructure(dbmem, null));
            IMigrationProfile profile = dialect.CreateMigrationProfile();
            dialect.MigrateDatabase(dbs, profile, null);
            foreach (var tbl in dbs.Tables)
            {
                GenerateFillTable(tbl, dmp);
            }
        }

        public override void GenerateSql(IDatabaseSource db, FullDatabaseRelatedName objname, ISqlDumper dmp, ISqlDialect dialect)
        {
            FillAllTables(db, dmp, dialect);
        }

        public override bool SuitableFor(AppObject appobj)
        {
            return appobj is DatabaseAppObject;
        }
    }

    public abstract class GenSql_SpecificObjectBase : AppObjectSqlGeneratorBase
    {
        public override void GenerateSql(IDatabaseSource db, FullDatabaseRelatedName objname, ISqlDumper dmp, ISqlDialect dialect)
        {
            var so = db.InvokeLoadSpecificObjectDetail(objname.ObjectType, objname.ObjectName);
            DoGenerateSql(dmp, so);
        }

        protected abstract void DoGenerateSql(ISqlDumper dmp, ISpecificObjectStructure so);

        public override bool SuitableFor(AppObject appobj)
        {
            var so = appobj as SpecificObjectAppObject;
            if (so != null)
            {
                return SuitableForCaps(so.ObjCaps);
            }
            return false;
        }

        protected abstract bool SuitableForCaps(ObjectOperationCaps caps);
    }

    [AppObjectSqlGenerator(Name = "create-specific-object", Title = "CREATE")]
    public class GenSql_CreateSpecificObject : GenSql_SpecificObjectBase
    {
        protected override void DoGenerateSql(ISqlDumper dmp, ISpecificObjectStructure so)
        {
            dmp.CreateSpecificObject(so);
        }

        protected override bool SuitableForCaps(ObjectOperationCaps caps)
        {
            return caps.Create;
        }
    }

    [AppObjectSqlGenerator(Name = "alter-specific-object", Title = "ALTER")]
    public class GenSql_AlterSpecificObject : GenSql_SpecificObjectBase
    {
        protected override void DoGenerateSql(ISqlDumper dmp, ISpecificObjectStructure so)
        {
            dmp.DropSpecificObject(so);
            dmp.CreateSpecificObject(so);
        }

        protected override bool SuitableForCaps(ObjectOperationCaps caps)
        {
            return caps.Create && caps.Drop;
        }
    }

    [AppObjectSqlGenerator(Name = "drop-specific-object", Title = "DROP")]
    public class GenSql_DropSpecificObject : GenSql_SpecificObjectBase
    {
        protected override void DoGenerateSql(ISqlDumper dmp, ISpecificObjectStructure so)
        {
            dmp.DropSpecificObject(so);
        }

        protected override bool SuitableForCaps(ObjectOperationCaps caps)
        {
            return caps.Drop;
        }
    }

    [AppObjectSqlGenerator(Name = "view-create-as-table", Title = "CREATE AS TABLE")]
    public class GenSql_CreateViewAsTable : AppObjectSqlGeneratorBase
    {
        public string Prefix { get; set; }
        public string Postfix { get; set; }

        public GenSql_CreateViewAsTable()
        {
            Postfix = "_table";
        }

        public override void GenerateSql(IDatabaseSource db, FullDatabaseRelatedName objname, ISqlDumper dmp, ISqlDialect dialect)
        {
            var ts = db.InvokeLoadViewStructure(objname.ObjectName);
            var tbl = new TableStructure(ts);
            tbl.FullName = objname.ObjectName;
            if (Prefix != null) tbl.FullName = new NameWithSchema(tbl.FullName.Schema, Prefix + tbl.FullName.Name);
            if (Postfix != null) tbl.FullName = new NameWithSchema(tbl.FullName.Schema, tbl.FullName.Name + Postfix);
            dmp.CreateTable(tbl);
        }

        public override bool SuitableFor(AppObject appobj)
        {
            var ao = appobj as SpecificObjectAppObject;
            if (ao == null) return false;
            if (ao.DbObjectType == "view")
            {
                return true;
            }
            return false;
        }
    }

    public abstract class GenSql_ColumnBase : AppObjectSqlGeneratorBase
    {
        public override void GenerateSql(IDatabaseSource db, FullDatabaseRelatedName objname, ISqlDumper dmp, ISqlDialect dialect)
        {
            var ts = db.InvokeLoadTableStructure(objname.ObjectName, TableStructureMembers.Columns);
            DoGenerateSql(dmp, ts, objname.SubName);
        }

        protected abstract void DoGenerateSql(ISqlDumper dmp, ITableStructure ts, string colname);

        public override bool SuitableFor(AppObject appobj)
        {
            var co = appobj as ColumnAppObject;
            return co != null;
        }
    }

    [AppObjectSqlGenerator(Name = "create-column", Title = "CREATE")]
    public class GenSql_CreateColumn : GenSql_ColumnBase
    {
        protected override void DoGenerateSql(ISqlDumper dmp, ITableStructure ts, string colname)
        {
            dmp.CreateColumn(ts.Columns[colname]);
        }
    }

    [AppObjectSqlGenerator(Name = "drop-column", Title = "DROP")]
    public class GenSql_DropColumn : GenSql_ColumnBase
    {
        protected override void DoGenerateSql(ISqlDumper dmp, ITableStructure ts, string colname)
        {
            dmp.DropColumn(ts.Columns[colname]);
        }
    }

    public abstract class GenSql_ConstraintBase : AppObjectSqlGeneratorBase
    {
        public override void GenerateSql(IDatabaseSource db, FullDatabaseRelatedName objname, ISqlDumper dmp, ISqlDialect dialect)
        {
            var ts = db.InvokeLoadTableStructure(objname.ObjectName, TableStructureMembers.ConstraintsNoRefs);
            var cs = ts.FindConstraint(objname.ObjectType, objname.SubName);
            DoGenerateSql(dmp, ts, cs);
        }

        protected abstract void DoGenerateSql(ISqlDumper dmp, ITableStructure ts, IConstraint cnt);

        public override bool SuitableFor(AppObject appobj)
        {
            var co = appobj as ConstraintAppObject;
            return co != null;
        }
    }

    [AppObjectSqlGenerator(Name = "create-constraint", Title = "CREATE")]
    public class GenSql_CreateConstraint : GenSql_ConstraintBase
    {
        protected override void DoGenerateSql(ISqlDumper dmp, ITableStructure ts, IConstraint cnt)
        {
            dmp.CreateConstraint(cnt);
        }
    }

    [AppObjectSqlGenerator(Name = "drop-constraint", Title = "DROP")]
    public class GenSql_DropConstraint : GenSql_ConstraintBase
    {
        protected override void DoGenerateSql(ISqlDumper dmp, ITableStructure ts, IConstraint cnt)
        {
            dmp.DropConstraint(cnt);
        }
    }
}
