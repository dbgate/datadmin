using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DatAdmin;

namespace Plugin.mssql
{
    public partial class MsSqlBackupDialog : FormEx
    {
        IDatabaseSource m_db;
        public MsSqlBackupDialog(string bakdir, IDatabaseSource db)
        {
            var dt = DateTime.UtcNow;
            InitializeComponent();
            m_db = db;
            cbxBackupType.SelectedIndex = 0;
            tbxDatabase.Text = db.DatabaseName;
            tbxBackupSetFolder.Text = bakdir;
            tbxBackupSetName.Text = db.DatabaseName + " Backup " + dt.ToString("yyyy-MM-dd-HH-mm-ss");
            tbxBackupSetFile.Text = db.DatabaseName + "-" + dt.ToString("yyyy-MM-dd-HH-mm-ss") + ".bak";
        }

        public static void Run(string bakdir, IDatabaseSource db)
        {
            var win = new MsSqlBackupDialog(bakdir, db);
            win.ShowDialogEx();
        }

        private string GenerateSql()
        {
            StringBuilder sb = new StringBuilder();
            if (cbxBackupType.SelectedIndex == 2)
            {
                sb.Append("BACKUP LOG [");
            }
            else
            {
                sb.Append("BACKUP DATABASE [");
            }
            sb.Append(m_db.DatabaseName);
            sb.Append("] TO DISK = '");
            sb.Append(tbxBackupSetFolder.Text + "\\" + tbxBackupSetFile.Text);
            sb.Append("' WITH NAME=N'");
            sb.Append(tbxBackupSetName.Text);
            sb.Append("'");
            if (!String.IsNullOrEmpty(tbxBackupSetDescription.Text))
            {
                sb.AppendFormat(", DESCRIPTION=N'{0}'", tbxBackupSetDescription.Text);
            }
            if (cbxBackupType.SelectedIndex == 1)
            {
                sb.Append(", DIFFERENTIAL");
            }
            return sb.ToString();
        }

        private void btnMakeScript_Click(object sender, EventArgs e)
        {
            OpenQueryParameters pars = new OpenQueryParameters();
            m_db.Connection.SetOnOpenDatabase(m_db.DatabaseName);
            pars.SqlText = GenerateSql();
            MainWindow.Instance.OpenContent(new QueryFrame(m_db.Connection, pars));
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            var job = RunSqlJob.CreateJob(m_db, GenerateSql());
            Close();
            job.StartProcess();
        }
    }
}
