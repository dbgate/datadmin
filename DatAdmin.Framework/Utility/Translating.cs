using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Globalization;
using System.ComponentModel;
using BrightIdeasSoftware;

namespace DatAdmin
{
    public interface ITooltipHolder
    {
        ToolTip GetToolTip();
    }

    public static class Translating
    {
        public static void TranslateControl(Control ctrl)
        {
            TranslateControl(ctrl, null);
        }

        public static void TranslateControl(Control ctrl, ToolTip tooltip)
        {
            if (ctrl is ITooltipHolder) tooltip = ((ITooltipHolder)ctrl).GetToolTip();

            if (tooltip != null)
            {
                string tdata = tooltip.GetToolTip(ctrl);
                if (!tdata.IsEmpty()) tooltip.SetToolTip(ctrl, TranslateText(tdata));
            }

            if (ShouldTranslate(ctrl.Text)) ctrl.Text = TranslateText(ctrl.Text);

            if (ctrl is ListView)
            {
                ListView obj = (ListView)ctrl;
                foreach (ColumnHeader hdr in obj.Columns)
                {
                    if (ShouldTranslate(hdr.Text)) hdr.Text = TranslateText(hdr.Text);
                    var ocol = hdr as OLVColumn;
                    if (ocol != null) ocol.ToolTipText = TranslateText(ocol.ToolTipText);
                }
            }

            if (ctrl is DataGridView)
            {
                DataGridView obj = (DataGridView)ctrl;
                foreach (DataGridViewColumn hdr in obj.Columns)
                {
                    try
                    {
                        if (ShouldTranslate(hdr.HeaderText)) hdr.HeaderText = TranslateText(hdr.HeaderText);
                    }
                        // zaplata, hazi nesmyslny vyjimky, ale dela, co ma ...
                    catch (Exception) { }
                }
            }

            if (ctrl.ContextMenuStrip != null)
            {
                foreach (ToolStripItem item in ctrl.ContextMenuStrip.Items)
                {
                    TranslateToolStrip(item);
                }
            }

            if (ctrl is Form)
            {
                Form obj = (Form)ctrl;
                if (obj.MainMenuStrip != null)
                {
                    foreach (ToolStripItem item in obj.MainMenuStrip.Items)
                    {
                        TranslateToolStrip(item);
                    }
                }
            }

            //if (ctrl is TabControl)
            //{
            //    TabControl obj = (TabControl)ctrl;
            //    foreach (TabPage page in obj.TabPages)
            //    {
            //        TranslateControl(page);
            //    }
            //}

            foreach (Control child in ctrl.Controls)
            {
                TranslateControl(child, tooltip);
            }

            if (ctrl is ToolStrip)
            {
                foreach (ToolStripItem item in ((ToolStrip)ctrl).Items) TranslateToolStrip(item);
            }
        }

        public static void TranslateToolStrip(ToolStripItem item)
        {
            if (ShouldTranslate(item.Text)) item.Text = TranslateText(item.Text);
            if (item is ToolStripDropDownButton)
            {
                var obj = (ToolStripDropDownButton)item;
                foreach (ToolStripItem subitem in obj.DropDownItems)
                {
                    TranslateToolStrip(subitem);
                }
            }
            if (item is ToolStripMenuItem)
            {
                ToolStripMenuItem obj = (ToolStripMenuItem)item;
                foreach (ToolStripItem child in obj.DropDownItems)
                {
                    TranslateToolStrip(child);
                }
            }
        }

        private static bool ShouldTranslate(string text)
        {
            if (text == null) return false;
            return text.StartsWith("s_");
        }

        private static string TranslateText(string text)
        {
            return Texts.Get(text);
        }

        public static string RemoveDiacritics(string s)
        {
            string snorm = s.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();

            for (int ich = 0; ich < snorm.Length; ich++)
            {
                char ch = snorm[ich];
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(ch);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(ch);
                }
            }

            return sb.ToString();
        }

        public static void TranslateDialog(CommonDialog dialog)
        {
            var file = dialog as FileDialog;
            if (file != null)
            {
                file.Filter = Texts.Replace(file.Filter);
            }
        }

        public static void TranslateColumns(DataGridViewColumnCollection columns)
        {
            foreach (DataGridViewColumn col in columns)
            {
                col.HeaderText = Texts.Replace(col.HeaderText);
            }
        }
    }
}
