using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class DataSqlGenColumnsFrame : DataSqlGenColumnsFrameBase
    {
        ITableStructure m_structure;

        List<string> m_columns = new List<string>();

        public DataSqlGenColumnsFrame()
        {
            InitializeComponent();
        }

        public override void SetHintStructure(ITableStructure value, string[] selcolumns)
        {
            m_structure = value;
        }

        public override DataSqlGeneratorColumnSet GetColumnSet()
        {
            var res = new DataSqlGeneratorColumnSet();
            if (rbtAllColumns.Checked) res.Mode = DataSqlGeneratorColumnSet.ModeEnum.AllColumns;
            if (rbtChooseColumns.Checked)
            {
                res.Mode = DataSqlGeneratorColumnSet.ModeEnum.ExplicitColumns;
                res.Columns.AddRange(m_columns);
            }
            if (rbtNoKeyColumns.Checked) res.Mode = DataSqlGeneratorColumnSet.ModeEnum.NoPkCols;
            if (rbtPrimaryKey.Checked) res.Mode = DataSqlGeneratorColumnSet.ModeEnum.PrimaryKey;
            if (rbtSelectedColumns.Checked)
            {
                res.Mode = DataSqlGeneratorColumnSet.ModeEnum.SelectedColumns;
            }
            if (rbtNoSelectedColumns.Checked)
            {
                res.Mode = DataSqlGeneratorColumnSet.ModeEnum.NoSelectedColumns;
            }
            return res;
        }

        public override void InitializeMode(DataSqlGeneratorColumnSet.ModeEnum value)
        {
            switch (value)
            {
                case DataSqlGeneratorColumnSet.ModeEnum.AllColumns:
                    rbtAllColumns.Checked = true;
                    break;
                case DataSqlGeneratorColumnSet.ModeEnum.ExplicitColumns:
                    rbtChooseColumns.Checked = true;
                    break;
                case DataSqlGeneratorColumnSet.ModeEnum.NoPkCols:
                    rbtNoKeyColumns.Checked = true;
                    break;
                case DataSqlGeneratorColumnSet.ModeEnum.NoSelectedColumns:
                    rbtNoSelectedColumns.Checked = true;
                    break;
                case DataSqlGeneratorColumnSet.ModeEnum.PrimaryKey:
                    rbtPrimaryKey.Checked = true;
                    break;
                case DataSqlGeneratorColumnSet.ModeEnum.SelectedColumns:
                    rbtSelectedColumns.Checked = true;
                    break;
            }
        }

        private void rbtChooseColumns_CheckedChanged(object sender, EventArgs e)
        {
            btnChooseColumns.Enabled = rbtChooseColumns.Checked;
            OnSettingsChanged();
        }

        public override string GroupBoxTitle
        {
            get { return groupBox1.Text; }
            set { groupBox1.Text = value; }
        }

        private void rbtSelectedColumns_CheckedChanged(object sender, EventArgs e)
        {
            OnSettingsChanged();
        }

        private void btnChooseColumns_Click(object sender, EventArgs e)
        {
            DataSqlGenChooseColumnsForm.Run(m_structure, m_columns);
            OnSettingsChanged();
        }
    }
}
