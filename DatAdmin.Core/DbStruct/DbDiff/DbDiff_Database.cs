using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    public static partial class DbDiffTool
    {
        public static void AlterDatabase(AlterPlan plan, DbObjectPairing pairing, DbDiffOptions opts)
        {
            var src = pairing.Source;
            var dst = pairing.Target;
            //var caps = proc.AlterCaps;

            // domains
            foreach (IDomainStructure dsrc in src.Domains)
            {
                IDomainStructure ddst = pairing.FindPair(dsrc);
                if (ddst == null)
                {
                    plan.DropDomain(dsrc);
                }
                else if (!DbDiffTool.EqualDomains(dsrc, ddst, opts, true))
                {
                    if (DbDiffTool.EqualDomains(dsrc, ddst, opts, false))
                    {
                        plan.RenameDomain(dsrc, ddst.FullName);
                    }
                    else
                    {
                        plan.ChangeDomain(dsrc, ddst);
                    }
                }
            }

            foreach (IDomainStructure ddst in dst.Domains)
            {
                if (!pairing.IsPaired(ddst)) plan.CreateDomain(ddst);
            }

            // drop tables
            foreach (ITableStructure tsrc in new List<ITableStructure>(src.Tables))
            {
                ITableStructure tdst = pairing.FindPair(tsrc);
                if (tdst == null)
                {
                    plan.DropTable(tsrc);
                }
            }
            // change tables
            foreach (ITableStructure tsrc in new List<ITableStructure>(src.Tables))
            {
                ITableStructure tdst = pairing.FindPair(tsrc);
                if (tdst == null) continue;
                if (!DbDiffTool.EqualTables(tsrc, tdst, opts, pairing))
                {
                    DbDiffTool.AlterTable(plan, tsrc, tdst, opts, pairing);
                }
                else
                {
                    DbDiffTool.AlterFixedData(plan, tsrc, tdst, opts);
                }
            }
            // create tables
            foreach (ITableStructure tdst in dst.Tables)
            {
                if (!pairing.IsPaired(tdst))
                {
                    var script = DbDiffTool.AlterFixedData(null, tdst.FixedData, null, opts);
                    plan.CreateTable(tdst, script);
                    //if (script != null) plan.UpdateData(tdst.FullName, script);
                }
            }

            // specific objects
            foreach (ISpecificObjectStructure osrc in src.GetAllSpecificObjects())
            {
                var repr = SpecificRepresentationAddonType.Instance.FindRepresentation(osrc.ObjectType);
                if (!repr.UseInSynchronization) continue;

                ISpecificObjectStructure odst = pairing.FindPair(osrc);
                if (odst == null)
                {
                    plan.DropSpecificObject(osrc);
                    //proc.DropSpecificObject(osrc);
                }
                else if (!DbDiffTool.EqualsSpecificObjects(osrc, odst, opts))
                {
                    DbDiffTool.AlterSpecificObject(osrc, odst, plan, opts, pairing);
                }
            }
            foreach (ISpecificObjectStructure odst in dst.GetAllSpecificObjects())
            {
                var repr = SpecificRepresentationAddonType.Instance.FindRepresentation(odst.ObjectType);
                if (!repr.UseInSynchronization) continue;

                if (!pairing.IsPaired(odst))
                {
                    plan.CreateSpecificObject(odst);
                    //proc.CreateSpecificObject(odst);
                }
            }

            foreach (ISchemaStructure ssrc in src.Schemata)
            {
                ISchemaStructure sdst = pairing.FindPair(ssrc);
                if (sdst == null)
                {
                    plan.DropSchema(ssrc);
                }
                else if (ssrc.SchemaName != sdst.SchemaName)
                {
                    plan.RenameSchema(ssrc, sdst.SchemaName);
                    //if (caps.RenameSchema) proc.RenameSchema(ssrc, sdst.SchemaName);
                    //else
                    //{
                    //    proc.DropSchema(ssrc);
                    //    proc.CreateSchema(sdst);
                    //}
                }
            }

            foreach (ISchemaStructure sdst in dst.Schemata)
            {
                if (!pairing.IsPaired(sdst)) plan.CreateSchema(sdst);
            }

            var alteredOptions = GetDatabaseAlteredOptions(src, dst, opts);
            if (alteredOptions.Count > 0) plan.ChangeDatabaseOptions(null, alteredOptions);

            if (opts.SchemaMode == DbDiffSchemaMode.Ignore)
            {
                plan.RunNameTransformation(new SetSchemaNameTransformation(null));
            }
        }

        public static Dictionary<string, string> GetDatabaseAlteredOptions(IDatabaseStructure oldDb, IDatabaseStructure newDb, DbDiffOptions opts)
        {
            Dictionary<string, string> alteredOptions = new Dictionary<string, string>();
            if (opts.IgnoreAllDatabaseProperties) return alteredOptions;
            foreach (string key in newDb.SpecificData.Keys)
            {
                if (opts.IgnoreDatabaseProperties.Contains(key)) continue;
                if (oldDb.SpecificData.Get(key) != newDb.SpecificData[key])
                    alteredOptions[key] = newDb.SpecificData[key];
            }
            return alteredOptions;
        }

        public static NameWithSchema GenerateNewName(NameWithSchema oldName, NameWithSchema newName, DbDiffOptions opts)
        {
            //if (oldName == null)
            //{
            //    if (opts.SchemaMode != DbDiffSchemaMode.Strict) return new NameWithSchema(null, newName.Name);
            //    return newName;
            //}
            NameWithSchema res = oldName;
            if (!EqualSchemas(oldName.Schema, newName.Schema, opts)) res = new NameWithSchema(newName.Schema, res.Name);
            if (oldName.Name != newName.Name) res = new NameWithSchema(res.Schema, newName.Name);
            return res;
        }

        public static bool GenerateRename(NameWithSchema oldName, NameWithSchema newName, Action<NameWithSchema, string> changeSchema, Action<NameWithSchema, string> rename, bool allowChangeSchema, bool allowRename, DbDiffOptions opts)
        {
            newName = GenerateNewName(oldName, newName, opts);
            if (DbDiffTool.EqualFullNames(oldName, newName, opts)) return true;
            if (!EqualSchemas(oldName.Schema, newName.Schema, opts) && !allowChangeSchema) return false;
            if (oldName.Name != newName.Name && !allowRename) return false;
            if (!EqualSchemas(oldName.Schema, newName.Schema, opts)) changeSchema(oldName, newName.Schema);
            if (oldName.Name != newName.Name) rename(new NameWithSchema(newName.Schema, oldName.Name), newName.Name);
            return true;
        }

        public static bool GenerateRenameSpecificObject(ISpecificObjectStructure oldObj, NameWithSchema newName, Action<ISpecificObjectStructure, string> changeSchema, Action<ISpecificObjectStructure, string> rename, bool allowChangeSchema, bool allowRename, DbDiffOptions opts)
        {
            newName = GenerateNewName(oldObj.ObjectName, newName, opts);
            if (DbDiffTool.EqualFullNames(oldObj.ObjectName, newName, opts)) return true;
            if (!EqualSchemas(oldObj.ObjectName.Schema, newName.Schema, opts) && !allowChangeSchema) return false;
            if (oldObj.ObjectName.Name != newName.Name && !allowRename) return false;
            if (!EqualSchemas(oldObj.ObjectName.Schema, newName.Schema, opts)) changeSchema(oldObj, newName.Schema);
            if (oldObj.ObjectName.Name != newName.Name)
            {
                var tmpo = (SpecificObjectStructure)oldObj.CloneObject();
                tmpo.ObjectName = new NameWithSchema(newName.Schema, oldObj.ObjectName.Name);
                rename(tmpo, newName.Name);
            }
            return true;
        }

        public static void AlterSpecificObject(ISpecificObjectStructure osrc, ISpecificObjectStructure odst, AlterPlan plan, DbDiffOptions opts, DbObjectPairing pairing)
        {
            //bool altered = false;
            if (osrc.CreateSql == odst.CreateSql)
            {
                plan.RenameSpecificObject(osrc, odst.ObjectName);
                //altered = GenerateRename(osrc.ObjectName, odst.ObjectName,
                //    (old, sch) =>
                //    {
                //        var o2 = new SpecificObjectStructure(osrc);
                //        o2.ObjectName = old;
                //        proc.ChangeSpecificObjectSchema(o2, sch);
                //    },
                //    (old, nam) =>
                //    {
                //        var o2 = new SpecificObjectStructure(osrc);
                //        o2.ObjectName = old;
                //        proc.RenameSpecificObject(o2, nam);
                //    }, caps[osrc.ObjectType].ChangeSchema, caps[osrc.ObjectType].Rename, opts);
            }
            else
            {
                plan.ChangeSpecificObject(osrc, odst);
            }
            //if (!altered)
            //{
            //    proc.DropSpecificObject(osrc);
            //    SpecificObjectStructure odst2 = new SpecificObjectStructure(odst);
            //    odst2.ObjectName = GenerateNewName(osrc.ObjectName, odst.ObjectName, opts);
            //    proc.CreateSpecificObject(odst);
            //}
        }

        public static void AlterDatabase(AlterPlan plan, IDatabaseStructure src, IDatabaseStructure dst, DbDiffOptions opts)
        {
            AlterDatabase(plan, new DbObjectPairing(src, dst), opts);
        }

        public static DbObjectPairing CreateTablePairing(ITableStructure oldTable, ITableStructure newTable)
        {
            DatabaseStructure src = new DatabaseStructure();
            DatabaseStructure dst = new DatabaseStructure(src);
            src.AddObject(oldTable, true);
            dst.AddObject(newTable, true);
            DbObjectPairing pairing = new DbObjectPairing(src, dst);
            return pairing;
        }

        /// alters table, decomposes to alter actions; doesn't transform alter plan
        /// proc must be able to run all logical operations
        public static void DecomposeAlterTable(this IAlterProcessor proc, ITableStructure oldTable, ITableStructure newTable, DbDiffOptions options)
        {
            DbObjectPairing pairing = CreateTablePairing(oldTable, newTable);
            // decompose to alter actions
            AlterPlan plan = new AlterPlan();
            AlterTable(plan, oldTable, newTable, options, pairing);
            var run = plan.CreateRunner();
            run.Run(proc, options);
        }

        public static AlterPlan PlanAlterTable(ITableStructure oldTable, ITableStructure newTable, DbDiffOptions opts)
        {
            AlterPlan plan = new AlterPlan();
            if (oldTable == null)
            {
                plan.CreateTable(newTable);
            }
            else
            {
                DbObjectPairing pairing = CreateTablePairing(oldTable, newTable);
                AlterTable(plan, oldTable, newTable, opts, pairing);
            }
            return plan;
        }
    }
}
