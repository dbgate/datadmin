using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;

namespace Plugin.mssql
{
    public class MsSqlDumper : SqlDumper
    {
        public MsSqlDumper(ISqlOutputStream stream, ISqlDialect dialect, SqlFormatProperties props)
            : base(stream, dialect, props)
        {
        }

        private MsSqlDumpWriterConfig Wcfg { get { return m_props.DumpWriterConfig as MsSqlDumpWriterConfig; } }

        public override void RenameColumn(IColumnStructure oldcol, string newcol)
        {
            PutCmd("^execute sp_rename '%f.%i', '%s', 'COLUMN'", oldcol.Table, oldcol.ColumnName, newcol);
        }

        public override void RenameConstraint(IConstraint cnt, string newname)
        {
            if (cnt.Type == ConstraintType.Index) PutCmd("^execute sp_rename '%f.%i', '%s', 'INDEX'", cnt.Table, cnt.Name, newname);
            else PutCmd("^execute sp_rename '%f', '%s', 'OBJECT'", new NameWithSchema(cnt.Table.FullName.Schema, cnt.Name), newname);
        }

        public override void RenameDatabase(string oldname, string newname)
        {
            PutCmd("^execute sp_rename '%s', '%s', 'DATABASE'", oldname, newname);
        }

        public override void DropDatabase(string dbname)
        {
            PutCmd("ALTER DATABASE %i SET SINGLE_USER WITH ROLLBACK IMMEDIATE;USE master; DROP DATABASE %i", dbname, dbname);
        }

        private void DropDefault(IColumnStructure col)
        {
            string defname = col.GetSpecificAttribute("mssql", "defname");
            if (defname != null) PutCmd("^alter ^table %f ^drop ^constraint %i", col.Table, defname);
        }

        private string GuessDefaultName(IColumnStructure col)
        {
            string defname = col.GetSpecificAttribute("mssql", "defname");
            if (defname == null)
            {
                defname = String.Format("DF_{0}_{1}_{2}", col.Table.FullName.Schema ?? "dbo", col.Table.FullName.Name, col.ColumnName);
            }
            return defname;
        }

        public override void ColumnDefinition_Default(IColumnStructure col)
        {
            string defsql = col.DefaultValue.GenerateSql(m_dialect, col.DataType, null);
            if (defsql != null)
            {
                Put(" ^constraint %i ^default %s ", GuessDefaultName(col), defsql);
            }
        }

        private void CreateDefault(IColumnStructure col)
        {
            if (col.DefaultValue == null) return;
            string defsql = col.DefaultValue.GenerateSql(m_dialect, col.DataType, null);
            if (defsql != null)
            {
                var defname = GuessDefaultName(col);
                PutCmd("^alter ^table %f ^add ^constraint %i ^default %s for %i", col.Table, defname, defsql, col.ColumnName);
            }
        }

        public override void ChangeColumn(IColumnStructure oldcol, IColumnStructure newcol, IEnumerable<IConstraint> constraints)
        {
            DropDefault(oldcol);
            if (oldcol.ColumnName != newcol.ColumnName) RenameColumn(oldcol, newcol.ColumnName);
            Put("^alter ^table %f ^alter ^column %i ", newcol.Table, newcol.ColumnName);
            // remove autoincrement flag
            ColumnStructure newcol2 = new ColumnStructure(newcol);
            newcol2.SetDummyTable(newcol.Table.FullName);
            newcol2.DataType.SetAutoincrement(false);
            ColumnDefinition(newcol2, false, true, true);
            EndCommand();
            CreateDefault(newcol);
            this.CreateConstraints(constraints);
        }

        public override void RenameTable(NameWithSchema oldName, string newName)
        {
            PutCmd("^execute sp_rename @objname = N'%f', @newname= N'%s', @objtype = N'OBJECT'", oldName, newName);
        }

        public override void AllowIdentityInsert(NameWithSchema table, bool allow)
        {
            PutCmd("^set ^identity_insert %f %k", table, allow ? "on" : "off");
        }

        public override void DropSpecificObject(ISpecificObjectStructure obj, DropFlags flags)
        {
            bool testIfExist = (flags & DropFlags.TestIfExist) != 0;
            switch (obj.ObjectType)
            {
                case "view":
                    if (testIfExist) Put("IF EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'%f'))&n", obj.ObjectName);
                    PutCmd("^drop ^view %f", obj.ObjectName);
                    break;
                case "procedure":
                    if (testIfExist) Put("IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'%f') AND type in (N'P', N'PC'))&n", obj.ObjectName);
                    PutCmd("^drop ^procedure %f", obj.ObjectName);
                    break;
                case "function":
                    if (testIfExist) Put("IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'%f') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))&n", obj.ObjectName);
                    PutCmd("^drop ^function %f", obj.ObjectName);
                    break;
                case "trigger":
                    if (testIfExist) Put("IF EXISTS (SELECT * FROM sys.triggers WHERE object_id = OBJECT_ID(N'%f'))&n", obj.ObjectName);
                    PutCmd("^drop ^trigger %f", obj.ObjectName);
                    break;
                default:
                    throw new NotImplementedError("DAE-00333");
            }
        }

        public override void DropTable(ITableStructure table, DropFlags flags)
        {
            bool testIfExist = (flags & DropFlags.TestIfExist) != 0;
            DropTableReferences(table, flags);
            if (testIfExist)
            {
                Put("IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'%f') AND type in (N'U'))&n", table.FullName);
            }
            PutCmd("^drop ^table %f", table.FullName);
        }

        public override void RenameSpecificObject(ISpecificObjectStructure obj, string newname)
        {
            PutCmd("execute sp_rename '%f', '%s', 'OBJECT'", obj.ObjectName, newname);
        }

        public override void ChangeTableSchema(NameWithSchema oldName, string newSchema)
        {
            PutCmd("execute sp_changeobjectowner '%f', '%s'", oldName, newSchema);
        }

        public override void ChangeSpecificObjectSchema(ISpecificObjectStructure obj, string newSchema)
        {
            PutCmd("execute sp_changeobjectowner '%f', '%s'", obj.ObjectName, newSchema);
        }

        public override void CreateDomain(IDomainStructure domain)
        {
            PutCmd("^exec sp_addtype '%s', '%s', '%k'",
                domain.FullName.Name,
                m_dialect.GenericTypeToSpecific(domain.DataType).ToString(),
                domain.IsNullable ? "NULL" : "NOT NULL"
                );
        }

        public override void DropDomain(IDomainStructure domain)
        {
            PutCmd("^exec sp_droptype '%s'", domain.FullName.Name);
        }

        public override void DropColumn(IColumnStructure column)
        {
            DropDefault(column);
            base.DropColumn(column);
        }

        public override void RecreateTable(ITableStructure oldTable, ITableStructure newTable)
        {
            foreach (var col in oldTable.Columns)
            {
                DropDefault(col);
            }
            base.RecreateTable(oldTable, newTable);
        }

        private void EnumDbOptions(Action<string, string, bool> op)
        {
            op("mssql.collation", "COLLATE", false);
        }

        public override void AlterDatabaseOptions(string dbname, Dictionary<string, string> options)
        {
            EnumDbOptions((specKey, option, realprop) =>
            {
                if (options != null && options.ContainsKey(specKey))
                {
                    PutCmd("^alter ^database %i %s %s %s", dbname, realprop ? "SET" : "", option, options[specKey]);
                }
            });
        }

        public override void EnableConstraints(NameWithSchema table, bool enabled)
        {
            PutCmd("^alter ^table %f %k ^constraint ^all", table, enabled ? "check" : "nocheck");
        }

        protected override bool AllowWriteColumnCollation()
        {
            if (Wcfg != null && !Wcfg.DumpColumnCollation) return false;
            return true;
        }
    }
}
