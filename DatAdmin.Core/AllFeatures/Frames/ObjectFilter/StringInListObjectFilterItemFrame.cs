using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace DatAdmin
{
    public partial class StringInListObjectFilterItemFrame : UserControl
    {
        StringInListObjectFilterItem m_item;

        public StringInListObjectFilterItemFrame(StringInListObjectFilterItem item)
        {
            InitializeComponent();
            m_item = item;

            chbEnabled.Text = Texts.Get(m_item.PropertyTitle);
            chbEnabled.Checked = m_item.Enabled;
            tbxList.Text = m_item.Items.CreateDelimitedText("\r\n");
            tbxRegexVal.Text = m_item.RegexVal;
            rbtRegex.Checked = m_item.IsRegex;
            rbtList.Checked = !m_item.IsRegex;
            chbEnabled_CheckedChanged(this, EventArgs.Empty);
            rbtRegex_CheckedChanged(this, EventArgs.Empty);
        }

        private void chbEnabled_CheckedChanged(object sender, EventArgs e)
        {
            m_item.Enabled = chbEnabled.Checked;
            rbtList.Enabled = rbtRegex.Enabled = tbxList.Enabled = tbxRegexVal.Enabled = chbEnabled.Checked;
        }

        private void rbtRegex_CheckedChanged(object sender, EventArgs e)
        {
            m_item.IsRegex = rbtRegex.Checked;
            tbxRegexVal.Enabled = m_item.IsRegex;
            tbxList.Enabled = !m_item.IsRegex;
        }

        private void tbxRegexVal_TextChanged(object sender, EventArgs e)
        {
            m_item.RegexVal = tbxRegexVal.Text;
        }

        private void tbxList_TextChanged(object sender, EventArgs e)
        {
            m_item.Items = (from s in tbxList.Text.Split('\n') select s.Trim()).ToList();
        }
    }

    public class StringInListObjectFilterItem : ObjectFilterItemBase
    {
        public StringInListObjectFilterItem()
        {
            Items = new List<string>();
        }

        [XmlElem]
        public bool IsRegex { get; set; }
        [XmlElem]
        public string RegexVal { get; set; }
        [XmlCollection(typeof(string))]
        public List<string> Items { get; set; }

        public override Control CreateEditor()
        {
            return new StringInListObjectFilterItemFrame(this);
        }

        public override bool Accept(string value)
        {
            if (IsRegex)
            {
                string regexVal = RegexVal ?? "";
                if (value.IsEmpty()) return regexVal.IsEmpty();
                return Regex.Match(value, regexVal).Success;
            }
            else
            {
                if (Items == null) return true;
                value = (value ?? "").Trim();
                foreach (string item in Items)
                {
                    if (String.Compare(item.Trim(), value, true) == 0) return true;
                }
                return false;
            }
        }
    }
}
