using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class SelectAddColumnsForm : FormEx
    {
        public SelectAddColumnsForm()
        {
            InitializeComponent();
        }

        public static string[] Run(string table, string[] columns)
        {
            var win = new SelectAddColumnsForm();
            win.tbxTable.Text = table;
            foreach (string col in columns)
            {
                win.checkedListBox1.Items.Add(col);
            }
            if (win.ShowDialogEx() == DialogResult.OK)
            {
                var res = new List<string>();
                foreach (string col in win.checkedListBox1.CheckedItems)
                {
                    res.Add(col);
                }
                return res.ToArray();
            }
            return null;
        }

        private void btnCheckAll_Click(object sender, EventArgs e)
        {
            checkedListBox1.SetAllChecked(true);
        }

        private void btnUncheckAll_Click(object sender, EventArgs e)
        {
            checkedListBox1.SetAllChecked(false);
        }
    }
}
