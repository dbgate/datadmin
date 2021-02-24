using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class TablePerspectiveEditorForm : FormEx
    {
        TablePerspective m_per;

        public TablePerspectiveEditorForm(TablePerspective per, ITableStructure table, IDatabaseSource db)
        {
            InitializeComponent();
            tablePerspectiveEditorFrame1.Init(per, table, db);
            m_per = per;
        }

        public static DialogResult Run(TablePerspective per, ITableStructure table, IDatabaseSource db)
        {
            var win = new TablePerspectiveEditorForm(per, table, db);
            win.ShowDialogEx();
            return win.DialogResult;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            tablePerspectiveEditorFrame1.SavePerspective();
            m_per.SaveToFile();
            Close();
            DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
            DialogResult = DialogResult.Cancel;
        }
    }
}
