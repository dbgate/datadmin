using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    //[NodeFactory(Name="addon")]
    //public class AddonNodeFactory : NodeFactoryBase
    //{
    //    public override ITreeNode FromFile(ITreeNode parent, string file)
    //    {
    //        if (file.ToLower().EndsWith(".adx"))
    //        {
    //            try
    //            {
    //                return new AddonTreeNode(parent, file);
    //            }
    //            catch (Exception)
    //            {
    //                return null;
    //            }
    //        }
    //        return null;
    //    }
    //}

    [FileHandler(Name = "addon")]
    public class AddonFileHandler : FileHandlerBase
    {
        public override string Extension
        {
            get { return "adx"; }
        }

        public override ITreeNode CreateNode(ITreeNode parent)
        {
            return new AddonTreeNode(parent, this);
        }

        public override FileHandlerCaps Caps
        {
            get
            {
                return new FileHandlerCaps
                {
                    AllFlags = false,
                    CreateNode = true
                };
            }
        }
    }

    public class AddonTreeNode : VirtualFileTreeNodeBase
    {
        public AddonTreeNode(ITreeNode parent, IFileHandler fhandler)
            : base(parent, fhandler)
        {
        }
        public override ITreeNode[] GetChildren()
        {
            return new ITreeNode[] { };
        }
        public override bool PreparedChildren
        {
            get { return true; }
        }
        [PopupMenu("s_properties", ImageName = CoreIcons.propertiesName)]
        public void EditProperties()
        {
            IAddonInstance addon = AddonBase.LoadFromFile(m_file.DataDiskPath);
            if (EditPropertiesForm.Run(addon, true))
            {
                //addon.Original = false;
                addon.SaveToFile(m_file.DataDiskPath);
            }
        }
        [PopupMenu("s_edit")]
        public void AddonEditor()
        {
            IAddonInstance addon = AddonBase.LoadFromFile(m_file.DataDiskPath);
            MainWindow.Instance.OpenContent(new AddonEditorFrame(addon, m_file.DataDiskPath));
        }
        public override string TypeTitle
        {
            get { return "s_addon"; }
        }
        public override bool DoubleClick()
        {
            AddonEditor();
            return true;
        }
        public override System.Drawing.Bitmap Image { get { return CoreIcons.command; } }
    }
}
