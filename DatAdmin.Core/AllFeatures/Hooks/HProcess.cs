using System;
using System.Collections.Generic;
using System.Text;

namespace DatAdmin
{
    public static class HProcess
    {
        public static void CallStartedProcess(Process proc)
        {
            if (StartedProcess != null) StartedProcess(proc);
        }

        public static void CallFinishedProcess(Process proc)
        {
            if (FinishedProcess != null) FinishedProcess(proc);
        }

        public static void CallChangedProcess(Process proc)
        {
            if (ChangedProcess != null) ChangedProcess(proc);
        }

        public static void CallBgTasksChanged()
        {
            if (BgTasksChanged != null) BgTasksChanged();
        }

        public static event Action<Process> StartedProcess;
        public static event Action<Process> FinishedProcess;
        public static event Action<Process> ChangedProcess;
        public static event Action BgTasksChanged;
    }
}
