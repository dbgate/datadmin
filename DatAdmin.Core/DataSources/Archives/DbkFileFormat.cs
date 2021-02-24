using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    [FileHandler(Name = "dbk")]
    public class DbkFileHandler : FileHandlerBase
    {
        //public override bool CanCreate
        //{
        //    get { return false; }
        //}

        //public override bool CanLoad
        //{
        //    get { return true; }
        //}

        public override FileHandlerCaps Caps
        {
            get
            {
                return new FileHandlerCaps
                {
                    AllFlags = false,
                    CreateNode = true,
                    OpenDatabase = true,
                    OpenAction = true,
                };
            }
        }

        public override string Extension
        {
            get { return "dbk"; }
        }

        public override string Description
        {
            get { return Texts.Get("s_data_archive"); }
        }

        public override void OpenAction()
        {
            OpenActionCreateLink();
        }

        public override IDatabaseSource OpenDatabase()
        {
            return new DataArchiveReader(m_file.DataDiskPath);
        }

        public override ITreeNode CreateNode(ITreeNode parent)
        {
            return new DataArchiveReaderTreeNode(parent, this);
        }
    }

    //[DbFileHandler(Name = "dbk_handler", Title = "DBK file handler")]
    //public class DbkDbFileHandler : DbFileHandlerBase
    //{
    //    public override IDatabaseSource OpenFile(string filename)
    //    {
    //        return new DataArchiveReader(filename);
    //    }

    //    public override bool CanOpenFile(string filename)
    //    {
    //        return filename.ToLower().EndsWith(".dbk");
    //    }
    //}
}
