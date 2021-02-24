using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.ComponentModel;

namespace Plugin.versiondb
{
    [DbModelTransform(Name = "remove_constraints", Title = "s_remove_constraints")]
    public class RemoveConstraintTransform : DbModelTransformBase
    {
        [XmlElem]
        [TypeConverter(typeof(YesNoTypeConverter))]
        public bool RemoveForeignKeys { get; set; }

        [XmlElem]
        [TypeConverter(typeof(YesNoTypeConverter))]
        public bool RemovePrimaryKeys { get; set; }

        [XmlElem]
        [TypeConverter(typeof(YesNoTypeConverter))]
        public bool RemoveIndexes { get; set; }

        [XmlElem]
        [TypeConverter(typeof(YesNoTypeConverter))]
        public bool RemoveChecks { get; set; }

        [XmlElem]
        [TypeConverter(typeof(YesNoTypeConverter))]
        public bool RemoveUniques { get; set; }

        [XmlElem]
        [TypeConverter(typeof(YesNoTypeConverter))]
        public bool RemoveAutoIncrements { get; set; }

        public override void RunTransform(DatabaseStructure model)
        {
            foreach (TableStructure tbl in model.Tables)
            {
                if (RemoveForeignKeys) tbl._Constraints.RemoveIf(c => c is IForeignKey);
                if (RemovePrimaryKeys) tbl._Constraints.RemoveIf(c => c is IPrimaryKey);
                if (RemoveIndexes) tbl._Constraints.RemoveIf(c => c is IIndex);
                if (RemoveUniques) tbl._Constraints.RemoveIf(c => c is IUnique);
                if (RemoveAutoIncrements)
                {
                    foreach (var c in tbl._Columns)
                    {
                        c.DataType.SetAutoincrement(false);
                    }
                }
            }
        }
    }
}
