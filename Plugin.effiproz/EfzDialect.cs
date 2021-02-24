using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.IO;
using Antlr.Runtime;

namespace Plugin.effiproz
{
    [Dialect(Title = "EffiProz", Name = "effiproz")]
    public class EfzDialect : DialectBase
    {
        public override string DisplayName
        {
            get { return "EffiProz"; }
        }

        public override ISqlDialect CloneDialect()
        {
            return new EfzDialect();
        }

        public override bool IsSystemTable(string schema, string table)
        {
            if (schema != null && schema.ToUpper() == "SYSTEM_LOBS") return true;
            if (schema != null && schema.ToUpper() == "INFORMATION_SCHEMA") return true;
            return base.IsSystemTable(schema, table);
        }

        public override DatabaseAnalyser CreateAnalyser()
        {
            return new EfzAnalyser();
        }

        public override ISpecificType GenericTypeToSpecific(DbTypeBase type, IMigrationProfile profile, IProgressInfo progress)
        {
            switch (type.Code)
            {
                case DbTypeCode.Int:
                    return GetEfzTypeInt((DbTypeInt)type);
                case DbTypeCode.String:
                    return GetEfzTypeString((DbTypeString)type);
                case DbTypeCode.Logical:
                    return new EfzTypeBoolean();
                case DbTypeCode.Datetime:
                    {
                        string attr = type.GetSpecificAttribute("effiproz", "subtype");
                        var dtype = type as DbTypeDatetime;
                        if (attr == "timestamp")
                        {
                            var res = new EfzTypeTimestamp();
                            res.Precision = Int32.Parse(type.GetSpecificAttribute("effiproz", "precision"));
                            return res;
                        }
                        if (attr == "yeartomonth")
                        {
                            var res = new EfzTypeIntervalYearToMonth();
                            //res.Precision = Int32.Parse(type.GetSpecificAttribute("effiproz", "precision"));
                            return res;
                        }
                        if (attr == "daytosecond")
                        {
                            var res = new EfzTypeIntervalDayToSecond();
                            //res.Precision = Int32.Parse(type.GetSpecificAttribute("effiproz", "precision"));
                            return res;
                        }
                        if (attr == "date")
                        {
                            var res = new EfzTypeDate();
                            return res;
                        }

                        switch (dtype.SubType)
                        {
                            case DbDatetimeSubType.Date:
                                return new EfzTypeDate();
                            case DbDatetimeSubType.Datetime:
                                if (dtype.HasTimeZone) return new EfzTypeTimestampTz();
                                else return new EfzTypeTimestamp();
                            case DbDatetimeSubType.Interval:
                                return new EfzTypeIntervalDayToSecond();
                            case DbDatetimeSubType.Time:
                                return new EfzTypeTimestamp();
                            case DbDatetimeSubType.Year:
                                return new EfzTypeDate();
                        }
                        return new EfzTypeTimestamp();
                    }
                case DbTypeCode.Numeric:
                    {
                        var res = new EfzTypeNumber();
                        res.Precision = ((DbTypeNumeric)type).Precision;
                        res.Scale = ((DbTypeNumeric)type).Scale;
                        res.IsIdentity = ((DbTypeNumeric)type).Autoincrement;

                        int increment;
                        if (Int32.TryParse(type.GetSpecificAttribute("effiproz", "identity_increment"), out increment))
                        {
                            res.IdentityIncrement = increment;
                            res.IdentitySeed = Int32.Parse(type.GetSpecificAttribute("effiproz", "identity_seed"));
                        }
                        return res;
                    }
                case DbTypeCode.Blob:
                    {
                        var res = new EfzTypeBlob();
                        string size = type.GetSpecificAttribute("effiproz", "maxbytes");
                        if (size != null) res.MaxBytes = Int32.Parse(size);
                        return res;
                    }
                case DbTypeCode.Text:
                    {
                        var res = new EfzTypeClob();
                        string size = type.GetSpecificAttribute("effiproz", "maxbytes");
                        if (size != null) res.MaxBytes = Int32.Parse(size);
                        return res;
                    }
                case DbTypeCode.Array:
                    return new EfzTypeClob();
                case DbTypeCode.Float:
                    return new EfzTypeDouble();
                case DbTypeCode.Xml:
                    return new EfzTypeClob();
                case DbTypeCode.Generic:
                    return new EfzTypeGeneric { Sql = ((DbTypeGeneric)type).Sql };
            }
            throw new Exception("DAE-00323 unknown type");
        }

        private EfzTypeBase GetEfzTypeString(DbTypeString type)
        {
            EfzTypeCharacter res;
            if (type.GetSpecificAttribute("effiproz", "subtype") == "uniqueidentifier")
            {
                return new EfzTypeUniqueIdentifier();
            }

            if (type.GetSpecificAttribute("effiproz", "subtype") == "varchar2")
            {
                res = new EfzTypeVarChar2();
            }
            else if (type.IsVarLength)
            {
                if (type.IsBinary) res = new EfzTypeVarBinary();
                else res = new EfzTypeVarChar();
            }
            else
            {
                if (type.IsBinary) res = new EfzTypeBinary();
                else res = new EfzTypeChar();
            }
            res.Length = type.Length;
            return res;
        }

        private EfzTypeBase GetEfzTypeInt(DbTypeInt type)
        {
            EfzTypeInteger res;
            switch (type.Bytes)
            {
                case 1:
                    res = new EfzTypeTinyInt();
                    break;
                case 2:
                    res = new EfzTypeSmallInt();
                    break;
                case 4:
                    res = new EfzTypeInt();
                    break;
                case 8:
                    res = new EfzTypeBigInt();
                    break;
                default:
                    res = new EfzTypeInt();
                    break;
            }
            res.IsIdentity = type.Autoincrement;
            int increment;
            if (Int32.TryParse(type.GetSpecificAttribute("effiproz", "identity_increment"), out increment))
            {
                res.IdentityIncrement = increment;
                res.IdentitySeed = Int32.Parse(type.GetSpecificAttribute("effiproz", "identity_seed"));
            }
            return res;
        }

        public override SqlDialectCaps DialectCaps
        {
            get
            {
                var res = base.DialectCaps;
                res.MultiCommand = false;
                res.NestedTransactions = false;
                res.ExplicitDropConstraint = false;
                res.Domains = true;
                //res.OptimizedComplexConditions = false;
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

        public override Type SpecificTypeEnum
        {
            get { return typeof(EfzTypeCode); }
        }

        public override ISpecificType CreateSpecificTypeInstance(object enumValue)
        {
            return EfzTypeBase.CreateType((EfzTypeCode)enumValue);
        }

        //public override DialectQuality TableAnalyserQuality(IPhysicalConnection conn)
        //{
        //    return DialectQuality.High;
        //}

        public override IBulkInserter CreateBulkInserter()
        {
            return new EfzBulkInserter();
        }

        //public override void MigrateTable(TableStructure table, IMigrationProfile profile, IProgressInfo progress)
        //{
        //    base.MigrateTable(table, profile, progress);
        //    table.FullName = new NameWithSchema(table.FullName.Schema, table.FullName.Name.ToUpper());
        //}

        public override char QuoteIdentBegin { get { return '"'; } }
        public override char QuoteIdentEnd { get { return '"'; } }

        public override string DefaultSchema
        {
            get { return "PUBLIC"; }
        }

        //public override string MigrateName(string name)
        //{
        //    if (name == null) return null;
        //    return name.ToUpper();
        //}

        public override ISqlDumper CreateDumper(ISqlOutputStream stream, SqlFormatProperties props)
        {
            return new EfzSqlDumper(stream, this, props);
        }

        public override List<ISpecificObjectType> GetSpecificTypes()
        {
            List<ISpecificObjectType> res = new List<ISpecificObjectType>();
            res.Add(new EfzViewType(this));
            res.Add(new EfzProcedureType(this));
            res.Add(new EfzFunctionType(this));
            res.Add(new EfzTriggerType(this));
            return res;
        }

        public override string SpecificSyntaxName
        {
            get
            {
                return "Sql_effiproz";
            }
        }

        public override string GetSyntaxDef()
        {
            return StdScripts.syntax;
        }

        public override IQuerySplitter CreateQuerySplitter()
        {
            return new EfzQuerySplitter(this);
        }

        public override IDumpLoader CreateDumpLoader()
        {
            return new EfzDumpLoader(this);
        }

        public override IDatabaseLoader CreateSpecificDatabaseLoader()
        {
            return new EfzDatabaseLoader();
        }

        public override bool QueryIsDump(string query)
        {
            foreach (string line in query.Split('\n'))
            {
                if (line.Trim() == "\\") return true;
            }
            return false;
        }

        public override AntlrTokens GetAntlrTokens()
        {
            var res = base.GetAntlrTokens(EffiProzParser.tokenNames);
            return res;
        }

        public override ITokenStream GetAntlrTokenStream(TextReader reader)
        {
            EffiProzLexer lexer = new EffiProzLexer(new ANTLRReaderStream(reader));
            CommonTokenStream tokens = new CommonTokenStream(lexer);
            return tokens;
        }
    }
}
