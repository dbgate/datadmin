using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace DatAdmin
{
    public abstract class ConfigNode : TreeNodeBase
    {
        public List<ConfigNode> Children = new List<ConfigNode>();

        public IVirtualPath VirtualPath;

        public string _Title;
        public Bitmap _Image;
        public bool _AutoSelect = true;
        public object Tag;
        bool m_selected;

        public ConfigNode(ITreeNode parent, IVirtualPath path)
            : base(parent, path.Name)
        {
            VirtualPath = path;
        }

        public ConfigNode(IVirtualPath rootPath)
            : base("config")
        {
            VirtualPath = rootPath;
        }

        public event EventHandler SelectedChanged;

        public override ITreeNode[] GetChildren()
        {
            var res = new List<ITreeNode>();
            foreach (var c in Children) res.Add(c);
            return res.ToArray();
        }

        public override string Title
        {
            get { return _Title; }
        }

        public override Bitmap Image
        {
            get { return _Image; }
        }

        public override string TypeTitle
        {
            get { return "s_configuration"; }
        }

        private void SelectParentTrue()
        {
            if (Parent != null)
            {
                ((ConfigNode)Parent).SelectThis(true);
                ((ConfigNode)Parent).SelectParentTrue();
            }
        }

        private void SelectChildren(bool value)
        {
            foreach (var child in Children)
            {
                if (!child._AutoSelect) continue;
                child.SelectThis(value);
                child.SelectChildren(value);
            }
        }

        public void SelectThis(bool value)
        {
            bool oldval = m_selected;
            m_selected = value;
            if (RealNode != null) RealNode.NodeChecked = value;
            if (oldval != value && SelectedChanged != null) SelectedChanged(this, EventArgs.Empty);
        }

        public void Select(bool value)
        {
            SelectThis(value);
            if (value) SelectParentTrue();
            SelectChildren(value);
        }

        public override void AfterUserCheck()
        {
            Select(RealNode.NodeChecked);
        }

        public override void OnSetRealNode()
        {
            base.OnSetRealNode();
            if (RealNode != null) RealNode.NodeChecked = m_selected;
        }

        protected string GetNewName(IVirtualPath oldpath, List<Tuple<string, string>> repls)
        {
            string path = oldpath.FullPath;
            if (repls != null)
            {
                foreach (var tpl in repls)
                {
                    if (path.StartsWith(tpl.V1))
                    {
                        path = tpl.V2 + path.Substring(tpl.V1.Length);
                        break;
                    }
                    if (tpl.V1.EndsWith("$") && path == tpl.V1.Substring(0, tpl.V1.Length - 1))
                    {
                        path = tpl.V2;
                        break;
                    }
                }
            }
            return path;
        }

        public void CopyCheckedTo(IVirtualFileSystem dstfs, bool isroot, List<Tuple<string, string>> repls)
        {
            if (!m_selected) return;
            if (!isroot)
            {
                string path = GetNewName(VirtualPath, repls);
                VirtualPath.CopyPathTo(dstfs, path);
            }
            foreach (var child in Children)
            {
                child.CopyCheckedTo(dstfs, false, repls);
            }
            CopyContentTo(dstfs, isroot, repls);
        }

        protected virtual void CopyContentTo(IVirtualFileSystem dstfs, bool isroot, List<Tuple<string, string>> repls)
        {
        }

        public void GetAllNodes(List<ConfigNode> res)
        {
            res.Add(this);
            foreach (var child in Children)
            {
                child.GetAllNodes(res);
            }
        }

        public bool Selected
        {
            get { return m_selected; }
        }

        public virtual void GetReplacePaths(List<Tuple<string, string>> repls)
        {
            foreach (var child in Children) child.GetReplacePaths(repls);
        }
    }

    public class ConfigRootTreeNode : ConfigNode
    {
        public ConfigRootTreeNode(IVirtualFolder root)
            : base(root)
        {
        }
    }

    public class ConfigFileNode : ConfigNode
    {
        public List<IVirtualFile> SubVirtFiles = new List<IVirtualFile>();
        public string NewName;
        public bool AutoSubFiles = false;

        public ConfigFileNode(ITreeNode parent, IVirtualFile file)
            : base(parent, file)
        {
        }

        public override void GetReplacePaths(List<Tuple<string, string>> repls)
        {
            base.GetReplacePaths(repls);
            if (!NewName.IsEmpty())
            {
                string p = VirtualPath.FullPath;
                int p1 = p.LastIndexOf('/');
                int p2 = p.IndexOf('.', p1);
                p = p.Substring(0, p1 + 1) + NewName + p.Substring(p2);
                repls.Add(new Tuple<string, string>(VirtualPath.FullPath + "$", p));
                repls.Add(new Tuple<string, string>(VirtualPath.FullPath + "/", p + "/"));
                repls.Add(new Tuple<string, string>(VirtualPath.FullPath + ".", p + "."));
            }
        }

        protected override void CopyContentTo(IVirtualFileSystem dstfs, bool isroot, List<Tuple<string, string>> repls)
        {
            base.CopyContentTo(dstfs, isroot, repls);

            foreach (var sub in SubVirtFiles)
            {
                string path = GetNewName(sub, repls);
                sub.CopyPathTo(dstfs, path);
            }
        }
    }

    public class ConfigFolderNode : ConfigNode
    {
        public ConfigFolderNode(ITreeNode parent, IVirtualFolder folder)
            : base(parent, folder)
        {
        }
    }

    public static class ConfigLoader
    {
        public static ConfigNode LoadConfig(IVirtualFileSystem fs)
        {
            var root = new ConfigRootTreeNode(fs.Root);
            LoadItems(fs.Root, root);
            return root;
        }

        private static void LoadItems(IVirtualFolder folder, ConfigNode parentnode)
        {
            var loaded = new Dictionary<string, ConfigFileNode>();
            List<ConfigNode> result = new List<ConfigNode>();

            var files = folder.LoadFiles();
            files.Sort((a, b) => a.Name.Length - b.Name.Length);

            foreach (IVirtualFile file in files)
            {
                if (PrefixTest(loaded, file)) continue;
                var childnode = ConfigNodeHandlerAddonType.Instance.LoadFile(parentnode, file);
                if (childnode == null) continue;
                parentnode.Children.Add(childnode);
                loaded[file.Name] = childnode;
            }

            foreach (IVirtualFolder cf in folder.LoadFolders())
            {
                var master = PrefixTest(loaded, cf);
                if (master != null)
                {
                    var childnode = ConfigNodeHandlerAddonType.Instance.LoadSubFolder(master, cf);
                    if (childnode == null) childnode = new ConfigFolderNode(master, cf)
                    {
                        _Title = cf.Name.Substring(master.Name.Length),
                        _Image = CoreIcons.img_folder,
                    };
                    childnode._AutoSelect = false;
                    LoadItems(cf, childnode);
                    master.Children.Add(childnode);
                }
                else
                {
                    var childnode = ConfigNodeHandlerAddonType.Instance.LoadFolder(parentnode, cf);
                    if (childnode == null) continue;
                    LoadItems(cf, childnode);
                    parentnode.Children.Add(childnode);
                }
            }
        }

        private static ConfigFileNode PrefixTest(Dictionary<string, ConfigFileNode> loaded, IVirtualFolder subfolder)
        {
            foreach (string prefix in loaded.Keys)
            {
                if (subfolder.Name.StartsWith(prefix + "."))
                {
                    var node = loaded[prefix];
                    return node;
                }
            }
            return null;
        }

        private static bool PrefixTest(Dictionary<string, ConfigFileNode> loaded, IVirtualFile file)
        {
            foreach (string prefix in loaded.Keys)
            {
                if (file.Name.StartsWith(prefix + "."))
                {
                    var node = loaded[prefix];
                    if (node.AutoSubFiles)
                    {
                        node.SubVirtFiles.Add(file);
                    }
                    else
                    {
                        var childnode = ConfigNodeHandlerAddonType.Instance.LoadSubFile(node, file);
                        if (childnode == null) continue;
                        loaded[prefix].Children.Add(childnode);
                    }
                    return true;
                }
            }
            return false;
        }
    }

    [ConfigNodeHandler(Name = "contree")]
    public class ConnectionTreeConfigLoader : ConfigNodeHandlerBase
    {
        public ConnectionTreeConfigLoader()
        {
            DefineFolder("s_connection_tree", CoreIcons.connect, "data", true);
            DefineFile("data", ".con", CoreIcons.dbserver, true);
            DefineSubFolder("data", "databases", "s_databases", CoreIcons.database);
        }
        //public override ConfigFolderNode LoadFolder(ITreeNode parent, IVirtualFolder folder)
        //{
        //    if (folder.PathEquals("data")) return new ConfigFolderNode(parent, folder)
        //    {
        //        _Title = "s_connection_tree",
        //        _Image = CoreIcons.connect,
        //    };
        //    if (folder.PathStarts("data/")) return new ConfigFolderNode(parent, folder)
        //    {
        //        _Title = folder.Name,
        //        _Image = CoreIcons.img_folder,
        //    };
        //    return base.LoadFolder(parent, folder);
        //}
        //public override ConfigFileNode LoadFile(ITreeNode parent, IVirtualFile file)
        //{
        //    if (file.PathStarts("data/") && file.GetExtension() == ".con")
        //    {
        //        return new ConfigFileNode(parent, file)
        //        {
        //            _Title = file.Name,
        //            _Image = CoreIcons.dbserver,
        //        };
        //    }
        //    return base.LoadFile(parent, file);
        //}
        //public override ConfigFolderNode LoadSubFolder(ITreeNode parent, ConfigFileNode master, IVirtualFolder folder)
        //{
        //    if (master.VirtualPath.PathStarts("data/") && master.VirtualPath.GetExtension() == ".con" && folder.NameEnds(".databases"))
        //    {
        //        return new ConfigFolderNode(parent, folder)
        //        {
        //            _Title = "s_databases",
        //            _Image = CoreIcons.database,
        //        };
        //    }
        //    return base.LoadSubFolder(parent, master, folder);
        //}
    }

    [ConfigNodeHandler(Name = "cfgtree")]
    public class ConfigTreeConfigLoader : ConfigNodeHandlerBase
    {
        public ConfigTreeConfigLoader()
        {
            DefineFolder("s_configuration", CoreIcons.settings, "cfg", false);
            DefineFolder("s_jobs", CoreIcons.job, "cfg/jobs", true);
            DefineFolder("s_favorites", CoreIcons.favorite, "cfg/favorites", true);
            DefineFolder("s_addons", CoreIcons.settings, "cfg/addons", true);
            DefineFolder("s_dashboards", CoreIcons.dashboard, "cfg/dashboards", true);

            DefineFile("cfg/jobs", ".job", CoreIcons.job, false);
            DefineFile("cfg/addons", ".adx", CoreIcons.settings, false);
            DefineFile("cfg/favorites", ".fav", CoreIcons.favorite, true);
            DefineFile("cfg/dashboards", ".das", CoreIcons.dashboard, true);

            if (AdvancedPerspectivesFeature.Allowed)
            {
                DefineFolder("s_perspectives", CoreIcons.perspective, "cfg/tblpers", true);
                DefineFile("cfg/tblpers", ".per", CoreIcons.perspective, true);
            }
        }
    }

    [ConfigNodeHandler(Name = "sqlscript")]
    public class SqlScriptTreeConfigLoader : ConfigNodeHandlerBase
    {
        public SqlScriptTreeConfigLoader()
        {
            DefineFolder("s_sql_scripts", CoreIcons.sql, "scripts", true);
            DefineFile("data", ".sql", CoreIcons.sql, false);
            DefineFile("scripts", ".sql", CoreIcons.sql, false);

            DefineSubFile("data", ".sql.design", "s_design_query", CoreIcons.querydesign);
            DefineSubFile("scripts", ".sql.design", "s_design_query", CoreIcons.querydesign);

            DefineSubFile("data", ".sql.ctx", "s_database_context_file_desc", CoreIcons.database);
            DefineSubFile("scripts", ".sql.ctx", "s_database_context_file_desc", CoreIcons.database);
        }
    }
}
