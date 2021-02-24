using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.Data.Common;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;

namespace Plugin.mssql
{
    public static class MySqlServerVersion
    {
        public static bool Is_2005(this SqlServerVersion version) { return version.IsMinimally(9, 0, 0); }
        public static bool Is_2008(this SqlServerVersion version) { return version.IsMinimally(10, 0, 0); }
    }

    public class MsSqlAnalyser : DatabaseAnalyser
    {
        protected override bool IsColumnNullable(IDataRecord row)
        {
            return row["IS_NULLABLE"].ToString() == "YES";
        }

        private SqlTypeBase MakeSpecificSqlType(string dt, int len, int prec, int scale)
        {
            switch (dt)
            {
                case "binary":
                    {
                        SqlTypeBinary res = new SqlTypeBinary();
                        res.Length = len;
                        return res;
                    }
                case "image":
                    return new SqlTypeImage();
                case "timestamp":
                    return new SqlTypeTimestamp();
                case "varbinary":
                    {
                        SqlTypeVarBinary res = new SqlTypeVarBinary();
                        res.Length = len;
                        return res;
                    }
                case "bit":
                    return new SqlTypeBit();
                case "tinyint":
                    return new SqlTypeTinyInt();
                case "datetime":
                    return new SqlTypeDatetime();
                case "datetime2":
                    return new SqlTypeDatetime2();
                case "datetimeoffset":
                    return new SqlTypeDatetimeOffset();
                case "date":
                    return new SqlTypeDate();
                case "time":
                    return new SqlTypeTime();
                case "smalldatetime":
                    return new SqlTypeSmallDatetime();
                case "decimal":
                    {
                        SqlTypeDecimal res = new SqlTypeDecimal();
                        res.Precision = prec;
                        res.Scale = scale;
                        return res;
                    }
                case "numeric":
                    {
                        SqlTypeNumeric res = new SqlTypeNumeric();
                        res.Precision = prec;
                        res.Scale = scale;
                        return res;
                    }
                case "float":
                    return new SqlTypeFloat();
                case "uniqueidentifier":
                    return new SqlTypeUniqueIdentifier();
                case "smallint":
                    return new SqlTypeSmallInt();
                case "int":
                    return new SqlTypeInt();
                case "bigint":
                    return new SqlTypeBigInt();
                case "real":
                    return new SqlTypeReal();
                case "char":
                    {
                        SqlTypeChar res = new SqlTypeChar();
                        res.Length = len;
                        return res;
                    }
                case "nchar":
                    {
                        SqlTypeNChar res = new SqlTypeNChar();
                        res.Length = len;
                        return res;
                    }
                case "varchar":
                    {
                        SqlTypeVarChar res = new SqlTypeVarChar();
                        res.Length = len;
                        return res;
                    }
                case "nvarchar":
                    {
                        SqlTypeNVarChar res = new SqlTypeNVarChar();
                        res.Length = len;
                        return res;
                    }
                case "text":
                    return new SqlTypeText();
                case "ntext":
                    return new SqlTypeNText();
                case "xml":
                    return new SqlTypeXml();
                case "money":
                    return new SqlTypeMoney();
                case "smallmoney":
                    return new SqlTypeSmallMoney();
                case "sql_variant":
                    return new SqlTypeVariant();
            }
            ReportUnknownType(dt);
            return new SqlTypeGeneric { Sql = dt };
        }

        protected override DbTypeBase AnalyseType(IDataRecord row, IPhysicalConnection conn, bool isdomain)
        {
            SqlTypeBase res = MakeSpecificSqlType(row);
            return res.ToGenericType();
        }

        //private SqlTypeBase MakeSpecificSqlType(IDataRecord row, IDictionary<int, string> mssql_typenames)
        //{
        //    int len, prec, scale, dt;
        //    if (!Int32.TryParse(row["CHARACTER_MAXIMUM_LENGTH"].ToString(), out len)) len = 0;
        //    if (!Int32.TryParse(row["NUMERIC_PRECISION"].ToString(), out prec)) prec = 0;
        //    if (!Int32.TryParse(row["NUMERIC_SCALE"].ToString(), out scale)) scale = 0;
        //    if (!Int32.TryParse(row["TYPE_ID"].ToString(), out dt)) dt = 0;
        //    return MakeSpecificSqlType(mssql_typenames[dt], len, prec, scale);
        //}

        private SqlTypeBase MakeSpecificSqlType(IDataRecord row)
        {
            string dt = row.DataTypeName().ToLower();
            int len = row.CharLength(), prec = row.Precision(), scale = row.Scale();
            return MakeSpecificSqlType(dt, len, prec, scale);
        }

        private Dictionary<int, string> GetTypeNames(IPhysicalConnection conn)
        {
            var res = (Dictionary<int, string>)conn.Cache.Get("mssql_typenames");
            if (res != null) return res;
            using (DbCommand cmd = conn.DbFactory.CreateCommand())
            {
                cmd.Connection = conn.SystemConnection;
                cmd.CommandText = "select a.xtype, a.name from systypes a where usertype = (select min(b.usertype) from systypes b where a.xtype = b.xtype)";
                Dictionary<int, string> names = new Dictionary<int, string>();
                using (DbDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        names[Int32.Parse(reader[0].ToString())] = reader[1].ToString();
                    }
                }
                conn.Cache.Put("mssql_typenames", names);
                return names;
            }
        }

        protected override DataTable LoadTableColumnsTable(NameWithSchema table)
        {
            string sql = "SELECT * FROM INFORMATION_SCHEMA.COLUMNS";
            if (table != null)
            {
                sql += String.Format(" WHERE TABLE_NAME='{0}'", table.Name);
                if (table.Schema != null) sql += String.Format(" AND TABLE_SCHEMA='{0}'", table.Schema);
            }
            return GetDbConn().LoadTableFromQuery(sql);
        }

        private string AddUsrObjFilter(string sql, NameWithSchema table, string conj)
        {
            if (table != null)
            {
                sql += String.Format(" {0} obj.name='{1}'", conj, table.Name);
                if (table.Schema != null) sql += String.Format(" AND usr.name='{0}'", table.Schema);
            }
            return sql;
        }

        private string AddJoinAndFilter(string sql, string objidexpr, NameWithSchema table)
        {
            sql += " INNER JOIN sysobjects obj ON obj.id=" + objidexpr + " INNER JOIN sysusers usr ON obj.uid=usr.uid";
            return AddUsrObjFilter(sql, table, "WHERE");
        }

        private DataTable LoadIdentityColumnsTable(NameWithSchema table)
        {
            try
            {
                string sql = AddJoinAndFilter("SELECT col.name as COLUMN_NAME, col.increment_value, col.seed_value, obj.name AS TABLE_NAME, usr.name as TABLE_SCHEMA FROM sys.identity_columns col", "col.object_id", table);
                return GetDbConn().LoadTableFromQuery(sql);
            }
            catch (Exception err)
            {
                Logging.Warning("Error loading sys.identity_columns:" + err.Message + "; trying syscolumns table");
                try
                {
                    string sql = AddJoinAndFilter("select c.name as COLUMN_NAME, c.colstat as COLUMN_STAT, obj.name AS TABLE_NAME, usr.name as TABLE_SCHEMA from syscolumns c", "c.id", table);
                    return GetDbConn().LoadTableFromQuery(sql);
                }
                catch (Exception err2)
                {
                    Logging.Warning("Error loading identity:" + err2.Message);
                    return null;
                }
            }
        }

        private void LoadIdentityColumns(TableStructure tbl)
        {
            var cols = GetCachedTableColumnsTable("mssql.identity_columns", LoadIdentityColumnsTable, tbl.FullName);
            if (cols == null) return;
            bool oldver = cols.Columns.IndexOf("increment_value") < 0;
            foreach (DataRow row in cols.Rows)
            {
                var col = (ColumnStructure)tbl.Columns[row.SafeString("COLUMN_NAME")];
                if (oldver)
                {
                    int colstat = row.SafeString("COLUMN_STAT").SafeIntParse();
                    if ((colstat & 1) != 0)
                    {
                        col.DataType.SetAutoincrement(true);
                        // this is neccessery for correctly set ms_sql attributes
                        col.DataType = m_dialect.GenericTypeToSpecific(col.DataType).ToGenericType();
                    }
                }
                else
                {
                    ISpecificType spec = m_dialect.GenericTypeToSpecific(col.DataType);
                    var autoinc = spec as IMsSqlAutoIncrement;
                    if (autoinc != null)
                    {
                        autoinc.IsIdentity = true;
                        autoinc.IdentityIncrement = row.SafeString("increment_value").SafeIntParse();
                        autoinc.IdentitySeed = row.SafeString("seed_value").SafeIntParse();
                        col.DataType = spec.ToGenericType();
                    }
                }
            }
        }

        protected override void LoadTableColumns(TableStructure tbl)
        {
            base.LoadTableColumns(tbl);
            LoadIdentityColumns(tbl);
            LoadDefaultConstraintNames(tbl);
        }

        private DataTable LoadDefaultConstraintNamesTable(NameWithSchema table)
        {
            string sql = AddUsrObjFilter(StdScripts.getdefaultconstraints, table, "AND");
            return GetDbConn().LoadTableFromQuery(sql);
        }

        private void LoadDefaultConstraintNames(TableStructure tbl)
        {
            try
            {
                var cols = GetCachedTableColumnsTable("mssql.default_constraints", LoadDefaultConstraintNamesTable, tbl.FullName);
                if (cols == null) return;
                foreach (DataRow row in cols.Rows)
                {
                    var col = (ColumnStructure)tbl.Columns[row.SafeString("colname").ToString()];
                    string cntname = row.SafeString("cntname").ToString();
                    col.SetSpecificAttribute("mssql", "defname", cntname);
                }
            }
            catch (Exception err)
            {
                Logging.Warning("Error loading default values names:" + err.Message);
            }
        }

        private DataTable LoadIndexesTable(NameWithSchema table)
        {
            string sqlidxlist = StdScripts.getindexes.Replace("#RETURNALL#", table == null ? "1" : "0");
            if (table != null) sqlidxlist = sqlidxlist.Replace("#TABLE#", table.Name).Replace("#SCHEMA#", table.Schema).Replace("#INDEX#", "%");
            sqlidxlist = ReplaceMsSqlPlaceholders(sqlidxlist);
            return GetDbConn().LoadTableFromQuery(sqlidxlist);
        }

        private DataTable LoadIndexColsTable(NameWithSchema table)
        {
            string sqlidxcols = StdScripts.getindexcols.Replace("#RETURNALL#", table == null ? "1" : "0");
            if (table != null) sqlidxcols = sqlidxcols.Replace("#TABLE#", table.Name).Replace("#SCHEMA#", table.Schema).Replace("#INDEX#", "%");
            sqlidxcols = ReplaceMsSqlPlaceholders(sqlidxcols);
            return GetDbConn().LoadTableFromQuery(sqlidxcols);
        }

        protected override void LoadIndexes(TableStructure table)
        {
            try
            {
                string tblname = table.FullName.Name;
                string schema = table.FullName.Schema;

                DataTable indexlist = GetCachedTableColumnsTable("indexes", LoadIndexesTable, table.FullName);
                DataTable indexcols = GetCachedTableColumnsTable("indexcols", LoadIndexColsTable, table.FullName);
                TableAnalyser tadidx = new TableAnalyser();
                Dictionary<string, bool> dct = new Dictionary<string, bool>();
                foreach (DataRow row in indexlist.Rows)
                {
                    TableAnalyser.Key key = new TableAnalyser.Key();
                    key.keytype = "INDEX";
                    key.keyisunique = Int32.Parse(row["IsUnique"].ToString()) == 1;
                    key.keyname = row["Name"].ToString();
                    key.tblname = tblname;
                    key.tblschema = schema;
                    dct[key.keyname] = true;
                    tadidx.keys.Add(key);
                }
                foreach (DataRow row in indexcols.Rows)
                {
                    TableAnalyser.Col col = new TableAnalyser.Col();
                    col.keytype = "INDEX";
                    col.keyname = row["IndexName"].ToString();
                    col.tblname = tblname;
                    col.tblschema = schema;
                    col.ordinal = row["KeyOrder"].ToString();
                    col.colname = row["ColumnName"].ToString();
                    if (!dct.ContainsKey(col.keyname)) continue;
                    tadidx.cols.Add(col);
                }
                tadidx.SaveConstraints(table, this);
            }
            catch (Exception err)
            {
                Logging.Warning("Error loading MSSQL indexes:" + err.ToString());
            }
            //table.FilledMembers |= TableStructureMembers.Indexes;
        }
        
        protected override string GetDefaultValueExpression(string def)
        {
            while (def != null && def.StartsWith("(") && def.EndsWith(")"))
            {
                def = def.Substring(1, def.Length - 2);
            }
            var defre = new Regex(@"create\s+default\s+\[[^\]]+\]\s+as\s+(.*)", RegexOptions.IgnoreCase);
            var ma = defre.Match(def);
            if (def != null && ma.Success)
            {
                return ma.Groups[1].Value.Trim();
            }
            return def;
        }

        private DataTable LoadProgrammableTable(NameWithSchema obj)
        {
            string sql = ReplaceMsSqlPlaceholders(StdScripts.loadprogrammable);

            if (obj != null)
            {
                sql = sql.Replace("#NAME#", obj.Name);
                sql = sql.Replace("#SCHEMA#", obj.Schema);
            }
            sql = sql.Replace("#RETURNALL#", obj != null ? "0" : "1");
            return GetDbConn().LoadTableFromQuery(sql);
        }

        private DataTable CachedLoadProgrammable(NameWithSchema obj)
        {
            var res = AnalyserCache.GetProgrammable(obj);
            if (res != null) return res;
            res = AnalyserCache.GetProgrammable(null);
            if (res != null) return FilterProgrammable(res, obj);
            if (LoadMoreSpecificObjects)
            {
                res = LoadProgrammableTable(null);
                AnalyserCache.PutProgrammable(null, res);
                return FilterProgrammable(res, obj);
            }
            else
            {
                res = LoadProgrammableTable(obj);
                AnalyserCache.PutProgrammable(obj, res);
                return res;
            }
        }

        private DataTable FilterProgrammable(DataTable table, NameWithSchema obj)
        {
            var res = table.Clone();
            foreach (DataRow row in table.Rows)
            {
                if (obj.Schema != null && String.Compare(row.SafeString("OBJ_SCHEMA"), obj.Schema, true) != 0) continue;
                if (obj.Name != null && String.Compare(row.SafeString("OBJ_NAME"), obj.Name, true) != 0) continue;
                res.ImportRow(row);
            }
            return res;
        }

        protected void LoadProgrammable(SpecificObjectStructure obj)
        {
            var tbl = CachedLoadProgrammable(obj.ObjectName);
            var sb = new StringBuilder();
            foreach (DataRow row in tbl.Rows)
            {
                sb.Append(row["CODE_TEXT"].SafeToString());
            }
            obj.CreateSql = sb.ToString();
        }

        protected void LoadTrigger(SpecificObjectStructure obj)
        {
            LoadProgrammable(obj);
            var tbl = CachedQueryTable(ReplaceMsSqlPlaceholders(StdScripts.loadtrigger));
            foreach (DataRow row in tbl.Rows)
            {
                if (obj.ObjectName.Schema != null && String.Compare(row.SafeString("SchemaName"), obj.ObjectName.Schema, true) != 0) continue;
                if (obj.ObjectName.Name != null && String.Compare(row.SafeString("Name"), obj.ObjectName.Name, true) != 0) continue;
                obj.RelatedTable = new NameWithSchema(row["TableSchema"].ToString(), row["TableName"].ToString());
            }
        }

        protected override void LoadSpecificObjects()
        {
            base.LoadSpecificObjects();

            LoadSpecificObjectList("procedure", ReplaceMsSqlPlaceholders("SELECT o.name, u.name as sname FROM sysobjects o INNER JOIN #SCHEMAS# u ON u.#SCHEMAID#=o.uid WHERE xtype in ('P')"), "sname", "name");
            LoadSpecificObjectDetail("procedure", LoadProgrammable);

            LoadSpecificObjectList("function", ReplaceMsSqlPlaceholders("SELECT o.name, u.name as sname FROM sysobjects o INNER JOIN #SCHEMAS# u ON u.#SCHEMAID#=o.uid WHERE xtype in ('FN', 'IF', 'TF')"), "sname", "name");
            LoadSpecificObjectDetail("function", LoadProgrammable);

            LoadSpecificObjectList("trigger", ReplaceMsSqlPlaceholders(StdScripts.gettriggers), "SchemaName", "Name");
            LoadSpecificObjectDetail("trigger", LoadTrigger);

            //LoadSpecificObjectListAndDetail("trigger", StdScripts.gettriggers, "SchemaName", "Name", LoadTriggerRow);
        }

        private string ReplaceMsSqlPlaceholders(string sql)
        {
            if (m_dialect.Version.Is_2005())
            {
                sql = sql.Replace("#SCHEMAS#", "sys.schemas");
                sql = sql.Replace("#SCHEMAID#", "schema_id");
            }
            else
            {
                sql = sql.Replace("#SCHEMAS#", "sysusers");
                sql = sql.Replace("#SCHEMAID#", "uid");
            }
            return sql;
        }

        protected override void LoadViews(bool listOnly)
        {
            LoadSpecificObjectList("view", ReplaceMsSqlPlaceholders("SELECT o.name, u.name as sname FROM sysobjects o INNER JOIN #SCHEMAS# u ON u.#SCHEMAID#=o.uid WHERE xtype in ('V')"), "sname", "name");
            if (!listOnly) LoadSpecificObjectDetail("view", LoadProgrammable);
        }

        protected override string DecodeSpecName(string objtype, string value)
        {
            if (value == null) return value;
            if (objtype == "procedure" || objtype == "trigger")
            {
                int pos = value.IndexOf(';');
                if (pos >= 0) return value.Substring(0, pos);
            }
            return value;
        }

        //private void LoadTriggerRow(DataRow row, SpecificObjectStructure so)
        //{
        //    so.CreateSql = row["Definition"].SafeToString();
        //    so.RelatedTable = new NameWithSchema(row.SafeString("TableSchema"), row.SafeString("TableName"));
        //}

        private static string SysObjectsCategoryToSpecType(string category)
        {
            switch (category.Trim())
            {
                case "FN":
                case "IF":
                case "TF":
                    return "function";
                case "P":
                    return "procedure";
                case "TR":
                    return "trigger";
                case "U":
                    return "table";
                case "V":
                    return "view";
            }
            return null;
        }

        protected override void LoadSchemata()
        {
            if (!m_members.SchemaList && !m_members.SchemaDetails) return;
            DataTable tbl;
            try { tbl = CachedQueryTable("SELECT * FROM sys.schemas"); }
            catch { tbl = CachedQueryTable("SELECT * FROM sysusers"); }
            //m_db.Schemata = new SchemaCollection();
            foreach (DataRow row in tbl.Rows)
            {
                m_db.Schemata.Add(new SchemaStructure { SchemaName = row["name"].SafeToString() });
            }
        }

        protected override void LoadDependencies()
        {
            if (!m_members.LoadDependencies) return;

            MsSqlSettings cfg = GlobalSettings.Pages.PageByName("mssql_client") as MsSqlSettings;
            if (!cfg.UseNativeDependencies) return;

            foreach (SpecificObjectStructure obj in m_db.GetAllSpecificObjects()) obj.DependsOn = new List<DependencyItem>();
            foreach (var row in CachedQueryRows(ReplaceMsSqlPlaceholders(StdScripts.getdepends)))
            {
                string mainspec = SysObjectsCategoryToSpecType(row.SafeString("MainType"));
                NameWithSchema main = new NameWithSchema(row.SafeString("MainSchema"), row.SafeString("MainName"));

                string refspec = SysObjectsCategoryToSpecType(row.SafeString("RefType"));
                NameWithSchema refn = new NameWithSchema(row.SafeString("RefSchema"), row.SafeString("RefName"));

                if (mainspec != null && refspec != null)
                {
                    if (m_db.SpecificObjects.ContainsKey(mainspec) && m_db.SpecificObjects[mainspec].GetIndex(main) >= 0)
                    {
                        SpecificObjectStructure obj = (SpecificObjectStructure)m_db.SpecificObjects[mainspec][main];
                        var newdep = new DependencyItem { Name = refn, ObjectType = refspec };
                        if (!obj.DependsOn.Contains(newdep)) obj.DependsOn.Add(newdep);
                    }
                }
            }
        }

        protected override void LoadDatabaseOptions()
        {
            if (!m_members.DatabaseOptions) return;
            var tbl = CachedQueryTable(String.Format(@"SELECT DATABASEPROPERTYEX('{0}', 'Collation') AS [Collation]", m_dbname));
            m_db.SpecificData["mssql.collation"] = tbl.Rows[0]["Collation"].ToString();
        }
        protected override void LoadCollations()
        {
            if (!m_members.CollationList && !m_members.CollationDetails) return;
            if (m_members.IgnoreSystemObjects) return;
            DataTable tbl = CachedQueryTable("SELECT * FROM ::fn_helpcollations()");
            foreach (DataRow row in tbl.Rows)
            {
                m_db.Collations.Add(new CollationStructure { Name = row["name"].SafeToString() });
            }
        }
    }

    public static class AnalyserCacheExtension
    {
        public static DataTable GetProgrammable(this DatabaseCache cache, NameWithSchema obj)
        {
            string key = obj != null ? obj.ToString() : "@#all";
            return (DataTable)cache.Get("mssql.programmable", key);
        }
        public static void PutProgrammable(this DatabaseCache cache, NameWithSchema obj, DataTable value)
        {
            string key = obj != null ? obj.ToString() : "@#all";
            cache.Put("mssql.programmable", key, value);
        }
    }
}
