using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class GridWidgetFrame : WidgetBaseFrame
    {
        DataTable m_table;
        DatAdmin.Scripting.ObjectGrid m_grid;
        ColumnInfo[] m_cols;

        public GridWidgetFrame(IGridWidget view)
            : base(view)
        {
            InitializeComponent();
        }

        IGridWidget Widget { get { return (IGridWidget)m_widget; } }

        protected override void ShowDataInGui()
        {
            if (m_grid != null)
            {
                dataGridView1.Rows.Clear();
                dataGridView1.Columns.Clear();
                foreach (string col in m_grid.Columns) dataGridView1.Columns.Add(col, col);
                foreach (object[] row in m_grid.Rows)
                {
                    try
                    {
                        dataGridView1.Rows.Add(row);
                    }
                    catch { }
                }
            }
            else
            {
                m_cols = null;

                if (m_table != null)
                {
                    m_table = NormalizeTable(m_table);
                    m_cols = new ColumnInfo[m_table.Columns.Count];

                    foreach (DataColumn col in m_table.Columns)
                    {
                        if (col.ColumnName.Contains("#"))
                        {
                            int index = col.ColumnName.IndexOf("#");
                            m_cols[col.Ordinal].Type = col.ColumnName.Substring(index + 1);
                            col.ColumnName = col.ColumnName.Substring(0, index);
                        }
                    }
                }
                dataGridView1.DataSource = m_table;
            }
        }

        private DataTable NormalizeTable(DataTable table)
        {
            bool recreate = false;
            foreach (DataColumn col in table.Columns)
            {
                if (col.DataType == typeof(byte[]))
                {
                    recreate = true;
                }
            }
            if (!recreate) return table;
            var res = new DataTable();
            foreach (DataColumn col in table.Columns)
            {
                var nc = new DataColumn(col.ColumnName, col.DataType == typeof(byte[]) ? typeof(string) : col.DataType);
                res.Columns.Add(nc);
            }
            foreach (DataRow row in table.Rows)
            {
                var nr = res.NewRow();
                res.Rows.Add(nr);
                for (int i = 0; i < res.Columns.Count; i++)
                {
                    if (res.Columns[i].DataType == table.Columns[i].DataType)
                    {
                        nr[i] = row[i];
                    }
                    else
                    {
                        if (row[i] == null || row[i] == DBNull.Value)
                        {
                            nr[i] = "(NULL)";
                        }
                        else
                        {
                            try { nr[i] = Encoding.UTF8.GetString((byte[])row[i]); }
                            catch { nr[i] = "(ERROR)"; }
                        }
                    }
                }
            }
            return res;
        }

        protected override void DoLoadData()
        {
            if (m_appobj == null)
            {
                m_table = null;
                m_grid = null;
            }
            else
            {
                Widget.GetData(m_appobj, ConnPack, out m_table, out m_grid);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DataRow row = ((DataRowView)dataGridView1.Rows[e.RowIndex].DataBoundItem).Row;
                HCellData.CallShowData(this, new GridDataHolder(null, e.ColumnIndex, e.RowIndex, dataGridView1, null, null));
            }
            catch (Exception)
            {
                try
                {
                    object value = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    HCellData.CallShowData(this, new ValueDataHolder(value));
                }
                catch (Exception)
                {
                }
            }
        }

        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
        }

        private string FormatToBF(uint val, uint div, string unit)
        {
            return string.Format("{0:0.00} {1}", (float)val / div, unit);
        }

        private string FormatToB(string from) //in B
        {
            try
            {
                uint data = uint.Parse(from);

                if (data / 1048576 > 0)
                    return FormatToBF(data, 1048576, "MB");

                if (data / 1024 > 0)
                    return FormatToBF(data, 1024, "KB");

                return string.Format("{0} B", from);
            }
            catch (Exception)
            {
                return from;
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (m_cols != null)
            {
                string stype = m_cols[e.ColumnIndex].Type;
                if (stype == "BYTES")
                {
                    e.Value = FormatToB(e.Value.ToString());
                }
                if (stype == "INT")
                {
                    int val;
                    if (Int32.TryParse(e.Value.SafeToString() ?? "", out val))
                    {
                        e.Value = val.FormatInt();
                    }
                }
            }
        }

        struct ColumnInfo
        {
            internal string Type;
        }
    }
}
