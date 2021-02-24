using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.Windows.Forms;

namespace Plugin.querytool
{
    [MenuExtender(Name = "querytool")]
    public class DataSynMenuExtenders : MenuExtenderBase
    {
        public override void GetToolbarItems(string toolbarName, List<ToolStripItem> items)
        {
            if (toolbarName == "query" && LicenseTool.FeatureAllowed(QueryHistoryFeature.Test))
            {
                var btn = new ToolStripButton(Texts.Get("s_query_history"), CoreIcons.history);
                btn.Click += new EventHandler(btn_Click);
                btn.DisplayStyle = ToolStripItemDisplayStyle.Image;
                items.Add(btn);
            }
        }

        void btn_Click(object sender, EventArgs e)
        {
            MainWindow.Instance.ShowDocker(new QueryHistoryDockerFactory());
        }
    }
}
