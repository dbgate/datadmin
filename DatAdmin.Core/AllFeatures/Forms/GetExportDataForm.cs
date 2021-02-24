using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class GetExportDataForm : FormEx
    {
        public enum Result { None, All, SelRows, SelRowsSelCols }

        public GetExportDataForm()
        {
            InitializeComponent();
        }

        public static Result Run()
        {
            var win = new GetExportDataForm();
            if (win.ShowDialog() == DialogResult.OK)
            {
                if (win.rbtAll.Checked) return Result.All;
                if (win.rbtSelectedRows.Checked) return Result.SelRows;
                if (win.rbtSelectedRowsColumns.Checked) return Result.SelRowsSelCols;
            }
            return Result.None;
        }
    }
}
