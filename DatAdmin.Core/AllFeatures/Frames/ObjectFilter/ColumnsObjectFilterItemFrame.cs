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
    public partial class ColumnsObjectFilterItemFrame : UserControl
    {
        ColumnsObjectFilterItem m_item;
        public ColumnsObjectFilterItemFrame(ColumnsObjectFilterItem item)
        {
            InitializeComponent();
            m_item = item;

            chbEnabled.Text = Texts.Get(m_item.PropertyTitle);
            chbEnabled.Checked = m_item.Enabled;
            tbxList.Text = m_item.Columns.CreateDelimitedText("\r\n");
            chbEnabled_CheckedChanged(this, EventArgs.Empty);
        }

        private void chbEnabled_CheckedChanged(object sender, EventArgs e)
        {
            m_item.Enabled = chbEnabled.Checked;
            tbxList.Enabled = chbEnabled.Checked;
        }

        private void tbxList_TextChanged(object sender, EventArgs e)
        {
            m_item.Columns = (from s in tbxList.Text.Split('\n') select s.Trim()).ToList();
        }
    }

    public class ColumnsObjectFilterItem : ObjectFilterItemBase
    {
        public ColumnsObjectFilterItem()
        {
            Columns = new List<string>();
        }

        [XmlCollection(typeof(string))]
        public List<string> Columns { get; set; }

        public override Control CreateEditor()
        {
            return new ColumnsObjectFilterItemFrame(this);
        }

        public override bool Accept(string value)
        {
            string[] cols = value.Split('|');
            if (Columns == null) return true;

            foreach (string col in Columns)
            {
                bool found = false;
                foreach (string col2 in cols)
                {
                    if (String.Compare(col, col2, true) != 0)
                    {
                        found = true;
                        break;
                    }
                }
                if (!found) return false;
            }
            return true;
        }

        public IEnumerable<string> PredefinedValue
        {
            set
            {
                Enabled = (value != null);
                Columns = null;
                if (value != null) Columns = new List<string>(value);
            }
        }
    }
}
