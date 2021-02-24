using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.Windows.Forms;
using System.IO;
using System.Data.Common;
using System.Data.OleDb;

namespace Plugin.access
{
    public class AccessConnWizard : FileConnWizard
    {
        const string V2000 = "Access 2000 (MDB)";
        const string V2007 = "Access 2007 (ACCDB)";
        AccessConnAuthFrame m_authFrame;

        public AccessConnWizard(ITreeNode parent, string name)
            : base(parent, name)
        {
            Text = "MS Access Database";
            InitAuth();
        }

        public AccessConnWizard(string file)
            : base(file)
        {
            Text = "MS Access Database";
            InitAuth();
        }

        private void InitAuth()
        {
            m_createNewExFrame = m_openExistingExFrame = m_authFrame = new AccessConnAuthFrame();
            RefreshAuthMode();
        }

        public override IEnumerable<string> Versions
        {
            get
            {
                if (m_filename == null || IOTool.FileHasExtension(m_filename, "mdb")) yield return V2000;
                if (m_filename == null || IOTool.FileHasExtension(m_filename, "accdb")) yield return V2007;
            }
        }

        public override string DefaultVersion
        {
            get { return V2007; }
        }

        public override void DoCreateFile()
        {
            if (SelectedVersion == V2000) DoCreateFile(GetNewFilePath(".mdb"));
            if (SelectedVersion == V2007) DoCreateFile(GetNewFilePath(".accdb"));
        }

        public override void DoCreateFile(string filename)
        {
            byte[] data;
            if (SelectedVersion == V2000) data = NewFiles.emptydb_2000;
            else data = NewFiles.emptydb_2007;
            data = Compression.ZlibDecompress(data);
            if (SelectedVersion == V2000) filename = Path.ChangeExtension(filename, ".mdb");
            if (SelectedVersion == V2007) filename = Path.ChangeExtension(filename, ".accdb");
            using (FileStream fw = new FileStream(filename, FileMode.Create))
            {
                fw.Write(data, 0, data.Length);
            }
            if (m_authFrame.UseAutentization)
            {
                var con = new AccessStoredConnection();
                con.DbFilename = filename;
                con.FileName = filename + ".con";
                con.UseAutentization = true;
                con.Password = m_authFrame.Password;
                con.Provider = AccessStoredConnection.GetProviderForFile(con.DbFilename);
                con.Save();
                using (OleDbConnection conn = new OleDbConnection(con.GenerateNoPwdNoShareConnectionString()))
                {
                    conn.Open();
                    using (OleDbCommand cmd = conn.CreateCommand())
                    {
                        cmd.CommandText = String.Format("ALTER DATABASE PASSWORD `{0}` ``", m_authFrame.Password);
                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }

        public override string OpenFileDialogFilter
        {
            get { return "Access JET (*.mdb)|*.mdb|Access ACE (*.accdb)|*.accdb"; }
        }

        AccessStoredConnection GetStoredConn()
        {
            AccessStoredConnection con = new AccessStoredConnection();
            con.UseAutentization = m_authFrame.UseAutentization;
            con.Password = m_authFrame.Password;
            con.Login = m_authFrame.Login;
            con.SystemDatabase = m_authFrame.SystemDatabase;
            con.DbFilename = OpenFileName;
            con.Provider = AccessStoredConnection.GetProviderForFile(OpenFileName);
            return con;
        }


        public override void DoOpenFile()
        {
            var con = GetStoredConn();
            con.FileName = GetNewFilePath(".con");
            con.Save();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // AccessConnWizard
            // 
            this.Name = "AccessConnWizard";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }

    [CreateFactoryItem(Name = "access")]
    public class AccessCreateWizard : CreateFactoryItemBase
    {
        public AccessCreateWizard() { }

        public override string Title
        {
            get { return "MS Access"; }
        }

        public override string Name
        {
            get { return "access"; }
        }

        public override string Group
        {
            get { return "s_connections"; }
        }

        public override string InfoText
        {
            get { return "s_file_desc_msaccess"; }
        }

        public override string GroupName
        {
            get { return "connections"; }
        }

        public override string FileExtensions
        {
            get { return "mdb|accdb"; }
        }

        public override bool AllowCreateFiles
        {
            get { return true; }
        }

        public override System.Drawing.Bitmap Bitmap
        {
            get { return StdIcons.msaccess32; }
        }

        public override bool Create(ITreeNode parent, string name)
        {
            AccessConnWizard wiz = new AccessConnWizard(parent, name);
            return wiz.ShowDialogEx() == DialogResult.OK;
        }

        public override bool CreateFile(string file)
        {
            AccessConnWizard wiz = new AccessConnWizard(file);
            return wiz.ShowDialogEx() == DialogResult.OK;
        }

        public override int Weight
        {
            get { return 7; }
        }
    }
}
