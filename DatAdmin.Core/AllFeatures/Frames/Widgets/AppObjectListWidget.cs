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
    public class ListWidgetColumn
    {
        public ListWidgetColumn()
        {
            Width = 120;
        }

        [XmlElem]
        public string HeaderText { get; set; }
        [XmlElem]
        public string DataSource { get; set; }
        [XmlElem]
        public int Width { get; set; }
    }

    public class ListWidgetColumnCollection : ListProxy<ListWidgetColumn>
    {
        AppObjectListWidget m_widget;

        public ListWidgetColumnCollection(AppObjectListWidget widget)
        {
            m_widget = widget;
        }

        protected override void OnChanged()
        {
            base.OnChanged();
        }
    }

    public class ListWidgetCaps
    {
        public bool CreateNew = false;
        public bool Delete = true;
        public bool Rename = true;
        public bool GenerateSql = true;
        public bool Design = true;
        public bool Move = false;
        public bool Search = true;
        public bool Copy = false;
        public bool Paste = false;
    }

    public enum DoubleClickActionType { None, OpenInTree, DefaultAction }

    public abstract class AppObjectListWidget : WidgetBase
    {
        View m_listStyle = View.Details;
        bool m_showToolbar = true;
        DoubleClickActionType m_doubleClickAction = DoubleClickActionType.OpenInTree;

        public AppObjectListWidget()
        {
            Columns = new ListWidgetColumnCollection(this);
        }

        internal void OnChangedColumns()
        {
            if (m_ctrl != null) ((AppObjectListWidgetFrame)m_ctrl).OnChangedColumns();
        }

        [Editor(typeof(CollectionEditor), typeof(UITypeEditor))]
        [XmlCollection(typeof(ListWidgetColumn))]
        public ListWidgetColumnCollection Columns { get; set; }

        protected override WidgetBaseFrame CreateControl()
        {
            return new AppObjectListWidgetFrame(this)
            {
                ListStyle = m_listStyle,
                ShowToolbar = m_showToolbar,
                Columns = Columns,
                DoubleClickAction = DoubleClickAction,
            };
        }

        public override Type GetControlType()
        {
            return typeof(AppObjectListWidgetFrame);
        }

        public abstract void GetObjectList(List<AppObject> objs, AppObject appobj, ConnectionPack connpack);

        [XmlElem]
        public View ListStyle
        {
            get { return m_listStyle; }
            set
            {
                m_listStyle = value;
                if (m_ctrl != null) ((AppObjectListWidgetFrame)m_ctrl).ListStyle = value;
            }
        }

        [XmlElem]
        public bool ShowToolbar
        {
            get { return m_showToolbar; }
            set
            {
                m_showToolbar = value;
                if (m_ctrl != null) ((AppObjectListWidgetFrame)m_ctrl).ShowToolbar = value;
            }
        }

        [XmlElem]
        public DoubleClickActionType DoubleClickAction
        {
            get { return m_doubleClickAction; }
            set
            {
                m_doubleClickAction = value;
                if (m_ctrl != null) ((AppObjectListWidgetFrame)m_ctrl).DoubleClickAction = value;
            }
        }

        public virtual void CreateNew(AppObject appobj, ConnectionPack connpack) { }

        [Browsable(false)]
        public virtual ListWidgetCaps Caps
        {
            get
            {
                return new ListWidgetCaps();
            }
        }

        public virtual void DeleteAppObject(AppObject obj, int index)
        {
            obj.DoDelete();
        }

        public virtual void MoveWidgetUp(AppObject appobj, int index)
        {
        }

        public virtual void MoveWidgetDown(AppObject appobj, int index)
        {
        }

        public virtual bool CanDeleteObject(AppObject appobj, int index)
        {
            return appobj.AllowDelete();
        }
    }

    [Widget(Name = "db_object_list", Title = "DB object list", Category = "Lists")]
    public class DbObjectListWidget : AppObjectListWidget
    {
        AddonHolder m_dbObjectType = SpecificRepresentationAddonType.Instance.FindHolder("table");

        [RegisterItemType(typeof(SpecificRepresentationAddonType))]
        [TypeConverter(typeof(RegisterItemTypeConverter))]
        [XmlElem]
        public AddonHolder DbObjectType
        {
            get { return m_dbObjectType; }
            set
            {
                m_dbObjectType = value;
                HDesigner.CallChangedWidget(this, HDesigner.WidgetPart.All);
            }
        }

        internal ISpecificRepresentation _Type
        {
            get { return (ISpecificRepresentation)m_dbObjectType.InstanceModel; }
        }

        public override string DefaultPageTitle
        {
            get { return Texts.Get(_Type.TitlePlural); }
        }

        public override Bitmap DefaultImage
        {
            get { return _Type.Icon; }
        }

        public override void GetObjectList(List<AppObject> objs, AppObject appobj, ConnectionPack connpack)
        {
            ObjectPath objpath = appobj.GetObjectPath();
            IDatabaseSource conn = appobj.FindDatabaseConnection(connpack);
            if (conn != null && conn.Connection.IsOpened)
            {
                var dbmem = new DatabaseStructureMembers();
                dbmem.IgnoreSystemObjects = true;
                if (_Type.ObjectType == "table")
                {
                    dbmem.TableList = true;
                }
                else
                {
                    var smem = new SpecificObjectMembers { ObjectList = true };
                    dbmem.SpecificObjectOverride[_Type.ObjectType] = smem;
                }
                var dbs = conn.InvokeLoadStructure(dbmem, null);
                if (_Type.ObjectType == "table")
                {
                    foreach (var tbl in dbs.Tables)
                    {
                        var o = new TableAppObject();
                        o.FillFromDatabase(conn);
                        o.DbObjectName = tbl.FullName;
                        objs.Add(o);
                    }
                }
                else
                {
                    if (dbs.SpecificObjects.ContainsKey(_Type.ObjectType))
                    {
                        foreach (var spec in dbs.SpecificObjects[_Type.ObjectType])
                        {
                            var o = new SpecificObjectAppObject();
                            o.FillFromDatabase(conn);
                            o.DbObjectName = spec.ObjectName;
                            o.DbObjectType = _Type.ObjectType;
                            objs.Add(o);
                        }
                    }
                }
            }
            objs.SortByKey(o => o.ToString());
        }

        protected void RefreshList()
        {
            HDesigner.CallChangedWidget(this, HDesigner.WidgetPart.Data);
        }

        public override void CreateNew(AppObject appobj, ConnectionPack connpack)
        {
            var tp = _Type;
            if (tp.ObjectType == "table")
            {
                var pars = new AlterTableEditorPars { SavedCallback = RefreshList };
                MainWindow.Instance.OpenContent(new TableEditFrame(appobj.CreateDatabaseConnection(), null, pars));
            }
            else
            {
                var dbconn = appobj.FindDatabaseConnection(connpack);
                var dbtype = dbconn.Dialect.GetSpecificObjectType(tp.ObjectType);
                if (dbconn.DatabaseCaps.ExecuteSql)
                {
                    OpenQueryParameters pars = new OpenQueryParameters();
                    IPhysicalConnection newconn = dbconn.Connection.Clone();
                    string dbname = appobj.FindDatabaseName();
                    if (dbname != null) newconn.AfterOpen += ConnTools.ChangeDatabaseCallback(dbname);
                    pars.GenerateSql = delegate(IPhysicalConnection conn)
                    {
                        return dbtype.GenerateCreateNew(conn.SystemConnection, appobj.GetObjectPath());
                    };
                    pars.HideDesign = true;
                    pars.ExecutedCallback = RefreshList;
                    MainWindow.Instance.OpenContent(new QueryFrame(newconn, pars));
                }
                else
                {
                    string sql = dbtype.GenerateCreateNew(dbconn.Connection.SystemConnection, appobj.GetObjectPath());
                    var pars = new ObjectEditorPars { SavedCallback = RefreshList };
                    var frm = new SpecificObjectFrame(dbconn, tp.ObjectType, sql, pars);
                    MainWindow.Instance.OpenContent(frm);
                }
            }
        }

        public override ListWidgetCaps Caps
        {
            get
            {
                var res = base.Caps;
                res.CreateNew = true;
                res.Copy = m_dbObjectType.Name == "table";
                res.Paste = m_dbObjectType.Name == "table";
                return res;
            }
        }
    }

    [Widget(Name = "table_column_list", Title = "Table column list", Category = "Lists")]
    public class TableColumnListWidget : AppObjectListWidget
    {
        public override void GetObjectList(List<AppObject> objs, AppObject appobj, ConnectionPack connpack)
        {
            ObjectPath objpath = appobj.GetObjectPath();
            var tsrc = appobj.TableSource;
            //IDatabaseSource conn = appobj.FindDatabaseConnection(connpack);
            if (tsrc == null) return;
            if (!tsrc.Connection.IsOpened) return;
            var ts = tsrc.InvokeLoadStructure(TableStructureMembers.Columns | TableStructureMembers.PrimaryKey | TableStructureMembers.ForeignKeys);
            foreach (var col in ts.Columns)
            {
                var cobj = new ColumnAppObject();
                cobj.Column = new ColumnStructure(col);
                cobj.FillFromTable(tsrc);
                cobj.FillRelatedConstraints(ts.Constraints);
                objs.Add(cobj);
            }
        }

        public override Bitmap DefaultImage
        {
            get { return CoreIcons.column; }
        }

        public override string DefaultPageTitle
        {
            get { return Texts.Get("s_columns"); }
        }

        public override ListWidgetCaps Caps
        {
            get
            {
                var res = base.Caps;
                res.CreateNew = true;
                return res;
            }
        }

        public override void CreateNew(AppObject appobj, ConnectionPack connpack)
        {
            ((TableAppObject)appobj).DoEdit(AlterTableEditorPars.Tab.Columns);
        }
    }

    [Widget(Name = "db_constraint_list", Title = "DB constraint list", Category = "Lists")]
    public class DbConstraintListWidget : AppObjectListWidget
    {
        public override void GetObjectList(List<AppObject> objs, AppObject appobj, ConnectionPack connpack)
        {
            ObjectPath objpath = appobj.GetObjectPath();
            IDatabaseSource conn = appobj.FindDatabaseConnection(connpack);
            if (conn != null && conn.Connection.IsOpened)
            {
                var ts = conn.InvokeLoadTableStructure(objpath.ObjectName, TableStructureMembers.ConstraintsNoRefs);
                foreach (var cnt in ts.Constraints)
                {
                    var cobj = new ConstraintAppObject();
                    cobj.Constraint = Constraint.CreateCopy(cnt);
                    cobj.FillFromTable(conn.GetTable(objpath.ObjectName));
                    objs.Add(cobj);
                }
            }
            objs.SortByKey(o => o.ToString());
        }

        public override Bitmap DefaultImage
        {
            get { return CoreIcons.primary_key; }
        }

        public override string DefaultPageTitle
        {
            get { return Texts.Get("s_indexes_keys"); }
        }

        public override ListWidgetCaps Caps
        {
            get
            {
                var res = base.Caps;
                res.CreateNew = true;
                return res;
            }
        }

        public override void CreateNew(AppObject appobj, ConnectionPack connpack)
        {
            ((TableAppObject)appobj).DoEdit(AlterTableEditorPars.Tab.IndexesKeys);
        }
    }

    [Widget(Name = "database_list", Title = "Database list", Category = "Lists")]
    public class DatabaseListWidget : AppObjectListWidget
    {
        public override Bitmap DefaultImage
        {
            get { return CoreIcons.database; }
        }

        public override string DefaultPageTitle
        {
            get { return Texts.Get("s_databases"); }
        }

        public override ListWidgetCaps Caps
        {
            get
            {
                var res = base.Caps;
                res.CreateNew = true;
                res.Copy = true;
                res.Paste = true;
                return res;
            }
        }

        public override void GetObjectList(List<AppObject> objs, AppObject appobj, ConnectionPack connpack)
        {
            var server = appobj.FindServerConnection(connpack);
            if (server == null || server.Connection == null) return;

            foreach (string name in server.Databases)
            {
                var dbappobj = new DatabaseAppObject();
                dbappobj.FillFromDatabase(server.GetDatabase(name));
                objs.Add(dbappobj);
            }
            objs.SortByKey(o => o.ToString());
        }

        public override void CreateNew(AppObject appobj, ConnectionPack connpack)
        {
            var dbprops = new DatabaseProperties();
            dbprops.Name = "newdb";
            var server = appobj.FindServerConnection(connpack);
            if (server == null || server.Connection == null) return;
            if (DatabasePropertiesForm.Run(dbprops, new GenericDatabaseSource(server, server.Connection, null), false))
            {
                if (ArrayTool.Contains(server.Databases, dbprops.Name))
                {
                    StdDialog.ShowError(Texts.Get("s_database_allready_exists"));
                    return;
                }
                server.Connection.InvokeScript(fmt => { fmt.CreateDatabase(dbprops.Name, dbprops.SpecificData); }, null);
                appobj.CallCompleteChanged();
            }
        }
    }
}
