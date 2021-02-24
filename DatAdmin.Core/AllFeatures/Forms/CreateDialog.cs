using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Linq;

namespace DatAdmin
{
    public partial class CreateDialog : FormEx, ITooltipHolder
    {
        ITreeNode m_parent;
        Dictionary<string, List<ICreateFactoryItem>> m_items = new Dictionary<string, List<ICreateFactoryItem>>();
        Dictionary<ICreateFactoryItem, int> m_imageIndexes = new Dictionary<ICreateFactoryItem, int>();
        List<ICreateFactoryItem> m_allItems = new List<ICreateFactoryItem>();
        string m_selectGroup;
        string m_selectItem;

        public CreateDialog(ITreeNode parent) : this(parent, "connections", null) { }

        public CreateDialog(ITreeNode parent, string selectGroup, string selectItem)
        {
            m_selectGroup = selectGroup;
            m_selectItem = selectItem;
            m_parent = parent;
            InitializeComponent();
            infoBoxFrame1.InfoText = Texts.Get("s_select_item_to_create");
            if (!ShowGroups)
            {
                int dx = lbxGroups.Width + lbxItems.Left - lbxGroups.Right;
                lbxGroups.Visible = false;
                Width -= dx;
                lbxItems.Left -= dx;
                lbxItems.Width += dx;
                infoBoxFrame1.Left -= dx;
                infoBoxFrame1.Width += dx;
            }
        }

        public bool ShowGroups { get { return !VersionInfo.HideGroupsInCreateDialog; } }

        private void CreateDialog_Load(object sender, EventArgs e)
        {
            if (m_parent == null) m_parent = new RootTreeNode();
            foreach (var item in CreateFactoryAddonType.GetItems(m_parent))
            {
                if (!m_parent.AllowCreate(item.GroupName, item.Name)) continue;
                m_allItems.Add(item);
            }
            foreach (ICreateFactoryItem item in m_allItems)
            {
                Bitmap bmp = item.Bitmap;
                if (bmp != null)
                {
                    m_imageIndexes[item] = imageList2.Images.Count;
                    imageList2.Images.Add(item.Bitmap);
                }
                else
                {
                    m_imageIndexes[item] = -1;
                }
            }
            if (!ShowGroups)
            {
                if (m_selectGroup != null && m_selectItem == null)
                {
                    foreach (var item in m_allItems)
                    {
                        if (item.GroupName == m_selectGroup)
                        {
                            m_selectItem = item.Name;
                            break;
                        }
                    }
                }
                listView1_SelectedIndexChanged(sender, e);
                return;
            }
            foreach (ICreateFactoryItem item in m_allItems)
            {
                if (item.Name == null || item.GroupName == null) continue;
                if (!m_parent.AllowCreate(item.GroupName, item.Name)) continue;
                if (!m_items.ContainsKey(item.GroupName))
                {
                    m_items[item.GroupName] = new List<ICreateFactoryItem>();
                    ListViewItem grp = lbxGroups.Items.Add(Texts.Get(item.Group));
                    grp.Name = item.GroupName;
                    grp.ImageIndex = 0;
                }
                m_items[item.GroupName].Add(item);
            }
            int selgrp = 0;
            if (m_selectGroup != null)
            {
                int index = 0;
                foreach (ListViewItem item in lbxGroups.Items)
                {
                    if (item.Name == m_selectGroup)
                    {
                        selgrp = index;
                    }
                    index++;
                }
            }
            foreach (var it in m_items)
            {
                it.Value.SortByKey(i => new Tuple<int, string>(-i.Weight, Texts.Get(i.Title)));
            }
            if (selgrp < lbxGroups.Items.Count)
            {
                lbxGroups.Items[selgrp].Focused = true;
                lbxGroups.Items[selgrp].Selected = true;
                listView1_SelectedIndexChanged(sender, e);
            }
            m_selectItem = null;
            m_selectGroup = null;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbxGroups.SelectedItems.Count == 0 && ShowGroups) return;
            var lst = new List<ICreateFactoryItem>();
            if (ShowGroups)
            {
                string grp = lbxGroups.SelectedItems[0].Name;
                lst.AddRange(m_items[grp]);
            }
            else
            {
                lst.AddRange(m_allItems);
            }

            lbxItems.Items.Clear();
            foreach (ICreateFactoryItem item in lst)
            {
                ListViewItem it = lbxItems.Items.Add(Texts.Get(item.Title));
                it.ImageIndex = m_imageIndexes[item];
                it.Name = item.Name;
                it.Tag = item;
            }

            int selitem = 0;
            if (m_selectItem != null)
            {
                int index = 0;
                foreach (ListViewItem item in lbxItems.Items)
                {
                    if (item.Name == m_selectItem)
                    {
                        selitem = index;
                    }
                    index++;
                }
            }
            if (selitem < lbxItems.Items.Count)
            {
                lbxItems.Items[selitem].Selected = true;
                lbxItems.Items[selitem].Focused = true;
                lbxItems_SelectedIndexChanged(sender, e);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (lbxItems.SelectedItems.Count == 0)
            {
                StdDialog.ShowError("s_you_must_select_item_to_create");
                lbxItems.Focus();
                return;
            }
            if (rbtNameInTree.Checked && tbxNewName.Text == "")
            {
                StdDialog.ShowError("s_you_must_enter_name");
                tbxNewName.Focus();
                return;
            }
            if (rbtFileOnDisk.Checked && tbxFileName.Text == "")
            {
                StdDialog.ShowError("s_input_file_name");
                tbxFileName.Focus();
                return;
            }
            ICreateFactoryItem item = (ICreateFactoryItem)lbxItems.SelectedItems[0].Tag;
            if (rbtFileOnDisk.Checked)
            {
                if (!StdDialog.CheckAbsoluteOutputFileName(tbxFileName.Text, item.FileExtensions)) return;
            }
            try { Directory.CreateDirectory(m_parent.FileSystemPath); }
            catch (Exception) { }
            if (rbtNameInTree.Checked)
            {
                if (item.Create(m_parent, tbxNewName.Text)) Close();
            }
            if (rbtFileOnDisk.Checked)
            {
                if (!item.CreateFile(tbxFileName.Text)) return;
                using (StreamWriter sw = new StreamWriter(Path.Combine(m_parent.FileSystemPath, Path.GetFileNameWithoutExtension(tbxFileName.Text) + ".lnk")))
                {
                    sw.Write(tbxFileName.Text);
                }
                Close();
            }
        }

        private void cbxDestination_SelectedIndexChanged(object sender, EventArgs e)
        {
            tbxNewName.Enabled = rbtNameInTree.Checked;
            btnBrowse.Enabled = tbxFileName.Enabled = rbtFileOnDisk.Checked;
        }

        private void lbxItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbxItems.SelectedItems.Count == 0) return;
            ICreateFactoryItem item = (ICreateFactoryItem)lbxItems.SelectedItems[0].Tag;
            if (!item.AllowCreateFiles)
            {
                rbtNameInTree.Checked = true;
                rbtFileOnDisk.Enabled = false;
            }
            else
            {
                rbtFileOnDisk.Enabled = true;
            }
            infoBoxFrame1.InfoText = Texts.Get(item.InfoText);
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (lbxItems.SelectedItems.Count == 0) return;
            saveFileDialog1.FileName = tbxFileName.Text;
            ICreateFactoryItem item = (ICreateFactoryItem)lbxItems.SelectedItems[0].Tag;
            if (!item.AllowCreateFiles) return;
            saveFileDialog1.Filter = (
                from f in item.FileExtensions.ToLower().Split('|')
                select String.Format("{0} {1}|*.{2}", f.ToUpper(), Texts.Get("s_files"), f.ToLower()))
                .CreateDelimitedText("|");
            if (saveFileDialog1.ShowDialogEx() == DialogResult.OK)
            {
                tbxFileName.Text = saveFileDialog1.FileName;
            }
        }

        #region ITooltipHolder Members

        public ToolTip GetToolTip()
        {
            return toolTip1;
        }

        #endregion

        private void CreateDialog_Shown(object sender, EventArgs e)
        {
            tbxNewName.Focus();
        }
    }
}