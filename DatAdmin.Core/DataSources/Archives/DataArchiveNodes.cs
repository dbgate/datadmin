using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    public class DataArchiveReaderTreeNode : Database_SourceConnectionTreeNode
    {
        public DataArchiveReaderTreeNode(ITreeNode parent, IFileHandler fhandler)
            : base(new DataArchiveReader(fhandler.FileObject.DataDiskPath), parent, fhandler, null, true)
        {
        }

        public override bool AllowExpand()
        {
            Async.SafeOpen(m_conn.Connection);
            RealNode.RefreshChilds(false);
            return true;
        }
        public override System.Drawing.Bitmap Image
        {
            get { return CoreIcons.dataarchive; }
        }

        //public override void DoRenameFile(string newfile)
        //{
        //    CloseConnection();
        //    base.DoRenameFile(newfile);
        //}
        //public override void DoDeleteFile()
        //{
        //    CloseConnection();
        //    base.DoDeleteFile();
        //}

        private void CloseConnection()
        {
            if (!HConnection.CallRemoveByKey(m_conn.Connection.GetConnKey()))
            {
                throw new InternalError("DAE-00192 " + Texts.Get("s_connection_cannot_be_closed"));
            }
            Async.SafeClose(m_conn.Connection);
            RealNode.CollapseNode();
            RealNode.ClearChilds();
        }
    }

    //[NodeFactory(Name="dataarchive")]
    //public class DataArchiveNodeFactory : NodeFactoryBase
    //{
    //    public override ITreeNode FromFile(ITreeNode parent, string file)
    //    {
    //        string dbfile = file;
    //        if (IOTool.FileIsLink(file)) dbfile = IOTool.GetLinkContent(file);
    //        if (dbfile.ToLower().EndsWith(".dbk"))
    //        {
    //            try
    //            {
    //                return new DataArchiveReaderTreeNode(parent, file, dbfile);
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
