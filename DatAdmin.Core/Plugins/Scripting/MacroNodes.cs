using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    //[NodeFactory(Name="macro")]
    //public class MacroNodeFactory : NodeFactoryBase
    //{
    //    public override ITreeNode FromFile(ITreeNode parent, string file)
    //    {
    //        if (file.ToLower().EndsWith(".mdx"))
    //        {
    //            try
    //            {
    //                return new MacroTreeNode(parent, file);
    //            }
    //            catch (Exception)
    //            {
    //                return null;
    //            }
    //        }
    //        return null;
    //    }
    //}
    [FileHandler(Name = "macro")]
    public class MacroFileHandler : FileHandlerBase
    {
        public override string Extension
        {
            get { return "mdx"; }
        }

        public override ITreeNode CreateNode(ITreeNode parent)
        {
            return new MacroTreeNode(parent, this);
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


    public class MacroTreeNode : VirtualFileTreeNodeBase
    {
        public MacroTreeNode(ITreeNode parent, IFileHandler fhandler)
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
        [PopupMenu("s_run")]
        public void Run()
        {
            Macro macro = Macro.LoadFromFile(m_file.DataDiskPath);
            var inst = macro.CreateParamsInstance();
            if (EditPropertiesForm.Run(inst, true))
            {
                macro.Run(MainWindow.Instance.Window, inst.Data);
            }
        }

        [PopupMenu("s_properties")]
        public void EditProperties()
        {
            object macro = XmlTool.DeserializeObject(m_file.DataDiskPath);
            if (EditPropertiesForm.Run(macro, true))
            {
                XmlTool.SerializeObject(m_file.DataDiskPath, macro);
            }
        }
        public override string TypeTitle
        {
            get { return "s_macro"; }
        }
    }
}
