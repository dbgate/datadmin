using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Linq;

namespace DatAdmin
{
    public partial class SelectReferenceForm : FormEx
    {
        ReferenceViewDefinition m_result;
        IForeignKey m_resultFk;
        ITableStructure m_table;
        ISqlDialect m_dialect;
        List<string> m_availcols;

        public SelectReferenceForm(ITableStructure table, string activeColumn, List<string> availcols, ISqlDialect dialect)
        {
            InitializeComponent();
            m_table = table;
            m_dialect = dialect;
            m_availcols = availcols;

            FillList(lsvDetailReferences, m_table.GetReferencedFrom(), null, AcceptDetail);
            FillList(lsvMasterReferences, m_table.GetConstraints<IForeignKey>(), activeColumn, AcceptMaster);
            tbxBaseTable.Text = table.ToString();
        }

        private bool AcceptMaster(IForeignKey fk)
        {
            if (m_availcols == null) return true;
            foreach (var col in fk.Columns)
            {
                if (!m_availcols.Contains(col.ColumnName)) return false;
            }
            return true;
        }

        private bool AcceptDetail(IForeignKey fk)
        {
            if (m_availcols == null) return true;
            foreach (var col in fk.PrimaryKeyColumns)
            {
                if (!m_availcols.Contains(col.ColumnName)) return false;
            }
            return true;
        }

        private void FillList(ListView list, IEnumerable<IForeignKey> fks, string selectColumn, Func<IForeignKey, bool> accept)
        {
            foreach (IForeignKey fk in fks.SortedByKey<IForeignKey, string>(k => k.PrimaryKeyTable == m_table.FullName ? k.Table.FullName.ToString() : k.PrimaryKeyTable.ToString()))
            {
                if (!accept(fk)) continue;
                var item = list.Items.Add(fk.PrimaryKeyTable == m_table.FullName ? fk.Table.FullName.ToString() : fk.PrimaryKeyTable.ToString());
                item.SubItems.Add(fk.ToString());
                item.SubItems.Add((from c in fk.Columns select c.ColumnName).CreateDelimitedText(" ,"));
                item.Tag = fk;
                if (selectColumn != null && fk.Columns.Exists(c => c.ColumnName == selectColumn))
                {
                    item.Focused = true;
                    item.Selected = true;
                }
            }
        }

        public static ReferenceViewDefinition Run(ITableStructure table, string activeColumn, List<string> availcols, ISqlDialect dialect)
        {
            var win = new SelectReferenceForm(table, activeColumn, availcols, dialect);
            win.ShowDialogEx();
            return win.m_result;
        }

        public static IForeignKey RunFk(ITableStructure table, string activeColumn, ISqlDialect dialect)
        {
            var win = new SelectReferenceForm(table, activeColumn, null, dialect);
            win.ShowDialogEx();
            return win.m_resultFk;
        }

        public static ReferenceViewDefinition CreateDetailReference(IForeignKey fk, ISqlDialect dialect)
        {
            var res = new ReferenceViewDefinition();
            res.Dialect = dialect;
            res.SourceColumns.AddRange(from c in fk.PrimaryKeyColumns select c.ColumnName);
            res.ReferenceColumns.AddRange(from c in fk.Columns select c.ColumnName);
            res.TableName = fk.Table.FullName;
            return res;
        }

        public static ReferenceViewDefinition CreateMasterReference(IForeignKey fk, ISqlDialect dialect)
        {
            var res = new ReferenceViewDefinition();
            res.Dialect = dialect;
            res.SourceColumns.AddRange(from c in fk.Columns select c.ColumnName);
            res.ReferenceColumns.AddRange(from c in fk.PrimaryKeyColumns select c.ColumnName);
            res.TableName = fk.PrimaryKeyTable;
            return res;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (rbtDetail.Checked)
            {
                if (lsvDetailReferences.SelectedItems.Count == 0)
                {
                    StdDialog.ShowError("s_please_select_foreign_key");
                    return;
                }
                IForeignKey fk = (IForeignKey)lsvDetailReferences.SelectedItems[0].Tag;
                m_result = CreateDetailReference(fk, m_dialect);
                m_resultFk = fk;
            }
            if (rbtMaster.Checked)
            {
                if (lsvMasterReferences.SelectedItems.Count == 0)
                {
                    StdDialog.ShowError("s_please_select_foreign_key");
                    return;
                }
                IForeignKey fk = (IForeignKey)lsvMasterReferences.SelectedItems[0].Tag;
                m_resultFk = fk;
                m_result = CreateMasterReference(fk, m_dialect);
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void lsvDetailReferences_SelectedIndexChanged(object sender, EventArgs e)
        {
            rbtDetail.Checked = true;
        }

        private void lsvMasterReferences_SelectedIndexChanged(object sender, EventArgs e)
        {
            rbtMaster.Checked = true;
        }

        private void lsvDetailReferences_DoubleClick(object sender, EventArgs e)
        {
            rbtDetail.Checked = true;
            btnOk_Click(sender, e);
        }

        private void lsvMasterReferences_DoubleClick(object sender, EventArgs e)
        {
            rbtMaster.Checked = true;
            btnOk_Click(sender, e);
        }

        private void SelectReferenceForm_Shown(object sender, EventArgs e)
        {
            if (lsvMasterReferences.SelectedItems.Count > 0) lsvMasterReferences.Focus();
            else lsvDetailReferences.Focus();
        }
    }
}
