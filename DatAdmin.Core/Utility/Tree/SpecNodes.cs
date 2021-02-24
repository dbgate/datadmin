using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;

namespace DatAdmin
{
    //[TreeRoot("data")]
    //[NodeFilter("s_root")]
    public class RootTreeNode : VirtualFolderTreeNode
    {
        public RootTreeNode()
            : base("data", new DiskFolder(Core.DataDirectory))
        {
            ConnPack.Cache = CachePack.TreeCache;
            //Properties["root"] = "data";
        }
        public static ITreeNode CreateRoot() { return new RootTreeNode(); }
        public override string Title
        {
            get { return "Data"; }
        }
        public override string TypeTitle
        {
            get { return "Data"; }
        }
        public override bool AllowCreate(string group, string name)
        {
            return group == "files" || group == "folder" || group == "connections" || group == "generic_connections";
        }
        public override List<IWidget> GetWidgets()
        {
            List<IWidget> res = base.GetWidgets();
            return res;
        }
        public override bool AllowRename() { return false; }
        public override bool AllowDelete() { return false; }
        protected override void PostprocessChildren(List<ITreeNode> children)
        {
            var node = children.Find(n => IOTool.NormalizePath(n.FileSystemPath) == IOTool.NormalizePath(Core.DataSamplesDirectory));
            if (node != null)
            {
                children.Remove(node);
                if (!GlobalSettings.Pages.Tree().HideDataSamplesFolder)
                {
                    children.Insert(0, new DataSamplesFolderTreeNode(this, Core.DataSamplesDirectory));
                }
            }
            if (VersionInfo.DatabaseSamplesFolder != null && Directory.Exists(VersionInfo.DatabaseSamplesFolder))
            {
                children.Insert(0, new DataSamplesFolderTreeNode(this, VersionInfo.DatabaseSamplesFolder));
            }
        }
    }

    public class DataSamplesFolderTreeNode : VirtualFolderTreeNode
    {
        public DataSamplesFolderTreeNode(ITreeNode parent, string directory)
            : base(parent, new DiskFolder(directory), "samples")
        {
        }

        public override string Title
        {
            get { return "s_samples"; }
        }

        public override Bitmap Image
        {
            get { return CoreIcons.example; }
        }

        public override Bitmap ExpandedImage
        {
            get { return CoreIcons.example; }
        }

        public override bool DoDelete()
        {
            if (StdDialog.YesNoDialog("s_really_hide_samples_directory"))
            {
                GlobalSettings.Pages.BeginEdit();
                GlobalSettings.Pages.Tree().HideDataSamplesFolder = true;
                GlobalSettings.Pages.EndEdit();
                return true;
            }
            return false;
        }

        public override bool AllowRename() { return false; }
    }

    public class ScriptsRootTreeNode : VirtualFolderTreeNode
    {
        public ScriptsRootTreeNode()
            : base("scripts", new DiskFolder(Core.ScriptsDirectory))
        {
            //Properties["root"] = "scripts";
        }
        public override string Title
        {
            get { return Texts.Get("s_scripts"); }
        }
        public override string TypeTitle
        {
            get { return Texts.Get("s_scripts"); }
        }
        public override bool AllowCreate(string group, string name)
        {
            return group == "folder" || name == "sqlscript";
        }
        public override List<IWidget> GetWidgets()
        {
            List<IWidget> res = base.GetWidgets();
            return res;
        }
        public override bool AllowRename() { return false; }
        public override bool AllowDelete() { return false; }
    }

    public class ChartsRootTreeNode : VirtualFolderTreeNode
    {
        public ChartsRootTreeNode()
            : base("charts", new DiskFolder(Core.ChartsDirectory))
        {
        }
        public override string Title
        {
            get { return Texts.Get("s_charts"); }
        }
        public override string TypeTitle
        {
            get { return Texts.Get("s_charts"); }
        }
        public override bool AllowCreate(string group, string name)
        {
            return group == "folder" || name == "chart";
        }
        public override List<IWidget> GetWidgets()
        {
            List<IWidget> res = base.GetWidgets();
            return res;
        }
        public override bool AllowRename() { return false; }
        public override bool AllowDelete() { return false; }
    }

    public class FavoritesRootTreeNode : TreeNodeBase
    {
        public FavoritesRootTreeNode()
            : base("favorites")
        {
            //Properties["root"] = "favorites";
        }
        public override string Title
        {
            get { return Texts.Get("s_favorites"); }
        }
        public override string TypeTitle
        {
            get { return Texts.Get("s_favorites"); }
        }
        public override bool AllowCreate(string group, string name)
        {
            return false;
        }
        public override bool AllowRename() { return false; }
        public override bool AllowDelete() { return false; }
        public override ITreeNode[] GetChildren()
        {
            var res = new List<ITreeNode>();
            foreach (var grp in Favorites.Groups)
            {
                res.Add(new FavoriteGroupTreeNode(this, grp));
            }
            return res.ToArray();
        }
        public override Bitmap Image
        {
            get { return CoreIcons.favorite; }
        }
    }

    //[TreeRoot("addons")]
    public class AddonsRootTreeNode : TreeNodeBase
    {
        public AddonsRootTreeNode()
            : base("addons")
        {
            //Properties["root"] = "addons";
        }
        public override string Title
        {
            get { return Texts.Get("s_configuration"); }
        }
        public override bool AllowCreate(string group, string name)
        {
            return false;
        }
        public override bool AllowRename() { return false; }
        public override bool AllowDelete() { return false; }
        public override ITreeNode[] GetChildren()
        {
            return new ITreeNode[] { 
                new AddonsSubRootTreeNode(this, Core.ConfigDirectory, "jobs", "jobs", "s_jobs", CoreIcons.job), 
                new AddonsSubRootTreeNode(this, null, "datastore", "datastores", "s_datastores", CoreIcons.command), 
                new AddonsSubRootTreeNode(this, null, "dbwriter", "dbwriters", "s_dbwriters", CoreIcons.command), 
                new AddonsSubRootTreeNode(this, Core.ConfigDirectory, "apps", "apps", "s_applications", CoreIcons.browse), 
                new AddonsSubRootTreeNode(this, Core.ConfigDirectory, "tblpers", "tblpers", "s_table_perspectives", CoreIcons.perspective), 
                //new AddonsSubRootTreeNode(this, null, "commands", "commands", "s_commands", CoreIcons.command), 
                //new AddonsSubRootTreeNode(this, null, "commands", "commands"), 
                //new AddonsSubRootTreeNode(this, null, "nodegens", "nodegenerators"), 
                //new AddonsSubRootTreeNode(this, null, "objectviews", "objectviews"),
                //new AddonsSubRootTreeNode(this, null, "datastores", "datastores"), 
                //new AddonsSubRootTreeNode(this, null, "diagramstyles", "diagramstyles"), 
                //new AddonsSubRootTreeNode(this, null, "macros", "macros"), 
            };
        }

        public override string TypeTitle
        {
            get { return "s_addons"; }
        }

        public override Bitmap Image
        {
            get { return CoreIcons.img_folder; }
        }

        public override Bitmap ExpandedImage
        {
            get { return CoreIcons.img_folder_expanded; }
        }
    }

    public class AddonsSubRootTreeNode : VirtualFolderTreeNode
    {
        string m_group;
        string m_title;
        Bitmap m_image;

        public AddonsSubRootTreeNode(AddonsRootTreeNode parent, string basedir, string subdir, string group, string title, Bitmap image)
            : base(parent, new DiskFolder(System.IO.Path.Combine(basedir ?? Core.AddonsDirectory, subdir)), subdir)
        {
            m_group = group;
            m_title = title;
            m_image = image;
        }

        public override bool AllowCreate(string group, string name)
        {
            if (group == "folder") return true;
            if (m_group.StartsWith("@")) return name == m_group.Substring(1);
            else return group == m_group;
        }

        public override bool AllowRename()
        {
            return false;
        }

        public override bool AllowDelete()
        {
            return false;
        }

        public override string Title
        {
            get { return m_title; }
        }

        public override Bitmap Image
        {
            get { return m_image; }
        }

        public override Bitmap ExpandedImage
        {
            get { return m_image; }
        }
    }

    [CreateFactoryItem(Name="folder")]
    public class FolderCreateWizard : CreateFactoryItemBase
    {
        public override string Title
        {
            get { return "s_folder"; }
        }

        public override string Name
        {
            get { return "folder"; }
        }

        public override string InfoText
        {
            get { return "s_file_desc_folder"; }
        }

        public override string Group
        {
            get { return "s_folder"; }
        }

        public override string GroupName
        {
            get { return "folder"; }
        }

        public override System.Drawing.Bitmap Bitmap
        {
            get { return CoreIcons.bigfolder; }
        }

        public override bool Create(ITreeNode parent, string name)
        {
            if (name != null)
            {
                try
                {
                    Directory.CreateDirectory(System.IO.Path.Combine(parent.FileSystemPath, name));
                    return true;
                }
                catch (Exception e)
                {
                    Errors.Report(e);
                }
            }
            return false;
        }
    }
}
