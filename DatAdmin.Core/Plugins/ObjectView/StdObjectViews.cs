using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Drawing;
using System.ComponentModel;

namespace DatAdmin
{
    [Widget(Name = "connection_info_html", Title = "Information about connection", Category = "HTML Info")]
    public class ConnectionInfoWidget : HtmlWidgetBase
    {
        public override string CreateHtml(AppObject appobj, ConnectionPack connpack, IDictionary<string, object> outnames)
        {
            HtmlGenerator gen = new HtmlGenerator();
            IPhysicalConnection pconn = appobj.FindPhysicalConnection(connpack);
            string dbversion = null;
            if (pconn != null && pconn.SystemConnection != null)
            {
                try
                {
                    dbversion = pconn.SystemConnection.ServerVersion;
                }
                catch
                {
                    dbversion = null;
                }
            }
            IStoredConnection scon = pconn.StoredConnection;
            gen.BeginHtml(VersionInfo.ProgramTitle, HtmlGenerator.HtmlObjectViewStyle);
            gen.Heading(Texts.Get("s_properties"), 2);
            gen.PropsTableBegin();
            if (scon != null) gen.PropTableRow("s_path", scon.FileName);
            var attr = XmlTool.GetRegisterAttr(scon);
            if (attr != null) gen.PropTableRow("s_database_engine", attr.Title ?? attr.Name);
            if (scon != null)
            {
                gen.PropTableRow("s_host", scon.GetDataSource());
                gen.PropTableRow("s_database", scon.ExplicitDatabaseName);
                gen.PropTableRow("s_login", scon.GetLogin());
            }
            else
            {
                gen.PropTableRow("s_path", pconn.PhysicalFactory.GetDataSource());
            }
            if (dbversion != null) gen.PropTableRow("s_version", dbversion);
            gen.PropsTableEnd();
            gen.EndHtml();
            return gen.HtmlText;
        }

        //private string MakeNiceEngine(string eng)
        //{
        //    switch (eng)
        //    {
        //        case "mssql": return "Microsoft SQL Server";
        //        case "mysql": return "MySQL";
        //        case "sqlite": return "SQLite";
        //        case "generic": return "Generic provider";
        //        case "postgre": return "Postgre SQL";
        //        case "access": return "MS Access";
        //        case "effiproz": return "EffiProz";
        //    }
        //    return eng;
        //}

        public override string DefaultPageTitle
        {
            get { return "s_connection"; }
        }
    }

    [Widget(Name = "table_info_html", Title = "Information about table", Category = "HTML Info")]
    public class TableInfoWidget : HtmlWidgetBase
    {
        bool m_showTableName = true;

        [XmlElem]
        public bool ShowTableName
        {
            get { return m_showTableName; }
            set
            {
                m_showTableName = value;
                HDesigner.CallChangedWidget(this, HDesigner.WidgetPart.Data);
            }
        }

        public override string CreateHtml(AppObject appobj, ConnectionPack connpack, IDictionary<string, object> outnames)
        {
            ObjectPath objpath = appobj.GetObjectPath();
            IPhysicalConnection conn = appobj.FindPhysicalConnection(connpack);
            if (conn != null && conn.IsOpened)
            {
                return conn.InvokeR<string>((Func<string>)delegate()
                {
                    string rowcount = "???", colcount = "???";
                    if (conn.SystemConnection != null)
                    {
                        var dbcache = conn.Cache.Database(appobj.GetObjectPath().DbName);

                        var dialect = conn.GetAnyDialect();

                        bool dbset = false;
                        rowcount = (string)dbcache.Get("htmlinfowidget_rows", objpath.ObjectName.ToString());
                        if (rowcount == null)
                        {
                            if (!dbset)
                            {
                                conn.SystemConnection.SafeChangeDatabase(objpath);
                                dbset = true;
                            }

                            rowcount = conn.SystemConnection.ExecuteScalar("SELECT COUNT(*) FROM " + dialect.QuoteFullName(objpath.ObjectName)).ToString();
                            try
                            {
                                int ival = Int32.Parse(rowcount);
                                rowcount = ival.FormatInt();
                            }
                            catch { }
                            dbcache.Put("htmlinfowidget_rows", objpath.ObjectName.ToString(), rowcount);
                        }

                        colcount = (string)dbcache.Get("htmlinfowidget_cols", objpath.ObjectName.ToString());
                        if (colcount == null)
                        {
                            if (!dbset)
                            {
                                conn.SystemConnection.SafeChangeDatabase(objpath);
                                dbset = true;
                            }
                            using (var cmd = conn.SystemConnection.CreateCommand())
                            {
                                cmd.CommandText = "SELECT * FROM " + dialect.QuoteFullName(objpath.ObjectName);
                                using (var reader = cmd.ExecuteReader(CommandBehavior.SchemaOnly))
                                {
                                    colcount = reader.GetSchemaTable().Rows.Count.ToString();
                                }
                                dbcache.Put("htmlinfowidget_cols", objpath.ObjectName.ToString(), colcount);
                            }
                        }
                    }
                    else
                    {
                        var db = appobj.FindDatabaseConnection(connpack);
                        if (db != null)
                        {
                            var ts = db.InvokeLoadTableStructure(objpath.ObjectName, TableStructureMembers.ColumnNames);
                            colcount = ts.Columns.Count.ToString();
                        }
                        //var tbl = appobj.FindTableConnection(connpack);
                        //if (tbl != null)
                        //{
                        //    var tabdata = tbl.GetTabularData();
                        //    if (tabdata != null)
                        //    {
                        //        int? rcnt = tabdata.LoadRowCount(new TableDataSetProperties());
                        //        if (rcnt != null) rowcount = rcnt.ToString();
                        //    }
                        //}
                    }

                    HtmlGenerator gen = new HtmlGenerator();
                    gen.BeginHtml(objpath.ObjectName.ToString(), HtmlGenerator.HtmlObjectViewStyle);
                    if (ShowTableName)
                    {
                        gen.Heading(objpath.ObjectName.ToString(), 2);
                    }
                    gen.PropsTableBegin();
                    gen.PropTableRow("s_row_count", rowcount);
                    gen.PropTableRow("s_column_count", colcount);
                    gen.PropsTableEnd();
                    return gen.HtmlText;
                });
            }
            return "";
        }

        public override Bitmap DefaultImage
        {
            get { return CoreIcons.sum; }
        }

        public override string DefaultPageTitle
        {
            get { return "s_table_statistics"; }
        }
    }

    [Widget(Name = "favorite_info_html", Title = "Information about favorite", Category = "HTML Info")]
    public class FavoriteInfoWidget : HtmlWidgetBase
    {
        public override string CreateHtml(AppObject appobj, ConnectionPack connpack, IDictionary<string, object> outnames)
        {
            var fav = (FavoriteAppObject)appobj;
            var holder = fav.LoadHolder();
            HtmlGenerator gen = new HtmlGenerator();
            gen.BeginHtml(holder.Name, HtmlGenerator.HtmlObjectViewStyle);
            gen.Heading(holder.Name, 2);
            gen.PropsTableBegin();
            gen.PropTableRow("s_name", IOTool.RelativePathTo(Core.FavoritesDirectory, fav.FileName));
            holder.Favorite.DisplayProps((n, v) => gen.PropTableRow(n, v));
            gen.PropsTableEnd();
            gen.BeginUl();
            gen.Li(String.Format("<a href='callback://open'>{0}</a>", Texts.Get("s_execute")));
            gen.EndUl();
            gen.EndHtml();
            outnames["open"] = (Action)delegate() { holder.Favorite.Open(); };
            return gen.HtmlText;
        }

        public override string DefaultPageTitle
        {
            get { return "s_summary"; }
        }
    }

    [Widget(Name = "favorite_sql", Title = "Favorite SQL", Category = "SQL View")]
    public class FavoriteSqlWidget : SyntaxTextWidgetBase
    {
        public override string CreateText(AppObject appobj, ConnectionPack connpack)
        {
            var fav = appobj as FavoriteAppObject;
            if (fav == null) return "";
            var holder = fav.LoadHolder();
            var favsql = holder.Favorite as IFavoriteWithSql;
            if (favsql == null) return "";
            return favsql.LoadSql();
        }

        public override string DefaultPageTitle
        {
            get { return "SQL"; }
        }

        public override Bitmap DefaultImage
        {
            get { return CoreIcons.sql; }
        }

        public override ISqlDialect GetDialect(AppObject appobj, ConnectionPack connpack)
        {
            var fav = appobj as FavoriteAppObject;
            if (fav == null) return null;
            var holder = fav.LoadHolder();
            var favsql = holder.Favorite as IFavoriteWithSql;
            if (favsql == null) return GenericDialect.Instance;
            return favsql.GetDialect();
        }
    }

    //public class ScriptingPropertiesObjectView : GridObjectViewBase
    //{
    //    public override void GetData(ITreeNode node, out DataTable data, out DatAdmin.Scripting.ObjectGrid grid)
    //    {
    //        data = null;
    //        grid = new DatAdmin.Scripting.ObjectGrid();
    //        grid.AddColumn(Texts.Get("s_name"));
    //        grid.AddColumn(Texts.Get("s_value"));
    //        foreach (string prop in node.Properties.Keys)
    //        {
    //            grid.AddRow(prop, node.Properties[prop]);
    //        }
    //    }

    //    public override string PageTitle
    //    {
    //        get { return Texts.Get("s_scripting_properties"); }
    //    }
    //}

    public abstract class GetSchemaWidget : GridWidgetBase
    {
        public abstract DataTable LoadSchema(DbConnection conn, ObjectPath objpath);

        public override void GetData(AppObject appobj, ConnectionPack connpack, out System.Data.DataTable data, out DatAdmin.Scripting.ObjectGrid grid)
        {
            ObjectPath objpath = appobj.GetObjectPath();
            IPhysicalConnection conn = appobj.FindPhysicalConnection(connpack);
            data = null;
            grid = null;
            if (conn != null && conn.IsOpened && conn.SystemConnection != null)
            {
                data = conn.InvokeR<DataTable>((Func<DataTable>)delegate()
                {
                    conn.SystemConnection.SafeChangeDatabase(objpath);
                    return LoadSchema(conn.SystemConnection, objpath);
                });
            }
        }
    }

    [Widget(Name = "tables_raw_grid", Title = "Tables", Category = "Raw grids")]
    public class TablesRawGridWidget : GetSchemaWidget
    {
        public override DataTable LoadSchema(DbConnection conn, ObjectPath objpath)
        {
            return conn.GetSchema("Tables").SelectNewTable("1=1", "TABLE_NAME ASC");
        }

        public override string DefaultPageTitle
        {
            get { return "s_tables"; }
        }

        public override System.Drawing.Bitmap DefaultImage
        {
            get { return CoreIcons.table; }
        }
    }

    [Widget(Name = "columns_raw_grid", Title = "Columns", Category = "Raw grids")]
    public class ColumnsRawGridWidget : GetSchemaWidget
    {
        public override DataTable LoadSchema(DbConnection conn, ObjectPath objpath)
        {
            DataTable res = conn.GetSchema("Columns", new string[] { null, objpath.ObjectName.Schema, objpath.ObjectName.Name });
            return res.SelectNewTable("1=1", "ORDINAL_POSITION ASC");
        }

        public override string DefaultPageTitle
        {
            get { return "s_columns"; }
        }

        public override System.Drawing.Bitmap DefaultImage
        {
            get { return CoreIcons.column; }
        }
    }

    [Widget(Name = "databases_raw_grid", Title = "Databases", Category = "Raw grids")]
    public class DatabasesRawGridWidget : GetSchemaWidget
    {
        public override DataTable LoadSchema(DbConnection conn, ObjectPath objpath)
        {
            DataTable res = conn.GetSchema("Databases");
            return res;
        }

        public override string DefaultPageTitle
        {
            get { return "s_databases"; }
        }

        public override System.Drawing.Bitmap DefaultImage
        {
            get { return CoreIcons.database; }
        }
    }

    public abstract class SqlScriptGridWidget : GridWidgetBase
    {
        protected string m_sql;
        protected string m_title;
        protected Bitmap m_image;

        public SqlScriptGridWidget(string sql, string title, Bitmap image)
        {
            m_sql = sql;
            m_title = title;
            m_image = image;
        }

        protected virtual string GetSelect(ObjectPath path)
        {
            return m_sql;
        }

        public override void GetData(AppObject appobj, ConnectionPack connpack, out System.Data.DataTable data, out DatAdmin.Scripting.ObjectGrid grid)
        {
            ObjectPath objpath = appobj.GetObjectPath();
            IPhysicalConnection conn = appobj.FindPhysicalConnection(connpack);
            data = null;
            grid = null;
            if (conn != null && conn.IsOpened)
            {
                data = conn.InvokeR<DataTable>((Func<DataTable>)delegate()
                {
                    conn.SystemConnection.SafeChangeDatabase(objpath);
                    using (DbCommand cmd = conn.SystemConnection.CreateCommand())
                    {
                        cmd.CommandText = GetSelect(objpath);
                        using (DbDataReader reader = cmd.ExecuteReader())
                        {
                            return reader.ToDataTable();
                        }
                    }
                });
            }
        }

        public override string DefaultPageTitle
        {
            get { return m_title; }
        }

        public override Bitmap DefaultImage
        {
            get { return m_image; }
        }
    }

    public abstract class SqlScriptSyntaxSqlWidget : SyntaxTextWidgetBase
    {
        protected int m_colnumber;
        protected string m_defaultTitle;

        public SqlScriptSyntaxSqlWidget(int colnumber, string title)
        {
            m_defaultTitle = title;
            m_colnumber = colnumber;
        }

        protected abstract string GetSelect(ObjectPath path);

        public override string CreateText(AppObject appobj, ConnectionPack connpack)
        {
            ObjectPath objpath = appobj.GetObjectPath();
            IDatabaseSource db = appobj.FindDatabaseConnection(connpack);
            IPhysicalConnection conn = appobj.FindPhysicalConnection(connpack);
            if (conn != null && objpath != null && conn.SystemConnection != null)
            {
                string text = conn.InvokeR<string>((Func<string>)delegate()
                {
                    conn.SystemConnection.SafeChangeDatabase(objpath);
                    string sql = GetSelect(objpath);
                    using (var cmd = conn.SystemConnection.CreateCommand())
                    {
                        cmd.CommandText = sql;
                        using (var reader = cmd.ExecuteReader(CommandBehavior.SingleRow))
                        {
                            if (reader.Read())
                            {
                                return reader[m_colnumber].SafeToString();
                            }
                        }
                    }
                    return "";
                });
                return text;
            }
            return "";
        }

        public override string DefaultPageTitle
        {
            get { return m_defaultTitle; }
        }

        public override System.Drawing.Bitmap DefaultImage
        {
            get { return CoreIcons.sql; }
        }
    }

    [Widget(Name = "db_table_sql", Title = "Show DDL - DB Table", Category = "SQL View")]
    public class CreateTableDDLObjectView : SyntaxTextWidgetBase
    {
        public override string CreateText(AppObject appobj, ConnectionPack connpack)
        {
            ObjectPath objpath = appobj.GetObjectPath();
            IDatabaseSource db = appobj.FindDatabaseConnection(connpack);
            IPhysicalConnection conn = appobj.FindPhysicalConnection(connpack);
            ITableSource tbl = appobj.FindTableConnection(connpack);
            if (conn != null && objpath != null)
            {
                string text = conn.InvokeR<string>((Func<string>)delegate()
                {
                    if (conn.SystemConnection != null) conn.SystemConnection.SafeChangeDatabase(objpath);
                    var sw = new StringWriter();
                    var gen = new GenSql_CreateTable();
                    gen.GenerateCreateTable(tbl, sw, conn.GetAnyDialect());
                    return sw.ToString();
                });
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


    public abstract class GuiWidget : WidgetBase
    {
    }
}