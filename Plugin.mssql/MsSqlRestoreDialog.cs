using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DatAdmin;
using System.IO;

namespace Plugin.mssql
{
    public partial class MsSqlRestoreDialog : FormEx
    {
        IPhysicalConnection m_conn;
        int m_backupsetid;
        bool m_ignoreTabChange;

        public MsSqlRestoreDialog(IPhysicalConnection conn, int backupsetid)
        {
            InitializeComponent();
            Initialize(conn);

            m_ignoreTabChange = true;
            tabControl1.SelectedIndex = 1;
            m_ignoreTabChange = false;
            rbtFile.Checked = true;
            LoadBackupSet(backupsetid);
        }

        public MsSqlRestoreDialog(IPhysicalConnection conn)
        {
            InitializeComponent();
            Initialize(conn);
        }

        private void Initialize(IPhysicalConnection conn)
        {
            m_conn = conn;
            m_conn.Owner = this;
            Async.SafeOpen(conn);
            cbxDatabase.Items.Clear();
            var table = conn.SystemConnection.LoadTableFromQuery("select distinct database_name from msdb..backupset order by database_name");
            foreach (DataRow row in table.Rows)
            {
                cbxDatabase.Items.Add(row[0].ToString());
            }
            rbtDatabase_CheckedChanged(this, EventArgs.Empty);
        }

        private void LoadBackupSet(int backupsetid)
        {
            m_backupsetid = backupsetid;
            tbxFile.Text = m_conn.SystemConnection.ExecuteScalar("select m.physical_device_name from msdb..backupmediafamily m inner join msdb..backupset b on b.media_set_id=m.media_set_id where b.backup_set_id=" + m_backupsetid.ToString()).ToString();
            var table = m_conn.SystemConnection.LoadTableFromQuery("select logical_name, physical_name, file_type from msdb..backupfile where backup_set_id=" + m_backupsetid.ToString());
            dataGridView1.Rows.Clear();
            foreach (DataRow row in table.Rows)
            {
                string physfile = row[1].ToString();
                dataGridView1.Rows.Add(row[0].ToString(), physfile, physfile);
            }
        }

        private void LoadBackupSet(string file)
        {
            var table = m_conn.SystemConnection.LoadTableFromQuery(
                String.Format("RESTORE FILELISTONLY FROM  DISK = N'{0}' WITH NOUNLOAD", file)
                );
            dataGridView1.Rows.Clear();
            foreach (DataRow row in table.Rows)
            {
                string physfile = row["PhysicalName"].ToString();
                dataGridView1.Rows.Add(row["LogicalName"].ToString(), physfile, physfile);
            }
        }

        private string GenerateSql()
        {
            var sb = new StringBuilder();
            sb.AppendFormat("RESTORE DATABASE [{0}] FROM DISK = N'{1}' WITH FILE = 1, NOUNLOAD, STATS = 10", tbxDatabase.Text, tbxFile.Text);
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                sb.AppendFormat(", MOVE N'{0}' TO N'{1}'", row.Cells[0].Value, row.Cells[2].Value);
            }
            return sb.ToString();
        }

        private void UpdateFileNames()
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                string file = row.Cells[1].Value.ToString();
                string dir = Path.GetDirectoryName(file);
                string ext = Path.GetExtension(file);
                row.Cells[2].Value = Path.Combine(dir, tbxDatabase.Text + ext);
            }
        }

        public static void Run(IPhysicalConnection conn, int backupsetid)
        {
            var win = new MsSqlRestoreDialog(conn, backupsetid);
            win.ShowDialogEx();
        }

        public static void Run(IPhysicalConnection conn)
        {
            var win = new MsSqlRestoreDialog(conn);
            win.ShowDialogEx();
        }

        private bool CheckDbName()
        {
            if (String.IsNullOrEmpty(tbxDatabase.Text))
            {
                StdDialog.ShowError("Please fill database name first");
                return false;
            }
            return true;
        }

        private void chbOverrideDbFileNames_CheckedChanged(object sender, EventArgs e)
        {
            if (!CheckDbName())
            {
                chbOverrideDbFileNames.Checked = false;
                return;
            }
            dataGridView1.ReadOnly = !chbOverrideDbFileNames.Checked;
        }

        private void tbxDatabase_TextChanged(object sender, EventArgs e)
        {
            if (!chbOverrideDbFileNames.Checked)
            {
                UpdateFileNames();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnMakeScript_Click(object sender, EventArgs e)
        {
            OpenQueryParameters pars = new OpenQueryParameters();
            //m_conn.SetOnOpenDatabase(m_db.DatabaseName);
            pars.SqlText = GenerateSql();
            MainWindow.Instance.OpenContent(new QueryFrame(m_conn.Clone(), pars));
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!CheckDbName()) return;
            var job = RunSqlJob.CreateJob(m_conn.StoredConnection, null, GenerateSql());
            Close();
            job.StartProcess();
        }

        private void MsSqlRestoreDialog_FormClosed(object sender, FormClosedEventArgs e)
        {
            Async.SafeClose(m_conn);
        }

        private void cbxDatabase_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbxBackup.Items.Clear();
            string db = cbxDatabase.Items[cbxDatabase.SelectedIndex].ToString();
            var table = m_conn.SystemConnection.LoadTableFromQuery("select backup_set_id, name from msdb..backupset where database_name='" + db + "' order by backup_finish_date desc");
            foreach (DataRow row in table.Rows)
            {
                cbxBackup.Items.Add(
                    new BackupSetHodler
                    {
                        bid = Int32.Parse(row[0].ToString()),
                        name = row[1].ToString()
                    });
            }
        }

        class BackupSetHodler
        {
            internal int bid;
            internal string name;

            public override string ToString()
            {
                return name;
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_ignoreTabChange) return;
            try
            {
                if (tabControl1.SelectedIndex == 1)
                {
                    if (rbtDatabase.Checked)
                    {
                        if (cbxDatabase.SelectedIndex < 0) throw new Exception("Please select database");
                        if (cbxBackup.SelectedIndex < 0) throw new Exception("Please select backup");
                        LoadBackupSet(((BackupSetHodler)(cbxBackup.Items[cbxBackup.SelectedIndex])).bid);
                    }
                    if (rbtFile.Checked)
                    {
                        LoadBackupSet(tbxFile.Text);
                    }
                }
            }
            catch (Exception ex)
            {
                StdDialog.ShowError("Error loading metadata from backup: " + ex.Message);
                tabControl1.SelectedIndex = 0;
            }
        }

        private void rbtDatabase_CheckedChanged(object sender, EventArgs e)
        {
            cbxDatabase.Enabled = cbxBackup.Enabled = rbtDatabase.Checked;
            tbxFile.Enabled = rbtFile.Checked;
        }
    }
}
