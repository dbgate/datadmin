using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DatAdmin
{
    public static class DatabaseStructureExtension
    {
        public static DatabaseStructure GetMappedDatabase(this IDatabaseStructure db, DatabaseCopyOptions copyOpts, ISqlDialect dialect)
        {
            DatabaseStructure res = new DatabaseStructure(db, copyOpts.CopyMembers);
            foreach (TableStructure tbl in res.Tables)
            {
                tbl.RemoveInvalidReferences(copyOpts, dialect);
                //tbl.MangleMappedTable(copyOpts, dialect);
            }
            if (copyOpts.SchemaMode == DbCopySchemaMode.Explicit)
            {
                // no schemata will be created
                res.Schemata.Clear();
                res.RunNameTransformation(new SetSchemaNameTransformation(copyOpts.ExplicitSchema));
            }
            return res;
        }

        public static IEnumerable<ISpecificObjectStructure> GetAllSpecificObjects(this IDatabaseStructure db)
        {
            foreach (string objtype in db.SpecificObjects.Keys)
            {
                foreach (SpecificObjectStructure spec in db.SpecificObjects[objtype])
                {
                    yield return spec;
                }
            }
        }

        public static IEnumerable<ISpecificObjectStructure> GetSpecObjectsOrderByDependency(this IDatabaseStructure db)
        {
            List<ISpecificObjectStructure> all = new List<ISpecificObjectStructure>();
            all.AddRange(db.GetAllSpecificObjects());
            while (all.Count > 0)
            {
                ISpecificObjectStructure selected = null;
                foreach (var obj in all)
                {
                    bool objIndependent = true;
                    var deps = obj.DependsOn;
                    if (deps == null && db.Dialect != null) deps = db.Dialect.DetectDependencies(obj);
                    if (deps != null)
                    {
                        foreach (var dep in deps)
                        {
                            if (DepInList(dep, all))
                            {
                                objIndependent = false;
                                break;
                            }
                        }
                    }
                    if (objIndependent)
                    {
                        selected = obj;
                        break;
                    }
                }
                if (selected == null)
                {
                    // throw new Exception("Object dependency cycle");
                    selected = all[0];
                }
                yield return selected;
                all.Remove(selected);
            }
        }

        private static bool DepInList(DependencyItem dep, List<ISpecificObjectStructure> objs)
        {
            foreach (var obj in objs)
            {
                if (obj.ObjectType == dep.ObjectType && obj.ObjectName == dep.Name) return true;
            }
            return false;
        }

        public static ITableStructure InvokeLoadTableStructure(this IDatabaseSource conn, NameWithSchema name, TableStructureMembers members)
        {
            DatabaseStructureMembers dbmem = new DatabaseStructureMembers
            {
                TableFilter = new List<NameWithSchema> { name },
                TableMembers = members
            };
            IDatabaseStructure dbs = conn.InvokeLoadStructure(dbmem, null);
            return dbs.Tables[name];
        }

        public static ITableStructure LoadTableStructure(this IDatabaseSource conn, NameWithSchema name, TableStructureMembers members)
        {
            DatabaseStructureMembers dbmem = new DatabaseStructureMembers
            {
                TableFilter = new List<NameWithSchema> { name },
                TableMembers = members
            };
            IDatabaseStructure dbs = conn.InvokeLoadStructure(dbmem, null);
            return dbs.Tables[name];
        }

        public static ITableStructure InvokeLoadViewStructure(this IDatabaseSource conn, NameWithSchema name)
        {
            DatabaseStructureMembers dbmem = new DatabaseStructureMembers
            {
                ViewAsTables = true,
                ViewAsTableFilter = new List<NameWithSchema> { name },
            };
            IDatabaseStructure dbs = conn.InvokeLoadStructure(dbmem, null);
            return dbs.ViewAsTables[name];
        }

        public static List<IAbstractObjectStructure> GetAllObjects(this IAbstractObjectStructure obj)
        {
            var res = new List<IAbstractObjectStructure>();
            obj.AddAllObjects(res);
            return res;
        }

        public static IAbstractObjectStructure FindByGroupId(this IDatabaseStructure db, string groupid)
        {
            foreach (var obj in db.GetAllObjects())
            {
                if (obj.GroupId == groupid) return obj;
            }
            return null;
        }

        public static T FindByGroupId<T>(this IDatabaseStructure db, T obj) where T : IAbstractObjectStructure
        {
            return (T)db.FindByGroupId(obj.GroupId);
        }

        public static ITableStructure FindTable(this IDatabaseStructure db, NameWithSchema name)
        {
            return (ITableStructure)(from t in db.Tables where t.FullName == name select t).FirstOrDefault();
        }

        public static ITableStructure FindSimilarTable(this IDatabaseStructure db, NameWithSchema name)
        {
            var res = db.FindTable(name);
            if (res != null) return res;
            return (ITableStructure)(from t in db.Tables where t.FullName.Name.ToLower() == name.Name.ToLower() select t).FirstOrDefault();
        }

        public static IDomainStructure FindDomain(this IDatabaseStructure db, NameWithSchema name)
        {
            return (IDomainStructure)(from d in db.Domains where d.FullName == name select d).FirstOrDefault();
        }

        public static ISchemaStructure FindSchema(this IDatabaseStructure db, string name)
        {
            return (ISchemaStructure)(from d in db.Schemata where d.SchemaName == name select d).FirstOrDefault();
        }

        public static IConstraint FindConstraint(this IDatabaseStructure db, IConstraint constraint)
        {
            return db.FindTable(constraint.Table.FullName).Constraints.First(c => c.Name == constraint.Name && c.Type == constraint.Type);
        }

        public static IColumnStructure FindColumn(this IDatabaseStructure db, IColumnStructure column)
        {
            return (IColumnStructure)db.FindTable(column.Table.FullName).Columns.First(c => c.ColumnName == column.ColumnName);
        }

        public static ISpecificObjectStructure FindSpecificObject(this IDatabaseStructure db, string objtype, NameWithSchema name)
        {
            if (!db.SpecificObjects.ContainsKey(objtype)) return null;
            return (ISpecificObjectStructure)(from i in db.SpecificObjects[objtype] where i.ObjectName == name select i).FirstOrDefault();
        }

        public static ISpecificObjectStructure FindSpecificObject(this IDatabaseStructure db, ISpecificObjectStructure obj)
        {
            return db.FindSpecificObject(obj.ObjectType, obj.ObjectName);
        }

        public static DependencyItem GetDepencencyItem(this ISpecificObjectStructure obj)
        {
            return new DependencyItem
            {
                Name = obj.ObjectName,
                ObjectType = obj.ObjectType,
            };
        }

        public static List<DependencyItem> BuildDependencyList(this ISpecificObjectStructure spec, DepsCollector dc)
        {
            var res = new List<DependencyItem>();
            foreach (var item in dc.Names)
            {
                foreach (var obj in spec.Database.GetAllDepItems())
                {
                    if (res.Contains(obj)) continue;
                    if (item.Match(obj) && !item.Match(spec.GetDepencencyItem())) res.Add(obj);
                }
            }
            return res;
        }

        public static IEnumerable<DependencyItem> GetAllDepItems(this IDatabaseStructure db)
        {
            foreach (var tbl in db.Tables)
            {
                yield return new DependencyItem
                {
                    Name = tbl.FullName,
                    ObjectType = "table"
                };
            }
            foreach (var spec in db.GetAllSpecificObjects())
            {
                yield return new DependencyItem
                {
                    Name = spec.ObjectName,
                    ObjectType = spec.ObjectType,
                };
            }
        }

        public static DependencyItem[] GetSelectedDependsOn(this IDatabaseStructure db, DependencyItem selected, bool processReferences)
        {
            var res = new List<DependencyItem>();
            if (selected != null)
            {
                var obj = db.FindSpecificObject(selected.ObjectType, selected.Name);
                if (obj != null && obj.DependsOn != null) res.AddRange(obj.DependsOn);
                if (processReferences && selected.ObjectType == "table")
                {
                    var tbl = db.FindTable(selected.Name);
                    if (tbl != null)
                    {
                        foreach (var fk in tbl.GetConstraints<IForeignKey>())
                        {
                            res.Add(new DependencyItem
                            {
                                Name = fk.PrimaryKeyTable,
                                ObjectType = "table",
                            });
                        }
                    }
                }
            }
            return res.ToArray();
        }

        public static DependencyItem[] GetDependsOnSelected(this IDatabaseStructure db, DependencyItem selected, bool processReferences)
        {
            var res = new List<DependencyItem>();
            if (selected != null)
            {
                foreach (var spec in db.GetAllSpecificObjects())
                {
                    if (spec.DependsOn != null && spec.DependsOn.Contains(selected))
                    {
                        res.Add(new DependencyItem
                        {
                            Name = spec.ObjectName,
                            ObjectType = spec.ObjectType,
                        });
                    }
                }
                if (processReferences && selected.ObjectType == "table")
                {
                    var tbl = db.FindTable(selected.Name);
                    if (tbl != null)
                    {
                        foreach (var fk in tbl.GetReferencedFrom())
                        {
                            res.Add(new DependencyItem
                            {
                                Name = fk.Table.FullName,
                                ObjectType = "table",
                            });
                        }
                    }
                }
            }
            return res.ToArray();
        }


        //public static T Freeze<T>(this T obj) where T : IAbstractObjectStructure
        //{
        //    return (T)obj.FreezeUntyped();
        //}

        //public static T Clone<T>(this T obj) where T : IAbstractObjectStructure
        //{
        //    return (T)obj.CloneUntyped();
        //}
    }
}
