using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class DataSqlGenColumnsFrame2 : DataSqlGenColumnsFrameBase
    {
        ITableStructure m_table;
        string[] m_selcolumns;

        public DataSqlGenColumnsFrame2()
        {
            InitializeComponent();
            //var overlay = CoreIcons.delete_overlay;
            //btnUncheckAll.Image = ImageTool.CombineImages(CoreIcons.checkall, overlay);
            //btnNoKeyColumns.Image = ImageTool.CombineImages(CoreIcons.primary_key, overlay);
            //btnNoSelectedColumns.Image = ImageTool.CombineImages(CoreIcons.table_data, overlay);
        }

        public override string GroupBoxTitle
        {
            get { return groupBox1.Text; }
            set { groupBox1.Text = value; }
        }

        public override void SetHintStructure(ITableStructure value, string[] selcolumns)
        {
            m_table = value;
            m_selcolumns = selcolumns;
            foreach (string colname in value.Columns.GetNames())
            {
                checkedListBox1.Items.Add(colname);
            }
        }

        ItemCheckEventArgs m_currentCheck;
        public override DataSqlGeneratorColumnSet GetColumnSet()
        {
            var res = new DataSqlGeneratorColumnSet();
            res.Mode = DataSqlGeneratorColumnSet.ModeEnum.ExplicitColumns;
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                bool value = checkedListBox1.GetItemChecked(i);
                if (m_currentCheck != null && i == m_currentCheck.Index) value = m_currentCheck.NewValue == CheckState.Checked;
                if (value) res.Columns.Add(checkedListBox1.Items[i].ToString());
            }
            return res;
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            m_currentCheck = e;
            OnSettingsChanged();
            m_currentCheck = null;
        }

        private void SetChecked(string[] cols)
        {
            if (cols == null) return;
            var set = new HashSetEx<string>(cols);
            for (int i = 0; i < checkedListBox1.Items.Count; i++)
            {
                checkedListBox1.SetItemChecked(i, set.Contains(checkedListBox1.Items[i].ToString()));
            }
            OnSettingsChanged();
        }

        public override void InitializeMode(DataSqlGeneratorColumnSet.ModeEnum value)
        {
            switch (value)
            {
                case DataSqlGeneratorColumnSet.ModeEnum.AllColumns:
                    SetChecked(m_table.Columns.GetNames());
                    break;
                case DataSqlGeneratorColumnSet.ModeEnum.NoPkCols:
                    SetChecked(m_table.GetNoPkColumnNames());
                    break;
                case DataSqlGeneratorColumnSet.ModeEnum.PrimaryKey:
                    SetChecked(m_table.GetPkColumnNames());
                    break;
                case DataSqlGeneratorColumnSet.ModeEnum.SelectedColumns:
                    SetChecked(m_selcolumns);
                    break;
            }
        }

        private void btnCheckAll_Click(object sender, EventArgs e)
        {
            SetChecked(m_table.Columns.GetNames());
        }

        private void btnUncheckAll_Click(object sender, EventArgs e)
        {
            SetChecked(new string[] { });
        }

        private void btnPrimaryKey_Click(object sender, EventArgs e)
        {
            SetChecked(m_table.GetPkColumnNames());
        }

        private void btnNoKeyColumns_Click(object sender, EventArgs e)
        {
            SetChecked(m_table.GetNoPkColumnNames());
        }

        private void btnSelectedColumns_Click(object sender, EventArgs e)
        {
            SetChecked(m_selcolumns);
        }

        private void btnNoSelectedColumns_Click(object sender, EventArgs e)
        {
            SetChecked((from c in m_table.Columns.GetNames() where !m_selcolumns.Contains(c) select c).ToArray());
        }
    }
}
