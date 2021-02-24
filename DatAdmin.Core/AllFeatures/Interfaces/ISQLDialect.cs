using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.IO;
using System.Data.Common;
using Antlr.Runtime;

namespace DatAdmin
{
    //public enum DialectQuality
    //{
    //    None, Low, Medium, High, Full
    //}

    public enum SqlSpecialConstant
    {
        None, Current_Timestamp, Utc_Timestamp, Current_User, 
        Current_Date, Current_Time
    }

    public class AntlrTokens
    {
        public int EOF, F_INC, F_DEC, F_NL, T_IDENT, T_QUOTED_IDENT, DOT, SELECT, ORDER, BY, GROUP, HAVING, WHERE, 
            JOIN, ON, FROM, T_STRING, UPDATE, DELETE, SET, INSERT, LPAREN, RPAREN, INTO;

        public bool IsIdent(int tokid)
        {
            return tokid == T_IDENT || tokid == T_QUOTED_IDENT;
        }
    }

    public interface ISqlDialect
    {
        string QuoteIdentifier(string ident);
        string UnquoteName(string name);

        SqlDumperCaps DumperCaps { get; }
        SqlDialectCaps DialectCaps { get; }

        //void GenerateSelectInto(string dsttable, ISqlSelect select, TextWriter tw, bool createNewTable);
        //string GenerateTempTableName(int id);
        string GeneratePing();
        //ITableStructure AnalyseTable(IPhysicalConnection conn, string dbname, string catalog, string schema, string tblname);
        IDatabaseStructure AnalyseDatabase(IPhysicalConnection conn, string dbname, DatabaseStructureMembers members, IProgressInfo progress);
        //DialectQuality TableAnalyserQuality(IPhysicalConnection conn);
        string GetCurrentDatabase(IPhysicalConnection conn);
        List<string> GetDatabaseNames(IPhysicalConnection conn);
        //void CreateDatabase(IPhysicalConnection conn, string dbname);

        HashSetEx<string> PossibleKeywords { get; }
        HashSetEx<string> NoContextReservedWords { get; }
        //List<string> Keywords { get;}
        //List<string> TypeNames { get;}
        //List<string> ContextKeywords { get;}

        //bool AllowedName(string name, NameType type);
        void CreateNamedParameter(string nameBase, out string sqlName, out string formalName);

        //IEnumerable<string> GetTableNames(DbConnection conn, string dbname, IConnectionBehaviourDetails behaviour);
        string DefaultSchema { get;}
        // used for hide tables from system schema
        bool IsSystemTable(string schema, string table);
        bool IsSystemObject(string type, string schema, string table);

        //DialectScripts SupportedScripts { get;}

        bool RunSpecialNonQuery(DbConnection conn, string sql); // returns, whether request was processed

        string OnUpdateSqlName(ForeignKeyAction action);
        string OnDeleteSqlName(ForeignKeyAction action);

        //string GetDefaultValueExpression(string valueFromInfoSchema);
        // value adapters for DbCommand parameters
        //string AdaptExpressionToType(string sqlExpr, DbTypeBase type);
        //object AdaptValueToType(object value, DbTypeBase type);

        string GetRangeSelect(string sql, int offset, int count);
        string GetLimitSelect(string sql, int count);

        void GetAdditionalWidgets(List<IWidget> res, ITreeNode node);
        void GetAdditionalWidgets(List<IWidget> res, AppObject appobj);
        void ReplaceStandardWidgets(List<IWidget> res, ITreeNode node);

        /// <summary>
        /// if this property is not null, dialect has specific types and must implement
        /// methods GenericTypeToSpecific and CreateSpecificTypeInstance
        /// </summary>
        Type SpecificTypeEnum { get; }
        ISpecificType GenericTypeToSpecific(DbTypeBase generic, IMigrationProfile profile, IProgressInfo progress);
        ISpecificType CreateSpecificTypeInstance(object enumValue);
        /// <summary>
        /// returns whether this dialect version supports given type code
        /// </summary>
        /// <param name="enumValue">value of type SpecificTypeEnum</param>
        /// <returns></returns>
        bool SupportsTypeCode(object enumValue);

        //public ISpecificObjectImpl ConvertSpecificToImpl(ISpecificDbObjectStructure obj);

        ISqlDumper CreateDumper(ISqlOutputStream stream, SqlFormatProperties props);

        string DialectName { get; }

        //List<string> DefinedSpecificObjects { get; }
        List<ISpecificObjectType> GetSpecificTypes();

        // reads text from file and splits it into separate queries (used for executing SQL dumps)
        //IEnumerable<string> SplitQuery(TextReader reader);

        char StringEscapeChar { get; }
        char QuoteIdentBegin { get; }
        char QuoteIdentEnd { get; }
        //string Symbols { get; }

        bool SupportsColumnCollation(DbTypeBase coltype);

        string GetSpecificExpression(SqlSpecialConstant specConst, DbTypeBase type, IProgressInfo progress);

        // returns editor for specified table structure, editor is passed to PropertyFrame
        IDialectSpecificEditor GetSpecificEditor(IAbstractObjectStructure obj, IDatabaseSource db);

        // migration functions
        void MigrateDatabase(DatabaseStructure db, IMigrationProfile profile, IProgressInfo progress);
        void MigrateTable(TableStructure table, IMigrationProfile profile, IProgressInfo progress);
        DbTypeBase MigrateDataType(IColumnStructure owningColumn, DbTypeBase type, IMigrationProfile profile, IProgressInfo progress);
        IMigrationProfile CreateMigrationProfile();
        string MigrateName(string name);

        // translates dialect exception to common datadmin exception
        Exception TranslateException(Exception err);

        // get syntax highlighting XML definition
        string GetSyntaxDef();
        // retuns name of specific syntax or null, when no specific syntax is defined
        string SpecificSyntaxName { get; }

        IBulkInserter CreateBulkInserter();

        ISqlParser CreateParser(ISqlTokenizer tokenizer);
        ISqlTokenizer CreateTokenizer(TextReader reader, IStringSliceProvider sliceProvider);
        // reformats create SQL for showing in object view
        string ReformatSpecificObject(string objtype, string createsql);
        bool EqualSpecificObjects(string objtype, string createSql1, string createSql2);

        SqlServerVersion Version { get; }
        void ParseVersion(string version);
        void SetVersion(SqlServerVersion version);
        ISqlDialect CloneDialect();
        string DisplayName { get; }
        ISqlDialect[] MajorVersions { get; }
        string SelectVersion(DbConnection conn);

        IDialectDataAdapter CreateDataAdapter();
        IDumpLoader CreateDumpLoader();
        IDatabaseLoader CreateSpecificDatabaseLoader();
        IQuerySplitter CreateQuerySplitter();
        IDataSynAdapter CreateDataSynAdapter();
        //void GetDumpFormatProps(SqlFormatProperties formatProps);
        bool QueryIsDump(string query);
        List<DependencyItem> DetectDependencies(ISpecificObjectStructure obj);
        ICmdLineAdapter CreateCmdLineAdapter();
        void BeginUnicodeDumpFile(TextWriter fw);

        bool SuportedUnicodeCharacter(char ch);
        ISqlDumpWriter CreateDumpWriter();
        SqlDumpWriterConfig CreateDumpWriterConfig();

        AntlrTokens GetAntlrTokens();
        DbTypeBase ReaderDataType(DataRow row);

        ITokenStream GetAntlrTokenStream(TextReader reader);
    }

    public interface IDialectSpecificEditor
    {
        void SaveToStructure(AbstractObjectStructure obj);
    }

    public interface ISpecificType
    {
        DbTypeBase ToGenericType();
        object Code { get;}
    }

    /// <summary>
    /// hooks, which are hold with physical connection
    /// holds subset of dialect functionality, but is dependend on db provider (DbConnection type)
    /// not on target dialect
    /// generally, if functions is available from hook and from dialect, dialect has bigger priority
    /// </summary>
    public interface IProviderHooks
    {
        List<string> GetDatabaseNames(IPhysicalConnection conn);
    }

    //public interface IDialectTypeSystem
    //{
    //    ISpecificType GenericTypeToSpecific(DbTypeBase generic);
    //    //DbTypeBase SpecificTypeToGeneric(ISpecificType specific);
    //    Type SpecificTypeEnum { get;}
    //    ISpecificType CreateSpecificTypeInstance(object enumValue);
    //}

    [AttributeUsage(AttributeTargets.Class)]
    public class DialectAttribute : RegisterAttribute
    {
    }

    [AddonType]
    public class DialectAddonType : AddonType
    {
        public override string Name
        {
            get { return "dialect"; }
        }

        public override Type InterfaceType
        {
            get { return typeof(ISqlDialect); }
        }

        public override Type RegisterAttributeType
        {
            get { return typeof(DialectAttribute); }
        }

        public static List<ISqlDialect> GetAllDialects(bool expandVersions)
        {
            List<ISqlDialect> dials = new List<ISqlDialect>();
            foreach (var hld in Instance.CommonSpace.GetAllAddons())
            {
                var dial = hld.InstanceModel as ISqlDialect;
                if (expandVersions)
                {
                    dials.Add(dial); // dialect without version
                    dials.AddRange(dial.MajorVersions);
                }
                else
                {
                    dials.Add(dial);
                }
            }
            return dials;
        }

        public static readonly DialectAddonType Instance = new DialectAddonType();
    }
}
