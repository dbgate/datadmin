using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class DbInfoWallEditor : UserControl
    {
        DbInfoWallWidget m_widget;

        public DbInfoWallEditor(DbInfoWallWidget widget)
        {
            InitializeComponent();
            m_widget = widget;
            foreach (var item in m_widget.Items)
            {
                dataGridView1.Rows.Add(item.Name, item.Value);
            }
        }

        public void SaveItems()
        {
            m_widget.Items.Clear();
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                string name = row.Cells[0].Value.SafeToString();
                string value = row.Cells[1].Value.SafeToString();
                if (!name.IsEmpty() && !value.IsEmpty())
                {
                    m_widget.Items.Add(new DbInfoWallWidgetItem
                    {
                        Name = name,
                        Value = value,
                    });
                }
            }
        }
    }
}
