using System;
using System.Collections.Generic;
using System.Text;
using TaskScheduler;
using System.IO;

namespace DatAdmin
{
    public static class SystemSchedulerTool
    {
        static ScheduledTasks tasks = new ScheduledTasks();

        public static bool Exists(string name)
        {
            string[] names = tasks.GetTaskNames();
            foreach (string tested in names)
            {
                if (Path.GetFileNameWithoutExtension(tested).ToLower() == name.ToLower())
                {
                    return true;
                }
            }
            return false;
        }

        public static void ScheduleTask(string name, string appname, string parameters)
        {
            Task t = Exists(name) ? tasks.OpenTask(name) : tasks.CreateTask(name);
            try
            {
                t.ApplicationName = appname;
                t.Parameters = parameters;
                t.Save();
                if (t.DisplayPropertySheet()) t.Save();
            }
            finally
            {
                t.Close();
            }
        }

        public static void DeleteTask(string name)
        {
            tasks.DeleteTask(name);
        }
    }
}
