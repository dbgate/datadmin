using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Text.RegularExpressions;
using Antlr.Runtime;
using System.IO;
using Antlr.Runtime.Tree;
using System.Xml;

namespace Plugin.mysql
{
    public static class MySqlServerVersion
    {
        public static bool Is_5_0_17(this SqlServerVersion version)  {return version.IsMinimally(5, 0, 17); }
        public static bool Is_5_0_10(this SqlServerVersion version) { return version.IsMinimally(5, 0, 10); }
        public static bool Is_5_1_0(this SqlServerVersion version) { return version.IsMinimally(5, 1, 0); }
    }

    public class MySqlParseCreateTableError : UnexpectedError
    {
        public MySqlParseCreateTableError(string errors, string sql)
            : base(errors, null)
        {
            this.Data["sql"] = sql;
        }
    }

    public class MySqlAnalyser : DatabaseAnalyser
    {
        //MySqlServerVersion m_version;
        HashSetEx<string> m_loadedTables = new HashSetEx<string>();
        Dictionary<string, string> m_variables;

        //protected override void ParseVersion()
        //{
        //    m_version = new MySqlServerVersion(m_serverVersion);
        //}

        protected void WantShowTable(TableStructure table)
        {
            if (m_loadedTables.Contains(table.FullName.Name)) return;
            LoadShowTable(table);
            m_loadedTables.Add(table.FullName.Name);
        }

        protected override void LoadTableList()
        {
            if (m_members.TableList || m_members.TableMembers.Contains(TableStructureMembers.SpecificDetails))
            {
                foreach (var row in CachedQueryRows("SHOW TABLE STATUS"))
                {
                    string schema = m_dbname;
                    string name = row.SafeString(0);
                    string engine = row.SafeString(1);
                    if (String.IsNullOrEmpty(engine)) continue;

                    if (m_dialect.IsSystemTable(schema, name) && m_members.IgnoreSystemObjects) continue;
                    var fullname = NewNameWithSchema(schema, name);
                    TableStructure tbl;
                    if (m_db.Tables.GetIndex(fullname) >= 0) tbl = (TableStructure)m_db.Tables[fullname];
                    else tbl = m_db.AddTable(fullname);

                    tbl.SpecificData["mysql.engine"] = engine;
                    tbl.Comment = row.SafeString("Comment");
                    if (row.SafeString("Auto_increment") != null) tbl.SpecificData["mysql.auto_increment"] = row.SafeString("Auto_increment");
                    if (row.SafeString("Collation") != null) tbl.SpecificData["mysql.collation"] = row.SafeString("Collation");
                }
            }
        }

        private string CachedGetShowTable(string table)
        {
            string data = AnalyserCache.GetShowTable(table);
            if (data == null)
            {
                using (DbCommand cmd = GetDbConn().CreateCommand())
                {
                    cmd.CommandText = String.Format("SHOW CREATE TABLE `{0}`", table);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (!reader.Read()) throw new InternalError("DAE-00056 Error reading MySQL table, empty result of SHOW CREATE TABLE");
                        data = reader[1].SafeToString();
                        AnalyserCache.PutShowTable(table, data);
                    }
                }
            }
            return data;
        }

        protected void LoadShowTable(TableStructure table)
        {
            string sql = CachedGetShowTable(table.FullName.Name);

            MySQLLexer lexer = new MySQLLexer(new ANTLRReaderStream(new StringReader(sql)));
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            MySQLParser parser = new MySQLParser(tokens);
            parser.create_table(table, false);
            if (parser.Errors != null)
            {
                throw new MySqlParseCreateTableError(parser.Errors, sql);
            }
            // remove indexes, which only reflects foreign keys
            table._Constraints.RemoveIf(IndexShouldBeRemoved);
            // fix primary key table name
            foreach (var cnt in table.Constraints)
            {
                var fk = cnt as ForeignKey;
                if (fk == null) continue;
                fk.PrimaryKeyTable = NewNameWithSchema(table.FullName.Schema, fk.PrimaryKeyTable.Name);
            }
        }

        private static bool IndexShouldBeRemoved(IConstraint cnt)
        {
            var table = cnt.Table;
            var idx = cnt as IIndex;
            if (idx == null) return false;
            if (String.IsNullOrEmpty(cnt.Name)) return false;
            foreach (var c in table.Constraints)
            {
                if (c is IIndex) continue;
                if (c is IForeignKey && c.Name == cnt.Name) return true;
            }
            return false;
        }

        protected override void AfterLoadIndexesOrConstraints()
        {
            // load all tables (because of correct values on update/on delete of references)
            if (m_members.TableMembers.ContainsAny(TableStructureMembers.ReferencedFrom))
            {
                // only if references are loaded
                foreach (TableStructure table in m_db.Tables)
                {
                    WantShowTable(table);
                }
            }
        }

        protected override void LoadTableColumns(TableStructure table)
        {
            WantShowTable(table);
        }

        protected override TableStructureMembers GetConstraintLoadMode()
        {
            return TableStructureMembers.ReferencedFrom;
        }

        protected override void LoadConstraints(TableStructure table)
        {
            WantShowTable(table);
            if (m_members.TableMembers.Contains(TableStructureMembers.ReferencedFrom))
            {
                base.LoadConstraints(table);
            }
        }

        protected override void LoadIndexes(TableStructure table)
        {
            WantShowTable(table);
        }

        protected override string GetDefaultValueExpression(string def)
        {
            if (String.IsNullOrEmpty(def)) return null;
            if (def.ToUpper().StartsWith("CURRENT_TIMESTAMP") || def.ToUpper().StartsWith("NOW")) return "NOW()";
            if (def.ToUpper().StartsWith("UTC_TIMESTAMP")) return "UTC_TIMESTAMP()";
            // handle boolean
            if (def.Length == 1 && (int)def[0] <= 1) return ((int)def[0]).ToString();
            double tmp;
            if (Double.TryParse(def, System.Globalization.NumberStyles.Any, CultureInfo.InvariantCulture, out tmp)) return def;
            return "'" + def + "'";
        }

        //protected override void LoadTableRowDetails(DataRow row, TableStructure tbl)
        //{
        //    tbl.SpecificData["mysql.engine"] = row.SafeString("ENGINE");
        //    tbl.Comment = row.SafeString("TABLE_COMMENT");
        //    if (row.SafeString("AUTO_INCREMENT") != null) tbl.SpecificData["mysql.auto_increment"] = row.SafeString("AUTO_INCREMENT");
        //    if (row.SafeString("TABLE_COLLATION") != null) tbl.SpecificData["mysql.collation"] = row.SafeString("TABLE_COLLATION");
        //}

        protected void LoadProcedure(SpecificObjectStructure obj)
        {
            var tbl = CachedQueryTable(String.Format("SHOW CREATE PROCEDURE `{0}`", obj.ObjectName.Name));
            obj.CreateSql = tbl.Rows[0][2].SafeToString();
        }
        protected void LoadFunction(SpecificObjectStructure obj)
        {
            var tbl = CachedQueryTable(String.Format("SHOW CREATE FUNCTION `{0}`", obj.ObjectName.Name));
            obj.CreateSql = tbl.Rows[0][2].SafeToString();
        }
        protected void LoadEvent(SpecificObjectStructure obj)
        {
            var tbl = CachedQueryTable(String.Format("SHOW CREATE EVENT `{0}`", obj.ObjectName.Name));
            obj.CreateSql = tbl.Rows[0][3].SafeToString();
        }

        protected void LoadTrigger(DataRow row, SpecificObjectStructure obj)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("CREATE TRIGGER ");
            sb.Append("`" + row["Trigger"].SafeToString() + "`");
            sb.Append(" " + row["Timing"].ToString());
            sb.Append(" " + row["Event"].ToString());
            sb.Append(" ON " + m_dialect.QuoteIdentifier(row["Table"].ToString()));
            sb.Append("\nFOR EACH ROW ");
            sb.Append(row["Statement"].ToString());
            obj.CreateSql = sb.ToString();
        }

        protected void LoadEngine(DataRow row, SpecificObjectStructure obj)
        {
            obj.Comment = row.SafeString("Comment");
            obj.SpecificData["supported"] = (row.SafeString("Supported") == "YES") ? "1" : "0";
        }

        protected void LoadViewDetail(SpecificObjectStructure obj)
        {
            var tbl = CachedQueryTable(String.Format("SHOW CREATE VIEW `{0}`", obj.ObjectName.Name));
            obj.CreateSql = tbl.Rows[0][1].SafeToString();
        }

        protected override void LoadViews(bool listOnly)
        {
            //LoadSpecificObjectList("view", "SELECT * FROM INFORMATION_SCHEMA.VIEWS WHERE TABLE_SCHEMA='" + m_dbname + "'", null, "TABLE_NAME");
            LoadSpecificObjectList("view", "SHOW TABLE STATUS WHERE Engine IS NULL", null, "Name");
            if (!listOnly) LoadSpecificObjectDetail("view", LoadViewDetail);
        }

        protected override ConstraintsLoader.Condition GetDbNameCondition()
        {
            return new ConstraintsLoader.EqualCondition("table_schema", m_dbname, false);
        }

        protected override void LoadSpecificObjects()
        {
            base.LoadSpecificObjects();

            LoadSpecificObjectList("procedure", "SHOW PROCEDURE STATUS WHERE Db='" + m_dbname + "'", null, "Name");
            LoadSpecificObjectDetail("procedure", LoadProcedure);

            LoadSpecificObjectList("function", "SHOW FUNCTION STATUS WHERE Db='" + m_dbname + "'", null, "Name");
            LoadSpecificObjectDetail("function", LoadFunction);

            LoadSpecificObjectListAndDetail("trigger", "SHOW TRIGGERS", null, "Trigger", LoadTrigger);

            try
            {
                LoadSpecificObjectList("mysql.event", "SHOW EVENTS", null, "Name");
                LoadSpecificObjectDetail("mysql.event", LoadEvent);
            }
            catch (Exception err)
            {
                m_progress.Warning("Failed to load MySQL events:" + err.Message);
            }

            if (!m_members.IgnoreSystemObjects)
            {
                LoadSpecificObjectListAndDetail("mysql.engine", "SHOW ENGINES", null, "Engine", LoadEngine);
            }
            //LoadSpecificObjectDetail("trigger", LoadTrigger);

            //LoadSpecificObjectListAndDetail
           // LoadSpecificObjectList("procedure", "SELECT * FROM sysobjects WHERE xtype in ('P')", null, "name");

            //LoadSpecificObjectList("function", "SELECT * FROM sysobjects WHERE xtype in ('FN', 'IF', 'TF')", null, "name");
            //LoadSpecificObjectDetail("function", LoadProgrammable);

            //LoadSpecificObjectListAndDetail("trigger", MsSqlTriggerType.FormatQuery(null, null), "SchemaName", "Name", LoadTriggerRow);
        }

        protected override void LoadDatabaseOptions()
        {
            if (!m_members.DatabaseOptions) return;
            WantVariables();
            if (m_variables.ContainsKey("character_set_database")) m_db.SpecificData["mysql.character_set"] = m_variables["character_set_database"];
            if (m_variables.ContainsKey("collation_database")) m_db.SpecificData["mysql.collation"] = m_variables["collation_database"];
        }

        private void WantVariables()
        {
            if (m_variables != null) return;
            m_variables = new Dictionary<string, string>();
            var tbl = GetDbConn().LoadTableFromQuery("SHOW VARIABLES");
            foreach (DataRow row in tbl.Rows)
            {
                m_variables[row[0].ToString()] = row[1].ToString();
            }
        }
    }

    public static class AnalyserCacheExtension
    {
        public static string GetShowTable(this DatabaseCache cache, string table)
        {
            return (string)cache.Get("mysql.createtable", table);
        }
        public static void PutShowTable(this DatabaseCache cache, string table, string data)
        {
            cache.Put("mysql.createtable", table, data);
        }
    }
}
