using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace DatAdmin
{
    //[NodeFilter("s_database_list")]
    public class DatabasesTreeNode : LateLoadChildrenConnectionTreeNodeBase
    {
        IServerSource m_conn;

        public DatabasesTreeNode(IServerSource conn, ITreeNode parent)
            : base(conn, parent, "databases")
        {
            m_conn = conn;
        }

        public override string TypeTitle
        {
            get { return "s_database"; }
        }

        public override AppObject GetFirstValidAppObject()
        {
            return Parent.GetPrimaryAppObject();
        }

        [DragDropOperation(Name = "copydb", Title = "s_copy_database")]
        public void DragDrop_CopyDatabase(AppObject draggingObject)
        {
            var dbo = GetFirstValidAppObject() as ServerAppObject;
            if (dbo != null)
            {
                dbo.DragDrop_CopyDatabase(draggingObject);
                this.CompleteRefresh();
            }
        }

        //public override bool AllowDragDrop(ITreeNode draggingNode)
        //{
        //    //if (!RealNode.TreeBehaviour.AllowDragDrop) return false;
        //    if (draggingNode is IDatabaseTreeNode) return true;
        //    return false;
        //}

        protected override void DoGetChildren()
        {
            List<ITreeNode> res = new List<ITreeNode>();

            var dbs = new HashSetEx<string>();
            foreach (string name in m_conn.Databases)
            {
                res.Add(new Database_SourceTreeNode(m_conn.GetDatabase(name), this, name, true));
                if (name == null) continue;
                dbs.Add(name.ToLower());
            }
            string dbsdir = Parent.GetPrivateSubFolder("databases");
            if (Directory.Exists(dbsdir))
            {
                foreach (string subdir in Directory.GetDirectories(dbsdir))
                {
                    string dbname = System.IO.Path.GetFileName(subdir);
                    if (dbs.Contains(dbname.ToLower())) continue;
                    var db = new PhantomDatabaseSource(m_conn, subdir);
                    res.Add(new Database_SourceTreeNode(db, this, dbname, true));
                }
            }
            res.SortByKey(n => n.Title);
            m_children = res.ToArray();
        }
        public override bool PreparedChildren
        {
            get { return m_conn.Connection.IsOpened ? base.PreparedChildren : true; }
        }

        public override string Title
        {
            get { return "s_databases"; }
        }
        public override System.Drawing.Bitmap Image
        {
            get { return CoreIcons.database; }
        }

        [PopupMenuEnabled("s_create_database")]
        public bool MenuCreateDatabaseEnabled()
        {
            return Dialect != null && Dialect.DumperCaps.CreateDatabase;
        }

        [PopupMenu("s_create_database", ImageName = CoreIcons.databaseName, Weight = MenuWeights.NEWOBJECT)]
        public void MenuCreateDatabase()
        {
            var dbprops = new DatabaseProperties();
            dbprops.Name = "newdb";
            if (DatabasePropertiesForm.Run(dbprops, new GenericDatabaseSource(m_conn, m_conn.Connection, null), false))
            {
                if (ArrayTool.Contains(m_conn.Databases, dbprops.Name))
                {
                    StdDialog.ShowError(Texts.Get("s_database_allready_exists"));
                    return;
                }
                InvokeScript(fmt => { fmt.CreateDatabase(dbprops.Name, dbprops.SpecificData); });
                TreeNodeExtension.CompleteRefresh(this);
            }
        }
        public override List<IWidget> GetWidgets()
        {
            var res = base.GetWidgets();
            if (m_conn.IsFullAvailable()) res.Add(new DatabasesRawGridWidget());
            return res;
        }

        public override string GetPrivateSubFolder(string name)
        {
            return System.IO.Path.Combine(Parent.GetPrivateSubFolder("databases"), name);
        }

        public override bool ContainsDatabaseNode()
        {
            return true;
        }
    }

    //public class DatabaseMenuCommands
    //{
    //    IDatabaseSource m_conn;
    //    ITreeNode m_node;

    //    public DatabaseMenuCommands(IDatabaseSource conn, ITreeNode node)
    //    {
    //        m_conn = conn;
    //        m_node = node;
    //    }



    //    //public List<IAppObjectSqlGenerator> GetSqlGenerators()
    //    //{
    //    //    List<IAppObjectSqlGenerator> res = new List<IAppObjectSqlGenerator>();
    //    //    ISqlDialect dialect = m_conn.Dialect ?? new GenericDialect();
    //    //    res.Add(new DelegateSqlGenerator("CREATE ALL", delegate(ITreeNode node, TextWriter tw, ISqlDialect dialectOverride)
    //    //    {
    //    //        SqlTemplates.CreateAllObjects(m_conn, tw, dialectOverride ?? dialect);
    //    //    }));
    //    //    res.Add(new DelegateSqlGenerator("CREATE TABLES", delegate(ITreeNode node, TextWriter tw, ISqlDialect dialectOverride)
    //    //    {
    //    //        SqlTemplates.CreateTables(m_conn, tw, dialectOverride ?? dialect);
    //    //    }));
    //    //    res.Add(new DelegateSqlGenerator("CREATE REFERENCES", delegate(ITreeNode node, TextWriter tw, ISqlDialect dialectOverride)
    //    //    {
    //    //        SqlTemplates.CreateReferences(m_conn, tw, dialectOverride ?? dialect);
    //    //    }));
    //    //    res.Add(new DelegateSqlGenerator("CREATE SPECIFIC OBJECTS", delegate(ITreeNode node, TextWriter tw, ISqlDialect dialectOverride)
    //    //    {
    //    //        SqlTemplates.CreateSpecificObjects(m_conn, tw, dialectOverride ?? dialect);
    //    //    }));
    //    //    res.Add(new DelegateSqlGenerator("FILL ALL TABLES FROM OTHER DB", delegate(ITreeNode node, TextWriter tw, ISqlDialect dialectOverride)
    //    //    {
    //    //        SqlTemplates.FillAllTables(m_conn, tw, dialectOverride ?? dialect);
    //    //    }));
    //    //    res.Add(new DelegateSqlGenerator("CREATE DATABASE", delegate(ITreeNode node, TextWriter tw, ISqlDialect dialectOverride)
    //    //    {
    //    //        SqlTemplates.CreateDatabase(m_conn, tw, dialectOverride ?? dialect);
    //    //    }));
    //    //    res.Add(new DelegateSqlGenerator("DROP DATABASE", delegate(ITreeNode node, TextWriter tw, ISqlDialect dialectOverride)
    //    //    {
    //    //        SqlTemplates.DropDatabase(m_conn, tw, dialectOverride ?? dialect);
    //    //    }));
    //    //    return res;
    //    //}
    //}

    //public class Database_OfflineTreeNode : TreeNodeBase, IDatabaseTreeNode
    //{
    //    string m_dbname;
    //    string m_filepath;

    //    public Database_OfflineTreeNode(ITreeNode parent, string dbname, string filepath)
    //        : base(parent, dbname)
    //    {
    //        m_dbname = dbname;
    //        m_filepath = filepath;
    //    }

    //    public override string Title
    //    {
    //        get { return m_dbname; }
    //    }

    //    #region IDatabaseTreeNode Members

    //    public IDatabaseSource DatabaseConnection
    //    {
    //        get { return null; }
    //    }

    //    public void BeforeDataRefreshChilds()
    //    {
    //    }

    //    #endregion

    //    public override System.Drawing.Bitmap Image
    //    {
    //        get { return StdIcons.database_disconnected; }
    //    }

    //    public override string GetPrivateSubFolder(string name)
    //    {
    //        return System.IO.Path.Combine(m_filepath, name);
    //    }

    //    public override ITreeNode[] GetChildren()
    //    {
    //        var res = new List<ITreeNode>();
    //        res.Add(new BackupsOffline_TreeNode(this, GetPrivateSubFolder("backups")));
    //        res.Add(new SqlScripts_TreeNode(null,
    //        TreeNodeExtension.AddFolderNodes(res, "sqlscripts", (folder, postfix, namePostfix) => new SqlScripts_TreeNode(m_conn, folder, parent, postfix, namePostfix), m_conn);
            

    //        return m_commands.GetChildren(this, m_dbname);
    //    }
    //}

    public static class DatabaseNodeExtension
    {
        public static ITreeNode[] GetChildren(IDatabaseSource dbconn, ITreeNode parent, string dbname)
        {
            List<ITreeNode> res = new List<ITreeNode>();
            if (!dbconn.DatabaseCaps.IsPhantom)
            {
                res.Add(new Tables_TreeNode(dbconn, parent, false));
                if (dbconn.DatabaseCaps.MultipleSchema)
                {
                    res.Add(new Schemas_TreeNode(dbconn, parent));
                }
                if (dbconn.DatabaseCaps.Domains)
                {
                    res.Add(new Domains_TreeNode(dbconn, parent));
                }
            }
            parent.GetDbObjectNodes(dbconn, res, DbObjectParent.Database, new ObjectPath(dbname), false);
            if (dbconn.DatabaseCaps.IsPhantom)
            {
                if (LicenseTool.FeatureAllowed(DatabaseBackupFeature.Test))
                {
                    res.Add(new Backups_TreeNode(dbconn, parent));
                }
            }
            else
            {
                if (dbconn.IsFullAvailable())
                {
                    res.Add(new SystemDbObjectsNode(dbconn, parent, dbname));
                    if (LicenseTool.FeatureAllowed(DatabaseBackupFeature.Test) && dbconn.GetAnyDialect().DialectCaps.SupportBackup)
                    {
                        res.Add(new Backups_TreeNode(dbconn, parent));
                    }
                }
            }
            if (dbconn.GetPrivateSubFolder("sqlscripts") != null)
            {
                TreeNodeExtension.AddFolderNodes(res, "sqlscripts", (folder, postfix, namePostfix) => new SqlScripts_TreeNode(dbconn, folder, parent, postfix, namePostfix), dbconn);
            }
            return res.ToArray();
        }
    }

    public class Database_SourceTreeNode : ConnectionUsageTreeNodeBase, IDatabaseTreeNode
    {
        protected IDatabaseSource m_conn;
        protected string m_dbname;

        public Database_SourceTreeNode(IDatabaseSource conn, ITreeNode parent, string dbname, bool allowPhantom)
            : base(conn, parent, dbname + ((allowPhantom && conn.DatabaseCaps.IsPhantom) ? "_phantom" : ""))
        {
            m_conn = conn;
            m_dbname = dbname;
            Initialize();
        }

        public Database_SourceTreeNode(IDatabaseSource conn)
            : base(conn)
        {
            m_conn = conn;
            m_dbname = conn.DatabaseName;
            Initialize();
        }

        private void Initialize()
        {
            //CreateDbProperties(this, m_conn, m_dbname);
            var appobj = new DatabaseAppObject();
            appobj.FillFromDatabase(m_conn);
            SetAppObject(appobj);
        }

        //public override string GetDatabaseName() { return m_dbname; }

        public override ObjectPath GetObjectPath()
        {
            return new ObjectPath(m_dbname);
        }
        public override string TypeTitle
        {
            get { return "s_database"; }
        }
        public override ITreeNode[] GetChildren()
        {
            return DatabaseNodeExtension.GetChildren(m_conn, this, m_dbname);
            //return m_commands.GetChildren(this, m_dbname);
        }
        public override string Title
        {
            get
            {
                if (String.IsNullOrEmpty(m_dbname)) return "(" + Texts.Get("s_default_database") + ")";
                return m_dbname;
            }
        }
        public override System.Drawing.Bitmap Image
        {
            get
            {
                if (m_conn.DatabaseCaps.IsPhantom) return CoreIcons.database_disconnected;
                return CoreIcons.database;
            }
        }
        public override void NotifyExpanded()
        {
            if (m_conn.Settings != null && m_conn.Settings.Tree().AutoExpandTables)
            {
                IRealTreeNode child = RealNode.ChildByName("tables");
                if (child != null) MainWindow.Instance.RunInMainWindow(child.ExpandNode);
            }
        }
        //public override void GetScriptingNS(IDictionary<string, object> names, IPhysicalConnection dstConnection)
        //{
        //    names["db"] = new DatAdmin.Scripting.Database(m_conn, m_dbname);
        //}
        //public override void GetPopupMenu(MenuBuilder menu)
        //{
        //    base.GetPopupMenu(menu);
        //    m_commands.GetPopupMenu(menu);
        //}
        public IDatabaseSource DatabaseConnection { get { return m_conn; } }

        //public override bool AllowDragDrop(ITreeNode draggingNode)
        //{
        //    return m_commands.AllowDragDrop(draggingNode);
        //}
        //protected override IEnumerable<object> GetDragDropOperationObjects()
        //{
        //    foreach (var x in base.GetDragDropOperationObjects()) yield return x;
        //    yield return m_commands;
        //}
        public virtual void BeforeDataRefreshChilds() { }
        //internal static void CreateDbProperties(ITreeNode node, IDatabaseSource conn, string dbname)
        //{
        //    node.Properties["dbobject"] = "database";
        //    node.Properties["dbname"] = dbname;
        //    node.Properties["db"] = new DatAdmin.Scripting.Database(conn, dbname);
        //    node.DeriveProperties.Add("db");
        //    node.DeriveProperties.Add("dbname");
        //}
        //public override string PrivateFolder
        //{
        //    get
        //    {
        //        if (m_parent == null) return null;
        //        return m_parent.GetPrivateSubFolder(m_dbname);
        //    }
        //}

        public override string GetPrivateSubFolder(string name)
        {
            if (m_parent == null) return null;
            return System.IO.Path.Combine(m_parent.GetPrivateSubFolder(m_dbname), name);
        }

        //public override bool AllowDelete()
        //{
        //    return Dialect != null && Dialect.DumperCaps.DropDatabase;
        //}

        //public override bool DoDelete()
        //{
        //    if (MessageBox.Show(Texts.Get("s_really_drop$database", "database", m_dbname), VersionInfo.ProgramTitle, MessageBoxButtons.YesNo) == DialogResult.Yes)
        //    {
        //        InvokeScript(fmt => { fmt.DropDatabase(m_dbname); });
        //        return true;
        //    }
        //    return false;
        //}

        //public override bool AllowRename()
        //{
        //    return Dialect != null && Dialect.DumperCaps.RenameDatabase;
        //}

        //public override void RenameNode(string newname)
        //{
        //    InvokerExtension.Invoke(GetConnection(), (Action)delegate()
        //    {
        //        InvokeScript(fmt => { fmt.RenameDatabase(m_dbname, newname); });
        //    });
        //}

        //public override List<IAppObjectSqlGenerator> GetSqlGenerators()
        //{
        //    var res = base.GetSqlGenerators();
        //    res.AddRange(m_commands.GetSqlGenerators());
        //    return res;
        //}

        public override bool DoubleClick()
        {
            if (base.DoubleClick()) return true;
            return OpenTheBestDashboard();
        }
    }

    public class Database_SourceConnectionTreeNode : ConnectionTreeNode, IDatabaseTreeNode
    {
        protected IDatabaseSource m_conn;
        DatabaseAppObject m_infoappobj;

        //DatabaseMenuCommands m_commands;

        public Database_SourceConnectionTreeNode(IDatabaseSource conn, ITreeNode parent, IFileHandler fhandler, IStoredConnection stored, bool autoConnect)
            : base(parent, fhandler, stored, autoConnect)
        {
            m_conn = conn.ChangeConnection(ConnPack);
            SetConnection(m_conn.Connection);
            var appobj = new DatabaseAppObject();
            appobj.FillFromDatabase(conn);
            SetAppObject(appobj);

            m_infoappobj = new DatabaseAppObject();
            m_infoappobj.FillFromDatabase(conn);
            m_infoappobj.DisableAutoConnect = true;
            //m_commands = new DatabaseMenuCommands(m_conn, this);
            //Database_SourceTreeNode.CreateDbProperties(this, m_conn, null);
        }

        protected override bool AppObjectAvailable(AppObject appobj)
        {
            if (appobj is ServerFieldsAppObject)
            {
                return m_conn.IsFullAvailable() || AutoConnect;
            }
            return base.AppObjectAvailable(appobj);
        }

        public override System.Drawing.Bitmap Image
        {
            get
            {
                if (m_conn.Connection.IsOpened || AutoConnect) return CoreIcons.database;
                else return CoreIcons.database_disconnected;
            }
        }
        protected override void OnDisconnect()
        {
        }
        public override ITreeNode[] GetChildren()
        {
            if (m_conn.Connection.IsOpened || AutoConnect)
            {
                return DatabaseNodeExtension.GetChildren(m_conn, this, null);
                //return m_commands.GetChildren(this, null);
            }
            else
            {
                return new ITreeNode[] { };
            }
        }
        protected override void OnConnect()
        {
            RealNode.ExpandNode();
            if (m_conn.Settings.Tree().AutoExpandTables)
            {
                MainWindow.Instance.RunInMainWindow(RealNode.ChildByName("tables").ExpandNode);
            }
        }
        public override ObjectPath GetObjectPath()
        {
            return new ObjectPath(null);
        }
        //[PopupMenu("s_query")]
        //public void RunQuery()
        //{
        //    Toolkit.WindowToolkit.OpenQuery(m_conn.CloneConnection(), null);
        //}
        //[PopupMenu("s_db_backup")]
        //public void DbBackup()
        //{
        //    Database_SourceTreeNode.RunDbBackup(m_conn);
        //}

        public override string TypeTitle
        {
            get { return "s_database"; }
        }
        //public override void GetScriptingNS(IDictionary<string, object> names, IPhysicalConnection dstConnection)
        //{
        //    names["db"] = new DatAdmin.Scripting.Database(m_conn);
        //}
        //public override void GetPopupMenu(MenuBuilder menu)
        //{
        //    base.GetPopupMenu(menu);
        //    m_commands.GetPopupMenu(menu);
        //}
        public override void NotifyExpanded()
        {
            if (m_conn.Settings.Tree().AutoExpandTables)
            {
                try
                {
                    MainWindow.Instance.RunInMainWindow(RealNode.ChildByName("tables").ExpandNode);
                }
                catch (Exception) { }
            }
        }
        public IDatabaseSource DatabaseConnection { get { return m_conn; } }
        //public override bool AllowDragDrop(ITreeNode draggingNode)
        //{
        //    return m_commands.AllowDragDrop(draggingNode);
        //}
        //protected override IEnumerable<object> GetDragDropOperationObjects()
        //{
        //    foreach (var x in base.GetDragDropOperationObjects()) yield return x;
        //    yield return m_commands;
        //}
        public virtual void BeforeDataRefreshChilds() { }

        //public override List<IAppObjectSqlGenerator> GetSqlGenerators()
        //{
        //    var res = base.GetSqlGenerators();
        //    res.AddRange(m_commands.GetSqlGenerators());
        //    return res;
        //}
        //public override void DoDeleteFile()
        //{
        //    Disconnect();
        //    base.DoDeleteFile();
        //}

        public override AppObject GetFirstValidAppObject()
        {
            if (!m_conn.IsFullAvailable()) return m_infoappobj;
            return m_primaryAppobj;
        }

        public override bool DoubleClick()
        {
            if (base.DoubleClick()) return true;
            return OpenTheBestDashboard();
        }
    }
}
