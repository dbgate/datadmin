using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;

namespace Plugin.versiondb
{
    [FileHandler(Name = "versiondb")]
    public class VersionDbFileHandler : FileHandlerBase
    {
        public override string Extension
        {
            get { return "vdb"; }
        }

        public override string Description
        {
            get { return Texts.Get("s_version_db"); }
        }

        public override void OpenAction()
        {
            OpenActionCreateLink();
        }

        public override ITreeNode CreateNode(ITreeNode parent)
        {
            return new VdbTreeNode(parent, this);
        }

        public override FileHandlerCaps Caps
        {
            get
            {
                return new FileHandlerCaps
                {
                    AllFlags = false,
                    OpenAction = true,
                    CreateNode = true,
                };
            }
        }
    }

    [FileHandler(Name = "vdbvariant")]
    public class VdbVariantFileHandler : FileHandlerBase
    {
        public override string Extension
        {
            get { return "vdv"; }
        }

        public override string Description
        {
            get { return Texts.Get("s_variant"); }
        }

        public override void OpenAction()
        {
            OpenActionCreateLink();
        }

        public override ITreeNode CreateNode(ITreeNode parent)
        {
            return new VdbVariantNode(parent, this);
        }

        public override FileHandlerCaps Caps
        {
            get
            {
                return new FileHandlerCaps
                {
                    AllFlags = false,
                    OpenAction = true,
                    CreateNode = true,
                };
            }
        }
    }
}
