using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace DatAdmin
{
    public partial class SqlDumper : ISqlDumper
    {
        protected readonly ISqlOutputStream m_stream;
        protected readonly SqlFormatProperties m_props;
        protected readonly ISqlDialect m_dialect;
        SqlFormatterState m_formatterState = new SqlFormatterState();
        IDialectDataAdapter m_DDA;

        public SqlDumper(ISqlOutputStream stream, ISqlDialect dialect, SqlFormatProperties props)
        {
            m_stream = stream;
            m_props = props;
            m_dialect = dialect;
            m_DDA = dialect.CreateDataAdapter();
            m_formatterState.DDA = m_DDA;
        }

        public ISqlOutputStream Stream
        {
            get { return m_stream; }
        }

        public SqlFormatProperties FormatProperties
        {
            get { return m_props; }
        }

        protected void DropTableReferences(ITableStructure table, DropFlags flags)
        {
            bool refs = (flags & DropFlags.DropReferences) != 0;
            if (refs) this.DropConstraints(table.GetReferencedFrom(), flags);
        }

        public virtual void DropTable(ITableStructure table, DropFlags flags)
        {
            DropTableReferences(table, flags);
            PutCmd("^drop ^table %f", table.FullName);
        }

        public void DropTable(ITableStructure table)
        {
            DropTable(table, DropFlags.None);
        }

        public virtual void CreateSpecificObject(ISpecificObjectStructure obj)
        {
            WriteRaw(obj.CreateSql);
            EndCommand();
        }

        public void DropSpecificObject(ISpecificObjectStructure obj)
        {
            DropSpecificObject(obj, DropFlags.None);
        }

        public virtual void DropSpecificObject(ISpecificObjectStructure obj, DropFlags flags)
        {
            PutCmd("^drop ^" + obj.ObjectType + " %f", obj.ObjectName);
        }

        public virtual void RenameSpecificObject(ISpecificObjectStructure obj, string newname)
        {
            throw new NotImplementedError("DAE-00108");
        }

        public virtual void DropColumn(IColumnStructure column)
        {
            PutCmd("^alter ^table %f ^drop ^column %i", column.Table, column.ColumnName);
        }

        public virtual void RenameColumn(IColumnStructure column, string newcol)
        {
            throw new NotImplementedError("DAE-00109");
        }

        public virtual void CreateColumn(IColumnStructure col, IEnumerable<IConstraint> constrains)
        {
            Put("^alter ^table %f add %i ", col.Table, col.ColumnName);
            ColumnDefinition(col, true, true, true);
            InlineConstraints(constrains);
            EndCommand();
        }

        protected virtual void InlineConstraints(IEnumerable<IConstraint> constrains)
        {
            if (constrains == null) return;
            foreach (var cnt in constrains)
            {
                if (cnt is IPrimaryKey)
                {
                    if (cnt.Name != null && !m_dialect.DialectCaps.AnonymousPrimaryKey)
                    {
                        Put(" ^constraint %i", cnt.Name);
                    }
                    Put(" ^primary ^key ");
                }
            }
        }

        public virtual void CreateDatabase(string dbname, Dictionary<string, string> options)
        {
            PutCmd("^create ^database %i", dbname);
            AlterDatabaseOptions(dbname, options);
        }

        public virtual void DropDatabase(string dbname)
        {
            PutCmd("^drop ^database %i", dbname);
        }

        public virtual void RenameDatabase(string oldname, string newname)
        {
            throw new NotImplementedError("DAE-00110");
        }

        public virtual void RenameConstraint(IConstraint constraint, string newname)
        {
            throw new NotImplementedError("DAE-00111");
        }

        public virtual void RenameTable(NameWithSchema oldName, string newName)
        {
            throw new NotImplementedError("DAE-00112");
        }

        public virtual void ChangeColumn(IColumnStructure oldcol, IColumnStructure newcol, IEnumerable<IConstraint> constrains)
        {
            Put("/*^alter ^table %f ^change ^column %i %i ", oldcol.Table, oldcol.ColumnName, newcol.ColumnName);
            ColumnDefinition(newcol, true, true, true);
            Put("*/");
            EndCommand();
        }

        public virtual void AllowIdentityInsert(NameWithSchema table, bool allow)
        {
        }

        public virtual void EnableConstraints(NameWithSchema table, bool enabled)
        {
        }

        public virtual void CreateDomain(IDomainStructure domain)
        {
            if (m_dialect.DialectCaps.Domains)
            {
                Put("^create ^domain %f ", domain.FullName);
                WriteRaw(m_dialect.GenericTypeToSpecific(domain.DataType).ToString());
                EndCommand();
            }
        }

        public virtual void DropDomain(IDomainStructure domain, DropFlags flags)
        {
            if (m_dialect.DialectCaps.Domains)
            {
                PutCmd("^drop ^domain %f", domain.FullName);
            }
        }

        public virtual void ChangeDomain(IDomainStructure dold, IDomainStructure dnew)
        {
            DropDomain(dold);
            CreateDomain(dnew);
        }

        public virtual void RenameDomain(NameWithSchema domain, string newname)
        {
        }
        public virtual void ChangeDomainSchema(NameWithSchema domain, string newschema)
        {
        }

        public virtual void DropDomain(IDomainStructure domain)
        {
            DropDomain(domain, DropFlags.None);
        }

        public virtual void CreateSchema(ISchemaStructure schema)
        {
            PutCmd("^create ^schema %i", schema.SchemaName);
        }

        public virtual void DropSchema(ISchemaStructure schema)
        {
            PutCmd("^drop ^schema %i", schema.SchemaName);
        }

        public ISqlDialect Dialect { get { return m_dialect; } }

        public virtual IProgressInfo ProgressInfo { get; set; }
        //public virtual IDatabaseSource TargetDb { get; set; }

        protected void SetCurWork(string text)
        {
            if (ProgressInfo != null) ProgressInfo.SetCurWork(text);
        }

        public virtual void CreateDatabaseObjects(IDatabaseStructure db, CreateDatabaseObjectsProps props)
        {
            if (Dialect.DialectCaps.Domains && props.CreateDomains)
            {
                foreach (var domain in db.Domains)
                {
                    try
                    {
                        CreateDomain(domain);
                    }
                    catch (Exception err)
                    {
                        ProgressInfo.RaiseErrorEx(err, "DAE-00244 " + Texts.Get("s_error_creating$domain", "domain", domain.FullName), "DOMAIN");
                    }
                }
            }
            if (Dialect.DialectCaps.MultipleSchema && props.CreateSchemata)
            {
                foreach (var schema in db.Schemata)
                {
                    try
                    {
                        CreateSchema(schema);
                    }
                    catch (Exception err)
                    {
                        ProgressInfo.RaiseErrorEx(err, "DAE-00245 " + Texts.Get("s_error_creating$schema", "schema", schema.SchemaName), "SCHEMA");
                    }
                }
            }
            var refsToCreate = new List<IForeignKey>();
            if (props.CreateTables)
            {
                foreach (var tbl in db.Tables)
                {
                    ITableStructure tbl2 = tbl;
                    if (!Dialect.DialectCaps.UncheckedReferences)
                    {
                        var newtbl = new TableStructure(tbl);
                        foreach (ForeignKey fk in new List<ForeignKey>(newtbl.GetConstraints<ForeignKey>()))
                        {
                            newtbl._Constraints.Remove(fk);
                            fk.SetDummyTable(tbl.FullName);
                            refsToCreate.Add(fk);
                        }
                        tbl2 = newtbl;
                    }

                    Logging.Debug("Creating table {0}", tbl2.FullName);
                    SetCurWork(String.Format("{0} {1}", Texts.Get("s_creating_table"), tbl2.FullName));
                    if (m_props.DumpWriterConfig != null && m_props.DumpWriterConfig.IncludeDropStatement)
                    {
                        DropTable(tbl2, DropFlags.TestIfExist);
                    }
                    try
                    {
                        CreateTable(tbl2);
                    }
                    catch (Exception err)
                    {
                        ProgressInfo.RaiseErrorEx(err, "DAE-00246 " + Texts.Get("s_error_creating$table", "table", tbl2.FullName), "TABLE");
                    }
                }
            }
            if (props.CreateFixedData)
            {
                foreach (var tbl in db.Tables)
                {
                    this.UpdateData(tbl, DbDiffTool.AlterFixedData(null, tbl.FixedData, null, new DbDiffOptions()), null);
                }
            }
            foreach (var fk in refsToCreate)
            {
                CreateConstraint(fk);
            }
            if (props.CreateSpecificObjects)
            {
                foreach (var obj in db.GetSpecObjectsOrderByDependency())
                {
                    SetCurWork(String.Format("{0} {1}", Texts.Get("s_creating_object"), obj.ObjectName));
                    if (m_props.DumpWriterConfig != null && m_props.DumpWriterConfig.IncludeDropStatement)
                    {
                        DropSpecificObject(obj, DropFlags.TestIfExist);
                    }
                    try
                    {
                        CreateSpecificObject(obj);
                    }
                    catch (Exception err)
                    {
                        if (ProgressInfo != null) ProgressInfo.LogMessage("s_create_object", LogLevel.Error, Texts.Get("s_error_creating_object$name", "name", obj.ObjectName) + ": " + err.Message);
                        Logging.Error("Error creating object:" + err.ToString());
                    }
                }
            }
        }

        public virtual void ChangeTableSchema(NameWithSchema oldName, string newSchema)
        {
        }

        public virtual void ChangeSpecificObjectSchema(ISpecificObjectStructure obj, string newSchema)
        {
        }

        public SqlFormatterState FormatterState
        {
            get { return m_formatterState; }
        }

        public virtual void ReorderColumns(NameWithSchema table, List<string> newColumnOrder)
        {
            PutCmd("/* RECORDER COLUMNS FOR %f (%,i) */", table, newColumnOrder);
        }

        public AlterProcessorCaps AlterCaps
        {
            get { return m_dialect.DumperCaps; }
        }

        public virtual void RenameSchema(ISchemaStructure schema, string newname)
        {
            throw new NotImplementedError("DAE-00113");
        }

        public virtual void ChangeSpecificObject(ISpecificObjectStructure osrc, ISpecificObjectStructure odst, out bool processed)
        {
            processed = false;
        }

        public void ChangeConstraint(IConstraint csrc, IConstraint cdst)
        {
            PutCmd("/* RECREATE %i TO %i (%s) */", csrc.Name, cdst.Name, cdst.GetType().Name);
        }

        public void ChangeSpecificObject(ISpecificObjectStructure osrc, ISpecificObjectStructure odst)
        {
            throw new NotImplementedError("DAE-00114");
        }

        public void ChangeSchema(ISchemaStructure ssrc, ISchemaStructure sdst)
        {
            throw new NotImplementedError("DAE-00115");
        }

        public virtual void AlterDatabaseOptions(string dbname, Dictionary<string, string> options)
        {
        }
    }
}
