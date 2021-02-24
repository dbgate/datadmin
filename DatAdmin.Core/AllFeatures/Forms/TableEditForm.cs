using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class TableEditForm : FormEx
    {
        public TableEditForm()
        {
            InitializeComponent();
        }

        public static bool Run(IDatabaseSource conn, TableStructure table, AlterTableEditorPars pars)
        {
            var win = new TableEditForm();
            win.tableEditFrame1.Init(conn, table, pars);
            win.tableEditFrame1.ShowAlteredInfoDialog = false;
            return win.ShowDialog() == DialogResult.OK;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            CloseOk();
        }

        private void CloseOk()
        {
            tableEditFrame1.Save();
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void TableEditForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            tableEditFrame1.OnClose();
        }

        private void TableEditForm_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.S && e.Control)
            {
                CloseOk();
            }
        }
    }
}
