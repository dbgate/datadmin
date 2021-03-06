﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace DatAdmin
{
    public enum AlterObjectMeaning { OldObject, NewObject }
    public enum AlterObjectActionType { Rename, Drop, Create, Change, Other }
    public struct AlterOperationEnumRecord
    {
        public AbstractObjectStructure Object;
        public AlterObjectMeaning Meaning;
        public AlterObjectActionType ActionType;
    }

    public abstract class AlterOperation
    {
        public virtual int OrderGroup { get { return 0; } }
        internal int m_tmpOrder;
        //public List<AlterOperation> Dependencies = new List<AlterOperation>();
        public TableStructure ParentTable;
        public abstract void Run(IAlterProcessor proc, DbDiffOptions opts);

        public virtual bool MustRunOnParalelStructure() { return true; }
        public virtual bool DenyTransaction() { return false; }

        protected AbstractObjectStructure GetPossibleTableObject(AbstractObjectStructure newObject)
        {
            if (ParentTable != null)
            {
                AbstractObjectStructure res = newObject.CloneObject();
                ((TableObjectStructure)res).SetDummyTable(ParentTable.FullName);
                return res;
            }
            else
            {
                return newObject;
            }
        }

        protected static ObjectOperationCaps GetConstraintCaps(AlterProcessorCaps caps, IAbstractObjectStructure obj)
        {
            if (obj is IIndex)
            {
                return new ObjectOperationCaps
                {
                    Create = caps.AddIndex,
                    Drop = caps.DropIndex,
                    Rename = caps.RenameIndex,
                    Change = caps.ChangeIndex,
                };
            }
            else
            {
                return new ObjectOperationCaps
                {
                    Create = caps.AddConstraint,
                    Drop = caps.DropConstraint,
                    Rename = caps.RenameConstraint,
                    Change = caps.ChangeConstraint,
                };
            }
        }

        public virtual void ChangeStructure(DatabaseStructure s)
        {
            if (ParentTable != null) ParentTable = s.FindByGroupId(ParentTable.GroupId) as TableStructure;
        }

        public AlterOperation CloneForStructure(DatabaseStructure s)
        {
            Type t = GetType();
            var res = t.GetConstructor(new Type[] { }).Invoke(new object[] { }) as AlterOperation;
            res.AssignFrom(this);
            if (s != null) res.ChangeStructure(s);
            return res;
        }

        public virtual void AddLogicalDependencies(AlterProcessorCaps caps, DbDiffOptions opts, List<AlterOperation> before, List<AlterOperation> after, AlterPlan plan, IDatabaseSource targetDb)
        {
        }
        public virtual void TransformToImplementedOps(AlterProcessorCaps caps, DbDiffOptions opts, List<AlterOperation> replacement, AlterPlan plan, IDatabaseSource targetDb)
        {
        }

        protected void TransformToRecreateTable(List<AlterOperation> replacement, AlterPlan plan, IDatabaseSource targetDb)
        {
            replacement.Clear();
            var op = new AlterOperation_RecreateTable { ParentTable = ParentTable };
            ParentTable.LoadStructure(TableStructureMembers.All, targetDb);
            foreach (var fk in ParentTable.GetReferencedFrom()) plan.RecreateObject(fk as AbstractObjectStructure, null);
            op.AppendOp(this);
            replacement.Add(op);
        }

        public virtual void AssignFrom(AlterOperation src)
        {
            ParentTable = src.ParentTable;
        }

        public virtual void AddPhysicalDependencies(AlterProcessorCaps caps, DbDiffOptions opts, List<AlterOperation> before, List<AlterOperation> after, AlterPlan alterPlan, IDatabaseSource targetDb)
        {
        }

        public virtual IEnumerable<AlterOperationEnumRecord> EnumObjects()
        {
            yield break;
        }

        public virtual bool MustRunAbsorbTest(AlterProcessorCaps caps) { return false; }
        public virtual bool AbsorbOperation(AlterProcessorCaps caps, AlterOperation op) { return false; }

        public virtual AbstractObjectStructure GetDropObject()
        {
            return null;
        }

        public virtual void RunNameTransformation(INameTransformation transform)
        {
        }
    }

    public class AlterOperation_Drop : AlterOperation
    {
        public AbstractObjectStructure OldObject;

        public override void Run(IAlterProcessor proc, DbDiffOptions opts)
        {
            proc.DropObject(OldObject);
        }

        public override void ChangeStructure(DatabaseStructure s)
        {
            base.ChangeStructure(s);
            OldObject = s.FindByGroupId(OldObject.GroupId) as AbstractObjectStructure;
        }

        public override void AssignFrom(AlterOperation src)
        {
            base.AssignFrom(src);
            OldObject = ((AlterOperation_Drop)src).OldObject;
        }

        public override IEnumerable<AlterOperationEnumRecord> EnumObjects()
        {
            yield return new AlterOperationEnumRecord { Object = OldObject, Meaning = AlterObjectMeaning.OldObject, ActionType = AlterObjectActionType.Drop };
        }

        public override string ToString()
        {
            return "DROP " + OldObject.ToString();
        }

        public override AbstractObjectStructure GetDropObject()
        {
            return OldObject;
        }
    }
    public class AlterOperation_Create : AlterOperation
    {
        public AbstractObjectStructure NewObject;

        public override void Run(IAlterProcessor proc, DbDiffOptions opts)
        {
            proc.CreateObject(GetPossibleTableObject(NewObject));
        }

        public override IEnumerable<AlterOperationEnumRecord> EnumObjects()
        {
            yield return new AlterOperationEnumRecord { Object = NewObject, Meaning = AlterObjectMeaning.NewObject, ActionType = AlterObjectActionType.Create };
        }

        public override void AssignFrom(AlterOperation src)
        {
            base.AssignFrom(src);
            NewObject = ((AlterOperation_Create)src).NewObject;
        }

        public override string ToString()
        {
            return "CREATE " + NewObject.ToString();
        }
    }
    public class AlterOperation_Rename : AlterOperation
    {
        public AbstractObjectStructure OldObject;
        public NameWithSchema NewName;

        public override void Run(IAlterProcessor proc, DbDiffOptions opts)
        {
            proc.RenameObject(OldObject, opts, NewName);
        }

        public override void ChangeStructure(DatabaseStructure s)
        {
            base.ChangeStructure(s);
            OldObject = s.FindByGroupId(OldObject.GroupId) as AbstractObjectStructure;
        }

        public override void AssignFrom(AlterOperation src)
        {
            base.AssignFrom(src);
            OldObject = ((AlterOperation_Rename)src).OldObject;
            NewName = ((AlterOperation_Rename)src).NewName;
        }

        public override IEnumerable<AlterOperationEnumRecord> EnumObjects()
        {
            yield return new AlterOperationEnumRecord { Object = OldObject, Meaning = AlterObjectMeaning.OldObject, ActionType = AlterObjectActionType.Rename };
        }

        public override string ToString()
        {
            return "RENAME " + OldObject.ToString() + "->" + NewName.ToString();
        }
    }
    public class AlterOperation_Change : AlterOperation
    {
        public AbstractObjectStructure OldObject;
        public AbstractObjectStructure NewObject;

        public override void Run(IAlterProcessor proc, DbDiffOptions opts)
        {
            proc.ChangeObject(OldObject, GetPossibleTableObject(NewObject));
        }

        public override void ChangeStructure(DatabaseStructure s)
        {
            base.ChangeStructure(s);
            OldObject = s.FindByGroupId(OldObject.GroupId) as AbstractObjectStructure;
        }

        public override void AssignFrom(AlterOperation src)
        {
            base.AssignFrom(src);
            OldObject = ((AlterOperation_Change)src).OldObject;
            NewObject = ((AlterOperation_Change)src).NewObject;
        }

        public override IEnumerable<AlterOperationEnumRecord> EnumObjects()
        {
            yield return new AlterOperationEnumRecord { Object = OldObject, Meaning = AlterObjectMeaning.OldObject, ActionType = AlterObjectActionType.Change };
            yield return new AlterOperationEnumRecord { Object = NewObject, Meaning = AlterObjectMeaning.NewObject, ActionType = AlterObjectActionType.Change };
        }

        public override string ToString()
        {
            return "CHANGE " + OldObject.ToString();
        }
    }

    public class AlterOperation_CreateDomain : AlterOperation_Create { }
    public class AlterOperation_DropDomain : AlterOperation_Drop { }
    public class AlterOperation_ChangeDomain : AlterOperation_Change { }
    public class AlterOperation_RenameDomain : AlterOperation_Rename { }
    public class AlterOperation_CreateColumn : AlterOperation_Create
    {
        public List<IConstraint> AdditionalConstraints = new List<IConstraint>();
        public override void TransformToImplementedOps(AlterProcessorCaps caps, DbDiffOptions opts, List<AlterOperation> replacement, AlterPlan plan, IDatabaseSource targetDb)
        {
            if (!caps.AddColumn) TransformToRecreateTable(replacement, plan, targetDb);
        }
        public override void Run(IAlterProcessor proc, DbDiffOptions opts)
        {
            proc.CreateColumn((IColumnStructure)GetPossibleTableObject(NewObject), AdditionalConstraints);
        }
        public override bool MustRunAbsorbTest(AlterProcessorCaps caps)
        {
            return caps.ForceAbsorbPrimaryKey;
        }
        public override bool AbsorbOperation(AlterProcessorCaps caps, AlterOperation op)
        {
            var cop = op as AlterOperation_Create;
            if (cop != null)
            {
                var pk = cop.NewObject as PrimaryKey;
                if (pk != null && pk.Columns.Count == 1 && pk.Columns[0].ColumnName == ((IColumnStructure)NewObject).ColumnName)
                {
                    pk = new PrimaryKey(pk);
                    pk.SetDummyTable(ParentTable.FullName);
                    AdditionalConstraints.Add(pk);
                    return true;
                }
            }
            return base.AbsorbOperation(caps, op);
        }
        public override void AssignFrom(AlterOperation src)
        {
            base.AssignFrom(src);
            AdditionalConstraints.AddRange(((AlterOperation_CreateColumn)src).AdditionalConstraints);
        }
    }
    public class AlterOperation_DropColumn : AlterOperation_Drop
    {
        public override void TransformToImplementedOps(AlterProcessorCaps caps, DbDiffOptions opts, List<AlterOperation> replacement, AlterPlan plan, IDatabaseSource targetDb)
        {
            if (!caps.DropColumn) TransformToRecreateTable(replacement, plan, targetDb);
        }

        public override void AddLogicalDependencies(AlterProcessorCaps caps, DbDiffOptions opts, List<AlterOperation> before, List<AlterOperation> after, AlterPlan plan, IDatabaseSource targetDb)
        {
            base.AddLogicalDependencies(caps, opts, before, after, plan, targetDb);

            var col = (IColumnStructure)OldObject;

            ParentTable.LoadStructure(TableStructureMembers.Constraints | TableStructureMembers.ReferencedFrom, targetDb);
            foreach (ForeignKey fk in ParentTable.GetReferencedFrom())
            {
                bool fkdeleted = false;
                for (int i = 0; i < fk.PrimaryKeyColumns.Count; i++)
                {
                    if (fk.PrimaryKeyColumns[i].ColumnName == col.ColumnName)
                    {
                        fkdeleted = true;
                        break;
                    }
                }
                if (fkdeleted)
                {
                    opts.AlterLogger.Warning(Texts.Get("s_dropped_reference$table$fk", "table", fk.Table.FullName, "fk", fk.Name));
                    before.Add(new AlterOperation_DropConstraint { ParentTable = ParentTable, OldObject = fk });
                }
            }

            foreach (var cnt in ParentTable.Constraints)
            {
                var cc = cnt as ColumnsConstraint;
                if (cc == null) continue;
                if (cc.Columns.Any(c => c.ColumnName == col.ColumnName))
                {
                    before.Add(new AlterOperation_DropConstraint { ParentTable = ParentTable, OldObject = cc });
                }
            }
        }
    }

    public class AlterOperation_ChangeColumn : AlterOperation_Change
    {
        public List<IConstraint> AdditionalConstraints = new List<IConstraint>();
        public override void TransformToImplementedOps(AlterProcessorCaps caps, DbDiffOptions opts, List<AlterOperation> replacement, AlterPlan plan, IDatabaseSource targetDb)
        {
            if (!caps.ChangeColumn)
            {
                TransformToRecreateTable(replacement, plan, targetDb);
                return;
            }
            if (!caps.ChangeAutoIncrement && ((IColumnStructure)OldObject).DataType.IsAutoIncrement() != ((IColumnStructure)NewObject).DataType.IsAutoIncrement())
            {
                TransformToRecreateTable(replacement, plan, targetDb);
                return;
            }
        }
        public override void AddLogicalDependencies(AlterProcessorCaps caps, DbDiffOptions opts, List<AlterOperation> before, List<AlterOperation> after, AlterPlan plan, IDatabaseSource targetDb)
        {
            ParentTable.LoadStructure(TableStructureMembers.ReferencedFrom, targetDb);
            var oldcol = OldObject as ColumnStructure;
            var newcol = NewObject as ColumnStructure;

            List<IForeignKey> recreateFks = new List<IForeignKey>();
            var changeCols = new List<Tuple<IColumnStructure, IColumnStructure>>();

            foreach (ForeignKey fk in ParentTable.GetReferencedFrom())
            {
                for (int i = 0; i < fk.PrimaryKeyColumns.Count; i++)
                {
                    if (fk.PrimaryKeyColumns[i].ColumnName == oldcol.ColumnName)
                    {
                        //plan.RecreateObject(fk, null);
                        TableStructure table = (TableStructure)fk.Table;
                        table.LoadStructure(TableStructureMembers.Columns, targetDb);
                        ColumnStructure othercol = table.Columns[fk.Columns[i].ColumnName] as ColumnStructure;

                        // compare types with ignoring autoincrement flag
                        // HACK: ignore specific attributes
                        var opts2 = opts.Clone();
                        opts2.IgnoreSpecificData = true;
                        DbTypeBase dt1 = othercol.DataType.Clone(), dt2 = newcol.DataType.Clone();
                        dt1.SetAutoincrement(false);
                        dt2.SetAutoincrement(false);
                        if (!DbDiffTool.EqualTypes(dt1, dt2, opts2))
                        {
                            after.Add(new AlterOperation_ChangeColumn
                            {
                                ParentTable = table,
                                OldObject = othercol,
                                NewObject = new ColumnStructure(othercol) { DataType = dt2 }
                            });
                        }
                        opts.AlterLogger.Warning(Texts.Get("s_changed_referenced_column$table$column", "table", fk.Table.FullName, "column", othercol.ColumnName));
                    }
                }
            }
        }
        public override void AddPhysicalDependencies(AlterProcessorCaps caps, DbDiffOptions opts, List<AlterOperation> before, List<AlterOperation> after, AlterPlan plan, IDatabaseSource targetDb)
        {
            var oldcol = OldObject as ColumnStructure;
            if (caps.DepCaps.ChangeColumn_Constraint || caps.DepCaps.ChangeColumn_Index)
            {
                ParentTable.LoadStructure(TableStructureMembers.Constraints, targetDb);
                foreach (var cnt in ParentTable.Constraints)
                {
                    var cc = cnt as ColumnsConstraint;
                    if (cc == null) continue;
                    if (cc.Columns.Any(c => c.ColumnName == oldcol.ColumnName))
                    {
                        if (
                            (cc is IIndex && caps.DepCaps.ChangeColumn_Index) ||
                            (!(cc is IIndex) && caps.DepCaps.ChangeColumn_Constraint))
                        {
                            plan.RecreateObject(cc, null);
                        }
                    }
                }
            }
            if (caps.DepCaps.ChangeColumn_Reference)
            {
                ParentTable.LoadStructure(TableStructureMembers.ReferencedFrom, targetDb);

                foreach (ForeignKey fk in ParentTable.GetReferencedFrom())
                {
                    for (int i = 0; i < fk.PrimaryKeyColumns.Count; i++)
                    {
                        if (fk.PrimaryKeyColumns[i].ColumnName == oldcol.ColumnName)
                        {
                            plan.RecreateObject(fk, null);
                        }
                    }
                }
            }
        }
        public override bool MustRunAbsorbTest(AlterProcessorCaps caps)
        {
            return caps.ForceAbsorbPrimaryKey;
        }
        public override bool AbsorbOperation(AlterProcessorCaps caps, AlterOperation op)
        {
            var cop = op as AlterOperation_Create;
            if (cop != null)
            {
                var pk = cop.NewObject as PrimaryKey;
                if (pk != null && pk.Columns.Count == 1 && pk.Columns[0].ColumnName == ((IColumnStructure)NewObject).ColumnName)
                {
                    pk = new PrimaryKey(pk);
                    pk.SetDummyTable(ParentTable.FullName);
                    AdditionalConstraints.Add(pk);
                    return true;
                }
            }
            return base.AbsorbOperation(caps, op);
        }
        public override void Run(IAlterProcessor proc, DbDiffOptions opts)
        {
            proc.ChangeColumn((IColumnStructure)OldObject, (IColumnStructure)GetPossibleTableObject(NewObject), AdditionalConstraints);
        }
        public override void AssignFrom(AlterOperation src)
        {
            base.AssignFrom(src);
            AdditionalConstraints.AddRange(((AlterOperation_ChangeColumn)src).AdditionalConstraints);
        }
    }
    public class AlterOperation_RenameColumn : AlterOperation_Rename
    {
        public override void TransformToImplementedOps(AlterProcessorCaps caps, DbDiffOptions opts, List<AlterOperation> replacement, AlterPlan plan, IDatabaseSource targetDb)
        {
            if (!caps.RenameColumn) TransformToRecreateTable(replacement, plan, targetDb);
        }
    }
    public class AlterOperation_CreateConstraint : AlterOperation_Create
    {
        public override int OrderGroup
        {
            get
            {
                if (NewObject is IForeignKey) return AlterPlan.AFTER_ORDERGROUP + 1;
                return 0;
            }
        }
        public override void TransformToImplementedOps(AlterProcessorCaps caps, DbDiffOptions opts, List<AlterOperation> replacement, AlterPlan plan, IDatabaseSource targetDb)
        {
            var c = GetConstraintCaps(caps, NewObject);
            if (!c.Create) TransformToRecreateTable(replacement, plan, targetDb);
        }
        public override void RunNameTransformation(INameTransformation transform)
        {
            base.RunNameTransformation(transform);
            var fk = NewObject as ForeignKey;
            if (fk != null) fk.RunNameTransformation(transform);
        }
    }
    public class AlterOperation_DropConstraint : AlterOperation_Drop
    {
        public override void AddLogicalDependencies(AlterProcessorCaps caps, DbDiffOptions opts, List<AlterOperation> before, List<AlterOperation> after, AlterPlan plan, IDatabaseSource targetDb)
        {
            base.AddLogicalDependencies(caps, opts, before, after, plan, targetDb);

            var pk = OldObject as IPrimaryKey;
            if (pk != null)
            {
                ParentTable.LoadStructure(TableStructureMembers.ReferencedFrom, targetDb);
                foreach (var col in pk.Columns)
                {
                    foreach (ForeignKey fk in ParentTable.GetReferencedFrom())
                    {
                        bool fkdeleted = false;
                        for (int i = 0; i < fk.PrimaryKeyColumns.Count; i++)
                        {
                            if (fk.PrimaryKeyColumns[i].ColumnName == col.ColumnName)
                            {
                                fkdeleted = true;
                                break;
                            }
                        }
                        if (fkdeleted)
                        {
                            opts.AlterLogger.Warning(Texts.Get("s_dropped_reference$table$fk", "table", fk.Table.FullName, "fk", fk.Name));
                            before.Add(new AlterOperation_DropConstraint { ParentTable = ParentTable, OldObject = fk });
                        }
                    }
                }
            }
        }
        public override int OrderGroup
        {
            get
            {
                if (OldObject is IForeignKey) return AlterPlan.BEFORE_ORDERGROUP - 1;
                return 0;
            }
        }
        public override void TransformToImplementedOps(AlterProcessorCaps caps, DbDiffOptions opts, List<AlterOperation> replacement, AlterPlan plan, IDatabaseSource targetDb)
        {
            var c = GetConstraintCaps(caps, OldObject);
            if (!c.Drop) TransformToRecreateTable(replacement, plan, targetDb);
        }
    }
    public class AlterOperation_ChangeConstraint : AlterOperation_Change
    {
        public override void TransformToImplementedOps(AlterProcessorCaps caps, DbDiffOptions opts, List<AlterOperation> replacement, AlterPlan plan, IDatabaseSource targetDb)
        {
            var c = GetConstraintCaps(caps, NewObject);
            if (!c.Change)
            {
                if (c.Create && c.Drop)
                {
                    plan.RecreateObject(OldObject, NewObject);
                    replacement.Clear();
                }
                else
                {
                    TransformToRecreateTable(replacement, plan, targetDb);
                }
            }
        }
        public override void RunNameTransformation(INameTransformation transform)
        {
            base.RunNameTransformation(transform);
            var fk = NewObject as ForeignKey;
            if (fk != null) fk.RunNameTransformation(transform);
        }
    }
    public class AlterOperation_RenameConstraint : AlterOperation_Rename
    {
        public override void TransformToImplementedOps(AlterProcessorCaps caps, DbDiffOptions opts, List<AlterOperation> replacement, AlterPlan plan, IDatabaseSource targetDb)
        {
            var c = GetConstraintCaps(caps, OldObject);
            if (!c.Rename)
            {
                if (c.Create && c.Drop)
                {
                    var newobj = OldObject.CloneObject();
                    ((Constraint)newobj).Name = NewName.Name;
                    plan.RecreateObject(OldObject, newobj);
                    replacement.Clear();
                }
                else
                {
                    TransformToRecreateTable(replacement, plan, targetDb);
                }
            }
        }
    }
    public class AlterOperation_CreateSpecificObject : AlterOperation_Create { }
    public class AlterOperation_DropSpecificObject : AlterOperation_Drop { }
    public class AlterOperation_ChangeSpecificObject : AlterOperation_Change
    {
        public override void TransformToImplementedOps(AlterProcessorCaps caps, DbDiffOptions opts, List<AlterOperation> replacement, AlterPlan plan, IDatabaseSource targetDb)
        {
            if (!caps[((ISpecificObjectStructure)OldObject).ObjectType].Change)
            {
                plan.RecreateObject(OldObject, NewObject);
                replacement.Clear();
            }
        }
    }
    public class AlterOperation_RenameSpecificObject : AlterOperation_Rename { }
    public class AlterOperation_PermuteColumns : AlterOperation
    {
        public List<string> NewColumnOrder;
        public override void TransformToImplementedOps(AlterProcessorCaps caps, DbDiffOptions opts, List<AlterOperation> replacement, AlterPlan plan, IDatabaseSource targetDb)
        {
            if (!caps.PermuteColumns)
            {
                TransformToRecreateTable(replacement, plan, targetDb);
            }
        }
        public override void Run(IAlterProcessor proc, DbDiffOptions opts)
        {
            proc.ReorderColumns(ParentTable.FullName, NewColumnOrder);
        }

        public override bool MustRunOnParalelStructure()
        {
            return false;
        }

        public override void AssignFrom(AlterOperation src)
        {
            base.AssignFrom(src);
            NewColumnOrder = new List<string>(((AlterOperation_PermuteColumns)src).NewColumnOrder);
        }

        public override string ToString()
        {
            return "REORDER COLUMNS " + ParentTable.ToString();
        }
    }
    public class AlterOperation_UpdateData : AlterOperation
    {
        public DataScript Script;

        public override bool MustRunOnParalelStructure()
        {
            return false;
        }
        public override void Run(IAlterProcessor proc, DbDiffOptions opts)
        {
            proc.UpdateData(ParentTable, Script, null);
        }
        public override void AddPhysicalDependencies(AlterProcessorCaps caps, DbDiffOptions opts, List<AlterOperation> before, List<AlterOperation> after, AlterPlan alterPlan, IDatabaseSource targetDb)
        {
            base.AddPhysicalDependencies(caps, opts, before, after, alterPlan, targetDb);
            ParentTable.LoadStructure(TableStructureMembers.Columns | TableStructureMembers.PrimaryKey, targetDb);
        }
        public override string ToString()
        {
            return "UPDATE DATA " + ParentTable.ToString();
        }
        public override void AssignFrom(AlterOperation src)
        {
            base.AssignFrom(src);
            Script = ((AlterOperation_UpdateData)src).Script;
        }
    }
    public class AlterOperation_ChangeTableOptions : AlterOperation
    {
        public Dictionary<string,string> Options;

        public override void Run(IAlterProcessor proc, DbDiffOptions opts)
        {
            proc.AlterTableOptions(ParentTable, Options);
        }
        public override string ToString()
        {
            return "CHANGE OPTIONS " + ParentTable.ToString();
        }
        public override void AssignFrom(AlterOperation src)
        {
            base.AssignFrom(src);
            Options = ((AlterOperation_ChangeTableOptions)src).Options;
        }
    }
    public class AlterOperation_ChangeDatabaseOptions : AlterOperation
    {
        public Dictionary<string, string> Options;
        public string DbName;

        public override void Run(IAlterProcessor proc, DbDiffOptions opts)
        {
            proc.AlterDatabaseOptions(DbName, Options);
        }
        public override string ToString()
        {
            return "CHANGE DATABSE OPTIONS";
        }
        public override void TransformToImplementedOps(AlterProcessorCaps caps, DbDiffOptions opts, List<AlterOperation> replacement, AlterPlan plan, IDatabaseSource targetDb)
        {
            base.TransformToImplementedOps(caps, opts, replacement, plan, targetDb);
            if (DbName == null) DbName = targetDb.DatabaseName;
        }
        public override void AssignFrom(AlterOperation src)
        {
            base.AssignFrom(src);
            Options = ((AlterOperation_ChangeDatabaseOptions)src).Options;
            DbName = ((AlterOperation_ChangeDatabaseOptions)src).DbName;
        }
        public override bool DenyTransaction()
        {
            return true;
        }
    }
    public class AlterOperation_RecreateTable : AlterOperation
    {
        //public AlterPlan InnerPlan = new AlterPlan();
        public List<AlterOperation> AlterTableOps = new List<AlterOperation>();

        public override void AssignFrom(AlterOperation src)
        {
            base.AssignFrom(src);
            foreach (var op in ((AlterOperation_RecreateTable)src).AlterTableOps)
            {
                AlterTableOps.Add(op.CloneForStructure(null));
            }
        }

        public override void ChangeStructure(DatabaseStructure s)
        {
            base.ChangeStructure(s);
            foreach (var op in AlterTableOps) op.ChangeStructure(s);
        }

        public void AppendOp(AlterOperation op)
        {
            var rop = op as AlterOperation_RecreateTable;
            if (rop != null) AlterTableOps.AddRange(rop.AlterTableOps);
            else AlterTableOps.Add(op);
        }

        public void InsertOp(AlterOperation op)
        {
            var rop = op as AlterOperation_RecreateTable;
            if (rop != null) AlterTableOps.InsertRange(0, rop.AlterTableOps);
            else AlterTableOps.Insert(0, op);
        }

        public override void Run(IAlterProcessor proc, DbDiffOptions opts)
        {
            var newtbl = new TableStructure(ParentTable);
            var dbs = new DatabaseStructure();
            dbs.Tables.Add(newtbl);
            foreach (var op in AlterTableOps)
            {
                op.Run(dbs, opts);
            }
            proc.RecreateTable(ParentTable, newtbl);
            opts.AlterLogger.Info(Texts.Get("s_recreated$table", "table", ParentTable.FullName));
        }
    }
    public class AlterOperation_CreateTable : AlterOperation_Create
    {
        public DataScript Data;

        public override void TransformToImplementedOps(AlterProcessorCaps caps, DbDiffOptions opts, List<AlterOperation> replacement, AlterPlan plan, IDatabaseSource targetDb)
        {
            if (targetDb != null && !targetDb.Dialect.DialectCaps.UncheckedReferences)
            {
                replacement.Clear();
                replacement.Add(this);
                var table = (TableStructure)NewObject;
                foreach (var cnt in new List<IConstraint>(table.Constraints))
                {
                    var fk = cnt as ForeignKey;
                    if (fk == null) continue;
                    table._Constraints.Remove(cnt);
                    fk.SetDummyTable(table.FullName);
                    replacement.Add(new AlterOperation_CreateConstraint
                    {
                        NewObject = fk
                    });
                }
                return;
            }
            base.TransformToImplementedOps(caps, opts, replacement, plan, targetDb);
        }
        public override void RunNameTransformation(INameTransformation transform)
        {
            base.RunNameTransformation(transform);
            var table = (TableStructure)NewObject;
            table.RunNameTransformation(transform);
        }
        public override void Run(IAlterProcessor proc, DbDiffOptions opts)
        {
            base.Run(proc, opts);
            if (Data != null) proc.UpdateData((TableStructure)NewObject, Data, null);
        }
        public override void AssignFrom(AlterOperation src)
        {
            base.AssignFrom(src);
            Data = ((AlterOperation_CreateTable)src).Data;
        }
    }
    public class AlterOperation_DropTable : AlterOperation_Drop { }
    public class AlterOperation_RenameTable : AlterOperation_Rename { }
    public class AlterOperation_CreateSchema : AlterOperation_Create { }
    public class AlterOperation_DropSchema : AlterOperation_Drop { }
    public class AlterOperation_ChangeSchema : AlterOperation_Change { }
    public class AlterOperation_RenameSchema : AlterOperation_Rename { }

    public class AlterOperation_CustomAction : AlterOperation
    {
        public string Query;
        public int Order;
        public override int OrderGroup
        {
            get { return Order; }
        }
        public override void AssignFrom(AlterOperation src)
        {
            base.AssignFrom(src);
            Query = ((AlterOperation_CustomAction)src).Query;
            Order = ((AlterOperation_CustomAction)src).Order;
        }
        public override string ToString()
        {
            return Query.Trim();
        }
        public override void Run(IAlterProcessor proc, DbDiffOptions opts)
        {
            proc.CustomAction(Query);
        }
    }
}
