using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public class CheckedListBoxEx : CheckedListBox
    {
        public event ListBoxExScrollDelegate Scroll;
        // WM_VSCROLL message constants
        private const int WM_VSCROLL = 0x0115;
        private const int SB_THUMBTRACK = 5;
        private const int SB_ENDSCROLL = 8;

        protected override void WndProc(ref Message m)
        {
            // Trap the WM_VSCROLL message to generate the Scroll event
            base.WndProc(ref m);
            if (m.Msg == WM_VSCROLL)
            {
                int nfy = m.WParam.ToInt32() & 0xFFFF;
                if (Scroll != null && (nfy == SB_THUMBTRACK || nfy == SB_ENDSCROLL))
                    Scroll(this, new ListBoxExScrollArgs(this.TopIndex, nfy == SB_THUMBTRACK));
            }
        }
    }

    public delegate void ListBoxExScrollDelegate(object Sender, ListBoxExScrollArgs e);

    public class ListBoxExScrollArgs
    {
        // Scroll event argument
        private int mTop;
        private bool mTracking;
        public ListBoxExScrollArgs(int top, bool tracking)
        {
            mTop = top;
            mTracking = tracking;
        }
        public int Top
        {
            get { return mTop; }
        }
        public bool Tracking
        {
            get { return mTracking; }
        }
    }
}
