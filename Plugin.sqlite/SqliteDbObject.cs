using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.Data.Common;
using System.Data;

namespace Plugin.sqlite
{
    public abstract class SqliteDbObjectType : SpecificObjectTypeBase
    {
        public SqliteDbObjectType(ISqlDialect dialect)
            : base(dialect)
        {
        }
    }

    public class SqliteTriggerType : SqliteDbObjectType
    {
        public SqliteTriggerType(ISqlDialect dialect)
            : base(dialect)
        {
            CreateNewTemplate = SqlScripts.createtrigger;
        }

        public override bool SupportsLoadOverview
        {
            get { return true; }
        }

        public override DataTable LoadOverview(DbConnection conn, ObjectPath parpath)
        {
            conn.SafeChangeDatabase(parpath);
            return conn.GetSchema("Triggers");
        }

        public override string ObjectType
        {
            get { return "trigger"; }
        }
    }
}
