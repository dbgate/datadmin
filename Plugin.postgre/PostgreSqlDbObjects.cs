using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.Data.Common;
using System.Data;

namespace Plugin.postgre
{
    public abstract class PostgreSqlDbObjectType : SpecificObjectTypeBase
    {
        public PostgreSqlDbObjectType(ISqlDialect dialect)
            : base(dialect)
        {
        }
    }

    public class PostgreSqlSequenceType : PostgreSqlDbObjectType
    {
        public PostgreSqlSequenceType(ISqlDialect dialect)
            : base(dialect)
        {
            CreateNewTemplate = SqlScripts.createsequence;
        }

        public override bool SupportsLoadOverview
        {
            get { return true; }
        }

        public override DataTable LoadOverview(DbConnection conn, ObjectPath parpath)
        {
            DbConnectionExtension.SafeChangeDatabase(conn, parpath);
            return DbConnectionExtension.LoadTableFromQuery(conn, SqlScripts.getsequences);
        }

        public override string ObjectType
        {
            get { return "sequence"; }
        }
    }
}
