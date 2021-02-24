using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class ChooseSearchColumnsFrame : UserControl
    {
        ColumnDisplay m_disp;
        string[] m_initColumns;
        bool m_closing;
        bool m_exactMatch;

        public bool ClosedOk { get; private set; }

        public ChooseSearchColumnsFrame()
        {
            InitializeComponent();
            //btnUncheckAll.Image = ImageTool.CombineImages(CoreIcons.checkall, CoreIcons.delete_overlay);
            Translating.TranslateControl(this);
            Disposed += new EventHandler(ChooseSearchColumnsFrame_Disposed);
            ClosedOk = false;
        }

        void ChooseSearchColumnsFrame_Disposed(object sender, EventArgs e)
        {
            if (ControlClosed != null) ControlClosed(this, EventArgs.Empty);
        }

        public event EventHandler ControlClosed;

        private void ChooseSearchColumnsFrame_Leave(object sender, EventArgs e)
        {
            ProcessOk();
        }

        private bool AreChangedColumns()
        {
            bool changed = true;
            var cols = GetCheckedColumns();
            if (m_initColumns == cols) changed = false;
            if (cols != null && m_initColumns != null && cols.SequenceEqual(m_initColumns)) changed = false;
            return changed;
        }

        public void ProcessOk()
        {
            if (m_closing) return;
            m_closing = true;
            ClosedOk = AreChangedColumns() || GetExactMatch() != m_exactMatch;
            Dispose();
        }

        public void FillColumns(ColumnDisplay disp, string[] filterColumns, bool exactMatch)
        {
            m_disp = disp;
            m_initColumns = filterColumns;
            m_exactMatch = exactMatch;
            var set = new HashSetEx<string>();
            if (filterColumns != null) set.AddRange(filterColumns);
            foreach (var col in disp)
            {
                var cs = (IColumnStructure)col.ValueTag;
                if (cs == null) continue;
                if (cs.DataType is DbTypeXml || cs.DataType is DbTypeBlob) continue;
                string colname = col.ValueRef.ToString();
                checkedListBox1.Items.Add(colname, set.Contains(colname));
            }
            chbExactMatch.Checked = exactMatch;
        }

        private void btnCheckAll_Click(object sender, EventArgs e)
        {
            checkedListBox1.SetAllChecked(true);
        }

        private void btnUncheckAll_Click(object sender, EventArgs e)
        {
            checkedListBox1.SetAllChecked(false);
        }

        public string[] GetCheckedColumns()
        {
            var res = checkedListBox1.GetCheckedItemNames();
            if (res.Count == checkedListBox1.Items.Count || res.Count == 0) return null;
            return res.ToArray();
        }

        public bool GetExactMatch() { return chbExactMatch.Checked; }

        private void checkedListBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                m_closing = true;
                Dispose();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                ProcessOk();
            }
        }
    }
}
