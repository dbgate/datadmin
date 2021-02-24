using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;

namespace Plugin.apps
{
    public class ApplicationTreeNode : FileTreeNodeBase
    {
        internal IDatabaseSource m_conn = null;
        public ApplicationTreeNode(ITreeNode parent, string filepath)
            : base(parent, filepath)
        {
        }

        public override ITreeNode[] GetChildren()
        {
            return new ITreeNode[] { };
        }

        public override string TypeTitle
        {
            get { return "s_application"; }
        }

        [PopupMenu("s_edit", ImageName = CoreIcons.designName)]
        public void EditApplication()
        {
            AppDesignForm.Run(FileSystemPath);
        }

        public override bool DoubleClick()
        {
            EditApplication();
            return true;
        }

        public override System.Drawing.Bitmap Image { get { return CoreIcons.browse; } }
    }

    [NodeFactory(Name = "application")]
    public class ApplicationNodeFactory : NodeFactoryBase
    {
        public override ITreeNode FromFile(ITreeNode parent, string file)
        {
            if (!LicenseTool.FeatureAllowed(ApplicationBuyilderFeature.Test)) return null;
            string fn = file.ToLower();

            if (fn.EndsWith(".app"))
            {
                try
                {
                    return new ApplicationTreeNode(parent, file);
                }
                catch (Exception)
                {
                    return null;
                }
            }
            return null;
        }
    }
}
