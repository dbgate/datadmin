using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SQLite;
using DatAdmin;
using System.IO;
using System.Data;

namespace Plugin.advjobs
{
    public class JobDbConnection : InternalDbConnection
    {
        public JobDbConnection()
            : base(SQLiteFactory.Instance, "schedule", Generated.DbCreator.UpdateDb)
        {
        }

        public void ScheduleJobDialog(string fullJobFile)
        {
            string relfile = IOTool.RelativePathTo(Core.JobsDirectory, fullJobFile);
            int cnt = Int32.Parse(ExecuteScalar(String.Format("select count(*) from Job where JobFile='{0}'", relfile.Replace("'", "''"))).ToString());
            if (cnt == 0) ExecuteNonQuery(String.Format("insert into Job (JobFile, CreatedAt) values ('{0}', '{1}')", relfile.Replace("'", "''"), DateTime.Now.ToString("s")));
            int id = Int32.Parse(ExecuteScalar(String.Format("select id from Job where JobFile='{0}'", relfile.Replace("'", "''"))).ToString());
            ScheduleForm.Run(this, id);
        }

        public bool JobFileScheduled(string fullJobFile)
        {
            string relfile = IOTool.RelativePathTo(Core.JobsDirectory, fullJobFile);
            int cnt = Int32.Parse(JobPlanner.Instance.Connection.ExecuteScalar(String.Format("select count(*) from Job where JobFile='{0}'", relfile.Replace("'", "''"))).ToString());
            return cnt > 0;
        }

        public void DeleteSchedule(string fullJobFile)
        {
            string relfile = IOTool.RelativePathTo(Core.JobsDirectory, fullJobFile);
            ExecuteNonQuery("delete from Job where JobFile='" + relfile + "'");
        }
    }
}
