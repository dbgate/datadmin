using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace DatAdmin
{
    //[AppObject(Name = "connection", Title = "s_connection")]
    //public abstract class ConnectionAppObject : AppObject
    //{
    //    public override string TypeName
    //    {
    //        get { return "connection"; }
    //    }

    //    public override string TypeTitle
    //    {
    //        get { return "s_connection"; }
    //    }

    //    public override Bitmap Image
    //    {
    //        get { return CoreIcons.connect; }
    //    }

    //    public abstract string GetDataSource();

    //    // unique identifier of connection inside ConnectionPack
    //    public abstract string ConnectionKey { get; }
    //    public abstract IPhysicalConnection CreatePhysicalConnection();

    //    public abstract string GetDatabasePrivateFolder(string dbname);
    //    public abstract string GetDatabasePrivateSubFolder(string dbname, string folderName);
    //}

    //[AppObject(Name = "stored_connection", Title = "s_stored_connection")]
    //public class StoredConnectionAppObject : ConnectionAppObject
    //{
    //    [XmlSubElem]
    //    public IStoredConnection StoredConn { get; set; }

    //    public override string GetDataSource()
    //    {
    //        return StoredConn.GetDataSource();
    //    }

    //    public override string ConnectionKey
    //    {
    //        get
    //        {
    //            if (StoredConn != null) return StoredConn.ConnectionKey;
    //            return null;
    //        }
    //    }

    //    public override IPhysicalConnection CreatePhysicalConnection()
    //    {
    //        return StoredConn.CreatePhysicalConnection();
    //    }

    //    public override string GetDatabasePrivateFolder(string dbname)
    //    {
    //        return StoredConn.GetDatabasePrivateFolder(dbname);
    //    }

    //    public override string GetDatabasePrivateSubFolder(string dbname, string folderName)
    //    {
    //        return StoredConn.GetDatabasePrivateSubFolder(dbname, folderName);
    //    }
    //}

    public abstract class ServerFieldsAppObject : AppObject
    {
        [XmlSubElem]
        public IPhysicalConnectionFactory Connection { get; set; }

        protected void FillServerObjectFilter(ServerObjectFilter filter)
        {
            if (Connection != null) filter.ServerName.PredefinedValue = Connection.GetDataSource();
        }

        public override IPhysicalConnectionFactory GetConnection()
        {
            return Connection;
        }

        public void FillFromUsage(IConnectionUsage usage)
        {
            if (usage.Connection != null)
            {
                Connection = usage.Connection.PhysicalFactory;
            }
        }

        public override void GetAppObjectProperties(Dictionary<string, string> props)
        {
            base.GetAppObjectProperties(props);
            if (ConnPack != null)
            {
                var conn = ConnPack.GetConnection(Connection, false);
                if (conn != null && conn.Dialect != null) props["dialect"] = conn.Dialect.DialectName;
                else props["dialect"] = GenericDialect.Instance.DialectName;
            }
            if (Connection != null)
            {
                props["server"] = Connection.GetDataSource();
            }
        }

        public IServerSource GetServerConnection(ConnectionPack connpack)
        {
            if (connpack == null) return null;
            var phys = connpack.GetConnection(Connection, true);
            if (phys == null) return null;
            return phys.PhysicalFactory.CreateServerSource(phys);
        }

        protected string GetServerTreePath()
        {
            if (Connection != null && Connection.GetDataTreeName() != null) return "data:/" + Connection.GetDataTreeName();
            return null;
        }

        public void AssignServerFields(ServerFieldsAppObject appobj)
        {
            if (appobj == null)
            {
                Connection = null;
            }
            else
            {
                Connection = appobj.Connection;
            }
        }
    }

    [AppObject(Name = "server", Title = "s_server")]
    public class ServerAppObject : ServerFieldsAppObject
    {
        public override string TypeName
        {
            get { return "server"; }
        }

        public override string TypeTitle
        {
            get { return "s_server"; }
        }

        public override Bitmap Image
        {
            get { return CoreIcons.dbserver; }
        }

        public override bool NewDashboardVisible()
        {
            return true;
        }

        public override ObjectFilterBase GetFilter()
        {
            var res = new ServerObjectFilter();
            FillServerObjectFilter(res);
            res.ObjectType.PredefinedValue = TypeName;
            return res;
        }

        public override void GetWidgets(List<IWidget> res)
        {
            base.GetWidgets(res);
            res.Add(new DatabasesRawGridWidget());
        }

        public override string GetTreePath()
        {
            return GetServerTreePath();
        }

        public override string GetFileFriendlySignature()
        {
            return IOTool.CreateFileFriendlyName("server" + Connection.GetConnectionKey());
        }

        public override string ToString()
        {
            if (Connection != null) return Connection.GetDataSource();
            return "(server)";
        }

        [DragDropOperationVisible(Name = "copydb")]
        public bool DragDrop_CopyDatabaseVisible(AppObject draggingObject)
        {
            return draggingObject is DatabaseAppObject;
        }

        [DragDropOperation(Name = "copydb", Title = "s_copy_database")]
        public void DragDrop_CopyDatabase(AppObject draggingObject)
        {
            var conn = this.FindServerConnection();
            if (draggingObject is DatabaseAppObject)
            {
                try
                {
                    IDatabaseSource dsource = ((DatabaseAppObject)draggingObject).FindDatabaseConnection();
                    var dbprops = new DatabaseProperties();
                    dbprops.Name = dsource.DatabaseName;
                    DatabasePropertiesForm.Run(dbprops, new GenericDatabaseSource(conn, conn.Connection, null), false);
                    //string newname = InputBox.Run(Texts.Get("s_name_of_new_database"), dsource.DatabaseName);
                    //if (newname == null) return;
                    if (ArrayTool.Contains(conn.Databases, dbprops.Name))
                    {
                        StdDialog.ShowError(Texts.Get("s_database_allready_exists"));
                        return;
                    }
                    IDatabaseSource newdb = conn.CreateDatabase(dbprops.Name, dbprops.SpecificData);
                    CopyDbWizard.Run(dsource.CloneSource(), newdb.CloneSource());
                    //CopyDbProcess.StartProcess(dsource.CloneSource(), newdb.CloneSource(), null);
                }
                catch (Exception e)
                {
                    Errors.Report(e);
                }
            }
        }

        [PopupMenu("s_query", ImageName = CoreIcons.sqlName, Weight = MenuWeights.QUERY)]
        public void RunQuery()
        {
            MainWindow.Instance.OpenContent(new QueryFrame(this.FindPhysicalConnection().Clone(), new OpenQueryParameters()));
        }

        public override void ModifiedDoubleClick(System.Windows.Forms.Keys keys)
        {
            base.ModifiedDoubleClick(keys);
            if (keys == Keys.Shift) RunQuery();
        }

        [PopupMenuEnabled("s_import_sql_dump")]
        public bool RunImportSqlDumpEnabled()
        {
            var sconn = this.FindServerConnection();
            return sconn != null;
        }

        [PopupMenu("s_import_sql_dump", Weight = 0)]
        public void RunImportSqlDump()
        {
            var dbconn = this.FindServerConnection();
            ImportSqlDumpForm.Run(dbconn.GetDatabase(null).CloneSource());
        }
    }

    public abstract class DatabaseFieldsAppObject : ServerFieldsAppObject
    {
        [XmlElem]
        public string DatabaseName { get; set; }

        protected void FillDatabaseObjectFilter(DatabaseObjectFilter filter)
        {
            base.FillServerObjectFilter(filter);
            filter.DatabaseName.PredefinedValue = DatabaseName;
        }

        public IDatabaseSource GetDatabaseConnection(ConnectionPack connpack)
        {
            if (connpack == null) return null;
            var phys = connpack.GetConnection(Connection, true);
            if (phys == null) return null;
            return phys.PhysicalFactory.CreateDatabaseSource(phys, DatabaseName);
        }

        public void FillFromDatabase(IDatabaseSource db)
        {
            FillFromUsage(db);

            DatabaseName = db.DatabaseName;
        }

        public override void GetAppObjectProperties(Dictionary<string, string> props)
        {
            base.GetAppObjectProperties(props);
            props["database"] = DatabaseName;
        }

        protected string GetDatabaseTreePath()
        {
            var srv = GetServerTreePath();
            if (srv != null)
            {
                var gen = Connection as GenericDbConnectionFactory;
                if (gen != null && gen.Stored != null && gen.Stored.DatabaseMode == ConnectionDatabaseMode.All)
                {
                    return srv + "/databases/" + DatabaseName;
                }
                return srv;
            }
            return null;
        }

        public void AssignDbFields(DatabaseFieldsAppObject appobj)
        {
            if (appobj == null)
            {
                DatabaseName = null;
            }
            else
            {
                DatabaseName = appobj.DatabaseName;
            }
            AssignServerFields(appobj);
        }

        public override string GetDatabaseName()
        {
            return DatabaseName;
        }
    }

    public abstract class DbObjectFieldsAppObject : DatabaseFieldsAppObject
    {
        [XmlElem]
        public NameWithSchema DbObjectName { get; set; }

        public abstract string GetDbObjectType();

        public ISpecificRepresentation DbObjectRepresentation
        {
            get { return SpecificRepresentationAddonType.Instance.FindRepresentation(GetDbObjectType()); }
        }

        public void AssignDbObjectFields(DbObjectFieldsAppObject appobj)
        {
            if (appobj != null)
            {
                DbObjectName = appobj.DbObjectName;
            }
            else
            {
                DbObjectName = null;
            }
            AssignDbFields(appobj);
        }

        protected void FillDbObjectObjectFilter(DbObjectObjectFilter filter)
        {
            base.FillDatabaseObjectFilter(filter);
            filter.DbObjectType.PredefinedValue = GetDbObjectType();
            if (DbObjectName != null)
            {
                filter.DbObjectName.PredefinedValue = DbObjectName.Name;
                filter.DbObjectSchema.PredefinedValue = DbObjectName.Schema;
                filter.DbObjectSchema.Enabled = false;
            }
        }

        public override ObjectPath GetObjectPath()
        {
            return new ObjectPath(DatabaseName, DbObjectName);
        }

        public override void GetAppObjectProperties(Dictionary<string, string> props)
        {
            base.GetAppObjectProperties(props);
            props["dbobjtype"] = GetDbObjectType();
            if (DbObjectName != null)
            {
                props["dbobjschema"] = DbObjectName.Schema;
                props["dbobjname"] = DbObjectName.Name;
            }
        }

        public override string ToString()
        {
            if (DbObjectName != null) return DbObjectName.ToString();
            return base.ToString();
        }

        protected string GetDbObjectTreePath()
        {
            string dbpath = GetDatabaseTreePath();
            if (dbpath == null) return null;
            var repr = DbObjectRepresentation;
            return dbpath + "/" + Texts.GetTextIdWithoutPrefix(repr.TitlePlural) + "/" + DbObjectName.ToString("F");
        }

        public override AppObject Clone()
        {
            var res = (DbObjectFieldsAppObject)base.Clone();
            res.DbObjectName = this.DbObjectName;
            return res;
        }
    }
}
