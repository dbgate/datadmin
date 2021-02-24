using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;

namespace Plugin.diagrams
{
    [CreateFactoryItem(Name = "diagram", RequiredFeature = DiagramsFeature.Test)]
    public class DiagramCreateWizard : CreateFactoryItemBase
    {
        public override string Title
        {
            get { return "s_diagram"; }
        }

        public override string Name
        {
            get { return "diagram"; }
        }

        public override string Group
        {
            get { return "s_misc"; }
        }

        public override string GroupName
        {
            get { return "misc"; }
        }

        public override System.Drawing.Bitmap Bitmap
        {
            get { return CoreIcons.diagram32; }
        }

        public override bool Create(ITreeNode parent, string name)
        {
            if (name != null)
            {
                try
                {
                    Diagram dia = Diagram.CreateNew();
                    var mfile = new InMemoryFile("tmp.dia");
                    dia.Save(mfile);
                    parent.CreateChildTextFile(name + ".dia", mfile.GetText());
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

    //[CreateFactoryItem(Name = "diagramstyle", RequiredFeature = DiagramsFeature.Test)]
    //public class DiagramStyleCreateWizard : CreateFactoryItemBase
    //{
    //    public override string Title
    //    {
    //        get { return "s_diagram_style"; }
    //    }

    //    public override string Name
    //    {
    //        get { return "diagramstyle"; }
    //    }

    //    public override string Group
    //    {
    //        get { return "s_diagram_styles"; }
    //    }

    //    public override string GroupName
    //    {
    //        get { return "diagramstyles"; }
    //    }

    //    public override System.Drawing.Bitmap Bitmap
    //    {
    //        get { return CoreIcons.diagram32; }
    //    }

    //    public override bool Create(ITreeNode parent, string name)
    //    {
    //        if (name != null)
    //        {
    //            try
    //            {
    //                DiagramStyle style = new DiagramStyle();
    //                XmlTool.SerializeObject(new DiskFile(System.IO.Path.Combine(parent.FileSystemPath, name + ".adx")), style);
    //                return true;
    //            }
    //            catch (Exception e)
    //            {
    //                Errors.Report(e);
    //            }
    //        }
    //        return false;
    //    }
    //}

    [FileHandler(Name = "diagram", RequiredFeature = DiagramsFeature.Test)]
    public class DiagramFileHandler : FileHandlerBase
    {
        public override string Extension
        {
            get { return "dia"; }
        }

        public override FileHandlerCaps Caps
        {
            get
            {
                return new FileHandlerCaps
                {
                    AllFlags = false,
                    CreateNode = true,
                };
            }
        }

        public override ITreeNode CreateNode(ITreeNode parent)
        {
            return new Diagram_TreeNode(parent, this);
        }
    }

    //[NodeFactory(Name = "diagram")]
    //public class DiagramNodeFactory : NodeFactoryBase
    //{
    //    public override ITreeNode FromVirtualFile(ITreeNode parent, IVirtualFile file)
    //    {
    //        if (!LicenseTool.FeatureAllowed(DiagramsFeature.Test)) return null;
    //        string fn = file.Name.ToLower();

    //        if (fn.EndsWith(".dia"))
    //        {
    //            try
    //            {
    //                return new Diagram_TreeNode(parent, file);
    //            }
    //            catch (Exception)
    //            {
    //                return null;
    //            }
    //        }
    //        return null;
    //    }
    //}
}
