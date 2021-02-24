using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace DatAdmin
{
    //public delegate void NodeCallback(ITreeNode node);

    public static class NodeFactory
    {
        //private static IEnumerable<INodeFactory> EnumFactories()
        //{
        //    foreach (var item in NodeFactoryAddonType.Instance.CommonSpace.GetAllAddons())
        //    {
        //        yield return (INodeFactory)item.InstanceModel;
        //    }
        //}

        //public static ITreeNode FromFile(ITreeNode parent, string file)
        //{
        //    foreach (INodeFactory fact in EnumFactories())
        //    {
        //        ITreeNode node = fact.FromFile(parent, file);
        //        if (node != null) return node;
        //    }
        //    return null;
        //}
        //public static ITreeNode FromVirtualFile(ITreeNode parent, IVirtualFile file)
        //{
        //    foreach (INodeFactory fact in EnumFactories())
        //    {
        //        ITreeNode node = fact.FromVirtualFile(parent, file);
        //        if (node != null) return node;
        //    }
        //    return null;
        //}

        public static ITreeNode FromFile(ITreeNode parent, IVirtualFile file)
        {
            var han = FileHandlerAddonType.FindFileHandler(file, h => h.Caps.CreateNode);
            if (han == null) return null;
            return han.CreateNode(parent);
        }

        public static ITreeNode CreateRoot(string protocol)
        {
            switch (protocol)
            {
                case "addons":
                    return new AddonsRootTreeNode();
                case "data":
                    return new RootTreeNode();
                case "scripts":
                    return new ScriptsRootTreeNode();
                case "charts":
                    return new ChartsRootTreeNode();
                case "favorites":
                    return new FavoritesRootTreeNode();
                case "config":
                    return new ConfigRootTreeNode(AppDataDiskFileSystem.Instance.Root);
            }
            throw new InternalError("DAE-00017 Unknown protocol:" + protocol);
        }
        /// returns node, can wait to connect databases
        public static ITreeNode GetNodeFromPath(string path)
        {
            //if (path == "") return CreateRoot();
            string[] p = path.Split('/');
            if (!p[0].EndsWith(":")) throw new TreeNodeNotFoundException(path);
            ITreeNode item = CreateRoot(p[0].Substring(0, p[0].Length - 1));
            //if (p[0] != "data:") throw new TreeNodeNotFoundException(path);
            //ITreeNode item = CreateRoot();
            for (int i = 1; i < p.Length; i++)
            {
                item = item.GetNamedChild(p[i]);
            }
            return item;
        }
    }
}
