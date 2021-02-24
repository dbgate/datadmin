using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DatAdmin;

namespace Plugin.charts
{
    public partial class TimeLineItemFrame : UserControl
    {
        DataSourceConfigurator m_cfg;

        public TimeLineItemFrame(DataSourceConfigurator cfg)
        {
            InitializeComponent();
            m_cfg = cfg;

            cbxAggr.Items.Add("COUNT");
            cbxAggr.Items.Add("SUM");
            cbxAggr.Items.Add("MIN");
            cbxAggr.Items.Add("MAX");
            cbxAggr.Items.Add("AVG");
            cbxAggr.SelectedIndex = 0;

            cbxAggr2.Items.Add("SUM");
            cbxAggr2.Items.Add("FIRST");
            cbxAggr2.Items.Add("LAST");
            cbxAggr2.Items.Add("MIDDLE");
            cbxAggr2.Items.Add("MIN");
            cbxAggr2.Items.Add("MAX");
            cbxAggr2.Items.Add("AVG");
            cbxAggr2.SelectedIndex = 0;

            foreach (var col in m_cfg.Table.Columns)
            {
                cbxColumn.Items.Add(col.ColumnName);
            }
        }

        public event EventHandler OnRemoveItem;

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (OnRemoveItem != null) OnRemoveItem(this, e);
        }

        private void cbxAggr_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbxColumn.Visible = cbxAggr.SelectedIndex > 0;
            m_cfg.CallChanged();
        }

        public TimeLineItem CreateItem()
        {
            var res = new TimeLineItem();
            res.Column = cbxColumn.SelectedItem.SafeToString();
            res.Expression = (TimeLineExpression)Enum.Parse(typeof(TimeLineExpression), cbxAggr.SelectedItem.ToString(), true);
            res.TimeSelector = (TimeGroupSelector)Enum.Parse(typeof(TimeGroupSelector), cbxAggr2.SelectedItem.ToString(), true);
            return res;
        }

        private void cbxColumn_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_cfg.CallChanged();
        }

        void SetComboValue(ComboBox cbx, object value)
        {
            string sval = value.ToString().ToUpper();
            for (int i = 0; i < cbx.Items.Count; i++)
            {
                if (cbx.Items[i].ToString().ToUpper() == sval)
                {
                    cbx.SelectedIndex = i;
                    break;
                }
            }
        }

        internal void LoadFromItem(TimeLineItem item)
        {
            if (item.Column != null) cbxColumn.SelectedIndex = cbxColumn.Items.IndexOf(item.Column);
            SetComboValue(cbxAggr, item.Expression);
            SetComboValue(cbxAggr2, item.TimeSelector);
        }

        public bool AdvancedSettings
        {
            get
            {
                return cbxAggr2.Visible;
            }
            set
            {
                if (value == cbxAggr2.Visible) return;
                if (value)
                {
                    cbxColumn.Width -= cbxAggr2.Width;
                    cbxColumn.Left += cbxAggr2.Width;
                }
                else
                {
                    cbxColumn.Width += cbxAggr2.Width;
                    cbxColumn.Left -= cbxAggr2.Width;
                }
                cbxAggr2.Visible = value;
            }
        }
    }
}
