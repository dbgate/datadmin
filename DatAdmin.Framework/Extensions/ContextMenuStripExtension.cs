using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Forms;

namespace DatAdmin
{
    public static class ContextMenuStripExtension
    {
        //[DllImport("user32.dll")]
        //static extern bool GetCursorPos(ref Point lpPoint);

        public static void ShowOnCursor(this ContextMenuStrip menu)
        {
            //Point pt = new Point();
            //GetCursorPos(ref pt);
            Point pt = Cursor.Position;
            menu.Show(pt);
        }
    }
}
