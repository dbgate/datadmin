using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;

namespace Plugin.versiondb
{
    [CreateFactoryItem(Name = "vdb", RequiredFeature = VersionedDbFeature.Test)]
    public class VersionDbCreateWizard : CreateFactoryItemBase
    {
        public VersionDbCreateWizard()
        {
        }
        public override System.Drawing.Bitmap Bitmap
        {
            get { return CoreIcons.versiondb32; }
        }
        public override string Name
        {
            get { return "versiondb"; }
        }
        public override string Title
        {
            get { return "s_version_db"; }
        }
        public override string InfoText
        {
            get { return "s_version_db_desc"; }
        }
        public override string Group
        {
            get { return "s_files"; }
        }
        public override string GroupName
        {
            get { return "files"; }
        }
        public override bool AllowCreateFiles
        {
            get { return true; }
        }

        public override bool CreateFile(string fn)
        {
            VersionDbProperties props = new VersionDbProperties();
            props.Dialect = GenericDialect.Instance;
            VersionDb vdb = new VersionDb(props);
            vdb.m_file = fn;

            if (VersionDbPropsForm.Run(vdb))
            {
                vdb.Save();
                return true;
            }
            return false;
        }

        public override string FileExtensions
        {
            get { return "vdb"; }
        }
    }

    //[NodeFactory(Name = "vdb", RequiredFeature = VersionedDbFeature.Test)]
    //public class VdbNodeFactory : NodeFactoryBase
    //{
    //    public override ITreeNode FromFile(ITreeNode parent, string file)
    //    {
    //        string dbfile = file;
    //        if (IOTool.FileIsLink(file)) dbfile = IOTool.GetLinkContent(file);
    //        if (dbfile.ToLower().EndsWith(".vdb"))
    //        {
    //            try
    //            {
    //                return new VdbTreeNode(parent, file, dbfile);
    //            }
    //            catch (Exception)
    //            {
    //                return null;
    //            }
    //        }
    //        return null;
    //    }

    //    public override ITreeNode FromVirtualFile(ITreeNode parent, IVirtualFile file)
    //    {
    //        if (!LicenseTool.FeatureAllowed(VersionedDbFeature.Test)) return null;
    //        string fn = file.Name.ToLower();

    //        if (fn.EndsWith(".vdv"))
    //        {
    //            try
    //            {
    //                return new VdbVariantNode(parent, file);
    //            }
    //            catch (Exception)
    //            {
    //                return null;
    //            }
    //        }
    //        return null;
    //    }
    //}

    [CreateFactoryItem(Name = "vdbvariant")]
    public class VariantCreateWizard : CreateFactoryItemBase
    {
        public override string Title
        {
            get { return "s_variant"; }
        }

        public override string Name
        {
            get { return "vdbvariant"; }
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
            get { return CoreIcons.variant; }
        }

        public override bool Create(ITreeNode parent, string name)
        {
            if (name != null)
            {
                try
                {
                    var vdv = new VariantDef();
                    var mfile = new InMemoryFile("tmp.vdv");
                    vdv.Save(mfile);
                    parent.CreateChildTextFile(name + ".vdv", mfile.GetText());
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
