using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.Data;
using Antlr.Runtime;
using System.IO;

namespace Plugin.postgre
{
    public class PostgreSqlParseCreateIndexError : UnexpectedError
    {
        public PostgreSqlParseCreateIndexError(string errors, string sql)
            : base(errors, null)
        {
            this.Data["sql"] = sql;
        }
    }

    public class PostgreAnalyser : DatabaseAnalyser
    {
        protected override DbTypeBase AnalyseType(System.Data.IDataRecord row, IPhysicalConnection conn, bool isdomain)
        {
            PostgreSqlTypeBase res = MakeSpecificPostgreSqlType(row);
            return res.ToGenericType();
        }

        private PostgreSqlTypeBase MakeSpecificPostgreSqlType(IDataRecord row)
        {
            string dt = row.DataTypeName().ToLower();
            string coldef = row.ColumnDefault();
            int len = row.CharLength(), prec = row.Precision(), scale = row.Scale();
            return MakeSpecificPostgreSqlType(dt, len, prec, scale, coldef);
        }

        private PostgreSqlTypeBase MakeSpecificPostgreSqlType(string dt, int len, int prec, int scale, string coldef)
        {
            if (dt.StartsWith("_"))
            {
                var res = MakeSpecificPostgreSqlType(dt.Substring(1), len, prec, scale, coldef);
                res.IsArray = true;
                return res;
            }
            switch (dt)
            {
                case "bigint":
                case "int8":
                    if (coldef != null && coldef.StartsWith("nextval(")) return new PostgreSqlTypeBigSerial();
                    return new PostgreSqlTypeBigInt();
                case "bigserial":
                case "serial8":
                    return new PostgreSqlTypeBigSerial();
                case "bit":
                    {
                        PostgreSqlTypeBit res = new PostgreSqlTypeBit();
                        res.Length = len;
                        return res;
                    }
                case "varbit":
                case "bit varying":
                    {
                        PostgreSqlTypeVarBit res = new PostgreSqlTypeVarBit();
                        res.Length = len;
                        return res;
                    }
                case "boolean":
                case "bool":
                    return new PostgreSqlTypeBoolean();
                case "box":
                    return new PostgreSqlTypeBox();
                case "bytea":
                    return new PostgreSqlTypeBytea();
                case "character varying":
                case "varchar":
                    {
                        PostgreSqlTypeVarChar res = new PostgreSqlTypeVarChar();
                        res.Length = len;
                        return res;
                    }
                case "character":
                case "char":
                    {
                        PostgreSqlTypeChar res = new PostgreSqlTypeChar();
                        res.Length = len;
                        return res;
                    }
                case "cidr":
                    return new PostgreSqlTypeCidr();
                case "circle":
                    return new PostgreSqlTypeCircle();
                case "date":
                    return new PostgreSqlTypeDate();
                case "double precision":
                case "float8":
                    return new PostgreSqlTypeDouble();
                case "inet":
                    return new PostgreSqlTypeInet();
                case "integer":
                case "int":
                case "int4":
                    if (coldef != null && coldef.StartsWith("nextval(")) return new PostgreSqlTypeSerial();
                    return new PostgreSqlTypeInteger();
                case "line":
                    return new PostgreSqlTypeLine();
                case "lseg":
                    return new PostgreSqlTypeLineSeg();
                case "macaddr":
                    return new PostgreSqlTypeMacAddr();
                case "money":
                    return new PostgreSqlTypeMoney();
                case "numeric":
                case "decimal":
                    {
                        PostgreSqlTypeNumeric res = new PostgreSqlTypeNumeric();
                        res.Precision = prec;
                        res.Scale = scale;
                        return res;
                    }
                case "path":
                    return new PostgreSqlTypePath();
                case "point":
                    return new PostgreSqlTypePoint();
                case "polygon":
                    return new PostgreSqlTypePolygon();
                case "real":
                case "float4":
                    return new PostgreSqlTypeReal();
                case "smallint":
                case "int2":
                    return new PostgreSqlTypeSmallInt();
                case "serial":
                case "serial4":
                    return new PostgreSqlTypeSerial();
                case "text":
                    return new PostgreSqlTypeText();
                case "time":
                    return new PostgreSqlTypeTime();
                case "timetz":
                    return new PostgreSqlTypeTimeTz();
                case "timestamp":
                    return new PostgreSqlTypeTimestamp();
                case "timestamptz":
                    return new PostgreSqlTypeTimestampTz();
                case "bpchar":
                    {
                        PostgreSqlTypeBpChar res = new PostgreSqlTypeBpChar();
                        res.Length = len;
                        return res;
                    }
                case "oid":
                    return new PostgreSqlTypeOid();
                case "box2d":
                    return new PostGISTypeBox2D();
                case "box3d":
                    return new PostGISTypeBox3D();
                case "box3d_extent":
                    return new PostGISTypeBox3D_Extent();
                case "geometry":
                    return new PostGISTypeGeometry();
                case "geometry_dump":
                    return new PostGISTypeGeometry_Dump();
                case "geography":
                    return new PostGISTypeGeography();
            }
            return new PostgreSqlTypeGeneric { Sql = dt };
            //throw new Exception(String.Format("Unknown Postgre SQL type:{0}", dt));
        }

        protected override bool IsColumnNullable(System.Data.IDataRecord row)
        {
            return row["IS_NULLABLE"].ToString() == "YES";
        }

        protected override string GetDefaultValueExpression(string valueFromInfoSchema)
        {
            if (valueFromInfoSchema != null && valueFromInfoSchema.StartsWith("nextval(")) return null;
            return base.GetDefaultValueExpression(valueFromInfoSchema);
        }

        public override bool SkipConstraint(IConstraint cnt)
        {
            if (cnt is ICheck && cnt.Name != null && cnt.Name.EndsWith("_not_null")) return true;
            return base.SkipConstraint(cnt);
        }

        Dictionary<NameWithSchema, List<IndexConstraint>> m_loadedIndexes = new Dictionary<NameWithSchema, List<IndexConstraint>>();
        protected override void LoadIndexes()
        {
            if (!m_members.TableMembers.Contains(TableStructureMembers.Indexes)) return;

            var data = m_conn.SystemConnection.LoadTableFromQuery("select * from pg_indexes where schemaname!='pg_catalog'");
            foreach (DataRow row in data.Rows)
            {
                string sql = row["indexdef"].SafeToString();
                PostgreSQLLexer lexer = new PostgreSQLLexer(new ANTLRReaderStream(new StringReader(sql)));
                CommonTokenStream tokens = new CommonTokenStream(lexer);
                PostgreSQLParser parser = new PostgreSQLParser(tokens);
                IndexConstraint index = new IndexConstraint();
                parser.create_index(index);
                if (parser.Errors != null)
                {
                    throw new PostgreSqlParseCreateIndexError(parser.Errors, sql);
                }
                if (index.TableName.Schema == null) index.SetDummyTable(new NameWithSchema("public", index.TableName.Name));
                if (!m_loadedIndexes.ContainsKey(index.TableName)) m_loadedIndexes[index.TableName] = new List<IndexConstraint>();
                m_loadedIndexes[index.TableName].Add(index);
            }

            base.LoadIndexes();
        }

        protected override void LoadIndexes(TableStructure table)
        {
            if (m_loadedIndexes.ContainsKey(table.FullName))
            {
                foreach (var index in m_loadedIndexes[table.FullName])
                {
                    table._Constraints.Add(index);
                }
            }
        }

        protected void LoadSequence(SpecificObjectStructure obj)
        {
            var tbl = GetDbConn().LoadTableFromQuery(String.Format("SELECT * FROM {0}", m_dialect.QuoteFullName(obj.ObjectName)));
            var row = tbl.Rows[0];
            obj.CreateSql = String.Format(
@"CREATE SEQUENCE {0} INCREMENT BY {1}
MINVALUE {2} MAXVALUE {3}
START {4} CACHE {5} {6}", m_dialect.QuoteFullName(obj.ObjectName), 
                 row["increment_by"], row["min_value"], row["max_value"],
                 row["last_value"], row["cache_value"], (bool)row["is_cycled"] ? "CYCLE": "NO CYCLE");
        }

        protected override void LoadSpecificObjects()
        {
            base.LoadSpecificObjects();

            LoadSpecificObjectList("sequence", SqlScripts.getsequences, "SchemaName", "SequenceName");
            LoadSpecificObjectDetail("sequence", LoadSequence);
        }
    }
}
