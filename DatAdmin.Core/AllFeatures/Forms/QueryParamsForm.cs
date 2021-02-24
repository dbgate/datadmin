using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class QueryParamsForm : FormEx
    {
        Dictionary<string, string> m_vars;
        public QueryParamsForm(Dictionary<string, string> vars)
        {
            InitializeComponent();
            m_vars = vars;
            foreach (var tuple in m_vars)
            {
                dataGridView1.Rows.Add(tuple.Key, tuple.Value);
            }
        }

        public static bool Run(Dictionary<string, string> vars)
        {
            var win = new QueryParamsForm(vars);
            return win.ShowDialogEx() == DialogResult.OK;
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            m_vars[dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString()] = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
        }

        private void QueryParamsForm_Shown(object sender, EventArgs e)
        {
            dataGridView1.Focus();
            dataGridView1.CurrentCell = dataGridView1.Rows[0].Cells[1];
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (!dataGridView1.IsCurrentCellInEditMode && e.KeyCode == Keys.Enter)
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }
    }
}
