using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;

namespace Plugin.postgre
{
    public class PostgreDumper : SqlDumper
    {
        public PostgreDumper(ISqlOutputStream stream, ISqlDialect dialect, SqlFormatProperties props)
            : base(stream, dialect, props)
        {
        }

        public override void RenameTable(NameWithSchema oldName, string newName)
        {
            PutCmd("^alter ^table %f ^rename ^to %i", oldName, newName);
        }

        public override void DropDatabase(string dbname)
        {
            PutCmd("@use postgres");
            PutCmd("@clearallpools");
            PutCmd("^drop ^database %i", dbname);
        }

        public override void ChangeColumn(IColumnStructure oldcol, IColumnStructure newcol, IEnumerable<IConstraint> constraints)
        {
            if (oldcol.ColumnName != newcol.ColumnName)
            {
                PutCmd("^alter ^table %f ^rename ^column %i ^to %i", oldcol.Table, oldcol.ColumnName, newcol.ColumnName);
            }
            if (!DbDiffTool.EqualTypes(oldcol.DataType, newcol.DataType, new DbDiffOptions()))
            {
                PutCmd("^alter ^table %f ^alter ^column %i ^type %s", newcol.Table, newcol.ColumnName, m_dialect.GenericTypeToSpecific(newcol.DataType));
            }
            if (oldcol.IsNullable != newcol.IsNullable)
            {
                if (newcol.IsNullable) PutCmd("^alter ^table %f ^alter ^column %i ^drop ^not ^null", newcol.Table, newcol.ColumnName);
                else PutCmd("^alter ^table %f ^alter ^column %i ^set ^not ^null", newcol.Table, newcol.ColumnName);
            }
            if (oldcol.DefaultValue.SafeGetSql(m_dialect) != newcol.DefaultValue.SafeGetSql(m_dialect))
            {
                if (newcol.DefaultValue == null) PutCmd("^alter ^table %f ^alter ^column %i ^drop ^default", newcol.Table, newcol.ColumnName);
                else PutCmd("^alter ^table %f ^alter ^column %i ^set ^default %s", newcol.Table, newcol.ColumnName, newcol.DefaultValue.SafeGetSql(m_dialect));
            }
            this.CreateConstraints(constraints);
        }

        public override void RenameColumn(IColumnStructure column, string newcol)
        {
            PutCmd("^alter ^table %f ^rename ^column %i ^to %i", column.Table, column.ColumnName, newcol);
        }

        public override void DropSpecificObject(ISpecificObjectStructure obj, DropFlags flags)
        {
            bool testIfExist = (flags & DropFlags.TestIfExist) != 0;
            switch (obj.ObjectType)
            {
                case "view":
                    PutCmd("^drop ^view%k %f", testIfExist ? " if exists" : "", obj.ObjectName);
                    break;
                case "sequence":
                    PutCmd("^drop ^sequence%k %f", testIfExist ? " if exists" : "", obj.ObjectName);
                    break;
                default:
                    throw new NotImplementedError("DAE-00147");
            }
        }

        public override void CreateSpecificObject(ISpecificObjectStructure obj)
        {
            switch (obj.ObjectType)
            {
                case "view":
                case "sequence":
                    WriteRaw(obj.CreateSql);
                    EndCommand();
                    break;

            }
        }

        public override void DropConstraint(IConstraint constraint, DropFlags flags)
        {
            if (constraint.Type == ConstraintType.Index)
            {
                PutCmd("^drop ^index %i", constraint.Name);
            }
            else
            {
                base.DropConstraint(constraint, flags);
            }
        }
    }
}
