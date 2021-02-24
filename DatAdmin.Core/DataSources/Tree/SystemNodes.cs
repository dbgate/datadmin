using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;

namespace DatAdmin
{
    public class SystemDbObjectsNode : ConnectionUsageTreeNodeBase
    {
        string m_dbname;
        IDatabaseSource m_conn;

        public SystemDbObjectsNode(IDatabaseSource conn, ITreeNode parent, string dbname)
            : base(conn, parent, "system_objects")
        {
            m_dbname = dbname;
            m_conn = conn;
        }

        public override string Title
        {
            get { return "s_system_objects"; }
        }

        public override string TypeTitle
        {
            get { return "s_system_objects"; }
        }

        public override ITreeNode[] GetChildren()
        {
            List<ITreeNode> res = new List<ITreeNode>();
            res.Add(new Tables_TreeNode(m_conn, this, true));
            res.Add(new AdoNetSchemasTreeNode(m_conn, this, m_dbname));
            this.GetDbObjectNodes(m_conn, res, DbObjectParent.Database, new ObjectPath(m_dbname), true);
            return res.ToArray();
        }

        public override System.Drawing.Bitmap Image
        {
            get { return CoreIcons.system; }
        }

        public IDatabaseSource Database { get { return m_conn; } }
    }

    public class AdoNetSchemasTreeNode : LateLoadChildrenConnectionTreeNodeBase
    {
        string m_dbname;
        IDatabaseSource m_conn;

        public AdoNetSchemasTreeNode(IDatabaseSource conn, ITreeNode parent, string dbname)
            : base(conn, parent, "adonetschemas")
        {
            m_dbname = dbname;
            m_conn = conn;
        }

        public override string Title
        {
            get { return "ADO.NET Medatada"; }
        }

        public override string TypeTitle
        {
            get { return "ADO.NET Medatada"; }
        }

        protected override void DoGetChildren()
        {
            List<ITreeNode> res = new List<ITreeNode>();
            DbConnection conn = m_conn.Connection.SystemConnection;
            DataTable tbl = conn.GetSchema();
            foreach (DataRow row in tbl.Rows)
            {
                res.Add(new AdoNetSchemaTreeNode(m_conn, this, m_dbname, row[0].ToString()));
            }
            m_children = res.ToArray();
        }

        public override ITabularDataView GetTabularData()
        {
            return new AdoSchemaTabularData(m_conn.Connection, null, m_dbname);
        }

        protected override bool HasOwnTabularData
        {
            get { return true; }
        }

        public override System.Drawing.Bitmap Image
        {
            get { return CoreIcons.table_data; }
        }
    }

    public class AdoNetSchemaTreeNode : ConnectionUsageTreeNodeBase
    {
        string m_collectionName;
        string m_dbname;
        public AdoNetSchemaTreeNode(IDatabaseSource conn, ITreeNode parent, string dbname, string collectionName)
            : base(conn, parent, collectionName)
        {
            m_collectionName = collectionName;
            m_dbname = dbname;

            //Properties["adonetcollection"] = collectionName;
        }

        public override ITabularDataView GetTabularData()
        {
            return new AdoSchemaTabularData(m_usageConn.Connection, m_collectionName, m_dbname);
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
            get { return "ADO.NET Schema"; }
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

    public class AdoSchemaTabularData : DataTableTabularData
    {
        string m_collectionName;
        string m_dbname;

        public AdoSchemaTabularData(IPhysicalConnection conn, string collectionName, string dbname)
            : base(conn)
        {
            m_collectionName = collectionName;
            m_dbname = dbname;
        }

        public override DataTable LoadTable(IPhysicalConnection pconn)
        {
            if (!String.IsNullOrEmpty(m_dbname)) pconn.SystemConnection.ChangeDatabase(m_dbname);
            if (m_collectionName == null) return pconn.SystemConnection.GetSchema();
            else return pconn.SystemConnection.GetSchema(m_collectionName);
        }

        public override ITabularDataView CloneView()
        {
            return new AdoSchemaTabularData(Connection.Clone(), m_collectionName, m_dbname);
        }

        public override string ToString()
        {
            return m_collectionName ?? "Collections";
        }

        public override SettingsPageCollection Settings { get { return Connection.FindSettings(m_dbname); } }
    }
}
