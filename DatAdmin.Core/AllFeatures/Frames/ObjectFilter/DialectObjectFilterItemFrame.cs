using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DatAdmin
{
    public partial class DialectObjectFilterItemFrame : UserControl
    {
        DialectObjectFilterItem m_item;

        public DialectObjectFilterItemFrame(DialectObjectFilterItem item)
        {
            InitializeComponent();
            m_item = item;
            chbEnabled.Checked = m_item.Enabled;
            chbEnabled.Text = Texts.Get(m_item.PropertyTitle);
            var dials = DialectAddonType.GetAllDialects(false);
            foreach (var dialect in dials)
            {
                cbxDialect.Items.Add(dialect);
            }
            if (m_item.DialectName != null)
            {
                cbxDialect.SelectedIndex = dials.IndexOfIf(d => d.DialectName == m_item.DialectName);
            }
            chbEnabled_CheckedChanged(this, EventArgs.Empty);
        }

        private void chbEnabled_CheckedChanged(object sender, EventArgs e)
        {
            m_item.Enabled = chbEnabled.Checked;
            cbxDialect.Enabled = m_item.Enabled;
        }

        private void cbxDialect_SelectedIndexChanged(object sender, EventArgs e)
        {
            var dial = cbxDialect.SelectedItem as ISqlDialect;
            m_item.DialectName = dial != null ? dial.DialectName : null;
        }
    }

    public class DialectObjectFilterItem : ObjectFilterItemBase
    {
        [XmlElem]
        public string DialectName { get; set; }

        public override Control CreateEditor()
        {
            return new DialectObjectFilterItemFrame(this);
        }

        public override bool Accept(string value)
        {
            return value == DialectName;
        }

        public string PredefinedValue
        {
            set
            {
                DialectName = value;
                Enabled = (value != null);
            }
        }
    }
}
