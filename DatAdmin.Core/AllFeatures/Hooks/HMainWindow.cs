using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public static class HMainWindow
    {
        public static event KeyEventHandler KeyDown;
        public static event KeyEventHandler KeyUp;
        public static event KeyPressEventHandler KeyPress;

        public static void CallKeyDown(object sender, KeyEventArgs e)
        {
            if (KeyDown != null) KeyDown(sender, e);
        }
        public static void CallKeyUp(object sender, KeyEventArgs e)
        {
            if (KeyUp!= null) KeyUp(sender, e);
        }
        public static void CallKeyPress(object sender, KeyPressEventArgs e)
        {
            if (KeyPress != null) KeyPress(sender, e);
        }
    }
}
