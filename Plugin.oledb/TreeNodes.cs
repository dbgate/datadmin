using System;
using System.Collections.Generic;
using System.Text;
using DatAdmin;
using System.Data.Common;
using System.Data.OleDb;
using System.Data;

namespace Plugin.oledb
{
    class OleDbSchemaDef
    {
        internal string name;
        internal Guid guid;
        internal OleDbSchemaDef(string name, Guid guid)
        {
            this.name = name;
            this.guid = guid;
        }
        internal static OleDbSchemaDef[] SchemaDefs = new OleDbSchemaDef[] {
            new OleDbSchemaDef("Assertions",OleDbSchemaGuid.Assertions),
            new OleDbSchemaDef("Catalogs",OleDbSchemaGuid.Catalogs),
            new OleDbSchemaDef("Collations",OleDbSchemaGuid.Collations),
            new OleDbSchemaDef("Column_Domain_Usage",OleDbSchemaGuid.Column_Domain_Usage),
            new OleDbSchemaDef("Column_Privileges",OleDbSchemaGuid.Column_Privileges),
            new OleDbSchemaDef("Columns",OleDbSchemaGuid.Columns),
            new OleDbSchemaDef("Constraint_Column_Usage",OleDbSchemaGuid.Constraint_Column_Usage),
            new OleDbSchemaDef("Constraint_Table_Usage",OleDbSchemaGuid.Constraint_Table_Usage),
            new OleDbSchemaDef("DbInfoKeywords",OleDbSchemaGuid.DbInfoKeywords),
            new OleDbSchemaDef("DbInfoLiterals",OleDbSchemaGuid.DbInfoLiterals),
            new OleDbSchemaDef("Foreign_Keys",OleDbSchemaGuid.Foreign_Keys),
            new OleDbSchemaDef("Character_Sets",OleDbSchemaGuid.Character_Sets),
            new OleDbSchemaDef("Check_Constraints",OleDbSchemaGuid.Check_Constraints),
            new OleDbSchemaDef("Check_Constraints_By_Table",OleDbSchemaGuid.Check_Constraints_By_Table),
            new OleDbSchemaDef("Indexes",OleDbSchemaGuid.Indexes),
            new OleDbSchemaDef("Key_Column_Usage",OleDbSchemaGuid.Key_Column_Usage),
            new OleDbSchemaDef("Primary_Keys",OleDbSchemaGuid.Primary_Keys),
            new OleDbSchemaDef("Procedure_Columns",OleDbSchemaGuid.Procedure_Columns),
            new OleDbSchemaDef("Procedure_Parameters",OleDbSchemaGuid.Procedure_Parameters),
            new OleDbSchemaDef("Procedures",OleDbSchemaGuid.Procedures),
            new OleDbSchemaDef("Provider_Types",OleDbSchemaGuid.Provider_Types),
            new OleDbSchemaDef("Referential_Constraints",OleDbSchemaGuid.Referential_Constraints),
            new OleDbSchemaDef("SchemaGuids",OleDbSchemaGuid.SchemaGuids),
            new OleDbSchemaDef("Schemata",OleDbSchemaGuid.Schemata),
            new OleDbSchemaDef("Sql_Languages",OleDbSchemaGuid.Sql_Languages),
            new OleDbSchemaDef("Statistics",OleDbSchemaGuid.Statistics),
            new OleDbSchemaDef("Table_Constraints",OleDbSchemaGuid.Table_Constraints),
            new OleDbSchemaDef("Table_Privileges",OleDbSchemaGuid.Table_Privileges),
            new OleDbSchemaDef("Table_Statistics",OleDbSchemaGuid.Table_Statistics),
            new OleDbSchemaDef("Tables",OleDbSchemaGuid.Tables),
            new OleDbSchemaDef("Tables_Info",OleDbSchemaGuid.Tables_Info),
            new OleDbSchemaDef("Translations",OleDbSchemaGuid.Translations),
            new OleDbSchemaDef("Trustee",OleDbSchemaGuid.Trustee),
            new OleDbSchemaDef("Usage_Privileges",OleDbSchemaGuid.Usage_Privileges),
            new OleDbSchemaDef("View_Column_Usage",OleDbSchemaGuid.View_Column_Usage),
            new OleDbSchemaDef("View_Table_Usage",OleDbSchemaGuid.View_Table_Usage),
            new OleDbSchemaDef("Views",OleDbSchemaGuid.Views),
        };
    }

    public class OleDbSchemasTreeNode : LateLoadChildrenConnectionTreeNodeBase
    {
        string m_dbname;
        IDatabaseSource m_conn;

        public OleDbSchemasTreeNode(IDatabaseSource conn, ITreeNode parent, string dbname)
            : base(conn, parent, "oledbschemas")
        {
            m_dbname = dbname;
            m_conn = conn;
        }

        public override string Title
        {
            get { return "OLE DB Medatada"; }
        }

        public override string TypeTitle
        {
            get { return "OLE DB Medatada"; }
        }

        protected override void DoGetChildren()
        {
            List<ITreeNode> res = new List<ITreeNode>();
            OleDbConnection conn = (OleDbConnection)m_conn.Connection.SystemConnection;
            DataTable tbl = conn.GetOleDbSchemaTable(OleDbSchemaGuid.SchemaGuids, null);
            Dictionary<Guid, bool> supportedGuids = new Dictionary<Guid, bool>();
            foreach (DataRow row in tbl.Rows)
            {
                supportedGuids[new Guid(row[0].ToString())] = false;
            }
            foreach (OleDbSchemaDef def in OleDbSchemaDef.SchemaDefs)
            {
                if (!supportedGuids.ContainsKey(def.guid)) continue;
                supportedGuids[def.guid] = true;
                res.Add(new OleDbSchemaTreeNode(m_conn, this, m_dbname, def.name, def.guid));
            }
            foreach (Guid guid in supportedGuids.Keys)
            {
                if (supportedGuids[guid]) continue;
                res.Add(new OleDbSchemaTreeNode(m_conn, this, m_dbname, guid.ToString(), guid));
            }
            m_children = res.ToArray();
        }

        public override System.Drawing.Bitmap Image
        {
            get { return CoreIcons.table_data; }
        }

        public override ITabularDataView GetTabularData()
        {
            return new OleDbTabularData(m_conn.Connection, OleDbSchemaGuid.SchemaGuids, "SchemaGuids", m_dbname);
        }

        protected override bool HasOwnTabularData
        {
            get { return true; }
        }
    }

    public class OleDbSchemaTreeNode : ConnectionUsageTreeNodeBase
    {
        string m_collectionName;
        string m_dbname;
        Guid m_guid;
        public OleDbSchemaTreeNode(IDatabaseSource conn, ITreeNode parent, string dbname, string collectionName, Guid guid)
            : base(conn, parent, collectionName)
        {
            m_collectionName = collectionName;
            m_dbname = dbname;
            m_guid = guid;

            //Properties["oledbcollection"] = collectionName;
            //Properties["oledbguid"] = guid;
        }

        public override ITabularDataView GetTabularData()
        {
            return new OleDbTabularData(m_usageConn.Connection, m_guid, m_collectionName, m_dbname);
        }

        protected override bool HasOwnTabularData
        {
            get { return true; }
        }

        public override string Title
        {
            get { return m_collectionName; }
        }

        public override string TypeTitle
        {
            get { return "OLE DB Schema"; }
        }

        public override ITreeNode[] GetChildren()
        {
            return new ITreeNode[] { };
        }

        public override System.Drawing.Bitmap Image
        {
            get { return CoreIcons.table_data; }
        }
    }

    public class OleDbTabularData : DataTableTabularData
    {
        string m_collectionName;
        Guid m_collectionGuid;
        string m_dbname;

        public OleDbTabularData(IPhysicalConnection conn, Guid collectionGuid, string collectionName, string dbname)
            : base(conn)
        {
            m_collectionName = collectionName;
            m_collectionGuid = collectionGuid;
            m_dbname = dbname;
        }

        public override ITabularDataView CloneView()
        {
            return new OleDbTabularData(m_conn.Clone(), m_collectionGuid, m_collectionName, m_dbname);
        }

        public override DataTable LoadTable(IPhysicalConnection pconn)
        {
            if (!String.IsNullOrEmpty(m_dbname)) pconn.SystemConnection.ChangeDatabase(m_dbname);
            OleDbConnection conn = (OleDbConnection)pconn.SystemConnection;
            try
            {
                return conn.GetOleDbSchemaTable(m_collectionGuid, null);
            }
            catch (Exception err)
            {
                conn.FillInfo(err.Data);
                err.Data["oledb_collection_guid"] = m_collectionGuid;
                err.Data["oledb_collection_name"] = m_collectionName;
                throw;
            }
        }

        public override SettingsPageCollection Settings
        {
            get { return Connection.FindSettings(m_dbname); }
        }

        public override string ToString()
        {
            return m_collectionName ?? "Collections";
        }
    }

    [TreeExtender(Name = "oledb")]
    public class OleDbNodeTreeExtender : TreeExtenderBase
    {
        private bool CanBeUsed(ITreeNode node)
        {
            if (node is SystemDbObjectsNode)
            {
                SystemDbObjectsNode n = (SystemDbObjectsNode)node;
                if (n.GetConnection() != null && n.GetConnection().SystemConnection is OleDbConnection) return true;
            }
            return false;
        }


        public override void GetExtendedChildren(ITreeNode parent, List<ITreeNode> children)
        {
            if (!CanBeUsed(parent)) return;
            children.Add(new OleDbSchemasTreeNode(((SystemDbObjectsNode)parent).Database, parent, ((SystemDbObjectsNode)parent).Database.DatabaseName));
        }
    }
}
