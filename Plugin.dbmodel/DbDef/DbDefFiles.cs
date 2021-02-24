using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;

namespace Plugin.dbmodel
{
    [FileHandler(Name = "dbdef", RequiredFeature = DatabaseModelsFeature.Test)]
    public class DbDefFileFormat : FileHandlerBase
    {
        public override string Extension
        {
            get { return "ddf"; }
        }

        public override string Description
        {
            get { return Texts.Get("s_database_structure"); }
        }

        public override void OpenAction()
        {
            OpenActionCreateLink();
        }

        public override IDatabaseSource OpenDatabase()
        {
            return new DbDefSource(m_file.DataDiskPath);
        }

        public override ITreeNode CreateNode(ITreeNode parent)
        {
            return new DbDefTreeNode(parent, this);
        }

        public override FileHandlerCaps Caps
        {
            get
            {
                return new FileHandlerCaps
                {
                    AllFlags = false,
                    OpenAction = true,
                    OpenDatabase = true,
                    CreateNode = true
                };
            }
        }
    }

    [CreateFactoryItem(Name = "dbdef", RequiredFeature = DatabaseModelsFeature.Test)]
    public class DbDefCreateWizard : CreateFactoryItemBase
    {
        public override string Title
        {
            get { return "s_database_definition"; }
        }

        public override string Name
        {
            get { return "dbdef"; }
        }

        public override string InfoText
        {
            get { return "s_file_desc_dbdef"; }
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
            get { return CoreIcons.img_database; }
        }

        public override bool AllowCreateFiles
        {
            get { return true; }
        }

        public override bool CreateFile(string fn)
        {
            DbDefProperties props = new DbDefProperties();
            props.Dialect = new GenericDialect();

            if (EditPropertiesForm.Run(props, true))
            {
                DatabaseStructure dbs = new DatabaseStructure();
                dbs.SetProps(props);
                dbs.Save(fn);
                return true;
            }
            return false;
        }

        public override string FileExtensions
        {
            get { return "ddf"; }
        }
    }

    //[DbFileHandler(Name = "ddf_handler", Title = "DDF file handler")]
    //public class DbDefDbFileHandler : DbFileHandlerBase
    //{
    //    public override IDatabaseSource OpenFile(string filename)
    //    {
    //        return new DbDefSource(filename);
    //    }

    //    public override bool CanOpenFile(string filename)
    //    {
    //        return filename.ToLower().EndsWith(".ddf");
    //    }
    //}
}
