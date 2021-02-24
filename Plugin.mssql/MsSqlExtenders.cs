using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;

namespace Plugin.mssql
{
    [AppObjectExtender(Name = "mssqlext", RequiredFeature = MsSqlBackupFeature.Test)]
    public class MsAppObjSqlExtenders : AppObjectExtenderBase
    {
        public override void GetAppObjectExtendObjects(AppObject appobj, List<object> objs)
        {
            if (appobj is DatabaseAppObject)
            {
                objs.Add(new NativeBackupBrowser(appobj));
                objs.Add(new NativeBackupMenu(appobj));
            }
            if (appobj is ServerAppObject)
            {
                objs.Add(new NativeBackupBrowser(appobj));
            }
        }

        public class NativeBackupBrowser : AppObjectExtenderInternalBase
        {
            public NativeBackupBrowser(AppObject appobj) : base(appobj) { }

            [PopupMenuVisible("MS SQL Native Backup Browser")]
            public bool BackupBrowserVisible()
            {
                var dbconn = m_appobj.FindPhysicalConnection();
                return dbconn != null
                    && dbconn.Dialect != null
                    && dbconn.SystemConnection != null
                    && dbconn.Dialect.DialectName == "mssql";
            }

            [PopupMenu("MS SQL Native Backup Browser", ImageName = CoreIcons.restoreName, Weight = MenuWeights.BACKUP, GroupName = "impexp")]
            public void BackupBrowser()
            {
                var dbconn = m_appobj.FindPhysicalConnection();
                MainWindow.Instance.OpenContent(new MsSqlBackupsFrame(m_appobj.CreatePhysicalConnection()));
            }
        }

        public class NativeBackupMenu : AppObjectExtenderInternalBase
        {
            public NativeBackupMenu(AppObject appobj) : base(appobj) { }

            [PopupMenuVisible("Backup - MS SQL Native")]
            public bool SystemBackupVisible()
            {
                var dbconn = m_appobj.FindDatabaseConnection();
                return dbconn != null
                    && dbconn.Dialect != null
                    && dbconn.Dialect.DialectName == "mssql"
                    && dbconn.Connection != null
                    && dbconn.Connection.SystemConnection != null;
            }

            [PopupMenu("Backup - MS SQL Native", ImageName = CoreIcons.backupName, Weight = MenuWeights.BACKUP, GroupName = "impexp")]
            public void SystemBackup()
            {
                var dbconn = m_appobj.FindDatabaseConnection();
                var sconn = dbconn.Connection.SystemConnection;
                string bakdir = sconn.ExecuteScalar(@"
                    declare @BackupDirectory nvarchar(512);
                    exec master.dbo.xp_instance_regread N'HKEY_LOCAL_MACHINE', N'Software\Microsoft\MSSQLServer\MSSQLServer', N'BackupDirectory', @BackupDirectory OUTPUT;
                    select @BackupDirectory AS [BackupDirectory]").ToString();
                MsSqlBackupDialog.Run(bakdir, dbconn.CloneSource());
            }
        }
    }

    [TreeExtender(Name = "mssqlext", RequiredFeature = MsSqlBackupFeature.Test)]
    public class MsAppNodeExtenders : TreeExtenderBase
    {
        public override void GetNodeExtendObjects(ITreeNode node, List<object> objs)
        {
            if (node is DatabasesTreeNode)
            {
                objs.Add(new DbsNodeExtender(node));
            }
        }

        public class DbsNodeExtender : NodeExtenderBase
        {
            public DbsNodeExtender(ITreeNode node) : base(node) { }

            [PopupMenuVisible("Restore - MS SQL Native")]
            public bool SystemRestoreVisible()
            {
                var dnode = (DatabasesTreeNode)Node;
                var conn = dnode.GetConnection();
                return conn != null
                    && conn.Dialect != null
                    && conn.SystemConnection != null
                    && conn.Dialect.DialectName == "mssql";
            }

            [PopupMenu("Restore - MS SQL Native", ImageName = CoreIcons.restoreName, Weight = MenuWeights.BACKUP, GroupName = "impexp")]
            public void SystemRestore()
            {
                MsSqlRestoreDialog.Run(Node.GetConnection().Clone());
            }
        }
    }
}
