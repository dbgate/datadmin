using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.Data;
using System.IO;

namespace Plugin.advjobs
{
    public class JobPlanner
    {
        public static JobPlanner Instance = new JobPlanner();

        private JobDbConnection m_conn;

        public JobPlanner()
        {
            m_conn = new JobDbConnection();
        }

        public JobDbConnection Connection
        {
            get { return m_conn; }
        }

        public void RunNow(int id, string file)
        {
            RunJob(id, file, DateTime.Now);
        }

        private void RunJob(int jobid, string file, DateTime exeTime)
        {
            var job = Job.LoadFromFile(Path.Combine(Core.JobsDirectory, file));
            var proc = job.CreateProcess(new Dictionary<string, string>());
            m_conn.ExecuteNonQuery(String.Format("insert into JobExecute (Job_ID, StartedAt) values ('{0}', '{1}')",
                                                 jobid, DateTime.Now.ToString("s")));
            int exeid = m_conn.GetInsertId();
            proc.OnFinish += new FinishCallback
                                 {
                                     Connection = m_conn,
                                     JobExeID = exeid,
                                 }.OnFinish;
            proc.Start();
        }

        public void EveryMinute()
        {
            var now = DateTime.Now;
            string sql =
                @"
    select j.ID, j.JobFile, j.Minutes, j.Hours, j.DaysOfMonth, j.Months, j.DaysOfWeek, max(ex.StartedAt) as LastExecute, j.CreatedAt
    from Job j left join JobExecute ex on j.ID = ex.Job_ID group by j.ID
";
            var tbl = m_conn.LoadQuery(sql);
            foreach (DataRow row in tbl.Rows)
            {
                var sch = new ScheduleDef(row[2].ToString(), row[3].ToString(), row[4].ToString(), row[5].ToString(),
                                          row[6].ToString());
                DateTime lastex;
                if (row[7] != null && row[7] != DBNull.Value) lastex = DateTime.Parse(row[7].ToString());
                else lastex = DateTime.Parse(row[8].ToString());
                DateTime? execute = sch.GetExecuteBetween(lastex, now);
                if (execute != null)
                {
                    RunJob(Int32.Parse(row[0].ToString()), row[1].ToString(), execute.Value);
                }
            }
        }

        private class FinishCallback
        {
            internal int JobExeID;
            internal JobDbConnection Connection;

            internal void OnFinish()
            {
                Connection.ExecuteNonQuery(String.Format("update JobExecute set FinishedAt='{0}' where ID='{1}'",
                                                         DateTime.Now.ToString("s"), JobExeID));
            }
        }
    }

    [PluginHandler]
    public class JobPluginHandler : PluginHandlerBase
    {
        public override void OnLoadedAllPlugins()
        {
            HJob.EveryMinute += JobPlanner.Instance.EveryMinute;
        }
    }
}

