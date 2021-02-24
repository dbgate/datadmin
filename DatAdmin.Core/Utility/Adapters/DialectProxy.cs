using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.IO;
using System.Data;
using Antlr.Runtime;

namespace DatAdmin
{
    public class DialectProxy : ISqlDialect
    {
        protected ISqlDialect m_dialect;

        #region ISqlDialect Members

        public virtual string QuoteIdentifier(string ident)
        {
            return m_dialect.QuoteIdentifier(ident);
        }

        public virtual string UnquoteName(string name)
        {
            return m_dialect.UnquoteName(name);
        }

        public virtual SqlDumperCaps DumperCaps
        {
            get { return m_dialect.DumperCaps; }
        }

        public virtual SqlDialectCaps DialectCaps
        {
            get { return m_dialect.DialectCaps; }
        }

        public virtual string GeneratePing()
        {
            return m_dialect.GeneratePing();
        }

        public virtual IDatabaseStructure AnalyseDatabase(IPhysicalConnection conn, string dbname, DatabaseStructureMembers members, IProgressInfo progress)
        {
            return m_dialect.AnalyseDatabase(conn, dbname, members, progress);
        }

        //public virtual DialectQuality TableAnalyserQuality(IPhysicalConnection conn)
        //{
        //    return m_dialect.TableAnalyserQuality(conn);
        //}

        public virtual string GetCurrentDatabase(IPhysicalConnection conn)
        {
            return m_dialect.GetCurrentDatabase(conn);
        }

        public virtual List<string> GetDatabaseNames(IPhysicalConnection conn)
        {
            return m_dialect.GetDatabaseNames(conn);
        }

        public virtual HashSetEx<string> PossibleKeywords
        {
            get { return m_dialect.PossibleKeywords; }
        }

        public virtual HashSetEx<string> NoContextReservedWords
        {
            get { return m_dialect.NoContextReservedWords; }
        }

        public virtual void CreateNamedParameter(string nameBase, out string sqlName, out string formalName)
        {
            m_dialect.CreateNamedParameter(nameBase, out sqlName, out formalName);
        }

        public virtual string DefaultSchema
        {
            get { return m_dialect.DefaultSchema; }
        }

        public virtual bool IsSystemTable(string schema, string table)
        {
            return m_dialect.IsSystemTable(schema, table);
        }

        public virtual bool IsSystemObject(string type, string schema, string table)
        {
            return m_dialect.IsSystemObject(type, schema, table);
        }

        public virtual bool RunSpecialNonQuery(System.Data.Common.DbConnection conn, string sql)
        {
            return m_dialect.RunSpecialNonQuery(conn, sql);
        }

        public virtual string OnUpdateSqlName(ForeignKeyAction action)
        {
            return m_dialect.OnUpdateSqlName(action);
        }

        public virtual string OnDeleteSqlName(ForeignKeyAction action)
        {
            return m_dialect.OnDeleteSqlName(action);
        }

        public virtual string GetRangeSelect(string sql, int offset, int count)
        {
            return m_dialect.GetRangeSelect(sql, offset, count);
        }

        public virtual string GetLimitSelect(string sql, int count)
        {
            return m_dialect.GetLimitSelect(sql, count);
        }

        public virtual void GetAdditionalWidgets(List<IWidget> res, ITreeNode node)
        {
            m_dialect.GetAdditionalWidgets(res, node);
        }

        public virtual void GetAdditionalWidgets(List<IWidget> res, AppObject appobj)
        {
            m_dialect.GetAdditionalWidgets(res, appobj);
        }
        public virtual void ReplaceStandardWidgets(List<IWidget> res, ITreeNode node)
        {
            m_dialect.ReplaceStandardWidgets(res, node);
        }

        public virtual Type SpecificTypeEnum
        {
            get { return m_dialect.SpecificTypeEnum; }
        }

        public virtual ISpecificType GenericTypeToSpecific(DbTypeBase generic, IMigrationProfile profile, IProgressInfo progress)
        {
            return m_dialect.GenericTypeToSpecific(generic, profile, progress);
        }

        public virtual ISpecificType CreateSpecificTypeInstance(object enumValue)
        {
            return m_dialect.CreateSpecificTypeInstance(enumValue);
        }

        public virtual bool SupportsTypeCode(object enumValue)
        {
            return m_dialect.SupportsTypeCode(enumValue);
        }

        public virtual ISqlDumper CreateDumper(ISqlOutputStream stream, SqlFormatProperties props)
        {
            return m_dialect.CreateDumper(stream, props);
        }

        public virtual string DialectName
        {
            get { return m_dialect.DialectName; }
        }

        public virtual List<ISpecificObjectType> GetSpecificTypes()
        {
            return m_dialect.GetSpecificTypes();
        }

        public virtual char StringEscapeChar
        {
            get { return m_dialect.StringEscapeChar; }
        }

        public virtual char QuoteIdentBegin
        {
            get { return m_dialect.QuoteIdentBegin; }
        }

        public virtual char QuoteIdentEnd
        {
            get { return m_dialect.QuoteIdentEnd; }
        }

        public virtual bool SupportsColumnCollation(DbTypeBase coltype)
        {
            return m_dialect.SupportsColumnCollation(coltype);
        }

        public virtual string GetSpecificExpression(SqlSpecialConstant specConst, DbTypeBase type, IProgressInfo progress)
        {
            return m_dialect.GetSpecificExpression(specConst, type, progress);
        }

        public virtual IDialectSpecificEditor GetSpecificEditor(IAbstractObjectStructure obj, IDatabaseSource db)
        {
            return m_dialect.GetSpecificEditor(obj, db);
        }

        public virtual void MigrateDatabase(DatabaseStructure db, IMigrationProfile profile, IProgressInfo progress)
        {
            m_dialect.MigrateDatabase(db, profile, progress);
        }

        public virtual DbTypeBase MigrateDataType(IColumnStructure owningColumn, DbTypeBase type, IMigrationProfile profile, IProgressInfo progress)
        {
            return m_dialect.MigrateDataType(owningColumn, type, profile, progress);
        }

        public virtual void MigrateTable(TableStructure table, IMigrationProfile profile, IProgressInfo progress)
        {
            m_dialect.MigrateTable(table, profile, progress);
        }

        public virtual IMigrationProfile CreateMigrationProfile()
        {
            return m_dialect.CreateMigrationProfile();
        }

        public virtual Exception TranslateException(Exception err)
        {
            return m_dialect.TranslateException(err);
        }

        public virtual string GetSyntaxDef()
        {
            return m_dialect.GetSyntaxDef();
        }

        public virtual string SpecificSyntaxName
        {
            get { return m_dialect.SpecificSyntaxName; }
        }

        public virtual IBulkInserter CreateBulkInserter()
        {
            return m_dialect.CreateBulkInserter();
        }

        public virtual ISqlParser CreateParser(ISqlTokenizer tokenizer)
        {
            return m_dialect.CreateParser(tokenizer);
        }

        public virtual ISqlTokenizer CreateTokenizer(System.IO.TextReader reader, IStringSliceProvider sliceProvider)
        {
            return m_dialect.CreateTokenizer(reader, sliceProvider);
        }

        public virtual string ReformatSpecificObject(string objtype, string createsql)
        {
            return m_dialect.ReformatSpecificObject(objtype, createsql);
        }

        public virtual bool EqualSpecificObjects(string objtype, string createSql1, string createSql2)
        {
            return m_dialect.EqualSpecificObjects(objtype, createSql1, createSql2);
        }

        public virtual SqlServerVersion Version
        {
            get { return m_dialect.Version; }
        }

        public virtual void ParseVersion(string version)
        {
            m_dialect.ParseVersion(version);
        }

        public virtual void SetVersion(SqlServerVersion version)
        {
            m_dialect.SetVersion(version);
        }

        public virtual ISqlDialect CloneDialect()
        {
            var res = (DialectProxy) GetType().GetConstructor(new Type[] { }).Invoke(new object[] { });
            res.m_dialect = m_dialect.CloneDialect();
            return res;
        }

        public virtual string DisplayName
        {
            get { return m_dialect.DisplayName; }
        }

        public virtual ISqlDialect[] MajorVersions
        {
            get { return m_dialect.MajorVersions; }
        }

        public virtual IDialectDataAdapter CreateDataAdapter()
        {
            return m_dialect.CreateDataAdapter();
        }

        public virtual string SelectVersion(DbConnection conn)
        {
            return m_dialect.SelectVersion(conn);
        }

        public virtual IDumpLoader CreateDumpLoader()
        {
            return m_dialect.CreateDumpLoader();
        }

        public virtual IQuerySplitter CreateQuerySplitter()
        {
            return m_dialect.CreateQuerySplitter();
        }

        //public virtual void GetDumpFormatProps(SqlFormatProperties formatProps)
        //{
        //    m_dialect.GetDumpFormatProps(formatProps);
        //}

        public virtual bool QueryIsDump(string query)
        {
            return m_dialect.QueryIsDump(query);
        }

        public virtual List<DependencyItem> DetectDependencies(ISpecificObjectStructure obj)
        {
            return m_dialect.DetectDependencies(obj);
        }

        public virtual IDataSynAdapter CreateDataSynAdapter()
        {
            return m_dialect.CreateDataSynAdapter();
        }

        public virtual ICmdLineAdapter CreateCmdLineAdapter()
        {
            return m_dialect.CreateCmdLineAdapter();
        }

        public virtual string MigrateName(string name)
        {
            return m_dialect.MigrateName(name);
        }

        public virtual void BeginUnicodeDumpFile(TextWriter fw)
        {
            m_dialect.BeginUnicodeDumpFile(fw);
        }

        public virtual bool SuportedUnicodeCharacter(char ch)
        {
            return m_dialect.SuportedUnicodeCharacter(ch);
        }

        public virtual IDatabaseLoader CreateSpecificDatabaseLoader()
        {
            return m_dialect.CreateSpecificDatabaseLoader();
        }

        public virtual ISqlDumpWriter CreateDumpWriter()
        {
            return m_dialect.CreateDumpWriter();
        }

        public virtual SqlDumpWriterConfig CreateDumpWriterConfig()
        {
            return m_dialect.CreateDumpWriterConfig();
        }

        public virtual AntlrTokens GetAntlrTokens()
        {
            return m_dialect.GetAntlrTokens();
        }

        public virtual DbTypeBase ReaderDataType(DataRow row)
        {
            return DataReaderExtension.ReaderDataType(row);
        }

        public virtual ITokenStream GetAntlrTokenStream(TextReader reader)
        {
            return m_dialect.GetAntlrTokenStream(reader);
        }

        #endregion
    }
}
