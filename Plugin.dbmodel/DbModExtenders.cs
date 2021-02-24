using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;

namespace Plugin.dbmodel
{
    [TreeExtender(Name = "dbmodext")]
    public class DbModTreeExtenders : TreeExtenderBase
    {
        public override void GetNodeExtendObjects(ITreeNode node, List<object> objs)
        {
            if (node is VirtualFolderTreeNode && ((VirtualFolderTreeNode)node).AllowGenericFolderExtenders()) objs.Add(new FolderNodeExtender(node));
            //if (node is IDatabaseTreeNode) objs.Add(new DbNodeExtender(node));
        }

        public class FolderNodeExtender : NodeExtenderBase
        {
            public FolderNodeExtender(ITreeNode node) : base(node) { }

            [DragDropOperationVisible(Name = "save_structure")]
            public bool DragDropVisible_SaveStructure(AppObject appobj)
            {
                return appobj is DatabaseAppObject && Node.FileSystemPath != null;
            }

            [DragDropOperation(Name = "save_structure", Title = "s_save_structure")]
            public void DragDrop_SaveStructure(AppObject appobj)
            {
                var dobj = appobj as DatabaseAppObject;
                if (dobj != null)
                {
                    string dbname = dobj.DatabaseName;
                    string fn = System.IO.Path.Combine(Node.FileSystemPath, (dbname ?? "") + ".ddf");
                    CopyDbWizard.Run(dobj.FindDatabaseConnection(Node.ConnPack).CloneSource(), new DatabaseStructureWriter { FilePlace = FilePlaceAddonType.PlaceFromVirtualFile(fn) });
                }
            }
        }
    }

    [AppObjectExtender(Name = "dbmodext")]
    public class DbModAppObjectExtenders : AppObjectExtenderBase
    {
        public override void GetAppObjectExtendObjects(AppObject appobj, List<object> objs)
        {
            if (appobj is DatabaseAppObject) objs.Add(new DbObjExtender(appobj));
        }

        public class DbObjExtender : AppObjectExtenderInternalBase
        {
            public DbObjExtender(AppObject appobj) : base(appobj) { }

            [PopupMenu("s_synchronize_structure", ImageName = CoreIcons.swapName, Weight = MenuWeights.BACKUP + 1, RequiredFeature = DbStructSynchronizationFeature.Test, GroupName = "dbmod")]
            public void SynchronizeStructure()
            {
                SynchronizeStructureForm.Run(m_appobj.CreateDatabaseConnection(), null);
            }

            [PopupMenu("s_find_and_replace", ImageName = CoreIcons.findName, Weight = MenuWeights.BACKUP + 2, GroupName = "dbmod")]
            public void FindAndReplace()
            {
                var win = new DbModelFindReplaceFrame(m_appobj.CreateDatabaseConnection());
                MainWindow.Instance.OpenContent(win);
            }

            [PopupMenu("s_dependency_browser", ImageName = CoreIcons.dependencyName, Weight = MenuWeights.BACKUP + 3, RequiredFeature = DependencyBrowserFeature.Test, GroupName = "dbmod")]
            public void DependencyBrowser()
            {
                var win = new DependencyBrowserFrame(m_appobj.CreateDatabaseConnection());
                MainWindow.Instance.OpenContent(win);
            }

            [DragDropOperationVisible(Name = "synstruct")]
            public bool DragDropVisible_SynchronizeStructure(AppObject appobj)
            {
                return appobj is DatabaseAppObject;
            }

            [DragDropOperation(Name = "synstruct", Title = "s_synchronize_structure", RequiredFeature = DbStructSynchronizationFeature.Test)]
            public void DragDrop_SynchronizeStructure(AppObject appobj)
            {
                if (appobj is DatabaseAppObject)
                {
                    try
                    {
                        IDatabaseSource dsource = ((DatabaseAppObject)appobj).FindDatabaseConnection(m_appobj.ConnPack);
                        SynchronizeStructureForm.Run(dsource.CloneSource(), m_appobj.CreateDatabaseConnection());
                    }
                    catch (Exception e)
                    {
                        Errors.Report(e);
                    }
                }
            }
        }

    }

    [MenuExtender(Name = "dbmodext")]
    public class DbModeMenuExtenders : MenuExtenderBase
    {
        public override void GetMainMenu(string menuName, MenuBuilder mb)
        {
            if (menuName == "tools" && LicenseTool.FeatureAllowed(DbStructSynchronizationFeature.Test))
            {
                mb.AddItem("s_synchronize_structure", SynchronizeStructureForm.RunNoParam);
            }
        }
    }
}
