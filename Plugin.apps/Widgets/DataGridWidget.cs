using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using DatAdmin;
using System.ComponentModel;
using System.Drawing.Design;
using System.Drawing;
using System.Text.RegularExpressions;
using IronPython.Hosting;

namespace Plugin.apps
{
    [ApplicationWidget(Name = "datagrid", Title = "s_data_grid")]
    public class DataGridWidget : AppWidget
    {
        static Brush m_headerBrush = new SolidBrush(Color.Cyan);

        public override IWidgetControl CreateControl(AppPageInstance pagei)
        {
            return new DataGridWidgetControl(pagei, this);
        }

        public override System.Drawing.Image GetImage()
        {
            return CoreIcons.table;
        }

        [Browsable(false)]
        public override string NameTemplate
        {
            get { return "dataGrid"; }
        }

        public override void DrawControlInner(System.Drawing.Graphics g)
        {
            base.DrawControlInner(g);
            g.FillRectangle(m_headerBrush, new Rectangle(Left, Top, Width, 25));
            g.DrawString(Sql, m_infoFont, m_blackBrush, new Rectangle(Left, Top + 30, Width, Height - 30));
        }

        [XmlElem]
        [SyntaxEditorLanguage(CodeLanguage.Sql)]
        [Editor(typeof(SyntaxEditor), typeof(UITypeEditor))]
        [Category("s_data")]
        public string Sql { get; set; }
    }

    public class DataGridWidgetControl : TableDataFrame, IWidgetControl
    {
        DataGridWidget m_widget;
        AppPageInstance m_pagei;
        IPhysicalConnection m_conn;

        public string GetSql()
        {
            try
            {
                string sql = m_widget.Sql;
                var re = new Regex(@"###\$(.*)###");
                sql = re.Replace(sql, EvalExpression);
                return sql;
            }
            catch (ApiError)
            {
                return null;
            }
        }

        private string EvalExpression(Match m)
        {
            var vars = new Dictionary<string, object>();
            m_pagei.GetPageNamespace(vars);
            return m_pagei.AppEnv.Engine.Evaluate(m.Groups[1].Value, m_pagei.AppEnv.Engine.DefaultModule, vars).SafeToString();
        }

        public DataGridWidgetControl(AppPageInstance pagei, DataGridWidget widget)
        {
            m_widget = widget;
            m_pagei = pagei;
            m_conn = m_pagei.Database.Connection.Clone();
            ReloadData();
        }

        private void ReloadData()
        {
            if (!GetSql().IsEmpty())
            {
                TabularData = new GenericTabularDataView(m_conn, m_pagei.Database.DatabaseName, GetSql(), null, null, m_widget.Name, true, false, null);
            }
            else
            {
                TabularData = null;
            }
        }

        public AppWidget Widget { get { return m_widget; } }

        protected override void OnSelectionChanged()
        {
            m_pagei.CallNotifyWidgets(this);
        }

        public void ProcessNotify()
        {
            ReloadData();
        }

        internal object GetActiveRowValue(int col)
        {
            var row = DataGrid.GetCurrentRow();
            if (row == null) throw new ApiError("No active row is selected");
            return row[col];
        }

        #region IWidgetControl Members


        public Control Control
        {
            get { return this; }
        }

        public AppPageInstance PageInstance
        {
            get { return m_pagei; }
        }

        public DatAdmin.Scripting.AppWidget CreateScriptingControl()
        {
            return new DatAdmin.Scripting.DataGrid(this);
        }

        #endregion
    }
}

namespace DatAdmin.Scripting
{
    using Plugin.apps;

    public class DataGrid : AppWidget
    {
        DataGridWidgetControl m_ctrl;

        public DataGrid(DataGridWidgetControl ctrl)
            : base(ctrl.Widget)
        {
            m_ctrl = ctrl;
        }

        public object GetActiveRowValue(string colname)
        {
            var col = m_ctrl.DataGrid.Columns[colname];
            if (col == null) throw new ApiError("Column not found:" + colname);
            int colindex = col.Index;
            return m_ctrl.GetActiveRowValue(colindex);
        }
    }
}

