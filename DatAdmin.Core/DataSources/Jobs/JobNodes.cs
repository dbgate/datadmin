using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Windows.Forms;

namespace DatAdmin
{
    //[NodeFactory(Name = "job")]
    //public class JobNodeFactory : NodeFactoryBase
    //{
    //    public override ITreeNode FromFile(ITreeNode parent, string file)
    //    {
    //        if (!LicenseTool.FeatureAllowed(JobsFeature.Test)) return null;
    //        if (file.ToLower().EndsWith(".djb"))
    //        {
    //            try
    //            {
    //                return new JobTreeNode(parent, file);
    //            }
    //            catch (Exception)
    //            {
    //                return null;
    //            }
    //        }
    //        return null;
    //    }
    //}

    [FileHandler(Name= " job")]
    public class JobFileHandler : FileHandlerBase
    {
        public override ITreeNode CreateNode(ITreeNode parent)
        {
            return new JobTreeNode(parent, this);
        }

        public override string Extension
        {
            get { return "djb"; }
        }

        public override FileHandlerCaps Caps
        {
            get
            {
                return new FileHandlerCaps
                {
                    AllFlags = false,
                    CreateNode = true
                };
            }
        }
    }

    public class JobTreeNode : VirtualFileTreeNodeBase
    {
        JobConnection m_jobconn;
        public JobTreeNode(ITreeNode parent, IFileHandler fhandler)
            : base(parent, fhandler)
        {
            m_jobconn = new JobConnection(fhandler.FileObject.DataDiskPath);
        }
        public override ITreeNode[] GetChildren()
        {
            return new ITreeNode[] 
            { 
                new PolyCommandTreeNode(this, m_jobconn)
            };
        }
        public override bool PreparedChildren
        {
            get { return true; }
        }
        [PopupMenu("s_run", ImageName = CoreIcons.runName)]
        public void Run()
        {
            Job job = Job.LoadFromFile(FileObject.DataDiskPath);
            if (MessageBox.Show(Texts.Get("s_really_run$job", "job", job), VersionInfo.ProgramTitle, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                job.StartProcess();
            }
            //var inst = job.CreateParamsInstance();
            //if (EditPropertiesForm.Run(inst, true))
            //{
            //    job.Run(inst.Data);
            //}
        }

        public override bool DoubleClick()
        {
            Run();
            return true;
        }

        [PopupMenu("s_properties")]
        public void EditProperties()
        {
            Job job = Job.LoadFromFile(FileObject.DataDiskPath);
            if (EditPropertiesForm.Run(job, true))
            {
                job.SaveToFile(FileObject.DataDiskPath);
            }
        }
        public override string TypeTitle
        {
            get { return "s_job"; }
        }
        public override System.Drawing.Bitmap Image
        {
            get
            {
                return ExistsWindowsTask() ? CoreIcons.windows : CoreIcons.job;
            }
        }

        bool ExistsWindowsTask()
        {
            return SystemSchedulerTool.Exists(GetTaskName());
        }

        string GetTaskName()
        {
            return "datadmin_" + System.IO.Path.GetFileNameWithoutExtension(FileObject.DataDiskPath);
        }

        [PopupMenu("s_schedule_using_windows_scheduler", ImageName = CoreIcons.windowsName)]
        public void ScheduleJob()
        {
            SystemSchedulerTool.ScheduleTask(
                GetTaskName(),
                System.IO.Path.Combine(Core.ProgramDirectory, "daci.exe"),
                "runjob " + System.IO.Path.ChangeExtension(IOTool.RelativePathTo(Core.JobsDirectory, FileSystemPath), null).Replace("\\", "/")
                );
            CallRefresh();
        }

        [PopupMenuEnabled("s_delete_windows_schedule")]
        public bool DeleteWindowsScheduleEnabled()
        {
            return ExistsWindowsTask();
        }

        [PopupMenu("s_delete_windows_schedule", ImageName = ImageTool.COMBI_PREFIX + CoreIcons.windowsName + ImageTool.COMBI_SEPARATOR + CoreIcons.delete_overlayName)]
        public void DeleteWindowsSchedule()
        {
            SystemSchedulerTool.DeleteTask(GetTaskName());
            CallRefresh();
        }

        public static ITreeNode GetCommandTreeNode(PolyCommandTreeNode parent, JobCommand command, JobConnection jobconn)
        {
            if (command is JobPolyCommand) return new PolyCommandTreeNode(parent, jobconn, (JobPolyCommand)command);
            else return new JobCommandTreeNode(parent, jobconn, command);
        }
    }

    public class JobCommandTreeNode : TreeNodeBase
    {
        JobCommand m_command;
        JobConnection m_jobconn;
        public JobCommandTreeNode(PolyCommandTreeNode parent, JobConnection jobconn, JobCommand command)
            : base(parent, command.GroupId)
        {
            m_command = command;
            m_jobconn = jobconn;
            SetAppObject(new JobCommandAppObject { JobFile = jobconn.FileName, CommandGroupId = command.GroupId });
        }

        public new PolyCommandTreeNode Parent { get { return (PolyCommandTreeNode)base.Parent; } }
        public JobConnection JobConn { get { return m_jobconn; } }

        public override System.Drawing.Bitmap Image
        {
            get { return m_command.Image; }
        }

        public override string Title
        {
            get { return m_command.ToString(); }
        }

        public override string TypeTitle
        {
            get { return "s_command"; }
        }

        public override ITreeNode[] GetChildren()
        {
            return new ITreeNode[] { };
        }

        public override bool DoubleClick()
        {
            Edit();
            return true;
        }

        [PopupMenu("s_edit", ImageName = CoreIcons.designName)]
        public void Edit()
        {
            m_jobconn.Reload();
            m_command = m_jobconn.GetCommand(m_command.GroupId);
            m_command.Edit(m_jobconn);
        }

        public JobCommand Command { get { return m_command; } }

        public override bool AllowDelete()
        {
            return true;
        }

        public override bool DoDelete()
        {
            if (MessageBox.Show(Texts.Get("s_really_delete$command", "command", m_command), VersionInfo.ProgramTitle, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                Parent.JobConn.Reload();
                Parent.Command.m_commands.RemoveIf(c => c.GroupId == m_command.GroupId);
                Parent.JobConn.SaveToFile();
                return true;
            }
            return false;
        }

        //public override bool AllowDragDrop(ITreeNode draggingNode)
        //{
        //    return draggingNode is JobCommandTreeNode;
        //}

        internal int IndexInParent
        {
            get
            {
                return Parent.Command.m_commands.IndexOfIf(c => c.GroupId == m_command.GroupId);
            }
        }

        [DragDropOperationVisible(Name = "move_before")]
        [DragDropOperationVisible(Name = "move_after")]
        [DragDropOperationVisible(Name = "copy_before")]
        [DragDropOperationVisible(Name = "copy_after")]
        public bool DragDropVisible(AppObject appobj)
        {
            return appobj is JobCommandAppObject;
        }

        [DragDropOperation(Name = "move_before", Title = "s_move_before")]
        public void MoveBefore(AppObject appobj)
        {
            CopyCommand(appobj, 0, true);
        }

        [DragDropOperation(Name = "move_after", Title = "s_move_after")]
        public void MoveAfter(AppObject appobj)
        {
            CopyCommand(appobj, 1, true);
        }

        [DragDropOperation(Name = "copy_before", Title = "s_copy_before")]
        public void CopyBefore(AppObject appobj)
        {
            CopyCommand(appobj, 0, false);
        }

        [DragDropOperation(Name = "copy_after", Title = "s_copy_after")]
        public void CopyAfter(AppObject appobj)
        {
            CopyCommand(appobj, 1, false);
        }

        [PopupMenu("s_run", ImageName = CoreIcons.runName)]
        public void Run()
        {
            Job job = Job.FromCommand(m_command, new JobProperties());
            if (MessageBox.Show(Texts.Get("s_really_run$job", "job", job), VersionInfo.ProgramTitle, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                job.StartProcess();
            }
        }

        private void CopyCommand(AppObject appobj, int d, bool deleteold)
        {
            var cmdobj = appobj as JobCommandAppObject;
            var cmd = cmdobj.GetCommand();
            int idx = IndexInParent;
            Parent.JobConn.Reload();

            if (idx < 0) Parent.Command.m_commands.Insert(0, cmd.Clone(true));
            else if (idx + d >= Parent.Command.m_commands.Count) Parent.Command.m_commands.Add(cmd.Clone(true));
            else Parent.Command.m_commands.Insert(idx + d, cmd.Clone(true));
            Parent.JobConn.SaveToFile();
            if (deleteold)
            {
                var parconn = new JobConnection(cmdobj.JobFile);
                parconn.GetJob().Root.m_commands.RemoveIf(c => c.GroupId == cmd.GroupId);
                parconn.SaveToFile();
                cmdobj.CallCompleteChanged();
            }
            Parent.CompleteRefresh();
        }
    }

    public class PolyCommandTreeNode : LateLoadNoConnTreeNode
    {
        JobPolyCommand m_command;
        JobConnection m_jobconn;
        public PolyCommandTreeNode(ITreeNode parent, JobConnection jobconn, JobPolyCommand command)
            : base(parent, command.GroupId)
        {
            m_command = command;
            m_jobconn = jobconn;
        }
        public PolyCommandTreeNode(ITreeNode parent, JobConnection jobconn)
            : base(parent, "commands")
        {
            m_jobconn = jobconn;
        }

        public JobPolyCommand Command { get { return m_command; } }
        public JobConnection JobConn { get { return m_jobconn; } }
        protected override void DoGetChildren()
        {
            if (m_command == null)
            {
                m_command = m_jobconn.GetJob().Root;
            }
            m_children = (from cmd in m_command.m_commands select JobTreeNode.GetCommandTreeNode(this, cmd, m_jobconn)).ToArray();
        }

        public override string Title
        {
            get { return "s_commands"; }
        }

        public override string TypeTitle
        {
            get { return "s_commands"; }
        }

        public override System.Drawing.Bitmap Image
        {
            get { return CoreIcons.job; }
        }

        //public override bool AllowDragDrop(ITreeNode draggingNode)
        //{
        //    return draggingNode is JobCommandTreeNode;
        //}

        [DragDropOperationVisible(Name = "copy_first")]
        [DragDropOperationVisible(Name = "copy_last")]
        public bool CopyDragDropVisible(AppObject appobj)
        {
            return appobj is JobCommandAppObject;
        }

        [DragDropOperation(Name = "copy_first", Title = "s_copy_first")]
        public void CopyFirst(AppObject appobj)
        {
            var n = appobj as JobCommandAppObject;
            m_command.m_commands.Insert(0, n.GetCommand().Clone(true));
            JobConn.SaveToFile();
            this.CompleteRefresh();
        }

        [DragDropOperation(Name = "copy_last", Title = "s_copy_last")]
        public void CopyLast(AppObject appobj)
        {
            var n = appobj as JobCommandAppObject;
            m_command.m_commands.Add(n.GetCommand().Clone(true));
            JobConn.SaveToFile();
            this.CompleteRefresh();
        }

        public override void DataRefresh()
        {
            base.DataRefresh();
            m_jobconn.Reload();
            m_command = (JobPolyCommand)m_jobconn.GetCommand(m_command.GroupId);
        }
    }
}
