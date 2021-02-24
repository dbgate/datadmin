using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.Data;
using Npgsql;
using System.Data.Common;
using Antlr.Runtime;
using System.IO;

namespace Plugin.postgre
{
    [Dialect(Title = "Postgre SQL", Name = "postgre")]
    public class PostgreDialect : DialectBase
    {
        public override SqlDialectCaps DialectCaps
        {
            get
            {
                var res = base.DialectCaps;
                res.MultiCommand = false;
                res.ExplicitDropConstraint = false;
                res.Arrays = true;
                res.SupportBackup = true;
                return res;
            }
        }

        public override ISpecificType GenericTypeToSpecific(DbTypeBase type, IMigrationProfile profile, IProgressInfo progress)
        {
            switch (type.Code)
            {
                case DbTypeCode.Int:
                    return GetPostgreSqlTypeInt((DbTypeInt)type);
                case DbTypeCode.String:
                    return GetPostgreSqlTypeString((DbTypeString)type);
                case DbTypeCode.Logical:
                    return new PostgreSqlTypeBoolean();
                case DbTypeCode.Datetime:
                    return GetPostgreSqlTypeDatetime((DbTypeDatetime)type);
                case DbTypeCode.Numeric:
                    {
                        PostgreSqlTypeNumeric res = new PostgreSqlTypeNumeric();
                        res.Precision = ((DbTypeNumeric)type).Precision;
                        res.Scale = ((DbTypeNumeric)type).Scale;
                        return res;
                    }
                case DbTypeCode.Blob:
                    return new PostgreSqlTypeBytea();
                case DbTypeCode.Xml:
                    return new PostgreSqlTypeText();
                case DbTypeCode.Text:
                    return GetPostgreSqlTypeText((DbTypeText)type);
                case DbTypeCode.Array:
                    return GetPostgreSqlTypeArray((DbTypeArray)type, profile, progress);
                case DbTypeCode.Float:
                    return GetPostgreSqlTypeFloat((DbTypeFloat)type);
                case DbTypeCode.Generic:
                    return new PostgreSqlTypeGeneric { Sql = ((DbTypeGeneric)type).Sql };
            }
            throw new Exception("DAE-00346 unknown type");
        }

        private ISpecificType GetPostgreSqlTypeArray(DbTypeArray type, IMigrationProfile profile, IProgressInfo progress)
        {
            var res = (PostgreSqlTypeBase) GenericTypeToSpecific(type.ElementType, profile, progress);
            res.IsArray = true;
            return res;
        }

        private ISpecificType GetPostgreSqlTypeText(DbTypeText type)
        {
            string subtype = type.GetSpecificAttribute("pgsql", "subtype");
            switch (subtype)
            {
                case "box2d": return new PostGISTypeBox2D();
                case "box3d": return new PostGISTypeBox3D();
                case "box3d_extent": return new PostGISTypeBox3D_Extent();
                case "geometry": return new PostGISTypeGeometry();
                case "geometry_dump": return new PostGISTypeGeometry_Dump();
                case "geography": return new PostGISTypeGeography();
            }
            return new PostgreSqlTypeText();
        }

        private ISpecificType GetPostgreSqlTypeFloat(DbTypeFloat type)
        {
            if (type.IsMoney) return new PostgreSqlTypeMoney();
            if (type.Bytes == 4) return new PostgreSqlTypeReal();
            return new PostgreSqlTypeDouble();
        }

        private ISpecificType GetPostgreSqlTypeDatetime(DbTypeDatetime type)
        {
            if (type.SubType == DbDatetimeSubType.Interval) return new PostgreSqlTypeInterval();
            if (type.SubType == DbDatetimeSubType.Time)
            {
                if (type.HasTimeZone) return new PostgreSqlTypeTimeTz();
                return new PostgreSqlTypeTime();
            }
            if (type.SubType == DbDatetimeSubType.Datetime)
            {
                if (type.HasTimeZone) return new PostgreSqlTypeTimestampTz();
                return new PostgreSqlTypeTimestamp();
            }
            if (type.SubType == DbDatetimeSubType.Date)
            {
                return new PostgreSqlTypeDate();
            }
            return new PostgreSqlTypeTimestamp();
        }

        private ISpecificType GetPostgreSqlTypeString(DbTypeString type)
        {
            string subtype = type.GetSpecificAttribute("pgsql", "subtype");
            switch (subtype)
            {
                case "box": return new PostgreSqlTypeBox();
                case "circle": return new PostgreSqlTypeCircle();
                case "line": return new PostgreSqlTypeLine();
                case "lseg": return new PostgreSqlTypeLineSeg();
                case "path": return new PostgreSqlTypePath();
                case "point": return new PostgreSqlTypePoint();
                case "polygon": return new PostgreSqlTypePolygon();

                case "cidr": return new PostgreSqlTypeCidr();
                case "inet": return new PostgreSqlTypeInet();
                case "macaddr": return new PostgreSqlTypeMacAddr();

                case "bpchar": return new PostgreSqlTypeBpChar { Length = type.Length };
            }
            if (type.IsVarLength)
            {
                if (type.IsBinary)
                {
                    PostgreSqlTypeVarBit res = new PostgreSqlTypeVarBit();
                    res.Length = type.Length;
                    return res;
                }
                else
                {
                    PostgreSqlTypeVarChar res = new PostgreSqlTypeVarChar();
                    res.Length = type.Length;
                    return res;
                }
            }
            else
            {
                if (type.IsBinary)
                {
                    PostgreSqlTypeBit res = new PostgreSqlTypeBit();
                    res.Length = type.Length;
                    return res;
                }
                else
                {
                    PostgreSqlTypeChar res = new PostgreSqlTypeChar();
                    res.Length = type.Length;
                    return res;
                }
            }
        }

        private ISpecificType GetPostgreSqlTypeInt(DbTypeInt type)
        {
            string subtype = type.GetSpecificAttribute("pgsql", "subtype");
            switch (subtype)
            {
                case "oid": return new PostgreSqlTypeOid();
            }

            if (type.Autoincrement)
            {
                if (type.Bytes == 8) return new PostgreSqlTypeBigSerial();
                return new PostgreSqlTypeSerial();
            }
            if (type.Bytes == 8) return new PostgreSqlTypeBigInt();
            if (type.Bytes == 2) return new PostgreSqlTypeSmallInt();
            return new PostgreSqlTypeInteger();
        }

        public override Type SpecificTypeEnum
        {
            get { return typeof(PostgreSqlTypeCode); }
        }

        public override ISpecificType CreateSpecificTypeInstance(object enumValue)
        {
            return PostgreSqlTypeBase.CreateType((PostgreSqlTypeCode)enumValue);
        }

        public override bool IsSystemTable(string schema, string table)
        {
            if (schema != null && schema.ToUpper() == "PG_CATALOG") return true;
            if (schema != null && schema.ToUpper() == "INFORMATION_SCHEMA") return true;
            return base.IsSystemTable(schema, table);
        }

        public override bool IsSystemObject(string type, string schema, string table)
        {
            if (type == "view" && schema.ToLower() == "pg_catalog") return true;
            return base.IsSystemObject(type, schema, table);
        }

        //public override ITableStructure AnalyseTable(IPhysicalConnection conn, string dbname, string catalog, string schema, string tblname)
        //{
        //    TableStructure res = AnalyseSql92Table.Run(conn, dbname, catalog, schema, tblname, this, true);
        //    //AnalysePostgresTable.AnalyseKeys(conn, dbname, catalog, schema, tblname, this, res);
        //    return res;
        //}

        public override List<ISpecificObjectType> GetSpecificTypes()
        {
            List<ISpecificObjectType> res = base.GetSpecificTypes();
            res.Add(new ViewObjectType(this));
            res.Add(new PostgreSqlSequenceType(this));
            return res;
        }

        public override bool RunSpecialNonQuery(System.Data.Common.DbConnection conn, string sql)
        {
            if (base.RunSpecialNonQuery(conn, sql)) return true;
            if (sql.Trim() == "@clearallpools")
            {
                NpgsqlConnection.ClearAllPools();
                return true;
            }
            return false;
        }

        public override string DefaultSchema
        {
            get { return "public"; }
        }

        public override void CreateNamedParameter(string nameBase, out string sqlName, out string formalName)
        {
            sqlName = ":" + nameBase;
            formalName = nameBase;
        }

        public override IDialectDataAdapter CreateDataAdapter()
        {
            return new PostgreDDA(this);
        }

        //public override string AdaptExpressionToType(string sqlExpr, DbTypeBase type)
        //{
        //    if (type is DbTypeLogical)
        //    {
        //        if (sqlExpr.Contains("1") || sqlExpr.ToLower().Contains("t")) return "true";
        //        return "false";
        //    }
        //    return base.AdaptExpressionToType(sqlExpr, type);
        //}

        public override DatabaseAnalyser CreateAnalyser()
        {
            return new PostgreAnalyser();
        }

        public override ISqlDumper CreateDumper(ISqlOutputStream stream, SqlFormatProperties props)
        {
            return new PostgreDumper(stream, this, props);
        }

        public override SqlDumperCaps DumperCaps
        {
            get
            {
                return new SqlDumperCaps
                {
                    AllFlags = false,
                    CreateTable = true,
                    DropTable = true,
                    //AlterTable = true,
                    CreateDatabase = true,
                    DropDatabase = true,
                    RenameColumn = true,
                    ChangeColumnType = true,
                    ChangeColumnDefaultValue = true,
                    AddColumn = true,
                    DropColumn = true,
                    ChangeColumn = true,
                    RenameTable = true,
                    AddConstraint = true,
                    DropConstraint = true,
                    AddIndex = true,
                    DropIndex = true,
                    SpecificCaps = new ObjectOperationCaps { AllFlags = false, Create = true, Drop = true },
                };
            }
        }

        public override char QuoteIdentBegin
        {
            get { return '"'; }
        }
        public override char QuoteIdentEnd
        {
            get { return '"'; }
        }

        public override string SpecificSyntaxName
        {
            get
            {
                return "Sql_postgre";
            }
        }

        public override string GetSyntaxDef()
        {
            return SqlScripts.syntax;
        }

        public override ISqlDialect CloneDialect()
        {
            return new PostgreDialect();
        }

        public override string DisplayName
        {
            get { return "Postgre SQL"; }
        }

        //public override void GetDumpFormatProps(SqlFormatProperties formatProps)
        //{
        //    formatProps.CommandSeparator = ";\n"; // override dialect default value
        //    formatProps.DumpFileBegin = "-- DatAdmin Native PostgreSQL Dump\n\n";
        //}

        public override IDataSynAdapter CreateDataSynAdapter()
        {
            return new PostgreDataSynAdapter(this);
        }

        public override IBulkInserter CreateBulkInserter()
        {
            return new PostgreBulkInserter();
        }

        public override ISqlDumpWriter CreateDumpWriter()
        {
            return new PostgreDumpWriter(this);
        }

        public override AntlrTokens GetAntlrTokens()
        {
            var res = base.GetAntlrTokens(PostgreSQLParser.tokenNames);
            return res;
        }

        public override ITokenStream GetAntlrTokenStream(TextReader reader)
        {
            PostgreSQLLexer lexer = new PostgreSQLLexer(new ANTLRReaderStream(reader));
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            return tokens;
        }
    }

    [DialectAutoDetector(Name = "postgre")]
    public class PostgreDialectAutoDetector : DialectAutoDetectorBase
    {
        public override bool TestCommand(DbConnection conn)
        {
            string res = conn.ExecuteScalar(@"
            select 
	            case 
		            when (exists (select * from INFORMATION_SCHEMA.TABLES where table_schema='pg_catalog')) then 1
		            else 0
	            end").ToString();
            return res == "1";
        }
        public override ISqlDialect GetDialect()
        {
            return new PostgreDialect();
        }
        public override ISqlDialect DetectDialect(string displayName)
        {
            if (displayName.ToLower().StartsWith("postgre")) return new PostgreDialect();
            return null;
        }
    }

    [PluginHandler]
    public class PostgrePluginHandler : PluginHandlerBase
    {
        public override void OnLoadAssembly()
        {
            HSplash.CallAddModuleInfo(StdIcons.postgresql, "Postgre SQL");
        }
    }
}
