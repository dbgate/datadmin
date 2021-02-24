using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    public static class ProcessRegister
    {
        private static List<Process> m_processes = new List<Process>();
        private static List<string> m_bgTasks = new List<string>();


        public static List<Process> Processes
        {
            get
            {
                List<Process> res = new List<Process>();
                lock (m_processes)
                {
                    res.AddRange(m_processes);
                }
                return res;
            }
        }

        public static List<string> BgTasks
        {
            get
            {
                List<string> res = new List<string>();
                lock (m_bgTasks)
                {
                    res.AddRange(m_bgTasks);
                }
                return res;
            }
        }

        public static int BgTaskCount
        {
            get
            {
                lock (m_bgTasks) return m_bgTasks.Count;
            }
        }

        public static void OnStartedProcess(Process proc)
        {
            lock (m_processes) m_processes.Insert(0, proc);
            HProcess.CallStartedProcess(proc);
        }

        public static void AddBackgroundTask(string title)
        {
            lock (m_bgTasks) m_bgTasks.Add(title);
            HProcess.CallBgTasksChanged();
        }

        public static void RemoveBackgroundTask(string title)
        {
            lock (m_bgTasks) m_bgTasks.Remove(title);
            HProcess.CallBgTasksChanged();
        }
    }
}
