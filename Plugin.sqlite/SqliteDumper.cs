using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;

namespace Plugin.sqlite
{
    public class SqliteDumper : SqlDumper
    {
        public SqliteDumper(ISqlOutputStream stream, ISqlDialect dialect, SqlFormatProperties props)
            : base(stream, dialect, props)
        {
        }

        public override bool CreateTablePrimaryKey(ITableStructure table, IPrimaryKey pk)
        {
            DbTypeBase type = null;
            if (pk.Columns.Count == 1) type = table.Columns[pk.Columns[0].ColumnName].DataType;
            if (type != null && type is DbTypeInt && ((DbTypeInt)type).Autoincrement) return false;
            return true;
        }

        public override void RenameTable(NameWithSchema oldName, string newName)
        {
            PutCmd("^alter ^table %f ^rename ^to %i", oldName, newName);
        }

        public override void DropConstraint(IConstraint constraint, DropFlags flags)
        {
            if (constraint is IIndex)
            {
                PutCmd("^drop ^index %i", constraint.Name);
            }
        }

        public override void CreateConstraint(IConstraint constraint)
        {
            if (constraint is IForeignKey) return;
            base.CreateConstraint(constraint);
        }
    }
}
