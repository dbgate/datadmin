using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class CopyTableForm : FormEx
    {
        public CopyTableForm(ITableSource src, IDatabaseSource dst)
        {
            InitializeComponent();
            cbcopydata.Enabled = src.TableCaps.DataStoreForReading && dst.TableCaps.DataStoreForWriting;
            tbltblname.Text = src.FullName.Name;
            cbcopydata_CheckedChanged(this, EventArgs.Empty);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        public static string Run(ITableSource src, IDatabaseSource dst, out bool copydata, out TableCopyOptions opts)
        {
            CopyTableForm win = new CopyTableForm(src, dst);
            DialogResult res = win.ShowDialogEx();
            copydata = win.cbcopydata.Checked;
            opts = win.GetCopyOptions();
            if (res == DialogResult.OK)
            {
                return win.tbltblname.Text;
            }
            return null;
        }

        private void CopyTableForm_Shown(object sender, EventArgs e)
        {
            tbltblname.Focus();
        }

        public TableCopyOptions GetCopyOptions()
        {
            return tableCopyOptionsFrame1.GetOptions();
        }

        private void cbcopydata_CheckedChanged(object sender, EventArgs e)
        {
            tableCopyOptionsFrame1.Enabled = cbcopydata.Checked;
        }
    }
}