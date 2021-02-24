using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Design;
using System.ComponentModel.Design;

namespace DatAdmin
{
    public partial class DbInfoWallWidgetFrame : WidgetBaseFrame
    {
        DbInfoWallEditor m_editor;
        string m_text;

        public DbInfoWallWidgetFrame(DbInfoWallWidget widget)
            : base(widget)
        {
            InitializeComponent();
        }

        private new DbInfoWallWidget Widget { get { return (DbInfoWallWidget)m_widget; } }

        public override void OnBeginDesign()
        {
            base.OnBeginDesign();
            htmlPanelEx1.Visible = false;
            m_editor = new DbInfoWallEditor(Widget);
            Controls.Add(m_editor);
            m_editor.Dock = DockStyle.Fill;
        }

        public override void BeforeSave()
        {
            if (m_editor != null) m_editor.SaveItems();
        }

        public override void OnFinishDesign()
        {
            m_editor.SaveItems();
            m_editor.Dispose();
            m_editor = null;
            htmlPanelEx1.Visible = true;
            CallLoad(m_appobj);
        }

        protected override void DoLoadData()
        {
            if (m_editor != null) return;
            if (m_appobj == null)
            {
                m_text = "";
                return;
            }

            IPhysicalConnection pconn = m_appobj.FindPhysicalConnection(ConnPack);
            bool dbset = false;
            HtmlGenerator gen = new HtmlGenerator();
            gen.BeginHtml(VersionInfo.ProgramTitle, HtmlGenerator.HtmlObjectViewStyle);
            if (Widget.Items.Count > 0)
            {
                gen.PropsTableBegin();
                foreach (var item in Widget.Items)
                {
                    var dbcache = pconn.Cache.Database(m_appobj.GetObjectPath().DbName);
                    string value = (string)dbcache.Get("dbinfowall", item.Value);
                    if (value == null)
                    {
                        if (!dbset)
                        {
                            pconn.SystemConnection.SafeChangeDatabase(m_appobj.GetObjectPath());
                            dbset = true;
                        }
                        value = pconn.SystemConnection.ExecuteScalar(item.Value).SafeToString() ?? "";
                        dbcache.Put("dbinfowall", item.Value, value);
                    }
                    gen.PropTableRow(Texts.Get(item.Name), value);
                }
                gen.PropsTableEnd();
            }
            else
            {
                gen.Write(Texts.Get("s_no_data"));
            }
            gen.EndHtml();
            m_text = gen.HtmlText;
        }

        protected override void ShowDataInGui()
        {
            htmlPanelEx1.Text = m_text;
            htmlPanelEx1.Visible = m_editor == null;
        }
    }

    public class DbInfoWallWidgetItem
    {
        [XmlElem]
        public string Name { get; set; }

        [XmlElem]
        [SyntaxEditorLanguage(CodeLanguage.Sql)]
        [Editor(typeof(SyntaxEditor), typeof(UITypeEditor))]
        public string Value { get; set; }
    }

    [Widget(Name = "dbinfowall", Title = "DB Info Wall")]
    public class DbInfoWallWidget : WidgetBase
    {
        [XmlCollection(typeof(DbInfoWallWidgetItem), "Item")]
        [Editor(typeof(CollectionEditor), typeof(UITypeEditor))]
        public List<DbInfoWallWidgetItem> Items { get; set; }

        public DbInfoWallWidget()
        {
            Items = new List<DbInfoWallWidgetItem>();
        }

        public override Bitmap DefaultImage
        {
            get { return CoreIcons.info; }
        }

        public override string DefaultPageTitle
        {
            get { return "s_summary"; }
        }

        public override Type GetControlType()
        {
            return typeof(DbInfoWallWidgetFrame);
        }

        protected override WidgetBaseFrame CreateControl()
        {
            return new DbInfoWallWidgetFrame(this);
        }
    }
}
