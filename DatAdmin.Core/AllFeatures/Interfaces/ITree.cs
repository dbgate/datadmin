using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace DatAdmin
{
    public delegate ITreeNode CreateRootNodeDelegate();

    public class TreeBehaviour
    {
        public bool ForceAutoConnect;
        public bool AllowDragDrop;
        public bool AllowDoubleClickNodeHandling;
        public bool AllowPopupMenu;
        public bool AllowRenameNode;
        public bool HideToolbar;
        public bool InfluenceGlobalTrees;
        public Func<ITreeNode, bool> ShowFilter;
        public Action<ITreeNode> AfterDeletedNode;
    }

    public interface IRealTreeNode
    {
        void RefreshChilds(bool deep);
        void RefreshSelf();
        IInvoker Invoker { get;}
        void ExpandNode();
        void CollapseNode();
        IRealTreeNode ChildByName(string name);
        IEnumerable<IRealTreeNode> RealChildren { get; }
        /// <summary>
        /// true if KillNode was called on this node
        /// </summary>
        bool IsDead { get; }
        /// <summary>
        /// makes node dead, after next refreshing this not is not reused, 
        /// but new object is created
        /// </summary>
        void KillNode();
        void ShowRenameEditor();
        TreeBehaviour TreeBehaviour { get; }
        bool NodeChecked { get; set; }
        Color NodeTextColor { get; set; }
        ITreeNode LogicalNode { get; }
        void ClearChilds();
        void EnsureVisibleNode();
    }

    public class TreeNodeNotFoundException : Exception
    {
        public string Node;
        public TreeNodeNotFoundException(string node) { Node = node; }
    }

    public class NodeInvalidOperationException : Exception
    {
    }

    public interface ITreeNode
    {
        IRealTreeNode RealNode { get; set;}

        /// text, which will displayed
        string Title { get;}

        string TypeTitle { get;}

        /// language independend node name, can be used for node pathing
        string Name { get;}

        /// image
        Bitmap Image { get;}

        /// image
        Bitmap ExpandedImage { get;}

        ITreeNode[] GetChildren();

        /// prepares children
        IAsyncResult BeginLoadChildren(AsyncCallback callback);
        void EndLoadChildren(IAsyncResult async);

        bool PreparedChildren { get;}

        /// creates popup menu (this popup menu can be extended from plugins)
        void GetPopupMenu(MenuBuilder menu);

        ITreeNode Parent { get;}

        string Path { get;}

        void DataRefresh();
        void NotifyDeepRefresh();

        /// finds child node with name child_name, throws TreeNodeNotFoundException, if not found
        ITreeNode GetNamedChild(string child_name);

        /// gets file system path, throws, if node is not represented by file system object
        string FileSystemPath { get;}

        /// <summary>
        /// gets private folder for object (in most cases it doesn't exist, 
        /// folder is created, when it is needed)
        /// </summary>
        string GetPrivateSubFolder(string name);

        bool DoubleClick();
        void ModifiedDoubleClick(Keys keys);

        bool Delete();

        void NotifyExpanded();

        // getts associated connection, if any
        IPhysicalConnection GetConnection();

        //// property access
        //string GetProperty(string name);
        //bool HasProperty(string name);
        //IEnumerable<string> EnumProperties();

        //IDictionary<string, object> Properties { get;}

        /// <summary>
        /// which properties to copy to children
        /// </summary>
        //IList<string> DeriveProperties { get;}

        //void GetScriptingNS(IDictionary<string, object> names);

        bool AllowExpand();

        void DragDrop(AppObject[] draggingObjects);
        bool AllowDragDrop(AppObject[] draggingObjects);
        DragDropBuilder GetDragDropBuilder(AppObject[] draggingObjects);

        //string GetDatabaseName();
        //ObjectPath GetObjectPath();
        bool AllowCreate(string group, string name);

        // tabular data of node; prefered way is to define tabular data in app objects and ignore these functions
        bool HasTabularData { get;}
        ITabularDataView GetTabularData();

        event EventHandler ChangedProperties;
        List<IWidget> GetWidgets();
        void RenameNode(string newname);
        bool AllowRename();
        bool DoDelete();
        bool AllowDelete();
        string GetRenamingText();
        //List<IAppObjectSqlGenerator> GetSqlGenerators();
        void CloseAllResources();
        void AfterUserCheck();

        // allow reuse this node if refreshing
        bool AllowReuse();

        bool AllowNewChild();
        bool NewChild();

        AppObject GetPrimaryAppObject();
        AppObject GetFirstValidAppObject();
        IEnumerable<AppObject> GetValidAppObjects();

        ConnectionPack ConnPack { get; set; }
        void RemovedConnection(string connkey);

        bool ContainsDatabaseNode();
    }

    //public interface ISubContentTreeNode
    //{
    //    void AddSubFile(string relpath);
    //    void AddSubFolder(string relpath);
    //    //List<string> SubFiles { get; }
    //    //List<string> SubFolders { get; }
    //}

    //[NodeFilter("s_database")]
    public interface IDatabaseTreeNode : ITreeNode
    {
        IDatabaseSource DatabaseConnection { get;}
        void BeforeDataRefreshChilds();
    }

    public interface IStructureTreeNode : ITreeNode
    {
        IAbstractObjectStructure InvokeLoadStructure();
    }

    public interface IStructureCollectionTreeNode : ITreeNode
    {
        List<IAbstractObjectStructure> InvokeLoadStructureList();
    }

    //[NodeFilter("s_schema")]
    //public interface ISchemaTreeNode
    //{
    //    // SchemaConnection can be null, then node dynamically doesn't support ISchemaNode
    //    ISchemaSource SchemaConnection { get;}
    //    void BeforeDataRefreshChilds();
    //}

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class TreeRootAttribute : Attribute
    {
        public TreeRootAttribute(string protocol)
        {
            Protocol = protocol;
        }
        public string Protocol;
    }
}
