using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.Linq;
using System.IO;
using System.Data;
using System.Data.OracleClient;
using Antlr.Runtime;

namespace Plugin.oracle
{
    [Dialect(Title = "Oracle", Name = "oracle")]
    public class OracleDialect : DialectBase
    {
        public override string DisplayName
        {
            get { return "Oracle"; }
        }

        public override ISqlDialect CloneDialect()
        {
            return new OracleDialect();
        }

        public override DatabaseAnalyser CreateAnalyser()
        {
            return new OracleAnalyser();
        }

        public override Type SpecificTypeEnum
        {
            get { return typeof(OracleTypeCode); }
        }

        public override ISpecificType CreateSpecificTypeInstance(object enumValue)
        {
            return OracleTypeBase.CreateType((OracleTypeCode)enumValue);
        }

        //public override bool IsSystemTable(string schema, string table)
        //{
        //    return table.Contains("$") || table == "HELP";
        //}

        public override ISpecificType GenericTypeToSpecific(DbTypeBase type, IMigrationProfile profile, IProgressInfo progress)
        {
            switch (type.Code)
            {
                case DbTypeCode.Array:
                    return new OracleTypeClob();
                case DbTypeCode.Blob:
                    if (type.GetSpecificAttribute("oracle", "subtype") == "longraw") return new OracleTypeLongRaw();
                    return new OracleTypeBlob();
                case DbTypeCode.Datetime:
                    {
                        var dtype = (DbTypeDatetime)type;
                        string subtype = type.GetSpecificAttribute("oracle", "subtype");
                        if (subtype == "timestamp")
                        {
                            var res = new OracleTypeTimestamp();
                            try { res.TimeZone = (TimeZoneType)Enum.Parse(typeof(TimeZoneType), type.GetSpecificAttribute("oracle", "timezone"), true); }
                            catch { res.TimeZone = TimeZoneType.None; }
                            string fprec = type.GetSpecificAttribute("oracle", "fractionalprecision");
                            if (fprec != null) res.FractionalPrecision = Int32.Parse(fprec);
                            return res;
                        }
                        if (subtype == "yeartomonth")
                        {
                            var res = new OracleTypeIntervalYearToMonth();
                            string yprec = type.GetSpecificAttribute("oracle", "yearprecision");
                            if (yprec != null) res.YearPrecision = Int32.Parse(yprec);
                            return res;
                        }
                        if (subtype == "daytosecond")
                        {
                            var res = new OracleTypeIntervalDayToSecond();
                            string dprec = type.GetSpecificAttribute("oracle", "dayprecision");
                            string fprec = type.GetSpecificAttribute("oracle", "fractionalprecision");
                            if (dprec != null) res.DayPrecision = Int32.Parse(dprec);
                            if (fprec != null) res.FractionalPrecision = Int32.Parse(fprec);
                            return res;
                        }
                        if (dtype.SubType == DbDatetimeSubType.Interval) return new OracleTypeIntervalDayToSecond();
                        return new OracleTypeDate();
                    }
                case DbTypeCode.Float:
                    if (type.GetSpecificAttribute("oracle", "subtype") == "number") return new OracleTypeNumber();
                    if (((DbTypeFloat)type).Bytes == 4) return new OracleTypeBinaryFloat();
                    return new OracleTypeBinaryDouble();
                case DbTypeCode.Generic:
                    return new OracleTypeGeneric { Sql = ((DbTypeGeneric)type).Sql };
                case DbTypeCode.Int:
                    {
                        string ilen = type.GetSpecificAttribute("oracle", "length");
                        if (ilen != null) return new OracleTypeNumber
                        {
                            Precision = Int32.Parse(ilen)
                        };
                    }
                    return new OracleTypeInteger();
                case DbTypeCode.Logical:
                    return new OracleTypeInteger();
                case DbTypeCode.Numeric:
                    {
                        var ntype = (DbTypeNumeric)type;
                        if (type.GetSpecificAttribute("oracle", "noprec") == "1") return new OracleTypeNumber
                        {
                            Scale = ntype.Scale
                        };
                        return new OracleTypeNumber
                        {
                            Precision = ntype.Precision,
                            Scale = ntype.Scale
                        };
                    }
                case DbTypeCode.String:
                    {
                        string subtype = type.GetSpecificAttribute("oracle", "subtype");
                        if (subtype != null)
                        {
                            switch (subtype)
                            {
                                case "rowid": return new OracleTypeRowId();
                                case "urowid": return new OracleTypeURowId();
                                case "mlslabel": return new OracleTypeMlsLabel();
                                case "bfile": return new OracleTypeBFile();
                            }
                        }
                        var stype = (DbTypeString)type;
                        if (stype.IsBinary) return new OracleTypeRaw { Length = stype.Length };
                        if (stype.IsVarLength)
                        {
                            if (stype.IsUnicode) return new OracleTypeNVarChar2 { Length = stype.Length };
                            else return new OracleTypeVarChar2 { Length = stype.Length };
                        }
                        else
                        {
                            if (stype.IsUnicode) return new OracleTypeNChar { Length = stype.Length };
                            else return new OracleTypeChar { Length = stype.Length };
                        }
                    }
                case DbTypeCode.Text:
                    if (type.GetSpecificAttribute("oracle", "subtype") == "long") return new OracleTypeLong();
                    if (((DbTypeText)type).IsUnicode) return new OracleTypeNClob();
                    else return new OracleTypeClob();
                case DbTypeCode.Xml:
                    return new OracleTypeXml();
            }
            throw new Exception("DAE-00342 unknown type");
        }

        public override ISqlDumper CreateDumper(ISqlOutputStream stream, SqlFormatProperties props)
        {
            return new OracleSqlDumper(stream, this, props);
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
                return "Sql_oracle";
            }
        }

        public override string GetSyntaxDef()
        {
            return SqlScripts.syntax;
        }

        public override List<ISpecificObjectType> GetSpecificTypes()
        {
            List<ISpecificObjectType> res = base.GetSpecificTypes();
            res.Add(new OracleViewType(this));
            res.Add(new OracleProcedureType(this));
            res.Add(new OracleFunctionType(this));
            res.Add(new OracleTriggerType(this));
            res.Add(new OracleSequenceType(this));
            return res;
        }

        public override IQuerySplitter CreateQuerySplitter()
        {
            return new OracleQuerySplitter(this);
        }

        public override IDumpLoader CreateDumpLoader()
        {
            return new OracleDumpLoader(this);
        }

        public override IDatabaseLoader CreateSpecificDatabaseLoader()
        {
            return new OracleDatabaseLoader();
        }

        public override ISqlDumpWriter CreateDumpWriter()
        {
            return new OracleDumpWriter(this);
        }

        public override SqlDumpWriterConfig CreateDumpWriterConfig()
        {
            return new OracleDumpWriterConfig();
        }

        public override IDialectDataAdapter CreateDataAdapter()
        {
            return new OracleDDA(this);
        }

        public override bool QueryIsDump(string query)
        {
            var spl = new OracleQuerySplitter(this);
            return spl.Run(new StringReader(query)).Count() >= 2;
        }

        public override SqlDialectCaps DialectCaps
        {
            get
            {
                var res = base.DialectCaps;
                res.LimitSelect = false;
                res.RangeSelect = false;
                res.MultipleSchema = false;
                res.AutoIncrement = false;
                res.MultiCommand = false;
                res.NestedTransactions = true;
                return res;
            }
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
                    ChangeTableSchema = false,

                    ChangeColumnType = true,
                    RenameColumn = true,
                    AddColumn = true,
                    DropColumn = true,
                    ChangeColumn = true,
                    ChangeColumnDefaultValue = true,
                    ChangeAutoIncrement = true,

                    RenameConstraint = true,
                    AddConstraint = true,
                    DropConstraint = true,
                    RenameIndex = true,
                    AddIndex = true,
                    DropIndex = true,

                    SpecificCaps = new ObjectOperationCaps { AllFlags = false, Create = true, Drop = true },
                    DepCaps = new AlterDependencyCaps { AllFlags = true },
                };
            }
        }

        public override DbTypeBase ReaderDataType(DataRow row)
        {
            switch ((OracleType)row["ProviderType"])
            {
                case OracleType.IntervalYearToMonth:
                    return new OracleTypeIntervalYearToMonth().ToGenericType();
                case OracleType.IntervalDayToSecond:
                    return new OracleTypeIntervalDayToSecond().ToGenericType();
            }
            return base.ReaderDataType(row);
        }

        public override string OnUpdateSqlName(ForeignKeyAction action)
        {
            return null;
        }

        public override AntlrTokens GetAntlrTokens()
        {
            var res = base.GetAntlrTokens(OracleParser.tokenNames);
            return res;
        }

        public override ITokenStream GetAntlrTokenStream(TextReader reader)
        {
            OracleLexer lexer = new OracleLexer(new ANTLRReaderStream(reader));
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            return tokens;
        }
    }

    [DialectAutoDetector(Name = "oracle")]
    public class OracleDialectAutoDetector : DialectAutoDetectorBase
    {
        public override bool TestCommand(System.Data.Common.DbConnection conn)
        {
            throw new NotImplementedError("DAE-00146");
        }
        public override ISqlDialect GetDialect()
        {
            return new OracleDialect();
        }
        public override ISqlDialect DetectDialect(string displayName)
        {
            displayName = displayName.ToLower();
            if (displayName.StartsWith("oracle"))
            {
                ISqlDialect dialect = new OracleDialect();
                return dialect;
            }
            return null;
        }
    }

    [PluginHandler]
    public class OraclePluginHandler : PluginHandlerBase
    {
        public override void OnLoadAssembly()
        {
            HSplash.CallAddModuleInfo(StdIcons.oracle, "Oracle");
        }
    }
}
