using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.Data.Common;
using System.Data;

namespace Plugin.mssql
{
    public abstract class MsSqlDbObjectType : SpecificObjectTypeBase
    {
        public MsSqlDbObjectType(ISqlDialect dialect)
            : base(dialect)
        {
        }
    }

    public class MsSqlViewType : ViewObjectType
    {
        public MsSqlViewType(ISqlDialect dialect)
            : base(dialect)
        {
        }
    }

    public abstract class MsSqlProgrammableType : MsSqlDbObjectType
    {
        public MsSqlProgrammableType(ISqlDialect dialect)
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

    public class MsSqlProcedureType : MsSqlProgrammableType
    {
        public MsSqlProcedureType(ISqlDialect dialect)
            : base(dialect)
        {
            CreateNewTemplate = StdScripts.createprocedure;
        }
        public override DataTable LoadOverview(DbConnection conn, ObjectPath parpath)
        {
            return conn.LoadTableFromQuery("SELECT * FROM sysobjects WHERE xtype in ('P')");
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

    public class MsSqlFunctionType : MsSqlProgrammableType
    {
        public MsSqlFunctionType(ISqlDialect dialect)
            : base(dialect)
        {
            CreateNewTemplate = StdScripts.createfunction;
        }
        public override DataTable LoadOverview(DbConnection conn, ObjectPath parpath)
        {
            return conn.LoadTableFromQuery("SELECT * FROM sysobjects WHERE xtype in ('FN', 'IF', 'TF')");
        }
        public override string ObjectType
        {
            get { return "function"; }
        }
    }

    public class MsSqlTriggerType : MsSqlDbObjectType
    {
        public MsSqlTriggerType(ISqlDialect dialect)
            : base(dialect)
        {
            CreateNewTemplate = StdScripts.createtrigger;
        }
        public override DataTable LoadOverview(DbConnection conn, ObjectPath parpath)
        {
            return DbConnectionExtension.LoadTableFromQuery(conn, StdScripts.gettriggers);
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
