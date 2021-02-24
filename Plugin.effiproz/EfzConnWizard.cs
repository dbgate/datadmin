using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DatAdmin;
using System.Data.Common;
using System.Data.EffiProz;

namespace Plugin.effiproz
{
    public partial class EfzConnWizard : FileConnWizard
    {
        FileConnAuthFrame m_authFrame;

        public EfzConnWizard()
        {
            InitializeComponent();
        }

        public EfzConnWizard(ITreeNode parent, string name)
            : base(parent, name)
        {
            Text = "EffiProz Database";
            InitAuth();
        }

        public EfzConnWizard(string filename)
            : base(filename)
        {
            Text = "EffiProz Database";
            InitAuth();
        }

        private void InitAuth()
        {
            m_openExistingExFrame = m_authFrame = new FileConnAuthFrame();
            RefreshAuthMode();
        }

        public override IEnumerable<string> Versions
        {
            get
            {
                yield return "EffiProz 1.5";
            }
        }

        public override void DoCreateFile(string filename)
        {
            string conns = EfzTool.GetConnectionString(filename, EfzLogin, EfzPassword);
            using (DbConnection conn = new EfzConnection(conns))
            {
                conn.Open();
                conn.Close();
            }
        }

        public override void DoCreateFile()
        {
            DoCreateFile(GetNewFilePath(".properties"));
        }

        public override string OpenFileDialogFilter
        {
            get { return "EffiProz (*.properties)|*.properties"; }
        }

        private string EfzLogin
        {
            get { return m_authFrame.UseAutentization ? m_authFrame.Login : "sa"; }
        }
        private string EfzPassword
        {
            get { return m_authFrame.UseAutentization ? m_authFrame.Password : ""; }
        }

        public override void DoOpenFile()
        {
            using (DbConnection conn = new EfzConnection(EfzTool.GetConnectionString(OpenFileName, EfzLogin, EfzPassword)))
            {
                Async.InvokeFromGui(conn.Open);
                Async.InvokeFromGui(conn.Close);
            }
            EfzStoredConnection stored = new EfzStoredConnection();
            stored.DbFilename = OpenFileName;
            stored.FileName = GetNewFilePath(".con");
            stored.Login = EfzLogin;
            stored.Password = EfzPassword;
            stored.Save();
        }
    }

    [CreateFactoryItem(Name = "effiproz")]
    public class EfzCreateWizard : CreateFactoryItemBase
    {
        public override string Title
        {
            get { return "EffiProz"; }
        }

        public override string Name
        {
            get { return "effiproz"; }
        }

        public override string Group
        {
            get { return "s_connections"; }
        }

        public override string GroupName
        {
            get { return "connections"; }
        }

        public override string InfoText
        {
            get { return "s_file_desc_effiproz"; }
        }

        public override System.Drawing.Bitmap Bitmap
        {
            get { return StdIcons.effiproz32; }
        }

        public override bool AllowCreateFiles
        {
            get { return true; }
        }

        public override string FileExtensions
        {
            get { return "properties"; }
        }

        public override bool Create(ITreeNode parent, string name)
        {
            EfzConnWizard wiz = new EfzConnWizard(parent, name);
            return wiz.ShowDialogEx() == DialogResult.OK;
        }

        public override bool CreateFile(string file)
        {
            EfzConnWizard wiz = new EfzConnWizard(file);
            return wiz.ShowDialogEx() == DialogResult.OK;
        }

        public override int Weight
        {
            get { return 5; }
        }
    }
}
