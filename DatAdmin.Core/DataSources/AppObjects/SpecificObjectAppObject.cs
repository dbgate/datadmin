using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace DatAdmin
{
    [AppObject(Name = "specificobject", Title = "s_specific_object")]
    public class SpecificObjectAppObject : DbObjectFieldsAppObject
    {
        [XmlElem]
        public string DbObjectType { get; set; }

        public SpecificObjectAppObject()
        {
            DbObjectType = "view";
        }

        public override bool NewDashboardVisible()
        {
            return true;
        }

        public override FullDatabaseRelatedName GetFullDatabaseRelatedName()
        {
            return new FullDatabaseRelatedName
            {
                ObjectName = DbObjectName,
                ObjectType = DbObjectType
            };
        }

        ObjectOperationCaps m_objCaps;
        public ObjectOperationCaps ObjCaps
        {
            get
            {
                if (m_objCaps == null)
                {
                    var dbconn = this.FindDatabaseConnection(ConnPack);
                    string type = GetDbObjectType();
                    if (dbconn != null && type != null) m_objCaps = dbconn.AlterCaps[DbObjectType];
                }
                return m_objCaps ?? new ObjectOperationCaps { AllFlags = false };
            }
        }

        ISpecificObjectType m_objType;
        ISpecificObjectType ObjType
        {
            get
            {
                if (m_objType == null)
                {
                    var dbconn = this.FindDatabaseConnection(ConnPack);
                    if (dbconn != null && dbconn.Dialect != null) m_objType = dbconn.Dialect.GetSpecificObjectType(DbObjectType);
                }
                return m_objType;
            }
        }

        public override string GetDbObjectType()
        {
            return DbObjectType;
        }

        public override string TypeName
        {
            get { return "dbobject"; }
        }

        public override string TypeTitle
        {
            get { return "s_database_object"; }
        }

        public override Bitmap Image
        {
            get
            {
                var repr = DbObjectRepresentation;
                if (repr != null) return repr.Icon;
                return CoreIcons.database;
            }
        }

        public override bool AllowDesign()
        {
            return EditEnabled();
        }

        public override void DoDesign()
        {
            Edit();
        }

        public override ITableSource TableSource
        {
            get
            {
                if (ExportVisible())
                {
                    var db = this.FindDatabaseConnection();
                    return new GenericViewAsTableSource(db, db.Connection, db.DatabaseName, DbObjectName.Schema, DbObjectName.Name);
                }
                return null;
            }
        }

        [PopupMenuVisible("s_export")]
        public bool ExportVisible()
        {
            var dbconn = this.FindDatabaseConnection(ConnPack);
            return ObjType.ObjectType == "view" && dbconn.DatabaseCaps.ExecuteSql;
        }

        [PopupMenu("s_export", ImageName = CoreIcons.exportName, Weight = MenuWeights.EXPORT)]
        public void Export()
        {
            BulkCopyWizard.Run(TableSource.GetDataStoreAndClone(), null);
        }

        [PopupMenuEnabled("s_edit")]
        public bool EditEnabled()
        {
            return ObjCaps.Create && ObjCaps.Drop;
        }

        public ISpecificObjectStructure LoadStructure()
        {
            var dbconn = this.FindDatabaseConnection(ConnPack);
            if (dbconn == null) return null;
            return dbconn.InvokeLoadSpecificObjectDetail(DbObjectType, DbObjectName);
        }

        [PopupMenu("s_edit", Weight = MenuWeights.EDIT)]
        public void Edit()
        {
            if (!EditEnabled()) return;
            var dbconn = this.FindDatabaseConnection(ConnPack);
            if (dbconn.DatabaseCaps.ExecuteSql)
            {
                OpenQueryParameters pars = new OpenQueryParameters();
                IPhysicalConnection newconn = Connection.CreateConnection();
                string dbname = DatabaseName;
                if (dbname != null) newconn.AfterOpen += ConnTools.ChangeDatabaseCallback(dbname);
                pars.GenerateSql = delegate(IPhysicalConnection conn)
                {
                    var props = new SqlFormatProperties { OmitVersionTests = true };
                    var dbs = conn.PhysicalFactory.CreateDatabaseSource(conn, dbname);
                    var so = new SpecificObjectStructure(dbs.LoadSpecificObjectDetail(DbObjectType, DbObjectName));
                    if (dbconn.Dialect != null) so.CreateSql = dbconn.Dialect.ReformatSpecificObject(so.ObjectType, so.CreateSql);
                    string drop = newconn.Dialect.GenerateScript(dmp => { dmp.DropSpecificObject(so); }, props);
                    string create = newconn.Dialect.GenerateScript(dmp => { dmp.CreateSpecificObject(so); }, props);
                    return drop + "\n\nGO\n\n" + create;
                };
                MainWindow.Instance.OpenContent(new QueryFrame(newconn, pars));
            }
            else
            {
                var frm = new SpecificObjectFrame(dbconn.CloneSource(), LoadStructure(), new ObjectEditorPars { });
                MainWindow.Instance.OpenContent(frm);
            }
        }

        public override bool DefaultAction()
        {
            Edit();
            return true;
        }

        public override bool AllowDelete()
        {
            return ObjCaps.Drop;
        }

        public override void DoDelete()
        {
            var so = LoadStructure();
            var dbconn = this.FindDatabaseConnection(ConnPack);
            dbconn.DropObject(so);
        }

        //public override bool DoDelete()
        //{
        //    if (MessageBox.Show(Texts.Get("s_really_drop$object", "object", m_objpath), VersionInfo.ProgramTitle, MessageBoxButtons.YesNo) == DialogResult.Yes)
        //    {
        //        var so = LoadStructure();
        //        m_conn.DropObject(so);
        //        return true;
        //    }
        //    return false;
        //}

        public override bool AllowRename()
        {
            return ObjCaps.Rename;
        }

        public override void RenameObject(string newname)
        {
            var dbconn = this.FindDatabaseConnection(ConnPack);
            dbconn.Connection.Invoke((Action)delegate()
            {
                ISpecificObjectStructure so = LoadStructure();
                dbconn.RenameObject(so, newname);
            });
        }
        [PopupMenuVisible("s_change_schema")]
        public bool ChangeSchemaVisible()
        {
            var dbconn = this.FindDatabaseConnection(ConnPack);
            return dbconn.DatabaseCaps.MultipleSchema;
        }

        [PopupMenu("s_change_schema", ImageName = CoreIcons.schemaName, Weight = MenuWeights.EDIT2)]
        public void ChangeSchema()
        {
            var dbconn = this.FindDatabaseConnection(ConnPack);
            var schemata = StructLoader.SchemaNames(dbmem => dbconn.InvokeLoadStructure(dbmem, null));
            string newschema = InputBox.Run("s_new_schema", DbObjectName.Schema, schemata);
            if (newschema != null)
            {
                ISpecificObjectStructure so = LoadStructure();
                dbconn.ChangeObjectSchema(so, newschema);
                //m_conn.RunScript(dmp => { dmp.ChangeSpecificObjectSchema(so, newschema); });
                CallCompleteChanged();
            }
        }

        public override string GetRenamingText()
        {
            return DbObjectName.Name;
        }

        public override bool HasTabularData
        {
            get
            {
                var dbconn = this.FindDatabaseConnection(ConnPack);
                return ObjType.HasTabularData && dbconn.DatabaseCaps.ExecuteSql;
            }
        }

        public override ITabularDataView GetTabularData(ConnectionPack connpack)
        {
            return ObjType.MergeTabularData(this.FindPhysicalConnection(connpack), GetObjectPath());
        }

        public override string GetTreePath()
        {
            return GetDbObjectTreePath();
        }

        public override string GetFileFriendlySignature()
        {
            return IOTool.CreateFileFriendlyName("dbobj" + DbObjectType + Connection.GetConnectionKey() + "_" + DatabaseName + "_" + DbObjectName.ToString("F"));
        }
    }
}
