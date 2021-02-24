using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.Data;
using System.Data.Common;
using System.Drawing;

namespace Plugin.mysql
{
    public abstract class MySqlDbObjectType : SpecificObjectTypeBase
    {
        public MySqlDbObjectType(ISqlDialect dialect)
            : base(dialect)
        {
        }
    }

    public class MySqlProcedureType : MySqlDbObjectType
    {
        public MySqlProcedureType(ISqlDialect dialect)
            : base(dialect)
        {
            CreateNewTemplate = SqlScripts.createprocedure;
        }

        public override bool SupportsLoadOverview
        {
            get { return true; }
        }

        public override DataTable LoadOverview(DbConnection conn, ObjectPath parpath)
        {
            conn.SafeChangeDatabase(parpath);
            return conn.LoadTableFromQuery("SHOW PROCEDURE STATUS WHERE Db='" + parpath.DbName + "'");
        }

        public override string ObjectType
        {
            get { return "procedure"; }
        }
    }

    public class MySqlFunctionType : MySqlDbObjectType
    {
        public MySqlFunctionType(ISqlDialect dialect)
            : base(dialect)
        {
            CreateNewTemplate = SqlScripts.createfunction;
        }

        public override bool SupportsLoadOverview
        {
            get { return true; }
        }

        public override DataTable LoadOverview(DbConnection conn, ObjectPath parpath)
        {
            DbConnectionExtension.SafeChangeDatabase(conn, parpath);
            return DbConnectionExtension.LoadTableFromQuery(conn, "SHOW FUNCTION STATUS WHERE Db='" + parpath.DbName + "'");
        }

        public override string ObjectType
        {
            get { return "function"; }
        }
    }

    public class MySqlEventType : MySqlDbObjectType
    {
        public MySqlEventType(ISqlDialect dialect)
            : base(dialect)
        {
            CreateNewTemplate = SqlScripts.createevent;
        }

        public override bool SupportsLoadOverview
        {
            get { return true; }
        }

        public override DataTable LoadOverview(DbConnection conn, ObjectPath parpath)
        {
            conn.SafeChangeDatabase(parpath);
            return DbConnectionExtension.LoadTableFromQuery(conn, "SHOW EVENTS");
        }

        public override string ObjectType
        {
            get { return "mysql.event"; }
        }
    }

    public class MySqlTriggerType : MySqlDbObjectType
    {
        public MySqlTriggerType(ISqlDialect dialect)
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
            DbConnectionExtension.SafeChangeDatabase(conn, parpath);
            return DbConnectionExtension.LoadTableFromQuery(conn, "SHOW triggers");
        }

        public override string ObjectType
        {
            get { return "trigger"; }
        }
    }

    public class MySqlEngineType : MySqlDbObjectType
    {
        public MySqlEngineType(ISqlDialect dialect)
            : base(dialect)
        {
        }

        public override bool SupportsLoadOverview
        {
            get { return true; }
        }

        public override DataTable LoadOverview(DbConnection conn, ObjectPath parpath)
        {
            DbConnectionExtension.SafeChangeDatabase(conn, parpath);
            return DbConnectionExtension.LoadTableFromQuery(conn, "SHOW ENGINES");
        }

        public override string ObjectType
        {
            get { return "mysql.engine"; }
        }
    }

    [SpecificRepresentation(Name = "mysqlengine", Title = "s_engine")]
    public class MySqlEngineRepresentation : SpecificObjectRepresentationBase
    {
        public override string TitlePlural
        {
            get { return "s_engines"; }
        }

        public override string TitleSingular
        {
            get { return "s_engine"; }
        }

        public override string ObjectType
        {
            get { return "mysql.engine"; }
        }

        public override bool ShowInTree
        {
            get { return false; }
        }

        public override bool UseInSynchronization
        {
            get { return false; }
        }
    }

    [SpecificRepresentation(Name = "mysqlevent", Title = "s_event")]
    public class MySqlEventRepresentation : SpecificObjectRepresentationBase
    {
        public override string TitlePlural
        {
            get { return "s_events"; }
        }

        public override string TitleSingular
        {
            get { return "s_event"; }
        }

        public override string ObjectType
        {
            get { return "mysql.event"; }
        }

        public override bool ShowInTree
        {
            get { return true; }
        }

        public override Bitmap Icon
        {
            get { return CoreIcons.clock; }
        }

        public override string XmlElementName
        {
            get { return "MySqlEvent"; }
        }
    }
}
