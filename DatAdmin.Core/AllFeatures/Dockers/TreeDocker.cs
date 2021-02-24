using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class TreeDocker : DockerBase
    {
        public TreeDocker(string rootpath, IDockerFactory factory, bool canreturnselected, bool autoExpandAll)
            : base(factory)
        {
            InitializeComponent();

            HTree.GetFocusedNode += Tree_GetFocusedNode;
            if (canreturnselected) HTree.GetSelectedNode += Tree_GetSelectedNode;
            HTree.RefreshRoot += HTree_RefreshRoot;
            HTree.SelectNode += HTree_SelectNode;
            //HTree.SelectAppObject += HTree_SelectAppObject;
            daTreeView1.TreeBehaviour.AfterDeletedNode = AfterDeleteNode;
            daTreeView1.RootPath = rootpath;
            if (autoExpandAll)
            {
                daTreeView1.FillAndExpandAll();
            }
        }

        //void HTree_SelectAppObject(AppObject appobj, SelectNodeFlags flags)
        //{
        //    daTreeView1.SelectAppObject(appobj, flags);
        //}

        void HTree_SelectNode(string path, SelectNodeFlags flags)
        {
            if (path.StartsWith(daTreeView1.RootPath)) 
            {
                var rnode = daTreeView1.FindNode(path, true);
                if (rnode != null)
                {
                    daTreeView1.SelectNode(rnode, flags);
                }
            }
        }

        void HTree_RefreshRoot()
        {
            daTreeView1.RefreshRoot();
        }

        void Tree_GetSelectedNode(List<ITreeNode> nodes)
        {
            if (daTreeView1.Selected != null)
            {
                nodes.Add(daTreeView1.Selected);
            }
        }

        void Tree_GetFocusedNode(List<ITreeNode> nodes)
        {
            if (daTreeView1.FocusedTree && daTreeView1.Selected != null)
            {
                nodes.Add(daTreeView1.Selected);
            }
        }

        private void AfterDeleteNode(ITreeNode node)
        {
            HTree.CallAfterDeleteNode(node);
        }

        private void daTreeView1_ActiveNodeChange(object sender, EventArgs e)
        {
            HTree.CallFocusedNodeChanged();
        }

        public override void OnClose()
        {
            HTree.GetFocusedNode -= Tree_GetFocusedNode;
            HTree.GetSelectedNode -= Tree_GetSelectedNode;
            HTree.RefreshRoot -= HTree_RefreshRoot;
            HTree.SelectNode -= HTree_SelectNode;
            //HTree.SelectAppObject -= HTree_SelectAppObject;
        }
    }

    [DockerFactory(Title = "Connection tree", Name = "connection_tree")]
    public class DataTreeDockerFactory : DockerFactoryBase
    {
        public override IDocker CreateDocker()
        {
            return new TreeDocker("data:", this, true, false);
        }

        public override string MenuTitle
        {
            get { return "s_connection_tree"; }
        }

        public override Bitmap Icon
        {
            get { return CoreIcons.connect; }
        }

        public override DockerState InitialState
        {
            get { return DockerState.DockLeft; }
        }

        public override Keys Shortcut
        {
            get { return Keys.Control | Keys.Alt | Keys.T; }
        }
    }

    [DockerFactory(Title = "Addons tree", Name = "addons_tree")]
    public class AddonsTreeDockerFactory : DockerFactoryBase
    {
        public override IDocker CreateDocker()
        {
            return new TreeDocker("addons:", this, false, false);
        }

        public override string MenuTitle
        {
            get { return "s_addons"; }
        }

        public override Bitmap Icon
        {
            get { return CoreIcons.settings; }
        }

        public override DockerState InitialState
        {
            get { return DockerState.DockLeft; }
        }

        public override Keys Shortcut
        {
            get { return Keys.Control | Keys.Alt | Keys.S; }
        }
    }

    [DockerFactory(Title = "Scripts tree", Name = "scripts_tree")]
    public class ScriptsTreeDockerFactory : DockerFactoryBase
    {
        public override IDocker CreateDocker()
        {
            return new TreeDocker("scripts:", this, false, false);
        }

        public override string MenuTitle
        {
            get { return "s_scripts"; }
        }

        public override Bitmap Icon
        {
            get { return CoreIcons.sql ; }
        }

        public override DockerState InitialState
        {
            get { return DockerState.DockLeft; }
        }

        public override Keys Shortcut
        {
            get { return Keys.Control | Keys.Alt | Keys.Q; }
        }
    }

    [DockerFactory(Title = "Favorites tree", Name = "favorites_tree")]
    public class FavoritesTreeDockerFactory : DockerFactoryBase
    {
        public override IDocker CreateDocker()
        {
            return new TreeDocker("favorites:", this, false, true);
        }

        public override string MenuTitle
        {
            get { return "s_favorites"; }
        }

        public override Bitmap Icon
        {
            get { return CoreIcons.favorite; }
        }

        public override DockerState InitialState
        {
            get { return DockerState.DockLeft; }
        }

        public override Keys Shortcut
        {
            get { return Keys.Control | Keys.Alt | Keys.F; }
        }
    }

    [PluginHandler]
    public class TreeLoadPluginHandler : PluginHandlerBase
    {
        public override void OnLoadedAllPlugins()
        {
            HTree.SelectNode += HTree_SelectNode;
        }

        void HTree_SelectNode(string path, SelectNodeFlags flags)
        {
            if (path.StartsWith("data:"))
            {
                MainWindow.Instance.ShowDocker(new DataTreeDockerFactory());
            }
            if (path.StartsWith("addons:"))
            {
                MainWindow.Instance.ShowDocker(new AddonsTreeDockerFactory());
            }
            if (path.StartsWith("scripts:"))
            {
                MainWindow.Instance.ShowDocker(new ScriptsTreeDockerFactory());
            }
            if (path.StartsWith("favorites:"))
            {
                MainWindow.Instance.ShowDocker(new FavoritesTreeDockerFactory());
            }
            if (path.StartsWith("charts:"))
            {
                MainWindow.Instance.ShowDocker(new ChartsTreeDockerFactory());
            }
        }
    }
}
