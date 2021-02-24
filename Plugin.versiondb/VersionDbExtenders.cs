using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.Windows.Forms;

namespace Plugin.versiondb
{
    [MenuExtender(Name = "versiondbext")]
    public class VersionDbExtenders : MenuExtenderBase
    {
        public override void GetMainMenu(string menuName, MenuBuilder mb)
        {
            if (menuName == "tools" && LicenseTool.FeatureAllowed(VersionedDbFeature.Test))
            {
                mb.AddItem("s_version_db", CreateVersionDbWindow);
            }
        }

        private static void CreateVersionDbWindow()
        {
            CreateDialog dlg = new CreateDialog(null, "files", "versiondb");
            dlg.ShowDialogEx();
            HTree.CallRefreshRoot();
        }

        public override void GetToolbarItems(string toolbarName, List<ToolStripItem> items)
        {
            if (toolbarName == "main" && LicenseTool.FeatureAllowed(VersionedDbFeature.Test))
            {
                var btn = new ToolStripButton(Texts.Get("s_version_db"), CoreIcons.versiondb);
                btn.Click += new EventHandler(btn_Click);
                items.Add(btn);
            }
        }

        void btn_Click(object sender, EventArgs e)
        {
            CreateVersionDbWindow();
        }
    }
}
