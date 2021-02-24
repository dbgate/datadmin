using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing;

namespace DatAdmin
{
    public class Tables_TreeNode : LateLoadChildrenConnectionTreeNodeBase, IStructureCollectionTreeNode
    {
        IDatabaseSource m_conn;
        bool m_isSystem;

        public Tables_TreeNode(IDatabaseSource conn, ITreeNode parent, bool isSystem)
            : base(conn, parent, "tables")
        {
            m_conn = conn;
            m_isSystem = isSystem;
        }

        public override AppObject GetFirstValidAppObject()
        {
            return Parent.GetPrimaryAppObject();
        }

        public override string TypeTitle
        {
            get { return "s_tables"; }
        }

        public override System.Drawing.Bitmap Image
        {
            get { return CoreIcons.table; }
        }

        protected override void DoGetChildren()
        {
            List<ITreeNode> res = new List<ITreeNode>();
            foreach (NameWithSchema table in m_conn.LoadFullTableNames(false).Sorted())
            {
                bool thisSystem = m_conn.Dialect != null && m_conn.Dialect.IsSystemTable(table);
                if (thisSystem != m_isSystem) continue;
                res.Add(new Table_SourceTreeNode(m_conn.GetTable(table), this, table));
            }
            m_children = res.ToArray();
        }
        public override string Title
        {
            get
            {
                if (m_isSystem) return "s_system_tables";
                return "s_tables";
            }
        }

        [PopupMenuEnabled("s_new_table")]
        [PopupMenuEnabled("s_new_table_from_file")]
        public bool NewTableEnabled()
        {
            return !m_isSystem;
        }

        [PopupMenu("s_new_table", Weight = MenuWeights.NEWOBJECT, ImageName = CoreIcons.table_addName)]
        public void NewTable()
        {
            var pars = new AlterTableEditorPars { SavedCallback = this.CompleteRefresh };
            MainWindow.Instance.OpenContent(new TableEditFrame(m_conn.CloneSource(), null, pars));
        }

        [PopupMenu("s_new_table_from_file", Weight = MenuWeights.NEWOBJECT, ImageName = CoreIcons.openName)]
        public void NewTableFromFile()
        {
            var dst = new GenericTabularDataStore(GetConnection().Clone(), m_conn.DatabaseName, null, null);
            dst.create_table = true;
            var wizard = new BulkCopyWizard(null, dst);
            wizard.FixedTarget = true;
            wizard.ShowDialogEx();
        }

        [DragDropOperation(Name = "copytable", Title = "s_copy_table")]
        public void DragDrop_CopyTable(AppObject appobj)
        {
            var dbobj = GetFirstValidAppObject() as DatabaseAppObject;
            if (dbobj != null) dbobj.DragDrop_CopyTable(appobj);
            this.CompleteRefresh();
        }

        [DragDropOperationVisible(Name = "copytable")]
        public bool DragDropVisible_CopyTable(AppObject appobj)
        {
            if (m_isSystem) return false;
            return appobj.TableSource != null;
        }

        public override void DataRefresh()
        {
            if (Parent is IDatabaseTreeNode) ((IDatabaseTreeNode)Parent).BeforeDataRefreshChilds();
            base.DataRefresh();
        }

        public override List<IWidget> GetWidgets()
        {
            var res = base.GetWidgets();
            res.Add(new TablesRawGridWidget());
            return res;
        }

        public override ObjectPath GetObjectPath()
        {
            return new ObjectPath(TreeNodeExtension.GetDatabaseName(this));
        }

        #region IStructureCollectionTreeNode Members

        public List<IAbstractObjectStructure> InvokeLoadStructureList()
        {
            return m_conn.GetStructureList_NonEffective(dbs => dbs.Tables);
        }

        #endregion
    }

    //[NodeFilter("s_table")]
    public class Table_SourceTreeNode : ConnectionUsageTreeNodeBase, IStructureTreeNode
    {
        internal ITableSource m_conn;
        NameWithSchema m_tblname;

        public Table_SourceTreeNode(ITableSource conn, ITreeNode parent, NameWithSchema tblname)
            : base(conn, parent, tblname.ToString("F"))
        {
            m_conn = conn;
            m_tblname = tblname;
            var appobj = new TableAppObject();
            appobj.FillFromTable(conn);
            SetAppObject(appobj);
        }

        //public override string GetDatabaseName() { return m_conn.Database.DatabaseName; }
        public override string TypeTitle
        {
            get { return "s_table"; }
        }
        public override ITreeNode[] GetChildren()
        {
            List<ITreeNode> res = new List<ITreeNode>();
            res.Add(new Columns_TreeNode(m_conn, this));
            res.Add(new Constraints_TreeNode(m_conn, this, cnt => !(cnt is IIndex), "constraints", "s_constraints", CoreIcons.foreign_key));
            res.Add(new Constraints_TreeNode(m_conn, this, cnt => cnt is IIndex, "indexes", "s_indexes", CoreIcons.index));
            if (AdvancedPerspectivesFeature.Allowed) res.Add(new PerspectiveInstancesTreeNode(m_conn, this));
            //this.GetDbObjectNodes(m_conn, res, DbObjectParent.Table, Dialect, GetObjectPath(), false);
            return res.ToArray();
        }
        public override bool PreparedChildren
        {
            get { return true; }
        }
        public override string Title
        {
            get
            {
                return m_tblname.ToString();
                //if (!m_conn.Connection.Dialect.SupportsSchemas) return m_tblname.Name;
                //else return m_tblname.ToString();
            }
        }
        public override ObjectPath GetObjectPath()
        {
            return new ObjectPath(TreeNodeExtension.GetDatabaseName(this), m_tblname);
        }

        public ITableSource TableSource { get { return m_conn; } }

        #region IStructureTreeNode Members

        public IAbstractObjectStructure InvokeLoadStructure()
        {
            return m_conn.InvokeLoadStructure(TableStructureMembers.AllNoRefs);
        }

        #endregion
    }

    public class Columns_TreeNode : LateLoadChildrenConnectionTreeNodeBase, IStructureCollectionTreeNode
    {
        ITableSource m_conn;

        public Columns_TreeNode(ITableSource conn, ITreeNode parent)
            : base(conn, parent, "columns")
        {
            m_conn = conn;
        }

        public override AppObject GetFirstValidAppObject()
        {
            return Parent.GetPrimaryAppObject();
        }

        //public override string GetDatabaseName() { return m_conn.Database.DatabaseName; }
        public override string TypeTitle
        {
            get { return "s_columns"; }
        }

        public override System.Drawing.Bitmap Image
        {
            get { return CoreIcons.column; }
        }

        protected override void DoGetChildren()
        {
            List<ITreeNode> res = new List<ITreeNode>();
            ITableStructure table = m_conn.LoadTableStructure(TableStructureMembers.Columns | TableStructureMembers.PrimaryKey | TableStructureMembers.ForeignKeys);
            foreach (IColumnStructure col in table.Columns)
            {
                res.Add(new Column_TreeNode(m_conn, table, col, this));
            }
            m_children = res.ToArray();
        }

        public override string Title
        {
            get { return "s_columns"; }
        }

        public override List<IWidget> GetWidgets()
        {
            var res = base.GetWidgets();
            res.Add(new ColumnsRawGridWidget());
            return res;
        }

        [PopupMenu("s_new", Weight = MenuWeights.NEWOBJECT, ImageName = CoreIcons._newName)]
        public void NewConstraint()
        {
            ((TableAppObject)Parent.GetPrimaryAppObject()).DoEdit(AlterTableEditorPars.Tab.Columns);
        }

        #region IStructureCollectionTreeNode Members

        public List<IAbstractObjectStructure> InvokeLoadStructureList()
        {
            return m_conn.GetStructureList_NonEffective(s => s.Columns);
        }

        #endregion
    }

    //[NodeFilter("s_column")]
    public class Column_TreeNode : ConnectionUsageTreeNodeBase, IStructureTreeNode
    {
        IColumnStructure m_column;
        ITableStructure m_table;
        ITableSource m_conn;
        string m_title;
        bool m_ispk;
        bool m_isfk;

        public Column_TreeNode(ITableSource conn, ITableStructure table, IColumnStructure column, ITreeNode parent)
            : base(conn, parent, column.ColumnName)
        {
            m_column = column;
            m_table = table;
            m_conn = conn;

            m_ispk = m_table.GetKeyWithColumn<IPrimaryKey>(m_column) != null;
            m_isfk = m_table.GetKeyWithColumn<IForeignKey>(m_column) != null;
            StringBuilder res = new StringBuilder(m_column.ColumnName);
            res.Append(" (");
            if (m_ispk) res.Append("PK, ");
            if (m_isfk) res.Append("FK, ");
            if (m_conn.Database.Dialect != null) res.Append(m_conn.Database.Dialect.GenericTypeToSpecific(m_column.DataType).ToString().ToLower());
            else res.Append(m_column.DataType.ToString().ToLower());
            res.Append(", ");
            if (m_column.IsNullable) res.Append("null"); else res.Append("not null");
            res.Append(")");
            m_title = res.ToString();

            var appobj = new ColumnAppObject();
            appobj.FillFromTable(conn);
            appobj.Column = new ColumnStructure(column);
            appobj.FillRelatedConstraints(table.Constraints);
            SetAppObject(appobj);
        }

        public override System.Drawing.Bitmap Image
        {
            get
            {
                if (m_ispk) return CoreIcons.primary_key;
                if (m_isfk) return CoreIcons.foreign_key;
                return CoreIcons.column;
            }
        }

        public override string TypeTitle
        {
            get { return "s_column"; }
        }

        public override string Title
        {
            get { return m_title; }
        }

        public override ITreeNode[] GetChildren()
        {
            return new ITreeNode[] { };
        }
        public override bool PreparedChildren { get { return true; } }

        public override bool AllowDelete()
        {
            return m_conn.Database.AlterCaps.DropColumn;
        }

        public override bool DoDelete()
        {
            if (MessageBox.Show(Texts.Get("s_really_drop$column", "column", m_column.ColumnName), VersionInfo.ProgramTitle, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                m_conn.Database.DropObject(m_column);
                return true;
            }
            return false;
        }

        public override bool AllowReuse()
        {
            return false;
        }

        public override ObjectPath GetObjectPath()
        {
            return new ObjectPath(TreeNodeExtension.GetDatabaseName(this), m_column.Table.FullName, m_column.ColumnName);
        }


        #region IStructureTreeNode Members

        public IAbstractObjectStructure InvokeLoadStructure()
        {
            return m_column;
        }

        #endregion
    }

    public class Constraints_TreeNode : LateLoadChildrenConnectionTreeNodeBase, IStructureCollectionTreeNode
    {
        ITableSource m_conn;
        Func<IConstraint, bool> m_filterFunc;
        Bitmap m_image;
        string m_title;

        public Constraints_TreeNode(ITableSource conn, ITreeNode parent, Func<IConstraint, bool> filterFunc, string name, string title, Bitmap image)
            : base(conn, parent, name)
        {
            m_conn = conn;
            m_title = title;
            m_image = image;
            m_filterFunc = filterFunc;
        }

        public override AppObject GetFirstValidAppObject()
        {
            return Parent.GetPrimaryAppObject();
        }

        public override string TypeTitle
        {
            get { return m_title; }
        }

        //public override string GetDatabaseName() { return m_conn.Database.DatabaseName; }
        public override Bitmap Image
        {
            get { return m_image; }
        }

        protected override void DoGetChildren()
        {
            List<ITreeNode> res = new List<ITreeNode>();
            ITableStructure table = m_conn.LoadTableStructure(TableStructureMembers.ConstraintsNoRefs);
            foreach (IConstraint con in table.Constraints.Sorted())
            {
                if (!m_filterFunc(con)) continue;
                res.Add(new Constraint_TreeNode(m_conn, table, con, this));
            }
            m_children = res.ToArray();
        }

        public override string Title
        {
            get { return m_title; }
        }

        [PopupMenu("s_new", Weight = MenuWeights.NEWOBJECT, ImageName = CoreIcons._newName)]
        public void NewConstraint()
        {
            ((TableAppObject)Parent.GetPrimaryAppObject()).DoEdit(AlterTableEditorPars.Tab.IndexesKeys);
        }


        #region IStructureCollectionTreeNode Members

        public List<IAbstractObjectStructure> InvokeLoadStructureList()
        {
            return m_conn.GetStructureList_NonEffective(s => s.Constraints);
        }

        #endregion
    }

    //[NodeFilter("s_constraint")]
    public class Constraint_TreeNode : ConnectionUsageTreeNodeBase, IStructureTreeNode
    {
        IConstraint m_constraint;
        ITableStructure m_table;
        ITableSource m_conn;

        public Constraint_TreeNode(ITableSource conn, ITableStructure table, IConstraint constraint, ITreeNode parent)
            : base(conn, parent, constraint.Name ?? "noname")
        {
            m_constraint = constraint;
            m_table = table;
            m_conn = conn;

            var appobj = new ConstraintAppObject();
            appobj.FillFromTable(m_conn);
            appobj.Constraint = Constraint.CreateCopy(m_constraint);
            SetAppObject(appobj);
        }

        public override string TypeTitle
        {
            get { return "s_constraint"; }
        }

        public override string Title
        {
            get
            {
                return m_primaryAppobj.ToString();
            }
        }

        public override ITreeNode[] GetChildren()
        {
            var col = m_constraint as IColumnsConstraint;
            List<ITreeNode> res = new List<ITreeNode>();
            if (col != null)
            {
                foreach (var c in col.Columns)
                {
                    res.Add(new ColumnRefTreeNode(this, c));
                }
            }
            return res.ToArray();
        }

        public override bool PreparedChildren { get { return true; } }

        public override ObjectPath GetObjectPath()
        {
            return new ObjectPath(TreeNodeExtension.GetDatabaseName(this), m_constraint.Table.FullName, m_constraint.Name);
        }

        #region IStructureTreeNode Members

        public IAbstractObjectStructure InvokeLoadStructure()
        {
            return m_constraint;
        }

        #endregion
    }

    public class ColumnRefTreeNode : TreeNodeBase
    {
        IColumnReference m_column;
        public ColumnRefTreeNode(ITreeNode parent, IColumnReference column)
            : base(parent, column.ColumnName)
        {
            m_column = column;
        }

        public override Bitmap Image
        {
            get { return CoreIcons.column; }
        }

        public override string Title
        {
            get { return m_column.ColumnName; }
        }

        public override string TypeTitle
        {
            get { return "s_column"; }
        }

        public override ITreeNode[] GetChildren()
        {
            return new ITreeNode[] { };
        }
    }

    public class PerspectiveInstancesTreeNode : LateLoadChildrenConnectionTreeNodeBase
    {
        ITableSource m_conn;

        public PerspectiveInstancesTreeNode(ITableSource conn, ITreeNode parent)
            : base(conn, parent, "perspectives")
        {
            m_conn = conn;
        }

        //public override string GetDatabaseName() { return m_conn.Database.DatabaseName; }
        public override string TypeTitle
        {
            get { return "s_perspectives"; }
        }

        public override System.Drawing.Bitmap Image
        {
            get { return CoreIcons.perspective; }
        }

        protected override void DoGetChildren()
        {
            List<ITreeNode> res = new List<ITreeNode>();
            ITableStructure table = m_conn.LoadTableStructure(TableStructureMembers.ColumnNames);
            var pers = TablePerspectiveManager.GetPerspectives(m_conn.Connection, m_conn.Database.DatabaseName, m_conn.FullName, table.Columns.GetNames());
            foreach (var per in pers)
            {
                res.Add(new PerspectiveInstanceTreeNode(this, m_conn, per));
            }
            m_children = res.ToArray();
        }

        public override string Title
        {
            get { return "s_perspectives"; }
        }
    }

    public class PerspectiveInstanceTreeNode : ConnectionUsageTreeNodeBase
    {
        TablePerspective m_per;
        ITableSource m_conn;

        public PerspectiveInstanceTreeNode(ITreeNode parent, ITableSource conn, TablePerspective per)
            : base(conn, parent, System.IO.Path.GetFileName(per.FileName))
        {
            m_per = per;
            m_conn = conn;
            var appobj = new PerspectiveInstanceAppObject();
            appobj.FillFromTable(conn);
            appobj.FileName = per.FileName;
            SetAppObject(appobj);
        }

        public override string TypeTitle
        {
            get { return "s_perspective"; }
        }

        public override string Title
        {
            get { return System.IO.Path.GetFileNameWithoutExtension(m_per.FileName); }
        }

        public override ITreeNode[] GetChildren()
        {
            return new ITreeNode[] { };
        }
    }
}
