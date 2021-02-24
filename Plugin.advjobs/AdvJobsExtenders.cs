using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;

namespace Plugin.advjobs
{
    internal static class JobNodeTool
    {
        internal static string RelativeFile(this ITreeNode node)
        {
            var jnode = node as JobTreeNode;
            if (jnode != null)
            {
                string file = jnode.FileSystemPath;
                string relfile = IOTool.RelativePathTo(Core.JobsDirectory, file);
                return relfile;
            }
            return null;
        }
    }

    [TreeExtender(Name = "advjobsext")]
    public class AdvJobsExtenders : TreeExtenderBase
    {
        public override void GetNodeExtendObjects(ITreeNode node, List<object> objs)
        {
            if (node is JobTreeNode) objs.Add(new JobNodeExtender(node));
        }

        public override System.Drawing.Bitmap GetImageOverride(ITreeNode node)
        {
            if (!LicenseTool.FeatureAllowed(AdvancedJobsFeature.Test)) return null;
            var jnode = node as JobTreeNode;
            if (jnode != null)
            {
                string file = jnode.FileSystemPath;
                if (JobPlanner.Instance.Connection.JobFileScheduled(file)) return StdIcons.clock;
                return null;
            }
            return null;
        }

        public class JobNodeExtender : NodeExtenderBase
        {
            public JobNodeExtender(ITreeNode node) : base(node) { }

            [PopupMenu("s_schedule", ImageName = CoreIcons.clockName, Weight = MenuWeights.BACKUP + 1, RequiredFeature = AdvancedJobsFeature.Test)]
            public void Schedule()
            {
                var jnode = Node as JobTreeNode;
                string file = jnode.FileSystemPath;
                JobPlanner.Instance.Connection.ScheduleJobDialog(file);
                TreeNodeBase.CallRefresh(Node);
            }

            [PopupMenu("s_report", ImageName = CoreIcons.reportName, Weight = MenuWeights.BACKUP + 2, RequiredFeature = AdvancedJobsFeature.Test)]
            public void Report()
            {
                var jnode = Node as JobTreeNode;
                string file = jnode.FileSystemPath;
                MainWindow.Instance.OpenContent(new JobReportFrame(file));
            }

            [PopupMenuEnabled("s_delete_schedule")]
            public bool DeleteScheduleEnabled()
            {
                var jnode = Node as JobTreeNode;
                string file = jnode.FileSystemPath;
                return JobPlanner.Instance.Connection.JobFileScheduled(file);
            }

            [PopupMenu("s_delete_schedule", ImageName = ImageTool.COMBI_PREFIX + CoreIcons.clockName + ImageTool.COMBI_SEPARATOR + CoreIcons.delete_overlayName, Weight = MenuWeights.BACKUP + 2, RequiredFeature = AdvancedJobsFeature.Test)]
            public void DeleteSchedule()
            {
                var jnode = Node as JobTreeNode;
                string file = jnode.FileSystemPath;
                JobPlanner.Instance.Connection.DeleteSchedule(file);
                jnode.CallRefresh();
            }
        }
    }
}
