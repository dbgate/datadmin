using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.Windows.Forms;
using Plugin.dbmodel;

namespace Plugin.versiondb
{
    public class VdbTreeNode : VirtualFileTreeNodeBase
    {
        VersionDb m_db;

        public VdbTreeNode(ITreeNode parent, IFileHandler fhandler)
            : base(parent, fhandler)
        {
            m_db = new VersionDb(fhandler.FileObject.DataDiskPath);
            SetAppObject(new VersionDbAppObject { VdbFile = fhandler.FileObject.DataDiskPath });
        }

        public override ITreeNode[] GetChildren()
        {
            return new ITreeNode[] 
            { 
                new VdbVersionsNode(this, m_db),
                new Plugin.diagrams.Diagrams_TreeNode(
                    new Plugin.dbmodel.DbDefSource(new VdbLastVersionConnection(m_db)),
                    new DiskFolder(m_db.DiagramsDirectory),
                    this, null, null),
                new VdbVariantsNode(this, m_db),
            };
        }

        public override System.Drawing.Bitmap Image
        {
            get { return CoreIcons.versiondb; }
        }
        public override string TypeTitle
        {
            get { return "s_version_db"; }
        }

        public override bool ContainsDatabaseNode()
        {
            return true;
        }
    }

    public class VdbVersionsNode : TreeNodeBase
    {
        VersionDb m_db;
        public VdbVersionsNode(ITreeNode parent, VersionDb db)
            : base(parent, "versions")
        {
            m_db = db;
        }
        public override System.Drawing.Bitmap Image
        {
            get { return CoreIcons.versiondb; }
        }
        public override string Title
        {
            get { return "s_versions"; }
        }
        public override string TypeTitle
        {
            get { return "s_versions"; }
        }
        [PopupMenu("s_add_version", ImageName = CoreIcons.addName)]
        public void AddVersion()
        {
            string nv = InputBox.Run("s_type_name_of_new_version", "new_version");
            if (nv != null)
            {
                m_db.AddVersion(nv);
                this.CompleteRefresh();
            }
        }
        public override bool PreparedChildren
        {
            get { return true; }
        }
        public override ITreeNode[] GetChildren()
        {
            List<ITreeNode> res = new List<ITreeNode>();
            foreach (var ver in m_db.Versions)
            {
                res.Add(new VdbVersionNode(this, ver));
            }
            return res.ToArray();
        }

        public override bool ContainsDatabaseNode()
        {
            return true;
        }
    }

    public class VdbVersionNode : TreeNodeBase, IDatabaseTreeNode
    {
        internal VersionDef m_version;
        public VdbVersionNode(ITreeNode parent, VersionDef version)
            : base(parent, version.Name)
        {
            m_version = version;
            SetAppObject(new VersionAppObject(m_version));
        }
        public override System.Drawing.Bitmap Image
        {
            get { return CoreIcons.versiondb; }
        }
        public override string Title
        {
            get { return m_version.Name; }
        }
        public override bool PreparedChildren
        {
            get { return true; }
        }
        public override ITreeNode[] GetChildren()
        {
            return new ITreeNode[] { 
                new VdbDbDefTreeNode(this, new VersionDbFileHandler { FileObject = new DiskFile( m_version.GetFile()) } ),
                new VdbScriptsTreeNode(this, m_version, "before", "s_scripts_before_change"),
                new VdbScriptsTreeNode(this, m_version, "before-nofk", "s_scripts_before_change_no_fk"),
                new VdbScriptsTreeNode(this, m_version, "after-nofk", "s_scripts_after_change_no_fk"),
                new VdbScriptsTreeNode(this, m_version, "after", "s_scripts_after_change"),
            };
        }
        public override bool AllowDelete()
        {
            return true;
        }
        public override bool DoDelete()
        {
            if (MessageBox.Show(Texts.Get("s_really_delete$version", "version", m_version.Name), VersionInfo.ProgramTitle, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                m_version.DeleteVersion();
                Parent.CompleteRefresh();
                return true;
            }
            return false;
        }
        public override string TypeTitle
        {
            get { return "s_version"; }
        }
        public override bool AllowRename()
        {
            return true;
        }
        public override void RenameNode(string newname)
        {
            m_version.Rename(newname);
        }

        #region IDatabaseTreeNode Members

        public IDatabaseSource DatabaseConnection
        {
            get { return new DbDefSource(m_version.GetFile()); }
        }

        public void BeforeDataRefreshChilds()
        {
        }

        #endregion
    }

    public class VdbScriptsTreeNode : VirtualFolderTreeNode
    {
        string m_title;
        VersionDef m_version;
        public VdbScriptsTreeNode(VdbVersionNode parent, VersionDef version, string scriptsSubDir, string title)
            : base(parent, new DiskFolder(version.GetPrivateSubFolder(scriptsSubDir)), "scripts")
        {
            m_title = title;
            m_version = version;
        }

        public override bool AllowCreate(string group, string name)
        {
            return name == "sqlscript";
        }

        public override System.Drawing.Bitmap Image
        {
            get { return CoreIcons.sql; }
        }

        public override System.Drawing.Bitmap ExpandedImage
        {
            get { return CoreIcons.sql; }
        }

        public override string Title
        {
            get { return m_title; }
        }
    }

    public class VdbDbDefTreeNode : Plugin.dbmodel.DbDefTreeNode
    {
        public VdbDbDefTreeNode(ITreeNode parent, IFileHandler fhandler)
            : base(parent, fhandler)
        {
        }

        public override bool AllowDelete()
        {
            return false;
        }

        public override bool AllowRename()
        {
            return false;
        }

        public override string Title
        {
            get { return "s_structure"; }
        }

        public override List<IWidget> GetWidgets()
        {
            return base.GetWidgets();
        }
    }

    public class VdbVariantsNode : VirtualFolderTreeNode
    {
        internal VersionDb m_db;
        public VdbVariantsNode(ITreeNode parent, VersionDb db)
            : base(parent, new DiskFolder(db.VariantsDirectory), "variants")
        {
            m_db = db;
        }
        public override System.Drawing.Bitmap Image
        {
            get { return CoreIcons.variant; }
        }
        public override string Title
        {
            get { return "s_variants"; }
        }
        public override string TypeTitle
        {
            get { return "s_variants"; }
        }
        public override bool AllowCreate(string group, string name)
        {
            return name == "vdbvariant";
        }
    }

    public class VdbVariantNode : VirtualFileTreeNodeBase
    {
        internal VersionDb m_db;
        public VdbVariantNode(ITreeNode parent, IFileHandler fhandler)
            : base(parent, fhandler)
        {
            if (parent is VdbVariantsNode)
            {
                m_db = ((VdbVariantsNode)parent).m_db;
            }
        }

        public override ITreeNode[] GetChildren()
        {
            return new ITreeNode[] { new VdbVariantVersionsNode(this, m_db, FileObject) };
        }

        public override string TypeTitle
        {
            get { return "s_variant"; }
        }

        [PopupMenu("s_edit", ImageName = CoreIcons.designName, Weight = MenuWeights.PROPERTIES)]
        public void Edit()
        {
            var vdv = new VariantDef(m_file);
            var ed = new VariantDefTransformsEditor (m_file);
            ed.ShowContent();
        }

        public override bool DoubleClick()
        {
            Edit();
            return true;
        }

        public override System.Drawing.Bitmap Image { get { return CoreIcons.variant; } }
    }

    public class VdbVariantVersionsNode : TreeNodeBase
    {
        VersionDb m_db;
        IVirtualFile m_varfile;

        public VdbVariantVersionsNode(ITreeNode parent, VersionDb db, IVirtualFile varfile)
            : base(parent, "versions")
        {
            m_db = db;
            m_varfile = varfile;
        }
        public override System.Drawing.Bitmap Image
        {
            get { return CoreIcons.versiondb; }
        }
        public override string Title
        {
            get { return "s_versions"; }
        }
        public override string TypeTitle
        {
            get { return "s_versions"; }
        }
        public override bool PreparedChildren
        {
            get { return true; }
        }
        public override ITreeNode[] GetChildren()
        {
            List<ITreeNode> res = new List<ITreeNode>();
            foreach (var ver in m_db.Versions)
            {
                res.Add(new VdbVariantVersionNode(this, new VdbVariantVersionDef(ver, m_varfile)));
            }
            return res.ToArray();
        }
    }

    public class VdbVariantVersionNode : TreeNodeBase
    {
        internal VdbVariantVersionDef m_version;

        public VdbVariantVersionNode(ITreeNode parent, VdbVariantVersionDef version)
            : base(parent, version.Version.Name)
        {
            m_version = version;
            SetAppObject(new VariantVersionAppObject(m_version));
        }
        public override System.Drawing.Bitmap Image
        {
            get { return CoreIcons.versiondb; }
        }
        public override string Title
        {
            get { return m_version.Version.Name; }
        }
        public override bool PreparedChildren
        {
            get { return true; }
        }
        public override ITreeNode[] GetChildren()
        {
            var fact = new VdbVariantVersionConnectionFactory
            {
                VdbFile = m_version.Version.Db.FileName,
                Version = m_version.Version.Name,
                Variant = m_version.VariantFile.Name,
            };
            return new ITreeNode[] 
            { 
                new DbDefReadonlyTreeNode(this, fact, "s_structure"),
            };
        }
        public override string TypeTitle
        {
            get { return "s_version"; }
        }
    }

    public class VariantDefTransformsEditor : CollectionNamedAddonListEditor
    {
        IVirtualFile m_file;
        VariantDef m_vdef;

        public VariantDefTransformsEditor(IVirtualFile file)
            : base(DbModelTransformAddonType.Instance)
        {
            m_file = file;
            m_vdef = new VariantDef(m_file);
        }

        public void ShowContent()
        {
            ShowContent("s_variant", m_vdef.ModelTransforms, CoreIcons.variant);
        }

        public override void OnSaveContent()
        {
            m_vdef.Save(m_file);
        }
    }
}
