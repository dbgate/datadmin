using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Xml;

namespace DatAdmin
{
    public partial class AddQueryToFavoriteForm : FormEx
    {
        XmlDocument m_context;
        IVirtualFile m_file;
        QueryDesignFrame m_designFrame;
        string m_queryCode;

        public AddQueryToFavoriteForm(XmlDocument context, IVirtualFile file, QueryDesignFrame designFrame, string queryCode)
        {
            InitializeComponent();
            m_context = context;
            m_file = file;
            m_designFrame = designFrame;
            m_queryCode = queryCode;

            rbtDesign.Enabled = designFrame != null && designFrame.IsDesign;
            rbtLinkToFile.Enabled = file != null && file.DiskPath != null;
            addToFavoritesFrame1.FavoriteName = rbtLinkToFile.Enabled ? Path.GetFileNameWithoutExtension(file.DiskPath) : "SQL"; ;
            rbtExecute.Enabled = context != null;
        }

        private void UpdateFavorite()
        {
            if (rbtDesign.Checked)
            {
                var des = XmlTool.CreateDocument("Design");
                m_designFrame.Save(des.DocumentElement);
                addToFavoritesFrame1.Favorite = new OpenQueryDesignFavorite { Design = des, Context = m_context };
            }
            else
            {
                if (rbtExecute.Checked)
                {
                    if (rbtQueryText.Checked)
                    {
                        addToFavoritesFrame1.Favorite = new ExecuteQueryCodeFavorite { Query = m_queryCode, Context = m_context };
                    }
                    if (rbtLinkToFile.Checked)
                    {
                        addToFavoritesFrame1.Favorite = new ExecuteQueryFileFavorite { QueryFile = m_file.DiskPath, Context = m_context };
                    }
                }
                if (rbtOpen.Checked)
                {
                    if (rbtQueryText.Checked)
                    {
                        addToFavoritesFrame1.Favorite = new OpenQueryCodeFavorite { Query = m_queryCode, Context = m_context };
                    }
                    if (rbtLinkToFile.Checked)
                    {
                        addToFavoritesFrame1.Favorite = new OpenQueryFileFavorite { QueryFile = m_file.DiskPath, Context = m_context };
                    }
                }
            }
        }

        public static bool Run(XmlDocument context, IVirtualFile file, QueryDesignFrame designFrame, string queryCode, bool selectedDesign)
        {
            var win = new AddQueryToFavoriteForm(context, file, designFrame, queryCode);
            if (selectedDesign && win.rbtDesign.Enabled) win.rbtDesign.Checked = true;
            return AddToFavoriteForm.RunLoop(win, win.addToFavoritesFrame1, win.UpdateFavorite);
        }
    }

    public abstract class QueryContextFavorite : FavoriteBase, IFavoriteWithSql
    {
        public XmlDocument Context { get; set; }

        public override void SaveToXml(XmlElement xml)
        {
            base.SaveToXml(xml);
            if (Context != null) xml.AppendChild(xml.OwnerDocument.ImportNode(Context.DocumentElement, true));
        }

        public override void LoadFromXml(XmlElement xml)
        {
            base.LoadFromXml(xml);
            var cnt = xml.FindElement("Context");
            if (cnt != null)
            {
                Context = new XmlDocument();
                Context.AppendChild(Context.ImportNode(cnt, true));
            }
        }

        protected virtual bool IsAvailableSql() { return true; }

        public override void GetWidgets(List<IWidget> res)
        {
            base.GetWidgets(res);
            if (IsAvailableSql()) res.Add(new FavoriteSqlWidget());
        }

        #region IFavoriteWithSql Members

        public virtual string LoadSql() { return ""; }
        public virtual ISqlDialect GetDialect()
        {
            try
            {
                var dbx = (XmlElement)Context.SelectSingleNode("//Database");
                return ((IDatabaseSource)DatabaseSourceAddonType.Instance.LoadAddon(dbx)).Dialect;
            }
            catch
            {
                return GenericDialect.Instance;
            }
        }

        #endregion
    }

    [Favorite(Name = "openquerycode")]
    public class OpenQueryCodeFavorite : QueryContextFavorite
    {
        [XmlElem]
        public string Query { get; set; }

        public override Bitmap Image
        {
            get { return CoreIcons.sql; }
        }

        public override void Open()
        {
            var pars = new OpenQueryParameters();
            pars.SqlText = Query;
            pars.SavedContext = Context;
            var frm = new QueryFrame(null, pars);
            MainWindow.Instance.OpenContent(frm);
        }

        public override string Description
        {
            get { return "s_open_query"; }
        }

        public override string LoadSql()
        {
            return Query;
        }
    }

    [Favorite(Name = "openqueryfile")]
    public class OpenQueryFileFavorite : QueryContextFavorite
    {
        [XmlElem]
        public string QueryFile { get; set; }

        public override Bitmap Image
        {
            get { return CoreIcons.sql; }
        }

        public override void Open()
        {
            var pars = new OpenQueryParameters();
            pars.File = new DiskFile(QueryFile);
            //pars.SavedContext = Context;
            var frm = new QueryFrame(null, pars);
            MainWindow.Instance.OpenContent(frm);
        }

        public override string Description
        {
            get { return "s_open_query"; }
        }

        public override void DisplayProps(Action<string, string> display)
        {
            base.DisplayProps(display);
            display("s_file", QueryFile);
        }

        public override string LoadSql()
        {
            using (var sr = new StreamReader(QueryFile))
            {
                return sr.ReadToEnd();
            }
        }
    }

    public abstract class ExecuteQueryFavoriteBase : QueryContextFavorite
    {
        protected void ExecuteQueryText(string sql)
        {
            sql = QueryFrame.AskQueryParams(sql, null);
            if (sql == null) return;
            var conn = (IDatabaseSource)DatabaseSourceAddonType.Instance.LoadAddon(Context.DocumentElement.FindElement("Database"));
            var job = RunSqlJob.CreateJob(conn, sql);
            job.StartProcess();
        }

        public override Bitmap Image
        {
            get { return CoreIcons.run; }
        }
    }

    [Favorite(Name = "executequerycode")]
    public class ExecuteQueryCodeFavorite : ExecuteQueryFavoriteBase
    {
        [XmlElem]
        public string Query { get; set; }

        public override void Open()
        {
            ExecuteQueryText(Query);
        }

        public override string Description
        {
            get { return "s_execute_query"; }
        }

        public override string LoadSql()
        {
            return Query;
        }
    }

    [Favorite(Name = "executequeryfile")]
    public class ExecuteQueryFileFavorite : ExecuteQueryFavoriteBase
    {
        [XmlElem]
        public string QueryFile { get; set; }

        public override void Open()
        {
            using (var sr = new StreamReader(QueryFile))
            {
                ExecuteQueryText(sr.ReadToEnd());
            }
        }

        public override string Description
        {
            get { return "s_execute_query"; }
        }

        public override string LoadSql()
        {
            using (var sr = new StreamReader(QueryFile))
            {
                return sr.ReadToEnd();
            }
        }
    }


    [Favorite(Name = "openquerydesign")]
    public class OpenQueryDesignFavorite : QueryContextFavorite
    {
        public XmlDocument Design { get; set; }

        public override Bitmap Image
        {
            get { return CoreIcons.querydesign; }
        }

        public override void SaveToXml(XmlElement xml)
        {
            base.SaveToXml(xml);
            if (Design != null) xml.AppendChild(xml.OwnerDocument.ImportNode(Design.DocumentElement, true));
        }

        public override void LoadFromXml(XmlElement xml)
        {
            base.LoadFromXml(xml);
            var des = xml.FindElement("Design");
            if (des != null)
            {
                Design = new XmlDocument();
                Design.AppendChild(Design.ImportNode(des, true));
            }
        }

        public override void Open()
        {
            var pars = new OpenQueryParameters();
            pars.SavedContext = Context;
            pars.GoToDesign = true;
            pars.SavedDesign = Design;
            var frm = new QueryFrame(null, pars);
            MainWindow.Instance.OpenContent(frm);
        }

        protected override bool IsAvailableSql()
        {
            return false;
        }

        public override string Description
        {
            get { return "s_open_query"; }
        }
    }
}
