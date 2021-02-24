using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace DatAdmin
{
    [AppObject(Name = "database", Title = "s_database")]
    public class DatabaseAppObject : DatabaseFieldsAppObject
    {
        public override ObjectFilterBase GetFilter()
        {
            var res = new DatabaseObjectFilter();
            FillDatabaseObjectFilter(res);
            res.ObjectType.PredefinedValue = TypeName;
            return res;
        }

        public override string TypeName
        {
            get { return "database"; }
        }

        public override string TypeTitle
        {
            get { return "s_database"; }
        }

        public override Bitmap Image
        {
            get { return CoreIcons.database; }
        }

        public override ObjectPath GetObjectPath()
        {
            return new ObjectPath(DatabaseName);
        }

        public override void GetWidgets(List<IWidget> res)
        {
            base.GetWidgets(res);
            res.Add(new TablesRawGridWidget());
        }

        public override string GetRenamingText()
        {
            return DatabaseName;
        }

        public override bool NewDashboardVisible()
        {
            return true;
        }

        [PopupMenu("s_import", ImageName = CoreIcons.importName, Weight = MenuWeights.IMPORT, GroupName = "impexp")]
        public void DummyImport() { }

        [PopupMenuVisible("s_query")]
        public bool QueryVisible()
        {
            var dbconn = this.FindDatabaseConnection(ConnPack);
            return dbconn != null && dbconn.DatabaseCaps.ExecuteSql;
        }

        [PopupMenuEnabled("s_query")]
        public bool QueryEnabled()
        {
            var dbconn = this.FindDatabaseConnection(ConnPack);
            return dbconn != null && QueryVisible() && dbconn.Connection.SystemConnection != null && dbconn.Connection.SystemConnection.State == System.Data.ConnectionState.Open;
        }

        [PopupMenu("s_query", ImageName = CoreIcons.sqlName, Weight = MenuWeights.QUERY, GroupName = "sql")]
        public void RunQuery()
        {
            var dbconn = this.FindDatabaseConnection(ConnPack);
            OpenQueryParameters pars = new OpenQueryParameters();
            MainWindow.Instance.OpenContent(new QueryFrame(dbconn.CloneConnection(), pars));
        }

        [PopupMenuVisible("s_properties")]
        public bool AlterDatabaseVisible()
        {
            var dbconn = this.FindDatabaseConnection(ConnPack);
            return dbconn != null && dbconn.Connection != null && dbconn.Connection.IsOpened;
        }

        [PopupMenu("s_properties", ImageName = CoreIcons.designName, Weight = MenuWeights.EDIT)]
        public void AlterDatabase()
        {
            var dbconn = this.FindDatabaseConnection(ConnPack);
            if (!AlterDatabaseVisible() || dbconn == null) return;
            var dbmem = new DatabaseStructureMembers();
            dbmem.DatabaseOptions = true;
            var m_db = dbconn.InvokeLoadStructure(dbmem, null);
            var props = new DatabaseProperties
            {
                Name = dbconn.DatabaseName,
                SpecificData = m_db.SpecificData,
            };
            if (DatabasePropertiesForm.Run(props, dbconn, true))
            {
                var plan = new AlterPlan();
                var opts = new DbDiffOptions();
                plan.ChangeDatabaseOptions(dbconn.DatabaseName, props.SpecificData);
                plan.Transform(dbconn.Dialect.DumperCaps, opts, dbconn);

                string alterSql = dbconn.Dialect.GenerateScript(dmp => plan.CreateRunner().Run(dmp, opts));
                if (SqlConfirmForm.Run(alterSql))
                {
                    dbconn.AlterDatabase(plan, opts);
                }
            }
        }

        [PopupMenuEnabled("s_delete_local_content")]
        public bool DeleteLocalContentEnabled()
        {
            var dbconn = this.FindDatabaseConnection(ConnPack);
            if (dbconn == null) return false;
            string path = dbconn.GetPrivateFolder();
            return path != null && Directory.Exists(path);
        }

        [PopupMenu("s_delete_local_content", ImageName = CoreIcons.recycle_binName, Weight = MenuWeights.DELETE)]
        public void DeleteLocalContent()
        {
            var dbconn = this.FindDatabaseConnection(ConnPack);
            string path = dbconn.GetPrivateFolder();
            if (path != null && StdDialog.YesNoDialog("s_really_delete_local_content$path", "path", path))
            {
                Directory.Delete(path, true);
                CallCompleteChanged();
            }
        }

        [PopupMenu("s_export", ImageName = CoreIcons.exportName, Weight = MenuWeights.EXPORT, GroupName = "impexp")]
        public void RunExport()
        {
            var dbconn = this.FindDatabaseConnection(ConnPack);
            CopyDbWizard.RunExport(dbconn.CloneSource());
        }

        [PopupMenuEnabled("s_import/SQL Dump")]
        public bool RunImportSqlDumpEnabled()
        {
            var dbconn = this.FindDatabaseConnection(ConnPack);
            return dbconn != null && dbconn.DatabaseCaps.ExecuteSql;
        }

        [PopupMenu("s_import/SQL Dump", Weight = 0)]
        public void RunImportSqlDump()
        {
            var dbconn = this.FindDatabaseConnection(ConnPack);
            ImportSqlDumpForm.Run(dbconn.CloneSource());
        }

        //[PopupMenuVisible("s_backup")]
        //public bool BackupVisible()
        //{
        //    return m_conn.Connection != null && m_conn.Connection.SystemConnection != null;
        //}

        [PopupMenuVisible("s_backup")]
        public bool BackupVisible()
        {
            return LicenseTool.FeatureAllowed(DatabaseBackupFeature.Test) && this.FindPhysicalConnection().GetAnyDialect().DialectCaps.SupportBackup;
        }

        [PopupMenu("s_backup", ImageName = CoreIcons.backupName, Weight = MenuWeights.BACKUP, GroupName = "impexp")]
        public void BackupDatabase()
        {
            var dbconn = this.FindDatabaseConnection(ConnPack);
            BackupDbForm.Run(dbconn.CloneSource());
        }

        [PopupMenu("s_import/s_data_archive", Weight = 1)]
        public void RunImportDataArchive()
        {
            var dbconn = this.FindDatabaseConnection(ConnPack);
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = String.Format("{0} (*.dbk)|*.dbk", Texts.Get("s_data_archives"));
            if (dlg.ShowDialogEx() == DialogResult.OK)
            {
                DataArchiveReader dar = new DataArchiveReader(dlg.FileName);
                CopyDbWizard.Run(dar, dbconn.CloneSource());
            }
        }

        //public void GetPopupMenu(MenuBuilder menu)
        //{
        //    menu.AddObject(this);
        //}

        [DragDropOperationVisible(Name = "copydb")]
        public bool DragDrop_CopyDatabaseVisible(AppObject appobj)
        {
            var dbconn = this.FindDatabaseConnection(ConnPack);
            return dbconn != null && !dbconn.DatabaseCaps.ReadOnly && appobj is DatabaseAppObject;
        }

        [DragDropOperation(Name = "copydb", Title = "s_copy_database")]
        public void DragDrop_CopyDatabase(AppObject appobj)
        {
            var dbconn = this.FindDatabaseConnection(ConnPack);
            if (appobj is DatabaseAppObject)
            {
                try
                {
                    IDatabaseSource dsource = ((DatabaseAppObject)appobj).FindDatabaseConnection(ConnPack);
                    CopyDbWizard.Run(dsource.CloneSource(), dbconn.CloneSource());
                }
                catch (Exception e)
                {
                    Errors.Report(e);
                }
            }
        }

        [PopupMenuEnabled("s_settings")]
        public bool DatabaseSettingsEnabled()
        {
            var dbconn = this.FindDatabaseConnection();
            return dbconn.Connection != null
                && dbconn.Connection.StoredConnection != null
                && dbconn.Connection.StoredConnection.GetDatabaseSettings(DatabaseName) != null;
        }
        [PopupMenu("s_settings", Weight = MenuWeights.SETTINGS, ImageName = CoreIcons.settingsName)]
        public void DatabaseSettings()
        {
            var dbconn = this.FindDatabaseConnection();
            SettingsForm.Run(dbconn.Connection.StoredConnection.GetDatabaseSettings(DatabaseName), SettingsTargets.Database);
        }

        public SettingsPageCollection GetDatabaseSettings()
        {
            var dbconn = this.FindDatabaseConnection();
            if (dbconn == null || dbconn.Connection == null || dbconn.Connection.StoredConnection == null) return null;
            return dbconn.Connection.StoredConnection.GetDatabaseSettings(DatabaseName);
        }

        public override string ToString()
        {
            if (String.IsNullOrEmpty(DatabaseName))
            {
                if (Connection != null) return Connection.GetDataSource();
                return "(database)";
            }
            return DatabaseName;
        }

        //public bool AllowDragDrop(ITreeNode draggingNode)
        //{
        //    if (draggingNode is IDatabaseTreeNode) return true;
        //    return false;
        //}

        public override string GetTreePath()
        {
            return GetDatabaseTreePath();
        }

        public override string GetFileFriendlySignature()
        {
            return IOTool.CreateFileFriendlyName("database" + Connection.GetConnectionKey() + "_" + DatabaseName);
        }

        [DragDropOperationVisible(Name = "copytable")]
        public bool DragDropVisible_CopyTable(AppObject appobj)
        {
            return appobj is TableAppObject;
        }

        [DragDropOperation(Name = "copytable", Title = "s_copy_table")]
        public void DragDrop_CopyTable(AppObject appobj)
        {
            //var tobj = appobj as TableAppObject;
            //if (tobj == null) return;
            var conn = this.FindDatabaseConnection();
            ITableSource tsource = appobj.TableSource;
            TableStructure tbl = new TableStructure(tsource.InvokeLoadStructure(TableStructureMembers.AllNoRefs));
            TableCopyOptions opts;
            bool copydata;
            string newname = CopyTableForm.Run(tsource, conn, out copydata, out opts);
            if (newname == null) return;
            List<NameWithSchema> names = new List<NameWithSchema>(conn.InvokeLoadFullTableNames());
            if (ArrayTool.Contains(names, new NameWithSchema(newname)))
            {
                StdDialog.ShowError(Texts.Get("s_table_allready_exists"));
                return;
            }
            tbl.FullName = new NameWithSchema(newname);
            if (conn.Dialect != null) conn.Dialect.MigrateTable(tbl, conn.Dialect.CreateMigrationProfile(), null);
            tbl.RemoveConstraints<IForeignKey>();
            PrimaryKey pk = (PrimaryKey)TableStructureExtension.FindConstraint<IPrimaryKey>(tbl);
            if (pk != null && pk.Name != null && pk.Name.ToUpper().StartsWith("PK_")) pk.Name = "PK_" + tbl.Name;
            try
            {
                ITableSource dst = conn.CreateTable(tbl);
                if (copydata)
                {
                    BulkCopyJob.Create(tsource.GetDataStoreAndClone(), dst.GetDataStoreAndClone(), new IdentityTransform(tbl), opts, null).StartProcess();
                }
            }
            catch (Exception e)
            {
                Errors.Report(e);
            }
        }

        public override bool AllowDelete()
        {
            var dialect = this.FindPhysicalConnection().GetAnyDialect();
            var dbconn = this.FindDatabaseConnection();
            return !String.IsNullOrEmpty(DatabaseName) && dialect.DumperCaps.DropDatabase && dbconn.DatabaseCaps.ExecuteSql;
        }

        public override void DoDelete()
        {
            this.FindDatabaseConnection().InvokeScript(fmt => { fmt.DropDatabase(DatabaseName); }, null, true);
        }

        public override bool AllowRename()
        {
            var dialect = this.FindPhysicalConnection().GetAnyDialect();
            return !String.IsNullOrEmpty(DatabaseName) && dialect.DumperCaps.RenameDatabase;
        }

        public override void RenameObject(string newname)
        {
            this.FindDatabaseConnection().InvokeScript(fmt => { fmt.RenameDatabase(DatabaseName, newname); }, null, true);
        }

        public override void ModifiedDoubleClick(Keys keys)
        {
            base.ModifiedDoubleClick(keys);
            if (keys == Keys.Shift) RunQuery();
        }
    }
}
