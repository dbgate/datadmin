using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;

namespace Plugin.access
{
    public class AccessDumper : SqlDumper
    {
        public AccessDumper(ISqlOutputStream stream, ISqlDialect dialect, SqlFormatProperties props)
            : base(stream, dialect, props)
        {
        }

        //public override void RenameTable(NameWithSchema oldName, string newName)
        //{
        //    PutCmd("^rename ^table %i ^to %i", oldName.Name, newName);
        //}

        public override void ChangeColumn(IColumnStructure oldcol, IColumnStructure newcol, IEnumerable<IConstraint> constraints)
        {
            if (oldcol.ColumnName != newcol.ColumnName) throw new Exception("DAE-00317 operation not supported");
            Put("^alter ^table %f ^alter ^column %i ", oldcol.Table, newcol.ColumnName);
            ColumnDefinition(newcol, false, true, true);
            EndCommand();
            this.CreateConstraints(constraints);
        }
    }
}
