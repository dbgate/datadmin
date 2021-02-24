using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class ChooseVisibleColumnsForm : FormEx
    {
        List<DmlfExpression> m_avail;
        DmlfResultFieldCollection m_visCols;
        DmlfResultFieldCollection m_hidCols;

        public ChooseVisibleColumnsForm(DmlfResultFieldCollection cols, List<DmlfExpression> avail)
        {
            InitializeComponent();
            m_avail = avail;
            cols.SplitVisible(out m_visCols, out m_hidCols);
            foreach (var av in m_avail)
            {
                lbxAvailableColumns.Items.Add(av);
            }
            foreach (var vis in m_visCols)
            {
                lbxVisibleColumns.Items.Add(vis);
            }
        }

        public static DmlfResultFieldCollection Run(DmlfResultFieldCollection cols, List<DmlfExpression> avail)
        {
            var win = new ChooseVisibleColumnsForm(cols, avail);
            if (win.ShowDialogEx() == DialogResult.OK)
            {
                return win.GetResult();
            }
            return null;
        }

        private void btnAddVisible_Click(object sender, EventArgs e)
        {
            foreach (DmlfExpression item in lbxAvailableColumns.SelectedItems)
            {
                lbxVisibleColumns.Items.Add(new DmlfResultField
                {
                    Expr = item
                });
            }
        }

        private DmlfResultFieldCollection GetResult()
        {
            var res = new DmlfResultFieldCollection();
            foreach (DmlfResultField fld in lbxVisibleColumns.Items)
            {
                res.Add(fld);
            }
            res.AddRange(m_hidCols);
            return res;
        }

        private void btnAddAllVisible_Click(object sender, EventArgs e)
        {
            var res = GetResult();
            foreach (var item in m_avail)
            {
                if (res.GetExpressionIndex(item) >= 0) continue;
                lbxVisibleColumns.Items.Add(new DmlfResultField { Expr = item });
            }
        }

        private void btnRemoveVisible_Click(object sender, EventArgs e)
        {
            lbxVisibleColumns.RemoveSelected();
        }

        private void btnRemoveAllVisible_Click(object sender, EventArgs e)
        {
            lbxVisibleColumns.Items.Clear();
        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            lbxVisibleColumns.MoveSelectedUp();
        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            lbxVisibleColumns.MoveSelectedDown();
        }
    }
}
