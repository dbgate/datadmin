using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DatAdmin
{
    public static class TreeNodeExtension
    {
        public static ObjectPath GetObjectPath(this ITreeNode node)
        {
            if (node == null) return null;
            var appobj = node.GetPrimaryAppObject();
            if (appobj == null) return null;
            return appobj.GetObjectPath();
        }

        public static ObjectPath GetAnyObjectPath(this ITreeNode node)
        {
            while (node != null && node.GetObjectPath() == null)
            {
                node = node.Parent;
            }
            if (node != null) return node.GetObjectPath();
            return null;
        }

        public static IPhysicalConnection GetAnyConnection(this ITreeNode node)
        {
            while (node != null && node.GetConnection() == null)
            {
                node = node.Parent;
            }
            if (node != null) return node.GetConnection();
            return null;
        }

        public static string GetDatabaseName(this ITreeNode node)
        {
            IDatabaseTreeNode dbnode = GetDatabaseNode(node);
            if (dbnode != null) return dbnode.DatabaseConnection.DatabaseName;
            return null;
        }

        public static IDatabaseTreeNode GetDatabaseNode(this ITreeNode node)
        {
            while (node != null && !(node is IDatabaseTreeNode))
            {
                node = node.Parent;
            }
            return (IDatabaseTreeNode)node;
        }

        public static List<IWidget> GetAllWidgets(this ITreeNode node)
        {
            List<IWidget> res = new List<IWidget>();
            if (node == null) return res;
            res.AddRange(node.GetWidgets());
            var appobj = node.GetPrimaryAppObject();
            var dialect = node.GetNodeDialect();
            if (appobj != null)
            {
                appobj.GetWidgets(res);
                if (dialect != null) dialect.GetAdditionalWidgets(res, appobj);
            }
            if (dialect != null)
            {
                dialect.GetAdditionalWidgets(res, node);
                dialect.ReplaceStandardWidgets(res, node);
            }
            foreach (AddonHolder hld in TreeExtenderAddonType.Instance.CommonSpace.GetAllAddons())
            {
                var ext = (ITreeExtender)hld.InstanceModel;
                ext.GetExtendedWidgets(node, res);
            }
            return res;
        }

        public static ISqlDialect GetNodeDialect(this ITreeNode node)
        {
            while (node != null)
            {
                var cnode = node as ConnectionTreeNode;
                if (cnode != null && cnode.StoredConnection != null) return cnode.StoredConnection.GetDialect();
                node = node.Parent;
            }
            return null;
        }

        public static void CompleteRefresh(this ITreeNode node, bool deep)
        {
            if (node == null) return;
            node.DataRefresh();
            if (deep) node.NotifyDeepRefresh();
            TreeNodeBase.CallRefresh(node, deep);
        }

        public static void CompleteRefresh(this ITreeNode node)
        {
            node.CompleteRefresh(false);
        }

        //public static IAppObjectSqlGenerator[] GetSqlGenerators(this ITreeNode node)
        //{
        //    List<IAppObjectSqlGenerator> res = new List<IAppObjectSqlGenerator>();
        //    if (node == null) return res.ToArray();
        //    res.AddRange(node.GetSqlGenerators());
        //    return res.ToArray();
        //}

        public static void GetDbObjectNodes(this ITreeNode parent, IDatabaseSource conn, List<ITreeNode> res, DbObjectParent parentType, ObjectPath parpath, bool isSystem)
        {
            foreach (ISpecificObjectType dbtype in conn.GetSpecificTypes())
            {
                if (dbtype.ParentType == parentType)
                {
                    if (isSystem && !dbtype.HasSystemVariant) continue;
                    var repr = SpecificRepresentationAddonType.Instance.FindRepresentation(dbtype.ObjectType);
                    if (!repr.ShowInTree) continue;
                    res.Add(new SpecificObjectsNode(conn, parent, dbtype, repr, parpath, isSystem));
                }
            }
        }

        public static void AddFolderNodes(List<ITreeNode> res, string folderName, Func<IVirtualFolder, string, string, ITreeNode> createNode, IDatabaseSource conn)
        {
            res.Add(createNode(
                new DiskFolder(conn.GetPrivateSubFolder(folderName)),
                String.Format(" - {0}", Texts.Get("s_local")),
                "_local"
            ));
            DXDriver driver = null;
            if (conn.Connection != null && !conn.OfflineDatabaseCaps.IsPhantom) driver = conn.Connection.GetDXDriver(conn.DatabaseName);
            if (driver != null)
            {
                res.Add(createNode(
                    driver.GetFolder(folderName, conn.DatabaseName),
                    String.Format(" - {0}", Texts.Get("s_on_server")),
                    "_onserver"
                ));
            }
        }

        //public static List<IVirtualFolder> GetFolderSet(this ITreeNode node, string dbsubfolder)
        //{
        //    ITreeNode seldb = node.GetDatabaseNode();
        //    if (seldb == null) return null;

        //    string dbname = node.GetDatabaseName();
        //    IPhysicalConnection conn = MainWindowExtension.SelectedConnection;
        //    if (conn != null && conn.SystemConnection != null)
        //    {
        //        DXDriver driver = PhysicalConnectionExtension.GetDXDriver(conn);
        //        IVirtualFolder local = new FileSystemFolder(seldb.GetPrivateSubFolder(dbsubfolder));
        //        List<IVirtualFolder> res = new List<IVirtualFolder>();
        //        res.Add(local);
        //        if (driver != null)
        //        {
        //            IVirtualFolder onserver = driver.GetFolder(dbsubfolder, dbname);
        //            res.Add(onserver);
        //        }
        //        return res;
        //    }
        //    return null;
        //}

        public static ITreeNode[] GetChildrenNow(this ITreeNode node)
        {
            if (!node.PreparedChildren)
            {
                IAsyncResult async = node.BeginLoadChildren(null);
                Async.WaitFor(async);
                node.EndLoadChildren(async);
            }
            return node.GetChildren();
        }

        public static bool IsTableNodeOrParent(this ITreeNode node, bool mustHaveData)
        {
            if (node.GetType() == typeof(VirtualFolderTreeNode)) return true;
            if (node is ConnectionTreeNode) return true;
            if (node is IDatabaseTreeNode) return true;
            if (node is DatabasesTreeNode) return true;
            if (node is Tables_TreeNode) return true;

            if (node is Table_SourceTreeNode)
            {
                Table_SourceTreeNode tnode = (Table_SourceTreeNode)node;
                if (!tnode.HasTabularData && mustHaveData) return false;
                return true;
            }

            if (!mustHaveData)
            {
                if (node is Database_SourceConnectionTreeNode) return true; // dbdef node
            }

            return false;
        }

        public static bool IsDatabaseNodeOrParent(this ITreeNode node)
        {
            if (node == null) return false;
            if (node.ContainsDatabaseNode()) return true;
            if (node is IDatabaseTreeNode) return true;
            return false;
        }

        public static bool IsQueryableNodeOrParent(this ITreeNode node)
        {
            if (node.GetType() == typeof(VirtualFolderTreeNode)) return true;
            if (node.GetConnection() != null)
            {
                if (node.Parent != null && node.Parent.GetConnection() != null) return false;
                var dbnode = node as IDatabaseTreeNode;
                if (dbnode != null && dbnode.DatabaseConnection != null) return dbnode.DatabaseConnection.DatabaseCaps.ExecuteSql;
                if (node is Server_SourceConnectionTreeNode) return true;
            }
            return false;
        }

        public static bool CheckedAllChildren(this IRealTreeNode node)
        {
            foreach (var child in node.RealChildren)
            {
                if (!child.NodeChecked) return false;
            }
            return true;
        }

        public static void CreateChildTextFile(this ITreeNode node, string file, string data)
        {
            if (node is VirtualFolderTreeNode)
            {
                var vnode = node as VirtualFolderTreeNode;
                var vf = vnode.Folder.GetFile(file);
                vf.SaveText(data);
            }
            //if (node is VirtualFolderTreeNode)
            //{
            //    using (StreamWriter fw = new StreamWriter(System.IO.Path.Combine(node.FileSystemPath, file), false))
            //    {
            //        fw.Write(data);
            //    }
            //}
        }

        public static AppObject FindTabularDataObject(this ITreeNode node)
        {
            if (node == null) return null;
            var appobj = node.GetPrimaryAppObject();
            if (appobj != null && appobj.HasTabularData) return appobj;
            return null;
        }

        public static string FileNameToDataTreeName(string filename)
        {
            if (filename == null) return null;
            filename = filename.Replace("/", "\\");
            if (filename != null && IOTool.FileIsInDirectory(filename, Core.DataDirectory))
            {
                return IOTool.RelativePathTo(Core.DataDirectory, filename).ToLower().Replace("\\", "/");
            }
            return null;
        }

        public static string DataTreeNameToFileName(string datatreename)
        {
            if (datatreename != null) return Path.Combine(Core.DataDirectory, datatreename);
            return null;
        }

        public static void CallCopy(this ITreeNode node)
        {
            var appobjs = new List<AppObject>(node.GetValidAppObjects());
            if (appobjs.Count == 0) return;
            ObjectClipboard.Memory = appobjs.ToArray();
        }

        public static bool CallCopyEnabled(this ITreeNode node)
        {
            var appobjs = new List<AppObject>(node.GetValidAppObjects());
            return appobjs.Count > 0;
        }

        public static DatabaseCache FindDatabaseCache(this ITreeNode node)
        {
            if (node == null) return null;
            var dnode = node.GetDatabaseNode();
            if (dnode == null) return null;
            var cache = dnode.DatabaseConnection.GetCache();
            return cache;
        }
    }
}
