using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.Data;
using System.Text.RegularExpressions;

namespace Plugin.oracle
{
    public class OracleAnalyser : DatabaseAnalyser
    {
        static Regex m_intervalDayToSecond = new Regex(@"INTERVAL.*DAY.*TO.*SECOND", RegexOptions.IgnoreCase);
        static Regex m_intervalYearToMonth = new Regex(@"INTERVAL.*YEAR.*TO.*MONTH", RegexOptions.IgnoreCase);
        static Regex m_timestampWithLocalTimezone = new Regex(@"TIMESTAMP.*WITH.*LOCAL.*TIMEZONE", RegexOptions.IgnoreCase);
        static Regex m_timestampWithTimezone = new Regex(@"TIMESTAMP.*WITH.*TIMEZONE", RegexOptions.IgnoreCase);
        static Regex m_timestamp = new Regex(@"TIMESTAMP.*", RegexOptions.IgnoreCase);

        protected override void LoadTableList()
        {
            if (m_members.TableList || m_members.TableMembers.Contains(TableStructureMembers.SpecificDetails))
            {
                foreach (var row in CachedQueryRows("select u.USERNAME as TABLE_SCHEMA, t.TABLE_NAME from USER_TABLES t, USER_USERS u"))
                {
                    string schema = row.SafeString(0);
                    string name = row.SafeString(1);

                    if (m_dialect.IsSystemTable(schema, name) && m_members.IgnoreSystemObjects) continue;
                    var fullname = NewNameWithSchema(schema, name);
                    TableStructure tbl;
                    if (m_db.Tables.GetIndex(fullname) >= 0) tbl = (TableStructure)m_db.Tables[fullname];
                    else tbl = m_db.AddTable(fullname);
                }
            }
        }

        protected override DataTable LoadTableColumnsTable(NameWithSchema table)
        {
            string sql = "select * from USER_TAB_COLUMNS ";
            if (table != null) sql += " where TABLE_NAME='" + table.Name + "'";
            sql += " order by COLUMN_ID";
            DataTable columns = GetDbConn().LoadTableFromQuery(sql);
            return columns;
        }

        protected override string GetDefaultValueExpression(string valueFromInfoSchema)
        {
            if (valueFromInfoSchema != null && String.Compare(valueFromInfoSchema.Trim(), "NULL", true) == 0) return null;
            return base.GetDefaultValueExpression(valueFromInfoSchema);
        }

        protected override void ProcessTableColumnsTable(TableStructure table, DataTable columns)
        {
            foreach (DataRow row in columns.Rows)
            {
                ColumnStructure col = table.AddColumn(row["COLUMN_NAME"].ToString(), AnalyseType(new DataRowAdapter(row), m_conn, false));
                LoadTableColumn(col, row);
            }
        }

        //protected override void LoadTableColumns(TableStructure table)
        //{
        //    DataTable columns = GetDbConn().LoadTableFromQuery("select * from USER_TAB_COLUMNS where TABLE_NAME='" + table.Name + "' order by COLUMN_ID");
        //    foreach (DataRow row in columns.Rows)
        //    {
        //        ColumnStructure col = table.AddColumn(row["COLUMN_NAME"].ToString(), AnalyseType(new DataRowAdapter(row), m_conn, false));
        //        LoadTableColumn(col, row);
        //    }
        //}

        protected override bool IsColumnNullable(IDataRecord row)
        {
            return row.SafeString("NULLABLE") == "Y";
        }

        private OracleTypeBase MakeSpecificOracleType(IDataRecord row)
        {
            string dt = row.DataTypeName().ToUpper();
            int len = row.CharLength();
            int? prec = row.NPrecision(), scale = row.NScale();
            return MakeSpecificOracleType(row, dt, len,  prec, scale);
        }

        private OracleTypeBase MakeSpecificOracleType(IDataRecord row, string dt, int len, int? prec, int? scale)
        {
            switch (dt)
            {
                case "VARCHAR2":
                    return new OracleTypeVarChar2 { Length = len };
                case "NVARCHAR2":
                    return new OracleTypeNVarChar2 { Length = len };
                case "CHAR":
                    return new OracleTypeChar { Length = len };
                case "NCHAR":
                    return new OracleTypeNChar { Length = len };
                case "NUMBER":
                    if (prec == null && scale == 0) return new OracleTypeInteger();
                    return new OracleTypeNumber { Precision = prec, Scale = scale };
                case "BINARY_FLOAT":
                    return new OracleTypeBinaryFloat();
                case "BINARY_DOUBLE":
                    return new OracleTypeBinaryDouble();
                case "LONG":
                    return new OracleTypeLong();
                case "DATE":
                    return new OracleTypeDate();
                case "RAW":
                    return new OracleTypeRaw { Length = row.SafeString("DATA_LENGTH").SafeIntParse() };
                case "LONG RAW":
                    return new OracleTypeLongRaw();
                case "ROWID":
                    return new OracleTypeRowId();
                case "UROWID":
                    return new OracleTypeURowId();
                case "MLSLABEL":
                    return new OracleTypeMlsLabel();
                case "CLOB":
                    return new OracleTypeClob();
                case "NCLOB":
                    return new OracleTypeNClob();
                case "BLOB":
                    return new OracleTypeBlob();
                case "BFILE":
                    return new OracleTypeBFile();
                case "XMLTYPE":
                    return new OracleTypeXml();
            }
            if (m_intervalDayToSecond.Match(dt).Success)
            {
                return new OracleTypeIntervalDayToSecond { DayPrecision = prec, FractionalPrecision = scale };
            }
            if (m_intervalYearToMonth.Match(dt).Success)
            {
                return new OracleTypeIntervalYearToMonth { YearPrecision = prec };
            }
            if (m_timestampWithLocalTimezone.Match(dt).Success)
            {
                return new OracleTypeTimestamp { TimeZone = TimeZoneType.Local, FractionalPrecision = scale };
            }
            if (m_timestampWithTimezone.Match(dt).Success)
            {
                return new OracleTypeTimestamp { TimeZone = TimeZoneType.Explicit, FractionalPrecision = scale };
            }
            if (m_timestamp.Match(dt).Success)
            {
                return new OracleTypeTimestamp { TimeZone = TimeZoneType.None, FractionalPrecision = scale };
            }
            ReportUnknownType(dt);
            return new OracleTypeGeneric { Sql = dt };
        }

        protected override DbTypeBase AnalyseType(IDataRecord row, IPhysicalConnection conn, bool isdomain)
        {
            var res = MakeSpecificOracleType(row);
            return res.ToGenericType();
        }

        protected override void LoadSchemata()
        {
            if (!m_members.SchemaList && !m_members.SchemaDetails) return;
            foreach (DataRow row in CachedQueryRows("select USERNAME from USER_USERS"))
            {
                m_db.Schemata.Add(new SchemaStructure { SchemaName = row["USERNAME"].SafeToString() });
            }
            
            base.LoadSchemata();
        }

        private string OracleConstraintToDatAdmin(string ctype)
        {
            switch (ctype)
            {
                case "P":
                    return "PRIMARY KEY";
                case "R":
                    return "FOREIGN KEY";
                case "U":
                    return "UNIQUE";
                case "C":
                    return "CHECK";
            }
            return null;
        }

        private TableAnalyser GetRefsTA()
        {
            var res = AnalyserCache.GetTableAnalyser("refs");
            if (res != null) return res;
            var ta = new TableAnalyser();
            foreach (var row in CachedQueryRows(SqlScripts.getrefs))
            {
                var key = new TableAnalyser.Key();
                key.keytype = "FOREIGN KEY";

                key.deleterule = row.SafeString("DELETE_RULE");

                key.keyname = row["FK_CONSTRAINT"].ToString();
                key.tblname = row["FK_TABLE"].ToString();
                key.tblschema = row["FK_OWNER"].ToString();

                key.dstpkschema = row["R_OWNER"].ToString();
                key.dstpkname = row["R_CONSTRAINT"].ToString();

                key.dsttblschema = row["R_OWNER"].ToString();
                key.dsttblname = row["R_TABLE"].ToString();

                ta.keys.Add(key);
            }

            foreach (var row in CachedQueryRows(SqlScripts.getrefcols))
            {
                var col = new TableAnalyser.Col();
                col.keytype = "FOREIGN KEY";

                col.keyname = row["FK_CONSTRAINT"].ToString();
                col.tblname = row["FK_TABLE"].ToString();
                col.tblschema = row["FK_OWNER"].ToString();
                col.colname = row["FK_COLUMN_NAME"].ToString();

                col.dstcolname = row["R_COLUMN_NAME"].ToString();

                ta.cols.Add(col);
            }

            AnalyserCache.PutTableAnalyser("refs", ta);
            return ta;
        }

        private TableAnalyser GetIndexesTA()
        {
            var res = AnalyserCache.GetTableAnalyser("indexes");
            if (res != null) return res;
            var ta = new TableAnalyser();
            ta.AllowDeduceFromColumns = false;
            foreach (var row in CachedQueryRows(SqlScripts.getindexes))
            {
                var key = new TableAnalyser.Key();
                key.keytype = "INDEX";

                key.keyname = row["INDEX_NAME"].ToString();
                key.keyschema = row["TABLE_OWNER"].ToString();

                key.tblname = row["TABLE_NAME"].ToString();
                key.tblschema = row["TABLE_OWNER"].ToString();

                key.keyisunique = row["UNIQUENESS"].SafeToString() == "UNIQUE";

                ta.keys.Add(key);
            }

            foreach (var row in CachedQueryRows(SqlScripts.getindexcols))
            {
                var col = new TableAnalyser.Col();
                col.keytype = "INDEX";

                col.keyschema = row["USERNAME"].ToString();
                col.keyname = row["INDEX_NAME"].ToString();
                col.colname = row["COLUMN_NAME"].ToString();

                col.ordinal = row["COLUMN_POSITION"].ToString();

                ta.cols.Add(col);
            }

            AnalyserCache.PutTableAnalyser("indexes", ta);
            return ta;
        }

        private DataTable LoadConstraintsTable(NameWithSchema table)
        {
            string sql = SqlScripts.getconstraints;
            sql = sql.Replace("#RETURNALL#", table == null ? "1" : "0");
            if (table != null) sql = sql.Replace("#TABLE#", table.Name);
            return GetDbConn().LoadTableFromQuery(sql);
        }

        private DataTable LoadConstraintsColsTable(NameWithSchema table)
        {
            string sql = SqlScripts.getconstraintcols;
            sql = sql.Replace("#RETURNALL#", table == null ? "1" : "0");
            if (table != null) sql = sql.Replace("#TABLE#", table.Name);
            return GetDbConn().LoadTableFromQuery(sql);
        }

        protected override void LoadConstraints(TableStructure table)
        {
            if (m_members.TableMembers.ContainsAny(TableStructureMembers.ConstraintsNoIndexesNoRefs))
            {
                TableAnalyser ta = new TableAnalyser();

                foreach (DataRow row in GetCachedTableColumnsTable("oracle.constraints", LoadConstraintsTable, table.FullName).Rows)
                {
                    TableAnalyser.Key key = new TableAnalyser.Key();
                    key.keytype = OracleConstraintToDatAdmin(row["CONSTRAINT_TYPE"].ToString());
                    key.keyname = row["CONSTRAINT_NAME"].ToString();
                    key.tblname = row["TABLE_NAME"].ToString();
                    key.tblschema = row["OWNER"].ToString();
                    key.checkexpr = row["SEARCH_CONDITION"].SafeToString();
                    ta.keys.Add(key);
                }

                foreach (DataRow row in GetCachedTableColumnsTable("oracle.constraintcols", LoadConstraintsColsTable, table.FullName).Rows)
                {
                    TableAnalyser.Col col = new TableAnalyser.Col();
                    col.keyname = row["CONSTRAINT_NAME"].ToString();
                    col.tblname = row["TABLE_NAME"].ToString();
                    col.tblschema = row["OWNER"].ToString();
                    col.ordinal = row["POSITION"].ToString();
                    col.colname = row["COLUMN_NAME"].ToString();
                    ta.cols.Add(col);
                }

                ta.SaveConstraints(table, this);
            }

            if (m_members.TableMembers.ContainsAny(TableStructureMembers.ForeignKeys | TableStructureMembers.ReferencedFrom))
            {
                GetRefsTA().SaveConstraints(table, this);
            }

            if (m_members.TableMembers.ContainsAny(TableStructureMembers.Indexes))
            {
                GetIndexesTA().SaveConstraints(table, this);
            }
        }

        protected override void LoadSpecificObjects()
        {
            base.LoadSpecificObjects();

            LoadSpecificObjectListAndDetail("trigger", "SELECT * FROM USER_TRIGGERS", LoadTrigger);
            LoadSpecificObjectListAndDetail("procedure", "select USERNAME, OBJECT_NAME from USER_OBJECTS, USER_USERS WHERE OBJECT_TYPE='PROCEDURE'", LoadRoutine);
            LoadSpecificObjectListAndDetail("function", "SELECT USERNAME, OBJECT_NAME FROM USER_OBJECTS, USER_USERS WHERE OBJECT_TYPE='FUNCTION'", LoadRoutine);
            LoadSpecificObjectListAndDetail("sequence", "SELECT * FROM USER_SEQUENCES, USER_USERS", LoadSequence);
        }

        private DataTable LoadSourceTable(NameWithSchema obj)
        {
            string sql = "SELECT TEXT, NAME FROM USER_SOURCE";
            if (obj != null) sql += " WHERE NAME='" + obj.Name + "'";
            sql += " ORDER BY LINE";
            return GetDbConn().LoadTableFromQuery(sql);
        }

        private DataTable FilterSource(DataTable table, NameWithSchema obj)
        {
            var res = table.Clone();
            foreach (DataRow row in table.Rows)
            {
                if (obj.Name != null && String.Compare(row.SafeString("NAME"), obj.Name, true) != 0) continue;
                res.ImportRow(row);
            }
            return res;
        }

        private DataTable CachedLoadSource(NameWithSchema obj)
        {
            var res = AnalyserCache.GetSource(obj);
            if (res != null) return res;
            res = AnalyserCache.GetSource(null);
            if (res != null) return FilterSource(res, obj);
            if (LoadMoreSpecificObjects)
            {
                res = LoadSourceTable(null);
                AnalyserCache.PutSource(null, res);
                return FilterSource(res, obj);
            }
            else
            {
                res = LoadSourceTable(obj);
                AnalyserCache.PutSource(obj, res);
                return res;
            }
        }

        private string LoadSource(SpecificObjectStructure obj)
        {
            var sb = new StringBuilder();
            foreach (DataRow dr in CachedLoadSource(obj.ObjectName).Rows)
            {
                sb.Append(dr[0]);
            }
            return sb.ToString();
        }

        protected void LoadSequence(DataRow row, SpecificObjectStructure obj)
        {
            obj.ObjectName = NewNameWithSchema(row["USERNAME"].SafeToString(), row["SEQUENCE_NAME"].SafeToString());
            var sb = new StringBuilder();
            sb.AppendFormat("CREATE SEQUENCE \"{0}\".\"{1}\"\n", obj.ObjectName.Schema, obj.ObjectName.Name);
            sb.AppendFormat("    MINVALUE {0} MAXVALUE {1}\n", row["MIN_VALUE"], row["MAX_VALUE"]);
            sb.AppendFormat("    INCREMENT BY {0} START WITH {1}\n", row["INCREMENT_BY"], row["LAST_NUMBER"]);
            sb.AppendFormat("    ");
            if (row["CACHE_SIZE"].SafeToString() == "0") sb.Append("NOCACHE ");
            else sb.AppendFormat("CACHE {0} ", row["CACHE_SIZE"]);
            if (row["ORDER_FLAG"].SafeToString() == "Y") sb.Append("ORDER "); else sb.Append("NOORDER ");
            if (row["CYCLE_FLAG"].SafeToString() == "Y") sb.Append("CYCLE"); else sb.Append("NOCYCLE");
            obj.CreateSql = sb.ToString();
        }

        protected void LoadRoutine(DataRow row, SpecificObjectStructure obj)
        {
            obj.ObjectName = NewNameWithSchema(row["USERNAME"].SafeToString(), row["OBJECT_NAME"].SafeToString());
            obj.CreateSql = "CREATE OR REPLACE " + LoadSource(obj);
        }

        protected void LoadTrigger(DataRow row, SpecificObjectStructure obj)
        {
            obj.ObjectName = NewNameWithSchema(row["TABLE_OWNER"].SafeToString(), row["TRIGGER_NAME"].SafeToString());
            obj.RelatedTable = NewNameWithSchema(row["TABLE_OWNER"].SafeToString(), row["TABLE_NAME"].SafeToString());
            obj.CreateSql = "CREATE OR REPLACE " + LoadSource(obj);
        }

        private void LoadView(DataRow row, SpecificObjectStructure obj)
        {
            obj.ObjectName = NewNameWithSchema(row["USERNAME"].ToString(), row["VIEW_NAME"].ToString());
            obj.CreateSql = String.Format("CREATE OR REPLACE VIEW \"{0}\".\"{1}\" AS\n{2}", obj.ObjectName.Schema, obj.ObjectName.Name, row["TEXT"].ToString());
        }

        protected override void LoadViews(bool listOnly)
        {
            LoadSpecificObjectListAndDetail("view", "SELECT * FROM USER_VIEWS, USER_USERS", LoadView);
        }
    }

    public static class AnalyserCacheExtension
    {
        public static DataTable GetSource(this DatabaseCache cache, NameWithSchema obj)
        {
            string key = obj != null ? obj.ToString() : "@#all";
            return (DataTable)cache.Get("oracle.source", key);
        }
        public static void PutSource(this DatabaseCache cache, NameWithSchema obj, DataTable value)
        {
            string key = obj != null ? obj.ToString() : "@#all";
            cache.Put("oracle.source", key, value);
        }
    }
}
