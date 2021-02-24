using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.Data;
using System.Data.Common;

namespace Plugin.effiproz
{
    public abstract class EfzDbObjectType : SpecificObjectTypeBase
    {
        public EfzDbObjectType(ISqlDialect dialect)
            : base(dialect)
        {
        }
    }

    public class EfzViewType : ViewObjectType
    {
        public EfzViewType(ISqlDialect dialect)
            : base(dialect)
        {
        }
    }

    public abstract class EfzProgrammableType : EfzDbObjectType
    {
        public EfzProgrammableType(ISqlDialect dialect)
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

    public class EfzProcedureType : EfzProgrammableType
    {
        public EfzProcedureType(ISqlDialect dialect)
            : base(dialect)
        {
            CreateNewTemplate = StdScripts.createprocedure;
        }
        public override DataTable LoadOverview(DbConnection conn, ObjectPath parpath)
        {
            return conn.LoadTableFromQuery("SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE='PROCEDURE'");
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

    public class EfzFunctionType : EfzProgrammableType
    {
        public EfzFunctionType(ISqlDialect dialect)
            : base(dialect)
        {
            CreateNewTemplate = StdScripts.createfunction;
        }
        public override DataTable LoadOverview(DbConnection conn, ObjectPath parpath)
        {
            return conn.LoadTableFromQuery("SELECT * FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE='FUNCTION'");
        }
        public override string ObjectType
        {
            get { return "function"; }
        }
    }

    public class EfzTriggerType : EfzProgrammableType
    {
        public EfzTriggerType(ISqlDialect dialect)
            : base(dialect)
        {
            CreateNewTemplate = StdScripts.createtrigger;
        }
        public override DataTable LoadOverview(DbConnection conn, ObjectPath parpath)
        {
            return DbConnectionExtension.LoadTableFromQuery(conn, "SELECT * FROM INFORMATION_SCHEMA.TRIGGERS");
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
}
