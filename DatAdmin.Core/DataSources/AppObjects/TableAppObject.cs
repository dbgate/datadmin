using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using System.Linq;
using System.IO;

namespace DatAdmin
{
    public abstract class TableFieldsAppObject : DbObjectFieldsAppObject
    {
        public override string GetDbObjectType()
        {
            return "table";
        }

        public void FillFromTable(ITableSource table)
        {
            if (table.Database != null)
            {
                FillFromDatabase(table.Database);
            }
            else
            {
                FillFromUsage(table);
            }
            DbObjectName = table.FullName;
        }

        public ITableSource GetTableConnection(ConnectionPack connpack)
        {
            var db = GetDatabaseConnection(connpack);
            return db.GetTable(DbObjectName);
        }

        public override ITableSource TableSource { get { return this.FindTableConnection(ConnPack); } }

        public string TableKey
        {
            get
            {
                return Connection.GetConnectionKey() + "#" + DatabaseName + "#" + DbObjectName.ToString();
            }
        }
    }

    [AppObject(Name = "table", Title = "s_table")]
    public class TableAppObject : TableFieldsAppObject
    {
        public override string TypeName
        {
            get { return "table"; }
        }

        public override string TypeTitle
        {
            get { return "s_table"; }
        }

        public override Bitmap Image
        {
            get { return CoreIcons.table; }
        }

        public override bool HasTabularData
        {
            get { return true; }
        }

        public override ITabularDataView GetTabularData(ConnectionPack connpack)
        {
            return GetTableConnection(connpack).GetTabularData();
        }

        public override void GetWidgets(List<IWidget> res)
        {
            base.GetWidgets(res);
            if (Connection is GenericDbConnectionFactory) res.Add(new ColumnsRawGridWidget());
            res.Add(new CreateTableDDLObjectView());
            if (Connection is GenericDbConnectionFactory) res.Add(new TableInfoWidget());
        }

        public override bool NewDashboardVisible()
        {
            return true;
        }

        [PopupMenuEnabled("s_open_data")]
        public bool OpenDataEnabled()
        {
            return TableSource.TableCaps.TabularData;
        }

        [PopupMenu("s_open_data", ImageName = CoreIcons.table_dataName, Weight = MenuWeights.OPEN1, MultiMode = MultipleMode.Sequencable, GroupName = "sql")]
        public void OpenData()
        {
            string winid = TableKey + "#table_data";
            if (MainWindow.Instance.HasContent(winid))
            {
                var sett = TableSource.Database.Settings.Tree();
                if (sett.OpenTableDuplicateMode.ShouldReuse())
                {
                    MainWindow.Instance.ActivateContent(winid);
                    return;
                }
            }
            var tabdata = TableSource.GetTabularData();
            tabdata.CloneConnection();
            if (tabdata.TableSource != null)
            {
                tabdata.TableSource.Connection = tabdata.Connection;
            }
            if (tabdata.DatabaseSource != null)
            {
                tabdata.DatabaseSource.Connection = tabdata.Connection;
            }
            var content = new TableDataFrame(tabdata);
            content.WinId = winid;
            MainWindow.Instance.OpenContent(content);
        }

        [PopupMenuEnabled("s_export")]
        public bool ExportEnabled()
        {
            return TableSource.TableCaps.DataStoreForReading;
        }

        [PopupMenu("s_export", ImageName = CoreIcons.exportName, Weight = MenuWeights.EXPORT, GroupName = "impexp")]
        public void Export()
        {
            BulkCopyWizard.Run(TableSource.GetDataStoreAndClone(), null);
        }

        [PopupMenuEnabled("s_import")]
        public bool ImportEnabled()
        {
            return TableSource.TableCaps.DataStoreForWriting;
        }

        [PopupMenu("s_import", ImageName = CoreIcons.importName, Weight = MenuWeights.IMPORT, GroupName = "impexp")]
        public void Import()
        {
            BulkCopyWizard.Run(null, TableSource.GetDataStoreAndClone());
        }

        public override bool AllowDesign()
        {
            return EditEnabled();
        }

        public override void DoDesign()
        {
            DoEdit();
        }

        [PopupMenuEnabled("s_edit")]
        public bool EditEnabled()
        {
            var caps = TableSource.Database.AlterCaps;
            return caps.AlterTable || caps.RecreateTable;
        }

        public void DoEdit(AlterTableEditorPars.Tab initialTab)
        {
            string winid = TableKey + "#edit_table";
            if (MainWindow.Instance.HasContent(winid))
            {
                var sett = TableSource.Database.Settings.Tree();
                if (sett.DesignTableDuplicateMode.ShouldReuse())
                {
                    MainWindow.Instance.ActivateContent(winid);
                    return;
                }
            }

            var pars = new AlterTableEditorPars { InitialTab = initialTab };
            var ts = new TableStructure { FullName = TableSource.FullName };
            var content = new TableEditFrame(TableSource.Database.CloneSource(), ts, pars);
            content.WinId = winid;
            MainWindow.Instance.OpenContent(content);
        }

        [PopupMenu("s_edit", ImageName = CoreIcons.table_editName, Shortcut = Keys.F4, Weight = MenuWeights.EDIT, MultiMode = MultipleMode.Sequencable)]
        public void DoEdit()
        {
            DoEdit(AlterTableEditorPars.Tab.Columns);
        }

        [PopupMenuEnabled("s_truncate_table")]
        public bool TruncateTableEnabled()
        {
            return TableSource.TableCaps.TruncateTable;
        }

        [PopupMenu("s_truncate_table", ImageName = CoreIcons.recycle_binName, Weight = MenuWeights.DELETE2, MultiMode = MultipleMode.NativeMulti, GroupName = "impexp")]
        public void TruncateTable(object[] tbls)
        {
            if (MessageBox.Show(Texts.Get("s_really_truncate$table", "table", tbls.CreateDelimitedText(", ")), VersionInfo.ProgramTitle, MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                foreach (TableAppObject appobj in tbls)
                {
                    appobj.TableSource.TruncateTable();
                    var cnt = MainWindow.Instance.GetCurrentContent() as DataBrowser;
                    if (cnt != null) cnt.DoRefreshData();
                }
            }
        }

        [PopupMenuEnabled("s_change_schema")]
        public bool ChangeSchemaEnabled()
        {
            return TableSource.Database.AlterCaps.ChangeTableSchema;
        }

        [PopupMenuVisible("s_change_schema")]
        public bool ChangeSchemaVisible()
        {
            return TableSource.Database.DatabaseCaps.MultipleSchema;
        }

        [PopupMenu("s_change_schema", ImageName = CoreIcons.schemaName, Weight = MenuWeights.EDIT2)]
        public void ChangeSchema()
        {
            var schemata = StructLoader.SchemaNames(dbmem => TableSource.Database.InvokeLoadStructure(dbmem, null));
            string newschema = InputBox.Run("s_new_schema", TableSource.FullName.Schema, schemata);
            if (newschema != null)
            {
                TableSource.ChangeSchema(newschema);
                CallCompleteChanged();
            }
        }

        [PopupMenu("s_design_query", RequiredFeature = QueryDesignerFeature.Test, ImageName = CoreIcons.querydesignName, GroupName = "sql")]
        public void DesignQuery()
        {
            OpenQueryParameters pars = new OpenQueryParameters();
            pars.GoToDesign = true;
            pars.AddDesignTable = new FullDatabaseRelatedName { ObjectName = TableSource.FullName, ObjectType = "table" };
            MainWindow.Instance.OpenContent(new QueryFrame(TableSource.Database.CloneConnection(), pars));
        }

        [PopupMenu("s_duplicate", ImageName = CoreIcons.duplicateName, GroupName = "impexp")]
        public void Duplicate()
        {
            var dbobj = new DatabaseAppObject();
            dbobj.AssignDbFields(this);
            dbobj.ConnPack = ConnPack;
            dbobj.DragDrop_CopyTable(this);
            CallCompleteChanged();
        }

        private bool RunOpenTableAction(OpenTableAction action)
        {
            switch (action)
            {
                case OpenTableAction.DESIGN_TABLE:
                    if (EditEnabled()) { DoEdit(); return true; }
                    break;
                case OpenTableAction.EXPORT:
                    if (ExportEnabled()) { Export(); return true; }
                    break;
                //case OpenTableAction.GENERATE_CREATE:
                //    RunSqlGenerator("CREATE TABLE");
                //    return true;
                //case OpenTableAction.GENERATE_RECREATE:
                //    RunSqlGenerator("RECREATE TABLE");
                //    return true;
                //case OpenTableAction.GENERATE_SELECT_ALL:
                //    RunSqlGenerator("SELECT ALL");
                //    return true;
                case OpenTableAction.IMPORT:
                    if (ImportEnabled()) { Import(); return true; }
                    break;
                case OpenTableAction.OPEN_DATA:
                    if (OpenDataEnabled()) { OpenData(); return true; }
                    break;
            }
            return false;
        }

        public override bool DefaultAction()
        {
            var sett = TableSource.Database.Settings.Tree();
            if (RunOpenTableAction(sett.PrimaryTableAction)) return true;
            if (RunOpenTableAction(sett.SecondaryTableAction)) return true;
            return true;
        }

        public override bool AllowDelete()
        {
            return TableSource.Database.AlterCaps.DropTable;
        }

        public override bool AllowRename()
        {
            return TableSource.Database.AlterCaps.RenameTable;
        }

        public override void DoDelete()
        {
            TableSource.DropTable();
        }

        public override string GetRenamingText()
        {
            return DbObjectName.Name;
        }

        public override void RenameObject(string newname)
        {
            TableSource.RenameTable(newname);
        }

        [DragDropOperationVisible(Name = "copytable")]
        public bool DragDropVisible_Table(AppObject appobj)
        {
            var tsrc = appobj.TableSource;
            if (tsrc == null) return false;
            if (tsrc.TableCaps.DataStoreForReading && TableSource.TableCaps.DataStoreForWriting) return true;
            return false;
        }

        [DragDropOperation(Name = "copytable", Title = "s_copy_table_data")]
        public void DragDrop_Table(AppObject appobj)
        {
            ITableSource src = appobj.TableSource;
            BulkCopyWizard.Run(src.GetDataStoreAndClone(), TableSource.GetDataStoreAndClone());
        }

        public override FullDatabaseRelatedName GetFullDatabaseRelatedName()
        {
            return new FullDatabaseRelatedName
            {
                ObjectName = DbObjectName,
                ObjectType = "table"
            };
        }

        public override string GetTreePath()
        {
            return GetDbObjectTreePath();
        }

        public override string GetFileFriendlySignature()
        {
            return IOTool.CreateFileFriendlyName("table" + Connection.GetConnectionKey() + "_" + DatabaseName + "_" + DbObjectName.ToString("F"));
        }
    }

    [AppObject(Name = "column", Title = "s_column")]
    public class ColumnAppObject : TableFieldsAppObject
    {
        [XmlSubElem]
        public ColumnStructure Column { get; set; }
        public List<Constraint> RelatedConstraints = new List<Constraint>();

        public override void SaveToXml(XmlElement xml)
        {
            base.SaveToXml(xml);
            foreach (var cnt in RelatedConstraints) cnt.SaveToXml(xml.AddChild("Constraint"));
        }

        public override void LoadFromXml(XmlElement xml)
        {
            base.LoadFromXml(xml);
            foreach (XmlElement child in xml.SelectNodes("Constraint"))
            {
                var cnt = Constraint.FromXml(child, false);
                if (cnt != null) RelatedConstraints.Add(cnt);
            }
        }

        public override string TypeName
        {
            get { return "column"; }
        }

        public override string TypeTitle
        {
            get { return "s_column"; }
        }

        public override Bitmap Image
        {
            get
            {
                bool ispk = RelatedConstraints.Find(cnt => cnt is IPrimaryKey) != null;
                bool isfk = RelatedConstraints.Find(cnt => cnt is IForeignKey) != null;
                if (ispk) return CoreIcons.primary_key;
                if (isfk) return CoreIcons.foreign_key;
                return CoreIcons.column;
            }
        }

        public override bool AllowRename()
        {
            var dbconn = this.FindDatabaseConnection(ConnPack);
            if (dbconn == null) return false;
            return dbconn.AlterCaps.RenameColumn;
        }

        private ColumnStructure GetColumnWithDummyTable()
        {
            var c = new ColumnStructure(Column);
            c.SetDummyTable(DbObjectName);
            return c;
        }

        public override void RenameObject(string newname)
        {
            this.FindDatabaseConnection(ConnPack).RenameObject(GetColumnWithDummyTable(), newname);
        }

        public override string GetRenamingText()
        {
            return Column.ColumnName;
        }

        public override bool AllowDelete()
        {
            var dbconn = this.FindDatabaseConnection(ConnPack);
            if (dbconn == null) return false;
            return dbconn.AlterCaps.DropColumn;
        }

        public override void DoDelete()
        {
            this.FindDatabaseConnection(ConnPack).DropObject(GetColumnWithDummyTable());
        }

        public void FillRelatedConstraints(IConstraintCollection constraints)
        {
            foreach (var cnt in constraints)
            {
                var ccnt = cnt as IColumnsConstraint;
                if (ccnt == null) continue;
                if (Array.IndexOf(ccnt.Columns.GetNames(), Column.ColumnName) >= 0) RelatedConstraints.Add(Constraint.CreateCopy(ccnt));
            }
        }

        public override void GetAppObjectProperties(Dictionary<string, string> props)
        {
            base.GetAppObjectProperties(props);
            props["column"] = Column.ColumnName;
            var dialect = this.FindPhysicalConnection().GetAnyDialect();
            if (dialect.SpecificTypeEnum != typeof(DbTypeCode))
            {
                props["datatype"] = dialect.GenericTypeToSpecific(Column.DataType).ToString().ToLower();
            }
            else
            {
                props["datatype"] = Column.DataType.ToString().ToLower();
            }
        }

        public override string ToString()
        {
            bool ispk = RelatedConstraints.Find(cnt => cnt is IPrimaryKey) != null;
            bool isfk = RelatedConstraints.Find(cnt => cnt is IForeignKey) != null;
            StringBuilder res = new StringBuilder(Column.ColumnName);
            res.Append(" (");
            if (ispk) res.Append("PK, ");
            if (isfk) res.Append("FK, ");
            res.Append(Column.DataType.ToString().ToLower());
            //if (m_conn.Database.Dialect != null) res.Append(m_conn.Database.Dialect.GenericTypeToSpecific(m_column.DataType).ToString().ToLower());
            //else res.Append(m_column.DataType.ToString().ToLower());
            res.Append(", ");
            if (Column.IsNullable) res.Append("null"); else res.Append("not null");
            res.Append(")");
            return res.ToString();
        }

        public override FullDatabaseRelatedName GetFullDatabaseRelatedName()
        {
            return new FullDatabaseRelatedName
            {
                ObjectName = DbObjectName,
                ObjectType = "column",
                SubName = Column.ColumnName
            };
        }

        public override string GetTreePath()
        {
            var tblpath = GetDbObjectTreePath();
            if (tblpath == null) return null;
            return tblpath + "/columns/" + Column.ColumnName;
        }

        [PopupMenuEnabled("s_edit")]
        public bool EditEnabled()
        {
            var caps = TableSource.Database.AlterCaps;
            return caps.AlterTable || caps.RecreateTable;
        }

        [PopupMenu("s_edit", ImageName = CoreIcons.table_editName, Shortcut = Keys.F4, Weight = MenuWeights.EDIT, MultiMode = MultipleMode.Sequencable, GroupName = "sql")]
        public void DoEdit()
        {
            var tbl = new TableAppObject();
            tbl.AssignDbObjectFields(this);
            tbl.ConnPack = ConnPack;
            tbl.DoEdit();
        }
    }

    [AppObject(Name = "constraint", Title = "s_constraint")]
    public class ConstraintAppObject : TableFieldsAppObject
    {
        public Constraint Constraint { get; set; }

        public override void SaveToXml(XmlElement xml)
        {
            base.SaveToXml(xml);
            Constraint.SaveToXml(xml.AddChild("Constraint"));
        }

        public override void LoadFromXml(XmlElement xml)
        {
            base.LoadFromXml(xml);
            Constraint = Constraint.FromXml(xml.FindElement("Constraint"), false);
        }

        public override string TypeName
        {
            get { return "constraint"; }
        }

        public override string TypeTitle
        {
            get
            {
                if (Constraint is IPrimaryKey) return "s_primary_key";
                if (Constraint is IForeignKey) return "s_foreign_key";
                if (Constraint is ICheck) return "s_check";
                if (Constraint is IIndex) return "s_index";
                if (Constraint is IUnique) return "s_unique";

                return "";
            }
        }

        public override Bitmap Image
        {
            get
            {
                if (Constraint is IPrimaryKey) return CoreIcons.primary_key;
                if (Constraint is IForeignKey) return CoreIcons.foreign_key;
                if (Constraint is ICheck) return CoreIcons.check;
                if (Constraint is IIndex) return CoreIcons.index;
                if (Constraint is IUnique) return CoreIcons.unique;
                return null;
            }
        }

        [PopupMenuEnabled("s_edit")]
        public bool EditEnabled()
        {
            var caps = TableSource.Database.AlterCaps;
            return caps.AlterTable || caps.RecreateTable;
        }

        [PopupMenu("s_edit", ImageName = CoreIcons.table_editName, Shortcut = Keys.F4, Weight = MenuWeights.EDIT, MultiMode = MultipleMode.Sequencable, GroupName = "sql")]
        public void DoEdit()
        {
            var tbl = new TableAppObject();
            tbl.AssignDbObjectFields(this);
            tbl.ConnPack = ConnPack;
            if (Constraint is IPrimaryKey || Constraint is IIndex || Constraint is IUnique)
            {
                tbl.DoEdit(AlterTableEditorPars.Tab.IndexesKeys);
            }
            if (Constraint is IForeignKey)
            {
                tbl.DoEdit(AlterTableEditorPars.Tab.ForeignKeys);
            }
            if (Constraint is ICheck)
            {
                tbl.DoEdit(AlterTableEditorPars.Tab.Checks);
            }
        }

        public override string ToString()
        {
            if (Constraint.Name != null) return Constraint.Name;
            if (Constraint is IPrimaryKey) return "(" + Texts.Get("s_primary_key") + ")";
            return "???";
        }

        private Constraint GetConstraintWithDummyTable()
        {
            var c = Constraint.Clone();
            c.SetDummyTable(DbObjectName);
            return c;
        }

        public override bool AllowDelete()
        {
            var dbconn = this.FindDatabaseConnection(ConnPack);
            if (dbconn != null) return dbconn.AlterCaps.DropConstraint;
            return false;
        }

        public override void DoDelete()
        {
            var dbconn = this.FindDatabaseConnection(ConnPack);
            if (dbconn != null) dbconn.DropObject(GetConstraintWithDummyTable());
        }

        public override bool AllowRename()
        {
            var dbconn = this.FindDatabaseConnection(ConnPack);
            if (dbconn != null) return dbconn.AlterCaps.DropConstraint;
            return false;
        }

        public override void RenameObject(string newname)
        {
            var dbconn = this.FindDatabaseConnection(ConnPack);
            if (dbconn != null) dbconn.RenameObject(GetConstraintWithDummyTable(), newname);
        }

        public override ObjectPath GetObjectPath()
        {
            return new ObjectPath(DatabaseName, DbObjectName, Constraint.Name);
        }

        public override FullDatabaseRelatedName GetFullDatabaseRelatedName()
        {
            return new FullDatabaseRelatedName
            {
                ObjectName = DbObjectName,
                ObjectType = Constraint.Type.GetIdentifier(),
                SubName = Constraint.Name,
            };
        }

        public override void GetAppObjectProperties(Dictionary<string, string> props)
        {
            base.GetAppObjectProperties(props);
            var cc = Constraint as IColumnsConstraint;
            if (cc != null)
            {
                props["columns"] = (from c in cc.Columns select c.ColumnName).CreateDelimitedText(",");
            }
            props["constraint_type"] = Texts.Get(TypeTitle);
            props["constraint"] = ToString();
        }

        public override string GetTreePath()
        {
            var tblpath = GetDbObjectTreePath();
            if (tblpath == null) return null;
            string plural = "constraints";
            if (Constraint is IIndex) plural = "indexes";
            return tblpath + "/" + plural + "/" + (Constraint.Name ?? "noname");
        }
    }

    [AppObject(Name = "perspective_instance", Title = "s_perspective", RequiredFeature = AdvancedPerspectivesFeature.Test)]
    public class PerspectiveInstanceAppObject : TableFieldsAppObject
    {
        [XmlElem]
        public string FileName { get; set; }

        public override Bitmap Image
        {
            get { return CoreIcons.perspective; }
        }

        public override bool HasTabularData
        {
            get { return true; }
        }

        public override ITabularDataView GetTabularData(ConnectionPack connpack)
        {
            var tabdata = GetTableConnection(connpack).GetTabularData() as GenericTabularDataView;
            tabdata.FixedPerspective = FileName;
            return tabdata;
        }

        public override string TypeName
        {
            get { return "perspective_instance"; }
        }

        public override string TypeTitle
        {
            get { return "s_perspective"; }
        }

        [PopupMenu("s_open_data", ImageName = CoreIcons.table_dataName, Weight = MenuWeights.OPEN1, MultiMode = MultipleMode.Sequencable, GroupName = "sql")]
        public void OpenData()
        {
            string winid = TableKey + "#table_data#per#" + Path.GetFileNameWithoutExtension(FileName.ToLower());
            if (MainWindow.Instance.HasContent(winid))
            {
                var sett = TableSource.Database.Settings.Tree();
                if (sett.OpenTableDuplicateMode.ShouldReuse())
                {
                    MainWindow.Instance.ActivateContent(winid);
                    return;
                }
            }
            var tabdata = GetTabularData(ConnPack);
            tabdata.CloneConnection();
            var content = new TableDataFrame(tabdata);
            content.WinId = winid;
            MainWindow.Instance.OpenContent(content);
        }

        public override bool DefaultAction()
        {
            OpenData();
            return true;
        }

        public override string ToString()
        {
            return DbObjectName.ToLower() + ":" + Path.GetFileNameWithoutExtension(FileName);
        }
    }
}
