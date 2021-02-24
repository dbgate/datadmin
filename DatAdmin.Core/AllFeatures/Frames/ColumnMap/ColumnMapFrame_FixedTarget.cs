using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class ColumnMapFrame_FixedTarget : UserControl, IColumnMapFrame
    {
        ControlInvoker m_invoker;
        ITableStructure m_source;
        ITableStructure m_target;
        List<CheckBox> m_checks = new List<CheckBox>();
        List<ComboBox> m_combos = new List<ComboBox>();
        List<ComboBox> m_typeCombos = new List<ComboBox>();

        public ColumnMapFrame_FixedTarget()
        {
            InitializeComponent();
            m_invoker = new ControlInvoker(this);
        }

        public void SetBindings(ITabularDataStore source, ITabularDataStore target)
        {
            using (WaitContext wc = new WaitContext())
            {
                Async.SafeOpen(source.Connection);
                Async.SafeOpen(target.Connection);
                try
                {
                    IAsyncResult res1 = source.BeginGetRowFormat(null);
                    Async.WaitFor(res1);
                    m_source = source.EndGetRowFormat(res1);
                }
                catch (Exception err)
                {
                    throw new BulkCopyInputError("DAE-00181", err);
                }

                if (m_source.Columns.Count == 0)
                {
                    throw new ExpectedError("DAE-00182 " + Texts.Get("s_no_columns_detected_in_imported_source"));
                }

                IAsyncResult res2 = target.BeginGetRowFormat(null);
                Async.WaitFor(res2);
                m_target = target.EndGetRowFormat(res2);

                int acty = labValue.Top + labValue.Height + 10;
                int colindex = 0;
                foreach (IColumnStructure col in m_target.Columns)
                {
                    Label lab = new Label();
                    Controls.Add(lab);
                    lab.Left = labTarget.Left;
                    lab.Top = acty;
                    lab.Text = col.ColumnName + " :";

                    CheckBox skip = new CheckBox();
                    Controls.Add(skip);
                    skip.Top = acty;
                    skip.Width = 30;
                    skip.Left = labSkip.Left;
                    m_checks.Add(skip);
                    skip.Tag = colindex;
                    skip.CheckedChanged += new EventHandler(skip_CheckedChanged);

                    ComboBox type = new ComboBox();
                    Controls.Add(type);
                    type.Left = labType.Left;
                    type.Top = acty;
                    type.DropDownStyle = ComboBoxStyle.DropDownList;
                    GenericTransform.GetColExprTypes(type.Items);
                    type.SelectedIndex = 0;
                    type.Tag = colindex;
                    m_typeCombos.Add(type);

                    ComboBox sel = new ComboBox();
                    Controls.Add(sel);
                    sel.Left = labValue.Left;
                    sel.Top = acty;
                    sel.DropDownStyle = ComboBoxStyle.DropDown;
                    sel.DropDownClosed += new EventHandler(sel_DropDownClosed);
                    sel.Tag = colindex;
                    foreach (IColumnStructure srccol in m_source.Columns)
                    {
                        sel.Items.Add(srccol.ColumnName);
                    }
                    sel.SelectedIndex = sel.Items.IndexOf(col.ColumnName);
                    if (sel.SelectedIndex < 0)
                    {
                        if (col.ColumnOrder < sel.Items.Count)
                        {
                            sel.SelectedIndex = col.ColumnOrder;
                        }
                        else
                        {
                            sel.SelectedIndex = 0;
                        }
                    }
                    m_combos.Add(sel);

                    acty += Math.Max(sel.Height, lab.Height) * 5 / 4;
                    colindex++;
                }
            }
        }

        void skip_CheckedChanged(object sender, EventArgs e)
        {
            var check = (CheckBox)sender;
            int index = (int)check.Tag;
            bool enabled = !check.Checked;
            m_combos[index].Enabled = enabled;
            m_typeCombos[index].Enabled = enabled;
        }

        private void SetColumnBindings(int colindex, GenericTransform.ColExprType type, string expr)
        {
            m_typeCombos[colindex].SelectedIndex = m_typeCombos[colindex].Items.IndexOf(type);
            m_combos[colindex].Text = expr;
        }

        void sel_DropDownClosed(object sender, EventArgs e)
        {
            m_typeCombos[(int)((ComboBox)sender).Tag].SelectedIndex = 0;
        }

        #region IColumnMapFrame Members

        public IRowTransform GetTransform()
        {
            bool ispermute = true;
            for (int i = 0; i < m_combos.Count; i++)
            {
                if (m_checks[i].Checked) continue;
                var c = m_typeCombos[i];
                if (c.SelectedIndex > 0) ispermute = false;
            }
            if (ispermute)
            {
                TableStructure target = new TableStructure();
                List<int> indexes = new List<int>();
                for (int i = 0; i < m_combos.Count; i++)
                {
                    if (m_checks[i].Checked) continue;
                    var combo = m_combos[i];
                    int idx = combo.Items.IndexOf(combo.Text);
                    if (idx < 0) throw new RowTransformError("DAE-00183" + Texts.Get("s_cannot_find$column", "column", combo.Text));
                    indexes.Add(idx);
                    target._Columns.Add(new ColumnStructure(m_target.Columns[i]));
                }
                return new PermuteTransform(m_source, target, indexes);
            }
            else
            {
                TableStructure target = new TableStructure();
                List<GenericTransform.ColExpr> cols = new List<GenericTransform.ColExpr>();
                for (int i = 0; i < m_typeCombos.Count; i++)
                {
                    if (m_checks[i].Checked) continue;
                    cols.Add(new GenericTransform.ColExpr
                    {
                        Type = (GenericTransform.ColExprType)m_typeCombos[i].SelectedItem,
                        Expression = m_combos[i].Text,
                        Name = m_target.Columns[i].ColumnName
                    });
                    target._Columns.Add(new ColumnStructure(m_target.Columns[i]));
                }
                GenericTransform gt = new GenericTransform(m_source, target, cols);
                return gt;
            }
        }

        public ITableStructure GetTargetRowFormat()
        {
            return m_target;
        }

        public void LoadFromTransform(IRowTransform tr)
        {
            bool[] activeCols = new bool[m_target.Columns.Count];
            if (tr is IdentityTransform)
            {
                for (int i = 0; i < m_combos.Count; i++)
                {
                    activeCols[i] = true;
                    int srcindex = m_source.Columns.GetIndex(m_target.Columns[i].ColumnName);
                    if (srcindex >= 0)
                    {
                        SetColumnBindings(i, new GenericTransform.ColumnColExprType(), m_source.Columns[srcindex].ColumnName);
                    }
                }
            }
            if (tr is PermuteTransform)
            {
                var t = tr as PermuteTransform;
                for (int i = 0; i < t.OutputFormat.Columns.Count; i++)
                {
                    var dstcol = t.OutputFormat.Columns[i];
                    int dsti = m_target.Columns.GetIndex(dstcol.ColumnName);
                    if (dsti < 0) continue;
                    int srci = t.DestIndexes[i];
                    IColumnStructure srccol = m_source.Columns[srci];
                    SetColumnBindings(dsti, new GenericTransform.ColumnColExprType(), srccol.ColumnName);
                    activeCols[dsti] = true;
                }
            }
            if (tr is GenericTransform)
            {
                var t = tr as GenericTransform;

                for (int i = 0; i < t.OutputFormat.Columns.Count; i++)
                {
                    var dstcol = t.OutputFormat.Columns[i];
                    int dsti = m_target.Columns.GetIndex(dstcol.ColumnName);
                    if (dsti < 0) continue;
                    var d=t.DestCols[i];
                    SetColumnBindings(dsti, d.Type, d.Expression);
                    activeCols[dsti] = true;
                }
            }
            for (int i = 0; i < activeCols.Length; i++) m_checks[i].Checked = !activeCols[i];
        }

        public ITableStructure SourceOnInput { get { return m_source; } }
        public ITableStructure TargetOnInput { get { return m_target; } }

        #endregion
    }
}
