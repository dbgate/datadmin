using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class RestoreDbForm : FormEx
    {
        IDatabaseSource m_conn;
        BackupContainer m_backup;

        public RestoreDbForm(IDatabaseSource conn, BackupContainer backup)
        {
            InitializeComponent();
            m_backup = backup;
            SetDatabase(conn);
        }

        void SetDatabase(IDatabaseSource conn)
        {
            cbxDatabase.Items.Clear();
            m_conn = conn;
            m_conn.Connection.Owner = this;
            Async.SafeOpen(m_conn.Connection);
            foreach (string s in m_conn.Connection.InvokeR1<List<string>, IPhysicalConnection>(m_conn.Dialect.GetDatabaseNames, m_conn.Connection))
            {
                if (s != null)
                {
                    cbxDatabase.Items.Add(s);
                }
            }
            if (conn.DatabaseName != null)
            {
                cbxDatabase.Text = conn.DatabaseName;
            }
            tbxDestServer.Text = m_conn.Connection.ToString();
        }

        public static void Run(IDatabaseSource conn, BackupContainer backup)
        {
            if (conn.Connection == null) return;
            var win = new RestoreDbForm(conn, backup);
            win.ShowDialogEx();
        }

        private IDatabaseSource GetDbConn()
        {
            string seldb = cbxDatabase.Text;
            if (seldb == m_conn.DatabaseName)
            {
                if (m_conn.DatabaseCaps.IsPhantom)
                {
                    return m_conn.Server.CreateDatabase(seldb, null);
                }
                return m_conn;
            }
            bool isdb = false;
            foreach (string s in cbxDatabase.Items)
            {
                if (String.Compare(s, seldb, true) == 0)
                {
                    isdb = true;
                    break;
                }
            }
            if (seldb.IsEmpty()) seldb = null;
            if (!isdb && m_conn.Dialect.DumperCaps.CreateDatabase && seldb != null) m_conn.Connection.InvokeScript(dmp => { dmp.CreateDatabase(seldb, null); }, null);
            return new GenericDatabaseSource(null, m_conn.Connection, seldb);
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            Close();
            RestoreDbJob.CreateJob(m_backup, GetDbConn(), new JobProperties()).StartProcess();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnExportAsJob_Click(object sender, EventArgs e)
        {
            Job.AskAndExportToFile(() => RestoreDbJob.CreateJob(m_backup, GetDbConn(), new JobProperties()));
        }

        private void RestoreDbForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Async.SafeClose(m_conn.Connection);
        }

        private void btnChooseOtherServer_Click(object sender, EventArgs e)
        {
            var db = TreeSelectForm.SelectDatabase();
            if (db != null)
            {
                Async.SafeClose(m_conn.Connection);
                SetDatabase(db.CloneSource());
            }
        }
    }
}
