using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace DatAdmin
{
    [ObjectFilter(Name = "appobject", Title = "s_any_object")]
    public class AppObjectFilter : ObjectFilterBase
    {
        [XmlSubElem]
        public StringObjectFilterItem ObjectType { get; set; }

        public AppObjectFilter()
        {
            ObjectType = new StringObjectFilterItem { PropertyName = "objtype", PropertyTitle = "s_object_type" };
        }

        public override void GetItems(List<ObjectFilterItemBase> items)
        {
            base.GetItems(items);
            items.Add(ObjectType);
        }
    }

    [ObjectFilter(Name = "server", Title = "s_server")]
    public class ServerObjectFilter : AppObjectFilter
    {
        [XmlSubElem]
        public StringObjectFilterItem ServerName { get; set; }

        [XmlSubElem]
        public DialectObjectFilterItem DialectName { get; set; }

        public ServerObjectFilter()
        {
            ServerName = new StringObjectFilterItem { PropertyName = "server", PropertyTitle = "s_server" };
            DialectName = new DialectObjectFilterItem { PropertyName = "dialect", PropertyTitle = "s_dialect" };
        }

        public override void GetItems(List<ObjectFilterItemBase> items)
        {
            base.GetItems(items);
            items.Add(ServerName);
            items.Add(DialectName);
        }
    }

    [ObjectFilter(Name = "database", Title = "s_database")]
    public class DatabaseObjectFilter : ServerObjectFilter
    {
        [XmlSubElem]
        public StringObjectFilterItem DatabaseName { get; set; }

        public DatabaseObjectFilter()
        {
            DatabaseName = new StringObjectFilterItem { PropertyName = "database", PropertyTitle = "s_database" };
        }

        public override void GetItems(List<ObjectFilterItemBase> items)
        {
            base.GetItems(items);
            items.Add(DatabaseName);
        }
    }

    [ObjectFilter(Name = "dbobject", Title = "s_database_object")]
    public class DbObjectObjectFilter : DatabaseObjectFilter
    {
        [XmlSubElem]
        public StringObjectFilterItem DbObjectSchema { get; set; }

        [XmlSubElem]
        public StringObjectFilterItem DbObjectName { get; set; }

        [XmlSubElem]
        public StringObjectFilterItem DbObjectType { get; set; }

        public DbObjectObjectFilter()
        {
            DbObjectType = new StringObjectFilterItem { PropertyName = "dbobjtype", PropertyTitle = "s_db_object_type" };
            DbObjectSchema = new StringObjectFilterItem { PropertyName = "dbobjschema", PropertyTitle = "s_schema" };
            DbObjectName = new StringObjectFilterItem { PropertyName = "dbobjname", PropertyTitle = "s_name" };
        }

        public override void GetItems(List<ObjectFilterItemBase> items)
        {
            base.GetItems(items);
            items.Add(DbObjectType);
            items.Add(DbObjectSchema);
            items.Add(DbObjectName);
        }
    }

    [ObjectFilter(Name = "table", Title = "s_table")]
    public class TableObjectFilter : DatabaseObjectFilter
    {
        [XmlSubElem]
        public StringObjectFilterItem TableSchema { get; set; }

        [XmlSubElem]
        public StringObjectFilterItem TableName { get; set; }

        public TableObjectFilter()
        {
            TableSchema = new StringObjectFilterItem { PropertyName = "dbobjschema", PropertyTitle = "s_schema" };
            TableName = new StringObjectFilterItem { PropertyName = "dbobjname", PropertyTitle = "s_name" };
        }

        public override void GetItems(List<ObjectFilterItemBase> items)
        {
            base.GetItems(items);
            items.Add(TableSchema);
            items.Add(TableName);
        }
    }

    [ObjectFilter(Name = "table_with_columns", Title = "Table with columns")]
    public class TableWithColumnsObjectFilter : TableObjectFilter 
    {
        [XmlSubElem]
        public ColumnsObjectFilterItem Columns { get; set; }

        public TableWithColumnsObjectFilter()
        {
            Columns = new ColumnsObjectFilterItem { PropertyName = "columns", PropertyTitle = "s_columns" };
        }

        public override void GetItems(List<ObjectFilterItemBase> items)
        {
            base.GetItems(items);
            items.Add(Columns);
        }
    }
}
