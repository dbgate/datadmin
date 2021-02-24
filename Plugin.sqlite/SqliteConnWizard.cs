using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.Windows.Forms;
using System.Data.Common;

namespace Plugin.sqlite
{
    public class SQLiteConnWizard : FileConnWizard
    {
        public SQLiteConnWizard(ITreeNode parent, string name)
            : base(parent, name)
        {
            Text = "SQLite Database";
            //AuthModeOpen = AuthModeCreate = FileAuthMode.None;
        }

        public SQLiteConnWizard(string filename)
            : base(filename)
        {
            Text = "SQLite Database";
            //AuthModeOpen = AuthModeCreate = FileAuthMode.None;
        }

        public override IEnumerable<string> Versions
        {
            get
            {
                //yield return "SQLite 2";
                yield return "SQLite 3";
            }
        }

        public override void DoCreateFile(string filename)
        {
            string conns = String.Format("Data Source={0};New=True;Version={1}", filename, 3);
            using (DbConnection conn = SqliteDriver.GetConnection(conns))
            {
                conn.Open();
                conn.Close();
            }
        }

        public override void DoCreateFile()
        {
            DoCreateFile(GetNewFilePath(".db3"));
        }

        public override string OpenFileDialogFilter
        {
            get { return "SQLite 3 database (*.db3)|*.db3|SQLite  database (*.db)|*.db|All files (*.*)|*.*"; }
        }

        public override void DoOpenFile()
        {
            using (DbConnection conn = SqliteDriver.GetConnection(String.Format("Data Source={0}", OpenFileName)))
            {
                Async.InvokeFromGui(conn.Open);
                Async.InvokeFromGui(conn.Close);
            }
            SQLiteStoredConnection stored = new SQLiteStoredConnection();
            stored.DbFilename = OpenFileName;
            stored.FileName = GetNewFilePath(".con");
            stored.Save();
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // SQLiteConnWizard
            // 
            this.Name = "SQLiteConnWizard";
            this.ResumeLayout(false);
            this.PerformLayout();

        }
    }

    [CreateFactoryItem(Name = "sqlite")]
    public class SQLiteCreateWizard : CreateFactoryItemBase
    {
        public override string Title
        {
            get { return "SQLite"; }
        }

        public override string Name
        {
            get { return "sqlite"; }
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
            get { return "s_file_desc_sqlite"; }
        }

        public override System.Drawing.Bitmap Bitmap
        {
            get { return StdIcons.sqlite32; }
        }

        public override bool AllowCreateFiles
        {
            get { return true; }
        }

        public override string FileExtensions
        {
            get { return "db3"; }
        }

        public override bool Create(ITreeNode parent, string name)
        {
            SQLiteConnWizard wiz = new SQLiteConnWizard(parent, name);
            return wiz.ShowDialogEx() == DialogResult.OK;
        }

        public override bool CreateFile(string file)
        {
            SQLiteConnWizard wiz = new SQLiteConnWizard(file);
            return wiz.ShowDialogEx() == DialogResult.OK;
        }

        public override int Weight
        {
            get { return 8; }
        }
    }
}
