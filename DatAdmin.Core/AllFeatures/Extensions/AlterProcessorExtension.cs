using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace DatAdmin
{
    public static class AlterProcessorExtension
    {
        public static void DropObject(this IAlterProcessor proc, IAbstractObjectStructure obj)
        {
            var tbl = obj as ITableStructure;
            if (tbl != null)
            {
                proc.DropTable(tbl);
                return;
            }
            var col = obj as IColumnStructure;
            if (col != null)
            {
                proc.DropColumn(col);
                return;
            }
            var cnt = obj as IConstraint;
            if (cnt != null)
            {
                proc.DropConstraint(cnt);
                return;
            }
            var spe = obj as ISpecificObjectStructure;
            if (spe != null)
            {
                proc.DropSpecificObject(spe);
                return;
            }
            var sch = obj as ISchemaStructure;
            if (sch != null)
            {
                proc.DropSchema(sch);
                return;
            }
            var dom = obj as IDomainStructure;
            if (dom != null)
            {
                proc.DropDomain(dom);
                return;
            }
        }

        public static void CreateColumn(this IAlterProcessor proc, IColumnStructure col)
        {
            proc.CreateColumn(col, null);
        }

        public static void ChangeColumn(this IAlterProcessor proc, IColumnStructure oldcol, IColumnStructure newcol)
        {
            proc.ChangeColumn(oldcol, newcol, null);
        }

        public static void CreateObject(this IAlterProcessor proc, IAbstractObjectStructure obj)
        {
            var tbl = obj as ITableStructure;
            if (tbl != null)
            {
                proc.CreateTable(tbl);
                return;
            }
            var col = obj as IColumnStructure;
            if (col != null)
            {
                proc.CreateColumn(col);
                return;
            }
            var cnt = obj as IConstraint;
            if (cnt != null)
            {
                proc.CreateConstraint(cnt);
                return;
            }
            var spe = obj as ISpecificObjectStructure;
            if (spe != null)
            {
                proc.CreateSpecificObject(spe);
                return;
            }
            var sch = obj as ISchemaStructure;
            if (sch != null)
            {
                proc.CreateSchema(sch);
                return;
            }
            var dom = obj as IDomainStructure;
            if (dom != null)
            {
                proc.CreateDomain(dom);
                return;
            }
        }

        public static AbstractObjectStructure CloneObject(this IAbstractObjectStructure obj)
        {
            var tbl = obj as ITableStructure;
            if (tbl != null) return new TableStructure(tbl);
            var col = obj as IColumnStructure;
            if (col != null) return new ColumnStructure(col);
            var cnt = obj as IConstraint;
            if (cnt != null) return Constraint.CreateCopy(cnt);
            var spe = obj as ISpecificObjectStructure;
            if (spe != null) return new SpecificObjectStructure(spe);
            var sch = obj as ISchemaStructure;
            if (sch != null) return new SchemaStructure(sch);
            var dom = obj as IDomainStructure;
            if (dom != null) return new DomainStructure(dom);
            var dbs = obj as IDatabaseStructure;
            if (dbs != null) return new DatabaseStructure(dbs);
            return null;
        }

        public static void RenameObject(this IAlterProcessor proc, IAbstractObjectStructure obj, DbDiffOptions opts, NameWithSchema newName)
        {
            bool renameOk = false;
            var dom = obj as IDomainStructure;
            if (dom != null)
            {
                renameOk = DbDiffTool.GenerateRename(dom.FullName, newName,
                    (old, sch) => proc.ChangeDomainSchema(old, sch),
                    (old, nam) => proc.RenameDomain(old, nam),
                    proc.AlterCaps.ChangeTableSchema, proc.AlterCaps.RenameDomain, opts);
            }
            var tbl = obj as ITableStructure;
            if (tbl != null)
            {
                renameOk = DbDiffTool.GenerateRename(tbl.FullName, newName,
                    (old, sch) => proc.ChangeTableSchema(old, sch),
                    (old, nam) => proc.RenameTable(old, nam),
                    proc.AlterCaps.ChangeTableSchema, proc.AlterCaps.RenameTable, opts);
            }
            var col = obj as IColumnStructure;
            if (col != null)
            {
                if (proc.AlterCaps.RenameColumn)
                {
                    proc.RenameColumn(col, newName.Name);
                    renameOk = true;
                }
            }
            var cnt = obj as IConstraint;
            if (cnt != null)
            {
                if (proc.AlterCaps.RenameConstraint)
                {
                    proc.RenameConstraint(cnt, newName.Name);
                    renameOk = true;
                }
            }
            var spec = obj as ISpecificObjectStructure;
            if (spec != null)
            {
                renameOk = DbDiffTool.GenerateRenameSpecificObject(spec, newName,
                    (old, sch) => proc.ChangeSpecificObjectSchema(old, sch),
                    (old, nam) => proc.RenameSpecificObject(old, nam),
                    proc.AlterCaps[spec.ObjectType].ChangeSchema, proc.AlterCaps[spec.ObjectType].Rename, opts);

            }
            if (!renameOk) throw new AlterNotPossibleError();
        }

        public static void ChangeObject(this IAlterProcessor proc, IAbstractObjectStructure obj, IAbstractObjectStructure newObj)
        {
            var tbl = obj as ITableStructure;
            if (tbl != null)
            {
                throw new AlterNotPossibleError();
            }
            var col = obj as IColumnStructure;
            if (col != null)
            {
                proc.ChangeColumn(col, (IColumnStructure)newObj);
                return;
            }
            var cnt = obj as IConstraint;
            if (cnt != null)
            {
                proc.ChangeConstraint(cnt, (IConstraint)newObj);
                return;
            }
            var spe = obj as ISpecificObjectStructure;
            if (spe != null)
            {
                proc.ChangeSpecificObject(spe, (ISpecificObjectStructure)newObj);
                return;
            }
            var sch = obj as ISchemaStructure;
            if (sch != null)
            {
                proc.ChangeSchema(sch, (ISchemaStructure)newObj);
                return;
            }
            var dom = obj as IDomainStructure;
            if (dom != null)
            {
                proc.ChangeDomain(dom, (IDomainStructure)newObj);
                return;
            }
        }
        public static void AlterDatabase(this IAlterProcessor proc, IDatabaseStructure src, IDatabaseStructure dst, DbDiffOptions opts, IDatabaseSource targetDb)
        {
            proc.AlterDatabase(src, dst, opts, targetDb, null);
        }

        public static void AlterDatabase(this IAlterProcessor proc, IDatabaseStructure src, IDatabaseStructure dst, DbDiffOptions opts, IDatabaseSource targetDb, Action<AlterPlan> extendPlan)
        {
            AlterPlan plan = new AlterPlan();
            DbDiffTool.AlterDatabase(plan, src, dst, opts);
            if (extendPlan != null) extendPlan(plan);
            plan.Transform(proc.AlterCaps, opts, targetDb);
            var run = plan.CreateRunner();
            run.Run(proc, opts);
        }

        public static void CreateConstraints(this IAlterProcessor fmt, IEnumerable constraints)
        {
            if (constraints == null) return;
            foreach (IConstraint cnt in constraints)
            {
                fmt.CreateConstraint(cnt);
            }
        }
    }
}
