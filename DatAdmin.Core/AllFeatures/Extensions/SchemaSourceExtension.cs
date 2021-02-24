using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    //public static class SchemaSourceExtension
    //{
    //    public static List<string> LoadTableNames(ISchemaSource schema)
    //    {
    //        CatalogOverview catalog = schema.Database.LoadCatalogOverview();
    //        List<string> res = new List<string>();
    //        foreach (TableOverview tbl in catalog.SchemaByName[schema.SchemaName])
    //        {
    //            res.Add(tbl.TableName);
    //        }
    //        res.Sort();
    //        return res;
    //    }

    //    public static ITableSource LoadTable(ISchemaSource schema, string table)
    //    {
    //        return schema.Database.GetTable(schema.CatalogName, schema.SchemaName, table);
    //    }
    //}
}
