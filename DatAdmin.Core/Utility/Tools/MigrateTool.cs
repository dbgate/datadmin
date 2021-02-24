using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    public static class MigrateTool
    {
        public static void RemoveNonPk1AutoIncrements(TableStructure table, IProgressInfo progress)
        {
            IPrimaryKey pk = table.FindConstraint<IPrimaryKey>();
            foreach (ColumnStructure col in table.Columns)
            {
                var type = col.DataType;
                if (type is DbTypeInt && ((DbTypeInt)type).Autoincrement)
                {
                    if (pk == null || pk.Columns.Count != 1 || pk.Columns[0].ColumnName != col.ColumnName)
                    {
                        type.SetAutoincrement(false);
                        progress.LogMessage("migrate", LogLevel.Warning, "Removed autoincrement flag, column {0}.{1} is not primary key", table.FullName, col.ColumnName);
                    }
                }
            }
        }
    }
}
