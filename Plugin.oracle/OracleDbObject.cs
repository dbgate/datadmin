using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.Data;
using System.Data.Common;

namespace Plugin.oracle
{
    public abstract class OracleDbObjectType : SpecificObjectTypeBase
    {
        public OracleDbObjectType(ISqlDialect dialect)
            : base(dialect)
        {
        }
    }

    public class OracleViewType : ViewObjectType
    {
        public OracleViewType(ISqlDialect dialect)
            : base(dialect)
        {
        }
        public override DataTable LoadOverview(DbConnection conn, ObjectPath parpath)
        {
            return conn.LoadTableFromQuery("select * from USER_VIEWS");
        }
    }

    public abstract class OracleProgrammableType : OracleDbObjectType
    {
        public OracleProgrammableType(ISqlDialect dialect)
            : base(dialect)
        {
        }
        public override DbObjectParent ParentType
        {
            get { return DbObjectParent.Database; }
        }
        public override bool SupportsLoadOverview
        {
            get { return true; }
        }
    }

    public class OracleProcedureType : OracleProgrammableType
    {
        public OracleProcedureType(ISqlDialect dialect)
            : base(dialect)
        {
            CreateNewTemplate = SqlScripts.createprocedure;
        }
        public override DataTable LoadOverview(DbConnection conn, ObjectPath parpath)
        {
            return conn.LoadTableFromQuery("select * from USER_PROCEDURES");
        }
        public override bool HasSystemVariant
        {
            get { return true; }
        }
        public override string ObjectType
        {
            get { return "procedure"; }
        }
    }

    public class OracleFunctionType : OracleProgrammableType
    {
        public OracleFunctionType(ISqlDialect dialect)
            : base(dialect)
        {
            CreateNewTemplate = SqlScripts.createfunction;
        }
        public override DataTable LoadOverview(DbConnection conn, ObjectPath parpath)
        {
            return conn.LoadTableFromQuery("select * from USER_OBJECTS WHERE OBJECT_TYPE='FUNCTION'");
        }
        public override string ObjectType
        {
            get { return "function"; }
        }
    }

    public class OracleTriggerType : OracleProgrammableType
    {
        public OracleTriggerType(ISqlDialect dialect)
            : base(dialect)
        {
            CreateNewTemplate = SqlScripts.createtrigger;
        }
        public override DataTable LoadOverview(DbConnection conn, ObjectPath parpath)
        {
            return DbConnectionExtension.LoadTableFromQuery(conn, "select * from USER_TRIGGERS");
        }
        public override string ObjectType
        {
            get { return "trigger"; }
        }
        public override bool SupportsLoadOverview
        {
            get { return true; }
        }
    }

    public class OracleSequenceType : OracleDbObjectType 
    {
        public OracleSequenceType(ISqlDialect dialect)
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
            return DbConnectionExtension.LoadTableFromQuery(conn, "select * from USER_SEQUENCES");
        }

        public override string ObjectType
        {
            get { return "sequence"; }
        }
    }
}
