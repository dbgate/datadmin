using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace DatAdmin
{
    public partial class ProcessListDocker : DockerBase
    {
        public ProcessListDocker(IDockerFactory factory)
            : base(factory)
        {
            InitializeComponent();

            HProcess.StartedProcess += ProcessRegister_AddedProcess;
            HProcess.FinishedProcess += ProcessRegister_RemovedProcess;
            HProcess.ChangedProcess += ProcessRegister_ChangedProcess;
            HProcess.BgTasksChanged += ProcessRegister_OnBgTasksChanged;

            ReloadProcesses();
        }

        public override void OnClose()
        {
            base.OnClose();
            HProcess.StartedProcess -= ProcessRegister_AddedProcess;
            HProcess.FinishedProcess -= ProcessRegister_RemovedProcess;
            HProcess.ChangedProcess -= ProcessRegister_ChangedProcess;
            HProcess.BgTasksChanged -= ProcessRegister_OnBgTasksChanged;
        }

        void ProcessRegister_OnBgTasksChanged()
        {
            //labBgActions.Text = String.Format("{0}: {1}", Texts.Get("s_bg_actions"), ProcessRegister.BgTaskCount);
        }

        void ReloadProcesses()
        {
            lswProcess.Items.Clear();
            foreach (Process proc in ProcessRegister.Processes)
            {
                ListViewItem it = lswProcess.Items.Add(proc.Title);
                it.SubItems.Add(proc.CurWork);
                if (proc.Estimate != null)
                {
                    it.SubItems.Add(proc.Estimate.Value.TotalSeconds + " s");
                }
                else
                {
                    it.SubItems.Add("???");
                }
                it.SubItems.Add(proc.Duration.TotalSeconds.ToString() + " s");
                it.SubItems.Add(Texts.Get(proc.StateTitle));
                it.SubItems.Add(Texts.Get(proc.Description));
                it.SubItems.Add(proc.Job.SavedName ?? "");
                it.Tag = proc;
            }
            UpdateProcessEnabling();
        }

        private void UpdateProcessEnabling()
        {
            if (lswProcess.FocusedItem != null)
            {
                var p = (Process)lswProcess.FocusedItem.Tag;
                btnCancelProcess.Enabled = p.CanCancel;
                btnShowProcessWindow.Enabled = true;
            }
            else
            {
                btnCancelProcess.Enabled = false;
                btnShowProcessWindow.Enabled = false;
            }
        }

        private void lswProcess_Click(object sender, EventArgs e)
        {
            UpdateProcessEnabling();
        }

        void ProcessRegister_ChangedProcess(Process proc)
        {
            Invoke((Action<Process>)ReloadProcess, proc);
        }

        void ProcessRegister_RemovedProcess(Process proc)
        {
            Invoke((Action)ReloadProcesses);
        }

        void ProcessRegister_AddedProcess(Process proc)
        {
            Invoke((Action)ReloadProcesses);
        }

        private void btnCancelProcess_Click(object sender, EventArgs e)
        {
            if (lswProcess.FocusedItem != null)
            {
                try
                {
                    ((Process)lswProcess.FocusedItem.Tag).CancelWithQuery();
                }
                catch (Exception err)
                {
                    Errors.Report(err);
                }
            }
        }

        private void btnShowProcessWindow_Click(object sender, EventArgs e)
        {
            if (lswProcess.FocusedItem != null)
            {
                RunProcessForm.Run((Process)lswProcess.FocusedItem.Tag);
            }
        }

        void ReloadProcess(Process process)
        {
            ReloadProcesses();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ReloadProcesses();
        }
    }

    [PluginHandler]
    public class ProcessListHandler : PluginHandlerBase
    {
        public override void OnLoadedAllPlugins()
        {
            HProcess.StartedProcess += new Action<Process>(HProcess_StartedProcess);
        }

        void HProcess_StartedProcess(Process proc)
        {
            MainWindow.Instance.RunInMainWindow((Action)delegate() { RunProcessForm.Run(proc); });
        }
    }

    [DockerFactory(Title = "Process list window", Name = "process_window")]
    public class ProcesslistDockerFactory : DockerFactoryBase
    {
        public override IDocker CreateDocker()
        {
            return new ProcessListDocker(this);
        }

        public override string MenuTitle
        {
            get { return "s_processes"; }
        }

        public override Bitmap Icon
        {
            get { return CoreIcons.job; }
        }

        public override DockerState InitialState
        {
            get { return DockerState.DockBottom; }
        }

        public override Keys Shortcut
        {
            get { return Keys.Control | Keys.Alt | Keys.P; }
        }
    }
}
