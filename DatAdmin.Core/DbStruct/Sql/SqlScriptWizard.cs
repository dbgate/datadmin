using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DatAdmin
{
    [CreateFactoryItem(Name="sqlscript")]
    public class SqlCreateWizard : CreateFactoryItemBase
    {
        public override string Title
        {
            get { return "s_sql_script"; }
        }

        public override string InfoText
        {
            get { return "s_file_desc_sql_script"; }
        }

        public override string Name
        {
            get { return "sqlscript"; }
        }

        public override string Group
        {
            get { return "s_files"; }
        }

        public override string GroupName
        {
            get { return "files"; }
        }

        public override System.Drawing.Bitmap Bitmap
        {
            get { return CoreIcons.sql; }
        }

        public override bool Create(ITreeNode parent, string name)
        {
            if (name != null)
            {
                try
                {
                    parent.CreateChildTextFile(name + ".sql", "");
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
