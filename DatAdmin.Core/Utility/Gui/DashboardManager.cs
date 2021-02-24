using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DatAdmin
{
    public class DashboardManager : AddonListManager
    {
        public static DashboardManager Instance = new DashboardManager();

        public override AddonType AddonType
        {
            get { return DashboardAddonType.Instance; }
        }

        public DashboardBase[] GetDashboards(AppObject ao)
        {
            var res = new List<DashboardBase>();
            foreach (DashboardBase p in Addons) if (p.SuitableFor(ao)) res.Add(p);
            return res.ToArray();
        }

        public static void RunManageDialog()
        {
            var editor = new DashboardListEditor();
            editor.ShowDialog("s_dashboard_manager");
            Instance.SaveAllItems();
        }

        public DockPanelDashboard FindDashboard(string dashboardFile)
        {
            foreach (DashboardBase p in Addons)
            {
                var das = p as DockPanelDashboard;
                if (das == null) continue;
                if (Path.GetFileName(das.AddonFileName).ToLower() == dashboardFile.ToLower()) return das;
            }
            return null;
        }
    }

    public class DashboardListEditor : AddonListManagerEditor
    {
        public DashboardListEditor()
            : base(DashboardManager.Instance)
        {
        }

        protected override IAddonInstance CreateNewAddon()
        {
            return new DockPanelDashboard();
        }

        public override object GetEditObject(object item)
        {
            return item;
        }
    }
}
