using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace DatAdmin
{
    public static class SqlDumperExtension
    {
        //public static void DropConstraint(this ISqlDumper fmt, IConstraint cnt, DropFlags flags)
        //{
        //    fmt.DropConstraint(cnt.Table, cnt.Name, cnt.SqlTypeName, flags);
        //}
        //public static void DropConstraint(this ISqlDumper fmt, IConstraint cnt)
        //{
        //    fmt.DropConstraint(cnt.Table, cnt.Name, cnt.SqlTypeName, DropFlags.None);
        //}
        public static void DropConstraints(this ISqlDumper fmt, IEnumerable constraints, DropFlags flags)
        {
            foreach (IConstraint cnt in constraints)
            {
                fmt.DropConstraint(cnt, flags);
            }
        }
        public static void Put(this ISqlDumper dmp, string format, params object[] args)
        {
            dmp.Stream.Write(SqlDumper.Format(dmp.Dialect, dmp.FormatProperties, dmp.FormatterState, format,  args));
        }
        public static void PutCmd(this ISqlDumper dmp, string format, params object[] args)
        {
            dmp.Put(format, args);
            dmp.Stream.EndCommand();
        }
        public static void WriteRaw(this ISqlDumper dmp, string data)
        {
            dmp.Stream.Write(data);
        }
        public static void EndCommand(this ISqlDumper dmp)
        {
            dmp.Stream.EndCommand();
        }
        public static void DropTable(this ISqlDumper dmp, ITableStructure table)
        {
            dmp.DropTable(table, DropFlags.None);
        }
        public static void DropSpecificObject(this ISqlDumper dmp, ISpecificObjectStructure obj)
        {
            dmp.DropSpecificObject(obj, DropFlags.None);
        }
        public static void CreateDatabaseObjects(this ISqlDumper dmp, IDatabaseStructure db)
        {
            dmp.CreateDatabaseObjects(db, new CreateDatabaseObjectsProps());
        }
    }
}
