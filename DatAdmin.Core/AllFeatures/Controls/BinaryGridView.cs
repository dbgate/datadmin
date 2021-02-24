using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

namespace DatAdmin
{
    public class BinaryGridView : DataGridView
    {
        BedTable m_data;
        TColumnDisplay<IColumnStructure> m_colDisplay;

        [Browsable(false)]
        public int HighlightRow { get; set; }
        [Browsable(false)]
        public int HighlightColumn { get; set; }

        public bool HightlightVisible;

        public BinaryGridView()
        {
            base.VirtualMode = true;
            base.AllowUserToAddRows = false;
            DoubleBuffered = true;

            HighlightRow = -1;
            HighlightColumn = -1;
        }

        public new bool VirtualMode { get { return true; } set { } }
        public new bool AllowUserToAddRows { get { return false; } set { } }

        public new BedTable DataSource
        {
            get { return m_data; }
            set
            {
                BedTable oldtbl = m_data;
                if (oldtbl != null)
                {
                    oldtbl.RemovedRow -= new BedRowEventHandler(table_RemovedRow);
                    oldtbl.AddedRow -= new BedRowEventHandler(table_AddedRow);
                }
                m_data = value;
                m_colDisplay = null;

                if (value != null)
                {
                    value.RemovedRow += new BedRowEventHandler(table_RemovedRow);
                    value.AddedRow += new BedRowEventHandler(table_AddedRow);
                    m_colDisplay = m_data.GetColumnDisplay();
                }

                ReloadData();
            }
        }

        void table_AddedRow(object sender, BedRowEventArgs e)
        {
            InvalidateDataRow(e.Row);
            RowCount = DataSource.Rows.Count;
        }

        void table_RemovedRow(object sender, BedRowEventArgs e)
        {
            InvalidateDataRow(e.Row);
            RowCount = DataSource.Rows.Count;
        }

        protected void InvalidateDataRow(BedRow dataRow)
        {
            int index = DataSource.Rows.IndexOf(dataRow);
            if (index >= 0 && index < RowCount) this.InvalidateRow(index);
        }

        public BedRow GetRow(int rowindex)
        {
            if (DataSource == null) return null;
            return DataSource.Rows[rowindex];
        }

        public int PopupRow
        {
            get
            {
                if (HighlightColumn > 0) return -1;
                if (HighlightRow >= 0) return HighlightRow;
                if (CurrentCell != null) return CurrentCell.RowIndex;
                return -1;
            }
        }

        public int PopupColumn
        {
            get
            {
                if (HighlightRow > 0) return -1;
                if (HighlightColumn >= 0) return HighlightColumn;
                if (CurrentCell != null) return CurrentCell.ColumnIndex;
                return -1;
            }
        }

        public BedRow GetCurrentRow()
        {
            if (CurrentCell == null) return null;
            return GetRow(CurrentCell.RowIndex);
        }

        public BedRow GetPopupRow()
        {
            if (PopupRow >= 0) return GetRow(PopupRow);
            return null;
        }

        public TColumnDisplay<IColumnStructure> ColumnDisplay { get { return m_colDisplay; } }

        public bool IgnoreSelectionChanged;

        private void ReloadData()
        {
            try
            {
                IgnoreSelectionChanged = true;
                RowCount = 0;
                Columns.Clear();
                if (m_data != null)
                {
                    RowCount = 0;
                    Columns.Clear();
                    var disp = m_data.GetColumnDisplay();
                    foreach (var col in disp)
                    {
                        DataGridViewColumn dcol = new DataGridViewTextBoxColumn();
                        dcol.HeaderText = col.ValueRef.HeaderTitle;
                        dcol.Name = col.ValueRef.HeaderTitle;
                        dcol.Tag = col.ValueRef;
                        Columns.Add(dcol);
                    }
                    RowCount = m_data.Rows.Count;
                }
            }
            finally
            {
                IgnoreSelectionChanged = false;
            }
        }

        protected override void OnCellValueNeeded(DataGridViewCellValueEventArgs e)
        {
            if (m_data != null && e.ColumnIndex < m_data.Structure.Columns.Count && e.RowIndex < m_data.Rows.Count)
            {
                e.Value = m_data.Rows[e.RowIndex].GetFormattedValue(e.ColumnIndex);
            }
            base.OnCellValueNeeded(e);
        }

        protected override void OnUserDeletingRow(DataGridViewRowCancelEventArgs e)
        {
            base.OnUserDeletingRow(e);
            e.Cancel = true;
            DataSource.Rows.RemoveAt(e.Row.Index);
            InvalidateRow(e.Row.Index);
        }

        protected override void OnCellBeginEdit(DataGridViewCellCancelEventArgs e)
        {
            base.OnCellBeginEdit(e);
            if (m_data != null && e.ColumnIndex >= 0 && e.ColumnIndex < m_data.Structure.Columns.Count)
            {
                var type = m_data.Structure.Columns[e.ColumnIndex].DataType.DefaultStorage;
                if (type == TypeStorage.ByteArray) e.Cancel = true;
                if (m_colDisplay[e.ColumnIndex].ValueIsReadOnly) e.Cancel = true;
            }
        }

        protected override void OnCellEndEdit(System.Windows.Forms.DataGridViewCellEventArgs e)
        {
            base.OnCellEndEdit(e);
            if (Core.IsMono)
            {
                // MONO fake, OnCellValuePushed is no called
                PushedValue(e.RowIndex, e.ColumnIndex, Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
            }
        }

        private void PushedValue(int row, int col, object value)
        {
            if (m_data != null)
            {
                try
                {
                    m_data.Rows[row][col] = value;
                }
                catch (Exception err)
                {
                    StdDialog.ShowError(err.Message);
                }
            }
            InvalidateRow(row);
        }

        protected override void OnCellValuePushed(DataGridViewCellValueEventArgs e)
        {
            base.OnCellValuePushed(e);
            PushedValue(e.RowIndex, e.ColumnIndex, e.Value);
        }
    }
}
