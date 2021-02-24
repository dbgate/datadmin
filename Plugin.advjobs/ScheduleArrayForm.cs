using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DatAdmin;

namespace Plugin.advjobs
{
    public partial class ScheduleArrayForm : FormEx
    {
        string[] m_items;
        int m_offset;
        int m_itemCount;
        List<CheckBox> m_checkBoxes = new List<CheckBox>();

        public ScheduleArrayForm(int rows, int cols, int itemCount, string[] items, int offset, bool every, ScheduleArray initValue)
        {
            InitializeComponent();
            Height += rows * btnOk.Height;
            m_items = items;
            m_offset = offset;
            m_itemCount = itemCount;
            for (int i = 0; i < itemCount; i++)
            {
                var chb = new CheckBox();
                chb.Text = (m_items != null ? m_items[i] : (i + m_offset).ToString());
                m_checkBoxes.Add(chb);
                panel1.Controls.Add(chb);
                chb.Left = panel1.Width / cols * (i % cols);
                chb.Top = panel1.Height / rows * (i / cols);
                chb.AutoSize = true;
            }
            btnEvery10.Visible = btnEvery15.Visible = btnEvery5.Visible = every;
            foreach (int i in initValue.Items)
            {
                m_checkBoxes[i - m_offset].Checked = true;
            }
        }

        private ScheduleArray CheckedItems
        {
            get
            {
                var res = new ScheduleArray(m_offset, m_offset + m_itemCount - 1);
                for (int i = 0; i < m_checkBoxes.Count; i++)
                {
                    if (m_checkBoxes[i].Checked) res.Items.Add(i + m_offset);
                }
                res.Items.Sort();
                return res;
            }
        }

        private void CheckEvery_Click(object sender, EventArgs e)
        {
            int every = Int32.Parse(((Button)sender).Tag.ToString());
            foreach (var chb in m_checkBoxes) chb.Checked = false;
            for (int i = 0; i < m_checkBoxes.Count; i += every) m_checkBoxes[i].Checked = true;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            foreach (var chb in m_checkBoxes) chb.Checked = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            foreach (var chb in m_checkBoxes) chb.Checked = true;
        }

        public static ScheduleArray Run(int rows, int cols, int itemCount, string[] items, int offset, bool every, ScheduleArray initValue)
        {
            var win = new ScheduleArrayForm(rows, cols, itemCount, items, offset, every, initValue);
            if (win.ShowDialogEx() == DialogResult.OK)
            {
                return win.CheckedItems;
            }
            return null;
        }
    }
}
