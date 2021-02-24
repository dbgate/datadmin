using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;

namespace Plugin.mysql
{
    partial class MySqlParser
    {
        protected override void ReadColumnSpecific(ColumnStructure col)
        {
            while (!IsSymbol(")") && !IsSymbol(","))
            {
                if (SkipTokenIf("auto_increment")) col.DataType.SetAutoincrement(true);
                else if (SkipMultiIf("primary", "key"))
                {
                    var pk = new PrimaryKey();
                    pk.Columns.Add(new ColumnReference(col.ColumnName));
                    ((TableStructure)col.Table)._Constraints.Add(pk);
                }
                else if (SkipTokenIf("references"))
                {
                    var fk = new ForeignKey();
                    fk.Columns.Add(new ColumnReference(col.ColumnName));
                    ((TableStructure)col.Table)._Constraints.Add(fk);
                    ReadReferencesClause(fk);
                }
            }
        }
    }
}
