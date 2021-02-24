using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Xml;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace DatAdmin
{
    public class TableStructure : DatabaseObjectStructure, ITableStructure
    {
        public TableStructureMembers FilledMembers { get; set; }

        public IColumnCollection Columns { get; private set; }
        public IConstraintCollection Constraints { get; private set; }
        //public IForeignKeyCollection ReferencedFrom { get; private set; }
        //public string Name = null;
        //public string SchemaName = null;
        //public ColumnCollection Columns;
        //public DatabaseStructure Database;
        //public List<DatAdmin.Constraint> Constraints = new List<DatAdmin.Constraint>();
        //public List<ForeignKey> ReferencedFrom = new List<ForeignKey>();

        public ColumnCollection _Columns { get { return (ColumnCollection)Columns; } }
        public ConstraintCollection _Constraints { get { return (ConstraintCollection)Constraints; } }
        //public ForeignKeyCollection _ReferencedFrom { get { return (ForeignKeyCollection)ReferencedFrom; } }

        public InMemoryTable FixedData { get; set; }

        public TableStructure()
        {
            Initialize();
            FilledMembers = TableStructureMembers.None;
        }

        private void Initialize()
        {
            Columns = new ColumnCollection(this);
            Constraints = new ConstraintCollection(this);
            //ReferencedFrom = new ForeignKeyCollection(this);
        }

        public TableStructure(ITableStructure table)
            : base(table)
        {
            Initialize();
            LoadFrom(table);
            FilledMembers = TableStructureMembers.None;
        }

        public NameWithSchema FullName { get; set; }

        [ShowInGrid]
        [DisplayName("s_name")]
        public string GridName
        {
            get { return Name; }
            set
            {
                var db = Database as DatabaseStructure;
                if (db != null) db.RenameTable(FullName, value);
            }
        }

        [ShowInGrid]
        [DisplayName("s_schema")]
        public string GridSchemaName
        {
            get { return SchemaName; }
            set
            {
                var db = Database as DatabaseStructure;
                if (db != null) db.ChangeTableSchema(FullName, value);
            }
        }

        [DisplayName("s_name")]
        public string Name
        {
            get { return FullName.Name; }
            set { FullName = new NameWithSchema(FullName.Schema, value); }
        }

        [DisplayName("s_schema")]
        public string SchemaName
        {
            get { return FullName.Schema; }
            set { FullName = new NameWithSchema(value, FullName.Name); }
        }

        //public NameWithSchema FullName
        //{
        //    get { return new NameWithSchema(SchemaName, Name); }
        //    set { SchemaName = value.Schema; Name = value.Name; }
        //}

        //#region ITableStructure Members

        //IColumnCollection ITableStructure.Columns
        //{
        //    get { return Columns; }
        //}

        //IList<IConstraint> ITableStructure.Constraints
        //{
        //    get { return Constraints.ToTypedList<IConstraint>(); }
        //}

        //IList<IForeignKey> ITableStructure.ReferencedFrom
        //{
        //    get { return ReferencedFrom.ToTypedList<IForeignKey>(); }
        //}

        ////TableStructureMembers ITableStructure.FilledMembers
        ////{
        ////    get { return FilledMembers; }
        ////}

        //#endregion

        public ColumnStructure AddColumn(string name, DbTypeBase datatype)
        {
            ColumnStructure col = new ColumnStructure();
            col.ColumnName = name;
            col.DataType = datatype;
            _Columns.Add(col);
            return col;
        }

        public ColumnStructure AddColumn(IColumnStructure source, bool reuseGrouId)
        {
            ColumnStructure col = new ColumnStructure(source);
            if (!reuseGrouId) col.GroupId = Guid.NewGuid().ToString();
            _Columns.Add(col);
            return col;
        }

        public void LoadFrom(ITableStructure table)
        {
            _Columns.Clear();
            _Constraints.Clear();
            //_ReferencedFrom.Clear();
            FullName = table.FullName;
            Comment = table.Comment;
            //Name = table.FullName.Name;
            //SchemaName = table.FullName.Schema;
            foreach (IColumnStructure col in table.Columns)
            {
                _Columns.Add(new ColumnStructure(col));
            }
            foreach (IConstraint cnt in table.Constraints)
            {
                _Constraints.Add(Constraint.CreateCopy(cnt));
            }
            //foreach (IForeignKey fk in table.ReferencedFrom)
            //{
            //    _ReferencedFrom.Add(new ForeignKey(fk));
            //}
            SpecificData.AddAll(table.SpecificData);
            FixedData = table.FixedData;
            //FilledMembers = table.FilledMembers;
        }

        public void RemoveConstraints<T>() where T : IConstraint
        {
            ((ConstraintCollection)Constraints).RemoveIf(c => c is T);
        }

        public void Save(XmlElement xml)
        {
            SaveToXml(xml, true);
        }

        public TableStructure(XmlElement xml)
        {
            Initialize();
            LoadFromXml(xml, true);
        }

        private void LoadFromXml(XmlElement xml, bool oldStyle)
        {
            LoadBase(xml);
            FullName = NameWithSchema.LoadFromXml(xml);
            if (oldStyle)
            {
                foreach (XmlElement child in xml)
                {
                    var cnt = Constraint.FromXml(child, true);
                    if (cnt != null) _Constraints.Add(cnt);
                    else if (child.Name == "Column") _Columns.Add(new ColumnStructure(child));
                }
            }
            else
            {
                foreach (XmlElement child in xml.SelectNodes("Column"))
                {
                    _Columns.Add(new ColumnStructure(child));
                }
                foreach (XmlElement child in xml.SelectNodes("Constraint"))
                {
                    _Constraints.Add(Constraint.FromXml(child, false));
                }
            }
            SpecificData = XmlTool.LoadParameters(xml);
            if (xml.FindElement("Comment") != null) Comment = xml.FindElement("Comment").InnerText;
            if (xml.FindElement("FixedData") != null) FixedData = new InMemoryTable(this, xml.FindElement("FixedData"));
        }

        private void SaveToXml(XmlElement xml, bool oldStyle)
        {
            SaveBase(xml);
            if (FullName != null) FullName.SaveToXml(xml);
            foreach (ColumnStructure col in Columns)
            {
                XmlElement cx = XmlTool.AddChild(xml, "Column");
                col.Save(cx);
            }
            if (oldStyle)
            {
                foreach (DatAdmin.Constraint cnt in Constraints)
                {
                    cnt.Save(xml);
                }
            }
            else
            {
                foreach (DatAdmin.Constraint cnt in Constraints)
                {
                    XmlElement cx = XmlTool.AddChild(xml, "Constraint");
                    cnt.SaveToXml(cx);
                }
            }
            XmlTool.SaveParameters(xml, SpecificData);
            if (!String.IsNullOrEmpty(Comment)) xml.AddChild("Comment").InnerText = Comment;
            if (FixedData != null) FixedData.SaveToXml(xml.AddChild("FixedData"));
        }

        public override void SaveToXml(XmlElement xml)
        {
            //base.SaveToXml(xml); - SaveBase is called in private SaveToXml
            SaveToXml(xml, false);
        }

        public override string ToString()
        {
            return FullName.ToString();
        }

        //internal void AfterLoadFix()
        //{
        //    foreach (Constraint cnt in Constraints)
        //    {
        //        cnt.Table = FullName;
        //    }
        //}

        public static ITableStructure FromName(NameWithSchema name)
        {
            TableStructure res = new TableStructure();
            res.FullName = name;
            //res.FilledMembers = TableStructureMembers.Name;
            return res;
        }

        public void RepairInconsistency()
        {
            List<Constraint> deleteConstraints = new List<Constraint>();
            foreach (Constraint cnt in Constraints)
            {
                var colc = cnt as ColumnsConstraint;
                if (colc != null)
                {
                    colc.Columns.RemoveIf(col => !Columns.ExistsEx(c => c.ColumnName == col.ColumnName));
                    //List<string> delcol = new List<string>();
                    //foreach (var col in colc.Columns)
                    //{
                    //    if (!Columns.Exists(c => c.ColumnName == col.ColumnName)) delcol.Add(col);
                    //}
                    //foreach (string col in delcol) colc.Columns.Remove(col);
                    if (colc.Columns.Count == 0) deleteConstraints.Add(colc);
                }
            }
            foreach (Constraint cnt in deleteConstraints) _Constraints.Remove(cnt);
        }

        public override void AddAllObjects(List<IAbstractObjectStructure> res)
        {
            base.AddAllObjects(res);
            foreach (ColumnStructure col in Columns) col.AddAllObjects(res);
            foreach (Constraint cnt in Constraints) cnt.AddAllObjects(res);
        }

        public void RunNameTransformation(INameTransformation nameTransform)
        {
            FullName = nameTransform.RenameObject(FullName, "table");
            foreach (ColumnStructure col in Columns)
            {
                col.ColumnName = nameTransform.RenameColumn(FullName, col.ColumnName);
            }
            foreach (Constraint cnt in Constraints)
            {
                var col = cnt as ColumnsConstraint;
                if (col != null)
                {
                    col.Columns = new List<IColumnReference>(col.Columns.MapEach(
                        c => (IColumnReference)new ColumnReference(nameTransform.RenameColumn(FullName, c.ColumnName), c.SpecificData)
                        ));
                }
                //cnt.Table = FullName;
                cnt.Name = nameTransform.RenameConstraint(cnt);
                var fk = cnt as ForeignKey;
                if (fk != null)
                {
                    fk.RunNameTransformation(nameTransform);
                }
            }
        }

        public override void NotifyRenameTable(NameWithSchema oldName, NameWithSchema newName)
        {
            if (FullName == oldName) FullName = newName;
        }

        public override void NotifyDropColumn(IColumnStructure column)
        {
            if (FullName == column.Table.FullName) _Columns.RemoveIf(c => c.ColumnName == column.ColumnName);
        }

        public override void RenameObject(string newname)
        {
            FullName = new NameWithSchema(FullName.Schema, newname);
        }

        public override void SetObjectSchema(string newschema)
        {
            FullName = new NameWithSchema(newschema, FullName.Name);
        }

        //public void ChangeColumnDefinition(IColumnStructure newcol)
        //{
        //    _Columns.ReplaceIf(col => col.ColumnName == newcol.ColumnName, new ColumnStructure(newcol));
        //}

        public void ReorderColumns(List<string> newColumnOrder)
        {
            var cols = Columns.ToDictionary(c => c.ColumnName);
            if (newColumnOrder.Count != cols.Count) throw new InternalError("DAE-00038 reorder must have the same number of columns");
            _Columns.Clear();
            foreach (string col in newColumnOrder)
            {
                _Columns.Add(cols[col]);
            }
        }

        //public override void NotifyChangeColumnType_RefsOnly(IColumnStructure newcol)
        //{
        //    foreach (var fk in this.GetConstraints<IForeignKey>())
        //    {
        //        if (fk.PrimaryKeyTable == newcol.Table.FullName)
        //        {
        //            for (int i = 0; i < fk.Columns.Count; i++)
        //            {
        //                if (fk.PrimaryKeyColumns[i].ColumnName == newcol.ColumnName)
        //                {
        //                    // use the same type as referenced column
        //                    ((ColumnStructure)Columns[fk.Columns[i].ColumnName]).DataType = newcol.DataType;
        //                }
        //            }
        //        }
        //    }
        //}

        public void CleanUp(Dictionary<NameWithSchema, TableStructure> existingTables)
        {
            var delcnt = new List<IConstraint>();
            foreach (Constraint cnt in Constraints)
            {
                if (cnt.Table.FullName != FullName) delcnt.Add(cnt);
                var colc = cnt as ColumnsConstraint;
                if (colc != null)
                {
                    foreach (var c in colc.Columns) if (Columns.GetIndex(c.ColumnName) < 0) delcnt.Add(cnt);
                }
                var fkc = cnt as ForeignKey;
                if (fkc != null)
                {
                    if (existingTables.ContainsKey(fkc.PrimaryKeyTable))
                    {
                        var pkt = existingTables[fkc.PrimaryKeyTable];
                        foreach (var c in fkc.PrimaryKeyColumns) if (pkt.Columns.GetIndex(c.ColumnName) < 0) delcnt.Add(cnt);
                    }
                    else
                    {
                        delcnt.Add(cnt);
                    }
                }
            }
            _Constraints.RemoveIf(delcnt.Contains);
        }

        public void RenameColumn(string oldName, string newName)
        {
            IColumnStructure oldCol = _Columns.First(c => c.ColumnName == oldName);
            foreach (AbstractObjectStructure obj in this.GetAllObjects())
            {
                obj.NotifyRenameColumn(FullName, oldCol.ColumnName, newName);
            }
        }

        public void UpdateData(DataScript script)
        {
            if (FixedData == null) FixedData = new InMemoryTable(this);
            FixedData = new InMemoryTable(FixedData, script);
        }

        public void AddReference(ForeignKey fk)
        {
            ((DatabaseStructure)Database).FindOrCreateTable(fk.Table.FullName)._Constraints.Add(fk);
            fk.m_addedAsReference = true;
        }

        private bool ShouldCopy(ITableStructure src, TableStructureMembers member)
        {
            return !FilledMembers.ContainsAll(member) && src.FilledMembers.ContainsAll(member);
        }

        protected void CopyFromTable(ITableStructure src, bool hardAssign)
        {
            if (hardAssign || ShouldCopy(src, TableStructureMembers.SpecificDetails))
            {
                base.AssignFrom(src);
            }
            if (hardAssign || ShouldCopy(src, TableStructureMembers.ColumnNames))
            {
                var oldcols = new List<ColumnStructure>();
                foreach (ColumnStructure c in _Columns) oldcols.Add(c);
                _Columns.Clear();
                foreach (var col in src.Columns)
                {
                    ColumnStructure scol = (from c in oldcols where c.ColumnName == col.ColumnName select c).FirstOrDefault();
                    if (scol != null)
                    {
                        scol.AssignFrom(col);
                    }
                    else
                    {
                        scol = new ColumnStructure(col);
                    }
                    _Columns.Add(scol);
                }
            }
            if (hardAssign || ShouldCopy(src, TableStructureMembers.ColumnTypes))
            {
                foreach (var col in src.Columns)
                {
                    ColumnStructure scol = this.FindColumn(col.ColumnName) as ColumnStructure;
                    if (scol != null)
                    {
                        scol.AssignFrom(col);
                    }
                }
            }
            if (hardAssign || ShouldCopy(src, TableStructureMembers.Constraints))
            {
                foreach (var cnt in src.Constraints)
                {
                    Constraint scnt = this.FindConstraint(cnt) as Constraint;
                    if (scnt != null)
                    {
                        scnt.AssignFrom(scnt);
                    }
                    else
                    {
                        _Constraints.Add(Constraint.CreateCopy(cnt));
                    }
                }
            }
        }

        public override void MergeFrom(IAbstractObjectStructure obj)
        {
            var tbl = obj as ITableStructure;
            CopyFromTable(tbl, false);
        }

        public override void AssignFrom(IAbstractObjectStructure obj)
        {
            CopyFromTable(obj as ITableStructure, true);
        }

        public void LoadStructure(TableStructureMembers members, IDatabaseSource db)
        {
            if (db == null) return; // db not provided, perhaps all is loaded
            if ((members & FilledMembers) == members) return; // all is loaded
            var ts = db.InvokeLoadTableStructure(FullName, members) as TableStructure;
            MergeFrom(ts);
            // merge references, not very nice...
            if (ShouldCopy(ts, TableStructureMembers.ReferencedFrom))
            {
                foreach (var fk in ts.GetReferencedFrom())
                {
                    var pkt = ((DatabaseStructure)Database).FindOrCreateTable(fk.Table.FullName);
                    if (pkt.FindConstraint(fk) == null)
                    {
                        pkt._Constraints.Add(new ForeignKey(fk));
                    }
                }
            }
            FilledMembers |= members;
        }

        internal void DropEmptyConstraints()
        {
            _Constraints.RemoveIf(cnt => cnt is IColumnsConstraint && ((IColumnsConstraint)cnt).Columns.Count == 0);
            _Constraints.RemoveIf(cnt => cnt is IForeignKey && ((IForeignKey)cnt).PrimaryKeyColumns.Count == 0);
        }

        internal void DropReferencesTo(NameWithSchema tableName)
        {
            _Constraints.RemoveIf(cnt => cnt is IForeignKey && ((IForeignKey)cnt).PrimaryKeyTable == tableName);
        }

        public override FullDatabaseRelatedName GetName()
        {
            return new FullDatabaseRelatedName
            {
                ObjectName = FullName,
                ObjectType = "table",
            };
        }
    }
}
