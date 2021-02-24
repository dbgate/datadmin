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
    public partial class HistogramCDSCFrame : UserControl
    {
        DataSourceConfigurator m_cfg;

        public HistogramCDSCFrame(DataSourceConfigurator cfg)
        {
            InitializeComponent();
            m_cfg = cfg;
            foreach (var col in m_cfg.Table.Columns)
            {
                cbxColumn.Items.Add(col.ColumnName);
            }
        }

        internal IChartDataProcessor GetProcessor()
        {
            var res = new HistogramChartDataProcessor();
            res.Column = cbxColumn.SelectedItem.ToString();
            return res;
        }

        private void cbxColumn_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_cfg.CallChanged();
        }

        internal void LoadFromProcessor(HistogramChartDataProcessor proc)
        {
            cbxColumn.SelectedIndex = cbxColumn.Items.IndexOf(proc.Column);
        }
    }

    public class HistogramDataSourceConfigurator : DataSourceConfigurator
    {
        HistogramCDSCFrame m_editor;

        public HistogramDataSourceConfigurator(ITableStructure table)
            : base(table)
        {
        }

        public override Control GetEditor()
        {
            if (m_editor == null) m_editor = new HistogramCDSCFrame(this);
            return m_editor;
        }

        public override IChartDataProcessor GetProcessor()
        {
            return m_editor.GetProcessor();
        }

        public override string ToString()
        {
            return Texts.Get("s_histogram");
        }

        public override void LoadFromProcessor(IChartDataProcessor proc)
        {
            GetEditor();
            m_editor.LoadFromProcessor((HistogramChartDataProcessor)proc);
        }

        public override Type GetProcessorType()
        {
            return typeof(HistogramChartDataProcessor);
        }
    }
}
