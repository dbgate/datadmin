using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace DatAdmin
{
    public partial class StringObjectFilterItemFrame : UserControl
    {
        StringObjectFilterItem m_item;

        public StringObjectFilterItemFrame(StringObjectFilterItem item)
        {
            InitializeComponent();
            m_item = item;
            tbxCondition.Text = m_item.DefinedVal ?? "";
            chbRegex.Checked = m_item.IsRegex;
            chbEnabled.Text = Texts.Get(m_item.PropertyTitle);
            chbEnabled.Checked = m_item.Enabled;
            chbEnabled_CheckedChanged(this, EventArgs.Empty);
        }

        private void tbxCondition_TextChanged(object sender, EventArgs e)
        {
            m_item.DefinedVal = tbxCondition.Text;
        }

        private void chbRegex_CheckedChanged(object sender, EventArgs e)
        {
            m_item.IsRegex = chbRegex.Checked;
        }

        private void chbEnabled_CheckedChanged(object sender, EventArgs e)
        {
            m_item.Enabled = chbEnabled.Checked;
            chbRegex.Enabled = tbxCondition.Enabled = m_item.Enabled;
        }
    }

    public class StringObjectFilterItem : ObjectFilterItemBase
    {
        [XmlElem]
        public bool IsRegex { get; set; }
        [XmlElem]
        public string DefinedVal { get; set; }

        public override Control CreateEditor()
        {
            return new StringObjectFilterItemFrame(this);
        }

        public override bool Accept(string value)
        {
            string definedVal = DefinedVal ?? "";
            if (value.IsEmpty()) return definedVal.IsEmpty();
            if (IsRegex)
            {
                return Regex.Match(value, definedVal).Success;
            }
            else
            {
                return value.Trim() == definedVal.Trim();
            }
        }

        public string PredefinedValue
        {
            set
            {
                IsRegex = false;
                DefinedVal = value;
                Enabled = (value != null);
            }
        }
    }
}
