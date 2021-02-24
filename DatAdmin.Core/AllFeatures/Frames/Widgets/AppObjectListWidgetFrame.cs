using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Xml;

namespace DatAdmin
{
    public partial class AppObjectListWidgetFrame : WidgetBaseFrame
    {
        List<AppObject> m_objs = new List<AppObject>();
        ImageCache m_imgCache;
        DateTime? m_lastDoubleClick;
        public bool ShowingRenameEditor = false;
        internal ListWidgetColumnCollection Columns;
        bool m_graySearch = true;
        DatabaseCache m_refreshing;

        public AppObjectListWidgetFrame(AppObjectListWidget widget)
            : base(widget)
        {
            InitializeComponent();
            m_imgCache = new ImageCache(imageList1, Color.White);
            UpdateEnabling();
            HObjectClipboard.Changed += HObjectClipboard_Changed;
            Disposed += new EventHandler(AppObjectListWidgetFrame_Disposed);
        }

        void AppObjectListWidgetFrame_Disposed(object sender, EventArgs e)
        {
            HObjectClipboard.Changed -= HObjectClipboard_Changed;
        }

        void HObjectClipboard_Changed()
        {
            UpdateEnabling();
        }

        new AppObjectListWidget Widget { get { return (AppObjectListWidget)m_widget; } }

        private void UpdateEnabling()
        {
            var caps = Widget.Caps;
            btnNew.Visible = caps.CreateNew;
            btnDelete.Visible = caps.Delete;
            btnRename.Visible = caps.Rename;
            btnGenerateSql.Visible = caps.GenerateSql;
            btnDesign.Visible = caps.Design;
            btnMoveUp.Visible = btnMoveDown.Visible = caps.Move;
            tbxSearch.Visible = caps.Search;
            btnCopy.Visible = caps.Copy;
            btnPaste.Visible = caps.Paste;
            toolStripSeparator1.Visible = caps.Copy || caps.Paste;

            var appobj = SelectedAppObject;
            btnDelete.Enabled = appobj != null && Widget.CanDeleteObject(appobj, SelectedItem.Index);
            btnRename.Enabled = appobj != null && appobj.AllowRename() && listView1.SelectedItems.Count == 1;
            btnGenerateSql.Enabled = appobj != null && appobj.GetSqlGenerators().Count > 0;
            btnDesign.Enabled = appobj != null && appobj.AllowDesign();

            btnCopy.Enabled = appobj != null;
            if (btnPaste.Visible && listView1.SelectedItems.Count == 1)
            {
                DragDropBuilder bld = null;
                if (appobj != null && ObjectClipboard.Memory != null) bld = appobj.GetDragDropBuilder(ObjectClipboard.Memory);
                ObjectClipboard.EnableAndFillPasteButton(bld, btnPaste);
            }
            else
            {
                btnPaste.Enabled = false;
                btnPaste.Text = "";
            }

        }

        public override void OnChangedDesigning()
        {
            base.OnChangedDesigning();
            UpdateEnabling();
        }

        protected override void DoLoadData()
        {
            base.DoLoadData();
            m_objs.Clear();
            try
            {
                if (m_appobj != null)
                {
                    Widget.GetObjectList(m_objs, m_appobj, ConnPack);
                }
            }
            finally
            {
                if (m_refreshing != null)
                {
                    m_refreshing.EndRefresh();
                    m_refreshing = null;
                }
            }
        }

        private void ReloadItemsCore()
        {
            List<AppObject> objs;
            if (!m_graySearch && !tbxSearch.Text.IsEmpty())
            {
                objs = new List<AppObject>();
                string s = tbxSearch.Text.ToUpper();
                foreach (var obj in m_objs)
                {
                    if (obj.ToString().ToUpper().Contains(s)) objs.Add(obj);
                }
            }
            else
            {
                objs = m_objs;
            }
            foreach (var obj in objs)
            {
                AppObject ao;
                if (obj.SupportSerialize)
                {
                    ao = obj.Clone();
                    ao.Owner = this;
                    ao.ConnPack = ConnPack;
                }
                else
                {
                    ao = obj;
                }

                string title = ao.ToString();
                var props = new Dictionary<string, string>();
                if (Columns != null && Columns.Count > 0)
                {
                    ao.GetAppObjectProperties(props);
                    title = props.Get(Columns[0].DataSource, "");
                }
                var item = listView1.Items.Add(title);
                if (Columns != null && Columns.Count > 0)
                {
                    for (int i = 1; i < Columns.Count; i++)
                    {
                        item.SubItems.Add(props.Get(Columns[i].DataSource, ""));
                    }
                }
                item.Tag = ao;
                item.ImageIndex = m_imgCache.GetImageIndex(ao.Image);
            }
        }

        protected override void ShowDataInGui()
        {
            try
            {
                listView1.BeginUpdate();
                listView1.Items.Clear();
                listView1.Columns.Clear();

                if (Columns != null && Columns.Count > 0)
                {
                    listView1.Columns.Clear();
                    foreach (var col in Columns)
                    {
                        var hdr = listView1.Columns.Add(Texts.Get(col.HeaderText));
                        hdr.Width = col.Width;
                    }
                }
                else
                {
                    var hdr = listView1.Columns.Add(Texts.Get("s_name"));
                    hdr.Width = 200;
                }

                ReloadItemsCore();
            }
            finally
            {
                listView1.EndUpdate();
            }
            UpdateEnabling();
        }

        private void ReloadItems()
        {
            try
            {
                listView1.BeginUpdate();
                listView1.Items.Clear();
                ReloadItemsCore();
            }
            finally
            {
                listView1.EndUpdate();
            }
        }

        private void listView1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var item = listView1.GetItemAt(e.X, e.Y);
                if (item == null) return;
                if (!item.Selected)
                {
                    listView1.SelectOneItem(item, true);
                }
                contextMenuStrip1.Items.Clear();
                var mb = new MenuBuilder();
                bool delete = true;
                foreach (ListViewItem it in listView1.SelectedItems)
                {
                    var appobj = (AppObject)it.Tag;
                    appobj.GetPopupMenu(mb);
                    if (!appobj.AllowDelete()) delete = false;
                }
                if (delete && listView1.SelectedItems.Count > 0)
                {
                    var mi = mb.AddItem("s_delete", DeleteSelectedObjects, CoreIcons.delete, MenuWeights.DELETE);
                    mi.GroupName = "node";
                }
                if (listView1.SelectedItems.Count == 1 && SelectedAppObject.AllowRename())
                {
                    var mi = mb.AddItem("s_rename", CallBeginRename, CoreIcons.rename, MenuWeights.RENAME);
                    mi.GroupName = "node";
                }
                if (listView1.SelectedItems.Count >= 1)
                {
                    var mi = mb.AddItem("s_copy_to_clipboard", CallCopy, CoreIcons.copy, MenuWeights.COPY);
                    mi.GroupName = "node";
                }
                mb.GetMenuItems(contextMenuStrip1.Items);
                contextMenuStrip1.ShowOnCursor();
            }
        }

        public ListViewItem SelectedItem
        {
            get
            {
                if (listView1.FocusedItem != null) return listView1.FocusedItem;
                if (listView1.SelectedItems.Count == 0) return null;
                return listView1.SelectedItems[0];
            }
        }

        public AppObject SelectedAppObject
        {
            get
            {
                var item = SelectedItem;
                if (item == null) return null;
                var ao = (AppObject)item.Tag;
                return ao;
            }
        }

        private DashboardFrame GetParentDashboard()
        {
            Control par = Parent;
            while (par != null)
            {
                var res = par as DashboardFrame;
                if (res != null) return res;
                par = par.Parent;
            }
            return null;
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            m_lastDoubleClick = DateTime.Now;
            var ao = SelectedAppObject;
            if (ao == null) return;
            switch (DoubleClickAction)
            {
                case DoubleClickActionType.DefaultAction:
                    ao.DefaultAction(); 
                    break;
                case DoubleClickActionType.OpenInTree:
                    {
                        var dash = GetParentDashboard();
                        if (dash != null && dash.WinId != null && MainWindow.Instance.HasContent(dash.WinId))
                        {
                            ao.OpenTheBestDashboard();
                            return;
                        }
                        else if (ao.GetTreePath() != null)
                        {
                            HTree.CallSelectNode(ao.GetTreePath(), SelectNodeFlags.FocusTree | SelectNodeFlags.ScrollInView);
                        }
                    }
                    break;
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshData();
        }

        private void RefreshData()
        {
            m_refreshing = m_appobj.FindDatabaseCache();
            if (m_refreshing != null) 
            {
                m_refreshing.BeginRefresh();
            }
            CallLoad(m_appobj);
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateEnabling();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteSelectedObjects();
        }

        private void DeleteSelectedObjects()
        {
            if (listView1.SelectedItems.Count == 0) return;
            if (!Widget.Caps.Delete) return;
            if (!StdDialog.YesNoDialog("s_really_delete$items", "items", listView1.SelectedItems.Count.ToString())) return;

            var itemsToDelete = new List<Tuple<AppObject, int>>();
            using (var wc = new WaitContext())
            {
                foreach (ListViewItem item in listView1.SelectedItems)
                {
                    var obj = (AppObject)item.Tag;
                    itemsToDelete.Add(new Tuple<AppObject, int>(obj, item.Index));
                }
            }
            itemsToDelete.SortByKey(t => -t.V2);
            foreach (var tpl in itemsToDelete)
            {
                Widget.DeleteAppObject(tpl.V1, tpl.V2);
            }
            RefreshData();
        }

        private void btnRename_Click(object sender, EventArgs e)
        {
            CallBeginRename();
        }

        private void CallBeginRename()
        {
            if (listView1.SelectedItems.Count == 1)
            {
                listView1.SelectedItems[0].BeginEdit();
            }
        }

        //private void ShowRenameEditor(AppObject appobj)
        //{
        //    foreach (ListViewItem item in listView1.Items)
        //    {
        //        if (item.Tag == appobj)
        //        {
        //            item.BeginEdit();
        //            return;
        //        }
        //    }
        //}

        private void listView1_BeforeLabelEdit(object sender, LabelEditEventArgs e)
        {
            if (m_lastDoubleClick != null && (DateTime.Now - m_lastDoubleClick.Value).TotalSeconds < 3)
            {
                e.CancelEdit = true;
                return;
            }
            if (ShowingRenameEditor) return;
            var item = listView1.Items[e.Item];
            var appobj = item.Tag as AppObject;
            if (appobj != null && appobj.AllowRename())
            {

                MainWindow.Instance.RunInMainWindow(() => ShowRenameEditor(item));
                e.CancelEdit = true;
            }
            e.CancelEdit = true;
        }

        private string GetObjectTitle(AppObject appobj)
        {
            if (Columns != null && Columns.Count > 0)
            {
                var props = appobj.GetAppObjectProperties();
                return props.Get(Columns[0].DataSource);
            }
            else
            {
                return appobj.ToString();
            }
        }

        private void listView1_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            try
            {
                var item = listView1.Items[e.Item];
                var appobj = item.Tag as AppObject;
                if (appobj == null) return;

                if (e.Label == null)
                {
                    item.Text = GetObjectTitle(appobj);
                    return;
                }

                string newname = e.Label;
                e.CancelEdit = true;// revert to old name
                item.Text = GetObjectTitle(appobj);
                appobj.RenameObject(newname);
                RefreshData();
            }
            catch (Exception ex)
            {
                Errors.Report(ex);
            }
        }

        public void ShowRenameEditor(ListViewItem item)
        {
            var appobj = item.Tag as AppObject;
            if (appobj == null) return;
            try
            {
                ShowingRenameEditor = true;
                item.Text = appobj.GetRenamingText();
                item.BeginEdit();
            }
            finally
            {
                ShowingRenameEditor = false;
            }
        }

        public View ListStyle
        {
            get { return listView1.View; }
            set { listView1.View = value; }
        }

        public bool ShowToolbar
        {
            get { return toolStrip1.Visible; }
            set
            {
                toolStrip1.Visible = value;
                if (toolStrip1.Visible) toolStrip1.SendToBack();
            }
        }

        private List<AppObject> GetSelectedObjects()
        {
            var objs = new List<AppObject>();
            foreach (ListViewItem item in listView1.SelectedItems)
            {
                var appobj = ((ListViewItem)item).Tag as AppObject;
                if (appobj != null) objs.Add(appobj);
            }
            return objs;
        }

        private void listView1_ItemDrag(object sender, ItemDragEventArgs e)
        {
            //var appobj = ((ListViewItem)e.Item).Tag as AppObject;
            //if (appobj == null) return;
            var objs = GetSelectedObjects();
            if (objs.Count == 0) return;
            try
            {
                //DoDragDrop(DragObjectContainer.Create(new AppObject[] { appobj }), DragDropEffects.Copy);
                DoDragDrop(DragObjectContainer.Create(objs.ToArray()), DragDropEffects.Copy);
            }
            catch (Exception ex)
            {
                Errors.Report(ex);
            }
        }

        private void btnGenerateSql_Click(object sender, EventArgs e)
        {
            CallGenerateSql();
        }

        private void CallGenerateSql()
        {
            var appobj = SelectedAppObject;
            bool enabled = appobj != null && appobj.GetSqlGenerators().Count > 0;
            if (enabled) GenerateSqlForm.Run(GetSelectedObjects().ToArray());
        }

        public void OnChangedColumns()
        {
            RefreshData();
        }

        private void btnDesign_Click(object sender, EventArgs e)
        {
            foreach (ListViewItem item in listView1.SelectedItems)
            {
                var obj = (AppObject)item.Tag;
                obj.DoDesign();
            }
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            Widget.CreateNew(m_appobj, ConnPack);
            RefreshData();
        }

        private void listView1_DragOver(object sender, DragEventArgs e)
        {
            try
            {
                var pt = listView1.PointToClient(new Point(e.X, e.Y));
                ListViewItem dropItem = listView1.GetItemAt(pt.X, pt.Y);
                DragObjectContainer cnt = (DragObjectContainer)e.Data.GetData(typeof(DragObjectContainer));
                if (cnt != null && dropItem != null && cnt.Data is AppObject[])
                {
                    var appobj = (AppObject)dropItem.Tag;
                    if (appobj.AllowDragDrop((AppObject[])cnt.Data))
                    {
                        e.Effect = DragDropEffects.Copy;
                    }
                    else
                    {
                        e.Effect = DragDropEffects.None;
                    }
                }
            }
            catch (Exception ex)
            {
                Errors.Report(ex);
            }
        }

        private void listView1_DragDrop(object sender, DragEventArgs e)
        {
            try
            {
                var pt = listView1.PointToClient(new Point(e.X, e.Y));
                ListViewItem dropItem = listView1.GetItemAt(pt.X, pt.Y);
                DragObjectContainer cnt = (DragObjectContainer)e.Data.GetData(typeof(DragObjectContainer));
                if (cnt != null && dropItem != null && cnt.Data is AppObject[])
                {
                    var appobj = (AppObject)dropItem.Tag;
                    AppObject[] draggedObjs = (AppObject[])cnt.Data;
                    appobj.DragDrop(draggedObjs);
                }
            }
            catch (Exception ex)
            {
                Errors.Report(ex);
            }
        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            if (SelectedAppObject != null) Widget.MoveWidgetUp(SelectedAppObject, SelectedItem.Index);
        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            if (SelectedAppObject != null) Widget.MoveWidgetDown(SelectedAppObject, SelectedItem.Index);
        }

        public DoubleClickActionType DoubleClickAction;

        private void tbxSearch_TextChanged(object sender, EventArgs e)
        {
            ReloadItems();
        }

        private void tbxSearch_Enter(object sender, EventArgs e)
        {
            if (m_graySearch)
            {
                tbxSearch.Text = "";
                tbxSearch.ForeColor = SystemColors.WindowText;
                m_graySearch = false;
            }
        }

        private void tbxSearch_Leave(object sender, EventArgs e)
        {
            if (tbxSearch.Text.IsEmpty())
            {
                m_graySearch = true;
                tbxSearch.Text = Texts.Get("s_search");
                tbxSearch.ForeColor = SystemColors.GrayText;
            }
        }

        private void listView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsLetterOrDigit(e.KeyChar) && tbxSearch.Visible && m_graySearch)
            {
                tbxSearch.Focus();
                tbxSearch.Text = new String(e.KeyChar, 1);
                tbxSearch.SelectionStart = 1;
            }
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            CallCopy();
        }

        private void CallCopy()
        {
            ObjectClipboard.Memory = GetSelectedObjects().ToArray();
        }

        private void listView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.C && e.Control)
            {
                CallCopy();
                e.Handled = true;
            }
            if (e.KeyCode == Keys.V && e.Control && btnPaste.Enabled)
            {
                btnPaste.ShowDropDown();
                e.Handled = true;
            }
            if (e.KeyCode == Keys.Delete)
            {
                DeleteSelectedObjects();
                e.Handled = true;
            }
            if (e.KeyCode == Keys.G && e.Control)
            {
                CallGenerateSql();
                e.Handled = true;
            }
        }
    }
}
