using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;

namespace Plugin.apps
{
    [TreeExtender(Name = "app")]
    public class AppTreeExtender : TreeExtenderBase
    {
        private bool CanBeUsed(ITreeNode node)
        {
            return node is IDatabaseTreeNode;
        }

        public override void GetExtendedWidgets(ITreeNode node, List<IWidget> objviews)
        {
            base.GetExtendedWidgets(node, objviews);
            if (node is IDatabaseTreeNode)
            {
                IDatabaseSource dbconn = ((IDatabaseTreeNode)node).DatabaseConnection;
                foreach (var app in Application.GetAppsForDb(dbconn))
                {
                    foreach (var pg in app.Root.Pages) objviews.Add(new AppPageObjectView(pg));
                }
            }
            if (node is AppInstanceTreeNode)
            {
                var anode = (AppInstanceTreeNode)node;
                foreach (var pg in anode.m_tpl.Pages) objviews.Add(new AppPageObjectView(pg));
            }
        }

        public override void GetExtendedChildren(ITreeNode parent, List<ITreeNode> children)
        {
            if (!CanBeUsed(parent)) return;
            IDatabaseSource dbconn = ((IDatabaseTreeNode)parent).DatabaseConnection;
            foreach (var app in Application.GetAppsForDb(dbconn))
            {
                app.Root.GetInstanceNodes(parent, children);
            }
        }

        public override System.Drawing.Bitmap GetImageOverride(ITreeNode node)
        {
            if (node is IDatabaseTreeNode)
            {
                IDatabaseSource dbconn = ((IDatabaseTreeNode)node).DatabaseConnection;
                foreach (var app in Application.GetAppsForDb(dbconn))
                {
                    if (app.Root.m_icon != null) return app.Root.m_icon;
                }
            }
            return base.GetImageOverride(node);
        }
    }
}
