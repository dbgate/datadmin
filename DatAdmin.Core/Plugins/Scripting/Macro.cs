using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using IronPython.Hosting;
using System.ComponentModel;
using System.Collections;

namespace DatAdmin
{
    public class MacroVariable
    {
        public string VarName;
        public string VarType;
        public string DefValue;
    }

    public class Macro
    {
        string m_pyfile;

        string m_title;
        public string Title
        {
            get { return m_title; }
            set { m_title = value; }
        }

        delegate Form GetWindowDelegate();
        public void Run(Form window, Dictionary<string, object> vars)
        {
            MacroPlayer player = new MacroPlayer(window, m_pyfile, vars);
            player.Run();
        }

        public DynamicInstance CreateParamsInstance()
        {
            DynamicClass cls = new DynamicClass();
            var defvalues = new Dictionary<string, string>();
            foreach (MacroVariable var in Variables)
            {
                var d = new DynamicAttribute();
                d.Description = String.Format("{0}; {1}:{2}", var.VarType, Texts.Get("s_variable"), var.VarName);
                d.Name = var.VarName;
                d.Title = var.VarType != null ? var.VarType : "(" + var.DefValue + ")";
                cls.Attributes.Add(d);
                defvalues[var.VarName] = var.DefValue;
            }
            DynamicInstance res = cls.CreateInstance();
            foreach (string key in defvalues.Keys)
            {
                res.Data[key] = defvalues[key];
            }
            return res;
        }

        public Dictionary<string, object> GetDefaultVars()
        {
            return CreateParamsInstance().Data;
        }

        public void SaveToFile(string file, string pycode)
        {
            XmlTool.SerializeObject(file, this);
            string pyfile = Path.ChangeExtension(file, ".py");
            using (StreamWriter fw = new StreamWriter(pyfile, false))
            {
                fw.Write(pycode);
            }
        }

        public static Macro LoadFromFile(string file)
        {
            Macro res = (Macro)XmlTool.DeserializeObject(file);
            res.m_pyfile = Path.ChangeExtension(file, ".py");
            return res;
        }

        public List<MacroVariable> Variables = new List<MacroVariable>();
    }

    public class MacroPlayer : IMacroListener
    {
        Form m_window;
        string m_mywindowproc;
        string m_innerwindowproc;
        string m_pyfile;
        Dictionary<string, object> m_vars;

        public MacroPlayer(Form window, string pyfile, Dictionary<string, object> vars)
        {
            m_window = window;
            m_pyfile = pyfile;
            m_vars = vars;
        }

        public void Run()
        {
            MacroManager.Listener = this;
            try
            {
                PythonEngine engine = new PythonEngine();
                ScriptingEnv.InitializeEngine(engine);
                engine.Globals["root"] = m_window;
                engine.Globals["procedure"] = m_mywindowproc;
                engine.Globals["GetLastOpenedWindow"] = (Func<Form>)delegate() { return MacroManager.LastOpenedWindow; };
                engine.Globals["DoEvents"] = (Action) DoEvents;
                engine.Globals["SetWindowProc"] = (Action<object>)SetWindowProc;
                engine.Globals["FindNodeByPath"] = (Func<object, object, object>)FindNodeByPath;
                engine.Globals["RunPopupMenuCommand"] = (Action<object, object>)DoRunPopupMenuCommand;
                engine.Globals["GetPropertyDescriptor"] = (Func<object, object, object>)GetPropertyDescriptor;
                engine.Globals["SetProperty"] = (Action<object, object, object>)SetProperty;
                engine.Globals["GetProperty"] = (Func<object, object, object>)GetProperty;
                foreach (string var in m_vars.Keys)
                {
                    engine.Globals[var] = m_vars[var];
                }
                engine.ExecuteFile(m_pyfile);
            }
            finally
            {
                MacroManager.Listener = null;
            }
        }

        static void DoEvents()
        {
            Application.DoEvents();
            while (ProcessRegister.BgTaskCount > 0)
            {
                Application.DoEvents();
            }
        }

        static void SetProperty(object obj, object propdesc, object value)
        {
            PropertyDescriptor desc = (PropertyDescriptor)propdesc;
            desc.SetValue(obj, Convert.ChangeType(value, desc.PropertyType));
        }

        static object GetProperty(object obj, object propdesc)
        {
            PropertyDescriptor desc = (PropertyDescriptor)propdesc;
            return desc.GetValue(obj);
        }

        static object GetPropertyDescriptor(object obj, object prop)
        {
            foreach (PropertyDescriptor desc in TypeDescriptor.GetProperties(obj))
            {
                if (desc.Name == prop.ToString()) return desc;
            }
            return null;
        }

        static object FindNodeByPath(object ctrl, object path)
        {
            return TreeTool.FindNode((TreeView)ctrl, (IEnumerable)path);
        }

        static void DoRunPopupMenuCommand(object obj, object command)
        {
            if (obj is ITreeNode)
            {
                var node = (ITreeNode)obj;
                var mb = new MenuBuilder();
                node.GetPopupMenu(mb);
                mb.RunCommand(command.ToString());
            }
        }

        void SetWindowProc(object obj)
        {
            m_innerwindowproc = obj.ToString();
        }

        #region IMacroListener Members

        public DialogResult ShowDialog(Form window)
        {
            MacroPlayer player = new MacroPlayer(window, m_pyfile, m_vars);
            player.m_mywindowproc = m_innerwindowproc;
            MacroManager.Listener = null;
            MacroManager.Listener = player;
            DialogResult res = window.ShowDialog();
            MacroManager.Listener = null;
            MacroManager.Listener = this;
            return res;
        }

        public void RunDialogMacro(Form window)
        {
            if (m_mywindowproc == null) return;
            Run();
        }

        public void RunPopupMenuCommand(string cmd)
        {
        }

        public void SetPopupMenuObject(object obj)
        {
        }

        public void ExpandNode(ITreeNode node)
        {
        }
        public void DeleteNode(ITreeNode node)
        {
        }
        public void DoubleClickNode(ITreeNode node)
        {
        }
        public void DragDropNode(ITreeNode targetNode, ITreeNode draggedNode)
        {
        }
        public void DropFileIntoTree(TreeView tree, string file)
        {
        }
        public void RenameNode(ITreeNode node, string newname)
        {
        }

        #endregion
    }
}
