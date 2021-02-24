using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace DatAdmin
{
    public static class MainWindowExtension
    {
        public static IPhysicalConnection SelectedConnection
        {
            get { return HTree.CallGetSelectedNode().GetAnyConnection(); }
        }

        public static IDatabaseTreeNode SelectedDatabaseNode
        {
            get { return HTree.CallGetSelectedNode().GetDatabaseNode(); }
        }

        public static string SelectedDatabaseName
        {
            get { return HTree.CallGetSelectedNode().GetDatabaseName(); }
        }

        public static bool HasContent(this IMainWindow window, string winid)
        {
            foreach (var cnt in window.GetContents()) if (cnt.WinId == winid) return true;
            return false;
        }

        public static void ActivateContent(this IMainWindow window, string winid)
        {
            foreach (var cnt in window.GetContents())
            {
                if (cnt.WinId == winid)
                {
                    window.ActivateContent(cnt);
                }
            }
        }
    }
}
