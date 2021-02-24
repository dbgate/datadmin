using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.Data;
using System.Data.Common;
using System.IO;
using MySql.Data.MySqlClient;
using Antlr.Runtime;

namespace Plugin.mysql
{
    [Dialect(Title = "MySQL", Name = "mysql")]
    public class MySqlDialect : DialectBase
    {
        static ISqlDialect[] m_majorVersions = new ISqlDialect[] {
            new MySqlDialect{m_version = new SqlServerVersion("4.1")},
            new MySqlDialect{m_version = new SqlServerVersion("5.0")},
            new MySqlDialect{m_version = new SqlServerVersion("5.1")},
        };
        //protected MySqlServerVersion m_version = new MySqlServerVersion(null);
        public override char QuoteIdentBegin { get { return '`'; } }
        public override char QuoteIdentEnd { get { return '`'; } }

        //public override SqlServerVersion ServerVersion
        //{
        //    get { return m_version; }
        //}

        public override SqlDialectCaps DialectCaps
        {
            get
            {
                var res = base.DialectCaps;
                res.AnonymousPrimaryKey = true;
                res.MultipleSchema = false;
                res.MultiCommand = true;
                res.Checks = false;
                res.UseDatabaseAsSchema = true;
                res.MARS = false;
                res.RangeSelect = true;
                res.SupportBackup = true;
                return res;
            }
        }

        public override ISpecificType GenericTypeToSpecific(DbTypeBase type, IMigrationProfile profile, IProgressInfo progress)
        {
            var prof = profile as IMySqlMigrationProfile;
            switch (type.Code)
            {
                case DbTypeCode.Int:
                    return GetMySqlTypeInt((DbTypeInt)type);
                case DbTypeCode.String:
                    return GetMySqlTypeString((DbTypeString)type);
                case DbTypeCode.Logical:
                    return new MySqlTypeBit();
                    //if (type.GetSpecificAttribute("mysql", "subtype") == "bit") return new MySqlTypeBit();
                    //return new MySqlTypeTinyInt();
                case DbTypeCode.Datetime:
                    return GetMySqlTypeDatetime((DbTypeDatetime)type, prof, progress);
                case DbTypeCode.Numeric:
                    {
                        MySqlTypeNumericBase res = new MySqlTypeDecimal();
                        var src = (DbTypeNumeric)type;
                        res.Length = src.Precision;
                        res.Decimals = src.Scale;
                        res.Unsigned = src.Unsigned;
                        res.Zerofill = src.GetSpecificAttribute("mysql", "zerofill") == "1";
                        return res;
                    }
                case DbTypeCode.Blob:
                    return GetMySqlTypeBlob((DbTypeBlob)type, prof, progress);
                case DbTypeCode.Text:
                    return GetMySqlTypeText((DbTypeText)type, prof, progress);
                case DbTypeCode.Array:
                    return new MySqlTypeText();
                case DbTypeCode.Xml:
                    return GetMySqlTypeText(null, prof, progress);
                case DbTypeCode.Float:
                    {
                        MySqlTypeReal res;
                        var src = (DbTypeFloat)type;
                        if (src.Bytes == 4) res = new MySqlTypeFloat();
                        else res = new MySqlTypeDouble();
                        res.Unsigned = src.Unsigned;
                        res.Zerofill = src.GetSpecificAttribute("mysql", "zerofill") == "1";
                        string len = src.GetSpecificAttribute("mysql", "length");
                        string decs = src.GetSpecificAttribute("mysql", "decimals");
                        if (len != null && decs != null)
                        {
                            res.Length = Int32.Parse(len);
                            res.Decimals = Int32.Parse(decs);
                        }
                        return res;
                    }
                case DbTypeCode.Generic:
                    return new MySqlTypeGeneric { Sql = ((DbTypeGeneric)type).Sql };
            }
            throw new Exception("DAE-00335 unknown type");
        }

        private MySqlTypeBase GetMySqlTypeDatetime(DbTypeDatetime type, IMySqlMigrationProfile profile, IProgressInfo progress)
        {
            switch (((DbTypeDatetime)type).SubType)
            {
                case DbDatetimeSubType.Date: return new MySqlTypeDate();
                case DbDatetimeSubType.Time: return new MySqlTypeTime();
                case DbDatetimeSubType.Datetime:
                    {
                        if (type.GetSpecificAttribute("mysql", "subtype") == "timestamp") return new MySqlTypeTimestamp();
                        if (type.GetSpecificAttribute("mysql", "subtype") == "datetime") return new MySqlTypeDatetime();
                        if (profile != null)
                        {
                            switch (profile.DateTimeAffinity)
                            {
                                case MySqlDateTimeAffinity.DateTime: return new MySqlTypeDatetime();
                                case MySqlDateTimeAffinity.Timespamp: return new MySqlTypeTimestamp();
                            }
                        }
                        return new MySqlTypeTimestamp();
                    }
                case DbDatetimeSubType.Year: return new MySqlTypeYear();
            }
            return new MySqlTypeDatetime();
        }

        private MySqlTypeBase GetMySqlTypeInt(DbTypeInt type)
        {
            if (type.GetSpecificAttribute("mysql", "subtype") == "bit")
            {
                MySqlTypeBit bit = new MySqlTypeBit();
                bit.Length = Int32.Parse(type.GetSpecificAttribute("mysql", "bitlength"));
                return bit;
            }

            MySqlTypeInteger res;
            switch (type.Bytes)
            {
                case 1:
                    res = new MySqlTypeTinyInt();
                    break;
                case 2:
                    res = new MySqlTypeSmallInt();
                    break;
                case 3:
                    res = new MySqlTypeMediumInt();
                    break;
                case 4:
                    res = new MySqlTypeInt();
                    break;
                case 8:
                    res = new MySqlTypeBigInt();
                    break;
                default:
                    res = new MySqlTypeInt();
                    break;
            }
            res.IsAutoIncrement = type.Autoincrement;
            res.Unsigned = type.Unsigned;
            res.Zerofill = type.GetSpecificAttribute("mysql", "zerofill") == "1";
            string len = type.GetSpecificAttribute("mysql", "length");
            if (len != null) res.Length = Int32.Parse(len);
            return res;
        }

        public ISpecificType GetMySqlTypeBlob(DbTypeBlob type, IMySqlMigrationProfile profile, IProgressInfo progress)
        {
            string attr = type.GetSpecificAttribute("mysql", "subtype");
            if (attr == "tinyblob") return new MySqlTypeTinyBlob();
            if (attr == "blob") return new MySqlTypeBlob();
            if (attr == "mediumblob") return new MySqlTypeMediumBlob();
            if (attr == "longblob") return new MySqlTypeLongBlob();
            if (profile != null)
            {
                switch (profile.BlobAffinity)
                {
                    case MySqlBlobAffinity.TinyBlob: return new MySqlTypeTinyBlob();
                    case MySqlBlobAffinity.Blob: return new MySqlTypeBlob();
                    case MySqlBlobAffinity.MediumBlob: return new MySqlTypeMediumBlob();
                    case MySqlBlobAffinity.LongBlob: return new MySqlTypeLongBlob();
                }
            }
            return new MySqlTypeBlob();
        }

        private MySqlTypeBase GetMySqlTypeText(DbTypeText type, IMySqlMigrationProfile profile, IProgressInfo progress)
        {
            string attr = null;
            if (type != null) attr = type.GetSpecificAttribute("mysql", "subtype");
            if (attr != null)
            {
                switch (attr)
                {
                    case "tinytext": return new MySqlTypeTinyText();
                    case "text": return new MySqlTypeText();
                    case "mediumtext": return new MySqlTypeMediumText();
                    case "longtext": return new MySqlTypeLongText();

                    case "geometry": return new MySqlTypeGeometry();
                    case "point": return new MySqlTypePoint();
                    case "linestring": return new MySqlTypeLineString();
                    case "polygon": return new MySqlTypePolygon();

                    case "mutlipoint": return new MySqlTypeMultiPoint();
                    case "mutlilinestring": return new MySqlTypeMultiLineString();
                    case "mutlipolygon": return new MySqlTypeMultiPolygon();
                    case "geometrycollection": return new MySqlTypeGeometryCollection();
                }
            }

            if (profile != null)
            {
                switch (profile.TextAffinity)
                {
                    case MySqlTextAffinity.TinyText: return new MySqlTypeTinyText();
                    case MySqlTextAffinity.Text: return new MySqlTypeText();
                    case MySqlTextAffinity.MediumText: return new MySqlTypeMediumText();
                    case MySqlTextAffinity.LongText: return new MySqlTypeLongText();
                }
            }
            return new MySqlTypeText();
        }

        private MySqlTypeBase GetMySqlTypeString(DbTypeString type)
        {
            string attr = type.GetSpecificAttribute("mysql", "subtype");
            MySqlTypeEnumSet reses = null;
            if (attr == "enum") reses = new MySqlTypeEnum();
            if (attr == "set") reses = new MySqlTypeSet();
            if (reses != null)
            {
                string vals = type.GetSpecificAttribute("mysql", "values");
                if (vals != null)
                {
                    reses.Values.AddRange(vals.Split(','));
                    return reses;
                }
                else
                {
                    return new MySqlTypeVarChar();
                }
            }

            MySqlTypeCharacter res;
            if (type.IsVarLength)
            {
                if (type.IsBinary) res = new MySqlTypeVarBinary();
                else res = new MySqlTypeVarChar();
            }
            else
            {
                if (type.IsBinary) res = new MySqlTypeBinary();
                else res = new MySqlTypeChar();
            }
            res.Length = type.Length;
            return res;
        }

        //public override ITableStructure AnalyseTable(IPhysicalConnection conn, string dbname, string catalog, string schema, string tblname)
        //{
        //    TableStructure res = AnalyseSql92Table.Run(conn, dbname, catalog, schema, tblname, this, true);


        //    return res;
        //}

        //public override List<string> Keywords
        //{
        //    get
        //    {
        //        List<string> res = base.Keywords;
        //        res.AddRange(new string[] { 
        //        "show", "tables", "databases", "columns"
        //        });
        //        return res;
        //    }
        //}

        public override Type SpecificTypeEnum
        {
            get { return typeof(MySqlTypeCode); }
        }

        public override ISpecificType CreateSpecificTypeInstance(object enumValue)
        {
            return MySqlTypeBase.CreateType((MySqlTypeCode)enumValue);
        }

        //public override bool AllowedName(string name, NameType type)
        //{
        //    if (type == NameType.PrimaryKey && name.ToLower() == "primary") return false;
        //    return base.AllowedName(name, type);
        //}

        //public override DialectQuality TableAnalyserQuality(IPhysicalConnection conn)
        //{
        //    //if (((MySqlAdvancedProperties)conn.AdvancedProperties).UseDefaultTableAnalyser) return DialectQuality.Low;
        //    return DialectQuality.High;
        //}

        public override string GetCurrentDatabase(IPhysicalConnection conn)
        {
            using (DbCommand cmd = conn.DbFactory.CreateCommand())
            {
                cmd.Connection = conn.SystemConnection;
                cmd.CommandText = "SELECT database()";
                object res = cmd.ExecuteScalar();
                return res.SafeToString();
            }
        }

        public override List<ISpecificObjectType> GetSpecificTypes()
        {
            List<ISpecificObjectType> res = base.GetSpecificTypes();
            if (m_version.Is_5_0_10()) res.Add(new ViewObjectType(this));
            if (m_version.Is_5_0_17()) res.Add(new MySqlTriggerType(this));
            if (m_version.Is_5_0_10()) res.Add(new MySqlProcedureType(this));
            if (m_version.Is_5_0_10()) res.Add(new MySqlFunctionType(this));
            if (m_version.Is_5_1_0()) res.Add(new MySqlEventType(this));
            res.Add(new MySqlEngineType(this));
            //res.Add(new MySqlIndexType(this));
            return res;
        }

        //public override bool ExplicitDropConstraint { get { return true; } }

        public override void GetAdditionalWidgets(List<IWidget> res, AppObject appobj)
        {
            var da = appobj as DatabaseAppObject;
            var sa = appobj as ServerAppObject;
            if (sa != null || da != null)
            {
                res.Add(new MySqlVariablesWidget());
                res.Add(new MySqlProcesslistWidget());
                res.Add(new MySqlStatusWidget());
                res.Add(new MySqlUsersWidget());
                res.Add(new MySqlEnginesWidget());
            }
        }

        public override void ReplaceStandardWidgets(List<IWidget> res, ITreeNode node)
        {
            int index;
            index = res.IndexOfIf(v => v is TablesRawGridWidget);
            if (index >= 0) res[index] = new MySqlTablesWidget();
            index = res.IndexOfIf(v => v is ColumnsRawGridWidget);
            if (index >= 0) res[index] = new MySqlColumnsWidget();
            index = res.IndexOfIf(v => v is DatabasesRawGridWidget);
            if (index >= 0) res[index] = new MySqlDatabasesWidget();
            index = res.IndexOfIf(v => v is CreateTableDDLObjectView);
            if (index >= 0) res[index] = new MySqlTableDDLWidget();
        }

        public override DatabaseAnalyser CreateAnalyser()
        {
            return new MySqlAnalyser();
        }

        public override ISqlDumper CreateDumper(ISqlOutputStream stream, SqlFormatProperties props)
        {
            return new MySqlDumper(stream, this, props);
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
                    RenameColumn = true,
                    ChangeColumnType = true,
                    ChangeColumnDefaultValue = true,
                    ChangeAutoIncrement = true,
                    AddColumn = true,
                    DropColumn = true,
                    ChangeColumn = true,
                    RenameTable = true,
                    AddConstraint = true,
                    DropConstraint = true,
                    AddIndex = true,
                    DropIndex = true,

                    CreateDatabase = true,
                    DropDatabase = true,
                    RecreateTable = true,
                    RenameDatabase = false, // http://dev.mysql.com/doc/refman/5.1/en/rename-database.html

                    ForceAbsorbPrimaryKey = true,

                    SpecificCaps = new ObjectOperationCaps { AllFlags = false, Create = true, Drop = true },
                    DepCaps = new AlterDependencyCaps { AllFlags = false, ChangeColumn_Reference = true },
                };
            }
        }

        public override ISqlParser CreateParser(ISqlTokenizer tokenizer)
        {
            return new MySqlParser(tokenizer, this);
        }

        public override ISqlTokenizer CreateTokenizer(TextReader reader, IStringSliceProvider sliceProvider)
        {
            return new MySqlTokenizer(reader, sliceProvider, this);
        }

        //public override string GetSpecificFunction(CommonSqlFunction commonFunc)
        //{
        //    switch (commonFunc)
        //    {
        //        case CommonSqlFunction.Now: return "NOW";
        //        case CommonSqlFunction.UtcNow: return "UTC_TIMESTAMP";
        //    }
        //    return null;
        //}

        //public override CommonSqlFunction GetCommonFunction(string specificFunc)
        //{
        //    switch (specificFunc.ToLower())
        //    {
        //        case "now":
        //        case "current_timestamp":
        //            return CommonSqlFunction.Now;
        //        case "utc_timestamp": return CommonSqlFunction.UtcNow;
        //    }
        //    return CommonSqlFunction.None;
        //}

        public override bool SupportsColumnCollation(DbTypeBase coltype)
        {
            return coltype is DbTypeString;
        }

        public override IDialectSpecificEditor GetSpecificEditor(IAbstractObjectStructure obj, IDatabaseSource db)
        {
            var tbl = obj as ITableStructure;
            if (tbl != null) return new MySqlTableEditor(tbl, db);
            var dbo = obj as IDatabaseStructure;
            if (dbo != null) return new MySqlDatabaseEditor(dbo, db);
            return null;
        }

        public override IBulkInserter CreateBulkInserter()
        {
            return new MySqlBulkInserter();
        }

        public override DbTypeBase MigrateDataType(IColumnStructure owningColumn, DbTypeBase type, IMigrationProfile profile, IProgressInfo progress)
        {
            if (type is DbTypeString && ((DbTypeString)type).Length <= 0)
            {
                // convert to BLOB variant, MySQL doesn't support varchar(max) notation
                return ((DbTypeString)type).ConvertToBlobVariant();
            }

            return base.MigrateDataType(owningColumn, type, profile, progress);
        }

        public override void MigrateTable(TableStructure table, IMigrationProfile profile, IProgressInfo progress)
        {
            base.MigrateTable(table, profile, progress);

            MigrateTool.RemoveNonPk1AutoIncrements(table, progress);

            foreach (ColumnStructure col in table.Columns)
            {
                var spec = col.DefaultValue as SpecialConstantSqlExpression;
                string subtype = col.DataType.GetSpecificAttribute("mysql", "subtype");
                if (spec != null)
                {
                    if (subtype != "timestamp")
                    {
                        col.DefaultValue = null;
                        col.IsNullable = true;
                        progress.Warning(String.Format("Default value for column {0}.{1} was removed, it is not valid on MySQL", table.FullName, col.ColumnName));
                    }
                }
            }
        }

        public override IMigrationProfile CreateMigrationProfile()
        {
            MySqlMigrationProfile cfg = GlobalSettings.Pages.PageByName("mysql_migration") as MySqlMigrationProfile;
            MySqlMigrationProfile res = new MySqlMigrationProfile();
            SettingsTool.CopySettingsPage(cfg, res);
            return res;
        }

        //public override object AdaptValueToType(object value, DbTypeBase type)
        //{
        //    DateTime mintimestamp = new DateTime(1970, 1, 1, 0, 0, 1);
        //    DateTime maxtimestamp = new DateTime(2038, 1, 9, 3, 14, 7);
        //    if (type is DbTypeDatetime && type.GetSpecificAttribute("mysql", "subtype") == "timestamp" && value is DateTime)
        //    {
        //        DateTime dt = (DateTime)value;
        //        if (dt < mintimestamp) dt = mintimestamp;
        //        if (dt > maxtimestamp) dt = maxtimestamp;
        //    }
        //    return base.AdaptValueToType(value, type);
        //}

        public override string GetSpecificExpression(SqlSpecialConstant specConst, DbTypeBase type, IProgressInfo progress)
        {
            if (type is DbTypeDatetime && type.GetSpecificAttribute("mysql", "subtype") == "timestamp")
            {
                switch (specConst)
                {
                    case SqlSpecialConstant.Current_Timestamp:
                    case SqlSpecialConstant.Utc_Timestamp: 
                        return "CURRENT_TIMESTAMP";
                }
            }
            if (type is DbTypeDatetime && type.GetSpecificAttribute("mysql", "subtype") == "datetime")
            {
                switch (specConst)
                {
                    case SqlSpecialConstant.Current_Timestamp:
                    case SqlSpecialConstant.Current_Time:
                    case SqlSpecialConstant.Current_Date:
                    case SqlSpecialConstant.Utc_Timestamp:
                        return "CURRENT_DATETIME";
                    //case SqlSpecialConstant.Current_Time: return "CURRENT_TIME";
                    //case SqlSpecialConstant.Current_Date: return "CURRENT_DATE";
                    //case SqlSpecialConstant.Utc_Timestamp: return "UTC_TIMESTAMP";
                }
            }
            return null;
        }

        public override string SpecificSyntaxName
        {
            get
            {
                return "Sql_mysql";
            }
        }

        public override string GetSyntaxDef()
        {
            return SqlScripts.syntax;
        }

        public override char StringEscapeChar
        {
            get { return '\\'; }
        }

        public override string ReformatSpecificObject(string objtype, string createsql)
        {
            if (objtype == "view")
            {
                try
                {
                    MySQLLexer lexer = new MySQLLexer(new ANTLRReaderStream(new StringReader(createsql)));
                    CommonTokenStream tokens = new CommonTokenStream(lexer);
                    MySQLParser parser = new MySQLParser(tokens);
                    var res = parser.create_view();
                    if (parser.Errors != null)
                    {
                        UsageStats.Usage("warning:badview", "viewsql", createsql);
                        return createsql;
                    }
                    var tree = (Antlr.Runtime.Tree.ITree)res.Tree;
                    var sw = new StringWriter();
                    var dmp = this.CreateDumper(sw);
                    var admp = new AntlrTreeDumper(tree, createsql, dmp);
                    admp.Run();
                    return sw.ToString();
                }
                catch { }
            }
            return base.ReformatSpecificObject(objtype, createsql);
        }

        public override bool EqualSpecificObjects(string objtype, string createSql1, string createSql2)
        {
            if (objtype == "view")
            {
                try
                {
                    var p1 = (MySqlParser)this.CreateParser(createSql1);
                    var p2 = (MySqlParser)this.CreateParser(createSql2);
                    var v1 = p1.ParseRuleCreateView();
                    var v2 = p2.ParseRuleCreateView();
                    var props = new MySqlEqualityTestProps();
                    return v1.EqualTo(v2, props);
                }
                catch { }
            }
            return base.EqualSpecificObjects(objtype, createSql1, createSql2);
        }

        public override ISqlDialect CloneDialect()
        {
            return new MySqlDialect();
        }

        public override string DisplayName
        {
            get
            {
                if (m_version.IsMax) return "MySQL";
                return "MySQL " + m_version.ToString(2);
            }
        }

        public override ISqlDialect[] MajorVersions
        {
            get { return m_majorVersions; }
        }
        //public override string DialectName
        //{
        //    get
        //    {
        //        if (m_version.IsMax) return "mysql";
        //        if (m_version.Is_5_1_0) return "mysql-5.1";
        //        if (m_version.IsMinimally(5, 0, 0)) return "mysql-5.0";
        //        if (m_version.IsMinimally(4, 1, 0)) return "mysql-4.1";
        //        return "mysql";
        //    }
        //}

        public override IDialectDataAdapter CreateDataAdapter()
        {
            return new MySqlDDA(this);
        }

        public override string GeneratePing()
        {
            return "SELECT version()";
        }

        public override string SelectVersion(DbConnection conn)
        {
            return conn.ExecuteScalar("SELECT version()").SafeToString();
        }

        public override List<string> GetDatabaseNames(IPhysicalConnection conn)
        {
            var res = new List<string>();
            using (var cmd = conn.SystemConnection.CreateCommand())
            {
                cmd.CommandText = "SHOW DATABASES";
                using (var reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        res.Add(reader[0].SafeToString());
                    }
                }
            }
            return res;
        }

        public override IDumpLoader CreateDumpLoader()
        {
            return new MySqlDumpLoader(this);
        }

        public override IQuerySplitter CreateQuerySplitter()
        {
            return new MySqlQuerySplitter(this);
        }

        //public override void GetDumpFormatProps(SqlFormatProperties formatProps)
        //{
        //    formatProps.CommandSeparator = ";\n"; // override dialect default value
        //    formatProps.SpecificData["mysql.dump_delim_mode"] = "1";
        //    formatProps.DumpFileBegin = "-- DatAdmin Native MySQL Dump\n\n/*!40101 SET NAMES utf8 */;\n/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;\n\n";
        //    formatProps.DumpFileEnd = "\n\n;/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;\n";
        //}

        public override bool QueryIsDump(string query)
        {
            if (query.Length > 10000) return true;
            foreach (var line in query.Split('\n'))
            {
                if (line.TrimStart().StartsWith("DELIMITER ", StringComparison.InvariantCultureIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        public override List<DependencyItem> DetectDependencies(ISpecificObjectStructure spec)
        {
            if (spec.CreateSql == null) return new List<DependencyItem>();
            var dc = new DepsCollector();
            MySQLLexer lexer = new MySQLLexer(new ANTLRReaderStream(new StringReader(spec.CreateSql)));
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            MySQLParser parser = new MySQLParser(tokens);
            parser.find_deps(dc);
            if (parser.Errors != null)
            {
                var err = new InternalError("DAE-00057 Error parsing dependencies:" + parser.Errors);
                err.Data["sql"] = spec.CreateSql;
                throw err;
            }
            return spec.BuildDependencyList(dc);
        }

        public override IDataSynAdapter CreateDataSynAdapter()
        {
            return new MySqlDataSynAdapter(this);
        }

        public override ICmdLineAdapter CreateCmdLineAdapter()
        {
            return new MySqlCmdLineAdapter();
        }

        public override void BeginUnicodeDumpFile(TextWriter fw)
        {
            fw.WriteLine("SET NAMES UTF8;");
        }

        public override IDatabaseLoader CreateSpecificDatabaseLoader()
        {
            return new MysqlDatabaseLoader();
        }

        public override ISqlDumpWriter CreateDumpWriter()
        {
            return new MySqlDumpWriter(this);
        }

        public override SqlDumpWriterConfig CreateDumpWriterConfig()
        {
            return new MySqlDumpWriterConfig();
        }

        public override AntlrTokens GetAntlrTokens()
        {
            var res = base.GetAntlrTokens(MySQLParser.tokenNames);
            return res;
        }

        public override ITokenStream GetAntlrTokenStream(TextReader reader)
        {
            MySQLLexer lexer = new MySQLLexer(new ANTLRReaderStream(reader));
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            return tokens;
        }
    }

    //[Dialect(Title = "MySQL", Name = "mysql")]
    //public class MySqlDialectGeneric : MySqlDialectBase
    //{
    //}
    //[Dialect(Title = "MySQL 4.1", Name = "mysql-4.1")]
    //public class MySqlDialect_4_1 : MySqlDialectBase
    //{
    //    public MySqlDialect_4_1()
    //    {
    //        m_version = new MySqlServerVersion("4.1");
    //    }
    //}
    //[Dialect(Title = "MySQL 4.1", Name = "mysql-4.1")]
    //public class MySqlDialect_4_1 : MySqlDialectBase
    //{
    //    public MySqlDialect_4_1()
    //    {
    //        m_version = new MySqlServerVersion("4.1");
    //    }
    //}
    //[Dialect(Title = "MySQL 5.0", Name = "mysql-5.0")]
    //public class MySqlDialect_5_0 : MySqlDialectBase
    //{
    //    public MySqlDialect_5_0()
    //    {
    //        m_version = new MySqlServerVersion("5.0");
    //    }
    //}
    //[Dialect(Title = "MySQL 5.1", Name = "mysql-5.1")]
    //public class MySqlDialect_5_1 : MySqlDialectBase
    //{
    //    public MySqlDialect_5_1()
    //    {
    //        m_version = new MySqlServerVersion("5.1");
    //    }
    //}
    //public class MySqlDialectAutoDetect : MySqlDialectBase
    //{
    //    public MySqlDialectAutoDetect(string version)
    //    {
    //        m_version = new MySqlServerVersion(version);
    //    }
    //}

    [PluginHandler]
    public class MySqlPluginHandler : PluginHandlerBase
    {
        public override void OnLoadAssembly()
        {
            HSplash.CallAddModuleInfo(StdIcons.mysql, "MySQL");
        }
    }
}
