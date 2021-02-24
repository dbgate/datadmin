using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Drawing;

namespace DatAdmin
{
    [SpecificRepresentation(Name = "view", Title = "s_dbview")]
    public class ViewRepresentation : SpecificObjectRepresentationBase
    {
        public override Bitmap Icon
        {
            get { return CoreIcons.view; }
        }

        public override string TitlePlural
        {
            get { return "s_dbviews"; }
        }

        public override string TitleSingular
        {
            get { return "s_dbview"; }
        }

        public override string ObjectType
        {
            get { return "view"; }
        }

        public override string XmlElementName
        {
            get { return "View"; }
        }
    }

    [SpecificRepresentation(Name = "procedure", Title = "s_procedure")]
    public class ProcedureRepresentation : SpecificObjectRepresentationBase
    {
        public override Bitmap Icon
        {
            get { return CoreIcons.procedure; }
        }

        public override string TitlePlural
        {
            get { return "s_procedures"; }
        }

        public override string TitleSingular
        {
            get { return "s_procedure"; }
        }

        public override string ObjectType
        {
            get { return "procedure"; }
        }

        public override string XmlElementName
        {
            get { return "Procedure"; }
        }
    }

    [SpecificRepresentation(Name = "function", Title = "s_function")]
    public class FunctionRepresentation : SpecificObjectRepresentationBase
    {
        public override Bitmap Icon
        {
            get { return CoreIcons.function; }
        }

        public override string TitlePlural
        {
            get { return "s_functions"; }
        }

        public override string TitleSingular
        {
            get { return "s_function"; }
        }

        public override string ObjectType
        {
            get { return "function"; }
        }

        public override string XmlElementName
        {
            get { return "Function"; }
        }
    }

    [SpecificRepresentation(Name = "trigger", Title = "s_trigger")]
    public class TriggerRepresentation : SpecificObjectRepresentationBase
    {
        public override Bitmap Icon
        {
            get { return CoreIcons.trigger; }
        }

        public override string TitlePlural
        {
            get { return "s_triggers"; }
        }

        public override string TitleSingular
        {
            get { return "s_trigger"; }
        }

        public override string ObjectType
        {
            get { return "trigger"; }
        }

        public override string XmlElementName
        {
            get { return "Trigger"; }
        }
    }

    [SpecificRepresentation(Name = "sequence", Title = "s_sequence")]
    public class SequenceRepresentation : SpecificObjectRepresentationBase
    {
        public override Bitmap Icon
        {
            get { return CoreIcons.sequence; }
        }

        public override string TitlePlural
        {
            get { return "s_sequences"; }
        }

        public override string TitleSingular
        {
            get { return "s_sequence"; }
        }

        public override string ObjectType
        {
            get { return "sequence"; }
        }

        public override string XmlElementName
        {
            get { return "Sequence"; }
        }
    }

    [SpecificRepresentation(Name = "table", Title = "s_table")]
    public class TableRepresentation : SpecificObjectRepresentationBase
    {
        public override Bitmap Icon
        {
            get { return CoreIcons.table; }
        }

        public override string TitlePlural
        {
            get { return "s_tables"; }
        }

        public override string TitleSingular
        {
            get { return "s_table"; }
        }

        public override string ObjectType
        {
            get { return "table"; }
        }

        public override string XmlElementName
        {
            get { return "Table"; }
        }
    }

    public class ViewObjectType : SpecificObjectTypeBase
    {
        public ViewObjectType(ISqlDialect dialect)
            : base(dialect)
        {
            CreateNewTemplate = CoreRes.createview;
        }
        public override DataTable LoadOverview(DbConnection conn, ObjectPath parpath)
        {
            conn.SafeChangeDatabase(parpath);
            return conn.LoadTableFromQuery("SELECT * FROM INFORMATION_SCHEMA.VIEWS");
        }

        public override bool SupportsLoadOverview
        {
            get { return true; }
        }

        public override string ObjectType
        {
            get { return "view"; }
        }

        public override bool HasSystemVariant
        {
            get { return true; }
        }

        public override bool HasTabularData
        {
            get { return true; }
        }

        internal ITabularDataView GetTabularData(IPhysicalConnection conn, ObjectPath objpath)
        {
            return new GenericTabularDataView(
                conn,
                objpath.DbName,
                "SELECT * FROM " + conn.Dialect.QuoteFullName(objpath.ObjectName),
                "SELECT COUNT(*) FROM " + conn.Dialect.QuoteFullName(objpath.ObjectName),
                "DELETE FROM " + conn.Dialect.QuoteFullName(objpath.ObjectName),
                objpath, true, false, null, null);
        }

        public override ITabularDataView MergeTabularData(IPhysicalConnection conn, ObjectPath objpath)
        {
            return GetTabularData(conn, objpath);
        }

        public override SpecificObjectMenuCommandsBase CreateMenu()
        {
            return new ViewMenu();
        }

        internal class ViewMenu : SpecificObjectMenuCommandsBase
        {
            [PopupMenu("s_open_data", ImageName = CoreIcons.table_dataName, Weight = MenuWeights.OPEN1)]
            public void OpenData()
            {
                var data = ((ViewObjectType)Parent).GetTabularData(Connection.Clone(), FullName);
                MainWindow.Instance.OpenContent(new TableDataFrame(data));
            }
        }

        //public override System.Drawing.Bitmap Icon
        //{
        //    get { return CoreIcons.view; }
        //}

        //public override string ListTitle
        //{
        //    get { return "s_views"; }
        //}

        //protected override string NameColumn
        //{
        //    get { return "TABLE_NAME"; }
        //}
        //protected override string SchemaColumn
        //{
        //    get { return "TABLE_SCHEMA"; }
        //}
        //public override DbObjectParent ParentType
        //{
        //    get { return DbObjectParent.Database; }
        //}
        //public override bool SupportsScript(DbObjectScript script, ObjectPath objpath)
        //{
        //    return true;
        //}
        //public override string GenerateCreate(DbConnection conn, ObjectPath objpath)
        //{
        //    string sql = String.Format("SELECT VIEW_DEFINITION FROM INFORMATION_SCHEMA.VIEWS WHERE TABLE_NAME='{0}' AND TABLE_SCHEMA='{1}'", objpath.ObjectName.Name, objpath.ObjectName.Schema);
        //    return DbConnectionExtension.ExecuteScalar<string>(conn, sql);
        //}
        //public override string GenerateDrop(DbConnection conn, ObjectPath objpath)
        //{
        //    return "DROP VIEW " + DialectExtension.QuoteFullName(m_dialect, objpath.ObjectName);
        //}
        //public override bool HasTabularData
        //{
        //    get { return true; }
        //}
    }

    public class GenericSpecificObjectType : SpecificObjectTypeBase
    {
        string m_objtype;

        public GenericSpecificObjectType(string objtype)
            : base(null)
        {
            m_objtype = objtype;
        }

        public override string ObjectType
        {
            get { return m_objtype; }
        }
    }
}
