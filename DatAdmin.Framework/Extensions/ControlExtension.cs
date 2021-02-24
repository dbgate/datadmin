using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Windows.Forms;

namespace DatAdmin
{
    public enum GenericDialogType { OkCancel, Close }
    public static class ControlExtension
    {
        public static void ReloadItemNames(this ListBox ctrl)
        {
            List<object> items = new List<object>();
            foreach (var i in ctrl.Items) items.Add(i);
            int selindex = ctrl.SelectedIndex;
            ctrl.Items.Clear();
            ctrl.Items.AddRange(items.ToArray());
            ctrl.SelectedIndex = selindex;
        }

        public static void AddCurrentToHistory(this ToolStripComboBox combo)
        {
            string text = combo.Text;
            if (text.IsEmpty()) return;
            if (combo.Items.Contains(text)) combo.Items.Remove(text);
            combo.Items.Insert(0, text);
            combo.Text = text;
        }

        public static void AddCurrentToHistory(this ComboBox combo)
        {
            string text = combo.Text;
            if (text.IsEmpty()) return;
            if (combo.Items.Contains(text)) combo.Items.Remove(text);
            combo.Items.Insert(0, text);
            combo.Text = text;
        }

        public static void RemoveSelected(this ListBox list)
        {
            var indexes = new List<int>();
            foreach (int sel in list.SelectedIndices) indexes.Add(sel);
            indexes.Sort();
            indexes.Reverse();
            foreach (int delindex in indexes)
            {
                list.Items.RemoveAt(delindex);
            }
            if (indexes.Count > 0) list.SelectedIndex = indexes.Min() - 1;
        }

        public static void TruncateTabs(this TabControl tabs, int count)
        {
            tabs.Enabled = false;
            while (tabs.TabPages.Count > count) tabs.TabPages.Remove(tabs.TabPages[count]);
            tabs.Enabled = true;
        }

        public static void ResizeColumnsToWidth(this ListView list)
        {
            int sum = (from ColumnHeader hdr in list.Columns select hdr.Width).Sum();
            float k = (list.Width - 16) / (float)sum;
            foreach (ColumnHeader hdr in list.Columns)
            {
                hdr.Width = (int)(hdr.Width * k);
            }
        }

        public static void ResizeControlToWidth(this StatusStrip status)
        {
            int sum = (from ToolStripItem item in status.Items select item.Width).Sum();
            float k = (status.Width - 16) / (float)sum;
            foreach (ToolStripItem item in status.Items)
            {
                item.Width = (int)(item.Width * k);
            }
        }

        public static void SelectOneItem(this ListView list, ListViewItem item, bool focus)
        {
            foreach (ListViewItem s in list.SelectedItems) s.Selected = false;
            if (item == null) return;
            item.Selected = true;
            if (focus)
            {
                item.Focused = true;
                list.FocusedItem = item;
            }
        }

        public static void SetAllChecked(this CheckedListBox ctrl, bool value)
        {
            for (int i = 0; i < ctrl.Items.Count; i++)
            {
                ctrl.SetItemChecked(i, value);
            }
        }

        public static List<string> GetCheckedItemNames(this CheckedListBox ctrl)
        {
            var res = new List<string>();
            for (int i = 0; i < ctrl.Items.Count; i++)
            {
                if (ctrl.GetItemChecked(i)) res.Add(ctrl.Items[i].ToString());
            }
            return res;
        }

        public static void MoveSelectedUp(this ListBox list)
        {
            int index = list.SelectedIndex;
            if (index > 0)
            {
                list.Items.Exchange(index, index - 1);
                list.SelectedItems.Clear();
                list.SelectedIndex = index - 1;
            }
        }

        public static void MoveSelectedDown(this ListBox list)
        {
            int index = list.SelectedIndex;
            if (index >= 0 && index < list.Items.Count - 1)
            {
                list.Items.Exchange(index, index + 1);
                list.SelectedItems.Clear();
                list.SelectedIndex = index + 1;
            }
        }

        public static void MoveCursor(this ListBox list, int d)
        {
            int index = list.SelectedIndex + d;
            if (index >= 0 && index < list.Items.Count)
            {
                list.SelectedItems.Clear();
                list.SelectedIndex = index;
            }
        }

        public static DialogResult ShowGenericDialog(this Control content, string title, GenericDialogType type)
        {
            return GenericDialogForm.Run(content, title, type);
        }
    }
}
