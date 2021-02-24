using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Windows.Forms;
using System.Linq;

namespace DatAdmin
{
    public class MethodDragDropOperation
    {
        MethodInfo m_mtd;
        DragDropOperationAttribute m_attr;
        object m_baseObject;
        public List<AppObject> Args = new List<AppObject>();

        public MethodDragDropOperation(MethodInfo mtd, DragDropOperationAttribute attr, object baseObject,  IEnumerable<AppObject> objs)
        {
            m_mtd = mtd;
            m_attr = attr;
            m_baseObject = baseObject;
            Args.AddRange(objs);
        }

        public bool Acceptable()
        {
            switch (m_attr.MultiMode)
            {
                case MultipleMode.NativeMulti:
                case MultipleMode.Sequencable:
                    return Args.Count > 0;
                case MultipleMode.SingleOnly:
                    return Args.Count == 1;
            }
            return false;
        }

        public string Title { get { return m_attr.Title; } }

        public void OnClick(object sender, EventArgs empty)
        {
            OnClickNoArg();
        }

        public void OnClickNoArg()
        {
            using (var ub = new UsageBuilder("dragdrop"))
            {
                if (m_baseObject != null)
                {
                    ub["dsttype"] = m_baseObject.GetType().FullName;
                    ub["dstobj"] = m_baseObject.SafeToString();
                }
                ub["name"] = m_attr.Name;
                ub["method"] = m_mtd.Name;

                ub["dragargs"] = Args.Count.ToString();
                if (Args.Count >= 1)
                {
                    ub["dragtype0"] = Args[0] != null ? Args[0].GetType().FullName : "null";
                    ub["drag0"] = Args[0].SafeToString();
                }
            }
            switch (m_attr.MultiMode)
            {
                case MultipleMode.NativeMulti:
                    m_mtd.Invoke(m_baseObject, new object[] { Args.ToArray() });
                    break;
                case MultipleMode.Sequencable:
                case MultipleMode.SingleOnly:
                    foreach (var arg in Args)
                    {
                        m_mtd.Invoke(m_baseObject, new object[] { arg });
                        if (m_attr.MultiMode == MultipleMode.SingleOnly) break;
                    }
                    break;
            }
        }
    }

    public class DragDropBuilder
    {
        public List<MethodDragDropOperation> Operations = new List<MethodDragDropOperation>();
        //public Dictionary<string, MethodDragDropOperation> OpsById = new Dictionary<string, MethodDragDropOperation>();

        public void AddObject(object baseObject, AppObject[] draggedObjs)
        {
            if (draggedObjs == null) return;
            foreach (MethodInfo mtd in baseObject.GetType().GetMethods())
            {
                foreach (DragDropOperationAttribute attr in mtd.GetCustomAttributes(typeof(DragDropOperationAttribute), true))
                {
                    ProcessOperation(baseObject, draggedObjs, mtd, attr);
                }
            }
        }

        private void ProcessOperation(object baseObject, AppObject[] draggedObjs, MethodInfo mtd, DragDropOperationAttribute attr)
        {
            if (!LicenseTool.FeatureAllowed(attr.RequiredFeature)) return;
            if (attr.MultiMode == MultipleMode.NativeMulti)
            {
                var okobjs = new List<AppObject>(draggedObjs);
                foreach (MethodInfo vmtd in baseObject.GetType().GetMethods())
                {
                    foreach (DragDropOperationFilterMultiAttribute fattr in vmtd.GetCustomAttributes(typeof(DragDropOperationFilterMultiAttribute), true))
                    {
                        if (fattr.Name == attr.Name)
                        {
                            vmtd.Invoke(baseObject, new object[] { okobjs });
                        }
                    }
                }
                var op = new MethodDragDropOperation(mtd, attr, baseObject, okobjs);
                if (op.Acceptable()) Operations.Add(op);
                return;
            }
            else
            {
                var okobjs = new List<AppObject>(draggedObjs);
                foreach (MethodInfo vmtd in baseObject.GetType().GetMethods())
                {
                    foreach (DragDropOperationVisibleAttribute vattr in vmtd.GetCustomAttributes(typeof(DragDropOperationVisibleAttribute), true))
                    {
                        if (vattr.Name == attr.Name)
                        {
                            okobjs.Clear();
                            foreach (var appobj in draggedObjs)
                            {
                                if ((bool)vmtd.Invoke(baseObject, new object[] { appobj }))
                                {
                                    okobjs.Add(appobj);
                                }
                            }
                        }
                    }
                }
                var op = new MethodDragDropOperation(mtd, attr, baseObject, okobjs);
                if (op.Acceptable()) Operations.Add(op);
                return;
            }
        }

        public bool ContainsOperation()
        {
            foreach (var op in Operations)
            {
                if (op.Acceptable()) return true;
            }
            return false;
        }

        public int OperationCount()
        {
            return Operations.Count(op => op.Acceptable());
        }

        public void GetMenuItems(ToolStripItemCollection items)
        {
            foreach (var op in Operations)
            {
                if (!op.Acceptable()) continue;
                var item = items.Add(Texts.Get(op.Title));
                item.Click += op.OnClick;
            }
        }

        public void ShowMenu()
        {
            ContextMenuStrip menu = new ContextMenuStrip();
            GetMenuItems(menu.Items);

            menu.Items.Add(new ToolStripSeparator());
            var canc = menu.Items.Add(Texts.Get("s_cancel"));
            canc.Click += DoNothing;

            menu.ShowOnCursor();
        }

        private void DoNothing(object sender, EventArgs e) { }

        public void GetMenuItems(MenuBuilder mb, string pathPrefix)
        {
            foreach (var op in Operations)
            {
                if (!op.Acceptable()) continue;
                mb.AddItem(pathPrefix + op.Title, op.OnClickNoArg);
            }
        }
    }
}
