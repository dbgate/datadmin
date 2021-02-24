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
    public partial class TimeLineCDSCFrame : UserControl
    {
        DataSourceConfigurator m_cfg;
        List<TimeLineItemFrame> m_items = new List<TimeLineItemFrame>();

        public TimeLineCDSCFrame(DataSourceConfigurator cfg)
        {
            InitializeComponent();
            m_cfg = cfg;

            foreach (var col in m_cfg.Table.Columns)
            {
                cbxColumn.Items.Add(col.ColumnName);
            }

            FillSteps(cbxStep);
            FillSteps(cbxStep2);

            AddItem(true);

            chbAdvanced_CheckedChanged(this, EventArgs.Empty);
        }

        private void FillSteps(ComboBox steps)
        {
            steps.Items.Add(new StepItem { Label = "s_day", Step = TimeLineStep.Day });
            steps.Items.Add(new StepItem { Label = "s_week", Step = TimeLineStep.Week });
            steps.Items.Add(new StepItem { Label = "s_month", Step = TimeLineStep.Month });
            steps.Items.Add(new StepItem { Label = "s_quarter", Step = TimeLineStep.Quarter });
            steps.Items.Add(new StepItem { Label = "s_year", Step = TimeLineStep.Year });
            steps.Items.Add(new StepItem { Label = "s_day_of_week", Step = TimeLineStep.DayOfWeek });
            steps.SelectedIndex = 0;
        }

        private TimeLineItemFrame AddItem(bool callHandlers)
        {
            var item = new TimeLineItemFrame(m_cfg);
            panel1.Controls.Add(item);
            item.Width = panel1.Width;
            item.Anchor = AnchorStyles.Left | AnchorStyles.Top | AnchorStyles.Right;
            item.AdvancedSettings = chbAdvanced.Checked;
            m_items.Add(item);
            item.OnRemoveItem += item_OnRemoveItem;
            if (callHandlers)
            {
                RelayoutItems();
                m_cfg.CallChanged();
            }
            return item;
        }

        private void RelayoutItems()
        {
            int acty = 0;
            foreach (var item in m_items)
            {
                item.Top = acty;
                acty += item.Height;
            }
            groupBox1.Height = acty + 30;
            btnAdd.Top = groupBox1.Bottom + 5;
        }

        void item_OnRemoveItem(object sender, EventArgs e)
        {
            var item = (TimeLineItemFrame)sender;
            RemoveItem(item, true);
        }

        private void RemoveItem(TimeLineItemFrame item, bool callHandlers)
        {
            item.OnRemoveItem -= item_OnRemoveItem;
            m_items.Remove(item);
            item.Dispose();
            if (callHandlers)
            {
                RelayoutItems();
                m_cfg.CallChanged();
            }
        }

        internal IChartDataProcessor GetProcessor()
        {
            var res = new TimeLineChartDataProcessor();
            res.DateColumn = cbxColumn.SelectedItem.SafeToString();
            res.Step = ((StepItem)cbxStep.SelectedItem).Step;
            res.UseStructuredTime = chbAdvanced.Checked;
            foreach (var item in m_items)
            {
                res.Items.Add(item.CreateItem());
            }
            return res;
        }

        private void LoadStep(TimeLineStep step, ComboBox cbx)
        {
            for (int i = 0; i < cbx.Items.Count; i++)
            {
                if (((StepItem)cbx.Items[i]).Step == step)
                {
                    cbx.SelectedIndex = i;
                    break;
                }
            }
        }

        internal void LoadFromProcessor(TimeLineChartDataProcessor proc)
        {
            while (m_items.Count > 0) RemoveItem(m_items[0], false);
            if (proc.DateColumn != null) cbxColumn.SelectedIndex = cbxColumn.Items.IndexOf(proc.DateColumn);
            chbAdvanced.Checked = proc.UseStructuredTime;

            LoadStep(proc.Step, cbxStep);
            LoadStep(proc.SmallStep, cbxStep2);

            foreach (var item in proc.Items)
            {
                AddItem(false).LoadFromItem(item);
            }
            RelayoutItems();
            m_cfg.CallChanged();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddItem(true);
        }

        class StepItem
        {
            public TimeLineStep Step;
            public string Label;

            public override string ToString()
            {
                return Texts.Get(Label);
            }
        }

        private void cbxStep_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_cfg.CallChanged();
        }

        private void cbxColumn_SelectedIndexChanged(object sender, EventArgs e)
        {
            m_cfg.CallChanged();
        }

        private void chbAdvanced_CheckedChanged(object sender, EventArgs e)
        {
            foreach (var item in m_items)
            {
                item.AdvancedSettings = chbAdvanced.Checked;
            }
            if (panAdvanced.Visible != chbAdvanced.Checked)
            {
                panAdvanced.Visible = chbAdvanced.Checked;
                if (panAdvanced.Visible)
                {
                    groupBox1.Top += panAdvanced.Height;
                    btnAdd.Top += panAdvanced.Height;
                }
                else
                {
                    groupBox1.Top -= panAdvanced.Height;
                    btnAdd.Top -= panAdvanced.Height;
                }
            }
        }
    }

    public class TimeLineDataSourceConfigurator : DataSourceConfigurator
    {
        TimeLineCDSCFrame m_editor;

        public TimeLineDataSourceConfigurator(ITableStructure table)
            : base(table)
        {
        }

        public override Control GetEditor()
        {
            if (m_editor == null) m_editor = new TimeLineCDSCFrame(this);
            return m_editor;
        }

        public override IChartDataProcessor GetProcessor()
        {
            return m_editor.GetProcessor();
        }

        public override string ToString()
        {
            return Texts.Get("s_timeline");
        }

        public override void LoadFromProcessor(IChartDataProcessor proc)
        {
            GetEditor();
            m_editor.LoadFromProcessor((TimeLineChartDataProcessor)proc);
        }

        public override Type GetProcessorType()
        {
            return typeof(TimeLineChartDataProcessor);
        }
    }
}
