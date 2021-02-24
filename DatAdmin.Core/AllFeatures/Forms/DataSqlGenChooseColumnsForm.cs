using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class DataSqlGenChooseColumnsForm : FormEx
    {
        List<string> m_columns;
        public DataSqlGenChooseColumnsForm(ITableStructure table, List<string> columns)
        {
            m_columns = columns;
            InitializeComponent();
            if (table != null)
            {
                textBox1.Visible = false;
                foreach (var col in table.Columns)
                {
                    checkedListBox1.Items.Add(col.ColumnName, columns.Contains(col.ColumnName));
                }
            }
            else
            {
                checkedListBox1.Visible = false;
                textBox1.Text = columns.CreateDelimitedText("\r\n");
            }
        }

        public static void Run(ITableStructure table, List<string> columns)
        {
            var win = new DataSqlGenChooseColumnsForm(table, columns);
            win.ShowDialogEx();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            m_columns.Clear();
            if (textBox1.Visible)
            {
                foreach (string line in textBox1.Text.Split('\n'))
                {
                    string col = line.Trim();
                    if (String.IsNullOrEmpty(col)) continue;
                    m_columns.Add(col);
                }
            }
            else
            {
                m_columns.AddRange(checkedListBox1.GetCheckedItemNames());
            }
            Close();
        }
    }
}
