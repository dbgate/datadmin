using System;
using System.Collections.Generic;
using System.Text;
using System.Data.OleDb;
using System.Data;
using DatAdmin;
using System.Data.Common;
using System.Text.RegularExpressions;
using System.IO;
using Antlr.Runtime;

namespace Plugin.access
{
    [Dialect(Title = "MS Access", Name = "access")]
    public class AccessDialect : DialectBase
    {
        public override bool IsSystemTable(string schema, string table)
        {
            if (table.StartsWith("MSys")) return true;
            return base.IsSystemTable(schema, table);
        }

        public override ISpecificType GenericTypeToSpecific(DbTypeBase type, IMigrationProfile profile, IProgressInfo progress)
        {
            switch (type.Code)
            {
                case DbTypeCode.Int:
                    return GetAccessTypeInt((DbTypeInt)type);
                case DbTypeCode.String:
                    return GetAccessTypeString((DbTypeString)type);
                case DbTypeCode.Logical:
                    return new AccessTypeBit();
                case DbTypeCode.Datetime:
                    return new AccessTypeDatetime();
                case DbTypeCode.Numeric:
                    {
                        AccessTypeDecimal res = new AccessTypeDecimal();
                        res.Precision = ((DbTypeNumeric)type).Precision;
                        res.Scale = ((DbTypeNumeric)type).Scale;
                        res.IsAutoIncrement = ((DbTypeNumeric)type).Autoincrement;
                        return res;
                    }
                case DbTypeCode.Blob:
                    return new AccessTypeImage();
                case DbTypeCode.Text:
                case DbTypeCode.Array:
                    return new AccessTypeText();
                case DbTypeCode.Float:
                    {
                        DbTypeFloat tp = (DbTypeFloat)type;
                        if (tp.IsMoney) return new AccessTypeMoney();
                        if (tp.Bytes == 4) return new AccessTypeReal();
                        else return new AccessTypeFloat();
                    }
                case DbTypeCode.Xml:
                    return new AccessTypeText();
                case DbTypeCode.Generic:
                    return new AccessTypeGeneric { Sql = ((DbTypeGeneric)type).Sql };
            }
            throw new InternalError("DAE-00058 unknown type");
        }

        private ISpecificType GetAccessTypeInt(DbTypeInt type)
        {
            AccessTypeIntBase res;
            switch (type.Bytes)
            {
                case 1:
                    res = new AccessTypeTinyInt();
                    break;
                case 2:
                    res = new AccessTypeSmallInt();
                    break;
                case 4:
                    res = new AccessTypeInteger();
                    break;
                default:
                    res = new AccessTypeTinyInt();
                    break;
            }
            res.IsAutoIncrement = type.Autoincrement;
            return res;
        }

        private ISpecificType GetAccessTypeString(DbTypeString type)
        {
            if (type.IsBinary)
            {
                AccessTypeBinary res = new AccessTypeBinary();
                res.Length = type.Length;
                return res;
            }
            else
            {
                AccessTypeCharacter res = new AccessTypeCharacter();
                res.Length = type.Length;
                return res;
            }
        }

        public override char QuoteIdentBegin { get { return '['; } }
        public override char QuoteIdentEnd { get { return ']'; } }

        public override SqlDialectCaps DialectCaps
        {
            get
            {
                var res = base.DialectCaps;
                res.MultiCommand = false;
                res.MultipleSchema = false;
                res.MultipleDatabase = false;
                res.ExplicitDropConstraint = false;
                return res;
            }
        }

        public override string GetLimitSelect(string sql, int count)
        {
            if (sql.IndexOf("from", StringComparison.InvariantCultureIgnoreCase) < 0) return sql;
            var m1 = Regex.Match(sql, @"^(\s*select\s+)(distinct\s+)?(.*)$", RegexOptions.IgnoreCase);
            if (m1.Success)
            {
                string res = m1.Groups[1].Value + m1.Groups[2].Value + " top " + count.ToString() + " " + m1.Groups[3].Value;
                return res;
            }
            else
            {
                return sql;
            }
        }

        public override Type SpecificTypeEnum
        {
            get { return typeof(AccessTypeCode); }
        }

        public override ISpecificType CreateSpecificTypeInstance(object code)
        {
            return AccessTypeBase.CreateType((AccessTypeCode)code);
        }

        public override void CreateNamedParameter(string nameBase, out string sqlName, out string formalName)
        {
            sqlName = "@" + nameBase;
            formalName = "@" + nameBase;
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
                    ChangeColumnType = true,
                    AddColumn = true,
                    DropColumn = true,
                    ChangeColumn = true,
                    //RenameTable = true,
                    AddConstraint = true,
                    DropConstraint = true,
                    AddIndex = true,
                    DropIndex = true,
                };
            }
        }

        public override string GeneratePing()
        {
            return "SELECT * FROM MSysObjects";
        }

        public override DatabaseAnalyser CreateAnalyser()
        {
            return new AccessAnalyser();
        }

        public override DbTypeBase MigrateDataType(IColumnStructure owningColumn, DbTypeBase type, IMigrationProfile profile, IProgressInfo progress)
        {
            var stype = type as DbTypeString;
            if (stype != null)
            {
                if (stype.Length > 255)
                {
                    string msg = Texts.Get("s_reduced_column_length$column$oldlen$newlen",
                    "column", owningColumn.Table.ToString() + "." + owningColumn.ColumnName,
                    "oldlen", stype.Length,
                    "newlen", 255);
                    progress.LogMessage("TABLE", LogLevel.Warning, msg);
                    Logging.Info(msg);
                    stype.Length = 255;
                    return stype;
                }
            }
            return base.MigrateDataType(owningColumn, type, profile, progress);
        }

        public override ISqlDumper CreateDumper(ISqlOutputStream stream, SqlFormatProperties props)
        {
            return new AccessDumper(stream, this, props);
        }

        public override ISqlDialect CloneDialect()
        {
            return new AccessDialect();
        }

        public override string DisplayName
        {
            get { return "Access"; }
        }

        public override string SpecificSyntaxName
        {
            get
            {
                return "Sql_access";
            }
        }

        public override string GetSyntaxDef()
        {
            return SqlScripts.syntax;
        }

        public override IDialectDataAdapter CreateDataAdapter()
        {
            return new AccessDDA(this);
        }

        public override AntlrTokens GetAntlrTokens()
        {
            var res = base.GetAntlrTokens(AccessParser.tokenNames);
            return res;
        }

        public override ITokenStream GetAntlrTokenStream(TextReader reader)
        {
            AccessLexer lexer = new AccessLexer(new ANTLRReaderStream(reader));
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            return tokens;
        }
    }

    [DialectAutoDetector(Name = "access")]
    public class AccessDialectAutoDetector : DialectAutoDetectorBase
    {
        public override bool TestCommand(DbConnection conn)
        {
            // we require OLE DB for analysing
            if (!(conn is OleDbConnection)) return false; 
            DataTable tbl = conn.GetSchema("DataSourceInformation");
            return tbl.Rows[0][1].ToString() == "MS Jet";
        }
        public override ISqlDialect GetDialect()
        {
            return new AccessDialect();
        }
        public override ISqlDialect DetectDialect(string displayName)
        {
            if (displayName.ToLower().StartsWith("access")) return new AccessDialect();
            return null;
        }
    }

    [PluginHandler]
    public class AccessPluginHandler : PluginHandlerBase
    {
        public override void OnLoadAssembly()
        {
            HSplash.CallAddModuleInfo(StdIcons.msaccess, "MS Access");
        }
    }
}
