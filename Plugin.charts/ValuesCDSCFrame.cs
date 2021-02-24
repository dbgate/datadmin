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
    public partial class ValuesCDSCFrame : UserControl
    {
        DataSourceConfigurator m_cfg;

        public ValuesCDSCFrame(DataSourceConfigurator cfg)
        {
            InitializeComponent();
            m_cfg = cfg;

            foreach (var col in m_cfg.Table.Columns)
            {
                cbxLabelColumn.Items.Add(col.ColumnName);
                chbValues.Items.Add(col.ColumnName);
            }
        }

        internal IChartDataProcessor GetProcessor()
        {
            var res = new ValuesChartDataProcessor();
            res.LabelColumn = cbxLabelColumn.SelectedItem.ToString();
            var vdefs = new List<ChartData.ValueDef>();
            foreach (int index in chbValues.CheckedIndices)
            {
                var def = new ChartData.ValueDef();
                def.Label = def.Column = chbValues.Items[index].ToString();
                vdefs.Add(def);
            }
            res.ValueDefs = vdefs;
            return res;
        }

        private void cbxLabelColumn_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_cfg.CallChanged();
        }

        private void chbValues_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            MainWindow.Instance.RunInMainWindow(m_cfg.CallChanged);
        }

        internal void LoadFromProcessor(ValuesChartDataProcessor proc)
        {
            cbxLabelColumn.SelectedIndex = cbxLabelColumn.Items.IndexOf(proc.LabelColumn);
            foreach (var vd in proc.ValueDefs)
            {
                var index = chbValues.Items.IndexOf(vd.Column);
                if (index >= 0) chbValues.SetItemChecked(index, true);
            }
        }
    }

    public class ValuesDataSourceConfigurator : DataSourceConfigurator
    {
        ValuesCDSCFrame m_editor;

        public ValuesDataSourceConfigurator(ITableStructure table)
            : base(table)
        {
        }

        public override Control GetEditor()
        {
            if (m_editor == null) m_editor = new ValuesCDSCFrame(this);
            return m_editor;
        }

        public override IChartDataProcessor GetProcessor()
        {
            return m_editor.GetProcessor();
        }

        public override string ToString()
        {
            return Texts.Get("s_values");
        }

        public override void LoadFromProcessor(IChartDataProcessor proc)
        {
            GetEditor();
            m_editor.LoadFromProcessor((ValuesChartDataProcessor)proc);
        }

        public override Type GetProcessorType()
        {
            return typeof(ValuesChartDataProcessor);
        }
    }
}
