using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class ReferencesCellDataFrame : CellDataFrameBase
    {
        public ReferencesCellDataFrame()
        {
            InitializeComponent();
        }

        public override void ShowCurrentData()
        {
            if (m_data.LookupInfo != null)
            {
                dataGridView1.DataSource = m_data.LookupInfo.Table;
                SelectLookupValue();
                Controls.ShowError(false);
            }
            else
            {
                dataGridView1.DataSource = null;
                Controls.ShowError(true);
            }
        }

        private void SelectLookupValue()
        {
            m_data.GetData(m_holder);
            string data = m_holder.GetValue().SafeToString();

            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                row.Selected = false;
            }

            if (data == null) return;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (row.Cells[m_data.LookupInfo.PkColIndex].Value.ToString() == data.ToString())
                {
                    dataGridView1.CurrentCell = row.Cells[0];
                    row.Selected = true;
                    break;
                }
            }
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            Logging.Warning("Data error, row={0}, col={1}, error={2}", e.RowIndex, e.ColumnIndex, e.Exception.Message);
        }

        private void dataGridView1_Sorted(object sender, EventArgs e)
        {
            SelectLookupValue();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            SendValue();
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendValue();
                e.Handled = true;
            }
        }

        private void SendValue()
        {
            if (m_loadingData) return;
            if (dataGridView1.DataSource == null) return;
            if (m_data == null || m_data.LookupInfo == null) return;
            if (dataGridView1.SelectedRows.Count != 1) return;
            DataTable tbl = (DataTable)dataGridView1.DataSource;
            DataGridViewRow row = dataGridView1.SelectedRows[0];
            object value = row.Cells[m_data.LookupInfo.PkColIndex].Value;
            BedValueHolder h1 = new BedValueHolder(), h2 = new BedValueHolder();
            h1.ReadFrom(value);
            m_data.BedConvertor.ConvertValue(h1, m_targetType, h2);
            m_data.SetData(h2);
        }
    }

    [CellDataEditor(Name = "references", Title = "References")]
    public class ReferencesCellDataEditor : CellDataEditorBase
    {
        public override string MenuTitle
        {
            get { return "s_referenced_data"; }
        }

        public override Bitmap Icon
        {
            get { return CoreIcons.references; }
        }

        protected override CellDataFrameBase CreateControl()
        {
            return new ReferencesCellDataFrame();
        }

        public override int SupportLevel(IDataHolder data, IBedValueReader holder)
        {
            if (data.LookupInfo != null) return 10;
            return 0;
        }
    }
}
