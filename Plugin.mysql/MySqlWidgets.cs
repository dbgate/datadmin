using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;

namespace Plugin.mysql
{
    [Widget(Name = "mysql_variables", Title = "Variables", Category = "MySQL")]
    public class MySqlVariablesWidget : SqlScriptGridWidget
    {
        public MySqlVariablesWidget()
            : base("SHOW VARIABLES", "s_variables", CoreIcons.variable)
        {
        }
    }

    [Widget(Name = "mysql_processlist", Title = "Process list", Category = "MySQL")]
    public class MySqlProcesslistWidget : SqlScriptGridWidget
    {
        public MySqlProcesslistWidget()
            : base("SHOW FULL PROCESSLIST", "s_processes", CoreIcons.job)
        {
        }
    }

    [Widget(Name = "mysql_status", Title = "Status", Category = "MySQL")]
    public class MySqlStatusWidget : SqlScriptGridWidget
    {
        public MySqlStatusWidget()
            : base("SHOW STATUS", "s_status", CoreIcons.log)
        {
        }
    }

    [Widget(Name = "mysql_tables", Title = "Table list", Category = "MySQL")]
    public class MySqlTablesWidget : SqlScriptGridWidget
    {
        public MySqlTablesWidget()
            : base("SHOW TABLE STATUS", "s_tables", CoreIcons.table)
        {
        }
    }

    [Widget(Name = "mysql_users", Title = "User list", Category = "MySQL")]
    public class MySqlUsersWidget : SqlScriptGridWidget
    {
        public MySqlUsersWidget()
            : base("SELECT * FROM mysql.user", "s_users", CoreIcons.user)
        {
        }
    }

    [Widget(Name = "mysql_engines", Title = "Engine list", Category = "MySQL")]
    public class MySqlEnginesWidget : SqlScriptGridWidget
    {
        public MySqlEnginesWidget()
            : base("SHOW ENGINES", "s_engines", CoreIcons.system)
        {
        }
    }

    [Widget(Name = "mysql_tableddl", Title = "Table DDL", Category = "MySQL")]
    public class MySqlTableDDLWidget : SqlScriptSyntaxSqlWidget
    {
        public MySqlTableDDLWidget()
            : base(1, "DDL")
        {
        }

        protected override string GetSelect(ObjectPath path)
        {
            return "SHOW CREATE TABLE `" + path.ObjectName.Name + "`";
        }
    }

    [Widget(Name = "mysql_columns", Title = "Column list", Category = "MySQL")]
    public class MySqlColumnsWidget : SqlScriptGridWidget
    {
        public MySqlColumnsWidget()
            : base(null, "s_columns", CoreIcons.column)
        {
        }

        protected override string GetSelect(ObjectPath path)
        {
            return "SHOW COLUMNS FROM `" + path.ObjectName.Name + "`";
        }
    }

    [Widget(Name = "mysql_databases", Title = "Database list", Category = "MySQL")]
    public class MySqlDatabasesWidget : SqlScriptGridWidget
    {
        public MySqlDatabasesWidget()
            : base("SHOW DATABASES", "s_databases", CoreIcons.database)
        {
        }
    }
}
