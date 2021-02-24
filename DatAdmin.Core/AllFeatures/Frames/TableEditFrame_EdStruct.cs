using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Linq;

namespace DatAdmin
{
    public class EditorColumnStructure : ICustomTypeDescriptor
    {
        internal string m_originalGroupId = null;
        internal TableEditFrame m_frame;
        private IDomainStructure m_domain = null;

        string m_columnName = "";
        [DisplayName("s_name")]
        public string ColumnName
        {
            get { return m_columnName; }
            set
            {
                if (IsOld && !m_frame.AllowRenameColumn)
                {
                    throw new CannotChangeTablePropertyError("DAE-00166");
                }
                m_columnName = value;
            }
        }

        [Browsable(false)]
        public bool IsNew { get { return m_originalGroupId == null; } }

        [Browsable(false)]
        public bool IsOld { get { return m_originalGroupId != null; } }

        object m_dataType;
        [TypeConverter(typeof(ExpandableObjectConverter))]
        [Browsable(false)]
        public object DataType
        {
            get { return m_dataType; }
            set { m_dataType = value; }
        }

        [Browsable(false)]
        public IDomainStructure Domain
        {
            get { return m_domain; }
            set { m_domain = value; }
        }

        bool m_isNullable;
        [TypeConverter(typeof(YesNoTypeConverter))]
        [DisplayName("s_nullable")]
        public bool IsNullable
        {
            get { return m_isNullable; }
            set
            {
                if (IsOld && !m_frame.AllowChangeColumnType)
                {
                    throw new CannotChangeTablePropertyError("DAE-00167");
                }
                m_isNullable = value;
            }
        }
        [Browsable(false)]
        public ColumnStructure Source;

        string m_defaultValue = "";
        [DisplayName("s_default_value")]
        public string DefaultValue
        {
            get { return m_defaultValue; }
            set
            {
                if (IsOld && !m_frame.AllowChangeColumnDefaultValue)
                {
                    throw new CannotChangeTablePropertyError("DAE-00168");
                }
                m_defaultValue = value;
            }
        }

        public string[] GetCharacterSets() { return m_frame.CharacterSets; }
        public string[] GetCollations() { return m_frame.Collations; }

        string m_charset;
        [DisplayName("s_character_set")]
        [TypeConverter(typeof(ListTypeConverter))]
        [ListMethod("GetCharacterSets")]
        public string CharacterSet
        {
            get { return m_charset; }
            set
            {
                if (IsOld && !m_frame.AllowChangeColumnType)
                {
                    throw new CannotChangeTablePropertyError("DAE-00169");
                }
                m_charset = value;
            }
        }

        string m_collation;
        [DisplayName("s_collation")]
        [TypeConverter(typeof(ListTypeConverter))]
        [ListMethod("GetCollations")]
        public string Collation
        {
            get { return m_collation; }
            set
            {
                if (IsOld && !m_frame.AllowChangeColumnType)
                {
                    throw new CannotChangeTablePropertyError("DAE-00170");
                }
                m_collation = value;
            }
        }

        public EditorColumnStructure(TableEditFrame frame, ColumnStructure src, ISqlDialect dialect)
        {
            m_frame = frame;
            if (src.Domain != null) Domain = m_frame.Domains[src.Domain];
            ColumnName = src.ColumnName;
            IsNullable = src.IsNullable;
            CharacterSet = src.CharacterSet;
            Collation = src.Collation;
            if (src.DefaultValue != null) DefaultValue = src.DefaultValue.GenerateSql(dialect, src.DataType, null);
            m_originalGroupId = src.GroupId;
            if (dialect == null)
            {
                DataType = src.DataType;
            }
            else
            {
                DataType = dialect.GenericTypeToSpecific(src.DataType);
            }
            Source = src;
        }

        public EditorColumnStructure(TableEditFrame frame)
        {
            m_frame = frame;
        }

        public void SetTable(TableStructure table)
        {
        }

        #region "Implements ICustomTypeDescriptor"

        public System.ComponentModel.AttributeCollection GetAttributes()
        {
            return TypeDescriptor.GetAttributes(this, true);
        }

        public string GetClassName()
        {
            return TypeDescriptor.GetClassName(this, true);
        }

        public string GetComponentName()
        {
            return TypeDescriptor.GetComponentName(this, true);
        }

        public System.ComponentModel.TypeConverter GetConverter()
        {
            return TypeDescriptor.GetConverter(this, true);
        }

        public System.ComponentModel.EventDescriptor GetDefaultEvent()
        {
            return TypeDescriptor.GetDefaultEvent(this, true);
        }

        public System.ComponentModel.PropertyDescriptor GetDefaultProperty()
        {
            return TypeDescriptor.GetDefaultProperty(this, true);
        }

        public object GetEditor(System.Type editorBaseType)
        {
            return TypeDescriptor.GetEditor(this, editorBaseType, true);
        }

        public System.ComponentModel.EventDescriptorCollection GetEvents()
        {
            return TypeDescriptor.GetEvents(this, true);
        }

        public System.ComponentModel.EventDescriptorCollection GetEvents(System.Attribute[] attributes)
        {
            return TypeDescriptor.GetEvents(this, attributes, true);
        }

        public System.ComponentModel.PropertyDescriptorCollection GetProperties()
        {
            return TypeDescriptor.GetProperties(this, true);
        }

        public System.ComponentModel.PropertyDescriptorCollection GetProperties(System.Attribute[] attributes)
        {
            PropertyDescriptorCollection src = TypeDescriptor.GetProperties(this, attributes, true);
            PropertyDescriptorCollection srcType = TypeDescriptor.GetProperties(DataType, attributes, true);

            PropertyDescriptorCollection res = new PropertyDescriptorCollection(null);

            var caps = m_frame.DatabaseConnection.AlterCaps;

            foreach (PropertyDescriptor desc in src)
            {
                var d = new ModifiedPropertyDescriptor(desc, PropertyPageBase.GetDisplayName(desc));
                res.Add(d);
            }
            if (Domain == null)
            {
                foreach (PropertyDescriptor desc in srcType)
                {
                    var d = new ReferencedPropertyDescriptor(desc, PropertyPageBase.GetDisplayName(desc), DataType);
                    bool isidentity = (from Attribute a in desc.Attributes where a.GetType() == typeof(IsIdentityAttribute) select a).Any();
                    if (IsOld && (!isidentity && !caps.ChangeColumnType || isidentity && !caps.ChangeAutoIncrement))
                    {
                        d.ReadonlyOverride = true;
                    }

                    res.Add(d);
                }
            }
            return res;
        }

        public object GetPropertyOwner(System.ComponentModel.PropertyDescriptor pd)
        {
            return this;
        }

        #endregion

        public void CheckConsistency()
        {
            if (ColumnName.IsEmpty()) throw new InconsistentTableStructureError("DAE-00171 Empty column name");
        }
    }

    public enum EditorIndexOrKeyType { PrimaryKey, UniqueConstraint, Index };

    public abstract class EditorConstraintBase : PropertyPageBase
    {
        internal TableEditFrame m_frame;
        internal NameWithSchema m_table;
        internal string m_originalGroupId = null;
        internal bool m_autoName = false; // whether it has automatic generated name => can be renamed

        internal string m_name = "";
        [DisplayName("s_name")]
        public string Name
        {
            get { return m_name; }
            set
            {
                WantChange();
                m_name = value;
                m_autoName = false;
            }
        }

        [Browsable(false)]
        public bool IsNew { get { return m_originalGroupId == null; } }

        [Browsable(false)]
        public bool IsOld { get { return m_originalGroupId == null; } }

        protected void WantChange()
        {
            if (!AllowChange)
            {
                throw new CannotChangeTablePropertyError("DAE-00172");
            }
        }

        public EditorConstraintBase(TableEditFrame frame)
        {
            m_frame = frame;
        }

        [Browsable(false)]
        public NameWithSchema Table
        {
            get { return m_table; }
        }

        internal abstract string DoCreateName(ISqlDialect dialect);

        public void CreateName(ISqlDialect dialect)
        {
            if (!String.IsNullOrEmpty(Name) && !m_autoName) return;
            if (m_table == null) return;
            m_name = DoCreateName(dialect);
            m_autoName = true;
        }

        public void SetTable(NameWithSchema table, ISqlDialect dialect)
        {
            m_table = table;
            CreateName(dialect);
        }

        internal bool AllowChange
        {
            get { return IsNew || m_frame.AllowChangeConstraint; }
        }
    }

    public class EditorIndexOrKeyStructure : EditorConstraintBase
    {
        [Browsable(false)]
        public ColumnsConstraint Source;

        List<IColumnReference> m_columns = new List<IColumnReference>();

        bool m_isUnique = false;

        [DisplayName("s_is_unique")]
        [TypeConverter(typeof(YesNoTypeConverter))]
        public bool IsUnique
        {
            get { return m_isUnique; }
            set
            {
                WantChange();
                m_isUnique = value;
            }
        }

        //[Editor(typeof(CollectionEditor), typeof(UITypeEditor))]
        [Browsable(false)]
        //[DisplayName("s_columns")]
        public List<IColumnReference> Columns
        {
            get { return m_columns; }
            set { m_columns = value; }
        }

        public EditorIndexOrKeyStructure(NameWithSchema table, ISqlDialect dialect, TableEditFrame frame)
            :base(frame)
        {
            SetTable(table, dialect);
        }

        public EditorIndexOrKeyStructure(TableEditFrame frame, ColumnsConstraint src)
            : base(frame)
        {
            m_originalGroupId = src.GroupId;
            Source = src;
            m_name = src.Name;
            m_columns.AddRange(src.Columns);
            if (src is IPrimaryKey) m_type = EditorIndexOrKeyType.PrimaryKey;
            if (src is IIndex)
            {
                m_type = EditorIndexOrKeyType.Index;
                m_isUnique = ((IIndex)src).IsUnique;
            }
            if (src is IUnique) m_type = EditorIndexOrKeyType.UniqueConstraint;
            m_origType = m_type;
        }

        EditorIndexOrKeyType m_origType;
        EditorIndexOrKeyType m_type = EditorIndexOrKeyType.UniqueConstraint;
        [Browsable(false)]
        public EditorIndexOrKeyType Type
        {
            get { return m_type; }
            set
            {
                WantChange();
                m_type = value;
            }
        }

        public string GetColumns()
        {
            return String.Join(",", (from c in Columns select c.ColumnName).ToArray());
        }

        public ColumnsConstraint CreateConstraint()
        {
            CheckConsistency();
            ColumnsConstraint res;
            if (m_type == m_origType && Source != null)
            {
                res = (ColumnsConstraint)Source.Clone();
            }
            else
            {
                switch (m_type)
                {
                    case EditorIndexOrKeyType.Index:
                        res = new IndexConstraint();
                        break;
                    case EditorIndexOrKeyType.PrimaryKey:
                        res = new PrimaryKey();
                        break;
                    case EditorIndexOrKeyType.UniqueConstraint:
                        res = new UniqueConstraint();
                        break;
                    default:
                        throw new InternalError("DAE-00173 Internal error");
                }
            }
            if (res is IndexConstraint)
            {
                ((IndexConstraint)res).IsUnique = IsUnique;
            }
            res.Name = Name;
            //res.Table = m_table;
            res.Columns.Clear();
            res.Columns.AddRange(m_columns);
            if (m_originalGroupId != null) res.GroupId = m_originalGroupId;
            return res;
        }

        public void CheckConsistency()
        {
            if (m_columns.Count == 0) throw new InconsistentTableStructureError("DAE-00174 Index or Unique error: no columns");
            foreach (var col in m_columns) if (col.ColumnName.IsEmpty()) throw new InconsistentTableStructureError("DAE-00175 Index or Unique error: empty column");
        }

        internal override string DoCreateName(ISqlDialect dialect)
        {
            if (dialect != null && dialect.DialectCaps.AnonymousPrimaryKey && Type == EditorIndexOrKeyType.PrimaryKey) return null;
            switch (Type)
            {
                case EditorIndexOrKeyType.PrimaryKey:
                    return DbObjectNameTool.PkName(m_table);
                case EditorIndexOrKeyType.Index:
                    return DbObjectNameTool.IndexName(m_table, m_columns);
                case EditorIndexOrKeyType.UniqueConstraint:
                    return DbObjectNameTool.UniqueName(m_table, m_columns);
            }
            throw new InternalError("DAE-00176 internal error");
        }
    }

    public class EditorForeignColumnStructure
    {
        public ComboBox SourceCombo;
        public ComboBox TargetCombo;
        public EditorForeignKeyStructure Fk;

        ColumnReference m_srcName = new ColumnReference("");
        public ColumnReference SrcName
        {
            get { return m_srcName; }
            set { m_srcName = value; }
        }

        ColumnReference m_dstName = new ColumnReference("");
        public ColumnReference DstName
        {
            get { return m_dstName; }
            set { m_dstName = value; }
        }
    }

    public class EditorForeignKeyStructure : EditorConstraintBase
    {
        List<EditorForeignColumnStructure> m_columns = new List<EditorForeignColumnStructure>();

        [Browsable(false)]
        public List<EditorForeignColumnStructure> Columns
        {
            get { return m_columns; }
            set { m_columns = value; }
        }

        NameWithSchema m_primaryKeyTable;
        [Browsable(false)]
        public NameWithSchema PrimaryKeyTable
        {
            get { return m_primaryKeyTable; }
            set { m_primaryKeyTable = value; }
        }

        [Browsable(false)]
        public ForeignKey Source;

        ForeignKeyAction m_onUpdate;
        [DisplayName("ON UPDATE")]
        [TypeConverter(typeof(EnumDescConverter))]
        public ForeignKeyAction OnUpdate
        {
            get { return m_onUpdate; }
            set
            {
                WantChange();
                m_onUpdate = value;
            }
        }

        ForeignKeyAction m_onDelete;
        [DisplayName("ON DELETE")]
        [TypeConverter(typeof(EnumDescConverter))]
        public ForeignKeyAction OnDelete
        {
            get { return m_onDelete; }
            set
            {
                WantChange();
                m_onDelete = value;
            }
        }

        public EditorForeignKeyStructure(NameWithSchema table, ISqlDialect dialect, TableEditFrame frame)
            :base(frame)
        {
            SetTable(table, dialect);
        }

        public EditorForeignKeyStructure(TableEditFrame frame, ForeignKey src)
            :base(frame)
        {
            m_originalGroupId = src.GroupId;
            Source = src;
            m_name = src.Name;
            m_table = src.Table.FullName;
            m_onDelete = src.OnDeleteAction;
            m_onUpdate = src.OnUpdateAction;
            PrimaryKeyTable = src.PrimaryKeyTable;
            for (int i = 0; i < src.Columns.Count; i++)
            {
                EditorForeignColumnStructure col = new EditorForeignColumnStructure();
                col.SrcName = new ColumnReference(src.Columns[i]);
                col.Fk = this;
                if (src.PrimaryKeyColumns != null && i < src.PrimaryKeyColumns.Count) col.DstName = new ColumnReference(src.PrimaryKeyColumns[i]);
                Columns.Add(col);
            }
        }

        public EditorForeignColumnStructure FindColumn(String column)
        {
            foreach (EditorForeignColumnStructure col in m_columns) if (col.SrcName.ColumnName == column) return col;
            return null;
        }

        public string GetColumns()
        {
            bool was = false;
            StringBuilder sb = new StringBuilder();
            foreach (EditorForeignColumnStructure col in Columns)
            {
                if (was) sb.Append(",");
                sb.AppendFormat("{0}=>{1}", col.SrcName, col.DstName);
                was = true;
            }
            return sb.ToString();
        }

        internal override string DoCreateName(ISqlDialect dialect)
        {
            return DbObjectNameTool.FkName(m_table, from col in m_columns select (IColumnReference)col.SrcName);
        }

        public ForeignKey CreateConstraint()
        {
            CheckConsistency();
            ForeignKey res;
            if (Source != null) res = (ForeignKey)Source.Clone();
            else res = new ForeignKey();
            res.Name = Name;
            res.PrimaryKeyTable = PrimaryKeyTable;
            res.Columns.Clear();
            res.PrimaryKeyColumns.Clear();
            //res.Table = m_table;
            res.OnDeleteAction = OnDelete;
            res.OnUpdateAction = OnUpdate;
            foreach (EditorForeignColumnStructure col in m_columns)
            {
                res.Columns.Add(col.SrcName);
                res.PrimaryKeyColumns.Add(col.DstName);
            }
            return res;
        }

        public void CheckConsistency()
        {
            if (PrimaryKeyTable == null) throw new InconsistentTableStructureError("DAE-00177 Bad FK: Target table not defined");
            if (m_columns.Count == 0) throw new InconsistentTableStructureError("DAE-00178 Bad FK: missing columns");
            foreach (var col in m_columns)
            {
                if (col.DstName.ColumnName.IsEmpty() || col.SrcName.ColumnName.IsEmpty()) throw new InconsistentTableStructureError("DAE-00179 Bad FK: empty column");
            }
        }

        public void SetPkTable(NameWithSchema name, ISqlDialect dialect)
        {
            PrimaryKeyTable = name;
            CreateName(dialect);
        }
    }

    public class EditorCheckStructure : EditorConstraintBase
    {
        string m_expression = "";
        [Browsable(false)]
        public string Expression
        {
            get { return m_expression; }
            set { m_expression = value; }
        }

        [Browsable(false)]
        public CheckConstraint Source;

        public EditorCheckStructure(NameWithSchema table, ISqlDialect dialect, TableEditFrame frame)
            :base(frame)
        {
            SetTable(table, dialect);
        }

        public EditorCheckStructure(TableEditFrame frame, CheckConstraint src)
            :base(frame)
        {
            m_originalGroupId = src.GroupId;
            Source = src;
            m_name = src.Name;
            m_table = src.Table.FullName;
            m_expression = src.Expression;
        }

        public void CheckConsistency()
        {
            if (m_expression.IsEmpty()) throw new InconsistentTableStructureError("DAE-00180 Bad CHECK: missing expression");
        }

        public CheckConstraint CreateConstraint()
        {
            CheckConsistency();
            CheckConstraint res;
            if (Source == null) res = new CheckConstraint();
            else res = (CheckConstraint)Source.Clone();
            res.Name = Name;
            res.Expression = Expression;
            //res.Table = m_table;
            return res;
        }

        internal override string DoCreateName(ISqlDialect dialect)
        {
            return m_frame.GetUniqueName(DbObjectNameTool.CheckName(m_table));
       }
    }
}
