using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Linq;
using System.ComponentModel;

namespace DatAdmin
{
    public class MacroVarDef
    {
        public string VarName;
        public string VarType;
        public string DefValue;
        public object Sender;

        public MacroVariable CreateVariable()
        {
            return new MacroVariable { VarName = VarName, VarType = VarType, DefValue = DefValue };
        }
    }

    public class MacroRecorder : IMacroListener
    {
        StringBuilder m_data;
        PythonModule m_module;
        string m_funcname;

        string m_lastLine;
        object m_lastAdder;
        string m_lastAddEvent;

        List<MacroVarDef> m_vardefs;

        class ControlWrapper
        {
            internal Control ctrl;
            internal Control parent;
            internal bool isvar;
            internal string varname;
            internal string vardef;
        }
        Dictionary<Control, ControlWrapper> m_ctrls = new Dictionary<Control, ControlWrapper>();

        class MenuWrapper
        {
            internal ToolStripItem item;
            internal MenuWrapper parent;
            internal ControlWrapper cparent;
            internal bool isvar;
            internal string varname;
            internal string vardef;
            internal bool hasevent;
        }
        Dictionary<ToolStripItem, MenuWrapper> m_menus = new Dictionary<ToolStripItem, MenuWrapper>();

        class ObjectWrapper
        {
            internal object obj;
            internal ControlWrapper cparent;
            internal bool isvar;
            internal string varname;
            internal string vardef;
        }
        Dictionary<object, ObjectWrapper> m_objs = new Dictionary<object, ObjectWrapper>();

        Dictionary<Form, string> m_openedWindows = new Dictionary<Form, string>();

        //Dictionary<String, bool> m_ctrlSet = new Dictionary<String, bool>();

        //List<ToolStripItem> m_menuItems = new List<ToolStripItem>();
        //Dictionary<ToolStripItem, String> m_menuVarNames = new Dictionary<ToolStripItem, string>();
        //Dictionary<String, bool> m_menuSet = new Dictionary<String, bool>();

        Form m_rootWindow;

        bool m_running = false;

        internal MacroRecorder(PythonModule module, Form rootWindow, string funcname, List<MacroVarDef> vardefs)
        {
            m_vardefs = vardefs ?? new List<MacroVarDef>();
            if (funcname == null) funcname = module.GenerateFuncName();
            m_funcname = funcname;
            m_module = module;
            m_data = module.AddFunction(funcname);
            m_rootWindow = rootWindow;
            MacroManager.OpenedWindow += new FormDelegate(MacroManager_OpenedWindow);
            RegisterControlHooks(rootWindow);
        }

        public MacroRecorder(Form rootWindow) : this(new PythonModule(), rootWindow, "main", null)
        {
            MacroManager.Listener = this;
        }


        public void AddCommand(string cmd)
        {
            AddCommand(cmd, null, null);
        }

        public void AddCommand(string cmd, object adder, string addevent)
        {
            bool replace = false;
            if (adder != null && addevent != null && adder == m_lastAdder && addevent == m_lastAddEvent) replace = true;
            if (!replace) FlushCommand();
            m_lastLine = cmd;
            m_lastAdder = adder;
            m_lastAddEvent = addevent;
        }

        private void FlushCommand()
        {
            if (m_lastLine != null)
            {
                m_data.Append(m_lastLine);
                m_lastAdder = null;
                m_lastAddEvent = null;
                m_lastLine = null;
            }
        }

        void MacroManager_OpenedWindow(Form window)
        {
            string varname = "win" + (m_openedWindows.Count + 1).ToString();
            m_openedWindows[window] = varname;
            AddCommand(String.Format("    {0} = GetLastOpenedWindow()\n", varname));
            RegisterControlHooks(window);
        }

        private void AddControl(Control ctrl, Control parent)
        {
            if (parent is PropertyGrid) return;
            ControlWrapper wrap = new ControlWrapper();
            m_ctrls[ctrl] = wrap;
            wrap.ctrl = ctrl;
            wrap.varname = "ctrl" + m_ctrls.Count.ToString();
            wrap.parent = parent;
            string parexpr = null;
            if (parent == m_rootWindow) parexpr = "root";
            else if (parent is Form && m_openedWindows.ContainsKey((Form)parent)) parexpr = m_openedWindows[(Form)parent];
            else parexpr = m_ctrls[parent].varname;
            bool processed = false;
            if (parent is SplitContainer)
            {
                if (ctrl == ((SplitContainer)parent).Panel1) wrap.vardef = String.Format("    {0} = {1}.Panel1\n", wrap.varname, parexpr);
                else wrap.vardef = String.Format("    {0} = {1}.Panel2\n", wrap.varname, parexpr);
                processed = true;
            }
            if (parent is TabControl && ctrl is TabPage)
            {
                TabPage page = (TabPage)ctrl;
                TabControl tabs = (TabControl)parent;
                if (String.IsNullOrEmpty(page.Name))
                {
                    wrap.vardef = String.Format("    {0} = {1}.TabPages[{2}]\n", wrap.varname, parexpr, tabs.TabPages.IndexOf(page));
                    processed = true;
                }
            }
            if (!processed)
            {
                wrap.vardef = String.Format("    {0} = {1}.Controls['{2}']\n", wrap.varname, parexpr, ctrl.Name);
            }
            RegisterControlHooks(ctrl);
        }

        private void WantControlVar(ControlWrapper wrap)
        {
            if (!wrap.isvar)
            {
                bool isform = wrap.parent is Form && m_openedWindows.ContainsKey((Form)wrap.parent);
                if (wrap.parent != m_rootWindow && !isform) WantControlVar(m_ctrls[wrap.parent]);
                AddCommand(wrap.vardef);
                wrap.isvar = true;
            }
        }

        private string GetObjectExpression(object obj, bool wantVar)
        {
            if (!m_objs.ContainsKey(obj)) AddObject(obj);
            ObjectWrapper wrap = m_objs[obj];
            if (wantVar) WantObjectVar(wrap);
            return wrap.varname;
        }

        private void WantObjectVar(ObjectWrapper wrap)
        {
            if (!wrap.isvar)
            {
                if (wrap.cparent != null) WantControlVar(wrap.cparent);
                AddCommand(wrap.vardef);
                wrap.isvar = true;
            }
        }

        private string GetControlExpression(Control ctrl, bool wantVar)
        {
            if (ctrl == m_rootWindow) return "root";
            if (ctrl is Form && m_openedWindows.ContainsKey((Form)ctrl)) return m_openedWindows[(Form)ctrl];
            ControlWrapper wrap = m_ctrls[ctrl];
            if (wantVar) WantControlVar(wrap);
            return wrap.varname;
        }

        private void RegisterControlHooks(Control ctrl)
        {
            if (ctrl == null) return;
            foreach (Control child in ctrl.Controls)
            {
                AddControl(child, ctrl);
            }

            if (ctrl is ToolStrip)
            {
                ToolStrip obj = (ToolStrip)ctrl;
                foreach (ToolStripItem item in obj.Items)
                {
                    AddToolStripItem(item, GetControlExpression(ctrl, false) + ".Items", null, m_ctrls[ctrl]);
                    //if (item is ToolStripButton)
                    //{
                    //    ToolStripButton iobj = (ToolStripButton)item;
                    //    iobj.Click += new EventHandler(ToolStripButton_Click);
                    //    AddToolStripItem(iobj, GetControlExpression(ctrl, false) +  ".Items", null, m_ctrls[ctrl]);
                    //}
                }
            }

            if (ctrl is Button)
            {
                Button obj = (Button)ctrl;
                obj.Click += new EventHandler(Button_Click);
            }

            if (ctrl is Form)
            {
                Form obj = (Form)ctrl;
                obj.FormClosed += new FormClosedEventHandler(obj_FormClosed);
            }

            //if (ctrl is Form)
            //{
            //    Form obj = (Form)ctrl;
            //    if (obj.MainMenuStrip != null)
            //    {
            //        foreach (ToolStripItem item in obj.MainMenuStrip.Items)
            //        {
            //            AddToolStripItem(item, "root.MainMenuStrip.Items", null, null);
            //        }
            //    }
            //    obj.FormClosed += new FormClosedEventHandler(obj_FormClosed);
            //}

            if (ctrl is TextBox)
            {
                TextBox obj = (TextBox)ctrl;
                obj.TextChanged += new EventHandler(obj_TextChanged);
            }
            if (ctrl is ListView)
            {
                ListView obj = (ListView)ctrl;
                obj.SelectedIndexChanged += new EventHandler(obj_SelectedIndexChanged);
            }
            if (ctrl is PropertyGrid)
            {
                PropertyGrid obj = (PropertyGrid)ctrl;
                obj.PropertyValueChanged += new PropertyValueChangedEventHandler(obj_PropertyValueChanged);
            }
        }

        void obj_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            GridItem gi = e.ChangedItem;
            List<PropertyDescriptor> desc = new List<PropertyDescriptor>();
            while (gi != null)
            {
                if (gi.PropertyDescriptor != null) desc.Add(gi.PropertyDescriptor);
                gi = gi.Parent;
            }
            desc.Reverse();
            AddCommand(String.Format("    pobj = {0}.SelectedObject\n", GetControlExpression((Control)s, true)));
            for (int i = 0; i < desc.Count - 1; i++)
            {
                AddCommand(String.Format("    pdesc = GetPropertyDescriptor(pobj, '{0}'); pobj = GetProperty(pobj, pdesc)\n", desc[i].Name));
            }
            PropertyDescriptor last = desc[desc.Count - 1];
            AddCommand(String.Format("    pdesc = GetPropertyDescriptor(pobj, '{0}'); SetProperty(pobj, pdesc, '{1}')\n", last.Name, e.ChangedItem.Value == null ? "None" : e.ChangedItem.Value.ToString()));
        }

        private void AddToolStripItem(ToolStripItem item, string childsCollection, MenuWrapper parent, ControlWrapper cparent)
        {
            MenuWrapper wrap = new MenuWrapper();
            m_menus[item] = wrap;
            wrap.varname = "menu" + m_menus.Count.ToString();
            wrap.item = item;
            wrap.parent = parent;
            wrap.cparent = cparent;
            wrap.vardef = String.Format("    {0} = {1}['{2}']\n", wrap.varname, childsCollection, item.Name); 
            
            int childCount = 0;
            if (item is ToolStripMenuItem)
            {
                ToolStripMenuItem obj = (ToolStripMenuItem)item;
                foreach (ToolStripItem child in obj.DropDownItems)
                {
                    childCount ++;
                    AddToolStripItem(child, wrap.varname + ".DropDownItems", wrap, null);
                }
            }
            if (childCount == 0)
            {
                wrap.hasevent = true;
                item.Click += new EventHandler(ToolStripItem_Click);
            }
        }

        private void AddObject(object obj)
        {
            if (obj is ITreeNode)
            {
                ITreeNode node = (ITreeNode)obj;
                DATreeNode realNode = (DATreeNode)node.RealNode;
                TreeView tree = realNode.TreeView;
                ObjectWrapper wrap = new ObjectWrapper();
                m_objs[obj] = wrap;
                wrap.varname = "obj" + m_objs.Count.ToString();
                wrap.obj = obj;
                wrap.cparent = m_ctrls[tree];

                List<string> pathvars = new List<string>();
                ITreeNode cur = node;
                while (cur != null && cur.Parent != null)
                {
                    pathvars.Add(AddVariable(cur.TypeTitle, cur.Name, cur));
                    cur = cur.Parent;
                }
                pathvars.Reverse();

                wrap.vardef = String.Format("    {0} = FindNodeByPath({1}, [{2}])\n", wrap.varname, GetControlExpression(tree, false), String.Join(",", pathvars.ToArray()));
            }
        }

        private void WantMenuVar(MenuWrapper wrap)
        {
            if (!wrap.isvar)
            {
                if (wrap.parent != null) WantMenuVar(wrap.parent);
                if (wrap.cparent != null) WantControlVar(wrap.cparent);
                AddCommand(wrap.vardef);
                wrap.isvar = true;
            }
        }

        private string GetMenuExpression(ToolStripItem item)
        {
            MenuWrapper wrap = m_menus[item];
            WantMenuVar(wrap);
            return wrap.varname;
        }

        private void RecordToolStripEvent(object sender)
        {
            AddCommand(String.Format("    {0}.PerformClick();DoEvents()\n", GetMenuExpression((ToolStripItem)sender)));
        }

        private void ToolStripItem_Click(object sender, EventArgs e)
        {
            if (m_running)
            {
                RecordToolStripEvent(sender);
            }
        }

        private void RecordControlEvent(object sender, string methodCall, string addevent)
        {
            AddCommand(String.Format("    {0}.{1};DoEvents()\n", GetControlExpression((Control)sender, true), methodCall), sender, addevent);
        }

        void obj_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (m_running)
            {
                RecordControlEvent(sender, "Close()", null);
            }
        }

        void obj_TextChanged(object sender, EventArgs e)
        {
            if (m_running)
            {
                TextBox obj = (TextBox)sender;
                if (!obj.ReadOnly)
                {
                    string varname = AddVariable(obj.Tag != null ? obj.Tag.ToString() : (string)null, obj.Text, sender);
                    //RecordControlEvent(sender, String.Format("Text = '{0}'", obj.Text.Replace("'", "\'").Replace("\\", "\\\\")), "settext");
                    RecordControlEvent(sender, String.Format("Text = {0}", varname), "settext");
                }
            }
        }

        private string AddVariable(string vartype, string defvalue, object sender)
        {
            if (sender != null)
            {
                foreach (MacroVarDef vd in m_vardefs)
                {
                    if (vd.Sender == sender)
                    {
                        vd.DefValue = defvalue;
                        return vd.VarName;
                    }
                }
            }
            MacroVarDef res = new MacroVarDef();
            m_vardefs.Add(res);
            res.DefValue = defvalue;
            res.VarName = "var" + (m_vardefs.Count + 1).ToString();
            res.VarType = vartype;
            res.Sender = sender;
            return res.VarName;
        }

        void obj_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (m_running)
            {
                ListView obj = (ListView)sender;
                ListViewItem cur = obj.FocusedItem;
                string expr = GetControlExpression(obj, true);
                AddCommand(String.Format("    if {0}.FocusedItem is not None: {0}.FocusedItem.Selected = False\n", expr));
                AddCommand(String.Format("    {0}.FocusedItem = {0}.Items.Find('{1}', False)[0]; DoEvents()\n", expr, cur.Name));
                AddCommand(String.Format("    {0}.FocusedItem.Selected = True; DoEvents()\n", expr));
            }
        }

        //private void ToolStripButton_Click(object sender, EventArgs e)
        //{
        //    if (m_running)
        //    {
        //        RecordToolStripEvent(sender);
        //    }
        //}

        private void Button_Click(object sender, EventArgs e)
        {
            if (m_running)
            {
                RecordControlEvent(sender, "PerformClick()", null);
            }
        }

        public void Start()
        {
            m_running = true;
        }

        public void Pause()
        {
            m_running = false;
        }

        public bool IsRunning { get { return m_running; } }

        public void SaveToFile(string fileName)
        {
            Macro m = new Macro();
            m.Title = Path.GetFileNameWithoutExtension(fileName);
            m.Variables.AddRange(from v in m_vardefs select v.CreateVariable());
            m.SaveToFile(fileName, m_module.ToString());
        }

        public void Close()
        {
            foreach (Control ctrl in m_ctrls.Keys)
            {
                if (ctrl is Button)
                {
                    Button obj = (Button)ctrl;
                    obj.Click -= new EventHandler(Button_Click);
                }
                //if (ctrl is ToolStrip)
                //{
                //    ToolStrip obj = (ToolStrip)ctrl;
                //    foreach (ToolStripItem item in obj.Items)
                //    {
                //        if (item is ToolStripButton)
                //        {
                //            ToolStripButton iobj = (ToolStripButton)item;
                //            iobj.Click -= new EventHandler(ToolStripButton_Click);
                //        }
                //    }
                //}
                if (ctrl is Form)
                {
                    Form obj = (Form)ctrl;
                    obj.FormClosed -= new FormClosedEventHandler(obj_FormClosed);
                }
                if (ctrl is TextBox)
                {
                    TextBox obj = (TextBox)ctrl;
                    obj.TextChanged -= new EventHandler(obj_TextChanged);
                }
                if (ctrl is ListView)
                {
                    ListView obj = (ListView)ctrl;
                    obj.SelectedIndexChanged -= new EventHandler(obj_SelectedIndexChanged);
                }
                if (ctrl is PropertyGrid)
                {
                    PropertyGrid obj = (PropertyGrid)ctrl;
                    obj.PropertyValueChanged -= new PropertyValueChangedEventHandler(obj_PropertyValueChanged);
                }
            }
            foreach (MenuWrapper wrap in m_menus.Values)
            {
                if (wrap.hasevent)
                {
                    wrap.item.Click -= new EventHandler(ToolStripItem_Click);
                }
            }
            MacroManager.OpenedWindow -= new FormDelegate(MacroManager_OpenedWindow);
            MacroManager.Listener = null;
            FlushCommand();
            m_module.EndFunction(m_data);
        }

        #region IMacroListener Members

        public void RunDialogMacro(Form window)
        {
        }

        public DialogResult ShowDialog(Form window)
        {
            MacroRecorder inner = new MacroRecorder(m_module, window, null, m_vardefs);
            AddCommand(String.Format("    SetWindowProc('{0}')\n", inner.m_funcname));
            MacroManager.Listener = null;
            MacroManager.Listener = inner;
            inner.Start();
            DialogResult res;
            try
            {
                res = window.ShowDialog();
            }
            finally
            {
                inner.Close();
            }
            MacroManager.Listener = this;
            return res;
        }

        public void SetPopupMenuObject(object obj)
        {
            AddCommand(String.Format("    popupMenuObject = {0}\n", GetObjectExpression(obj, true)));
        }

        public void RunPopupMenuCommand(string path)
        {
            AddCommand(String.Format("    RunPopupMenuCommand(popupMenuObject, '{0}'); DoEvents()\n", path));
        }

        public void ExpandNode(ITreeNode node)
        {
            AddCommand(String.Format("    {0}.RealNode.Expand(); DoEvents()\n", GetObjectExpression(node, true)));
        }
        public void DeleteNode(ITreeNode node)
        {
            AddCommand(String.Format("    {0}.Delete(); DoEvents()\n", GetObjectExpression(node, true)));
        }
        public void DoubleClickNode(ITreeNode node)
        {
            AddCommand(String.Format("    {0}.DoubleClick(); DoEvents()\n", GetObjectExpression(node, true)));
        }
        public void DragDropNode(ITreeNode targetNode, ITreeNode draggedNode)
        {
            AddCommand(String.Format("    {0}.DragDrop({1}); DoEvents()\n", GetObjectExpression(targetNode, true), GetObjectExpression(draggedNode, true)));
        }
        public void DropFileIntoTree(TreeView tree, string file)
        {

        }
        public void RenameNode(ITreeNode node, string newname)
        {
            AddCommand(String.Format("    {0}.RenameNode('{1}'); DoEvents()\n", GetObjectExpression(node, true), newname));
        }

        #endregion
    }

    class PythonModule
    {
        List<StringBuilder> m_functions = new List<StringBuilder>();
        Dictionary<string, StringBuilder> m_funcByName = new Dictionary<string, StringBuilder>();
        List<StringBuilder> m_functionEnds = new List<StringBuilder>();
        int funcCount = 0;

        internal string GenerateFuncName()
        {
            lock (this)
            {
                funcCount++;
                return "func" + funcCount.ToString();
            }
        }
        internal StringBuilder AddFunction(String funcname)
        {
            StringBuilder res = new StringBuilder();
            res.AppendFormat("def {0}():\n    pass\n", funcname);
            m_functions.Add(res);
            m_funcByName[funcname] = res;
            return res;
        }
        internal void EndFunction(StringBuilder func)
        {
            m_functionEnds.Add(func);
        }
        public override string ToString()
        {
            StringBuilder res = new StringBuilder();
            foreach (StringBuilder fend in m_functionEnds)
            {
                res.Append(fend.ToString());
                res.Append("\n");
            }
            //res.Append("if __name__ == '__main__':\n");
            res.Append("if procedure is None: main()\n");
            foreach (string fname in m_funcByName.Keys)
            {
                res.AppendFormat("if procedure == '{0}': {0}()\n", fname);
            }
            return res.ToString();
        }
    }
}
