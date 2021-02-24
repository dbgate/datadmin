using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DatAdmin
{
    public class SqlScripts_TreeNode : VirtualFolderTreeNode
    {
        internal IDatabaseSource m_conn;
        string m_titlePostfix;
        public SqlScripts_TreeNode(IDatabaseSource conn, IVirtualFolder folder, ITreeNode parent, string titlePostfix, string namePostfix)
            : base(parent, folder, "sqlscripts" + namePostfix)
        {
            m_conn = conn;
            m_titlePostfix = titlePostfix;
            //DXDriver driver = conn.Connection.GetDXDriver();
            //if (driver != null) m_virtualFolders.Add(driver.GetFolder("sqlscripts", conn.DatabaseName));
        }

        public override System.Drawing.Bitmap Image { get { return CoreIcons.sql; } }
        public override System.Drawing.Bitmap ExpandedImage { get { return CoreIcons.sql; } }

        public override bool AllowCreate(string group, string name)
        {
            return name == "sqlscript";
        }

        public override string Title
        {
            get { return Texts.Get("s_sql_scripts") + m_titlePostfix; }
        }
    }

    public class SqlScript_TreeNode : VirtualFileTreeNodeBase//, IFileSystemTreeNode
    {
        internal IDatabaseSource m_conn;
        public SqlScript_TreeNode(ITreeNode parent, IFileHandler han)
            : base(parent, han)
        {
            //SubFiles = new List<string>();
            //SubFolders = new List<string>();
            if (parent is SqlScripts_TreeNode)
            {
                m_conn = ((SqlScripts_TreeNode)parent).m_conn;
            }
        }

        public override ITreeNode[] GetChildren()
        {
            return new ITreeNode[] { };
        }

        public override string TypeTitle
        {
            get { return "s_sql_script"; }
        }

        [PopupMenu("s_edit", Weight = MenuWeights.EDIT)]
        public void EditScript()
        {
            if (QueryFrame.FindOpenedFile(m_file.DataDiskPath)) return;
            IPhysicalConnection conn = MainWindowExtension.SelectedConnection;
            if (conn != null) conn = conn.Clone();
            OpenQueryParameters pars = new OpenQueryParameters();
            string dbname = MainWindowExtension.SelectedDatabaseName;
            pars.File = m_file;
            pars.DisableContext = m_conn != null;
            if (dbname != null && conn != null) conn.AfterOpen += ConnTools.ChangeDatabaseCallback(dbname);
            MainWindow.Instance.OpenContent(new QueryFrame(conn, pars));
        }

        public override bool DoubleClick()
        {
            EditScript();
            return true;
        }

        public override System.Drawing.Bitmap Image
        {
            get
            {
                if (m_file.Parent.GetFile(m_file.Name + ".design").Exists()) return CoreIcons.querydesign;
                else return CoreIcons.sql;
            }
        }

        //#region IFileSystemTreeNode Members

        //public List<string> SubFiles { get; private set; }
        //public List<string> SubFolders { get; private set; }

        //#endregion
    }

    //[NodeFactory(Name="sqlscript")]
    //public class SqlScriptNodeFactory : NodeFactoryBase
    //{
    //    public override ITreeNode FromVirtualFile(ITreeNode parent, IVirtualFile file)
    //    {
    //        string fn = file.Name.ToLower();
    //        if (fn.EndsWith(".sql"))
    //        {
    //            try
    //            {
    //                return new SqlScript_TreeNode(parent, file);
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
