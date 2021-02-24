using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace DatAdmin
{
    public class Domains_TreeNode : LateLoadChildrenConnectionTreeNodeBase, IStructureCollectionTreeNode
    {
        IDatabaseSource m_conn;

        public Domains_TreeNode(IDatabaseSource conn, ITreeNode parent)
            : base(conn, parent, "domains")
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
            dbmem.DomainDetails = dbmem.DomainList = true;
            IDatabaseStructure dbs = m_conn.LoadDatabaseStructure(dbmem, null);
            List<ITreeNode> res = new List<ITreeNode>();
            foreach (var domain in dbs.Domains.Sorted())
            {
                res.Add(new Domain_TreeNode(domain, m_conn, this));
            }
            m_children = res.ToArray();
        }

        public override string Title
        {
            get { return "s_domains"; }
        }

        public override System.Drawing.Bitmap Image
        {
            get { return CoreIcons.domain; }
        }

        public override string TypeTitle
        {
            get { return "s_domains"; }
        }

        [PopupMenu("s_create_domain", Weight = MenuWeights.NEWOBJECT)]
        public void CreateDomain()
        {
            MainWindow.Instance.OpenContent(new DomainEditFrame(m_conn, null));
        }

        #region IStructureCollectionTreeNode Members

        public List<IAbstractObjectStructure> InvokeLoadStructureList()
        {
            return m_conn.GetStructureList_NonEffective(s => s.Domains);
        }

        #endregion
    }

    public class Domain_TreeNode : ConnectionUsageTreeNodeBase, IStructureTreeNode
    {
        IDomainStructure m_domain;
        IDatabaseSource m_conn;

        public Domain_TreeNode(IDomainStructure domain, IDatabaseSource conn, ITreeNode parent)
            : base(conn, parent, domain.FullName.ToString())
        {
            m_domain = domain;
            m_conn = conn;
        }

        public override string Title
        {
            get { return m_domain.FullName.ToString(); }
        }

        public override System.Drawing.Bitmap Image
        {
            get { return CoreIcons.domain; }
        }

        public override string TypeTitle
        {
            get { return "s_domains"; }
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
            if (MessageBox.Show(Texts.Get("s_really_drop$domain", "domain", Title), VersionInfo.ProgramTitle, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                m_conn.DropObject(m_domain);
                Parent.CompleteRefresh();
                return true;
            }
            return false;
        }

        [PopupMenuEnabled("s_edit")]
        public bool EditEnabled()
        {
            var caps = m_conn.AlterCaps;
            return caps.ChangeDomain;
        }

        [PopupMenu("s_edit", ImageName = CoreIcons.designName, Shortcut = Keys.F4, Weight = MenuWeights.EDIT)]
        public void DoEdit()
        {
            var pars = new ObjectEditorPars { };
            MainWindow.Instance.OpenContent(new DomainEditFrame(m_conn, m_domain));
        }

        public override bool AllowRename()
        {
            return m_conn.AlterCaps.RenameDomain;
        }

        public override void RenameNode(string newname)
        {
            m_conn.RenameObject(m_domain, newname);
            Parent.CompleteRefresh();
        }

        [PopupMenuEnabled("s_change_schema")]
        public bool ChangeSchemaEnabled()
        {
            return m_conn.AlterCaps.RenameDomain;
        }

        [PopupMenuVisible("s_change_schema")]
        public bool ChangeSchemaVisible()
        {
            return m_conn.DatabaseCaps.MultipleSchema;
        }

        [PopupMenu("s_change_schema", ImageName = CoreIcons.schemaName, Weight = MenuWeights.EDIT2)]
        public void ChangeSchema()
        {
            var schemata = StructLoader.SchemaNames(dbmem => m_conn.InvokeLoadStructure(dbmem, null));
            string newschema = InputBox.Run("s_new_schema", m_domain.FullName.Schema, schemata);
            if (newschema != null)
            {
                m_conn.ChangeObjectSchema(m_domain, newschema);
                Parent.CompleteRefresh();
            }
        }

        #region IStructureTreeNode Members

        public IAbstractObjectStructure InvokeLoadStructure()
        {
            return m_domain;
        }

        #endregion

        //public override List<IAppObjectSqlGenerator> GetSqlGenerators()
        //{
        //    List<IAppObjectSqlGenerator> res = base.GetSqlGenerators();
        //    res.Add(new DelegateSqlGenerator("CREATE DOMAIN", delegate(ITreeNode node, TextWriter tw, ISqlDialect dialectOverride)
        //    {
        //        SqlTemplates.GenerateCreateDomain(m_domain, tw, dialectOverride ?? Dialect);
        //    }));
        //    res.Add(new DelegateSqlGenerator("DROP DOMAIN", delegate(ITreeNode node, TextWriter tw, ISqlDialect dialectOverride)
        //    {
        //        SqlTemplates.GenerateDropDomain(m_domain, tw, dialectOverride ?? Dialect);
        //    }));
        //    res.Add(new DelegateSqlGenerator("RECREATE DOMAIN", delegate(ITreeNode node, TextWriter tw, ISqlDialect dialectOverride)
        //    {
        //        SqlTemplates.GenerateRecreateDomain(m_domain, tw, dialectOverride ?? Dialect);
        //    }));
        //    return res;
        //}

        public override ObjectPath GetObjectPath()
        {
            return new ObjectPath(m_conn.DatabaseName, m_domain.FullName);
        }
    }
}
