using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Xml;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Drawing;

namespace DatAdmin
{
    public abstract class Constraint : TableObjectStructure, IConstraint, IComparable
    {
        [DisplayName("s_name")]
        [ShowInGrid]
        public string Name { get; set; }
        protected virtual string GetNiceName() { return Name; }

        //public string TableName;
        //public string SchemaName;

        //public NameWithSchema Table
        //{
        //    get { return new NameWithSchema(SchemaName, TableName); }
        //    set
        //    {
        //        SchemaName = value.Schema;
        //        TableName = value.Name;
        //    }
        //}

        public Constraint() { }
        public Constraint(IConstraint src)
            : base(src)
        {
            Name = src.Name;
            //Table = src.Table;
            SpecificData.AddAll(src.SpecificData);
        }

        public abstract ConstraintType Type { get; }
        //public abstract string SqlTypeName { get; }

        public abstract Constraint Clone();

        public static Constraint CreateCopy(IConstraint cnt)
        {
            if (cnt is IIndex) return new IndexConstraint((IIndex)cnt);
            if (cnt is IPrimaryKey) return new PrimaryKey((IPrimaryKey)cnt);
            if (cnt is IForeignKey) return new ForeignKey((IForeignKey)cnt);
            if (cnt is IUnique) return new UniqueConstraint((IUnique)cnt);
            if (cnt is ICheck) return new CheckConstraint((ICheck)cnt);
            throw new InternalError("DAE-00036 Unknown constraint type:" + cnt.GetType().ToString());
        }

        public override void SaveToXml(XmlElement xml)
        {
            base.SaveToXml(xml);
            SaveCommon(xml);
        }

        public override void LoadFromXml(XmlElement xml)
        {
            base.LoadFromXml(xml);
            LoadCommon(xml);
        }

        public abstract void Save(XmlElement parentXml);
        public void SaveCommon(XmlElement xml)
        {
            SaveBase(xml);
            if (Name != null) xml.SetAttribute("name", Name);
            XmlTool.SaveSpecificAttributes(SpecificData, "specific.", xml);
        }
        public Constraint(XmlElement xml)
        {
            LoadCommon(xml);
        }

        public void LoadCommon(XmlElement xml)
        {
            LoadBase(xml);
            if (xml.HasAttribute("name")) Name = xml.GetAttribute("name");
            XmlTool.LoadSpecificAttributes(SpecificData, "specific.", xml);
        }

        public override void AssignFrom(IAbstractObjectStructure source)
        {
            base.AssignFrom(source);
            var cnt = source as IConstraint;
            Name = cnt.Name;
            SpecificData.Clear();
            SpecificData.AddAll(source.SpecificData);
        }

        public override void RenameObject(string newname)
        {
            Name = newname;
        }

        public override void SetDummyTable(NameWithSchema name)
        {
            TableStructure table = new TableStructure();
            table.FullName = name;
            table._Constraints.Add(this);
        }

        #region IComparable Members

        public int CompareTo(object obj)
        {
            IConstraint c2 = obj as IConstraint;
            if (c2 != null)
            {
                var tp1 = Type;
                var tp2 = c2.Type;
                if (tp1 != tp2) return (int)(tp1 - tp2);
                return String.Compare(ToString(), obj.SafeToString(), true);
            }
            return String.Compare(ToString(), obj.SafeToString(), true);
        }

        #endregion

        private static Constraint CreateConstraint(string type)
        {
            switch (type)
            {
                case "primarykey":
                    return new PrimaryKey();
                case "foreignkey":
                    return new ForeignKey();
                case "index":
                    return new IndexConstraint();
                case "unique":
                    return new UniqueConstraint();
                case "check":
                    return new CheckConstraint();
            }
            return null;
        }

        public static Constraint FromXml(XmlElement xml, bool oldStyle)
        {
            if (oldStyle)
            {
                switch (xml.Name)
                {
                    case "PrimaryKey":
                        return new PrimaryKey(xml);
                    case "ForeignKey":
                        return new ForeignKey(xml);
                    case "Index":
                        return new IndexConstraint(xml);
                    case "Unique":
                        return new UniqueConstraint(xml);
                    case "Check":
                        return new CheckConstraint(xml);
                }
            }
            else
            {
                var cnt = CreateConstraint(xml.GetAttribute("type"));
                if (cnt == null) return null;
                cnt.LoadFromXml(xml);
                return cnt;
            }
            return null;
        }

        public static string GetTypeTitle(string type)
        {
            switch (type)
            {
                case "primarykey": return "s_primary_key";
                case "foreignkey": return "s_foreign_key";
                case "check": return "s_check";
                case "index": return "s_index";
                case "unique": return "s_unique";
            }
            return null;
        }

        public static Bitmap GetTypeBitmap(string type)
        {
            switch (type)
            {
                case "primarykey": return CoreIcons.primary_key;
                case "foreignkey": return CoreIcons.foreign_key;
                case "check": return CoreIcons.check;
                case "index": return CoreIcons.index;
                case "unique": return CoreIcons.unique;
            }
            return null;
        }
    }

    public class ColumnReference : IColumnReference, IComparable
    {
        [XmlAttrib("name")]
        public string ColumnName { get; set; }
        public Dictionary<string, string> SpecificData { get; set; }

        public ColumnReference(IColumnReference src)
        {
            ColumnName = src.ColumnName;
            SpecificData = new Dictionary<string, string>();
            SpecificData.AddAll(src.SpecificData);
        }

        public void Save(XmlElement xml)
        {
            this.SavePropertiesCore(xml);
            XmlTool.SaveParameters(xml, SpecificData);
        }

        public ColumnReference(string colname)
        {
            SpecificData = new Dictionary<string, string>();
            ColumnName = colname;
        }

        public ColumnReference(string colname, Dictionary<string, string> specData)
        {
            ColumnName = colname;
            SpecificData = new Dictionary<string, string>();
            SpecificData.AddAll(specData);
        }

        public ColumnReference(XmlElement xml)
        {
            this.LoadPropertiesCore(xml);
            SpecificData = XmlTool.LoadParameters(xml);
        }

        #region IComparable Members

        public int CompareTo(object obj)
        {
            if (obj == null) return 0;
            return String.Compare(ToString(), obj.ToString(), true);
        }

        #endregion

        public override string ToString()
        {
            return ColumnName;
        }

        public override bool Equals(object obj)
        {
            var cr = obj as ColumnReference;
            if (cr != null)
            {
                if (ColumnName != cr.ColumnName) return false;
                if (!SpecificData.EqualsDictionary(cr.SpecificData)) return false;
                return true;
            }
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return ColumnName.GetHashCode();
        }

        public static List<IColumnReference> CopyList(IEnumerable<IColumnReference> list)
        {
            var res = new List<IColumnReference>();
            foreach (var col in list) res.Add(new ColumnReference(col));
            return res;
        }
    }

    public abstract class ColumnsConstraint : Constraint, IColumnsConstraint
    {
        public List<IColumnReference> Columns = new List<IColumnReference>();

        public ColumnsConstraint() { }
        public ColumnsConstraint(IColumnsConstraint src)
            : base(src)
        {
            foreach (var col in src.Columns)
            {
                Columns.Add(new ColumnReference(col));
            }
        }

        #region IColumnsConstraint Members

        List<IColumnReference> IColumnsConstraint.Columns
        {
            get { return Columns; }
        }

        #endregion

        public override void AssignFrom(IAbstractObjectStructure source)
        {
            base.AssignFrom(source);
            var cnt = source as IColumnsConstraint;
            Columns = ColumnReference.CopyList(cnt.Columns);
        }

        public void SaveColumns(XmlElement xml)
        {
            SaveColumns(xml, Columns, "Column");
        }
        public static void SaveColumns(XmlElement xml, List<IColumnReference> columns, string elemName)
        {
            foreach (ColumnReference col in columns)
            {
                col.Save(xml.AddChild(elemName));
            }
        }
        public static void LoadColumns(XmlElement xml, List<IColumnReference> columns, string attrname, string elemname)
        {
            if (xml.HasAttribute(attrname))
            {
                // legaqcy code - load old style column list
                string val = xml.GetAttribute(attrname);
                foreach (string item in val.Split(',')) columns.Add(new ColumnReference(item));
            }
            else
            {
                foreach (XmlElement x in xml.SelectNodes(elemname))
                {
                    columns.Add(new ColumnReference(x));
                }
            }
        }
        public override void SaveToXml(XmlElement xml)
        {
            base.SaveToXml(xml);
            SaveColumns(xml);
        }
        public void LoadColumns(XmlElement xml)
        {
            LoadColumns(xml, Columns, "columns", "Column");
        }
        public override void LoadFromXml(XmlElement xml)
        {
            base.LoadFromXml(xml);
            LoadColumns(xml);
        }

        public override void NotifyRenameColumn(NameWithSchema table, string oldcol, string newcol)
        {
            base.NotifyRenameColumn(table, oldcol, newcol);
            if (Table.FullName == table)
            {
                for (int i = 0; i < Columns.Count; i++) if (Columns[i].ColumnName == oldcol) ((ColumnReference)Columns[i]).ColumnName = newcol;
            }
        }
        public override void NotifyDropColumn(IColumnStructure column)
        {
            base.NotifyDropColumn(column);
            if (Table == column.Table)
            {
                Columns.RemoveIf(c => c.ColumnName == column.ColumnName);
            }
        }
        public ColumnsConstraint(XmlElement xml) : base(xml) { }
        public override string ToString()
        {
            try
            {
            return String.Format("{0}:{1}({2})", Name, Table, (from c in Columns select c.ColumnName).CreateDelimitedText(","));
        }
            catch
            {
                return base.ToString();
    }
        }
    }

    public class PrimaryKey : ColumnsConstraint, IPrimaryKey
    {
        public PrimaryKey() { }
        public PrimaryKey(IPrimaryKey src)
            : base(src)
        {
        }
        public override Constraint Clone() { return new PrimaryKey(this); }
        public override ConstraintType Type { get { return ConstraintType.PrimaryKey; } }
        public override void Save(XmlElement parentXml)
        {
            XmlElement xml = XmlTool.AddChild(parentXml, "PrimaryKey");
            SaveColumns(xml);
            SaveCommon(xml);
        }
        public override void SaveToXml(XmlElement xml)
        {
            base.SaveToXml(xml);
            xml.SetAttribute("type", "primarykey");
        }
        public PrimaryKey(XmlElement xml)
            : base(xml)
        {
            LoadColumns(xml);
        }
        protected override string GetNiceName()
        {
            return Name ?? "PK";
        }
        public override FullDatabaseRelatedName GetName()
        {
            return new FullDatabaseRelatedName
            {
                ObjectName = Table != null ? Table.FullName : null,
                ObjectType = "primarykey",
                SubName = Name
            };
        }
    }

    public class IndexConstraint : ColumnsConstraint, IIndex
    {
        bool m_isunique;

        public IndexConstraint() { }
        public IndexConstraint(IIndex src)
            : base(src)
        {
            IsUnique = src.IsUnique;
        }
        public override Constraint Clone() { return new IndexConstraint(this); }
        public override ConstraintType Type { get { return ConstraintType.Index; } }
        public override void Save(XmlElement parentXml)
        {
            XmlElement xml = XmlTool.AddChild(parentXml, "Index");
            SaveColumns(xml);
            SaveCommon(xml);
        }
        public IndexConstraint(XmlElement xml)
            : base(xml)
        {
            LoadColumns(xml);
        }

        [XmlAttrib("unique")]
        [DisplayName("s_is_unique")]
        [TypeConverter(typeof(YesNoTypeConverter))]
        public bool IsUnique
        {
            get { return m_isunique; }
            set { m_isunique = value; }
        }

        public override void SaveToXml(XmlElement xml)
        {
            base.SaveToXml(xml);
            xml.SetAttribute("type", "index");
        }

        public override FullDatabaseRelatedName GetName()
        {
            return new FullDatabaseRelatedName
            {
                ObjectName = Table != null ? Table.FullName : null,
                ObjectType = "index",
                SubName = Name
            };
        }
    }

    public class UniqueConstraint : ColumnsConstraint, IUnique
    {
        public UniqueConstraint() { }
        public UniqueConstraint(IUnique src)
            : base(src)
        {
        }
        public override Constraint Clone() { return new UniqueConstraint(this); }
        public override ConstraintType Type { get { return ConstraintType.Unique; } }
        public override void Save(XmlElement parentXml)
        {
            XmlElement xml = XmlTool.AddChild(parentXml, "Unique");
            SaveColumns(xml);
            SaveCommon(xml);
        }
        public UniqueConstraint(XmlElement xml)
            : base(xml)
        {
            LoadColumns(xml);
        }
        public override void SaveToXml(XmlElement xml)
        {
            base.SaveToXml(xml);
            xml.SetAttribute("type", "unique");
        }

        public override FullDatabaseRelatedName GetName()
        {
            return new FullDatabaseRelatedName
            {
                ObjectName = Table != null ? Table.FullName : null,
                ObjectType = "unique",
                SubName = Name
            };
        }
    }

    public class CheckConstraint : Constraint, ICheck
    {
        [XmlAttrib("expr")]
        public string Expression { get; set; }

        public CheckConstraint() { }
        public CheckConstraint(ICheck src)
            : base(src)
        {
            Expression = src.Expression;
        }
        public override Constraint Clone() { return new CheckConstraint(this); }
        public override ConstraintType Type { get { return ConstraintType.Check; } }

        #region ICheck Members

        string ICheck.Expression
        {
            get { return Expression; }
        }

        #endregion
        public override void Save(XmlElement parentXml)
        {
            XmlElement xml = XmlTool.AddChild(parentXml, "Check");
            //xml.SetAttribute("expr", Expression);
            SaveCommon(xml);
        }
        public CheckConstraint(XmlElement xml)
            : base(xml)
        {
            //Expression = xml.GetAttribute("expr");
        }

        public override string ToString()
        {
            return String.Format("{0}:{1} ({2})", Name, Table, Expression);
        }

        public override void SaveToXml(XmlElement xml)
        {
            base.SaveToXml(xml);
            xml.SetAttribute("type", "check");
        }

        public override FullDatabaseRelatedName GetName()
        {
            return new FullDatabaseRelatedName
            {
                ObjectName = Table != null ? Table.FullName : null,
                ObjectType = "check",
                SubName = Name
            };
        }
    }

    public class ForeignKey : ColumnsConstraint, IForeignKey
    {
        public List<IColumnReference> PrimaryKeyColumns = new List<IColumnReference>();

        [XmlAttrib("ondelete")]
        public ForeignKeyAction OnDeleteAction { get; set; }
        [XmlAttrib("onupdate")]
        public ForeignKeyAction OnUpdateAction { get; set; }

        internal bool m_addedAsReference; // helper flag for database analyser

        private void Initialize()
        {
            OnDeleteAction = ForeignKeyAction.Undefined;
            OnUpdateAction = ForeignKeyAction.Undefined;
        }

        public ForeignKey() { }
        public ForeignKey(IForeignKey src)
            : base(src)
        {
            PrimaryKeyTable = src.PrimaryKeyTable;
            //Table = src.Table;
            foreach (var col in src.PrimaryKeyColumns) PrimaryKeyColumns.Add(new ColumnReference(col));
            OnUpdateAction = src.OnUpdateAction;
            OnDeleteAction = src.OnDeleteAction;
        }

        #region IForeignKey Members

        NameWithSchema IForeignKey.PrimaryKeyTable
        {
            get { return PrimaryKeyTable; }
        }

        IList<IColumnReference> IForeignKey.PrimaryKeyColumns
        {
            get { return PrimaryKeyColumns; }
        }

        ForeignKeyAction IForeignKey.OnDeleteAction
        {
            get { return OnDeleteAction; }
        }

        ForeignKeyAction IForeignKey.OnUpdateAction
        {
            get { return OnUpdateAction; }
        }

        #endregion

        public override Constraint Clone() { return new ForeignKey(this); }
        public override ConstraintType Type { get { return ConstraintType.ForeignKey; } }

        public NameWithSchema PrimaryKeyTable { get; set; }

        public string PrimaryKeyTableName
        {
            get
            {
                if (PrimaryKeyTable != null) return PrimaryKeyTable.Name;
                return null;
            }
        }
        public string PrimaryKeyTableSchema
        {
            get
            {
                if (PrimaryKeyTable != null) return PrimaryKeyTable.Schema;
                return null;
            }
        }

        //{
        //    get { return new NameWithSchema(PrimaryKeyTableSchema, PrimaryKeyTableName); }
        //    set
        //    {
        //        PrimaryKeyTableName = value.Name;
        //        PrimaryKeyTableSchema = value.Schema;
        //    }
        //}

        [Browsable(false)]
        [DisplayName("s_references")]
        [ShowInGrid]
        public string PrimaryKeyTableFullName
        {
            get { return PrimaryKeyTable.ToString(); }
            set { PrimaryKeyTable = new NameWithSchema(value); }
        }

        public override void AssignFrom(IAbstractObjectStructure source)
        {
            base.AssignFrom(source);
            var fk = source as IForeignKey;
            OnUpdateAction = fk.OnUpdateAction;
            OnDeleteAction = fk.OnDeleteAction;
            PrimaryKeyTable = fk.PrimaryKeyTable;
            PrimaryKeyColumns = ColumnReference.CopyList(fk.PrimaryKeyColumns);
        }

        public override void Save(XmlElement parentXml)
        {
            XmlElement xml = XmlTool.AddChild(parentXml, "ForeignKey");
            SaveColumns(xml);
            SaveFkData(xml);
            SaveCommon(xml);
        }

        private void SaveFkData(XmlElement xml)
        {
            xml.SetAttribute("pktable", PrimaryKeyTableName);
            if (PrimaryKeyTableSchema != null) xml.SetAttribute("pkschema", PrimaryKeyTableSchema);
            SaveColumns(xml, PrimaryKeyColumns, "PkColumn");
        }

        public ForeignKey(XmlElement xml)
            : base(xml)
        {
            LoadColumns(xml);
            LoadFkData(xml);
        }

        public override void LoadFromXml(XmlElement xml)
        {
            base.LoadFromXml(xml);
            LoadFkData(xml);
        }

        private void LoadFkData(XmlElement xml)
        {
            LoadColumns(xml, PrimaryKeyColumns, "pkcolumns", "PkColumn");
            string name = xml.GetAttribute("pktable");
            string schema = null;
            if (xml.HasAttribute("pkschema")) schema = xml.GetAttribute("pkschema");
            PrimaryKeyTable = new NameWithSchema(schema, name);
        }

        public override string ToString()
        {
            return String.Format("{0}:{1}->{2}", Name, Table, PrimaryKeyTable);
        }

        public override void NotifyRenameTable(NameWithSchema oldName, NameWithSchema newName)
        {
            base.NotifyRenameTable(oldName, newName);
            if (PrimaryKeyTable == oldName) PrimaryKeyTable = newName;
        }
        public override void NotifyRenameColumn(NameWithSchema table, string oldcol, string newcol)
        {
            base.NotifyRenameColumn(table, oldcol, newcol);
            if (PrimaryKeyTable == table)
            {
                for (int i = 0; i < PrimaryKeyColumns.Count; i++) if (PrimaryKeyColumns[i].ColumnName == oldcol) ((ColumnReference)PrimaryKeyColumns[i]).ColumnName = newcol;
            }
        }
        public override void NotifyDropColumn(IColumnStructure column)
        {
            base.NotifyDropColumn(column);
            if (PrimaryKeyTable == column.Table)
            {
                PrimaryKeyColumns.RemoveIf(c => c.ColumnName == column.ColumnName);
            }
        }

        public void RunNameTransformation(INameTransformation nameTransform)
        {
            PrimaryKeyTable = nameTransform.RenameObject(PrimaryKeyTable, "table");
            PrimaryKeyColumns = new List<IColumnReference>(PrimaryKeyColumns.MapEach(
                c => (IColumnReference)new ColumnReference(nameTransform.RenameColumn(PrimaryKeyTable, c.ColumnName), c.SpecificData)
                    ));
        }

        public override void SaveToXml(XmlElement xml)
        {
            base.SaveToXml(xml);
            SaveFkData(xml);
            xml.SetAttribute("type", "foreignkey");
        }

        public override FullDatabaseRelatedName GetName()
        {
            return new FullDatabaseRelatedName
            {
                ObjectName = Table != null ? Table.FullName : null,
                ObjectType = "foreignkey",
                SubName = Name
            };
        }
    }
}
