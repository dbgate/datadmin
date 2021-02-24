using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace DatAdmin
{
    public static class ObjectClipboard
    {
        static AppObject[] m_memory;

        public static AppObject[] Memory
        {
            get { return m_memory; }
            set
            {
                m_memory = value;
                HObjectClipboard.CallChanged();
            }
        }

        public static string GetClipboardText()
        {
            string text = ObjectClipboard.Memory[0].ToString();
            if (ObjectClipboard.Memory.Length > 1) text += String.Format("... {0}", Texts.Get("s_more"));
            return text;
        }

        public static Bitmap GetClipboardImage()
        {
            if (Memory != null && Memory.Length > 0) return Memory[0].Image;
            return null;
        }

        public static void EnableAndFillPasteButton(DragDropBuilder bld, ToolStripDropDownButton btnPaste)
        {
            int opcnt = 0;
            if (bld != null) opcnt = bld.OperationCount();
            if (opcnt > 0 && ObjectClipboard.Memory.Length > 0)
            {
                string newtext = String.Format("({0})", opcnt);
                if (btnPaste.Text != newtext) btnPaste.Text = newtext;
                btnPaste.Enabled = true;
                btnPaste.DropDownItems.Clear();

                var lab = new ToolStripLabel();
                btnPaste.DropDownItems.Add(lab);
                lab.Image = GetClipboardImage();
                lab.Text = "(" + GetClipboardText() + ")";
                btnPaste.DropDownItems.Add(new ToolStripSeparator());

                bld.GetMenuItems(btnPaste.DropDownItems);
            }
            else
            {
                btnPaste.Text = "";
                btnPaste.Enabled = false;
            }
        }
    }
}
