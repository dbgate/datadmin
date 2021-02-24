using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.Data;
using System.IO;
using Antlr.Runtime;

namespace Plugin.sqlite
{
    [Dialect(Title = "SQLite", Name = "sqlite")]
    public class SqliteDialect : DialectBase
    {
        public override char QuoteIdentBegin { get { return '"'; } }
        public override char QuoteIdentEnd { get { return '"'; } }

        public override ISpecificType CreateSpecificTypeInstance(object enumValue)
        {
            return SqliteTypeBase.CreateType((SqliteTypeCode)enumValue);
        }

        public override Type SpecificTypeEnum
        {
            get { return typeof(SqliteTypeCode); }
        }

        public override ISpecificType GenericTypeToSpecific(DbTypeBase type, IMigrationProfile profile, IProgressInfo progress)
        {
            switch (type.Code)
            {
                case DbTypeCode.Int:
                    return new SqliteTypeInt { IsAutoIncrement = ((DbTypeInt)type).Autoincrement };
                case DbTypeCode.Logical:
                    return new SqliteTypeLogical();
                case DbTypeCode.Float:
                    return new SqliteTypeReal();
                case DbTypeCode.Blob:
                    return new SqliteTypeBlob();
                case DbTypeCode.String:
                case DbTypeCode.Xml:
                case DbTypeCode.Array:
                    return new SqliteTypeText();
                case DbTypeCode.Datetime:
                    return new SqliteTypeDateTime();
                case DbTypeCode.Numeric:
                    return new SqliteTypeNumeric();
                case DbTypeCode.Text:
                    {
                        string subtype = type.GetSpecificAttribute("sqlite", "subtype");
                        return new SqliteTypeText { SpecificCode = subtype };
                    }
                    break;
            }
            return new SqliteTypeText();
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
                    RenameTable = true,
                    RecreateTable = true,
                    AddIndex = true,
                    DropIndex = true,

                    SpecificCaps = new ObjectOperationCaps { AllFlags = false, Create = true, Drop = true },
                };
            }
        }

        public override SqlDialectCaps DialectCaps
        {
            get
            {
                var res = base.DialectCaps;
                res.MultiCommand = true;
                res.ForeignKeys = true;
                res.Uniques = false;
                res.MultipleSchema = false;
                res.MultipleDatabase = false;
                res.UncheckedReferences = true;
                res.NestedTransactions = true;
                res.AnonymousPrimaryKey = true;
                return res;
            }
        }

        public string GenerateTempTableName(int id)
        {
            return "tmp_table_" + id.ToString();
        }

        public override void CreateNamedParameter(string nameBase, out string sqlName, out string formalName)
        {
            sqlName = ":" + nameBase;
            formalName = nameBase;
        }

        public override string GeneratePing()
        {
            return "SELECT * FROM sqlite_master";
        }

        public override DatabaseAnalyser CreateAnalyser()
        {
            return new SqliteAnalyser();
        }

        public override ISqlDumper CreateDumper(ISqlOutputStream stream, SqlFormatProperties props)
        {
            return new SqliteDumper(stream, this, props);
        }

        public override List<ISpecificObjectType> GetSpecificTypes()
        {
            var res = base.GetSpecificTypes();
            res.Add(new SqliteTriggerType(this));
            res.Add(new ViewObjectType(this));
            return res;
        }

        public override ISqlDialect CloneDialect()
        {
            return new SqliteDialect();
        }

        public override string DisplayName
        {
            get { return "SQLite"; }
        }

        public override void MigrateTable(TableStructure table, IMigrationProfile profile, IProgressInfo progress)
        {
            base.MigrateTable(table, profile, progress);

            MigrateTool.RemoveNonPk1AutoIncrements(table, progress);
        }

        public override string SpecificSyntaxName
        {
            get
            {
                return "Sql_sqlite";
            }
        }

        public override string GetSyntaxDef()
        {
            return SqlScripts.syntax;
        }

        public override IDataSynAdapter CreateDataSynAdapter()
        {
            return new SqliteDataSynAdapter(this);
        }

        public override IDialectDataAdapter CreateDataAdapter()
        {
            return new SqliteDDA(this);
        }

        public override IDumpLoader CreateDumpLoader()
        {
            return new SqliteDumpLoader(this);
        }

        public override bool IsSystemTable(string schema, string table)
        {
            if (table != null && table.ToLower() == "sqlite_sequence") return true;
            return base.IsSystemTable(schema, table);
        }

        public override AntlrTokens GetAntlrTokens()
        {
            var res = base.GetAntlrTokens(SQLiteParser.tokenNames);
            return res;
        }

        public override ITokenStream GetAntlrTokenStream(TextReader reader)
        {
            SQLiteLexer lexer = new SQLiteLexer(new ANTLRReaderStream(reader));
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            return tokens;
        }
    }

    [PluginHandler]
    public class SqlitePluginHandler : PluginHandlerBase
    {
        public override void OnLoadAssembly()
        {
            HSplash.CallAddModuleInfo(StdIcons.sqlite, "SQLite");
        }
    }
}
