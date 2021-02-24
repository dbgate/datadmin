using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Drawing;

namespace DatAdmin
{
    public class MenuItemData
    {
        public int Weight = 0;
        public float GroupWeight = 0;
        public Bitmap Image;
        public Keys Shortcut;
        public string ShortcutDisplayString;
        public MultipleMode MultiMode = MultipleMode.SingleOnly;
        public string GroupName;
        public MultiCallable Callable;
        public bool Enabled = true;
        public bool HideIfNoChildren;
    }

    public abstract class MultiCallable
    {
        public List<object> Args = new List<object>();

        public abstract void Call(MultipleMode mode);
        public void AddArgument(object arg)
        {
            Args.Add(arg);
        }

        public bool Acceptable(MultipleMode mode)
        {
            switch (mode)
            {
                case MultipleMode.NativeMulti:
                case MultipleMode.Sequencable:
                    return Args.Count > 0;
                case MultipleMode.SingleOnly:
                    return Args.Count == 1;
            }
            return false;
        }

        public virtual void GetUsageParams(UsageBuilder usage)
        {
            usage["args"] = Args.Count.ToString();
            if (Args.Count >= 1)
            {
                usage["objtype0"] = Args[0] != null ? Args[0].GetType().FullName : "null";
                usage["obj0"] = Args[0].SafeToString();
            }
        }
    }

    public class MethodCallable : MultiCallable
    {
        public MethodInfo Method;

        public MethodCallable(MethodInfo method)
        {
            Method = method;
        }

        public override void Call(MultipleMode mode)
        {
            switch (mode)
            {
                case MultipleMode.NativeMulti:
                    Method.Invoke(Args[0], new object[] { Args.ToArray() });
                    break;
                case MultipleMode.Sequencable:
                    for (int i = 0; i < Args.Count; i++)
                    {
                        Method.Invoke(Args[i], null);
                    }
                    break;
                case MultipleMode.SingleOnly:
                    Method.Invoke(Args[0], null);
                    break;
            }
        }
    }

    public class ActionCallable : MultiCallable
    {
        public List<Action> Actions = new List<Action>();
        public Action<object[]> MultiAction;

        public ActionCallable()
        {
        }

        public ActionCallable(Action action)
        {
            Actions.Add(action);
        }

        public override void Call(MultipleMode mode)
        {
            switch (mode)
            {
                case MultipleMode.NativeMulti:
                    MultiAction(Args.ToArray());
                    break;
                case MultipleMode.Sequencable:
                    foreach (var action in Actions)
                    {
                        action();
                    }
                    break;
                case MultipleMode.SingleOnly:
                    if (Actions[0] != null) Actions[0]();
                    break;
            }
        }
    }

    public class MenuItemInfo : MenuItemData
    {
        //class Arg
        //{

        //    internal AppObject AppObjArg;
        //    internal object BaseObject;
        //}

        public object BaseObject;
        public string Path;
        public string Name;
        //public List<Arg> Args = new List<Arg>();
        public Dictionary<string, MenuItemInfo> Items = new Dictionary<string, MenuItemInfo>();
        public List<MenuItemInfo> ItemsNaturalOrder = new List<MenuItemInfo>();

        public MenuItemInfo FindOrCreateItem(string[] path)
        {
            if (path.Length == 0) return this;
            if (!Items.ContainsKey(path[0]))
            {
                var newitem = new MenuItemInfo();
                newitem.Name = path[0];
                Items[path[0]] = newitem;
                ItemsNaturalOrder.Add(newitem);
            }
            return Items[path[0]].FindOrCreateItem(PyList.SliceFrom(path, 1));
        }

        public void AssignFrom(MenuItemData data)
        {
            Weight = data.Weight;
            //Method = data.Method;
            //BaseObject = data.BaseObject;
            Image = data.Image;
            Shortcut = data.Shortcut;
            ShortcutDisplayString = data.ShortcutDisplayString;
            Callable = data.Callable;
            MultiMode = data.MultiMode;
            HideIfNoChildren = data.HideIfNoChildren;
            Enabled = data.Enabled;
            GroupName = data.GroupName;
        }

        public static int CompareInfos(MenuItemInfo i1, MenuItemInfo i2)
        {
            float fres;
            int ires;
            fres = i1.GroupWeight - i2.GroupWeight;
            if (fres != 0) return Math.Sign(fres);

            ires = String.Compare(i1.GroupName, i2.GroupName);
            if (ires != 0) return ires;

            ires = i1.Weight - i2.Weight;
            if (ires != 0) return ires;
            return String.Compare(Texts.Get(i1.Name), Texts.Get(i2.Name));
        }

        public void GetMenuItems(MenuBuilder mb, List<ToolStripItem> items)
        {
            var childs = new List<MenuItemInfo>();
            if (mb.NaturalOrder)
            {
                childs.AddRange(ItemsNaturalOrder);
            }
            else
            {
                childs.AddRange(Items.Values);
                var gsum = new Dictionary<string, int>();
                var gcnt = new Dictionary<string, int>();
                foreach (var child in childs)
                {
                    gsum[child.GroupName ?? ""] = gsum.Get(child.GroupName ?? "", 0) + child.Weight;
                    gcnt[child.GroupName ?? ""] = gcnt.Get(child.GroupName ?? "", 0) + 1;
                }
                foreach (var child in childs)
                {
                    child.GroupWeight = (float)gsum[child.GroupName ?? ""] / gcnt[child.GroupName ?? ""];
                }
                childs.Sort(CompareInfos);
            }
            string lastgrp = null;
            foreach (var child in childs)
            {
                if (!child.Acceptable()) continue;
                if (lastgrp != null && child.GroupName != lastgrp && !mb.NaturalOrder)
                {
                    items.Add(new ToolStripSeparator());
                }
                items.Add(child.CreateToolItem(mb));
                lastgrp = child.GroupName;
            }
        }

        public void GetMenuItems(MenuBuilder mb, ToolStripItemCollection items)
        {
            var lst = new List<ToolStripItem>();
            GetMenuItems(mb, lst);
            foreach (var item in lst) items.Add(item);
        }

        private ToolStripMenuItem CreateToolItem(MenuBuilder mb)
        {
            ToolStripMenuItem res = new ToolStripMenuItem();
            res.Text = Texts.Get(Name);
            res.Image = Image;
            res.Enabled = Enabled;
            if (!mb.IgnoreShortcuts) res.ShortcutKeys = Shortcut;
            res.ShortcutKeyDisplayString = ShortcutDisplayString;
            res.Click += res_Click;
            GetMenuItems(mb, res.DropDownItems);
            return res;
        }

        void res_Click(object sender, EventArgs e)
        {
            if (Callable != null)
            {
                using (var ub = new UsageBuilder("popup"))
                {
                    Callable.Call(MultiMode);

                    Callable.GetUsageParams(ub);
                    if (BaseObject != null)
                    {
                        ub["objtype"] = BaseObject.GetType().FullName;
                        ub["baseobj"] = BaseObject.SafeToString();
                    }
                    ub["path"] = Path;
                }
            }
        }

        public void ProcessKeyDown(Keys keys)
        {
            if (!Acceptable()) return;
            if (!Enabled) return;
            if (Shortcut == keys) Callable.Call(MultiMode);
            foreach (var item in ItemsNaturalOrder) item.ProcessKeyDown(keys);
        }

        public void RunCommand(string cmd)
        {
            if (!Acceptable()) return;
            if (cmd == Path) Callable.Call(MultiMode);
            foreach (var item in ItemsNaturalOrder) RunCommand(cmd);
        }

        public bool Acceptable()
        {
            if (HideIfNoChildren && Items.Count == 0) return false;
            if (Callable == null) return true;
            return Callable.Acceptable(MultiMode);
        }


        public void RemoveItem(string path)
        {
            var remove = new List<string>();
            foreach (var tuple in Items)
            {
                tuple.Value.RemoveItem(path);
                if (tuple.Value.Path == path)
                {
                    remove.Add(tuple.Key);
                }
            }
            foreach (string r in remove)
            {
                ItemsNaturalOrder.Remove(Items[r]);
                Items.Remove(r);
            }
        }
    }

    public class MenuBuilder
    {
        public MenuItemInfo Root = new MenuItemInfo();
        public bool IgnoreShortcuts { get; set; }
        public bool NaturalOrder { get; set; }
        static Dictionary<Bitmap, Bitmap> m_transparencyCache = new Dictionary<Bitmap, Bitmap>();
        Dictionary<string, MenuItemInfo> m_definedActions = new Dictionary<string, MenuItemInfo>();

        private static bool FindBoolValue(string path, Dictionary<string, bool> values)
        {
            bool res = true;
            string curpath = "";
            try
            {
                foreach (var pitem in path.Split('/'))
                {
                    if (curpath != "") curpath += "/";
                    curpath += pitem;
                    if (values.ContainsKey(curpath))
                    {
                        res = values[curpath];
                    }
                }
            }
            catch (Exception)
            {
                res = true;
            }
            return res;
        }

        public void AddObject(object obj)
        {
            Dictionary<string, bool> enabled = new Dictionary<string, bool>();
            Dictionary<string, bool> visible = new Dictionary<string, bool>();
            foreach (MethodInfo mtd in obj.GetType().GetMethods())
            {
                foreach (PopupMenuEnabledAttribute attr in mtd.GetCustomAttributes(typeof(PopupMenuEnabledAttribute), true))
                {
                    enabled[attr.Path] = (bool)mtd.Invoke(obj, null);
                }
                foreach (PopupMenuVisibleAttribute attr in mtd.GetCustomAttributes(typeof(PopupMenuVisibleAttribute), true))
                {
                    visible[attr.Path] = (bool)mtd.Invoke(obj, null);
                }
            }

            foreach (MethodAttribute<PopupMenuAttribute> rec in ReflTools.GetMethods<PopupMenuAttribute>(obj))
            {
                if (!LicenseTool.FeatureAllowed(rec.Attribute.RequiredFeature)) continue;
                bool v = FindBoolValue(rec.Attribute.Path, visible);
                if (!v) continue;

                bool e = FindBoolValue(rec.Attribute.Path, enabled);

                Bitmap image = Framework.Instance.ImageFromName(rec.Attribute.ImageName, null);
                if (image != null && Framework.IsMono)
                {
                    if (!m_transparencyCache.ContainsKey(image)) m_transparencyCache[image] = image.FixTransparency(SystemColors.ButtonFace);
                    image = m_transparencyCache[image];
                }

                var data = new MenuItemData();
                data.Image = image;
                data.Weight = rec.Attribute.Weight;
                data.Shortcut = rec.Attribute.Shortcut;
                data.ShortcutDisplayString = rec.Attribute.ShortcutDisplayString;
                data.MultiMode = rec.Attribute.MultiMode;
                data.HideIfNoChildren = rec.Attribute.HideIfNoChildren;
                data.GroupName = rec.Attribute.GroupName;
                data.Callable = new MethodCallable(rec.Method);
                data.Enabled = e;
                AddItem(obj, rec.Attribute.Path, data);
            }
        }

        public MenuItemInfo AddItem(string path, Action action)
        {
            return AddItem(null, path, new MenuItemData { Callable = new ActionCallable(action) });
        }

        public MenuItemInfo AddItem(string path, Bitmap image, int weight)
        {
            return AddItem(null, path, new MenuItemData { Weight = weight, Image = image });
        }

        public MenuItemInfo AddItem(string path, Action action, Bitmap image)
        {
            return AddItem(null, path, new MenuItemData { Callable = new ActionCallable(action), Image = image });
        }

        public MenuItemInfo AddItem(string path, Action action, Bitmap image, int weight)
        {
            return AddItem(null, path, new MenuItemData { Callable = new ActionCallable(action), Image = image, Weight = weight });
        }

        public MenuItemInfo AddItem(string path, Action action, int weight)
        {
            return AddItem(null, path, new MenuItemData { Callable = new ActionCallable(action), Weight = weight });
        }

        public MenuItemInfo AddItem(object baseObject, string path, MenuItemData data)
        {
            if (m_definedActions.ContainsKey(path))
            {
                var item = m_definedActions[path];
                if (item.Callable != null) item.Callable.AddArgument(baseObject);
                return item;
            }
            else
            {
                string[] apath = path.Split('/');
                var item = Root.FindOrCreateItem(apath);
                item.AssignFrom(data);
                item.Path = path;
                item.Name = apath[apath.Length - 1];
                if (item.Callable != null) item.Callable.AddArgument(baseObject);
                m_definedActions[path] = item;
                return item;
            }
        }

        public void GetMenuItems(ToolStripItemCollection items)
        {
            Root.GetMenuItems(this, items);
        }

        public void GetMenuItems(List<ToolStripItem> items)
        {
            Root.GetMenuItems(this, items);
        }

        public void ProcessKeyDown(Keys keys)
        {
            Root.ProcessKeyDown(keys);
        }

        public void RunCommand(string cmd)
        {
            Root.RunCommand(cmd);
        }

        public void RemoveItem(string path)
        {
            m_definedActions.Remove(path);
            Root.RemoveItem(path);
        }
    }
}
