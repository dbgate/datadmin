using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DatAdmin;
using System.Linq;
using System.IO;

namespace Plugin.advjobs
{
    public partial class JobManagerDocker : DockerBase
    {
        List<JobWithFile> m_loadedJobs = new List<JobWithFile>();
        ImageCache m_imgCache;
        JobWithFile m_filledJob;

        public JobManagerDocker(IDockerFactory factory)
            : base(factory)
        {
            InitializeComponent();
            m_imgCache = new ImageCache(imageList1, Color.White);
            RefreshJobs();
        }

        public void RefreshJobs()
        {
            lsvJobs.Items.Clear();
            int index = 0;
            foreach (string file in Directory.GetFiles(Core.JobsDirectory, "*.djb", SearchOption.AllDirectories))
            {
                var job = Job.LoadFromFile(file);
                var rec = new JobWithFile { Job = job, File = IOTool.RelativePathTo(Core.JobsDirectory, file), FullFile = file };
                m_loadedJobs.Add(rec);
                var item = lsvJobs.Items.Add(rec.File);
                item.SubItems.Add(job.GetAllElementaryCommands().Count().ToString());
                item.SubItems.Add(job.GetAllReportConfigs().CreateDelimitedText(";"));
                item.Tag = rec;
                if (index == 0)
                {
                    item.Selected = item.Focused = true;
                    lsvJobs.FocusedItem = item;
                }
                if (JobPlanner.Instance.Connection.JobFileScheduled(file)) item.ImageIndex = m_imgCache.GetImageIndex(CoreIcons.clock);
                index++;
            }
            FillCommands();
        }

        public void RefreshCommands()
        {
            if (lsvJobs.FocusedItem == null) return;
            var rec = (JobWithFile)lsvJobs.FocusedItem.Tag;
            rec.Reload();
            FillCommands();
            FillCurrentJob();
        }

        private void FillCommands()
        {
            lsvCommands.Items.Clear();
            m_filledJob = null;
            if (lsvJobs.FocusedItem == null) return;
            var rec = (JobWithFile)lsvJobs.FocusedItem.Tag;
            m_filledJob = rec;
            foreach (var cmd in rec.Job.GetAllElementaryCommands())
            {
                var item = lsvCommands.Items.Add(Texts.Get(cmd.TypeTitle));
                item.ImageIndex = m_imgCache.GetImageIndex(cmd.Image);
                item.SubItems.Add(cmd.ToString());
                item.Tag = cmd;
            }
        }

        private void FillCurrentJob()
        {
            var item = lsvJobs.FocusedItem;
            if (item == null) return;
            var rec = (JobWithFile)item.Tag;
            if (JobPlanner.Instance.Connection.JobFileScheduled(rec.FullFile)) item.ImageIndex = m_imgCache.GetImageIndex(CoreIcons.clock);
            else item.ImageIndex = -1;
            item.SubItems[1].Text = rec.Job.GetAllElementaryCommands().Count.ToString();
            item.SubItems[2].Text = rec.Job.GetAllReportConfigs().CreateDelimitedText(";");
        }

        class JobWithFile
        {
            internal Job Job;
            internal string File;
            internal string FullFile;

            internal void Reload()
            {
                Job = Job.LoadFromFile(FullFile);
            }

            internal void Save()
            {
                Job.SaveToFile(FullFile);
            }
        }

        private void btnRefreshJobs_Click(object sender, EventArgs e)
        {
            RefreshJobs();
        }

        private void btnRefreshCommands_Click(object sender, EventArgs e)
        {
            RefreshCommands();
        }

        private void lsbJobs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lsvJobs.FocusedItem != null && lsvJobs.FocusedItem.Tag == m_filledJob) return;
            FillCommands();
        }

        private void btnRunJob_Click(object sender, EventArgs e)
        {
            if (lsvJobs.SelectedItems.Count == 0) return;
            if (MessageBox.Show(Texts.Get("s_really_run$jobs", "jobs", lsvJobs.SelectedItems.Count), VersionInfo.ProgramTitle, MessageBoxButtons.YesNo) != DialogResult.Yes) return;
            var cmdlist = new List<JobCommand>();
            var joblist = new List<string>();
            var job = new Job();
            foreach (ListViewItem item in lsvJobs.SelectedItems)
            {
                var rec = (JobWithFile)item.Tag;
                joblist.Add(rec.Job.ToString());
                // we must load current version of job
                var job2 = Job.LoadFromFile(rec.FullFile);
                foreach (var cmd in rec.Job.Root.Commands)
                {
                    var find = job2.FindCommand(cmd.GroupId);
                    if (find != null)
                    {
                        job.AddCommand(find.Clone(false));
                    }
                    else
                    {
                        job.AddCommand(cmd.Clone(false));
                    }
                }
                job.Root.ReportConfigs.AddRange(job2.Root.ReportConfigs);
            }
            job.Title = joblist.CreateDelimitedText(";");
            job.CreateProcess(new Dictionary<string, string>()).Start();
        }

        private void btnRunCommand_Click(object sender, EventArgs e)
        {
            if (lsvCommands.SelectedItems.Count == 0) return;
            if (MessageBox.Show(Texts.Get("s_really_run$commands", "commands", lsvCommands.SelectedItems.Count), VersionInfo.ProgramTitle, MessageBoxButtons.YesNo) != DialogResult.Yes) return;
            var cmdlist = new List<JobCommand>();
            var job2 = Job.LoadFromFile(m_filledJob.FullFile);
            foreach (ListViewItem item in lsvCommands.SelectedItems)
            {
                var cmd = (JobCommand)item.Tag;
                cmdlist.Add(job2.FindCommand(cmd.GroupId).Clone(false));
            }
            var job = Job.FromCommands(cmdlist, new JobProperties());
            job.CreateProcess(new Dictionary<string, string>()).Start();
        }

        private void btnDeleteJob_Click(object sender, EventArgs e)
        {
            var files = new List<string>();
            if (lsvJobs.SelectedItems.Count == 0) return;
            foreach (ListViewItem item in lsvJobs.SelectedItems)
            {
                var rec = (JobWithFile)item.Tag;
                files.Add(rec.FullFile);
            }
            if (MessageBox.Show(Texts.Get("s_really_delete_jobs") + "\n" + files.CreateDelimitedText("\n"), VersionInfo.ProgramTitle, MessageBoxButtons.YesNo) != DialogResult.Yes) return;
            foreach (string fn in files) File.Delete(fn);
            RefreshJobs();
        }

        private List<JobCommand> GetCommands()
        {
            if (lsvCommands.SelectedItems.Count == 0) return null;
            if (m_filledJob == null) return null;
            var res = new List<JobCommand>();

            foreach (ListViewItem item in lsvCommands.SelectedItems)
            {
                var cmd = (JobCommand)item.Tag;
                res.Add(cmd);
            }
            return res;
        }

        private void btnDeleteCommand_Click(object sender, EventArgs e)
        {
            var cmds = GetCommands();
            if (cmds == null) return;

            if (MessageBox.Show(Texts.Get("s_really_delete_commands") + "\n" + cmds.CreateDelimitedText("\n"), VersionInfo.ProgramTitle, MessageBoxButtons.YesNo) != DialogResult.Yes) return;
            m_filledJob.Reload();
            foreach (var cmd in cmds)
            {
                m_filledJob.Job.Root.Commands.RemoveIf(c => c.GroupId == cmd.GroupId);
            }
            m_filledJob.Save();
            FillCommands();
            FillCurrentJob();
        }

        private void btnDuplicateCommand_Click(object sender, EventArgs e)
        {
            var cmds = GetCommands();
            if (cmds == null) return;

            m_filledJob.Reload();
            foreach (var cmd in cmds)
            {
                m_filledJob.Job.Root.Commands.Add(cmd.Clone(true));
            }
            m_filledJob.Save();
            FillCommands();
            FillCurrentJob();
        }

        private void btnEditCommand_Click(object sender, EventArgs e)
        {
            if (lsvCommands.FocusedItem == null) return;
            var cmd = (JobCommand)lsvCommands.FocusedItem.Tag;
            var rec = (JobWithFile)lsvJobs.FocusedItem.Tag;

            Job job = Job.LoadFromFile(rec.FullFile);
            JobConnection jobconn = new JobConnection(rec.FullFile);
            JobCommand cmd2 = job.Root.FindCommand(cmd.GroupId);
            cmd2.Edit(jobconn);
        }

        private void lsvCommands_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Clicks == 2)
            {
                btnEditCommand_Click(sender, e);
            }
            else
            {
                var item = lsvCommands.GetItemAt(e.X, e.Y);
                if (item == null || !item.Selected) return;
                //if (item != null && !item.Selected)
                //{
                //    if ((Control.ModifierKeys & Keys.Shift) != 0 || (Control.ModifierKeys & Keys.Control) != 0)
                //    {
                //        item.Selected = true;
                //    }
                //    else
                //    {
                //        lsvCommands.SelectOneItem(item, true);
                //    }
                //}
                var cmds = GetCommands();
                if (cmds == null) return;
                lsvCommands.DoDragDrop(cmds, DragDropEffects.Copy);
            }
        }

        private void lsvJobs_DragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetData(typeof(List<JobCommand>)) != null)
            {
                var pt = new Point(e.X, e.Y);
                pt = lsvJobs.PointToClient(pt);
                var item = lsvJobs.GetItemAt(pt.X, pt.Y);
                if (item == null) return;
                //if ((e.KeyState & (int)Keys.Shift) != 0) e.Effect = DragDropEffects.Move;
                //else e.Effect = DragDropEffects.Copy;
                e.Effect = DragDropEffects.Copy;
                lsvJobs.SelectOneItem(item, false);
            }
        }

        private void lsvJobs_DragDrop(object sender, DragEventArgs e)
        {
            var data = (List<JobCommand>)e.Data.GetData(typeof(List<JobCommand>));
            if (data == null) return;
            //bool move = (e.KeyState & (int)Keys.Shift) != 0;
            var pt = new Point(e.X, e.Y);
            pt = lsvJobs.PointToClient(pt);
            var item = lsvJobs.GetItemAt(pt.X, pt.Y);
            if (item == null) return;
            var rec = (JobWithFile)item.Tag;
            rec.Reload();
            foreach (var cmd in data)
            {
                rec.Job.Root.Commands.Add(cmd.Clone(true));
            }
            rec.Save();
            //if (move)
            //{
            //    m_filledJob.Reload();
            //    foreach (var cmd in data)
            //    {
            //        rec.Job.Root.Commands.RemoveIf(c => c.GroupId == cmd.GroupId);
            //    }
            //    m_filledJob.Save();
            //}
            FillCommands();
            FillCurrentJob();
        }

        private void btnNewJob_Click(object sender, EventArgs e)
        {
            string name = InputBox.Run("s_select_job_name", "newjob");
            if (name == null) return;
            string fn = Path.Combine(Core.JobsDirectory, name + ".djb");
            if (File.Exists(fn))
            {
                if (!StdDialog.ReallyOverwriteFile(fn)) return;
            }
            Job job = new Job();
            job.SaveToFile(fn);
            RefreshJobs();
        }

        private void MoveCurrentCommand(int d)
        {
            if (lsvCommands.SelectedIndices.Count > 0)
            {
                int index = lsvCommands.SelectedIndices[0];
                if (index + d >= 0 && index + d < m_filledJob.Job.Root.Commands.Count)
                {
                    m_filledJob.Job.Root.Commands.Exchange(index, index + d);
                    FillCommands();
                    var item = lsvCommands.Items[index + d];
                    item.Selected = item.Focused = true;
                    lsvCommands.FocusedItem = item;
                }
            }
        }

        private void btnMoveCommandUp_Click(object sender, EventArgs e)
        {
            MoveCurrentCommand(-1);
        }

        private void btnMoveCommandDown_Click(object sender, EventArgs e)
        {
            MoveCurrentCommand(1);
        }

        private void mnuSchedule_Click(object sender, EventArgs e)
        {
            if (m_filledJob == null) return;
            JobPlanner.Instance.Connection.ScheduleJobDialog(m_filledJob.FullFile);
            FillCurrentJob();
        }

        private void mnuDeleteSchedule_Click(object sender, EventArgs e)
        {
            if (m_filledJob == null) return;
            JobPlanner.Instance.Connection.DeleteSchedule(m_filledJob.FullFile);
            FillCurrentJob();
        }

        private void mnuManageSchedule_Click(object sender, EventArgs e)
        {
            MainWindow.Instance.ShowDocker(new JobSchedulerDockerFactory());
        }

        private void JobManagerDocker_Resize(object sender, EventArgs e)
        {
            lsvCommands.ResizeColumnsToWidth();
            lsvJobs.ResizeColumnsToWidth();
        }

        private void mnuSaveJobShellToFile_Click(object sender, EventArgs e)
        {
            if (lsvJobs.SelectedItems.Count == 0)
            {
                StdDialog.ShowError("s_no_jobs_selected");
                return;
            }
            SaveToShellDialog(SaveJobShell);
        }

        private void SaveToShellDialog(Action<TextWriter> saveFunc)
        {
            if (saveFileDialog1.ShowDialogEx() == DialogResult.OK)
            {
                using (var sw = new StreamWriter(saveFileDialog1.FileName))
                {
                    saveFunc(sw);
                }
            }
        }

        private void SaveToShellClipboard(Action<TextWriter> saveFunc)
        {
            var sw = new StringWriter();
            saveFunc(sw);
            Clipboard.SetText(sw.ToString());
        }

        private void SaveJobShell(TextWriter tw)
        {
            foreach (ListViewItem item in lsvJobs.SelectedItems)
            {
                var rec = (JobWithFile)item.Tag;
                tw.WriteLine("\"{0}\" runjob \"{1}\"", Path.Combine(Core.ProgramDirectory, "daci.exe"), rec.File);
            }
        }

        private void mnuCopyJobSchellToClipboard_Click(object sender, EventArgs e)
        {
            if (lsvJobs.SelectedItems.Count == 0)
            {
                StdDialog.ShowError("s_no_jobs_selected");
                return;
            }
            SaveToShellClipboard(SaveJobShell);
        }

        private void SaveCommandsShell(TextWriter tw)
        {
            var cmds = GetCommands();
            if (cmds == null) return;
            tw.WriteLine("\"{0}\" runjob \"{1}\" --commands \"{2}\"", 
                Path.Combine(Core.ProgramDirectory, "daci.exe"), 
                m_filledJob.File,
                (from c in cmds select c.GroupId).CreateDelimitedText("|")
                );
        }

        private void btnSaveCommandShellToFile_Click(object sender, EventArgs e)
        {
            if (lsvCommands.SelectedItems.Count == 0)
            {
                StdDialog.ShowError("s_no_commands_selected");
                return;
            }
            SaveToShellDialog(SaveCommandsShell);
        }

        private void btnCopyCommandShellToClipboard_Click(object sender, EventArgs e)
        {
            if (lsvCommands.SelectedItems.Count == 0)
            {
                StdDialog.ShowError("s_no_commands_selected");
                return;
            }
            SaveToShellClipboard(SaveCommandsShell);
        }

        private void btnAddToFavorites_Click(object sender, EventArgs e)
        {
            if (m_filledJob == null) return;
            AddToFavoriteForm.Run(new JobFavorite
            {
                JobFile = m_filledJob.File,
            }, Path.GetFileNameWithoutExtension(m_filledJob.FullFile));
        }

        private void tbnReport_Click(object sender, EventArgs e)
        {
            if (m_filledJob == null) return;
            MainWindow.Instance.OpenContent(new JobReportFrame(m_filledJob.FullFile));
        }
    }

    [DockerFactory(Title = "Job manager", Name = "job_manager", RequiredFeature = AdvancedJobsFeature.Test)]
    public class JobManagerDockerFactory : DockerFactoryBase
    {
        public override IDocker CreateDocker()
        {
            return new JobManagerDocker(this);
        }

        public override string MenuTitle
        {
            get { return "s_job_manager"; }
        }

        public override Bitmap Icon
        {
            get { return CoreIcons.job; }
        }

        public override DockerState InitialState
        {
            get { return DockerState.Document; }
        }

        public override Keys Shortcut
        {
            get { return Keys.Control | Keys.Alt | Keys.J; }
        }
    }
}
