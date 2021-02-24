using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class TableDataWidgetFrame : WidgetBaseFrame
    {
        public TableDataWidgetFrame(IWidget widget)
            : base(widget)
        {
            InitializeComponent();
        }

        protected override void DoLoadData()
        {
            base.DoLoadData();
            ITabularDataView tabdata = null;
            if (m_appobj != null)
            {
                tabdata = m_appobj.GetTabularData(ConnPack);
            }
            if (AsyncTool.IsMainThread)
            {
                tableDataFrame1.TabularData = tabdata;
            }
            else
            {
                tableDataFrame1.Invoke((Action)(() => tableDataFrame1.TabularData = tabdata));
            }
        }

        public bool ReadOnly
        {
            get { return tableDataFrame1.ForceReadOnly; }
            set { tableDataFrame1.ForceReadOnly = value; }
        }

        public bool HideToolbars
        {
            get { return tableDataFrame1.HideToolbars; }
            set { tableDataFrame1.HideToolbars = value; }
        }

        private void tableDataFrame1_ReportError(object sender, ReportErrorEventArgs e)
        {
            Controls.ShowError(true, Errors.ExtractMessage(e.Error), ControlVisibility);
        }
    }

    [Widget(Name = "table_data", Title = "Tabular data", Category = "Data")]
    public class TableDataWidget : WidgetBase
    {
        bool m_readOnly = false;
        bool m_hideToolbars = false;

        protected override WidgetBaseFrame CreateControl()
        {
            return new TableDataWidgetFrame(this)
            {
                ReadOnly = m_readOnly,
                HideToolbars = m_hideToolbars,
            };
        }

        public override Type GetControlType()
        {
            return typeof(TableDataWidget);
        }

        public override string DefaultPageTitle
        {
            get { return "s_data"; }
        }

        public override Bitmap DefaultImage
        {
            get { return StdIcons.table_data; }
        }

        [XmlElem]
        public bool ReadOnly
        {
            get { return m_readOnly; }
            set
            {
                m_readOnly = value;
                if (m_ctrl != null) ((TableDataWidgetFrame)m_ctrl).ReadOnly = value;
            }
        }

        [XmlElem]
        public bool HideToolbars
        {
            get { return m_hideToolbars; }
            set
            {
                m_hideToolbars = value;
                if (m_ctrl != null) ((TableDataWidgetFrame)m_ctrl).HideToolbars = value;
            }
        }
    }
}
