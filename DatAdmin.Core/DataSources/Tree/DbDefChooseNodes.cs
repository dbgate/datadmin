using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.ComponentModel;

namespace DatAdmin
{
    public class DbDefChooseTreeNode : TreeNodeBase
    {
        internal readonly IDatabaseStructure m_db;
        ITreeNode[] m_children;

        public DbDefChooseTreeNode(IDatabaseStructure db)
            : base("dbstruct")
        {
            m_db = db;
        }

        public override string Title
        {
            get { return "dbdef"; }
        }
        public override ITreeNode[] GetChildren()
        {
            if (m_children == null)
            {
                List<ITreeNode> res = new List<ITreeNode>();
                res.Add(new DbDefChooseTablesTreeNode(this));
                res.Add(new DbDefChooseDomainsTreeNode(this));
                foreach (string objtype in m_db.SpecificObjects.Keys)
                {
                    var repr = SpecificRepresentationAddonType.Instance.FindRepresentation(objtype);
                    if (!repr.ShowInTree) continue;
                    res.Add(new DbDefChooseSpecObjectsTreeNode(this, objtype));
                }
                m_children = res.ToArray();
            }
            return m_children;
        }

        public override string TypeTitle
        {
            get { return "dbdef"; }
        }

        public DatabaseStructureMembers GetChoosedMembers()
        {
            DatabaseStructureMembers res = new DatabaseStructureMembers();
            foreach (var node in GetChildren())
            {
                var tnode = node as IMembersHolderNode;
                if (tnode != null) tnode.GetMembers(res);
            }
            res.LoadDependencies = true;
            return res;
        }
    }

    public interface IMembersHolderNode
    {
        void GetMembers(DatabaseStructureMembers dbmem);
    }

    public class DbDefChooseTablesTreeNode : TreeNodeBase, IMembersHolderNode
    {
        internal readonly IDatabaseStructure m_db;

        public DbDefChooseTablesTreeNode(DbDefChooseTreeNode parent)
            : base(parent, "tables")
        {
            m_db = parent.m_db;
        }

        public override string Title
        {
            get { return "s_tables"; }
        }

        public override string TypeTitle
        {
            get { return "s_tables"; }
        }

        public override ITreeNode[] GetChildren()
        {
            List<ITreeNode> res = new List<ITreeNode>();
            foreach (var tbl in m_db.Tables.SortedByKey<ITableStructure, NameWithSchema>(tbl => tbl.FullName))
            {
                res.Add(new DbDefChooseTableTreeNode(this, tbl));
            }
            return res.ToArray();
        }

        public override System.Drawing.Bitmap Image
        {
            get { return CoreIcons.table; }
        }

        public override void AfterUserCheck()
        {
            foreach (IRealTreeNode node in RealNode.RealChildren)
            {
                node.NodeChecked = RealNode.NodeChecked;
            }
        }

        public void GetMembers(DatabaseStructureMembers dbmem)
        {
            if (RealNode.NodeChecked)
            {
                dbmem.TableList = true;
                dbmem.TableMembers = TableStructureMembers.AllNoRefs;
                if (!RealNode.CheckedAllChildren())
                {
                    dbmem.TableFilter = new List<NameWithSchema>();
                    foreach (var child in RealNode.RealChildren)
                    {
                        if (child.NodeChecked) dbmem.TableFilter.Add(((DbDefChooseTableTreeNode)child.LogicalNode).m_table.FullName);
                    }
                }
            }
        }

        public override void OnSetRealNode()
        {
            RealNode.NodeChecked = true;
        }
    }

    public class DbDefChooseTableTreeNode : TreeNodeBase
    {
        internal readonly IDatabaseStructure m_db;
        internal readonly ITableStructure m_table;

        public DbDefChooseTableTreeNode(DbDefChooseTablesTreeNode parent, ITableStructure table)
            : base(parent, table.FullName.ToString())
        {
            m_db = parent.m_db;
            m_table = table;
        }

        public override string Title
        {
            get { return m_table.FullName.ToString(); }
        }

        public override string TypeTitle
        {
            get { return "s_table"; }
        }

        public override System.Drawing.Bitmap Image
        {
            get { return CoreIcons.table; }
        }

        public override ITreeNode[] GetChildren()
        {
            return new ITreeNode[] { };
        }

        public override void AfterUserCheck()
        {
            if (RealNode.NodeChecked)
            {
                Parent.RealNode.NodeChecked = true;
            }
        }

        public override void OnSetRealNode()
        {
            RealNode.NodeChecked = true;
        }
    }






    public class DbDefChooseDomainsTreeNode : TreeNodeBase, IMembersHolderNode
    {
        internal readonly IDatabaseStructure m_db;

        public DbDefChooseDomainsTreeNode(DbDefChooseTreeNode parent)
            : base(parent, "domains")
        {
            m_db = parent.m_db;
        }

        public override string Title
        {
            get { return "s_domains"; }
        }

        public override string TypeTitle
        {
            get { return "s_domains"; }
        }

        public override ITreeNode[] GetChildren()
        {
            List<ITreeNode> res = new List<ITreeNode>();
            foreach (var tbl in m_db.Domains.Sorted())
            {
                res.Add(new DbDefChooseDomainTreeNode(this, tbl));
            }
            return res.ToArray();
        }

        public override System.Drawing.Bitmap Image
        {
            get { return CoreIcons.domain; }
        }

        public override void AfterUserCheck()
        {
            foreach (IRealTreeNode node in RealNode.RealChildren)
            {
                node.NodeChecked = RealNode.NodeChecked;
            }
        }

        public void GetMembers(DatabaseStructureMembers dbmem)
        {
            if (RealNode.NodeChecked)
            {
                dbmem.DomainList = true;
                dbmem.DomainDetails = true;
            }
        }

        public override void OnSetRealNode()
        {
            RealNode.NodeChecked = true;
        }
    }

    public class DbDefChooseDomainTreeNode : TreeNodeBase
    {
        internal readonly IDatabaseStructure m_db;
        internal readonly IDomainStructure m_domain;

        public DbDefChooseDomainTreeNode(DbDefChooseDomainsTreeNode parent, IDomainStructure domain)
            : base(parent, domain.FullName.ToString())
        {
            m_db = parent.m_db;
            m_domain = domain;
        }

        public override string Title
        {
            get { return m_domain.FullName.ToString(); }
        }

        public override string TypeTitle
        {
            get { return "s_domain"; }
        }

        public override System.Drawing.Bitmap Image
        {
            get { return CoreIcons.domain; }
        }

        public override ITreeNode[] GetChildren()
        {
            return new ITreeNode[] { };
        }

        public override void AfterUserCheck()
        {
            RealNode.NodeChecked = Parent.RealNode.NodeChecked;
            // cannot select or deselect domains
            //if (RealNode.NodeChecked)
            //{
            //    Parent.RealNode.NodeChecked = true;
            //}
        }

        public override void OnSetRealNode()
        {
            RealNode.NodeChecked = true;
        }
    }

    public class DbDefChooseSpecObjectsTreeNode : TreeNodeBase, IMembersHolderNode
    {
        internal readonly IDatabaseStructure m_db;
        internal readonly ISpecificRepresentation m_repr;
        internal readonly string m_objtype;

        public DbDefChooseSpecObjectsTreeNode(DbDefChooseTreeNode parent, string objtype)
            : base(parent, objtype + "_list")
        {
            m_repr = SpecificRepresentationAddonType.Instance.FindRepresentation(objtype);
            m_objtype = objtype;
            m_db = parent.m_db;
        }

        public override string Title
        {
            get { return m_repr.TitlePlural; }
        }

        public override string TypeTitle
        {
            get { return m_repr.TitlePlural; }
        }

        public override ITreeNode[] GetChildren()
        {
            List<ITreeNode> res = new List<ITreeNode>();
            foreach (var obj in m_db.SpecificObjects[m_objtype].SortedByKey<ISpecificObjectStructure, NameWithSchema>(o => o.ObjectName))
            {
                res.Add(new DbDefChooseSpecObjectTreeNode(this, obj));
            }
            return res.ToArray();
        }

        public override System.Drawing.Bitmap Image
        {
            get { return m_repr.Icon; }
        }

        public override void AfterUserCheck()
        {
            foreach (IRealTreeNode node in RealNode.RealChildren)
            {
                node.NodeChecked = RealNode.NodeChecked;
            }
        }

        public void GetMembers(DatabaseStructureMembers dbmem)
        {
            if (RealNode.NodeChecked)
            {
                var mem = new SpecificObjectMembers();
                dbmem.SpecificObjectOverride[m_repr.ObjectType] = mem;
                mem.ObjectDetail = true;
                mem.ObjectList = true;
                if (!RealNode.CheckedAllChildren())
                {
                    mem.ObjectFilter = new List<NameWithSchema>();
                    foreach (var child in RealNode.RealChildren)
                    {
                        if (child.NodeChecked)
                        {
                            mem.ObjectFilter.Add(((DbDefChooseSpecObjectTreeNode)child.LogicalNode).m_obj.ObjectName);
                        }
                    }
                }
            }
        }

        public override void OnSetRealNode()
        {
            RealNode.NodeChecked = true;
        }
    }

    public class DbDefChooseSpecObjectTreeNode : TreeNodeBase
    {
        internal readonly IDatabaseStructure m_db;
        internal readonly ISpecificObjectStructure m_obj;
        internal readonly ISpecificRepresentation m_repr;

        public DbDefChooseSpecObjectTreeNode(DbDefChooseSpecObjectsTreeNode parent, ISpecificObjectStructure obj)
            : base(parent, obj.ObjectName.ToString())
        {
            m_repr = SpecificRepresentationAddonType.Instance.FindRepresentation(obj.ObjectType);
            m_obj = obj;
            m_db = parent.m_db;
        }

        public override string Title
        {
            get { return m_obj.ObjectName.ToString(); }
        }

        public override string TypeTitle
        {
            get { return m_repr.TitlePlural; }
        }


        public override System.Drawing.Bitmap Image
        {
            get { return m_repr.Icon; }
        }

        public override ITreeNode[] GetChildren()
        {
            return new ITreeNode[] { };
        }

        public override void AfterUserCheck()
        {
            if (RealNode.NodeChecked)
            {
                Parent.RealNode.NodeChecked = true;
            }
        }

        public override void OnSetRealNode()
        {
            RealNode.NodeChecked = true;
        }
    }
}
