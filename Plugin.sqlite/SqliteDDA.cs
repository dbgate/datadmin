using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;

namespace Plugin.sqlite
{
    public class SqliteDDA : DialectDataAdapterBase
    {
        public SqliteDDA(ISqlDialect dialect)
            : base(dialect)
        {
        }

        protected override void ApplyTypeRestrictions(BedValueHolder holder, DbTypeBase type, ILogger logger)
        {
            var htype = holder.GetFieldType();
            if (htype == TypeStorage.String) return; // skip string restrictions
            base.ApplyTypeRestrictions(holder, type, logger);
        }
    }
}
