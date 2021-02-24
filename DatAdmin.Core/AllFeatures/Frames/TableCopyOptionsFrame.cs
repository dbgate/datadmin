using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class TableCopyOptionsFrame : UserControl
    {
        public TableCopyOptionsFrame()
        {
            InitializeComponent();
        }

        public TableCopyOptions GetOptions()
        {
            TableCopyOptions res = new TableCopyOptions();
            res.AllowBulkCopy = chbAllowBulkCopy.Checked;
            res.TruncateBeforeCopy = chbTruncateBeforeCopy.Checked;
            res.DisableConstraints = chbDisableConstraints.Checked;
            return res;
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            if (Height != 94) Height = 94;
        }
    }
}
