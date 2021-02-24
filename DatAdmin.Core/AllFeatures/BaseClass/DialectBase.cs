using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.IO;
using System.Linq;
using System.ComponentModel;
using Antlr.Runtime;

namespace DatAdmin
{
    public abstract class DialectBase : AddonBase, ISqlDialect
    {
        HashSetEx<string> m_possibleKeywords;
        HashSetEx<string> m_noContextReservedWords;

        public HashSetEx<string> PossibleKeywords
        {
            get
            {
                if (m_possibleKeywords == null) m_possibleKeywords = LoadPossibleKeywords();
                return m_possibleKeywords;
            }
        }

        public HashSetEx<string> NoContextReservedWords
        {
            get
            {
                if (m_noContextReservedWords == null) m_noContextReservedWords = LoadNoContextReservedWords();
                return m_noContextReservedWords;
            }
        }

        protected virtual HashSetEx<string> LoadNoContextReservedWords()
        {
            var res = new HashSetEx<string>();
            res.Add("SELECT"); res.Add("CREATE"); res.Add("UPDATE"); res.Add("DELETE");
            res.Add("DROP"); res.Add("ALTER"); res.Add("TABLE"); res.Add("VIEW");
            res.Add("FROM"); res.Add("WHERE"); res.Add("GROUP"); res.Add("ORDER"); res.Add("BY");
            res.Add("ASC"); res.Add("DESC"); res.Add("HAVING"); res.Add("INTO"); res.Add("ASC");
            res.Add("LEFT"); res.Add("RIGHT"); res.Add("INNER"); res.Add("OUTER");
            res.Add("CROSS"); res.Add("NATURAL"); res.Add("JOIN"); res.Add("ON");
            res.Add("DISTINCT"); res.Add("ALL"); res.Add("ANY");
            return res;
        }

        protected virtual HashSetEx<string> LoadPossibleKeywords()
        {
            var res = new HashSetEx<string>();
            res.AddRange(from k in CoreRes.keywords.Split('\n') where k != "" select k.ToUpper().Trim());
            return res;
        }


        #region ISqlDialect Members

        protected SqlServerVersion m_version = new SqlServerVersion(null);
        public virtual SqlServerVersion Version
        {
            get { return m_version; }
        }
        public virtual void ParseVersion(string version)
        {
            m_version = new SqlServerVersion(version);
        }
        public virtual void SetVersion(SqlServerVersion version)
        {
            m_version = version;
        }

        public virtual string ReformatSpecificObject(string objtype, string createsql)
        {
            return createsql;
        }

        public abstract string DisplayName { get; }

        public virtual string QuoteIdentifier(string ident)
        {
            //if (ident.IndexOf('.') >= 0) return ident;
            if (QuoteIdentBegin != '\0' && QuoteIdentEnd != '\0') return QuoteIdentBegin + ident + QuoteIdentEnd;
            return ident;
        }

        public virtual string UnquoteName(string name)
        {
            int dstart = 0, dend = 0;
            if (QuoteIdentBegin != '\0' && name.Length > 0 && name[0] == QuoteIdentBegin) dstart = 1;
            if (QuoteIdentEnd != '\0' && name.Length > dstart && name[name.Length - 1] == QuoteIdentEnd) dend = 1;
            if (dstart > 0 || dend > 0) return name.Substring(dstart, name.Length - dstart - dend);
            return name;
        }

        protected virtual bool CanAddLimit(string sql)
        {
            return (sql.ToLower().TrimStart().StartsWith("select") && sql.ToLower().Contains("from"));
        }

        public virtual string GetRangeSelect(string sql, int offset, int count)
        {
            if (!DialectCaps.RangeSelect) return sql;
            if (!CanAddLimit(sql)) return sql;
            return String.Format("{0} LIMIT {1} OFFSET {2}", sql, count, offset);
        }

        public virtual string GetLimitSelect(string sql, int count)
        {
            if (!DialectCaps.LimitSelect) return sql;
            if (!CanAddLimit(sql)) return sql;
            return String.Format("{0} LIMIT {1}", sql, count);
        }

        public virtual SqlDialectCaps DialectCaps
        {
            get
            {
                var res = new SqlDialectCaps(true);
                res.Domains = false;
                res.UncheckedReferences = false;
                res.AnonymousPrimaryKey = false;
                res.NestedTransactions = false;
                res.UseDatabaseAsSchema = false;
                res.RangeSelect = false;
                res.Arrays = false;
                res.SupportBackup = false;
                res.AutoIncrement = true;
                return res;
                //                return new SqlDialectCaps(true)
                //                {
                //                    Domains = false,
                //                    UncheckedReferences = false,
                //                    AnonymousPrimaryKey = false,
                //                    NestedTransactions = false,
                //                    UseDatabaseAsSchema = false,
                //                };
            }
        }

        //public abstract bool SupportsMultiCommand { get;}
        //public virtual bool AnonymousPrimaryKey { get { return false; } }

        //public virtual string QuoteString(string value)
        //{
        //    return "'" + EscapeString(value) + "'";
        //}

        //public virtual string EscapeString(string value)
        //{
        //    char esc = StringEscapeChar;
        //    StringBuilder sb = new StringBuilder(value.Length + 10);
        //    foreach (var ch in value)
        //    {
        //        if (ch == '\'' || ch == esc)
        //        {
        //            sb.Append(esc);
        //            sb.Append(ch);
        //        }
        //        else
        //        {
        //            sb.Append(ch);
        //        }
        //    }
        //    return sb.ToString();
        //}

        //public virtual string EscapeDateTime(DateTime value)
        //{
        //    return "'" + value.ToString("s", CultureInfo.InvariantCulture) + "'";
        //}

        //public virtual string EscapeNumber(object number)
        //{
        //    return Convert.ToString(number, CultureInfo.InvariantCulture);
        //}

        //public virtual string EscapeLogical(bool value)
        //{
        //    return value ? "1" : "0";
        //}

        //public virtual string EscapeBinary(byte[] value)
        //{
        //    if (value.Length == 0) return "''";
        //    return "0x" + StringTool.EncodeHex(value);
        //}

        //public virtual IDialectTypeSystem TypeSystem
        //{
        //    get { return null; ; }
        //}

        public virtual string DefaultSchema { get { return null; } }

        //public abstract string GenerateRenameTable(NameWithSchema oldName, NameWithSchema newName);

        //public virtual void GenerateAllowIdentityInsert(NameWithSchema table, bool allow, ISqlOutputStream tw)
        //{
        //}

        //public virtual ITableStructure AnalyseTable(IPhysicalConnection conn, string dbname, string catalog, string schema, string tblname)
        //{
        //    return AnalyseSql92Table.Run(conn, dbname, catalog, schema, tblname, this, true);
        //}

        //public virtual List<string> Keywords
        //{
        //    get
        //    {
        //        List<string> res = new List<string>();
        //        res.AddRange(new string[] { 
        //        "select", "update", "from", "where", "order", "by", 
        //        "create", "table", "column", "delete", "database", "alter", "add", "remove",
        //        "index", "constraint", "drop", "primary", "foreign", "key",
        //        "inner", "outer", "full", "left", "right", "join", "on", "cross", "natural",
        //        "insert", "into", "go"
        //        });
        //        return res;
        //    }
        //}

        //public virtual List<string> TypeNames
        //{
        //    get
        //    {
        //        List<string> res = new List<string>();
        //        res.AddRange(new string[] { 
        //        "int", "char", "varchar", "text", "numeric", "decimal"
        //        });
        //        return res;
        //    }
        //}

        //public virtual List<string> ContextKeywords
        //{
        //    get
        //    {
        //        List<string> res = new List<string>();
        //        return res;
        //    }
        //}

        //public virtual DialectQuality TableAnalyserQuality(IPhysicalConnection conn)
        //{
        //    return DialectQuality.Low;
        //}

        //public virtual bool AllowedName(string name, NameType type)
        //{
        //    return true;
        //}

        public virtual string GetCurrentDatabase(IPhysicalConnection conn)
        {
            return conn.SystemConnection.Database;
        }

        //public virtual void CreateDatabase(IPhysicalConnection conn, string dbname)
        //{
        //    using (DbCommand cmd = conn.DbFactory.CreateCommand())
        //    {
        //        cmd.Connection = conn.SystemConnection;
        //        cmd.CommandText = "CREATE DATABASE " + QuoteIdentifier(dbname);
        //        cmd.ExecuteNonQuery();
        //    }
        //}

        public virtual void CreateNamedParameter(string nameBase, out string sqlName, out string formalName)
        {
            sqlName = "?" + nameBase;
            formalName = nameBase;
        }

        //public virtual bool SupportsNestedTransactions { get { return false; } }

        //// constraint support
        //public virtual bool SupportsForeignKeys { get { return true; } }
        //public virtual bool SupportsPrimaryKeys { get { return true; } }
        //public virtual bool SupportsUniques { get { return true; } }
        //public virtual bool SupportsIndexes { get { return true; } }
        //public virtual bool SupportsChecks { get { return true; } }

        //public virtual bool SupportsAlterTable { get { return true; } }
        //public virtual bool SupportsSchemas { get { return true; } }


        public virtual void ChangeColumn(NameWithSchema table, IColumnStructure oldcol, IColumnStructure newcol, ISqlOutputStream tw)
        {
            throw new NotImplementedError("DAE-00155");
        }

        public virtual SqlDumperCaps DumperCaps
        {
            get
            {
                return new SqlDumperCaps
                {
                    AllFlags = false,
                };
            }
        }

        //public virtual void ChangeColumn(NameWithSchema table, string oldname, string newname, string coldef, ISqlOutputStream tw)
        //{
        //    tw.Write(String.Format("ALTER TABLE {0} ADD tmpcol {1}", DialectExtension.QuoteFullName(this,table), coldef));
        //    tw.EndCommand();
        //    tw.Write(String.Format("UPDATE {0} SET tmpcol={1}", DialectExtension.QuoteFullName(this, table), QuoteIdentifier(oldname)));
        //    tw.EndCommand();
        //    tw.Write(String.Format("ALTER TABLE {0} DROP COLUMN {1}", DialectExtension.QuoteFullName(this, table), QuoteIdentifier(oldname)));
        //    tw.EndCommand();
        //    tw.Write(String.Format("ALTER TABLE {0} ADD {1} {2}", DialectExtension.QuoteFullName(this, table), QuoteIdentifier(newname), coldef));
        //    tw.EndCommand();
        //    tw.Write(String.Format("UPDATE {0} SET {1}=tmpcol", DialectExtension.QuoteFullName(this, table), QuoteIdentifier(newname)));
        //    tw.EndCommand();
        //    tw.Write(String.Format("ALTER TABLE {0} DROP COLUMN tmpcol", DialectExtension.QuoteFullName(this, table)));
        //    tw.EndCommand();
        //}

        //public virtual bool ExplicitDropConstraint { get { return false; } }

        //public virtual IEnumerable<string> GetTableNames(DbConnection conn, string dbname, IConnectionBehaviourDetails behaviour)
        //{
        //    return SchemaTool.GetTableNames(conn, dbname, behaviour.UseDbInGetSchemaRestriction);
        //}

        public virtual bool IsSystemTable(string schema, string table)
        {
            return table.StartsWith("d2dx_");
        }

        public virtual bool IsSystemObject(string type, string schema, string table)
        {
            if (type == "view" && schema != null && schema.ToLower() == "information_schema") return true;
            return false;
        }

        public virtual List<ISpecificObjectType> GetSpecificTypes()
        {
            return new List<ISpecificObjectType>();
        }

        //public virtual DialectScripts SupportedScripts
        //{
        //    get { return DialectScripts.DropColumn | DialectScripts.DropConstraint | DialectScripts.CreateDatabase | DialectScripts.DropDatabase; }
        //}

        //public virtual string GenerateScript_DropColumn(NameWithSchema table, string column)
        //{
        //    return String.Format("ALTER TABLE {0} DROP COLUMN {1}", DialectExtension.QuoteFullName(this, table), QuoteIdentifier(column));
        //}

        //public virtual string GenerateScript_RenameColumn(NameWithSchema table, string oldcol, string newcol)
        //{
        //    throw new Exception("The method or operation is not implemented.");
        //}

        //public virtual string GenerateScript_CreateDatabase(string dbname)
        //{
        //    return String.Format("CREATE DATABASE {0}", dbname);
        //}

        //public virtual string GenerateScript_DropDatabase(string dbname)
        //{
        //    return String.Format("DROP DATABASE {0}", dbname);
        //}

        //public virtual string GenerateScript_RenameDatabase(string oldname, string newname)
        //{
        //    throw new Exception("The method or operation is not implemented.");
        //}

        //public virtual string GenerateScript_DropConstraint(NameWithSchema table, string constraint, string sqltype)
        //{
        //    if (sqltype == "INDEX")
        //    {
        //        return String.Format("DROP INDEX {0} ON {1}", QuoteIdentifier(constraint), this.QuoteFullName(table));
        //    }
        //    else
        //    {
        //        if (ExplicitDropConstraint)
        //        {
        //            if (sqltype == "PRIMARY KEY")
        //            {
        //                return String.Format("ALTER TABLE {0} DROP PRIMARY KEY", this.QuoteFullName(table));
        //            }
        //            else
        //            {
        //                return String.Format("ALTER TABLE {0} DROP {1} {2}", this.QuoteFullName(table), sqltype, QuoteIdentifier(constraint));
        //            }
        //        }
        //        else
        //        {
        //            if (constraint != null)
        //            {
        //                return String.Format("ALTER TABLE {0} DROP CONSTRAINT {1}", this.QuoteFullName(table), QuoteIdentifier(constraint));
        //            }
        //        }
        //    }
        //    return null;
        //}

        //public virtual string GenerateScript_RenameConstraint(NameWithSchema table, string oldcnt, string newcnt, string sqltype)
        //{
        //    throw new Exception("The method or operation is not implemented.");
        //}

        public virtual bool RunSpecialNonQuery(DbConnection conn, string sql)
        {
            return false;
        }

        public virtual string OnUpdateSqlName(ForeignKeyAction action)
        {
            return FkActionSqlName(action);
        }

        public virtual string OnDeleteSqlName(ForeignKeyAction action)
        {
            return FkActionSqlName(action);
        }

        //public virtual string GetDefaultValueExpression(string valueFromInfoSchema)
        //{
        //    return valueFromInfoSchema;
        //}

        public virtual string GeneratePing()
        {
            return "SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES";
        }

        #endregion

        public virtual DatabaseAnalyser CreateAnalyser()
        {
            return new DatabaseAnalyser();
        }

        public IDatabaseStructure AnalyseDatabase(IPhysicalConnection conn, string dbname, DatabaseStructureMembers members, IProgressInfo progress)
        {
            DatabaseAnalyser analyser = CreateAnalyser();
            try
            {
                return analyser.Run(this, conn, dbname, members, progress);
            }
            catch (Exception err)
            {
                conn.FillInfo(err.Data);
                err.Data["analyse_dbname"] = dbname;
                throw;
            }
        }

        protected virtual string FkActionSqlName(ForeignKeyAction action)
        {
            return action.SqlName();
        }

        //public virtual string AdaptExpressionToType(string sqlExpr, DbTypeBase type)
        //{
        //    return sqlExpr;
        //}

        //public virtual object AdaptValueToType(object value, DbTypeBase type)
        //{
        //    if (value == null || DBNull.Value == value) return DBNull.Value;
        //    return Convert.ChangeType(value, type.DotNetType, CultureInfo.InvariantCulture);
        //}

        public virtual void GetAdditionalWidgets(List<IWidget> res, ITreeNode node)
        {
        }

        public virtual void GetAdditionalWidgets(List<IWidget> res, AppObject appobj)
        {
        }
        public virtual void ReplaceStandardWidgets(List<IWidget> res, ITreeNode node)
        {
        }


        public virtual Type SpecificTypeEnum { get { return typeof(DbTypeCode); } }
        public virtual ISpecificType GenericTypeToSpecific(DbTypeBase generic, IMigrationProfile profile, IProgressInfo progress)
        {
            return generic;
        }
        public virtual ISpecificType CreateSpecificTypeInstance(object enumValue)
        {
            return DbTypeBase.CreateType((DbTypeCode)enumValue);
        }

        public virtual ISqlDumper CreateDumper(ISqlOutputStream stream, SqlFormatProperties props)
        {
            return new SqlDumper(stream, this, props);
        }

        // implicitly read from attribute
        public virtual string DialectName
        {
            get
            {
                foreach (RegisterAttribute attr in GetType().GetCustomAttributes(typeof(RegisterAttribute), true))
                {
                    return attr.Name;
                }
                return null;
            }
        }

        //public virtual IEnumerable<string> SplitQuery(TextReader reader)
        //{
        //    return QueryTools.GoSplit(reader);
        //}

        //public virtual List<string> DefinedSpecificObjects
        //{
        //    get { return new List<string>(); }
        //}

        public virtual char StringEscapeChar { get { return '\''; } }
        public virtual char QuoteIdentBegin { get { return '\0'; } }
        public virtual char QuoteIdentEnd { get { return '\0'; } }

        //public virtual string Symbols { get { return "()=+-<>*/"; } }

        public virtual string GetSpecificExpression(SqlSpecialConstant specConst, DbTypeBase type, IProgressInfo progress)
        {
            return null;
        }
        public virtual ISqlParser CreateParser(ISqlTokenizer tokenizer)
        {
            return new SqlParser(tokenizer, this);
        }
        public virtual ISqlTokenizer CreateTokenizer(TextReader reader, IStringSliceProvider sliceProvider)
        {
            var res = new SqlTokenizer(reader, sliceProvider, this);
            return res;
        }
        public virtual bool SupportsColumnCollation(DbTypeBase coltype)
        {
            return false;
        }
        public virtual IDialectSpecificEditor GetSpecificEditor(IAbstractObjectStructure obj, IDatabaseSource db)
        {
            return null;
        }
        public virtual void MigrateTable(TableStructure table, IMigrationProfile profile, IProgressInfo progress)
        {
            foreach (ColumnStructure col in table.Columns)
            {
                // character and collation is not portable
                col.Collation = "";
                col.CharacterSet = "";
                col.DataType = MigrateDataType(col, col.DataType, profile, progress);
                List<string> todel = new List<string>();
                foreach (string attr in col.SpecificData.Keys)
                {
                    if (!attr.StartsWith(DialectName + ".")) todel.Add(attr);
                }
                foreach (string d in todel) col.SpecificData.Remove(d);
            }
            var newConstraints = new List<Constraint>();
            foreach (Constraint cnt in table.Constraints)
            {
                var newcnt = MigrateConstraintOrIndex(cnt, profile, progress);
                if (newcnt != null) newConstraints.Add(newcnt);
            }
            table._Constraints.Clear();
            foreach (var cnt in newConstraints) table._Constraints.Add(cnt);
        }

        public virtual void MigrateDatabase(DatabaseStructure db, IMigrationProfile profile, IProgressInfo progress)
        {
            if (db.Dialect.SameDialect(this)) return;
            foreach (TableStructure table in db.Tables)
            {
                MigrateTable(table, profile, progress);
            }
            db.Dialect = this;
        }

        protected virtual Constraint MigrateConstraintOrIndex(Constraint cnt, IMigrationProfile profile, IProgressInfo progress)
        {
            if (cnt is IPrimaryKey)
            {
                if (DialectCaps.AnonymousPrimaryKey)
                {
                    PrimaryKey pk = (PrimaryKey)cnt;
                    pk.Name = DbObjectNameTool.PkName(pk.Table.FullName);
                }
            }
            return cnt;
        }

        public virtual DbTypeBase MigrateDataType(IColumnStructure owningColumn, DbTypeBase type, IMigrationProfile profile, IProgressInfo progress)
        {
            ISpecificType spectype = GenericTypeToSpecific(type, profile, progress);
            //if (!DialectCaps.Arrays && type.ArraySpec.IsArray)
            //{
            //    return new DbTypeText(); // migrate arrays as text blob
            //}
            return spectype.ToGenericType();
        }

        public virtual IMigrationProfile CreateMigrationProfile() { return null; }

        public virtual List<string> GetDatabaseNames(IPhysicalConnection conn)
        {
            try
            {
                return conn.SystemConnection.GetDatabaseNames();
            }
            catch
            {
                if (conn.ProviderHooks != null) return conn.ProviderHooks.GetDatabaseNames(conn);
                return new List<string> { null };
            }
        }

        public virtual IBulkInserter CreateBulkInserter()
        {
            return new BulkInserterBase();
        }

        public virtual Exception TranslateException(Exception err)
        {
            while (err.InnerException != null && !(err is DbException)) err = err.InnerException;
            return err;
        }

        [Browsable(false)]
        public override AddonType AddonType
        {
            get { return DialectAddonType.Instance; }
        }

        public virtual string GetSyntaxDef()
        {
            return null;
        }
        public virtual string SpecificSyntaxName
        {
            get { return null; }
        }

        public virtual bool EqualSpecificObjects(string objtype, string createSql1, string createSql2)
        {
            return createSql1.Trim().Replace("\r\n", "\n") == createSql2.Trim().Replace("\r\n", "\n");
        }

        public abstract ISqlDialect CloneDialect();
        public virtual ISqlDialect[] MajorVersions
        {
            get { return new ISqlDialect[] { }; }
        }

        public override string ToString()
        {
            return DisplayName;
        }

        public virtual bool SupportsTypeCode(object enumValue)
        {
            return true;
        }

        public virtual IDialectDataAdapter CreateDataAdapter()
        {
            return new DialectDataAdapterBase(this);
        }

        public virtual string SelectVersion(DbConnection conn)
        {
            return null;
        }

        public virtual IQuerySplitter CreateQuerySplitter()
        {
            return new QuerySplitterBase(this);
        }

        public virtual IDumpLoader CreateDumpLoader()
        {
            return new DumpLoaderBase(this);
        }

        //public virtual void GetDumpFormatProps(SqlFormatProperties formatProps)
        //{
        //}

        public virtual bool QueryIsDump(string query)
        {
            return false;
        }

        public virtual List<DependencyItem> DetectDependencies(ISpecificObjectStructure obj)
        {
            return new List<DependencyItem>();
        }

        public virtual IDataSynAdapter CreateDataSynAdapter()
        {
            return null;
        }

        public virtual ICmdLineAdapter CreateCmdLineAdapter()
        {
            return null;
        }

        public virtual string MigrateName(string name)
        {
            return name;
        }

        public virtual void BeginUnicodeDumpFile(TextWriter fw)
        {
        }

        public virtual bool SuportedUnicodeCharacter(char ch)
        {
            return true;
            //var cat = Char.GetUnicodeCategory(ch);
            //if (
            //    (cat == UnicodeCategory.Control && (int)ch > 32)
            //    || ch == 0
            //    || cat == UnicodeCategory.OtherSymbol
            //    || cat == UnicodeCategory.Format
            //    || cat == UnicodeCategory.OtherNotAssigned
            //    || cat == UnicodeCategory.ModifierLetter
            //    || cat == UnicodeCategory.ModifierSymbol
            //    )
            //    return false;
            //return true;
        }

        public virtual IDatabaseLoader CreateSpecificDatabaseLoader()
        {
            return new GenericDatabaseLoader();
        }

        public virtual ISqlDumpWriter CreateDumpWriter()
        {
            return new SqlDumpWriterBase(this);
        }

        public virtual SqlDumpWriterConfig CreateDumpWriterConfig()
        {
            return new SqlDumpWriterConfig();
        }

        public virtual AntlrTokens GetAntlrTokens()
        {
            return new AntlrTokens();
        }

        protected AntlrTokens GetAntlrTokens(string[] tokenNames)
        {
            var res = new AntlrTokens();
            res.EOF = Array.IndexOf(tokenNames, "EOF");
            res.F_DEC = Array.IndexOf(tokenNames, "F_DEC");
            res.F_INC = Array.IndexOf(tokenNames, "F_INC");
            res.F_NL = Array.IndexOf(tokenNames, "F_NL");
            res.T_IDENT = Array.IndexOf(tokenNames, "T_IDENT");
            res.T_QUOTED_IDENT = Array.IndexOf(tokenNames, "T_QUOTED_IDENT");
            res.DOT = Array.IndexOf(tokenNames, "DOT");
            res.SELECT = Array.IndexOf(tokenNames, "SELECT");
            res.ORDER = Array.IndexOf(tokenNames, "ORDER");
            res.BY = Array.IndexOf(tokenNames, "BY");
            res.GROUP = Array.IndexOf(tokenNames, "GROUP");
            res.HAVING = Array.IndexOf(tokenNames, "HAVING");
            res.WHERE = Array.IndexOf(tokenNames, "WHERE");
            res.JOIN = Array.IndexOf(tokenNames, "JOIN");
            res.ON = Array.IndexOf(tokenNames, "ON");
            res.FROM = Array.IndexOf(tokenNames, "FROM");
            res.T_STRING = Array.IndexOf(tokenNames, "T_STRING");
            res.UPDATE = Array.IndexOf(tokenNames, "UPDATE");
            res.DELETE = Array.IndexOf(tokenNames, "DELETE");
            res.SET = Array.IndexOf(tokenNames, "SET");
            res.INSERT = Array.IndexOf(tokenNames, "INSERT");
            res.LPAREN = Array.IndexOf(tokenNames, "LPAREN");
            res.RPAREN = Array.IndexOf(tokenNames, "RPAREN");
            res.INTO = Array.IndexOf(tokenNames, "INTO");
            return res;
        }

        public virtual DbTypeBase ReaderDataType(DataRow row)
        {
            return DataReaderExtension.ReaderDataType(row);
        }

        public virtual ITokenStream GetAntlrTokenStream(TextReader reader)
        {
            return null;
        }
    }

    //[Dialect(Name = "dummy", Title = "Dummy")]
    //public class DummyDialect : DialectBase
    //{
    //    public readonly static DummyDialect Instance = new DummyDialect();
    //    //public override DbTypeBase AnalyseType(IDataRecord row, IPhysicalConnection conn)
    //    //{
    //    //    return new DbTypeString();
    //    //}

    //    //public override bool IsColumnNullable(IDataRecord row)
    //    //{
    //    //    return true;
    //    //}

    //    //public override string GenerateRenameTable(NameWithSchema oldName, NameWithSchema newName)
    //    //{
    //    //    throw new Exception("The method or operation is not implemented.");
    //    //}
    //    //public override bool SupportsMultiCommand
    //    //{
    //    //    get { return true; }
    //    //}
    //}
}
