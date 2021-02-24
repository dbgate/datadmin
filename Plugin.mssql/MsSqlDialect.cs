using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.Data;
using System.IO;
using System.Data.Common;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using Antlr.Runtime;

namespace Plugin.mssql
{
    [Dialect(Title = "MS SQL", Name = "mssql")]
    public class MsSqlDialect : DialectBase
    {
        static ISqlDialect[] m_majorVersions = new ISqlDialect[] {
            new MsSqlDialect{m_version = new SqlServerVersion("8.0.0")},
            new MsSqlDialect{m_version = new SqlServerVersion("9.0.0")},
            new MsSqlDialect{m_version = new SqlServerVersion("10.0.0")},
        };
        public static MsSqlDialect Instance = new MsSqlDialect();
        public override char QuoteIdentBegin { get { return '['; } }
        public override char QuoteIdentEnd { get { return ']'; } }

        public override string DefaultSchema
        {
            get { return "dbo"; }
        }

        public override ISpecificType GenericTypeToSpecific(DbTypeBase type, IMigrationProfile profile, IProgressInfo progress)
        {
            switch (type.Code)
            {
                case DbTypeCode.Int:
                    return GetSqlTypeInt((DbTypeInt)type);
                case DbTypeCode.String:
                    return GetSqlTypeString((DbTypeString)type);
                case DbTypeCode.Logical:
                    return new SqlTypeBit();
                case DbTypeCode.Datetime:
                    {
                        string attr = type.GetSpecificAttribute("mssql", "subtype");
                        var dtype = type as DbTypeDatetime;
                        if (attr == "timestamp") return new SqlTypeTimestamp();
                        if (attr == "datetime") return new SqlTypeDatetime();
                        if (attr == "smalldatetime") return new SqlTypeSmallDatetime();
                        if (m_version.Is_2008())
                        {
                            if (dtype.HasTimeZone) return new SqlTypeDatetimeOffset();
                            if (dtype.SubType == DbDatetimeSubType.Date) return new SqlTypeDate();
                            if (dtype.SubType == DbDatetimeSubType.Time) return new SqlTypeTime();
                            if (attr == "datetime2") return new SqlTypeDatetime2();
                            // on 2008 server prefer datetime2 type
                            // NO: return new SqlTypeDatetime2();
                        }
                        return new SqlTypeDatetime();
                    }
                case DbTypeCode.Numeric:
                    {
                        SqlTypeNumericBase res;
                        if (type.GetSpecificAttribute("mssql", "subtype") == "decimal") res = new SqlTypeDecimal();
                        else res = new SqlTypeNumeric();
                        res.Precision = ((DbTypeNumeric)type).Precision;
                        res.Scale = ((DbTypeNumeric)type).Scale;
                        res.IsIdentity = ((DbTypeNumeric)type).Autoincrement;

                        int increment;
                        if (Int32.TryParse(type.GetSpecificAttribute("mssql", "identity_increment"), out increment))
                        {
                            res.IdentityIncrement = increment;
                            res.IdentitySeed = Int32.Parse(type.GetSpecificAttribute("mssql", "identity_seed"));
                        }
                        return res;
                    }
                //return String.Format("decimal({0},{1})", ((DbTypeNumeric)type).Precision, ((DbTypeNumeric)type).Scale);
                case DbTypeCode.Blob:
                    if (type.GetSpecificAttribute("mssql", "subtype") == "variant") return new SqlTypeVariant();
                    return new SqlTypeImage();
                case DbTypeCode.Text:
                    if (((DbTypeText)type).IsUnicode) return new SqlTypeNText();
                    else return new SqlTypeText();
                case DbTypeCode.Array:
                    return new SqlTypeText();
                case DbTypeCode.Float:
                    {
                        DbTypeFloat tp = (DbTypeFloat)type;
                        if (tp.IsMoney)
                        {
                            if (tp.Bytes == 8) return new SqlTypeSmallMoney();
                            return new SqlTypeMoney();
                        }
                        if (tp.Bytes == 4) return new SqlTypeReal();
                        else return new SqlTypeFloat();
                    }
                case DbTypeCode.Xml:
                    if (m_version.Is_2005()) return new SqlTypeXml();
                    return new SqlTypeText();
                case DbTypeCode.Generic:
                    return new SqlTypeGeneric { Sql = ((DbTypeGeneric)type).Sql };
            }
            throw new InternalError("DAE-00332 unknown type");
        }

        private SqlTypeBase GetSqlTypeInt(DbTypeInt type)
        {
            SqlTypeInteger res;
            switch (type.Bytes)
            {
                case 1:
                    // tinyint is unsigned, when type is signed, 
                    // we must use 2-byte signed int
                    if (type.Unsigned) res = new SqlTypeTinyInt();
                    else res = new SqlTypeSmallInt();
                    break;
                case 2:
                    res = new SqlTypeSmallInt();
                    break;
                case 4:
                    res = new SqlTypeInt();
                    break;
                case 8:
                    res = new SqlTypeBigInt();
                    break;
                default:
                    res = new SqlTypeInt();
                    break;
            }
            res.IsIdentity = type.Autoincrement;
            int increment;
            if (Int32.TryParse(type.GetSpecificAttribute("mssql", "identity_increment"), out increment))
            {
                res.IdentityIncrement = increment;
                res.IdentitySeed = Int32.Parse(type.GetSpecificAttribute("mssql", "identity_seed"));
            }
            return res;
        }

        private SqlTypeBase GetSqlTypeString(DbTypeString type)
        {
            SqlTypeCharacter res;
            if (type.GetSpecificAttribute("mssql", "subtype") == "uniqueidentifier") return new SqlTypeUniqueIdentifier();
            if (type.IsVarLength)
            {
                if (type.IsUnicode) res = new SqlTypeNVarChar();
                else if (type.IsBinary) res = new SqlTypeVarBinary();
                else res = new SqlTypeVarChar();
            }
            else
            {
                if (type.IsUnicode) res = new SqlTypeNChar();
                else if (type.IsBinary) res = new SqlTypeBinary();
                else res = new SqlTypeChar();
            }
            res.Length = type.Length;
            return res;
        }

        public DbTypeBase SpecificTypeToGeneric(object specific)
        {
            return ((SqlTypeBase)specific).ToGenericType();
        }

        public override Type SpecificTypeEnum
        {
            get { return typeof(SqlTypeCode); }
        }

        public override ISpecificType CreateSpecificTypeInstance(object enumValue)
        {
            return SqlTypeBase.CreateType((SqlTypeCode)enumValue);
        }

        //public override List<string> TypeNames
        //{
        //    get
        //    {
        //        List<string> res = new List<string>();
        //        res.AddRange(new string[]{"binary", "image", "timestamp", "varbinary", "bit", "tinyint", "datetime", "smalldatetime",
        //                     "decimal", "numeric", "float", "uniqueidentifier", "smallint", "int", "bigint", "real",
        //                     "char", "nchar", "varchar", "nvarchar", "text", "ntext", "xml", "money", "smallmoney", "sql_variant"});
        //        return res;
        //    }
        //}

        //public override List<string> Keywords
        //{
        //    get
        //    {
        //        List<string> res = base.Keywords;
        //        res.AddRange(new string[] { "set", "exec" });
        //        return res;
        //    }
        //}

        //public override List<string> ContextKeywords
        //{
        //    get
        //    {
        //        List<string> res = base.ContextKeywords;
        //        res.AddRange(new string[] { "identity_insert", "on", "off", "sp_rename" });
        //        return res;
        //    }
        //}

        //public override DialectQuality TableAnalyserQuality(IPhysicalConnection conn)
        //{
        //    //if (((MsSqlAdvancedProperties)conn.AdvancedProperties).UseDefaultTableAnalyser) return DialectQuality.Low;
        //    return DialectQuality.High;
        //}

        public override void CreateNamedParameter(string nameBase, out string sqlName, out string formalName)
        {
            sqlName = "@" + nameBase;
            formalName = "@" + nameBase;
        }

        public override List<ISpecificObjectType> GetSpecificTypes()
        {
            List<ISpecificObjectType> res = new List<ISpecificObjectType>();
            res.Add(new MsSqlViewType(this));
            res.Add(new MsSqlProcedureType(this));
            res.Add(new MsSqlFunctionType(this));
            res.Add(new MsSqlTriggerType(this));
            //res.Add(new MsSqlIndexType(this));
            return res;
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
                    RenameTable = true,
                    RecreateTable = true,
                    ChangeTableSchema = true,

                    ChangeColumnType = true,
                    RenameColumn = true,
                    AddColumn = true,
                    DropColumn = true,
                    ChangeColumn = true,
                    ChangeColumnDefaultValue = true,

                    RenameConstraint = true,
                    AddConstraint = true,
                    DropConstraint = true,
                    RenameIndex = true,
                    AddIndex = true,
                    DropIndex = true,

                    CreateDatabase = true,
                    DropDatabase = true,
                    RenameDatabase = true,

                    SpecificCaps = new ObjectOperationCaps { AllFlags = true, Change = false },
                    DepCaps = new AlterDependencyCaps { AllFlags = true },
                };
            }
        }

        public override SqlDialectCaps DialectCaps
        {
            get
            {
                var res = base.DialectCaps;
                res.MultiCommand = false;
                res.NestedTransactions = true;
                res.ExplicitDropConstraint = false;
                if (m_version.Is_2005()) res.LimitSelect = true;
                res.Domains = true;
                res.SupportBackup = true;
                return res;
            }
        }

        public override string GetLimitSelect(string sql, int count)
        {
            if (!m_version.Is_2005()) return sql;

            if (sql.IndexOf("from", StringComparison.InvariantCultureIgnoreCase) < 0) return sql;
            var m1 = Regex.Match(sql.Replace("\n"," ").Replace("\r"," "), @"^(\s*select\s+)(distinct\s+)?(.*)$", RegexOptions.IgnoreCase);
            if (m1.Success)
            {
                string res = m1.Groups[1].Value + m1.Groups[2].Value + " top(" + count.ToString() + ") " + m1.Groups[3].Value;
                return res;
            }
            else
            {
                return sql;
            }
        }

        public override bool IsSystemTable(string schema, string table)
        {
            if (schema == "dbo" && table == "sysdiagrams") return true;
            if (schema == "sys") return true;
            return base.IsSystemTable(schema, table);
        }

        public override DatabaseAnalyser CreateAnalyser()
        {
            return new MsSqlAnalyser();
        }

        public override ISqlDumper CreateDumper(ISqlOutputStream stream, SqlFormatProperties props)
        {
            return new MsSqlDumper(stream, this, props);
        }

        public override string GetSpecificExpression(SqlSpecialConstant specConst, DbTypeBase type, IProgressInfo progress)
        {
            switch (specConst)
            {
                case SqlSpecialConstant.Current_Timestamp:
                case SqlSpecialConstant.Current_Date:
                case SqlSpecialConstant.Current_Time:
                    return "GETDATE()";
                case SqlSpecialConstant.Utc_Timestamp:
                    return "GETUTCDATE()";
            }
            return null;
        }

        public override ISqlParser CreateParser(ISqlTokenizer tokenizer)
        {
            return new MsSqlParser(tokenizer, this);
        }

        public override List<string> GetDatabaseNames(IPhysicalConnection conn)
        {
            string olddb = GetCurrentDatabase(conn);
            conn.SystemConnection.ChangeDatabase("master");
            DataTable tbl = conn.SystemConnection.LoadTableFromQuery("SELECT * FROM sysdatabases");
            List<string> res = new List<string>();
            foreach (DataRow row in tbl.Rows)
            {
                res.Add(row["name"].SafeToString());
            }
            conn.SystemConnection.ChangeDatabase(olddb);
            return res;
        }

        public override Exception TranslateException(Exception err)
        {
            err = base.TranslateException(err);
            var ex = err as SqlException;
            if (ex != null)
            {
                DatabaseError res = new DatabaseError(err);
                foreach (SqlError e in ex.Errors)
                {
                    res.Items.Add(new DatabaseErrorItem
                    {
                        ErrorCode = e.Number,
                        LineNumber = e.LineNumber - 1,
                        Message = e.Message,
                        Procedure = e.Procedure,
                    });
                }
                return res;
            }
            return err;
        }

        public override string SpecificSyntaxName
        {
            get
            {
                return "Sql_mssql";
            }
        }

        public override string GetSyntaxDef()
        {
            return StdScripts.syntax;
        }

        public override IBulkInserter CreateBulkInserter()
        {
            return new MsSqlBulkInserter();
        }

        public override ISqlDialect CloneDialect()
        {
            return new MsSqlDialect();
        }

        public override string DisplayName
        {
            get
            {
                if (m_version.IsMax) return "Microsoft SQL";
                string res = "Microsoft SQL";
                if (m_version.IsMinimally(10, 0, 0)) res += " 2008";
                else if (m_version.IsMinimally(9, 0, 0)) res += " 2005";
                else if (m_version.IsMinimally(8, 0, 0)) res += " 2000";
                else return res + " [" + m_version.ToString() + "]";
                if (m_version.V2 != 0 || m_version.V3 != 0) res += " [" + m_version.ToString() + "]";
                return res;
            }
        }

        public override ISqlDialect[] MajorVersions
        {
            get { return m_majorVersions; }
        }

        public override bool SupportsTypeCode(object enumValue)
        {
            var val = (SqlTypeCode)enumValue;
            switch (val)
            {
                case SqlTypeCode.Date:
                case SqlTypeCode.DateTime2:
                case SqlTypeCode.DateTimeOffset:
                case SqlTypeCode.Time:
                    return m_version.Is_2008();
                case SqlTypeCode.Xml:
                    return m_version.Is_2005();
            }
            return true;
        }
        //public override List<string> DefinedSpecificObjects
        //{
        //    get
        //    {
        //        return new List<string> { "view", "procedure", "function", "trigger" };
        //    }
        //}

        public override IDialectDataAdapter CreateDataAdapter()
        {
            return new MsSqlDDA(this);
        }

        public override void GetAdditionalWidgets(List<IWidget> res, AppObject appobj)
        {
            var da = appobj as DatabaseAppObject;
            if (LicenseTool.FeatureAllowed(MsSqlBackupFeature.Test) && da != null)
            {
                res.Add(new MsSqlBackupsWidget());
            }
            if (da != null)
            {
                res.Add(new MsSqlTableSizesWidget());
            }
        }

        public override void GetAdditionalWidgets(List<IWidget> res, ITreeNode node)
        {
            if (node is Tables_TreeNode)
            {
                res.Add(new MsSqlTableSizesWidget());
            }
        }

        protected override DatAdmin.Constraint MigrateConstraintOrIndex(DatAdmin.Constraint cnt, IMigrationProfile profile, IProgressInfo progress)
        {
            var index = cnt as IndexConstraint;
            if (index != null)
            {
                foreach (var colname in index.Columns)
                {
                    var col = cnt.Table.Columns[colname.ColumnName];
                    if (col.DataType is DbTypeBlob || col.DataType is DbTypeText || col.DataType is DbTypeXml)
                    {
                        progress.Warning("Column {0}:{1} cannot be indexable, because it is BLOB type", colname, col.DataType);
                        return null;
                    }
                }
            }
            return base.MigrateConstraintOrIndex(cnt, profile, progress);
        }

        public override List<DependencyItem> DetectDependencies(ISpecificObjectStructure spec)
        {
            if (spec.CreateSql == null) return new List<DependencyItem>();
            var dc = new DepsCollector();
            MSSQLLexer lexer = new MSSQLLexer(new ANTLRReaderStream(new StringReader(spec.CreateSql)));
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            MSSQLParser parser = new MSSQLParser(tokens);
            parser.find_deps(dc);
            if (parser.Errors != null)
            {
                var err = new InternalError("DAE-00002 Error parsing dependencies:" + parser.Errors);
                err.Data["sql"] = spec.CreateSql;
                throw err;
            }
            return spec.BuildDependencyList(dc);
        }

        public override IDialectSpecificEditor GetSpecificEditor(IAbstractObjectStructure obj, IDatabaseSource db)
        {
            if (obj is IDatabaseStructure)
            {
                return new MsSqlDatabaseEditor((IDatabaseStructure)obj, db);
            }
            return null;
        }

        public override IDataSynAdapter CreateDataSynAdapter()
        {
            return new MsSqlDataSynAdapter(this);
        }

        public override bool SuportedUnicodeCharacter(char ch)
        {
            return true;
        }

        public override IDatabaseLoader CreateSpecificDatabaseLoader()
        {
            return new MsSqlDatabaseLoader();
        }

        public override ISqlDumpWriter CreateDumpWriter()
        {
            return new MsSqlDumpWriter(this);
        }

        public override SqlDumpWriterConfig CreateDumpWriterConfig()
        {
            return new MsSqlDumpWriterConfig();
        }

        public override AntlrTokens GetAntlrTokens()
        {
            var res = base.GetAntlrTokens(MSSQLParser.tokenNames);
            return res;
        }

        public override ITokenStream GetAntlrTokenStream(TextReader reader)
        {
            MSSQLLexer lexer = new MSSQLLexer(new ANTLRReaderStream(reader));
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            return tokens;
        }
    }

    [PluginHandler]
    public class MsSqlPluginHandler : PluginHandlerBase
    {
        public override void OnLoadAssembly()
        {
            HSplash.CallAddModuleInfo(StdIcons.microsoft, "Microsoft SQL Server");
        }
    }
}
