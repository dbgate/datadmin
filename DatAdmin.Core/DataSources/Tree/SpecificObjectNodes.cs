using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.IO;

namespace DatAdmin
{
    public class SpecificObjectsNode : LateLoadChildrenConnectionTreeNodeBase, IStructureCollectionTreeNode
    {
        ISpecificObjectType m_dbtype;
        ISpecificRepresentation m_repr;
        ObjectPath m_parpath;
        IDatabaseSource m_conn;
        bool m_isSystem;

        public SpecificObjectsNode(IDatabaseSource conn, ITreeNode parent, ISpecificObjectType dbtype, ISpecificRepresentation repr, ObjectPath parpath, bool isSystem)
            : base(conn, parent, Texts.GetTextIdWithoutPrefix(repr.TitlePlural))
        {
            m_conn = conn;
            m_dbtype = dbtype;
            m_repr = repr;
            m_parpath = parpath;
            m_isSystem = isSystem;
        }

        protected override void DoGetChildren()
        {
            List<ITreeNode> res = new List<ITreeNode>();
            foreach (NameWithSchema name in m_conn.LoadSpecificObjectList(m_dbtype.ObjectType, m_isSystem).Sorted())
            {
                res.Add(new SpecificObjectNode(this, m_conn, m_dbtype, m_repr, m_parpath.GetChild(name)));
            }
            m_children = res.ToArray();
        }

        public override AppObject GetFirstValidAppObject()
        {
            return Parent.GetPrimaryAppObject();
        }

        public override string Title
        {
            get { return m_repr.TitlePlural; }
        }

        public override string TypeTitle
        {
            get { return m_repr.TitlePlural; }
        }

        public override Bitmap Image
        {
            get { return m_repr.Icon; }
        }

        public override List<IWidget> GetWidgets()
        {
            List<IWidget> res = base.GetWidgets();
            if (m_dbtype.SupportsLoadOverview) res.Add(new SpecificObjectsWidget(m_dbtype));
            return res;
        }

        [PopupMenuEnabled("s_create_new")]
        public bool CreateNewObjectEnabled()
        {
            if (m_conn.DatabaseCaps.ExecuteSql) return m_dbtype.SupportedCreateNew;
            return m_conn.AlterCaps[m_repr.ObjectType].Create;
        }

        [PopupMenu("s_create_new", ImageName = CoreIcons._newName, Weight = MenuWeights.NEWOBJECT)]
        public void CreateNewObject()
        {
            if (m_conn.DatabaseCaps.ExecuteSql)
            {
                OpenQueryParameters pars = new OpenQueryParameters();
                IPhysicalConnection newconn = GetConnection().Clone();
                string dbname = TreeNodeExtension.GetDatabaseName(this);
                if (dbname != null) newconn.AfterOpen += ConnTools.ChangeDatabaseCallback(dbname);
                pars.GenerateSql = delegate(IPhysicalConnection conn)
                {
                    return m_dbtype.GenerateCreateNew(conn.SystemConnection, TreeNodeExtension.GetAnyObjectPath(this));
                };
                pars.HideDesign = true;
                pars.ExecutedCallback = this.CompleteRefresh;
                MainWindow.Instance.OpenContent(new QueryFrame(newconn, pars));
            }
            else
            {
                string sql = m_dbtype.GenerateCreateNew(m_conn.Connection.SystemConnection, TreeNodeExtension.GetAnyObjectPath(this));
                var pars = new ObjectEditorPars { SavedCallback = this.CompleteRefresh };
                var frm = new SpecificObjectFrame(m_conn, m_repr.ObjectType, sql, pars);
                MainWindow.Instance.OpenContent(frm);
            }
        }

        #region IStructureCollectionTreeNode Members

        public List<IAbstractObjectStructure> InvokeLoadStructureList()
        {
            return m_conn.GetStructureList_NonEffective(dbs => dbs.SpecificObjects.Get(m_dbtype.ObjectType, null));
        }

        #endregion
    }

    public class SpecificObjectNode : ConnectionUsageTreeNodeBase, IStructureTreeNode
    {
        ISpecificObjectType m_dbtype;
        ObjectPath m_objpath;
        ISpecificRepresentation m_repr;
        IDatabaseSource m_conn;
        ObjectOperationCaps m_objCaps;
        //ISpecificObjectStructure m_struct;

        public SpecificObjectNode(SpecificObjectsNode parent, IDatabaseSource conn, ISpecificObjectType dbtype, ISpecificRepresentation repr, ObjectPath objpath)
            : base(new ConnectionWrapperUsage(parent.GetConnection()), parent, objpath.ObjectName.ToString())
        {
            m_dbtype = dbtype;
            m_repr = repr;
            m_objpath = objpath;
            m_conn = conn;
            m_objCaps = m_conn.AlterCaps[m_dbtype.ObjectType];
            var appobj = new SpecificObjectAppObject();
            appobj.FillFromDatabase(conn);
            appobj.DbObjectName = m_objpath.ObjectName;
            appobj.DbObjectType = dbtype.ObjectType;
            SetAppObject(appobj);
        }

        //private ISpecificObjectStructure Structure
        //{
        //    get
        //    {
        //        if (m_struct == null)
        //        {
        //            m_struct = m_conn.InvokeLoadSpecificObjectDetail(m_dbtype.ObjectType, m_objpath.ObjectName);
        //        }
        //        return m_struct;
        //    }
        //}

        public override string Title
        {
            get { return m_objpath.ToString(); }
        }

        public override string TypeTitle
        {
            get { return m_repr.TitlePlural; }
        }

        public override ITreeNode[] GetChildren()
        {
            var res = new List<ITreeNode>();
            if (m_dbtype.ObjectType == "view" && m_conn.DatabaseCaps.ExecuteSql)
            {
                res.Add(new Columns_TreeNode(
                    new GenericViewAsTableSource(m_conn, m_conn.Connection, m_conn.DatabaseName, m_objpath.ObjectName.Schema, m_objpath.ObjectName.Name),
                    this));
            }
            return res.ToArray();
        }

        public override System.Drawing.Bitmap Image
        {
            get { return m_repr.Icon; }
        }

        public override List<IWidget> GetWidgets()
        {
            List<IWidget> res = base.GetWidgets();
            res.AddRange(m_dbtype.GetWidgets());
            return res;
        }

        public override ObjectPath GetObjectPath()
        {
            return m_objpath;
        }

        private bool IsConnected()
        {
            if (m_conn.DatabaseCaps.ExecuteSql) return m_conn.Connection.IsOpened;
            return true;
        }

        //public override List<IAppObjectSqlGenerator> GetSqlGenerators()
        //{
        //    List<IAppObjectSqlGenerator> res = base.GetSqlGenerators();

        //    ISpecificObjectStructure so;
        //    try
        //    {
        //        so = LoadStructure();
        //    }
        //    catch (Exception err)
        //    {
        //        Errors.LogError(err);
        //        return res;
        //    }

        //    if (m_objCaps.Create)
        //    {
        //        res.Add(new DelegateSqlGenerator("CREATE", delegate(ITreeNode node, TextWriter tw, ISqlDialect dialectOverride)
        //        {
        //            ISqlDumper dmp = (m_conn.Dialect ?? dialectOverride ?? new GenericDialect()).CreateDumper(tw, SqlTemplates.TemplateFormatProps);
        //            dmp.CreateSpecificObject(so);
        //        }));
        //    }
        //    if (m_objCaps.Drop)
        //    {
        //        res.Add(new DelegateSqlGenerator("DROP", delegate(ITreeNode node, TextWriter tw, ISqlDialect dialectOverride)
        //        {
        //            ISqlDumper dmp = (m_conn.Dialect ?? dialectOverride ?? new GenericDialect()).CreateDumper(tw, SqlTemplates.TemplateFormatProps);
        //            dmp.DropSpecificObject(so);
        //        }));
        //    }
        //    if (m_objCaps.Create && m_objCaps.Drop)
        //    {
        //        res.Add(new DelegateSqlGenerator("ALTER", delegate(ITreeNode node, TextWriter tw, ISqlDialect dialectOverride)
        //        {
        //            ISqlDumper dmp = (m_conn.Dialect ?? dialectOverride ?? new GenericDialect()).CreateDumper(tw, SqlTemplates.TemplateFormatProps);
        //            dmp.DropSpecificObject(so);
        //            dmp.CreateSpecificObject(so);
        //        }));
        //    }

        //    return res;
        //}

        public override void GetPopupMenu(MenuBuilder menu)
        {
            base.GetPopupMenu(menu);
            m_dbtype.GetPopupMenu(menu, m_conn.Connection, m_objpath);
        }

        #region IStructureTreeNode Members

        public IAbstractObjectStructure InvokeLoadStructure()
        {
            return ((SpecificObjectAppObject)m_primaryAppobj).LoadStructure();
        }

        #endregion
    }
}
