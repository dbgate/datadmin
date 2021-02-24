using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.Data;
using System.Linq;

namespace DatAdmin
{
    public class DatabaseAnalyser
    {
        protected IPhysicalConnection m_conn;
        protected ISqlDialect m_dialect;
        private DbConnection m_dbConnection;
        protected DatabaseStructureMembers m_members;
        protected string m_dbname;
        protected DatabaseStructure m_db = new DatabaseStructure();
        // is not null only when it containts information about all tables
        //private TableAnalyser m_loadedConstraints = null;
        protected IProgressInfo m_progress;
        protected List<ISpecificObjectType> m_definedSpecs;
        //public string m_serverVersion;

        public DatabaseCache AnalyserCache;
        //public DatabaseAnalyserCache AnalyserCache = new DatabaseAnalyserCache();

        public DatabaseAnalyser()
        {
        }

        protected virtual string GetDefaultValueExpression(string valueFromInfoSchema)
        {
            return valueFromInfoSchema;
        }

        public NameWithSchema NewNameWithSchema(string schema, string name)
        {
            if (schema.IsEmpty()) schema = null;
            NameWithSchema res = new NameWithSchema(schema, name);
            if (!m_dialect.DialectCaps.MultipleSchema) res = res.GetNameWithHiddenSchema();
            return res;
        }

        protected bool DefinedSpecificType(string type)
        {
            return (from s in m_definedSpecs where s.ObjectType == type select s).Any();
        }

        protected virtual DbTypeBase AnalyseType(IDataRecord row, IPhysicalConnection conn, bool isdomain)
        {
            return new DbTypeString();
        }
        protected virtual bool IsColumnNullable(IDataRecord row)
        {
            return true;
        }

        private void LoadViewsAsTables()
        {
            if (m_members.ViewAsTables)
            {
                if (m_members.ViewAsTableFilter == null)
                {
                    LoadViews(true);
                    foreach (var v in m_db.SpecByType("view"))
                    {
                        LoadViewAsTable(v.ObjectName);
                    }
                }
                else
                {
                    foreach (var v in m_members.ViewAsTableFilter)
                    {
                        LoadViewAsTable(v);
                    }
                }
            }
        }

        protected virtual void LoadViewAsTable(NameWithSchema name)
        {
            string sql = "SELECT * FROM " + m_dialect.QuoteFullName(name);
            var ts = CachedQueryStructure(sql);
            ts.FullName = name;
            m_db.ViewAsTables.Add(ts);
        }

        protected virtual void LoadTableList()
        {
            if (m_members.TableList || m_members.TableMembers.Contains(TableStructureMembers.SpecificDetails))
            {
                DataTable tables = CachedSchemaTable("Tables");
                int schemacol = tables.SafeOrdinal(AnalysingExtensions.SCHEMACOL);
                int typecol = tables.SafeOrdinal(AnalysingExtensions.TABLETYPECOL);

                foreach (DataRow row in tables.Rows)
                {
                    if (row.SafeString(typecol).ToLower().Contains("view")) continue;
                    //if (m_members.IgnoreSystemObjects && row["TABLE_TYPE"].ToString().ToLower().Contains("system")) continue;
                    string name = row.SafeString("TABLE_NAME");
                    string schema = row.SafeString(schemacol);
                    if (m_dialect.IsSystemTable(schema, name) && m_members.IgnoreSystemObjects) continue;
                    var fullname = NewNameWithSchema(schema, name);
                    TableStructure tbl;
                    if (m_db.Tables.GetIndex(fullname) >= 0) tbl = (TableStructure)m_db.Tables[fullname];
                    else tbl = m_db.AddTable(fullname);
                    LoadTableRowDetails(row, tbl);
                }
                //m_db.FilledMembers.TableList = true;
            }
        }

        protected virtual void LoadTableRowDetails(DataRow row, TableStructure tbl)
        {
        }

        protected virtual bool WantLoadTableColumns()
        {
            return m_members.TableMembers.ContainsAnyColumns();
        }

        protected virtual void LoadTableColumns()
        {
            if (!WantLoadTableColumns()) return;

            foreach (TableStructure tbl in EnumTables())
            {
                LoadTableColumns(tbl);
            }
        }

        protected virtual void LoadTableColumn(ColumnStructure col, DataRow row)
        {
            col.IsNullable = IsColumnNullable(new DataRowAdapter(row));
            string def = row.ColumnDefault();
            col.DefaultValue = SqlExpression.ParseDefaultValue(GetDefaultValueExpression(def), m_dialect);
            col.Collation = row.SafeString("COLLATION_NAME");
            col.CharacterSet = row.SafeString("CHARACTER_SET_NAME");
            string dschema = row.SafeString("DOMAIN_SCHEMA"), dname = row.SafeString("DOMAIN_NAME");
            if (dname != null && dname.Trim() != "") col.Domain = NewNameWithSchema(dschema, dname);
        }

        protected virtual DataTable LoadTableColumnsTable(NameWithSchema table)
        {
            string[] filter = null;
            if (table != null) filter = new string[] { null, table.Schema, table.Name };
            DataTable columns = CachedSchemaTable("Columns", filter);
            return columns;
        }

        protected DataTable GetCachedTableColumnsTable(string ns, Func<NameWithSchema, DataTable> loadFunc, NameWithSchema table)
        {
            var res = AnalyserCache.GetTableColumns(ns, table);
            if (res != null) return res;
            res = AnalyserCache.GetTableColumns(ns, null);
            if (res != null) return FilterTableColumns(ns + ".index", res, table);
            var tables = EnumTables();
            if (tables.Count > 1)
            {
                res = loadFunc(null);
                AnalyserCache.PutTableColumns(ns, null, res);
                return FilterTableColumns(ns + ".index", res, table);
            }
            if (tables.Count == 1)
            {
                res = loadFunc(tables[0].FullName);
                AnalyserCache.PutTableColumns(ns, tables[0].FullName, res);
                return res;
            }
            throw new InternalError("DAE-00001 error loading cached columns");
        }

        protected DataTable GetCachedTableColumnsTable(NameWithSchema table)
        {
            return GetCachedTableColumnsTable("analyse.columns", LoadTableColumnsTable, table);
        }

        protected DataTable FilterTableColumns(string ns, DataTable allcols, NameWithSchema table)
        {
            var index = (DataTableIndex)AnalyserCache.Get(ns, "colscache");
            if (index == null)
            {
                int schemai = allcols.SafeOrdinal(AnalysingExtensions.SCHEMACOL);
                int tablei = allcols.SafeOrdinal(AnalysingExtensions.TABLECOL);
                index = new DataTableIndex(allcols, schemai, tablei);
                AnalyserCache.Put(ns, "colscache", index);
            }
            return index.Query(v => v == null || table.Schema == null || String.Compare(v, table.Schema, true) == 0, v => v == null || table.Name == null || String.Compare(v, table.Name, true) == 0);
            //var res = allcols.Clone();
            //foreach (DataRow row in allcols.Rows)
            //{
            //    if (table.Schema != null && String.Compare(row.SafeString(AnalysingExtensions.SCHEMACOL), table.Schema, true) != 0) continue;
            //    if (table.Name != null && String.Compare(row.SafeString(AnalysingExtensions.TABLECOL), table.Name, true) != 0) continue;
            //    res.ImportRow(row);
            //}
            //return res;
        }

        protected virtual void LoadTableColumns(TableStructure table)
        {
            DataTable columns = GetCachedTableColumnsTable(table.FullName);
            ProcessTableColumnsTable(table, columns);
        }

        protected virtual void ProcessTableColumnsTable(TableStructure table, DataTable columns)
        {
            foreach (DataRow row in columns.Rows.SortedByKey<DataRow, int>(row => Int32.Parse(row["ORDINAL_POSITION"].ToString())))
            {
                //NameWithSchema table = NewNameWithSchema(row["TABLE_SCHEMA"].SafeToString(), row["TABLE_NAME"].ToString());
                ColumnStructure col = table.AddColumn(row["COLUMN_NAME"].ToString(), AnalyseType(new DataRowAdapter(row), m_conn, false));
                LoadTableColumn(col, row);
            }
            //table.FilledMembers |= TableStructureMembers.ColumnNames | TableStructureMembers.ColumnTypes;
        }

        //protected virtual void AfterLoadTableColumns()
        //{
        //    foreach (TableStructure tbl in m_db.Tables)
        //    {
        //        if (!tbl.FilledMembers.ContainsAnyColumns()) continue;
        //        AfterLoadTableColumns(tbl);
        //    }
        //}

        //protected virtual void AfterLoadTableColumns(TableStructure table) { }

        protected virtual void LoadConstraints()
        {
            if (!m_members.TableMembers.ContainsAnyConstraints(false) && !m_members.TableMembers.Contains(TableStructureMembers.ReferencedFrom)) return;
            foreach (var table in EnumTables())
            {
                LoadConstraints(table);
            }
        }

        private void InternalAfterLoadConstraints()
        {
            // delete all foreigk keys added as references in tables, which have own keys
            foreach (TableStructure table in m_db.Tables)
            {
                if (table.GetConstraints<ForeignKey>().Any(fk => !fk.m_addedAsReference))
                {
                    table._Constraints.RemoveIf(c => c is ForeignKey && ((ForeignKey)c).m_addedAsReference);
                }
            }
        }

        protected virtual void AfterLoadIndexesOrConstraints()
        {
            if (!m_members.TableMembers.ContainsAnyConstraints(false) && !m_members.TableMembers.Contains(TableStructureMembers.Indexes)) return;
            foreach (var table in EnumTables())
            {
                AfterLoadIndexesOrConstraints(table);
            }
        }

        // which constraints should be loaded be LoadConstraints method
        protected virtual TableStructureMembers GetConstraintLoadMode()
        {
            return TableStructureMembers.Constraints | TableStructureMembers.ReferencedFrom;
        }

        protected virtual ConstraintsLoader.Condition GetDbNameCondition() { return null; }

        protected DbConnection GetDbConn()
        {
            WantConnection();
            return m_dbConnection;
        }

        private TableAnalyser GetLoadedConstraints(TableStructure table)
        {
            {
                var res = (TableAnalyser)AnalyserCache.GetTableAnalyser("constraints");
                if (res != null) return res;
                res = (TableAnalyser)AnalyserCache.GetTableAnalyser("constraints:" + table.FullName.ToString());
                if (res != null) return res;
            }
            InformationSchema ins = InformationSchema.LoadSchemaOnce(GetDbConn, m_conn.GetConnKey());
            if (m_members.TableFilter != null && m_members.TableFilter.Count == 1)
            {
                // load constraint for one table
                TableAnalyser ta = new TableAnalyser();
                ConstraintsLoader loader = new ConstraintsLoader(GetDbConn(), ins, ta, table.FullName, m_members.TableMembers & GetConstraintLoadMode(), GetDbNameCondition());
                loader.Run();
                AnalyserCache.PutTableAnalyser("constraints:" + table.FullName.ToString(), ta);
                return ta;
            }
            else
            {
                // load constraints for all tables
                TableAnalyser ta = new TableAnalyser();
                ConstraintsLoader loader = new ConstraintsLoader(GetDbConn(), ins, ta, null, m_members.TableMembers & GetConstraintLoadMode(), GetDbNameCondition());
                loader.Run();
                AnalyserCache.PutTableAnalyser("constraints", ta);
                return ta;
            }
        }

        protected virtual void LoadConstraints(TableStructure table)
        {
            TableAnalyser ta = GetLoadedConstraints(table);
            ta.SaveConstraints(table, this);
            //table.FilledMembers |= m_members.TableMembers.FilterConstraints(false);
        }

        protected virtual void LoadIndexes()
        {
            if (!m_members.TableMembers.Contains(TableStructureMembers.Indexes)) return;
            foreach (var table in EnumTables())
            {
                LoadIndexes(table);
            }
        }

        protected virtual void LoadIndexes(TableStructure table)
        {
        }

        protected virtual void AfterLoadIndexesOrConstraints(TableStructure table)
        {
        }

        protected bool FilterContains(IEnumerable<NameWithSchema> filter, NameWithSchema name)
        {
            foreach (var f in filter)
            {
                if (f.Schema == null && f.Name == name.Name) return true;
                if (f == name) return true;
            }
            return false;
        }

        protected List<TableStructure> EnumTables()
        {
            var res = new List<TableStructure>();
            foreach (var table in m_db.Tables)
            {
                if (m_members.TableFilter != null && !FilterContains(m_members.TableFilter, table.FullName)) continue;
                res.Add((TableStructure)table);
            }
            return res;
        }

        private void PrepareMembers()
        {
            if (m_members.TableMembers != TableStructureMembers.None && !m_members.TableList)
            {
                if (m_members.TableFilter != null)
                {
                    foreach (var tname in m_members.TableFilter)
                    {
                        TableStructure ts = new TableStructure();
                        var name2 = tname;
                        if (m_dialect.DefaultSchema != null && name2.Schema == null)
                        {
                            name2 = NewNameWithSchema(m_dialect.DefaultSchema, name2.Name);
                        }
                        ts.FullName = name2;
                        m_db.Tables.Add(ts);
                    }
                }
                else
                {
                    m_members.TableList = true;
                }
            }
            if (m_members.SpecificObjectDetails) m_members.SpecificObjectList = true;
            if (m_members.SpecificObjectOverride != null)
            {
                foreach (var v in m_members.SpecificObjectOverride.Values)
                {
                    if (v.ObjectDetail && v.ObjectFilter == null) v.ObjectList = true;
                }
            }
        }

        protected virtual void LoadSpecificObjects()
        {
            foreach (ISpecificObjectType so in m_definedSpecs)
            {
                if (so.ObjectType == "view") LoadViews(false);
            }
        }

        protected virtual void LoadViews(bool listOnly)
        {
            LoadSpecificObjectListAndDetail("view", "SELECT * FROM INFORMATION_SCHEMA.VIEWS", "TABLE_SCHEMA", "TABLE_NAME", LoadViewRow);
        }

        protected virtual void LoadViewRow(DataRow row, SpecificObjectStructure so)
        {
            so.CreateSql = row["VIEW_DEFINITION"].SafeToString();
        }

        protected void SetCurWork(string text)
        {
            if (m_progress != null) m_progress.SetCurWork(text);
        }

        protected virtual void LoadDatabaseOptions()
        {
            if (!m_members.DatabaseOptions) return;
        }

        protected void WantConnection()
        {
            if (m_dbConnection == null)
            {
                m_conn.ReconnectIfBroken();
                m_dbConnection = m_conn.SystemConnection;
                m_dbConnection.SafeChangeDatabase(m_dbname);
            }
        }

        public DatabaseStructure Run(ISqlDialect dialect, IPhysicalConnection conn, string dbname, DatabaseStructureMembers members, IProgressInfo progress)
        {
            AnalyserCache = conn.Cache.GetAnalyserCache(dbname);
            m_conn = conn;
            //m_serverVersion = m_dbConn.ServerVersion;
            //ParseVersion();
            m_dialect = dialect;
            m_members = members;
            m_dbname = dbname;
            m_progress = progress;
            m_db.Dialect = m_dialect;
            m_definedSpecs = m_dialect.GetSpecificTypes();

            
            PrepareMembers();

            SetCurWork(Texts.Get("s_loading_tables"));
            LoadDomains();
            LoadTableList();
            LoadTableColumns();
            LoadConstraints();
            LoadIndexes();
            AfterLoadIndexesOrConstraints();
            InternalAfterLoadConstraints();
            MarkFilledTableMembers();
            LoadSchemata();
            LoadCharacterSets();
            LoadCollations();

            SetCurWork(Texts.Get("s_loading_objects"));
            LoadSpecificObjects();

            LoadDatabaseOptions();
            LoadViewsAsTables();

            LoadDependencies();

            return m_db;
        }

        public void MarkFilledTableMembers()
        {
            foreach (var tbl in EnumTables())
            {
                tbl.FilledMembers = m_members.TableMembers;
            }
        }

        protected virtual void LoadDomains()
        {
            if ((m_members.DomainDetails || m_members.DomainList) && m_dialect.DialectCaps.Domains)
            {
                //m_db.Domains = new DomainCollection();
                foreach (DataRow row in CachedQueryRows("SELECT * FROM INFORMATION_SCHEMA.DOMAINS"))
                {
                    var dom = new DomainStructure();
                    LoadDomain(dom, row);
                    m_db.Domains.Add(dom);
                }
            }
        }

        protected virtual void LoadDomain(DomainStructure domain, DataRow row)
        {
            domain.FullName = NewNameWithSchema(row.SafeString("DOMAIN_SCHEMA"), row.SafeString("DOMAIN_NAME"));
            domain.DataType = AnalyseType(new DataRowAdapter(row), m_conn, true);

            string def = row.SafeString("DOMAIN_DEFAULT");
            domain.DefaultValue = SqlExpression.ParseDefaultValue(GetDefaultValueExpression(def), m_dialect);

        }

        protected virtual void LoadDependencies()
        {
            if (!m_members.LoadDependencies) return;
        }

        //protected virtual void ParseVersion() { }

        protected virtual void LoadSchemata()
        {
            if (!m_dialect.DialectCaps.MultipleSchema) return;
            if (!m_members.SchemaList && !m_members.SchemaDetails) return;
            //m_db.Schemata = new SchemaCollection();
            foreach (DataRow row in CachedQueryRows("SELECT * FROM INFORMATION_SCHEMA.SCHEMATA"))
            {
                m_db.Schemata.Add(new SchemaStructure { SchemaName = row["SCHEMA_NAME"].SafeToString() });
            }
            //m_db.FilledMembers.SchemaDetails = m_db.FilledMembers.SchemaList = true;
        }

        protected virtual void LoadCharacterSets()
        {
            if (!m_members.CharacterSetList && !m_members.CharacterSetDetails) return;
            if (m_members.IgnoreSystemObjects) return;
            foreach (DataRow row in CachedQueryRows("SELECT * FROM INFORMATION_SCHEMA.CHARACTER_SETS"))
            {
                m_db.CharacterSets.Add(new CharacterSetStructure { Name = row["CHARACTER_SET_NAME"].SafeToString(), DefaultCollation = row.SafeString("DEFAULT_COLLATE_NAME") });
            }
            //m_db.FilledMembers.CharacterSetList = m_db.FilledMembers.CharacterSetDetails = true;
        }

        protected virtual void LoadCollations()
        {
            if (!m_members.CollationList && !m_members.CollationDetails) return;
            if (m_members.IgnoreSystemObjects) return;
            foreach (DataRow row in CachedQueryRows("SELECT * FROM INFORMATION_SCHEMA.COLLATIONS"))
            {
                m_db.Collations.Add(new CollationStructure { Name = row["COLLATION_NAME"].SafeToString(), CharacterSet = row["CHARACTER_SET_NAME"].SafeToString() });
            }
            //m_db.FilledMembers.CollationList = m_db.FilledMembers.CollationDetails = true;
        }

        protected bool WantSpecificObjectList(string objtype)
        {
            if (!DefinedSpecificType(objtype)) return false;
            if (m_members.SpecificObjectOverride != null && m_members.SpecificObjectOverride.ContainsKey(objtype))
            {
                var s = m_members.SpecificObjectOverride[objtype];
                return s.ObjectList;
            }
            return m_members.SpecificObjectList;
        }

        protected bool WantSpecificObjectDetail(string objtype, out List<NameWithSchema> objfilter)
        {
            objfilter = null;
            if (!DefinedSpecificType(objtype)) return false;
            if (m_members.SpecificObjectOverride != null && m_members.SpecificObjectOverride.ContainsKey(objtype))
            {
                var s = m_members.SpecificObjectOverride[objtype];
                objfilter = s.ObjectFilter;
                if (!s.ObjectDetail) return false;
                return true;
            }
            else
            {
                return m_members.SpecificObjectDetails;
            }

        }

        // utility functions for loading specific object list
        protected void LoadSpecificObjectList(string objtype, string query, string schemacol, string namecol)
        {
            if (!WantSpecificObjectList(objtype)) return;
            m_db.SpecByType(objtype);
            foreach (DataRow row in CachedQueryRows(query))
            {
                string schema = null, name = null;
                if (schemacol != null) schema = row[schemacol].SafeToString();
                if (namecol != null) name = row[namecol].SafeToString();
                if (m_members.IgnoreSystemObjects && m_dialect.IsSystemObject(objtype, schema, name)) continue;
                var obj = m_db.AddSpecificObject(objtype, DialectName, NewNameWithSchema(schema, name));
            }
        }

        protected void LoadSpecificObjectListAndDetail(string objtype, string query, string schemacol, string namecol, Action<DataRow, SpecificObjectStructure> loadFunc)
        {
            LoadSpecificObjectListAndDetail(objtype, query, (row, so) =>
            {
                string schema = null, name = null;
                if (schemacol != null) schema = row[schemacol].SafeToString();
                if (namecol != null) name = DecodeSpecName(objtype, row[namecol].SafeToString());
                so.ObjectName = NewNameWithSchema(schema, name);
                loadFunc(row, so);
            });
        }

        protected bool LoadMoreSpecificObjects
        {
            get
            {
                int sum = 0;
                foreach (var spec in m_definedSpecs)
                {
                    List<NameWithSchema> objs;
                    if (WantSpecificObjectDetail(spec.ObjectType, out objs))
                    {
                        if (objs != null) sum += objs.Count;
                        else sum += 2;
                    }
                }
                return sum > 1;
            }
        }

        protected void LoadSpecificObjectListAndDetail(string objtype, string query, Action<DataRow, SpecificObjectStructure> loadFunc)
        {
            List<NameWithSchema> objfilter;
            if (!WantSpecificObjectList(objtype) && !WantSpecificObjectDetail(objtype, out objfilter)) return;

            m_db.SpecByType(objtype);
            foreach (DataRow row in query.StartsWith("@") ? CachedSchemaRows(query.Substring(1)) : CachedQueryRows(query))
            {
                SpecificObjectStructure so = new SpecificObjectStructure();
                so.ObjectType = objtype;
                so.SpecificDialect = m_dialect.DialectName;
                loadFunc(row, so);
                if (m_members.IgnoreSystemObjects && m_dialect.IsSystemObject(objtype, so.ObjectName)) continue;
                m_db.AddSpecificObject(so, true);
            }
        }

        protected virtual string DecodeSpecName(string objtype, string value)
        {
            return value;
        }


        //private void LoadSpecificObjectList(ISpecificObjectType so)
        //{
        //    if (!WantSpecificObjectList(so.ObjectType)) return;
        //    foreach (var name in so.LoadNames(m_dbConn, new ObjectPath(m_dbname)))
        //    {
        //        m_db.AddSpecificObject(so.ObjectType, DialectName, name);
        //    }
        //}

        protected string DialectName { get { return m_dialect.DialectName; } }

        // utility function for loading details about choosed specific objects
        protected void LoadSpecificObjectDetail(string objtype, Action<SpecificObjectStructure> loadFunc)
        {
            List<NameWithSchema> objfilter;
            if (!WantSpecificObjectDetail(objtype, out objfilter)) return;
            m_db.SpecByType(objtype);
            if (objfilter != null)
            {
                foreach (var oname in objfilter)
                {
                    SpecificObjectStructure speco = (SpecificObjectStructure)m_db.FindSpecificObject(objtype, oname);
                    if (speco == null) speco = m_db.AddSpecificObject(objtype, DialectName, oname);
                    loadFunc(speco);
                }
            }
            else
            {
                // load all specific objects of given type
                foreach (SpecificObjectStructure o in m_db.SpecificObjects[objtype])
                {
                    loadFunc(o);
                }
            }
        }

        public virtual bool SkipConstraint(IConstraint cnt)
        {
            return false;
        }

        protected void ReportUnknownType(string dt)
        {
            using (var ub = new UsageBuilder("warning:unknown_db_type"))
            {
                m_conn.FillInfo(ub.Params);
                ub["data_type"] = dt;
                ub["analyser"] = GetType().FullName;
            }
            Logging.Info("Unknown database type:" + dt);
        }

        public IEnumerable<DataRow> CachedQueryRows(string sql, params string[] args)
        {
            return CachedQueryRows(String.Format(sql, args));
        }

        public IEnumerable<DataRow> CachedSchemaRows(string collection)
        {
            return CachedSchemaRows(collection, null);
        }

        public DataTable CachedSchemaTable(string collection)
        {
            return CachedSchemaTable(collection, null);
        }

        public DataTable CachedSchemaTable(string collection, string[] schemaFilter)
        {
            string key = "@" + collection + "##";
            if (schemaFilter != null)
            {
                foreach (string item in schemaFilter)
                {
                    key += (item ?? "(null)") + "||";
                }
            }
            var res = AnalyserCache.GetSqlData(key);
            if (res == null)
            {
                if (schemaFilter != null) res = GetDbConn().GetSchema(collection, schemaFilter);
                else res = GetDbConn().GetSchema(collection);
                AnalyserCache.PutSqlData(key, res);
            }
            return res;
        }

        public IEnumerable<DataRow> CachedSchemaRows(string collection, string[] schemaFilter)
        {
            foreach (DataRow row in CachedSchemaTable(collection, schemaFilter).Rows)
            {
                yield return row;
            }
        }

        public DataTable CachedQueryTable(string sql)
        {
            var res = AnalyserCache.GetSqlData(sql);
            if (res == null)
            {
                try
                {
                    res = GetDbConn().LoadTableFromQuery(sql);
                    AnalyserCache.PutSqlData(sql, res);
                }
                catch (Exception err)
                {
                    AnalyserCache.PutException("analyser.sqldata", sql, err);
                    throw err;
                }
            }
            return res;
        }

        public TableStructure CachedQueryStructure(string sql)
        {
            var res = AnalyserCache.GetQueryStructure(sql);
            if (res == null)
            {
                try
                {
                    using (var cmd = GetDbConn().CreateCommand())
                    {
                        cmd.CommandText = sql;
                        using (var reader = cmd.ExecuteReader(System.Data.CommandBehavior.KeyInfo | System.Data.CommandBehavior.SchemaOnly))
                        {
                            res = reader.GetTableStructure(m_dialect);
                        }
                    }
                    AnalyserCache.PutQueryStructure(sql, res);
                }
                catch (Exception err)
                {
                    AnalyserCache.PutException("analyser.querystructure", sql, err);
                    throw err;
                }
            }
            return res;
        }

        public IEnumerable<DataRow> CachedQueryRows(string sql)
        {
            var res = CachedQueryTable(sql);
            foreach (DataRow row in res.Rows) yield return row;
        }
    }

    public static class AnalyserDatabaseCacheExtension
    {
        public static DataTable GetSqlData(this DatabaseCache cache, string sql)
        {
            return (DataTable)cache.Get("analyser.sqldata", sql);
        }
        public static void PutSqlData(this DatabaseCache cache, string sql, DataTable value)
        {
            cache.Put("analyser.sqldata", sql, value);
        }
        public static TableStructure GetQueryStructure(this DatabaseCache cache, string sql)
        {
            return (TableStructure)cache.Get("analyser.querystructure", sql);
        }
        public static void PutQueryStructure(this DatabaseCache cache, string sql, TableStructure value)
        {
            cache.Put("analyser.querystructure", sql, value);
        }
        public static TableAnalyser GetTableAnalyser(this DatabaseCache cache, string key)
        {
            return (TableAnalyser)cache.Get("analyser.tableanal", key);
        }
        public static void PutTableAnalyser(this DatabaseCache cache, string key, TableAnalyser value)
        {
            cache.Put("analyser.tableanal", key, value);
        }
        public static DataTable GetTableColumns(this DatabaseCache cache, string ns, NameWithSchema table)
        {
            string key = table != null ? table.ToString() : "@#all";
            return (DataTable)cache.Get(ns, key);
        }
        public static void PutTableColumns(this DatabaseCache cache, string ns, NameWithSchema table, DataTable value)
        {
            string key = table != null ? table.ToString() : "@#all";
            cache.Put(ns, key, value);
        }
    }

    //public class DatabaseAnalyserCache
    //{
    //    public Dictionary<string, TableAnalyser> TA = new Dictionary<string, TableAnalyser>();
    //    public Dictionary<string, List<DataRow>> SqlData = new Dictionary<string, List<DataRow>>();
    //}
}
