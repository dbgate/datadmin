using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    public class InfoSqlDumper : SqlDumper
    {
        public InfoSqlDumper(ISqlOutputStream fw, ISqlDialect dialect, SqlFormatProperties props)
            : base(fw, dialect, props)
        {
        }
        public override void CreateSpecificObject(ISpecificObjectStructure obj)
        {
            WriteRaw(obj.CreateSql);
            EndCommand();
        }
    }
}
