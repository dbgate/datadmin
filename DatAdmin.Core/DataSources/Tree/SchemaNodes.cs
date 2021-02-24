using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace DatAdmin
{
    public class Schemas_TreeNode : LateLoadChildrenConnectionTreeNodeBase, IStructureCollectionTreeNode
    {
        IDatabaseSource m_conn;

        public Schemas_TreeNode(IDatabaseSource conn, ITreeNode parent)
            : base(conn, parent, "schemas")
        {
            m_conn = conn;
        }

        public override AppObject GetFirstValidAppObject()
        {
            return Parent.GetPrimaryAppObject();
        }

        protected override void DoGetChildren()
        {
            var dbmem = new DatabaseStructureMembers();
            dbmem.SchemaDetails = dbmem.SchemaList = true;
            IDatabaseStructure dbs = m_conn.LoadDatabaseStructure(dbmem, null);
            List<ITreeNode> res = new List<ITreeNode>();
            foreach (var schema in dbs.Schemata.Sorted())
            {
                res.Add(new Schema_TreeNode(schema, m_conn, this));
            }
            m_children = res.ToArray();
        }

        public override string Title
        {
            get { return "s_schemas"; }
        }

        public override System.Drawing.Bitmap Image
        {
            get { return CoreIcons.schema; }
        }

        public override string TypeTitle
        {
            get { return "s_schemas"; }
        }

        [PopupMenu("s_create_schema", Weight = MenuWeights.NEWOBJECT)]
        public void CreateSchemaMenu()
        {
            string name = InputBox.Run("s_name_of_new_schema", "new_schema");
            if (name != null)
            {
                m_conn.CreateObject(new SchemaStructure { SchemaName = name });
                this.CompleteRefresh();
            }
        }

        #region IStructureCollectionTreeNode Members

        public List<IAbstractObjectStructure> InvokeLoadStructureList()
        {
            return m_conn.GetStructureList_NonEffective(s => s.Schemata);
        }

        #endregion
    }

    public class Schema_TreeNode : ConnectionUsageTreeNodeBase, IStructureTreeNode
    {
        ISchemaStructure m_schema;
        IDatabaseSource m_conn;

        public Schema_TreeNode(ISchemaStructure schema, IDatabaseSource conn, ITreeNode parent)
            : base(conn, parent, schema.SchemaName)
        {
            m_schema = schema;
            m_conn = conn;
        }

        public override string Title
        {
            get { return m_schema.SchemaName; }
        }

        public override System.Drawing.Bitmap Image
        {
            get { return CoreIcons.schema; }
        }

        public override string TypeTitle
        {
            get { return "s_schemas"; }
        }

        public override ITreeNode[] GetChildren()
        {
            return new ITreeNode[] { };
        }

        public override bool AllowDelete()
        {
            return true;
        }

        public override bool DoDelete()
        {
            if (MessageBox.Show(Texts.Get("s_really_drop$schema", "schema", Title), VersionInfo.ProgramTitle, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                m_conn.DropObject(m_schema);
                Parent.CompleteRefresh();
                return true;
            }
            return false;
        }

        #region IStructureTreeNode Members

        public IAbstractObjectStructure InvokeLoadStructure()
        {
            return m_schema;
        }

        #endregion

        public override ObjectPath GetObjectPath()
        {
            return new ObjectPath(m_conn.DatabaseName, new NameWithSchema(m_schema.SchemaName));
        }
    }
}
