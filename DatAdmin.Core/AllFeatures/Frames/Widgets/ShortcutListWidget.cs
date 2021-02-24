using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Drawing;

namespace DatAdmin
{
    [Widget(Name = "shortcut_list", Title = "Shortcut list", Category = "Lists")]
    public class ShortcutListWidget : AppObjectListWidget
    {
        public List<AppObject> Items = new List<AppObject>();

        public ShortcutListWidget()
        {
            DoubleClickAction = DoubleClickActionType.DefaultAction;
        }

        public override Bitmap DefaultImage
        {
            get { return CoreIcons.favorite; }
        }

        public override string DefaultPageTitle
        {
            get { return "Database shortcuts"; }
        }

        public override void LoadFromXml(XmlElement xml)
        {
            base.LoadFromXml(xml);
            foreach (XmlElement x in xml.SelectNodes("Item"))
            {
                Items.Add((AppObject)AppObjectAddonType.Instance.LoadAddon(x));
            }
        }

        public override void SaveToXml(XmlElement xml)
        {
            base.SaveToXml(xml);
            foreach (var item in Items)
            {
                item.SaveToXml(xml.AddChild("Item"));
            }
        }

        public override void GetObjectList(List<AppObject> objs, AppObject appobj, ConnectionPack connpack)
        {
            if (IsDesigning)
            {
                objs.Add(new AddShortcutAppObject(this));
            }
            foreach (var item in Items)
            {
                var newobj = item.CloneUsingXml() as DatabaseFieldsAppObject;
                if (newobj == null) continue;
                newobj.AssignDbFields(appobj as DatabaseFieldsAppObject);
                objs.Add(newobj);
            }
        }

        protected override void OnChangedDesigning()
        {
            base.OnChangedDesigning();
            CallChangedData();
        }

        public void AddObjects(IEnumerable<AppObject> objs)
        {
            foreach (var obj in objs)
            {
                _AddObject(obj);
            }
            CallChangedData();
        }

        private void _AddObject(AppObject obj)
        {
            if (!(obj is DatabaseFieldsAppObject)) return;
            var newobj = (DatabaseFieldsAppObject)obj.CloneUsingXml();
            newobj.AssignDbFields(null);
            Items.Add(newobj);
        }

        public override ListWidgetCaps Caps
        {
            get
            {
                var res = base.Caps;
                res.Move = IsDesigning;
                res.Delete = IsDesigning;
                return res;
            }
        }

        public override void DeleteAppObject(AppObject obj, int index)
        {
            index--;
            if (index >= 0) Items.RemoveAt(index);
            CallChangedData();
        }

        public override void MoveWidgetDown(AppObject appobj, int index)
        {
            index--;
            if (index >=0 && index < Items.Count) Items.Exchange(index, index + 1);
            CallChangedData();
        }

        public override void MoveWidgetUp(AppObject appobj, int index)
        {
            index--;
            if (index > 0) Items.Exchange(index, index - 1);
            CallChangedData();
        }

        public override bool CanDeleteObject(AppObject appobj, int index)
        {
            return index > 0;
        }
    }

    public class AddShortcutAppObject : AppObject
    {
        ShortcutListWidget m_widget;

        public AddShortcutAppObject(ShortcutListWidget widget)
        {
            m_widget = widget;
        }

        public AddShortcutAppObject() { }

        public override System.Drawing.Bitmap Image
        {
            get { return CoreIcons.add; }
        }

        public override string ToString()
        {
            return Texts.Get("s_drag_and_drop_here");
        }

        public override bool AllowDragDrop(AppObject[] draggingObjects)
        {
            return true;
        }

        public override void DragDrop(AppObject[] draggingObjects)
        {
            m_widget.AddObjects(draggingObjects);
        }

        public override string TypeName
        {
            get { return "add_shortcut"; }
        }

        public override string TypeTitle
        {
            get { return "Add Shortcut"; }
        }
    }
}
