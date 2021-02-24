using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DatAdmin
{
    [FileHandler(Name = "sql")]
    public class SqlFileHandler : FileHandlerBase
    {
        public override string Extension
        {
            get { return "sql"; }
        }

        public override string Description
        {
            get { return Texts.Get("s_sql_script"); }
        }

        public override ITreeNode CreateNode(ITreeNode parent)
        {
            return new SqlScript_TreeNode(parent, this);
        }

        public override void OpenAction()
        {
            if (QueryFrame.FindOpenedFile(m_file.DataDiskPath)) return;
            OpenQueryParameters pars = new OpenQueryParameters();
            IPhysicalConnection conn = MainWindowExtension.SelectedConnection;
            if (conn != null) conn = conn.Clone();
            pars.File = m_file;
            MainWindow.Instance.OpenContent(new QueryFrame(conn, pars));
        }

        public override FileHandlerCaps Caps
        {
            get
            {
                return new FileHandlerCaps
                {
                    AllFlags = false,
                    CreateNode = true,
                    OpenAction = true
                };
            }
        }
    }
}
