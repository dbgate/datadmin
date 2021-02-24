using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DatAdmin;

namespace Plugin.advjobs
{
    public partial class JobSchedulerDocker : DockerBase
    {
        JobDbConnection m_conn;

        public JobSchedulerDocker(IDockerFactory factory)
            : base(factory)
        {
            InitializeComponent();
            m_conn = new JobDbConnection();
            ReloadData();
        }

        private void ReloadData()
        {
            string sql = @"
    select j.ID, j.JobFile, j.Minutes, j.Hours, j.DaysOfWeek, j.DaysOfMonth, j.Months, max(ex.FinishedAt) as LastExecute
    from Job j left join JobExecute ex on j.ID = ex.Job_ID group by j.ID
";
            var tbl = m_conn.LoadQuery(sql);
            dataGridView1.DataSource = tbl;
        }

        private void btnAddSchedule_Click(object sender, EventArgs e)
        {
            string job = SelectJobForm.Run();
            if (job != null)
            {
                m_conn.ExecuteNonQuery(String.Format("insert into Job (JobFile, CreatedAt) values ('{0}', '{1}')", job.Replace("'", "''"), DateTime.Now.ToString("s")));
                CheckServiceRunning();
                ReloadData();
            }
        }

        int SelectedJob
        {
            get
            {
                if (dataGridView1.CurrentCell == null) return 0;
                int id = Int32.Parse(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value.ToString());
                return id;
            }
        }

        string SelectedJobFile
        {
            get
            {
                if (dataGridView1.CurrentCell == null) return null;
                return dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[1].Value.ToString();
            }
        }

        private void btnSchedule_Click(object sender, EventArgs e)
        {
            int id = SelectedJob;
            if (id == 0) return;
            ScheduleForm.Run(m_conn, id);
            ReloadData();
            CheckServiceRunning();
        }

        private void CheckServiceRunning()
        {
            if (!ServiceControl.IsRunning())
            {
                StdDialog.ShowError("s_warning_service_not_running");
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ReloadData();
        }

        private void btnRunNow_Click(object sender, EventArgs e)
        {
            int id = SelectedJob;
            if (id == 0) return;
            JobPlanner.Instance.RunNow(id, SelectedJobFile);
        }

        private void btnStart_Click(object sender, EventArgs e)
        {
            ServiceControl.Start();
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            ServiceControl.Stop();
        }

        private void btnInstall_Click(object sender, EventArgs e)
        {
            ServiceControl.Install();
        }

        private void btnUninstall_Click(object sender, EventArgs e)
        {
            ServiceControl.Uninstall();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            labState.Text = Texts.Get(ServiceControl.GetState());
        }

        private void btnRemoveSchedule_Click(object sender, EventArgs e)
        {
            int id = SelectedJob;
            m_conn.ExecuteNonQuery("delete from Job where ID=" + id.ToString());
            ReloadData();
        }
    }

    [DockerFactory(Title = "Scheduled jobs", Name = "scheduled_jobs", RequiredFeature = AdvancedJobsFeature.Test)]
    public class JobSchedulerDockerFactory : DockerFactoryBase
    {
        public override IDocker CreateDocker()
        {
            return new JobSchedulerDocker(this);
        }

        public override string MenuTitle
        {
            get { return "s_scheduled_jobs"; }
        }

        public override Bitmap Icon
        {
            get { return CoreIcons.clock; }
        }

        public override DockerState InitialState
        {
            get { return DockerState.Document; }
        }

        public override Keys Shortcut
        {
            get { return Keys.Control | Keys.Alt | Keys.E; }
        }
    }
}
