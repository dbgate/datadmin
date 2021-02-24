using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Collections;
using System.Data.Common;

namespace DatAdmin
{
    public static class DatabaseSourceExtension
    {
        public static IEnumerable<NameWithSchema> InvokeLoadFullTableNames(this IDatabaseSource db, bool skipSystemTables)
        {
            IDatabaseStructure cat = db.InvokeLoadStructure(new DatabaseStructureMembers { TableList = true }, null);
            foreach (ITableStructure t in cat.Tables)
            {
                if (skipSystemTables && db.Dialect != null && db.Dialect.IsSystemTable(t.FullName.Schema, t.FullName.Name))
                {
                    continue;
                }
                yield return t.FullName;
            }
        }

        public static IEnumerable<NameWithSchema> InvokeLoadFullTableNames(this IDatabaseSource db)
        {
            return db.InvokeLoadFullTableNames(false);
        }

        public static IEnumerable<NameWithSchema> LoadFullTableNames(this IDatabaseSource db, bool skipSystemTables)
        {
            IDatabaseStructure cat = db.LoadDatabaseStructure(new DatabaseStructureMembers { TableList = true }, null);
            foreach (ITableStructure t in cat.Tables)
            {
                if (skipSystemTables && db.Dialect != null && db.Dialect.IsSystemTable(t.FullName.Schema, t.FullName.Name))
                {
                    continue;
                }
                yield return t.FullName;
            }
        }

        public static IEnumerable<string> InvokeLoadCharacterSets(this IDatabaseSource db)
        {
            IDatabaseStructure cat = db.InvokeLoadStructure(new DatabaseStructureMembers { CharacterSetList = true }, null);
            foreach (var c in cat.CharacterSets)
            {
                yield return c.Name;
            }
        }

        public static IEnumerable<string> InvokeLoadCollations(this IDatabaseSource db)
        {
            IDatabaseStructure cat = db.InvokeLoadStructure(new DatabaseStructureMembers { CollationList = true }, null);
            foreach (var c in cat.Collations)
            {
                yield return c.Name;
            }
        }

        public static IEnumerable<string> InvokeLoadSchemata(this IDatabaseSource db)
        {
            IDatabaseStructure cat = db.InvokeLoadStructure(new DatabaseStructureMembers { SchemaList = true }, null);
            foreach (var s in cat.Schemata.Sorted())
            {
                yield return s.SchemaName;
            }
        }

        public static IDatabaseStructure InvokeLoadStructure(this IDatabaseSource db, DatabaseStructureMembers members, IProgressInfo progress)
        {
            if (db.Connection == null) return db.LoadDatabaseStructure(members, progress);
            IAsyncResult async = db.Connection.BeginInvoke(
                (Func<IDatabaseStructure>)delegate() { return db.LoadDatabaseStructure(members, progress); },
                null);
            Async.WaitFor(async);
            return (IDatabaseStructure)db.Connection.EndInvoke(async);
        }

        //public static IEnumerable<NameWithSchema> GetSpecificObjectList(this IDatabaseSource db, string type, ObjectPath parent)
        //{
        //    if (parent.ObjectName != null) throw new Exception("Only database objects are supported");
        //    return db.GetSpecificObjectList(type, parent.DbName);
        //}

        public static IEnumerable<NameWithSchema> LoadSpecificObjectList(this IDatabaseSource db, string type, bool isSystem)
        {
            DatabaseStructureMembers dbmem = new DatabaseStructureMembers();
            dbmem.SpecificObjectOverride[type] = new SpecificObjectMembers();
            dbmem.SpecificObjectOverride[type].ObjectList = true;
            IDatabaseStructure dbs = db.LoadDatabaseStructure(dbmem, null);
            if (!dbs.SpecificObjects.ContainsKey(type)) yield break;
            foreach (var obj in dbs.SpecificObjects[type])
            {
                bool objsys;
                try { objsys = db.Dialect.IsSystemObject(type, obj.ObjectName); }
                catch { objsys = false; }
                if (objsys != isSystem) continue;
                yield return obj.ObjectName;
            }
        }

        public static ISpecificObjectStructure LoadSpecificObjectDetail(this IDatabaseSource db, string type, NameWithSchema objname)
        {
            DatabaseStructureMembers dbmem = new DatabaseStructureMembers();
            dbmem.SpecificObjectOverride[type] = new SpecificObjectMembers();
            dbmem.SpecificObjectOverride[type].ObjectDetail = true;
            dbmem.SpecificObjectOverride[type].ObjectFilter = new List<NameWithSchema> { objname };
            IDatabaseStructure dbs = db.LoadDatabaseStructure(dbmem, null);
            return dbs.SpecificObjects[type][objname];
        }

        public static ISpecificObjectStructure InvokeLoadSpecificObjectDetail(this IDatabaseSource db, string type, NameWithSchema objname)
        {
            if (db.Connection == null) return db.LoadSpecificObjectDetail(type, objname);
            return db.Connection.InvokeR(() => db.LoadSpecificObjectDetail(type, objname));
        }

        //public static ITableSource GetTable(this IDatabaseSource dconn, NameWithSchema table)
        //{
        //    return dconn.GetTable(null, table.Schema, table.Name);
        //}

        public static void InvokeScript(this IDatabaseSource db, Action<ISqlDumper> script)
        {
            db.InvokeScript(script, null, false);
        }

        public static void InvokeScript(this IDatabaseSource db, Action<ISqlDumper> script, IProgressInfo progress)
        {
            db.InvokeScript(script, progress, false);
        }

        public static void InvokeScript(this IDatabaseSource db, Action<ISqlDumper> script, IProgressInfo progress, bool denyTransaction)
        {
            db.Connection.Invoke(
             (Action)delegate()
             {
                 db.Connection.SystemConnection.SafeChangeDatabase(db.DatabaseName);
                 DbTransaction tran = null;
                 if (db.Dialect.DialectCaps.NestedTransactions && !denyTransaction) tran = db.Connection.SystemConnection.BeginTransaction();
                 try
                 {
                     db.Connection.RunScript(script, tran, progress);
                 }
                 catch
                 {
                     if (tran != null) tran.Rollback();
                     throw;
                 }
                 if (tran != null) tran.Commit();
             });
        }

        public static void RunScript(this IDatabaseSource db, Action<ISqlDumper> script)
        {
            db.Connection.SystemConnection.SafeChangeDatabase(db.DatabaseName);
            db.Connection.RunScript(script);
        }

        public static List<IAbstractObjectStructure> GetStructureList_NonEffective(this IDatabaseSource db, Func<IDatabaseStructure, IEnumerable> extractCollection)
        {
            List<IAbstractObjectStructure> res = new List<IAbstractObjectStructure>();
            IDatabaseStructure dbs;
            if (db.Connection == null) dbs = db.LoadDatabaseStructure(DatabaseStructureMembers.FullStructure, null);
            else dbs = db.InvokeLoadStructure(DatabaseStructureMembers.FullStructure, null);
            var col = extractCollection(dbs);
            if (col == null) return res;
            foreach (IAbstractObjectStructure obj in col)
            {
                res.Add(obj);
            }
            return res;
        }

        public static void AlterDatabase(this IDatabaseSource conn, IDatabaseStructure src, IDatabaseStructure dst)
        {
            conn.AlterDatabase(src, dst, new DbDiffOptions());
        }

        public static void ChangeObject(this IDatabaseSource conn, IAbstractObjectStructure oldObj, Action<AbstractObjectStructure> changeFunc)
        {
            DatabaseStructure oldDb = new DatabaseStructure();
            oldDb.AddObject(oldObj, true);
            DatabaseStructure newDb = new DatabaseStructure(oldDb);
            AbstractObjectStructure newObj = (AbstractObjectStructure)newDb.FindByGroupId(oldObj);
            changeFunc(newObj);
            conn.AlterDatabase(oldDb, newDb, DbDiffOptions.AlterStructureOptions());
        }

        public static void RenameObject(this IDatabaseSource conn, IAbstractObjectStructure oldObj, string newname)
        {
            conn.ChangeObject(oldObj, obj => obj.RenameObject(newname));
        }

        public static void ChangeObjectSchema(this IDatabaseSource conn, IAbstractObjectStructure oldObj, string newschema)
        {
            conn.ChangeObject(oldObj, obj => obj.SetObjectSchema(newschema));
        }

        public static void DropObject(this IDatabaseSource conn, IAbstractObjectStructure obj)
        {
            DatabaseStructure oldDb = new DatabaseStructure();
            oldDb.AddObject(obj, true);
            DatabaseStructure newDb = new DatabaseStructure(oldDb);
            newDb.DropObject(obj);
            conn.AlterDatabase(oldDb, newDb, DbDiffOptions.AlterStructureOptions());
        }

        public static void CreateObject(this IDatabaseSource conn, IAbstractObjectStructure obj)
        {
            DatabaseStructure oldDb = new DatabaseStructure();
            DatabaseStructure newDb = new DatabaseStructure(oldDb);
            newDb.AddObject(obj, true);
            conn.AlterDatabase(oldDb, newDb);
        }

        public static void AlterObject(this IDatabaseSource conn, IAbstractObjectStructure oldObj, IAbstractObjectStructure newObj)
        {
            conn.AlterObject(oldObj, newObj, new DbDiffOptions());
        }

        public static void AlterObject(this IDatabaseSource conn, IAbstractObjectStructure oldObj, IAbstractObjectStructure newObj, DbDiffOptions options)
        {
            if (oldObj == null)
            {
                conn.CreateObject(newObj);
                return;
            }
            if (newObj == null)
            {
                conn.DropObject(oldObj);
                return;
            }
            if (oldObj.GroupId != newObj.GroupId) throw new InternalError("DAE-00010 Altering object with different groupid");
            DatabaseStructure oldDb = new DatabaseStructure();
            DatabaseStructure newDb = new DatabaseStructure(oldDb);
            oldDb.AddObject(oldObj, true);
            newDb.AddObject(newObj, true);
            conn.AlterDatabase(oldDb, newDb, options);
        }

        public static ITableSource CreateTable(this IDatabaseSource db, ITableStructure table)
        {
            db.CreateObject(table);
            return db.GetTable(table.FullName);
        }

        public static string ExtractRepresentativeName(this IDatabaseTreeNode node)
        {
            var db = node.DatabaseConnection;
            string dbname = null;
            if (db != null) dbname = db.DatabaseName;
            if (dbname == null && db.Connection != null && db.Connection.StoredConnection != null)
            {
                dbname = db.Connection.StoredConnection.ExplicitDatabaseName;
                if (dbname == null)
                {
                    try
                    {
                        dbname = System.IO.Path.GetFileNameWithoutExtension(node.Title);
                    }
                    catch { }
                }
            }
            return dbname;
        }

        public static ISqlDialect GetAnyDialect(this IDatabaseSource conn)
        {
            if (conn.Dialect != null) return conn.Dialect;
            return GenericDialect.Instance;
        }

        public static IDialectDataAdapter GetAnyDDA(this IDatabaseSource conn)
        {
            return conn.GetAnyDialect().CreateDataAdapter();
        }

        public static DatabaseCache GetCache(this IDatabaseSource src)
        {
            if (src == null) return null;
            if (src.Connection == null) return null;
            var cache = src.Connection.Cache;
            if (cache == null) return null;
            return cache.Database(src.DatabaseName);
        }
    }
}
