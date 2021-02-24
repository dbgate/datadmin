using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;

namespace Plugin.versiondb
{
    public static class VdbExtension
    {
        public static DatabaseStructure LoadStructure(this VersionDef verdef, VariantDef vardef)
        {
            if (verdef == null) return null;
            var model = DatabaseStructure.Load(verdef.GetFile());
            vardef.RunTransform(model);
            return model;
        }
    }
}
