using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using System.Collections;
using System.IO;

namespace DatAdmin
{
    public abstract class AppObject : AddonBase, IConnectionPackHolder
    {
        public event EventHandler CompleteChanged;
        object m_owner;

        public virtual void GetAppObjectProperties(Dictionary<string, string> props) { }
        public virtual ObjectFilterBase GetFilter()
        {
            var res = new AppObjectFilter();
            res.ObjectType.PredefinedValue = TypeName;
            return res;
        }

        [Browsable(false)]
        public abstract Bitmap Image { get; }
        [Browsable(false)]
        public abstract string TypeTitle { get; }
        [Browsable(false)]
        public abstract string TypeName { get; }
        [Browsable(false)]
        public ConnectionPack ConnPack { get; set; }

        List<object> m_appobjExtenders = new List<object>();

        public bool DisableAutoConnect;

        public virtual ITabularDataView GetTabularData(ConnectionPack connpack) { return null; }
        public virtual bool HasTabularData { get { return false; } }
        public virtual IPhysicalConnectionFactory GetConnection() { return null; }
        public virtual ObjectPath GetObjectPath() { return null; }
        public virtual ITableSource TableSource { get { return null; } }

        public Dictionary<string, string> GetAppObjectProperties()
        {
            var props = new Dictionary<string, string>();
            props["objtype"] = TypeName;
            GetAppObjectProperties(props);
            return props;
        }

        public AppObject()
        {
            InitializeExtenders();
        }

        public override AddonType AddonType
        {
            get { return AppObjectAddonType.Instance; }
        }

        private void InitializeExtenders()
        {
            foreach (var hold in AppObjectExtenderAddonType.Instance.CommonSpace.GetAllAddons())
            {
                var ext = hold.InstanceModel as IAppObjectExtender;
                ext.GetAppObjectExtendObjects(this, m_appobjExtenders);
            }
        }

        public void GetWidgetsEx(List<IWidget> res, ConnectionPack connpack)
        {
            GetWidgets(res);
            var conn = connpack.GetConnection(GetConnection(), false);
            if (conn != null)
            {
                var dialect = conn.Dialect;
                if (dialect != null) dialect.GetAdditionalWidgets(res, this);
            }
        }

        public virtual void GetWidgets(List<IWidget> res)
        {
        }

        public virtual bool DefaultAction()
        {
            return false;
        }

        public object Owner
        {
            get
            {
                return m_owner;
            }
            set
            {
                if (m_owner != null && m_owner != value) throw new InternalError("DAE-00013 Onwer of appobject allready set");
                if (value == null) throw new InternalError("DAE-00014 Owner cannot be null");
                m_owner = value;
            }
        }

        public void CallCompleteChanged()
        {
            if (CompleteChanged != null) CompleteChanged(this, EventArgs.Empty);
        }

        public virtual void GetPopupMenu(MenuBuilder mb)
        {
            mb.AddObject(this);
            foreach (var ext in m_appobjExtenders) mb.AddObject(ext);
            if (CustomDashboardsFeature.Allowed)
            {
                foreach (var dash in DashboardManager.Instance.GetDashboards(this))
                {
                    var d = dash as DockPanelDashboard;
                    if (d == null) continue;
                    mb.AddItem("s_advanced/s_open_dashboard/" + Path.GetFileNameWithoutExtension(d.AddonFileName), d.OpenAsNewWindowDelegate(this));
                }
            }
            var bld = GetDragDropBuilder(ObjectClipboard.Memory);
            if (bld.OperationCount() > 0)
            {
                var mi = mb.AddItem("s_clipboard", null);
                mi.Image = CoreIcons.paste;
                mi.GroupName = "node";

                var mt = mb.AddItem("s_clipboard/(" + ObjectClipboard.GetClipboardText() + ")", null);
                mt.Image = ObjectClipboard.GetClipboardImage();
                mt.GroupName = "title";
                mt.Weight = -100;

                bld.GetMenuItems(mb, "s_clipboard/");
            }
        }

        [PopupMenuVisible("s_advanced/s_open_dashboard")]
        public bool _OpenDashboardVisible()
        {
            if (!CustomDashboardsFeature.Allowed) return false;
            foreach (var dash in DashboardManager.Instance.GetDashboards(this))
            {
                var d = dash as DockPanelDashboard;
                if (d == null) continue;
                return true;
            }
            return false;
        }

        [PopupMenu("s_advanced/s_open_dashboard", ImageName = CoreIcons.dashboardName, GroupName = "dashboard", RequiredFeature = CustomDashboardsFeature.Test)]
        public void _OpenDashboard()
        {
        }

        public virtual bool AllowDelete()
        {
            return false;
        }

        public virtual void DoDelete()
        {
        }

        public virtual bool AllowRename()
        {
            return false;
        }

        public virtual string GetRenamingText()
        {
            return ToString();
        }

        public virtual void RenameObject(string newname)
        {
            throw new InternalError("DAE-00015 rename not supported");
        }

        //protected virtual void GetSqlGeneratorsEx(List<ISqlGenerator> res)
        //{
        //}

        public List<IAppObjectSqlGenerator> GetSqlGenerators()
        {
            var res = new List<IAppObjectSqlGenerator>();
            foreach (var holder in AppObjectSqlGeneratorAddonType.Instance.CommonSpace.GetFilteredAddons(RegisterItemUsage.DirectUse))
            {
                var inst = (IAppObjectSqlGenerator)holder.CreateInstance();
                if (!inst.SuitableFor(this)) continue;
                res.Add(inst);
            }
            //GetSqlGeneratorsEx(res);
            return res;
        }

        [PopupMenuVisible("s_generate_sql")]
        public bool GenerateSqlVisible()
        {
            return GetSqlGenerators().Count > 0;
        }

        [PopupMenu("s_generate_sql", ImageName = CoreIcons.generate_sqlName, MultiMode = MultipleMode.NativeMulti, GroupName = "sql", Shortcut = Keys.Control | Keys.G)]
        public void GenerateSql(object[] objs)
        {
            GenerateSqlForm.Run(AppObject.ToArray(objs));
        }

        public static AppObject[] ToArray(IEnumerable objs)
        {
            var res = new List<AppObject>();
            foreach (AppObject obj in objs) res.Add(obj);
            return res.ToArray();
        }

        public virtual FullDatabaseRelatedName GetFullDatabaseRelatedName()
        {
            return null;
        }

        public virtual bool AllowDesign()
        {
            return false;
        }

        public virtual void DoDesign()
        {
        }

        public virtual string GetTreePath()
        {
            return null;
        }

        [PopupMenu("s_advanced", ImageName = CoreIcons.advancedName, HideIfNoChildren = true, MultiMode = MultipleMode.Sequencable)]
        public void _AdvancedMenu() { }

        [PopupMenuVisible("s_advanced/s_new_dashboard")]
        public virtual bool NewDashboardVisible()
        {
            return false;
        }

        [PopupMenu("s_advanced/s_new_dashboard", ImageName = CoreIcons.dashboardName, GroupName = "dashboard", RequiredFeature = CustomDashboardsFeature.Test)]
        public void NewDashboard()
        {
            string dname = InputBox.Run("s_type_new_dashboard_name", "new dashboard");
            if (dname == null) return;
            string fn = Path.Combine(Core.DashboardsDirectory, dname + ".das");
            if (File.Exists(fn))
            {
                if (!StdDialog.ReallyOverwriteFile(fn)) return;
            }
            var dash = new DockPanelDashboard(fn);
            dash.Filter = GetFilter();
            dash.SaveToFile(fn);
            //dash.EnableDesign(true);
            DashboardManager.Instance.Addons.Add(dash);
            var pars = new DashboardInstanceParams { LayoutName = null };
            var win = dash.CreateControl(pars) as DashboardFrame;
            win.SetSelectedObject(this);
            MainWindow.Instance.OpenContent(win);
            MainWindow.Instance.ShowDocker(new PropertiesDockerFactory());
            MainWindow.Instance.ShowDocker(new ToolboxDockerFactory());
            dash.SetDesignFrame(win);
        }

        public virtual bool SupportSerialize
        {
            get
            {
                return XmlTool.GetRegisterAttr(this) != null;
            }
        }

        public DragDropBuilder GetDragDropBuilder(AppObject[] draggingObjects)
        {
            var bld = new DragDropBuilder();
            foreach (var obj in GetDragDropOperationObjects())
            {
                bld.AddObject(obj, draggingObjects);
            }
            return bld;
        }

        public IEnumerable<object> GetDragDropOperationObjects()
        {
            yield return this;
            foreach (var obj in m_appobjExtenders) yield return obj;
        }

        public virtual bool AllowDragDrop(AppObject[] draggingObjects)
        {
            var bld = GetDragDropBuilder(draggingObjects);
            return bld.ContainsOperation();
        }

        public virtual void DragDrop(AppObject[] draggingObjects)
        {
            var bld = GetDragDropBuilder(draggingObjects);
            bld.ShowMenu();
        }

        protected virtual string GetConfirmDeleteMessage()
        {
            return Texts.Get("s_really_delete$object", "object", ToString());
        }

        public void DeleteWithQueryVoid()
        {
            DeleteWithQuery();
        }

        public bool DeleteWithQuery()
        {
            if (StdDialog.YesNoDialog(GetConfirmDeleteMessage()))
            {
                DoDelete();
                return true;
            }
            return false;
        }

        public void RenameWithQueryVoid()
        {
            RenameWithQuery();
        }

        public bool RenameWithQuery()
        {
            string newname = InputBox.Run(Texts.Get("s_new_name"), GetRenamingText());
            if (newname != null)
            {
                RenameObject(newname);
                return true;
            }
            return false;
        }

        public virtual AppObject Clone()
        {
            return this.CloneUsingXml();
        }

        public virtual string GetFileFriendlySignature()
        {
            return IOTool.CreateFileFriendlyName(GetType().FullName + "_" + ToString());
        }

        public bool OpenTheBestDashboard()
        {
            if (!CustomDashboardsFeature.Allowed) return false;
            var lst = new List<DockPanelDashboard>();
            foreach (var dash in DashboardManager.Instance.GetDashboards(this))
            {
                var d = dash as DockPanelDashboard;
                if (d == null) continue;
                lst.Add(d);
            }
            if (lst.Count == 0) return false;
            lst.SortByKey(d => -d.Priority);
            lst[0].OpenAsNewWindow(this);
            return true;
        }

        public virtual void ModifiedDoubleClick(Keys keys)
        {
            if (keys == Keys.Control) this.OpenTheBestDashboard();
        }

        public virtual ISqlDialect Dialect
        {
            get { return null; }
        }

        public virtual string GetDatabaseName()
        {
            return null;
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class AppObjectAttribute : RegisterAttribute
    {
    }

    [AddonType]
    public class AppObjectAddonType : AddonType
    {
        public override string Name
        {
            get { return "appobject"; }
        }

        public override Type InterfaceType
        {
            get { return typeof(AppObject); }
        }

        public override Type RegisterAttributeType
        {
            get { return typeof(AppObjectAttribute); }
        }

        public static readonly AppObjectAddonType Instance = new AppObjectAddonType();
    }
}
