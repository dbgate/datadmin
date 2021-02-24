using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;

namespace Plugin.apps
{
    [CreateFactoryItem(Name = "application")]
    public class AppCreateWizard : CreateFactoryItemBase
    {
        public override string Title
        {
            get { return "s_application"; }
        }

        public override string InfoText
        {
            get { return "s_file_desc_application"; }
        }

        public override string Name
        {
            get { return "application"; }
        }

        public override string Group
        {
            get { return "s_applications"; }
        }

        public override string GroupName
        {
            get { return "apps"; }
        }

        public override System.Drawing.Bitmap Bitmap
        {
            get { return CoreIcons.browse; }
        }

        public override bool Create(ITreeNode parent, string name)
        {
            if (name != null)
            {
                try
                {
                    var app = new Application();
                    app.SaveToFile(System.IO.Path.Combine(parent.FileSystemPath, name + ".app"));
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
