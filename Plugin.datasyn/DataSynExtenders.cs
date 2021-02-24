using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.Windows.Forms;

namespace Plugin.datasyn
{
    [AppObjectExtender(Name = "datasynext")]
    public class DataSynExtenders : AppObjectExtenderBase
    {
        public override void GetAppObjectExtendObjects(AppObject appobj, List<object> objs)
        {
            if (appobj is DatabaseAppObject) objs.Add(new DbObjExtender(appobj));
        }

        public class DbObjExtender : AppObjectExtenderInternalBase
        {
            public DbObjExtender(AppObject appobj) : base(appobj) { }

            [PopupMenu("s_synchronize_data", ImageName = CoreIcons.swapName, Weight = MenuWeights.BACKUP + 1, RequiredFeature = DataSynchronizationFeature.Test, GroupName = "dbmod")]
            public void SynchronizeData()
            {
                DataSynForm.Run(DataSynDef.CreateEmpty(), m_appobj.CreateDatabaseConnection(), null);
            }

            [DragDropOperationVisible(Name = "syndata")]
            public bool DragDropVisible_SynchronizeData(AppObject appobj)
            {
                return appobj is DatabaseAppObject;
            }

            [DragDropOperation(Name = "syndata", Title = "s_synchronize_data", RequiredFeature = DataSynchronizationFeature.Test)]
            public void DragDrop_SynchronizeData(AppObject appobj)
            {
                if (appobj is DatabaseAppObject)
                {
                    try
                    {
                        IDatabaseSource dsource = ((DatabaseAppObject)appobj).FindDatabaseConnection(m_appobj.ConnPack);
                        DataSynForm.Run(DataSynDef.CreateEmpty(), dsource.CloneSource(), m_appobj.CreateDatabaseConnection());
                    }
                    catch (Exception e)
                    {
                        Errors.Report(e);
                    }
                }
            }
        }
    }

    [MenuExtender(Name = "datasynext")]
    public class DataSynMenuExtenders : MenuExtenderBase
    {
        public override void GetMainMenu(string menuName, MenuBuilder mb)
        {
            if (menuName == "tools" && LicenseTool.FeatureAllowed(DataSynchronizationFeature.Test))
            {
                mb.AddItem("s_synchronize_data", ShowDataSynWindow);
            }
        }

        private static void ShowDataSynWindow()
        {
            DataSynForm.Run(DataSynDef.CreateEmpty(), null, null);
        }

        public override void GetToolbarItems(string toolbarName, List<ToolStripItem> items)
        {
            if (toolbarName == "main" && LicenseTool.FeatureAllowed(DataSynchronizationFeature.Test))
            {
                var btn = new ToolStripButton(Texts.Get("s_data_synchronization"), DataSynIcons.sync);
                btn.Click += new EventHandler(btn_Click);
                items.Add(btn);
            }
        }

        void btn_Click(object sender, EventArgs e)
        {
            ShowDataSynWindow();
        }
    }
}
