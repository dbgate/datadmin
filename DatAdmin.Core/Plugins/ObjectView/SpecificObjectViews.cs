using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

namespace DatAdmin
{
    [Widget(Name = "db_object_sql", Title = "Show DDL - DB Object", Category = "SQL View")]
    public class DbObjectSqlWidget : SyntaxTextWidgetBase
    {
        DbObjectScript m_script;

        public DbObjectSqlWidget(DbObjectScript script)
        {
            m_script = script;
        }

        public DbObjectSqlWidget()
            : this(DbObjectScript.Create)
        {
        }

        public override string CreateText(AppObject appobj, ConnectionPack connpack)
        {
            ObjectPath objpath = appobj.GetObjectPath();
            IDatabaseSource db = appobj.FindDatabaseConnection(connpack);
            IPhysicalConnection conn = appobj.FindPhysicalConnection(connpack);
            var sobj = appobj as SpecificObjectAppObject;
            if (conn != null && objpath != null)
            {
                string text = conn.InvokeR<string>((Func<string>)delegate()
                                    {
                                        ISpecificObjectStructure so = db.LoadSpecificObjectDetail(sobj.DbObjectType, objpath.ObjectName);
                                        return conn.Dialect.GenerateScript(dmp => { dmp.CreateSpecificObject(so); }, AppObjectSqlGeneratorBase.TemplateFormatProps);
                                    });
                if (conn.Dialect != null) text = conn.Dialect.ReformatSpecificObject(sobj.DbObjectType, text);
                return text;
            }
            return "";
        }

        public override string DefaultPageTitle
        {
            get { return "DDL"; }
        }

        public override System.Drawing.Bitmap DefaultImage
        {
            get { return CoreIcons.sql; }
        }
    }

    public class SpecificObjectsWidget : GridWidgetBase
    {
        ISpecificObjectType m_dbtype;
        ISpecificRepresentation m_repr;

        public SpecificObjectsWidget(ISpecificObjectType dbtype)
        {
            m_dbtype = dbtype;
            m_repr = SpecificRepresentationAddonType.Instance.FindRepresentation(m_dbtype.ObjectType);
        }

        public override void GetData(AppObject appobj, ConnectionPack connpack, out System.Data.DataTable data, out DatAdmin.Scripting.ObjectGrid grid)
        {
            ObjectPath objpath = appobj.GetObjectPath();
            IPhysicalConnection conn = appobj.FindPhysicalConnection(connpack);
            data = null;
            grid = null;
            if (conn != null && objpath != null)
            {
                data = InvokerExtension.InvokeR<DataTable>(conn, (Func<DataTable>)delegate()
                                    {
                                        conn.SystemConnection.SafeChangeDatabase(objpath);
                                        return m_dbtype.LoadOverview(conn.SystemConnection, objpath);
                                    });
            }
        }

        public override string DefaultPageTitle
        {
            get { return m_repr.TitlePlural; }
        }

        public override System.Drawing.Bitmap DefaultImage
        {
            get { return m_repr.Icon; }
        }
    }
}
