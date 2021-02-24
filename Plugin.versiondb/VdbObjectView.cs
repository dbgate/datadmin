using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.IO;

namespace Plugin.versiondb
{
    public abstract class DiffWidgetBase : SyntaxTextWidgetBase
    {
        public override string DefaultPageTitle
        {
            get { return "SQL"; }
        }

        public override System.Drawing.Bitmap DefaultImage
        {
            get { return CoreIcons.sql; }
        }
    }

    public class VersionDiffWidget : DiffWidgetBase
    {
        public override string CreateText(AppObject appobj, ConnectionPack connpack)
        {
            var va = appobj as VersionAppObject;
            if (va == null) return "";
            return va.GetVersion().GetAlterSql();
        }
    }

    public class VersionVariantDiffWidget : DiffWidgetBase
    {
        public override string CreateText(AppObject appobj, ConnectionPack connpack)
        {
            var va = appobj as VariantVersionAppObject;
            if (va == null) return "";
            return va.GetVersion().GetAlterSql();
        }
    }
}
