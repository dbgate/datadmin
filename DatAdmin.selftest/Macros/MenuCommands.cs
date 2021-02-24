using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin.selftest
{
    public static class MenuCommands
    {
        public static void Quit(MainForm win){
            var menu1 = (ToolStripMenuItem)win.MainMenuStrip.Items["mnuFile"];
            var menu9 = menu1.DropDownItems["squitToolStripMenuItem"];
            menu9.PerformClick();
            Application.DoEvents();
        }
    }
}
