using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public class Backups_TreeNode : VirtualFolderTreeNode
    {
        internal IDatabaseSource m_conn;
        public Backups_TreeNode(IDatabaseSource conn, ITreeNode parent)
            : base(parent, new DiskFolder(conn.GetPrivateSubFolder("backups")), "backups")
        {
            m_conn = conn;
        }

        public override System.Drawing.Bitmap Image { get { return CoreIcons.dbbackup; } }
        public override System.Drawing.Bitmap ExpandedImage { get { return CoreIcons.dbbackup; } }

        public override bool AllowCreate(string group, string name)
        {
            return false;
        }

        public override string Title
        {
            get { return Texts.Get("s_backups"); }
        }

        [PopupMenu("s_new_backup", Weight = MenuWeights.NEWOBJECT, ImageName = CoreIcons.backupName)]
        public void NewBackup()
        {
            BackupDbForm.Run(m_conn.CloneSource());
        }
    }

    //public class BackupsOffline_TreeNode : FolderTreeNode
    //{
    //    public BackupsOffline_TreeNode(ITreeNode parent, string filepath)
    //        : base(parent, filepath)
    //    {
    //    }

    //    public override System.Drawing.Bitmap Image { get { return CoreIcons.dataarchive; } }
    //    public override System.Drawing.Bitmap ExpandedImage { get { return CoreIcons.dataarchive; } }

    //    public override bool AllowCreate(string group, string name)
    //    {
    //        return false;
    //    }

    //    public override string Title
    //    {
    //        get { return Texts.Get("s_backups"); }
    //    }
    //}

    public class Backup_TreeNode : VirtualFileTreeNodeBase
    {
        internal IDatabaseSource m_conn;
        BackupContainer m_backup;
        public Backup_TreeNode(ITreeNode parent, IFileHandler fhandler)
            : base(parent, fhandler)
        {
            m_backup = new BackupContainer(m_file.DataDiskPath);
            if (parent is Backups_TreeNode)
            {
                m_conn = ((Backups_TreeNode)parent).m_conn;
            }
        }

        public override ITreeNode[] GetChildren()
        {
            return m_backup.GetChildTreeNodes(this);
        }

        public override string TypeTitle
        {
            get { return "s_backup"; }
        }

        [PopupMenuEnabled("s_restore")]
        public bool RestoreEnabled()
        {
            return m_conn != null;
        }

        [PopupMenu("s_restore", ImageName = CoreIcons.restoreName, Weight = MenuWeights.IMPORT)]
        public void Restore()
        {
            try
            {
                m_backup.Format.CheckRestoreConfiguration(m_conn);
                BackupContainer backup = new BackupContainer(m_file.DataDiskPath);
                RestoreDbForm.Run(m_conn.CloneSource(), backup);
            }
            catch (Exception err)
            {
                Errors.Report(err);
            }
        }

        //[PopupMenuEnabled("s_restore/s_restore_now")]
        //public bool RestoreNowEnabled()
        //{
        //    return m_conn != null;
        //}

        //[PopupMenu("s_restore/s_restore_now", ImageName=CoreIcons.restoreName)]
        //public void RestoreNow()
        //{
        //    try
        //    {
        //        BackupContainer backup = new BackupContainer(m_filepath);
        //        backup.Format.CheckRestoreConfiguration(m_conn);
        //        if (MessageBox.Show("s_really_restore_database", "DatAdmin", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
        //        {
        //            RestoreDbJob.CreateJob(backup, m_conn, new JobProperties()).StartProcess();
        //        }
        //    }
        //    catch (Exception err)
        //    {
        //        Errors.Report(err);
        //    }
        //}

        //[PopupMenu("s_restore/s_create_job", ImageName = CoreIcons.jobName, Weight = MenuWeights.NEWOBJECT)]
        //public void RestoreCreateJob()
        //{
        //    try
        //    {
        //        m_backup.Format.CheckRestoreConfiguration(m_conn);
        //        Job.AskAndExportToFile(() => RestoreDbJob.CreateJob(m_backup, m_conn, new JobProperties()));
        //    }
        //    catch (Exception err)
        //    {
        //        Errors.Report(err);
        //    }
        //}

        public override bool DoubleClick()
        {
            Restore();
            return true;
        }

        public override System.Drawing.Bitmap Image { get { return CoreIcons.dbbackup; } }
    }

    [FileHandler(Name = "backup")]
    public class BackupFileHandler : FileHandlerBase
    {
        public override string Extension
        {
            get { return "bck"; }
        }

        public override ITreeNode CreateNode(ITreeNode parent)
        {
            return new Backup_TreeNode(parent, this);
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

    //[NodeFactory(Name = "backup")]
    //public class BackupNodeFactory : NodeFactoryBase
    //{
    //    public override ITreeNode FromFile(ITreeNode parent, string file)
    //    {
    //        string fn = file.ToLower();
    //        if (fn.EndsWith(".bck"))
    //        {
    //            try
    //            {
    //                return new Backup_TreeNode(parent, file);
    //            }
    //            catch (Exception)
    //            {
    //                return null;
    //            }
    //        }
    //        return null;
    //    }
    //}
}
