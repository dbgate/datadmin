using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.Xml;

namespace DatAdmin
{
    public partial class ListEditorFrame : ContentFrame
    {
        ListEditor m_editor;
        ListEditorCaps m_caps;
        IList m_items;
        Dictionary<object, object> m_editors = new Dictionary<object, object>();
        public string Title;
        public Bitmap Icon;

        public ListEditorFrame(ListEditor editor, IList items)
        {
            InitializeComponent();

            m_editor = editor;
            m_items = m_editor.ReloadItems(items);
            m_caps = m_editor.EditorCaps;
            propertyFrame1.Enabled = m_caps.Edit;
            btnDuplicate.Enabled = m_caps.Duplicate;
            btnRemove.Enabled = m_caps.Delete;
            btnAdd.Enabled = m_caps.CreateNew;
            btnRename.Enabled = m_caps.Rename;
            btnMoveUp.Visible = m_caps.Reorder;
            btnMoveDown.Visible = m_caps.Reorder;
            btnRefresh.Visible = m_caps.Refresh;

            LoadItems();
        }

        public ListEditorFrame(ListEditor editor)
            : this(editor, null)
        {
        }

        private void LoadItems()
        {
            lbxItems.Items.Clear();
            foreach (var item in m_items)
            {
                lbxItems.Items.Add(item);
            }
        }

        private void lbxItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            object item = lbxItems.SelectedItem;
            if (item == null)
            {
                propertyFrame1.SelectedObject = null;
                return;
            }
            if (!m_editors.ContainsKey(item)) m_editors[item] = m_editor.GetEditObject(item);
            propertyFrame1.SelectedObject = m_editors[item];
        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            lbxItems.MoveSelectedUp();
            SaveCollection();
        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            lbxItems.MoveSelectedDown();
            SaveCollection();
        }

        private void SaveCollection()
        {
            if (m_items == null) return;
            m_items.Clear();
            foreach (object item in lbxItems.Items)
            {
                m_items.Add(item);
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            object newitem = m_editor.CreateNew();
            if (newitem == null) return;
            SaveLoadedItems();
            m_items.Add(newitem);
            RefreshItemsNoSave();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            RefreshItems();
        }

        private void RefreshItems()
        {
            SaveLoadedItems();
            RefreshItemsNoSave();
        }

        private void RefreshItemsNoSave()
        {
            m_items = m_editor.ReloadItems(m_items);
            LoadItems();
        }

        internal void SaveLoadedItems()
        {
            foreach (var item in m_editors)
            {
                m_editor.SaveItem(item.Key, item.Value);
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            m_editors.Clear();
            object item = lbxItems.SelectedItem;
            if (item == null) return;
            SaveLoadedItems();
            if (m_editor.Delete(item))
            {
                m_items.Remove(item);
            }
            RefreshItemsNoSave();
        }

        private void btnRename_Click(object sender, EventArgs e)
        {
            int index = lbxItems.SelectedIndex;
            if (index < 0) return;
            object newitem = m_editor.Rename(m_items[index]);
            if (newitem == null) return;
            SaveLoadedItems();
            m_items[index] = newitem;
            RefreshItemsNoSave();
        }

        private void btnDuplicate_Click(object sender, EventArgs e)
        {
            if (lbxItems.SelectedItem == null) return;
            object newitem = m_editor.Duplicate(lbxItems.SelectedItem);
            if (newitem == null) return;
            SaveLoadedItems();
            m_items.Add(newitem);
            RefreshItemsNoSave();
        }

        public override string PageTitle
        {
            get { return Title; }
        }

        public override Bitmap Image
        {
            get { return Icon; }
        }

        public override bool Save()
        {
            m_editor.OnSaveContent();
            return true;
        }

        public override bool SupportsSave
        {
            get { return true; }
        }
    }

    public class ListEditorCaps
    {
        public bool CreateNew;
        public bool Duplicate;
        public bool Delete;
        public bool Rename;
        public bool Edit;
        public bool Reorder;
        public bool Refresh;

        public bool AllFlags
        {
            set
            {
                CreateNew = value;
                Duplicate = value;
                Delete = value;
                Rename = value;
                Edit = value;
                Reorder = value;
                Refresh = value;
            }
        }
    }

    public abstract class ListEditor
    {
        public ListEditorCaps EditorCaps;
        public ListEditor()
        {
            EditorCaps = new ListEditorCaps();
            FillDefaultCaps(EditorCaps);
        }
        public abstract bool Delete(object item);
        public abstract object Duplicate(object item);
        public abstract object Rename(object item);
        public abstract object GetEditObject(object item);
        public abstract object CreateNew();
        public virtual void SaveItem(object item, object editem) { }
        public virtual IList ReloadItems(IList origItems)
        {
            return origItems;
        }
        public virtual void OnSaveContent() { }

        protected virtual void FillDefaultCaps(ListEditorCaps caps)
        {
            caps.AllFlags = true;
        }

        public DialogResult ShowDialog(string title)
        {
            return ShowDialog(title, GenericDialogType.Close, null);
        }

        public DialogResult ShowDialog(string title, GenericDialogType type)
        {
            return ShowDialog(title, type, null);
        }

        public DialogResult ShowDialog(string title, GenericDialogType type, IList list)
        {
            var frm = new ListEditorFrame(this, list);
            var res = frm.ShowGenericDialog(title, type);
            frm.SaveLoadedItems();
            return res;
        }

        public void ShowContent(string title, IList list, Bitmap icon)
        {
            var frm = new ListEditorFrame(this, list);
            frm.Title = title;
            frm.Icon = icon;
            MainWindow.Instance.OpenContent(frm);
        }
    }

    public abstract class AddonListEditor : ListEditor
    {
        protected AddonType m_adtype;

        public AddonListEditor(AddonType adtype)
        {
            m_adtype = adtype;
        }
    }

    public abstract class DirectoryAddonListEditorBase : AddonListEditor
    {
        public DirectoryAddonListEditorBase(AddonType adtype)
            : base(adtype)
        {
        }

        protected string GetItemName(string file)
        {
            if (file == null) return null;
            string dir = m_adtype.GetDirectory();
            string relfile = IOTool.RelativePathTo(dir, file);
            return Path.ChangeExtension(relfile, null);
        }

        public override bool Delete(object item)
        {
            if (StdDialog.ReallyDeleteFile(item))
            {
                File.Delete(GetFileName(item));
                return true;
            }
            return false;
        }
        private object CopyTo(object item, CopyFileMode mode)
        {
            string file = GetFileName(item);
            string oldname = GetItemName(file);
            string newname = InputBox.Run(Texts.Get("s_new_name"), oldname);
            if (newname != null)
            {
                string newfile = System.IO.Path.Combine(
                        System.IO.Path.GetDirectoryName(file),
                        System.IO.Path.ChangeExtension(newname, System.IO.Path.GetExtension(file)));

                if (File.Exists(newfile))
                {
                    if (!StdDialog.ReallyOverwriteFile(newfile)) return null;
                }

                if (mode == CopyFileMode.Copy) File.Copy(file, newfile, true);
                if (mode == CopyFileMode.Move) File.Move(file, newfile);
                var res = CreateNewAddon();
                res.LoadFromFile(newfile);
                if (res is IFileBasedAddonInstance) ((IFileBasedAddonInstance)res).AddonFileName = newfile;
                return res;
                //return GetItemName(newfile);
            }
            return null;
        }
        public override object Duplicate(object item)
        {
            return CopyTo(item, CopyFileMode.Copy);
        }
        public override object Rename(object item)
        {
            return CopyTo(item, CopyFileMode.Move);
        }
        public override object GetEditObject(object item)
        {
            string fn = GetFileName(item);
            var doc = new XmlDocument();
            doc.Load(fn);
            var obj = m_adtype.LoadAddon(doc.DocumentElement);
            return obj;
        }
        protected virtual IAddonInstance CreateNewAddon() { return null; }

        protected abstract string GetFileName(object item);
    }

    public class DirectoryAddonListEditor : DirectoryAddonListEditorBase
    {
        public DirectoryAddonListEditor(AddonType adtype)
            : base(adtype)
        {
        }
        protected override string GetFileName(object item)
        {
            if (item == null) return null;
            string dir = m_adtype.GetDirectory();
            string fn = Path.Combine(dir, item.ToString() + m_adtype.FileExtension);
            return fn;
        }
        public override IList ReloadItems(IList origItems)
        {
            var res = new List<string>();
            string dir = m_adtype.GetDirectory();
            try { Directory.CreateDirectory(dir); }
            catch { }
            foreach (string fn in Directory.GetFiles(dir, "*" + m_adtype.FileExtension))
            {
                res.Add(GetItemName(fn));
            }
            return res;
        }
        public override object CreateNew()
        {
            string newname = InputBox.Run(Texts.Get("s_new_name"), "new");
            if (newname == null) return null;
            var obj = CreateNewAddon();
            if (obj == null) return null;
            string fn = GetFileName(newname);
            obj.SaveToFile(GetFileName(newname));
            return GetItemName(fn);
        }
    }

    public class CollectionAddonListEditor : AddonListEditor
    {
        public CollectionAddonListEditor(AddonType adtype)
            : base(adtype)
        {
        }

        public override object GetEditObject(object item)
        {
            return item;
        }
        public override object Duplicate(object item)
        {
            return null;
        }
        public override bool Delete(object item)
        {
            return false;
        }
        public override object Rename(object item)
        {
            return null;
        }
        public override object CreateNew()
        {
            return null;
        }
    }

    public class CollectionNamedAddonListEditor : CollectionAddonListEditor
    {
        public CollectionNamedAddonListEditor(AddonType adtype)
            : base(adtype)
        {
        }

        public override object GetEditObject(object item)
        {
            return ((NamedAddonInstance)item).Instance;
        }
        public override object Duplicate(object item)
        {
            return null;
        }
        public override bool Delete(object item)
        {
            return false;
        }
        public override object Rename(object item)
        {
            var old = (NamedAddonInstance)item;
            string newname = InputBox.Run(Texts.Get("s_new_name"), old.Name);
            return new NamedAddonInstance(old.Instance, newname);
        }
        public override object CreateNew()
        {
            return NewAddonForm.Run(m_adtype);
        }
    }
}
