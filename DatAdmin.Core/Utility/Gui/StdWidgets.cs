using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing.Design;
using System.Drawing;

namespace DatAdmin
{
    [Widget(Name = "generic_ddl", Title = "Show DDL - Generic", Category = "SQL View")]
    public class GenericShowDDLWidget : SqlScriptSyntaxSqlWidget
    {
        //Bitmap m_icon;
        string m_sql;

        public GenericShowDDLWidget()
            : base(0, "DDL")
        {
        }

        [XmlElem]
        [SyntaxEditorLanguage(CodeLanguage.Sql)]
        [Editor(typeof(SyntaxEditor), typeof(UITypeEditor))]
        public string Sql
        {
            get { return m_sql; }
            set
            {
                m_sql = value;
                HDesigner.CallChangedWidget(this, HDesigner.WidgetPart.Data);
            }
        }

        [XmlElem]
        public int ColumnNumber
        {
            get { return m_colnumber; }
            set
            {
                m_colnumber = value;
                HDesigner.CallChangedWidget(this, HDesigner.WidgetPart.Data);
            }
        }

        //[XmlElem]
        //public string Text
        //{
        //    get { return m_title; }
        //    set
        //    {
        //        m_title = value;
        //        HDesigner.CallChangedWidget(this, HDesigner.WidgetPart.Title);
        //    }
        //}

        //public override Bitmap DefaultImage
        //{
        //    get
        //    {
        //        return Icon ?? base.DefaultImage;
        //    }
        //}

        //[XmlElem]
        //public Bitmap Icon
        //{
        //    get { return m_icon; }
        //    set
        //    {
        //        m_icon = value;
        //        HDesigner.CallChangedWidget(this, HDesigner.WidgetPart.Icon);
        //    }
        //}

        protected override string GetSelect(ObjectPath path)
        {
            string res = Sql;
            res = res.Replace("#DATABASE#", path.DbName);
            res = res.Replace("#SCHEMA#", path.ObjectName.SafeGetSchema());
            res = res.Replace("#NAME#", path.ObjectName.SafeGetName());
            return res;
        }
    }

    [Widget(Name = "generic_raw_grid", Title = "Query result", Category = "Raw grids")]
    public class GenericRawGridWidget : SqlScriptGridWidget
    {
        Bitmap m_icon;

        public GenericRawGridWidget()
            : base("", "Data", null)
        {
        }

        [XmlElem]
        [SyntaxEditorLanguage(CodeLanguage.Sql)]
        [Editor(typeof(SyntaxEditor), typeof(UITypeEditor))]
        public string Sql
        {
            get { return m_sql; }
            set
            {
                m_sql = value;
                HDesigner.CallChangedWidget(this, HDesigner.WidgetPart.Data);
            }
        }

        [XmlElem]
        public string Text
        {
            get { return m_title; }
            set
            {
                m_title = value;
                HDesigner.CallChangedWidget(this, HDesigner.WidgetPart.Title);
            }
        }

        public override Bitmap DefaultImage
        {
            get
            {
                return Icon ?? CoreIcons.table_data;
            }
        }

        [XmlElem]
        public Bitmap Icon
        {
            get { return m_icon; }
            set
            {
                m_icon = value;
                HDesigner.CallChangedWidget(this, HDesigner.WidgetPart.Icon);
            }
        }

        protected override string GetSelect(ObjectPath path)
        {
            string res = Sql;
            res = res.Replace("#DATABASE#", path.DbName);
            res = res.Replace("#SCHEMA#", path.ObjectName.SafeGetSchema());
            res = res.Replace("#NAME#", path.ObjectName.SafeGetName());
            return res;
        }
    }
}
