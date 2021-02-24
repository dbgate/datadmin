using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class ColumnMapFrame_VarTarget : UserControl, IColumnMapFrame
    {
        IInvoker m_invoker;
        ITabularDataStore m_source;
        ITabularDataStore m_target;
        ITableStructure m_srcformat;
        Dictionary<string, GenericTransform.ColExprType> m_types = new Dictionary<string, GenericTransform.ColExprType>();

        public ColumnMapFrame_VarTarget()
        {
            InitializeComponent();
            m_invoker = new ControlInvoker(this);
            var col = (DataGridViewComboBoxColumn)lbtarget.Columns[1];
            foreach (var type in GenericTransform.GetColExprTypes())
            {
                col.Items.Add(type.ToString());
                m_types[type.ToString()] = type;
            }
        }

        public void SetBindings(ITabularDataStore source, ITabularDataStore target)
        {
            Async.SafeOpen(source.Connection);
            Async.SafeOpen(target.Connection);
            //lbsource.Enabled = false;
            //lbtarget.Enabled = false;
            lbsource.Items.Clear();
            lbtarget.Rows.Clear();
            //lbtarget.Items.Clear();
            Errors.Assert(source.AvailableRowFormat);
            //Async.CreateInvokeCallback(m_invoker, GotRowFormat));
            m_source = source;
            m_target = target;
            //lbsource.Enabled = true;
            //lbtarget.Enabled = true;

            try
            {
                IAsyncResult async = m_source.BeginGetRowFormat(null);
                Async.WaitFor(async);
                m_srcformat = m_source.EndGetRowFormat(async);
            }
            catch (Exception err)
            {
                throw new BulkCopyInputError("DAE-00184", err);
            }

            foreach (IColumnStructure col in m_srcformat.Columns)
            {
                lbsource.Items.Add(col.ColumnName);
                lbtarget.Rows.Add(col.ColumnName, new GenericTransform.ColumnColExprType().ToString(), col.ColumnName);
                //lbtarget.Items.Add(col.ColumnName);
            }
        }

        GenericTransform.ColExprType GetExprType(DataGridViewRow row)
        {
            if (row.IsNewRow) return null;
            object value = row.Cells[1].Value;
            if (value == null) return null;
            if (!m_types.ContainsKey(value.ToString())) return null;
            return m_types[value.ToString()];
        }

        private ITableStructure CreateTargetStructure()
        {
            TableStructure table = new TableStructure();
            foreach (DataGridViewRow row in lbtarget.Rows)
            {
                var type = GetExprType(row);
                if (type == null) continue;
                if (type is GenericTransform.ColumnColExprType)
                {
                    string oldcol = row.Cells[2].Value.ToString();
                    if (m_srcformat.Columns.GetIndex(oldcol) < 0) throw new IncorrectObjectReferenceError("DAE-00185", "s_column", oldcol);
                    var coldef = new ColumnStructure(m_srcformat.Columns[oldcol]);
                    coldef.ColumnName = row.Cells[0].Value.ToString();
                    table.AddColumn(coldef, true);
                }
                else if (type is GenericTransform.RowNumberColExprType)
                {
                    var col = new ColumnStructure();
                    col.ColumnName = row.Cells[0].Value.ToString();
                    col.DataType = new DbTypeInt();
                    table._Columns.Add(col);
                    if (table.FindConstraint<IPrimaryKey>() == null && table.FindAutoIncrementColumn() == null)
                    {
                        var pk = new PrimaryKey();
                        pk.Columns.Add(new ColumnReference(col.ColumnName));
                        table._Constraints.Add(pk);
                    }
                }
                else
                {
                    var col = new ColumnStructure();
                    col.ColumnName = row.Cells[0].Value.ToString();
                    col.DataType = new DbTypeString(250);
                    table._Columns.Add(col);
                }
            }
            return table;
        }

        //void GotRowFormat(IAsyncResult async)
        //{
        //    lbsource.Enabled = true;
        //    lbtarget.Enabled = true;
        //    DataTable rowformat = m_source.EndGetRowFormat(async);
        //    foreach (DataRow row in rowformat.Rows)
        //    {
        //        lbsource.Items.Add(row["ColumnName"]);
        //    }
        //}

        public IRowTransform GetTransform()
        {
            bool ispermute = true;
            bool iscolumn = false;
            foreach (DataGridViewRow row in lbtarget.Rows)
            {
                var type = GetExprType(row);
                if (type == null) continue;
                if (!(type is GenericTransform.ColumnColExprType)) ispermute = false;
                iscolumn = true;
            }
            if (!iscolumn) throw new InternalError("DAE-00372 no columns");
            if (ispermute)
            {
                List<int> colindexes = new List<int>();
                foreach (DataGridViewRow row in lbtarget.Rows)
                {
                    var type = GetExprType(row);
                    if (type == null) continue;
                    int index = m_srcformat.Columns.GetIndex(row.Cells[2].Value.ToString());
                    if (index < 0) throw new InternalError("DAE-00012 Undefined column:" + row.Cells[2].Value.ToString());
                    colindexes.Add(index);
                }
                return new PermuteTransform(m_srcformat, CreateTargetStructure(), colindexes);
            }
            else
            {
                List<GenericTransform.ColExpr> cols = new List<GenericTransform.ColExpr>();
                foreach (DataGridViewRow row in lbtarget.Rows)
                {
                    var type = GetExprType(row);
                    if (type == null) continue;
                    cols.Add(new GenericTransform.ColExpr
                    {
                        Type = type,
                        Expression = row.Cells[2].Value.SafeToString(),
                        Name = row.Cells[0].Value.ToString()
                    });
                }
                GenericTransform gt = new GenericTransform(m_srcformat, CreateTargetStructure(), cols);
                return gt;
            }
        }

        public ITableStructure GetTargetRowFormat()
        {
            return CreateTargetStructure();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (lbsource.SelectedIndex >= 0)
            {
                lbtarget.Rows.Add(lbsource.SelectedItem, new GenericTransform.ColumnColExprType().ToString(), lbsource.SelectedItem);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            lbtarget.DeleteRowsWithSelectedCells();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            lbtarget.MoveRowUp();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            lbtarget.MoveRowDown();
        }

        private void lbsource_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode){
                case Keys.Right:
                    lbtarget.Focus();
                    e.Handled = true;
                    break;
                case Keys.Insert:
                    button1_Click(sender, e);
                    break;
            }
        }

        private void lbtarget_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Left:
                    lbsource.Focus();
                    e.Handled = true;
                    break;
                case Keys.Delete:
                    if (e.Control)
                    {
                        button2_Click(sender, e);
                    }
                    break;
                case Keys.Up:
                    if (e.Control)
                    {
                        button3_Click(sender, e);
                        e.Handled = true;
                    }
                    break;
                case Keys.Down:
                    if (e.Control)
                    {
                        button4_Click(sender, e);
                        e.Handled = true;
                    }
                    break;
            }
        }

        public ITableStructure SourceOnInput { get { return m_srcformat; } }
        public ITableStructure TargetOnInput { get { return null; } }

        public void LoadFromTransform(IRowTransform tr)
        {
            lbtarget.Rows.Clear();
            if (tr is IdentityTransform)
            {
                foreach (IColumnStructure col in m_srcformat.Columns)
                {
                    lbtarget.Rows.Add(col.ColumnName, new GenericTransform.ColumnColExprType().ToString(), col.ColumnName);
                }
            }
            if (tr is PermuteTransform)
            {
                var t = tr as PermuteTransform;
                foreach (int i in t.DestIndexes)
                {
                    IColumnStructure col = m_srcformat.Columns[i];
                    lbtarget.Rows.Add(col.ColumnName, new GenericTransform.ColumnColExprType().ToString(), col.ColumnName);
                }
            }
            if (tr is GenericTransform)
            {
                var t = tr as GenericTransform;
                foreach (var col in t.DestCols)
                {
                    lbtarget.Rows.Add(col.Name, col.Type.ToString(), col.Expression);
                }
            }
        }
    }
}
